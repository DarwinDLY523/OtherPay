using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.EntityMis.Enums.Eng
{
    public class BulidsEnum
    {
        #region 消息类型
        /// <summary>
        /// 消息类型：1系统消息2工地验收3催款通知4投诉消息5开工申请6工程派工7材料申请8材料变更9工程结算10借支申请11工地延期12工地评价13设计方案14施工巡检15工期安排
        /// </summary>
        public enum BuildEnum
        {
            /// <summary>
            /// 系统消息
            /// </summary>
            [EnumDescription("系统消息")]
            系统消息 = 1,
            /// <summary>
            /// 工地验收
            /// </summary>
            [EnumDescription("工地验收")]
            工地验收 = 2,
            /// <summary>
            /// 催款通知
            /// </summary>
            [EnumDescription("催款通知")]
            催款通知 = 3,
            /// <summary>
            /// 投诉消息
            /// </summary>
            [EnumDescription("投诉消息")]
            投诉消息 = 4,
            /// <summary>
            /// 开工申请
            /// </summary>
            [EnumDescription("开工申请")]
            开工申请 = 5,
            /// <summary>
            /// 工程派工
            /// </summary>
            [EnumDescription("工程派工")]
            工程派工 = 6,
            /// <summary>
            /// 材料申请
            /// </summary>
            [EnumDescription("材料申请")]
            材料申请 = 7,
            /// <summary>
            /// 材料变更
            /// </summary>
            [EnumDescription("材料变更")]
            材料变更 = 8,
            /// <summary>
            /// 工程结算
            /// </summary>
            [EnumDescription("工程结算")]
            工程结算 = 9,
            /// <summary>
            /// 借支申请
            /// </summary>
            [EnumDescription("借支申请")]
            借支申请 = 10,
            /// <summary>
            /// 工地延期
            /// </summary>
            [EnumDescription("工地延期")]
            工地延期 = 11,
            /// <summary>
            /// 工地评价
            /// </summary>
            [EnumDescription("工地评价")]
            工地评价 = 12,
            /// <summary>
            /// 设计方案
            /// </summary>
            [EnumDescription("设计方案")]
            设计方案 = 13,
            /// <summary>
            /// 施工巡检
            /// </summary>
            [EnumDescription("施工巡检")]
            施工巡检 = 14,
            /// <summary>
            /// 工期安排
            /// </summary>
            [EnumDescription("工期安排")]
            工期安排 = 15,
            /// <summary>
            /// 工期安排
            /// </summary>
            [EnumDescription("施工现场图")]
            施工现场图 = 16,
            /// <summary>
            /// 工地停工
            /// </summary>
            [EnumDescription("工地停工")]
            工地停工 = 17,
        }
        #endregion



        #region 工程进度
        /// <summary>
        /// 工程进度：///1形象工程
        ///2水电工程
        ///3瓦木工程
        ///竣工工程
        /// </summary>
        public enum BuildprogressEnum
        {
            /// <summary>
            /// 形象工程
            /// </summary>
            [EnumDescription("形象工程")]
            形象工程 = 1,
            /// <summary>
            /// 水电工程
            /// </summary>
            [EnumDescription("水电工程")]
            水电工程 = 2,
            /// <summary>
            /// 瓦木工程
            /// </summary>
            [EnumDescription("瓦木工程")]
            瓦木工程 = 3,
            /// <summary>
            /// 竣工工程
            /// </summary>
            [EnumDescription("竣工工程")]
            竣工工程 = 4,

        }
        #endregion


        #region 成员类型
        /// <summary>
        /// 成员类型：字典编号：eng_sitemember_membertype
        ///1品质总经理（全国交付总经理）（数据权限）
        ///2总督察（全国交付总助）（数据权限）

        ///3大区品质总经理（大区交付助理）（数据权限）
        ///4大区总经理（数据权限）

        ///5分公司总经理（数据权限）
        ///6分公司督察（数据权限）

        ///7区督查（数据权限）--删除
        ///8区总（数据权限）

        ///9区工程助理（交付助理）（（数据权限）
        ///10区工程经理（交付经理）（（数据权限）
        ///11区工程主管（高级主管）（数据权限）
        ///12区材料主管 （商品经理）（数据权限）

        ///13项目经理（默认加入）
        ///14工程监理（普通管家）（默认加入）
        ///15设计师 （默认加入） 

        ///16其他
        ///17业主


        ///18客服（默认加入）
        ///19设计师经理
        ///20网络部经理
        ///21分公司副经理
        ///22交付总经理
        /// </summary>
        public enum BuilduserTypeEnum
        {
            /// <summary>
            /// 全国交付总经理
            /// </summary>
            [EnumDescription("全国交付总经理")]
            全国交付总经理 = 1,
            /// <summary>
            /// 全国交付总助
            /// </summary>
            [EnumDescription("全国交付总助")]
            全国交付总助 = 2,
            /// <summary>
            /// 大区交付助理
            /// </summary>
            [EnumDescription("大区交付助理")]
            大区交付助理 = 3,
            /// <summary>
            /// 大区总经理
            /// </summary>
            [EnumDescription("大区总经理")]
            大区总经理 = 4,
            /// <summary>
            /// 分公司总经理
            /// </summary>
            [EnumDescription("分公司总经理")]
            分公司总经理 = 5,
            /// <summary>
            /// 分公司督察
            /// </summary>
            [EnumDescription("分公司督察")]
            分公司督察 = 6,
            /// <summary>
            /// 区督查
            /// </summary>
            [EnumDescription("区督查")]
            区督查 = 7,
            /// <summary>
            /// 区总
            /// </summary>
            [EnumDescription("区总")]
            区总 = 8,
            /// <summary>
            /// 交付助理
            /// </summary>
            [EnumDescription("交付助理")]
            交付助理 = 9,
            /// <summary>
            /// 交付经理
            /// </summary>
            [EnumDescription("交付经理")]
            交付经理 = 10,
            /// <summary>
            /// 高级主管
            /// </summary>
            [EnumDescription("高级管家")]
            高级管家 = 11,
            /// <summary>
            /// 商品经理
            /// </summary>
            [EnumDescription("商品经理")]
            商品经理 = 12,
            /// <summary>
            /// 项目经理
            /// </summary>
            [EnumDescription("项目经理")]
            项目经理 = 13,
            /// <summary>
            /// 普通管家
            /// </summary>
            [EnumDescription("普通管家")]
            普通管家 = 14,
            /// <summary>
            /// 设计师
            /// </summary>
            [EnumDescription("设计师")]
            设计师 = 15,
            /// <summary>
            /// 其他
            /// </summary>
            [EnumDescription("其他")]
            其他 = 16,
            /// <summary>
            /// 业主
            /// </summary>
            [EnumDescription("业主")]
            业主 = 17,
            /// <summary>
            /// 客服
            /// </summary>
            [EnumDescription("客服")]
            客服 = 18,
            /// <summary>
            /// 设计师经理
            /// </summary>
            [EnumDescription("设计师经理")]
            设计师经理 = 19,
            /// <summary>
            /// 网络部经理
            /// </summary>
            [EnumDescription("网络部经理")]
            网络部经理 = 20,
            /// <summary>
            /// 分公司副经理
            /// </summary>
            [EnumDescription("分公司副经理")]
            分公司副经理 = 21,
            /// <summary>
            /// 交付总经理
            /// </summary>
            [EnumDescription("交付总经理")]
            交付总经理 = 22,

        }
        #endregion


        #region 申请开工类型
        /// <summary>
        /// 申请开工类型：///1设计师申请，2客户申请
        /// </summary>
        public enum BuildapplyusertypeEnum
        {
            /// <summary>
            /// 设计师
            /// </summary>
            [EnumDescription("设计师")]
            设计师 = 1,
            /// <summary>
            /// 水电工程
            /// </summary>
            [EnumDescription("业主")]
            业主 = 2,

        }
        #endregion

        #region 客户申请开工审核状态

        public enum CustomerVerifyStatusEnum
        {

            [EnumDescription("待客户申请")]
            待客户申请 = 1,
            [EnumDescription("待审核")]
            待审核 = 2,
            [EnumDescription("审核通过")]
            审核通过 = 3,
            [EnumDescription("审核不通过")]
            审核不通过 = 4,
        }

        #endregion

        #region 服务号消息显示
        public enum MpMessageTypeEnum
        {
            [EnumDescription("系统消息")]
            系统消息 = 1,
            [EnumDescription("施工验收")]
            施工验收 = 2,
            [EnumDescription("我的投诉")]
            我的投诉 = 4,
            [EnumDescription("延期停工")]
            延期停工 = 11,
            [EnumDescription("工地评价")]
            工地评价 = 12,
            [EnumDescription("设计方案")]
            设计方案 = 13,
            [EnumDescription("施工现场")]
            施工现场 = 16,

        }

        #endregion

        #region 投诉类型
        public enum ComplaintTypeEnum
        {
            [EnumDescription("施工质量")]
            施工质量 = 1,
            [EnumDescription("施工进度")]
            施工进度 = 2,
            [EnumDescription("服务")]
            服务 = 3,
            [EnumDescription("建材")]
            建材 = 4,
            [EnumDescription("家具")]
            家具 = 5,
            [EnumDescription("家电")]
            家电 = 6,
            [EnumDescription("售后")]
            售后 = 7,
            [EnumDescription("其他")]
            其他 = 8,
        }
        #endregion

        #region 延期审核状态
        /// <summary>
        /// 延期审核状态
        /// </summary>
        public enum BuildDelayVerifystatus
        {
            ///1待提交审批
            ///2待普通管家审批
            ///3待交付助理审批
            ///4待督察审批
            ///5审核通过
            ///6审核不通过
            [EnumDescription("待提交审批")]
            待提交审批 = 1,
            [EnumDescription("待普通管家审批")]
            待普通管家审批 = 2,
            [EnumDescription("待交付助理审批")]
            待交付助理审批 = 3,
            [EnumDescription("待督察审批")]
            待督察审批 = 4,
            [EnumDescription("审核通过")]
            审核通过 = 5,
            [EnumDescription("审核不通过")]
            审核不通过 = 6,

        }
        #endregion

        #region 催款单类型
        /// <summary>
        /// 催款单类型
        /// </summary>
        public enum BuildprogressMoneyEnum
        {
            /// <summary>
            /// 二期款
            /// </summary>
            [EnumDescription("二期款")]
            二期款 = 2,
            /// <summary>
            /// 三期款
            /// </summary>
            [EnumDescription("三期款")]
            三期款 = 3,
            /// <summary>
            /// 尾款
            /// </summary>
            [EnumDescription("尾款")]
            尾款 = 4,

        }
        #endregion

        #region 合同变更审核状态
        /// <summary>
        /// 合同变更审核状态
        /// </summary>
        public enum BuildContractChangeEnum
        {
            /// <summary>
            /// 待项目经理审批
            /// </summary>
            [EnumDescription("待项目经理审批")]
            待项目经理审批 = 2,
            /// <summary>
            /// 待普通管家审批
            /// </summary>
            [EnumDescription("待普通管家审批")]
            待普通管家审批 = 3,
            /// <summary>
            /// 待交付助理审批
            /// </summary>
            [EnumDescription("待交付助理审批")]
            待交付助理审批 = 4,
            /// <summary>
            /// 审核通过
            /// </summary>
            [EnumDescription("审核通过")]
            审核通过 = 5,
            /// <summary>
            /// 审核不通过
            /// </summary>
            [EnumDescription("审核不通过")]
            审核不通过 = 6,

        }
        #endregion


        ///1保存=未提交
        ///2待财务审核
        ///3审核通过==已结算
        ///4审核不通过
        ///1 2 4 为未结算的，3为结算的
        #region 工程结算审核状态
        public enum EngSettlementEnum
        {
            /// <summary>
            /// 未提交
            /// </summary>
            [EnumDescription("未提交")]
            未提交 = 1,
            /// <summary>
            /// 待财务审核
            /// </summary>
            [EnumDescription("待财务审核")]
            待财务审核 = 2,
            /// <summary>
            /// 已结算
            /// </summary>
            [EnumDescription("已结算")]
            已结算 = 3,
            /// <summary>
            /// 审核不通过
            /// </summary>
            [EnumDescription("审核不通过")]
            审核不通过 = 4,
        }
        #endregion
    }
}
