using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void calcTipButton_Click(object sender, EventArgs e)
        {
            double bill = Convert.ToDouble(billAmtGiven.Text);
            double percentage = Convert.ToDouble(tipPercentTextBox.Text);
            double tip = bill * percentage / 100;
            double total = bill + tip;

            TipExpectedToPay.Text = tip.ToString();
            totalAmtBill.Text = total.ToString();
            //TipExpectedToPay.Text = (Convert.ToDouble(billAmtGiven.Text) * 0.2).ToString();
        }

        

        private void TipExpectedToPay_TextChanged(object sender, EventArgs e)
        {

        }

        private void calcTipButton_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void calcTipButton_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void tipPercentTextBox_TextChanged(object sender, EventArgs e)
        {
            updateTip();
            //checkInputSanity();
        }

        private void billAmtGiven_TextChanged(object sender, EventArgs e)
        {
            //checkInputSanity();
            updateTip();
        }

        private bool checkInputSanity()
        {
           if(!Double.TryParse(billAmtGiven.Text, out double unused) || !Double.TryParse(tipPercentTextBox.Text, out double unused2)){
                calcTipButton.Enabled = false;
                return false;
            }
            else
            {
                calcTipButton.Enabled = true;
                return true;
            }
        }
        private void updateTip()
        {
            if (!checkInputSanity())
            {
                return;
            }
            double bill = Convert.ToDouble(billAmtGiven.Text);
            double percentage = Convert.ToDouble(tipPercentTextBox.Text);
            double tip = bill * percentage / 100;
            double total = bill + tip;

            TipExpectedToPay.Text = tip.ToString();
            totalAmtBill.Text = total.ToString();
        }
        private void totalAmtBill_TextChanged(object sender, EventArgs e)
        {

        }

        private void tipAmtLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
