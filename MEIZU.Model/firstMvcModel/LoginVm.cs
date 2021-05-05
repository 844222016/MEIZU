using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model.firstMvcModel
{
    /// <summary>
    /// 后台查看管理用户类
    /// </summary>
    public class LoginVm : BaseModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPwd { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public DateTime LastTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 登录状态 0 未登录 1 以登录
        /// </summary>
        public bool States { get; set; }
        /// <summary>
        /// 用户 账号状态 0 该账户正常 1 该账户被封禁
        /// </summary>
        public bool Current { get; set; }
    }
}
