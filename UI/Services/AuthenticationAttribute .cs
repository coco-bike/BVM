using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Common;

namespace UI.Services
{
    // 登录认证特性
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {   string key = filterContext.HttpContext.Request.Cookies["sessionId"].Value;
            if ( CacheHelper.Get(key)==null)
            {
                filterContext.Result = new RedirectResult("../user/Login");
            }             
            base.OnActionExecuting(filterContext);
        }
    }
}