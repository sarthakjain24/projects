using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using SS;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A class to test the Spreadsheet class
/// </summary>
namespace SpreadsheetTests
{
    [TestClass]
    public class SpreadsheetTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestNullNameGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.GetCellContents(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestNullNameSetContentsOfCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell(null, "10");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestBothNullNameSetContentsOfCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullContentSetContentsOfCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A100", null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameSetContentsOfCellOnlyNumber()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("21", "=100");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameOrderSetContentsOfCellOnlyNumber()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("21ABC", "=100");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameNameSetContentsOfCellOnlyLetter()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A", "=100");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameNameSetContentsOfCellOnlyLetterAndUnderscore()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1_12", "=100");
        }

        [TestMethod]
        public void TestSetCellContentsFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
        }

        [TestMethod]
        public void TestSetCellContentsFormulaMultiVariables()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A2", "10");
            spreadsheet.SetContentsOfCell("A1", "=A2+2");
            Assert.AreEqual((double)12, spreadsheet.GetCellValue("A1"));
        }

        [TestMethod]
        public void TestXMLSaveDefault()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.Save("test1.xml");
            Assert.AreEqual("default", spreadsheet.GetSavedVersion("test1.xml"));
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestXMLSaveDefaultNull()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.Save(null);
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestXMLSaveDefaultEmpty()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.Save("");
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestXMLFilePathThatDoesntExist()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.Save(null);
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestGetXMLFilePathThatDoesntExist()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.Save("test1.xml");
            Assert.AreEqual("default", spreadsheet.GetSavedVersion(null));
        }

        [TestMethod]
        public void TestSetCellContentsFormulaMultiVariablesWithCellCalledThatDoesntExist()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=A2+2");
            Assert.IsInstanceOfType(spreadsheet.GetCellValue("A1"), typeof(FormulaError));
        }


        [TestMethod]
        public void TestAddingString()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "john cena");
            Assert.AreEqual("john cena", spreadsheet.GetCellContents("A1"));
            Assert.AreEqual("john cena", spreadsheet.GetCellValue("A1"));
        }

        [TestMethod]
        public void TestXMLSaveMultipleItems()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s, "Version 1stOct2020 13:57");
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.SetContentsOfCell("A2", "bigshow");
            spreadsheet.SetContentsOfCell("A3", "10000003432");
            spreadsheet.Save("multipleVariables.xml");
            Assert.AreEqual("Version 1stOct2020 13:57", spreadsheet.GetSavedVersion("multipleVariables.xml"));
        }

        [TestMethod]
        public void TestReplaceCellValueWithDouble()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "brock lesnar");
            spreadsheet.SetContentsOfCell("A1", "100");
            spreadsheet.SetContentsOfCell("A2", "=A1 + 10");
            spreadsheet.SetContentsOfCell("A3", "=A2 * 10");
            spreadsheet.SetContentsOfCell("A4", "=A3 / 5");
            List<string> l = new List<string>();
            l.Add("A1");
            l.Add("A2");
            l.Add("A3");
            l.Add("A4");
            CollectionAssert.AreEqual(l, (List<string>)spreadsheet.SetContentsOfCell("A1", "23"));
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestXMLSaveMultipleItemsIncorrectFileName()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s, "Version 1stOct2020 13:57");
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.SetContentsOfCell("A2", "bigshow");
            spreadsheet.SetContentsOfCell("A3", "10000003432");
            spreadsheet.Save("multipleVariables.xml");
            Assert.AreEqual("Version 1stOct2020 13:57", spreadsheet.GetSavedVersion("multipleVariable.xml"));
        }

        [TestMethod]
        public void TestSetCellContentsFormulaMultiVariablesWithNewCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=A2+2");
            Assert.IsInstanceOfType(spreadsheet.GetCellValue("A1"), typeof(FormulaError));
            spreadsheet.SetContentsOfCell("A2", "100");
            Assert.AreEqual((double)102, spreadsheet.GetCellValue("A1"));
        }

        [TestMethod]
        public void TestMultipleIncorrectGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2.0");
            spreadsheet.SetContentsOfCell("B1", "=A1 * 2");
            spreadsheet.SetContentsOfCell("C1", "=A1+B1");
            spreadsheet.SetContentsOfCell("A1", "");
            Assert.IsFalse(new HashSet<string>() { "A1", "B1", "C1" }.SetEquals(spreadsheet.GetNamesOfAllNonemptyCells()));
        }
        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestXMLGetPreviousSaveMultipleItems()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(null, s => true, s => s, "Version 1stOct2020 13:57");
        }


        [TestMethod]
        public void TestXMLSaveMultipleItemsCallingAndSettingToNewSpreadsheet()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s, "Version 1stOct2020 13:57");
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.SetContentsOfCell("A2", "bigshow");
            spreadsheet.SetContentsOfCell("A3", "10000003432");
            spreadsheet.Save("multipleVariables.xml");
            Assert.AreEqual("Version 1stOct2020 13:57", spreadsheet.GetSavedVersion("multipleVariables.xml"));

            AbstractSpreadsheet newSpreadsheet = new Spreadsheet("multipleVariables.xml", s => true, s => s, "Version 1stOct2020 13:57");
            Assert.AreEqual("Version 1stOct2020 13:57", newSpreadsheet.GetSavedVersion("multipleVariables.xml"));
            Assert.AreEqual(f, newSpreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, newSpreadsheet.GetCellValue("A1"));
            Assert.AreEqual("bigshow", newSpreadsheet.GetCellContents("A2"));
            Assert.AreEqual((double)10000003432, newSpreadsheet.GetCellContents("A3"));
            newSpreadsheet.SetContentsOfCell("A1", "2131");
            newSpreadsheet.Save("multiVariables.xml");
            Assert.AreEqual((double)2131, newSpreadsheet.GetCellValue("A1"));

        }

        [TestMethod]
        public void TestXMLChanged()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s, "Version 1stOct2020 13:57");
            spreadsheet.SetContentsOfCell("A1", "=2+2");
            Formula f = new Formula("2+2");
            Assert.AreEqual(f, spreadsheet.GetCellContents("A1"));
            Assert.AreEqual((double)4, spreadsheet.GetCellValue("A1"));
            spreadsheet.SetContentsOfCell("A2", "bigshow");
            spreadsheet.SetContentsOfCell("A3", "10000003432");
            Assert.IsTrue(spreadsheet.Changed);
            spreadsheet.Save("multipleVariables.xml");
            Assert.IsFalse(spreadsheet.Changed);
        }

        [TestMethod]
        public void Changed()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "hello");
            spreadsheet.SetContentsOfCell("A1", "");
            Assert.IsTrue(spreadsheet.Changed);
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestIncorrectXML()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet("versionNotDeclared.xml",s => true, s => s, "1.0");
        }

    


        [TestMethod]
        public void TestEasyBasicForDoubleGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2.0");
            Assert.AreEqual((double)2.0, spreadsheet.GetCellContents("A1"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameFormulaSetCellContentsAfterNormalizing()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s + "!", "default");
            spreadsheet.SetContentsOfCell("A1", "=10+10");
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestNullNameFormulaSetCellContentsAfterNormalizing()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s + "!", "default");
            spreadsheet.SetContentsOfCell(null, "=10+10");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameStringGetCellContentsAfterNormalizing()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s + "!", "default");
            spreadsheet.GetCellContents("A1");
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameStringGetCellValueAfterNormalizing()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet(s => true, s => s + "!", "default");
            spreadsheet.GetCellValue("A1");
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestReferenceToSelf()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            try
            {
                spreadsheet.SetContentsOfCell("A1", "=A1");
            }
            catch (CircularException e)
            {
                Assert.IsTrue(new HashSet<string>() { }.SetEquals(spreadsheet.GetNamesOfAllNonemptyCells()));
                throw e;
            }
        }

        [TestMethod]
        public void StressTest()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("D1", "0");
            spreadsheet.SetContentsOfCell("B2", "=D1+D1");
            spreadsheet.SetContentsOfCell("C2", "=D1*D1");
            spreadsheet.SetContentsOfCell("B1", "=B2+D1");
            spreadsheet.SetContentsOfCell("C1", "=C2+D1");
            spreadsheet.SetContentsOfCell("A1", "=B1+C1");

            List<string> l = new List<string>();
            l.Add("D1");
            l.Add("C2");
            l.Add("C1");
            l.Add("B2");
            l.Add("B1");
            l.Add("A1");


            CollectionAssert.AreEqual(l, (List<string>)spreadsheet.SetContentsOfCell("D1", "=10"));
            Assert.AreEqual((double)20, spreadsheet.GetCellValue("B2"));
            Assert.AreEqual((double)100, spreadsheet.GetCellValue("C2"));
            Assert.AreEqual((double)30, spreadsheet.GetCellValue("B1"));
            Assert.AreEqual((double)110, spreadsheet.GetCellValue("C1"));
            Assert.AreEqual((double)140, spreadsheet.GetCellValue("A1"));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestInvalidNameGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.GetCellContents("A1?!");
        }

        [TestMethod]
        public void TestEmptyValueGetNamesOfAllNonEmptyCellContentsAfterReplacing()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "fwferd");
            spreadsheet.SetContentsOfCell("A1", "");
            Assert.IsTrue(new HashSet<string>() { }.SetEquals(spreadsheet.GetNamesOfAllNonemptyCells()));
        }

        [TestMethod]
        public void TestEmptyValueGetCellContentsAfterReplacing()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "fwferd");
            spreadsheet.SetContentsOfCell("A1", "");
            Assert.AreEqual("", spreadsheet.GetCellContents("A1"));
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestReadingBadXMLVersion()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("D1", "0");
            spreadsheet.SetContentsOfCell("B2", "=D1+D1");
            spreadsheet.SetContentsOfCell("C2", "=D1*D1");
            spreadsheet.SetContentsOfCell("B1", "=B2+D1");
            spreadsheet.SetContentsOfCell("C1", "=C2+D1");
            spreadsheet.SetContentsOfCell("A1", "=B1+C1");
            spreadsheet.Save("complexFormula.xml");
            AbstractSpreadsheet newSpreadsheet = new Spreadsheet("complexFormula.xml", s => true, s => s, "1.0");
            
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "0");
            spreadsheet.SetContentsOfCell("A2", "=10/A1");
            Assert.IsInstanceOfType(spreadsheet.GetCellValue("A2"), typeof(FormulaError));
        }

        [TestMethod]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestReadingBadXML()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet("badxmlfile.xml", s => true, s => s, "1.0");
        }

        [TestMethod]
        public void TestSpaceValueGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "                    ");
            Assert.AreEqual("                    ", spreadsheet.GetCellContents("A1"));
        }

        [TestMethod]
        public void TestSpaceValueGetNameOfNonEmptyCell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "                    ");
            Assert.IsTrue(new HashSet<string>() { "A1" }.SetEquals(spreadsheet.GetNamesOfAllNonemptyCells()));
        }

        [TestMethod]
        public void TestEmptyValueGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "");
            Assert.IsTrue(new HashSet<string>() { }.SetEquals(spreadsheet.GetNamesOfAllNonemptyCells()));
        }

        [TestMethod]
        public void TestGetCellContentFormula()
        {
            Spreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=0");
            spreadsheet.SetContentsOfCell("A2", "=A1+A3");
            Formula f1 = new Formula("A1+A4");
            spreadsheet.SetContentsOfCell("A3", "=A1+A4");
            Assert.AreEqual(f1, spreadsheet.GetCellContents("A3"));
        }

        [TestMethod]
        public void TestGetCellValueFormulaSpacedOut()
        {
            Spreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=0");
            spreadsheet.SetContentsOfCell("A2", "=A1+A3");
            Formula f1 = new Formula("A1+A4");
            spreadsheet.SetContentsOfCell("A3", "          =A1+A4");
            Assert.AreEqual("          =A1+A4", spreadsheet.GetCellContents("A3"));
            Assert.AreEqual("          =A1+A4", spreadsheet.GetCellValue("A3"));
        }

        [TestMethod]
        public void TestDifferentVariableGetContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "fwferd");
            Assert.AreEqual("", spreadsheet.GetCellContents("a1"));
        }

        [TestMethod]
        public void TestDifferentVariableGetValues()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "fwferd");
            Assert.AreEqual("", spreadsheet.GetCellValue("a1"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestNullGetValues()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            Assert.AreEqual("", spreadsheet.GetCellValue(null));
        }

        [TestMethod]
        public void TestReplaceCellContentWithDouble()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2.0");
            spreadsheet.SetContentsOfCell("B1", "=A1*2");
            spreadsheet.SetContentsOfCell("C1", "=A1+B1");
            List<string> l = new List<string>();
            l.Add("A1");
            l.Add("B1");
            l.Add("C1");
            CollectionAssert.AreEqual(l, (List<string>)spreadsheet.SetContentsOfCell("A1", "23"));
        }

        [TestMethod]
        public void TestMultipleGetCellContents()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2.0");
            Formula f = new Formula("A1*2");
            spreadsheet.SetContentsOfCell("B1", "=A1*2");
            Formula f1 = new Formula("A1+B1");
            spreadsheet.SetContentsOfCell("C1", "=A1+B1");
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "C1" }.SetEquals(spreadsheet.GetNamesOfAllNonemptyCells()));
        }

        [TestMethod]
        public void TestReplaceCellContentWithNewFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2.0");
            spreadsheet.SetContentsOfCell("B1", "=A1*2");
            spreadsheet.SetContentsOfCell("C1", "=A1+B1");
            List<string> l = new List<string>();
            l.Add("A1");
            l.Add("B1");
            l.Add("C1");
            CollectionAssert.AreEqual(l, (List<string>)spreadsheet.SetContentsOfCell("A1", "=A2+D1"));
        }

        [TestMethod]
        public void TestReplaceCellContentWithDoubleAndEverythingElseBeingFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "=0");
            spreadsheet.SetContentsOfCell("A2", "=A1+A3");
            spreadsheet.SetContentsOfCell("A3", "=A1+A4");
            List<string> l = new List<string>();
            l.Add("A4");
            l.Add("A3");
            l.Add("A2");
            CollectionAssert.AreEqual(l, (List<string>)spreadsheet.SetContentsOfCell("A4", "100"));
        }

        [TestMethod]
        public void TestReplaceCellContentWithFormulaAsWell()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "10");
            spreadsheet.SetContentsOfCell("A2", "=A1+A3");
            spreadsheet.SetContentsOfCell("A3", "=A1+A4");
            List<string> l = new List<string>();
            l.Add("A4");
            l.Add("A3");
            l.Add("A2");
            CollectionAssert.AreEqual(l, (List<string>)spreadsheet.SetContentsOfCell("A4", "=A5+2"));
        }

        [TestMethod]
        public void TestReplaceCellContentWithString()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "10");
            Formula f = new Formula("A1+A3");
            spreadsheet.SetContentsOfCell("A2", "=A1+A3");
            Formula f1 = new Formula("A1+A4");
            spreadsheet.SetContentsOfCell("A3", "=A1+A4");
            spreadsheet.SetContentsOfCell("A5", "21231243325321");
            List<string> l = new List<string>();
            l.Add("A4");
            l.Add("A3");
            l.Add("A2");
            CollectionAssert.AreEqual(l, (List<string>)spreadsheet.SetContentsOfCell("A4", "the rock"));
        }

        [TestMethod]
        public void TestSimpleSetContentsChangeDouble()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2210");
            spreadsheet.SetContentsOfCell("A1", "0.1212");
            Assert.AreEqual(0.1212, (double)spreadsheet.GetCellContents("A1"));
        }

        [TestMethod]
        public void TestSimpleSetContentsChangeString()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2.0");
            spreadsheet.SetContentsOfCell("A1", "rey mysterio");
            Assert.AreEqual("rey mysterio", spreadsheet.GetCellContents("A1"));
        }

        [TestMethod]
        public void TestSimpleSetContentsChangeFormula()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            spreadsheet.SetContentsOfCell("A1", "2.0");
            spreadsheet.SetContentsOfCell("A1", "=23 / 22");
            Assert.AreEqual(new Formula("23 / 22"), spreadsheet.GetCellContents("A1"));
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestCircularDependencyWithoutCheck()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();

            spreadsheet.SetContentsOfCell("A1", "10");
            spreadsheet.SetContentsOfCell("A2", "=A1+60");
            spreadsheet.SetContentsOfCell("A3", "=A1+A2");
            spreadsheet.SetContentsOfCell("A4", "=A1+A3");

            Formula f3 = new Formula("A2+A3");
            spreadsheet.SetContentsOfCell("A1", "=A2+A3");
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void TestCircularDependency()
        {
            AbstractSpreadsheet spreadsheet = new Spreadsheet();
            try
            {
                spreadsheet.SetContentsOfCell("A1", "10");
                spreadsheet.SetContentsOfCell("A2", "=A1+60");
                Formula f1 = new Formula("A1+A2");
                spreadsheet.SetContentsOfCell("A3", "=A1+A2");
                Assert.AreEqual((double)80, spreadsheet.GetCellValue("A3"));
                Formula f2 = new Formula("A1+A3");
                spreadsheet.SetContentsOfCell("A4", "=A1+A3");
                Formula f3 = new Formula("A2+A3");
                spreadsheet.SetContentsOfCell("A1", "=A2+A3");
            }
            catch (CircularException e)
            {
                Assert.AreEqual(10, (double)spreadsheet.GetCellContents("A1"));
                Assert.AreEqual((double)80, spreadsheet.GetCellValue("A3"));
                throw new CircularException();
            }
        }


        // EMPTY SPREADSHEETS
        [TestMethod(), Timeout(2000)]
        [TestCategory("1")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestEmptyGetNull()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("2")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestEmptyGetContents()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents("1AA");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("3")]
        public void TestGetEmptyContents()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.AreEqual("", s.GetCellContents("A2"));
        }

        // SETTING CELL TO A DOUBLE
        [TestMethod(), Timeout(2000)]
        [TestCategory("4")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNullDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "1.5");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("5")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetInvalidNameDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1A1A", "1.5");

        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("6")]
        public void TestSimpleSetDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "1.5");
            Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
        }

        // SETTING CELL TO A STRING
        [TestMethod(), Timeout(2000)]
        [TestCategory("7")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetNullStringVal()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A8", (string)null);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("8")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNullStringName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "hello");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("9")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetSimpleString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1AZ", "hello");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("10")]
        public void TestSetGetSimpleString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "hello");
            Assert.AreEqual("hello", s.GetCellContents("Z7"));
        }

        // SETTING CELL TO A FORMULA
        [TestMethod(), Timeout(2000)]
        [TestCategory("11")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSetNullFormVal()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A8", null);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("12")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetNullFormName()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell(null, "=2");

        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("13")]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSetSimpleForm()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("1AZ", "=2");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("14")]
        public void TestSetGetForm()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("Z7", "=3");
            Formula f = (Formula)s.GetCellContents("Z7");
            Assert.AreEqual(new Formula("3"), f);
            Assert.AreNotEqual(new Formula("2"), f);
        }

        // CIRCULAR FORMULA DETECTION
        [TestMethod(), Timeout(2000)]
        [TestCategory("15")]
        [ExpectedException(typeof(CircularException))]
        public void TestSimpleCircular()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2");
            s.SetContentsOfCell("A2", "=A1");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("16")]
        [ExpectedException(typeof(CircularException))]
        public void TestComplexCircular()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A3", "=A4+A5");
            s.SetContentsOfCell("A5", "=A6+A7");
            s.SetContentsOfCell("A7", "=A1+A1");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("17")]
        [ExpectedException(typeof(CircularException))]
        public void TestUndoCircular()
        {
            Spreadsheet s = new Spreadsheet();
            try
            {
                s.SetContentsOfCell("A1", "=A2+A3");
                s.SetContentsOfCell("A2", "15");
                s.SetContentsOfCell("A3", "30");
                s.SetContentsOfCell("A2", "=A3*A1");
            }
            catch (CircularException e)
            {
                Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
                throw e;
            }
        }

        // NONEMPTY CELLS
        [TestMethod(), Timeout(2000)]
        [TestCategory("18")]
        public void TestEmptyNames()
        {
            Spreadsheet s = new Spreadsheet();
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("19")]
        public void TestExplicitEmptySet()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "");
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("20")]
        public void TestSimpleNamesString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "hello");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("21")]
        public void TestSimpleNamesDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "52.25");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("22")]
        public void TestSimpleNamesFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "=3.5");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("23")]
        public void TestMixedNames()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("C1", "hello");
            s.SetContentsOfCell("B1", "=3.5");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "A1", "B1", "C1" }));
        }

        // RETURN VALUE OF SET CELL CONTENTS
        [TestMethod(), Timeout(2000)]
        [TestCategory("24")]
        public void TestSetSingletonDouble()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("B1", "hello");
            s.SetContentsOfCell("C1", "=5");
            Assert.IsTrue(s.SetContentsOfCell("A1", "17.2").SequenceEqual(new List<string>() { "A1" }));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("25")]
        public void TestSetSingletonString()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("C1", "=5");
            Assert.IsTrue(s.SetContentsOfCell("B1", "hello").SequenceEqual(new List<string>() { "B1" }));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("26")]
        public void TestSetSingletonFormula()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "17.2");
            s.SetContentsOfCell("B1", "hello");
            Assert.IsTrue(s.SetContentsOfCell("C1", "=5").SequenceEqual(new List<string>() { "C1" }));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("27")]
        public void TestSetChain()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A2", "6");
            s.SetContentsOfCell("A3", "=A2+A4");
            s.SetContentsOfCell("A4", "=A2+A5");
            Assert.IsTrue(s.SetContentsOfCell("A5", "82.5").SequenceEqual(new List<string>() { "A5", "A4", "A3", "A1" }));
            Assert.AreEqual((double)88.5, s.GetCellValue("A4"));
            Assert.AreEqual((double)94.5, s.GetCellValue("A3"));
            Assert.AreEqual((double)100.5, s.GetCellValue("A1"));

        }

        // CHANGING CELLS
        [TestMethod(), Timeout(2000)]
        [TestCategory("28")]
        public void TestChangeFtoD()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A1", "2.5");
            Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("29")]
        public void TestChangeFtoS()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=A2+A3");
            s.SetContentsOfCell("A1", "Hello");
            Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("30")]
        public void TestChangeStoF()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "Hello");
            s.SetContentsOfCell("A1", "=23");
            Assert.AreEqual(new Formula("23"), (Formula)s.GetCellContents("A1"));
            Assert.AreNotEqual(new Formula("24"), (Formula)s.GetCellContents("A1"));
        }

        // STRESS TESTS
        [TestMethod(), Timeout(2000)]
        [TestCategory("31")]
        public void TestStress1()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetContentsOfCell("A1", "=B1+B2");
            s.SetContentsOfCell("B1", "=C1-C2");
            s.SetContentsOfCell("B2", "=C3*C4");
            s.SetContentsOfCell("C1", "=D1*D2");
            s.SetContentsOfCell("C2", "=D3*D4");
            s.SetContentsOfCell("C3", "=D5*D6");
            s.SetContentsOfCell("C4", "=D7*D8");
            s.SetContentsOfCell("D1", "=E1");
            s.SetContentsOfCell("D2", "=E1");
            s.SetContentsOfCell("D3", "=E1");
            s.SetContentsOfCell("D4", "=E1");
            s.SetContentsOfCell("D5", "=E1");
            s.SetContentsOfCell("D6", "=E1");
            s.SetContentsOfCell("D7", "=E1");
            s.SetContentsOfCell("D8", "=E1");
            IList<String> cells = s.SetContentsOfCell("E1", "0");
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        }

        // Repeated for extra weight
        [TestMethod(), Timeout(2000)]
        [TestCategory("32")]
        public void TestStress1a()
        {
            TestStress1();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("33")]
        public void TestStress1b()
        {
            TestStress1();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("34")]
        public void TestStress1c()
        {
            TestStress1();
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("35")]
        public void TestStress2()
        {
            Spreadsheet s = new Spreadsheet();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);
                Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "=A" + (i + 1))));
            }
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("36")]
        public void TestStress2a()
        {
            TestStress2();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("37")]
        public void TestStress2b()
        {
            TestStress2();
        }
        [TestMethod(), Timeout(2000)]
        [TestCategory("38")]
        public void TestStress2c()
        {
            TestStress2();
        }

        [TestMethod()]
        [TestCategory("39")]
        public void TestStress3()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 1; i < 200; i++)
            {
                s.SetContentsOfCell("A" + i, "=A" + (i + 1));
            }
            try
            {
                s.SetContentsOfCell("A150", "=A50");
                Assert.Fail();
            }
            catch (CircularException)
            {
            }
        }

        [TestMethod()]
        [TestCategory("40")]
        public void TestStress3a()
        {
            TestStress3();
        }
        [TestMethod()]
        [TestCategory("41")]
        public void TestStress3b()
        {
            TestStress3();
        }
        [TestMethod()]
        [TestCategory("42")]
        public void TestStress3c()
        {
            TestStress3();
        }

        [TestMethod()]
        [TestCategory("43")]
        public void TestStress4()
        {
            Spreadsheet s = new Spreadsheet();
            for (int i = 0; i < 500; i++)
            {
                s.SetContentsOfCell("A1" + i, "=A1" + (i + 1));
            }
            LinkedList<string> firstCells = new LinkedList<string>();
            LinkedList<string> lastCells = new LinkedList<string>();
            for (int i = 0; i < 250; i++)
            {
                firstCells.AddFirst("A1" + i);
                lastCells.AddFirst("A1" + (i + 250));
            }
            Assert.IsTrue(s.SetContentsOfCell("A1249", "25.0").SequenceEqual(firstCells));
            Assert.IsTrue(s.SetContentsOfCell("A1499", "0").SequenceEqual(lastCells));
        }
        [TestMethod()]
        [TestCategory("44")]
        public void TestStress4a()
        {
            TestStress4();
        }
        [TestMethod()]
        [TestCategory("45")]
        public void TestStress4b()
        {
            TestStress4();
        }
        [TestMethod(), Timeout(7000)]
        [TestCategory("46")]
        public void TestStress4c()
        {
            TestStress4();
        }
     

        

        [TestMethod()]
        [TestCategory("47")]
        public void TestStress5_With100Loop()
        {
            RunRandomizedTest100Loop(47, 98);
        }

        [TestMethod()]
        [TestCategory("48")]
        public void TestStress6_With100Loop()
        {
            RunRandomizedTest100Loop(48, 97);
        }

        [TestMethod()]
        [TestCategory("49")]
        public void TestStress7_With100Loop()
        {
            RunRandomizedTest100Loop(49, 95);
        }

        [TestMethod()]
        [TestCategory("50")]
        public void TestStress8_With100Loop()
        {
            RunRandomizedTest100Loop(50, 99);
        }

      

        /// Sets random contents for a random cell 100 times
        /// </summary>
        /// <param name="seed">Random seed</param>
        /// <param name="size">The known resulting spreadsheet size, given the seed</param>
        public void RunRandomizedTest100Loop(int seed, int size)
        {
            Spreadsheet s = new Spreadsheet();
            Random rand = new Random(seed);

            for (int i = 0; i < 100; i++)
            {
                try
                {
                    switch (rand.Next(3))
                    {
                        case 0:
                            s.SetContentsOfCell(randomName(rand), "3.14");
                            break;
                        case 1:
                            s.SetContentsOfCell(randomName(rand), "hello");
                            break;
                        case 2:
                            s.SetContentsOfCell(randomName(rand), "=" + randomFormula(rand));
                            break;
                    }
                }
                catch (CircularException)
                {
                }
            }
            ISet<string> set = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(size, set.Count);
        }

        
        

        /// <summary>
        /// Generates a random cell name with a capital letter and number between 1 - 99
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        private String randomName(Random rand)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
        }

        /// <summary>
        /// Generates a random Formula
        /// </summary>
        /// <param name="rand"></param>
        /// <returns></returns>
        private String randomFormula(Random rand)
        {
            String f = randomName(rand);
            for (int i = 0; i < 10; i++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        f += "+";
                        break;
                    case 1:
                        f += "-";
                        break;
                    case 2:
                        f += "*";
                        break;
                    case 3:
                        f += "/";
                        break;
                }
                switch (rand.Next(2))
                {
                    case 0:
                        f += 7.2;
                        break;
                    case 1:
                        f += randomName(rand);
                        break;
                }
            }
            return f;
        }
    }
}
