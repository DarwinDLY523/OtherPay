// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_ENGINEERINGDELAYSearch.cs
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
    public class EngineeringdelaySearch : BaseSearch
    {
        public int Type { get; set; }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string FRoleNumber { get; set; }

        /// <summary>
        /// 审核类型 
        ///1待提交审批
        ///2待工程监理审批
        ///3待工程助理审批
        ///4待督察审批
        ///5审核通过
        ///6审核不通过
        ///以',分割'
        /// </summary>
        public string  statusType { get; set; }
    }
}

