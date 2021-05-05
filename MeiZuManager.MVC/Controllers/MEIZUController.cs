using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEIZU.BLL;

namespace MeiZuManager.MVC.Controllers
{
    public class MEIZUController : Controller
    {
        public FirstManager Fm { get; set; } = new FirstManager();
        /// <summary>
        /// 手机详情模板页，通过Id生成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PhoneDesc(int id) 
        {
            var list = Fm.GetOnePhoneDesc(id);
            return View(list);
        }
        /// <summary>
        /// 手机详情页颜色图片
        /// </summary>
        /// <param name="ShoppingId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAllPhoneDescPic(int ShoppingId)
        {
            var list = Fm.GetOnePhoneDesc(ShoppingId);
            return Json(new {data = list},JsonRequestBehavior.AllowGet);
        }
    }
}