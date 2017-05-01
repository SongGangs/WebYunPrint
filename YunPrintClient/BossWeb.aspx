<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BossWeb.aspx.cs" Inherits="YunPrintClient.BossWeb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>打堆云打印--商家主界面/订单中心</title>
    <link href="Content/radiusLay.css" type="text/css" rel="stylesheet"/>
    <link href="Content/lrtk.css" type="text/css" rel="stylesheet"/>
    <link href="Content/windowLay.css" type="text/css" rel="stylesheet"/>
    <link href="Content/bossstyle.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="Scripts/TopMenu.js"></script> 
    <script type="text/javascript" src="Scripts/js.js"></script>
    <style type="text/css">
        #ul1 li {
            font-size: 25px;
            font-weight: bold;
            text-align: center;
            float: left;
            width: 198px;
            height: 40px;
            line-height: 40px;
            list-style-type: none;
            cursor: pointer;
            background-color: #eef2f3;
            border-right: 1px solid #ccc;
            border-left: 1px solid #ccc;
        }

        #a1 {
            text-decoration: none;
            position: absolute;
            margin-left: 590px;
            margin-top: -6px;
            z-index: 1003;
            background-color: #eef2f3;
            border-radius: 100px;
            -moz-border-radius: 100px;
            -webkit-border-radius: 100px;
        }

            #a1:hover {
                background-color: red;
            }
    </style>
    <script type="text/javascript">
        
        //弹出隐藏层
        function ShowDiv(showDiv, bgDiv) {
            document.getElementById(showDiv).style.display = 'block';
            document.getElementById(bgDiv).style.display = 'block';
            var bgdiv = document.getElementById(bgDiv);
            bgdiv.style.width = document.body.scrollWidth;
            // bgdiv.style.height = $(document).height();
            $("#" + bgDiv).height($(document).height());
            var it = document.getElementById('<%=Timer1.ClientID%>');
            it.enable = 'false';
        };
        //关闭弹出层
        function CloseDiv(showDiv, bgDiv) {
            document.getElementById(showDiv).style.display = 'none';
            document.getElementById(bgDiv).style.display = 'none';
        };

        function xianshi(no1,no2) {
            document.getElementById(no1).style.display = 'block';
            document.getElementById(no2).style.display = 'none';
        }
        function fangshi() {
            if (document.getElementById('list').value == 'OneDay') {
                document.getElementById('severals').style.display = 'none';
                document.getElementById('oneday').style.display = 'block';
                
            }
            else {
                document.getElementById('oneday').style.display = 'none';
                document.getElementById('severals').style.display = 'block';
            }
        }
        function xianshi1() {
            document.getElementById('list1').style.display = 'block';
            //document.getElementById('oneday').style.display = 'block';
        }
        function tab(it1, it2, it3) {
            document.getElementById(it1).style.display = 'block';
            document.getElementById(it2).style.display = 'none';
            document.getElementById(it3).style.display = 'none';

        }

    </script>
