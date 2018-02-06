using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaaS.Framework.Cache;
using SaaS.EntityMis.Info;

namespace SaaS.Cache.Oa
{
    public class DailybulletinCache
    {
        /// <summary>
        /// 设置日常公共缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SetDailybulletin(string tokenId, List<DailybulletinInfo> list)
        {
            return Memcache.Set(CacheConstants.dailybulletin + tokenId, list, DateTime.Now.AddMinutes(30));
        }


        /// <summary>
        /// 返回日常公共缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static List<DailybulletinInfo>  GetDailybulletin(string tokenId)
        {
            if (!Memcache.KeyExists(CacheConstants.dailybulletin + tokenId))
            {
                return null;
            }
            List<DailybulletinInfo> ListTodoacceptInfo = Memcache.Get(CacheConstants.dailybulletin + tokenId) as List<DailybulletinInfo>;

            if (ListTodoacceptInfo != null)
            {
                SetDailybulletin(tokenId, ListTodoacceptInfo);
                return ListTodoacceptInfo;
            }
            return null;
        }



        /// <summary>
        /// 清除日常公共缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static bool DeleteDailybulletin(string tokenId)
        {
            return Memcache.Delete(CacheConstants.dailybulletin + tokenId );
        }
    }
}
