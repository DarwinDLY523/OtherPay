//期数缓存
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaaS.Framework.Cache;
using SaaS.EntityMis.Model;

namespace SaaS.Cache.Sys
{

    public class PeriodnumberCache
    {
        /// <summary>
        /// 设置当前期缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SetPeriodnumber(string tokenId, T_SYS_PERIODNUMBER list)
        {
            return Memcache.Set(CacheConstants.periodnumber + tokenId, list, DateTime.Now.AddMinutes(30));
        }


        /// <summary>
        /// 返回当前期缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static T_SYS_PERIODNUMBER GetPeriodnumber(string tokenId)
        {
            if (!Memcache.KeyExists(CacheConstants.periodnumber + tokenId))
            {
                return null;
            }
            T_SYS_PERIODNUMBER modelPeriodnumber = Memcache.Get(CacheConstants.periodnumber + tokenId) as T_SYS_PERIODNUMBER;

            if (modelPeriodnumber != null)
            {
                SetPeriodnumber(tokenId, modelPeriodnumber);
                return modelPeriodnumber;
            }
            return null;
        }



        /// <summary>
        /// 清除当前期缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static bool DeletePeriodnumber(string tokenId)
        {
            return Memcache.Delete(CacheConstants.periodnumber + tokenId);
        }
    }
}
