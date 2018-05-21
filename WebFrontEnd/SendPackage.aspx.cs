using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;
using YHFramework.SysModel;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

public partial class SendPackage : System.Web.UI.Page
{
    ApployInfoDal adal = new ApployInfoDal();
    CodeDataDal cdal = new CodeDataDal();
    public int hasCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        hasCount = cdal.GetRowCount(string.Format(" and ContentStatusId=2 and ISNULL(ApployInfoId,0)=0 ") );
        if (!Right.IsAdmmin())
        {
            Response.Redirect("Login.aspx");
            return;
        }

        this.btnSave.Attributes.Add("click","return confirm('确定要开始生成码包吗？')");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.txtMsg.Text = "开始压缩";
        string txtTitle = this.txtTitle.Value.Trim();               //标题
        string txtFileName = this.txtFileName.Value.Trim();         //压缩文件名称
        string txtBathCode = this.txtBathCode.Value.Trim();         //批次
        string txtSecret = this.txtSecret.Value.Trim();             //压缩密码
        int txtCount = string.IsNullOrEmpty(this.txtCount.Value.Trim())?0:Convert.ToInt32(this.txtCount.Value.Trim()); //数量   
        string txtCreateTime = this.txtEffectiveDateBegin.Value;    //压缩时间
        string txtEmaile = this.txtEmale.Value.Trim();  //邮件

        #region 验证批次号唯一
        ApployInfoModel amodel = new ApployInfoModel();
        amodel = adal.GetModel(string.Format(" and FileName='{0}' ", txtFileName));
        if (amodel.ID > 0) {
            this.txtMsg.Text = "文件名不能重复";
            return;
        }
        #endregion

        #region 查询串码数量是否足够
        if (cdal.GetRowCount(string.Format(" and ContentStatusId=2 ")) < txtCount) {
            this.txtMsg.Text = "库内数量不足";
            return;
        }
        #endregion

        #region 添加批次&&串码绑定批次
        amodel.Title = txtTitle;
        amodel.FileName = txtFileName;
        amodel.ApployTime = Convert.ToDateTime(txtCreateTime);
        amodel.BathCode = txtBathCode;
        amodel.Secret = txtSecret;
        amodel.CustomerEmail = txtEmaile;
        amodel.Type = 0;
        amodel.Url = "/down/"+amodel.FileName;
        amodel.Status = 0;
        amodel.CreatedTime = DateTime.Now;

        int ApployInfoId = adal.Add(amodel);

        if (ApployInfoId > 0) {
            int count = cdal.SendPackage(txtCount, ApployInfoId);
            if (count > 0) {
                 ExportFileByZip(ApployInfoId);
            }
        }
        #endregion

    }

    public void ExportFileByZip(int apployInfoId)
    {
        YHFramework.DAL.ApployInfoDal apployInfoDal = new ApployInfoDal();
        YHFramework.SysModel.ApployInfoModel model = apployInfoDal.GetModel(apployInfoId);
        OutFileFactory FileFactory = new OutFileFactory();
        IOutFile outfile = FileFactory.CreateNewObj(GetOutFileType(apployInfoId));
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
            this.txtMsg.Text = "压缩成功";
        }
        else {
            this.txtMsg.Text = "压缩失败";
        }
        #endregion

        #region 更新任务

        #endregion
    }


    public OutFileType GetOutFileType(int apployInfoId) 
    {
        OutFileType OutFileType = OutFileType.TXT; 

        OutFileType = (OutFileType)Enum.Parse(typeof(OutFileType), System.Configuration.ConfigurationManager.AppSettings["OutFileType"]); 

        return OutFileType;

    }
}