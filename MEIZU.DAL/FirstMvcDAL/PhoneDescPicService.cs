using MEIZU.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEIZU.DAL.FirstMvcDAL
{
    public class PhoneDescPicService
    {
        public List<PhoneDescPicDto> GetAll()
        {
            return new DbHelper<PhoneDescPicDto>().GetAllInnerJoin("inner join PhoneDescSonPic on PhoneDescPic.Id = PhoneDescSonPic.PicList");
        }
    }
}
