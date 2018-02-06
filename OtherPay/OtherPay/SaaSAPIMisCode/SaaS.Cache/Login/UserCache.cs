using SaaS.Entity.Info;
using SaaS.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace SaaS.Cache.Login
{
    public class UserCache
    {
       

        /// <summary>
        /// 保存登陆缓存信息
        /// </summary>
        /// <param name="userInfo">登陆信息</param>
        /// <returns></returns>
        public static bool SetUserInfo(LoginUserInfo userInfo)
        {
            return Memcache.Set(CacheConstants.tokenid + userInfo.TokenId, userInfo, DateTime.Now.AddMinutes(30));
        }


        /// <summary>
        /// 返回登陆缓存信息
        /// </summary>
        /// <param name="userInfo">登陆信息</param>
        /// <returns></returns>
        public static LoginUserInfo GetUserInfo(string tokenId)
        {
            if (!Memcache.KeyExists(CacheConstants.tokenid + tokenId))
            {
                return null;
            }
            LoginUserInfo userInfo = Memcache.Get(CacheConstants.tokenid + tokenId) as LoginUserInfo;
          
            if (userInfo != null && !string.IsNullOrEmpty(userInfo.TokenId))
            {
                Memcache.Set(CacheConstants.tokenid + tokenId, userInfo, DateTime.Now.AddMinutes(30));
                return userInfo;
            }
            return null;
        }



        /// <summary>
        /// 清除登陆信息
        /// </summary>
        /// <param name="tokenId">tokenId</param>
        /// <returns></returns>
        public static bool DeleteUserInfo(string tokenId)
        {
            return Memcache.Delete(CacheConstants.tokenid + tokenId);
        }
    }
}
