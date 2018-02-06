using SaaS.EntityMis.Enums;
using SaaS.EntityMis.Info;
using SaaS.Framework.Cache;
using SaaS.InterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.Cache
{
    /// <summary>
    /// 微信消息模版缓存
    /// </summary>
    public class MessagetemplateweixinCache
    {
        private readonly static IMessagetemplateweixinDAL dal = SaaS.Factory.MessagetemplateweixinFactory.MessagetemplateweixinDAL();

        #region 获取租户模版列表
        /// <summary>
        /// 获取租户模版列表
        /// </summary>
        /// <param name="FLesseeID">租户ID</param>
        /// <returns></returns>
        public static  List<MessagetemplateweixinInfo> getListByFLesseeID(string FLesseeID)
        {
            List<MessagetemplateweixinInfo> list = Memcache.Get(CacheConstants.messageTemplate + FLesseeID) as List<MessagetemplateweixinInfo>;
            //if (list == null || list.Count <= 0)
            if (true)
            {
                list = dal.getList(FLesseeID);
                Memcache.Set(CacheConstants.messageTemplate + FLesseeID, list);
            }
            return list;
        }
        #endregion

        #region 删除租户模版缓存
        /// <summary>
        /// 删除租户模版缓存
        /// </summary>
        /// <param name="FLesseeID">租户ID</param>
        /// <returns></returns>
        public static  bool DeleteByFLesseeID(string FLesseeID)
        {
            return Memcache.Delete(CacheConstants.messageTemplate + FLesseeID);
        }
        #endregion

        #region 根据模版key获取模版value
        /// <summary>
        /// 根据模版key获取模版value
        /// </summary>
        /// <param name="FLesseeID">租户ID</param>
        /// <param name="key">模版key枚举</param>
        /// <returns></returns>
        public static  string GetTemplateValue(string FLesseeID, string template)
        {
            List<MessagetemplateweixinInfo> list = getListByFLesseeID(FLesseeID);
            if (list != null && list.Count > 0)
            {
                MessagetemplateweixinInfo info = list.Where(o => o.FTEMPLATEKEY == template.ToString()).FirstOrDefault();
                if (info==null)
                {
                    info = new MessagetemplateweixinInfo();
                }
                return info.FTEMPLATEVALUE;
            }
            return string.Empty;
        }
        #endregion

    }
}
