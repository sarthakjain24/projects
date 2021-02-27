using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SpreadsheetUtilities;
using SS;
namespace SpreadsheetGUI
{
    /// <summary>
    /// A Spreadsheet Form
    /// </summary>
    /// <authors> Sarthak Jain and Dimitrius Maritsas</authors>
    public partial class SpreadsheetForm : Form
    {
        //Initializes a private backing spreadsheet
        private Spreadsheet spreadsheet;

        //A private instance variable for making the spreadsheet being in dark mode
        private bool darkMode;

        /// <summary>
        /// Initializes the spreadsheet and creates eventhandlers for multiple diffrent events
        /// Also creates the backing spreadsheet normilized with all uppercase letters and with the version "ps6"
        /// Sets the default cell name text box to "A1"
        /// </summary>
        public SpreadsheetForm()
        {
            InitializeComponent();

            spreadsheet = new Spreadsheet(s => Regex.IsMatch(s, @"^([A-Z])[1-9][0-9]?$"), s => s.ToUpper(), "ps6");
            spreadsheetPanel.SelectionChanged += OnSelectionChanged;
            cellContentsTextBox.KeyDown += new KeyEventHandler(cellContentsTextBox_KeyDown);
            spreadsheetPanel.SetSelection(0, 0);
            cellNameTextBox.Text = "A1";

            darkMode = false;
            lightModeButton.Checked = true;
        }
        /// <summary>
        /// Closes the spreadsheet using the Close button in the file drop down menu
        /// </summary>
        private void closeSpreadsheet_Click(object sender, EventArgs e)
        {
            CloseSpreadsheet();
        }
        /// <summary>
        /// A helper method to close the spreadsheet
        /// </summary>
        private void CloseSpreadsheet()
        {
            //If the spreadsheet did not change, then closes automatically
            if (!spreadsheet.Changed)
            {
                Close();
            }
            //If the spreadsheet did change, then goes in this condition
            else if (spreadsheet.Changed)
            {
                //Asks the user if they want to save their spreadsheet before closing
                if (MessageBox.Show("Do you want to save before closing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Saves the spreadsheet if user enters yes
                    SaveSpreadsheet();
                }
                else
                {
                    //Closes the spreadsheet if user enters no
                    Close();
                }
            }

        }
        /// <summary>
        /// Saves the SpreadSheet when the "Save" button is clicked in the drop down file menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveSpreadsheet_Click(object sender, EventArgs e)
        {
            //Calls the helper method to save the spreadsheet
            SaveSpreadsheet();
        }

        /// <summary>
        /// A helper method to help save a Spreadsheet
        /// </summary>
        /// <citation> https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.savefiledialog?view=netcore-3.1 </citation>
        private void SaveSpreadsheet()
        {
            Stream myStream;
            SaveFileDialog save = new SaveFileDialog();
            //Gives the user two options to filter through when saving the file, .sprd and all files
            save.Filter = "Spreadsheet Files (*.sprd)|*.sprd|All files (*.*)|*.*";
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
                //saves the XML version of the SpreadSheet using the filename enter by the user
                spreadsheet.Save(fileName);
            }
        }
        /// <summary>
        /// Creates a new SpreadSheet when the New button is clicked from the file drop down menu.
        /// </summary>
        private void newSpreadsheet_Click(object sender, EventArgs e)
        {
            //opens a new window 
            SpreadsheetApplicationContext.getAppContext().RunForm(new SpreadsheetForm());
        }

        /// <summary>
        /// Clears the spreadsheet when the Clear button is clicked from the file drop down menu
        /// </summary>
        private void clearSpreadsheet_Click(object sender, EventArgs e)
        {
            ClearSpreadsheet();
        }

