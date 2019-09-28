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
        private Postfix postfixStack;
        private bool needsReset;

        public Calculator()
        {
            InitializeComponent();
            this.postfixStack = new Postfix();
            this.needsReset = false;
        }

        private void addTerm(string term)
        {
            if (this.needsReset)
            {
                return;
            }
            textBox1.Text = textBox1.Text + term;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            addTerm("+");

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            addTerm("1");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            addTerm("2");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            addTerm("3");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            addTerm("4");
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            addTerm("5");
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            addTerm("6");
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            addTerm("7");
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            addTerm("8");
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            addTerm("9");
        }

        //Zero
        private void Button11_Click(object sender, EventArgs e)
        {
            addTerm("0");
        }

        private void ButtonSub_Click(object sender, EventArgs e)
        {
            addTerm("-");
        }

        private void ButtonMult_Click(object sender, EventArgs e)
        {
            addTerm("*");
        }

        private void ButtonDiv_Click(object sender, EventArgs e)
        {
            addTerm("/");
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            //empty all stacks, clear everything
            textBox1.Text = "";
            this.postfixStack.empty();
            this.needsReset = false;
        }

        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            if (this.needsReset)
            {
                return;
            }
            this.postfixStack.buildFromInfix(this.textBox1.Text);
            double res = 0;
            try
            {
                res = this.postfixStack.evaluate();
                textBox1.Text = res.ToString();
            }
            catch (DivideByZeroException ex)
            {
                res = 0;
                textBox1.Text = "Invalid Expression";
            }
            catch (InvalidOperationException ex)
            {
                res = 0;
                textBox1.Text = "Invalid Expression";
            }
            catch (Exception ex)
            {
                textBox1.Text = "Invalid Expression";
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                this.needsReset = true;
            }
            
        }

        private void ButtonExp_Click(object sender, EventArgs e)
        {
            addTerm("^");
        }

        private void ButtonLeftBracket_Click(object sender, EventArgs e)
        {
            addTerm("(");
        }

        private void ButtonRightBracket_Click(object sender, EventArgs e)
        {
            addTerm(")");
        }

        private void ButtonNegation_Click(object sender, EventArgs e)
        {
            addTerm("~");
        }
    }
}
