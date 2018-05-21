﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls; 
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

/// <summary>
/// ZipCommon 的摘要说明
/// </summary>
public class ZipCommon
{
	public ZipCommon()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// ZIP:压缩单个文件
    /// add yuangang by 2016-06-13
    /// </summary>
    /// <param name="FileToZip">需要压缩的文件（绝对路径）</param>
    /// <param name="ZipedPath">压缩后的文件路径（绝对路径）</param>
    /// <param name="ZipedFileName">压缩后的文件名称（文件名，默认 同源文件同名）</param>
    /// <param name="CompressionLevel">压缩等级（0 无 - 9 最高，默认 5）</param>
    /// <param name="BlockSize">缓存大小（每次写入文件大小，默认 2048）</param>
    /// <param name="IsEncrypt">是否加密（默认 加密）</param>
    public static bool ZipFile(string FileToZip, string ZipedPath, string ZipedFileName = "", int CompressionLevel = 5, int BlockSize = 2048, string password = "")
    {
        bool _result = true;
        try
        {
            //如果文件没有找到，则报错
            if (!System.IO.File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }

            //文件名称（默认同源文件名称相同）
            string ZipFileName = string.IsNullOrEmpty(ZipedFileName) ? ZipedPath + "\\" + new FileInfo(FileToZip).Name.Substring(0, new FileInfo(FileToZip).Name.LastIndexOf('.')) + ".zip" : ZipedPath + "\\" + ZipedFileName + ".zip";

            using (System.IO.FileStream ZipFile = System.IO.File.Create(ZipFileName))
            {
                using (ZipOutputStream ZipStream = new ZipOutputStream(ZipFile))
                {
                    using (System.IO.FileStream StreamToZip = new System.IO.FileStream(FileToZip, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        string fileName = FileToZip.Substring(FileToZip.LastIndexOf("\\") + 1);

                        ZipEntry ZipEntry = new ZipEntry(fileName);

                        if (string.IsNullOrEmpty(password) == false)
                        {
                            //压缩文件加密
                            ZipStream.Password = password;
                        }

                        ZipStream.PutNextEntry(ZipEntry);

                        //设置压缩级别
                        ZipStream.SetLevel(CompressionLevel);

                        //缓存大小
                        byte[] buffer = new byte[BlockSize];

                        int sizeRead = 0;

                        try
                        {
                            do
                            {
                                sizeRead = StreamToZip.Read(buffer, 0, buffer.Length);
                                ZipStream.Write(buffer, 0, sizeRead);
                            }
                            while (sizeRead > 0);
                        }
                        catch (System.Exception ex)
                        {
                            throw ex;
                        }

                        StreamToZip.Close();
                    }

                    ZipStream.Finish();
                    ZipStream.Close();
                }

                ZipFile.Close();
            }
        }
        catch (Exception)
        {
            _result = false;
        }
        return _result;
    }




    /// <summary>
    /// ZIP:解压一个zip文件
    /// add yuangang by 2016-06-13
    /// </summary>
    /// <param name="ZipFile">需要解压的Zip文件（绝对路径）</param>
    /// <param name="TargetDirectory">解压到的目录</param>
    /// <param name="Password">解压密码</param>
    /// <param name="OverWrite">是否覆盖已存在的文件</param>
    public static string UnZip(string ZipFile, string TargetDirectory, string Password, bool OverWrite = true)
    {
        string _result = "";
        try
        {
            //如果解压到的目录不存在，则报错
            if (!System.IO.Directory.Exists(TargetDirectory))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + TargetDirectory + " 不存在!");
            }
            //目录结尾
            if (!TargetDirectory.EndsWith("\\")) { TargetDirectory = TargetDirectory + "\\"; }

            using (ZipInputStream zipfiles = new ZipInputStream(File.OpenRead(ZipFile)))
            {
                zipfiles.Password = Password;
                ZipEntry theEntry;

                while ((theEntry = zipfiles.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    string fileName = Path.GetFileName(pathToZip);

                    Directory.CreateDirectory(TargetDirectory + directoryName);

                    if (fileName != "")
                    {
                        if ((File.Exists(TargetDirectory + directoryName + fileName) && OverWrite) || (!File.Exists(TargetDirectory + directoryName + fileName)))
                        {
                            using (FileStream streamWriter = File.Create(TargetDirectory + directoryName + fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = zipfiles.Read(data, 0, data.Length);

                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }
                                streamWriter.Close();
                            }
                        }
                    }

                    _result = fileName;
                }

                zipfiles.Close();
            }
        }
        catch (Exception ex)
        {
            _result = "";
        }
        return _result;
    }


    /// <summary>
    /// ZIP:解压一个zip文件
    /// add yuangang by 2016-06-13
    /// </summary>
    /// <param name="ZipFile">需要解压的Zip文件（绝对路径）</param>
    /// <param name="TargetDirectory">解压到的目录</param>
    /// <param name="Password">解压密码</param>
    /// <param name="OverWrite">是否覆盖已存在的文件</param>
    public static List<string> UnZipFiles(string ZipFile, string TargetDirectory, string Password, bool OverWrite = true,bool isfullname=true)
    {
        List<string> _result = new List<string>();
        try
        {
            //如果解压到的目录不存在，则报错
            if (!System.IO.Directory.Exists(TargetDirectory))
            {
                throw new System.IO.FileNotFoundException("指定的目录: " + TargetDirectory + " 不存在!");
            }
            //目录结尾
            if (!TargetDirectory.EndsWith("\\")) { TargetDirectory = TargetDirectory + "\\"; }

            using (ZipInputStream zipfiles = new ZipInputStream(File.OpenRead(ZipFile)))
            {
                zipfiles.Password = Password;
                ZipEntry theEntry;

                while ((theEntry = zipfiles.GetNextEntry()) != null)
                {
                    string directoryName = "";
                    string pathToZip = "";
                    pathToZip = theEntry.Name;

                    if (pathToZip != "")
                        directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                    string fileName = Path.GetFileName(pathToZip);

                    Directory.CreateDirectory(TargetDirectory + directoryName);

                    if (fileName != "")
                    {
                        if ((File.Exists(TargetDirectory + directoryName + fileName) && OverWrite) || (!File.Exists(TargetDirectory + directoryName + fileName)))
                        {
                            using (FileStream streamWriter = File.Create(TargetDirectory + directoryName + fileName))
                            {
                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = zipfiles.Read(data, 0, data.Length);

                                    if (size > 0)
                                        streamWriter.Write(data, 0, size);
                                    else
                                        break;
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                    if (fileName != "")
                    {
                        if (isfullname)
                        {
                            _result.Add(TargetDirectory + directoryName + fileName);
                        }
                        else
                        {
                            _result.Add(fileName);
                        }
                    }
                }

                zipfiles.Close();
            }
        }
        catch (Exception ex)
        {
            LogTool.LogCommon.WriteLog(ex.ToString());
        }
        return _result;
    }
}