        /// <summary>
        /// A helper method that clears the spreadsheet
        /// </summary>
        private void ClearSpreadsheet()
        {
            //Asks the user if they want to clear the spreadsheet
            if (MessageBox.Show("Are you sure you want to clear?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //If the user says yes, then it goes in this condition

                //Clears the panel and the text
                spreadsheetPanel.Clear();
                cellValueTextBox.Clear();
                cellNameTextBox.Clear();
                cellContentsTextBox.Clear();

                //Creates a new spreadsheet
                spreadsheet = new Spreadsheet(s => Regex.IsMatch(s, @"^([A-Z])[1-9][0-9]?$"), s => s.ToUpper(), "ps6");

                //Sets the highlighted cell back to A1
                spreadsheetPanel.SetSelection(0, 0);
                cellNameTextBox.Text = "A1";

                //Displays a message saying that the Spreadsheet has been cleared
                MessageBox.Show("Spreadsheet Cleared!");
            }
            else
            {
                //If the user says no, then it does not do anything
            }
        }

        /// <summary>
        /// Opens the help details if the user clicks on Help from the file drop down menu
        /// </summary>
        private void helpSpreadsheet_Click(object sender, EventArgs e)
        {
            HelpSpreadsheet();
        }


        /// <summary>
        /// Opens and loads a previous spreadsheet if the user clicks on Open from the file drop down menu
        /// </summary>
        private void openSpreadsheet_Click(object sender, EventArgs e)
        {
            OpenSpreadsheet();
        }

        /// <summary>
        /// A helper method that helps open a spreadsheet
        /// </summary>
        /// <Citation> https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.openfiledialog.openfile?view=netcore-3.1 </Citation>
        private void OpenSpreadsheet()
        {
            //A string to get the contents of the file
            string fileContent = null;

            //Initializes the openFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //Has two options in the openFileDialog (spreadsheet files and all files)
            openFileDialog.Filter = "Spreadsheet files (*.sprd)|*.sprd|All files (*.*)|*.*";

            //Keeps spreadsheet files as the default
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            //If the user saves the file, then goes in here
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                String filePath = openFileDialog.FileName;

                //Initializes a new spreadsheet using that filePath
                spreadsheet = new Spreadsheet(filePath, s => Regex.IsMatch(s, @"^([A-Z])[1-9][0-9]?$"), s => s.ToUpper(), "ps6");

                //Clears the grid, the contents and value text box
                spreadsheetPanel.Clear();
                cellContentsTextBox.Clear();
                cellValueTextBox.Clear();

                //Iterates through the cells in the spreadsheet we just opened and adds them to the current spreadsheet
                foreach (string cellName in spreadsheet.GetNamesOfAllNonemptyCells())
                {
                    //Gets the first character of the cellName
                    char firstChar = cellName[0];

                    //Parses the rest of the cellName and subtracts that parsed num by 1, because we are using 0
                    //based row and col to get the row
                    int tempRow = int.Parse(cellName.Substring(1)) - 1;

                    //Subtracts the firstChar by A to get the col because we are using 0 based rows and col to get the col
                    int tempCol = firstChar - 'A';

                    //Sets the value in the associated grid spot in the spreadsheet based on the col and row 
                    spreadsheetPanel.SetValue(tempCol, tempRow, spreadsheet.GetCellValue(cellName).ToString());

                    //Sets the selection for the col and row in the spreadsheet
                    spreadsheetPanel.SetSelection(tempCol, tempRow);

                    //Sets the contents of the spreadsheet in the textBox based on the contents of the cellName associated
                    cellContentsTextBox.Text = spreadsheet.GetCellContents(cellName).ToString();

                    //If the cellName is a formula, then it appends a "=" in front of the contents of the cell
                    if (spreadsheet.GetCellContents(cellName) is Formula f)
                    {
                        cellContentsTextBox.Text = "=" + spreadsheet.GetCellContents(cellName).ToString();
                    }
                    //Sets the value of the spreadsheet in the textBox based on the value of the cellName associated
                    cellValueTextBox.Text = spreadsheet.GetCellValue(cellName).ToString();

                    //If the cellValue is a FormulaError, then it displays formulaError in as the value
                    if (spreadsheet.GetCellValue(cellName) is FormulaError fe)
                    {
                        cellValueTextBox.Text = "Formula Error!";
                    }
                }
                //Sets the text of the cellName as it is highlighted by getting the selection of the cell, add the column to 'A' 
                //to account for the column num adding one to it as our panel is 0 based to get the row, and setting that as the
                //name of the cell in the text box
                int col;
                int row;
                spreadsheetPanel.GetSelection(out col, out row);
                int rowNum = row + 1;
                char column = (char)('A' + Convert.ToChar(col));
                cellNameTextBox.Text = column.ToString() + rowNum.ToString() + "";


                //Reads the file and sets it as the fileContents
                Stream fileStream = openFileDialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
            }

            //Displays the contents of the file
            MessageBox.Show(fileContent, "Spreadsheet Opened was like so: ", MessageBoxButtons.OK);
        }
        /// <summary>
        /// The method that is called everytime the another grid cell is selected
        /// Updates the cell name, cell value and cell contents text boxs to display 
        /// the name, value and contents of the newly selected cell.
        /// </summary>
        /// <param name="ssp"></param>
        private void OnSelectionChanged(SpreadsheetPanel ssp)
        {

            //gets the current selected cell and sets col and row equal to that of the cell
            spreadsheetPanel.GetSelection(out int col, out int row);
            //The rowNum variable that is used to determine the cell name. this variable is offset by one to compensate for the zero based grid
            int rowNum = row + 1;
            //the column variable that stores the letter version of the col number
            char column = (char)('A' + Convert.ToChar(col));
            cellNameTextBox.Text = "" + column.ToString() + rowNum.ToString();
            //sets the cell contents text box to the current cells contents
            cellContentsTextBox.Text = spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();
            //checks if the content of the cell is a formula, if so '=' is added to the start of the string
            if (spreadsheet.GetCellContents(cellNameTextBox.Text) is Formula f)
            {
                //add '=' to the start of the string so that it is treated like a formula
                cellContentsTextBox.Text = "=" + spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();
            }

            //Sets the cursor to the end of the contents
            cellContentsTextBox.SelectionStart = cellContentsTextBox.Text.Length;

            //sets the value of the cell value text box equal to the value of the selected cell
            cellValueTextBox.Text = spreadsheet.GetCellValue(cellNameTextBox.Text).ToString();
            //checks to see if the value in the cell is a formula error
            if (spreadsheet.GetCellValue(cellNameTextBox.Text) is FormulaError fe)
            {
                //sets the cell value text box to formula error 
                cellValueTextBox.Text = "Formula Error!";
            }
        }

