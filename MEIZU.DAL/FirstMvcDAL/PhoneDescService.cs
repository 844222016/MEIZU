using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DTO;
using MEIZU.Model.firstMvcModel;

namespace MEIZU.DAL.FirstMvcDAL
{
    public class PhoneDescService
    {
        public List<DTO.EditionInPhoneDescPicDTO> GetOne(int id)
        {
            return new DbHelper<DTO.EditionInPhoneDescPicDTO>().GetAllInnerJoin($"inner join Edition on Edition.Id = EditionInPhoneDescPic.Edition\r\ninner join ShoppingCod on ShoppingCod.Id = Edition.Id\r\ninner join PhoneDescPic on PhoneDescPic.Id = EditionInPhoneDescPic.PhoneDescPic\r\ninner join PhoneDescSonPic on PhoneDescSonPic.PicList = PhoneDescPic.Id where ShoppingId = {id}");
        }
        public List<DTO.EditionInPhoneDescPicDTO> GetOne(int id,int ShoppingId)
        {
            return new DbHelper<DTO.EditionInPhoneDescPicDTO>().GetAllInnerJoin($"inner join Edition on Edition.Id = EditionInPhoneDescPic.Edition\r\ninner join ShoppingCod on ShoppingCod.Id = Edition.Id\r\ninner join PhoneDescPic on PhoneDescPic.Id = EditionInPhoneDescPic.PhoneDescPic\r\ninner join PhoneDescSonPic on PhoneDescSonPic.PicList = PhoneDescPic.Id where ShoppingId = {id}");
        }

        public List<DTO.EditionInPhoneDescPicDTO> GetAll()
        {
            return new DbHelper<DTO.EditionInPhoneDescPicDTO>().GetAllInnerJoin(
                "inner join Edition on Edition.Id = EditionInPhoneDescPic.Edition\r\ninner join ShoppingCod on ShoppingCod.Id = Edition.Id\r\ninner join PhoneDescPic on PhoneDescPic.Id = EditionInPhoneDescPic.PhoneDescPic\r\ninner join PhoneDescSonPic on PhoneDescSonPic.PicList = PhoneDescPic.Id");
        }
    }
}
