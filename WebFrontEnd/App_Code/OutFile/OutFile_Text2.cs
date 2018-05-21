using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHFramework.DB;
using System.Data;
using System.IO;
using System.Text;

/// <summary>
/// OutFile_Text 主要用来输出 纯串码类的文本文件
/// </summary>
public class OutFile_Text2:IOutFile
{
    public OutFile_Text2()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        ExtName = ".txt"; 
	}

    public string OutFilePath { get; set; }
    public string FileName { get; set; }
    public string ExtName { get; set; }

    public ReturnValue OutFile(DataTable data) 
    {
        ReturnValue _result = new ReturnValue();

        if (data.Rows.Count > 0)
        {
            using (FileStream fs = File.Open(OutFilePath + FileName, FileMode.Append, FileAccess.Write))
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow item in data.Rows)
                {
                    sb.AppendLine(item["CodeData"].ToString() + "," + item["ShortCodeData"].ToString());
                }

                byte[] datastr = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
                fs.Write(datastr, 0, datastr.Length);
                fs.Close();
            }
        }
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