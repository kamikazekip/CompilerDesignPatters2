using prahSC.CompilerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prahSC.TokenizerClasses
{
    
    public class Tokenizer
    {
        private int currentPosition { get; set; }
        private int currentLevel { get; set; }
        private String[] code { get; set; }
        private List<Token> tokens { get; set; }
        private LinkedList<Token> tokenLinkedList { get; set; }
        private PartnerStack partnerStack { get; set; }
        private CompilerListMaker compiler { get; set; }

        public Tokenizer(){
            this.currentPosition = 1;
            this.currentLevel = 1;
            this.code = System.IO.File.ReadAllLines(@"code.txt");
            this.tokens = new List<Token>();
            this.tokenLinkedList = new LinkedList<Token>();
            this.partnerStack = new PartnerStack();

            for (int x = 0; x < code.Length; x++)
                this.tokenize(code[x], x);

            for (int x = 0; x < this.tokens.Count; x++)
            {
                if (this.tokens[x].getValue().Equals("{") || this.tokens[x].getValue().Equals("(") || this.tokens[x].getValue().Equals("["))
                    this.partnerStack.push(x);
                if (this.tokens[x].getValue().Equals("}") || this.tokens[x].getValue().Equals(")") || this.tokens[x].getValue().Equals("]"))
                {
                    int partner = this.partnerStack.pop();
                    this.tokens[partner].setPartner(x);
                    this.tokens[x].setPartner(partner);
                }
            }

            if (this.currentLevel > 1)
                Console.WriteLine("There is a '{', '(' or '[' character unclosed!");
            else if(this.currentLevel < 1)
                Console.WriteLine("There is a '}', ')' or ']' character unopened!");
            else
            {
                Console.WriteLine("");
                Console.WriteLine("<------ Tokenizer ------>");
                Console.WriteLine("");
                for (int c = 0; c < this.tokens.Count; c++)
                {
                    Console.WriteLine("[ Token " + c + " ][ Value: " + this.tokens[c].getValue() + " ][ Type: " + this.tokens[c].tokentype.ToString() + " ][ level: " +
                       this.tokens[c].getLevel() + " ][ LinePosition: " + this.tokens[c].getLinePosition() + " ][ LineNumber: " + this.tokens[c].getLineNumber() + " ]");
                    this.tokenLinkedList.AddLast(this.tokens[c]);
                }
                compiler = new CompilerListMaker(this.tokenLinkedList);
            }
        }

        public void tokenize(String codeLine, int lineNumber)
        {
            Boolean hasSemicolon = false;
            int linePosition = 0;
            String[] codeWords = codeLine.Split(' ');
            foreach (String word in codeWords)
            {
                String newWord = word;
                if (!word.Equals(""))
                {
                    if (word.EndsWith(";"))
                    {
                        newWord = Regex.Replace(word, @";", "");
                        hasSemicolon = true;
                    }
                    linePosition++;
                    if (newWord.Equals("}") || newWord.Equals(")") || newWord.Equals("]"))
                        this.currentLevel--;

                    newWord = Regex.Replace(newWord, @"\t|\n|\r", "");
                    Token token = new Token(this.currentPosition, lineNumber, linePosition, newWord, this.currentLevel);
                    this.tokens.Add(token);

                    if(hasSemicolon){
                        Token semicolon = new Token(this.currentPosition, lineNumber, linePosition, ";", this.currentLevel);
                        this.tokens.Add(semicolon);
                    }

                    if (newWord.Equals("{") || newWord.Equals("(") || newWord.Equals("["))
                        this.currentLevel++;   
                }
            }
        }
    }
}
