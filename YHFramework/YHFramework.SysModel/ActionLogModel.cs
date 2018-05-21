using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHFramework.SysModel
{
    public class ActionLogModel
    {
        #region Basic Property

        public int ActionLogId { get; set; }
        public string ActionName { get; set; }
        public string UserName { get; set; }
        public string ActionResult { get; set; }
        public string KeyData { get; set; }
        public string Notes { get; set; }
        public DateTime CreateOn { get; set; }

        #endregion
    }
}
