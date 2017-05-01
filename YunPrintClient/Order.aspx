<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="YunPrintClient.Order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>打堆云打印--用户下单页面02</title>
    </head>
<body style="background-color: antiquewhite">
    <form id="form1" runat="server">
        <div style="margin: 0px auto; width: 1050px;">
            <div style="background-image: url('Images/logo1.gif'); height: 150px; width: 1050px;">
            </div>
            <%--<marquee style="font-size:25px;color:red;margin-top:20px" Direction="left" Behaviour="Scroll" scrolldelay="-500" scrollamount="15" loop="-1" onmouseout="this.start()" onmouseover="this.stop()">
                温馨提示：配送方式若为半小时加急送则会额外增加相应费用！
            </marquee>--%>
            <div style="float:left;margin-top: 25px; margin-left: 50px; width: 425px">
                <label style="float:left;font-weight:bold">选择地址：</label>
                <asp:RadioButtonList ID="AddressList" runat="server" style="width:325px"></asp:RadioButtonList>
                <br />
                <asp:Button ID="AddAdress" runat="server" Text="添加地址" BorderStyle="None" OnClick="AddAdress_Click"/>
                <asp:TextBox ID="AdressTex" runat="server" Width="285px" Visible="false" placeholder="地址不能超过三个！"></asp:TextBox>
                <asp:Button ID="SureAdress" runat="server" Text="添加" Visible="false" OnClick="SureAdress_Click"/>
                
            </div>
            <div style="float:left;margin-top: 25px; margin-left: 75px; width:425px">
                <label style="float:left;font-weight:bold">配送方式：</label>
                <asp:RadioButtonList ID="SendMothedList" runat="server" style="width:325px" OnSelectedIndexChanged="SendMothedList_SelectedIndexChanged" AutoPostBack="true" ></asp:RadioButtonList>
            </div>
            <hr style="height:1px; width: 925px;margin-left:50px"/>
            <div style="margin-top: 25px; margin-left: 50px; width: 925px">
                <label style="font-weight:bold">确认订单信息：</label>
                <div style="border: solid; border-width: 2px; margin-top: 10px; width: 925px;">
                    <div style="float: left; width: 225px;">
                        <div style="width: 225px; text-align: center; margin-top: 10px">
                            <label style="font-size: 20px; font-weight: bold">订单号</label>
                            <hr style="background-color: #66ccff; height: 2px" />
                        </div>
                        <div style="width: 225px;text-align: center; margin-top: 5px">
                            <asp:Label ID="OrderNumber" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 225px;margin-left: 5px">
                        <div style="width: 225px; text-align: center; margin-top: 10px">
                            <label style="font-size: 20px; font-weight: bold">文件名</label>
                            <hr style="background-color: #66ccff; height: 2px" />
                        </div>
                        <div style="width: 225px; margin-top: 5px">
                            <asp:Repeater ID="Repeater1" runat="server" >
                                <ItemTemplate>
                                    <div style="height:10px"></div>
                                    <%#Eval("DocName") %>
                                     <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        &nbsp;
                        </div>
                    </div>
                    <div style="float: left; width: 225px;margin-left: 5px">
                        <div style="width: 225px; text-align: center; margin-top: 10px">
                            <label style="font-size: 20px; font-weight: bold">用户名</label>
                            <hr style="background-color: #66ccff; height: 2px" />
                        </div>
                        <div style=" width: 225px; text-align: center; margin-top: 5px">
                            <asp:Label ID="UserName" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                    <div style="float: left; width: 225px;margin-left: 5px">
                        <div style="width: 225px; text-align: center; margin-top: 10px">
                            <label style="font-size: 20px; font-weight: bold">下单时间</label>
                            <hr style="background-color: #66ccff; height: 2px" />
                        </div>
                        <div style=" width: 225px; text-align: center; margin-top: 5px">
                            <asp:Label ID="OrderTime" runat="server" Text="Label"></asp:Label>
                        </div>
                        
                    </div>
                </div>
            </div>
            <hr style="height:1px; width: 925px;margin-left:50px"/>
            <div style="height: 30px; margin-top: 10px; margin-left: 50px">
                <label>备注:</label>
                <asp:TextBox ID="Coments" runat="server" style="width:850px" placeholder="请填写有关订单的一些特殊要求，如双面打印等"></asp:TextBox>
            </div>
            <div style="margin-left: 50px; width: 925px; margin-top: 20px; font-weight: bold">
                <a style="margin-left: 550px; font-size: 20px">金额合计：</a>
                <asp:Label ID="Money" runat="server" Text="50" Style="font-size: 25px; color: red"></asp:Label>
                &nbsp;元（运费：
                <asp:Label ID="AddMoney" runat="server" Text="Label" style="color: red"></asp:Label>元）
            </div>
            <div style="width: 925px;margin-top:20px;margin-left:50px">
                    <asp:Button ID="Back" runat="server" Text="返回下单界面" ForeColor="gray" style="margin-left:600px;color:black;font-weight:bold;cursor:pointer" BorderStyle="None" Font-Size="20px" Height="40px" width="150px" OnClick="Back_Click"/>
                <asp:Button ID="OrderBtu"  runat="server" Text="提交" BackColor="Red" style="margin-left:25px;width:125px;font-weight:bold;color:white;cursor:pointer" BorderStyle="None" Font-Size="20px" Height="40px" OnClick="OrderBtu_Click" />
            </div>
        </div>
    <input id="MessageTxt" type="hidden" runat="server"/> <%--添加隐藏控件--%>
</form>
<script language="javascript">
    if (document.all("MessageTxt").value != "") {
        alert(document.all("MessageTxt").value);
        document.all("MessageTxt").value = ""; //这句可不能掉哟！
    }
</script>
</body>
</html>
