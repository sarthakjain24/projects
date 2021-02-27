using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
/// <summary>
/// Evaluates the formulas
/// </summary>
namespace FormulaEvaluator
{
    /// <summary>
    /// A class that evaluates basic string expressions using an Infix Calculator approach
    /// <author>Sarthak Jain</author>
    /// </summary>
    public static class Evaluator
    {
        /// <summary>
        /// A delegate used to lookup variables
        /// </summary>
        /// <param name="v">The variable used to look up</param>
        /// <returns>An integer representing the variable</returns>
        public delegate int Lookup(String v);

        /// <summary>
        /// Evaluates the expression
        /// </summary>
        /// <param name="exp"></param>
        /// <param name="variableEvaluator"></param>
        /// <returns>A number that represents the evaluation</returns>
        public static int Evaluate(String exp, Lookup variableEvaluator)
        {

            //The number that would be returned after calculating everything
            int returnNum = 0;

            //A stack to represent the values in the expression
            Stack<int> valStack = new Stack<int>();

            //A stack to represent the operators in the expression
            Stack<string> opStack = new Stack<string>();

            //Splits the expressions by a space, parentheses, addition, subtraction, multiplication, division, numbers and adds it to an array
            string[] expArray = Regex.Split(exp, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            //Iterates through the array
            for (int i = 0; i < expArray.Length; i++)
            {
                //Gets the value from index i in the array, and sets it to arrValue, while trimming the leading and trailing whitespace
                string arrValue = expArray[i].Trim();

                //If the arrValue is "", then moves on to the next iteration
                if (arrValue == "")
                {
                    continue;
                }

                //If the arrValue is a number, by which it can be parsed, meaning that it is an integer, then it goes into this condition
                if (int.TryParse(arrValue, out _))
                {
                    //Parses the value to a number
                    int parsedNum = int.Parse(arrValue);

                    //If the operator stack is not empty, then it goes into this condition
                    if (opStack.Count != 0)
                    {

                        //If the operator's stack has a "*" or a "/" symbol on the top, then we go into this condition
                        if (opStack.Peek() == "*" || opStack.Peek() == "/")
                        {

                            //If the value stack is empty, then we throw an exception, as we can't perform our operation
                            if (valStack.Count == 0)
                            {
                                throw new ArgumentException("Error. The values are matched incorrectly");
                            }

                            //Pops the top value from the value stack and stores it in the poppedVal variable
                            int poppedVal = valStack.Pop();

                            //Pops the top operator from the operator stack and stores it in the poppedOp variable
                            String poppedOp = opStack.Pop();

                            //Computes the new number that is going to be found based on the poppedOp and the two numbers passed and pushes the new number to the value stack
                            int newNum = Computation(parsedNum, poppedVal, poppedOp);
                            valStack.Push(newNum);

                        }

                        //If the top of the operator is not a "*" or a "/", we push the arrValue parsed to an int, and into the value stack
                        else
                        {
                            valStack.Push(parsedNum);
                        }
                    }

                    //If the operator stack is empty, then it pushes the value into the value stack
                    else
                    {
                        valStack.Push(parsedNum);
                    }
                }

                //If the arrValue is a "+" or a "-", then it goes into the condition
                else if (arrValue == "+" || arrValue == "-")
                {

                    //If the opStack size is greater than 0, then it goes into this condition
                    if (opStack.Count > 0)
                    {

                        //If the opStack peek is "+" or "-", then it goes into the condition
                        if (opStack.Peek() == "+" || opStack.Peek() == "-")
                        {

                            //If the valStack count is less than 2, then it throws an exception
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException("Error. The number of values are incorrect");
                            }

                            //Pops the two values from the valStack, and one value from the opStack and stores it in the given variables
                            int rightNum = valStack.Pop();
                            int leftNum = valStack.Pop();
                            string operatorVal = opStack.Pop();

                            //Gets a new number based on the computations of rightNum, leftNum and operatorVal and pushes that value to the value stack
                            int newNum = Computation(rightNum, leftNum, operatorVal);
                            valStack.Push(newNum);

                        }
                    }

                    //Pushes the arrValue to the opStack
                    opStack.Push(arrValue);
                }

                //If the arrValue is a "*" or a "/" or a "(", then we push it to the operator stack
                else if (arrValue == "*" || arrValue == "/" || arrValue == "(")
                {
                    opStack.Push(arrValue);
                }

                //If the arrValue is a ")", then it goes into the condition
                else if (arrValue == ")")
                {

                    //If the opStack peek is "+" or "-", then it goes into this condition
                    if (opStack.Peek() == "+" || opStack.Peek() == "-")
                    {

                        //If the valStack count is less than 2, then it throws an exception
                        if (valStack.Count < 2)
                        {
                            throw new ArgumentException("Error. The number of values are incorrect");
                        }

                        //Pops the two values from the valStack, and one value from the opStack and stores it in the given variables
                        int rightNum = valStack.Pop();
                        int leftNum = valStack.Pop();
                        string operatorVal = opStack.Pop();

                        //Gets a new number based on the computations of rightNum, leftNum and operatorVal and pushes that value to the value stack
                        int newNum = Computation(rightNum, leftNum, operatorVal);
                        valStack.Push(newNum);


                    }

                    //If the opStack peek is not "(" or if the operator stack is empty, then it throws an exception
                    if (opStack.Count == 0 || opStack.Peek() != "(")
                    {
                        throw new ArgumentException("Error. The brackets aren't balanced properly");
                    }

                    //Else If the opStack peek is "(", then it pops the operator
                    else if (opStack.Peek() == "(")
                    {
                        opStack.Pop();
                    }

                    //If the opStack size is not 0, then it goes to this condition
                    if (opStack.Count != 0)
                    {

                        //If the opStack peek is either "*" or "/", then it goes into this condition
                        if (opStack.Peek() == "*" || opStack.Peek() == "/")
                        {

                            //If the valStack is less than 2, then it throws an exception
                            if (valStack.Count < 2)
                            {
                                throw new ArgumentException("Error. The number of values are incorrect");
                            }

                            //Pops the two values from the valStack, and one value from the opStack and stores it in the given variables
                            int rightNum = valStack.Pop();
                            int leftNum = valStack.Pop();
                            string operatorVal = opStack.Pop();

                            //Gets a new number based on the computations of rightNum, leftNum and operatorVal and pushes that value to the value stack
                            int newNum = Computation(rightNum, leftNum, operatorVal);
                            valStack.Push(newNum);
                        }
                    }
                }

                //If the arrValue is anything else, including a variable, then it goes into the condition
                else
                {

                    //Initializes the looked up value
                    int lookedUpValue;

                    //If the arrValue is a variable, then it passes the arrValue to the delegate, and sets whatever comes out as the lookedUpValue, else throws an exception
                    if (isVar(arrValue))
                    {
                        lookedUpValue = variableEvaluator(arrValue);
                    }
                    else
                    {
                        throw new ArgumentException("Error. Variable is defined incorrectly");
                    }

                    if (opStack.Count != 0)
                    {
                        //If the operator's stack has a "*" or a "/" symbol on the top, then we go into this condition
                        if (opStack.Peek() == "*" || opStack.Peek() == "/")
                        {

                            //If the value stack is empty, then we throw an exception, as we can't perform our operation
                            if (valStack.Count == 0)
                            {
                                throw new ArgumentException("Error. The number of values are incorrect");
                            }

                            //Pops the top value from the value stack and stores it in the poppedVal variable
                            int poppedVal = valStack.Pop();

                            //Pops the top operator from the operator stack and stores it in the poppedOp variable
                            String poppedOp = opStack.Pop();

                            //Gets a new number based on the computations of lookedUpValue, poppedVal and poppedOp and pushes that value to the value stack
                            int newNum = Computation(lookedUpValue, poppedVal, poppedOp);
                            valStack.Push(newNum);
                        }
                        else
                        {
                            valStack.Push(lookedUpValue);
                        }
                    }
                    //If the top of the operator is not a "*" or a "/", we push the arrValue parsed to an int, and into the value stack
                    else
                    {
                        valStack.Push(lookedUpValue);
                    }
                }
            }

            /*
             * After all the values are processed, then it goes into the conditions
             */

            //If the opStack is empty, then it goes into this condition
            if (opStack.Count == 0)
            {

                //If the valStack has more than 1 item, then it throws an error, else sets the value from the valStack to returnNum
                if (valStack.Count != 1)
                {
                    throw new ArgumentException("Error the expression is incorrect");
                }

                //Returns the num based on the value at the top of the stack
                returnNum = valStack.Pop();
            }

            //If the opStack is not empty, then it goes into this condition
            else
            {
                //If the opStack is not 1, then it throws an exception
                if (opStack.Count != 1)
                {
                    throw new ArgumentException("Error, the number of operators are incorrect to be a legal expression");
                }

                //If the valStack is not 2, then it throws an exception
                if (valStack.Count != 2)
                {
                    throw new ArgumentException("Error, the numbers of values are incorrect to be a legal expression");
                }

                //If the opStack's peek is not "+" or "-", then it throws an exception
                if (opStack.Peek() != "+" && opStack.Peek() != "-")
                {
                    throw new ArgumentException("Error, the number of operators are incorrect to be a legal expression");
                }

                //If the top of the opStack is a "+" or a "-", then it goes into this condition
                if (opStack.Peek() == "+" || opStack.Peek() == "-")
                {

                    //Pops the two values from the valStack, and one value from the opStack and stores it in the given variables
                    int rightNum = valStack.Pop();
                    int leftNum = valStack.Pop();
                    string operatorVal = opStack.Pop();

                    //Gets a new number based on the computations of rightNum, leftNum and operatorVal and pushes that value to the value stack and sets it to returnNum
                    returnNum = Computation(rightNum, leftNum, operatorVal);

                }

            }

            //Returns the number based on what is left from the stack
            return returnNum;
        }


