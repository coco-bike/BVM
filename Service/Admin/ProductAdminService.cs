using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IService;
using Model;

namespace Service
{
    public partial class ProductAdminService : ProductService, IProductAdminService
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="SavePath"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public Tuple<bool, string> UploadImage(string SavePath, HttpPostedFileBase file)
        {
            try
            {
                string path = null;
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMdd") + "-" + Path.GetFileName(file.FileName);
                    string filepath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(SavePath), fileName);
                    file.SaveAs(filepath);
                    path = filepath;
                }
                return new Tuple<bool, string>(true, path);
            }
            catch (Exception)
            {
                return new Tuple<bool, string>(false, null);
            }
        }
        public bool AddProduct(string name, double price, int state, int categroy, DateTime produceTime, int protectTime, int number, string imagePath, int mid)
        {
            Product product = null;

            product.Category = categroy;
            product.ImagePath = imagePath;
            product.MarketTime = DateTime.Now;
            product.Name = name;
            product.Number = number;
            product.Price = price;
            product.ProduceTime = produceTime;
            product.ProtectTime = protectTime;
            product.Status = state;

            var machine = this.dbSession.MachineDal.GetList(t => t.ID == mid).FirstOrDefault();
            if (machine != null)
            {
                product.Machines.Add(machine);
                this.dbSession.ProductDal.Add(product);
                return true;
            }
            return false;
        }
        public bool RemoveMachine(long pid, long mid)
        {
            var machine = this.dbSession.MachineDal.GetList(s => s.ID == mid).FirstOrDefault();
            var product = this.dbSession.ProductDal.GetList(s => s.ID == pid).FirstOrDefault();
            if (machine != null & product != null)
            {
                product.Machines.Remove(machine);
                this.dbSession.ProductDal.Update(product);
                return true;
            }
            return false;
        }
    }
}
