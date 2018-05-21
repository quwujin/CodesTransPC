using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YHFramework.SysModel
{
    public class ExecutePlanModel
    {
         #region Basic Property
		 
           public int Id { get; set; }
           public int CodeType { get; set; }
           public int MaxCount { get; set; }
           public int EachCount { get; set; }
           public int BatchCodeBegin { get; set; }
           public DateTime EffectiveDateBegin { get; set; }
           public int GeneratedCount { get; set; }
           public int CurrentBatchCode { get; set; }
           public int Status { get; set; }
           public string CustomerEmail { get; set; }

         #endregion 
    }
}
