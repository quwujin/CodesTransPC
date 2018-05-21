<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendPackage.aspx.cs" Inherits="SendPackage" %>

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
    <script>
        function setMsg() {
            //var msg = $("#txtTitle").val();
           // $("#txtFileName").val(msg.replace('天津希赛', '_XS') + ".zip");
           // $("#txtBathCode").val(msg.replace('天津希赛', '_XS'));
          
        }

        function randomString(len) {
            len = len || 32;
            var $chars = 'ABCDEFGHJKMNPQRSTWXYZ2345678';    
            var maxPos = $chars.length;
            var pwd = '';
            for (i = 0; i < len; i++) {
                pwd += $chars.charAt(Math.floor(Math.random() * maxPos));
            }
            return pwd;
        }

        function getnewpass()
        {
            $("#txtSecret").val(randomString(15));
        }
    </script>
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
                        <input type="text" id="txtFileName" name="txtMaxCount" value="" placeholder="压缩包名称" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtBathCode" name="txtMaxCount" value="" placeholder="批次" class="login password-field" runat="server" />
                    </div>
                     <div class="field form-line">
                        <input type="text" id="txtEmale" name="txtMaxCount" value="Wei.Wu@bericap.com" placeholder="邮件" class="login password-field" runat="server" />
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtSecret" name="txtMaxCount" value="" placeholder="压缩密码" class="login password-field" runat="server" /> <span id="bt_GetPass" name="bt_GetPass" onclick="getnewpass()" >生成密码</span>
                    </div>
                    <div class="field form-line">
                        <input type="text" id="txtCount" name="txtMaxCount" value="500000" placeholder="数量" class="login password-field" runat="server" />
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
