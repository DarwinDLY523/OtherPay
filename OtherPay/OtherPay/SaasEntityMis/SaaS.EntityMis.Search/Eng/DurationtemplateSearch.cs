// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_DURATIONTEMPLATESearch.cs
// 项目名称：SaaS.EntityMis.Search
// 创建时间：2016/11/23
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaS.Framework;

namespace SaaS.EntityMis.Search
{
    public class DurationtemplateSearch : BaseSearch
    {
        /// <summary>
        /// 行政组织ID
        /// </summary>
        public string FAdminId { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string FISENABLE { get; set; }
    }
}

