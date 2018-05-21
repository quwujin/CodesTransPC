<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JackDemo.aspx.cs" Inherits="JackDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" Visible="false" runat="server" OnClick="Button1_Click" Text="重新压包" />
      
        <asp:Button ID="Button2" Visible="false" runat="server"  Text="检查码包状态" OnClick="Button2_Click" />
      
    </div>
    </form>
</body>
</html>
