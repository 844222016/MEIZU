using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MEIZU.MVC.Models.MeiZuVm
{
    public class LoginVm
    {
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("账号")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        public string UserPwd { get; set; }
    }
}