// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_EVALUATESearch.cs
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
    public class EvaluateSearch : BaseSearch
    {
        /// <summary>
        /// 城市id
        /// </summary>
        public string FcityId { get; set; }

        /// <summary>
        /// 工地名称
        /// </summary>
        public string groupName { get; set; }
    }
}

