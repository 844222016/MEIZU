using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DAL;
using MEIZU.DTO;
using MEIZU.Model;

namespace MEIZU.BLL
{
    public class BaseManager
    {
        /// <summary>
        /// 获取特定组的所有值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Model.Dictionary> GetDictionariesByGroupName(string name)
        {
            DAL.DictionaryService dicSvc = new DictionaryService();
            return dicSvc.GetAllByWhere($"GroupName = '{name}'");
        }
        /// <summary>
        /// 创建dic数据
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="value"></param>
        public void CreateDictionary(string groupName, string value)
        {

            DAL.DictionaryService dicSvc = new DictionaryService();
            dicSvc.Create(new Dictionary() { GroupName = groupName, Value = value });
        }
        /// <summary>
        /// 获取所有的dic信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDictionaryGroupName()
        {
            DictionaryService dicSvc = new DictionaryService();
            return dicSvc.GetAllGroupName();
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public void RemoveDictionary(long id)
        {

            DictionaryService dicSvc = new DictionaryService();
            dicSvc.Remove(id);
        }

        public bool UserManagerLogin(string name, string pwd)
        {
            ManagerUserService userSvc = new ManagerUserService();
            return userSvc.Login(name, pwd);
        }

        /// <summary>
        /// 获取所有菜单数据
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetAllMenus()
        {
            MenuService menuSvc = new MenuService();
            return menuSvc.GetAll().OrderBy(m => m.Order).ToList();
        }

        /// <summary>
        /// 获取当前管理员所有菜单数据,
        /// </summary>
        /// <returns></returns>
        public List<Menu> GetAllMenus(string name)
        {
            MenuService menuSvc = new MenuService();
            return menuSvc.GetAllByRoleName(name).OrderBy(m => m.Order).ToList();
        }

        /// <summary>
        /// 获取某个角色的所有菜单数据
        /// </summary>
        /// <returns></returns>
        public List<MenuInRole> GetAllMenusByRoleId(int roleId)
        {
            MenuInRoleService menuSvc = new MenuInRoleService();
            return menuSvc.GetAllByWhere("RoleId = " + roleId).ToList();
        }


        public List<Dictionary> GetAllDictinarys(string where, int pageIndex = 0, int pageSize = 10)
        {
            DictionaryService dicSvc = new DictionaryService();
            return dicSvc.GetAllByPageOrderBy("GroupName", where, pageIndex, pageSize);
        }

        public int DictinaryCount(string where = "1=1")
        {
            DictionaryService dicSvc = new DictionaryService();
            return dicSvc.Count(where);
        }
        /// <summary>
        /// 获取单个数据字典对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dictionary GetOneDictionary(long id)
        {
            DictionaryService dicSvc = new DictionaryService();
            return dicSvc.GetOne(id);

        }
        /// <summary>
        /// 修改数据字典
        /// </summary>
        /// <param name="model"></param>
        public void EditDictionary(Dictionary model)
        {
            DictionaryService dicSvc = new DictionaryService();
            dicSvc.Edit(model);
        }
        /// <summary>
        /// 查询单个菜单对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Menu MenuGetOneById(long id)
        {
            MenuService menuService = new MenuService();
            return menuService.GetOne(id);
        }
        /// <summary>
        /// 获取满足条件的菜单
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Menu> MenusGetAllByWhere(string where)
        {
            return new MenuService().GetAllByWhere(where);
        }
        /// <summary>
        /// 获取所有菜单类别
        /// </summary>
        /// <returns></returns>
        public List<MenuType> MenuTypesGetAll()
        {
            return new MenuTypeService().GetAll();
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        /// <param name="menu"></param>
        public void EditMenu(Menu menu)
        {
            MenuService menuService = new MenuService();
            menuService.Edit(menu);
        }

        /// <summary>
        /// 移动菜单顺序
        /// </summary>
        /// <param name="moveUp"></param>
        /// <param name="parentId"></param>
        /// <param name="id"></param>
        public void MenuMove(bool moveUp, long? parentId, long id)
        {
            var svc = new MenuService();
            var menus = svc.GetAllByWhere(parentId == 0 ? $"ParentMenuId is null" : $"ParentMenuId = {parentId}");

            var currentMenu = menus.First(m => m.Id == id);

            Menu upObj = null;
            if (moveUp)
            {

                upObj = menus
                    .Where(m => m.Order < currentMenu.Order)
                    .OrderBy(m => m.Order)
                    .Last();
            }
            else
            {

                upObj = menus
                    .Where(m => m.Order > currentMenu.Order)
                    .OrderByDescending(m => m.Order)
                    .Last();
            }

            //两个变量数据交换
            var cOrder = currentMenu.Order;
            currentMenu.Order = upObj.Order;
            upObj.Order = cOrder;

            svc.Edit(currentMenu);
            svc.Edit(upObj);


        }

        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="menu"></param>
        public void CreateMenu(Menu menu)
        {
            MenuService svc = new MenuService();
            svc.Create(menu);
        }
        /// <summary>
        /// 获取所有的角色数据
        /// </summary>
        /// <returns></returns>
        public List<Role> RoleGetAll()
        {
            RoleService roleService = new RoleService();
            return roleService.GetAll();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="roleName"></param>
        public void RoleCreate(string roleName)
        {
            RoleService roleService = new RoleService();

            if (roleService.Count($"RoleName = '{roleName}'") == 0)
            {
                roleService.Create(new Role
                {
                    RoleName = roleName
                });
            }
            else
            {
                throw new Exception("角色已经存在");
            }

        }
        /// <summary>
        /// 最大的排序数字
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public int MenuMax(long? parentId)
        {
            MenuService menuSve = new MenuService();

            if (parentId is null)
            {
                if (menuSve.Count($"ParentMenuId is null") == 0)
                {
                    return 1;
                }
                return menuSve.GetAllByWhere($"ParentMenuId is null").OrderBy(m => m.Order).Last().Order + 1;
            }

            if (menuSve.Count($"ParentMenuId = {parentId}") == 0)
            {
                return 1;
            }
            return menuSve.GetAllByWhere($"ParentMenuId = {parentId}").OrderBy(m => m.Order).Last().Order + 1;
        }
        /// <summary>
        /// 添加父节点
        /// </summary>
        /// <param name="model"></param>
        public void CreateMenuParent(Menu model)
        {
            MenuService menuSve = new MenuService();
            menuSve.Create(model);
        }


        /// <summary>
        /// 删除父级节点
        /// </summary>
        /// <param name="id"></param>
        public void MenuRemoveByParent(long id)
        {
            MenuService menuSve = new MenuService();
            var item = menuSve.GetAllByWhere($"ParentMenuId = {id}");
            foreach (var t in item)
            {
                RoleRemove(id);
                menuSve.Remove(t.Id);
            }
            menuSve.Remove(id);
        }

        /// <summary>
        /// 删除子节点
        /// </summary>
        /// <param name="id">菜单Id</param>
        public void MenuRemoveByChild(long id)
        {
            MenuService menuSve = new MenuService();
            RoleRemove(id);
            menuSve.Remove(id);
        }
        /// <summary>
        /// 删除角色权限
        /// </summary>
        /// <param name="id">菜单Id</param>
        public void RoleRemove(long id)
        {
            RoleService roleService = new RoleService();
            MenuInRoleService roleSve = new MenuInRoleService();
            var item = roleSve.GetAllByWhere($"MenuId = {id}");
            foreach (var i in item)
            {
                roleSve.Remove(i.Id);
            }
            roleService.Remove(id);
        }


        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="model"></param>
        public void RoleEdit(Role model)
        {
            RoleService roleSve = new RoleService();
            roleSve.Edit(model);
        }

        /// <summary>
        /// 获取单个角色对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetOneRoleById(int id)
        {
            RoleService sve = new RoleService();
            return sve.GetOne(id);
        }


        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public ManagerUser GetManager(string loginName)
        {
            ManagerUserService svc = new ManagerUserService();
            return svc.GetAllByWhere($"LoginName = '{loginName}'")[0];
        }
        /// <summary>
        /// 创建管理员用户
        /// </summary>
        /// <param name="modelLoginName"></param>
        /// <param name="modelLoginPwd"></param>
        /// <param name="modelFaceImage"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ManagerCreate(string modelLoginName, string modelLoginPwd, string modelFaceImage)
        {
            ManagerUserService svc = new ManagerUserService();
            svc.Create(new ManagerUser()
            {
                LoginName = modelLoginName,
                LoginPwd = modelLoginPwd,
                FaceImagePath = modelFaceImage
            });
        }

        /// <summary>
        /// 获取所有管理员信息
        /// </summary>
        /// <returns></returns>
        public List<ManagerUserDto> GetAllManager(string name)
        {
            ManagerUserService svc = new ManagerUserService();
            return svc.GetAll().Select(m => new ManagerUserDto()
            {
                Id = m.Id,
                LoginName = m.LoginName,
                CreateTime = m.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
                FaceImagePath = m.FaceImagePath,
                LastLoginTime = m.LastLoginTime.ToString("yyyy-MM-dd hh:mm:ss"),
                IsEnable = m.IsEnable
            }).ToList();
        }



        /// <summary>
        /// 通过模糊查询获取管理员信息
        /// </summary>
        /// <param name="likeSql"></param>
        /// <returns></returns>
        public List<ManagerUserDto> GetAllManagerLike(string likeSql)
        {
            ManagerUserService sve = new ManagerUserService();
            return sve.GetAllByLike(likeSql).Select(m => new ManagerUserDto()
            {
                Id = m.Id,
                LoginName = m.LoginName,
                CreateTime = m.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
                FaceImagePath = m.FaceImagePath,
                LastLoginTime = m.LastLoginTime.ToString("yyyy-MM-dd hh:mm:ss"),
                IsEnable = m.IsEnable
            }).ToList();
        }



        /// <summary>
        /// 修改管理员状态
        /// </summary>
        public void ManagerUpdateByState(int id)
        {
            ManagerUserService sve = new ManagerUserService();
            var item = sve.GetOne(id);
            var index = !item.IsEnable;
            sve.Edit(new ManagerUser
            {
                Id = id,
                LoginName = item.LoginName,
                LoginPwd = item.LoginPwd,
                CreateTime = item.CreateTime,
                FaceImagePath = item.FaceImagePath,
                IsEnable = index,
                LastLoginTime = item.LastLoginTime
            });
        }

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="model"></param>
        public void ManagerUpdate(ManagerUser model)
        {
            ManagerUserService sve = new ManagerUserService();
            sve.Edit(model);
        }

        /// <summary>
        /// 全部管理员与权限关系表
        /// </summary>
        /// <returns></returns>
        public List<UserInRole> GetAllUserInRoles()
        {
            UserInRoleService sve = new UserInRoleService();
            return sve.GetAll();
        }
        /// <summary>
        /// 获取特定用户的角色列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<UserInRole> GetUserInRolesByUserId(long id)
        {
            UserInRoleService svc = new UserInRoleService();
            return svc.UserInRolesWhereManagerId(id);
        }

        /// <summary>
        /// 删除管理员与权限关系
        /// </summary>
        /// <param name="id"></param>
        public void UserInRolesRemove(long id)
        {
            UserInRoleService sve = new UserInRoleService();
            sve.Remove(id);
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="id"></param>
        public void ManagerRemove(long id)
        {
            ManagerUserService sve = new ManagerUserService();
            sve.Remove(id);
        }

        /// <summary>
        /// 获得单个管理员对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ManagerUser ManagerGetOne(int id)
        {
            ManagerUserService sve = new ManagerUserService();
            return sve.GetOne(id);
        }


        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="menuId">权限Id</param>
        public void CreateMenuInRole(int roleId, int menuId)
        {
            MenuInRoleService sve = new MenuInRoleService();
            sve.Create(new MenuInRole()
            {
                MenuId = menuId,
                RoleId = roleId
            });
        }

        /// <summary>
        /// 删除当前用户的全部权限
        /// </summary>
        /// <param name="roleId"></param>
        public void RemoveMenuInRole(int roleId)
        {
            MenuInRoleService sve = new MenuInRoleService();
            sve.RemoveRoleId(roleId);
        }

        /// <summary>
        /// 给用户添加角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        public void CreateUserInRole(int userId, int roleId)
        {
            UserInRoleService sve = new UserInRoleService();
            sve.Create(new UserInRole
            {
                RoleId = roleId,
                UserId = userId
            });
        }

        /// <summary>
        /// 删除当前用户的全部角色
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveUserInRole(int userId)
        {
            UserInRoleService sve = new UserInRoleService();
            sve.RemoveUserId(userId);

        }

        public bool UserAdminLogin(string name, string pwd)
        {
            UserAdminService userSvc = new UserAdminService();
            return userSvc.Login(name, pwd);
        }
    }
}