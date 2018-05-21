<%@ Page Language="C#" AutoEventWireup="true" CodeFile="upList.aspx.cs" Inherits="upList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        table {
            border: solid 1px black;
            border-collapse: collapse;
        }

            table td, table th {
                border: solid 1px black;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="table  table-bordered">
                <thead>
                    <tr>
                        <th>文件名</th>
                        <th>描述</th>
                        <th>发布时间</th>
                        <th>批号</th>
                        <th>客户邮箱</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptrList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# DataBinder.Eval(Container.DataItem, "FileName") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "Title") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "ApployTime") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "BathCode") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem, "CustomerEmail") %></td>
                                <td><%# GetDownloadLink(DataBinder.Eval(Container.DataItem, "Url").ToString()) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
