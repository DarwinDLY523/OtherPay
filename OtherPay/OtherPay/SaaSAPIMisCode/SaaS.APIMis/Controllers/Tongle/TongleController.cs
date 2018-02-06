
using SaaS.APIMis.HuiHe;
using SaaS.BLL;
using SaaS.EntityMis.Model.HuiHe;
using SaaS.Framework.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;

namespace SaaS.APIMis.Controllers.huihe
{
    public class TongleController : Controller
    {

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <returns></returns>
        public string notify_url()
        {
            return "success";
        }
        // GET: HuiHe
        public ActionResult payTo()
        {
              HuiHePayResult result = new HuiHePayResult();
            decimal amount = QueryStringHelper.GetDecimal("payAmount");
            int payType = QueryStringHelper.GetInt("payType");

            if (amount <= 0 || amount > 100000000)
            {
                result.Success = false;
                result.Message = "请输入正确的金额";
                return Json(result);
            }

            Random ran = new Random();
            int RandKey = ran.Next(1, 9999);
            string version = "1.0";
            string charset = "UTF-8";
            string merchant_id = "1026"; //商户号
            string out_trade_no = CodeHelper.OrderCode();
            string user_ip = "127.0.0.1";
            //string trade_type = "010008";
            string trade_type= QueryStringHelper.GetString("payType");
            string subject = "goods";
            string body = "goodsdesc";
            string user_id = "0";
            string total_fee = amount.ToString();
            string notify_url = "http://120.79.34.90/tongle/notify_url";
            string return_url = "http://120.79.34.90/tongle/return_url";

            string nonce_str = RandKey.ToString();
            string attach = "attach"; //用户自定义参数
            string biz_content = "{\"mch_app_id\":\"111\",\"device_info\":\"iOS_WAP\",\"ua\":\"111\",\"mch_app_name\":\"111\"}";
            biz_content = UrlEnCode.EnUniCode(biz_content);
            biz_content = Base64Encode(biz_content);
            string sign = ""; //签名


            ViewBag.IsImg = false;
          

            //创建支付应答对象
            var packageReqHandler = new RequestHandler(System.Web.HttpContext.Current);
            //初始化
            packageReqHandler.Init();

            //设置package订单参数
            #region 接收参数验证签名
            Hashtable parameters = new Hashtable();

            parameters = new Hashtable();
            parameters.Add("version", version);
            parameters.Add("charset", charset);
            parameters.Add("merchant_id", merchant_id);
            parameters.Add("out_trade_no", out_trade_no);
            parameters.Add("user_ip", user_ip);
            parameters.Add("trade_type", trade_type);
            parameters.Add("subject", subject);
            parameters.Add("body", body);
            parameters.Add("user_id", user_id);
            parameters.Add("total_fee", total_fee);

            parameters.Add("notify_url", notify_url);
            parameters.Add("return_url", return_url);


            parameters.Add("nonce_str", nonce_str);
            parameters.Add("attach", attach);
            parameters.Add("biz_content", biz_content);

            packageReqHandler.SetParameter("version", version);
            packageReqHandler.SetParameter("charset", charset);
            packageReqHandler.SetParameter("merchant_id", merchant_id);
            packageReqHandler.SetParameter("out_trade_no", out_trade_no);
            packageReqHandler.SetParameter("user_ip", user_ip);
            packageReqHandler.SetParameter("trade_type", trade_type);
            packageReqHandler.SetParameter("subject", subject);
            packageReqHandler.SetParameter("body", body);
            packageReqHandler.SetParameter("user_id", user_id);
            packageReqHandler.SetParameter("total_fee", total_fee);

            packageReqHandler.SetParameter("notify_url", notify_url);
            packageReqHandler.SetParameter("return_url", return_url);


            packageReqHandler.SetParameter("nonce_str", nonce_str);
            packageReqHandler.SetParameter("attach", attach);
            packageReqHandler.SetParameter("biz_content", biz_content);

            string url = "http://open.cs925.com/gateway/soa?";
            string sign_t = QueryStringHelper.GetSortQueryString(parameters);

            string signdata = sign_t + "&key=39c28ff223134077cbfb6eb50c8cda0e";
            sign = Encrypt(signdata).ToUpper();
            packageReqHandler.SetParameter("sign", sign);
            string data = packageReqHandler.ParseXML();
            url += sign_t;
            url += "&sign=" + sign;
            string xmlresult = PaySendBLL.PostTongleData(data, url);
            var resultData = GetXMLdata(xmlresult);
            if(resultData!=null&&!string.IsNullOrEmpty( resultData.pay_info))
            {
                result.Success = true;
                result.Data = resultData.pay_info;
            }
            else
            {
                if (resultData==null)
                {
                    resultData = new resultCode();
                    resultData.message = "解析数据失败";
                }
                
                result.Success = false;
                result.Message = resultData.message;
            }
            //Response.Redirect(url);
            #endregion

            return Json(result);

        }

        public class resultCode
        {
            public string pay_info { get; set; }

            /// <summary>
            /// 状态值
            /// </summary>
            public string status { get; set; }

            /// <summary>
            /// 返回消息
            /// </summary>
            public string message { get; set; }
        }

        public resultCode GetXMLdata(string xmlData)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xmlData);

            var rowNoteList = doc.SelectNodes("/xml");

            resultCode resultCode = new resultCode();
            if (rowNoteList != null)
            {
                foreach (XmlNode rowNode in rowNoteList)
                {
                    var fieldNodeList = rowNode.ChildNodes;
                    foreach (XmlNode courseNode in fieldNodeList)
                    {
                        if (courseNode.Name == "status")
                        {
                            resultCode.status = courseNode.InnerText.Trim();
                        }
                        if (courseNode.Name == "message")
                        {
                            resultCode.message = courseNode.InnerText.Trim();
                        }
                        if (courseNode.Name == "pay_info")
                        {
                            resultCode.pay_info = courseNode.InnerText.Trim();
                        }
                    }

                }
                return resultCode;
            }
            else
            {
                return null;
            }



        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="qrcode"></param>
        /// <returns></returns>
        public ActionResult GetQrCode(string qrcode)
        {
            BarCodeHelper.QRCode(qrcode);
            return View();
        }

        public string MD5code(string signString)
        {
            byte[] result = Encoding.Default.GetBytes(signString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }


        public static string EncryptUTF8(string source)
        {
            string md5String = string.Empty;
            try
            {
                byte[] byteCode = System.Text.Encoding.UTF8.GetBytes(source);
                byteCode = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(byteCode);

                for (int i = 0; i < byteCode.Length; i++)
                {
                    md5String += byteCode[i].ToString("x").PadLeft(2, '0');
                }
            }
            catch (Exception ex)
            {

                md5String = ex.ToString();
                return md5String;
            }
            return md5String;
        }


        //32位加密
        public static String Encrypt(String s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            string ret = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                ret += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return ret.PadLeft(32, '0');
        }
        // 16 位
        public static string Encrypt16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }


        public ActionResult pay()
        {
            return View();
        }


        /// <summary>  
        /// 获取IP  
        /// </summary>  
        /// <returns></returns>  
        private static string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            if (string.IsNullOrEmpty(ip))
                ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            return ip;
        }


        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(string source)
        {
            return Base64Encode(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(string result)
        {
            return Base64Decode(Encoding.UTF8, result);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encodeType">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
    }
}