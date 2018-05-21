using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;

public partial class upList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        bool logined = false;
        try
        {
            logined = Convert.ToBoolean(this.Session["logined"]);
        }
        catch { }

        if (!logined)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            BindData();
        }

    }

    protected void BindData()
    {
        DataTable dt = TableDAL.GetTable("UpInfo", "");

        this.rptrList.DataSource = dt;
        this.rptrList.DataBind();
    }

    protected string GetDownloadLink(string url)
    {
        if (Right.IsAdmmin())
        {
            url = this.Request.ApplicationPath.TrimStart(new char[] { '/' }) + "/" + url.TrimStart(new char[] { '/' });
            return "<a href=\"" + url + "\">下载</a>";
        }
        return "";
    }
}