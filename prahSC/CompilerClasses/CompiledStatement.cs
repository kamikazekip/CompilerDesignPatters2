using prahSC.Nodes;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public abstract class CompiledStatement
    {
        private static int uniqueIdentifier = 0;
        public NodeLinkedList compiled;

        public CompiledStatement()
        {
            compiled = new NodeLinkedList();
        }

        public abstract void Compile(ref LinkedListNode<Token> currentToken);

        public abstract Boolean IsMatch(LinkedListNode<Token> currentToken);

        public abstract CompiledStatement Clone();

        public string getNextUniqueId()
        {
            uniqueIdentifier++;
            if (uniqueIdentifier < 10)
                return "$00" + uniqueIdentifier;
            if (uniqueIdentifier < 100)
                return "$0" + uniqueIdentifier;
            else
                return "$" + uniqueIdentifier;
        }
    }
}
