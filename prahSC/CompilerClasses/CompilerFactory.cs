using prahSC.CompilerClasses.If;
using prahSC.CompilerClasses.Operator;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public class CompilerFactory
    {
        #region Singleton
        private static CompilerFactory instance;

        public static CompilerFactory getInstance()
        {
            if (instance == null)
                instance = new CompilerFactory();
            return instance;
        }
        #endregion

        private List<CompiledStatement> compilers;

        private CompilerFactory()
        {
            this.compilers = new List<CompiledStatement>();
            this.compilers.Add(new CompiledAssignment());
            this.compilers.Add(new CompiledPlus());
            this.compilers.Add(new CompiledMinus());
            this.compilers.Add(new CompiledWhile());
            this.compilers.Add(new CompiledCondition());
            this.compilers.Add(new CompiledConstant());
            this.compilers.Add(new CompiledPrint());
            this.compilers.Add(new CompiledUnairyOperator());
            this.compilers.Add(new CompiledIfGeneral());
        }

        public CompiledStatement CreateCompiledStatement(LinkedListNode<Token> currentToken)
        {
            foreach (CompiledStatement compiledStatement in this.compilers)
                if (compiledStatement.IsMatch(currentToken))
                    return compiledStatement.Clone();
            throw new Exception();
        }
    }
}