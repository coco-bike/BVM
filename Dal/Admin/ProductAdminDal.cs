using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDal;
using System.Web;
using Model;

namespace Dal
{
    public class ProductAdminDal:ProductDal,IProductAdminDal
    {
        ///// <summary>
        ///// 添加商品
        ///// </summary>
        ///// <param name="name"></param>
        ///// <param name="price"></param>
        ///// <param name="state"></param>
        ///// <param name="categroy"></param>
        ///// <param name="marketTime"></param>
        ///// <param name="produceTime"></param>
        ///// <param name="protectTime"></param>
        ///// <param name="number"></param>
        ///// <param name="imagePath"></param>
        ///// <returns></returns>
        //public bool AddProducts(string name, double price, int state, int categroy, DateTime marketTime, DateTime produceTime, int protectTime, int number, string imagePath)
        //{
        //    Product product = this.entity.Set<Product>().Add(new Product(){Category=categroy,ImagePath=imagePath,MarketTime=marketTime,Name=name,Number=number,Price=price,ProduceTime=produceTime,ProtectTime=protectTime,State=state});

        //    if(this.entity.SaveChanges()>0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 更新商品
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="name"></param>
        ///// <param name="price"></param>
        ///// <param name="state"></param>
        ///// <param name="categroy"></param>
        ///// <param name="marketTime"></param>
        ///// <param name="produceTime"></param>
        ///// <param name="protectTime"></param>
        ///// <param name="number"></param>
        ///// <param name="imagePath"></param>
        ///// <returns></returns>
        //public bool UpdateProducts(long id,string name, double price, int state, int categroy, DateTime marketTime, DateTime produceTime, int protectTime, int number, string imagePath)
        //{
        //    var product = this.entity.Set<Product>().Where(t => t.ID == id).FirstOrDefault();

        //    product.Category = categroy;
        //    product.ImagePath = imagePath;
        //    product.MarketTime = marketTime;
        //    product.Name = name;
        //    product.Number = number;
        //    product.Price = price;
        //    product.ProduceTime = produceTime;
        //    product.ProtectTime = protectTime;
        //    product.State = state;

        //    this.entity.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;

        //    if(this.entity.SaveChanges()>0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
