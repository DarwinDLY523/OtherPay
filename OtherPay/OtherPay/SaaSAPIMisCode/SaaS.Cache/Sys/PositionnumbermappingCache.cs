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
    /// 微信配置缓存
    /// </summary>
    public class PositionnumbermappingCache
    {
        private readonly static IPositionnumbermappingDAL dal = SaaS.Factory.PositionnumbermappingFactory.PositionnumbermappingDAL();

        #region 获取租户职位映射列表
        /// <summary>
        /// 获取租户职位映射列表
        /// </summary>
        /// <param name="FLesseeID">租户ID</param>
        /// <returns></returns>
        public static List<PositionnumbermappingInfo> getListByFLesseeID(string FLesseeID)
        {
            List<PositionnumbermappingInfo> list = Memcache.Get(CacheConstants.position + FLesseeID) as List<PositionnumbermappingInfo>;
           // if (list == null || list.Count <= 0)
            if (true)
            {
                list = dal.GetList(FLesseeID);
                Memcache.Set(CacheConstants.position + FLesseeID, list);
            }
            return list;
        }
        #endregion

        #region 删除租户职位映射缓存
        /// <summary>
        /// 删除租户职位映射缓存
        /// </summary>
        /// <param name="FLesseeID">租户ID</param>
        /// <returns></returns>
        public static bool DeleteByFLesseeID(string FLesseeID)
        {
            return Memcache.Delete(CacheConstants.position + FLesseeID);
        }
        #endregion

        #region 根据映射key获取职位编码
        /// <summary>
        /// 根据映射key获取职位编码
        /// </summary>
        /// <param name="FLesseeID">租户ID</param>
        /// <param name="key">映射key枚举</param>
        /// <returns></returns>
        public static string GetPositionNumber(string FLesseeID, PostionEnum.GdgjPostion position)
        {
            List<PositionnumbermappingInfo> list = getListByFLesseeID(FLesseeID);
            if (list != null && list.Count > 0)
            {
                PositionnumbermappingInfo info = list.Where(o => o.FMAPPINGNUMBER == position.ToString()).FirstOrDefault();
                return info.FPOSITIONNAME;
            }
            return string.Empty;
        }
        #endregion
    }
}
