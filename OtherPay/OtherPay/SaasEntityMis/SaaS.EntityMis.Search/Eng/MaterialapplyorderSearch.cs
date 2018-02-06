// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_MATERIALAPPLYORDERSearch.cs
// 项目名称：SaaS.EntityMis.Search
// 创建时间：2016/12/17
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaS.Framework;

namespace SaaS.EntityMis.Search
{
    public class MaterialapplyorderSearch : BaseSearch
    {
        /// <summary>
        /// 材料单统一类型
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string FOrderType { get; set; }
        /// <summary>
        /// 工程ID
        /// </summary>
        public string FEngineeringID { get; set; }
    }
}

