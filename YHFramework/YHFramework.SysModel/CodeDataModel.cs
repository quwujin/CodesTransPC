using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YHFramework.SysModel
{
    public class CodeDataModel
    {
         #region Basic Property
		 
           public int CodeDataId { get; set; }
           public string CodeData { get; set; }
           public string ShortCodeData { get; set; }
           public string CodeIndex { get; set; }
           public string BatchNumber { get; set; }
           public int BoxNumberId { get; set; }
           public int ContentStatusId { get; set; }
           public DateTime UpdateOn { get; set; }
           public DateTime Createon { get; set; }
           public int ApployInfoId { get; set; }

         #endregion 
    }
}
