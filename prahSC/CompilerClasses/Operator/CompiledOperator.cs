using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses.Operator
{
    public class CompiledOperator : CompiledStatement
    {
        public CompiledOperator()
            : base()
        {

        }

        public override CompiledStatement Clone()
        {
            return new CompiledOperator();
        }

        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            
        }

        public override bool IsMatch(LinkedListNode<Token> currentToken)
        {
            return false;
        }
    }
}
