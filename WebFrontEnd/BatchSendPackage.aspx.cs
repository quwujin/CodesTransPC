using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DAL;
using YHFramework.SysModel;

public partial class BatchSendPackage : System.Web.UI.Page
{
    ApployInfoDal adal = new ApployInfoDal();
    CodeDataDal cdal = new CodeDataDal();
    public int hasCount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        hasCount = cdal.GetRowCount(string.Format(" and ContentStatusId=2 and ISNULL(ApployInfoId,0)=0 "));
        if (!Right.IsAdmmin())
        {
            Response.Redirect("Login.aspx");
            return;
        }

        this.btnSave.Attributes.Add("click", "return confirm('确定要开始生成码包吗？')");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        this.txtMsg.Text = "开始压缩";
        string txtTitle = this.txtTitle.Value.Trim();       //标题
        string txtEmale = this.txtEmale.Value.Trim();       //邮件
        int txtStartCount = string.IsNullOrEmpty(this.txtStartCount.Value.Trim()) ? 0 : Convert.ToInt32(this.txtStartCount.Value.Trim()); //起始包   
        int txtPackageCount = string.IsNullOrEmpty(this.txtPackageCount.Value.Trim()) ? 0 : Convert.ToInt32(this.txtPackageCount.Value.Trim()); //包数量   
        int txtCount = string.IsNullOrEmpty(this.txtCount.Value.Trim()) ? 0 : Convert.ToInt32(this.txtCount.Value.Trim()); //数量   
        string txtCreateTime = this.txtEffectiveDateBegin.Value;    //压缩时间
        string txtEmaile = this.txtEmale.Value.Trim();  //邮件

        #region 查询串码数量是否足够
        if (cdal.GetRowCount(string.Format(" and ContentStatusId=2 ")) < txtCount * txtPackageCount)
        {
            this.txtMsg.Text = "库内数量不足";
            return;
        }
        #endregion

        #region 添加批次&&串码绑定批次
        ApployInfoModel amodel = new ApployInfoModel();
        amodel.ApployTime = Convert.ToDateTime(txtCreateTime);
        amodel.CustomerEmail = txtEmaile;
        amodel.Type = 0;
        amodel.Status = 0;
        amodel.CreatedTime = DateTime.Now;


        for (int i=0; i < txtPackageCount; i++) {
            if (txtStartCount < 10)
            {
                amodel.Title = txtTitle + "0" + txtStartCount.ToString();   //标题
            }
            else {
                amodel.Title = txtTitle + txtStartCount.ToString();
            }
            amodel.FileName = amodel.Title + ".zip";    //文件名
            amodel.BathCode = amodel.Title;             //批次
            amodel.Secret = GetCharAndNum(12);          //密码
            amodel.Url = "/down/" + amodel.FileName;

            int ApployInfoId = adal.Add(amodel);

            if (ApployInfoId > 0)
            {
                cdal.SendPackage(txtCount, ApployInfoId);
            }

            txtStartCount++;
        }

        this.txtMsg.Text = "批量导包成功";
        #endregion

    }


    #region 创建随机字母加数字（0-9A-Z）
    public string GetCharAndNum(int num)
    {
        string a = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < num; i++)
        {
            sb.Append(a[new Random(Guid.NewGuid().GetHashCode()).Next(0, a.Length - 1)]);
        }
        return sb.ToString();
    }
    #endregion
}