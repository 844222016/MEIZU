using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using MEIZU.BLL;
using MEIZU.Tools;

namespace MEIZU.Models
{
    public class DictionaryIndexVm
    {
        public DictionaryIndexVm()
        {
            //获取select的数据
            BaseManager baseManager = new BaseManager();
            var searchList =  baseManager.GetAllDictionaryGroupName().Select(m=>new SelectListItem()
            {
                Text = m,
                Value = m
            }).ToList();
       
            
            DictionaryTableConfig =  new PearTableData()
            {  
                Id = "dicTbl",
                PearTableFeilds = new List<PearTableFeild>
                {
                    new PearTableFeild
                    {
                        title = "序号",
                        type = "numbers",
                        width = 100
                    },new PearTableFeild()
                    {
                        field = "GroupName",
                        title="组名",
                        IsSearch = true,
                        SearchUseList = true,
                        SearchList = searchList 
                    
                    },new PearTableFeild
                    {
                        field = "Value",
                        title="数据值"
                    }
                },
                IsShowDetail = false,
                IsShowEdit = true,
                EditFunc = "dicEdit",
                IsShowDelete = true,
                DeleteFunc = "dicDelete",
                DeleteWhere = "1===1",
                Url = "/SysSetting/DicData"
            };
        }
        
        
        [UIHint("PearDataTable")] 
        public PearTableData DictionaryTableConfig { get; set; } 
}

    public class DictionaryEditVm
    {
        [UIHint("FormText")]
        [Display(Name = "组名")]
        [Required]
        public string GroupName { get; set; }
        [UIHint("FormText")]
        [Display(Name = "数据值")]
        [Required]
        public string Value { get; set; }
        public long Id { get; set; }
    }
    
    public class DictionaryCreateVm
    {
        [UIHint("FormText")]
        [Display(Name = "组名")]
        [Required]
        public string GroupName { get; set; }
        [UIHint("FormText")]
        [Display(Name = "数据值")]
        [Required]
        public string Value { get; set; } 
    }
}