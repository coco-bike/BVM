using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IDal
{
    public partial interface IProductAdminDal:IProductDal
    {
        //bool AddProducts(string name, double price, int state, int categroy, DateTime marketTime, DateTime produceTime, int protectTime, int number, string imagePath);

        //bool UpdateProducts(long id, string name, double price, int state, int categroy, DateTime marketTime, DateTime produceTime, int protectTime, int number, string imagePath);
    }
}
