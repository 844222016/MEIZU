using System.ComponentModel.DataAnnotations;

namespace MEIZU.Models
{
    public class AccountLoginVm
    {
        [Display(Name = "账号")]
        public string LoginName { get; set; }
        
        [Display(Name = "密码")]
        public string LoginPwd { get; set; }
    }
}