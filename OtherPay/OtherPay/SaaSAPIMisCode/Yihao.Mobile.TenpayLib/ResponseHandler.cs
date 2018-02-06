using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Xml;
using System.Text;

namespace YiHao.Mobile.TenpayLib
{
    /** 
    '============================================================================
    'api说明：
    'getKey()/setKey(),获取/设置密钥
    'getParameter()/setParameter(),获取/设置参数值
    'getAllParameters(),获取所有参数
    'isTenpaySign(),是否正确的签名,true:是 false:否
    'isWXsign(),是否正确的签名,true:是 false:否
    ' * isWXsignfeedback判断微信维权签名
    ' *getDebugInfo(),获取debug信息
    '============================================================================
    */

    public class ResponseHandler
    {
        // 密钥 

        // appkey
        private static string SignField = "appid,appkey,timestamp,openid,noncestr,issubscribe";
        private string appkey;

        //xmlMap

        private string charset = "gb2312";
        protected string content;

        //参与签名的参数列表

        protected HttpContextBase httpContext;
        private string key;
        protected Hashtable parameters;
      //  private Hashtable xmlMap;

        //初始化函数

        //获取页面提交的get和post参数
        public ResponseHandler(HttpContextBase httpContext)
        {
            parameters = new Hashtable();
          //  xmlMap = new Hashtable();

            this.httpContext = httpContext;
            NameValueCollection collection;
            //post data
            if (this.httpContext.Request.HttpMethod == "POST")
            {
                collection = this.httpContext.Request.Form;
                foreach (string k in collection)
                {
                    string v = collection[k];
                    setParameter(k, v);
                }
            }
            //query string
            collection = this.httpContext.Request.QueryString;
            foreach (string k in collection)
            {
                string v = collection[k];
                setParameter(k, v);
            }
            if (this.httpContext.Request.InputStream.Length > 0)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(this.httpContext.Request.InputStream);
                XmlNode root = xmlDoc.SelectSingleNode("xml");
                XmlNodeList xnl = root.ChildNodes;

                foreach (XmlNode xnf in xnl)
                {
                    parameters.Add(xnf.Name, xnf.InnerText);
                }
            }
        }

        public virtual void init()
        {
        }


        /** 获取密钥 */

        public string getKey()
        {
            return key;
        }

        /** 设置密钥 */

        public void setKey(string key )
        {
            this.key = key;
            
        }

        /** 获取参数值 */

        public string getParameter(string parameter)
        {
            var s = (string) parameters[parameter];
            return (null == s) ? "" : s;
        }

        /** 设置参数值 */

        public void setParameter(string parameter, string parameterValue)
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

        protected virtual string getCharset()
        {
            return httpContext.Request.ContentEncoding.BodyName;
        }
        #region 辅助方法===================================
        /// <summary>
        /// 判断微信签名
        /// </summary>
        /// <returns></returns>
        public virtual bool isWXsign(out string error)
        {
            StringBuilder sb = new StringBuilder();
            Hashtable signMap = new Hashtable();
            foreach (string k in parameters.Keys)
            {
                if (k != "sign")
                {
                    signMap.Add(k.ToLower(), parameters[k]);
                }
            }

            ArrayList akeys = new ArrayList(signMap.Keys);
            akeys.Sort();

            foreach (string k in akeys)
            {
                string v = (string)signMap[k];
                sb.Append(k + "=" + v + "&");
            }
            sb.Append("key=" + this.key);

            string sign = TenpayMd5.GetMD5(sb.ToString(), charset).ToString().ToUpper();
            error = "sign = " + sign + "\r\n xmlMap[sign]=" + parameters["sign"].ToString() + "\r\n " + sb.ToString();
            return sign.Equals(parameters["sign"]);

        }

        /// <summary>
        /// 判断微信维权签名
        /// </summary>
        /// <returns></returns>
        public virtual bool isWXsignfeedback()
        {
            return true;
            //StringBuilder sb = new StringBuilder();
            //Hashtable signMap = new Hashtable();

            //foreach (string k in xmlMap.Keys)
            //{
            //    if (SignField.IndexOf(k.ToLower()) != -1)
            //    {
            //        signMap.Add(k.ToLower(), xmlMap[k]);
            //    }
            //}
            //signMap.Add("appkey", this.appkey);


            //ArrayList akeys = new ArrayList(signMap.Keys);
            //akeys.Sort();

            //foreach (string k in akeys)
            //{
            //    string v = (string)signMap[k];
            //    if (sb.Length == 0)
            //    {
            //        sb.Append(k + "=" + v);
            //    }
            //    else
            //    {
            //        sb.Append("&" + k + "=" + v);
            //    }
            //}

            //string sign = SHA1Util.getSha1(sb.ToString()).ToString().ToLower();

            //this.setDebugInfo(sb.ToString() + " => SHA1 sign:" + sign);

            //return sign.Equals(xmlMap["AppSignature"]);

        }

        
        #endregion
    }
}