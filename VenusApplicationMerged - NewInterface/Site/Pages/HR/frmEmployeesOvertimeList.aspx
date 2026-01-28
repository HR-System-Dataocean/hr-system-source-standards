<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesOvertimeList.aspx.vb"
    Inherits="frmEmployeesOvertimeList" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ Attendance Tables</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/Float.js" type="text/javascript"></script>

    <script type="text/javascript">
        // flag to add listener for resize events
        var _onloadFlag = true;
        function adjustHeight() {
            var myHeight = 0;
            if (typeof (window.innerWidth) == 'number') {
                myHeight = window.innerHeight;
            } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                myHeight = document.documentElement.clientHeight;
            } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                myHeight = document.body.clientHeight;
            }
            var tab = igtab_getTabById('UltraWebTab1');
            // <td> which is used as content pane
            var cp = document.getElementById(tab.ID + '_cp');
            // <table> of tab
            var table = tab.element;
            // <div> container of tab
            var container = table.parentNode;
            // height available for tab
            var height = container.clientHeight;
            height = (myHeight - 85);
            if (!height) return;
            // difference between heights of tab and content pane
            var heightShift = tab._myHeightShift;
            // 4 - is adjustment for top/bottom borders of tab
            if (!heightShift)
                heightShift = tab._myHeightShift = (table.offsetHeight - cp.offsetHeight + 4);
            // calculate height for content pane (can be improved for different browsers)
            height -= heightShift;
            if (height < 0) return;
            // set height of content pane to make height of tab to fit with container
            if (table.offsetHeight < (myHeight - 85)) {
                cp.style.height = height + 'px';
            }
            if (!_onloadFlag)
                return;
            _onloadFlag = false;
            // process onresize events
            ig_shared.addEventListener(window, 'resize', adjustHeight);
        }
        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            if (CheckID == false) {
                var page = pageurl;
                var $dialog = $('<div></div>')
                    .html('<iframe style="border: 0px; " src="' + page + '" width="100%" height="100%"></iframe>')
                    .dialog({
                        autoOpen: false,
                        modal: true,
                        height: height,
                        width: width
                    });
                ODialoge = $dialog;
                OSender = SenderCtrl;
                $dialog.dialog('open');
            }
        }
        function CloseIt(retvalue) {
            if (retvalue != "") {
                var Sender = window.document.getElementById(OSender);
                Sender.value = retvalue;
                Sender.focus();
            }
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
    <style type="text/css">
        .style1 {
            height: 23px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployeesExcuses" runat="server">
        <div style="display: none">
            <asp:TextBox ID="lbOverTimeListID" runat="server" Visible="False"></asp:TextBox>
            <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="nameResource1"></asp:Label>
            <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
                TabIndex="-1" Width="99px"></asp:Label>
            <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
                Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
                meta:resourcekey="TargetControlResource1"></asp:Label>
            <igtxt:WebTextEdit meta:resourcekey="WebTextEdit1Recourcekey" ID="WebTextEdit1" runat="server"
                Height="22px" Style="z-index: 1; left: 383px; top: 206px; position: absolute; width: 14%;">
            </igtxt:WebTextEdit>
            <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" DisplayModeFormat="HH:mm"
                EditModeFormat="HH:mm" EnableAppStyling="False" SkinID="WebDateTimeEdit_Default"
                DataMode="Text">
            </igtxt:WebDateTimeEdit>
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <table align="center" style="width: 100%;">
                <tr>
                    <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                        <div runat="server" id="ButtonDiv">
                            <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver;">
                                <tr>
                                    <td style="display: none">
                                        <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                            meta:resourcekey="ImageButton1Resource1" />
                                    </td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                            SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                            OnClientClick="SaveOtherFieldsData();" />
                                    </td>
                                    <td style="width: 120px">
                                        <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                            CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                            OnClientClick="SaveOtherFieldsData();" />
                                        <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" CommandArgument="SaveNew"
                                            meta:resourcekey="LinkButton_SaveNResource1" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
                                    </td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                            CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" />
                                    </td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                            SkinID="HrDelete_Command" OnClientClick="javascript:return confirm('Are you sure?');" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" />
                                    </td>
                                    <td style="width: 40px">
                                        <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                                    </td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                            SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
                                    </td>
                                    <td style="width: 80px">
                                        <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                            SkinID="HrProperties_Command" CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" />
                                        <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" CommandArgument="Property"
                                            meta:resourcekey="LinkButton_PropertiesResource1"></asp:LinkButton>
                                    </td>
                                    <td style="width: 80px">
                                        <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                            SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" />
                                        <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" CommandArgument="Remarks"
                                            meta:resourcekey="LinkButton_RemarksResource1"></asp:LinkButton>
                                    </td>
                                    <td style="width: 40px">
                                        <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                                    </td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                            SkinID="HrLast_Command" CommandArgument="Last" meta:resourcekey="ImageButton_LastResource1" />
                                    </td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                            SkinID="HrNext_Command" CommandArgument="Next" meta:resourcekey="ImageButton_NextResource1" />
                                    </td>
                                    <td style="width: 10px"></td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                            SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" />
                                    </td>
                                    <td style="width: 24px">
                                        <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                            SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" />
                                    </td>
                                    <td style="width: 30%"></td>
                                    <td style="width: 80px"></td>
                                    <td style="width: 5%"></td>
                                </tr>
                            </table>

                            <table style="width: 100%; height: 42px; vertical-align: top">
                                <tr>
                                    <td style="width: 32px; vertical-align: top">
                                        <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                            meta:resourcekey="Image_LogoResource1" />
                                    </td>
                                    <td style="width: 50%; vertical-align: middle">
                                        <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                                    </td>
                                    <td style="width: 50%; vertical-align: middle">
                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                            <tr>
                                                <td style="width: 49%; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                                <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" SkinID="Label_CopyRightsBold"
                                                                    meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                                <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 2%; vertical-align: top"></td>
                                                <td style="width: 49%; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                                <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" SkinID="Label_CopyRightsBold"
                                                                    meta:resourcekey="lblRegUserResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                                <asp:Label ID="lblRegUserValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 49%; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                                <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء" SkinID="Label_CopyRightsBold"
                                                                    meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                            </td>
                                                            <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                                <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal"
                                                                    meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 2%; vertical-align: top"></td>
                                                <td style="width: 49%; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td style="width: 40%; height: 16px; vertical-align: middle;"></td>
                                                            <td style="width: 60%; height: 16px; vertical-align: middle;"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="Details">
                        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                            meta:resourcekey="UltraWebTab1Resource1">
                            <Tabs>
                                <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                    <ContentTemplate>
                                        <table style="width: 100%; vertical-align: top"
                                            cellspacing="0">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td colspan="2"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>

                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                    <%--<tr>
                                                                        <td class="SeparArea"></td>
                                                                        <td class="LabelArea" style="min-width: 90px;">
                                                                            <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                                        </td>
                                                                        <td class="DataAreawithsearch">
                                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                                AutoPostBack="True" meta:resourcekey="txtEmployeeResource1"></asp:TextBox>
                                                                        </td>
                                                                        <td class="search">
                                                                            <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                                                meta:resourcekey="btnEmployeeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                Width="24px">
                                                                                <Alignments TextImage="ImageBottom" />
                                                                                <Appearance>
                                                                                    <Image Url="./Img/forum_search.gif" />
                                                                                </Appearance>
                                                                            </igtxt:WebImageButton>
                                                                        </td>
                                                                    </tr>--%>
                                                                    <tr>
                                                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="SeparArea"></td>
                                                                                    <td class="LabelArea" style="min-width: 90px;">
                                                                                        <asp:Label ID="lblCode" runat="server"  Text="الكود" SkinID="Label_DefaultNormal"
                                                                                            meta:resourcekey="lblCodeResource1"></asp:Label>
                                                                                        <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource1"></asp:Label>
                                                                                    </td>
                                                                                    <td class="DataAreawithsearch">
                                                                                        <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                                            AutoPostBack="True" meta:resourcekey="txtEmployeeResource1"></asp:TextBox>
                                                                                    </td>
                                                                                    <td class="search">
                                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                                                            meta:resourcekey="btnEmployeeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="24px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="SeparArea"></td>
                                                                                    <td class="LabelArea"></td>
                                                                                    <td class="DataArea"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <%--<tr>
                                                                        <td class="SeparArea"></td>
                                                                        <td class="LabelArea">
                                                                            <asp:Label ID="labelDescEnglishName" Width="90px" runat="server" Text="اسم الموظف"
                                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelDescEnglishNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalC"
                                                                                ReadOnly="True" MaxLength="255" meta:resourcekey="lblDescEnglishNameResource1"
                                                                                TabIndex="1"></asp:TextBox>
                                                                        </td>
                                                                    </tr>--%>
                                                                    <tr>
                                                                        <td style="width: 47%; height: 16px; vertical-align: top">
                                                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                                <tr>
                                                                                    <td class="SeparArea"></td>
                                                                                    <td class="LabelArea">
                                                                                        <asp:Label ID="lblEngName" runat="server" Text="التوصيف إنجليزى" Width="90px" SkinID="Label_DefaultNormal"
                                                                                            meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                                                    </td>
                                                                                    <td class="DataArea">
                                                                                        <asp:TextBox ID="txtEngName" runat="server" Width="90px" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                                            ReadOnly="True" meta:resourcekey="txtEngNameResource1" TabIndex="1"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                            </td>
                                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                            <td style="width: 47%; height: 16px; vertical-align: top">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                    <tr>
                                                                        <td class="SeparArea"></td>
                                                                        <td class="LabelArea">
                                                                            <asp:Label ID="lblArbName" runat="server" Text="التوصيف عربى" Width="90px" SkinID="Label_DefaultNormal"
                                                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:TextBox ID="txtArbName" runat="server" Width="90px" SkinID="TextBox_LargeNormalrtl" MaxLength="255"
                                                                                ReadOnly="True" meta:resourcekey="txtArbNameResource1" TabIndex="2"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                    <tr>
                                                                        <td class="SeparArea"></td>
                                                                        <td class="LabelArea">
                                                                            <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                                                <tr>
                                                                                    <td class="SeparArea"></td>
                                                                                    <td class="LabelArea"></td>
                                                                                    <td class="DataArea"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td class="DataArea"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 50%"></td>
                                            </tr>
                                        </table>
                                        </td>
                                            </tr>


                                            <tr>
                                                <td>
                                                    <div runat="server" id="ButtonDiv2">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>

                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                                                    <tr>
                                                                                        <td style="vertical-align: bottom">
                                                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title1Resource2" SkinID="Label_DefaultBold"
                                                                                                Text=""></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">&nbsp;</td>
                                                                                        <td class="DataArea">&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <%--<tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">&nbsp;</td>
                                                                                        <td class="DataArea">&nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>--%>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:Label ID="lblWebDateChooser1" runat="server" meta:resourcekey="lblWebDateChooser1Resource1" SkinID="Label_DefaultNormal" Text="تاريخ البدء" Width="90px"></asp:Label>
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <igsch:WebDateChooser ID="WebDateChooser1" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                                                Width="130px">
                                                                                            </igsch:WebDateChooser>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:Label ID="lblWebDateChooser4" runat="server" meta:resourcekey="lblEndDateResource1" SkinID="Label_DefaultNormal" Text="تاريخ الانتهاء" Width="90px"></asp:Label>
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <igsch:WebDateChooser ID="WebDateChooser2" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                                                Width="130px">
                                                                                            </igsch:WebDateChooser>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top; height: 18px;">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:Label ID="lblWebDateChooser2" runat="server" meta:resourcekey="lblExcuseHoursResource1" SkinID="Label_DefaultNormal" Text="عدد الدقائق" Width="90px"></asp:Label>
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <asp:TextBox ID="DdlExcuseHours" runat="server" meta:resourcekey="ddl_PrantBranchResource1" SkinID="TextBox_SmallBoldC"></asp:TextBox>
                                                                                            &nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top; height: 18px;">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:Label ID="Label3" runat="server" meta:resourcekey="lblShiftResource1" SkinID="Label_DefaultNormal" Text="الوردية" Width="90px"></asp:Label>
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <asp:DropDownList ID="DdlShift" runat="server" meta:resourcekey="ddl_PrantBranchResource1" SkinID="DropDownList_SmalltNormal">
                                                                                                <asp:ListItem meta:resourcekey="lblSelectOptionResource1" Value="0">حدد الخيار</asp:ListItem>
                                                                                                <asp:ListItem meta:resourcekey="lblFirstShiftResource1" Value="1">الأولى</asp:ListItem>
                                                                                                <asp:ListItem meta:resourcekey="lblSecondShiftResource1" Value="2">الثانية</asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                            &nbsp;</td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top; height: 18px;">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">&nbsp;</td>
                                                                                        <td class="DataArea">
                                                                                            <asp:CheckBox ID="ckFingerPrint" runat="server" Text="مع بصمة" meta:resourcekey="ChkFingrPrintResource1" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <table cellspacing="0" style="width: 100%; vertical-align: top; height: 18px;">
                                                                                    <tr>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="LabelArea">
                                                                                            <asp:Label ID="labelDescNationality" runat="server" meta:resourcekey="lblNotesResource1" SkinID="Label_DefaultNormal" Text="ملاحظات" Width="90px"></asp:Label>
                                                                                        </td>
                                                                                        <td class="DataArea">
                                                                                            <asp:TextBox ID="txtNotes" runat="server" meta:resourcekey="txtNotesResource1" SkinID="TextBox_LargeNormalc" TabIndex="2"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="width: 50%"></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                                <tr>
                                                    <td>
                                                        <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                            <tr>
                                                                <td style="vertical-align: bottom">
                                                                    <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_Title2Resource2" SkinID="Label_DefaultBold" Text="الاذونات السابقة"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgEmployeesOvertimeList" runat="server" EnableAppStyling="True" Height="250px" meta:resourcekey="uwgEmployeeVacationsResource1" SkinID="Default" Width="99%">
                                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient" AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect" CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet" HeaderClickActionDefault="SortMulti" Name="uwgEmployeeVacations" RowHeightDefault="15px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="250px" Width="99%">
                                                                </FrameStyle>
                                                                <Pager MinimumPagesForDisplay="2">
                                                                    <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </PagerStyle>
                                                                </Pager>
                                                                <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                                </EditCellStyleDefault>
                                                                <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </FooterStyleDefault>
                                                                <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                </HeaderStyleDefault>
                                                                <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
                                                                    <Padding Left="3px" />
                                                                    <BorderDetails ColorLeft="Window" ColorTop="Window" />
                                                                </RowStyleDefault>
                                                                <GroupByRowStyleDefault BackColor="Control" BorderColor="Window">
                                                                </GroupByRowStyleDefault>
                                                                <GroupByBox Hidden="True">
                                                                    <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                    </BoxStyle>
                                                                </GroupByBox>
                                                                <AddNewBox>
                                                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </BoxStyle>
                                                                </AddNewBox>
                                                                <ActivationObject BorderColor="" BorderWidth="">
                                                                </ActivationObject>
                                                                <FilterOptionsDefault>
                                                                    <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" Width="200px">
                                                                        <Padding Left="2px" />
                                                                    </FilterDropDownStyle>
                                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                        <Padding Left="2px" />
                                                                    </FilterOperandDropDownStyle>
                                                                </FilterOptionsDefault>
                                                            </DisplayLayout>
                                                            <Bands>
                                                                <igtbl:UltraGridBand AllowSorting="Yes">
                                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                                    </AddNewRow>
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True" Key="ID">
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="StartDate" Key="StartDate" Width="20%" DataType="System.DateTime" Format="dd/MM/yyyy" meta:resourceKey="UltraGridColumnResource0">
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Header Caption="StartDate">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="EndDate" Key="EndDate" Width="20%" DataType="System.DateTime" Format="dd/MM/yyyy" meta:resourceKey="UltraGridColumnResource1">
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Header Caption="EndDate">
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="OvertimeHours" Key="OvertimeHours" Width="20%" meta:resourceKey="UltraGridColumnResource2">
                                                                            <Header Caption="OvertimeHours">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Shift" Key="Shift" Width="20%" meta:resourceKey="UltraGridColumnResource3">
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <Header Caption="Shift">
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Notes" Key="Notes" Width="20%" meta:resourceKey="UltraGridColumnResource4">
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Header Caption="OvertimeHours">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Footer>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Header Caption="Shift">
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Footer>
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Header Caption="Notes">
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="FingerPrint" Key="FingerPrint" Type="CheckBox" meta:resourceKey="UltraGridColumnResource5">
                                                                            <Header Caption="WithFinger">
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>
                                                    </td>
                                                </tr>
                                            </tr>

                                        </table>

                                    </ContentTemplate>
                                </igtab:Tab>
                            </Tabs>
                        </igtab:UltraWebTab>
                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
