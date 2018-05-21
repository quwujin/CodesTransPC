using AjaxPro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;

[AjaxNamespace("DownList")]
public partial class dlist : System.Web.UI.Page
{
    NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    public string strTitle { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.strTitle = TableDAL.GetTable("Config", "DataType=100").Rows[0]["Literal"].ToString();
        }
        catch { }

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
            string condition = "Status <> -1 AND ApployTime <= '" + System.DateTime.Now.ToShortDateString() + " 23:59:59'";

            DataTable dtList = TableDAL.GetTable("ApployInfo", condition);
            this.rptrList.DataSource = dtList;
            this.rptrList.DataBind();
        }
        Utility.RegisterTypeForAjax(typeof(dlist));
    }

    [AjaxMethod]
    public bool Down(int id)
    {
        try
        {
            DataTable dt = TableDAL.GetTable("ApployInfo", "id=" + id);

            if (null != dt && 0 < dt.Rows.Count)
            {
                dt.Rows[0]["Status"] = 1;
                dt.Rows[0]["DownTime"] = System.DateTime.Now;
                dt.Rows[0]["UpdateTime"] = System.DateTime.Now;
                DataTable dtSchema = TableDAL.GetTableSchema("ApployInfo");
                dtSchema.Columns.Remove("Type");
                dtSchema.Columns.Remove("CompleteTime");
                dtSchema.Columns.Remove("Remarks");
                TableDAL.UpdateDataTable(dt, dtSchema);
            }
        }
        catch (Exception ex)
        {
            log.Error("下载时出错！Message:" + ex.Message);
        }
        return true;
    }

    [AjaxMethod]
    public bool DataConfirm(int id)
    {
        try
        {
            this.strTitle = TableDAL.GetTable("Config", "DataType=100").Rows[0]["Literal"].ToString();
        }
        catch { }

        try
        {

            DataTable dt = TableDAL.GetTable("ApployInfo", "id=" + id);

            if (null != dt && 0 < dt.Rows.Count)
            {
                dt.Rows[0]["Status"] = 2;
                dt.Rows[0]["UpdateTime"] = System.DateTime.Now;
                DataTable dtSchema = TableDAL.GetTableSchema("ApployInfo");
                dtSchema.Columns.Remove("Type");
                dtSchema.Columns.Remove("CompleteTime");
                dtSchema.Columns.Remove("Remarks");
                TableDAL.UpdateDataTable(dt, dtSchema);

                #region --发送邮件
                string mailBody = "";
                mailBody += this.strTitle + "项目(批号：" + dt.Rows[0]["BathCode"].ToString() + " 码包：" + dt.Rows[0]["FileName"].ToString() + ")下载的解压密码为：" + dt.Rows[0]["Secret"].ToString() + System.Environment.NewLine + System.Environment.NewLine;
                mailBody += "本邮件中所包含的信息可能是保密并受相关法律保护的。本邮件仅供收件人参阅。我们特此告知，如果您不是特定的收件人，您对本邮件任何的使用、转发、传播或复制是被严令禁止的并且可能是违法的，请通过发还电子邮件的方式联系发件人，并且销毁所有的原始邮件。" + System.Environment.NewLine;
                //string titles = "建项申请,【待审批】项目名称:" + m.Name + ",项目编号:" + m.ProjectNo; modify by lanyiwei 20161109 title 显示不下全部内容，进行精简
                string titles = "【"+ this.strTitle + "】解压密码";
                Common.EmailTool.sendEmail(dt.Rows[0]["CustomerEmail"].ToString(), titles, mailBody, "");
                #endregion
            }
        }
        catch (Exception ex)
        {
            log.Error("数据确认时出错。Message:" + ex.Message);
        }
        return true;
    }

    public string StatDesc(int status)
    {
        string strDes = "";
        switch (status)
        {
            case 2:
                strDes = "已确认";
                break;
            case 1:
                strDes = "已下载";
                break;
            default:
            case 0:
                strDes = "未下载";
                break;
        }
        return strDes;
    }


    public string GetClass(int status)
    {
        string strClassName = "btn-success";

        if (0 == status || 2 == status)
        {
            strClassName = "";
        }
        return strClassName;
    }

    public string GetEnabled(int status)
    {
        string strClassName = "";

        if (0 == status || 2 == status)
        {
            strClassName = "disabled=\"disabled\"";
        }
        return strClassName;
    }
}