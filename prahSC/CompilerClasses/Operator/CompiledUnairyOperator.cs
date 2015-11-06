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
    public class CompiledUnairyOperator : CompiledStatement
    {
        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            string leftVariable = "";
            string rightVariable = "";
            compiled.addLast(new DoNothing());

            Token leftToken = currentToken.Value;
            leftVariable = leftToken.getValue();

            currentToken = currentToken.Next; // verplaats 1 naar rechts ( dus de '--' of '++' )
            Token rightToken = currentToken.Value;
            rightVariable = getNextUniqueId();
            compiled.addLast(new DirectFunctionCall("ConstantToReturn", "1"));
            compiled.addLast(new DirectFunctionCall("ReturnToVariable", rightVariable));

            if(currentToken.Value.tokentype == Token.Type.UnairyMinus)
                compiled.addLast(new FunctionCall("Subtract", leftVariable, rightVariable));
            else
                compiled.addLast(new FunctionCall("Add", leftVariable, rightVariable));

            this.compiled.addLast(new DirectFunctionCall("ReturnToVariable", leftVariable));
            this.compiled.addLast(new DoNothing());
            currentToken = currentToken.Next;
        }

        public override CompiledStatement Clone()
        {
            return new CompiledUnairyOperator();
        }

        public override bool IsMatch(System.Collections.Generic.LinkedListNode<prahSC.TokenizerClasses.Token> currentToken)
        {
            return currentToken.Value.tokentype == Token.Type.Identifier
                && currentToken.Next != null &&
                (currentToken.Next.Value.tokentype == Token.Type.UnairyMinus || currentToken.Next.Value.tokentype == Token.Type.UnairyPlus);
        }
    }
}
