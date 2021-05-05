using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;
using MEIZU.BLL;

namespace MeiZuManager.MVC.Controllers
{
    public class HomeController : Controller
    {
        // 初始化 业务逻辑服务
        public FirstManager FirstManager { get; set; } = new FirstManager();
        public ActionResult Index()
        {
            return Redirect("/MEIZU/Html/Index.html");
        }
        [HttpPost]
        public ActionResult IndexPic()
        {
            FirstManager Fm = new FirstManager();
            var list = Fm.GetAllPic();
            return Json(new {data = list},JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetAllPhoneDescPic()
        {
            var list = new FirstManager().GetAllDescPic();
            return Json(new {data = list},JsonRequestBehavior.AllowGet);
        }
        #region 注册

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string phone, string Pwd)
        {
            //判断 有没有该用户 有则返回 已注册
            // 没有 则 添加 该用户信息
            // 数据库中的数据进行判断
            if (FirstManager.JudgeUserLogin(phone))
                return RedirectToAction("UserEnter");
            else
            { 
                try
                {
                    FirstManager.CreateUser(phone, Pwd);
                    return RedirectToAction("UserEnter");
                }
                catch (Exception e)
                {
                    return JavaScript($"alert('错误{e.Message}')");
                }
            }
        }

        #endregion

        #region 登录

        /// <summary>
        /// 重定向至登录页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserEnter()
        {
            return Redirect("/MEIZU/Html/RLogin.html");
        }

        /// <summary>
        /// 登录
        /// </summary>
        [HttpPost]
        public ActionResult UserEnter(string phone, string Pwd)
        {
            if (FirstManager.Login(name: phone, pwd: Pwd))
            {
                if (FirstManager.JudgeState(phone))
                {
                    Session["UserName"] = phone;
                    FirstManager.UpdataLogin(phone);
                    return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new {code = 404}, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult UserNameIsTrue()
        {
            if(Session["UserName"] != null)
                return Json(new {data = true}, JsonRequestBehavior.AllowGet);
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 登录状态
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginState()
        {
            if (Session["UserName"] != null)
            {
                FirstManager.UpdataLoginFalse(Session["UserName"].ToString());
                Session.Remove("UserName");
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}