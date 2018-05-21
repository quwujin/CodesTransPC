using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHFramework.DAL;

/// <summary>
/// Handler 的摘要说明
/// </summary>
public class Handler : IHttpHandler
{
    public Handler()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {

        bool enableed = false;
        try
        {
            enableed = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["enableIpAllow"]);
        }
        catch { }


        if (enableed)
        {
            bool allow = false;

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

            if (!allow)
            {
                context.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath + "error.aspx");
            }
            else
            {

            }
        }
    }
}