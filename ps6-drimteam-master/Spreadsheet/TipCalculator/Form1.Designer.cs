namespace TipCalculator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.totalBillLabel = new System.Windows.Forms.Label();
            this.calcTipButton = new System.Windows.Forms.Button();
            this.billAmtGiven = new System.Windows.Forms.TextBox();
            this.TipExpectedToPay = new System.Windows.Forms.TextBox();
            this.tipPercentLabel = new System.Windows.Forms.Label();
            this.tipAmtLabel = new System.Windows.Forms.Label();
            this.billAfterTip = new System.Windows.Forms.Label();
            this.totalAmtBill = new System.Windows.Forms.TextBox();
            this.tipPercentTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // totalBillLabel
            // 
            this.totalBillLabel.AutoSize = true;
            this.totalBillLabel.Location = new System.Drawing.Point(166, 94);
            this.totalBillLabel.Name = "totalBillLabel";
            this.totalBillLabel.Size = new System.Drawing.Size(105, 20);
            this.totalBillLabel.TabIndex = 0;
            this.totalBillLabel.Text = "Enter Total Bill";
            // 
            // calcTipButton
            // 
            this.calcTipButton.Location = new System.Drawing.Point(166, 186);
            this.calcTipButton.Name = "calcTipButton";
            this.calcTipButton.Size = new System.Drawing.Size(117, 34);
            this.calcTipButton.TabIndex = 1;
            this.calcTipButton.Text = "Compute Tip";
            this.calcTipButton.UseVisualStyleBackColor = true;
            this.calcTipButton.EnabledChanged += new System.EventHandler(this.calcTipButton_EnabledChanged);
            this.calcTipButton.VisibleChanged += new System.EventHandler(this.calcTipButton_VisibleChanged);
            this.calcTipButton.Click += new System.EventHandler(this.calcTipButton_Click);
            // 
            // billAmtGiven
            // 
            this.billAmtGiven.Location = new System.Drawing.Point(309, 96);
            this.billAmtGiven.Name = "billAmtGiven";
            this.billAmtGiven.Size = new System.Drawing.Size(136, 27);
            this.billAmtGiven.TabIndex = 2;
            this.billAmtGiven.TextChanged += new System.EventHandler(this.billAmtGiven_TextChanged);
            // 
            // TipExpectedToPay
            // 
            this.TipExpectedToPay.Location = new System.Drawing.Point(309, 238);
            this.TipExpectedToPay.Name = "TipExpectedToPay";
            this.TipExpectedToPay.Size = new System.Drawing.Size(134, 27);
            this.TipExpectedToPay.TabIndex = 3;
            this.TipExpectedToPay.TextChanged += new System.EventHandler(this.TipExpectedToPay_TextChanged);
            // 
            // tipPercentLabel
            // 
            this.tipPercentLabel.AutoSize = true;
            this.tipPercentLabel.Location = new System.Drawing.Point(173, 143);
            this.tipPercentLabel.Name = "tipPercentLabel";
            this.tipPercentLabel.Size = new System.Drawing.Size(84, 20);
            this.tipPercentLabel.TabIndex = 4;
            this.tipPercentLabel.Text = "Enter Tip %";
            this.tipPercentLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // tipAmtLabel
            // 
            this.tipAmtLabel.AutoSize = true;
            this.tipAmtLabel.Location = new System.Drawing.Point(166, 245);
            this.tipAmtLabel.Name = "tipAmtLabel";
            this.tipAmtLabel.Size = new System.Drawing.Size(79, 20);
            this.tipAmtLabel.TabIndex = 6;
            this.tipAmtLabel.Text = "Tip To Pay:";
            this.tipAmtLabel.Click += new System.EventHandler(this.tipAmtLabel_Click);
            // 
            // billAfterTip
            // 
            this.billAfterTip.AutoSize = true;
            this.billAfterTip.Location = new System.Drawing.Point(166, 283);
            this.billAfterTip.Name = "billAfterTip";
            this.billAfterTip.Size = new System.Drawing.Size(132, 20);
            this.billAfterTip.TabIndex = 7;
            this.billAfterTip.Text = "Total Bill After Tip:";
            // 
            // totalAmtBill
            // 
            this.totalAmtBill.Location = new System.Drawing.Point(310, 282);
            this.totalAmtBill.Name = "totalAmtBill";
            this.totalAmtBill.Size = new System.Drawing.Size(134, 27);
            this.totalAmtBill.TabIndex = 8;
            this.totalAmtBill.TextChanged += new System.EventHandler(this.totalAmtBill_TextChanged);
            // 
            // tipPercentTextBox
            // 
            this.tipPercentTextBox.Location = new System.Drawing.Point(309, 143);
            this.tipPercentTextBox.Name = "tipPercentTextBox";
            this.tipPercentTextBox.Size = new System.Drawing.Size(136, 27);
            this.tipPercentTextBox.TabIndex = 9;
            this.tipPercentTextBox.TextChanged += new System.EventHandler(this.tipPercentTextBox_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tipPercentTextBox);
            this.Controls.Add(this.totalAmtBill);
            this.Controls.Add(this.billAfterTip);
            this.Controls.Add(this.tipAmtLabel);
            this.Controls.Add(this.tipPercentLabel);
            this.Controls.Add(this.TipExpectedToPay);
            this.Controls.Add(this.billAmtGiven);
            this.Controls.Add(this.calcTipButton);
            this.Controls.Add(this.totalBillLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label totalBillLabel;
        private System.Windows.Forms.Button calcTipButton;
        private System.Windows.Forms.TextBox billAmtGiven;
        private System.Windows.Forms.TextBox TipExpectedToPay;
        private System.Windows.Forms.Label tipPercentLabel;
        private System.Windows.Forms.Label tipAmtLabel;
        private System.Windows.Forms.Label billAfterTip;
        private System.Windows.Forms.TextBox totalAmtBill;
        private System.Windows.Forms.TextBox tipPercentTextBox;
    }
}

