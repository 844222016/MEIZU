using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DTO;

namespace MEIZU.DAL.FirstMvcDAL
{
    public class ShoppingCodDtoService
    {
        public List<ShoppingCodDto> GetAll()
        {
            return new DbHelper<ShoppingCodDto>().GetAllInnerJoin(
                "inner join Edition on Edition.ShoppingCod = ShoppingCod.Id");
        }
    }
}
