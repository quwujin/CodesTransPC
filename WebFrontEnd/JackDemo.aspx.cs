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


public partial class JackDemo : System.Web.UI.Page
{
    System.Diagnostics.Stopwatch watch = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false) 
        {
            if (Request["s"] == "ssss") 
            {
                Button1.Visible = true;
                Button2.Visible = true;
            }
        }


    }
    protected void Button1_Click(object sender, EventArgs e)
    { ;

        watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        Response.Write(watch.Elapsed.TotalMilliseconds);
        Response.Write("开始压缩<br/>");
        ExportFileByZip(18); 
        watch.Stop();
        Response.Write(watch.Elapsed.TotalMilliseconds);

        return;

        //YHFramework.DAL.CodeDataDal codedatadal = new CodeDataDal();
        //OutFileFactory FileFactory = new OutFileFactory();
        //IOutFile outfile = FileFactory.CreateNewObj(OutFileType.XLSX2);
        //string tempfilename = "test" + outfile.ExtName; 
        //string tempfilepath = System.AppDomain.CurrentDomain.BaseDirectory + "tempfile\\";
        //DataTable dt2 = codedatadal.GetPageList(" a.* ", "", "", 1, 100);
        //outfile.OutFilePath = tempfilepath;
        //outfile.FileName = tempfilename;

        //if (File.Exists(tempfilepath + tempfilename))
        //{
        //    File.Delete(tempfilepath + tempfilename);
        //}
        //outfile.OutFile(dt2);

        //return;


        //DataTable dt = TableDAL.GetTable("ApployInfo", "id=" + 40);

        //if (null != dt && 0 < dt.Rows.Count)
        //{
        //    dt.Rows[0]["Status"] = 2;
        //    dt.Rows[0]["UpdateTime"] = System.DateTime.Now;
             

        //    #region --发送邮件
        //    string mailBody = "";
        //    mailBody += "农夫-河源工厂串码传输平台项目(批号：" + dt.Rows[0]["BathCode"].ToString() + ")下载的解压密码为：" + dt.Rows[0]["Secret"].ToString() + System.Environment.NewLine + System.Environment.NewLine;
        //    mailBody += "本邮件中所包含的信息可能是保密并受相关法律保护的。本邮件仅供收件人参阅。我们特此告知，如果您不是特定的收件人，您对本邮件任何的使用、转发、传播或复制是被严令禁止的并且可能是违法的，请通过发还电子邮件的方式联系发件人，并且销毁所有的原始邮件。" + System.Environment.NewLine;
        //    //string titles = "建项申请,【待审批】项目名称:" + m.Name + ",项目编号:" + m.ProjectNo; modify by lanyiwei 20161109 title 显示不下全部内容，进行精简
        //    string titles = "【农夫-河源工厂串码传输平台】解压密码";
        //    Common.EmailTool.sendEmail("jack.song@esmartwave.com", titles, mailBody, "");

            
        //    #endregion
        //}
        //Response.Write("测试结束<br/>");
        //return;
         
        Response.Write("开始解析<br/>");
        BindBoxTool tool = new BindBoxTool();
        //ReturnValue _result = tool.ProcessUpInfoData(52, "HONG@00.quan");
        //Response.Write(_result.Success.ToString() + "<br/>"); 
        //Response.Write(_result.ErrMessage + "<br/>");
      


        //watch= new System.Diagnostics.Stopwatch();
        //watch.Start();
       // Response.Write(watch.Elapsed.TotalMilliseconds);
        //Response.Write("开始压缩<br/>");
       // ExportFileByZip(1);
       // ExportFileByZip(2);
         

        //watch.Stop();
       // Response.Write(watch.Elapsed.TotalMilliseconds);
    }


    public void ExportFileByZip(int apployInfoId)
    {
        YHFramework.DAL.ApployInfoDal apployInfoDal = new ApployInfoDal();
        YHFramework.SysModel.ApployInfoModel model = apployInfoDal.GetModel(apployInfoId);
        OutFileFactory FileFactory = new OutFileFactory();
        IOutFile outfile = FileFactory.CreateNewObj(OutFileType.XLSX2);
        FileInfo fileinfo = new FileInfo(model.FileName);
        string fileanme = fileinfo.Name.Replace(fileinfo.Extension, "");
        string tempfilename = fileanme + outfile.ExtName;
        string zipfilename = fileanme + ".zip"; 
        string tempfilepath = System.AppDomain.CurrentDomain.BaseDirectory + "tempfile\\";
        string zipfilepath = System.AppDomain.CurrentDomain.BaseDirectory + "down\\";
        if (Directory.Exists(tempfilepath) == false)
        {
            Directory.CreateDirectory(tempfilepath);
        }

        #region 清理文件

        if (File.Exists(tempfilepath + tempfilename))
        {
            File.Delete(tempfilepath + tempfilename);
        }

        if (File.Exists(zipfilepath + zipfilename))
        {
            File.Delete(zipfilepath + zipfilename);
        }

        #endregion

        #region 写入临时文件

        int pagezise = 20000;
        int pagecount = 0;
        int datacount = 0;


        YHFramework.DAL.CodeDataDal codedatadal = new CodeDataDal();
        string sqlwhere = " and ApployInfoId=" + apployInfoId + " ";
        datacount = codedatadal.GetCount(sqlwhere, "");
        if (datacount % pagezise == 0)
        {
            pagecount = datacount / pagezise;
        }
        else
        {
            pagecount = datacount / pagezise + 1;
        }

        //for (int i = 1; i <= pagecount; i++)
        for (int i = 1; i <= pagecount; i++)
        {
            DataTable dt = codedatadal.GetPageList(" a.* ", "", sqlwhere, i, pagezise);
            outfile.OutFilePath = tempfilepath;
            outfile.FileName = tempfilename;
            outfile.OutFile(dt);
        }


        #endregion

        #region 压缩
        bool result = ZipCommon.ZipFile(tempfilepath + tempfilename, zipfilepath, fileanme, 5, 2048, model.Secret);
        if (result)
        {
           Response.Write( "压缩成功");
        }
        else
        {
           Response.Write(  "压缩失败");
        }
        #endregion

        #region 更新任务

        #endregion
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        List<int> idlist = new List<int>();

        bool doall = true;
        if (doall)
        {
            idlist.Clear();

            YHFramework.DAL.ApployInfoDal applist = new ApployInfoDal();
            List<YHFramework.SysModel.ApployInfoModel> datalist = applist.GetModelList();
            foreach (var item in datalist)
            {
                if (item.AutoCheck == false)
                {
                    idlist.Add(item.ID);
                } 

            }
        }

        ApplyTools ApplyTools = new ApplyTools();
        foreach (var idkey in idlist)
        { 
            ApplyTools.GetBagStatusInfo(idkey); 
        }

        Response.Write("执行完毕<br/>");

    }
}