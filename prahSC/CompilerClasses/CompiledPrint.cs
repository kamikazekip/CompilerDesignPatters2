using prahSC.Nodes;
using prahSC.Nodes.FunctionCalls;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public class CompiledPrint : CompiledStatement 
    {
        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            this.compiled.addLast(new DoNothing());
            this.compiled.addLast(new FunctionCall("Print", currentToken.Next.Next.Value.getValue()));
            this.compiled.addLast(new DoNothing());
            currentToken = currentToken.Next.Next.Next.Next;
        }

        public override CompiledStatement Clone()
        {
            return new CompiledPrint();
        }

        public override bool IsMatch(System.Collections.Generic.LinkedListNode<prahSC.TokenizerClasses.Token> currentToken)
        {
            /* expect: "print ( <number or identifier> )" */
            return currentToken.Value.tokentype == Token.Type.PrintFunction
                && currentToken.Next != null && currentToken.Next.Value.tokentype == Token.Type.EllipsisOpen
                && currentToken.Next.Next != null
                && (currentToken.Next.Next.Value.tokentype == Token.Type.Number || currentToken.Next.Next.Value.tokentype == Token.Type.Identifier)
                && currentToken.Next.Next.Next != null && currentToken.Next.Next.Next.Value.tokentype == Token.Type.EllipsisClose;
        }
    }
}
