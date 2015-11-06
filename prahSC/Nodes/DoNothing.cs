using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prahSC.Nodes.Visitors;

namespace prahSC.Nodes
{
    public class DoNothing : Node 
    {
        public override void accept(NodeVisitor visitor)
        {
            visitor.visit(this);
        }
    }
}
