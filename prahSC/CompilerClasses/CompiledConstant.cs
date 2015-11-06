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
    public class CompiledConstant : CompiledStatement
    {
        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            this.compiled.addLast(new DoNothing());
            this.compiled.addLast(new DirectFunctionCall("ConstantToReturn", currentToken.Value));
            this.compiled.addLast(new DirectFunctionCall("ReturnToVariable", getNextUniqueId()));
            this.compiled.addLast(new DoNothing());
            currentToken = currentToken.Next;
        }

        public override CompiledStatement Clone()
        {
            return new CompiledConstant();
        }

        public override bool IsMatch(System.Collections.Generic.LinkedListNode<prahSC.TokenizerClasses.Token> currentToken)
        {
            return currentToken.Value.tokentype == Token.Type.Number
                && currentToken.Next != null && ( currentToken.Next.Value.tokentype == Token.Type.Semicolon
                || currentToken.Next.Value.tokentype == Token.Type.EllipsisClose );
        }
    }
}
