using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.Model.firstMvcModel
{
    public class Pic : BaseModel
    {
        public string ImgName { get; set; }
        public string ImgPath { get; set; }
        public string ImgUrl { get; set; }
        public string ImgPic { get; set; }
        public bool IsEnable { get; set; } = true;
    }
}
