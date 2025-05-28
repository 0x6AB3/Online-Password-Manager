namespace ClientSoftware
{
    partial class PasswordGeneratorForm
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.cbxLower = new System.Windows.Forms.CheckBox();
            this.cbxUpper = new System.Windows.Forms.CheckBox();
            this.cbxNumber = new System.Windows.Forms.CheckBox();
            this.cbxSymbol = new System.Windows.Forms.CheckBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLength = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Black;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(12, 12);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(324, 23);
            this.txtPassword.TabIndex = 0;
            // 
            // btnCopy
            // 
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Source Code Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnCopy.ForeColor = System.Drawing.Color.White;
            this.btnCopy.Location = new System.Drawing.Point(261, 41);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 35);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cbxLower
            // 
            this.cbxLower.AutoSize = true;
            this.cbxLower.Checked = true;
            this.cbxLower.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxLower.ForeColor = System.Drawing.Color.White;
            this.cbxLower.Location = new System.Drawing.Point(12, 39);
            this.cbxLower.Name = "cbxLower";
            this.cbxLower.Size = new System.Drawing.Size(119, 19);
            this.cbxLower.TabIndex = 3;
            this.cbxLower.Text = "Lowercase Letters";
            this.cbxLower.UseVisualStyleBackColor = true;
            // 
            // cbxUpper
            // 
            this.cbxUpper.AutoSize = true;
            this.cbxUpper.Checked = true;
            this.cbxUpper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxUpper.ForeColor = System.Drawing.Color.White;
            this.cbxUpper.Location = new System.Drawing.Point(12, 64);
            this.cbxUpper.Name = "cbxUpper";
            this.cbxUpper.Size = new System.Drawing.Size(119, 19);
            this.cbxUpper.TabIndex = 4;
            this.cbxUpper.Text = "Uppercase Letters";
            this.cbxUpper.UseVisualStyleBackColor = true;
            // 
            // cbxNumber
            // 
            this.cbxNumber.AutoSize = true;
            this.cbxNumber.Checked = true;
            this.cbxNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxNumber.ForeColor = System.Drawing.Color.White;
            this.cbxNumber.Location = new System.Drawing.Point(12, 89);
            this.cbxNumber.Name = "cbxNumber";
            this.cbxNumber.Size = new System.Drawing.Size(75, 19);
            this.cbxNumber.TabIndex = 5;
            this.cbxNumber.Text = "Numbers";
            this.cbxNumber.UseVisualStyleBackColor = true;
            // 
            // cbxSymbol
            // 
            this.cbxSymbol.AutoSize = true;
            this.cbxSymbol.Checked = true;
            this.cbxSymbol.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSymbol.ForeColor = System.Drawing.Color.White;
            this.cbxSymbol.Location = new System.Drawing.Point(12, 114);
            this.cbxSymbol.Name = "cbxSymbol";
            this.cbxSymbol.Size = new System.Drawing.Size(71, 19);
            this.cbxSymbol.TabIndex = 6;
            this.cbxSymbol.Text = "Symbols";
            this.cbxSymbol.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerate.Font = new System.Drawing.Font("Source Code Pro", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGenerate.ForeColor = System.Drawing.Color.White;
            this.btnGenerate.Location = new System.Drawing.Point(149, 41);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(106, 35);
            this.btnGenerate.TabIndex = 7;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // btnLeft
            // 
            this.btnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLeft.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLeft.ForeColor = System.Drawing.Color.White;
            this.btnLeft.Location = new System.Drawing.Point(199, 92);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(25, 40);
            this.btnLeft.TabIndex = 8;
            this.btnLeft.Text = "<";
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
            // 
            // btnRight
            // 
            this.btnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRight.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRight.ForeColor = System.Drawing.Color.White;
            this.btnRight.Location = new System.Drawing.Point(311, 93);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(25, 40);
            this.btnRight.TabIndex = 9;
            this.btnRight.Text = ">";
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
            // 
            // btnLength
            // 
            this.btnLength.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLength.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnLength.ForeColor = System.Drawing.Color.White;
            this.btnLength.Location = new System.Drawing.Point(230, 92);
            this.btnLength.Name = "btnLength";
            this.btnLength.Size = new System.Drawing.Size(75, 40);
            this.btnLength.TabIndex = 10;
            this.btnLength.Text = "16";
            this.btnLength.UseVisualStyleBackColor = true;
            // 
            // PasswordGeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(348, 141);
            this.Controls.Add(this.btnLength);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.cbxSymbol);
            this.Controls.Add(this.cbxNumber);
            this.Controls.Add(this.cbxUpper);
            this.Controls.Add(this.cbxLower);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtPassword);
            this.Name = "PasswordGeneratorForm";
            this.Text = "Password Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.CheckBox cbxLower;
        private System.Windows.Forms.CheckBox cbxUpper;
        private System.Windows.Forms.CheckBox cbxNumber;
        private System.Windows.Forms.CheckBox cbxSymbol;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLength;
    }
}