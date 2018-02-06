using SaaS.EntityMis.Info;
using SaaS.Framework.Cache;
using SaaS.InterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.Cache.Sup
{
    public class MaterialCache
    {
        private readonly static IMaterialgroupDAL dal = SaaS.Factory.MaterialgroupFactory.MaterialgroupDAL();

        #region 物料分类 写入缓存 MaterialGroupCache
        /// <summary>
        /// 物料分类 写入缓存
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static List<TreeLowerInfo> MaterialGroupCache(string fLesseeID, string fId, string fadminId)
        {
            List<TreeLowerInfo> list = new List<TreeLowerInfo>();
            object obj = Memcache.Get(CacheConstants.materialGroup + fLesseeID + "_" + fadminId);
            if (obj != null)
            {
                list = obj as List<TreeLowerInfo>;

            }
            else
            {
                list = dal.GetMaterialGroupTree(fLesseeID, fId, fadminId);
                if (list.Count > 0)
                {
                    Memcache.Set(CacheConstants.materialGroup + fLesseeID + "_" + fadminId, list, DateTime.Now.AddMinutes(3));
                }
            }
            return list;
        }
        #endregion
    }
}
