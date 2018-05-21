using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;


public partial class Login : System.Web.UI.Page
{
    public string LoginTitle { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.LoginTitle = TableDAL.GetTable("Config", "DataType=100").Rows[0]["Literal"].ToString();
            }
            catch { }
        }
    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string condition = "UserName='" + this.txtUserName.Value.Replace("'", "").Replace("-", "") + "' AND Password='" + this.txtPassword.Value.Replace("'", "").Replace("-", "") + "'";
        DataTable dtUser = TableDAL.GetTable("UserInfo", condition);
        if (null != dtUser && 0 < dtUser.Rows.Count)
        {
            this.Session["logined"] = true;
            this.Session["userId"] = Convert.ToInt32(dtUser.Rows[0]["Id"]);
            Response.Redirect("index.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.btnLogin, typeof(Button), "", "<script type=\"text/javascript\">alert('登录失败！');</script>", false);
        }
    }
}