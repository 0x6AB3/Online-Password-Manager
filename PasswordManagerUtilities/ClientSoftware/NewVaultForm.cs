using System;
using System.Windows.Forms;

namespace ClientSoftware
{
    public partial class NewVaultForm : Form
    {
        private ComboBox.ObjectCollection existingNames;
        public string vaultName;

        public NewVaultForm(ComboBox.ObjectCollection names)
        {
            existingNames = names;
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // Removing whitespaces from the vault name.
            txtVaultName.Text.Replace(' ', '_');

            // Checking if vaults exist with the desired name in order to prevent overwriting existing vaults.
            if (existingNames.Contains(txtVaultName.Text + ".vault"))
            {
                MessageBox.Show("This vault already exists on your device", "Error", MessageBoxButtons.OK);
                return;
            }

            // The vault name is retrieved by the object that calls this form.
            vaultName = txtVaultName.Text + ".vault";
            this.Close();
        }

        private void txtVaultName_TextChanged(object sender, EventArgs e)
        {
            // Replacing whitespace with a null character.
            txtVaultName.Text = txtVaultName.Text.Replace(' ', '\0');

            // Not allowing the user to create a vault with no name.
            if (txtVaultName.Text == "")
            {
                btnCreate.Enabled = false;
                return;
            }
            btnCreate.Enabled = true;
        }

        // Ignoring spaces in the file name.
        private void txtVaultName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
            }
        }
    }
}
