using MEIZU.Models;
using MEIZU.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MEIZU.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["username"] == null) return RedirectToAction("Login", "Account");
            return View();
        }

        public ActionResult TableData(int page=1,int limit=10)
        {
            var list = new List<Teacher>();
            for (int i = 0; i < limit; i++)
            {
                list.Add(new Teacher{Id = (page-1)*limit+i+1,Name =$"姓名-{(page - 1) * limit + i + 1}" });
            }
           

            return Json(new PearTableReponseData()
            {
                code = 0,
                msg = "",
                count = 1000,
                data = list 
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult TableDemo()
        {
            return View(new TableDataViewModel());
        }
    }

    public class PearTableReponseData
    {
        public int code { get; set; } = 0;
        public string msg { get; set; }
        public int count { get; set; }
        public object data { get; set; }
    }

    class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}