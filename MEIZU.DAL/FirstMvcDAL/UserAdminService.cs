using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DTO;
using MEIZU.Model;
using MEIZU.Model.firstMvcModel;

namespace MEIZU.DAL
{
    #region 前端用户
    public class UserAdminService : BaseService<LoginVm>
    {
        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool Login(string name, string pwd)
        {
            // 查到的数据大于 0 则有该用户
            return (int)SuperDbHelper.SelectForScalar(
                @"select count(0) from LoginVm where UserName = @UserName and UserPwd= @UserPwd",
                new SqlParameter("@UserName",name),
                new SqlParameter("@UserPwd", pwd)
                ) > 0;
        }

        /// <summary>
        /// 注册时添加 用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CreateUser(string name,string pwd)
        {
            return SuperDbHelper
                .Update("insert into LoginVm values(@name,@pwd,getdate(),getdate(),0,0)",
                    new SqlParameter("@name", name),
                    new SqlParameter("@pwd", pwd)
                    );
        }

        /// <summary>
        /// 注册时 获取该用户 是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool JudgeUser(string name)
        {
            return (int)SuperDbHelper
                .SelectForScalar("select count(0) from LoginVm where UserName=@name",
                    new SqlParameter("@name", SqlDbType.NVarChar) { Value = name })>0;
        }
        
        /// <summary>
        /// 登录时 获取该用户 状态是否正常
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool JudgeState(string name)
        {
            return (bool)SuperDbHelper
                .SelectForScalar("select [current] from LoginVm where userName=@state",
                    new SqlParameter("@state", SqlDbType.NVarChar) { Value = name });
        }
        /// <summary>
        /// 更改用户登录状态
        /// </summary>
        public bool JudgeLogin(string name)
        {
            return SuperDbHelper.Update("update LoginVm set states = 1 where UserName = @name",
                new SqlParameter("@name", SqlDbType.NVarChar) {Value = name});
        }
        /// <summary>
        /// 更改用户登录状态
        /// </summary>
        public bool JudgeLoginFalse(string name)
        {
            return SuperDbHelper.Update("update LoginVm set states = 0 where UserName = @name",
                new SqlParameter("@name", SqlDbType.NVarChar) { Value = name });
        }

    }
    #endregion

    #region 魅族后台用户管理方法

    /// <summary>
    /// 用户管理 方法
    /// </summary>
    public class UserManagerService : BaseService<LoginVm>
    {

    }

    #endregion
}
