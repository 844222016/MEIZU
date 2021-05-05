using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MEIZU.Tools;

namespace MEIZU.Models
{
    /// <summary>
    /// 添加管理员的Vm
    /// </summary>
    public class ManagerCreateVm
    {
        [UIHint("FormText")]
        [Display(Name = "账号")]
        public string LoginName { get; set; }
        [UIHint("FormText")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string LoginPwd { get; set; } 
        [Display(Name = "头像")]
        [UIHint("FormText")]
        public string FaceImage { get; set; }
    }
    
    /// <summary>
    /// 修改管理员的Vm
    /// </summary>
    public class ManagerUpdataVm
    {
        [UIHint("FormInt")]
        public long Id { get; set; }
        [UIHint("FormText")]
        [Display(Name = "账号")]
        public string LoginName { get; set; }
        [UIHint("FormText")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string LoginPwd { get; set; } 
        [Display(Name = "头像")]
        [UIHint("FormText")]
        public string FaceImage { get; set; }
        [UIHint("FormText")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        [UIHint("FormText")]
        public DateTime LastLoginTime { get; set; } = DateTime.Now;
        [UIHint("FormText")]
        public bool IsEnable { get; set; } = true;
        
    }
    /// <summary>
    /// 管理员管理页面的Vm
    /// </summary>
    public class 
        ManagerSettingVm
    {
        [UIHint("PearDataTable")]
        public PearTableData ManagerTableConfig { get; set; } = new PearTableData
        {
            Id = "ManagerSettingTable",
            Url = "/SysSetting/ManagerData",
            IsShowDelete = true,
            DeleteFunc = "ManagerDelete",
            IsShowEdit = true,
            EditFunc = "ManagerEdit",
            IsShowDetail = true,
            DetailFunc = "ManagerDetail",
            DeleteWhere = "d.LoginName!='admin'",
            PearTableFeilds = new List<PearTableFeild>()
            {
                new PearTableFeild()
                {
                    title = "序号",
                    type = "numbers"
                },
                new PearTableFeild()
                {
                  field  = "Id",
                  hide = true
                },
                new PearTableFeild()
                {
                    title = "账号",
                    field = "LoginName",
                    IsSearch = true,
                    
                },
                new PearTableFeild()
                {
                    field = "CreateTime",
                    title = "注册时间",
                    IsSearch = true, 
                    SearchType = typeof(DateTime),
                    IsDateTimeRange = true,
                    
                },new PearTableFeild()
                {
                    title = "最后一次登录时间",
                    field = "LastLoginTime"
                }, 
                new PearTableFeild()
                {
                    field = "IsEnable",
                    title = "状态", 
                    templet = "#ManagerEnableTemplate"
                } 
            },
            RowBtns = new List<RowBtn>()
            {
                new RowBtn()
                {
                    EventName = "enableState",
                    Text = "启用账户",
                    ShowWhere = "d.IsEnable === false"
                },
                 new RowBtn()
                 {
                     EventName = "enableState",
                     Text = "禁用账户",
                     ShowWhere = "d.IsEnable === true"
                 },
                new RowBtn()
                {
                    EventName = "setRole",
                    Text = "设置角色" 
                }
            }
            
        };
    }
}