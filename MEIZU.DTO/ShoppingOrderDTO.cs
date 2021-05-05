using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.Model;

namespace MEIZU.DTO
{
    public class ShoppingOrderDTO
    {
        [AttClass("ShoppingOrder")]
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string EditionName { get; set; }
        [AttClass("PhoneDescPic")]
        public string PicName { get; set; }
        public decimal Price { get; set; }
        [AttClass("ShoppingOrder")]
        public int Count { get; set; }
        public string StateName { get; set; }
        public string OrderName { get; set; }
        public string OrderPhone { get; set; }
        public string Orderprovince { get; set; }
        public string OrderCity { get; set; }
        public string OrderCounty { get; set; }
        public string OrderAddress { get; set; }
        public string Remarks { get; set; }
        [AttClass("ShoppingOrder")]
        public DateTime CreateTime { get; set; }

    }
}
