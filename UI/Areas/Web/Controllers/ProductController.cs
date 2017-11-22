using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IService;
using UI.Controllers.Base;

namespace UI.Areas.Web.Controllers
{
    public class ProductController : JsonController
    {
        //
        // GET: /Web/Product/

        public ActionResult Index()
        {
            return View();
        }
        readonly IProductAdminService _productAdminService;
        public ProductController(IProductAdminService productService)
        {
            this._productAdminService = productService;
          
        }

    }
}
