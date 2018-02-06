using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SaaS.BLL
{
   public  class RequestHandler
    {

       protected System.Web.HttpContext httpContext;
           private string key;

           /** 请求的参数 */
           protected Hashtable parameters;

           public RequestHandler(HttpContext httpContext)
           {
               parameters = new Hashtable();

               this.httpContext = httpContext;
           }


           /** 初始化函数 */

           public virtual void Init()
           {
           }


           /** 获取密钥 */

           public String GetKey()
           {
               return key;
           }

           /** 设置密钥 */

           public void SetKey(string key)
           {
               this.key = key;
           }

           /** 设置参数值 */
           public void SetParameter(string parameter, string parameterValue)
           {
               if (parameter != null && parameter != "")
               {
                   if (parameters.Contains(parameter))
                   {
                       parameters.Remove(parameter);
                   }

                   parameters.Add(parameter, parameterValue);
               }
           }

           /// <summary>
           /// 获取参数
           /// </summary>
           /// <returns></returns>
           public Hashtable GetParameters()
           {
               return parameters;
           }

           //创建package签名
           public virtual string CreateMd5Sign(string key, string value)
           {
               var sb = new StringBuilder();

               var akeys = new ArrayList(parameters.Keys);
               akeys.Sort();

               foreach (string k in akeys)
               {
                   var v = (string)parameters[k];
                   if (null != v && "".CompareTo(v) != 0
                       && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                   {
                       sb.Append(k + "=" + v + "&");
                   }
               }

               sb.Append(key + "=" + value);
               //return sb.ToString();


               string sign = TenpayMd5.GetMD5(sb.ToString(), getCharset()).ToUpper();

               return sign;
           }

           public virtual string CreateMd5Sign(string key, string value, out string signStr)
           {
               var sb = new StringBuilder();

               var akeys = new ArrayList(parameters.Keys);
               akeys.Sort();

               foreach (string k in akeys)
               {
                   var v = (string)parameters[k];
                   if (null != v && "".CompareTo(v) != 0
                       && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                   {
                       sb.Append(k + "=" + v + "&");
                   }
               }

               sb.Append(key + "=" + value);
               //return sb.ToString();

               signStr = sb.ToString();
               string sign = TenpayMd5.GetMD5(sb.ToString(), getCharset()).ToUpper();

               return sign;
           }

           //输出XML
           public string ParseXML()
           {
               var sb = new StringBuilder();
               sb.Append("<xml>");
               var akeys = new ArrayList(parameters.Keys);
               foreach (string k in akeys)
               {
                   var v = (string)parameters[k];
                   if (Regex.IsMatch(v, @"^[0-9.]$"))
                   {
                       sb.Append("<" + k + ">" + v + "</" + k + ">");
                   }
                   else
                   {
                       sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                   }
               }
               sb.Append("</xml>");
               return sb.ToString();
           }



           protected virtual string getCharset()
           {
               return httpContext.Request.ContentEncoding.BodyName;
           }
       
    }
}
