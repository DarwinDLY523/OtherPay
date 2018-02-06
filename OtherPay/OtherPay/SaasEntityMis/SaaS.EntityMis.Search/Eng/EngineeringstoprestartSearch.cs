// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_ENGINEERINGSTOPRESTARTSearch.cs
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
    public class EngineeringstoprestartSearch : BaseSearch
    {
        public int Type { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string FVERIFYSTATUS { get; set; }

        /// <summary>
        /// 申请类型：字典编号：eng_engineeringstoprestart
        ///1停工申请
        ///2复工申请
        /// </summary>
        public string FAPPLYTYPE { get; set; }
    }
}

