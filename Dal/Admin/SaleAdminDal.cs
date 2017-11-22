using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDal;
using Model;

namespace Dal
{
    public class SaleAdminDal:SaleDal,ISaleAdminDal
    {
        /// <summary>
        /// 判断付款成功失败并找零
        /// </summary>
        /// <param name="number"></param>
        /// <param name="productID"></param>
        /// <param name="money"></param>
        /// <returns></returns>
        public Tuple<bool, double> PayMoney(int number, int productID,double money)
        {
            double change;
            var product = this.entity.Set<Product>().Where(t => t.ID == productID).FirstOrDefault();
            if(product!=null)
            {
                if(product.Number>=number)
                {
                    change = money - product.Number * product.Price;
                    if(change>=0)
                    {
                        return new Tuple<bool, double>(true, change);
                    }
                    else
                    {
                        return new Tuple<bool, double>(false, money);
                    }
                }
                else
                {
                    return new Tuple<bool, double>(false, money);
                }
            }
            else
            {
                return new Tuple<bool, double>(false, money);
            }
        }
        ///// <summary>
        ///// 添加记录
        ///// </summary>
        ///// <param name="price"></param>
        ///// <param name="change"></param>
        ///// <param name="time"></param>
        ///// <param name="number"></param>
        ///// <param name="product"></param>
        ///// <param name="machine"></param>
        ///// <returns></returns>
        //public bool AddSale(double price, double change, DateTime time, int number, Product product, Machine machine)
        //{
        //    Sale sale = this.entity.Set<Sale>().Add(new Sale(){
        //      Price=price,Change=change,Machine=machine,MachineID=machine.ID,
        //      Number=number,Product=product,ProductID=product.ID,Time=time});
        //    if(this.entity.SaveChanges()>0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 修改记录
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="price"></param>
        ///// <param name="change"></param>
        ///// <param name="time"></param>
        ///// <param name="number"></param>
        ///// <param name="product"></param>
        ///// <param name="machine"></param>
        ///// <returns></returns>
        //public bool UpdateSale(long id, double price, double change, DateTime time, int number, Product product, Machine machine)
        //{
        //    var sale = this.entity.Set<Sale>().Where(t => t.ID == id).FirstOrDefault();

        //    sale.Change = change;
        //    sale.Machine = machine;
        //    sale.MachineID = machine.ID;
        //    sale.Number = number;
        //    sale.Price = price;
        //    sale.Product = product;
        //    sale.ProductID = product.ID;
        //    sale.Time=time;

        //    this.entity.Entry<Sale>(sale).State = System.Data.Entity.EntityState.Modified;
        //    if(this.entity.SaveChanges()>0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
