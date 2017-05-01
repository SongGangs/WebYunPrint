<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YunPrintClient.Login" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打堆云打印--登录界面</title>
    <link rel="stylesheet" href="Content/radiusLay.css" type="text/css"/>
    <link rel="stylesheet" href="Content/windowLay.css" type="text/css"/>
    <link rel="stylesheet" href="Content/slideLay.css" type="text/css"/>
    <link rel="stylesheet" href="Content/LoginLay.css" type="text/css"/>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/slider.js" type="text/javascript"></script>
    <script type="text/javascript">
        //弹出隐藏层
        function ShowDiv(showDiv, bgDiv) {
            document.getElementById(showDiv).style.display = 'block';
            document.getElementById(bgDiv).style.display = 'block';
            var bgdiv = document.getElementById(bgDiv);
            bgdiv.style.width = document.body.scrollWidth;
            // bgdiv.style.height = $(document).height();
            $("#" + bgDiv).height($(document).height());
        };
        //关闭弹出层
        function CloseDiv(showDiv, bgDiv) {
            document.getElementById(showDiv).style.display = 'none';
            document.getElementById(bgDiv).style.display = 'none';
        };
        //广告栏滚动
        $(function() {
            var bannerSlider = new Slider($('#banner_tabs'), {
                time: 3000,
                delay: 400,
                event: 'hover',
                auto: true,
                mode: 'fade',
                controller: $('#bannerCtrl'),
                activeControllerCls: 'active'
            });
            //$('#banner_tabs .flex-prev').click(function() {
            //    bannerSlider.prev();
            //});
            //$('#banner_tabs .flex-next').click(function() {
            //    bannerSlider.next();
            //});
        });

    </script>
