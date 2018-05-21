using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHFramework.DB;
using System.Data;
using System.IO;
using System.Text;
using System.Data.OleDb;

/// <summary>
/// OutFile_Text 主要用来输出 纯串码类的文本文件
/// </summary>
public class OutFile_Mdb:IOutFile
{
    //http://bbs.csdn.net/topics/390060750
   // http://blog.csdn.net/xiammy/article/details/1781459
    public OutFile_Mdb()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        ExtName = ".mdb"; 
	}

    public string OutFilePath { get; set; }
    public string FileName { get; set; }
    public string ExtName {get;set;}

    public ReturnValue OutFile(DataTable data) 
    {
        ReturnValue _result = new ReturnValue();
        string templatefile = System.AppDomain.CurrentDomain.BaseDirectory + "Sample\\mdb-sample.mdb";


        #region 检查数据库文件 
        if (System.IO.File.Exists(OutFilePath + FileName) == false) 
        {
            System.IO.File.Copy(templatefile, OutFilePath + FileName);
        }
        #endregion


         YHFramework.DB.DBFactory dbfactory = new DBFactory(DBFactory.DBType.MDB, DBFactory.GetMdbConnectionString(OutFilePath + FileName, ""));
         
        if (data.Rows.Count > 0)
        {
            //using (FileStream fs = File.Open(OutFilePath + "temp"+FileName.Replace(this.ExtName,".txt"), FileMode.Append, FileAccess.Write))
            //{
            //    StringBuilder sb = new StringBuilder();
            //    foreach (DataRow item in data.Rows)
            //    {
            //        sb.AppendLine(item["CodeData"].ToString());
            //    }

            //    byte[] datastr = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            //    fs.Write(datastr, 0, datastr.Length);
            //    fs.Close();
            //}
            //StringBuilder sbsql = new StringBuilder();
            //sbsql.AppendLine("");
            //dbfactory.CommandText = sbsql.ToString();
            //dbfactory.ExecuteNonQuery();

            foreach (DataRow item in data.Rows)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(string.Format("insert into [1t](SN)values('{0}');", item["CodeData"].ToString()));
                dbfactory.CommandText = sb.ToString();
                dbfactory.ExecuteNonQuery();
            }

            #region 批量插入
            //System.Data.DataTable temptable = new System.Data.DataTable("1t");
            //System.Data.DataRow dr;
            //temptable.Columns.Add(new System.Data.DataColumn("CodeData", typeof(System.String)));
             
             
            //for (int i = 0; i < data.Rows.Count; i++)
            //{
            //    dr = temptable.NewRow();
            //    dr["CodeData"] = data.Rows[i]["CodeData"];
            //    temptable.Rows.Add(dr);
            //}

             
            //String connectionString = DBFactory.GetMdbConnectionString(OutFilePath + FileName, "");
            //OleDbConnection conn = new OleDbConnection(connectionString);

            //OleDbDataAdapter adapt = new OleDbDataAdapter();
            //var cmd = new OleDbCommand("insert into 1t(SN) values(@SN)", conn);
            //cmd.Parameters.Add("@SN", OleDbType.VarChar, 40, "CodeData");
            //adapt.InsertCommand = cmd;
            //OleDbCommandBuilder builder = new OleDbCommandBuilder(adapt);
            //builder.QuotePrefix = "[";
            //builder.QuoteSuffix = "]";

            //try
            //{
            //    adapt.Update(temptable);
            //}
            //finally
            //{

            //    adapt.Dispose();
            //    cmd.Dispose();
            //    conn.Close();
            //}

            #endregion

            
        }

        dbfactory.Close();
        return _result;
    }

    public int GetDataRowCount() 
    {
        return 0;
    }

    public ReturnValue CheckData() 
    {
        return new ReturnValue();
    }
}