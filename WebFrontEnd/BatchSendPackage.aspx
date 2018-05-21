<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BatchSendPackage.aspx.cs" Inherits="BatchSendPackage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-1.9.1.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/pages/signin.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/laydate/laydate-v1.1/laydate/laydate.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="account-container">
            <div class="content clearfix">
                <h2>批量生成(库存<%=hasCount %>)</h2>
                <div class="login-fields">
                    <div class="field form-line">
                        <input type="text" id="txtTitle" name="txtMaxCount" value="" placeholder="标题" class="login password-field" runat="server" onblur="setMsg()" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtStartCount" name="txtMaxCount" value="" placeholder="起始包" class="login password-field" runat="server" />
                    </div>
                     <div class="field form-line">
                        <input type="text" id="txtEmale" name="txtMaxCount" value="" placeholder="邮件" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtPackageCount" name="txtMaxCount" value="" placeholder="包数量" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtCount" name="txtMaxCount" value="" placeholder="数量" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtEffectiveDateBegin" name="txtEffectiveDateBegin" value="" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" placeholder="生效开始日期" class="login password-field" runat="server" />
                    </div>
                </div>
                <div class="login-actions float">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button btn btn-success btn-large" OnClick="btnSave_Click" />
                </div>
                <div class="login-actions float">
                    <asp:Label ID="txtMsg" runat="server" style="color:red;"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

