using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MEIZU.Tools;

namespace MEIZU.Models
{
    public class RoleIndexVm
    {
        [UIHint("PearDataTable")]
        public PearTableData RoleTableConfig { get; set; } = new PearTableData
        {
            Id = "roleTable",
            Url = "/SysSetting/RoleGetAllData",
            IsShowDelete = true,
            DeleteFunc = "roleDelete",
            IsShowEdit = true,
            EditFunc = "roleEdit",
            DeleteWhere = "d.RoleName!='admin'",
            RowBtns = new List<RowBtn>()
            {
                new RowBtn
                {
                    EventName = "setRoleMenu",
                    Text = "设置角色权限"
                }
            },
            PearTableFeilds = new List<PearTableFeild>
            {
                new PearTableFeild
                {
                    title  = "序号",
                    type = "numbers"
                },
                new PearTableFeild
                {
                    field = "Id",
                    hide = true,
                    title = "编号"
                },
                new PearTableFeild
                {
                    field = "RoleName",
                    title = "角色名称"
                }
            }
        };
        
        
        
    }

    public class RoleCreateVm
    {
        [Display(Name = "角色名称")]
        [UIHint("FormText")]
        public string RoleName { get; set; }
    }
    public class RoleEditVm
    {
        [UIHint("FormInt")]
        public int Id { get; set; }
        
        [Display(Name = "角色名称")]
        [UIHint("FormText")]
        [Required]
        public string RoleName { get; set; }
    }

    public class RoleMenuVm
    {
        [UIHint("PearTreeTable")]
        public PearTreeTableConfig TreeTableConfig { get; set; }  
    }
}