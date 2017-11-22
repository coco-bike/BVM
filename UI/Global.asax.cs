using Autofac;
using Autofac.Integration.Mvc;
using log4net;
using UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UI.Services;
using Model;
using System.Data.Entity;
using Common;
using IService;
using Service;
using System.IO;

namespace UI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            #region Autofac在MVC中注册
            ContainerBuilder builder = new ContainerBuilder();
            var service = Assembly.Load("IService");
            var service1 = Assembly.Load("Service");
            var service2 = Assembly.Load("Model");
            Assembly[] assemblyArr = new Assembly[] { service, service1, service2 };
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(assemblyArr).AsImplementedInterfaces();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

            DbContext db = new MyContext();
            if(db.Database.CreateIfNotExists())
            {
                //产品表
                IProductAdminService productSerivce = new ProductAdminService();
                List<Product> productList = new List<Product>()
                {
                    new Product(){Category=1,ImagePath="../image",MarketTime=DateTime.Now,Number=30,Name="可口可乐",Price=2.5,ProduceTime=DateTime.Now,ProtectTime=12,Status=1},
                    new Product(){Category=1,ImagePath="../image",MarketTime=DateTime.Now,Number=30,Name="雪碧",Price=2.5,ProduceTime=DateTime.Now,ProtectTime=12,Status=1}
                };
                productSerivce.AddRange(productList);

                //机器表
                IMachineAdminService machineService = new MachineAdminService();
                List<Machine> machineList=new List<Machine>()
                {
                    new Machine(){Address="雨花台软件大道",Code="JQ0001",MaintainTime=DateTime.Now,MarketTime=DateTime.Now},
                     new Machine(){Address="玄武区孝陵卫",Code="JQ0002",MaintainTime=DateTime.Now,MarketTime=DateTime.Now}
                };
                machineService.AddRange(machineList);

                //权限
                IAuthorityService authorityService = new AuthorityAdminService();
                List<Authority> authorityList = new List<Authority>()
                {
                    new Authority(){ Id=1,BuildTime=DateTime.Now,Description="测试",Name="R&W",Status=0,Type=0,UpdateTime=DateTime.Now,Roles=new List<Role>()},
                    new Authority(){Id=2,BuildTime=DateTime.Now,Description="测试1",Name="W",Status=0,Type=0,UpdateTime=DateTime.Now,Roles=new List<Role>()}
                };
                authorityService.AddRange(authorityList);

                Authority authority1 = new Authority();
                authority1 = authorityService.GetList(s => s.Id == 1).FirstOrDefault();
                Authority authority2 = new Authority();
                authority2 = authorityService.GetList(s => s.Id == 2).FirstOrDefault();

                //角色
                IRoleAdminService roleService = new RoleAdminService();
                List<Role> roleList = new List<Role>()
                {
                    new Role(){Id=1,BuildTime=DateTime.Now,Description="测试1",RoleName="测试1",Status=0,UpateTime=DateTime.Now,Authoritys=new List<Authority>()},
                    new Role(){Id=2,BuildTime=DateTime.Now,Description="测试2",RoleName="测试2",Status=0,UpateTime=DateTime.Now,Authoritys=new List<Authority>()}
                };
                roleService.AddRange(roleList);

                Role role1 = new Role();
                role1 = roleService.GetList(s => s.Id == 1).FirstOrDefault();
                Role role2 = new Role();
                role2 = roleService.GetList(s => s.Id == 2).FirstOrDefault();

                role1.Authoritys.Add(authority1);
                role2.Authoritys.Add(authority2);
                //用户表
                IUserAdminService userService = new UserAdminService();
                List<User> userList = new List<User>()
                {
                    new User(){ ID=1,NickName="薄荷",Password="112233",EMail="444503829@qq.com",Role=role1,RoleID=role1.Id,Status=0,LoginTime=DateTime.Now,Count=0,BuildTime=DateTime.Now,UpdateTime=DateTime.Now},
                    new User(){ID=2,NickName="少年",Password="112233",EMail="1746680121@qq.com",Role=role2,RoleID=role2.Id,Status=0,LoginTime=DateTime.Now,Count=0,BuildTime=DateTime.Now,UpdateTime=DateTime.Now}
                };
                userService.AddRange(userList);               
            }

            //log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

    

            //MiniProfilerEF6.Initialize();//注册MiniProfiler，网页性能插件
            log4net.Config.XmlConfigurator.Configure();
            //WaitCallback
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count > 0)
                    {

                        Exception ex = MyExceptionAttribute.ExceptionQueue.Dequeue();//出队
                        //string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                        //File.AppendAllText(Path.Combine("App_Data", fileName), ex.ToString(), System.Text.Encoding.Default);
                        //ILog logger = LogManager.GetLogger("errorMsg");
                        ILog logger = log4net.LogManager.GetLogger("logger");
                        logger.Error(ex.ToString());

                        #region 发送邮件
                        //MailHelper mail = new MailHelper();
                        //mail.MailServer = "smtp.qq.com";
                        //mail.MailboxName = "2872845261@qq.com";
                        //mail.MailboxPassword = "obxxsfowztbideee";//开启QQ邮箱POP3/SMTP服务时给的授权码
                        ////操作打开QQ邮箱->在账号下方点击"设置"->账户->POP3/IMAP/SMTP/Exchange/CardDAV/CalDAV服务
                        ////obxxsfowztbideee为2872845261@qq的授权码
                        //mail.MailName = "Error";
                        //try
                        //{
                        //    mail.Send("1015934551@qq.com", "Error", ex.ToString());
                        //}
                        //catch
                        //{ } 
                        #endregion

                    }
                    else
                    {
                        Thread.Sleep(3000);//如果队列中没有数据，则休息为了避免占用CPU的资源.
                    }
                }
            });
        }
        //void Application_Error(object sender, EventArgs e)
        //{
        //    // 在出现未处理的错误时运行的代码
        //    Exception objExp = HttpContext.Current.Server.GetLastError();
        //    LogHelper.ErrorLog("<br/><strong>客户机IP</strong>：" + Request.UserHostAddress + "<br /><strong>错误地址</strong>：" + Request.Url, objExp);
        //}
        //protected void Application_BeginRequest()
        //{
        //    MiniProfiler.Start();
        //}

        //protected void Application_EndRequest()
        //{
        //    MiniProfiler.Stop();
        //}
    }
}