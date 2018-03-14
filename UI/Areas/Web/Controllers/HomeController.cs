using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using UI.Controllers.Base;

namespace UI.Areas.Web.Controllers
{
    public class HomeController : JsonController
    {
        //
        // GET: /Web/Home/

        public ActionResult Index()
        {
            ViewBag.Id = 2;
            return View();
        }
        public class abc
        {
            public int Id{get;set;}

            public string Name { get; set; }
        }

        public JsonBackResult GetData()
        {
            int abc = 1;
            return JsonBackResult(ResultStatus.Success,abc);
        }

    }
}
