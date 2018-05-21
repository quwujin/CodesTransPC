using System;
using System.Configuration;
using System.Web;
using YHFramework.DAL;

namespace YHFramework.Security
{
	public class IPAllowUtility
	{
		public static bool IpIsAllowed(HttpContext context)
		{
			bool allow = false;
			bool enabled = false;
			try
			{
				enabled = Convert.ToBoolean(ConfigurationManager.AppSettings["enableIpAllow"]);
			}
			catch
			{
			}
			if (enabled)
			{
				string ipAddress = context.Request.UserHostAddress;
				string sqlWhere = "  type=0 and Address = '" + ipAddress + "'";
				int bkCount = TableDAL.GetTableCount("IPAllowList", sqlWhere);
				if (bkCount > 0)
				{
					allow = false;
				}
				sqlWhere = "  type=1 and Address = '" + ipAddress + "'";
				int whCount = TableDAL.GetTableCount("IPAllowList", sqlWhere);
				if (whCount > 0)
				{
					allow = true;
				}
			}
			else
			{
				allow = true;
			}
			return allow;
		}
	}
}
