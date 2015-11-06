using prahSC.Nodes;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.CompilerClasses
{
    public class CompileWhile : Compiler
    {
        private LinkedList<Node> compiledStatement;
        private LinkedList<Node> condition;
        private LinkedList<Node> body;

        public CompileWhile()
            : base()
        {
            this.compiledStatement = new LinkedList<Node>();
            this.condition = new LinkedList<Node>();
            this.body = new LinkedList<Node>();

            ConditionalJump conditionalJumpNode = new ConditionalJump();
            Jump jumpBackNode = new Jump();

            compiledStatement.AddLast(new DoNothing());
            combineLinkedLists(this.compiledStatement, this.condition);
            compiledStatement.AddLast(conditionalJumpNode);
            combineLinkedLists(this.compiledStatement, this.body);
            compiledStatement.AddLast(jumpBackNode);
            compiledStatement.AddLast(new DoNothing());


            /*
            jumpBackNode.setJump(this.compiledStatement.First.Value);
            conditionalJumpNode.setOnTrue(this.body.First.Value);
            conditionalJumpNode.setOnFalse(this.compiledStatement.Last.Value); */
        }

        public override void compile(){

        }

        public override Token getLastToken()
        {
            return null;
        }

        public void combineLinkedLists(LinkedList<Node> first, LinkedList<Node> after)
        {
            foreach (Node node in after)
                first.AddLast(node);
        }
    }
}
