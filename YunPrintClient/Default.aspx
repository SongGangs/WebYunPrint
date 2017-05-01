<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    Inherits="_Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>打堆云打印--用户下单界面01</title>
    <link href="Content/bossstyle.css" rel="Stylesheet" type="text/css"/>
    <link href="Content/ThemeBlue.css" rel="Stylesheet" type="text/css" />
    <link href="Content/UserCenterLay.css" rel="Stylesheet" type="text/css"/>
    <script src="Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="Scripts/TopMenu.js" type="text/ecmascript"></script>
    <script type="text/ecmascript">
        //Enumeration for messages status
        MessageStatus = {
            Success: 1,
            Information: 2,
            Warning: 3,
            Error: 4
        }

        //Enumeration for messages status class
        MessageCSS = {
            Success: "Success",
            Information: "Information",
            Warning: "Warning",
            Error: "Error"
        }

        //Global variables
        var intervalID = 0;
        var subintervalID = 0;
        var fileUpload;
        var form;
        var previousClass = '';

        //Attach to the upload click event and grab a reference to the progress bar
        function pageLoad() {
            $addHandler($get('upload'), 'click', onUploadClick);
        }

        //Register the form
        function register(form, fileUpload) {
            this.form = form;
            this.fileUpload = fileUpload;
        }

        //Start upload process
        function onUploadClick() {
            if (fileUpload.value.length > 0) {
                var filename = fileExists();
                if (filename == '') {
                    //Update the message
                    updateMessage(MessageStatus.Information, 'Initializing upload ...', '', '0 of 0 Bytes');
                    //Submit the form containing the fileupload control
                    form.submit();
                    //Set transparancy 20% to the frame and upload button
                    Sys.UI.DomElement.addCssClass($get('dvUploader'), 'StartUpload');
                    //Initialize progressbar
                    setProgress(0);
                    //Start polling to check on the progress ...
                    startProgress();
                    intervalID = window.setInterval(function () {
                        PageMethods.GetUploadStatus(function (result) {
                            if (result) {
                                setProgress(result.percentComplete);
                                //Upadte the message every 500 milisecond
                                updateMessage(MessageStatus.Information, result.message, result.fileName, result.downloadBytes);
                                if (result == 100) {
                                    //clear the interval
                                    window.clearInterval(intervalID);
                                    clearTimeout(subintervalID);
                                }
                            }
                        });
                    }, 500);
                }
                else
                    onComplete(MessageStatus.Error, "File name '<b>" + filename + "'</b> already exists in the list.", '', '0 of 0 Bytes');
            }
            else
                onComplete(MessageStatus.Warning, '你需要选择一个文件.', '', '0 of 0 Bytes');
        }

        //Stop progrss when file was successfully uploaded
        function onComplete(type, msg, filename, downloadBytes) {
            window.clearInterval(intervalID);
            clearTimeout(subintervalID);
            updateMessage(type, msg, filename, downloadBytes);
            if (type == MessageStatus.Success) setProgress(100);
            //Set transparancy 100% to the frame and upload button
            Sys.UI.DomElement.removeCssClass($get('dvUploader'), 'StartUpload');
            //Refresh uploaded files list.

            refreshFileList('<%=UpFilesInfo.ClientID%>');
}

//Update message based on status
function updateMessage(type, message, filename, downloadBytes) {
    var _className = MessageCSS.Error;
    var _messageTemplate = $get('tblMessage');
    var _icon = $get('dvIcon');
    _icon.innerHTML = message;
    $get('dvDownload').innerHTML = downloadBytes;
    $get('dvFileName').innerHTML = filename;
    switch (type) {
        case MessageStatus.Success:
            _className = MessageCSS.Success;
            break;
        case MessageStatus.Information:
            _className = MessageCSS.Information;
            break;
        case MessageStatus.Warning:
            _className = MessageCSS.Warning;
            break;
        default:
            _className = MessageCSS.Error;
            break;
    }
    _icon.className = '';
    _messageTemplate.className = '';
    Sys.UI.DomElement.addCssClass(_icon, _className);
    Sys.UI.DomElement.addCssClass(_messageTemplate, _className);
}

//Refresh uploaded file list when new file was uploaded successfully
function refreshFileList(hiddenFieldID) {
    var hiddenField = $get(hiddenFieldID);
    if (hiddenField) {
        hiddenField.value = (new Date()).getTime();
        __doPostBack(hiddenFieldID, '');
    }
}

//Set progressbar based on completion value
function setProgress(completed) {
    $get('dvProgressPrcent').innerHTML = completed + '%';
    $get('dvProgress').style.width = completed + '%';
}

//Display mouse over and out effect of file upload list
function eventMouseOver(_this) {
    previousClass = _this.className;
    _this.className = 'GridHoverRow';
}
function eventMouseOut(_this) {
    _this.className = previousClass;
}

