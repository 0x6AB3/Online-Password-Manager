using System;
using System.Windows.Forms;
using PasswordManagerUtilities;

namespace ClientSoftware
{
    public partial class LoginForm : Form
    {
        // Attributes used for vault/server interaction.
        byte[] vaultKey, authenticationHash;
        Client client;

        public LoginForm()
        {
            InitializeComponent();

            client = null;
        }

        // Changing between hidden and visible characters for the password text box.
        private void cbxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxPassword.Checked)
            {
                txtPassword.PasswordChar = '•';
            }
            else
            {
                txtPassword.PasswordChar = '\0';
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!ValidLogin())
            {
                return;
            }

            // Attempting to send a SIGNUP command to the server.
            string result = CheckOffline("SIGNUP");

            // Opening the vault view form if successfully signed up, otherwise reporting the error to the user.
            switch (result)
            {
                case "SUCCESS":
                    OpenVaultView();
                    break;

                case "ERROR ACCOUNT_EXISTS":
                    MessageBox.Show("Error: An account with this username already exists", "Error", MessageBoxButtons.OK);
                    break;
            }
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            if (!ValidLogin())
            {
                return;
            }

            // Attempting to log in to the server with the inputted credentials.
            string result = CheckOffline("LOGIN");

            // Vault view form instantiation and error handling based on response from server.
            switch (result)
            {
                case "SUCCESS":
                    OpenVaultView();
                    break;

                case "ERROR INCORRECT_HASH":
                    MessageBox.Show("Error: Incorrect username/password", "Error", MessageBoxButtons.OK);
                    break;

                case "ERROR ACCOUNT_DOESNT_EXIST":
                    MessageBox.Show("Error: This account does not exist", "Error", MessageBoxButtons.OK);
                    break;
            } 
        }

        // Opening the vault view form with the given vault key and client object.
        private void OpenVaultView()
        {
            this.Hide();
            VaultViewForm vaultView = new VaultViewForm(vaultKey, ref client);
            vaultView.ShowDialog();
            Application.Exit();
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Splitting the end point into IP and port.
                string[] endpoint = txtIP.Text.Split(":");

                // Beginning connection with server.
                client = new Client(endpoint[0], int.Parse(endpoint[1]));
                client.Begin();

                // Informing user of connection.
                btnConnect.Text = "Connected";
                MessageBox.Show("Successfully connected to server", "Connection Success", MessageBoxButtons.OK);
            }
            catch
            {
                // Informing user of failure.
                MessageBox.Show("Error: Couldn't connect to the specified address", "Connection Failure", MessageBoxButtons.OK);
                client = null;
                btnConnect.Text = "Connect";
            }
        }

        // Deriving the vault key and authentication hash if the username and password are greater than 8 characters.
        private bool ValidLogin()
        {
            if (txtUsername.Text.Length < 8 || txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Error: Username and Password must both be 8+ characters", "Error", MessageBoxButtons.OK);
                return false;
            }

            vaultKey = PBKDF2.DeriveKey(txtPassword.Text, txtUsername.Text, 100000);
            authenticationHash = PBKDF2.DeriveKey(vaultKey, txtPassword.Text, 100000);

            return true;
        }

        // CheckOffline checks if a connection to the server is present and if so, sends the specified command.
        private string CheckOffline(string command)
        {
            string result = "";

            try
            {
                if (client == null)
                {
                    throw new Exception();
                }

                result = client.SendCommand(command, username: txtUsername.Text, authenticationHash: authenticationHash);
            }

            catch
            {
                DialogResult confirmation = MessageBox.Show("Error: Couldn't connect to the password server,\nwould you like to log in offline?", "Connection Failure", MessageBoxButtons.YesNo);

                if (confirmation == DialogResult.Yes)
                {
                    OpenVaultView();
                }
            }

            return result;
        }
    }
}
