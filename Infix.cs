using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Calculator
{

    class Infix
    {
        private Queue<string> terms;

        public Infix()
        {
            this.terms = new Queue<string>();
        }

        public void push(string term)
        {
            this.terms.Enqueue(term);
        }

        public string pop()
        {
            return this.terms.Dequeue();
        }

        public void empty()
        {
            this.terms.Clear();
        }

        public bool isEmpty()
        {
            Debug.WriteLine(this.terms.Count);
            if (this.terms.Count > 0)
            {
                return false;
            }
            return true;
        }

        public void printDebug()
        {
            foreach (string term in this.terms.ToArray()) {
                Debug.WriteLine(term);
            }
        }
    }
}
