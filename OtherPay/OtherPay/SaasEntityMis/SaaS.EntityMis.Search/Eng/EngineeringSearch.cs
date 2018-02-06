// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_ENGINEERINGSearch.cs
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
    public class EngineeringSearch : BaseSearch
    {
        /// <summary>
        /// 列表
        /// </summary>
        public int Type { get; set; }

        #region 工地管家查询字段
        /// <summary>
        /// 设计师ID
        /// </summary>
        public string FDesignerID { get; set; }
        /// <summary>
        /// 工程状态
        /// </summary>
        public string FStatus { get; set; }
        /// <summary>
        /// 员工类型
        /// </summary>
        public string FMemberType { get; set; }
        /// <summary>
        /// 查询名称
        /// </summary>
        public string searchName { get; set; }
        /// <summary>
        /// 是否选择模板
        /// </summary>
        public string FDurationTemplate { get; set; }
        /// <summary>
        /// 开工工期审核状态
        /// </summary>
        public string FStartLimitVerifyStatus { get; set; }
        /// <summary>
        /// 工程阶段
        /// </summary>
        public string FStage { get; set; }

        /// <summary>
        /// 是否含有包含邀请加入的工地 （0：不包含 1：包含）
        /// </summary>
        public int IsInvitation { get; set; }

        /// <summary>
        /// 派工确认状态
        /// </summary>
        public string FConfirmStatus { get; set; }

        /// <summary>
        /// 楼盘城市ID
        /// </summary>
        public string FCityID { get; set; }

        #endregion

        #region 项目上货查询类型
        /// <summary>
        /// 项目上货查询类型1辅材2商贸
        /// </summary>
        public string searchType { get; set; }
        #endregion

    }
}

