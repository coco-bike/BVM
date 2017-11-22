using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Model;

namespace Service
{
    public partial class MachineAdminService : MachineService, IMachineAdminService
    {

        public bool AddMachine(long mid, long pid)
        {
            var machine = this.dbSession.MachineDal.GetList(s => s.ID == mid).FirstOrDefault();
            var product = this.dbSession.ProductDal.GetList(s => s.ID == pid).FirstOrDefault();
            if (machine != null && product != null)
            {
                machine.Products.Add(product);
                var res = this.dbSession.MachineDal.Update(machine);
                if (res > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool AddMachine(string code, DateTime maintainTime, string address, long pid)
        {
            Machine machine = null;

            machine.Address = address;
            machine.Code = code;
            machine.MaintainTime = maintainTime;
            machine.MarketTime = DateTime.Now;

            var product = this.dbSession.ProductDal.GetList(s => s.ID == pid).FirstOrDefault();
            if (product!= null)
            {
                machine.Products.Add(product);
                this.dbSession.MachineDal.Add(machine);
                return true;
            }
            return false;          
        }
    }
}
