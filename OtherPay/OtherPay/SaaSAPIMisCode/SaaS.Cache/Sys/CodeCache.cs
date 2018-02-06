using SaaS.Framework.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace SaaS.Cache
{
    public class CodeCache  //
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="obj">缓存内容</param>
        /// <param name="key">缓存key</param>
        /// <param name="time">缓存时间 默认30分钟</param>
        /// <returns>成功:key 失败:""</returns>
        public static string SetCache(object obj, string key = "", int? time = 30)
        {
            key = string.IsNullOrEmpty(key) ? Guid.NewGuid().ToString() : key;   
            if (Memcache.Set(key, obj, DateTime.Now.AddMinutes(time.Value)))
            {
                return key;
            }
            return "";
        }
        
        /// <summary>
        /// 根据key获取缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="type">是否删除 默认 不删除 </param>
        /// <returns>缓存信息 </returns>
        public static object GetCache(string key, bool isdelete = false)
        { 
            if (!Memcache.KeyExists(key)) { return string.Empty; }
            object obj = Memcache.Get(key);
            if (isdelete)
            {
                Memcache.Delete(key);//删除缓存
            }
            return obj;
        }

        /// <summary>
        /// 根据key删除缓存
        /// </summary>
        public static bool DelCache(string key)
        {       
            if (!Memcache.KeyExists(key)) { return false; }
           return Memcache.Delete(key);//删除缓
        }

        
    }
}
