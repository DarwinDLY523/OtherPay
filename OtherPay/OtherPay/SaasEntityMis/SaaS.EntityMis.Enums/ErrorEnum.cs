
using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.EntityMis.Enums
{
    public class ErrorEnum
    {
        public enum Error
        {
            #region 系统错误 error 1000~1099

            /// <summary>
            /// 网络异常
            /// </summary>
            [EnumDescription("网络异常")]
            error_network = 1000,

            /// <summary>
            /// 序列化错误
            /// </summary>
            [EnumDescription("序列化错误")]
            error_serialization = 1001,

            /// <summary>
            /// 程序异常
            /// </summary>
            [EnumDescription("程序异常")]
            error_program = 1002,

            /// <summary>
            /// 没有找到数据
            /// </summary>
            [EnumDescription("没有找到数据")]
            error_nodata = 1003,

            /// <summary>
            /// 数据库的数据错误
            /// </summary>
            [EnumDescription("数据库的数据错误")]
            error_sqldata = 1004,

            /// <summary>
            /// 系统缓存错误
            /// </summary>
            [EnumDescription("设置系统缓存错误")]
            error_setcache = 1005,

            /// <summary>
            /// 反序列化错误
            /// </summary>
            [EnumDescription("反序列化错误")]
            error_deserialization = 1006,


            [EnumDescription("无效的链接")]
            error_URL = 1007,

            [EnumDescription("添加错误")]
            error_Add = 1008,

            [EnumDescription("修改错误")]
            error_Update = 1009,

            [EnumDescription("删除错误")]
            error_Delete = 1010,

            [EnumDescription("保存错误")]
            error_Save = 1011,

            [EnumDescription("审核错误")]
            error_Audit = 1012,

            [EnumDescription("操作错误")]
            error_Oper = 1013,
            #endregion

            #region 参数错误  1201~1299

            /// <summary>
            /// 缺少必选参数
            /// </summary>
            [EnumDescription("缺少必选参数")]
            parameter_missing = 1201,

            /// <summary>
            /// 参数格式不正确
            /// </summary>
            [EnumDescription("参数格式不正确")]
            parameter_format_error = 1202,

            /// <summary>
            /// 参数类型错误
            /// </summary>
            [EnumDescription("参数类型错误")]
            parameter_type_error = 1203,

            /// <summary>
            /// 参数缺少分页大小
            /// </summary>
            [EnumDescription("参数缺少分页大小")]
            parameter_nopagesize = 1204,

            /// <summary>
            /// 参数无效
            /// </summary>
            [EnumDescription("参数无效")]
            parameter_value_error = 1205,



            /// <summary>
            /// 缺少城市ID
            /// </summary>
            [EnumDescription("缺少城市ID")]
            parameter_cityid_missing = 1207,

            /// <summary>
            /// 缺少用户ID
            /// </summary>
            [EnumDescription("缺少用户ID")]
            parameter_userId_mis = 1208,

            /// <summary>
            /// 缺少主键ID
            /// </summary>
            [EnumDescription("缺少主键ID")]
            parameter_keyId_mis = 1209,

            /// <summary>
            /// 参数长度过长（溢出）
            /// </summary>
            [EnumDescription("参数长度过长（溢出）")]
            parameter_overflow = 1210,

            /// <summary>
            /// 缺少链接
            /// </summary>
            [EnumDescription("缺少链接")]
            parameter_url_mis = 1211,

            /// <summary>
            /// 签名认证失败
            /// </summary>
            [EnumDescription("签名认证失败")]
            parameter_sign_fail = 1212,
            #endregion

            #region 账户错误 1300~1399

            /// <summary>
            /// 请您先登录
            /// </summary>
            [EnumDescription("请您先登录")]
            passport_unlogin = 1300,

            /// <summary>
            /// 账号不存在
            /// </summary>
            [EnumDescription("账号不存在")]
            passport_acccountnotexist = 1301,

            /// <summary>
            /// 账号已存在
            /// </summary>
            [EnumDescription("账号已存在")]
            passport_accountalreadyexists = 1302,

            /// <summary>
            /// 密码错误
            /// </summary>
            [EnumDescription("密码错误")]
            passport_pwderror = 1303,

            /// <summary>
            /// 账号已锁定
            /// </summary>
            [EnumDescription("账号已锁定")]
            passport_locked = 1304,

            /// <summary>
            /// 账号已禁用
            /// </summary>
            [EnumDescription("账号已禁用")]
            passport_forbidden = 1305,

            /// <summary>
            /// 修改密码操作失败
            /// </summary>
            [EnumDescription("修改密码操作失败")]
            passport_changefailed = 1306,


            /// <summary>
            /// 修改密码时，原密码验证错误
            /// </summary>
            [EnumDescription("原密码错误")]
            failed_passport_pwd = 1307,

            /// <summary>
            /// 凭据错误或者失效
            /// </summary>
            [EnumDescription("凭据错误或者失效")]
            error_password_proof = 1308,

            /// <summary>
            /// 用户名不能为空
            /// </summary>
            [EnumDescription("用户名不能为空")]
            error_account_empty = 1309,

            /// <summary>
            /// 用户名只能是手机
            /// </summary>
            [EnumDescription("用户名只能是手机")]
            error_account_format_ = 1310,

            /// <summary>
            /// 用户名长度超过50
            /// </summary>
            [EnumDescription("用户名长度超过50")]
            error_account_long = 1311,

            /// <summary>
            /// 密码长度超过20
            /// </summary>
            [EnumDescription("密码长度超过20")]
            error_password_long = 1312,

            /// <summary>
            /// 密码长度不能小于6
            /// </summary>
            [EnumDescription("密码长度不能小于6")]
            error_password_short = 1313,




            /// <summary>
            /// 一小时错误超过3次,请输入验证码和key
            /// </summary>
            [EnumDescription("请输入验证码")]
            error_longin_count = 1314,

            /// <summary>
            /// 图片验证码错误
            /// </summary>
            [EnumDescription("验证码错误")]
            error_code = 1315,

            /// <summary>
            /// 请输入验证码
            /// </summary>
            [EnumDescription("请输入验证码")]
            error_nocode = 1316,

            /// <summary>
            /// 登陆失败
            /// </summary>
            [EnumDescription("登陆失败")]
            error_login_fail = 1316,

            /// <summary>
            /// 登陆失败
            /// </summary>
            [EnumDescription("用户手机号码为空无法发送验证码")]
            error_login_mobilenull = 1318,

            #endregion

            #region 邮件短信 1400~1499

            /// <summary>
            /// 发送邮件错误
            /// </summary>
            [EnumDescription("发送邮件错误")]
            error_sendemail = 1401,

            /// <summary>
            /// 发送短信错误
            /// </summary>
            [EnumDescription("发送短信错误")]
            error_sendmessage = 1402,

            /// <summary>
            /// 获取邮件模板失败
            /// </summary>
            [EnumDescription("获取邮件模板失败")]
            error_getemailtemp = 1403,

            /// <summary>
            /// 手机号格式错误
            /// </summary>
            [EnumDescription("手机号格式错误")]
            error_mobileformat = 1404,

            /// <summary>
            /// 获取短信模板失败
            /// </summary>
            [EnumDescription("获取短信模板失败")]
            failed_temp_sendmobilecode = 1405,

            /// <summary>
            /// 短信验证码错误
            /// </summary>
            [EnumDescription("短信验证码错误")]
            error_mobilecode = 1406,

            /// <summary>
            /// 短信验证码凭据失效或者错误
            /// </summary>
            [EnumDescription("验证码已失效或者错误")]
            error_mobilekeycode = 1407,

            /// <summary>
            /// 邮件凭据失效或者错误
            /// </summary>
            [EnumDescription("邮件凭据失效或者错误")]
            error_emailekeycode = 1408,

            [EnumDescription("请1分钟后重新发送")]
            error_allreadymobliekey = 1409,

            [EnumDescription("找回类型和账号不匹配")]
            error_match_mobileemail = 1410,
            [EnumDescription("邮件格式错误")]
            error_email = 1411,
            [EnumDescription("账号已经激活")]
            error_allreadyactive = 1412,
            #endregion

            #region 权限 2000-2099
            /// <summary>
            /// 没有权限
            /// </summary>
            [EnumDescription("没有数据权限")]
            noPower = 2000,

            [EnumDescription("角色名称已经存在")]
            RoleNameExists = 2001,
            #endregion

            #region 系统菜单 Sys_Menu 6000-6999
            /// <summary>
            /// 该菜单存在子菜单，不能删除
            /// </summary>
            [EnumDescription("该菜单存在子菜单，不能删除")]
            menuDelError = 6000,
            #endregion

            #region 操作成功  7000-7999
            [EnumDescription("添加成功")]
            success_Add = 7000,

            [EnumDescription("修改成功")]
            success_Update = 7001,

            [EnumDescription("删除成功")]
            success_Delete = 7002,

            [EnumDescription("保存成功")]
            success_Save = 7003,

            [EnumDescription("审核操作成功")]
            success_Audit = 7004,

            [EnumDescription("操作成功")]
            success_Oper = 7005,
            #endregion

            #region 合同相关 8000-8999
            [EnumDescription("缺少公司收款比例信息")]
            ERROR_CONTRACT_NORECEIVABLESRATIO = 8000,

            [EnumDescription("缺少公司标准设计费信息")]
            ERROR_NAMELIST_NODESIGNFEE = 8001,

            [EnumDescription("缺少合同收款计划")]
            ERROR_CONTRACT_NORECEIVABLERATIO = 8002,

            #endregion

        }
    }
}
