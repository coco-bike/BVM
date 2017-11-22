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
    public class SaleController : JsonController
    {
        //
        // GET: /Admin/Sale/
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region
        readonly ISaleAdminService _saleAdminService;
        public SaleController(ISaleAdminService saleAdminService)
        {
            this._saleAdminService = saleAdminService;
        }
        #endregion

        #region 分页查询
        public JsonBackResult GetPagingList(int pageIndex,int pageSize,string name="")
        {
            int totalCount;
            IEnumerable<object> salesList = null;
            salesList = this._saleAdminService.GetPagingList(pageIndex, pageSize,out totalCount, true,t=>t.Change.ToString().Contains(name), t => t.ID).Select(t => new { t.ID, t.Change, t.MachineID, t.Number, t.Price, t.ProductID, t.Time }).ToList();
            return JsonBackResult(true, new PagingResult<object>(salesList, totalCount));
        }
        #endregion

        #region 增加
        public JsonBackResult AddSale(double price,double change,int Number,int pid,int mid)
        {
            var res=this._saleAdminService.AddSale(price, change, Number, pid, mid);
            if(res==true)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

        #region 修改
        public JsonBackResult UpdateSale(long id,double price,double change,int number,int pid,int mid)
        {
            var res = this._saleAdminService.UpdateSale(id, price, change, number, pid, mid);
            if(res==true)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

        #region 删除
        public JsonBackResult DeleteSale(long id)
        {
            var res = this._saleAdminService.DeleteReal(s => s.ID == id);
            if(res>0)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion
    }
}
