using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Calculator
{
    class Postfix
    {

        private Queue<string> postfix;
        private Stack<string> myStack;
        private string currentNumber;

        public Postfix()
        {
            this.postfix = new Queue<string>();
            this.myStack = new Stack<string>();
            this.currentNumber = string.Empty;
        }

        public void buildFromInfix(string infix)
        {
            string expression = infix + ")";
            int len = expression.Length;
            myStack.Push("(");
            for (int i=0;i<len;i++)
            {
                char currChar = expression[i];
                processChar(currChar);
            }
            EnqueueCurrentNumber();
        }

        //wrap this in a try catch later to check for empty stack exception
        private void processChar(char term)
        {
            if (IsOperator(term))
            {
                EnqueueCurrentNumber();
                string x = myStack.Pop();
                while (IsOperator(x[0]) && precedence(x[0]) >= precedence(term))
                {
                    postfix.Enqueue(x);
                    x = myStack.Pop();
                }
                myStack.Push(x);
                myStack.Push(term.ToString());
            }
            else if (IsLeftBracket(term))
            {
                EnqueueCurrentNumber();
                myStack.Push(term.ToString());
            }
            else if (IsRightBracket(term))
            {
                EnqueueCurrentNumber();
                string x = myStack.Pop();
                while (!IsLeftBracket(x[0]))
                {
                    postfix.Enqueue(x);
                    x = myStack.Pop();
                }
            }
            else
            {
                this.currentNumber += term;
            }
        }

        private void EnqueueCurrentNumber()
        {
            if (this.currentNumber.Length > 0)
            {
                this.postfix.Enqueue(this.currentNumber);
                this.currentNumber = string.Empty;
            }
        }

        private static bool IsOperator(char term)
        {
            if (term.Equals('+')
                || term.Equals('-')
                || term.Equals('*')
                || term.Equals('/')
                || term.Equals('^')
                || term.Equals('~'))
            {
                return true;
            }
            return false;
        }

        private static bool IsLeftBracket(char term)
        {
            if (term.Equals('('))
            {
                return true;
            }
            return false;
        }

        private static bool IsRightBracket(char term)
        {
            if (term.Equals(')'))
            {
                return true;
            }
            return false;
        }

        private int precedence(char op)
        {
            if (op.Equals('*') || op.Equals('/'))
            {
                return 2;
            } else if (op.Equals('+') || op.Equals('-'))
            {
                return 1;
            } else if (op.Equals('^'))
            {
                return 4;
            } else if (op.Equals('~'))
            {
                return 3;
            }
            return 0;
        }

        public double evaluate()
        {
            double sum = 0;
            Stack<double> evalStack = new Stack<double>();
            postfix.Enqueue(")");
            string term = postfix.Dequeue();
            Console.WriteLine(term);
            while (!IsRightBracket(term[0]))
            {
                Console.WriteLine(term);
                if (IsOperator(term[0]))
                {
                    double res = compute(evalStack,term);
                    evalStack.Push(res);
                }
                else
                {
                    double operand = double.Parse(term);
                    evalStack.Push(operand);
                }
                term = postfix.Dequeue();
            }
            return evalStack.Pop();
        }

        private double compute(Stack<double> evalStack, string op)
        {
            double term1 = evalStack.Pop();
            double term2;
            switch (op)
            {
                case "+":
                    term2 = evalStack.Pop();
                    return term2 + term1;
                case "-":
                    term2 = evalStack.Pop();
                    return term2 - term1;
                case "*":
                    term2 = evalStack.Pop();
                    return term2 * term1;
                case "/":
                    term2 = evalStack.Pop();
                    return term2 / term1;
                case "^":
                    term2 = evalStack.Pop();
                    return Math.Pow(term2,term1);
                case "~":
                    return term1 * -1;
                default:
                    return 0;
            }
        }

        public void empty()
        {
            this.postfix.Clear();
            this.myStack.Clear();
        }

        public void printDebug()
        {
            Debug.WriteLine("Printing postfix:");
            foreach (string term in this.postfix.ToArray())
            {
                Debug.WriteLine(term);
            }
        }
    }
}
