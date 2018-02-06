
using SaaS.APIMis.HuiHe;
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

namespace SaaS.APIMis.Controllers.huihe
{
    public class HuiHeController : Controller
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
            ViewBag.IsImg = false;
            HuiHePayResult result = new HuiHePayResult();
            decimal amount = QueryStringHelper.GetDecimal("payAmount");
            int payType = QueryStringHelper.GetInt("payType");

            if (amount <= 0 || amount > 100000000)
            {
                result.Success = false;
                result.Message = "请输入正确的金额";
                return View(result);
            }
            string AppId = "201711181043221684";//appid
            string Method = "trade.page.pay";//接口名称
            string Format = "JSON";
            string Charset = "UTF-8";
            string Version = "1.0";
            string SignType = "MD5"; //不参与加密
            string sign = "";
            string Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");////发送请求的时间，格式"yyyy -MM-dd HH:mm:ss"
            int PayType = payType;//2 - 微信扫码      3 - QQ钱包 11 - QQWAP 6 - 支付宝扫码高费率 7 - 微信WAP 8 - 百度钱包 9 - 京东钱包
            //10 - 银联钱包
            //string BankCode = ""; //空字符串不支持加密
            string OutTradeNo = CodeHelper.OrderCode();
            string TotalAmount = amount.ToString();
            string Subject = "test";
            string Body = "testpay";
            string NotifyUrl = "http://www.baidu.com/NotifyUrl";
            #region 接收参数验证签名
            Hashtable parameters = new Hashtable();

            parameters = new Hashtable();
            parameters.Add("AppId", AppId);
            parameters.Add("Method", Method);
            parameters.Add("Format", Format);
            parameters.Add("Charset", Charset);
            parameters.Add("Version", Version);
            //parameters.Add("SignType", SignType);
            parameters.Add("Timestamp", Timestamp);
            parameters.Add("PayType", PayType.ToString());
            // parameters.Add("BankCode", BankCode);
            parameters.Add("OutTradeNo", OutTradeNo);
            parameters.Add("TotalAmount", TotalAmount);
            parameters.Add("Subject", Subject);

            parameters.Add("Body", Body);
            parameters.Add("NotifyUrl", NotifyUrl);

            string sign_t = QueryStringHelper.GetSortQueryString(parameters);
            //sign_t = sign_t + "&key=" + "43ed65d777ff25b83eabe750d1193e29";
            sign_t = sign_t + "43ed65d777ff25b83eabe750d1193e29";
            sign = Encrypt(sign_t).ToUpper();
            string postdata = string.Format("AppId={0}&Method={1}&Format={2}&Charset={3}&Version={4}&SignType={5}&Sign={6}",
                AppId, Method, Format, Charset, Version, SignType, sign);
            postdata += string.Format("&Timestamp={0}&PayType={1}&OutTradeNo={2}&TotalAmount={3}&Subject={4}&Body={5}&NotifyUrl={6}",
                Timestamp, PayType, OutTradeNo, TotalAmount, Subject, Body, NotifyUrl);

            result = PaySendBLL.PostData(postdata);
            #endregion

            result.Data = postdata;
            if ((payType == 2) && result.Code == "0")//扫码支付
            {
                //BarCodeHelper.QRCode(result.Qrcode);
                ViewBag.IsImg = true;
            }
            return View(result);

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
    }
}