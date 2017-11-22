using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IService
{
    public partial interface IProductAdminService:IProductService
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="SavePath"></param>
        /// <param name="file"></param>
        /// <returns></returns>
         Tuple<bool, string> UploadImage(string SavePath, HttpPostedFileBase file);

         bool AddProduct(string name, double price, int state, int categroy, DateTime produceTime, int protectTime, int number, string imagePath,int mid);

         //bool UpdateProducts(long id, string name, double price, int state, int categroy, DateTime marketTime, DateTime produceTime, int protectTime, int number, string imagePath);
         bool RemoveMachine(long pid, long mid);
    }
}
