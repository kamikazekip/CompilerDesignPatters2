using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public class Variable
    {
        public Token.Type tokenType;
        private string value;

        public Variable()
        {

        }

        public Variable(Token t)
        {
            tokenType = t.tokentype;
            value = t.getValue();
        }

        public Variable(string value)
        {
            this.tokenType = Token.Type.Identifier;
            this.value = value;
        }

        public void setValue(string value)
        {
            this.value = value;
        }

        public string getValue()
        {
            return this.value;
        }

        public Variable clone()
        {
            Variable newVar = new Variable();
            newVar.value = value;
            newVar.tokenType = tokenType;
            return newVar;
        }
    }
}
