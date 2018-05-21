using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YHFramework.DB;

/// <summary>
/// IProcessFile 的摘要说明
/// </summary>
public interface IProcessFile
{  
    ReturnValue UnZipFiles(string zipFilePath, string password);

    ReturnValue RealseDataFile(string filename);
}

public enum ProcessFileType
{
    DaNeng_Hongquan 
}
public class ProcessFileFactory
{ 
    public static IProcessFile CreateNewObj(ProcessFileType type)
    {
        IProcessFile _result = null;
        switch (type)
        {
            case ProcessFileType.DaNeng_Hongquan:
                _result = new ProcessFile_DaNeng_Hongquan();
                break;
             
            default:
                break;
        }

        return _result;

    }
}