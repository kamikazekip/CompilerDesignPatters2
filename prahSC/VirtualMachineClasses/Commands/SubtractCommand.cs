using prahSC.CompilerClasses;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.VirtualMachineClasses.Commands
{
    public class SubtractCommand : BaseCommand
    {

        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            Variable variable1 = vm.getVariable(parameters[1]);
            Variable variable2 = vm.getVariable(parameters[2]);

            string newValue = (Int32.Parse(variable1.getValue()) - Int32.Parse(variable2.getValue())).ToString();
            vm.ReturnVariable.setValue(newValue);
        }
    }
}
