using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using SaaS.Framework;
using SaaS.Framework.Common;
using System.Threading.Tasks;




namespace SaaS.APIMis.BLL
{
    public class SendRequest
    {
        /// <summary>
        /// 中间件验证key
        /// </summary>
        public static string apiKey = ConfigHelper.GetSettings("apiKey");

        /// <summary>
        /// 中间件验证keyValue
        /// </summary>
        public static string apiKeyValue = ConfigHelper.GetSettings("apiKeyValue");

        #region get请求

        /// <summary>
        /// Get请求，根据路径请求，参数可包含在路径中（x.com/?name=张三）
        /// </summary>
        /// <typeparam name="T">返回类</typeparam>
        /// <param name="url">请求路径（不带IP跟端口，IP端口配置化）</param>
        /// <returns></returns>
        public static T Get<T>(string url) where T : BaseResult, new()
        {

            return Get<T>(url, ServerPath.ApiService);
        }

        /// <summary>
        /// Get请求，根据请求服务器域名
        /// </summary>
        /// <typeparam name="T">返回类</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="ServerPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static T Get<T>(string url, string serverPath) where T : BaseResult, new()
        {
            //容错斜杠
            if (serverPath.EndsWith("/"))
            {
                serverPath = serverPath.Substring(0, serverPath.Length - 1);
            }
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            url = serverPath + url;


            if (!url.Contains("?"))
            {
                url = url + "?apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }
            else
            {
                url = url + "&apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }
            T result = null;

            try
            {
                string content = SendGet(url);
                result = JsonHelper.Deserialize<T>(content);
            }
            catch (Exception ex)
            {
                result = new T();
                result.Code = "1007";
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Get请求，根据请求服务器域名及自定义apikey和value
        /// </summary>
        /// <typeparam name="T">返回类</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="ServerPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <param name="apiKey">验证Key</param>
        /// <param name="apiKeyValue">验证value</param>
        /// <returns></returns>
        public static T Get<T>(string url, string serverPath, string apiKey, string apiKeyValue) where T : BaseResult, new()
        {
            //容错斜杠
            if (serverPath.EndsWith("/"))
            {
                serverPath = serverPath.Substring(0, serverPath.Length - 1);
            }
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            url = serverPath + url;



            if (!url.Contains("?"))
            {
                url = url + "?apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }
            else
            {
                url = url + "&apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }
            T result = null;

            try
            {
                string content = SendGet(url);
                result = JsonHelper.Deserialize<T>(content);
            }
            catch (Exception ex)
            {
                result = new T();
                result.Code = "1007";
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }

            return result;
        }


        /// <summary>
        /// Get请求，根据Dictionary
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <returns></returns>
        public static T Get<T>(string url, Dictionary<string, string> param) where T : BaseResult, new()
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> p in param)
            {
                sb.Append(p.Key + "=" + p.Value + "&");
            }
            if (url.Contains("?"))
            {
                url = url + "&" + sb.ToString();
            }
            else
            {
                url = url + "?" + sb.ToString();
            }

            return Get<T>(url, ServerPath.ApiService);
        }

        /// <summary>
        /// Get请求，根据实体类
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <returns></returns>
        public static T Get<T>(string url, object param) where T : BaseResult, new()
        {
            string model = JsonHelper.ObjectToJSON(param);
            if (url.Contains("?"))
            {
                url = url + "&model=" + model;
            }
            else
            {
                url = url + "?model=" + model;
            }

            return Get<T>(url, ServerPath.ApiService);
        }
        /// <summary>
        /// Get请求，使用Dictionary参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static T Get<T>(string url, Dictionary<string, string> param, string serverPath) where T : BaseResult, new()
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> p in param)
            {
                sb.Append(p.Key + "=" + p.Value + "&");
            }
            if (url.Contains("?"))
            {
                url = url + "&" + sb.ToString();
            }
            else
            {
                url = url + "?" + sb.ToString();
            }

            return Get<T>(url, serverPath);
        }
        /// <summary>
        /// Get请求，使用实体参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static T Get<T>(string url, object param, string serverPath) where T : BaseResult, new()
        {
            string model = JsonHelper.ObjectToJSON(param);
            if (url.Contains("?"))
            {
                url = url + "&model=" + model;
            }
            else
            {
                url = url + "?model=" + model;
            }

            return Get<T>(url, serverPath);
        }

        /// <summary>
        /// Get请求，使用实体参数及自定义apikey和value
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <param name="apiKey">验证Key</param>
        /// <param name="apiKeyValue">验证value</param>
        /// <returns></returns>
        public static T Get<T>(string url, object param, string serverPath, string apiKey, string apiKeyValue) where T : BaseResult, new()
        {
            string model = JsonHelper.ObjectToJSON(param);
            if (url.Contains("?"))
            {
                url = url + "&model=" + model;
            }
            else
            {
                url = url + "?model=" + model;
            }

            return Get<T>(url, serverPath, apiKey, apiKeyValue);
        }

        /// <summary>
        /// 发送get服务器请求
        /// </summary>
        /// <param name="url">完整路径，IP+端口+文件路径</param>
        /// <returns></returns>
        public static string SendGet(string url)
        {

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;

            byte[] page = null;
            try
            {
                page = client.DownloadData(url);
            }
            catch (WebException)
            {
                throw;
            }
            finally
            {
                client.Dispose();
            }

            string content = page == null ? "" : Encoding.UTF8.GetString(page);
            return content;
        }
        #endregion

        #region post请求

        /// <summary>
        /// Post请求，根据路径请求，参数可包含在路径中（x.com/?name=张三）
        /// </summary>
        /// <typeparam name="T">返回类</typeparam>
        /// <param name="url">请求路径（不带IP跟端口，IP端口配置化）</param>
        /// <returns></returns>
        public static T Post<T>(string url) where T : BaseResult, new()
        {

            return Post<T>(url, "", ServerPath.ApiService);
        }

        /// </summary>Post请求，根据路径请求，参数请求服务器
        /// <typeparam name="T">返回类</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">string类型参数（格式：&name=东海&sex=1）</param>
        /// <returns></returns>
        public static T Post<T>(string url, string param) where T : BaseResult, new()
        {
            return Post<T>(url, param, ServerPath.ApiService);
        }

        /// <summary>
        /// Post请求，根据Dictionary
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <returns></returns>
        public static T Post<T>(string url, Dictionary<string, string> param) where T : BaseResult, new()
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> p in param)
            {
                sb.Append("&" + p.Key + "=" + p.Value);
            }
            string data = sb.ToString();


            return Post<T>(url, data, ServerPath.ApiService);
        }

