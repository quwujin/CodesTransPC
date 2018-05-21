using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;
using System.IO;

public partial class ApplyList : System.Web.UI.Page
{
    protected  ApplyTools ApplyTools = new ApplyTools();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Right.IsAdmmin())
        {
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            BindData();
        }
    }

    protected void BindData()
    {
        DataTable dt = TableDAL.GetTable("ApployInfo", "Status<>-1");

        this.rptrList.DataSource = dt;
        this.rptrList.DataBind();
    }






}