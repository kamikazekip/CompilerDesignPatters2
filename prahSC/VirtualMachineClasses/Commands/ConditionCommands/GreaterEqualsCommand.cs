using prahSC.CompilerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.VirtualMachineClasses.Commands.ConditionCommands
{
    public class GreaterEqualsCommand : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            Variable variable1 = vm.getVariable(parameters[1]);
            Variable variable2 = vm.getVariable(parameters[2]);

            int leftHand = Int32.Parse(variable1.getValue());
            int rightHand = Int32.Parse(variable2.getValue());

            Variable newReturnVariable = vm.ReturnVariable.clone();
            newReturnVariable.setValue((leftHand >= rightHand).ToString());
            vm.ReturnVariable = newReturnVariable;
        }
    }
}
