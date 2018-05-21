using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHFramework.DB;
using System.Data;

/// <summary>
/// IOutFile 的摘要说明
/// </summary>
public interface IOutFile
{
     
    string OutFilePath { get; set; }
    string FileName { get; set; }
    string ExtName{get;set;}

    ReturnValue OutFile(DataTable data);

    int GetDataRowCount();

    ReturnValue CheckData();

}
public enum OutFileType
{
    TXT,
    TXT2,
    MDB,
    XLSX ,
    /// <summary>
    /// 有里头 三列
    /// </summary>
    XLSX2
}
public class OutFileFactory 
{
   

    public IOutFile CreateNewObj(OutFileType type) 
    {
        IOutFile _result = null;
        switch (type)
        {
            case OutFileType.TXT:
                _result= new OutFile_Text();
                break;
            case OutFileType.TXT2:
                _result = new OutFile_Text2();
                break;
            case OutFileType.MDB:
                _result = new OutFile_Mdb();
                break;
            case OutFileType.XLSX:
                _result = new OutFile_Xlsx();
                break;
            case OutFileType.XLSX2:
                _result = new OutFile_Xlsx2();
                break;
            default:
                break;
        }

        return _result;

    }
}

