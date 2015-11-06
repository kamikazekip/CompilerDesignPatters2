using prahSC.Nodes.FunctionCalls;
using prahSC.VirtualMachineClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.Nodes.Visitors
{
    public class NextNodeVisitor : NodeVisitor
    {
        public Node nextNode { get; private set; }

        public NextNodeVisitor(VirtualMachine vm)
            : base(vm)
        {

        }

        public override void visit(DoNothing node)
        {
            nextNode = node.getNextNode();
        }

        public override void visit(Jump node)
        {
            nextNode = node.getJumpToNode();
        }

        public override void visit(ConditionalJump node)
        {
            if (Convert.ToBoolean(vm.ReturnVariable.getValue()))
                nextNode = node.getOnTrue();
            else
                nextNode = node.getOnFalse();
        }

        public override void visit(DirectFunctionCall node)
        {
            nextNode = node.getNextNode();
        }

        public override void visit(FunctionCall node)
        {
            nextNode = node.getNextNode();
        }
    }
}
