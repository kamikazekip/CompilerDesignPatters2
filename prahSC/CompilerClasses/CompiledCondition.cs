using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prahSC.Nodes.FunctionCalls;
using prahSC.Nodes;

namespace prahSC.CompilerClasses
{
    public class CompiledCondition : CompiledStatement
    {
        public override CompiledStatement Clone()
        {
            return new CompiledCondition();
        }

        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            compiled.addLast(new DoNothing());

            Token leftToken = currentToken.Value;
            string leftName = leftToken.getValue();
            currentToken = currentToken.Next;

            Token operatorToken = currentToken.Value;
            currentToken = currentToken.Next;

            Token rightToken = currentToken.Value;
            currentToken = currentToken.Next;
            string rightName = rightToken.getValue();

            if (leftToken.tokentype != Token.Type.Identifier)
            {
                leftName = base.getNextUniqueId();
                this.compiled.addLast(new DirectFunctionCall("ConstantToReturn", leftToken));
                this.compiled.addLast(new DirectFunctionCall("ReturnToVariable", leftName));
            }
            if (rightToken.tokentype != Token.Type.Identifier)
            {
                rightName = base.getNextUniqueId();
                this.compiled.addLast(new DirectFunctionCall("ConstantToReturn", rightToken));
                this.compiled.addLast(new DirectFunctionCall("ReturnToVariable", rightName));
            }

            switch (operatorToken.tokentype)
            {
                case (Token.Type.Equals): 
                    this.compiled.addLast(new FunctionCall("AreEqual", leftName, rightName));
                    break;
                case(Token.Type.GreaterEquals):
                    this.compiled.addLast(new FunctionCall("GreaterThan", leftName, rightName));
                    break;
                case(Token.Type.IsNot):
                    this.compiled.addLast(new FunctionCall("IsNot", leftName, rightName));
                    break;
                default:
                    break;
            }
            this.compiled.addLast(new DoNothing());
        }

        public override bool IsMatch(LinkedListNode<Token> currentToken)
        {
            return currentToken.Next.Value.tokentype == Token.Type.Equals
                || currentToken.Next.Value.tokentype == Token.Type.GreaterEquals
                || currentToken.Next.Value.tokentype == Token.Type.IsNot;
        }
    }    
}
