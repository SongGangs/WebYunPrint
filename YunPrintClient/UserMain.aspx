<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMain.aspx.cs" Inherits="YunPrintClient.UserMain" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打堆云打印--用户界面</title>
    <link rel="stylesheet" href="Content/radiusLay.css" type="text/css" />
    <link rel="stylesheet" href="Content/UserMainLay.css" type="text/css" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
</head>
<body style="background-color:antiquewhite">
<form runat="server">
<%--    <input id="Button1" type="button" value="点击弹出层" onclick="ShowDiv('MyDiv', 'fade')" />--%>
    <div class="area">
        <asp:Panel runat="server" Style="position: absolute; top: 25px;margin: 0 auto; width: 1040px;margin-left: 5px; height: 150px; background: url(Images/logo1.gif)"></asp:Panel>
        <div style="position: absolute; margin-left: 280px; top:200px; width: 340px;">
            <asp:Label ID="Label4" runat="server" Text="当前时间:" Font-Bold="true"></asp:Label>
            <asp:Label ID="TimeTxt" runat="server" Text="2015-09-01" Width="200px" />
        </div>
        <span style="position: absolute; top:200px; margin-left: 800px;width:170px;">
            <span>
                <img src="images/user.png" style="height: 14px; width: 14px" />
                <asp:Label ID="userName1" runat="server" Text="用户名"></asp:Label>
            </span>
            <span>|</span>
            <asp:linkbutton runat="server" Text="注销" style="text-decoration: none; color: black" onmouseover="this.style.color='red'" onmouseout="this.style.color='black'"  onclick="Back_Click"></asp:linkbutton>   
        </span>
        <div style="position: absolute; top: 190px; margin-left: 5px; width: 250px; height: 200px; border: 2px solid sandybrown; background-color:#EEDD99">
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
        <div runat="server" style="position: absolute; top: 405px; margin-left: 5px; width: 250px; height: 325px;background-color:#EEDD99; border: 2px solid sandybrown;">
            <div style="text-align: center; background-color: skyblue;font-size:30px;font-weight:bold;height:40px;line-height:40px">用户操作</div>
            <ul>
                <li><a href="UserCenter.aspx">个人中心</a></li>
                <li><a href="UserPassword.aspx">密码管理</a></li>
                <li><a href="UserHistory.aspx">订单中心</a></li>
            </ul>
        </div>
        <asp:Panel runat="server" Style="position: absolute; top: 235px; margin-left: 270px; width: 775px; height: 500px; background-image: url(Images/back01.jpg)">
            <label style="font-size: 30px; font-weight: bold; height: 60px; color: red; margin-top: 10px">温馨提示:<a style="font-size: 20px;">目前仅支持A4纸，文档仅支持Excel和Word！</a></label>
            <table style="margin-left: 150px; margin-top: 15px">
                <tr style="font-size: 20px; font-weight: bold;">
                    <td><asp:Label ID="Label24" runat="server" Text="黑白打印价格："></asp:Label></td>
                    <td>
                        <asp:Label ID="BWPrint" runat="server"  Font-Size="25px" ForeColor="red"></asp:Label>&nbsp;(元/页)</td>
                </tr>
                <tr style="font-size: 20px; font-weight: bold; height: 40px">
                    <td><asp:Label ID="Label26" runat="server" Text="彩色打印价格："></asp:Label></td>
                    <td>
                        <asp:Label ID="ColorPrint" runat="server"  Font-Size="25px" ForeColor="red"></asp:Label>&nbsp;(元/页)</td>
                </tr>
                <tr style="font-size: 20px; font-weight: bold; height: 40px">
                    <td><asp:Label ID="Label20" runat="server" Text="商家电话号码："></asp:Label></td>
                    <td>
                        <asp:Label ID="BusiTelNumber" runat="server" ></asp:Label>
                        </td>
                </tr>
                <tr style="font-size: 20px; font-weight: bold; height: 40px">
                    <td><asp:Label ID="Label23" runat="server" Text="商 家 地 址："></asp:Label></td>
                    <td>
                        <asp:Label ID="BusiAdress" runat="server" Height="19px" ></asp:Label></td>
                </tr>
                <tr style="height: 60px">
                    <td colspan="2" style="text-align:center">
                        <asp:Button runat="server" CssClass="radiusBtn" Text="去下单" Style="border-style: none; width: 250px; height: 40px; font-size: 30px;" OnClick="Unnamed6_Click"/></td>
                </tr>

            </table>
            <asp:Panel CssClass="guide1" ID="Panel2" runat="server" Width="125px" Height="125px" Font-Size="30px">
                <img src="images/file.jpg" style="width: 125px; height: 125px" />
                上传文件
            </asp:Panel>
            <img class="reference1" src="images/refer.jpg" style="width: 50px; height: 25px" />
            <asp:Panel CssClass="guide2" ID="Panel3" runat="server" Width="125px" Height="125px" Font-Size="30px">
                <img src="images/set.jpg" style="width: 125px; height: 125px" />
                页面设置
            </asp:Panel>
            <img class="reference2" src="images/refer.jpg" style="width: 50px; height: 25px" />
            <asp:Panel CssClass="guide3" ID="Panel4" runat="server" Width="125px" Height="125px" Font-Size="30px">
                <img src="images/pay.jpg" style="width: 125px; height: 125px" />
                支付下单
            </asp:Panel>
            <img class="reference3" src="images/refer.jpg" style="width: 50px; height: 25px" />
            <asp:Panel CssClass="guide4" ID="Panel5" runat="server" Width="125px" Height="125px" Font-Size="30px">
                <img src="images/goods.jpg" style="width: 125px; height: 125px" />
                送货到手
            </asp:Panel>
        </asp:Panel>
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
