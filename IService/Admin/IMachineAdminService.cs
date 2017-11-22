using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public partial interface IMachineAdminService:IMachineService
    {
        bool AddMachine(string code,DateTime maintainTime,string address,long pid);
    }
}
