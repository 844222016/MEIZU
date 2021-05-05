using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DAL;
using MEIZU.Model;

namespace MEIZU.DAL
{
    public class BaseService<T> where T : BaseModel, new()
    {
        public SuperDbHelper<T> SuperDbHelper { get; set; } = new SuperDbHelper<T>();
        public void Create(T model)
        {
            SuperDbHelper.Create(model);
        }

        public void Edit(T model)
        {
            SuperDbHelper.Updates(model);
        }

        public void Remove(T model)
        {
            SuperDbHelper.Delete(model);
        }
        public void Remove(long id)
        {
            SuperDbHelper.Delete(id);
        }

        public void RemoveRoleId(long id)
        {
            SuperDbHelper.DeleteByRoleId(id);
        }

        public void RemoveUserId(long id)
        {
            SuperDbHelper.DeleteByUserId(id);
        }

        public T GetOne(long id)
        {
            return SuperDbHelper.GetOne(id);
        }
        /// <summary>
        /// 通过管理员名称获的此管理员的权限
        /// </summary>
        /// <param name="name">管理员名称</param>
        /// <returns></returns>
        public List<Menu> GetAllByRoleName(string name)
        {
            var sql = $@"select Id,MenuText,ParentMenuId,MenuTypeId,IconString,[Url],[Order] from Menu
                            where Menu.Id in (select MenuId from MenuInRole
                            where RoleId = (select RoleId from UserInRole
                            where UserId  = (select Id from ManagerUser where LoginName = @name)))";
            return SuperDbHelper.GetListBySql<Menu>(sql, new SqlParameter("@name", name));
        }

        public List<T> GetAll()
        {
            return SuperDbHelper.GetAll();
        }

        public List<T> GetAllByWhere(string where)
        {
            return SuperDbHelper.GetAll(where);
        }
        public List<T> GetAllByPage(string where, int pageIndex = 0, int pageSize = 10)
        {
            return SuperDbHelper.GetAllByPage(where, pageIndex, pageSize);
        }

        public List<T> GetAllByLike(string like)
        {
            return SuperDbHelper.GetAllByLike(like);
        }
        public int Count(string where)
        {
            return SuperDbHelper.Count(where);

        }

        public List<T> GetAllByPageOrderBy(string orderBy, string where, int pageIndex = 0, int pageSize = 10)
        {
            return SuperDbHelper.GetAllByPageOrderBy(orderBy, where, pageIndex, pageSize);
        }

    }

    public class DictionaryService : BaseService<Model.Dictionary>
    {
        /// <summary>
        /// 获取所有的组名
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllGroupName()
        {
            var cmd = SuperDbHelper.Cmd;
            cmd.CommandText = "select distinct  GroupName from Dictionary";
            var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            List<string> groups = new List<string>();
            while (dr.Read())
            {
                groups.Add(dr.GetString(0));
            }
            dr.Close();
            return groups;
        }
    }
    public class ManagerUserService : BaseService<Model.ManagerUser>
    {
        public bool Login(string name, string pwd)
        {
            var sql = @"select count(0) from ManagerUser where LoginName=@name and LoginPwd=@pwd";
            return (int)SuperDbHelper.SelectForScalar(sql, new[] { new SqlParameter("@name", name), new SqlParameter("@pwd", pwd) }) > 0;
        }
    }
    public class MenuTypeService : BaseService<Model.MenuType>
    {

    }
    public class MenuService : BaseService<Model.Menu>
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        public void CreateMenu(Menu menu)
        {
            var menus = GetAllByWhere(
                 $"ParentMenuId {(menu.ParentMenuId == null || menu.ParentMenuId == 0 ? "is null" : "=" + menu.ParentMenuId)}");
            if (menus.Count == 0)
            {
                menu.Order = 1;
            }
            else
            {
                menu.Order = menus.Max(m => m.Order) + 1;
            }
            Create(menu);
        }
    }
    public class RoleService : BaseService<Model.Role>
    {

    }
    public class UserInRoleService : BaseService<Model.UserInRole>
    {
        public List<UserInRole> UserInRolesWhereManagerId(long id)
        {

            return GetAllByWhere("UserId = " + id);
        }

    }
    public class MenuInRoleService : BaseService<Model.MenuInRole>
    {


    }
}