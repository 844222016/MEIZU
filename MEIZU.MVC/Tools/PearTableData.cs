using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MEIZU.BLL;

namespace MEIZU.Tools
{
    /// <summary>
    /// 表格数据
    /// </summary>
    public class PearTableData
    {
        public int Limit { get; set; } = 10;
        public int[] Limits { get; set; } =  {10, 20, 30};
        public string Url { get; set; }
        public object WhereObj { get; set; }
        public object Data { get; set; }
        public int CurrentIndex { get; set; }
        public string DetailFunc { get; set; }
        public string DeleteFunc { get; set; }
        public string EditFunc { get; set; }
        public bool IsShowDetail { get; set; }  
        public bool IsShowDelete { get; set; } 
        public bool IsShowEdit { get; set; }
        /// <summary>
        /// 头部左侧菜单设置
        /// </summary>
        public Dictionary<string, string> OtherBtns { get; set; } = new Dictionary<string, string>()
        {
            {"新增", "create"}
        };
        public List<PearTableFeild> PearTableFeilds { get; set; }
        public string Method { get; set; } = "get";
        /// <summary>
        /// 右侧菜单
        /// </summary>
        public string Toolbar { get; set; } = "default";
        public string Id { get; set; }

        public List<RowBtn> RowBtns { get; set; } = new List<RowBtn>();
        //     =new List<RowBtn>
        // {
        //     new RowBtn
        //     { 
        //         Text = "商品下架",
        //         EventName = "downItem"
        //     },
        //     new RowBtn
        //     { 
        //         Text = "退房",
        //         EventName = "outRoom",
        //         ShowWhere = "d.Id == 2"
        //     }
        // };
        /// <summary>
        /// 其他按钮全局控制条件
        /// </summary>
        public string RowBtnsShowWhere { get; set; } = "1===1";

        /// <summary>
        /// 显示删除按钮的条件
        /// </summary>
        public string DeleteWhere { get; set; }
    }
    /// <summary>
    ///  行级其他按钮
    /// </summary>
    public class RowBtn
    {
        /// <summary>
        /// 显示条件
        /// </summary>
        public string ShowWhere { get; set; } = "1==1";
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 事件名称
        /// </summary>
        public string EventName { get; set; }
        
    }

    public class PearTableFeild
    {
        /// <summary>
        /// 绑定属性
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 显示标题文字
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 单元格宽度
        /// </summary>
        public int width { get; set; } = 200;
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get; set; } = "normal";
        /// <summary>
        /// 固定方向left right
        /// </summary>
        public string @fixed { get; set; }
        /// <summary>
        /// 隐藏
        /// </summary>
        public bool hide { get; set; }
        /// <summary>
        /// 自定义单元格
        /// </summary>
        public string templet { get; set; }
        /// <summary>
        /// 行菜单
        /// </summary>
        public string toolbar { get; set; }
        /// <summary>
        /// 是否作为查询条件
        /// </summary>
        public bool IsSearch { get; set; } = false;
        /// <summary>
        /// 检索用的数据类型 比如enum,datetime,stirng 等
        /// 如果是Enum则用Enum显示下拉框数据
        /// DateTime则会显示日期选择组件
        /// </summary>
        public Type SearchType { get; set; }
        /// <summary>
        /// 是否使用时间范围（前提条件是SearchType date或datetime）
        /// </summary>
        public bool IsDateTimeRange { get; set; }
        /// <summary>
        /// 范围最小值 （前提条件是SearchType date或datetime）
        /// </summary>
        public string DateRangeMinVal { get; set; } 
        /// <summary>
        /// 范围最大值 （前提条件是SearchType date或datetime）
        /// </summary>
        public string DateRangeMaxVal { get; set; }
        /// <summary>
        /// 启用数据字典数据
        /// </summary>
        public bool SearchUseDictionary { get; set; } = false;
        /// <summary>
        /// 数据字典数据组名 （前提是SearchUseDictionary开启）
        /// </summary>
        public string DictionaryGroupName { get; set; }
        /// <summary>
        /// 使用常规的下拉框
        /// </summary>
        public bool SearchUseList { get; set; } = false;
        /// <summary>
        /// 下拉框的下拉数据 （前提是SearchUseList为True）
        /// </summary>
        public List<SelectListItem> SearchList { get; set; }
        
        /// <summary>
        /// 获取数据字典数据
        /// </summary>
        /// <returns></returns>
        public List<MEIZU.Model.Dictionary> GetDicByGroupName( )
        {
            BaseManager baseManager = new BaseManager();
            return baseManager.GetDictionariesByGroupName(DictionaryGroupName);
        }
    }

    
}