</head>
<body style="background-color: whitesmoke">
    <form id="form1" runat="server">
        <div style="width: 1050px; height: 800px; margin: 0 auto; margin-top: 15px">
            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick1" Interval="10000">
                </asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="margin: 0 auto">
                <img src="Images/logo1.gif" style="height: 150px; width: 1050px" />
            </div>
            <div runat="server" id="menu2" class="menu">
                <ul>
                    <li><a href="Login.aspx">注销</a></li>
                    <li><a href="ShopManagement.aspx" runat="server" id="shop">店铺管理</a></li>
                    <li><a runat="server" id="num" onclick="return ShowDiv('white','black')">账号管理</a></li>
                </ul>
                <div style="float: left; width: 375px; height: 30px; font-size: 20px">
                    <asp:Label ID="Label1" runat="server" Text="当前时间:" Style="height: 30px; line-height: 30px; font-weight: bold; color: whitesmoke" />
                    <asp:Label ID="TimeTxt" runat="server" Text="2015-09-01" Style="height: 30px; line-height: 30px; font-weight: bold; width: 200px" />
                </div>
            </div>
            <div style="height: 25px; margin-top: 5px; width: 1050px;">
                <asp:Button ID="Button1" runat="server" OnClick="Back_LinkBtn_Click" Style="float: left; margin-left: 5px; width: 100px" Text="回到今日"></asp:Button>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <input type="button" value="搜索订单" id="Select" style="float: left; width: 100px; margin-left: 10px" runat="server" onclick="if (xianshi1()) return true;" onserverclick="Select_ServerClick" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <div id="list1" style="float: left; height: 20px; width: 70px; display: none;margin-left:5px">
                        <select runat="server" id="list" onchange="fangshi()">
                            <option value="OneDay">一天</option>
                            <option value="QuJian">时间段</option>
                        </select>
                    </div>
                    <div id="severals" style="float: left; display: none; margin-left: 10px; width: 650px; height: 20px">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" style="width: 650px; height: 20px">
                            <ContentTemplate>
                                <asp:TextBox ID="StartTime" runat="server" Width="150px"></asp:TextBox>
                                <button type="button" style="width:75px;cursor:pointer" onclick="xianshi('no1','no2')">选择日期</button>
                                <label style="line-height: 20px; height: 20px; font-size: 20px; width: 40px">--</label>
                                <asp:TextBox ID="EndTime" runat="server" Width="150px"></asp:TextBox>
                                <button type="button" style="width:75px;cursor:pointer" onclick="xianshi('no2', 'no1')">选择日期</button>
                                <asp:Button ID="SelectMores" runat="server" Style="margin-left: 10px; width: 75px" Text="搜索" OnClick="SelectMores_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="oneday" style="float: left; display: none; margin-left: 10px; width: 350px">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" style="height: 20px; width: 350px">
                            <ContentTemplate>
                                <asp:TextBox ID="SelectOneDay" runat="server" Width="150px"></asp:TextBox>
                                <button type="button" style="width:75px;cursor:pointer" onclick="xianshi('no1', 'no2')">选择日期</button>
                                <asp:Button ID="SelectOnes" runat="server" Style="width: 75px; margin-left: 10px" Text="搜索" OnClick="SelectOnes_Click" /></button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div>
                            <div id="no1" style="position: absolute; display: none; z-index: 1001; background-color: white; margin-left: 200px; margin-top: 25px; width: 300px;" onmouseover="this.style.display='block'" onmouseout="this.style.display='none'">
                                <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" Width="300px"></asp:Calendar>
                            </div>
                            <div id="no2" style="position: absolute; display: none; z-index: 1001; background-color: white; margin-left: 425px; margin-top: 25px; width: 300px;" onmouseover="this.style.display='block'" onmouseout="this.style.display='none'">
                                <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged" Width="300px"></asp:Calendar>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div style="position: absolute; width: 1050px; margin-top: 20px; font-size: 25px; color: red; text-align: center; z-index: 901">当前日期暂无订单！</div>
            <div style="position: absolute; margin: 0 auto; width: 1050px; margin-top: 10px;z-index:1000">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DataList runat="server" ID="DataList1" Style="background-color: #F0F2EB; margin: 0 auto" Width="1050px" OnItemDataBound="DataList1_ItemDataBound">
                            <ItemTemplate>
                                <div runat="server" style="font-size: 14px">
                                    <div style="width: 100px; float: left">
                                        <img src="Images/pay.jpg" style="width: 100px" />
                                    </div>
                                    <div style="float: left; width: 850px;">
                                        <div style="margin-left: 10px; margin-top: 5px; width: 150px; float: left">
                                            <a style="color: gray">订单号:</a>
                                            <asp:Label ID="OrderNumber" runat="server" Text='<%#Eval("OrderNumber") %>'></asp:Label>
                                        </div>
                                        <div style="margin-left: 10px; margin-top: 5px; width: 150px; float: left">
                                            <a style="color: gray">用户名:</a>
                                            <asp:Label ID="UserName" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                        </div>
                                        <div style="margin-left: 10px; margin-top: 5px; width: 250px; float: left">
                                            <a style="color: gray">下单时间:</a>
                                            <asp:Label ID="PlaceOrderTime" runat="server" Width="180px" Text='<%#Eval("PlaceOrderTime") %>'></asp:Label>
                                        </div>
                                        <div style="margin-left: 5px; margin-top: 5px; width: 250px; float: left">
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
                                                    <div style="width: 445px">
                                                        <div style="float: left; width: 300px">
                                                            <label><%#Eval("DocName") %></label>
                                                        </div>
                                                        <div style="float: left; width: 125px; margin-left: 18px">
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
                                        <div style="width: 150px; float: right;">
                                            <a style="color: gray">总金额:</a>
                                            <label style="color: red; font-size: 20px"><%#Eval("ToalPrice") %></label>元
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <div style="background-color: black; height: 5px;"></div>
                            </SeparatorTemplate>
                        </asp:DataList>
                        </div>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        <input id="MessageTxt" type="hidden" runat="server"/> <%--添加隐藏控件--%>
       <script language="javascript">
           if (document.all("MessageTxt").value != "") {
               alert(document.all('MessageTxt').value);
               document.all("MessageTxt").value = ""; //这句可不能掉哟！
               window.location.href = 'BusiCenter.aspx';
           }
       </script>
        <div id="black" class="black_overlay"></div>
        <div id="white" style="position: absolute; display: none; z-index: 1002; width: 600px; height: 300px; left: 30%; top: 20%; background-color: white; border: 1px solid whitesmoke">
            <a id="a1" href="#" onclick="CloseDiv('black','white')">×</a>
            <ul id="ul1" style="height: 40px; border-bottom: none">
                <li onclick="tab('addworker','manageworker','updatepwd')">增加员工账号</li>
                <li onclick="tab('manageworker','addworker','updatepwd')">管理员工账号</li>
                <li onclick="tab('updatepwd','manageworker','addworker')">修改密码</li>
            </ul>
            <div id="addworker" style="position: absolute; height: 260px; width: 600px; display: block; border-top: none">
                <table style="margin: 0 auto; width: 400px; margin-top: 10px">
                    <tr>
                        <td>用 户 名:</td>
                        <td>
                            <asp:TextBox ID="StaffName" runat="server" Style="width: 250px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>性&nbsp; 别 ：</td>
                        <td>
                            <asp:RadioButtonList ID="Sex" runat="server" RepeatDirection="Horizontal" Width="255px">
                                <asp:ListItem Value="0">男</asp:ListItem>
                                <asp:ListItem Value="1">女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>电话号码:</td>
                        <td>
                            <asp:TextBox ID="StaffTel" runat="server" TextMode="Phone" Style="width: 250px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>密&nbsp; 码 :</td>
                        <td>
                            <asp:TextBox ID="StaffPass" runat="server" TextMode="Password" Style="width: 250px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>确认密码:</td>
                        <td>
                            <asp:TextBox ID="SureStaffPass" runat="server" TextMode="Password" Style="width: 250px"></asp:TextBox></td>
                    </tr>
                </table>
                <div style="margin: 0 auto; width: 300px; margin-top: 15px;">
                    <asp:Button ID="AddStaff_But" runat="server" CssClass="radiusBtn" Text="确认" Style="width: 125px; height: 30px; font-size: 20px" OnClick="AddStaff_But_OnClick" OnClientClick="javascript:return confirm('你确认要添加吗?')" />
                    <asp:Button ID="Cancel_Button" runat="server" CssClass="radiusBtn" Text="取消" Style="margin-left: 25px; width: 125px; height: 30px; font-size: 20px" OnClick="Cancel_Btn_Click" />
                </div>
            </div>
            <div id="manageworker" style="position: absolute; display: none; border-top: none;margin-top:10px">
                <div style="width: 600px;">
                    <div style="font-size: 25px; font-weight: bold; text-align: center; height: 30px">
                        <div style="float: left; width: 150px; margin: 5px 0px">用户名</div>
                        <div style="float: left; width: 150px; margin: 5px">密码</div>
                        <div style="float: left; width: 150px; margin: 5px 0px">手机号</div>
                    </div>
                </div>
                <div style="font-size: 20px; margin-top: 5px; text-align: center; width: 600px;">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DataList ID="UserList" DataKeyField="PhoneNumber" OnUpdateCommand="UserList_UpdateCommand" OnDeleteCommand="UserList_DeleteCommand" runat="server" Width="600px" OnEditCommand="UserList_EditCommand" OnCancelCommand="UserList_CancelCommand">
                                <ItemTemplate>
                                    <div style="float: left">
                                        <div style="text-align: center; float: left; width: 140px; margin-left: 5px;"><%#Eval("UserName")%></div>
                                        <div style="text-align: center; float: left; width: 145px; margin-left: 5px;"><%#Eval("Password")%></div>
                                        <div style="text-align: center; float: left; width: 145px; margin: 0 5px"><%#Eval("PhoneNumber")%></div>
                                        <div style="text-align: center; float: left">
                                            <asp:LinkButton CommandName="Edit" ID="Edit_But" ForeColor="#003366" runat="server" Style="float: left; margin-left: 15px">修改</asp:LinkButton>
                                            <asp:LinkButton CommandName="Delete" ID="Del_But" OnClientClick="javascript:return confirm('你确认要删除吗?')" ForeColor="#003366" runat="server" Style="float: left; margin-left: 10px">删除</asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div>
                                        <div style="float: left">
                                            <asp:TextBox ID="userText" runat="server" CssClass="inputX"
                                                Text='<%#Eval("UserName")%>' Width="150px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:TextBox ID="pwdText" runat="server" CssClass="inputX"
                                                Text='<%#Eval("Password")%>' Width="150px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:TextBox ID="phoneNumText" runat="server" CssClass="inputX"
                                                Text='<%#Eval("PhoneNumber")%>' Width="150px"></asp:TextBox>
                                        </div>
                                        <div style="float: left">
                                            <asp:LinkButton ID="Update_But" runat="server"
                                                CommandName="Update" ForeColor="#003366" OnClientClick="javascript:return confirm('你确认要更新吗?')">更新</asp:LinkButton>
                                            <asp:LinkButton ID="Cancel_But" runat="server"
                                                CommandName="Cancel" ForeColor="#003366">取消</asp:LinkButton>
                                        </div>
                                    </div>
                                </EditItemTemplate>
                            </asp:DataList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            
            <div id="updatepwd" style="position: absolute; display: none; width: 600px; height: 260px; border-top: none">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <table style="margin: 0 auto; width: 400px; height: 75px; margin-top: 30px">
                            <tr>
                                <td>新 密 码:
                                </td>
                                <td>
                                    <asp:TextBox ID="BusiNewPass" runat="server" TextMode="Password" Width="250px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>确认密码:
                                </td>
                                <td>
                                    <asp:TextBox ID="SurePass" runat="server" TextMode="Password" Width="250px"></asp:TextBox></td>
                            </tr>
                        </table>
                        <div style="margin: 0 auto; width: 300px; margin-top: 10px;">
                            <asp:Button ID="ModifyPass_But" runat="server" CssClass="radiusBtn" Text="确认" Style="width: 125px; height: 30px; font-size: 20px" OnClick="ModifyPass_But_Click" OnClientClick="javascript:return confirm('你确认要修改吗?')" />
                            <asp:Button ID="Cancel_Btn" runat="server" CssClass="radiusBtn" Text="取消" Style="margin-left: 25px; width: 125px; height: 30px; font-size: 20px" OnClick="Cancel_Button_Click" />
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

    </form>
</body>
</html>
