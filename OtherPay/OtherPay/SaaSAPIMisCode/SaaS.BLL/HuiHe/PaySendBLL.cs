using SaaS.APIMis.BLL;
using SaaS.EntityMis.Model.HuiHe;
using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaaS.APIMis.HuiHe
{
    public class PaySendBLL
    {
        /// <summary>
        /// post 支付数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HuiHePayResult PostData(string data)
        {
           string server=  ConfigHelper.GetSettings("HuiHeSend"); 
           var result = SendRequest.Post<HuiHePayResult>("", data, server );


            return result;
        }

        /// <summary>
        /// post 支付数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PostTongleData(string data,string url)
        {
          
            var result = SendRequest.Post(url, data);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string PostData(string url, string data)
        {
            var result = SendRequest.Post(url, data);
            return result;
        }
    }
}
