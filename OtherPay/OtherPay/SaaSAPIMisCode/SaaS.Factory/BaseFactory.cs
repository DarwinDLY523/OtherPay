using SaaS.Framework.Common;
using System.Reflection;


namespace SaaS.Factory
{
    public class BaseFactory
    {
        /// <summary>
        /// ���ݷ��ʲ��������·�� 
        /// </summary>
        protected static readonly string path = SaaS.EntityMis.Info.ConfigInfo.DbType == "2" ? "SaaS.OracleDAL" : "SaaS.SqlServerDAL";
            
        /// <summary>
        /// ʵ�ֽӿ�
        /// </summary>
        /// <param name="path"></param>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public static object CreateObject(string path, string CacheKey)
        {
            object objType = CacheHelper.GetCache(CacheKey);

            if (objType == null)
            {
                try
                {
                    objType = Assembly.Load(path).CreateInstance(CacheKey);

                    CacheHelper.SetCache(CacheKey, objType);
                }
                catch
                {
                    throw;
                }
            }

            return objType;
        }
    }
}
