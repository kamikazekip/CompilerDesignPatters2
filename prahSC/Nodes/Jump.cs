using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prahSC.Nodes.Visitors;

namespace prahSC.Nodes
{
    public class Jump : Node
    {
        private Node jump { get; set; }

        public Jump()
            : base()
        {

        }

        public void setJump(Node node)
        {
            this.jump = node;
        }

        public Node getJumpToNode()
        {
            return this.jump;
        }

        public override void accept(NodeVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
