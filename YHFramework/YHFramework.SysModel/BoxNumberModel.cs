using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YHFramework.SysModel
{
    public class BoxNumberModel
    {
         #region Basic Property
		 
           public int BoxNumberId { get; set; }
           public string BoxNumber { get; set; }
           public string ShortBoxNumber { get; set; }
           public string IndexCode { get; set; }
           public string BatchNumber { get; set; }
           public string ContentStatusId { get; set; }
           public bool IsBind { get; set; }
           public DateTime BindTime { get; set; }
           public DateTime UpdateOn { get; set; }
           public DateTime CreateOn { get; set; }
           public int ApployInfoId { get; set; }

         #endregion 
    }
}
