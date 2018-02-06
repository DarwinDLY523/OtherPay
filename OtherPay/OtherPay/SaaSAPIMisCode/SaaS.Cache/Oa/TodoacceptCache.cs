using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaaS.Framework.Cache;
using SaaS.EntityMis.Info;

namespace SaaS.Cache.Oa
{
    public class TodoacceptCache
    {
        /// <summary>
        /// 设置待办事宜缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SetTodoaccept(string tokenId, List<TodoacceptInfo> list)
        {
            return Memcache.Set(CacheConstants.todoaccept + tokenId, list, DateTime.Now.AddMinutes(30));
        }


        /// <summary>
        /// 返回待办事宜缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static List<TodoacceptInfo> GetTodoaccept(string tokenId)
        {
            if (!Memcache.KeyExists(CacheConstants.todoaccept + tokenId))
            {
                return null;
            }
            List<TodoacceptInfo> ListTodoacceptInfo = Memcache.Get(CacheConstants.todoaccept + tokenId) as List<TodoacceptInfo>;

            if (ListTodoacceptInfo != null)
            {
                SetTodoaccept(tokenId, ListTodoacceptInfo);
                return ListTodoacceptInfo;
            }
            return null;
        }



        /// <summary>
        /// 清除待办事宜缓存
        /// </summary>
        /// <param name="flessID"></param>
        /// <param name="fUserID"></param>
        /// <returns></returns>
        public static bool DeleteTodoaccept(string tokenId)
        {
            return Memcache.Delete(CacheConstants.todoaccept + tokenId);
        }
    }
}
