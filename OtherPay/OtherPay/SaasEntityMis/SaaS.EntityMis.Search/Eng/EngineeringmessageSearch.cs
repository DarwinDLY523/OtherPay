// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_ENGINEERINGMESSAGESearch.cs
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
    public class EngineeringmessageSearch : BaseSearch
    {
        /// <summary>
        /// 消息类型
        ///0:除施工现场图外所有消息（服务号）
        ///1系统消息
        ///2工地验收
        ///3催款通知
        ///4投诉消息
        ///5开工申请
        ///6工程派工
        ///7材料申请
        ///8材料变更
        ///9工程结算
        ///10借支申请
        ///11工地延期
        ///12工地评价
        ///13设计方案
        ///14施工巡检
        ///15工期安排
        ///16施工现场图
        /// </summary>
        public int FMessageType { get; set; }

        /// <summary>
        /// 成员id
        /// </summary>
        public string FsiteMemberId { get; set; }

        /// <summary>
        /// 工程阶段
        /// </summary>
        public int Fstage { get; set; }
        /// <summary>
        /// 工程id
        /// </summary>
        public string FEngineeringID { get; set; }

        /// <summary>
        /// 多个消息类型，以“,”分割
        /// </summary>
        public string FMessageTypes { get; set; }

    }
}

