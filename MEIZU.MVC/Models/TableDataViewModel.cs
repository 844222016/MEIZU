using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MEIZU.Tools;

namespace MEIZU.Models
{
    public class TableDataViewModel
    {
        [UIHint("PearDataTable")]
        public PearTableData PearTableData { get; set; } = new PearTableData()
        {
            Id = "table1",
            Url = "/Home/TableData",
            WhereObj = new
            {
                id=3
            },
            IsShowDetail = true,
            DetailFunc = "detailFunc", 
            IsShowEdit = true,
            EditFunc = "editFunc",
            IsShowDelete = true,
            DeleteFunc = "deleteFunc",
            PearTableFeilds = new List<PearTableFeild>
            {
                new PearTableFeild
                {
                    title = "序号",
                    type = "numbers"
                }
                ,
                new PearTableFeild
                {
                    field = "Name",
                    title = "姓名",
                    IsSearch = true,
                    //SearchType = typeof(Sex)
                    SearchUseDictionary = true,
                    DictionaryGroupName = "Grade"
                }
                ,
                new PearTableFeild
                {
                    field = "Id",
                    title = "编号",
                    IsSearch = true,
                    SearchType = typeof(DateTime)
                }
            }
        };
    }

   public enum Sex
    { 
        男,女
    }
}
