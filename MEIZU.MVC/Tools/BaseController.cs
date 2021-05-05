using System;
using System.Web.Mvc;
using MEIZU.BLL;
using MEIZU.Model;
using MEIZU.Models;

namespace MEIZU.Tools
{
    public class BaseController:Controller
    {
        public BaseManager BaseManager { get; set; }    = new BaseManager();
        /// <summary>
        /// 通过get获取json数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult GetJson(object data)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取登录的用户信息
        /// </summary>
        /// <returns></returns>
        public ManagerUser GetLoginUser()
        {
            if(Session["username"] == null) throw  new Exception("该账户没有登录");
            var loginName = Session["username"].ToString();
            return BaseManager.GetManager(loginName);
        }
        
        
    }
}