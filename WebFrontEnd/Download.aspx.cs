using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;

public partial class Download : System.Web.UI.Page
{
    //NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            bool isLogined = false;
            try
            {
                isLogined = Convert.ToBoolean(this.Session["logined"]);
            }
            catch { }

            if (isLogined)
            {
                string fileName = "";
                try
                {
                    fileName = this.Request.Url.ToString().Substring(this.Request.Url.ToString().LastIndexOf('/') + 1);
                }
                catch { }


                string postfix = "";
                int lasindex = this.Request.Url.ToString().LastIndexOf('.');
                if (-1 != lasindex)
                {
                    postfix = this.Request.Url.ToString().Substring(lasindex + 1);
                }

                int totalCount = 0;
                if (Log(out totalCount))
                {
                    this.Response.Clear();
                    string absPath = this.Request.Url.AbsolutePath.Substring(0, this.Request.Url.AbsolutePath.Length - 1 - this.Request.Url.AbsolutePath.LastIndexOf('/'));
                    string absFileName = this.Server.MapPath(this.Request.Url.AbsolutePath);

                    if (absFileName.ToLower().EndsWith(".rar")==false && absFileName.ToLower().EndsWith(".zip")==false) 
                    {
                        Response.Write("非法请求");
                        Response.End();
                        return;
                    }

                    FileStream fs = File.Open(System.Web.HttpUtility.UrlDecode(absFileName), FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    byte[] result = br.ReadBytes((int)fs.Length);
                    fs.Close();

                    Response.ContentType = "application/x-zip-compressed";
                    Response.Headers["Accept-Ranges"] = "bytes";

                    Response.BinaryWrite(result);
                    Response.End();
                }
                else
                {
                    Response.Clear();
                    Response.Write("Error:05017,请联系管理员");
                    Response.End();
                    return;
                }
            }
            else
            {
                Response.Clear();
                Response.Write("请先登录！");
                Response.End();
                return;
            }
        }
        catch (FileNotFoundException nofileEx)
        {
            //log.Error("下载的文件(" + this.Request.Url.ToString() + ")不存在！Message:" + nofileEx.Message);
            Response.Clear();
            Response.Write("您要下载的文件不存在！");
            Response.End();
            return;
        }
        catch (Exception ex)
        {
            //log.Error("Message:" + ex.Message + "\r\n" + ex.StackTrace); Response.Clear();
            Response.Write(ex.Message);
            Response.End();
        }
    }

    protected bool Log(out int total)
    {
        bool hasLoged = false;
        total = 0;

        try
        {
            DataRow drLog = TableDAL.GetNewNullRow("DownloadLog");
            drLog["IPAddress"] = this.Request.UserHostAddress;
            drLog["Url"] = this.Request.Url.ToString();
            drLog["UserId"] = Convert.ToInt32(this.Session["userId"]);
            drLog["CreatedTime"] = System.DateTime.Now;

            int count = TableDAL.InsertDataTable(drLog.Table, "DownloadLog");
            if (0 < count)
            {
                hasLoged = true;
            }

            string condition = "url='" + this.Request.Url.ToString() + "'";
            total = TableDAL.GetTableCount("DownloadLog", condition);
        }
        catch { }

        return hasLoged;
    }
}