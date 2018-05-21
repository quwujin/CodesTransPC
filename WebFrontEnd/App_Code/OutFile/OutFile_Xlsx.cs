using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHFramework.DB;
using System.Data;
using System.IO;
using System.Text;
using NPOI;
using NPOI.XSSF.UserModel; 
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;


/// <summary>
/// OutFile_Text 主要用来输出 纯串码类的文本文件
/// </summary>
public class OutFile_Xlsx:IOutFile
{
    public OutFile_Xlsx()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        ExtName = ".xlsx"; 
	}

    public string OutFilePath { get; set; }
    public string FileName { get; set; }
    public string ExtName { get; set; }

    public ReturnValue OutFile(DataTable data) 
    {
        ReturnValue _result = new ReturnValue();
        string templatefile = System.AppDomain.CurrentDomain.BaseDirectory + "Sample\\xlsx-sample.xlsx";


        #region 检查数据库文件
        if (System.IO.File.Exists(OutFilePath + FileName) == false)
        {
            System.IO.File.Copy(templatefile, OutFilePath + FileName);
        }
        #endregion

        if (data.Rows.Count > 0)
        {
            using (FileStream fs = File.Open(OutFilePath + FileName, FileMode.Open, FileAccess.Read,FileShare.ReadWrite))
            {
                XSSFWorkbook xssfworkbook = new XSSFWorkbook(fs);
                int i = 0;
               
                ISheet sheet = xssfworkbook.GetSheetAt(0);//获取工作表
                if (sheet.LastRowNum > 0)
                {
                    i = sheet.LastRowNum+1;
                } 
                foreach (DataRow item in data.Rows)
                {
                   
                    IRow row = sheet.CreateRow(i);//在工作表中添加一行 
                    ICell cell1 = row.CreateCell(0);
                    cell1.SetCellValue(item["CodeData"].ToString());
                    i++;
                }
                FileStream newfile = new FileStream(OutFilePath + FileName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);//写入流
                xssfworkbook.Write(newfile);
                xssfworkbook = null;
                newfile.Close();
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