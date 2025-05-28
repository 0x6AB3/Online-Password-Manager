using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using PasswordManagerUtilities;
using Newtonsoft.Json;

namespace ClientSoftware
{
    public partial class VaultViewForm : Form
    {
        // Cryptographic keys used in decrypting vaults.
        private byte[] vaultKey;
        private byte[] internalKey;

        // Currently selected vault and the account list corresponding to it.
        private Vault selectedVault;
        private List<AccountBox> accounts;
        private string selectedVaultName;

        // The client object that is used to interact with the connected server.
        private Client client;

        public VaultViewForm(byte[] key, ref Client connection)
        {
            InitializeComponent();

            accounts = new List<AccountBox>();

            vaultKey = key;
            client = connection;

            // Disabling server interaction buttons if there is no client object is passed.
            if (client == null)
            {
                DisableServerInteraction();
            }
            else
            {
                // Informing the server that form instantiation took place and requesting a list of available vaults.
                client.SendCommand("CONTINUE");
                GetServerVaults();

                lblConnectionStatus.Text = "Connected";
                lblConnectionStatus.ForeColor = Color.Lime;
                PollServer.Enabled = true;
            }

            // Checking the application data folder in order to locate any existing vaults.
            string VaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PasswordVaults";

            Directory.CreateDirectory(VaultDirectory);
            Directory.SetCurrentDirectory(VaultDirectory);
            
            RefreshVaults();
        }


        // --------------- FORM EVENTS --------------- 
        private void btnNewVault_Click(object sender, EventArgs e)
        {
            // Prompting user to save their changes and disabling vault interactions.
            CheckChanges();
            VaultSelected(false);

            // Creating a new vault with a random internal key.
            Vault newVault = new Vault(vaultKey);
            byte[] rawVault = UTF8Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newVault, Formatting.Indented));

            // Allowing user to assign name to the file in which the vault is stored.
            // The cmbVaultSelection.Items list is passed as a parameter as it contains already existing vault names which can't be used.
            NewVaultForm vaultName = new NewVaultForm(cmbVaultSelection.Items);
            vaultName.ShowDialog();

            // Checking if a name inputted by the user.
            if (vaultName.vaultName != null)
            {
                // Creating the vault file and writing the created vault to it.
                string path = Directory.GetCurrentDirectory() + "\\" + vaultName.vaultName;
                File.WriteAllBytes(path, rawVault);
                MessageBox.Show("Vault has successfully been created, it is now accessible in your local vaults.", "Vault Created", MessageBoxButtons.OK);
            }

