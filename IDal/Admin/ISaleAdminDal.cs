using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace IDal
{
    public partial interface ISaleAdminDal:ISaleDal
    {
        Tuple<bool, double> PayMoney(int number, int productID,double money);

        //bool AddSale(double price, double change, DateTime time, int number, Product product, Machine machine);

        //bool UpdateSale(long id, double price, double change, DateTime time, int number, Product product, Machine machine);
    }
}
