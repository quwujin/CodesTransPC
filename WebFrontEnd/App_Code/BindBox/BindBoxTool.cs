using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Text;
using YHFramework.DB;
using YHFramework.SysModel;
using YHFramework.DAL;

public class RequestBindData
{
    public string BoxNumber { get; set; }
    public List<string> CodeList = new List<string>();
}
public enum ActionName
{
    BindBox,
    SearchBox,
    SearchCode,
    CancelCode
}
public class BindBoxTool 
{

    /// <summary>
    /// 解析 txt 文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public List<RequestBindData> ReadBoxDataFile(string file)
    {
        List<RequestBindData> _dataResult = new List<RequestBindData>();
        Dictionary<string, List<string>> _result = new Dictionary<string, List<string>>();

        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            StreamReader read = new StreamReader(fs, Encoding.Default);
            string strReadline;
            while ((strReadline = read.ReadLine()) != null)
            {
                string[] data = strReadline.Split(',');
                if (data.Length == 2 && string.IsNullOrEmpty(data[0]) == false && string.IsNullOrEmpty(data[1]) == false)
                {
                    if (_result.Keys.Contains(data[1]) == false)
                    {
                        _result.Add(data[1], new List<string>());
                    }
                    _result[data[1]].Add(data[0]);
                }
                // strReadline即为按照行读取的字符串
            }
            read.Close();
            fs.Close();
        }
        foreach (var item in _result.Keys)
        {
            _dataResult.Add(new RequestBindData()
            {
                BoxNumber = item,
                CodeList = _result[item]
            });
        }

