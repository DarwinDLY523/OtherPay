using SaaS.EntityMis.Info;
using SaaS.Framework.Cache;
using SaaS.Framework.Common;
using SaaS.InterfaceDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.Cache
{
    public class PersonCache
    {
        private readonly static IPersonDAL dal = SaaS.Factory.PersonFactory.PersonDAL();

        #region 根据手机号码获取员工信息

        /// <summary>
        /// 根据手机号码获取员工信息
        /// </summary>
        /// <param name="fCell">手机号码</param>
        /// <param name="FLesseeID">租户ID</param>
        /// <returns></returns>
        public static PersonInfo GetModelByCellCache(string fCell, string FLesseeID)
        {
            PersonInfo person = null;
            object obj = Memcache.Get(CacheConstants.person + FLesseeID + "_" + fCell);

            if (obj != null)
            {
                person = obj as PersonInfo;
            }
            if (person == null)
            {
                person = dal.GetModelByCell(fCell, FLesseeID);
                if (person != null && !string.IsNullOrEmpty(person.FID))
                {
                    object objperson = JsonHelper.ObjectToJSON(person);
                    Memcache.Set(CacheConstants.person + FLesseeID + "_" + fCell, objperson, DateTime.Now.AddDays(1));
                }
            }

            return person;
        }
        #endregion



    }
}
