using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace FormulaTests
{
    [TestClass]
    public class FormulaTests
    {

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestUnAcceptableValues()
        {
            Formula f = new Formula("3D@D");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNull()
        {
            Formula f = new Formula(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyString()
        {
            Formula f = new Formula("");
        }

        [TestMethod]
        public void TestScientificNotation()
        {
            Formula f = new Formula("1e3");
            Assert.AreEqual(1000, (double)f.Evaluate(s => 0));
        }

        [TestMethod]
        public void TestScientificNotationInEquality()
        {
            Formula f1 = new Formula("1e-100");
            Formula f2 = new Formula("1e-99");
            Assert.IsFalse(f1.Equals(f2));
        }

        [TestMethod]
        public void TestScientificNotationEquality()
        {
            Formula f1 = new Formula("1e-1000");
            Formula f2 = new Formula("1e-1000");
            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod]
        public void TestScientificNotationEqualityAfterToStringConversion()
        {
            Formula f1 = new Formula("1e-1000");
            Formula f2 = new Formula(f1.ToString());
            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod]
        public void TestVerySmallNumberEquality()
        {
            Formula f1 = new Formula("0.00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001");
            Formula f2 = new Formula("0.00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001");
            Assert.IsTrue(f1.Equals(f2));
        }


        [TestMethod]
        public void TestVerySmallNumberInEquality()
        {
            Formula f1 = new Formula("0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001");
            Formula f2 = new Formula("0.00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001");
            Assert.IsFalse(f1.Equals(f2));
        }

        [TestMethod]
        public void TestNullEquality()
        {
            Formula f1 = new Formula("0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001");
         
            Assert.IsFalse(f1.Equals(null));
        }

        [TestMethod]
        public void TestDifferentObjectsEquality()
        {
            Formula f1 = new Formula("0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001");
            Random r = new Random(100);
            int x = r.Next();
            Assert.IsFalse(f1.Equals(r));
        }

        [TestMethod]
        public void TestDifferentObjectsWithPrimitiveEquality()
        {
            Formula f1 = new Formula("0.0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001");
            Random r = new Random();
            int x = r.Next();
            Assert.IsFalse(f1.Equals(x));
        }



        [TestMethod]
        public void TestScientificNotationReallySmallNum()
        {
            Formula f = new Formula("1e-100");
            Assert.AreEqual(0, (double)f.Evaluate(s => 0), 1e-20);
        }

        [TestMethod]
        public void TestExtremeNumOfDigits()
        {
            Formula f = new Formula("6.00000000000001 * 6");
            Assert.AreEqual(36.00000000000001, (double)f.Evaluate(s => 0), 1e-10);
        }

        [TestMethod]
        public void TestExtremeNumOfDigitsRounded()
        {
            Formula f = new Formula("6.00000000000000000001 * 6");
            Assert.AreEqual(36, (double)f.Evaluate(s => 0));
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOnlyParentheses()
        {
            Formula f = new Formula("((()))");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestWrongCharacterAfterVariable()
        {
            Formula f = new Formula("A2(2+5)");
        }


        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOnlyOpeningParentheses()
        {
            Formula f = new Formula("(((");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOnlyClosingParentheses()
        {
            Formula f = new Formula("))");
        }


        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestUnbalancedParentheses()
        {
            Formula f = new Formula("(4)+2))");
        }

        [TestMethod]
        public void TestToStringWithoutNormalizer()
        {
            Formula f = new Formula("x + Y");
            Assert.AreEqual("x+Y", f.ToString());
        }

        [TestMethod]
        public void TestToStringWithNormalizer()
        {
            Formula f = new Formula("x + y", s => s.ToUpper(), s => true);
            Assert.AreEqual("X+Y", f.ToString());
        }

        [TestMethod]
        public void TestToStringWithNormalizerHavingNum()
        {
            Formula f = new Formula("x1 + y2", s => s.ToUpper(), s => true);
            Assert.AreEqual("X1+Y2", f.ToString());
        }
        [TestMethod]
        public void TestEqual()
        {
            Assert.IsTrue(new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void TestEqualOnlyNumbers()
        {
            Assert.IsTrue(new Formula("2.0 ").Equals(new Formula("2.000")));
        }

        [TestMethod]
        public void TestEqualOperator()
        {
            Assert.IsTrue(new Formula("2.0 + x7") == (new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void TestEqualOperatorUnEqual()
        {
            Assert.IsFalse(new Formula("1.0 + x7") == (new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void TestEqualOperatorBothNull()
        {
            Assert.IsTrue(null == null);
        }

        /**
         * Double check this
         */
        [TestMethod]
        public void TestEqualOperatorWithOneNullObject()
        {
            Formula f = null;
            Assert.IsFalse(f == (new Formula("2.000 + x7")));
        }
       
        [TestMethod]
        public void TestEqualOperatorWithOneNull()
        {
            Formula f = null;
            Assert.IsFalse(new Formula("2.000 + x7") == f);
        }

        [TestMethod]
        public void TestNotEqualOperatorBothNull()
        {
            Formula f = null;
            Formula f2 = null;
            Assert.IsFalse(f != f2);
        }


        [TestMethod]
        public void TestNotEqualOperatorIsEqual()
        {
            Assert.IsFalse(new Formula("2.0 + x7") != (new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void TestNotEqualOperator()
        {
            Assert.IsTrue(new Formula("1.0 + x7") != (new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void TestNotEqualOperatorWithOneNullObject()
        {

            Assert.IsTrue(null != (new Formula("2.000 + x7")));
        }

        [TestMethod]
        public void TestNotEqualOperatorWithOneNull()
        {

            Assert.IsTrue((new Formula("2.000 + x7")) != null);
        }

        [TestMethod]
        public void TestDecimalEAddition()
        {
            Formula f = new Formula("5e-1+1");
            double e = (double)f.Evaluate(x => 0);
            Assert.AreEqual(1.5, e);
        }


        [TestMethod]
        public void TestComplexAddition()
        {
            Formula f = new Formula("413134+2423423+241234");
            Assert.AreEqual(3077791, (double)f.Evaluate(x => 0));
        }


        [TestMethod]
        public void TestSimpleAddition()
        {
            Formula f = new Formula("1 + 1");
            Assert.AreEqual((double)2, f.Evaluate(s => 0));
        }

        [TestMethod]
        public void TestDivideByZeroSlightlyComplex()
        {
            Formula f = new Formula("2 / (5.5-3.3 - 1.1 - 0.1 - 1.0)");
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }

        [TestMethod]
        public void TestDivideByZeroSlightlyComplexGetReason()
        {
            Formula f = new Formula("2 / (5.5-3.3 - 1.1 - 0.1 - 1.0)");
            FormulaError fe = (FormulaError)f.Evaluate(s => 0);
            Assert.AreEqual("Error. A division by 0 occurred", fe.Reason);
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }

        [TestMethod]
        public void TestNeverEndingDivision()
        {
            Formula f = new Formula("2 / 3");
            Assert.AreEqual(0.666666666666, (double)f.Evaluate(s => 0), 1e-10);
        }

        [TestMethod]
        public void TestDivideByZero()
        {
            Formula f = new Formula("2 / 0");
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            Formula f = new Formula("1 + 1");
            Formula f1 = new Formula("1.0 + 1.0");
            Assert.AreEqual(f1.GetHashCode(), f.GetHashCode());
        }

        [TestMethod]
        public void TestGetHashCodeComparingString()
        {
            Formula f = new Formula("1.00 + 1.00");
            Formula f1 = new Formula("1.0 + 1.0");
            Assert.AreEqual(f1.GetHashCode(), f.GetHashCode());
        }

        [TestMethod]
        public void TestGetHashCodeWithVariables()
        {
            Formula f = new Formula("x1 + X2", s => s.ToLower(), s => true);
            Formula f1 = new Formula("x1 + x2");
            Assert.AreEqual(f1.GetHashCode(), f.GetHashCode());
        }

        [TestMethod]
        public void TestGetHashCodeWithVariablesSwitchedAround()
        {
            Formula f = new Formula("X2 + x1", s => s.ToLower(), s => true);
            Formula f1 = new Formula("x1 + x2");
            Assert.AreNotEqual(f1.GetHashCode(), f.GetHashCode());
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestWithVariableHavingSpaces()
        {
            Formula f = new Formula("X 21", s => s.ToLower(), s => true);
          
        }

        [TestMethod]
        public void TestGetHashCodeWithVariablesComparingString()
        {
            String s = "x1+x2";
            Formula f = new Formula("x1 + X2", s => s.ToLower(), s => true);
            Assert.AreEqual(f.GetHashCode(), s.GetHashCode());
        }

        [TestMethod]
        public void TestSimpleAdditionDouble()
        {
            Formula f = new Formula("2.00000000000000000000000005 + 2.0");
            Assert.AreEqual(4.0, (double)f.Evaluate(s => 0), 1e-10);
        }

        [TestMethod]
        public void TestGetMultiVariablesWithoutNormalizer()
        {
            Formula f = new Formula("x + Y / y");
            HashSet<string> hashSet = new HashSet<string>() { "x", "Y", "y" };
            Assert.IsTrue(hashSet.SetEquals(f.GetVariables()));
        }

        [TestMethod]
        public void TestGetMultiVariablesWithNormalizer()
        {
            Formula f = new Formula("x + y * X", s => s.ToUpper(), s => true);
            HashSet<string> hashSet = new HashSet<string>() { "X", "Y" };
            Assert.IsTrue(hashSet.SetEquals(f.GetVariables()));
        }

        [TestMethod]
        public void TestGetMultiVariablesWithoutNormalizerStress()
        {
            Formula f = new Formula("x + Y / y / _1 + 334 + ABCD32432 + __ + _5 - AA");
            HashSet<string> hashSet = new HashSet<string>() { "x", "Y", "y", "_1", "ABCD32432", "__", "_5", "AA" };
            Assert.IsTrue(hashSet.SetEquals(f.GetVariables()));
        }

        [TestMethod]
        public void TestGetMultiVariablesWithNormalizerStress()
        {
            Formula f = new Formula("x + Y / y / _1 + 334 +  aa  * ABCD32432 + __ + _5 - AA", s => s.ToUpper(), s => true);
            HashSet<string> hashSet = new HashSet<string>() { "X", "Y", "_1", "ABCD32432", "__", "_5", "AA" };
            Assert.IsTrue(hashSet.SetEquals(f.GetVariables()));
        }


        [TestMethod]
        public void TestGetVariablesWithoutNormalizer()
        {
            Formula f = new Formula("x + Y");
            HashSet<string> hashSet = new HashSet<string>() { "x", "Y" };
            Assert.IsTrue(hashSet.SetEquals(f.GetVariables()));
        }

        [TestMethod]
        public void TestGetVariablesWithNormalizer()
        {
            Formula f = new Formula("x + y", s => s.ToUpper(), s => true);
            HashSet<string> hashSet = new HashSet<string>() { "X", "Y" };
            Assert.IsTrue(hashSet.SetEquals(f.GetVariables()));
        }


        [TestMethod]
        public void TestSingleNumber()
        {
            Formula f = new Formula("70");
            Assert.AreEqual(70, (double)f.Evaluate(s => 0));
        }

        [TestMethod]
        public void TestSingleVariable()
        {
            Formula f = new Formula("x5", s => s.ToUpper(), s => true);
            Assert.AreEqual(22, (double)f.Evaluate(s => 22));
        }

        [TestMethod]
        public void TestSingleVariableNotFound()
        {
            Formula f = new Formula("x5", s => s.ToUpper(), s => true);
            Assert.IsInstanceOfType(f.Evaluate(s => (s == "x5") ? 0 : throw new ArgumentException()), typeof(FormulaError));
        }

        [TestMethod]
        public void TestSimpleSubtraction()
        {
            Formula f = new Formula("8-10");
            Assert.AreEqual(-2, (double)f.Evaluate(s => 10));
        }

        [TestMethod]
        public void TestDecimalSubtraction()
        {
            Formula f = new Formula("3.55-2.5");
            Assert.AreEqual(1.05, (double)f.Evaluate(s => 0), 1e-2);
        }

        [TestMethod]
        public void TestSimpleMultiplication()
        {
            Formula f = new Formula("3*3");
            Assert.AreEqual(9, (double)f.Evaluate(s => 0));
        }

        [TestMethod]
        public void TestDecimalMultiplication()
        {
            Formula f = new Formula("3.5*2.5");
            Assert.AreEqual(8.75, (double)f.Evaluate(s => 0));
        }

        [TestMethod]
        public void TestSimpleDivision()
        {
            Formula f = new Formula("16/2");
            Assert.AreEqual(8.00, (double)f.Evaluate(s => 0), 1e-2);
        }

        [TestMethod]
        public void TestDecimalDivision()
        {
            Formula f = new Formula("132/13");
            Assert.AreEqual(10.153, (double)f.Evaluate(s => 0), 1e-3);
        }


        [TestMethod]
        public void TestAllOperatorsTogether()
        {
            Formula f = new Formula("A10-A10*A10/A10");
            Assert.AreEqual(0, (double)f.Evaluate(s => 3));
        }


        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestClosingParenthesesAfterOperator()
        {
            Formula f = new Formula("(2+5-2/)4");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestMoreClosingParenthesesThanOpening()
        {
            Formula f = new Formula("(5+(3))+(3)+2)");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidVariableAfterNormalizing()
        {
            Formula f = new Formula("x + y", s => s.ToUpper()+"#$", s => false);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidVariable()
        {
            Formula f = new Formula("x + y", s => s.ToUpper(), s => false);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestFinalInvalidVariableAfterNormalizing()
        {
            Formula f = new Formula("10 + y", s => s.ToUpper() + "#$", s => true);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestFinalInvalidVariable()
        {
            Formula f = new Formula("1423 + y", s => s.ToUpper(), s => false);
        }

        

        [TestMethod]
        public void TestUnknownVariable()
        {
            Formula f = new Formula("2+x1", s => s + "wassup", s => true);
            Assert.IsInstanceOfType(f.Evaluate(s => { throw new ArgumentException("Unknown variable"); }), typeof(FormulaError));
        }

        [TestMethod]
        public void TestLeftToRight()
        {
            Formula f = new Formula("2*6+3");
            Assert.AreEqual(15,(double) f.Evaluate( s => 0));
        }

        [TestMethod]
        public void TestParenthesesMultiplication()
        {
            Formula f = new Formula("(2+6)*3");
            Assert.AreEqual(24, (double) f.Evaluate(s => 0));
        }



        [TestMethod]
        public void TestTimesParentheses()
        {
            Formula f = new Formula("2*(3+5)");
            Assert.AreEqual(16, (double)f.Evaluate( s => 0));
        }

        [TestMethod]
        public void TestPlusParentheses()
        {
            Formula f = new Formula("2+(3+5)");
            Assert.AreEqual(10, (double)f.Evaluate(s=>0));
        }

        [TestMethod]
        public void TestPlusComplex()
        {
            Formula f = new Formula("2+(3+5*9)");
            Assert.AreEqual(50, (double)f.Evaluate(s => 0));
        }

        [TestMethod]
        public void TestOperatorAfterParens()
        {
            Formula f = new Formula("(1*1)-2/2") ;
            Assert.AreEqual(0,(double) f.Evaluate(s => 0));
        }

        [TestMethod]
        public void TestComplexTimesParentheses()
        {
            Formula f = new Formula("2+3*(3+5)");
            Assert.AreEqual(26, (double)f.Evaluate( s => 0));
        }

     

        [TestMethod]
        public void TestComplexAndParentheses()
        {
            Formula f = new Formula("2+3*5+(3+4*8)*5+2");
            Assert.AreEqual(194,(double) f.Evaluate( s => 0));
        }


        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidNumAfterParentheses()
        {
            Formula f = new Formula("(2)4");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSingleOperator()
        {
            Formula f = new Formula("+", s => "0", s=> true);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraOperator()
        {
            Formula f = new Formula("2+5+");
        }



        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParensNoOperator()
        {
            Formula f = new Formula("5+7+(5)8");
        }



        [TestMethod]
        public void TestComplexMultiVar()
        {
            Formula f = new Formula("y1*3-8/2+4*(8-9*2)/14*x7");
            Assert.AreEqual(5.1428, (double)f.Evaluate(s => (s == "x7") ? 1 : 4), 1e-4);
        }

       

        [TestMethod]
        public void TestComplexNestedParensLeft()
        {
            Formula f = new Formula("((((x1*x2)*x3)*x4)*x5)*x6");
            Assert.AreEqual(64, (double)f.Evaluate(s => 2));
        }

        // Normalizer tests
        [TestMethod(), Timeout(2000)]
        [TestCategory("1")]
        public void TestNormalizerGetVars()
        {
            Formula f = new Formula("2+x1", s => s.ToUpper(), s => true);
            HashSet<string> vars = new HashSet<string>(f.GetVariables());

            Assert.IsTrue(vars.SetEquals(new HashSet<string> { "X1" }));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("2")]
        public void TestNormalizerEquals()
        {
            Formula f = new Formula("2+x1", s => s.ToUpper(), s => true);
            Formula f2 = new Formula("2+X1", s => s.ToUpper(), s => true);

            Assert.IsTrue(f.Equals(f2));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("3")]
        public void TestNormalizerToString()
        {
            Formula f = new Formula("2+x1", s => s.ToUpper(), s => true);
            Formula f2 = new Formula(f.ToString());

            Assert.IsTrue(f.Equals(f2));
        }

        // Validator tests
        [TestMethod(), Timeout(2000)]
        [TestCategory("4")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestValidatorFalse()
        {
            Formula f = new Formula("2+x1", s => s, s => false);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("5")]
        public void TestValidatorX1()
        {
            Formula f = new Formula("2+x", s => s, s => (s == "x"));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("6")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestValidatorX2()
        {
            Formula f = new Formula("2+y1", s => s, s => (s == "x"));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("7")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestValidatorX3()
        {
            Formula f = new Formula("2+x1", s => s, s => (s == "x"));
        }


        // Simple tests that return FormulaErrors
        [TestMethod(), Timeout(2000)]
        [TestCategory("8")]
        public void TestUnknownVariable1()
        {
            Formula f = new Formula("2+X1");
            Assert.IsInstanceOfType(f.Evaluate(s => { throw new ArgumentException("Unknown variable"); }), typeof(FormulaError));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("9")]
        public void TestDivideByZero1()
        {
            Formula f = new Formula("5/0");
            Assert.IsInstanceOfType(f.Evaluate(s => 0), typeof(FormulaError));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("10")]
        public void TestDivideByZeroVars()
        {
            Formula f = new Formula("(5 + X1) / (X1 - 3)");
            Assert.IsInstanceOfType(f.Evaluate(s => 3), typeof(FormulaError));
        }


        // Tests of syntax errors detected by the constructor
        [TestMethod(), Timeout(2000)]
        [TestCategory("11")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSingleOperator1()
        {
            Formula f = new Formula("+");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("12")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraOperator1()
        {
            Formula f = new Formula("2+5+");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("13")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraCloseParen()
        {
            Formula f = new Formula("2+5*7)");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("14")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraOpenParen()
        {
            Formula f = new Formula("((3+5*7)");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("15")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNoOperator()
        {
            Formula f = new Formula("5x");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("16")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNoOperator2()
        {
            Formula f = new Formula("5+5x");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("17")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNoOperator3()
        {
            Formula f = new Formula("5+7+(5)8");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("18")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestNoOperator4()
        {
            Formula f = new Formula("5 5");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("19")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestDoubleOperator()
        {
            Formula f = new Formula("5 + + 3");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("20")]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmpty()
        {
            Formula f = new Formula("");
        }

        // Some more complicated formula evaluations
        [TestMethod(), Timeout(2000)]
        [TestCategory("21")]
        public void TestComplex1()
        {
            Formula f = new Formula("y1*3-8/2+4*(8-9*2)/14*x7");
            Assert.AreEqual(5.14285714285714, (double)f.Evaluate(s => (s == "x7") ? 1 : 4), 1e-9);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("22")]
        public void TestRightParens()
        {
            Formula f = new Formula("x1+(x2+(x3+(x4+(x5+x6))))");
            Assert.AreEqual(6, (double)f.Evaluate(s => 1), 1e-9);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("23")]
        public void TestLeftParens()
        {
            Formula f = new Formula("((((x1+x2)+x3)+x4)+x5)+x6");
            Assert.AreEqual(12, (double)f.Evaluate(s => 2), 1e-9);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("53")]
        public void TestRepeatedVar()
        {
            Formula f = new Formula("a4-a4*a4/a4");
            Assert.AreEqual(0, (double)f.Evaluate(s => 3), 1e-9);
        }

        // Test of the Equals method
        [TestMethod(), Timeout(2000)]
        [TestCategory("24")]
        public void TestEqualsBasic()
        {
            Formula f1 = new Formula("X1+X2");
            Formula f2 = new Formula("X1+X2");
            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("25")]
        public void TestEqualsWhitespace()
        {
            Formula f1 = new Formula("X1+X2");
            Formula f2 = new Formula(" X1  +  X2   ");
            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("26")]
        public void TestEqualsDouble()
        {
            Formula f1 = new Formula("2+X1*3.00");
            Formula f2 = new Formula("2.00+X1*3.0");
            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("27")]
        public void TestEqualsComplex()
        {
            Formula f1 = new Formula("1e-2 + X5 + 17.00 * 19 ");
            Formula f2 = new Formula("   0.0100  +     X5+ 17 * 19.00000 ");
            Assert.IsTrue(f1.Equals(f2));
        }


        [TestMethod(), Timeout(2000)]
        [TestCategory("28")]
        public void TestEqualsNullAndString()
        {
            Formula f = new Formula("2");
            Assert.IsFalse(f.Equals(null));
            Assert.IsFalse(f.Equals(""));
        }


        // Tests of == operator
        [TestMethod(), Timeout(2000)]
        [TestCategory("29")]
        public void TestEq()
        {
            Formula f1 = new Formula("2");
            Formula f2 = new Formula("2");
            Assert.IsTrue(f1 == f2);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("30")]
        public void TestEqFalse()
        {
            Formula f1 = new Formula("2");
            Formula f2 = new Formula("5");
            Assert.IsFalse(f1 == f2);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("31")]
        public void TestEqNull()
        {
            Formula f1 = new Formula("2");
            Formula f2 = new Formula("2");
            Assert.IsFalse(null == f1);
            Assert.IsFalse(f1 == null);
            Assert.IsTrue(f1 == f2);
        }


        // Tests of != operator
        [TestMethod(), Timeout(2000)]
        [TestCategory("32")]
        public void TestNotEq()
        {
            Formula f1 = new Formula("2");
            Formula f2 = new Formula("2");
            Assert.IsFalse(f1 != f2);
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("33")]
        public void TestNotEqTrue()
        {
            Formula f1 = new Formula("2");
            Formula f2 = new Formula("5");
            Assert.IsTrue(f1 != f2);
        }


        // Test of ToString method
        [TestMethod(), Timeout(2000)]
        [TestCategory("34")]
        public void TestString()
        {
            Formula f = new Formula("2*5");
            Assert.IsTrue(f.Equals(new Formula(f.ToString())));
        }


        // Tests of GetHashCode method
        [TestMethod(), Timeout(2000)]
        [TestCategory("35")]
        public void TestHashCode()
        {
            Formula f1 = new Formula("2*5");
            Formula f2 = new Formula("2*5");
            Assert.IsTrue(f1.GetHashCode() == f2.GetHashCode());
        }

        // Technically the hashcodes could not be equal and still be valid,
        // extremely unlikely though. Check their implementation if this fails.
        [TestMethod(), Timeout(2000)]
        [TestCategory("36")]
        public void TestHashCodeFalse()
        {
            Formula f1 = new Formula("2*5");
            Formula f2 = new Formula("3/8*2+(7)");
            Assert.IsTrue(f1.GetHashCode() != f2.GetHashCode());
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("37")]
        public void TestHashCodeComplex()
        {
            Formula f1 = new Formula("2 * 5 + 4.00 - _x");
            Formula f2 = new Formula("2*5+4-_x");
            Assert.IsTrue(f1.GetHashCode() == f2.GetHashCode());
        }


        // Tests of GetVariables method
        [TestMethod(), Timeout(2000)]
        [TestCategory("38")]
        public void TestVarsNone()
        {
            Formula f = new Formula("2*5");
            Assert.IsFalse(f.GetVariables().GetEnumerator().MoveNext());
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("39")]
        public void TestVarsSimple()
        {
            Formula f = new Formula("2*X2");
            List<string> actual = new List<string>(f.GetVariables());
            HashSet<string> expected = new HashSet<string>() { "X2" };
            Assert.AreEqual(actual.Count, 1);
            Assert.IsTrue(expected.SetEquals(actual));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("40")]
        public void TestVarsTwo()
        {
            Formula f = new Formula("2*X2+Y3");
            List<string> actual = new List<string>(f.GetVariables());
            HashSet<string> expected = new HashSet<string>() { "Y3", "X2" };
            Assert.AreEqual(actual.Count, 2);
            Assert.IsTrue(expected.SetEquals(actual));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("41")]
        public void TestVarsDuplicate()
        {
            Formula f = new Formula("2*X2+X2");
            List<string> actual = new List<string>(f.GetVariables());
            HashSet<string> expected = new HashSet<string>() { "X2" };
            Assert.AreEqual(actual.Count, 1);
            Assert.IsTrue(expected.SetEquals(actual));
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("42")]
        public void TestVarsComplex()
        {
            Formula f = new Formula("X1+Y2*X3*Y2+Z7+X1/Z8");
            List<string> actual = new List<string>(f.GetVariables());
            HashSet<string> expected = new HashSet<string>() { "X1", "Y2", "X3", "Z7", "Z8" };
            Assert.AreEqual(actual.Count, 5);
            Assert.IsTrue(expected.SetEquals(actual));
        }

        // Tests to make sure there can be more than one formula at a time
        [TestMethod(), Timeout(2000)]
        [TestCategory("43")]
        public void TestMultipleFormulae()
        {
            Formula f1 = new Formula("2 + a1");
            Formula f2 = new Formula("3");
            Assert.AreEqual(2.0, f1.Evaluate(x => 0));
            Assert.AreEqual(3.0, f2.Evaluate(x => 0));
            Assert.IsFalse(new Formula(f1.ToString()) == new Formula(f2.ToString()));
            IEnumerator<string> f1Vars = f1.GetVariables().GetEnumerator();
            IEnumerator<string> f2Vars = f2.GetVariables().GetEnumerator();
            Assert.IsFalse(f2Vars.MoveNext());
            Assert.IsTrue(f1Vars.MoveNext());
        }

        // Repeat this test to increase its weight
        [TestMethod(), Timeout(2000)]
        [TestCategory("44")]
        public void TestMultipleFormulaeB()
        {
            TestMultipleFormulae();
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("45")]
        public void TestMultipleFormulaeC()
        {
            TestMultipleFormulae();
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("46")]
        public void TestMultipleFormulaeD()
        {
            TestMultipleFormulae();
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("47")]
        public void TestMultipleFormulaeE()
        {
            TestMultipleFormulae();
        }

        // Stress test for constructor
        [TestMethod(), Timeout(2000)]
        [TestCategory("48")]
        public void TestConstructor()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        // This test is repeated to increase its weight
        [TestMethod(), Timeout(2000)]
        [TestCategory("49")]
        public void TestConstructorB()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("50")]
        public void TestConstructorC()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        [TestMethod(), Timeout(2000)]
        [TestCategory("51")]
        public void TestConstructorD()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        // Stress test for constructor
        [TestMethod(), Timeout(2000)]
        [TestCategory("52")]
        public void TestConstructorE()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }


    }
}
