using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.TokenizerClasses
{
    public class PartnerStack
    {
        private List<int> stackArray;
        private int top;

        public PartnerStack()
        {
            stackArray = new List<int>();
            top = -1;
        }

        public void push(int j)
        {
            stackArray.Add(j);
            top++;
        }

        public int pop()
        {
            if(!this.isEmpty())
            {
                int x = stackArray[top];
                stackArray.RemoveAt(top);
                top--;
                return x;
            }
            return 0;
        }

        public int peek()
        {
            return stackArray[top];
        }

        public Boolean isEmpty()
        {
            return (top == -1);
        }
    }
}
