using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEIZU.DAL;
using MEIZU.DAL.FirstMvcDAL;
using MEIZU.DTO;
using MEIZU.Model;
using MEIZU.Model.firstMvcModel;

namespace MEIZU.BLL
{
    public class FirstManager
    {
        /// <summary>
        /// 实例化 用户登录注册 服务
        /// </summary>
        public UserAdminService ua { get; set; } = new UserAdminService();
        /// <summary>
        /// 首页获取所有轮播图片
        /// </summary>
        /// <returns></returns>
        public List<Pic> GetAllPic()
        {
            return new PicService().GetAll();
        }
        /// <summary>
        /// 修改轮播详情
        /// </summary>
        /// <param name="pic"></param>
        public void EditPic(Pic pic)
        {
            new PicService().Edit(new Pic()
            {
                Id = pic.Id,
                ImgName = pic.ImgName,
                ImgPath = pic.ImgPath,
                ImgUrl = pic.ImgUrl
            });
        }
        /// <summary>
        /// 查询单个轮播图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Pic GetonePic(int id)
        {
            return new PicService().GetOne(id);
        }
        /// <summary>
        /// 获取手机详情页所有图片
        /// </summary>
        /// <returns></returns>
        public List<PhoneDescPicDto> GetAllDescPic()
        {
            return new PhoneDescPicService().GetAll();
        }
        /// <summary>
        /// 获取手机详情页所有信息
        /// </summary>
        /// <returns></returns>
        public List<DTO.EditionInPhoneDescPicDTO> GetAllPhoneDesc()
        {
            return new PhoneDescService().GetAll();
        }
        /// <summary>
        /// 获取手机详情页所有信息
        /// </summary>
        /// <returns></returns>
        public List<DTO.EditionInPhoneDescPicDTO> GetOnePhoneDesc(int id)
        {
            return new PhoneDescService().GetOne(id);
        }

        /// <summary>
        /// 修改轮播是否启用
        /// </summary>
        /// <param name="id"></param>
        public void EditPicStateId(int id)
        {
            var item = new PicService().GetOne(id);
            var index = !item.IsEnable;
            new PicService().Edit(new Pic()
            {
                Id = item.Id,
                ImgName = item.ImgName,
                ImgPath = item.ImgPath,
                ImgPic = item.ImgPic,
                ImgUrl = item.ImgUrl,
                IsEnable = index
            });
        }

        /// <summary>
        /// 登录注册
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool Login(string name, string pwd)
        {
            return ua.Login(name, pwd);
        }

        /// <summary>
        /// 判断用户状态
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool JudgeState(string name)
        {
            return ua.JudgeState(name);
        }

        /// <summary>
        /// 判断该用户是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool JudgeUserLogin(string name)
        {
            return ua.JudgeUser(name);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public bool CreateUser(string name, string pwd)
        {
            return ua.CreateUser(name, pwd);
        }

        /// <summary>
        /// 更改用户 登录状态 
        /// </summary>
        /// <param name="name"></param>
        public void UpdataLogin(string name)
        {
            ua.JudgeLogin(name);
        }
        /// <summary>
        /// 更改用户 登录状态 
        /// </summary>
        /// <param name="name"></param>
        public void UpdataLoginFalse(string name)
        {
            ua.JudgeLoginFalse(name);
        }
        /// <summary>
        /// 获取简要商品数据
        /// </summary>
        /// <returns></returns>
        public List<ShoppingCodDto> GetAllCod()
        {
            return new ShoppingCodDtoService().GetAll();
        }
        /// <summary>
        /// 获取所有类别
        /// </summary>
        /// <returns></returns>
        public List<PhoneType> GetAllType()
        {
            return new PhoneTypeService().GetAll();
        }
        /// <summary>
        /// 添加类别根节点
        /// </summary>
        /// <param name="name"></param>
        public void CreateTypeName(string name)
        {
            new PhoneTypeService().Create(new PhoneType()
            {
                TypeName = name,
                TypeId = null,
            });
        }
        /// <summary>
        /// 修改商品类别
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeName"></param>
        /// <param name="typeId"></param>
        public void EditPhoneType(int id,string typeName,int? typeId)
        {
            new PhoneTypeService().Edit(new PhoneType()
            {
                Id = id,
                TypeName = typeName,
                TypeId = typeId == 0 ? (int?) null : typeId
            });
        }
        /// <summary>
        /// 查找所有订单信息
        /// </summary>
        /// <returns></returns>
        public List<ShoppingOrderDTO> GetAllOrder()
        {
            return new ShoppingOrderDTOService().GetAll();
        }
        /// <summary>
        /// 获取所有订单状态下拉框
        /// </summary>
        /// <returns></returns>
        public List<OrderState> GetAllOrderStates()
        {
            return new OrderStateService().GetAll();
        }
        /// <summary>
        /// 修改订单信息
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="orderName"></param>
        /// <param name="orderPhone"></param>
        /// <param name="orderAddress"></param>
        /// <param name="remaks"></param>
        public void EditOrder(int id,int userid,int shoppingcodid,int shopping,string codNumber,int stateId,string waypay,int count,string orderName, string orderPhone, string orderAddress, string remaks,DateTime createTime,string province,string county,string city)
        {
            new ShoppingOrderService().Edit(new ShoppingOrder()
            {
                Id = id,
                UserId = userid,
                ShoppingCodId = shoppingcodid,
                Shopping = shopping,
                CodNumber = codNumber,
                WayPay = waypay,
                Count = count,
                OrderState = stateId,
                OrderPhone = orderPhone,
                OrderName = orderName,
                OrderAddress = orderAddress,
                Remarks = remaks,
                CreateTime = createTime,
                Orderprovince = province,
                OrderCounty = county,
                OrderCity = city
            });
        }
        /// <summary>
        /// 获取单个订单信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ShoppingOrder GetOneOrder(long id)
        {
            return new ShoppingOrderService().GetOne(id);
        }
    }
}
