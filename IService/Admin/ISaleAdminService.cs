using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService
{
    public partial interface ISaleAdminService:ISaleService
    {
        Tuple<bool, double> PayMoney(int number, int productID, double money);

        bool AddSale(double price, double change, int Number, int pid, int mid);
        bool UpdateSale(long id, double price, double change, int Number, int pid, int mid);
    }
}
