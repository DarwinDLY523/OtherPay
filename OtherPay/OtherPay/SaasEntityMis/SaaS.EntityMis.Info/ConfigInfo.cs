using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.EntityMis.Info
{
   public class ConfigInfo
    {
       public static string _DbType;
       /// <summary>
       /// 数据库类型
       /// </summary>
       public static string DbType
       {
           get { if (_DbType == null) { return "2"; } return _DbType; }
           set { _DbType = value; }
       }
       
    }
}
