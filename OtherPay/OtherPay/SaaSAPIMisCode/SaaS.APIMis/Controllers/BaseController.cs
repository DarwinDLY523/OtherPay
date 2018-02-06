

using SaaS.EntityMis.Enums;
using SaaS.EntityMis.Info;
using SaaS.Framework;
using SaaS.Framework.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SaaS.APIMis.Controllers
{
    public class BaseController : Controller
    {

        #region 属性
        /// <summary>
        /// 当前登录用户，null表示未登录
        /// </summary>
        protected SaaS.Entity.Info.LoginUserInfo userInfo;

     

        private Result _result;
        /// <summary>
        /// 返回参数(增删改通用)
        /// </summary>
        protected Result result
        {
            get
            {
                if (_result == null) { _result = new Result(); }
                return _result;
            }
            set
            {
                _result = value;
            }
        }

        private ResultSearch _resultSearch;
        /// <summary>
        /// 返回参数（查询专用）
        /// </summary>
        protected ResultSearch resultSearch
        {
            get
            {
                if (_resultSearch == null) { _resultSearch = new ResultSearch(); }
                return _resultSearch;
            }
            set
            {
                _resultSearch = value;
            }
        }

        private BaseSearch _baseSearch;
        /// <summary>
        /// 通用搜索
        /// </summary>
        protected BaseSearch baseSearch
        {
            get
            {
                if (_baseSearch == null) { _baseSearch = new BaseSearch(); }
                return _baseSearch;
            }
            set { _baseSearch = value; }
        }
        #endregion

        #region 数据转Json格式
        /// <summary>
        /// 在数据转Json格式
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <returns></returns>
        protected new JsonResult Json(object data)
        {
            //return base.Json(data, JsonRequestBehavior.AllowGet); //老的方式如果json数据太大回返回不了，增加了新的方式
            #region 增加最大json长度
            ScriptingJsonSerializationSection section = ConfigurationManager.GetSection("system.web.extensions/scripting/webServices/jsonSerialization") as ScriptingJsonSerializationSection;
            int MaxJsonLength = section.MaxJsonLength;
            return new System.Web.Mvc.JsonResult()
            {
                Data = data,
                MaxJsonLength = MaxJsonLength,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            #endregion
        }
        #endregion


        #region 调用方密钥集合
        /// <summary>
        /// 调用方密钥集合
        /// </summary>
        protected Dictionary<string, string> ApiKey = null;

        public BaseController()
        {
            try
            {
                if (ApiKey == null)
                {
                    ApiKey = new Dictionary<string, string>();
                    string[] arr = ConfigHelper.GetSettings("MessageKey").Split('|');
                    foreach (string str in arr)
                    {
                        ApiKey.Add(str.Split('$')[0], str.Split('$')[1]);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4Helper.Error("获取初始化权限密钥集合报错：", ex);
            }
        }
        #endregion

        #region 重写初始化
        /// <summary>
        /// 重写初始化
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            // 判断api安全key是否的请求
            string _apiKey = QueryStringHelper.GetString("apiKey");
            string _apiKeyValue = QueryStringHelper.GetString("apiKeyValue");
            if (!ApiKey.ContainsKey(_apiKey) || _apiKeyValue != ApiKey[_apiKey])
            {
                requestContext.HttpContext.Response.Write(JsonHelper.ObjectToJson(new Result(ErrorEnum.Error.noPower)));
                requestContext.HttpContext.Response.End();
            }
            else
            {
                base.Initialize(requestContext);
            }
        }
        #endregion

        #region 初始化权限值
        /// <summary>
        /// 初始化权限值
        /// </summary>
        /// <param name="baseSearch">父类对象</param>
        public void SetSearchModel(ref BaseSearch _baseSearch)
        {
            _baseSearch.UserID = this.baseSearch.UserID;
            _baseSearch.UserIsAdmin = this.baseSearch.UserIsAdmin;
            _baseSearch.LesseeID = this.baseSearch.LesseeID;
            _baseSearch.DepartmentID = this.baseSearch.DepartmentID;
            _baseSearch.AdminID = this.baseSearch.AdminID;
            _baseSearch.PlaCustomerID = this.baseSearch.PlaCustomerID;
            _baseSearch.FPersonId = this.baseSearch.FPersonId;


            //_baseSearch.AdminPowerID = this.baseSearch.AdminPowerID;
            _baseSearch.LesseePowerID = this.baseSearch.LesseePowerID;
            _baseSearch.PlaCustomerPower = this.baseSearch.PlaCustomerPower;
            _baseSearch.AdminType = this.baseSearch.AdminType;
        }
        #endregion



        #region 返回参数（查询专用）
        /// <summary>
        /// 返回参数（查询专用）
        /// </summary>
        protected void ResultSearchVal(BaseSearch baseSearch, object data, int total)
        {
            resultSearch.CurrentPage = baseSearch.pageIndex;
            resultSearch.PageRow = baseSearch.pageSize;
            resultSearch.CountRow = total;
            resultSearch.Data = data;
        }
        #endregion

        #region JSON文本转对象,泛型方法
        /// <summary>
        /// JSON文本转对象,泛型方法
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj">参数</param>
        /// <returns>指定类型的对象</returns>
        public T GetJsonSearch<T>(string obj) where T : BaseSearch, new()
        {
            T t = null;
            try
            {
                if (!string.IsNullOrEmpty(QueryStringHelper.GetString(obj)))
                {
                    t = JsonHelper.JSONToObject<T>
                       (QueryStringHelper.GetString(obj));
                    if (t.pageIndex <= 0)
                    {
                        t.pageIndex = 1;
                    }
                    if (t.pageSize <= 0)
                    {
                        t.pageSize = 20;
                    }
                }
                else
                {
                    t = new T();

                    t.pageIndex = 1;
                    t.pageSize = 20;
                }
                return t;
            }
            catch (Exception ex)
            {
                return t;
            }
        }
        #endregion

    }
}