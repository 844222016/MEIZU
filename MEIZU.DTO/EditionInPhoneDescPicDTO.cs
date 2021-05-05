using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.Model;

namespace MEIZU.DTO
{
    /// <summary>
    /// 手机详情DTO
    /// </summary>
    public class EditionInPhoneDescPicDTO
    {

        public string Name { get; set; }

        public string Title { get; set; }
        public string Configure { get; set; }
        public string PicName { get; set; }
        public string SonPicPath { get; set; }
        public int ShoppingId { get; set; }
        public string EditionName { get; set; }
        public int Count { get; set; }
    }
}
