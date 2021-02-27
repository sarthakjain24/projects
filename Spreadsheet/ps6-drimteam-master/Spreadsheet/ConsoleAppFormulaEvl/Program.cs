using FormulaEvaluator;
using System;

namespace ConsoleAppFormulaEvl
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(Evaluator.Evaluate("2+5*7)", s => 0));
            Console.WriteLine("(2 + 3) / 2 + 2: " + FormulaEvaluator.Evaluator.Evaluate("(2 + 3) / 2 + 2", Foo)); //Return 4
            Console.WriteLine("(2 * A6) + 5 + 2: " + Evaluator.Evaluate("(2 * A6) + 5 + 2", Foo)); //Return 21
            Console.WriteLine("(3 + 3 - 3): " + FormulaEvaluator.Evaluator.Evaluate("(3 + 3 - 3)", Foo)); //Return 3
            Console.WriteLine("(3 - 3): " + FormulaEvaluator.Evaluator.Evaluate("(3) - 3", Foo)); //Return 0;
            Console.WriteLine("(2 + A6) * 5 + 2: " + FormulaEvaluator.Evaluator.Evaluate("(2 + A6) * 5 + 2", Foo)); //Return 47 
            Console.WriteLine("5 + 3 * 7 - 8 / (4 + 3) - 2 / 2: " + FormulaEvaluator.Evaluator.Evaluate("5 + 3 * 7 - 8 / (4 + 3) - 2 / 2", Foo)); //Return 24
            Console.WriteLine("1(+1): " + FormulaEvaluator.Evaluator.Evaluate("1(+1)", Foo)); //Return 2
            Console.WriteLine("(3 * 4) + 7: " + FormulaEvaluator.Evaluator.Evaluate("(3 * 4) + 7", Foo)); //Return 19
            Console.WriteLine("(8 * 0 - 4): " + FormulaEvaluator.Evaluator.Evaluate("(8 * 0 - 4) ", Foo)); //Return -4
            Console.WriteLine("3 + A10: " + FormulaEvaluator.Evaluator.Evaluate("3 + A10", Foo)); //Return 103
            Console.WriteLine("2+7-(8*2)+3*2: " + FormulaEvaluator.Evaluator.Evaluate("2+7-(8*2)+3*2", Foo)); //Returns -1
            Console.WriteLine("4/(1-3)*5-3: " + FormulaEvaluator.Evaluator.Evaluate("4/(1-3)*5-3", Foo));
            Console.WriteLine("((5) + (3)): " + FormulaEvaluator.Evaluator.Evaluate("((5) + (3))", Foo));
            Console.WriteLine("5 * * (9 - 7) 3: " + FormulaEvaluator.Evaluator.Evaluate("5 * * (9 - 7) 3", Foo));
            Console.WriteLine("45-19 / (20 + 3) * ((10 + 6) / 3) - (10 + 9) * (25 / 5) - (10 + 10 * (11 - 10)): " + FormulaEvaluator.Evaluator.Evaluate("45-19 / (20 + 3) * ((10 + 6) / 3) - (10 + 9) * (25 / 5) - (10 + 10 * (11 - 10))", Foo)); //Return -70
            Console.WriteLine("(1)(1) +:" + FormulaEvaluator.Evaluator.Evaluate("(1)(1) +", Foo)); //Return 2
            Console.WriteLine("5-3:" + FormulaEvaluator.Evaluator.Evaluate("5-3", Foo)); //Return 2
            Console.WriteLine("10 / 2:" + FormulaEvaluator.Evaluator.Evaluate("10 / 2", Foo)); //Return 5
            int ii = Evaluator.Evaluate(null, Foo); //Throw ArgumentException
            int returnNull = FormulaEvaluator.Evaluator.Evaluate("2 / 0", Foo); //Throw divide by 0 exception






            try
            {
                int returnMultiVariable2 = FormulaEvaluator.Evaluator.Evaluate("A12B2 + 3", Foo); //Return error
                int y = FormulaEvaluator.Evaluator.Evaluate("[23 + 2]", Foo);
                int returnNegativeError = Evaluator.Evaluate("(2 * -3) ", Foo); //Return Error
                int rerr = FormulaEvaluator.Evaluator.Evaluate("a2a+1+1", Foo);
                int returnUnaryError = FormulaEvaluator.Evaluator.Evaluate("-3 * 1", Foo);
                int returnMultiVariable = FormulaEvaluator.Evaluator.Evaluate("A 1 + 2 + 3", Foo); //Return error
                int returnMultiVariable3 = FormulaEvaluator.Evaluator.Evaluate("12AB2 + 3", Foo); //Return error
                int returnError = FormulaEvaluator.Evaluator.Evaluate("3 + A 6", Foo); //Return error

                int re = FormulaEvaluator.Evaluator.Evaluate("2@+2", Foo);


            }
            catch (ArgumentException e)
            {

                Console.WriteLine(e.Message);
            }


        }

        static int Foo(String s)
        {
            if (s == "A6")
            {
                return 7;
            }
            if (s == "A10")
            {
                return 100;
            }
            throw new ArgumentException();
            //return 0;
        }
    }
}
