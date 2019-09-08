using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {

        //newest term entered in the calculator
        private string currTerm = "";
        private Infix infixQueue;
        private Postfix postfixStack;

        public Calculator()
        {
            InitializeComponent();
            this.infixQueue = new Infix();
            this.postfixStack = new Postfix();
        }

        private void addNumber(string num)
        {
            if (Calculator.isOperator(this.currTerm))
            {
                this.infixQueue.push(this.currTerm);
                this.currTerm = num;
            }
            else
            {
                if (this.currTerm.Equals("0"))
                {
                    this.currTerm = num;
                    textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
                } else
                {
                    this.currTerm += num;
                }
            }
            textBox1.Text = textBox1.Text + num;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.addNumber("1");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.addNumber("2");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.addNumber("3");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.addNumber("4");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.addNumber("5");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            this.addNumber("6");
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.addNumber("7");
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            this.addNumber("8");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            this.addNumber("9");
        }

        //Zero
        private void Button11_Click(object sender, EventArgs e)
        {
            this.addNumber("0");
        }

        public static bool isOperator(string term)
        {
            if (term.Equals("+") || term.Equals("-") || term.Equals("*") || term.Equals("/"))
            {
                return true;
            }
            return false;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (Calculator.isOperator(this.currTerm))
            {
                this.currTerm = "+";
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 3) + " + ";
            } else if (this.currTerm.Length > 0)
            {
                this.infixQueue.push(this.currTerm);
                this.currTerm = "+";
                textBox1.Text += " + ";
            }
            
        }

        private void ButtonSub_Click(object sender, EventArgs e)
        {
            if (Calculator.isOperator(this.currTerm))
            {
                this.currTerm = "-";
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 3) + " - ";
            }
            else if (this.currTerm.Length > 0)
            {
                this.infixQueue.push(this.currTerm);
                this.currTerm = "-";
                textBox1.Text += " - ";
            }
        }

        private void ButtonMult_Click(object sender, EventArgs e)
        {
            if (Calculator.isOperator(this.currTerm))
            {
                this.currTerm = "*";
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 3) + " * ";
            }
            else if (this.currTerm.Length > 0)
            {
                this.infixQueue.push(this.currTerm);
                this.currTerm = "*";
                textBox1.Text += " * ";
            }
        }

        private void ButtonDiv_Click(object sender, EventArgs e)
        {
            if (Calculator.isOperator(this.currTerm))
            {
                this.currTerm = "/";
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 3) + " / ";
            }
            else if (this.currTerm.Length > 0)
            {
                this.infixQueue.push(this.currTerm);
                this.currTerm = "/";
                textBox1.Text += " / ";
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            //empty all stacks, clear everything
            textBox1.Text = "";
            this.currTerm = "";
            this.infixQueue.empty();
            this.postfixStack.empty();
        }

        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            if (!Calculator.isOperator(this.currTerm))
            {
                this.infixQueue.push(this.currTerm);
                this.currTerm = "";
            }
            Debug.WriteLine("evaluating...");
            this.postfixStack.buildFromInfix(this.infixQueue);
            this.postfixStack.printDebug();
            double res = 0;
            try
            {
                res = this.postfixStack.evaluate();
            } catch (DivideByZeroException ex)
            {
                res = 0;
            } finally
            {
                textBox1.Text = res.ToString();
                this.infixQueue.empty();
                this.postfixStack.empty();
                this.currTerm = res.ToString();
            }
            
        }
    }
}
