using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using IService;
using Service;

namespace UI.Controllers.Base
{
    public class BaseController : JsonController
    {   
        IUserAdminService userShopService
        {
            get
            {
                return new UserAdminService();
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            string contr = filterContext.RouteData.Values["controller"].ToString();
            string action = filterContext.RouteData.Values["action"].ToString();
            if(!WhetherSuccess())
            {
                filterContext.Result = Redirect("/Web/User/Index");
            }
        }

        private bool WhetherSuccess()
        {
            if(Request.Cookies["sessionId"]!=null)
            {
               string sessionId = Request.Cookies["sessionId"].Value;
               Model.User user = CacheHelper.Get(sessionId) as Model.User;
                if(user == null)
                {
                    return false;
                }
                Model.User temp = userShopService.GetList(t => t.NickName == user.NickName && t.Password == user.Password).FirstOrDefault();
                if(temp !=null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