        /// <summary>
        /// A method that creates key bindings
        /// </summary>
        /// <Citation> https://www.daniweb.com/programming/software-development/threads/261384/disabling-arrow-keys-other-beginner-questions </Citation>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //If the user enters Alt + F4, closes the spreadsheet 
            if (keyData == (Keys.Alt | Keys.F4))
            {
                CloseSpreadsheet();
                //Returns true after the operation was completed
                return true;
            }
            //If the user enters Ctrl + S, closes the spreadsheet
            if (keyData == (Keys.Control | Keys.S))
            {
                SaveSpreadsheet();
                //Returns true after the operation was completed
                return true;
            }
            //If the user enters Ctrl + O, opens the spreadsheet
            if (keyData == (Keys.Control | Keys.O))
            {
                OpenSpreadsheet();

                //Returns true after the operation was completed
                return true;
            }
            //If the user enters Ctrl + N, creates a new spreadsheet
            if (keyData == (Keys.Control | Keys.N))
            {
                SpreadsheetApplicationContext.getAppContext().RunForm(new SpreadsheetForm());
                //Returns true after the operation was completed
                return true;
            }
            //If the user enters Ctrl + H, shows the help options
            if (keyData == (Keys.Control | Keys.H))
            {
                HelpSpreadsheet();
                //Returns true after the operation was completed
                return true;
            }
            //If the user enters Ctrl + C, clears the spreadsheet
            if (keyData == (Keys.Control | Keys.C))
            {
                ClearSpreadsheet();
                //Returns true after the operation was completed
                return true;
            }


