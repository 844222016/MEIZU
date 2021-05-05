using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model.firstMvcModel
{
    /// <summary>
    /// 商品详情
    /// </summary>
    public class ShoppingCod:BaseModel
    {
        /// <summary>
        /// 商品名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public string Configure { get; set; }
        /// <summary>
        /// 商品类别
        /// </summary>
        public int PhoneType { get; set; }
        /// <summary>
        /// ???
        /// </summary>
        public int ShoppingId { get; set; }
        public bool CodState { get; set; }
    }
}
