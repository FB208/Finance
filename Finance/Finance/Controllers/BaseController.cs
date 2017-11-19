using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finance.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public class Result
        {
            /// <summary>是否成功
            /// </summary>
            public bool success { get; set; }
            /// <summary>消息
            /// </summary>
            public string msg { get; set; }
            /// <summary>状态
            /// <para>1表成功</para>
            /// </summary>
            public int status { get; set; }
            /// <summary>数据
            /// </summary>
            public object Data { get; set; }
            /// <summary>
            /// 基础数据
            /// </summary>
            public object BaseData { get; set; }
            public int Total { get; set; }
        }
    }
}