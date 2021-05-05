using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.Model.firstMvcModel;

namespace MEIZU.Model
{
    /// <summary>
    /// 订单
    /// </summary>
    public class ShoppingOrder : BaseModel
    {/// <summary>
     /// 用户
     /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 商品
        /// </summary>
        public int ShoppingCodId { get; set; }
        /// <summary>
        /// 存货
        /// </summary>
        public int Shopping { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string CodNumber { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderState { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string WayPay { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        public string OrderName { get; set; }
        public string OrderPhone { get; set; }
        public string Orderprovince { get; set; }
        public string OrderCity { get; set; }
        public string OrderCounty { get; set; }
        public string OrderAddress { get; set; }
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
