// =================================================================== 
// 项目说明
//====================================================================
// 作者：luyisheng
// 文件： T_ENG_COMPLAINTSearch.cs
// 项目名称：SaaS.EntityMis.Search
// 创建时间：2016/11/23
// ===================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SaaS.Framework;

namespace SaaS.EntityMis.Search
{
    public class ComplaintSearch : BaseSearch
    {
      
        /// <summary>
        /// 处理状态
        /// </summary>
        public int  fHandleStatus { get; set; }
    }
}