//This will call every 200 milisecnd and update the progress based on value
function startProgress() {
    var increase = $get('dvProgressPrcent').innerHTML.replace('%', '');
    increase = Number(increase) + 1;
    if (increase <= 100) {
        setProgress(increase);
        subintervalID = setTimeout("startProgress()", 200);
    }
    else {
        window.clearInterval(subintervalID);
        clearTimeout(subintervalID);
    }
}

//This will check whether will was already exist on the server, 
//if file was already exists it will return file name else empty string.
function fileExists() {
    var selectedFile = fileUpload.value.split('\\');
    var file = $get('UpFilesInfo').getElementsByTagName('a');
    for (var f = 0; f < file.length; f++) {
        if (file[f].innerHTML == selectedFile[selectedFile.length - 1]) {
            return file[f].innerHTML;
        }
    }
    return '';
}
    </script>

</head>

<body style="background-color: antiquewhite">
    <form runat="server">
        <%--    <input id="Button1" type="button" value="点击弹出层" onclick="ShowDiv('MyDiv', 'fade')" />--%>
        <div class="area">
            <asp:Panel runat="server" Style="position: absolute; top: 25px; margin: 0 auto; width: 1040px; margin-left: 5px; height: 150px;">
                <img alt="" src="Images/logo1.gif" style="width: 1040px; height: 150px" />
            </asp:Panel>
            <div style="position: absolute; margin-left: 280px; top: 200px; width: 340px;">
                <asp:Label ID="Label4" runat="server" Text="最近登录时间:" Font-Bold="true"></asp:Label>
                <asp:Label ID="TimeTxt" runat="server" Text="2015-09-01" Width="200px" />
            </div>
            <span style="position: absolute; top: 200px; margin-left: 750px; width: 250px">
                <span>
                    <img src="images/user.png" style="height: 14px; width: 14px" />
                    <asp:Label ID="userName1" runat="server" Text="用户名"></asp:Label>
                </span>
                <span>|</span>
                <a href="UserMain.aspx" style="text-decoration: none; color: black" onmouseover="this.style.color = 'red'" onmouseout="this.style.color = 'black'">首页</a><span>|</span>
                <asp:LinkButton runat="server" Text="注销" Style="text-decoration: none; color: black" onmouseover="this.style.color='red'" onmouseout="this.style.color='black'" OnClick="Back_Click"></asp:LinkButton>

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
                        <td style="font-size: 20px">
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
                            <asp:Label runat="server" ID="Major" Font-Size="20px"></asp:Label></td>
                    </tr>
                </table>
            </div>
            <div runat="server" style="position: absolute; top: 405px; margin-left: 5px; width: 250px; height: 325px; background-color: #EEDD99; border: 2px solid sandybrown;">
                <div style="text-align: center; background-color: skyblue; font-size: 30px; font-weight: bold; height: 40px; line-height: 40px">用户操作</div>
                <ul>
                    <li><a href="UserCenter.aspx">个人中心</a></li>
                    <li><a href="UserPassword.aspx">密码管理</a></li>
                    <li><a href="UserHistory.aspx">订单中心</a></li>
                </ul>
            </div>
            <div style="width: 750px; margin-left: 280px; margin-top: 220px; height: 565px; font-size: 14px">
                <div style="width: 600px; margin: 0 auto;">
                    <asp:ScriptManager ID="scriptManager" runat="server" EnablePageMethods="true" />
                    <table width="600px" cellpadding="5" cellspacing="5" border="0">
                        <tr>
                            <td>
                                <table class="ContainerWrapper" border="0" cellpadding="2" cellspacing="0" width="100%">
                                    <tr class="ContainerHeader">
                                        <td>File upload control
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="Container" cellpadding="0" cellspacing="4" width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <div id="dvUploader">
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <iframe id="uploadFrame" frameborder="0" height="30px" width="400" scrolling="no" src="UploadEngine.aspx"></iframe>
                                                                    </td>
                                                                    <td>
                                                                        <input id="upload" type="button" value="上传" style="width: 75px; height: 25px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table id="tblMessage" cellpadding="4" cellspacing="4" class="Information" border="0">
                                                            <tr>
                                                                <td style="text-align: left" colspan="2">
                                                                    <div id="dvIcon" class="Information">
                                                                        请选择一个上传的文件
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="2" width="100%" border="0">
                                                            <tr>
                                                                <td style="width: 100px; text-align: left">进度
                                                                </td>
                                                                <td style="width: auto">
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <div id="dvProgressContainer">
                                                                                    <div id="dvProgress">
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div id="dvProgressPrcent">
                                                                                    0%
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left" class="auto-style1">Download Bytes
                                                                </td>
                                                                <td align="right" class="auto-style1">
                                                                    <div id="dvDownload">
                                                                        Bytes
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: left">文件名
                                                                </td>
                                                                <td align="right">
                                                                    <div id="dvFileName"><%--FileName--%>
                                                                        
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 600px; margin: 0 auto; height: 334px;">
                    <asp:UpdatePanel ID="UpFileOrder" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="UpFilesInfo" OnValueChanged="UpFilesInfo_OnValueChanged" />
                            <asp:DataList ID="NewfilesInfo" runat="server" Width="600px">
                                <FooterTemplate>
                                    <tr>
                                        <td colspan="3" style="height: 40px;">
                                            <div style="float: left; margin-top: 10px; height: 30px; margin-left: 200px">
                                                <asp:Label ID="Label4" runat="server" Text="金额："></asp:Label>
                                                <asp:Label ID="totalPrice" runat="server" Style="font: 25px bold; color: red;"></asp:Label>
                                                <asp:Label runat="server">元</asp:Label>
                                            </div>
                                            <asp:Button ID="Button3" runat="server" Text="提交订单" Style="float: right; margin-right: 10px; margin-top: 5px; width: 75px; height: 30px" OnClick="UpLoadBut_OnClick" />
                                        </td>
                                    </tr>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <table style="width: 600px; font-size: 14px; background-color: #F0F2EB" onmouseover="this.style.backgroundColor='#DFE8F6'" onmouseout="this.style.backgroundColor='#808080'">
                                        <tr>
                                            <tr style="height: 10px; width: 590px; background-color: #F0F2EB">
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Style="color: gray" Text="订单号"></asp:Label>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("orderID") %>'></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Style="color: gray" Text="打印类型"></asp:Label>
                                                    <%--<asp:Label ID="Label4" runat="server" Text='<%#Bind("surePrintType")%>'>'></asp:Label>--%>
                                                    <asp:DropDownList ID="PrintTypes" runat="server" Width="100px" ValidationGroup='<%# ((DataListItem)Container).ItemIndex %>' DataSource='<%#Bind("printTypeTable") %>' DataTextField="PrintName" DataValueField="PrintTypeID" AutoPostBack="True" OnSelectedIndexChanged="PrintTypes_OnSelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Style="color: gray" Text="价格"></asp:Label>
                                                    <asp:Label ID="PicePrice" runat="server" Text='<%# Bind("onePagePrice")%>'></asp:Label>（元/页）
                                                </td>
                                            </tr> 
                                            <tr style="height: 0px; width:0px; background-color: #F0F2EB;display:none">
                                                <td colspan="3">
                                                    <asp:label id="filepath" runat="server" type="hidden"  Text='<%# Bind("filepath") %>' ></asp:label>
                                                </td>
                                            </tr>
                                            <tr style="height: 10px; width: 476px; background-color: #F0F2EB">
                                                <td colspan="3">
                                                    <asp:Label ID="fileName" runat="server" Text='<%# Bind("fileName") %>' align="center"></asp:Label>
                                                   
                                                </td>
                                            </tr>
                                            <tr style="height: 10px; width: 476px; background-color: #F0F2EB">
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Style="color: gray" Text="页数"></asp:Label>
                                                    <asp:Label ID="PageCount" runat="server" Text='<%#Bind("numberOfPages")%>'></asp:Label>（页）
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Style="color: gray" Text="份数"></asp:Label>
                                                    <asp:Button ID="TallyDown" runat="server" Style="margin-left: 15px" Text="-" ValidationGroup='<%# ((DataListItem)Container).ItemIndex %>' OnClick="TallyDown_OnClick" />
                                                    <asp:TextBox ID="CopiesTextBox" runat="server" Width="50px" Text='<%#Bind("count")%>' ValidationGroup='<%# ((DataListItem)Container).ItemIndex %>' AutoPostBack="True" OnTextChanged="CopiesTextBox_OnTextChanged"></asp:TextBox>
                                                    <asp:Button ID="AddOne" runat="server" Text="+" ValidationGroup='<%# ((DataListItem)Container).ItemIndex %>' OnClick="AddOne_OnClick" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Style="color: gray" Text="金额"></asp:Label>
                                                    <asp:Label ID="PriceLable" runat="server" Text='<%#Bind("price")%>'></asp:Label>（元）
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                    <!--添加新行时动态添加文本框-->
                                    <%--  <asp:Literal ID="litAdd" runat="server"></asp:Literal> --%>
                                </ItemTemplate>

                                <SeparatorTemplate>
                                    <tr>
                                        <td colspan="3" style="height: 5px; width: 600px; background-color: black;"></td>
                                    </tr>

                                </SeparatorTemplate>

                            </asp:DataList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="NewfilesInfo" runat="server" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
