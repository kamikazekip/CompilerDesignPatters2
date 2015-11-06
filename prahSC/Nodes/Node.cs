using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prahSC.Nodes.Visitors;

namespace prahSC.Nodes
{
    public abstract class Node
    {
        protected Node next { get; set; }
        protected Node previous { get; set; }

        public Node()
        {

        }

        public void setNextNode(Node next)
        {
            this.next = next;
        }

        public Node getNextNode()
        {
            return this.next;
        }

        public void setPreviousNode(Node previous)
        {
            this.previous = previous;
        }

        public Node getPreviousNode()
        {
            return this.previous;
        }

        public Boolean hasNextNode()
        {
            return this.next != null;
        }

        public abstract void accept(NodeVisitor visitor);
    }
}
