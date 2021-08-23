
namespace BudgetingApp
{
    partial class Budgeting_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Budgeting_Form));
            this.budgetingPanel = new System.Windows.Forms.Panel();
            this.editExpensesButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.viewExpensesComboBox = new System.Windows.Forms.ComboBox();
            this.viewExpensesButton = new System.Windows.Forms.Button();
            this.viewExpenseLabel = new System.Windows.Forms.Label();
            this.expenseComboBox = new System.Windows.Forms.ComboBox();
            this.costOfExpense = new System.Windows.Forms.TextBox();
            this.expenseNameBox = new System.Windows.Forms.TextBox();
            this.DeductFrom = new System.Windows.Forms.Label();
            this.costExpenseLabel = new System.Windows.Forms.Label();
            this.itemLabel = new System.Windows.Forms.Label();
            this.addExpenseButton = new System.Windows.Forms.Button();
            this.needsMoneyTextBox = new System.Windows.Forms.TextBox();
            this.wantsMoneyTextBox = new System.Windows.Forms.TextBox();
            this.savingsMoneyTextBox = new System.Windows.Forms.TextBox();
            this.totalMoneyTextBox = new System.Windows.Forms.TextBox();
            this.totalMoneyLabel = new System.Windows.Forms.Label();
            this.savingsMoneyLabel = new System.Windows.Forms.Label();
            this.wantsMoneyLabel = new System.Windows.Forms.Label();
            this.needsMoneyLabel = new System.Windows.Forms.Label();
            this.totalTextBox = new System.Windows.Forms.TextBox();
            this.totalLabel = new System.Windows.Forms.Label();
            this.savingLabel = new System.Windows.Forms.Label();
            this.wantsLabel = new System.Windows.Forms.Label();
            this.needsLabel = new System.Windows.Forms.Label();
            this.savingsTextBox = new System.Windows.Forms.TextBox();
            this.wantsTextBox = new System.Windows.Forms.TextBox();
            this.needsTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expenseButton = new System.Windows.Forms.Button();
            this.budgetingPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // budgetingPanel
            // 
            this.budgetingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.budgetingPanel.Controls.Add(this.editExpensesButton);
            this.budgetingPanel.Controls.Add(this.removeButton);
            this.budgetingPanel.Controls.Add(this.viewExpensesComboBox);
            this.budgetingPanel.Controls.Add(this.viewExpensesButton);
            this.budgetingPanel.Controls.Add(this.viewExpenseLabel);
            this.budgetingPanel.Controls.Add(this.expenseComboBox);
            this.budgetingPanel.Controls.Add(this.costOfExpense);
            this.budgetingPanel.Controls.Add(this.expenseNameBox);
            this.budgetingPanel.Controls.Add(this.DeductFrom);
            this.budgetingPanel.Controls.Add(this.costExpenseLabel);
            this.budgetingPanel.Controls.Add(this.itemLabel);
            this.budgetingPanel.Controls.Add(this.addExpenseButton);
            this.budgetingPanel.Controls.Add(this.needsMoneyTextBox);
            this.budgetingPanel.Controls.Add(this.wantsMoneyTextBox);
            this.budgetingPanel.Controls.Add(this.savingsMoneyTextBox);
            this.budgetingPanel.Controls.Add(this.totalMoneyTextBox);
            this.budgetingPanel.Controls.Add(this.totalMoneyLabel);
            this.budgetingPanel.Controls.Add(this.savingsMoneyLabel);
            this.budgetingPanel.Controls.Add(this.wantsMoneyLabel);
            this.budgetingPanel.Controls.Add(this.needsMoneyLabel);
            this.budgetingPanel.Controls.Add(this.totalTextBox);
            this.budgetingPanel.Controls.Add(this.totalLabel);
            this.budgetingPanel.Controls.Add(this.savingLabel);
            this.budgetingPanel.Controls.Add(this.wantsLabel);
            this.budgetingPanel.Controls.Add(this.needsLabel);
            this.budgetingPanel.Controls.Add(this.savingsTextBox);
            this.budgetingPanel.Controls.Add(this.wantsTextBox);
            this.budgetingPanel.Controls.Add(this.needsTextBox);
            this.budgetingPanel.Location = new System.Drawing.Point(1, 30);
            this.budgetingPanel.Name = "budgetingPanel";
            this.budgetingPanel.Size = new System.Drawing.Size(1210, 664);
            this.budgetingPanel.TabIndex = 0;
            // 
            // editExpensesButton
            // 
            this.editExpensesButton.Location = new System.Drawing.Point(993, 91);
            this.editExpensesButton.Name = "editExpensesButton";
            this.editExpensesButton.Size = new System.Drawing.Size(159, 36);
            this.editExpensesButton.TabIndex = 27;
            this.editExpensesButton.Text = "Edit Expense";
            this.editExpensesButton.UseVisualStyleBackColor = true;
            this.editExpensesButton.Click += new System.EventHandler(this.EditExpensesButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(993, 132);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(159, 36);
            this.removeButton.TabIndex = 26;
            this.removeButton.Text = "Remove Expense";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // viewExpensesComboBox
            // 
            this.viewExpensesComboBox.FormattingEnabled = true;
            this.viewExpensesComboBox.Items.AddRange(new object[] {
            "Needs",
            "Wants",
            "Savings"});
            this.viewExpensesComboBox.Location = new System.Drawing.Point(817, 250);
            this.viewExpensesComboBox.Name = "viewExpensesComboBox";
            this.viewExpensesComboBox.Size = new System.Drawing.Size(136, 24);
            this.viewExpensesComboBox.TabIndex = 25;
            // 
            // viewExpensesButton
            // 
            this.viewExpensesButton.Location = new System.Drawing.Point(993, 243);
            this.viewExpensesButton.Name = "viewExpensesButton";
            this.viewExpensesButton.Size = new System.Drawing.Size(159, 36);
            this.viewExpensesButton.TabIndex = 24;
            this.viewExpensesButton.Text = "View Expenses";
            this.viewExpensesButton.UseVisualStyleBackColor = true;
            this.viewExpensesButton.Click += new System.EventHandler(this.ViewExpensesButton_Click);
            // 
            // viewExpenseLabel
            // 
            this.viewExpenseLabel.AutoSize = true;
            this.viewExpenseLabel.Location = new System.Drawing.Point(709, 253);
            this.viewExpenseLabel.Name = "viewExpenseLabel";
            this.viewExpenseLabel.Size = new System.Drawing.Size(102, 17);
            this.viewExpenseLabel.TabIndex = 23;
            this.viewExpenseLabel.Text = "View Expenses";
            // 
            // expenseComboBox
            // 
            this.expenseComboBox.FormattingEnabled = true;
            this.expenseComboBox.Items.AddRange(new object[] {
            "Needs",
            "Wants",
            "Savings"});
            this.expenseComboBox.Location = new System.Drawing.Point(817, 142);
            this.expenseComboBox.Name = "expenseComboBox";
            this.expenseComboBox.Size = new System.Drawing.Size(136, 24);
            this.expenseComboBox.TabIndex = 22;
            // 
            // costOfExpense
            // 
            this.costOfExpense.Location = new System.Drawing.Point(817, 98);
            this.costOfExpense.Name = "costOfExpense";
            this.costOfExpense.Size = new System.Drawing.Size(136, 22);
            this.costOfExpense.TabIndex = 21;
            // 
            // expenseNameBox
            // 
            this.expenseNameBox.Location = new System.Drawing.Point(817, 57);
            this.expenseNameBox.Name = "expenseNameBox";
            this.expenseNameBox.Size = new System.Drawing.Size(136, 22);
            this.expenseNameBox.TabIndex = 20;
            // 
            // DeductFrom
            // 
            this.DeductFrom.AutoSize = true;
            this.DeductFrom.Location = new System.Drawing.Point(668, 144);
            this.DeductFrom.Name = "DeductFrom";
            this.DeductFrom.Size = new System.Drawing.Size(143, 17);
            this.DeductFrom.TabIndex = 19;
            this.DeductFrom.Text = "Deduct Expense from";
            // 
            // costExpenseLabel
            // 
            this.costExpenseLabel.AutoSize = true;
            this.costExpenseLabel.Location = new System.Drawing.Point(701, 103);
            this.costExpenseLabel.Name = "costExpenseLabel";
            this.costExpenseLabel.Size = new System.Drawing.Size(110, 17);
            this.costExpenseLabel.TabIndex = 18;
            this.costExpenseLabel.Text = "Cost of Expense";
            // 
            // itemLabel
            // 
            this.itemLabel.AutoSize = true;
            this.itemLabel.Location = new System.Drawing.Point(692, 62);
            this.itemLabel.Name = "itemLabel";
            this.itemLabel.Size = new System.Drawing.Size(119, 17);
            this.itemLabel.TabIndex = 17;
            this.itemLabel.Text = "Name of Expense";
            // 
            // addExpenseButton
            // 
            this.addExpenseButton.Location = new System.Drawing.Point(993, 50);
            this.addExpenseButton.Name = "addExpenseButton";
            this.addExpenseButton.Size = new System.Drawing.Size(159, 36);
            this.addExpenseButton.TabIndex = 16;
            this.addExpenseButton.Text = "Add Expense";
            this.addExpenseButton.UseVisualStyleBackColor = true;
            this.addExpenseButton.Click += new System.EventHandler(this.ExpenseButton_Click);
            // 
            // needsMoneyTextBox
            // 
            this.needsMoneyTextBox.Enabled = false;
            this.needsMoneyTextBox.Location = new System.Drawing.Point(142, 291);
            this.needsMoneyTextBox.Name = "needsMoneyTextBox";
            this.needsMoneyTextBox.Size = new System.Drawing.Size(136, 22);
            this.needsMoneyTextBox.TabIndex = 15;
            // 
            // wantsMoneyTextBox
            // 
            this.wantsMoneyTextBox.Enabled = false;
            this.wantsMoneyTextBox.Location = new System.Drawing.Point(142, 327);
            this.wantsMoneyTextBox.Name = "wantsMoneyTextBox";
            this.wantsMoneyTextBox.Size = new System.Drawing.Size(136, 22);
            this.wantsMoneyTextBox.TabIndex = 14;
            // 
            // savingsMoneyTextBox
            // 
            this.savingsMoneyTextBox.Enabled = false;
            this.savingsMoneyTextBox.Location = new System.Drawing.Point(142, 364);
            this.savingsMoneyTextBox.Name = "savingsMoneyTextBox";
            this.savingsMoneyTextBox.Size = new System.Drawing.Size(136, 22);
            this.savingsMoneyTextBox.TabIndex = 13;
            // 
            // totalMoneyTextBox
            // 
            this.totalMoneyTextBox.Location = new System.Drawing.Point(142, 253);
            this.totalMoneyTextBox.Name = "totalMoneyTextBox";
            this.totalMoneyTextBox.Size = new System.Drawing.Size(136, 22);
            this.totalMoneyTextBox.TabIndex = 12;
            this.totalMoneyTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TotalMoneyTextBox_KeyDown);
            // 
            // totalMoneyLabel
            // 
            this.totalMoneyLabel.AutoSize = true;
            this.totalMoneyLabel.Location = new System.Drawing.Point(50, 256);
            this.totalMoneyLabel.Name = "totalMoneyLabel";
            this.totalMoneyLabel.Size = new System.Drawing.Size(86, 17);
            this.totalMoneyLabel.TabIndex = 11;
            this.totalMoneyLabel.Text = "Total Money";
            // 
            // savingsMoneyLabel
            // 
            this.savingsMoneyLabel.AutoSize = true;
            this.savingsMoneyLabel.Location = new System.Drawing.Point(78, 367);
            this.savingsMoneyLabel.Name = "savingsMoneyLabel";
            this.savingsMoneyLabel.Size = new System.Drawing.Size(58, 17);
            this.savingsMoneyLabel.TabIndex = 10;
            this.savingsMoneyLabel.Text = "Savings";
            // 
            // wantsMoneyLabel
            // 
            this.wantsMoneyLabel.AutoSize = true;
            this.wantsMoneyLabel.Location = new System.Drawing.Point(88, 330);
            this.wantsMoneyLabel.Name = "wantsMoneyLabel";
            this.wantsMoneyLabel.Size = new System.Drawing.Size(48, 17);
            this.wantsMoneyLabel.TabIndex = 9;
            this.wantsMoneyLabel.Text = "Wants";
            // 
            // needsMoneyLabel
            // 
            this.needsMoneyLabel.AutoSize = true;
            this.needsMoneyLabel.Location = new System.Drawing.Point(87, 294);
            this.needsMoneyLabel.Name = "needsMoneyLabel";
            this.needsMoneyLabel.Size = new System.Drawing.Size(49, 17);
            this.needsMoneyLabel.TabIndex = 8;
            this.needsMoneyLabel.Text = "Needs";
            // 
            // totalTextBox
            // 
            this.totalTextBox.Enabled = false;
            this.totalTextBox.Location = new System.Drawing.Point(142, 180);
            this.totalTextBox.Name = "totalTextBox";
            this.totalTextBox.Size = new System.Drawing.Size(136, 22);
            this.totalTextBox.TabIndex = 7;
            this.totalTextBox.TextChanged += new System.EventHandler(this.TotalTextBox_TextChanged);
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Location = new System.Drawing.Point(80, 183);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(56, 17);
            this.totalLabel.TabIndex = 6;
            this.totalLabel.Text = "Total %";
            // 
            // savingLabel
            // 
            this.savingLabel.AutoSize = true;
            this.savingLabel.Location = new System.Drawing.Point(62, 142);
            this.savingLabel.Name = "savingLabel";
            this.savingLabel.Size = new System.Drawing.Size(74, 17);
            this.savingLabel.TabIndex = 5;
            this.savingLabel.Text = "Savings %";
            // 
            // wantsLabel
            // 
            this.wantsLabel.AutoSize = true;
            this.wantsLabel.Location = new System.Drawing.Point(72, 101);
            this.wantsLabel.Name = "wantsLabel";
            this.wantsLabel.Size = new System.Drawing.Size(64, 17);
            this.wantsLabel.TabIndex = 4;
            this.wantsLabel.Text = "Wants %";
            // 
            // needsLabel
            // 
            this.needsLabel.AutoSize = true;
            this.needsLabel.Location = new System.Drawing.Point(71, 60);
            this.needsLabel.Name = "needsLabel";
            this.needsLabel.Size = new System.Drawing.Size(65, 17);
            this.needsLabel.TabIndex = 3;
            this.needsLabel.Text = "Needs %";
            // 
            // savingsTextBox
            // 
            this.savingsTextBox.Location = new System.Drawing.Point(142, 139);
            this.savingsTextBox.Name = "savingsTextBox";
            this.savingsTextBox.Size = new System.Drawing.Size(136, 22);
            this.savingsTextBox.TabIndex = 2;
            this.savingsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SavingsTextBox_KeyDown);
            // 
            // wantsTextBox
            // 
            this.wantsTextBox.Location = new System.Drawing.Point(142, 98);
            this.wantsTextBox.Name = "wantsTextBox";
            this.wantsTextBox.Size = new System.Drawing.Size(136, 22);
            this.wantsTextBox.TabIndex = 1;
            this.wantsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WantsTextBox_KeyDown);
            // 
            // needsTextBox
            // 
            this.needsTextBox.Location = new System.Drawing.Point(142, 57);
            this.needsTextBox.Name = "needsTextBox";
            this.needsTextBox.Size = new System.Drawing.Size(136, 22);
            this.needsTextBox.TabIndex = 0;
            this.needsTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NeedsTextBox_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1212, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newToolStripMenuItem.Text = "New (Ctrl + N)";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.openToolStripMenuItem.Text = "Open (Ctrl + O)";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.saveToolStripMenuItem.Text = "Save (Ctrl + S)";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.closeToolStripMenuItem.Text = "Close (Alt + F4)";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.ClearToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // expenseButton
            // 
            this.expenseButton.Location = new System.Drawing.Point(993, 50);
            this.expenseButton.Name = "expenseButton";
            this.expenseButton.Size = new System.Drawing.Size(159, 36);
            this.expenseButton.TabIndex = 16;
            this.expenseButton.Text = "Add Expense";
            this.expenseButton.UseVisualStyleBackColor = true;
            this.expenseButton.Click += new System.EventHandler(this.ExpenseButton_Click);
            // 
            // Budgeting_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 664);
            this.Controls.Add(this.budgetingPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Budgeting_Form";
            this.Text = "Budget";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.budgetingPanel.ResumeLayout(false);
            this.budgetingPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel budgetingPanel;
        private System.Windows.Forms.TextBox savingsTextBox;
        private System.Windows.Forms.TextBox wantsTextBox;
        private System.Windows.Forms.TextBox needsTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Label savingLabel;
        private System.Windows.Forms.Label wantsLabel;
        private System.Windows.Forms.Label needsLabel;
        private System.Windows.Forms.TextBox totalTextBox;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label needsMoneyLabel;
        private System.Windows.Forms.TextBox needsMoneyTextBox;
        private System.Windows.Forms.TextBox wantsMoneyTextBox;
        private System.Windows.Forms.TextBox savingsMoneyTextBox;
        private System.Windows.Forms.TextBox totalMoneyTextBox;
        private System.Windows.Forms.Label totalMoneyLabel;
        private System.Windows.Forms.Label savingsMoneyLabel;
        private System.Windows.Forms.Label wantsMoneyLabel;
        private System.Windows.Forms.Button addExpenseButton;
        private System.Windows.Forms.ComboBox expenseComboBox;
        private System.Windows.Forms.TextBox costOfExpense;
        private System.Windows.Forms.TextBox expenseNameBox;
        private System.Windows.Forms.Label DeductFrom;
        private System.Windows.Forms.Label costExpenseLabel;
        private System.Windows.Forms.Label itemLabel;
        private System.Windows.Forms.ComboBox viewExpensesComboBox;
        private System.Windows.Forms.Button viewExpensesButton;
        private System.Windows.Forms.Label viewExpenseLabel;
        private System.Windows.Forms.Button editExpensesButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button expenseButton;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
    }
}

