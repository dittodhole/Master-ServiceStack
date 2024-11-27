<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelloWorld.Website.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:HyperLink runat="server" NavigateUrl="~/api/requestlogs">
            RequestLogs
        </asp:HyperLink>
        <asp:TextBox runat="server"/>
        <asp:Button runat="server" Text="Send"/>
    </div>
</form>
</body>
</html>