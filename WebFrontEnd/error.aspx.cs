using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class error : System.Web.UI.Page
{
    public string IPAddress { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        IPAddress = Request.UserHostAddress;


        hiddeninfo.InnerHtml += Request.UserHostAddress + "<br />";
        hiddeninfo.InnerHtml += Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + "<br />";
        hiddeninfo.InnerHtml += Request.ServerVariables["REMOTE_ADDR"] + "<br />"; 
        LogTool.LogCommon.WriteLog(new LogTool.GlobalErrorHandler().ExceptionToString(new Exception("IP错误")));
    }
}