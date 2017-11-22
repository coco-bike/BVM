using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IService;
using Model;
using Service;

namespace Service
{
    public partial class SaleAdminService:SaleService,ISaleAdminService
    {

        public Tuple<bool, double> PayMoney(int number, int productID,double money)
        {
            return this.dbSession.SaleDal.PayMoney(number, productID, money);
        }


        public bool AddSale(double price, double change, int number, int pid, int mid)
        {
            Sale sale = null;
            var product = this.dbSession.ProductDal.GetList(t => t.ID == pid).FirstOrDefault();
            var machine = this.dbSession.MachineDal.GetList(t => t.ID == mid).FirstOrDefault();
            if (product != null && machine != null)
            {
                sale.Change = change;
                sale.Number = number;
                sale.Price = price;
                sale.Time = DateTime.Now;
                sale.ProductID = product.ID;
                sale.MachineID = machine.ID;

                sale.Machine = machine;
                sale.Product = product;

                this.dbSession.SaleDal.Add(sale);
                return true;
            }
            return false;
        }


        public bool UpdateSale(long id, double price, double change, int Number, int pid, int mid)
        {
            var sale = this.dbSession.SaleDal.GetList(t => t.ID == id).FirstOrDefault();
            var product = this.dbSession.ProductDal.GetList(t => t.ID == pid).FirstOrDefault();
            var machine = this.dbSession.MachineDal.GetList(t => t.ID == mid).FirstOrDefault();

            if(sale!=null&&product!=null&&machine!=null)
            {
                sale.Change = change;
                sale.Machine = machine;
                sale.MachineID = machine.ID;
                sale.Number = Number;
                sale.Price = price;
                sale.Product = product;
                sale.ProductID = product.ID;
                sale.Time = DateTime.Now;

                this.dbSession.SaleDal.Update(sale);
                return true;
            }
            return false;
        }
    }
}
