using System.Web.Mvc;
using MEIZU.BLL;
using MEIZU.Models;
using MEIZU.Tools;

namespace MEIZU.Controllers
{
    public class AccountController : BaseController
    {
        // GET
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountLoginVm model)
        {
            BaseManager manager = new BaseManager();
            if (manager.UserManagerLogin(model.LoginName, model.LoginPwd))
            {
                    Session["username"] = model.LoginName;
                    return Json(new {code=200});
            }
            else
            {
                return Json(new {code = 500});
            }
        }
    }
}