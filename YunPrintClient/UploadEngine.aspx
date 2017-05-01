<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="ReadOnly" Async="true" Inherits="YunPrintClient.UploadEngine" Codebehind="UploadEngine.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/ThemeBlue.css" rel="Stylesheet" type="text/css" />
</head>
<body>
    <form id="form" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="scriptManager" runat="server"/>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            //Register the form and upload elements
            window.parent.register(
                $get('<%= this.form.ClientID %>'),
                $get('<%= this.fileUpload.ClientID %>')
            );
        }
    </script>
    <asp:FileUpload ID="fileUpload" runat="server" style="height:20px"/>
    </form>
</body>
</html>