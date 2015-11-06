using prahSC.CompilerClasses;
using prahSC.Nodes;
using prahSC.Nodes.FunctionCalls;
using prahSC.Nodes.Visitors;
using prahSC.VirtualMachineClasses.Commands;
using prahSC.VirtualMachineClasses.Commands.ConditionCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.VirtualMachineClasses
{
    public class VirtualMachine
    {
        private Dictionary<string, BaseCommand> _commands;
        public Variable ReturnVariable;
        public Dictionary<string, Variable> Variables;

        public void run(NodeLinkedList list)
        {
            Console.WriteLine("<------ Virtual Machine ------>");
            Console.WriteLine("");
            _commands = new Dictionary<string, BaseCommand>();
            Variables = new Dictionary<string, Variable>();
            ReturnVariable = new Variable();

            initialiseCommands();

            Node currentNode = list.getFirst();
            NextNodeVisitor visitor = new NextNodeVisitor(this);

            while (currentNode != null)
            {
                AbstractFunctionCall actionNode = currentNode as AbstractFunctionCall;
                
                if (actionNode != null)
                {
                    string name = actionNode.parameters[0];
                    _commands[name].execute(this, actionNode.parameters);
                }
                
                // Bepaal de volgende node:
                currentNode.accept(visitor);
                currentNode = visitor.nextNode;
            }
            Console.WriteLine("");
            Console.WriteLine("<------ Einde programma ------>");
            Console.WriteLine("");
        }

        public void setVariable(string key, Variable variable)
        {
            if (!Variables.ContainsKey(key))
                Variables.Add(key, variable);
            else
                Variables[key] = variable;
        }

        public Variable getVariable(string key)
        {
            if (Variables.ContainsKey(key))
                return Variables[key];
            return null;
        }

        private void initialiseCommands()
        {
            _commands.Add("ConstantToReturn",   new ConstantToReturnCommand());
            _commands.Add("ReturnToVariable",   new ReturnToVariableCommand());
            _commands.Add("Print",              new PrintCommand());
            _commands.Add("Add",                new AddCommand());
            _commands.Add("Subtract",           new SubtractCommand());
            _commands.Add("IsNot",              new IsNotCommand());
            _commands.Add("Equals",             new EqualsCommand());
            _commands.Add("GreaterThan",        new GreaterThanCommand());
            _commands.Add("SmallerThan",        new SmallerThanCommand());
            _commands.Add("SmallerEquals",      new SmallerEqualsCommand());
            _commands.Add("GreaterEquals",      new GreaterEqualsCommand());
        }
    }
}