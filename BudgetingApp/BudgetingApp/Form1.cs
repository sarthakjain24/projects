using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace BudgetingApp
{
    /// <summary>
    /// A Budgeting form representing the GUI
    /// </summary>
    public partial class Budgeting_Form : Form
    {
        // A variable to represent the total money available to allocate
        private double TotalMoneyAvailable;

        //A variable to represent the needs percentage
        private double NeedsPercentage;

        //A variable to represent the wants percentage
        private double WantsPercentage;

        //A variable to represent the savings percentage
        private double SavingsPercentage;

        //A variable to represent the total percentage, which is the total of the needs, wants and savings        
        private double TotalPercentage;

        //A variable to represent the money allocated to the needs
        private double NeedsMoney;

        //A variable to represent the money allocated to the savings
        private double SavingsMoney;

        //A variable to represent the money allocated to the wants
        private double WantsMoney;

        //A variable to represent the money allocated to the needs spent
        private double NeedsSpent;

        //A variable to represent the money allocated to the wants spent
        private double WantsSpent;

        //A variable to represent the money allocated to the savings spent
        private double SavingsSpent;

        //A variable to represent the needs item and the value associated with it
        private Dictionary<String, Double> NeedsHashMap;

        //A variable to represent the wants item and the value associated with it
        private Dictionary<String, Double> WantsHashMap;

        //A variable to represent the savings item and the value associated with it
        private Dictionary<String, Double> SavingsHashMap;

       


        /// <summary>
        /// The main constructor for the budgeting form initializing the values
        /// </summary>
        public Budgeting_Form()
        {
            InitializeComponent();
            TotalMoneyAvailable = 0;
            NeedsPercentage = 0;
            WantsPercentage = 0;
            SavingsPercentage = 0;
            TotalPercentage = 0;
            NeedsMoney = 0;
            SavingsMoney = 0;
            WantsMoney = 0;
            NeedsSpent = 0;
            WantsSpent = 0;
            SavingsSpent = 0;
            NeedsHashMap = new Dictionary<String, Double>();
            WantsHashMap = new Dictionary<String, Double>();
            SavingsHashMap = new Dictionary<String, Double>();
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveApp();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewBudgetingApp();
        }
        /// <summary>
        /// Creates a new budgeting app and closes the current form
        /// </summary>
        private void NewBudgetingApp()
        {
            SaveApp();
            Budgeting_Form bud = new Budgeting_Form();
            this.Hide();
            bud.Show();
        }

        /// <summary>
        /// Saves the budgeting application locally
        /// </summary>
        private void SaveApp()
        {
            Stream myStream;
            SaveFileDialog save = new SaveFileDialog();
            //Gives the user two options to filter through when saving the file, .budg and all files
            save.Filter = "Budget (*.budg)|*.budg|All files (*.*)|*.*";
            save.FilterIndex = 1;
            save.RestoreDirectory = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                //Saves the file name that the user inputted
                string fileName = save.FileName;
                if ((myStream = save.OpenFile()) != null)
                {
                    myStream.Close();
                }
                Save(fileName);
            }
        }

        /// <summary>
        /// A helper method to save the app as an XML file
        /// </summary>
        /// <param name="file"></param>
        private void Save(String file)
        {
            //Creates the settings of the XMLWriterSettings
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            try
            {
                //Uses the XmlWriter to create an XML file using the settings and the fileName 
                //provided where the XML will be stored
                using (XmlWriter xml = XmlWriter.Create(file, settings))
                {
                    //Starts writing the document
                    xml.WriteStartDocument();

                    //Starts the budgeting
                    xml.WriteStartElement("budgeting");

                    // Writes the needs percentage
                    xml.WriteElementString("NeedsPercentage", NeedsPercentage.ToString());

                    // Writes the wants percentage
                    xml.WriteElementString("WantsPercentage", WantsPercentage.ToString());

                    // Writes the savings percentage
                    xml.WriteElementString("SavingsPercentage", SavingsPercentage.ToString());

                    // Writes the total money
                    xml.WriteElementString("TotalMoney", TotalMoneyAvailable.ToString());


                    // Writes the needs money
                    xml.WriteElementString("NeedsMoney", NeedsMoney + "");

                    // Writes the wants money
                    xml.WriteElementString("WantsMoney", WantsMoney + "");

                    // Writes the savings money
                    xml.WriteElementString("SavingsMoney", SavingsMoney + "");


                    foreach (String name in NeedsHashMap.Keys)
                    {
                        xml.WriteStartElement("Needs");

                
                        xml.WriteElementString("NeedsKey", name);
                        xml.WriteElementString("NeedsValue", NeedsHashMap[name].ToString());
                        xml.WriteEndElement();
                    }

                    foreach (String name in WantsHashMap.Keys)
                    {
                        xml.WriteStartElement("Wants");
            
                        xml.WriteElementString("WantsKey", name);
                        xml.WriteElementString("WantsValue", WantsHashMap[name].ToString());
                        xml.WriteEndElement();
                    }

                    foreach (String name in SavingsHashMap.Keys)
                    {
                        xml.WriteStartElement("Savings");

                        xml.WriteElementString("SavingsKey", name);
                        xml.WriteElementString("SavingsValue", SavingsHashMap[name].ToString());
                        xml.WriteEndElement();
                    }

                    //Ends the budgeting
                    xml.WriteEndElement();
                    //Ends the document
                    xml.WriteEndDocument();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //A string to get the contents of the file
            string fileContent = null;

            //Initializes the openFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Has two options in the openFileDialog (budgeting files and all files)
            openFileDialog.Filter = "Budget (*.budg)|*.budg|All files (*.*)|*.*";

            //Keeps budgeting files as the default
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            //If the user saves the file, then goes in here
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                String filePath = openFileDialog.FileName;

                //Clears the initial text boxes
                ClearTextBoxes();

                //Uses the XMLReader to populate the application
                using (XmlReader xmlReader = XmlReader.Create(filePath))
                {
                    string key = "";
                    double value = 0;
                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {
                            if (xmlReader.Name.Equals("budgeting"))
                            {

                            }
                            else if (xmlReader.Name.Equals("NeedsPercentage"))
                            {
                                xmlReader.Read();
                                string val = xmlReader.Value;
                                Double.TryParse(val, out NeedsPercentage);
                                needsTextBox.Text = NeedsPercentage + "";
                            }
                            else if (xmlReader.Name.Equals("WantsPercentage"))
                            {
                                xmlReader.Read();
                                string val = xmlReader.Value;
                                Double.TryParse(val, out WantsPercentage);
                                wantsTextBox.Text = WantsPercentage + "";
                            }
                            else if (xmlReader.Name.Equals("SavingsPercentage"))
                            {
                                xmlReader.Read();
                                string val = xmlReader.Value;
                                Double.TryParse(val, out SavingsPercentage);
                                savingsTextBox.Text = SavingsPercentage + "";
                            }
                            else if (xmlReader.Name.Equals("TotalMoney"))
                            {
                                xmlReader.Read();
                                string val = xmlReader.Value;
                                double.TryParse(val, out TotalMoneyAvailable);

                                totalMoneyTextBox.Text = "$" + val;
                                totalMoneyTextBox.Enabled = false;
                            }
                            else if (xmlReader.Name.Equals("NeedsMoney"))
                            {
                                xmlReader.Read();
                                string val = xmlReader.Value;
                                Double.TryParse(val, out NeedsMoney);
                            }
                            else if (xmlReader.Name.Equals("WantsMoney"))
                            {
                                xmlReader.Read();
                                string val = xmlReader.Value;
                                Double.TryParse(val, out WantsMoney);
                            }
                            else if (xmlReader.Name.Equals("SavingsMoney"))
                            {
                                xmlReader.Read();
                                string val = xmlReader.Value;
                                Double.TryParse(val, out SavingsMoney);
                            }
                            else if (xmlReader.Name.Equals("NeedsKey"))
                            {
                                xmlReader.Read();
                                key = xmlReader.Value;
                            }
                            else if (xmlReader.Name.Equals("NeedsValue"))
                            {
                                xmlReader.Read();
                                string str = xmlReader.Value;
                                double.TryParse(str, out value);
                                NeedsHashMap.Add(key, value);
                                key = "";
                                value = 0;

                            }
                            else if (xmlReader.Name.Equals("WantsKey"))
                            {
                                xmlReader.Read();
                                key = xmlReader.Value;

                            }
                            else if (xmlReader.Name.Equals("WantsValue"))
                            {
                                xmlReader.Read();
                                string str = xmlReader.Value;
                                double.TryParse(str, out value);
                                WantsHashMap.Add(key, value);
                                key = "";
                                value = 0;

                            }
                            else if (xmlReader.Name.Equals("SavingsKey"))
                            {
                                xmlReader.Read();
                                key = xmlReader.Value;
                            }
                            else if (xmlReader.Name.Equals("SavingsValue"))
                            {
                                xmlReader.Read();
                                string str = xmlReader.Value;
                                double.TryParse(str, out value);
                                SavingsHashMap.Add(key, value);
                                key = "";
                                value = 0;

                            }
                        }
                    }
                    FillMoneyValues();
                }



                //Reads the file and sets it as the fileContents
                Stream fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// Text box that represents the total percentage shown
        /// </summary>
        private void TotalTextBox_TextChanged(object sender, EventArgs e)
        {
            CalculatePercentage();
        }

        private void TotalMoneyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (totalMoneyTextBox.Text.Length != 0)
                {
                    char firstDigit = totalMoneyTextBox.Text[0];
                    if (firstDigit == '-')
                    {
                        MessageBox.Show("Money has to be a positive value");
                    }
                    bool validMoney = false;
                    double money = 0;
                    if (Char.IsDigit(firstDigit))
                    {
                        validMoney = double.TryParse(totalMoneyTextBox.Text, out double mo);
                        money = mo;
                    }
                    else
                    {
                        validMoney = double.TryParse(totalMoneyTextBox.Text.Substring(1), out double moh);
                        money = moh;
                    }

                    if (validMoney)
                    {
                        TotalMoneyAvailable = money;
                        totalMoneyTextBox.Text = "$" + money;
                        totalMoneyTextBox.Enabled = false;
                        FillMoneyValues();
                    }
                    else
                    {
                        MessageBox.Show("Enter valid money that starts with a currency sign and then the monetary value. Valid examples are $123 or 123 or $123.45 or 123.45");
                    }
                }
                else
                {
                    MessageBox.Show("Enter valid money value");
                }
            }
        }

        private void NeedsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (needsTextBox.Text.Length != 0)
                {
                    char lastDigit = needsTextBox.Text[needsTextBox.Text.Length - 1];
                    e.SuppressKeyPress = true;
                    if (Char.IsDigit(lastDigit))
                    {
                        Double.TryParse(needsTextBox.Text, out double result);
                        if (result > 100 - (WantsPercentage + SavingsPercentage))
                        {
                            MessageBox.Show("The percentage has to be less than 100%");
                            needsTextBox.Text = "";
                        }
                        else
                        {
                            NeedsPercentage = result;
                            e.Handled = true;
                            FillMoneyValues();
                        }
                    }
                    else if (lastDigit == '%')
                    {
                        Double.TryParse(needsTextBox.Text.Substring(0, needsTextBox.Text.Length - 1), out double result);
                        if (result > 100 - (WantsPercentage + SavingsPercentage))
                        {
                            MessageBox.Show("The percentage has to be less than 100%");
                            needsTextBox.Text = "";
                        }
                        else
                        {
                            NeedsPercentage = result;
                            e.Handled = true;
                            FillMoneyValues();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The value of the text box should only be a number, or only have the last digit be a percentage sign(%)");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a valid needs percentage");
                }
            }
        }

        private void WantsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (wantsTextBox.Text.Length != 0)
                {
                    char lastDigit = wantsTextBox.Text[wantsTextBox.Text.Length - 1];
                    e.SuppressKeyPress = true;
                    if (Char.IsDigit(lastDigit))
                    {
                        Double.TryParse(wantsTextBox.Text, out double result);
                        if (result > 100 - (NeedsPercentage + SavingsPercentage))
                        {
                            MessageBox.Show("The percentage has to be less than 100%");
                            wantsTextBox.Text = "";
                        }
                        else
                        {
                            WantsPercentage = result;
                            e.Handled = true;
                            FillMoneyValues();
                        }
                    }
                    else if (lastDigit == '%')
                    {
                        Double.TryParse(wantsTextBox.Text.Substring(0, wantsTextBox.Text.Length - 1), out double result);
                        if (result > 100 - (NeedsPercentage + SavingsPercentage))
                        {
                            MessageBox.Show("The percentage has to be less than 100%");
                            wantsTextBox.Text = "";
                        }
                        else
                        {
                            WantsPercentage = result;
                            e.Handled = true;
                            FillMoneyValues();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The value of the text box should only be a number, or only have the last digit be a percentage sign(%)");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a valid needs percentage");
                }
            }
        }

        private void SavingsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (savingsTextBox.Text.Length != 0)
                {
                    char lastDigit = savingsTextBox.Text[savingsTextBox.Text.Length - 1];
                    e.SuppressKeyPress = true;
                    if (Char.IsDigit(lastDigit))
                    {
                        Double.TryParse(savingsTextBox.Text, out double result);
                        if (result > 100 - (NeedsPercentage + WantsPercentage))
                        {
                            MessageBox.Show("The percentage has to be less than 100%");
                            savingsTextBox.Text = "";
                        }
                        else
                        {
                            SavingsPercentage = result;
                            e.Handled = true;
                            FillMoneyValues();
                        }
                    }
                    else if (lastDigit == '%')
                    {
                        Double.TryParse(savingsTextBox.Text.Substring(0, savingsTextBox.Text.Length - 1), out double result);
                        if (result > 100 - (NeedsPercentage + WantsPercentage))
                        {
                            MessageBox.Show("The percentage has to be less than 100%");
                            savingsTextBox.Text = "";
                        }
                        else
                        {
                            SavingsPercentage = result;
                            e.Handled = true;
                            FillMoneyValues();
                        }
                    }
                    else
                    {
                        MessageBox.Show("The value of the text box should only be a number, or only have the last digit be a percentage sign(%)");
                    }
                }
                else
                {
                    MessageBox.Show("Enter a valid savings percentage");
                }
            }
        }

        /// <summary>
        /// The expense button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExpenseButton_Click(object sender, EventArgs e)
        {
            if (expenseNameBox.Text != "" && costOfExpense.Text != "")
            {
                char firstDigit = costOfExpense.Text[0];
                if (firstDigit == '-')
                {
                    MessageBox.Show("Money has to be a positive value");
                }
                bool validMoney = false;
                double money = 0;
                if (Char.IsDigit(firstDigit))
                {
                    validMoney = double.TryParse(costOfExpense.Text, out double mo);
                    money = mo;
                }
                else
                {
                    validMoney = double.TryParse(costOfExpense.Text.Substring(1), out double moh);
                    money = moh;
                }

                if (validMoney)
                {
                    String key = expenseNameBox.Text.ToLower();
                    if (expenseComboBox.Text == "Needs")
                    {
                        if (!NeedsHashMap.ContainsKey(key))
                        {
                            if ((NeedsSpent + money) <= NeedsMoney)
                            {

                                NeedsHashMap.Add(key, money);
                                NeedsSpent += money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                        else
                        {
                            NeedsHashMap.TryGetValue(key, out double valFromBefore);
                            double newVal = valFromBefore + money;
                            if ((NeedsSpent + money) <= NeedsMoney)
                            {
                                NeedsSpent += money;
                                NeedsHashMap[key] = newVal;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                    }
                    else if (expenseComboBox.Text == "Wants")
                    {
                        if (!WantsHashMap.ContainsKey(key))
                        {
                            if ((WantsSpent + money) <= WantsMoney)
                            {
                                WantsHashMap.Add(key, money);
                                WantsSpent += money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                        else
                        {
                            WantsHashMap.TryGetValue(key, out double valFromBefore);
                            double newVal = valFromBefore + money;
                            if ((WantsSpent + money) <= WantsMoney)
                            {
                                WantsHashMap[key] = newVal;
                                WantsSpent += money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                    }
                    else if (expenseComboBox.Text == "Savings")
                    {
                        if (!SavingsHashMap.ContainsKey(key))
                        {

                            if ((SavingsSpent + money) <= SavingsMoney)
                            {
                                SavingsHashMap.Add(key, money);
                                SavingsSpent += money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                        else
                        {
                            SavingsHashMap.TryGetValue(key, out double valFromBefore);
                            double newVal = valFromBefore + money;
                            if ((SavingsSpent + money) <= SavingsMoney)
                            {
                                SavingsHashMap[key] = newVal;
                                SavingsSpent += money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                    }
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Enter valid money that starts with a currency sign and then the monetary value. Valid examples are $123 or 123 or $123.45 or 123.45");
                }
            }
            else if (costOfExpense.Text == "" && expenseNameBox.Text == "")
            {
                MessageBox.Show("Enter a non empty input for the name of the expense, and enter a valid money value");
            }
            else if (costOfExpense.Text == "")
            {
                MessageBox.Show("Enter a non empty input for the name of the expense");
            }
            else if (expenseNameBox.Text == "")
            {
                MessageBox.Show("Enter valid money value");
            }
        }

        private void EditExpensesButton_Click(object sender, EventArgs e)
        {
            if (expenseNameBox.Text != "" && costOfExpense.Text != "")
            {
                char firstDigit = costOfExpense.Text[0];
                if (firstDigit == '-')
                {
                    MessageBox.Show("Money has to be a positive value");
                }
                bool validMoney = false;
                double money = 0;
                if (Char.IsDigit(firstDigit))
                {
                    validMoney = double.TryParse(costOfExpense.Text, out double mo);
                    money = mo;
                }
                else
                {
                    validMoney = double.TryParse(costOfExpense.Text.Substring(1), out double moh);
                    money = moh;
                }

                if (validMoney)
                {
                    String key = expenseNameBox.Text.ToLower();
                    if (expenseComboBox.Text == "Needs")
                    {
                        if (!NeedsHashMap.ContainsKey(key))
                        {
                            MessageBox.Show("Needs section does not include the expense name indicated");
                        }
                        else
                        {
                            NeedsHashMap.TryGetValue(key, out double valFromBefore);
                            NeedsSpent -= valFromBefore;
                            if ((NeedsSpent + money) <= NeedsMoney)
                            {
                                NeedsSpent += money;
                                NeedsHashMap[key] = money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                    }
                    else if (expenseComboBox.Text == "Wants")
                    {
                        if (!WantsHashMap.ContainsKey(key))
                        {
                            MessageBox.Show("Wants section does not include the expense name indicated");
                        }
                        else
                        {
                            WantsHashMap.TryGetValue(key, out double valFromBefore);
                            WantsSpent = WantsSpent - valFromBefore;

                            if ((WantsSpent + money) <= WantsMoney)
                            {
                                WantsSpent += money;
                                WantsHashMap[key] = money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                    }
                    else if (expenseComboBox.Text == "Savings")
                    {
                        if (!SavingsHashMap.ContainsKey(key))
                        {
                            MessageBox.Show("Savings section does not include the expense name indicated");
                        }
                        else
                        {
                            SavingsHashMap.TryGetValue(key, out double valFromBefore);
                            SavingsSpent = SavingsSpent - valFromBefore;

                            if ((SavingsSpent + money) <= SavingsMoney)
                            {
                                SavingsSpent += money;
                                SavingsHashMap[key] = money;
                            }
                            else
                            {
                                MessageBox.Show("Can't spend more than the money currently allocated");
                            }
                        }
                    }
                    ClearTextBoxes();
                }
                else
                {
                    MessageBox.Show("Enter valid money that starts with a currency sign and then the monetary value. Valid examples are $123 or 123 or $123.45 or 123.45");
                }
            }
            else if (costOfExpense.Text == "" && expenseNameBox.Text == "")
            {
                MessageBox.Show("Enter a non empty input for the name of the expense, and enter a valid money value");
            }
            else if (costOfExpense.Text == "")
            {
                MessageBox.Show("Enter a non empty input for the name of the expense");
            }
            else if (expenseNameBox.Text == "")
            {
                MessageBox.Show("Enter valid money value");
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (expenseNameBox.Text != "")
            {
                String key = expenseNameBox.Text.ToLower();
                if (expenseComboBox.Text == "Needs")
                {
                    if (!NeedsHashMap.ContainsKey(key))
                    {
                        MessageBox.Show("Needs section does not include the expense name indicated");
                    }
                    else
                    {
                        NeedsHashMap.TryGetValue(key, out double valFromBefore);
                        if ((NeedsSpent - valFromBefore) > WantsMoney)
                        {
                            NeedsSpent = NeedsSpent - valFromBefore;
                            NeedsHashMap.Remove(key);
                        }
                        else
                        {
                            MessageBox.Show("Can't have more money spent than the money currently allocated");
                        }
                    }

                }
                else if (expenseComboBox.Text == "Wants")
                {
                    if (!WantsHashMap.ContainsKey(key))
                    {
                        MessageBox.Show("Wants section does not include the expense name indicated");
                    }
                    else
                    {
                        WantsHashMap.TryGetValue(key, out double valFromBefore);
                        if ((WantsSpent - valFromBefore) > WantsMoney)
                        {
                            WantsSpent = WantsSpent - valFromBefore;
                            WantsHashMap.Remove(key);
                        }
                        else
                        {
                            MessageBox.Show("Can't have more money spent than the money currently allocated");
                        }
                    }
                }
                else if (expenseComboBox.Text == "Savings")
                {
                    if (!SavingsHashMap.ContainsKey(key))
                    {
                        MessageBox.Show("Savings section does not include the expense name indicated");
                    }
                    else
                    {
                        SavingsHashMap.TryGetValue(key, out double valFromBefore);
                        if ((SavingsSpent - valFromBefore) > SavingsMoney)
                        {
                            SavingsSpent = SavingsSpent - valFromBefore;
                            SavingsHashMap.Remove(key);
                        }
                        else
                        {
                            MessageBox.Show("Can't have more money spent than the money currently allocated");
                        }
                    }
                }
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show("Enter a non empty input for the name of the expense");
            }
        }

        /// <summary>
        /// Clears the text boxes of the name inputs
        /// </summary>
        private void ClearTextBoxes()
        {
            costOfExpense.Text = "";
            expenseNameBox.Text = "";
            expenseComboBox.Text = "";
        }

        /// <summary>
        /// Calculates the total percentage based on the needs, wants and savings
        /// </summary>
        private void CalculatePercentage()
        {
            double count = NeedsPercentage + WantsPercentage + SavingsPercentage;
            TotalPercentage = count;
            totalTextBox.Text = count + " %";
        }

        /// <summary>
        /// Fill Money boxes up if all neccessary values are filled in
        /// </summary>
        private void FillMoneyValues()
        {
            CalculatePercentage();
            if (savingsTextBox.Text != "" && wantsTextBox.Text != "" && needsTextBox.Text != "" && totalMoneyTextBox.Text != "" && totalTextBox.Text != "")
            {
                NeedsMoney = (NeedsPercentage / TotalPercentage) * TotalMoneyAvailable;
                needsMoneyTextBox.Text = "$" + String.Format("{0:0.00}", NeedsMoney);
                WantsMoney = (WantsPercentage / TotalPercentage) * TotalMoneyAvailable;
                wantsMoneyTextBox.Text = "$" + String.Format("{0:0.00}", WantsMoney);
                SavingsMoney = (SavingsPercentage / TotalPercentage) * TotalMoneyAvailable;
                savingsMoneyTextBox.Text = "$" + String.Format("{0:0.00}", SavingsMoney);
            }
        }

        private void ViewExpensesButton_Click(object sender, EventArgs e)
        {
            string choice = viewExpensesComboBox.Text;
            if (choice != "")
            {
                if (choice == "Needs")
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (NeedsHashMap.Count == 0)
                    {
                        MessageBox.Show("There is no values entered as a need");
                    }
                    else
                    {
                        stringBuilder.Append("Name").Append("\t").Append("Price").Append("\n");

                        foreach (String key in NeedsHashMap.Keys)
                        {
                            String upperCase = char.ToUpper(key[0]) + key.Substring(1);
                            stringBuilder.Append(upperCase.Trim()).Append("\t").Append("$").Append(String.Format("{0:0.00}", NeedsHashMap[key])).Append("\n");
                        }
                        MessageBox.Show(stringBuilder.ToString());
                    }
                }
                else if (choice == "Wants")
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (WantsHashMap.Count == 0)
                    {
                        MessageBox.Show("There is no values entered as a want");
                    }
                    else
                    {
                        stringBuilder.Append("Name").Append("\t").Append("Price").Append("\n");

                        foreach (String key in WantsHashMap.Keys)
                        {
                            String upperCase = char.ToUpper(key[0]) + key.Substring(1);
                            stringBuilder.Append(upperCase.Trim()).Append("\t").Append("$").Append(String.Format("{0:0.00}", WantsHashMap[key])).Append("\n");
                        }
                        MessageBox.Show(stringBuilder.ToString());
                    }
                }
                else if (choice == "Savings")
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    if (SavingsHashMap.Count == 0)
                    {
                        MessageBox.Show("There is no values entered as a savings");
                    }
                    else
                    {
                        stringBuilder.Append("Name").Append("\t").Append("Price").Append("\n");

                        foreach (String key in SavingsHashMap.Keys)
                        {
                            String upperCase = char.ToUpper(key[0]) + key.Substring(1);
                            stringBuilder.Append(upperCase.Trim()).Append("\t").Append("$").Append(String.Format("{0:0.00}", SavingsHashMap[key])).Append("\n");
                        }
                        MessageBox.Show(stringBuilder.ToString());
                    }
                }
                viewExpensesComboBox.Text = "";
            }
            else
            {
                MessageBox.Show("Choose a valid choice");
            }
        }
        /// <summary>
        /// The help menu item that helps the user with some basic tips
        /// </summary>
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press the enter key after adding the percentage for it to register." + "\n" +
                "The total money that you have cannot change after first entering it." + "\n" +
                "You can't spend more than the money that you have allocated in your sub budget");
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TotalMoneyAvailable = 0;
            NeedsPercentage = 0;
            WantsPercentage = 0;
            SavingsPercentage = 0;
            TotalPercentage = 0;
            NeedsMoney = 0;
            SavingsMoney = 0;
            WantsMoney = 0;
            NeedsSpent = 0;
            WantsSpent = 0;
            SavingsSpent = 0;
            NeedsHashMap.Clear();
            WantsHashMap.Clear();
            SavingsHashMap.Clear(); 
            totalMoneyTextBox.Enabled = true;
        }

    }
}
