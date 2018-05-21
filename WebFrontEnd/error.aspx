<%@ Page Language="C#" AutoEventWireup="true" CodeFile="error.aspx.cs" Inherits="error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    您无权限访问！（IP:<%= IPAddress %>）
    </div>

        <div id="hiddeninfo" style="display:none" runat="server">

        </div>
    </form>
</body>
</html>
