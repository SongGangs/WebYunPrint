<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopManagement.aspx.cs" Inherits="YunPrintClient.ShopManagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>打堆云打印--商家店铺管理中心</title>
    <link rel="stylesheet" href="Content/radiusLay.css" type="text/css"/>
    <link rel="stylesheet" href="Content/bossstyle.css" type="text/css"/>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/TopMenu.js" type="text/javascript"></script>

</head>
<body style="background-color:whitesmoke">
    <form runat="server">
        <div style="width: 1050px;height:800px; margin: 0 auto; margin-top: 15px">
            <div style="margin: 0 auto">
                <img src="Images/logo1.gif" style="height: 150px; width: 1050px" />
            </div>
            <div id="menu2" class="menu">
                <ul>
                    <li><asp:linkbutton runat="server" Text="注销"   onclick="Back_Click"></asp:linkbutton>   </li>
                    <li><a href="BossWeb.aspx">首页</a></li>
                </ul>
                <div style="float: left; width: 375px; height: 30px; font-size: 20px">
                    <asp:Label ID="Label1" runat="server" Text="当前  时间:" Style="height: 30px; line-height: 30px; font-weight: bold; color: whitesmoke" />
                    <asp:Label ID="TimeTxt" runat="server" Text="2015-09-01" Style="height: 30px; line-height: 30px; font-weight: bold; width: 200px" />
                </div>
            </div>
            <div style="width:1050px;height:800px;">
                <div style="margin: 0 auto; margin-top: 20px; width: 650px; height: 500px;background-color:whitesmoke">
                    <label style="font-size: 30px; font-weight: bold; height: 45px; width: 525px; color: red">温馨提示:<label style="font-size: 20px;">请务必单击页面下的保存按钮，否则变更不会被生效！</label></label>
                    <table style="margin-top: 10px; margin-left: 100px; text-align: center; height: 375px; width: 500px;">
                        <tr>
                            <td style="font-size: 25px; width: 125px; font-weight: bold">打印类型</td>
                            <td style="font-size: 20px; border-bottom: dashed 1px white; font-weight: bold">打印类型名称</td>
                            <td style="font-size: 20px; border-bottom: dashed 1px white; font-weight: bold">价格<a>(元/页)</a></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:Label ID="BWPrint" runat="server">黑白打印</asp:Label>
                            </td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:TextBox ID="BWPrice" runat="server" Style="width: 100px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:Label ID="ColorPrint" runat="server" Text="彩色打印"></asp:Label>
                            </td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:TextBox ID="ColorPrice" runat="server" Style="width: 100px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 25px; width: 125px; font-weight: bold">配送方式</td>
                            <td style="font-size: 20px; border-bottom: dashed 1px white; font-weight: bold">配送方式名称</td>
                            <td style="font-size: 20px; border-bottom: dashed 1px white; font-weight: bold">价格<a>(元)</a></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:Label ID="SendModeName0" runat="server" Text="送货上门"></asp:Label>
                            </td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:TextBox ID="SendModePrice0" runat="server" Style="width: 100px;"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:Label ID="SendModeName1" runat="server" Text="加急配送"></asp:Label>
                            </td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:TextBox ID="SendModePrice1" runat="server" Style="width: 100px;color: red"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:Label ID="SendModeName2" runat="server" Text="上门自提"></asp:Label>
                            </td>
                            <td style="border-bottom: dashed 1px white">
                                <asp:TextBox ID="SendModePrice2" runat="server" Style="width: 100px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="font-size: 25px; font-weight: bold">联系电话</td>
                            <td colspan="2">
                                <asp:TextBox ID="PhoneNumberTXT" runat="server" Text="Label" Style="font-size: 15px" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 25px; font-weight: bold">商家地址</td>
                            <td colspan="2">
                                <asp:TextBox ID="AddressTXT" runat="server" Text="Label" Style="font-size: 15px" Width="300px"></asp:TextBox>
                            </td>

                        </tr>
                        <tr style="height: 50px">
                            <td colspan="3">
                                <asp:Button runat="server" Text="保存" CssClass="radiusBtn" BorderStyle="None" Style="margin-left: 25px; float: left; width: 100px; height: 25px; font-size: 20px" ID="SaveBtu" OnClientClick="return confirm('你确定要保存吗？')" OnClick="SaveBtu_Click" />
                                <asp:Button runat="server" CssClass="radiusBtn" Text="取消" BorderStyle="None" Style="margin-right: 75px; float: right; width: 100px; height: 25px; font-size: 20px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <input id="MessageTxt" runat="server" type="hidden"/>
    </form>
    <script language="javascript">
        if (Document.all("MessageTxt").value != "") {
            alert(Document.all("MessageTxt").value);
            Document.all("MessageTxt").value = "";
        }
    </script>
</body>
</html>
