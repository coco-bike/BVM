using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Controllers.Base;

namespace UI.Areas.Admin.Controllers
{
    public class UserController : JsonController
    {
        //
        // GET: /Admin/User/
        public ActionResult Index()
        {
            return View();
        }
      
    }
}
