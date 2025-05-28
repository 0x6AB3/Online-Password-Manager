
namespace ClientSoftware
{
    partial class AccountForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAccountName = new System.Windows.Forms.Label();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.RichTextBox();
            this.cbxShowPassword = new System.Windows.Forms.CheckBox();
            this.btnCopyUsername = new System.Windows.Forms.Button();
            this.btnCopyPassword = new System.Windows.Forms.Button();
            this.cbxShowUsername = new System.Windows.Forms.CheckBox();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnPasswordGen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.BackColor = System.Drawing.Color.Black;
            this.lblAccountName.ForeColor = System.Drawing.Color.White;
            this.lblAccountName.Location = new System.Drawing.Point(12, 17);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(87, 15);
            this.lblAccountName.TabIndex = 0;
            this.lblAccountName.Text = "Account Name";
            // 
            // txtAccountName
            // 
            this.txtAccountName.BackColor = System.Drawing.Color.Black;
            this.txtAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountName.ForeColor = System.Drawing.Color.White;
            this.txtAccountName.Location = new System.Drawing.Point(105, 9);
            this.txtAccountName.MaxLength = 20;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(135, 23);
            this.txtAccountName.TabIndex = 1;
            this.txtAccountName.TextChanged += new System.EventHandler(this.txtAccountName_TextChanged);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Black;
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(12, 46);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(60, 15);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.Black;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.ForeColor = System.Drawing.Color.White;
            this.txtUsername.Location = new System.Drawing.Point(78, 38);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = '•';
            this.txtUsername.Size = new System.Drawing.Size(162, 23);
            this.txtUsername.TabIndex = 3;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Black;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(78, 67);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(162, 23);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Black;
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(12, 75);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Password";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.BackColor = System.Drawing.Color.Black;
            this.lblNotes.ForeColor = System.Drawing.Color.White;
            this.lblNotes.Location = new System.Drawing.Point(12, 102);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(162, 15);
            this.lblNotes.TabIndex = 6;
            this.lblNotes.Text = "Additional information/notes";
            // 
            // txtNote
            // 
            this.txtNote.BackColor = System.Drawing.Color.Black;
            this.txtNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNote.ForeColor = System.Drawing.Color.White;
            this.txtNote.Location = new System.Drawing.Point(12, 120);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(362, 96);
            this.txtNote.TabIndex = 7;
            this.txtNote.Text = "Empty";
            this.txtNote.TextChanged += new System.EventHandler(this.txtNote_TextChanged);
            // 
            // cbxShowPassword
            // 
            this.cbxShowPassword.AutoSize = true;
            this.cbxShowPassword.BackColor = System.Drawing.Color.Black;
            this.cbxShowPassword.ForeColor = System.Drawing.Color.White;
            this.cbxShowPassword.Location = new System.Drawing.Point(306, 71);
            this.cbxShowPassword.Name = "cbxShowPassword";
            this.cbxShowPassword.Size = new System.Drawing.Size(55, 19);
            this.cbxShowPassword.TabIndex = 8;
            this.cbxShowPassword.Text = "Show";
            this.cbxShowPassword.UseVisualStyleBackColor = false;
            this.cbxShowPassword.CheckedChanged += new System.EventHandler(this.cbxShowPassword_CheckedChanged);
            // 
            // btnCopyUsername
            // 
            this.btnCopyUsername.BackColor = System.Drawing.Color.Black;
            this.btnCopyUsername.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyUsername.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCopyUsername.ForeColor = System.Drawing.Color.White;
            this.btnCopyUsername.Location = new System.Drawing.Point(246, 38);
            this.btnCopyUsername.Name = "btnCopyUsername";
            this.btnCopyUsername.Size = new System.Drawing.Size(54, 23);
            this.btnCopyUsername.TabIndex = 9;
            this.btnCopyUsername.Text = "Copy";
            this.btnCopyUsername.UseVisualStyleBackColor = false;
            this.btnCopyUsername.Click += new System.EventHandler(this.btnCopyUsername_Click);
            // 
            // btnCopyPassword
            // 
            this.btnCopyPassword.BackColor = System.Drawing.Color.Black;
            this.btnCopyPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopyPassword.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCopyPassword.ForeColor = System.Drawing.Color.White;
            this.btnCopyPassword.Location = new System.Drawing.Point(246, 67);
            this.btnCopyPassword.Name = "btnCopyPassword";
            this.btnCopyPassword.Size = new System.Drawing.Size(54, 23);
            this.btnCopyPassword.TabIndex = 10;
            this.btnCopyPassword.Text = "Copy";
            this.btnCopyPassword.UseVisualStyleBackColor = false;
            this.btnCopyPassword.Click += new System.EventHandler(this.btnCopyPassword_Click);
            // 
            // cbxShowUsername
            // 
            this.cbxShowUsername.AutoSize = true;
            this.cbxShowUsername.BackColor = System.Drawing.Color.Black;
            this.cbxShowUsername.ForeColor = System.Drawing.Color.White;
            this.cbxShowUsername.Location = new System.Drawing.Point(306, 42);
            this.cbxShowUsername.Name = "cbxShowUsername";
            this.cbxShowUsername.Size = new System.Drawing.Size(55, 19);
            this.cbxShowUsername.TabIndex = 11;
            this.cbxShowUsername.Text = "Show";
            this.cbxShowUsername.UseVisualStyleBackColor = false;
            this.cbxShowUsername.CheckedChanged += new System.EventHandler(this.cbxShowUsername_CheckedChanged);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.Color.Black;
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveChanges.ForeColor = System.Drawing.Color.Lime;
            this.btnSaveChanges.Location = new System.Drawing.Point(246, 9);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(125, 23);
            this.btnSaveChanges.TabIndex = 12;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnPasswordGen
            // 
            this.btnPasswordGen.BackColor = System.Drawing.Color.Black;
            this.btnPasswordGen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPasswordGen.ForeColor = System.Drawing.Color.White;
            this.btnPasswordGen.Location = new System.Drawing.Point(246, 94);
            this.btnPasswordGen.Name = "btnPasswordGen";
            this.btnPasswordGen.Size = new System.Drawing.Size(125, 23);
            this.btnPasswordGen.TabIndex = 13;
            this.btnPasswordGen.Text = "Password Generator";
            this.btnPasswordGen.UseVisualStyleBackColor = false;
            this.btnPasswordGen.Click += new System.EventHandler(this.btnPasswordGen_Click);
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(383, 230);
            this.Controls.Add(this.btnPasswordGen);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.cbxShowUsername);
            this.Controls.Add(this.btnCopyPassword);
            this.Controls.Add(this.btnCopyUsername);
            this.Controls.Add(this.cbxShowPassword);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.lblAccountName);
            this.Name = "AccountForm";
            this.Text = "Edit Account";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.RichTextBox txtNote;
        private System.Windows.Forms.CheckBox cbxShowPassword;
        private System.Windows.Forms.Button btnCopyUsername;
        private System.Windows.Forms.Button btnCopyPassword;
        private System.Windows.Forms.CheckBox cbxShowUsername;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnPasswordGen;
    }
}