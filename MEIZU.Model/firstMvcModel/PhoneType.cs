using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model.firstMvcModel
{
    /// <summary>
    /// 商品类别类
    /// </summary>
    public class PhoneType:BaseModel
    {
        /// <summary>
        /// 商品名
        /// </summary>
        public string TypeName { get; set; }
        /// <summary>
        /// 自引Id
        /// </summary>
        public int? TypeId { get; set; }
    }
}
