using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model.firstMvcModel
{
    /// <summary>
    /// 购物车
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public int ShoppingCodId { get; set; }
        /// <summary>
        /// 存货
        /// </summary>
        public int Shopping { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
