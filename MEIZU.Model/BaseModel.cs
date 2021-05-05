using System;

namespace MEIZU.Model
{
    public class BaseModel
    {
        public long Id { get; set; } 
    }

    public class Dictionary:BaseModel
    { 
        public string GroupName { get; set; }
        public string Value { get; set; }
    }

    public class ManagerUser:BaseModel
    {
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string FaceImagePath { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime LastLoginTime { get; set; } = DateTime.Now;
        public bool IsEnable { get; set; } = true;
    }

    public class MenuType:BaseModel
    {
        public string TypeName { get; set; }
    }

    public class Menu:BaseModel
    {
        public string MenuText { get; set; }
        public long? ParentMenuId { get; set; }
        public long? MenuTypeId { get; set; }
        public string IconString { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
    }

    public class Role:BaseModel
    {
        public string RoleName { get; set; }
    }

    public class UserInRole:BaseModel
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }
    }
    public class MenuInRole : BaseModel
    {
        public long RoleId { get; set; }
        public long MenuId { get; set; }
        
    }

    public class DbName : BaseModel
    {
        public string DbNames { get; set; }
    }
}