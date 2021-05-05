using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MEIZU.Tools;

namespace MEIZU.MVC.Models.MeiZuVm
{
    /// <summary>
    /// 用户页面初始数据
    /// </summary>
    public class UserVmModel
    {
        [UIHint("PearDataTable")]
        public PearTableData UserTableData { get; set; } = new PearTableData()
        {
            Id = "UserTable",
            WhereObj = new
            {
                id = 3
            },
            PearTableFeilds = new List<PearTableFeild>
            {
                new PearTableFeild
                {
                    title = "序号",
                    type = "numbers",
                    width = 100
                }
                ,
                new PearTableFeild
                {
                    field = "UserName",
                    title = "账号",
                    IsSearch = true,
                    width = 100,
                }
                ,
                new PearTableFeild
                {
                    field = "UserPwd",
                    title = "密码",
                    width = 100
                }
                ,
                new PearTableFeild
                {
                    field = "CreateTime",
                    title = "创建时间",
                }
                ,
                new PearTableFeild
                {
                    field = "LastTime",
                    title = "最后一次登录时间",
                }
                ,
                new PearTableFeild
                {
                    field = "States",
                    title = "用户是否登录",
                    templet = "#ManagerEnableTemplate",
                    width = 100
                },
                new PearTableFeild
                {
                    field = "Current",
                    title = "账号状态",
                    templet = "#ManagerEnableTemplate1",
                    width = 100
                }
            },
            IsShowDetail = false,
            IsShowEdit = false,
            EditFunc = "userEdit",
            IsShowDelete = true,
            DeleteFunc = "userDelete",
            DeleteWhere = "1===1",
            Url = "/MeiZu/UserData",
            // 右侧按钮
            RowBtns = new List<RowBtn>()
            {
                new RowBtn()
                {
                    EventName ="enableState",
                    Text = "启用账户",
                    ShowWhere = "d.Current=== false"
                },
                new RowBtn()
                {
                    EventName ="enableState",
                    Text = "禁用账户",
                    ShowWhere = "d.Current=== true"
                },
                new RowBtn()
                {
                    EventName ="setRole",
                    Text = "设置账户"
                },
            }
        };
    }

    public class UserCreateVm
    {
        /// <summary>
        /// 账号
        /// </summary>
        [UIHint("FormText")]
        [Display(Name = "账号")]
        [Required]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [UIHint("FormText")]
        [Display(Name = "密码")]
        [Required]
        public string UserPwd { get; set; }
    }
}