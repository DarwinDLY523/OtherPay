using SaaS.EntityMis.Info;
using SaaS.Framework.Cache;
using SaaS.Framework.Common;
using SaaS.InterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.Cache
{
    /// <summary>
    /// 微信配置缓存
    /// </summary>
    public class WechtaqyweixinCache
    {
        private readonly static IWechtaqyweixinDAL dal = SaaS.Factory.WechtaqyweixinFactory.WechtaqyweixinDAL();

        #region 根据域名获取服务号企业号配置
        /// <summary>
        /// 根据域名获取服务号企业号配置
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static  WechtaqyweixinInfo GetModelByDomain(string domain)
        {
            WechtaqyweixinInfo info = Memcache.Get(CacheConstants.wechat+domain) as WechtaqyweixinInfo;
            if (true) 
            {
                info = dal.GetModelByDomain(domain);
                Memcache.Set(CacheConstants.wechat + domain, info);
            }
            return info;
        }
        #endregion

        #region 根据租户ID获取服务号企业号配置
        /// <summary>
        /// 根据租户ID获取服务号企业号配置
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static  WechtaqyweixinInfo GetModelByLesseeID(string fLesseeID)
        {
            WechtaqyweixinInfo info = Memcache.Get(CacheConstants.wechat + fLesseeID) as WechtaqyweixinInfo;
           if (true)           
            {
                info = dal.GetModelByLesseeID(fLesseeID);
                Memcache.Set(CacheConstants.wechat + fLesseeID, info);
            }
            return info;
        }
        #endregion

        #region 根据租户ID获取企业号应用配置
        /// <summary>
        /// 根据租户ID获取企业号应用配置
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static List<WechatQyAgentIDInfo> GetQyAgentIDByLesseeID(string fLesseeID)
        {
            List<WechatQyAgentIDInfo> list = Memcache.Get(CacheConstants.wechatQyAgentId + fLesseeID) as List<WechatQyAgentIDInfo>;
             if (true)           
            {
                list = dal.GetQyAgentIDByLesseeID(fLesseeID);
                Memcache.Set(CacheConstants.wechatQyAgentId + fLesseeID, list);
            }
            return list;
        }
        #endregion

        #region 获取企业号应用ID
        /// <summary>
        /// 获取企业号应用ID
        /// </summary>
        /// <param name="qyAgentKey">企业号应用key</param>
        /// <param name="fLesseeID">租户ID</param>
        /// <returns></returns>
        public static string GetQyAgentIDByKey(SaaS.EntityMis.Enums.WechatEnum.QyAgentKey qyAgentKey,string fLesseeID)
        {
            List<WechatQyAgentIDInfo> list = GetQyAgentIDByLesseeID(fLesseeID);
            if (list != null && list.Count > 0)
            {
                WechatQyAgentIDInfo info = list.Where(o => o.FFAgentKey ==qyAgentKey.ToString()).FirstOrDefault();
                if (info != null && !string.IsNullOrEmpty(info.FAgentID))
                {
                    return info.FAgentID;
                }
            }
            return "0";
        }
        #endregion
    }
}
