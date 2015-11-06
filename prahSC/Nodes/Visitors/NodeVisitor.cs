using prahSC.Nodes.FunctionCalls;
using prahSC.VirtualMachineClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.Nodes.Visitors
{
    public abstract class NodeVisitor
    {
        protected VirtualMachine vm { get; set; }

        public NodeVisitor(VirtualMachine vm)
        {
            this.vm = vm;
        }

        public abstract void visit(DoNothing node);

        public abstract void visit(Jump node);

        public abstract void visit(ConditionalJump node);

        public abstract void visit(DirectFunctionCall node);

        public abstract void visit(FunctionCall node);
    }
}
