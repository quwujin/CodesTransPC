using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;

public partial class apply : System.Web.UI.Page
{
    public string strTitle { get; set; }
    public int Id { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Right.IsAdmmin())
        {
            Response.Redirect("Login.aspx");
            return;
        }
        try
        {
            this.strTitle = TableDAL.GetTable("Config", "DataType=100").Rows[0]["Literal"].ToString();
        }
        catch { }
        try
        {
            Id = Convert.ToInt32(Request["Id"]);
        }
        catch { }

        if (!IsPostBack)
        {
            if (0 != this.Id)
            {
                Bind(Id);
            }
        }
    }

    public void Bind(int Id)
    {
        DataTable dt = TableDAL.GetTable("ApployInfo", "ID=" + Id);
        if (null != dt && 0 < dt.Rows.Count)
        {
            DataRow dr = dt.Rows[0];
            this.txtFileName.Value = dr["FileName"].ToString();
            this.txtTitle.Value = dr["Title"].ToString();
            this.txtApplyDate.Value = Convert.ToDateTime(dr["ApployTime"]).ToString("yyyy-MM-dd");
            this.txtBathCode.Value = dr["BathCode"].ToString();
            this.txtCustomerEmail.Value = dr["CustomerEmail"].ToString();
            txtFileName.Disabled = true;
            txtSecret.Disabled = true;
        }
    }


    public void test()
    {

        //AttachmentModel file = new AttachmentModel();

        //string fileName = System.Guid.NewGuid().ToString("N");
        //int indexOfPoint = this.fTechTbl.FileName.IndexOf('.');

        //if (-1 != indexOfPoint)
        //{
        //    string postfix = this.fTechTbl.FileName.Substring(indexOfPoint);

        //    fileName += postfix;
        //}

        //string fileUrl = "/MyAdmin/Uploads/" + fileName;
        //string absFileName = Server.MapPath("/MyAdmin/Uploads") + "\\" + fileName;
        ////FileStream fs = System.IO.File.Open(absFileName, FileMode.Create);


        //file.AttachmentName = projectid + "-" + fileName;
        //file.AttachmentType = Convert.ToInt32(type);
        //file.AttachmentUrl = fileUrl;
        //file.PhysicPath = absFileName;

        //return file;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string url = "";
        if (0 == this.Id)
        {
            
        }


        #region --Save Info
        bool success = true;
        DataTable dtNew = TableDAL.GetNullValueTable("ApployInfo");
        DataTable dtSchema = TableDAL.GetTableSchema("ApployInfo");
        DataRow drNew = dtNew.NewRow();
        dtNew.Rows.Add(drNew);
        drNew["FileName"] = this.txtFileName.Value;
        try
        {
            drNew["ApployTime"] = Convert.ToDateTime(this.txtApplyDate.Value);
        }
        catch { }
        drNew["Status"] = 0;
        drNew["Title"] = this.txtTitle.Value;
        drNew["BathCode"] = this.txtBathCode.Value;
        drNew["CreatedTime"] = System.DateTime.Now;
        drNew["Url"] = url;
        drNew["Secret"] = this.txtSecret.Value;
        drNew["CustomerEmail"] = this.txtCustomerEmail.Value;

        if (0 != this.Id)
        {
            drNew["Id"] = this.Id;
        }

        int count = 0;
        if (0 == this.Id)
        {
            //count = TableDAL.InsertDataTable(dtNew, dtSchema);
        }
        else
        {
            dtSchema.Columns.Remove("Secret");
            dtSchema.Columns.Remove("CreatedTime");
            dtSchema.Columns.Remove("Url");
            dtSchema.Columns.Remove("Type");

            count = TableDAL.UpdateDataTable(dtNew, dtSchema);
        }
        if (0 < count)
        {
            success = true;

            if (success)
            {
                if (!string.IsNullOrEmpty(this.txtCustomerEmail.Value))
                {
                    //string mailBody = "";
                    //mailBody += this.strTitle + "项目(批号：" + this.txtBathCode.Value + ")下载的解压密码为：" + this.txtSecret.Value + System.Environment.NewLine;

                    ////string titles = "建项申请,【待审批】项目名称:" + m.Name + ",项目编号:" + m.ProjectNo; modify by lanyiwei 20161109 title 显示不下全部内容，进行精简
                    //string titles = "【" + this.strTitle + "】解压密码";
                    //Common.EmailTool.sendEmail(this.txtCustomerEmail.Value, titles, mailBody, "");

                }
            }
        }

        #endregion


        if (success)
        {
            ScriptManager.RegisterStartupScript(this.btnSave, typeof(Button), "", "<script type=\"text/javascript\">alert('保存成功！');</script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.btnSave, typeof(Button), "", "<script type=\"text/javascript\">alert('保存失败！');</script>", false);
        }
    }
}