using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DAL;
using MEIZU.DTO;
using MEIZU.Model;
using MEIZU.Model.firstMvcModel;

namespace MEIZU.BLL
{
    public class AfterEndManage
    {
        /// <summary>
        /// 后端用户管理
        /// </summary>
        public UserManagerService UserManagerService { get; set; } = new UserManagerService();

        #region 后端用户管理类

        public List<LoginVmDto> UserGetAll()
        {
            return UserManagerService.GetAll().Select(m=>new LoginVmDto()
            {
                Id = m.Id,
                UserName = m.UserName,
                UserPwd = m.UserPwd,
                CreateTime = m.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
                LastTime = m.LastTime.ToString("yyyy-MM-dd hh:mm:ss"),
                States = m.States,
                Current = m.Current
            }).ToList();
        }
        /// <summary>
        /// 通过账号获取数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<LoginVm> UserGetWhere(string where)
        {
            return UserManagerService.GetAllByWhere("UserName =" + where);
        }
        /// <summary>
        /// 用户状态更改
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public bool EditUserState(string UserName)
        {

            var list = UserGetWhere(UserName);
            foreach (var item in list)
            {
                var index = !item.Current;
                UserManagerService.Edit(new Model.firstMvcModel.LoginVm()
                {
                    Id = item.Id,
                    UserName = UserName,
                    UserPwd = item.UserPwd,
                    CreateTime = item.CreateTime,
                    LastTime = item.LastTime,
                    Current = index,
                    States = item.States
                });
            }

            return true;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        public void UserDelete(int id)
        {
            UserManagerService.Remove(id);
        }

        #endregion

    }
}
