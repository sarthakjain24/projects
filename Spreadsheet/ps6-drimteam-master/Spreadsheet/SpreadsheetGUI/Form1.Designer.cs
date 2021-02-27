namespace SpreadsheetGUI
{
    partial class SpreadsheetForm
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
            this.cellNameTextBox = new System.Windows.Forms.TextBox();
            this.cellValueTextBox = new System.Windows.Forms.TextBox();
            this.cellContentsTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.openSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.closeSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.helpSpreadsheet = new System.Windows.Forms.ToolStripMenuItem();
            this.darkModeButton = new System.Windows.Forms.RadioButton();
            this.lightModeButton = new System.Windows.Forms.RadioButton();
            this.spreadsheetPanel = new SS.SpreadsheetPanel();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cellNameTextBox
            // 
            this.cellNameTextBox.Enabled = false;
            this.cellNameTextBox.Location = new System.Drawing.Point(52, 6);
            this.cellNameTextBox.Name = "cellNameTextBox";
            this.cellNameTextBox.ReadOnly = true;
            this.cellNameTextBox.Size = new System.Drawing.Size(96, 22);
            this.cellNameTextBox.TabIndex = 1;
            this.cellNameTextBox.TextChanged += new System.EventHandler(this.cellNameTextBox_TextChanged);
            // 
            // cellValueTextBox
            // 
            this.cellValueTextBox.Enabled = false;
            this.cellValueTextBox.Location = new System.Drawing.Point(154, 6);
            this.cellValueTextBox.Name = "cellValueTextBox";
            this.cellValueTextBox.ReadOnly = true;
            this.cellValueTextBox.Size = new System.Drawing.Size(232, 22);
            this.cellValueTextBox.TabIndex = 2;
            this.cellValueTextBox.TextChanged += new System.EventHandler(this.cellValueTextBox_TextChanged);
            // 
            // cellContentsTextBox
            // 
            this.cellContentsTextBox.AcceptsReturn = true;
            this.cellContentsTextBox.Location = new System.Drawing.Point(392, 6);
            this.cellContentsTextBox.Name = "cellContentsTextBox";
            this.cellContentsTextBox.Size = new System.Drawing.Size(338, 22);
            this.cellContentsTextBox.TabIndex = 3;
            this.cellContentsTextBox.TextChanged += new System.EventHandler(this.darkModeButton_CheckedChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1002, 28);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newSpreadsheet,
            this.openSpreadsheet,
            this.saveSpreadsheet,
            this.clearSpreadsheet,
            this.closeSpreadsheet,
            this.helpSpreadsheet});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newSpreadsheet
            // 
            this.newSpreadsheet.Name = "newSpreadsheet";
            this.newSpreadsheet.Size = new System.Drawing.Size(194, 26);
            this.newSpreadsheet.Text = "New (Ctrl + N)";
            this.newSpreadsheet.Click += new System.EventHandler(this.newSpreadsheet_Click);
            // 
            // openSpreadsheet
            // 
            this.openSpreadsheet.Name = "openSpreadsheet";
            this.openSpreadsheet.Size = new System.Drawing.Size(194, 26);
            this.openSpreadsheet.Text = "Open (Ctrl + O)";
            this.openSpreadsheet.Click += new System.EventHandler(this.openSpreadsheet_Click);
            // 
            // saveSpreadsheet
            // 
            this.saveSpreadsheet.Name = "saveSpreadsheet";
            this.saveSpreadsheet.Size = new System.Drawing.Size(194, 26);
            this.saveSpreadsheet.Text = "Save (CtrI + S)";
            this.saveSpreadsheet.Click += new System.EventHandler(this.saveSpreadsheet_Click);
            // 
            // clearSpreadsheet
            // 
            this.clearSpreadsheet.Name = "clearSpreadsheet";
            this.clearSpreadsheet.Size = new System.Drawing.Size(194, 26);
            this.clearSpreadsheet.Text = "Clear (Ctrl + C)";
            this.clearSpreadsheet.Click += new System.EventHandler(this.clearSpreadsheet_Click);
            // 
            // closeSpreadsheet
            // 
            this.closeSpreadsheet.Name = "closeSpreadsheet";
            this.closeSpreadsheet.Size = new System.Drawing.Size(194, 26);
            this.closeSpreadsheet.Text = "Close (Alt + F4)";
            this.closeSpreadsheet.Click += new System.EventHandler(this.closeSpreadsheet_Click);
            // 
            // helpSpreadsheet
            // 
            this.helpSpreadsheet.Name = "helpSpreadsheet";
            this.helpSpreadsheet.Size = new System.Drawing.Size(194, 26);
            this.helpSpreadsheet.Text = "Help (Ctrl + H)";
            this.helpSpreadsheet.Click += new System.EventHandler(this.helpSpreadsheet_Click);
            // 
            // darkModeButton
            // 
            this.darkModeButton.AutoSize = true;
            this.darkModeButton.ForeColor = System.Drawing.Color.Black;
            this.darkModeButton.Location = new System.Drawing.Point(849, 6);
            this.darkModeButton.Name = "darkModeButton";
            this.darkModeButton.Size = new System.Drawing.Size(98, 21);
            this.darkModeButton.TabIndex = 6;
            this.darkModeButton.TabStop = true;
            this.darkModeButton.Text = "Dark Mode";
            this.darkModeButton.UseVisualStyleBackColor = true;
            this.darkModeButton.CheckedChanged += new System.EventHandler(this.darkModeButton_CheckedChanged);
            // 
            // lightModeButton
            // 
            this.lightModeButton.AutoSize = true;
            this.lightModeButton.Location = new System.Drawing.Point(744, 6);
            this.lightModeButton.Name = "lightModeButton";
            this.lightModeButton.Size = new System.Drawing.Size(99, 21);
            this.lightModeButton.TabIndex = 7;
            this.lightModeButton.TabStop = true;
            this.lightModeButton.Text = "Light Mode";
            this.lightModeButton.UseVisualStyleBackColor = true;
            this.lightModeButton.CheckedChanged += new System.EventHandler(this.lightModeButton_CheckedChanged);
            // 
            // spreadsheetPanel
            // 
            this.spreadsheetPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel.Location = new System.Drawing.Point(0, 31);
            this.spreadsheetPanel.Name = "spreadsheetPanel";
            this.spreadsheetPanel.Size = new System.Drawing.Size(1002, 503);
            this.spreadsheetPanel.TabIndex = 5;
            this.spreadsheetPanel.Load += new System.EventHandler(this.spreadsheetPanel_Load);
            // 
            // SpreadsheetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1002, 537);
            this.Controls.Add(this.lightModeButton);
            this.Controls.Add(this.darkModeButton);
            this.Controls.Add(this.spreadsheetPanel);
            this.Controls.Add(this.cellContentsTextBox);
            this.Controls.Add(this.cellValueTextBox);
            this.Controls.Add(this.cellNameTextBox);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "SpreadsheetForm";
            this.Text = "Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpreadsheetForm_FormClosing);
            this.Load += new System.EventHandler(this.SpreadsheetForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SpreadsheetForm_Paint);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox cellNameTextBox;
        private System.Windows.Forms.TextBox cellValueTextBox;
        private System.Windows.Forms.TextBox cellContentsTextBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newSpreadsheet;
        private System.Windows.Forms.ToolStripMenuItem openSpreadsheet;
        private System.Windows.Forms.ToolStripMenuItem saveSpreadsheet;
        private System.Windows.Forms.ToolStripMenuItem closeSpreadsheet;
        private SS.SpreadsheetPanel spreadsheetPanel;
        private System.Windows.Forms.ToolStripMenuItem helpSpreadsheet;
        private System.Windows.Forms.ToolStripMenuItem clearSpreadsheet;
        private System.Windows.Forms.RadioButton darkModeButton;
        private System.Windows.Forms.RadioButton lightModeButton;
    }
}

