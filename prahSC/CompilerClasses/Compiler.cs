using prahSC.Nodes;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public abstract class Compiler
    {

        public Compiler()
        {

        }

        public virtual void compile()
        {

        }

        public virtual Token getLastToken()
        {
            return null;
        }   
    }
}
