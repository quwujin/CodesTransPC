<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= this.LoginTitle %></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/pages/signin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="account-container">
            <div class="content clearfix">
                <h1><%= this.LoginTitle %></h1>
                <div class="login-fields">
                    <div class="field form-line">
                        <!--用户名-->
                        <input type="text" id="txtUserName" name="username" value="" placeholder="用户名" class="login username-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <!--密码-->
                        <input type="password" id="txtPassword" name="password" value="" placeholder="密码" class="login password-field" runat="server" />
                    </div>
                </div>
                <div class="login-actions">
                    <asp:Button ID="btnLogin" runat="server" Text="登录" CssClass="button btn btn-success btn-large" OnClick="btnLogin_Click" />
                </div>
                <!-- .actions -->
            </div>
        </div>
    </form>
</body>
</html>
