<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessUpFile.aspx.cs" Inherits="ProcessUpFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        文件ID<asp:TextBox ID="txt_IdList" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
&nbsp; 密码：<asp:TextBox ID="txt_Pass" runat="server"></asp:TextBox>
        <asp:Button ID="Bt_Process" runat="server" Text="处理" OnClick="Bt_Process_Click" />
    
    </div>

   <fieldset title="单独处理问题文件" >
       文件路径：<asp:TextBox ID="txt_Filename" runat="server"></asp:TextBox>
        特定箱码可为空：<asp:TextBox ID="txt_Boxnumber" runat="server"></asp:TextBox>
       <asp:Button ID="bt_ProcessIssueFile" runat="server" Text="处理问题文件" OnClick="bt_ProcessIssueFile_Click" />

   </fieldset>
    </form>
</body>
</html>