        return _dataResult;
    }

    
    /// <summary>
    /// 解析 xlsx
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public List<RequestBindData> ReadBoxDataFileByXls(string file)
    {
        List<RequestBindData> _dataResult = new List<RequestBindData>();
        Dictionary<string, List<string>> _result = new Dictionary<string, List<string>>();

        System.Data.DataTable dt = Common.NPOIHelper.Import(file);
        foreach (DataRow item in dt.Rows)
        {
            string boxnumber = item[0].ToString();
            string codedata = item[1].ToString();
            if (string.IsNullOrEmpty(boxnumber) == false && string.IsNullOrEmpty(codedata) == false)
            {
                if (_result.Keys.Contains(boxnumber) == false)
                {
                    _result.Add(boxnumber, new List<string>());
                }
                _result[boxnumber].Add(codedata);
            }
        }

        foreach (var item in _result.Keys)
        {
            _dataResult.Add(new RequestBindData()
            {
                BoxNumber = item,
                CodeList = _result[item]
            });
        }

        return _dataResult;
    }


    /// <summary>
    /// 绑定文件
    /// </summary>
    /// <param name="requestdata"></param>
    /// <param name="filename"></param>
    /// <returns></returns>
    public ReturnValue ProcessBindBox(RequestBindData requestdata, string filename,bool needbind=true,bool boxappend=false)
    {
        ReturnValue _result = new ReturnValue();


        YHFramework.DAL.BoxNumberDal boxnumberdal = new YHFramework.DAL.BoxNumberDal();
        YHFramework.DAL.CodeDataDal codedatadal = new YHFramework.DAL.CodeDataDal();

        if (string.IsNullOrEmpty(requestdata.BoxNumber))
        {
            _result.Success = false;
            _result.ErrMessage = "当前箱号不能为空：";
            return _result;
        }

        if (requestdata.CodeList.Count == 0)
        {
            _result.Success = false;
            _result.ErrMessage = "当前箱号对应的串码不能为空：";
            return _result;
        }



        #region 检查箱号是否可用

        YHFramework.SysModel.BoxNumberModel boxdata = boxnumberdal.GetModelByBoxNumber(requestdata.BoxNumber);
        if (boxdata.BoxNumberId == 0)
        {
            #region 创建箱码数据
            boxdata.BoxNumber = requestdata.BoxNumber;
            boxdata.ShortBoxNumber = requestdata.BoxNumber;
            boxdata.BatchNumber = filename;
            boxdata.IsBind = false;
            boxdata.ApployInfoId = 0;
            boxdata.CreateOn = DateTime.Now;
            boxdata.ContentStatusId = "2";
            boxdata.BoxNumberId = boxnumberdal.Add(boxdata);
            #endregion
        }
        if (boxdata.BoxNumberId == 0)
        {
            _result.Success = false;
            _result.ErrMessage = "创建箱码失败：" + requestdata.BoxNumber;
            return _result;
        }

        if (boxdata.IsBind )
        {
            if (boxappend == false)
            {
                _result.Success = false;
                _result.ErrMessage = "当前箱号已被使用：" + requestdata.BoxNumber;
                return _result;
            } 
        }
        
           
       

        #endregion

        if (needbind == false) 
        {
            return _result;
        }

        #region 检查串码是否可用

        List<YHFramework.SysModel.CodeDataModel> codedatalist = codedatadal.GetModelListByCodelist(requestdata.CodeList);
        if (codedatalist.Count != requestdata.CodeList.Count)
        {


            List<string> distinctkey = new List<string>();
            System.Text.StringBuilder nofundcodelist = new System.Text.StringBuilder();
            System.Text.StringBuilder repeatercodelist = new System.Text.StringBuilder();
            foreach (var item in requestdata.CodeList)
            {

                if (distinctkey.Contains(item) == false)
                {
                    distinctkey.Add(item);
                }
                else
                {
                    repeatercodelist.AppendLine(item);
                }


                if (codedatalist.Find(s => s.CodeData == item) == null)
                {
                    nofundcodelist.AppendLine(item);
                }

            }




            _result.Success = false;
            _result.ErrMessage = "当前箱号对应的串码部分不存在：" + nofundcodelist.ToString();
            if (repeatercodelist.Length > 0)
            {
                _result.ErrMessage += "当前箱号对应的串码部分重复：" + repeatercodelist.ToString();
            }
            return _result;
        }
        List<YHFramework.SysModel.CodeDataModel> errcodedatalist = codedatalist.FindAll(s => s.BoxNumberId > 0);
        if (errcodedatalist.Count > 0)
        {
            _result.Success = false;
            _result.ErrMessage = "当前箱号对应的串码部分已经分配箱号：";
            string errdata = "";
            errdata = "【异常】前箱号对应的串码部分已经分配箱号：";
            foreach (var item in errcodedatalist)
            {
                _result.ErrMessage = _result.ErrMessage + "," + item.CodeData;
                errdata = errdata + "," + item.CodeData + "-" + item.BoxNumberId + "-" + requestdata.BoxNumber;
            }
            LogTool.LogCommon.WriteFileLog(errdata, "异常");
            return _result;

        }

        #endregion


        #region 开始绑定


        ReturnValue dataresult = codedatadal.BindBoxData(boxdata.BoxNumberId, requestdata.CodeList, boxappend);

        _result.Success = dataresult.Success;
        _result.ErrMessage = dataresult.ErrMessage;  

        #endregion


        return _result;
    }



    public ReturnValue ProcessUpInfoData(int upInfoId,string password) 
    {
        ReturnValue _result = new ReturnValue(); 
        YHFramework.DAL.ActionLogDal actionlog = new YHFramework.DAL.ActionLogDal();
        YHFramework.DAL.UpInfoDal upinfodal = new YHFramework.DAL.UpInfoDal(); 
        IProcessFile processFile=ProcessFileFactory.CreateNewObj(ProcessFileType.DaNeng_Hongquan);

        UpInfoModel upinfodata=  upinfodal.GetModel(upInfoId);
       
        if (upinfodata.ID == 0) 
        {
            _result.Success = false;
            _result.ErrMessage = "没有找到任何数据";
        }
        if (upinfodata.Status != 0) 
        {
            _result.Success = false;
            _result.ErrMessage = "文件已经被处理完毕";
        }
        upinfodata.Status = 1;
        upinfodata.Remarks += "开始处理" + upinfodata.Status + DateTime.Now.ToString("yyyyMMddHHmmss");
        upinfodata.UpdateTime = DateTime.Now;
        upinfodal.Update(upinfodata);
         
        #region 准备文件

        string filename = System.AppDomain.CurrentDomain.BaseDirectory + upinfodata.Url.Replace("/", "\\").TrimStart("\\".ToCharArray());
         
        #endregion


        #region 解压文件
        _result = processFile.UnZipFiles(filename, password);
        if (_result.Success == false) 
        {
            #region  日志
            actionlog.Add(new ActionLogModel()
            {
                ActionName = "离线文件绑定-"+"解压文件",
                Notes =_result.ErrMessage,
                KeyData = upinfodata.ID.ToString(),
                UserName = Right.UserId.ToString(),
                ActionResult = "失败" ,
                CreateOn = DateTime.Now
            }); 
            #endregion
            
            return _result;
        } 
        #endregion

        List<string> taskFiles = _result.ObjectValue as List<string>;
        if (taskFiles.Count == 0) 
        {
            _result.Success = false;
            _result.ErrMessage = "压缩包文件没有找到任何文件" + filename;
            #region  日志
            actionlog.Add(new ActionLogModel()
            {
                ActionName = "离线文件绑定-" + "解压文件",
                Notes = _result.ErrMessage,
                KeyData = upinfodata.ID.ToString(),
                UserName = Right.UserId.ToString(),
                ActionResult = "失败",
                CreateOn = DateTime.Now
            });
            #endregion

            return _result;
        }

        #region 处理文件

        foreach (string item in taskFiles)
        { 
           ReturnValue _itemResult=  processFile.RealseDataFile(item);
           if (_itemResult.Success == false) 
           {
               _result.MessageList.Add(_itemResult.ErrMessage);
               #region  日志
               actionlog.Add(new ActionLogModel()
               {
                   ActionName = "离线文件绑定-" + "解析文件",
                   Notes = item+_itemResult.ErrMessage,
                   KeyData = upinfodata.ID.ToString(),
                   UserName = Right.UserId.ToString(),
                   ActionResult = "失败",
                   CreateOn = DateTime.Now
               }); 
               #endregion

               continue;
           }
           string xlsfilename=System.IO.Path.GetFileName(item);
           List<RequestBindData> reqeustdatalist = _itemResult.ObjectValue as List<RequestBindData>;

           foreach (RequestBindData requestdata in reqeustdatalist)
           {
               ReturnValue _bindResult = ProcessBindBox(requestdata, upinfodata.Url + "--" + xlsfilename, true, true);
               if (_bindResult.Success == false)
               {
                   _result.MessageList.Add(_bindResult.ErrMessage);
                   #region  日志
                   actionlog.Add(new ActionLogModel()
                   {
                       ActionName = "离线文件绑定-" + "绑定",
                       Notes = requestdata.BoxNumber + "-" + item + "-" + _bindResult.ErrMessage,
                       KeyData = upinfodata.ID.ToString(),
                       UserName = Right.UserId.ToString(),
                       ActionResult = "失败",
                       CreateOn = DateTime.Now
                   });
                   #endregion

               }
                 
           }
        }


        #endregion

        #region 更新任务状态
        upinfodata.Status = 2;
        upinfodata.Remarks += "更新完毕" + upinfodata.Status + DateTime.Now.ToString("yyyyMMddHHmmss");
        upinfodata.UpdateTime = DateTime.Now;
        upinfodal.Update(upinfodata);
        #endregion
        #region  日志
        actionlog.Add(new ActionLogModel()
        {
            ActionName = "离线文件绑定-" + "完成",
            Notes = _result.ErrMessage+string.Join("-",_result.MessageList.ToArray<string>()) ,
            KeyData = upinfodata.ID.ToString(),
            UserName = Right.UserId.ToString(),
            ActionResult = "执行完毕成功",
            CreateOn = DateTime.Now
        });
        #endregion
        return _result;
    }


    /// <summary>
    /// 单独处理问题箱子
    /// </summary>
    /// <param name="issueFiles"></param>
    /// <param name="boxnumber">如果传具体箱子 则单独处理该箱数据</param>
    /// <returns></returns>
    public ReturnValue ReProcessBindBox(string issueFiles,string boxnumber=null) 
    {
        ReturnValue _result = new ReturnValue();
        YHFramework.DAL.ActionLogDal actionlog = new YHFramework.DAL.ActionLogDal(); 
        IProcessFile processFile = ProcessFileFactory.CreateNewObj(ProcessFileType.DaNeng_Hongquan);


        ReturnValue _itemResult = processFile.RealseDataFile(issueFiles);
        if (_itemResult.Success == false)
        {
            _result.MessageList.Add(_itemResult.ErrMessage);
            #region  日志
            actionlog.Add(new ActionLogModel()
            {
                ActionName = "离线文件绑定-" + "解析文件",
                Notes = issueFiles + _itemResult.ErrMessage,
                KeyData = "手工操作",
                UserName = Right.UserId.ToString(),
                ActionResult = "失败",
                CreateOn = DateTime.Now
            });
            #endregion
             _result.Success=false;
             return _result;
        }
        string xlsfilename = System.IO.Path.GetFileName(issueFiles);
        List<RequestBindData> reqeustdatalist = _itemResult.ObjectValue as List<RequestBindData>;

        foreach (RequestBindData requestdata in reqeustdatalist)
        {
            if (string.IsNullOrEmpty(boxnumber) == false) 
            {
                if (requestdata.BoxNumber != boxnumber) 
                {
                    continue;
                }
            }

            ReturnValue _bindResult = ProcessBindBox(requestdata, "问题文件--" + xlsfilename, true, true);
            if (_bindResult.Success == false)
            {
                _result.MessageList.Add(_bindResult.ErrMessage);
                #region  日志
                actionlog.Add(new ActionLogModel()
                {
                    ActionName = "离线文件绑定-" + "绑定",
                    Notes = requestdata.BoxNumber + "-" + issueFiles + "-" + _bindResult.ErrMessage,
                    KeyData =  "手工操作",
                    UserName = Right.UserId.ToString(),
                    ActionResult = "失败",
                    CreateOn = DateTime.Now
                });
                #endregion
                _result.Success = false;
                return _result;
            }

        }

        return _result;
    }
}