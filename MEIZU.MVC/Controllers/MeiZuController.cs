using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MEIZU.BLL;
using MEIZU.Controllers;
using MEIZU.DAL.FirstMvcDAL;
using MEIZU.DTO;
using MEIZU.Model.firstMvcModel;
using MEIZU.MVC.Models.MeiZuVm;
using MEIZU.MVC.Models.UserSys;
using MEIZU.Tools;

namespace MEIZU.MVC.Controllers
{
        #region 轮播

    public class MeiZuController : BaseController
    {
        // GET: MeiZu
        public ActionResult RLoginResult()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RloginResult(string phone, string pwd)
        {
            BaseManager Bm = new BaseManager();
            if (Bm.UserAdminLogin(phone, pwd))
            {
                Session["username"] = phone;
                return Redirect("/MEIZU/Html/Index.html");
            }
            else
            {
                return Json(new { code = 500 });
            }
        }
        [HttpGet]
        public ActionResult IndexPic()
        {
            List<Pic> list = new FirstManager().GetAllPic();

            return View(new PicViewModel());
        }

        public ActionResult GetAllIndexPic()
        {
            var list = new FirstManager().GetAllPic().Select(m => new
            {
                m.Id,
                m.ImgName,
                m.ImgPath,
                m.ImgUrl,
                ArgImgPic = m.ImgPic = $"<img src='//localhost:2080/MEIZU{m.ImgPath}'>",
                m.IsEnable
            });
            return Json(new PearTableReponseData()
            {
                data = list,
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditIndexPic(int id)
        {
            var list = new FirstManager().GetonePic(id);
            return View(new PicViewModel() { Id = id, Name = list.ImgName, Path = list.ImgPath, IsEnable = list.IsEnable, Url = list.ImgUrl, ImgPic = $"<img src='http://localhost:2080/MEIZU{list.ImgPath}'>" });
        }
        [HttpPost]
        public ActionResult EditIndexPic(PicViewModel model)
        {
            if (ModelState.IsValid)
            {
                FirstManager Fm = new FirstManager();
                Fm.EditPic(new Pic()
                {
                    Id = model.Id,
                    ImgName = model.Name,
                    ImgPath = model.Path,
                    ImgUrl = model.Url,
                    IsEnable = model.IsEnable
                });
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EditUploadPic()
        {
            try
            {
                //获取上传的文件
                var file = Request.Files[0];
                var filecombin = file.FileName.Split('.');
                if (file == null || string.IsNullOrEmpty(file.FileName) || file.ContentLength == 0 || filecombin.Length < 2)
                {
                    return GetJson(new PearTableReponseData { code = 500, msg = "上传文件失败" });
                }
                string cc = HttpRuntime.AppDomainAppPath.ToString();
                int idx = cc.TrimEnd('\\').LastIndexOf('\\');
                string localPath = cc.Substring(0, idx) + @"\MeiZuManager.MVC\MEIZU\Images\Upload\";
                string filePathName = string.Empty;
                filePathName = filecombin[0] + "." + filecombin[1];
                if (!Directory.Exists(localPath))
                {
                    Directory.CreateDirectory(localPath);
                }
                string DbPath = "/Images/Upload/" + filePathName;
                //保存图片
                file.SaveAs(Path.Combine(localPath, filePathName));
                return GetJson(new PearTableReponseData { code = 200, data = Path.Combine(localPath, DbPath), msg = "上传文件成功" });
            }
            catch (Exception ex)
            {
                return GetJson(new PearTableReponseData { code = 500, msg = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult EditPicState(int id)
        {
            FirstManager Fm = new FirstManager();
            Fm.EditPicStateId(id);
            return GetJson(new PearTableReponseData() { code = 200, msg = "更新成功" });
        }


        #endregion
        
        #region 用户管理

        /// <summary>
        /// 初始化 用户管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManagerMent()
        {
            return View(new UserVmModel());
        }

        /// <summary>
        /// 返回数据 同时作为 条件筛选
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public ActionResult UserData(string UserName = "")
        {
            if (UserName == "")
                return Json(new { code = 0, data = new AfterEndManage().UserGetAll() }, JsonRequestBehavior.AllowGet);
            return Json(new { code = 0, data = new AfterEndManage().UserGetWhere($"UserName={UserName}") },
                JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserDelete(int id)
        {
            try
            {
                new AfterEndManage().UserDelete(id);
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { code = 500, msg = "删除失败:" + e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 设置用户
        /// </summary> 
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult UserSetRole(int id)
        {
            return View();
        }
        /// <summary>
        /// 禁用账号
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserEnableState(string UserName)
        {

            AfterEndManage Ae = new AfterEndManage();
            if (Ae.EditUserState(UserName))
            {
                return GetJson(new PearTableReponseData() { code = 200, msg = "更新成功" });
            }
            else
            {
                return GetJson(new PearTableReponseData() { code = 500, msg = "更新失败" });
            }
        }
        /// <summary>
        /// 用户添加 页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserCreate()
        {
            return View();
        }

        /// <summary>
        /// 用户添加 逻辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserCreate(UserCreateVm model)
        {
            if (ModelState.IsValid)
            {
                if (new FirstManager().CreateUser(model.UserName, model.UserPwd))
                    return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
                return Json(new { code = 500, err = "数据有误" });
            }
            else
            {
                return Json(new { code = 500, err = "数据有误" });
            }
        }

        #endregion

        #region 分类
        
        public ActionResult PhoneTypeAll()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PhoneTypeData()
        {
            return Json(new {code = 0,data = new PhoneTypeService().GetAll().Select(m=>new
            {
                m.Id,
                m.TypeName,
                Tid = m.TypeId ?? 0
            }).ToList()}, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加手机分类根节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreatePhoneType()
        {
            return View(new PhoneTypeVM());
        }
        [HttpPost]
        public ActionResult CreatePhoneType(string TypeName)
        {
            if (!string.IsNullOrEmpty(TypeName))
            {
                new FirstManager().CreateTypeName(TypeName);
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { code = 500 }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult EditNode(int id)
        {
            var dr = new PhoneTypeService().GetOne(id);
            var enList = new PhoneTypeService().GetAll().Where(m => m.TypeId == null).ToList();
            ViewBag.list = new SelectList(enList, "Id", "TypeName");
            return View(new PhoneTypeVM(){Id = id,TypeName = dr.TypeName,TypeId =dr.TypeId});
        }
        [HttpPost]
        public ActionResult EditNode(PhoneTypeVM model)
        {
            new FirstManager().EditPhoneType(model.Id,model.TypeName,model.TypeId);
            return Json(new PearTableReponseData() {code = 200}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 订单
        [HttpGet]
        public ActionResult UserOrder()
        {
            return View(new UserOrderVm());
        }
        [HttpGet]
        public ActionResult GetAllUserOrder()
        {
            var list = new FirstManager().GetAllOrder();
            return Json(new PearTableReponseData() {data = list, code = 0}, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 订单详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDetail(int id)
        {
            return View(new ShoppingOrderDTOService().GetOne(id));
        }
        [HttpGet]
        public ActionResult GetAllUserOrderDetail()
        {
            var list = new FirstManager().GetAllOrder().Select(m=>new
            {
                m.Id,
                m.Count,
                CreateTime = m.CreateTime.ToString("G"),
                m.EditionName,
                m.Name,
                m.OrderAddress,
                m.OrderName,
                m.OrderPhone,
                m.UserName,
                m.StateName,
                m.PicName,
                m.Price,
                m.Remarks,
            });
            return Json(new PearTableReponseData() { data = list, code = 0 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 订单详情操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrderDetail(ShoppingOrder model)
        {
            return Json(new PearTableReponseData() {code = 200}, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 订单详情修改页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderEdit(int id)
        {
            var list = new ShoppingOrderDTOService().GetOne(id);
            var list1 = new FirstManager().GetOneOrder(id);
            ViewBag.StateList = new SelectList(new FirstManager().GetAllOrderStates(), "Id", "StateName", list1.OrderState);
            return View(new ShoppingOrder(){UserName = list.UserName,Name = list.Name,EditionName = list.EditionName,PicName = list.PicName,Count = list.Count,StateName = list.StateName,OrderName = list.OrderName,OrderPhone = list.OrderPhone,OrderAddress = list.OrderAddress,Remarks = list.Remarks,CreateTime = list.CreateTime,OrderProvince = list.Orderprovince,OrderCity = list.OrderCity,OrderCounty = list.OrderCounty});
        }
        /// <summary>
        /// 订单详情修改操作
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrderEdit(ShoppingOrder model,int OrderStateId,string province,string county,string city)
        {
            if (ModelState.IsValid)
            {
                var list = new FirstManager().GetOneOrder(model.Id);
                new FirstManager().EditOrder(
                    id: (int)list.Id, 
                    (int)list.UserId,
                    list.ShoppingCodId,
                    list.Shopping,
                    list.CodNumber, 
                    OrderStateId, 
                    list.WayPay, 
                    list.Count, 
                    model.OrderName, 
                    model.OrderPhone,
                    model.OrderAddress,
                    model.Remarks,
                    createTime:list.CreateTime,
                    province,
                    county,
                    city
                    );

                return Json(new PearTableReponseData() { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new PearTableReponseData() { code = 500 }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 商品详情
        /// <summary>
        /// 后台手机页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PhoneShow()
        {
            return View(new PhoneDescVm());
        }
        /// <summary>
        /// 获取手机数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllPhone(int id = 1)
        {
            var list = new FirstManager().GetAllCod().Select(m => new
            {
                m.Name,
                m.Title,
                m.Configure,
                m.EditionName,
                m.ShoppingId,
                m.CodState
            }).Where(m=>m.ShoppingId == id).ToList();
            return Json(new PearTableReponseData() {data = list}, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 商品

        /// <summary>
        /// 添加商品页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateShopping()
        {
            return View();
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateShopping(PhoneDesc model)
        {
            var list = "";
            return Json(new PearTableReponseData() { data = list, code = 200 });
        }
        /// <summary>
        /// 修改商品页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditShopping(int id)
        {
            return View();
        }
        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditShopping(PhoneDesc model)
        {
            var list = "";
            return Json(new PearTableReponseData() { data = list, code = 200 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteShopping(int id)
        {
            return Json(new PearTableReponseData() { code = 200 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 商品是否启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EnableStateShopping(int id)
        {
            return Json(new PearTableReponseData() { code = 200 }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
    }
}