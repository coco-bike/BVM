using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Controllers.Base;
using IService;
using Common;
using Model;

namespace UI.Areas.Web.Controllers
{
    public class UserController : JsonController
    {
        //
        // GET: /Web/User/
        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
           return View();
        }
        //
        // GET: /Web/register/
        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult register()
        {
            return View();
        }
        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <returns></returns>
        public ActionResult forgotpassword()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult tables()
        {
            return View();
        }
        public ActionResult charts()
        {
            return View();
        }
        public ActionResult navbar()
        {
            return View();
        }
        public ActionResult cards()
        {
            return View();
        }
        public ActionResult blank()
        {
            return View();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        readonly IUserAdminService _userService;
        readonly IRoleAdminService _roleService;
        readonly IAuthorityAdminService _authorityService;
        public UserController(IUserAdminService userService,IRoleAdminService roleService,IAuthorityAdminService authorityService)
        {
            this._userService = userService;
            this._authorityService = authorityService;
            this._roleService = roleService;
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="autologin"></param>
        /// <returns></returns>
        public JsonBackResult CheckLoginInfo(string email,string password,bool autologin)
        {
            var user = this._userService.GetList(s => s.EMail == email && s.Password == password&&s.Status==0).FirstOrDefault();
            if (user != null && user.Role.Authoritys.FirstOrDefault().Id == 1)
            {
                string sessionId = Guid.NewGuid().ToString();
                Response.Cookies["sessionId"].Value = sessionId;
                if (autologin==true)
                {
                    CacheHelper.Set(sessionId, user, DateTime.Now.AddDays(30));
                    Response.Cookies["sessionId"].Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    CacheHelper.Set(sessionId, user, DateTime.Now.AddDays(1));
                    Response.Cookies["sessionId"].Expires = DateTime.Now.AddDays(1);
                }
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonBackResult RegisterUserInfo(string email,string name,string password)
        {
           var user = this._userService.GetList(s => s.EMail == email).FirstOrDefault();
            if(user!=null)
            {
                return JsonBackResult(ResultStatus.Fail, "你输入的电子邮箱已经注册过");
            }

            Model.User realuser = new Model.User();
            realuser.EMail = email;
            realuser.NickName = name;
            realuser.Password = password;
            realuser.Count = +1;
            realuser.LoginTime = DateTime.Now;
            Role role1 = _roleService.GetList(s => s.Id == 1).FirstOrDefault();
            realuser.Role = role1;
            realuser.RoleID = role1.Id;

            this._userService.Add(realuser);

            return JsonBackResult(ResultStatus.Success);
        }
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public JsonBackResult FindUserInfo(string mail)
        {
            var user = this._userService.GetList(s => s.EMail == mail).FirstOrDefault();
            if(user!=null)
            {
                return JsonBackResult(ResultStatus.Success, user);
            }
            return JsonBackResult(ResultStatus.Fail, "你所找的用户不存在");
        }


        public JsonBackResult SeedEmail(string mail)
        {

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonBackResult UpdateUserInfo(string mail,string name,string password)
        {
            Model.User user = null;

            user.EMail = mail;
            user.NickName = name;
            user.Password = password;

            this._userService.Add(user);

            return JsonBackResult(ResultStatus.Success);
        }
        public ActionResult Delete()
        {
            return View();
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public JsonBackResult DeleteUserInfo(string mail)
        {
            int result = this._userService.DeleteReal(t => t.EMail == mail);
            if(result>0)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
        }
    }
}
