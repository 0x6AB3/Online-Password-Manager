
using System;

namespace ClientSoftware
{
    partial class VaultViewForm
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
            this.components = new System.ComponentModel.Container();
            this.cmbVaultSelection = new System.Windows.Forms.ComboBox();
            this.btnDownloadVault = new System.Windows.Forms.Button();
            this.lblLocalVaults = new System.Windows.Forms.Label();
            this.lblServerVaults = new System.Windows.Forms.Label();
            this.cmbServerVaultSelection = new System.Windows.Forms.ComboBox();
            this.btnNewVault = new System.Windows.Forms.Button();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.btnUploadVault = new System.Windows.Forms.Button();
            this.lblServerStatus = new System.Windows.Forms.Label();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnDeleteVault = new System.Windows.Forms.Button();
            this.btnRemoveServerVault = new System.Windows.Forms.Button();
            this.backgroundLocal = new System.Windows.Forms.PictureBox();
            this.backgroundServer = new System.Windows.Forms.PictureBox();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnPasswordGen = new System.Windows.Forms.Button();
            this.PollServer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.backgroundLocal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundServer)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbVaultSelection
            // 
            this.cmbVaultSelection.BackColor = System.Drawing.Color.Black;
            this.cmbVaultSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVaultSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVaultSelection.ForeColor = System.Drawing.Color.White;
            this.cmbVaultSelection.FormattingEnabled = true;
            this.cmbVaultSelection.Location = new System.Drawing.Point(13, 27);
            this.cmbVaultSelection.Name = "cmbVaultSelection";
            this.cmbVaultSelection.Size = new System.Drawing.Size(156, 23);
            this.cmbVaultSelection.TabIndex = 1;
            this.cmbVaultSelection.SelectedIndexChanged += new System.EventHandler(this.cmbVaultSelection_SelectedIndexChanged);
            // 
            // btnDownloadVault
            // 
            this.btnDownloadVault.BackColor = System.Drawing.Color.Black;
            this.btnDownloadVault.Enabled = false;
            this.btnDownloadVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDownloadVault.ForeColor = System.Drawing.Color.White;
            this.btnDownloadVault.Location = new System.Drawing.Point(511, 27);
            this.btnDownloadVault.Name = "btnDownloadVault";
            this.btnDownloadVault.Size = new System.Drawing.Size(92, 23);
            this.btnDownloadVault.TabIndex = 2;
            this.btnDownloadVault.Text = "Download";
            this.btnDownloadVault.UseVisualStyleBackColor = false;
            this.btnDownloadVault.Click += new System.EventHandler(this.btnDownloadVault_Click);
            // 
            // lblLocalVaults
            // 
            this.lblLocalVaults.AutoSize = true;
            this.lblLocalVaults.BackColor = System.Drawing.Color.Black;
            this.lblLocalVaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLocalVaults.ForeColor = System.Drawing.Color.White;
            this.lblLocalVaults.Location = new System.Drawing.Point(13, 10);
            this.lblLocalVaults.Name = "lblLocalVaults";
            this.lblLocalVaults.Size = new System.Drawing.Size(86, 18);
            this.lblLocalVaults.TabIndex = 3;
            this.lblLocalVaults.Text = "Local vaults";
            // 
            // lblServerVaults
            // 
            this.lblServerVaults.AutoSize = true;
            this.lblServerVaults.BackColor = System.Drawing.Color.Black;
            this.lblServerVaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblServerVaults.ForeColor = System.Drawing.Color.White;
            this.lblServerVaults.Location = new System.Drawing.Point(337, 10);
            this.lblServerVaults.Name = "lblServerVaults";
            this.lblServerVaults.Size = new System.Drawing.Size(93, 18);
            this.lblServerVaults.TabIndex = 4;
            this.lblServerVaults.Text = "Server vaults";
            // 
            // cmbServerVaultSelection
            // 
            this.cmbServerVaultSelection.BackColor = System.Drawing.Color.Black;
            this.cmbServerVaultSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerVaultSelection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbServerVaultSelection.ForeColor = System.Drawing.Color.White;
            this.cmbServerVaultSelection.FormattingEnabled = true;
            this.cmbServerVaultSelection.Location = new System.Drawing.Point(336, 27);
            this.cmbServerVaultSelection.Name = "cmbServerVaultSelection";
            this.cmbServerVaultSelection.Size = new System.Drawing.Size(169, 23);
            this.cmbServerVaultSelection.TabIndex = 5;
            this.cmbServerVaultSelection.SelectedIndexChanged += new System.EventHandler(this.cmbServerVaultSelection_SelectedIndexChanged);
            // 
            // btnNewVault
            // 
            this.btnNewVault.BackColor = System.Drawing.Color.Black;
            this.btnNewVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnNewVault.ForeColor = System.Drawing.Color.White;
            this.btnNewVault.Location = new System.Drawing.Point(175, 27);
            this.btnNewVault.Name = "btnNewVault";
            this.btnNewVault.Size = new System.Drawing.Size(98, 24);
            this.btnNewVault.TabIndex = 0;
            this.btnNewVault.Text = "New Vault";
            this.btnNewVault.UseVisualStyleBackColor = false;
            this.btnNewVault.Click += new System.EventHandler(this.btnNewVault_Click);
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.BackColor = System.Drawing.Color.Black;
            this.btnAddAccount.Enabled = false;
            this.btnAddAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAddAccount.ForeColor = System.Drawing.Color.White;
            this.btnAddAccount.Location = new System.Drawing.Point(12, 56);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(261, 23);
            this.btnAddAccount.TabIndex = 9;
            this.btnAddAccount.Text = "Add Account to this Vault";
            this.btnAddAccount.UseVisualStyleBackColor = false;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // btnUploadVault
            // 
            this.btnUploadVault.BackColor = System.Drawing.Color.Black;
            this.btnUploadVault.Enabled = false;
            this.btnUploadVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnUploadVault.ForeColor = System.Drawing.Color.White;
            this.btnUploadVault.Location = new System.Drawing.Point(336, 56);
            this.btnUploadVault.Name = "btnUploadVault";
            this.btnUploadVault.Size = new System.Drawing.Size(267, 33);
            this.btnUploadVault.TabIndex = 16;
            this.btnUploadVault.Text = "Upload Local Vault";
            this.btnUploadVault.UseVisualStyleBackColor = false;
            this.btnUploadVault.Click += new System.EventHandler(this.btnUploadVault_Click);
            // 
            // lblServerStatus
            // 
            this.lblServerStatus.AutoSize = true;
            this.lblServerStatus.BackColor = System.Drawing.Color.Black;
            this.lblServerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblServerStatus.ForeColor = System.Drawing.Color.White;
            this.lblServerStatus.Location = new System.Drawing.Point(339, 127);
            this.lblServerStatus.Name = "lblServerStatus";
            this.lblServerStatus.Size = new System.Drawing.Size(101, 18);
            this.lblServerStatus.TabIndex = 17;
            this.lblServerStatus.Text = "Server Status:";
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.BackColor = System.Drawing.Color.Black;
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveChanges.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSaveChanges.ForeColor = System.Drawing.Color.Lime;
            this.btnSaveChanges.Location = new System.Drawing.Point(145, 85);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(128, 33);
            this.btnSaveChanges.TabIndex = 19;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = false;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnDeleteVault
            // 
            this.btnDeleteVault.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnDeleteVault.Enabled = false;
            this.btnDeleteVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDeleteVault.ForeColor = System.Drawing.Color.Firebrick;
            this.btnDeleteVault.Location = new System.Drawing.Point(12, 85);
            this.btnDeleteVault.Name = "btnDeleteVault";
            this.btnDeleteVault.Size = new System.Drawing.Size(128, 33);
            this.btnDeleteVault.TabIndex = 20;
            this.btnDeleteVault.Text = "Delete Vault";
            this.btnDeleteVault.UseVisualStyleBackColor = false;
            this.btnDeleteVault.Click += new System.EventHandler(this.btnDeleteVault_Click);
            // 
            // btnRemoveServerVault
            // 
            this.btnRemoveServerVault.BackColor = System.Drawing.Color.Black;
            this.btnRemoveServerVault.Enabled = false;
            this.btnRemoveServerVault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveServerVault.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnRemoveServerVault.ForeColor = System.Drawing.Color.Firebrick;
            this.btnRemoveServerVault.Location = new System.Drawing.Point(337, 95);
            this.btnRemoveServerVault.Name = "btnRemoveServerVault";
            this.btnRemoveServerVault.Size = new System.Drawing.Size(266, 33);
            this.btnRemoveServerVault.TabIndex = 21;
            this.btnRemoveServerVault.Text = "Remove Server Vault";
            this.btnRemoveServerVault.UseVisualStyleBackColor = false;
            this.btnRemoveServerVault.Click += new System.EventHandler(this.btnRemoveServerVault_Click);
            // 
            // backgroundLocal
            // 
            this.backgroundLocal.BackColor = System.Drawing.Color.Black;
            this.backgroundLocal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.backgroundLocal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backgroundLocal.Location = new System.Drawing.Point(12, 9);
            this.backgroundLocal.Name = "backgroundLocal";
            this.backgroundLocal.Size = new System.Drawing.Size(261, 109);
            this.backgroundLocal.TabIndex = 22;
            this.backgroundLocal.TabStop = false;
            // 
            // backgroundServer
            // 
            this.backgroundServer.BackColor = System.Drawing.Color.Black;
            this.backgroundServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.backgroundServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backgroundServer.Location = new System.Drawing.Point(336, 9);
            this.backgroundServer.Name = "backgroundServer";
            this.backgroundServer.Size = new System.Drawing.Size(267, 138);
            this.backgroundServer.TabIndex = 23;
            this.backgroundServer.TabStop = false;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.BackColor = System.Drawing.Color.Black;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lblConnectionStatus.Location = new System.Drawing.Point(480, 127);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(99, 18);
            this.lblConnectionStatus.TabIndex = 24;
            this.lblConnectionStatus.Text = "Disconnected";
            // 
            // btnPasswordGen
            // 
            this.btnPasswordGen.BackColor = System.Drawing.Color.Black;
            this.btnPasswordGen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPasswordGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnPasswordGen.ForeColor = System.Drawing.Color.White;
            this.btnPasswordGen.Location = new System.Drawing.Point(13, 127);
            this.btnPasswordGen.Name = "btnPasswordGen";
            this.btnPasswordGen.Size = new System.Drawing.Size(261, 23);
            this.btnPasswordGen.TabIndex = 25;
            this.btnPasswordGen.Text = "Password Generator";
            this.btnPasswordGen.UseVisualStyleBackColor = false;
            this.btnPasswordGen.Click += new System.EventHandler(this.btnPasswordGen_Click);
            // 
            // PollServer
            // 
            this.PollServer.Interval = 2500;
            this.PollServer.Tick += new System.EventHandler(this.PollServer_Tick);
            // 
            // VaultViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(673, 451);
            this.Controls.Add(this.btnPasswordGen);
            this.Controls.Add(this.btnRemoveServerVault);
            this.Controls.Add(this.lblConnectionStatus);
            this.Controls.Add(this.cmbServerVaultSelection);
            this.Controls.Add(this.lblServerStatus);
            this.Controls.Add(this.btnUploadVault);
            this.Controls.Add(this.lblServerVaults);
            this.Controls.Add(this.btnDownloadVault);
            this.Controls.Add(this.backgroundServer);
            this.Controls.Add(this.cmbVaultSelection);
            this.Controls.Add(this.btnDeleteVault);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnAddAccount);
            this.Controls.Add(this.lblLocalVaults);
            this.Controls.Add(this.btnNewVault);
            this.Controls.Add(this.backgroundLocal);
            this.Name = "VaultViewForm";
            this.Text = "Vault View";
            ((System.ComponentModel.ISupportInitialize)(this.backgroundLocal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundServer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion
        private System.Windows.Forms.ComboBox cmbVaultSelection;
        private System.Windows.Forms.Button btnDownloadVault;
        private System.Windows.Forms.Label lblLocalVaults;
        private System.Windows.Forms.Label lblServerVaults;
        private System.Windows.Forms.ComboBox cmbServerVaultSelection;
        private System.Windows.Forms.Button btnNewVault;
        private System.Windows.Forms.Button btnUploadVault;
        private System.Windows.Forms.Label lblServerStatus;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnDeleteVault;
        private System.Windows.Forms.Button btnRemoveServerVault;
        private System.Windows.Forms.PictureBox backgroundLocal;
        private System.Windows.Forms.PictureBox backgroundServer;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Button btnPasswordGen;
        private System.Windows.Forms.Timer PollServer;
    }
}