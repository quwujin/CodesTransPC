using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YHFramework.SysModel
{
    public class ApployCheckLogModel
    {
         #region Basic Property
		 
           public int CheckLogId { get; set; }
           public int ApployId { get; set; }
           public string TypeCode { get; set; }
           public string FileName { get; set; }
           public Int64 Number { get; set; }
           public string Message { get; set; }
           public DateTime CreateOn { get; set; }
           public DateTime UpdateOn { get; set; }

         #endregion 
    }
}
