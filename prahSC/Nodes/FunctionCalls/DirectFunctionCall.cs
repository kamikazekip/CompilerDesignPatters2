using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prahSC.Nodes.Visitors;
using prahSC.CompilerClasses;
using prahSC.TokenizerClasses;

namespace prahSC.Nodes.FunctionCalls
{
    public class DirectFunctionCall : AbstractFunctionCall
    {
        private String functionType { get; set; }
        private String value { get; set; }
        public Variable variable { get; set; }

        public DirectFunctionCall(String functionType, Token token)
            : base()
        {
            parameters = new String[2]{ functionType, token.getValue() };
            this.variable = new Variable(token);
            this.functionType = functionType;
            this.value = token.getValue();
        }

        public DirectFunctionCall(String functionType, String value)
        {
            parameters = new String[2] { functionType, value };
            this.variable = new Variable(value);
            this.functionType = functionType;
            this.value = value;
        }

        public override void accept(NodeVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
