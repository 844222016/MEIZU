using System.ComponentModel.DataAnnotations;

namespace MEIZU.Models
{
    public class InstallViewModel
    {
        [Display(Name = "服务器地址")]
        [UIHint("FormText")]
        public string ServerHost { get; set; } = ".";

        [Display(Name = "服务器账号")]
        [UIHint("FormText")]
        public string ServerName { get; set; } = "sa";

        [Display(Name = "服务器密码")]
        [UIHint("FormText")]
        public string ServerPwd { get; set; } = "sa";

        [Display(Name = "服务器数据库名称")]
        [UIHint("FormText")]
        public string DbName { get; set; } = "PearAdminDb";
    }
}