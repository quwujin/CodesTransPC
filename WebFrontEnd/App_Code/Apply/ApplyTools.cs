using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using YHFramework.DAL;

/// <summary>
/// ApplyTools 的摘要说明
/// </summary>
public class ApplyTools
{
	public ApplyTools()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}



    public string GetStatus(object status)
    {
        int statusid = Common.Fun.ConvertTo<int>(status, 0);

        if (statusid == 1)
        {
            return "未确认";
        }
        if (statusid == 2)
        {
            return "已下载";
        }
        return "未下载";
    }

    public string GetDownTime(object status, object downdate)
    {
        int statusid = Common.Fun.ConvertTo<int>(status, 0);

        if (statusid == 1)
        {
            return "";
        }
        if (statusid == 2)
        {
            return Common.Fun.ConvertTo<DateTime>(downdate, DateTime.Now).ToString();
        }
        return "";
    }


    public string GetBagStatusInfo(object appid)
    {
        string _result = "";
        int _appId = Common.Fun.ConvertTo<int>(appid, 0);
        YHFramework.DAL.ApployInfoDal appdal = new ApployInfoDal();
        YHFramework.SysModel.ApployInfoModel appinfodata = appdal.GetModel(_appId);
        if (appinfodata.AutoCheck == false)
        {
            int datarow = appdal.GetCodeRowCountByAppId(_appId);
            FileInfo file = new FileInfo(System.AppDomain.CurrentDomain.BaseDirectory + appinfodata.Url);
            if (file.Exists)
            {
                _result = "大小：" + file.Length / 1024 / 1024 + ",串码数量：" + datarow;
            }
            else
            {
                _result = "文件不存在";
            }

            appdal.UpdateBagInfo(_appId, _result);


            #region 写日志

            new YHFramework.DAL.ApployCheckLogDal().Add(new YHFramework.SysModel.ApployCheckLogModel()
            {
                ApployId = _appId,
                CreateOn = DateTime.Now,
                FileName = appinfodata.FileName,
                Message = _result,
                Number = datarow,
                TypeCode = "BagCheck"
            });

            #endregion
        }
        else
        {
            _result = appinfodata.CheckResult;
        }
        return _result;
    }

}