using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MEIZU.BLL;
using MEIZU.Tools;

namespace MEIZU.MVC.Models.MeiZuVm
{
    public class PhoneDescVm
    {

        [UIHint("PearDataTable")]
        public PearTableData PicTableData { get; set; } = new PearTableData
        {
            Id = "PhoneTable",
            Url = "/MeiZu/GetAllPhone",
            IsShowDelete = true,
            DeleteFunc = "PhoneDelete",
            IsShowEdit = true,
            EditFunc = "PhoneEdit",
            DeleteWhere = "d.RoleName!='admin'",
            RowBtns = new List<RowBtn>()
            {
                new RowBtn()
                {
                    EventName = "enableState",
                    Text = "启用",
                    ShowWhere = "d.IsEnable === false"
                },
                new RowBtn()
                {
                    EventName = "enableState",
                    Text = "禁用",
                    ShowWhere = "d.IsEnable === true"
                },
            },
            PearTableFeilds = new List<PearTableFeild>
            {
                new PearTableFeild
                {
                    title  = "编号",
                    type = "numbers"
                },
                new PearTableFeild()
                {
                    field  = "Id",
                    hide = true
                },
                new PearTableFeild
                {
                    field = "Name",
                    title = "手机名称"
                },
                new PearTableFeild()
                {
                    field = "Title",
                    title = "手机标题"
                },
                new PearTableFeild
                {
                    field = "Configure",
                    title = "手机配置"
                },
                new PearTableFeild
                {
                    field = "ShoppingId",
                    title = "跳转编号"
                },
                new PearTableFeild
                {
                    field = "CodState",
                    title = "是否启用"
                },
                new PearTableFeild()
                {
                    field = "EditionName",
                    title = "版本信息",
                }
            }
        };
    }

    public class PhoneDesc
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Configure { get; set; }
        public int PhoneType { get; set; }
        public int ShoppingId { get; set; }
        public bool CodState { get; set; }
    }

}