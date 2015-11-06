using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prahSC.TokenizerClasses
{
    public class Token
    {
        private int position { get; set; }
        private int lineNumber { get; set; }
        private int linePosition { get; set; }
        public enum Type { Identifier, Equals, Assign, Plus, Minus, Multiply, Divide, Number, Semicolon, While, If, EllipsisOpen, GreaterEquals, IsNot, GreaterThan,
            SmallerThan, SmallerEquals, EllipsisClose, BracketsOpen, BracketsClose, BlockOpen, BlockClose, PrintFunction, UnairyPlus, UnairyMinus, Any }
        public Type tokentype { get; set; }
        private String value { get; set; }
        private int level { get; set; }
        private int partner { get; set; }

        private static Dictionary<string, Type> types;
        private static void Init()
        {
            if (types == null)
            {
                types = new Dictionary<string, Type>();
                types["=="] = Type.Equals;
                types["="]  = Type.Assign;
                types["+"] = Type.Plus;
                types["-"] = Type.Minus;
                types["*"] = Type.Multiply;
                types["/"] = Type.Divide;
                types[";"] = Type.Semicolon;
                types["while"] = Type.While;
                types["if"] = Type.If;
                types["print"] = Type.PrintFunction;
                types["("] = Type.EllipsisOpen;
                types[">="] = Type.GreaterEquals;
                types["!="] = Type.IsNot;
                types["<="] = Type.SmallerEquals;
                types["++"] = Type.UnairyPlus;
                types["--"] = Type.UnairyMinus;
                types[">"] = Type.GreaterThan;
                types["<"] = Type.SmallerThan;
                types[")"] = Type.EllipsisClose;
                types["{"] = Type.BracketsOpen;
                types["}"] = Type.BracketsClose;
                types["["] = Type.BlockOpen;
                types["]"] = Type.BlockClose;
            }
        }

        private Type GetTokenType(string value)
        {
            int n;
            bool isNumeric = int.TryParse(value, out n);

            if(types.ContainsKey(value))
                return types[value];
            else if(isNumeric)
                return Type.Number;
            else
                return Type.Identifier;
        }

        public Token(int position, int lineNumber, int linePosition, String value, int level)
        {
            Init();
            this.position = position;
            this.lineNumber = lineNumber;
            this.linePosition = linePosition;
            this.value = value;
            this.level = level;
            this.tokentype = this.GetTokenType(value);
        }

        public void setPartner(int partner)
        {
            this.partner = partner;
        }

        public String getValue()
        {
            return this.value;
        }

        public int getLevel()
        {
            return this.level;
        }

        public int getLinePosition()
        {
            return this.linePosition;
        }

        public int getLineNumber()
        {
            return this.lineNumber;
        }
    }
}
