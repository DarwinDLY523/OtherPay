using SaaS.Framework.Common;
using System.Collections.Generic;


namespace SaaS.APIMis.BLL
{


    /// <summary>
    /// 服务器地址
    /// </summary>
    public static class ServerPath
    {


        #region 中间件地址
        /// <summary>
        /// 中间件地址
        /// </summary>
        public static string ApiService
        {
            get
            {
                string result = ConfigHelper.GetSettings("ApiService");
                return string.IsNullOrEmpty(result) ? "" : result.ToLower().Trim();
            }
        }
        #endregion

    
    }
}
