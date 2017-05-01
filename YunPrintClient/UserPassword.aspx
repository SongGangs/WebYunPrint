<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPassword.aspx.cs" Inherits="YunPrintClient.UserPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打堆云打印--用户密码管理中心</title>
    <link rel="stylesheet" href="Content/radiusLay.css" type="text/css" />
    <link rel="stylesheet" href="Content/UserMainLay.css" type="text/css" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
</head>
<body style="background-color:antiquewhite">
<form runat="server">
<%--    <input id="Button1" type="button" value="点击弹出层" onclick="ShowDiv('MyDiv', 'fade')" />--%>
    <div class="area">
        <asp:Panel runat="server" Style="position: absolute; top: 25px;margin: 0 auto; width: 1040px;margin-left: 5px; height: 150px; background-image: url(Images/logo1.gif)"></asp:Panel>
        <div style="position: absolute; margin-left: 280px; top:200px; width: 340px;">
            <asp:Label ID="Label4" runat="server" Text="当前时间:" Font-Bold="true"></asp:Label>
            <asp:Label ID="TimeTxt" runat="server" Text="2015-09-01" Width="200px" />
        </div>
        <span style="position: absolute; top:200px; margin-left: 750px; width: 250px">
            <span>
                <img src="images/user.png" style="height: 14px; width: 14px" />
                <asp:Label ID="userName1" runat="server" Text="用户名"></asp:Label>
            </span>
            <span>|</span>
            <a href="UserMain.aspx" style="text-decoration: none; color: black" onmouseover="this.style.color = 'red'" onmouseout="this.style.color = 'black'">首页</a><span>|</span>
            <asp:linkbutton runat="server" Text="注销" style="text-decoration: none; color: black" onmouseover="this.style.color='red'" onmouseout="this.style.color='black'"  onclick="Back_Click"></asp:linkbutton>       

        </span>
        <div style="position: absolute; top: 190px; margin-left: 5px; width: 250px; height: 200px; border: 2px solid sandybrown; background-color: #EEDD99">
            <table style="width: 250px; height: 200px;">
                <tr>
                    <td style="background-color: skyblue; font-weight: bold; font-size: 30px; text-align: center; height: 30px" colspan="2">用户信息</td>
                </tr>
                <tr>
                    <td style="width: 90px; font-weight: bold">用 户 名 :</td>
                    <td>
                        <asp:Label runat="server" ID="UserName"  Font-Size="20px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 90px; font-weight: bold">性&emsp; 别 :</td>
                    <td>
                        <asp:Label runat="server" ID="SexTxt"  Font-Size="20px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 90px; font-weight: bold"> 学&emsp; 院 :</td>
                    <td>
                        <asp:Label runat="server" ID="College"  Font-Size="20px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 90px; font-weight: bold">专&emsp; 业 :</td>
                    <td>
                        <asp:Label runat="server" ID="Major"  Font-Size="20px"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div runat="server" style="position: absolute; top: 405px; margin-left: 5px; width: 250px; height: 300px;background-color:#EEDD99; border: 2px solid sandybrown;">
            <div style="text-align: center; background-color: skyblue;font-size:30px;font-weight:bold;height:40px;line-height:40px">用户操作</div>
            <ul>
                <li><a href="UserCenter.aspx">个人中心</a></li>
                <li><a href="UserPassword.aspx">密码管理</a></li>
                <li><a href="UserHistory.aspx">订单中心</a></li>
                <li><a href="Default.aspx">去下单</a></li>
            </ul>
        </div>
        <div style="position: absolute; top: 235px; margin-left: 270px; width: 775px; height: 475px;background:url(images/back4.jpg)">
                <table style="width: 400px; margin-top: 50px;margin-left:50px">
                    <tr style="height: 40px">
                        <td style="font-size: 20px;">新 密 码:
                        </td>
                        <td>
                            <asp:TextBox ID="NewPass" runat="server" Font-Size="20px" TextMode="Password" Width="250px" placeholder="请输入新密码！" Height="25px"></asp:TextBox></td>
                    </tr>
                    <tr style="height: 40px">
                        <td style="font-size: 20px;">确认密码:
                        </td>
                        <td>
                            <asp:TextBox ID="SurePass" runat="server" Font-Size="20px" TextMode="Password" placeholder="请再次输入新密码！" Width="250px" Height="25px"></asp:TextBox></td>
                    </tr>
                    <tr style="height: 100px">
                        <td colspan="2">
                            <asp:Button runat="server" BorderStyle="None" CssClass="radiusBtn" OnClientClick="return confirm('你确定要保存吗？')" OnClick="Save_Button_Click" Style="margin-left: 40px; float: left; width: 100px; height: 30px; font-size: 20px" Text="确定" />
                            <asp:Button runat="server" BorderStyle="None" CssClass="radiusBtn" ID="Cancel_Button" Style="margin-right: 90px; float: right; width: 100px; height: 30px; font-size: 20px" Text="取消" OnClick="Cancel_Button_Click" />
                        </td>
                    </tr>
                </table>

        </div>
    </div>
    <input id="MessageTxt" type="hidden" runat="server"/> <%--添加隐藏控件--%>
</form>
<script language=javascript>
    if (document.all("MessageTxt").value != "") {
        alert(document.all("MessageTxt").value);
        document.all("MessageTxt").value = ""; //这句可不能掉哟！
    }
</script>
</body>
</html>
