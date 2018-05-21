<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apply.aspx.cs" Inherits="apply" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>发布下载文件</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/pages/signin.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/laydate/laydate-v1.1/laydate/laydate.js"></script>
</head>
<body>
    <div class="account-container">
        <div class="content clearfix">
            <form id="form1" runat="server">
                <h1>下载发布</h1>
                <div class="login-fields">
                    <div class="field form-line">
                        <input type="text" id="txtFileName" name="username" value="" placeholder="文件名" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtTitle" name="txtTitle" value="" placeholder="描述" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtApplyDate" name="txtApplyDate" value="" placeholder="发布时间" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtBathCode" name="txtBathCode" value="" placeholder="批号" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="password" id="txtSecret" name="txtSecret" value="" placeholder="解压密码" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="password" id="txtSecretConfirm" name="txtSecretConfirm" value="" placeholder="解压密码确认" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtCustomerEmail" name="txtCustomerEmail" value="" placeholder="客户邮箱" class="login password-field" runat="server" />
                    </div>
                </div>
                <div class="login-actions">
                    <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button btn btn-success btn-large" OnClick="btnSave_Click" />
                </div>
            </form>
        </div>
    </div>
</body>
</html>