        /// <summary>
        /// A helper method that does the computation of numbers
        /// </summary>
        /// <param name="secondNum">The second number being computed</param>
        /// <param name="firstNum">The first number being computed</param>
        /// <param name="operatorVal">The operator on the basis of which the computation will happen</param>
        /// <returns></returns>
        private static int Computation(int secondNum, int firstNum, string operatorVal)
        {
            //The new number that is to be returned after the computation
            int returnNum = 0;
            //If the operatorVal is "+", then it adds the two numbers
            if (operatorVal == "+")
            {
                returnNum = firstNum + secondNum;
            }
            //If the operatorVal is "-", then it subtracts the two numbers
            if (operatorVal == "-")
            {
                returnNum = firstNum - secondNum;
            }
            //If the operatorVal is "*", then it multiplies the two numbers
            if (operatorVal == "*")
            {
                returnNum = firstNum * secondNum;
            }
            //If the operatorVal is "/", then it divides the secondNum from the firstNum
            if (operatorVal == "/")
            {
                //If the secondNum is 0, then it throws an ArgumentException because a division by 0 takes place
                if (secondNum == 0)
                {
                    throw new ArgumentException("Error. A division by 0 occurred");
                }
                returnNum = firstNum / secondNum;

            }
            //Returns the new number
            return returnNum;
        }
        /// <summary>
        /// A helper method to check if the variable is legal or not, done by using Prof. Kopta's example from the lecture 
        /// </summary>
        /// <param name="variable">The variable being checked</param>
        /// <returns>Whether the variable is legal or not</returns>
        private static bool isVar(string variable)
        {
            //Checks for a letter in the variable
            bool checkForLetter = false;
            //Checks for a digit in the variable
            bool checkForDigit = false;

            //If the first character is not a letter, then it throws an exception
            if (!Char.IsLetter(variable[0]))
            {
                throw new ArgumentException("Intial Character is not a letter");
            }
            int j;
            //Iterates till the end of the variable's length
            for (j = 0; j < variable.Length; j++)
            {

                //If the character is a letter, then it sets the checkForLetter to true, else breaks out of the for loop indicating that we should stop searching for letters
                if (Char.IsLetter(variable[j]))
                {
                    checkForLetter = true;
                }
                else
                {
                    break;
                }
            }
            //Iterates from where we left off till the end of the variable's length
            for (; j < variable.Length; j++)
            {
                //If the character is a digit, then it sets the checkForDigit to true, else throws an exception because we should be only expecting digits from here on out
                if (Char.IsDigit(variable[j]))
                {
                    checkForDigit = true;
                }
                else
                {
                    throw new ArgumentException("Variable is defined incorrectly");
                }
            }

            //Returns the boolean condition for checkForDigit and checkForLetter
            return checkForDigit && checkForLetter;
        }
    }


}
