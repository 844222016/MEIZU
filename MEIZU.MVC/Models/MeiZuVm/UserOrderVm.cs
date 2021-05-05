using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MEIZU.Tools;

namespace MEIZU.MVC.Models.MeiZuVm
{
    public class UserOrderVm
    {
        [UIHint("PearDataTable")]
        public PearTableData UserTableData { get; set; } = new PearTableData()
        {
            Id = "UserOrderTable",
            PearTableFeilds = new List<PearTableFeild>
            {
                new PearTableFeild()
                {
                    title = "用户名称",
                    field = "UserName"
                },
                new PearTableFeild()
                {
                    title = "商品名称",
                    field = "Name"
                },
                new PearTableFeild()
                {
                    title = "版本名称",
                    field = "EditionName"
                },
                new PearTableFeild()
                {
                    title = "数量",
                    field = "Count"
                },
                new PearTableFeild()
                {
                    title = "订单状态",
                    field = "StateName"
                },
            },
            IsShowDetail = true,
            DetailFunc = "OrderDetail",
            IsShowEdit = true,
            EditFunc = "OrderEdit",
            IsShowDelete = true,
            DeleteFunc = "OrderDelete",
            DeleteWhere = "1===1",
            Url = "/MeiZu/GetAllUserOrder",
            // 右侧按钮
            //RowBtns = new List<RowBtn>()
            //{
            //    new RowBtn()
            //    {
            //        EventName ="enableState",
            //        Text = "启用账户",
            //        ShowWhere = "d.Current=== false"
            //    },
            //}
        };
    }
    public class UserOrderDetailVm
    {
        [UIHint("PearDataTable")]
        public PearTableData UserTableData { get; set; } = new PearTableData()
        {
            Id = "UserOrderDetailTable",
            OtherBtns =  new Dictionary<string, string>(),
            PearTableFeilds = new List<PearTableFeild>
            {
                new PearTableFeild()
                {
                    title = "用户名称",
                    field = "UserName"
                },
                new PearTableFeild()
                {
                    title = "商品名称",
                    field = "Name"
                },
                new PearTableFeild()
                {
                    title = "版本名称",
                    field = "EditionName"
                },
                new PearTableFeild()
                {
                    title = "数量",
                    field = "Count"
                },
                new PearTableFeild()
                {
                    title = "订单状态",
                    field = "StateName"
                },
                new PearTableFeild()
                {
                    title = "手机颜色",
                    field = "PicName"
                },
                new PearTableFeild()
                {
                    title = "总价",
                    field = "Price"
                },
                new PearTableFeild()
                {
                    title = "收货人",
                    field = "OrderName"
                },
                new PearTableFeild()
                {
                    title = "联系电话",
                    field = "OrderPhone"
                },
                new PearTableFeild()
                {
                    title = "收货地址",
                    field = "OrderAddress"
                },
                new PearTableFeild()
                {
                    title = "下单时间",
                    field = "CreateTime"
                },
            },
            Url = "/MeiZu/GetAllUserOrderDetail"
        };
    }
    public class ShoppingOrder
    {

        [UIHint("FormInt")]
        [DisplayName("用户编号")]
        public long Id { get; set; }
        [UIHint("FormText")]
        [DisplayName("用户账号")]
        public string UserName { get; set; }
        [UIHint("FormText")]
        [DisplayName("商品名称")]
        public string Name { get; set; }
        [UIHint("FormText")]
        [DisplayName("版本名称")]
        public string EditionName { get; set; }
        [UIHint("FormText")]
        [DisplayName("所选颜色")]
        public string PicName { get; set; }
        [UIHint("FormText")]
        [DisplayName("总价")]
        public decimal Price { get; set; }
        [UIHint("FormInt")]
        [DisplayName("数量")]
        public int Count { get; set; }
        [UIHint("FormText")]
        [DisplayName("订单状态")]
        public string StateName { get; set; }
        [UIHint("FormText")]
        [DisplayName("收货人")]
        public string OrderName { get; set; }
        [UIHint("FormText")]
        [DisplayName("手机号")]
        public string OrderPhone { get; set; }

        [UIHint("FormText")]
        [DisplayName("省")]
        public string OrderProvince { get; set; }
        [UIHint("FormText")]
        [DisplayName("市")]
        public string OrderCity { get; set; }
        [UIHint("FormText")]
        [DisplayName("区")]
        public string OrderCounty { get; set; }   
        public string S { get; set; }
        [UIHint("FormText")]
        [DisplayName("地址")] 
        public string OrderAddress { get; set; }
        [UIHint("FormText")]
        [DisplayName("备注")]
        public string Remarks { get; set; }
        public DateTime CreateTime { get; set; }

    }
}