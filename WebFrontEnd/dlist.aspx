<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dlist.aspx.cs" Inherits="dlist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%= this.strTitle %></title>
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/index.css" />
    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function down(id) {
            var res = DownList.Down(id).value;
            if(res){            
                window.location = window.location;
            }
        }

        function done(id, fileNmae){
            if(confirm('是否确认'+fileNmae+'数据包无误。数据一旦确认将无法再次下载！请务必仔细确认。')){
                var res = DownList.DataConfirm(id).value;
                if(res){
                    window.location = window.location;
                }
            }
        }
        

    </script>
    <style type="text/css">
        .en_2 {
            background-color: #e4e0e0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wind container">
            <h1><%= this.strTitle %></h1>
            <table class="table  table-bordered">
                <thead>
                    <tr>
                        <th class="log">文件名</th>
                        <th>发布时间</th>
                        <th>描述</th>
                        <th>批号</th>
                        <th>状态</th>
                        <th colspan="2"></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptrList" runat="server">
                        <ItemTemplate>
                            <tr id="tr_<%# DataBinder.Eval(Container.DataItem,"ID") %>" class="en_<%# DataBinder.Eval(Container.DataItem,"Status") %>">
                                <td><%# DataBinder.Eval(Container.DataItem,"FileName") %></td>
                                <td><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"ApployTime")).ToString("yyyy-MM-dd") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem,"Title") %></td>
                                <td><%# DataBinder.Eval(Container.DataItem,"BathCode") %></td>
                                <td><%# StatDesc(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Status"))) %></td>
                                <td class="edit"><a onclick="down(<%# DataBinder.Eval(Container.DataItem,"ID") %>)" href="<%= this.Request.ApplicationPath.TrimEnd(new char[]{'/'}) %><%# DataBinder.Eval(Container.DataItem,"url") %>" target="_blank">下载</a></td>
                                <td class="edit">
                                    <input type="button" value="数据确认" onclick="done(<%# DataBinder.Eval(Container.DataItem,"ID") %>, '<%# DataBinder.Eval(Container.DataItem,"FileName") %>    ')" class='<%# GetClass(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"status"))) %>' <%# GetEnabled(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"status"))) %> />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
    <script type="text/javascript">        
        $('tr.en_2 td.edit').each(function(){
            $(this).html('');
        });
    </script>
</body>
</html>
