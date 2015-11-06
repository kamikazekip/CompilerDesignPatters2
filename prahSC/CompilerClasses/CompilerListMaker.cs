using prahSC.Nodes;
using prahSC.TokenizerClasses;
using prahSC.VirtualMachineClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public class CompilerListMaker
    {
        private LinkedList<Token> receivedTokens { get; set; }
        private NodeLinkedList nodeList { get; set; }
        private CompilerFactory cf { get; set; }
        private VirtualMachine vm { get; set; }

        public CompilerListMaker(LinkedList<Token> receivedTokens)
        {
            this.receivedTokens = receivedTokens;
            this.nodeList = new NodeLinkedList();
            this.cf = CompilerFactory.getInstance();
            this.compile();
            vm = new VirtualMachine();
            vm.run(nodeList);
        }

        public void compile()
        {
            
            LinkedListNode<Token> currentToken = this.receivedTokens.First;
            while (currentToken != null)
            {
                if (currentToken.Value.tokentype != Token.Type.Semicolon)
                {
                    CompiledStatement cs = cf.CreateCompiledStatement(currentToken);
                    cs.Compile(ref currentToken);
                    this.nodeList.addLast(cs.compiled);
                }
                currentToken = currentToken.Next;
            }
            Console.WriteLine("");
            Console.WriteLine("<------ Compiler ------>");
            Console.WriteLine("");

            Node currentNode = nodeList.getFirst();
            while (currentNode != null)
            {
                Console.WriteLine(currentNode.GetType().ToString());
                currentNode = currentNode.getNextNode();
            }

            Console.WriteLine("");
        }
    }
}