</head>
<body style="background-color:antiquewhite">
<form runat="server">
      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="area">
        <asp:Panel runat="server" Style="position: absolute; top: 25px;margin: 0 auto; width: 1040px;margin-left: 5px; height: 150px;">
            <img alt="" src="Images/logo1.gif" style="width:1040px;height:150px"/>
        </asp:Panel>
        <asp:Panel runat="server" Style="position: absolute;float: left; top: 190px; margin-left: 5px; width: 250px; height: 200px; border: 2px solid sandybrown;">
            <table style="width: 250px; height: 200px;">
                <tr>
                    <td style="background-color: skyblue; font-weight: bold; font-size: 25px; text-align: center;height:30px" colspan="2">登录</td>
                </tr>
                <tr>
                    <td>用户名:</td>
                    <td class="tr-height">
                        <asp:TextBox ID="UserTextBox" runat="server" Width="150px" placeholder="请输入用户名/手机号" OnTextChanged="UserTextBox_TextChanged"  ></asp:TextBox></td>
                </tr>
                <tr>
                    <td>密 码 :</td>
                    <td class="tr-height">  
                        <asp:TextBox ID="PasswarTextBox" runat="server" Width="150px" TextMode="Password" placeholder="请输入密码"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tr-height" colspan="2">
                        <div style="float:left">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <span style="margin-left: 20px">
                                        <asp:CheckBox ID="AutoLogin" runat="server" Text="记住密码" Width="100px" Font-Size="10" Checked="false" AutoPostBack="true" /></span>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <span style="font-size: 13px"><a href="#" id="ForgetPwd" style="font-size: 13px; margin-left: 30px" onclick="return ShowDiv('MyDiv1', 'fade1')">忘记密码</a></span>
                    </td>
                </tr>
                <tr>
                    <td class="tr-height" colspan="2">
                        <input type="button" class="radiusBtn"  id="RegisterBtn" style="width: 100px; height: 25px; font-size: 18px;margin-left: 10px" runat="server" value="注册" onclick="ShowDiv('MyDiv', 'fade')" />
                        <asp:Button class="radiusBtn" ID="LoginBtn" runat="server" style="width: 100px; height: 25px; font-size: 18px;margin-left: 10px" Text="登陆" OnClick="LoginClick" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" Style="position: absolute; top: 405px; margin-left: 5px; width: 250px; height: 325px; border: 2px solid sandybrown;background-image:url(images/back001.jpg)">
            <div style="background-color:skyblue;text-align:center;height:35px;font-size:25px;font-weight:bold;line-height:35px">联系我们</div>
            <div style="margin-top:10px">
                <a style="font-weight:bold">电话:</a>
                <asp:Label ID="RelPnone" runat="server" Text="Label"></asp:Label>
            </div>
            <div style="margin-top:10px">
                <a style="font-weight:bold">地址:</a>
                <asp:Label ID="RelAdress" runat="server" Text="Label"></asp:Label>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" Style="position: absolute; top: 190px; margin-left: 280px; width: 775px; height: 540px;">
            <asp:Panel runat="server" Style="position: absolute; margin-top: 0px; width: 760px; left: 5px;height: 25px">
                <div style="float: left">欢迎光临打堆云网上打印店！</div>
                <div style="float: right">
                    <asp:Label ID="Label4" runat="server" Text="当前时间:" Font-Bold="true"></asp:Label>
                    <asp:Label ID="TimeTxt" runat="server" Text="2015-09-01" Width="200px" />
                </div>
            </asp:Panel>
            <div id="banner_tabs" class="flexslider" style="margin-top: 25px">
                <ul class="slides">
                    <li>
                        <a title="" href="#">
                            <img alt="" style="width: 750px; height: 250px;" src="images/banner1.jpg" />
                        </a>
                    </li>
                    <li>
                        <a title="" href="#">
                            <img alt="" style="width: 750px; height: 250px;" src="images/banner2.jpg" />
                        </a>
                    </li>
                    <li>
                        <a title="" href="#">
                            <img alt="" style="width: 750px; height: 250px;" src="images/banner3.jpg" />
                        </a>
                    </li>
                </ul>
                <%--                <ul class="flex-direction-nav">
                    <li><a class="flex-prev" href="javascript:;">Previous</a></li>
                    <li><a class="flex-next" href="javascript:;">Next</a></li>
                </ul>--%>
                <ol id="bannerCtrl" class="flex-control-nav flex-control-paging">
                    <li><a>1</a></li>
                    <li><a>2</a></li>
                    <li><a>3</a></li>
                </ol>
            </div>
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
                下单支付
            </asp:Panel>
            <img class="reference3" src="images/refer.jpg" style="width: 50px; height: 25px" />
            <asp:Panel CssClass="guide4" ID="Panel5" runat="server" Width="125px" Height="125px" Font-Size="30px">
                <img src="images/goods.jpg" style="width: 125px; height: 125px" />
                送货到手
            </asp:Panel>
        </asp:Panel>
    </div>
    <!--用户注册界面弹出层-->
    <div id="fade" class="black_overlay"></div>
    <div id="MyDiv" style="width: 500px;height: 350px" class="white_content">
        <div style="height: 50px; background-color: black;">
            <span style="color: red; font-size: 45px;margin-left: 5px">用户注册中心</span>
            <span id="closeBtn" onmouseout="this.style.backgroundColor='black'" onmouseover="this.style.backgroundColor='red'" onclick="CloseDiv('MyDiv','fade')" style="color: white; width: 25px; font-size: 45px; margin-left: 172px;cursor: pointer">×</span>
        </div>
        <div style="margin-top: 20px">
            <table style="height: 250px; width:350px;margin-left:100px">
                <tr>
                    <td style="font-size: 20px;font-weight:  bold">用 户 名:</td>
                    <td>
                        <input type="text" id="UserNameText" runat="server"/></td>
                </tr>
                <tr>
                    <td style="font-size: 20px;font-weight: bold">密&nbsp; 码 :</td>
                    <td>
                        <input type="password" id="password" runat="server"/></td>
                </tr>
                <tr>
                    <td style="font-size: 20px;font-weight: bold">确认密码:</td>
                    <td>
                        <input type="password" id="sure_password" runat="server"/></td>
                </tr>
                <tr>
                    <td style="font-size: 20px;font-weight: bold">手机号码:</td>
                    <td>
                        <input type="text" id="user_PhoneNumber" runat="server"/></td>
                </tr>
                <tr>
                    
                    <td style="font-size: 20px;font-weight: bold">验 证 码:</td>
                    <td>
                      
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div>
                                    <input type="text" id="IdentifyCode" runat="server" style="width: 85px" />
                                    <asp:Button ID="Get_IdentifyCode" runat="server" Text="获取验证码" Style="width: 85px; cursor: pointer; border: none" OnClick="Get_IdentifyCode_Click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:button  runat="server" class="radiusBtn" text="确定" style=" width: 100px; height: 25px;font-size: 20px" OnClick="OK_Btu_Click" />
                        <input type="button" runat="server" class="radiusBtn" value="返回" style="margin-left: 95px; width: 100px; height: 25px;font-size: 20px" onclick="CloseDiv('MyDiv', 'fade')"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <%--忘记密码弹出层--%>
   <div id="fade1" class="black_overlay"></div>
    <div id="MyDiv1" style="width: 500px;height: 350px" class="white_content">
        <div style="height: 50px; background-color: black;">
            <span style="color: red; font-size: 45px;margin-left: 5px">密码重置中心</span>
            <span onmouseout="this.style.backgroundColor='black'" onmouseover="this.style.backgroundColor='red'"  onclick="CloseDiv('MyDiv1','fade1')" style="color: white; width: 25px; font-size: 45px; margin-left: 172px;cursor: pointer">×</span>
        </div>
        <div>
            <table style="height: 250px; width: 400px; margin-left:75px;margin-top: 25px">
                <tr>
                    <td style="font-size: 20px;font-weight: bold">&nbsp;手机号码:</td>
                    <td>
                        <input type="text" id="UserTel" runat="server" /></td>
                </tr>
                <tr>
                    <td style="font-size: 20px;font-weight: bold">&nbsp;验 证 码:</td>
                    <td>
                        
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div>
                                    <input type="text" id="IdentCode" runat="server" style="width: 90px"/>
                                    <asp:Button ID="GetIdentCode" runat="server" Text="获取验证码" Style="width: 85px; cursor: pointer; border: none" OnClick="GetIdentCode_Click"/>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 20px;font-weight: bold">&nbsp;新 密 码:</td>
                    <td>
                        <input type="password" id="UserNewpassword" runat="server" /></td>
                </tr>
                <tr>
                    <td style="font-size: 20px;font-weight: bold">&nbsp;确认密码:</td>
                    <td>
                        <input type="password" id="Sure_Userpassword" runat="server" /></td>
                </tr>

                <tr>
                    <td colspan="2">
                        <asp:button  runat="server" class="radiusBtn" text="确定" style=" margin-left:15px; width: 100px; height: 25px;font-size: 20px" OnClick="OK_Click" />
                        <input type="button" runat="server" class="radiusBtn" value="返回" style="margin-left: 90px; width: 100px; height: 25px;font-size: 20px" onclick="CloseDiv('MyDiv1', 'fade1')" />
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
        document.all("MessageTxt").value = "";//这句可不能掉哟！
    } 
    </script>
</body>
</html>