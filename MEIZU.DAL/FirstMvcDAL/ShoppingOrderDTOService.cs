using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DTO;

namespace MEIZU.DAL.FirstMvcDAL
{
    public class ShoppingOrderDTOService
    {
        public List<ShoppingOrderDTO> GetAll()
        {
            return new DbHelper<ShoppingOrderDTO>().GetAllInnerJoin("inner join LoginVm on LoginVm.id = ShoppingOrder.UserId\r\ninner join ShoppingCod on ShoppingCod.Id = ShoppingOrder.ShoppingCodId\r\ninner join EditionInPhoneDescPic on EditionInPhoneDescPic.Id = ShoppingOrder.Shopping\r\ninner join OrderState on OrderState.Id = ShoppingOrder.OrderState\r\ninner join PhoneDescPic on PhoneDescPic.Id = EditionInPhoneDescPic.PhoneDescPic\r\ninner join Edition on Edition.Id = EditionInPhoneDescPic.Edition");
        }

        public ShoppingOrderDTO GetOne(int  id)
        {
            return new DbHelper<ShoppingOrderDTO>().GetOneInnerJoin("inner join LoginVm on LoginVm.id = ShoppingOrder.UserId\r\ninner join ShoppingCod on ShoppingCod.Id = ShoppingOrder.ShoppingCodId\r\ninner join EditionInPhoneDescPic on EditionInPhoneDescPic.Id = ShoppingOrder.Shopping\r\ninner join OrderState on OrderState.Id = ShoppingOrder.OrderState\r\ninner join PhoneDescPic on PhoneDescPic.Id = EditionInPhoneDescPic.PhoneDescPic\r\ninner join Edition on Edition.Id = EditionInPhoneDescPic.Edition  ", $"where ShoppingOrder.Id = {id}");
        }
    }
}
