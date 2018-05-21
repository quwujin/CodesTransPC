using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHFramework.DB;

public partial class ProcessUpFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Right.IsAdmmin())
        {
            Response.Redirect("Login.aspx");
            return;
        }

        this.Bt_Process.Attributes.Add("onclick", "return confirm('确定要开始生处理？')");
        
    }
    protected void Bt_Process_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(txt_Pass.Text)) 
        {

            Response.Write("密码不能为空<br/>");
            return;
        }

        if (string.IsNullOrEmpty(txt_IdList.Text))
        { 
            Response.Write("文件ID 不能为空<br/>");
            return;
        }

        string[] keylist = txt_IdList.Text.Replace(Environment.NewLine, "$").Split('$');

        foreach (var item in keylist)
        {
            Response.Write("开始解析<br/>");
            BindBoxTool tool = new BindBoxTool();
            ReturnValue _result = tool.ProcessUpInfoData(int.Parse(item), txt_Pass.Text);
            Response.Write(_result.Success.ToString() + "<br/>");
            Response.Write(_result.ErrMessage + "<br/>");
            Response.Write(string.Join("-", _result.MessageList.ToArray<string>() + "<br/>"));
            
        }

       
    }
    protected void bt_ProcessIssueFile_Click(object sender, EventArgs e)
    {
        Response.Write("开始重新<br/>");
        if (string.IsNullOrEmpty(txt_Filename.Text))
        { 
            Response.Write("路径不能为空<br/>");
            return;
        }
        BindBoxTool tool = new BindBoxTool();
        var _result= tool.ReProcessBindBox(txt_Filename.Text, txt_Boxnumber.Text);
        Response.Write(_result.Success.ToString() + "<br/>");
        Response.Write(_result.ErrMessage + "<br/>");
        Response.Write(string.Join("-", _result.MessageList.ToArray<string>() + "<br/>"));
    }
}