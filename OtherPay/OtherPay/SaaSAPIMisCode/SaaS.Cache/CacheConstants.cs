
using SaaS.Framework.Common;

namespace SaaS.Cache
{
    /// <summary>
    /// 页 面 名：缓存常量定义
    /// 说    明：所有人不允许各自定义缓存，所有缓存key的常量必须在此定义（除GUID会话信息）；

    internal class CacheConstants
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        public static  string preFix = ConfigHelper.GetSettings("preFix");

        /// <summary>
        /// 登陆tokenid
        /// </summary>
        public const string tokenid = "tokenid_";


        #region 用户数据权限
        /// <summary>
        /// 租户权限，格式  "ID","ID","ID"
        /// </summary>
        public static   string lesseePower = preFix+ "lesseePower_";
        /// <summary>
        /// 平台客户权限，格式  "ID","ID","ID"
        /// </summary>
        public static string plaCustomerPower = preFix + "plaCustomerPower_";
        #endregion

        #region 物料分类
        /// <summary>
        /// 物料分类  缓存key 不能和订单收银项目重复
        /// </summary>
        public static string materialGroup = preFix + "CrmmaterialGroup_";

        /// <summary>
        /// 物料分类IDs
        /// </summary>
        public static string materialGroupIds = preFix + "CrmmaterialGroupIds_";
        #endregion

        #region 微信配置缓存
        /// <summary>
        /// 微信配置
        /// </summary>
        public static string wechat = preFix + "wechat_";

        /// <summary>
        /// 微信消息模版
        /// </summary>
        public static string messageTemplate = preFix + "messageTemplate_";

        /// <summary>
        /// 微信企业号应用配置
        /// </summary>
        public static string wechatQyAgentId = preFix + "wechatqyagentid_";
        #endregion

        #region 系统配置
        /// <summary>
        /// 职位配置
        /// </summary>
        public static string position = preFix + "position_";

      
        #endregion

        #region 组织权限缓存
        /// <summary>
        /// 员工信息
        /// </summary>
        public static string person = preFix + "person_";
        #endregion
        
        #region 首页缓存
        #region 日常公告
        public static string dailybulletin = preFix + "dailybulletin_";
        #endregion

        #region 待办事宜
        public static string todoaccept = preFix + "todoaccept_";
        #endregion

        #region 待办事宜回复
        public static string todo = preFix + "todo_";
        #endregion

        #region 期数信息
        public static string periodnumber = preFix + "periodnumber_";
        #endregion

        #endregion
    }
}
