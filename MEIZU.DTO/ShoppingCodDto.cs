using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.Model;

namespace MEIZU.DTO
{
    public class ShoppingCodDto
    {
        [AttClass("ShoppingCod")]
        public string Name { get; set; }
        [AttClass("ShoppingCod")]
        public string Title { get; set; }
        public string Configure { get; set; }
        public string EditionName { get; set; }
        public int ShoppingId { get; set; }
        public bool CodState { get; set; }
    }
}
