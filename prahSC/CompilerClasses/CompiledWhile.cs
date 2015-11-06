using prahSC.Nodes;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public class CompiledWhile : CompiledStatement 
    {
        private NodeLinkedList _compiledStatement;
        private NodeLinkedList _condition;
        private NodeLinkedList _body;
        private ConditionalJump _conditionalJump;
        private Jump _jump;

        public CompiledWhile()
            : base()
        {
            _compiledStatement = new NodeLinkedList();
            _condition = new NodeLinkedList();
            _body = new NodeLinkedList();
            _conditionalJump = new ConditionalJump();
            _jump = new Jump();
        }

        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            int whileLevel = currentToken.Value.getLevel();
            _condition.addLast(new DoNothing());

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(whileLevel, Token.Type.While), 
                new TokenExpectation(whileLevel, Token.Type.EllipsisOpen),
                new TokenExpectation(whileLevel + 1, Token.Type.Any), // Condition
                new TokenExpectation(whileLevel, Token.Type.EllipsisClose),
                new TokenExpectation(whileLevel, Token.Type.BracketsOpen), 
                new TokenExpectation(whileLevel + 1, Token.Type.Any), // Body
                new TokenExpectation(whileLevel, Token.Type.BracketsClose)
            };

            foreach (var expectation in expected)
            {
                if (expectation.Level == whileLevel)
                {
                    if (currentToken.Value.tokentype != expectation.TokenType)
                        throw new Exception(String.Format("Unexpected end of statement, expected {0}", expectation.TokenType));
                    else
                    {
                        Console.WriteLine(currentToken.Value.getValue());
                        if(currentToken.Next != null)
                            currentToken = currentToken.Next;
                    }
                }
                else if (expectation.Level >= whileLevel)
                {
                    if (_condition.getSize() < 2) // We komen eerst de conditie tegen, deze vullen we daarom eerst.
                    {
                        var compiledCondition = new CompiledCondition();
                        compiledCondition.Compile(ref currentToken);
                        _condition.addLast(compiledCondition.compiled);
                    }
                    else
                    {
                        while (currentToken.Value.getLevel() > whileLevel) // Zolang we in de body zitten mag de factory hiermee aan de slag. Dit is niet onze zaak.
                        {
                            if (currentToken.Value.tokentype != Token.Type.Semicolon){
                                var compiledBodyPart = CompilerFactory.getInstance().CreateCompiledStatement(currentToken);
                                compiledBodyPart.Compile(ref currentToken);
                                _body.addLast(compiledBodyPart.compiled);
                            } else 
                                currentToken = currentToken.Next;
                        }
                    }
                }
            }


            _compiledStatement.addLast(new DoNothing());
            _compiledStatement.addLast(_condition);
            _compiledStatement.addLast(_conditionalJump);
            _compiledStatement.addLast(_body);
            _compiledStatement.addLast(_jump);
            _compiledStatement.addLast(new DoNothing());

            _jump.setJump(_compiledStatement.getFirst());
            _conditionalJump.setOnTrue(_body.getFirst());
            _conditionalJump.setOnFalse(_compiledStatement.getLast());

            compiled.addLast(_compiledStatement);
            /*
              var conditionalJumpNode = new ConditionalJumpNode();
            var jumpBackNode = new JumpNode();

            _compiledStatement.Add(new DoNothingNode());
            _compiledStatement.Add(_condition);
            _compiledStatement.Add(conditionalJumpNode); // De body komt dus rechtstreeks na de conditionalJumpNode (dus op de .Next property)
            _compiledStatement.Add(_body);
            _compiledStatement.Add(jumpBackNode);
            _compiledStatement.Add(new DoNothingNode());


            jumpBackNode.JumpToNode = _compiledStatement.First; // JumpToNode is een extra property ten opzichte van andere nodes.
            conditionalJumpNode.NextOnTrue = _body.First; // NextOnTrue en NextOnFalse zijn extra properties ten opzichte van andere nodes.
            conditionalJumpNode.NextOnFalse = _compiledStatement.Last;
             */
        }

        public override CompiledStatement Clone()
        {
            return new CompiledWhile();
        }

        public override bool IsMatch(LinkedListNode<Token> currentToken)
        {
            Boolean firstPart = currentToken.Value.tokentype == Token.Type.While && currentToken.Next.Value.tokentype == Token.Type.EllipsisOpen;
            Boolean secondPart = false;
            Boolean thirdPart = false;
            if (!firstPart)
                return false;
            else
            {
                int currentLevel = currentToken.Value.getLevel();
                LinkedListNode<Token> conditionToken = currentToken.Next.Next;

                while (conditionToken.Value.getLevel() > currentLevel)
                    conditionToken = conditionToken.Next;
                secondPart = conditionToken.Value.tokentype == Token.Type.EllipsisClose;

                if(conditionToken.Next == null)
                    return false;

                LinkedListNode<Token> brackets = conditionToken.Next;
                int bracketsLevel = brackets.Value.getLevel();
                if (brackets.Next == null)
                    return false;

                brackets = brackets.Next;
                while (brackets.Value.getLevel() > bracketsLevel)
                    brackets = brackets.Next;
                thirdPart = brackets.Value.tokentype == Token.Type.BracketsClose;       
            }
            return secondPart && thirdPart;
        }
    }

    public struct TokenExpectation
    {
        public int Level;
        public Token.Type TokenType;

        public TokenExpectation(int level, Token.Type tokenType)
        {
            Level = level;
            TokenType = tokenType;
        }
    }
}
