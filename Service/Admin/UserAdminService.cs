using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;

namespace Service
{
    public partial class UserAdminService:UserService,IUserAdminService
    {
        public bool AddMachine(int uid, int pid)
        {
            var machine = this.dbSession.MachineDal.GetList(t => t.ID == uid).FirstOrDefault();
            var product = this.dbSession.ProductDal.GetList(t => t.ID == pid).FirstOrDefault();
            if (machine != null && product != null)
            {
                product.Machines.Add(machine);
                var res = this.dbSession.ProductDal.Update(product);
                if (res > 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
