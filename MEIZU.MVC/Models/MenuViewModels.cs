using System.ComponentModel.DataAnnotations;
using MEIZU.Model;
using MEIZU.Models;

namespace MEIZU.Models
{
    public class MenuEditVm
    {
        public long Id { get; set; }
        
        [Display(Name = "菜单名称")]
        [UIHint("FormText")]
        public string MenuName { get; set; }
        [Display(Name = "排序序号")]
        [UIHint("FormInt")]
        public int Order { get; set; } 
        
        public long? ParentId { get; set; }

        [Display(Name = "图标")]
        [UIHint("FormText")]
        public string Icon { get; set; }

        [Display(Name = "跳转路径")]
        [UIHint("FormText")]
        public string Url { get; set; }
        
        [Display(Name = "菜单类型")]
        public MenuType MenuType { get; set; }

        public int MenuTypeId { get; set; }
    }
 
    /// <summary>
    /// 添加菜单视图类
    /// </summary>
    public class MenuCreateVm
    {
        [Display(Name = "菜单名称")]
        [UIHint("FormText")]
        public string MenuName { get; set; }
        [Display(Name = "图标")]
        [UIHint("FormText")]
        public string Icon { get; set; }
        [UIHint("FormText")]
        [Display(Name = "跳转路径")]
        public string Url { get; set; }
        [Display(Name = "菜单类型")]
        public MenuType MenuType { get; set; }

        [UIHint("FormText")]
        public long ParentId { get; set; }

        public int MenuTypeId { get; set; }
    }


    /// <summary>
    /// 添加子节点视图类
    /// </summary>
    public class MenuCreateParentVm
    {
        [Display(Name = "菜单名称")]
        [UIHint("FormText")]
        public string MenuName { get; set; }
        [Display(Name = "图标")]
        [UIHint("FormText")]
        public string Icon { get; set; }
        [UIHint("FormText")]
        [Display(Name = "跳转路径")]
        public string Url { get; set; }
        [Display(Name = "菜单类型")]
        public MenuType MenuType { get; set; }
        public int MenuTypeId { get; set; }
    }
}