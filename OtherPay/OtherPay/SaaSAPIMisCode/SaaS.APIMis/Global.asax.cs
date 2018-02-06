using SaaS.EntityMis.Info;
using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Xml;

namespace SaaS.APIMis
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //初始化缓存

            // SaaS.Framework.Cache.CacheInit.InitCache();

            ////初始化数据库类型,如果读取错误，默认sqlserver-----1:sqlserver/2:oracle
            try
            {
                XmlDocument xmlDbType = new XmlDocument();
                xmlDbType.Load(Server.MapPath("/Config") + "//DB.xml");
                XmlNode xmlNode = xmlDbType.SelectSingleNode("/DbType");
                ConfigInfo.DbType = xmlNode.InnerText;
            }
            catch (Exception)
            {
                ConfigInfo.DbType = "1";
            }


            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        #region 应用程序全局错误
        /// <summary>
        /// 应用程序全局错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(Object sender, EventArgs e)
        {
            Log4Helper.Error("RequestUrl=" + HttpContext.Current.Request.Url);
            Exception ex = Server.GetLastError().GetBaseException();
            if (ex != null)
            {
                Log4Helper.Error("错误的信息=" + ex.Message);
                Log4Helper.Error("错误的堆栈=" + ex.StackTrace.Replace(" ", "<br/>"));
                Log4Helper.Error("出错的方法名=" + ex.TargetSite.Name);
                Log4Helper.Error("出错的类名=" + ex.TargetSite.DeclaringType.FullName);
            }
        }
        #endregion
    }
}






