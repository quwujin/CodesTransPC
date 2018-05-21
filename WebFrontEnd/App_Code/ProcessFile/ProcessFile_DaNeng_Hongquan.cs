using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using YHFramework.DB;

/// <summary>
/// ProcessFile_DaNeng_Hongquan 的摘要说明
/// </summary>
public class ProcessFile_DaNeng_Hongquan : IProcessFile
{
	public ProcessFile_DaNeng_Hongquan()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}


    public ReturnValue UnZipFiles(string zipFilePath, string password) 
    {
        ReturnValue _result = new ReturnValue();

        #region 检查压缩文件
        if (File.Exists(zipFilePath)==false) 
        {
            _result.Success = false;
            _result.ErrMessage = "没找到文件 " + zipFilePath;

            return _result;
        }



        #endregion

        #region 解压文件
        //目录结尾 
        string targetpath= System.IO.Path.GetDirectoryName(zipFilePath)+"\\"+System.IO.Path.GetFileNameWithoutExtension(zipFilePath);

        if (Directory.Exists(targetpath) == false) 
        {
            Directory.CreateDirectory(targetpath);
        }

        List<string> files=  ZipCommon.UnZipFiles(zipFilePath, targetpath, password);

        _result.ObjectValue = files;

        #endregion

        return _result;
    }


    /// <summary>
    /// 解析文件
    /// </summary>
    /// <param name="filename"></param>
    /// <returns></returns>
    public ReturnValue RealseDataFile(string filename) 
    {
        ReturnValue _result = new ReturnValue();
        BindBoxTool bindBoxTool = new BindBoxTool();
        List<RequestBindData> listitem =new List<RequestBindData>();

        #region 检查文件并读取
        if (File.Exists(filename) == false)
        {
            _result.Success = false;
            _result.ErrMessage = "没找到文件 " + filename;

            return _result;
        }
        listitem = bindBoxTool.ReadBoxDataFileByXls(filename);
        if (listitem.Count == 0)
        {
            _result.Success = false;
            _result.ErrMessage = "没找到文件 " + filename;

            return _result;
        }

        #endregion

         
        _result.ObjectValue = listitem; 

        return _result;
    }
}