using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;
using YHFramework.DB;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

public partial class Plan : System.Web.UI.Page
{
    public string CustomerEmail { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }

    public string CurrentTableName { get; set; }
    public int CurrentApployId { get; set; }

    public int BatchCodeBegin { get; set; }
    public DateTime EffectiveDateBegin { get; set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            this.CustomerEmail = TableDAL.GetTable("Config", "DataType=103").Rows[0]["Literal"].ToString();
        }
        catch { }

        if (!Right.IsAdmmin())
        {
            Response.Redirect("Login.aspx");
            return;
        }
    }

    public void GenerateCode(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sbIds = new StringBuilder();
        if (null != dt && 0 < dt.Rows.Count)
        {
            foreach (DataRow drCode in dt.Rows)
            {
                sb.Append(drCode[this.CurrentTableName].ToString());

                if ("boxnumber" == this.CurrentTableName.ToLower())
                {
                    sb.Append("," + drCode["Short" + this.CurrentTableName].ToString());
                }

                sb.Append("\n");

                sbIds.Append(drCode[this.CurrentTableName + "Id"].ToString() + ",");
            }
        }

        using (FileStream fs = File.Open(this.FilePath + this.FileName + ".csv", FileMode.Append, FileAccess.Write))
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(sb.ToString());
            fs.Write(data, 0, data.Length);
            fs.Close();
        }

        

        string sql = "update " + this.CurrentTableName + " set UpdateOn = getdate(), ContentStatusId = 1, ApployInfoId = " + this.CurrentApployId + " where " + this.CurrentTableName + "Id in(" + sbIds.ToString().TrimEnd(new char[] { ',' }) + ")";

        SqlConnection cn = new SqlConnection(TableDAL.DefaultConnectionString);
        cn.Open();
        int count = SqlHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region --新增批量计划
        DataTable dtPlan = TableDAL.GetNullValueTable("ExecutePlan");
        DataRow dr = dtPlan.NewRow();
        dtPlan.Rows.Add(dr);


        dr["CodeType"] = Convert.ToInt32(this.ddType.SelectedValue);
        dr["MaxCount"] = Convert.ToInt32(this.txtMaxCount.Value);
        dr["EachCount"] = Convert.ToInt32(this.txtEachCount.Value);
        dr["BatchCodeBegin"] = Convert.ToInt32(this.txtBathCodeBegin.Value);
        dr["EffectiveDateBegin"] = Convert.ToDateTime(this.txtEffectiveDateBegin.Value);
        dr["CustomerEmail"] = this.txtCustomerEmail.Value;


        int id = TableDAL.InsertOneRowDataTable(dtPlan, "ExecutePlan");
        #endregion

        if (0 != id)
        {
            int batchCodeBegin = Convert.ToInt32(dr["BatchCodeBegin"]);
            DateTime effectiveDateBegin = Convert.ToDateTime(dr["EffectiveDateBegin"]);

            int maxCount = Convert.ToInt32(dr["MaxCount"]);

            string tableName = "CodeData";
            if ("1" == this.ddType.SelectedValue)
            {
                tableName = "CodeData";
            }
            else
            {
                tableName = "BoxNumber";
            }
            this.CurrentTableName = tableName;


            #region --自动生成发布
            for (int i = 0; i < maxCount; )
            {
                int count = Convert.ToInt32(dr["EachCount"]);

                string Path = Server.MapPath("down") + "\\";
                string fileName = System.Guid.NewGuid().ToString("N");
                string absFileName = Path + fileName + ".csv";


                this.FilePath = Path;
                this.FileName = fileName;


                int topCount = 0;
                if (i + count <= maxCount)
                {
                    topCount = count;
                    i += count;
                }
                else
                {
                    topCount = i + count - maxCount;
                    i += topCount;
                }


                FileStream fs = File.Open(absFileName, FileMode.Create);
                fs.Close();

                #region --ApployInfo
                string zipFileName = Path + fileName + ".zip";
                string password = GetRandomPassword(12);

                DataTable dtApploy = TableDAL.GetNullValueTable("ApployInfo");
                DataRow drApploy = dtApploy.NewRow();
                dtApploy.Rows.Add(drApploy);

                drApploy["FileName"] = this.FileName + ".zip";//zipFileName;
                drApploy["ApployTime"] = effectiveDateBegin;
                effectiveDateBegin = effectiveDateBegin.AddDays(1);
                drApploy["BathCode"] = batchCodeBegin++;
                drApploy["url"] = "/down/" + fileName + ".zip";
                drApploy["Status"] = 0;
                drApploy["Secret"] = password;
                drApploy["CustomerEmail"] = this.CustomerEmail;
                drApploy["CreatedTime"] = System.DateTime.Now;
                drApploy["CustomerEmail"] = dr["CustomerEmail"];
                if (this.ddType.SelectedValue == "2")
                {
                    drApploy["Type"] = 2;
                }
                else
                {
                    drApploy["Type"] = 1;
                }
                int pkId = TableDAL.InsertOneRowDataTable(dtApploy, "ApployInfo");
                this.CurrentApployId = pkId;
                #endregion

                string condition = "ContentStatusId = 2";

                try
                {
                    TableDAL.GetDataAndExecute(tableName, topCount, condition, GenerateCode);
                }
                catch (Exception ex)
                {
                    string sql = "update ApployInfo set Remarks = '" + ex.Message.Replace("'", "''") + "\r\n" + ex.StackTrace.Replace("'", "''") + "'  where id=" + pkId;

                    SqlConnection cn = new SqlConnection(TableDAL.DefaultConnectionString);
                    cn.Open();
                    SqlHelper.ExecuteNonQuery(cn, CommandType.Text, sql, null);
                }


                //Zip(absFileName, zipFileName, password);
                bool zipSuccess = false;//ZipFile(absFileName, Path, fileName, 5, 2048, password);

                if (zipSuccess)
                {
                    DataTable dtUpdate = TableDAL.GetTable("ApployInfo", "id=" + pkId);
                    if (null != dtUpdate && 0 < dtUpdate.Rows.Count)
                    {
                        dtUpdate.Rows[0]["UpdateTime"] = System.DateTime.Now;
                        dtUpdate.Rows[0]["CompleteTime"] = System.DateTime.Now;
                        TableDAL.UpdateDataTable(dtUpdate, TableDAL.GetTableSchema("ApployInfo"));


                        #region --记录日志
                        dtUpdate.Rows[0]["Id"] = System.DBNull.Value;
                        TableDAL.InsertDataTable(dtUpdate, "ApployInfoLog");
                        #endregion
                    }
                }

                System.IO.File.Delete(absFileName);
            }
            #endregion
        }
        ScriptManager.RegisterStartupScript(this.btnSave, typeof(Button), "test", "<script>alert('执行成功！');</script>", false);
    }

    protected void Zip(string fileName, string zipFileName, string password)
    {
        using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
        {
            zip.Password = password;
            zip.AddFile(fileName);
            zip.Save(zipFileName);
        }
    }

    public string GetRandomPassword(int length)
    {
        string pwd = "";
        char[] pwdList = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        for (int i = 0; i < length; i++)
        {
            int random = (new System.Random(System.Guid.NewGuid().GetHashCode())).Next(0, pwdList.Length - 1);
            pwd += pwdList[random];
        }
        return pwd;
    }
     


}