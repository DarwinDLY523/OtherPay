// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_STAGEEVALUATESearch.cs
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
    public class StageevaluateSearch : BaseSearch
    {
        /// <summary>
        /// 职位
        /// </summary>
        public int positionType { get; set; }

        /// <summary>
        /// 工程阶段
        /// </summary>
        public int fstage { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string staffID { get; set; }

        /// <summary>
        /// 工程id
        /// </summary>
        public string FEngineeringID { get; set; }
    }
}

