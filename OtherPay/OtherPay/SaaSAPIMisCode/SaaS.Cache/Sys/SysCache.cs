using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaaS.Framework.Cache;
using System.Data;

using System.Collections;
using SaaS.Entity.Info;
using SaaS.Cache.Login;


namespace SaaS.Cache.Sys
{
    public class SysCache
    {
        #region 租户权限
        /// <summary>
        /// 设置租户权限
        /// </summary>
        /// <param name="tokenId">tokenid</param>
        /// <param name="AdminPower">租户权限ID列表</param>
        /// <returns></returns>
        public static bool SetLesseePower(string tokenId, string lesseePower)
        {
            return Memcache.Set(CacheConstants.lesseePower + tokenId, lesseePower, DateTime.Now.AddMinutes(30));
        }
        /// <summary>
        /// 获取租户权限
        /// </summary>
        /// <param name="tokenId">tokenid</param>
        /// <returns></returns>
        public static string GetLesseePower(string tokenId)
        {
            string lesseePower = Memcache.Get(CacheConstants.lesseePower + tokenId) as string;
            if (lesseePower != null)
            {
                SetLesseePower(tokenId, lesseePower);
            }
            return lesseePower;
        }
        /// <summary>
        /// 删除租户权限
        /// </summary>
        /// <param name="tokenId">tokenid</param>
        /// <returns></returns>
        public static bool DeleteLesseePower(string tokenId)
        {
            return Memcache.Delete(CacheConstants.lesseePower + tokenId);
        }
        #endregion

        #region 平台客户权限
        /// <summary>
        /// 设置平台客户权限
        /// </summary>
        /// <param name="tokenId">tokenid</param>
        /// <param name="AdminPower">平台客户权限ID列表</param>
        /// <returns></returns>
        public static bool SetPlaCustomerPower(string tokenId, string plaCustomerPower)
        {
            return Memcache.Set(CacheConstants.plaCustomerPower + tokenId, plaCustomerPower, DateTime.Now.AddMinutes(30));
        }
        /// <summary>
        /// 获取平台客户权限
        /// </summary>
        /// <param name="tokenId">tokenid</param>
        /// <returns></returns>
        public static string GetPlaCustomerPower(string tokenId)
        {
            string plaCustomerPower = Memcache.Get(CacheConstants.plaCustomerPower + tokenId) as string;
            if (plaCustomerPower != null)
            {
                SetPlaCustomerPower(tokenId, plaCustomerPower);
            }

            return plaCustomerPower;
        }
        /// <summary>
        /// 删除平台客户权限
        /// </summary>
        /// <param name="tokenId">tokenid</param>
        /// <returns></returns>
        public static bool DeletePlaCustomerPower(string tokenId)
        {
            return Memcache.Delete(CacheConstants.plaCustomerPower + tokenId);
        }
        #endregion
    }
}
