using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MEIZU.Tools;

namespace MEIZU.MVC.Models.UserSys
{
    public class PicViewModel
    {
        [UIHint("PearDataTable")]
        public PearTableData PicTableData { get; set; } = new PearTableData
        {
            Id = "PicTable",
            Url = "/MeiZu/GetAllIndexPic",
            IsShowDelete = true,
            DeleteFunc = "PicDelete",
            IsShowEdit = true,
            EditFunc = "PicEdit",
            DeleteWhere = "d.RoleName!='admin'",
            OtherBtns =  new Dictionary<string, string>(),
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
                    field = "ImgName",
                    title = "图片名称"
                },
                new PearTableFeild()
                {
                    field = "ArgImgPic",
                    title = "预览图片"
                },
                new PearTableFeild
                {
                    field = "ImgPath",
                    title = "图片路径"
                },
                new PearTableFeild
                {
                    field = "ImgUrl",
                    title = "跳转地址"
                },
                new PearTableFeild
                {
                    field = "Id",
                    hide = true,
                    title = "路径"
                },
                new PearTableFeild()
                {
                    field = "IsEnable",
                    title = "状态",
                    templet = "#ManagerEnableTemplate"
                }
            }
        };
        public int Id { get; set; }
        [DisplayName("图片名称")]
        [UIHint("FormText")]
        public string Name { get; set; }
        [DisplayName("图片路径")]
        [UIHint("FormText")]
        [Required]
        public string Path { get; set; }
        [UIHint("FormText")]
        [DisplayName("跳转路径")]
        public string Url { get; set; }

        [UIHint("FormText")]
        public bool IsEnable { get; set; }
        [UIHint("FormText")]
        public string ImgPic { get; set; }
    }
}