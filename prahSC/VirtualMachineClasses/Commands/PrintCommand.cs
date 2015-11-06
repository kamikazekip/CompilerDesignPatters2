using prahSC.CompilerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.VirtualMachineClasses.Commands
{
    public class PrintCommand : BaseCommand
    {
        public override void execute(VirtualMachine vm, IList<string> parameters)
        {
            int number;
            bool isNumeric = int.TryParse(parameters[1], out number);
            if (isNumeric)
                Console.WriteLine(number);
            else
            {
                string toPrint = vm.getVariable(parameters[1]).getValue();
                Console.WriteLine(toPrint);
            }
            
        }
    }
}