using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Response.Write(Request.UserHostAddress + "<br />");

            Response.Write(Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + "<br />");

            Response.Write(Request.ServerVariables["REMOTE_ADDR"] + "<br />");
        }
    }
}