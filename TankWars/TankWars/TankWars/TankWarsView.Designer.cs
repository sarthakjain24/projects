using System;

namespace TankWars
{
    partial class TankWarsView
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
            this.serverName = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.helpMenuStrip = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.helpMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // serverName
            // 
            this.serverName.AutoSize = true;
            this.serverName.Location = new System.Drawing.Point(0, 7);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(52, 17);
            this.serverName.TabIndex = 0;
            this.serverName.Text = "server:";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(177, 5);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(47, 17);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "name:";
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(58, 5);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(113, 22);
            this.serverTextBox.TabIndex = 2;
            this.serverTextBox.Text = "localhost";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(230, 4);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(117, 22);
            this.nameBox.TabIndex = 4;
            this.nameBox.Text = "player";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(368, 1);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(139, 28);
            this.connectButton.TabIndex = 5;
            this.connectButton.Text = "Connect:";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // helpMenuStrip
            // 
            this.helpMenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.helpMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.helpMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.helpMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.helpMenuStrip.Location = new System.Drawing.Point(1078, 1);
            this.helpMenuStrip.Name = "helpMenuStrip";
            this.helpMenuStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.helpMenuStrip.Size = new System.Drawing.Size(65, 30);
            this.helpMenuStrip.TabIndex = 6;
            this.helpMenuStrip.Text = "Help";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.controlsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(55, 26);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // controlsToolStripMenuItem
            // 
            this.controlsToolStripMenuItem.Name = "controlsToolStripMenuItem";
            this.controlsToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.controlsToolStripMenuItem.Text = "Controls";
            this.controlsToolStripMenuItem.Click += new System.EventHandler(this.controlsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(147, 26);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            //
            // TankWarsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 662);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.serverName);
            this.Controls.Add(this.serverTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.helpMenuStrip);
            this.MainMenuStrip = this.helpMenuStrip;
            this.Name = "TankWarsView";
            this.Text = "TankWarsGame";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.helpMenuStrip.ResumeLayout(false);
            this.helpMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

      

        #endregion

        private System.Windows.Forms.Label serverName;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.MenuStrip helpMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem controlsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

