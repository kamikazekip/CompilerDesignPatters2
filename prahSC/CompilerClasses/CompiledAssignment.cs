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
    public class CompiledAssignment : CompiledStatement
    {
        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            this.compiled.addLast(new DoNothing());
            var variableName = currentToken.Value.getValue(); // x
            currentToken = currentToken.Next.Next; // verplaats 2 naar rechts (voorbij de = )

            // De factory bepaalt wat er verder gebeurt.
            CompiledStatement rightCompiled = CompilerFactory.getInstance().CreateCompiledStatement(currentToken);
            rightCompiled.Compile(ref currentToken);

            // Na de rightCompiled uitgevoerd te hebben hoeven we alleen maar de ReturnVariable op te halen en in een variable op te slaan.
            this.compiled.addLast(rightCompiled.compiled);
            this.compiled.addLast(new DirectFunctionCall("ReturnToVariable", variableName));
            this.compiled.addLast(new DoNothing());
        }

        public override CompiledStatement Clone()
        {
            return new CompiledAssignment();
        }

        public override bool IsMatch(LinkedListNode<Token> currentToken)
        {
            // We zijn een match als we een identifier met daarna een = tegenkomen.
            return currentToken.Value.tokentype == Token.Type.Identifier
                && currentToken.Next.Value.tokentype == Token.Type.Assign;
        }
    }
}