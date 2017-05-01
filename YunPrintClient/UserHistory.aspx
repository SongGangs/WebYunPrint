<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserHistory.aspx.cs" Inherits="YunPrintClient.UserHistory" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打堆云打印--用户订单中心</title>
    <link rel="stylesheet" href="Content/radiusLay.css" type="text/css" />
    <link rel="stylesheet" href="Content/UserCenterLay.css" type="text/css" />
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function start() {
            document.getElementById('body1').style.overflowY = 'hidden';
        };
        function stop() {
            document.getElementById('body1').style.overflowY = 'auto';
        };
    </script>

</head>
<body id="body1" style="background-color:antiquewhite">
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
                    <td style ="font-size :20px">
                        <asp:Label runat="server" ID="SexTxt"  Font-Size="20px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 90px; font-weight: bold">学&emsp; 院 :</td>
                    <td>
                        <asp:Label runat="server" ID="College" Font-Size="20px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 90px; font-weight: bold">专&emsp; 业 :</td>
                    <td>
                        <asp:Label runat="server"  ID="Major"  Font-Size="20px"></asp:Label></td>
                </tr>
            </table>
        </div>
        <div runat="server" style="position: absolute; top: 405px; margin-left: 5px; width: 250px; height: 325px;background-color:#EEDD99; border: 2px solid sandybrown;">
            <div style="text-align: center; background-color: skyblue;font-size:30px;font-weight:bold;height:40px;line-height:40px">用户操作</div>
            <ul>
                <li><a href="UserCenter.aspx">个人中心</a></li>
                <li><a href="UserPassword.aspx">密码管理</a></li>
                <li><a href="UserHistory.aspx">订单中心</a></li>
                <li><a href="Default.aspx">去下单</a></li>
            </ul>
        </div>
        <div style="position: absolute; width: 775px;margin-left:270px; margin-top: 220px; font-size: 25px; color: red; text-align: center; z-index: 900">目前暂无订单信息！</div>
        <div style="position: absolute; top: 230px; margin-left: 270px; width: 775px; height: 500px; overflow: auto;z-index:1000" onmouseover="start()" onmouseout="stop()">
            <asp:DataList runat="server" ID="DataList1" Style="background-color: #F0F2EB; margin: 0 auto; width: 770px" OnItemDataBound="DataList1_ItemDataBound">
                <ItemTemplate>
                    <div runat="server" style="font-size: 14px;">
                        <div style="float: left; width: 770px;">
                            <div style="margin-left: 10px; margin-top: 5px; width: 150px; float: left">
                                <a style="color: gray">订单号:</a>
                                <asp:Label ID="OrderNumber" runat="server" Text='<%#Eval("OrderNumber") %>'></asp:Label>
                            </div>

                            <div style="margin-left: 20px; margin-top: 5px; width: 210px; float: left">
                                <a style="color: gray">下单时间:</a>
                                <asp:Label ID="PlaceOrderTime" runat="server" Text='<%#Eval("PlaceOrderTime") %>'></asp:Label>
                            </div>
                            <div style="margin-left: 70px; margin-top: 5px; width: 250px; float: left">
                                <a style="color: gray">手机号:</a>
                                <asp:Label ID="PhoneNumber" runat="server" Text='<%#Eval("PhoneNumber") %>'></asp:Label>
                            </div>
                            <br />


                            <div style="float: left; border-bottom: solid 1px white; width: 300px; margin-top: 5px; margin-left: 10px">
                                <span style="color: gray">文件名</span>
                            </div>
                            <div style="float: left; border-bottom: solid 1px white; width: 125px; margin-top: 5px; margin-left: 20px">
                                <span style="color: gray">打印类型</span>
                            </div>
                            <br />
                            <br />
                            <div style="margin-left: 10px; width: 445px; margin-top: -5px">
                                <asp:DataList ID="DataList2" runat="server">
                                    <ItemTemplate>
                                        <div style="width: 445px;">
                                            <div style="float: left; width: 300px;margin-top:10px">
                                                <label style="margin-top:5px"><%#Eval("DocName") %></label>
                                            </div>
                                            <div style="float: left; width: 125px; margin-left: 18px;margin-top:10px">
                                                <label><%#Eval("PrintName") %></label>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                            <div style="margin-left: 10px; margin-top: 10px; width: 310px; float: left">
                                <a style="color: gray">地址:</a>
                                <asp:Label ID="AddressName" runat="server" Text='<%#Eval("AddressName") %>'></asp:Label>
                            </div>
                            <div style="margin-left: 10px; margin-top: 5px; width: 500px; float: left;">
                                <a style="color: gray">备注信息:</a>
                                <asp:Label ID="DocName" runat="server" Text='<%#Eval("Coment") %>' Style="color: red"></asp:Label>
                            </div>
                            <div style="width: 150px; float: right; margin-right: 10px; margin-bottom: 10px">
                                <a style="color: gray">总金额:</a>
                                <label style="color: red; font-size: 20px"><%#Eval("ToalPrice").ToString().Remove(4) %></label>元
                            </div>
                            <br />
                        </div>
                    </div>
                    </div>
                </ItemTemplate>
                <SeparatorTemplate>
                    <div style="background-color: black; height: 5px;"></div>
                </SeparatorTemplate>
            </asp:DataList>
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