            // Refreshing the vault combo box.
            RefreshVaults();
        }

        private void btnDeleteVault_Click(object sender, EventArgs e)
        {
            // User confirmation.
            DialogResult result = MessageBox.Show("Are you sure you would like to delete this vault?", "Delete Vault", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                // Disabling vault interaction as no vault will be selected after the current vault is deleted.
                VaultSelected(false);

                // Deleting the vault and informing the user.
                File.Delete(GetVaultPath());
                MessageBox.Show("The vault has been succesfully deleted", "Delete Success", MessageBoxButtons.OK);

                cmbVaultSelection.Text = "";
                selectedVault = null;

                // Removing the account boxes of the deleted vault from the form and refreshing the vault combo box.
                RemoveAccountBoxes();
                RefreshVaults();
            }
        }
       
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            // Creating an AccountForm in order to get input from the user.
            AccountForm newAccount = new AccountForm(internalKey);
            newAccount.ShowDialog();

            // Account wont be created if no changes were made.
            if (newAccount.changed)
            {
                // As changes were made, the user can choose to save changes.
                btnSaveChanges.Enabled = true;

                // Adding account to the currently selected vault and refreshing the account boxes that are displayed on the form.
                selectedVault.AddAccount(newAccount.account);
                RefreshAccounts();
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // Disabling button interaction
            btnSaveChanges.Enabled = false;
            
            // Getting the Json string of the currently selected vault and writing it to the file that it is stored in.
            string vault = JsonConvert.SerializeObject(selectedVault, Formatting.Indented);
            File.WriteAllText(GetVaultPath(), vault);

            MessageBox.Show("Changes saved successfully", "Saving Success", MessageBoxButtons.OK);
        }

        private void cmbVaultSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Checking if changes were made to the previous vault and removing all existing AccountBox instances.
            CheckChanges();
            VaultSelected(false);
            RemoveAccountBoxes();

            selectedVaultName = cmbVaultSelection.Text;
            btnDeleteVault.Enabled = true;

            string vaultLocation = Directory.GetCurrentDirectory() + "\\" + cmbVaultSelection.Items[cmbVaultSelection.SelectedIndex];

            // Reading the vault file and checking if empty.
            byte[] rawVault = File.ReadAllBytes(vaultLocation);
            if (rawVault.Length == 0)
            {
                MessageBox.Show("Error: Vault has no data", "Vault Error", MessageBoxButtons.OK);
                return;
            }

            // Converting the raw vault data to a UTF8 Json string.
            string vaultString = UTF8Encoding.UTF8.GetString(rawVault);

            try
            {
                // Attempting to open the vault using the vault key.
                selectedVault = JsonConvert.DeserializeObject<Vault>(vaultString);
                internalKey = selectedVault.GetInternalKey(vaultKey);
                RefreshAccounts();
                btnAddAccount.Enabled = true;
            }
            catch
            {
                // Informing user of error.
                MessageBox.Show("Error: Couldn't retrieve vault (Vault is corrupt/Incorrect login)", "Vault Error", MessageBoxButtons.OK);
                btnAddAccount.Enabled = false;
                return;
            }
            VaultSelected(true);
        }

        private void btnUploadVault_Click(object sender, EventArgs e)
        {
            // Opening the vault and sending it to the server.
            string name = cmbVaultSelection.Text;
            string vaultPath = Directory.GetCurrentDirectory() + $@"\{name}";
            byte[] vaultData = File.ReadAllBytes(vaultPath);
            string result = client.SendCommand("UPLOAD", vaultName: name, rawVault: vaultData);

            // Asking the user if they wish to override the vault stored on the server if it has the same name.
            if (result == "ERROR VAULT_EXISTS")
            {
                DialogResult confirmation = MessageBox.Show("This vault already exists on the server, it will be overwritten if you continue", "Vault Exists", MessageBoxButtons.OKCancel);
                if (confirmation == DialogResult.OK)
                {
                    result = client.SendCommand("CONTINUE");
                    ServerVaultSelected(false);
                }
                else
                {
                    client.SendCommand("CANCEL");
                    return;
                }
            }
            // Informing user of the upload outcome.
            switch (result)
            {
                case "SUCCESS":
                    MessageBox.Show("Success: Vault successfully uploaded", "Success", MessageBoxButtons.OK);
                    break;

                case "ERROR FAILED":
                    MessageBox.Show("Error: Failed to upload vault", "Error", MessageBoxButtons.OK);
                    break;
            }

            GetServerVaults();

        }

        private void btnDownloadVault_Click(object sender, EventArgs e)
        {
            // Receiving the Json string of the vault stored on the server.
            string vault = client.SendCommand("DOWNLOAD", vaultName: cmbServerVaultSelection.SelectedItem.ToString());

            // Infomring the user of failure.
            if (vault == "ERROR FAILED")
            {
                MessageBox.Show($"Error: Failed retrieving {cmbServerVaultSelection.SelectedItem.ToString()} from the server", "Download Error", MessageBoxButtons.OK);
                return;
            }

            // Saving the vault to the local device.
            string path = Directory.GetCurrentDirectory() + $@"\{cmbServerVaultSelection.SelectedItem.ToString()}";
            if (File.Exists(path))
            {
                // Querying the user about overriding a local vault with the same name.
                DialogResult result = MessageBox.Show("This vault already exists on your device, would you like to overwrite it with the downloaded vault?", "Overwrite?", MessageBoxButtons.OKCancel);

                if (result == DialogResult.Cancel)
                {
                    return;
                }

            }

            // Writing the Json string to the vault file.
            File.WriteAllBytes(path, UTF8Encoding.UTF8.GetBytes(vault));
            RefreshVaults();
            VaultSelected(false);
            MessageBox.Show($"Success: Vault has been successfully downloaded from the server", "Download Success", MessageBoxButtons.OK);
        }

        private void btnRemoveServerVault_Click(object sender, EventArgs e)
        {
            // Sending the DELETE command, followed by the vault name to the server.
            string result = client.SendCommand("DELETE", vaultName: cmbServerVaultSelection.SelectedItem.ToString());

            // Informing the user of the outcome.
            if (result == "SUCCESS")
            {
                MessageBox.Show("Success: vault has successfully been removed from the server", "Deletion Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error: Vault could not be deleted from the server", "Deletion Error", MessageBoxButtons.OK);
            }

            // Refreshing the list of vaults stored on the server.
            GetServerVaults();
            ServerVaultSelected(false);
        }

        private void btnPasswordGen_Click(object sender, EventArgs e)
        {
            // Instantiation of the password generator while in the vault view form.
            PasswordGeneratorForm generator = new PasswordGeneratorForm();
            generator.ShowDialog();
        }

        private void VaultViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Reminding user to save their changes before the program closes.
            CheckChanges();
        }

        private void cmbServerVaultSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServerVaultSelected(true);
        }

        private void PollServer_Tick(object sender, EventArgs e)
        {
            // Pinging the server to check if the client is still connected. An error will be caught if this is not the case and server interaction will be disabled.
            try
            {
                string result = client.SendCommand("PING");
            }
            catch
            {
                DisableServerInteraction();
            }
        }

        // --------------- FORM METHODS --------------- 
        private void RefreshVaults()
        {
            // Removing all entites from the combo box.
            cmbVaultSelection.Items.Clear();

            string[] vaults = Directory.GetFiles(Directory.GetCurrentDirectory());

            // Retreiving all vaults locally stored on the device and appending them to the combo box.
            foreach(string vault in vaults)
            {
                string[] vaultInfo = vault.Split(Directory.GetCurrentDirectory() + "\\");
                string vaultName = vaultInfo[1];
                if (vaultName.Substring(vaultName.Length - 6) == ".vault")
                {
                    cmbVaultSelection.Items.Add(vaultName);
                }
            }

            // Removing the account boxes of the previously seleted vault as the vault is no longer selected.
            RemoveAccountBoxes();
        }

        public void RefreshAccounts()
        {
            RemoveAccountBoxes();

            // Displaying all accounts in the currently selected vault on account boxes. The following math allows for 3 account boxes per row.
            for (int i = 0; i < selectedVault.Length; i++)
            {
                AccountBox accountBox = new AccountBox(this, selectedVault.GetAccount(i), internalKey, 12 + (i % 3) * 218, 165 + (i / 3) * 60);
                accounts.Add(accountBox);
            }
        }

        public void UpdateAccount(Account newAccount, Account oldAccount)
        {
            // Replacing the old account with the updated account into the vault.
            btnSaveChanges.Enabled = true;
            selectedVault.RemoveAccount(oldAccount);
            selectedVault.AddAccount(newAccount);
            RefreshAccounts();
        }

        public void RemoveAccount(Account account)
        {
            btnSaveChanges.Enabled = true;
            selectedVault.RemoveAccount(account);
            RefreshAccounts();
        }

        private void RemoveAccountBoxes()
        {
            // Removing all account boxes present in the form.
            foreach (AccountBox accountBox in accounts)
            {
                accountBox.Remove();
            }
        }

        private void CheckChanges()
        {
            // Informing the user that changes haven't been saved.
            if (btnSaveChanges.Enabled)
            {
                DialogResult result = MessageBox.Show("There are still changes which haven't been saved, would you like to save them now?", "Unsaved Changes", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    btnSaveChanges.PerformClick();
                    btnAddAccount.Enabled = btnDeleteVault.Enabled = false;
                    return;
                }
                else
                {
                    btnSaveChanges.Enabled = false;
                }
            }
        }

        private string GetVaultPath()
        {
            return Directory.GetCurrentDirectory() + "\\" + selectedVaultName;
        }

        private void DisableServerInteraction()
        {
            // Disabling all server related interactions on the form.
            PollServer.Enabled = false;
            VaultSelected(false);
            lblConnectionStatus.Text = "Disconnected";
            lblConnectionStatus.ForeColor = Color.Firebrick;
            cmbServerVaultSelection.Enabled = btnUploadVault.Enabled = btnDownloadVault.Enabled = btnRemoveServerVault.Enabled = false;
            client = null;
        }

        private void GetServerVaults()
        {
            cmbServerVaultSelection.Items.Clear();

            try
            {
                // Requesting server to send a list of vaults that are stored on the server and owned by the current client.
                string[] vaults = client.SendCommand("LIST").Split();

                // Appending all retrieved vault names to the server vault combo box.
                for (int i = 0; i < vaults.Length -1; i++)
                {
                    cmbServerVaultSelection.Items.Add(vaults[i]);
                }

            }
            catch
            {
                MessageBox.Show("Couldn't retreive vaults from server", "Error", MessageBoxButtons.OK);
            }
        }

        private void VaultSelected(bool selected)
        {
            // Disabling/enabling different interactable objects depending upon connection to the server and the currently selected vault.
            switch (selected, client is null)
            {
                case (false, false):
                    btnDeleteVault.Enabled = btnAddAccount.Enabled = btnUploadVault.Enabled = false;
                    break;

                case (true, false):
                    btnDeleteVault.Enabled =btnAddAccount.Enabled = btnUploadVault.Enabled = true;
                    break;

                case (false, true):
                    btnDeleteVault.Enabled = btnAddAccount.Enabled = false;
                    break;

                case (true, true):
                    btnDeleteVault.Enabled = btnAddAccount.Enabled = true;
                    break;
            }
        }

        private void ServerVaultSelected(bool selected)
        {
            // Enabling/disabling server related interactable objects depending on whether the client has selected a vault to download/delete.
            if (selected)
            {
                btnDownloadVault.Enabled = btnRemoveServerVault.Enabled = true;
                return;
            }
            btnDownloadVault.Enabled = btnRemoveServerVault.Enabled = false;
        }
    }

    // The account box class responsible for displaying the accounts contained inside of the currently selected vault.
    public class AccountBox
    {
        private PictureBox background;
        private Button open;
        private Button delete;
        private Label accountName;

        private VaultViewForm parentForm;
        private Account storedAccount;
        private byte[] internalKey;

        public AccountBox(VaultViewForm form, Account account, byte[] key, int x, int y)
        {
            parentForm = form;
            storedAccount = account;

            internalKey = key;

            background = new PictureBox();
            background.Size = new Size(200, 50);
            background.BackColor = Color.Black;
            background.BorderStyle = BorderStyle.FixedSingle;

            open = new Button();
            open.Size = new Size(background.Size.Width, background.Size.Height/2);
            open.Text = "Open";
            open.Click += Open_Click;
            open.FlatStyle = FlatStyle.Flat;
            open.BackColor = Color.Black;
            open.ForeColor = Color.White;
            open.BringToFront();

            delete = new Button();
            delete.Size = new Size(25, 25);
            delete.Text = "X";
            delete.FlatStyle = FlatStyle.Flat;
            delete.BackColor = Color.Black;
            delete.ForeColor = Color.Firebrick;
            delete.Click += Delete_Click;
            delete.BringToFront();

            // Decrypting and displaying the account name.
            AES_CBC blockCipher = new AES_CBC();
            blockCipher.Key = internalKey;
            blockCipher.IV = account.AccountName.IV;
            byte[] decryptedAccountName = blockCipher.Decrypt(account.AccountName.Data);
            accountName = new Label();
            accountName.Text = UTF8Encoding.UTF8.GetString(decryptedAccountName);
            accountName.Font = new Font(accountName.Font.FontFamily, 12);
            accountName.ForeColor = Color.White;
            accountName.BackColor = Color.Black;
            accountName.Size = new Size((background.Left+background.Width)-(delete.Left+delete.Width), background.Height - open.Height);

            ChangeLocation(x, y);

            parentForm.Controls.Add(background);
            parentForm.Controls.Add(open);
            parentForm.Controls.Add(delete);
            parentForm.Controls.Add(accountName);

            delete.BringToFront();
            accountName.BringToFront();
            open.BringToFront();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            // Instantiating a AccountForm in order to view and edit the selected account.
            AccountForm accountView = new AccountForm(internalKey, storedAccount);
            accountView.ShowDialog();

            // Saving changes if the account data was changed in any way.
            if (accountView.changed)
            {
                parentForm.UpdateAccount(accountView.account, storedAccount);
                storedAccount = accountView.account;
                Remove();
                
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            // Confirming that the user intends to delete the account.
            DialogResult result = MessageBox.Show("Are you sure you want to delete this account?", "Delete Account", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            // Removing both the account box from the form, and the account from the vault.
            Remove();
            parentForm.RemoveAccount(storedAccount);
            parentForm.RefreshAccounts();
        }

        // Removing the account box from the form.
        public void Remove()
        {
            parentForm.Controls.Remove(background);
            parentForm.Controls.Remove(open);
            parentForm.Controls.Remove(delete);
            parentForm.Controls.Remove(accountName);
        }

        public void ChangeLocation(int x, int y)
        {
            background.Left = x;
            background.Top = y;

            open.Left = background.Left;
            open.Top = background.Bottom - open.Size.Height;

            delete.Left = background.Left - delete.Size.Width/ 4;
            delete.Top = background.Top - delete.Size.Height / 4;

            accountName.Left = background.Left + background.Width - (accountName.Width) - 1;
            accountName.Top = background.Top+1;
        }

    }

}