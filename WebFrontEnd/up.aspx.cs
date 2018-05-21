using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;

public partial class up : System.Web.UI.Page
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

    }


    protected void btnUp_Click(object sender, EventArgs e)
    {
        bool isAllowed = false;
        int lasindex = this.fileUp.FileName.LastIndexOf('.');
        string oriFileName = this.fileUp.FileName;
        if (-1 != lasindex)
        {
            string postfix = this.fileUp.FileName.Substring(lasindex + 1);

            if ("zip" == postfix.ToLower() || "rar" == postfix.ToLower())
            {
                isAllowed = true;
            }
        }

        if (isAllowed)
        {
            string fileName = System.Guid.NewGuid().ToString("N");
            string fullName = System.AppDomain.CurrentDomain.BaseDirectory + "Uploads\\up\\" + fileName + ".zip";
            this.fileUp.SaveAs(fullName);


            #region --Save Info
            bool success = false;
            DataTable dtNew = TableDAL.GetNullValueTable("UpInfo");
            DataTable dtSchema = TableDAL.GetTableSchema("UpInfo");
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
            drNew["Url"] = "/Uploads/up/" + fileName + ".zip";
            drNew["BathCode"] = oriFileName;


            int count = TableDAL.InsertDataTable(dtNew, dtSchema);


            if (0 < count)
            {
                success = true;

                if (success)
                {
                }


                if (success)
                {
                    ScriptManager.RegisterStartupScript(this.btnUp, typeof(Button), "", "<script type=\"text/javascript\">alert('保存成功！');</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.btnUp, typeof(Button), "", "<script type=\"text/javascript\">alert('保存失败！');</script>", false);
                }
            }

            #endregion
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.btnUp, typeof(Button), "at", "<script>alert('上传的文件只能是zip文件！')</script>", false);
        }






    }
}