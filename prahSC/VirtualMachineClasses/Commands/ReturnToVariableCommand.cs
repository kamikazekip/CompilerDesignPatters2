using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.VirtualMachineClasses.Commands
{
    public class ReturnToVariableCommand : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            vm.setVariable( parameters[1], vm.ReturnVariable.clone() );
        }
    }
}
