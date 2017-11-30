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
        static string usercode = null;
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
            var md5password = EncryptionHelper.GetMd5Str(password);
            var user = this._userService.GetList(s => s.EMail == email && s.Password == md5password && s.Status == 1).FirstOrDefault();
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
                user.Count = user.Count+1;
                user.LoginTime = DateTime.Now;
                var res = this._userService.Update(user);
                if (res > 0)
                {
                    return JsonBackResult(ResultStatus.Success);
                }
            }
            return JsonBackResult(ResultStatus.Fail);
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonBackResult GetCode(string email)
        {
            if (!RegularHelper.IsEmail(email))
            {
                return JsonBackResult(ResultStatus.EmailErr);
            }
            var user = this._userService.GetList(t => t.EMail == email && t.Status == 0).FirstOrDefault();
            if (user != null)
            {
                return JsonBackResult(ResultStatus.EmailExist);
            }
            Tuple<string, bool> items = SendEmail(email, "亚太官网后台", "用户注册码");
            string code = items.Item1;
            usercode = code;
            bool sendRes = items.Item2;
            if (sendRes)
            {
                bool res1 = CacheHelper.Set("RegCode", code, DateTime.Now.AddSeconds(90));
                bool res2 = CacheHelper.Set("Email", email, DateTime.Now.AddSeconds(90));
                if (res1 && res2)
                {
                    return JsonBackResult(ResultStatus.Success);
                }
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
        public JsonBackResult RegisterUserInfo(string email,string name,string password,string code)
        {
           var user = this._userService.GetList(s => s.EMail == email).FirstOrDefault();
            if(user!=null)
            {
                return JsonBackResult(ResultStatus.EmailExist, "你输入的电子邮箱已经注册过");
            }
            if (usercode!=code)
            {
                return JsonBackResult(ResultStatus.ValidateCodeErr, "验证码错误,请重新发送并输入新的验证码");
            }
            Model.User realuser = new Model.User();
            realuser.EMail = email;
            realuser.NickName = name;
            realuser.Password = EncryptionHelper.GetMd5Str(password);
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
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonBackResult FindUserInfo(string email)
        {
            var user = this._userService.GetList(s => s.EMail == email).FirstOrDefault();
            if(user==null)
            {
                return JsonBackResult(ResultStatus.EmailNoExist, user);
            }
            if (!RegularHelper.IsEmail(email))//判断邮箱格式是否正确
            {
                return JsonBackResult(ResultStatus.EmailErr);
            }
            Tuple<string, bool> items = SendEmail(email, "亚太官网", "用户找回密码验证码");
            string code = items.Item1;
            usercode = code;
            bool sendRes = items.Item2;
            if (sendRes)
            {
                bool res1 = CacheHelper.Set("PwdBackCode", code, DateTime.Now.AddSeconds(90));
                bool res2 = CacheHelper.Set("PwdBackEMail", email, DateTime.Now.AddSeconds(90));//判断用户发送验证码和注册用的是同一个邮箱
                if (res1 && res2)
                {
                    return JsonBackResult(ResultStatus.Success);
                }
            }
            return JsonBackResult(ResultStatus.Fail);
        }


        /// <summary>
        /// 用户找回密码发送验证码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public JsonBackResult ValidatePwdBackCode(string code)
        {
            if (usercode == code)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.ValidateCodeErr);
        }

        /// <summary>
        /// 用户找回密码成功
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonBackResult GetPassword(string email, string password)
        {
            var md5password = EncryptionHelper.GetMd5Str(password);
            var user = this._userService.GetList(s => s.EMail == email).FirstOrDefault();
            if (user == null)
            {
                return JsonBackResult(ResultStatus.EmailNoExist);
            }

            user.Password = md5password;
            user.Count = +1;
            user.LoginTime = DateTime.Now;
            int res= this._userService.Update(user);
            if (res > 0)
            {
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.Fail);
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
            var user = this._userService.GetList(s => s.EMail == mail).FirstOrDefault();
            if (user != null)
            {
                var md5password = EncryptionHelper.GetMd5Str(password);
                user.NickName = name;
                user.Password = md5password;
                user.Count = +1;
                user.UpdateTime = DateTime.Now;
                this._userService.Update(user);
                return JsonBackResult(ResultStatus.Success);
            }
            return JsonBackResult(ResultStatus.EmailNoExist);
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


         /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="eMail">接收方邮箱</param>
        /// <param name="mailName">发件人名称</param>
        /// <param name="mailTitle">邮件名称</param>
        /// <returns></returns>
        public static Tuple<string, bool> SendEmail(string eMail, string mailName, string mailTitle)
        {
            MailHelper mail = new MailHelper();
            mail.MailServer = "smtp.qq.com";
            mail.MailboxName = "444503829@qq.com";
            mail.MailboxPassword = "fplslqpringqbhhi";//开启QQ邮箱POP3/SMTP服务时给的授权码
            //操作打开QQ邮箱->在账号下方点击"设置"->账户->POP3/IMAP/SMTP/Exchange/CardDAV/CalDAV服务
            //obxxsfowztbideee为2872845261@qq的授权码
            mail.MailName = mailName;
            string code;
            VerifyCode codeHelper = new VerifyCode();
            codeHelper.GetVerifyCode(out code);
            if (code == "")
                return new Tuple<string, bool>("", false);
            if (mail.Send(eMail, mailTitle, code))
                return new Tuple<string, bool>(code, true);
            return new Tuple<string, bool>("", false);
        }
    }
}
