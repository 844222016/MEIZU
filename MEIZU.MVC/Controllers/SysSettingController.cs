using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MEIZU.BLL;
using MEIZU.DTO;
using MEIZU.Model;
using MEIZU.Models;
using MEIZU.Tools;

namespace MEIZU.Controllers
{ 
    public class SysSettingController : BaseController
    {
        enum MenuType
        {
            Menu,Page
        }
        class PearMenu
        {
            public long id { get; set; }
            public string title { get; set; }
            public string icon { get; set; } = "layui-icon layui-icon-console";
            public MenuType type { get; set; } = MenuType.Page;
            public string openType { get; set; } = "_iframe";
            public string href { get; set; }
            public List<PearMenu> children { get; set; }
        }
        /// <summary>
        /// 用来返回菜单数据
        /// </summary>
        /// <returns></returns>
        // GET
        public ActionResult PearMenuData()
        { 
            var menus = BaseManager.GetAllMenus();
            var data = menus.Where(m => m.ParentMenuId == null).Select(m => new PearMenu
            {
                id = m.Id,
                href = m.Url,
                title = m.MenuText,
                icon = m.IconString,
                type = m.MenuTypeId == 1 ? MenuType.Menu : MenuType.Page
            }).ToList();

           for(var i =0;i<data.Count;i++)
           {
                var child = menus.Where(m => m.ParentMenuId == data[i].id
                ).Select(m => new PearMenu
                {
                    id = m.Id,
                    href = m.Url,
                    title = m.MenuText,
                    icon = m.IconString,
                    type = m.MenuTypeId == 1 ? MenuType.Menu : MenuType.Page
                }).ToList();
                data[i].children = child;
            }
             
            
            return Json(data.ToList(),JsonRequestBehavior.AllowGet);
        }
    
        [HttpGet]
        public ActionResult Install()
        {
            return View(new InstallViewModel());
        }
        [HttpPost]
        public ActionResult Install(InstallViewModel model)
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);
            ConnectionStringsSection con = (ConnectionStringsSection)config.GetSection("connectionStrings");
            con.ConnectionStrings["con"].ConnectionString = $"server={model.ServerHost};database={model.DbName};uid={model.ServerName};pwd={model.ServerPwd}";
            config.Save();

            
            SqlConnection con1 = new SqlConnection($"server={model.ServerHost};database=master;uid={model.ServerName};pwd={model.ServerPwd}");
            con1.Open();
            var cmd = con1.CreateCommand();

            cmd.CommandText = $"create database {model.DbName}";
            cmd.ExecuteNonQuery(); 
            cmd.CommandText = $"use {model.DbName}";
            cmd.ExecuteNonQuery();

            var sql = System.IO.File.ReadAllText(Request.MapPath("~/Tools/install.txt"),Encoding.Default);
            
            var setSql = sql.Split(new[]{"go"},StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in setSql)
            {
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }
            
