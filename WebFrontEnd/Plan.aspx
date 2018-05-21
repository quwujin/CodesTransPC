<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Plan.aspx.cs" Inherits="Plan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>批量生成</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="css/pages/signin.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/laydate/laydate-v1.1/laydate/laydate.js"></script>
</head>
<body>
    <div class="account-container">
        <div class="content clearfix">
            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3000" >
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <h1>批量生成</h1>
                        <div class="login-fields">
                            <div class="field form-line">
                                <asp:DropDownList ID="ddType" runat="server">
                                    <asp:ListItem Value="" Text="请选择码类型"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="串码"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="箱码"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="field form-line">
                                <input type="text" id="txtMaxCount" name="txtMaxCount" value="" placeholder="计划生成数量" class="login password-field" runat="server" />
                            </div>
                            <div class="field form-line">
                                <input type="text" id="txtEachCount" name="txtEachCount" value="" placeholder="打包码的个数" class="login password-field" runat="server" />
                            </div>
                            <div class="field form-line">
                                <input type="text" id="txtBathCodeBegin" name="txtBathCodeBegin" value="" placeholder="开始批号" class="login password-field" runat="server" />
                            </div>
                            <div class="field form-line">
                                <input type="text" id="txtEffectiveDateBegin" name="txtEffectiveDateBegin" value="" onclick="laydate({ istime: true, format: 'YYYY-MM-DD' })" placeholder="生效开始日期" class="login password-field" runat="server" />
                            </div>
                            <div class="field form-line">
                                <input type="text" id="txtCustomerEmail" name="txtCustomerEmail" value="" placeholder="客户邮箱" class="login password-field" runat="server" />
                            </div>
                        </div>
                        <div class="login-actions">
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="button btn btn-success btn-large" OnClick="btnSave_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <span>正在执行……</span>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </form>
        </div>
    </div>
</body>
</html>
