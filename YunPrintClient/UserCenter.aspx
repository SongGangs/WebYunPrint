<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCenter.aspx.cs" Inherits="YunPrintClient.UserCenter" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打堆云打印--用户个人中心</title>
    <link rel="stylesheet" href="Content/radiusLay.css" type="text/css" />
    <link rel="stylesheet" href="Content/UserCenterLay.css" type="text/css" />
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
            <a href="UserMain.aspx" style="text-decoration: none;color:black" onmouseover="this.style.color='red'" onmouseout="this.style.color='black'">首页</a><span>|</span>
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
                        <asp:Label runat="server" ID="College"  Font-Size="20px"></asp:Label></td>
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
        <asp:Panel runat="server" Style="position: absolute; top: 235px; margin-left: 270px; width: 775px; height: 500px; background-image: url(Images/back01.jpg)">
            <label style="font-size: 30px; font-weight: bold;height: 60px;color:red;margin-top:10px">温馨提示:<a style="font-size: 20px;">请务必单击页面下的保存按钮，否则变更不会被生效！</a></label>
            <table style="margin-left: 150px; width: 400px; margin-top: 20px; height: 400px">                
                <tr style="height: 30px">
                    <td style="font-weight: bold; width: 100px">性 别&nbsp; :</td>
                    <td>
                        <asp:RadioButtonList ID="Sex"  runat="server" RepeatDirection="Horizontal" Width="255px" >
                            <asp:ListItem Value="0" >男</asp:ListItem>
                            <asp:ListItem Value="1">女</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="font-weight: bold; width: 100px">电话号码:
                    </td>
                    <td>
                        <asp:TextBox ID="UserTelNumber" runat="server" Width="250px" placeholder="请确保电话号码输入无误"></asp:TextBox></td>
                </tr>
                <tr style="height: 30px">
                    <td style="font-weight: bold; width: 100px">验证码&nbsp;:</td>
                    <td>
                                <asp:TextBox ID="IdentifyCode" runat="server" Width="135px" placeholder="请确保验证码输入无误"></asp:TextBox>
                                <asp:Button ID="Get_IdentifyCode" runat="server" Text="获取验证码" Style="width: 85px; margin-left: 30px; cursor: pointer; border: none" OnClick="Get_IdentifyCode_Click"/>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="font-weight: bold; width: 100px">学 院&nbsp; :</td>
                    <td>
                        <asp:TextBox ID="UserCollge" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="font-weight: bold; width: 100px">专 业&nbsp; :</td>
                    <td>
                        <asp:TextBox ID="UserMajor" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="font-weight: bold; width: 100px">电子邮箱:</td>
                    <td>
                        <asp:TextBox ID="UserQQMail" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold; width: 100px">地 址&nbsp; :</td>
                    <td>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                            <ItemTemplate>
                                <%#Eval("AddressName") %>
                                <asp:LinkButton ID="lkbtnDel" runat="server" CommandArgument='<%#Eval("AddressName")%>' CommandName="del" OnClientClick="javascript:return confirm('你确认要删除吗?')" Style="float: right">删除</asp:LinkButton>
                                <br />
                                <hr />
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="UserAdressAdd" runat="server" placeholder="请输入详细地址信息！" Width="200px"></asp:TextBox>
                                <asp:LinkButton ID="UserAdrAdd" runat="server" CommandArgument='<%# Eval("AddressName") %>' CommandName="insert" OnClientClick="return confirm('你真的要插入该条记录吗？')" Style="float: right">增添</asp:LinkButton>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr style="height: 35px">
                    <td colspan="2">
                        <asp:Button runat="server" BorderStyle="None" CssClass="radiusBtn" OnClick="Save_Button_Click" Style="margin-left: 25px;float:left; width: 100px; height: 25px; font-size: 20px" Text="保存" />
                        <asp:Button runat="server" BorderStyle="None" CssClass="radiusBtn" ID="Cancel_Button" Style="margin-right:75px;float:right; width: 100px; height: 25px; font-size: 20px" Text="取消" OnClick="Cancel_Button_Click" />
                    </td>
                </tr>
            </table>
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