            con1.Close();
            return Content("ok");
        }

        #region 数据字典处理action

        public ActionResult DectionarySetting()
        {
            return View(new DictionaryIndexVm());
        }

        public ActionResult DicData(string groupName="",int page=1,int limit=10)
        {
            BaseManager baseManager = new BaseManager();

            List<Dictionary> data;
            if(string.IsNullOrEmpty(groupName))
                data= baseManager.GetAllDictinarys("1=1",page - 1, limit);
            else 
                data= baseManager.GetAllDictinarys($"GroupName='{groupName}'",page - 1, limit);


            return Json(new {
                code = 0,
                msg = "",
                count = baseManager.DictinaryCount(),
                data 
            },JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 数据字典修改的显示页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditDictionary(long id)
        {
            BaseManager baseManager = new BaseManager();
            var data = baseManager.GetOneDictionary(id);
            
            return View(new DictionaryEditVm()
            {
                Id = data.Id,
                GroupName = data.GroupName,
                Value = data.Value
            });
        }
        /// <summary>
        /// 处理dictionary修改的逻辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditDictionary(DictionaryEditVm model)
        {
            if (ModelState.IsValid)
            {
                
                BaseManager baseManager = new BaseManager();
                baseManager.EditDictionary(new Dictionary
                {
                    Id = model.Id,
                    GroupName = model.GroupName,
                    Value = model.Value
                });
                return Json(new {code = 200});
            }

            return Json(new {code = 500, msg = "数据不合法"});
        }

        [HttpGet]
        public ActionResult RemoveDictionary(long id)
        {
            BaseManager baseManager  = new BaseManager();
            baseManager.RemoveDictionary(id);
            return Json(new {code = 200},JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateDictionary()
        {
            return View();
            
        }
        [HttpPost]
        public ActionResult CreateDictionary(DictionaryCreateVm model)
        {
            if (ModelState.IsValid)
            {
                BaseManager baseManager  = new BaseManager();
                baseManager.CreateDictionary(model.GroupName,model.Value);
                return Json(new {code = 200});
            }

            return Json(new {code = 500,msg="数据有误"});

        }
        
        
        #endregion


        #region 菜单管理Action
        
         /// <summary>
        /// 菜单管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MenuSetting()
        {
            return View();
        }
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateMenu(long Id)
        {
            return View(new MenuCreateVm(){ParentId = Id});
        }

        [HttpPost]
        public ActionResult CreateMenu(MenuCreateVm model)
        {
            if (ModelState.IsValid)
            {
                BaseManager Svc = new BaseManager();
                var order = Svc.MenuMax(model.ParentId);
                Svc.CreateMenu(new Menu
                {
                    MenuText = model.MenuName,
                    IconString = model.Icon,
                    ParentMenuId = model.ParentId,
                    Order = order,
                    MenuTypeId = 2,
                    Url = model.Url
                });
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 500, msg = "数据有误" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加父节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MenuCreateByParent()
        {
            var svc = new BaseManager();
            return View(new MenuCreateParentVm());
        }

        [HttpPost]
        public ActionResult MenuCreateByParent(MenuCreateParentVm model)
        {
            var sve = new BaseManager();
            var order = sve.MenuMax(null);
            if (ModelState.IsValid)
            {
                sve.CreateMenuParent(new Menu
                {
                    ParentMenuId = null,
                    MenuText = model.MenuName,
                    MenuTypeId = 1,
                    IconString = model.Icon,
                    Url = "#",
                    Order = order
                });
                return Json(new {code = 200});
            }

            return Json(new {code = 500});
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MenuRemove(long id,long? parentId)
        {
            BaseManager baseMgr = new BaseManager();
            if (parentId != 0)
            {
                baseMgr.MenuRemoveByChild(id);
            }
            else
            {
                baseMgr.MenuRemoveByParent(id);
            }
            
            return Json(new {code = 200}, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>   
        /// 获取菜单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MenuData()
        {
            BaseManager baseManager = new BaseManager();
            return Json(new {code = 0 ,data=baseManager.GetAllMenus().Select(m=>new
            {
                m.Id,
                m.Order,
                m.Url,
                m.MenuTypeId,
                ParentMenuId =  m.ParentMenuId ?? 0,
                m.IconString,
                m.MenuText
            }).ToList() },JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditMenu(long id)
        {
            var svc = new BaseManager();
            var obj = svc.MenuGetOneById(id);

            ViewBag.ParentMenus = new SelectList(svc.MenusGetAllByWhere($"ParentMenuId is null")
                ,"Id","MenuText",obj.ParentMenuId); 
             
            
            ViewBag.MenuTypes = new SelectList(svc.MenuTypesGetAll(),"Id","TypeName",obj.MenuTypeId.ToString());
          
            return View(new MenuEditVm(){Id = obj.Id,MenuName = obj.MenuText,
                ParentId = obj.ParentMenuId,Order = obj.Order,Icon = obj.IconString,Url = obj.Url});
        }

        [HttpPost]
        public ActionResult EditMenu(MenuEditVm model, long id)
        {
            if (ModelState.IsValid)
            {
                BaseManager baseManager = new BaseManager();
                baseManager.EditMenu(new Menu()
                {
                    Id = model.Id,
                    Order = model.Order,
                    Url = model.Url,
                    ParentMenuId = model.ParentId,
                    IconString = model.Icon,
                    MenuText = model.MenuName,
                    MenuTypeId = model.MenuTypeId
                });
                return Json(new { code = 200 }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { code = 500, msg = "数据不合法" });
        }

        /// <summary>
        /// 菜单上下移动的逻辑接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <param name="isMoveUp"></param>
        /// <returns></returns>
        ///
        [HttpGet]
        public ActionResult MenuMove(long id,long parentId, bool isMoveUp)
        {
            BaseManager baseMgr = new BaseManager();
            baseMgr.MenuMove(isMoveUp,parentId,id);
            return Json(new {code = 200},JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 角色管理
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleCreate()
        {
            return View();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult RoleCreate(RoleCreateVm model)
        {
            BaseManager baseManager = new BaseManager();
            try
            {
                baseManager.RoleCreate(model.RoleName);
                return Json(new {code = 200}, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            { 
                return Json(new {code = 500,msg = e.Message}, JsonRequestBehavior.AllowGet);
            }
            
        }
        
        
        /// <summary>
        /// 权限管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleSetting()
        {
            return View(new RoleIndexVm());
        }

        /// <summary>
        /// 全部角色信息
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleGetAllData()
        { 
            return Json(new PearTableReponseData()
            {
                data = BaseManager.RoleGetAll()
            }, JsonRequestBehavior.AllowGet);
        }
        
        
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RoleRemove(int id)
        {
            BaseManager baseManager = new BaseManager();
            try
            {
                baseManager.RoleRemove(id);
                return Json(new {code = 200});
            }
            catch (Exception e)
            {
                return Json(new {code = 500, msg = e.Message });
            }
            
        }
        
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleEdit(int id)
        {
            BaseManager sve = new BaseManager();
            var item = sve.GetOneRoleById(id);
            return View(new RoleEditVm() {Id = id,RoleName = item.RoleName});
        }

        [HttpPost]
        public ActionResult RoleEdit(RoleEditVm model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BaseManager sve = new BaseManager();
                    sve.RoleEdit(new Role
                    {
                        Id = model.Id,
                        RoleName = model.RoleName
                    });
                    return Json(new {code = 200});
                }
                else
                {
                    return Json(new {code = 500, msg = "数据填写错误"});
                }
            }
            catch (Exception e)
            {
                return Json(new {code = 500,msg = "修改失败" + e.Message});
            }
           
        }
        /// <summary>
        /// 设置角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetRoleMenu(int id)
        {
            
            return View( 
          
                 new PearTreeTableConfig()
                {
                    Id = "RoleMenuTree",
                    Url = "/SysSetting/RoleMenuData/"+id,
                    ParentId = "ParentId",
                    RoleId = id
                
            });
        }

        /// <summary>
        /// 获取当前角色的权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleMenuData(int id)
        {  
            var menuInRole = BaseManager.GetAllMenusByRoleId(id);
            var allMenus = BaseManager.GetAllMenus().Select(m => new
            {
                m.Id,
                m.Order,
                m.Url,
                m.MenuTypeId,
                ParentMenuId = m.ParentMenuId ?? 0,
                m.IconString,
                m.MenuText,
                LAY_CHECKED=menuInRole.Any(n=>n.MenuId == m.Id)
            }).ToList();
          
          
            
            
            return Json(new {code = 0 ,data=allMenus },JsonRequestBehavior.AllowGet);
             
        }
        
        #endregion


        #region 管理员管理
        
        [HttpGet]
        public ActionResult ManagerCreate()
        {
            return View();
        }
        /// <summary>
        /// 添加管理员数据处理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ManagerCreate(ManagerCreateVm model)
        {
            if (ModelState.IsValid)
            {
                
                BaseManager.ManagerCreate(model.LoginName, model.LoginPwd, model.FaceImage);
            
                return GetJson(new {code = 200});
            }
            else
            {
                return GetJson(new {code = 500, error = "数据错误"});
            }
        }
        /// <summary>
        /// 管理员设置管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagerSetting()
        {
            return View(new ManagerSettingVm());
        }

        /// <summary>
        /// 管理员数据
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="CreateTime"></param>
        /// <returns></returns>
        public ActionResult ManagerData(string LoginName="",string CreateTime = "")
        {
            string sql;
            List<ManagerUserDto> date;
            if (string.IsNullOrEmpty(CreateTime))
            {
                sql = $"LoginName like '%{LoginName}%'";
                date =BaseManager.GetAllManagerLike(sql);
            }
            else
            {
                var times = CreateTime.Split(new[] {'~'});
                var timeSql = $"CreateTime >= '{times[0]}' and CreateTime <= '{times[1]}'";
                sql = $"LoginName like '%{LoginName}%' and {timeSql}";
                date =BaseManager.GetAllManagerLike(sql);
            }  
            return GetJson(new PearTableReponseData(){ data = date})  ;
        }

        /// <summary>
        /// 修改管理员状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ManagerUpdateEnableState(int id)
        {
            try
            {
                if (id == 1)
                {
                    return Json(new {code = 500, msg = "不可修改默认账户"});
                }
                else
                {
                    BaseManager.ManagerUpdateByState(id);
                    return Json(new {code = 200});
                }
            }
            catch (Exception e)
            {
                return Json(new {code = 500, msg = e.Message});
            }
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ManagerRemove(long id)
        {
            BaseManager sve = new BaseManager();
            try
            {
                var item = sve.GetAllUserInRoles().Where(m => m.UserId == id).ToList();
                foreach (var i in item)
                {
                    sve.UserInRolesRemove(i.Id);
                }
                sve.ManagerRemove(id);
                return Json(new {code = 200});
            }
            catch (Exception e)
            {
                return Json(new {code = 500, msg = e.Message});
            }
        }

        [HttpGet]
        public ActionResult ManagerEdit(int id)
        {
            BaseManager sve = new BaseManager();
            var item = sve.ManagerGetOne(id);
            return View(new ManagerUpdataVm
            {
                Id = item.Id,
                LoginName = item.LoginName,
                LoginPwd = item.LoginPwd,
                FaceImage = item.FaceImagePath,
                CreateTime = item.CreateTime,
                LastLoginTime = item.LastLoginTime,
                IsEnable = item.IsEnable
            });
        }

        [HttpPost]
        public ActionResult ManagerEdit(ManagerUpdataVm model)
        {
            BaseManager sve = new BaseManager();
            try
            {
                sve.ManagerUpdate(new ManagerUser
                {
                    Id = model.Id,
                    LoginName = model.LoginName,
                    LoginPwd = model.LoginPwd,
                    FaceImagePath = model.FaceImage,
                    CreateTime = model.CreateTime,
                    LastLoginTime = model.LastLoginTime,
                    IsEnable = model.IsEnable
                });
                return Json(new {code = 200});
            }
            catch (Exception e)
            {
                return Json(new {code = 500, msg = e.Message});
            }
        }

        /// <summary>
        /// 用户设置角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ManageSetRoles(int id)
        {
            var roles = BaseManager.RoleGetAll().Select(m=>new SelectListItem()
            {
                Text = m.RoleName,
                Value = m.Id.ToString()
            }).ToList();
            var userInRoles = BaseManager.GetUserInRolesByUserId(id);

            foreach (var role in userInRoles)
            {
                roles.First(m => m.Value == role.RoleId.ToString()).Selected = true;
            }
        

            ViewBag.Roles = roles;
 
            
            return View(BaseManager.ManagerGetOne(id));
        }
        [HttpPost]
        public ActionResult ManageSetRoles(ManagerUser model)
        {
            var roles = BaseManager.RoleGetAll().Select(m=>new SelectListItem()
            {
                Text = m.RoleName,
                Value = m.Id.ToString()
            }).ToList();
            var userInRoles = BaseManager.GetUserInRolesByUserId(model.Id);
            return Json(userInRoles, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
    }
}