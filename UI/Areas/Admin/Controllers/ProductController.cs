using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IService;
using UI.Controllers.Base;
using UI.Models;

namespace UI.Areas.Admin.Controllers
{
    public class ProductController : JsonController
    {
        //
        // GET: /Admin/Product/

        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region
        readonly IProductAdminService _productService;

        public ProductController(IProductAdminService productService)
        {
            this._productService = productService;
        }
        #endregion

        #region 分页查询
        public JsonBackResult GetPagingList(int pageIndex, int pageSize, string name = "")
        {
            int totalCount;
            IEnumerable<object> productList = null;
            productList = this._productService.GetPagingList(pageIndex, pageSize, out totalCount, true, t => t.Name.Contains(name), t => t.ID).Select(t => new { t.ID, t.Category, t.ImagePath, t.MarketTime, t.Name, t.Number, t.Price, t.ProduceTime, t.ProtectTime, State = t.Status }).ToList();

            return JsonBackResult(true, new PagingResult<object>(productList, totalCount));
        }
        #endregion

        #region 增加
        public JsonBackResult AddProduct(string name, double price, int state, int categroy, DateTime marketTime, DateTime produceTime, int protectTime, int number, string imagePath, int mid)
        {
            var res = this._productService.AddProduct(name, price, state, categroy, produceTime, protectTime, number, imagePath, mid);
            if (res == true)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

        #region 修改
        public JsonBackResult UpadteProduct(long id, string name, double price, int state, int categroy, DateTime marketTime, DateTime produceTime, int protectTime, int number, string imagePath)
        {
            var product = this._productService.GetList(s => s.ID == id).FirstOrDefault();
            if (product != null)
            {
                product.Category = categroy;
                product.ImagePath = imagePath;
                product.MarketTime = marketTime;
                product.Name = name;
                product.Number = number;
                product.Price = price;
                product.ProduceTime = produceTime;
                product.ProtectTime = protectTime;
                product.Status = state;

                this._productService.Update(product);
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

        #region 删除
        public JsonBackResult DeleteProduct(long id)
        {
            var res = this._productService.DeleteReal(s => s.ID == id);
            if (res > 0)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

        #region 删除联级关系
        public JsonBackResult DeleteProduct(long pid, long mid)
        {
            if (pid != null && mid != null)
            {
                this._productService.RemoveMachine(pid, mid);
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion
    }
}
