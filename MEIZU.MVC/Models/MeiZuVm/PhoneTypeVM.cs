using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MEIZU.MVC.Models.MeiZuVm
{
    public class PhoneTypeVM
    {
        public int Id { get; set; }
        [Required]
        [UIHint("FormText")]
        [DisplayName("名称")]
        public string TypeName { get; set; }
        [Required]

        [UIHint("FormText")]
        public int? TypeId { get; set; }
    }
}