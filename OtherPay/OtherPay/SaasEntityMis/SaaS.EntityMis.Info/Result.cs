
using SaaS.EntityMis.Enums;
using SaaS.Framework;
using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.EntityMis.Info
{
    [Serializable]
    public class Result : BaseResult
    {
        /// <summary>
        /// 初始化参数
        /// </summary>
        public Result()
        {
            Success = true;
            Code = "00";
            Message = "成功";

        }

        /// <summary>
        /// 直接返回成功数据
        /// </summary>
        /// <param name="data">数据</param>
        public Result(object data)
        {
            Data = data;
            Success = true;
            Code = "00";
            Message = "成功";
        }

        /// <summary>
        /// 失败返回结果
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误消息</param>
        public Result(string code, string message)
        {
            Code = code;
            Message = message;
            Success = false;
        }

        /// <summary>
        /// 成功提示信息
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误消息</param>
        public Result(string message, bool success)
        {
            Code = "00";
            Message = message;
            Success = success;
        }


        /// <summary>
        /// 返回结果
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误消息</param>
        public Result(string code, string message, bool success)
        {
            Code = code;
            Message = message;
            Success = success;
        }

        /// <summary>
        /// 失败返回结果，根据枚举返回错误信息
        /// </summary>
        /// <param name="enumName">Error 枚举</param>
        public Result(ErrorEnum.Error enumName)
        {
            Code = (int)enumName + "";
            Message = EnumDescription.GetRemark(enumName);
            Success = false;
        } 
    }
}
