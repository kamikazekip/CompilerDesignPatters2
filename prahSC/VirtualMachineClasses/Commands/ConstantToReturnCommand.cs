using prahSC.CompilerClasses;
using prahSC.TokenizerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.VirtualMachineClasses.Commands
{
    public class ConstantToReturnCommand : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            vm.ReturnVariable.setValue(parameters[1]);
            vm.ReturnVariable.tokenType = Token.Type.Number;
        }
    }
}