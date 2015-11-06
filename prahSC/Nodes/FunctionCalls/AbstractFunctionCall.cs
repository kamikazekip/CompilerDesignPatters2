using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prahSC.Nodes.Visitors;

namespace prahSC.Nodes.FunctionCalls
{
    public abstract class AbstractFunctionCall : Node
    {
        public String[] parameters;

        public AbstractFunctionCall()
            : base()
        {

        }
    }
}