            //If the user enters the down key, then goes in here
            if (keyData == Keys.Down)
            {

                int col;
                int row;

                //Gets the row and col in the panel
                spreadsheetPanel.GetSelection(out col, out row);
                //If the row is less than 98, then goes in here
                if (row < 98)
                {
                    //Adds 1 to row number
                    row = row + 1;

                    //moves the cellHighlighted
                    moveCellHighlighted(col, row);
                }
                //Returns true after the change is made in the cell
                return true;
            }
            //Condition if the user enters the left key
            if (keyData == Keys.Left)
            {
                int col;
                int row;

                //Gets the row and col of the spreadsheet
                spreadsheetPanel.GetSelection(out col, out row);
                //If the col is greater than 0, goes in here
                if (col > 0)
                {
                    //Subtracts by 1 to move left
                    col = col - 1;

                    //moves the cellHighlighted
                    moveCellHighlighted(col, row);
                }               
                //Returns true after the change is made in the cell
                return true;
            }
            //Goes in if the user enters the up key
            if (keyData == Keys.Up)
            {
                int col;
                int row;
                //Gets the selection based on the row and col of the spreadsheet
                spreadsheetPanel.GetSelection(out col, out row);
                if (row > 0)
                {
                    //Subtracts from the row to move up
                    row = row - 1;

                    cellContentsTextBox.Enabled = false;
                    //moves the cellHighlighted
                    moveCellHighlighted(col, row);

                    cellContentsTextBox.Enabled = true;
                    cellContentsTextBox.Select();
                }
                //Returns true after the change is made in the cell
                return true;
            }

            //If the user enters the right key goes in here
            if (keyData == Keys.Right)
            {
                int col;
                int row;
                //Gets the selection based on the row and col of the spreadsheet
                spreadsheetPanel.GetSelection(out col, out row);
                //If the col is less than 25, goes in here
                if (col < 25)
                {
                    //Increments the col by 1
                    col = col + 1;
                    //Sets the selection on the basis of the row and col 
                    spreadsheetPanel.SetSelection(col, row);

                    //moves the cellHighlighted
                    moveCellHighlighted(col, row);
                }
                //Returns true after the change is made in the cell
                return true;
            }

            //Returns true if a key was pressed
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// A helper method that moves the cell highlighted using the arrow keys
        /// </summary>
        private void moveCellHighlighted(int col, int row)
        {
            //Sets the selection on the basis of the row and col 
            spreadsheetPanel.SetSelection(col, row);

            //Adds 1 to offset the 0 based row and col in the grid
            row = row + 1;

            //Converts the col to a character and adds it to 'A' and then casts it
            char column = (char)('A' + Convert.ToChar(col));

            //Set the name of the cell
            cellNameTextBox.Text = "" + column.ToString() + row.ToString();

            //Sets the contents of the new cell
            cellContentsTextBox.Text = spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();

            //If the contents is a formula, adds an equals sign in front to make sure that contents stay as a formula
            if (spreadsheet.GetCellContents(cellNameTextBox.Text) is Formula f)
            {
                cellContentsTextBox.Text = "=" + spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();
            }
            //Sets the cursor to the end of the contents
            
            cellContentsTextBox.SelectionStart = cellContentsTextBox.Text.Length + 1;


            //Sets the value of the new cell
            cellValueTextBox.Text = spreadsheet.GetCellValue(cellNameTextBox.Text).ToString();

            //If the cellValue is a formula, sets the text as a formula error
            if (spreadsheet.GetCellValue(cellNameTextBox.Text) is FormulaError fe)
            {
                cellValueTextBox.Text = "Formula Error!";
            }
        }

