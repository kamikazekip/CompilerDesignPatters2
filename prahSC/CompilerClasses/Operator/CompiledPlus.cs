using prahSC.Nodes;
using prahSC.Nodes.FunctionCalls;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses.Operator
{
    public class CompiledPlus : CompiledStatement
    {
        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            string leftVariable = "";
            string rightVariable = "";
            compiled.addLast(new DoNothing());

            Token leftToken = currentToken.Value;
            if (leftToken.tokentype == Token.Type.Number)
            {
                leftVariable = getNextUniqueId();
                compiled.addLast(new DirectFunctionCall("ConstantToReturn", leftToken));
                compiled.addLast(new DirectFunctionCall("ReturnToVariable", leftVariable));
            }
            else
                leftVariable = leftToken.getValue();

            currentToken = currentToken.Next.Next; // verplaats 2 naar rechts (voorbij de + )
            Token rightToken = currentToken.Value;
            if (rightToken.tokentype == Token.Type.Number)
            {
                rightVariable = getNextUniqueId();
                compiled.addLast(new DirectFunctionCall("ConstantToReturn", rightToken));
                compiled.addLast(new DirectFunctionCall("ReturnToVariable", rightVariable));
            }
            else
                rightVariable = rightToken.getValue();

            compiled.addLast(new FunctionCall("Add", leftVariable, rightVariable));
            compiled.addLast(new DoNothing());
        }

        public override CompiledStatement Clone()
        {
            return new CompiledPlus();
        }

        public override bool IsMatch(System.Collections.Generic.LinkedListNode<prahSC.TokenizerClasses.Token> currentToken)
        {
            return (currentToken.Value.tokentype == Token.Type.Identifier || currentToken.Value.tokentype == Token.Type.Number)
                && currentToken.Next.Value.tokentype == Token.Type.Plus
                && (currentToken.Next.Next.Value.tokentype == Token.Type.Identifier || currentToken.Next.Next.Value.tokentype == Token.Type.Number);
        }
    }
}
