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

        public Postfix()
        {
            this.postfix = new Queue<string>();
            this.myStack = new Stack<string>();
        }

        public void buildFromInfix(Infix infix)
        {
            infix.printDebug();
            while(!infix.isEmpty())
            {
                string term = infix.pop();
                if (Calculator.isOperator(term))
                {
                    while (this.myStack.Count > 0)
                    {
                        //exit loop if precedence lower
                        string prevOp = this.myStack.Pop();
                        if (precedence(prevOp) < precedence(term))
                        {
                            this.myStack.Push(prevOp);
                            break;
                        }
                        this.postfix.Enqueue(prevOp);
                    }
                    this.myStack.Push(term);
                } else
                {
                    this.postfix.Enqueue(term);
                }
            }
            //if stack not empty, finish it here
            while (this.myStack.Count > 0)
            {
                string op = this.myStack.Pop();
                this.postfix.Enqueue(op);
            }
        }

        private int precedence(string op)
        {
            if (op.Equals("*") || op.Equals("/"))
            {
                return 2;
            } else if (op.Equals("+") || op.Equals("-"))
            {
                return 1;
            } else if (op.Equals("^"))
            {
                return 3;
            }
            return 0;
        }

        public double evaluate()
        {
            double sum = 0;
            Stack<double> evalStack = new Stack<double>();
            while (this.postfix.Count > 0)
            {
                string term = this.postfix.Dequeue();
                if (Calculator.isOperator(term))
                {
                    double num2 = evalStack.Pop();
                    double num1 = evalStack.Pop();
                    double res = this.compute(num1, num2, term);
                    evalStack.Push(res);
                } else
                {
                    evalStack.Push(Double.Parse(term));
                }
            }
            return evalStack.Pop();
        }

        private double compute(double num1, double num2, string op)
        {
            switch(op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "*":
                    return num1 * num2;
                case "/":
                    return num1 / num2;
                case "^":
                    return Math.Pow(num1,num2);
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
