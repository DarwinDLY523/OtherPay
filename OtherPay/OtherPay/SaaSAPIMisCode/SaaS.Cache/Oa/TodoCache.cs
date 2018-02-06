using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaaS.Framework.Cache;
using SaaS.EntityMis.Model;

namespace SaaS.Cache.Oa
{
    public class TodoCache
    {
        /// <summary>
        /// 设置待办事宜回复缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SetTodo(string tokenId, List<T_OA_TODO> list)
        {
            return Memcache.Set(CacheConstants.todo + tokenId, list, DateTime.Now.AddMinutes(30));
        }


        /// <summary>
        /// 返回待办事宜回复缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static List<T_OA_TODO> GetTodo(string tokenId)
        {
            if (!Memcache.KeyExists(CacheConstants.todo + tokenId))
            {
                return null;
            }
            List<T_OA_TODO> ListTodo = Memcache.Get(CacheConstants.todo + tokenId) as List<T_OA_TODO>;

            if (ListTodo != null)
            {
                SetTodo(tokenId, ListTodo);
                return ListTodo;
            }
            return null;
        }



        /// <summary>
        /// 清除待办事宜回复缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static bool DeleteTodo(string tokenId)
        {
            return Memcache.Delete(CacheConstants.todo + tokenId);
        }
    }
}
