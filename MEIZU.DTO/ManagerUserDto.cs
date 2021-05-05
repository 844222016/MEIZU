namespace MEIZU.DTO
{

    /// <summary>
    /// 管理员DTO 类
    /// </summary>
    public class ManagerUserDto
    {
        public long Id { get; set; }
        public string LoginName { get; set; } 
        public string FaceImagePath { get; set; }
        public string CreateTime { get; set; }  
        public string LastLoginTime { get; set; }  
        public bool IsEnable { get; set; } = true;
    }

    public class LoginVmDto
    {
        public long Id { get; set; }

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
        public string CreateTime { get; set; }
        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        public string LastTime { get; set; }
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