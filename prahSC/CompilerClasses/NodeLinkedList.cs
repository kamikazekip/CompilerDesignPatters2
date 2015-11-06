using prahSC.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public class NodeLinkedList
    {
        private Node head;

        public NodeLinkedList() { this.head = null; }

        public void addFirst(Node node)
        {
            if (this.head == null) { this.head = node; }
            else
            {
                node.setNextNode(head);
                head = node;
            }
        }

        public Node getFirst() { return this.head; }

        public void addLast(Node node)
        {
            if (head == null) { this.addFirst(node); }
            else
            {
                Node lastNode = this.getLast();

                lastNode.setNextNode(node);
            }
        }

        public void addLast(NodeLinkedList nodeList)
        {
            Node currentNode = nodeList.getFirst();
            this.addLast(currentNode);
        }

        public Node getLast()
        {
            Node node = head;

            while (node.hasNextNode()) { node = node.getNextNode(); }

            return node;
        }

        public Node get(int index)
        {
            Node node = head;
            int count = 0;

            while (node != null && node.hasNextNode() && count++ < index) { node = node.getNextNode(); }

            if (count < index) { return null; }
            else { return node; }
        }

        public int getSize()
        {
            Node node = head;
            int count = 0;
            while (node != null)
            {
                count++;
                node = node.getNextNode();
            }
            return count;
        }

        public void insertBefore(Node keyNode, Node insertNode)
        {
            if (keyNode == null) { addLast(insertNode); }
            else if (this.head.Equals(keyNode)) { addFirst(insertNode); }
            else
            {
                Node previous = null;
                Node current = head;

                while (current != null && !current.Equals(keyNode))
                {
                    previous = current;
                    current = current.getNextNode();
                }

                if (current != null)
                {
                    if (previous != null) { previous.setNextNode(insertNode); }

                    while (insertNode.hasNextNode()) { insertNode = insertNode.getNextNode(); }

                    insertNode.setNextNode(current);
                }
            }
        }
    }
}
