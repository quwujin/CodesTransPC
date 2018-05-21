using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YHFramework.SysModel
{
    public class ApployInfoModel
    {
         #region Basic Property
		 
           public int ID { get; set; }
           public string Title { get; set; }
           public string FileName { get; set; }
           public DateTime ApployTime { get; set; }
           public string BathCode { get; set; }
           public string Secret { get; set; }
           public string CustomerEmail { get; set; }
           public int Type { get; set; }
           public string Url { get; set; }
           public int Status { get; set; }
           public DateTime CreatedTime { get; set; }
           public DateTime UpdateTime { get; set; }
           public DateTime DownTime { get; set; }
           public DateTime CompleteTime { get; set; }
           public string Remarks { get; set; }
           public bool AutoCheck { get; set; }
           public string CheckResult { get; set; }
         #endregion 
    }
}
