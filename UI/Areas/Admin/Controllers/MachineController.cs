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
    public class MachineController : JsonController
    {
        //
        // GET: /Admin/Machine/
        #region 页面
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 初始化
        readonly IMachineAdminService _machineService;
        public MachineController(IMachineAdminService machineService)
        {
            this._machineService = machineService;
        }
        #endregion

        #region 查询
        public JsonBackResult GetPagingList(int pageIndex, int pageSize, string name = "")
        {
            int totalCount;
            IEnumerable<object> machinesList = null;
            machinesList = this._machineService.GetPagingList(pageIndex, pageSize, out totalCount, true, t => t.Code.Contains(name), t => t.ID).Select(t => new { t.ID, t.Address, t.Code, t.MaintainTime, t.MarketTime }).ToList();

            return JsonBackResult(true, new PagingResult<object>(machinesList, totalCount));
        }
        #endregion

        #region 增加
        public JsonBackResult AddMachine(string code, DateTime marketTime, DateTime maintainTime, string address, long pid)
        {
            var res = this._machineService.AddMachine(code, maintainTime, address, pid);
            if (res == true)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

        #region 修改
        public JsonBackResult UpdateProduct(long id,string address,string code,DateTime maintaintime,DateTime markettime)
        {
            var machine = this._machineService.GetList(s => s.ID == id).FirstOrDefault();

            if (machine != null)
            {
                machine.Address = address;
                machine.Code = code;
                machine.MaintainTime = maintaintime;
                machine.MarketTime = markettime;

                this._machineService.Update(machine);

                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

        #region 删除
        public JsonBackResult DeleteProduct(long id)
        {
            int res = this._machineService.DeleteReal(t => t.ID == id);
            if (res>0)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        #endregion

      
    }
}

