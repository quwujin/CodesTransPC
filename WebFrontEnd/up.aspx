<%@ Page Language="C#" AutoEventWireup="true" CodeFile="up.aspx.cs" Inherits="up" %>

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
    <form id="form2" runat="server">
        <div class="account-container">
            <div class="content clearfix">
                <h1>发布回传</h1>
                <div class="login-fields">
                    <div class="field form-line">
                        <asp:FileUpload ID="fileUp" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtTitle" name="txtTitle" value="" placeholder="描述" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtFileName" name="username" value="" placeholder="文件名" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtApplyDate" name="txtApplyDate" value="" placeholder="发布时间" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" class="login password-field" runat="server" />
                    </div>

                </div>
            </div>
            <div class="login-actions">
                <asp:Button ID="btnUp" runat="server" Text="保存" CssClass="button btn btn-success btn-large" OnClick="btnUp_Click" />
            </div>
        </div>
    </form>
</body>
</html>
