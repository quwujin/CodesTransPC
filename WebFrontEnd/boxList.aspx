<%@ Page Language="C#" AutoEventWireup="true" CodeFile="boxList.aspx.cs" Inherits="boxList" %>
<%@ Register assembly="AspNetPager" namespace="Wuqi.Webdiyer" tagprefix="webdiyer" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-1.9.1.min.js"></script>
    <script src="js/bootstrap.js"></script>
    <style>
        #form1 {
            width: 60%;
            margin: 30px auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>项目编号</th>
                        <th>箱号</th>
                        <th>批次</th>
                        <th>是否回传</th>
                        <th>回传时间</th>
                        <th>生存时间</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="boxLists">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("BoxNumber").ToString() %></td>
                                <td><%#Eval("ShortBoxNumber").ToString() %></td>
                                <td><%#Eval("BatchNumber").ToString() %></td>
                                <td><%#Eval("IsBind").ToString() %></td> 
                                <td><%#Eval("BindTime").ToString() %></td>
                                <td><%#Eval("CreateOn").ToString() %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        <webdiyer:AspNetPager ID="AspNetPager1" CssClass="page" runat="server" OnPageChanged="AspNetPager1_PageChanged"
            CurrentPageButtonPosition="Center" PageSize="30"
            ShowCustomInfoSection="Right" PageIndexBoxType="TextBox"
            ShowPageIndexBox="Never"
            CustomInfoHTML="<span>当前第 %CurrentPageIndex% 页, 共 %PageCount%页 共%RecordCount% 条记录</span>" FirstPageText="首页"
            LastPageText="尾页" NextPageText="下一页" PrevPageText="上一页">
        </webdiyer:AspNetPager>
    </div>
    </form>
</body>
</html>
