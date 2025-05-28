using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClientSoftware
{
    public partial class PasswordGeneratorForm : Form
    {
        // The character sets used to generate passwords.
        char[] lower, upper, number, symbol;

        // Decreasing the password length.
        private void btnLeft_Click(object sender, EventArgs e)
        {
            int newVal = int.Parse(btnLength.Text) - 1;
            if (newVal > 0)
            {
                btnLength.Text = newVal.ToString();
            }
        }
        
        // Increasing the password length.
        private void btnRight_Click(object sender, EventArgs e)
        {
            int newVal = int.Parse(btnLength.Text) + 1;

            // Leaving the upper password length limit to 64 characters to prevent performance/instability issues.
            if (newVal < 65)
            {
                btnLength.Text = newVal.ToString();
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int length = int.Parse(btnLength.Text);

            // The character sets that the user chose are added onto the characterSet list.
            List<char> characterSet = new List<char>();

            if (cbxLower.Checked)
            {
                characterSet.AddRange(lower);
            }
            if (cbxUpper.Checked)
            {
                characterSet.AddRange(upper);
            }
            if (cbxNumber.Checked)
            {
                characterSet.AddRange(number);
            }
            if (cbxSymbol.Checked)
            {
                characterSet.AddRange(symbol);
            }

            // Not generating a password if no character sets were chosen.
            if (characterSet.Count == 0)
            {
                return;
            }

            Random random = new Random();
            char[] password = new char[length];

            // Choosing characters from the characterSet at random positions.
            for (int i = 0; i < length; i++)
            {
                password[i] = characterSet[random.Next(0, characterSet.Count)];
            }

            // Displaying the generated password onto the textbox.
            txtPassword.Text = new string(password);
        }

        // Form constructor.
        public PasswordGeneratorForm()
        {
            InitializeComponent();

            lower  = new char[26];
            upper  = new char[26];
            number = new char[10];
            symbol = new char[32];

            // Lowercase and Uppercase letters from ASCII.
            for (int i = 0; i < 26; i++)
            {
                lower[i] = (char)(97 + i);
                upper[i] = (char)(65 + i);
            }

            // Digits from 0-9 from ASCII.
            for (int i = 0; i < 10; i++)
            {
                number[i] = (char)(48 + i);
            }

            // All ASCII symbols.
            for (int i = 0; i < 32; i++)
            {
                if (i < 15)
                {
                    symbol[i] = (char)(33 + i);
                    continue;
                }
                else if (i < 22)
                {
                    symbol[i] = (char)(58 + (i-15));
                    continue;
                }
                else if (i < 28)
                {
                    symbol[i] = (char)(91 + (i-22));
                    continue;
                }
                else
                {
                    symbol[i] = (char)(123 + (i - 28));
                }
            }
        }

        // Copying the generated password to the clipboard.
        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txtPassword.Text);
            }
            catch
            {
                MessageBox.Show("Couldn't copy password", "Copy Error", MessageBoxButtons.OK);
            }
        }
    }
}
