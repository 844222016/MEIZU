using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model.firstMvcModel
{
    /// <summary>
    /// 存货
    /// </summary>
    public class EditionInPhoneDescPic
    {
        /// <summary>
        /// 版本
        /// </summary>
        public int Edition { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public int PhoneDescPic { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 剩余数量
        /// </summary>
        public int Count { get; set; }
    }
}
