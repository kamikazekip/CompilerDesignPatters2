using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prahSC.VirtualMachineClasses.Commands
{
    public abstract class BaseCommand
    {
        public abstract void execute(VirtualMachine vm, IList<string> parameters);
    }
}