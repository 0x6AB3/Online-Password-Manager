
namespace ClientSoftware
{
    partial class NewVaultForm
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.lblVaultName = new System.Windows.Forms.Label();
            this.txtVaultName = new System.Windows.Forms.TextBox();
            this.lblVaultExtension = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.Black;
            this.btnCreate.Enabled = false;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(12, 59);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(220, 35);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblVaultName
            // 
            this.lblVaultName.AutoSize = true;
            this.lblVaultName.BackColor = System.Drawing.Color.Black;
            this.lblVaultName.ForeColor = System.Drawing.Color.White;
            this.lblVaultName.Location = new System.Drawing.Point(12, 12);
            this.lblVaultName.Name = "lblVaultName";
            this.lblVaultName.Size = new System.Drawing.Size(99, 15);
            this.lblVaultName.TabIndex = 1;
            this.lblVaultName.Text = "Enter vault name:";
            // 
            // txtVaultName
            // 
            this.txtVaultName.BackColor = System.Drawing.Color.Black;
            this.txtVaultName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVaultName.ForeColor = System.Drawing.Color.White;
            this.txtVaultName.Location = new System.Drawing.Point(12, 30);
            this.txtVaultName.MaxLength = 20;
            this.txtVaultName.Name = "txtVaultName";
            this.txtVaultName.Size = new System.Drawing.Size(167, 23);
            this.txtVaultName.TabIndex = 2;
            this.txtVaultName.TextChanged += new System.EventHandler(this.txtVaultName_TextChanged);
            this.txtVaultName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVaultName_KeyDown);
            // 
            // lblVaultExtension
            // 
            this.lblVaultExtension.AutoSize = true;
            this.lblVaultExtension.BackColor = System.Drawing.Color.Black;
            this.lblVaultExtension.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVaultExtension.ForeColor = System.Drawing.Color.White;
            this.lblVaultExtension.Location = new System.Drawing.Point(175, 28);
            this.lblVaultExtension.Name = "lblVaultExtension";
            this.lblVaultExtension.Size = new System.Drawing.Size(57, 25);
            this.lblVaultExtension.TabIndex = 3;
            this.lblVaultExtension.Text = ".vault";
            // 
            // NewVaultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(238, 102);
            this.Controls.Add(this.txtVaultName);
            this.Controls.Add(this.lblVaultExtension);
            this.Controls.Add(this.lblVaultName);
            this.Controls.Add(this.btnCreate);
            this.Name = "NewVaultForm";
            this.Text = "New Vault";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label lblVaultName;
        private System.Windows.Forms.Label lblVaultExtension;
        private System.Windows.Forms.TextBox txtVaultName;
    }
}