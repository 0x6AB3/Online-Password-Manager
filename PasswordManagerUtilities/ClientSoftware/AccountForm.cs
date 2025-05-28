using System;
using System.Text;
using System.Windows.Forms;
using PasswordManagerUtilities;

namespace ClientSoftware
{
    public partial class AccountForm : Form
    {
        // Attributes about current account
        public Account account;
        public bool changed;
        private byte[] internalKey;

        public AccountForm(byte[] key, Account accountToUse = null)
        {
            account = accountToUse;
            internalKey = key;

            changed = false;

            InitializeComponent();

            // Creating a new account object if there is no specified account to use (user created a new account instead of editting a previous one).
            if (accountToUse == null)
            {
                account = new Account();
                return;
            }

            // decrypting data about the current account and displaying it to the form.
            string accountName = GetDecryptedData(account.AccountName, internalKey);
            string username = GetDecryptedData(account.Username, internalKey);
            string password = GetDecryptedData(account.Password, internalKey);
            string note = GetDecryptedData(account.Note, internalKey);

            txtAccountName.Text = accountName;
            txtUsername.Text = username;
            txtPassword.Text = password;
            txtNote.Text = note;
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // Encrypting the data in the account form input fields and saving it to the vault.
            changed = true;
            account.AccountName = GetEncryptedData(txtAccountName.Text, internalKey);
            account.Username = GetEncryptedData(txtUsername.Text, internalKey);
            account.Password = GetEncryptedData(txtPassword.Text, internalKey);
            account.Note = GetEncryptedData(txtNote.Text, internalKey);
            MessageBox.Show("Account has been saved succesfully!", "Success", MessageBoxButtons.OK);
            this.Close();
        }

        private AccountData GetEncryptedData(string data, byte[] key)
        {
            // Encrypting the specified string with the internal key.
            byte[] rawData = UTF8Encoding.UTF8.GetBytes(data);
            
            AES_CBC blockCipher = new AES_CBC();
            blockCipher.Key = key;
            blockCipher.GenerateIV();
            
            AccountData encryptedData = new AccountData(blockCipher.Encrypt(rawData), blockCipher.IV);
            return encryptedData;
        }

        private string GetDecryptedData(AccountData encryptedData, byte[] key)
        {
            // Decrypting the account data with the internal key and returning it as a string.
            AES_CBC blockCipher = new AES_CBC();
            blockCipher.Key = key;
            blockCipher.IV = encryptedData.IV;
            
            byte[] decryptedData = blockCipher.Decrypt(encryptedData.Data);
            return UTF8Encoding.UTF8.GetString(decryptedData);
        }

        // Copying data from input fields into clipboard.
        private void btnCopyUsername_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtUsername.Text);
        }

        private void btnCopyPassword_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPassword.Text);
        }

        // Switching between visible and concealed text display.
        private void cbxShowUsername_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowUsername.Checked)
            {
                txtUsername.PasswordChar = '\0';
                return;
            }
            txtUsername.PasswordChar = '•';
        }

        private void cbxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
                return;
            }
            txtPassword.PasswordChar = '•';
        }

        // Not allowing the user to save the account if any input fields are left empty (otherwise and error would occur).
        private void txtAccountName_TextChanged(object sender, EventArgs e)
        {
            DisableSaveIfEmpty();
        }
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            DisableSaveIfEmpty();
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            DisableSaveIfEmpty();
        }
        private void txtNote_TextChanged(object sender, EventArgs e)
        {
            DisableSaveIfEmpty();
        }

        private void DisableSaveIfEmpty()
        {
            if (txtAccountName.Text == "" || txtUsername.Text == "" || txtPassword.Text == "" || txtNote.Text == "")
            {
                btnSaveChanges.Enabled = false;
                return;
            }
            btnSaveChanges.Enabled = true;
        }

        // Instantiation of a password generator form.
        private void btnPasswordGen_Click(object sender, EventArgs e)
        {
            PasswordGeneratorForm generator = new PasswordGeneratorForm();
            generator.ShowDialog();
        }
    }
}