        /// <summary>
        /// Post请求，根据Dictionary
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <returns></returns>
        public static T Post<T>(string url, Dictionary<string, int> param) where T : BaseResult, new()
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> p in param)
            {
                sb.Append("&" + p.Key + "=" + p.Value);
            }
            string data = sb.ToString();


            return Post<T>(url, data, ServerPath.ApiService);
        }

        /// <summary>
        /// Post请求，根据实体类
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <returns></returns>
        public static T Post<T>(string url, object param) where T : BaseResult, new()
        {
            string model = JsonHelper.ObjectToJSON(param);

            string data = "&model=" + model;
            return Post<T>(url, data, ServerPath.ApiService);
        }
        /// <summary>
        /// Post请求，使用Dictionary参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static T Post<T>(string url, Dictionary<string, string> param, string serverPath) where T : BaseResult, new()
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> p in param)
            {
                sb.Append("&" + p.Key + "=" + p.Value);
            }

            string data = sb.ToString();
            return Post<T>(url, data, serverPath);
        }
        /// <summary>
        /// Get请求，使用实体参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static T Post<T>(string url, object param, string serverPath) where T : BaseResult, new()
        {
            string model = JsonHelper.ObjectToJSON(param);
            string data = "&model=" + model;
            return Post<T>(url, data, serverPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="data">参数（格式 name = a sex = 26）</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static T Post<T>(string url, string data, string serverPath) where T : BaseResult, new()
        {
            //容错斜杠
            if (serverPath.EndsWith("/"))
            {
                serverPath = serverPath.Substring(0, serverPath.Length - 1);
            }
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            url = serverPath + url;




            //if (!url.Contains("apiKey"))
            //{
            //    if (string.IsNullOrEmpty(data))
            //    {
            //        data = "apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            //    }
            //    else if (!data.Contains("apiKey"))
            //    {
            //        data = data + "&apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            //    }
            //}
            T result = null;

            try
            {
                string content = SendPost(url, data);
                result = JsonHelper.Deserialize<T>(content);
            }
            catch (Exception ex)
            {
                result = new T();
                result.Code = "1007";
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }

            return result;

        }

        public static string Post(string url, string data)
        {

            try
            {
                string content = SendPost(url, data);
                return content;
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return "success";

        }

        /// <summary>
        /// Get请求，使用实体参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <param name="apiKey">验证Key</param>
        /// <param name="apiKeyValue">验证value</param>
        /// <returns></returns>
        public static T Post<T>(string url, object param, string serverPath, string apiKey, string apiKeyValue) where T : BaseResult, new()
        {
            string model = JsonHelper.ObjectToJSON(param);
            string data = "&model=" + model;
            return Post<T>(url, data, serverPath, apiKey, apiKeyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="data">参数（格式 name = a sex = 26）</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <param name="apiKey">验证Key</param>
        /// <param name="apiKeyValue">验证value</param>
        /// <returns></returns>
        public static T Post<T>(string url, string data, string serverPath, string apiKey, string apiKeyValue) where T : BaseResult, new()
        {
            //容错斜杠
            if (serverPath.EndsWith("/"))
            {
                serverPath = serverPath.Substring(0, serverPath.Length - 1);
            }
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            url = serverPath + url;

            if (string.IsNullOrEmpty(data))
            {
                data = "apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }
            else
            {
                data = data + "&apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }
            T result = null;

            try
            {
                string content = SendPost(url, data);
                result = JsonHelper.Deserialize<T>(content);
            }
            catch (Exception ex)
            {
                result = new T();
                result.Code = "1007";
                result.Message = ex.Message;
                result.Success = false;
                return result;
            }

            return result;

        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="url">全路径，包含http://IP:端口/文件</param>
        /// <param name="data">参数（格式 name = a sex = 26）</param>
        /// <returns></returns>
        public static string SendPost(string url, string data)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            string page = null;
            try
            {
                page = client.UploadString(url, "POST", data);
            }
            catch (WebException e)
            {
                throw e;
            }
            finally
            {

                client.Dispose();

            }
            return page == null ? "" : page;
        }
        #endregion

        #region postAsync请求

        /// <summary>
        /// 异步Post请求，根据路径请求，参数可包含在路径中（x.com/?name=张三）
        /// </summary>
        /// <typeparam name="T">返回类</typeparam>
        /// <param name="url">请求路径（不带IP跟端口，IP端口配置化）</param>
        /// <returns></returns>
        public static void PostAsync(string url)
        {

            PostAsync(url, "", ServerPath.ApiService);
        }

        ///<summary>
        /// Post请求，根据路径请求，参数请求服务器
        /// </summary>
        /// <typeparam name="T">返回类</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">string类型参数（格式：&name=东海&sex=1）</param>
        /// <returns></returns>
        public static void PostAsync(string url, string param)
        {
            PostAsync(url, param, ServerPath.ApiService);
        }

        /// <summary>
        /// 异步Post请求，根据Dictionary
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <returns></returns>
        public static void PostAsync(string url, Dictionary<string, string> param)
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> p in param)
            {
                sb.Append("&" + p.Key + "=" + p.Value);
            }
            string data = sb.ToString();


            PostAsync(url, data, ServerPath.ApiService);
        }

        /// <summary>
        /// 异步Post请求，根据Dictionary
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <returns></returns>
        public static void PostAsync(string url, Dictionary<string, int> param)
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> p in param)
            {
                sb.Append("&" + p.Key + "=" + p.Value);
            }
            string data = sb.ToString();


            PostAsync(url, data, ServerPath.ApiService);
        }

        /// <summary>
        /// 异步Post请求，根据实体类
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <returns></returns>
        public static void PostAsync(string url, object param)
        {
            string model = JsonHelper.ObjectToJSON(param);

            string data = "&model=" + model;
            PostAsync(url, data, ServerPath.ApiService);
        }
        /// <summary>
        /// 异步Post请求，使用Dictionary参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">Dictionary参数</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static void PostAsync(string url, Dictionary<string, string> param, string serverPath)
        {
            //循环添加参数
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> p in param)
            {
                sb.Append("&" + p.Key + "=" + p.Value);
            }

            string data = sb.ToString();
            PostAsync(url, data, serverPath);
        }
        /// <summary>
        /// 异步Post请求，使用实体参数
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="param">实体类,model={"name":"东海"}</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static void PostAsync(string url, object param, string serverPath)
        {
            string model = JsonHelper.ObjectToJSON(param);
            string data = "&model=" + model;
            PostAsync(url, data, serverPath);
        }
        /// <summary>
        /// 异步Post请求
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="url">请求路径（不包括IP跟端口，IP端口配置化）</param>
        /// <param name="data">参数（格式 name = a sex = 26）</param>
        /// <param name="serverPath">域名、IP跟端口（在ServerPath类中获取）</param>
        /// <returns></returns>
        public static void PostAsync(string url, string data, string serverPath)
        {
            //容错斜杠
            if (serverPath.EndsWith("/"))
            {
                serverPath = serverPath.Substring(0, serverPath.Length - 1);
            }
            if (!url.StartsWith("/"))
            {
                url = "/" + url;
            }
            url = serverPath + url;

            if (string.IsNullOrEmpty(data))
            {
                data = "apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }
            else
            {
                data = data + "&apiKey=" + apiKey + "&apiKeyValue=" + apiKeyValue;
            }

            try
            {
                SendPostAsync(url, data);
            }
            catch (Exception ex)
            {

            }


        }
        /// <summary>
        /// 发送异步Post请求
        /// </summary>
        /// <param name="url">全路径，包含http://IP:端口/文件</param>
        /// <param name="data">参数（格式 name = a sex = 26）</param>
        /// <returns></returns>
        /// 
        public static void SendPostAsync(string url, string data)
        {
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            try
            {
                client.UploadStringAsync(new Uri(url), "POST", data);
            }
            catch (WebException e)
            {
                throw e;
            }
            finally
            {
                client.Dispose();
            }
        }

        //public static async void SendPostAsync(string url, string data)
        //{
        //    WebClient client = new WebClient();
        //    client.Encoding = Encoding.UTF8;
        //    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        //    try
        //    {
        //        //await client.DownloadStringAsync(new Uri(url), data);
        //       await client.UploadFileTaskAsync(new Uri(url), "POST", data);
        //    }
        //    catch (WebException e)
        //    {
        //        throw e;
        //    }
        //    finally
        //    {
        //        client.Dispose();
        //    }
        //}
        #endregion
    }
}
