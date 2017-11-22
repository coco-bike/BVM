using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Controllers.Base;

namespace UI.Areas.Web.Controllers
{
    public class SaleController : JsonController
    {
        //
        // GET: /Web/Sale/

        public ActionResult Index()
        {
            return View();
        }

    }
}