        /// <summary>
        /// A helper method that displays all the contents to help the user
        /// </summary>
        private void HelpSpreadsheet()
        {
            MessageBox.Show("To create a new spreadsheet, click on New in the file drop down or use Ctrl + N on your keyboard.\n" +
               "To load a previous spreadsheet, click on Open in the file drop down or use Ctrl + O on your keyboard.\n"
               + "To Save a spreadsheet, click on Save in the file drop down or use Ctrl + S on your keyboard.\n" +
               "To Close a spreadsheet, click on Close in the file drop down or use Alt + F4 on your keyboard or use the close button on the title bar.\n" +
               "To Clear a spreadsheet, click on Clear in the file drop down or use Ctrl + C on your keyboard.\n" +
               "For help, click on Help in the file drop down or use Ctrl + H on your keyboard.\n" +
               "Instructions: \n"
               + "The first box represents the cell name, the one after that represents the value and the final box represents the contents of the cell, where you can type in.\n"
               + "Click on a Cell, and enter it's contents and the value will show up corresponding to that.\n" +
               "You have to click on the contents box if you want to re-align the cursor to the end of the contents in the cell\n"
               + "To create a formula, add an equals sign before the equation.\n"
               + "If an error is found, it will throw an Error message.\n"
               + "If your formula is incorrect, then the value will display a FormulaError!"
               + "A formula error can be something like referencing a variable that has not been set to a valid value or a division by 0.\n"
               + "To toggle Dark Mode, click on the Dark Mode button near the contents box, and click on light mode if you would like to keep it in light mode.\n"
               + "A spreadsheet implementation by Sarthak Jain and Dimitrius Maritsas"
               );
        }
        /// <summary>
        /// The method that detects whenever the user is using the text box and also uses a key.
        /// This method detects when the enter key is pressed down and then updates the cell value, name, and contents
        /// according to what was typed in the text box.
        /// </summary>
        private void cellContentsTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //Checks to see if the key pressed was the enter key
            if (e.KeyCode == Keys.Enter)
            {
                //gets rid of the "ding" sound
                e.SuppressKeyPress = true;

                //the variable used for find the row
                int rowNum = 0;
                //the variable used for find the column after converting to a character
                char column = 'A';
                //tries to set the contents of the cell equal to the contents the user entered in the text box
                try
                {
                    //the variable used for getting the cell value
                    String valueVar = "";
                    //finds what cell in the grid the user has selected
                    spreadsheetPanel.GetSelection(out int col, out int row);
                    //offests to account for the zero based grid 
                    rowNum = row + 1;
                    //converts the column integer to a char
                    column = (char)('A' + Convert.ToChar(col));
                    //sets the cells name to the proper name
                    cellNameTextBox.Text = "" + column.ToString() + rowNum.ToString();
                    //checks to see if the cell had a previous value
                    if (spreadsheet.GetCellValue(cellNameTextBox.Text).ToString() != "")
                    {
                        //Stores the old cell value in the valueVar variable incase we need to revert it
                        valueVar = spreadsheet.GetCellValue(cellNameTextBox.Text).ToString();
                    }
                    //gets the list of all the cell names we need to update after changing this cell 
                    List<string> cellNames = new List<string>(spreadsheet.SetContentsOfCell(cellNameTextBox.Text, cellContentsTextBox.Text));
                    //Goes through each of the names of the cells that need to be updated 
                    foreach (string s in cellNames)
                    {
                        //stores the letter part of the name
                        char firstChar = s[0];
                        //converts the letter part of the name into an integer
                        int tempRow = int.Parse(s.Substring(1)) - 1;
                        //corrects the integer value to be appropriate for the grid
                        int tempCol = firstChar - 'A';
                        //sets the value of the named cell in the grid to the updated value
                        spreadsheetPanel.SetValue(tempCol, tempRow, spreadsheet.GetCellValue(s).ToString());
                    }
                    //sets the current value to the new updated value
                    cellValueTextBox.Text = spreadsheet.GetCellValue(cellNameTextBox.Text).ToString();
                    //checks to see if the cell value is a formula error
                    if (spreadsheet.GetCellValue(cellNameTextBox.Text) is FormulaError fe)
                    {
                        //changes the cell value text box to display "Formua Error!"
                        cellValueTextBox.Text = "Formula Error!";
                        //sets the cell value in the grid to "formula Error!"
                        spreadsheetPanel.SetValue(col, row, cellValueTextBox.Text);
                    }

                    //Gets the current cell row and col numbers and sets them to col1 and row1 
                    spreadsheetPanel.GetSelection(out int col1, out int row1);
                    //Checks to see if the selected row is the last row in the grid
                    if (row1 < 98)
                    {

                        //Adds 1 to row number
                        row = row + 1;

                        //Moves the cell highlighted by 1
                        moveCellHighlighted(col, row);
                    }
                }
                //catches any FormulaFormatExceptions thrown, displays a message box and then reverts the changes
                catch (FormulaFormatException ffe)
                {
                    //Displays the message box explaining the error to the user
                    MessageBox.Show("Error, your formula is incorrect because " + ffe.Message);
                    //sets the value of the cell in the grid to the previous value
                    spreadsheetPanel.SetValue(column, rowNum, cellValueTextBox.Text);
                    //sets the contents text box to display the previous contents
                    cellContentsTextBox.Text = spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();
                    //checks to see if the contents in the cell is a formula type
                    if (spreadsheet.GetCellContents(cellNameTextBox.Text) is Formula f)
                    {
                        //adds a '=' to the front of the text in the contents text box so that a formula is dispalyed
                        cellContentsTextBox.Text = "=" + spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();
                    }
                    //sets the contents in the value text box to the value of the cell
                    cellValueTextBox.Text = spreadsheet.GetCellValue(cellNameTextBox.Text).ToString();
                    //checks to see if the cell value is a formula error
                    if (spreadsheet.GetCellValue(cellNameTextBox.Text) is FormulaError fe)
                    {
                        //sets the value in the value text box to display "Formula Error!"
                        cellValueTextBox.Text = "Formula Error!";
                    }

                }
                //catches any CircularException thrown 
                catch (CircularException ce)
                {
                    //Tells the user that the formula is incorrect and why
                    MessageBox.Show("Error. Your formula is incorrect. You have a circular exception, because one of your cells is indirectly referencing itself");
                    //sets the contents in the cell contents text box to the previous contents
                    cellContentsTextBox.Text = spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();
                    //checks to see if the contents in the cell is a formula type
                    if (spreadsheet.GetCellContents(cellNameTextBox.Text) is Formula f)
                    {
                        //adds a '=' to the front of the text in the contents text box so that a formula is dispalyed
                        cellContentsTextBox.Text = "=" + spreadsheet.GetCellContents(cellNameTextBox.Text).ToString();
                    }
                    //sets the contents in the value text box to the value of the cell
                    cellValueTextBox.Text = spreadsheet.GetCellValue(cellNameTextBox.Text).ToString();
                    //checks to see if the cell value is a formula error
                    if (spreadsheet.GetCellValue(cellNameTextBox.Text) is FormulaError fe)
                    {
                        //sets the value in the value text box to display "Formula Error!"
                        cellValueTextBox.Text = "Formula Error!";
                    }
                }
                //Catches any other exceptions thrown
                catch (Exception ex)
                {
                    //displays a message box that tells the user that their formua is incorrect and why
                    MessageBox.Show("Error. Your formula is incorrect" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Called when the user tries to close the form using the X button
        /// </summary>
        private void SpreadsheetForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If the spreadsheet did not change, then closes the spreadsheet
            if (!spreadsheet.Changed)
            {
            }
            //If the spreadsheet did change, then asks the user if they wanted to save their spreadsheet, 
            //if they enter yes, then saves else closes
            else if (spreadsheet.Changed)
            {
                //Displays a message box that asks the user if they would like to save before they close and asks yes or no
                if (MessageBox.Show("Do you want to save before closing?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //if the user clicked yes then the spreadsheet is saved
                    SaveSpreadsheet();
                }
                //if the user clicks no the program exits like normal
                else
                {
                }
            }
        }

        /// <summary>
        /// Changes the mode between dark and light depending on which button is clicked on
        /// </summary>
        private void darkModeButton_CheckedChanged(object sender, EventArgs e)
        {
            //If the user selects the darkMode button, goes in here

            if (darkModeButton.Checked)
            {
                //Sets the panel to be in darkMode and changes the color
                darkMode = true;
                spreadsheetPanel.setDarkMode(darkMode);
                changeColors(true, Color.Orange, Color.Black, BorderStyle.FixedSingle);
            }
            //If the user selects lightMode, then goes in here
            if (!darkModeButton.Checked)
            {
                //Sets the panel to not be in darkMode and changes the color to reflect light mode
                darkMode = false;
                spreadsheetPanel.setDarkMode(darkMode);

                changeColors(false, Color.Black, Color.White, BorderStyle.Fixed3D);

            }
            //Puts the focus on the cell contents box, meaning that arrow keys can't be used to toggle the button
            cellContentsTextBox.Focus();
        }
        /// <summary>
        /// Changes the mode between dark and light depending on which button is clicked on
        /// </summary>
        private void lightModeButton_CheckedChanged(object sender, EventArgs e)
        {
            //If the darkMode button is selected, sets darkMode as true, and sets the panel to have darkMode
            if (darkModeButton.Checked)
            {
                darkMode = true;
                spreadsheetPanel.setDarkMode(darkMode);

            }
            //If the darkMode button is not selected, sets darkMode as false, and sets the panel to have lightMode
            if (!darkModeButton.Checked)
            {
                darkMode = false;
                spreadsheetPanel.setDarkMode(darkMode);
            }
        }

        /// <summary>
        /// A helper method to change the UI of the form, by switching the colors
        /// </summary>
        /// <Citation> https://stackoverflow.com/questions/276179/how-to-change-the-font-color-of-a-disabled-textbox </Citation>
        /// <Citation> https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-set-the-background-of-a-windows-forms-panel?view=netframeworkdesktop-4.8 </Citation>
        private void changeColors(bool b, Color foreColor, Color backColor, BorderStyle border)
        {
            //Changes the back color of the spreadsheet form and the menu strip
            this.BackColor = backColor;
            menuStrip.BackColor = backColor;

            //Changes the fore color and back color for the file tool strip and drop down options there
            fileToolStripMenuItem.ForeColor = foreColor;
            newSpreadsheet.ForeColor = foreColor;
            newSpreadsheet.BackColor = backColor;
            openSpreadsheet.ForeColor = foreColor;
            openSpreadsheet.BackColor = backColor;
            saveSpreadsheet.ForeColor = foreColor;
            saveSpreadsheet.BackColor = backColor;
            clearSpreadsheet.ForeColor = foreColor;
            clearSpreadsheet.BackColor = backColor;
            helpSpreadsheet.ForeColor = foreColor;
            helpSpreadsheet.BackColor = backColor;
            closeSpreadsheet.ForeColor = foreColor;
            closeSpreadsheet.BackColor = backColor;

            //Changes the back color of the spreadsheet panel
            spreadsheetPanel.BackColor = backColor;

            //Changes the color of the text of the dark and light mode button
            darkModeButton.ForeColor = foreColor;
            lightModeButton.ForeColor = foreColor;


            //Changes the foreground and background color of the cellValue box as well
            //as adds or removes a border depending on the mode. Also enables or disables
            //the textbox to show the dark mode colors
            cellValueTextBox.Enabled = b;
            cellValueTextBox.ForeColor = foreColor;
            cellValueTextBox.BackColor = backColor;
            cellValueTextBox.BorderStyle = border;

            //Changes the foreground and background color of the cellName box as well
            //as adds or removes a border depending on the mode. Also enables or disables
            //the textbox to show the dark mode colors
            cellNameTextBox.Enabled = b;
            cellNameTextBox.BackColor = backColor;
            cellNameTextBox.ForeColor = foreColor;
            cellNameTextBox.BorderStyle = border;

            //Changes the foreground and background color of the cellContents box as well
            //as adds or removes a border depending on the mode
            cellContentsTextBox.BackColor = backColor;
            cellContentsTextBox.ForeColor = foreColor;
            cellContentsTextBox.BorderStyle = border;
        }

        /// <summary>
        /// Method that is run when the spreadsheet panel is first loaded
        /// </summary>
        private void spreadsheetPanel_Load(object sender, EventArgs e)
        {
            //Unused
        }
        /// <summary>
        /// Method that is run when the spreadsheet form is first loaded
        /// </summary>
        private void SpreadsheetForm_Load(object sender, EventArgs e)
        {
            //Unused
        }
        /// <summary>
        /// Method that is run when the spreadsheet form is to be painted
        /// </summary>
        private void SpreadsheetForm_Paint(object sender, PaintEventArgs e)
        {
            //Unused
        }
        /// <summary>
        /// Method that is for when the value of the textBox is changed
        /// </summary>
        private void cellValueTextBox_TextChanged(object sender, EventArgs e)
        {
            //Unused
        }
        /// <summary>
        /// Method that is for when the name of the cell is changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cellNameTextBox_TextChanged(object sender, EventArgs e)
        {
            //Unused
        }
        /// <summary>
        /// Method that is for when the contents of the cell is changed
        /// </summary>
        private void cellContentsTextBox_TextChanged(object sender, EventArgs e)
        {
            //Unused
        }
    }
}