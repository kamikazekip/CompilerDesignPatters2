using prahSC.Nodes.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.Nodes
{
    public class ConditionalJump : Node
    {
        private Node onTrue { get; set; }
        private Node onFalse { get; set; }

        public ConditionalJump()
            : base()
        {

        }

        public void setOnTrue(Node onTrue)
        {
            this.onTrue = onTrue;
        }

        public void setOnFalse(Node onFalse)
        {
            this.onFalse = onFalse;
        }

        public Node getOnTrue()
        {
            return onTrue;
        }

        public Node getOnFalse()
        {
            return onFalse;
        }

        public override void accept(NodeVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
