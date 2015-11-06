using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prahSC.Nodes.Visitors;

namespace prahSC.Nodes.FunctionCalls
{
    public class FunctionCall : AbstractFunctionCall
    {
        private String functionType;
        private String l_value;
        private String r_value;

        public FunctionCall(String functionType, String l_value, String r_value)
            : base()
        {
            this.functionType = functionType;
            this.l_value = l_value;
            this.r_value = r_value;
            parameters = new String[3] { functionType, this.l_value, this.r_value };
        }

        public FunctionCall(String functionType, String l_value)
            : base()
        {
            this.functionType = functionType;
            this.l_value = l_value;
            parameters = new String[2] { functionType, this.l_value };
        }

        public override void accept(NodeVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
