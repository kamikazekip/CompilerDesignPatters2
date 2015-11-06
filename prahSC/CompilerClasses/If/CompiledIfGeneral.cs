using prahSC.Nodes;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses.If
{
    public class CompiledIfGeneral : CompiledStatement
    {
        private NodeLinkedList _compiledStatement;
        private NodeLinkedList _condition;
        private NodeLinkedList _body;
        private ConditionalJump _conditionalJump;

        public CompiledIfGeneral() 
            : base()
        {
            _compiledStatement = new NodeLinkedList();
            _condition = new NodeLinkedList();
            _body = new NodeLinkedList();
            _conditionalJump = new ConditionalJump();
        }

        public override void Compile(ref LinkedListNode<Token> currentToken)
        {
            int ifLevel = currentToken.Value.getLevel();
            _condition.addLast(new DoNothing());

            List<TokenExpectation> expected = new List<TokenExpectation>()
            {
                new TokenExpectation(ifLevel, Token.Type.If), 
                new TokenExpectation(ifLevel, Token.Type.EllipsisOpen),
                new TokenExpectation(ifLevel + 1, Token.Type.Any), // Condition
                new TokenExpectation(ifLevel, Token.Type.EllipsisClose),
                new TokenExpectation(ifLevel, Token.Type.BracketsOpen), 
                new TokenExpectation(ifLevel + 1, Token.Type.Any), // Body
                new TokenExpectation(ifLevel, Token.Type.BracketsClose)
            };

            foreach (var expectation in expected)
            {
                if (expectation.Level == ifLevel)
                {
                    if (currentToken.Value.tokentype != expectation.TokenType)
                        throw new Exception(String.Format("Unexpected end of statement, expected {0}", expectation.TokenType));
                    else
                    {
                        if (currentToken.Next != null)
                            currentToken = currentToken.Next;
                    }
                }
                else if (expectation.Level >= ifLevel)
                {
                    if (_condition.getSize() < 2) // We komen eerst de conditie tegen, deze vullen we daarom eerst.
                    {
                        var compiledCondition = new CompiledCondition();
                        compiledCondition.Compile(ref currentToken);
                        _condition.addLast(compiledCondition.compiled);
                    }
                    else
                    {
                        while (currentToken.Value.getLevel() > ifLevel) // Zolang we in de body zitten mag de factory hiermee aan de slag. Dit is niet onze zaak.
                        {
                            if (currentToken.Value.tokentype != Token.Type.Semicolon)
                            {
                                var compiledBodyPart = CompilerFactory.getInstance().CreateCompiledStatement(currentToken);
                                compiledBodyPart.Compile(ref currentToken);
                                _body.addLast(compiledBodyPart.compiled);
                            }
                            else
                                currentToken = currentToken.Next;
                        }
                    }
                }
            }

            _compiledStatement.addLast(new DoNothing());
            _compiledStatement.addLast(_condition);
            _compiledStatement.addLast(_conditionalJump);
            _compiledStatement.addLast(_body);
            _compiledStatement.addLast(new DoNothing());

            _conditionalJump.setOnTrue(_body.getFirst());
            _conditionalJump.setOnFalse(_compiledStatement.getLast());

            compiled.addLast(_compiledStatement);
        }

        public override CompiledStatement Clone()
        {
            return new CompiledIfGeneral();
        }

        public override bool IsMatch(LinkedListNode<Token> currentToken)
        {
            Boolean firstPart = currentToken.Value.tokentype == Token.Type.If && currentToken.Next.Value.tokentype == Token.Type.EllipsisOpen;
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

                if (conditionToken.Next == null)
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
}
