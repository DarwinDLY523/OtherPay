// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_ENGINEERINGBORROWSearch.cs
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
    public class EngineeringborrowSearch : BaseSearch
    {
        /// <summary>
        /// 审核状态，多个值以","分割
        /// </summary>
        public string FVerifyStatus { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string searchType { get; set; }
    }
}

