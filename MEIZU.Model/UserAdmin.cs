using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model
{
    public class UserAdmin:BaseModel
    {
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime LastTime { get; set; } = DateTime.Now;
        public int States { get; set; } = 0;
    }
}
