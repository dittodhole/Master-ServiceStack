<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HelloWorld.Website.Default" %>
<%@ Import Namespace="ServiceStack.Html" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <%= ServiceStack.MiniProfiler.Profiler.RenderIncludes().AsRaw() %>
<form id="form1" runat="server">
    <div>
        hello
    </div>
</form>
</body>
</html>