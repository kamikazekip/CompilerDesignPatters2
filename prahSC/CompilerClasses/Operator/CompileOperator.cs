using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses.Operator
{
    public class CompileOperator : CompiledStatement
    {
        public CompileOperator()
            : base()
        {

        }

        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            throw new NotImplementedException();
        }

        public override CompiledStatement Clone()
        {
            throw new NotImplementedException();
        }

        public override bool IsMatch(System.Collections.Generic.LinkedListNode<prahSC.TokenizerClasses.Token> currentToken)
        {
            throw new NotImplementedException();
        }
    }
}
