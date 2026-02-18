<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesEndofService.aspx.vb"
    Inherits="frmEmployeesEndofService" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Employees End of Service</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_EmployeesEndOfService.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
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

        function OpenPrintedScreen(v) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeID&preview=1&ReportCode=EOSSlip&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
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
        function OpenModal12(pageurl, height, width) {
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
            $dialog.dialog('open');
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
        .td1
        {
            border: 1px solid #C0C0C0;
            width: 20%;
            background-color: #DFDFDF;
        }
        
        .td2
        {
            border: 1px solid #C0C0C0;
            width: 20%;
            text-align: center;
            background-color: #EAEAEB;
        }
        .auto-style1 {
            height: 24px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployeesEndofService" runat="server" defaultbutton="ImageButton1">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:Label ID="lblDescworkingDays" runat="server" meta:resourcekey="lblDescworkingDaysResource1"></asp:Label>
        <asp:Label ID="lblVacationAmount" runat="server" meta:resourcekey="lblVacationAmountResource1"></asp:Label>
        <asp:Label ID="lblTotalDeduction2" runat="server" meta:resourcekey="lblTotalDeduction2Resource1"></asp:Label>
        <asp:Label ID="lblTotalDeduction1" runat="server" meta:resourcekey="lblTotalDeduction1Resource1"></asp:Label>
        <asp:Label ID="lblLoansBalance" runat="server" meta:resourcekey="lblLoansBalanceResource1"></asp:Label>
        <asp:Label ID="lblLoansAmount" runat="server" meta:resourcekey="lblLoansAmountResource1"></asp:Label>
        <asp:Label ID="lblBenifitsAmount" runat="server" meta:resourcekey="lblBenifitsAmountResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                    OnClientClick="SaveOtherFieldsData();" Enabled="False" />
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 40px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1"
                                    Enabled="False" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print0" Width="15px" Height="12px" runat="server"
                                    ImageUrl="~/Pages/HR/Img/i.p.edit.gif" Enabled="False" />
                            </td>
                            <td style="width: 80px">
                                &nbsp;
                                <asp:ImageButton ID="ImageButton2" Width="15px" Height="12px" runat="server" ImageUrl="~/Pages/HR/Img/cal_recur.gif"
                                    Enabled="False" />
                            </td>
                            <td style="width: 80px">
                                &nbsp;
                            </td>
                            <td style="width: 40px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Role" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRole_Command" OnClientClick="OpenModal12('CompanyRoles.aspx?FrmID=4',450,844); return false;" />
                                <asp:LinkButton ID="LinkButton_Role" runat="server" Text="الإجراءات" meta:resourcekey="LinkButton_RoleResource1"
                                    OnClientClick="OpenModal12('CompanyRoles.aspx?FrmID=4',450,844); return false;"></asp:LinkButton>
                            </td>
                            <td style="width: 5%">
                            </td>
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
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
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
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
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
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbDescEmployeeCode" runat="server" Text="Employee Code" SkinID="Label_DefaultNormal"
                                                                Width="100px" meta:resourcekey="lbDescEmployeeCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataAreawithsearch">
                                                            <asp:TextBox ID="lblDescEmployeeCode" runat="server" SkinID="TextBox_SmalltNormalC"
                                                                MaxLength="30" AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                        </td>
                                                        <td class="search">
                                                            <igtxt:WebImageButton ID="btnEmployee" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnEmployeeResource1">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="./Img/forum_search.gif" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <asp:Label ID="lblError" runat="server" meta:resourcekey="lblErrorResource1" SkinID="Label_WarningBold"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEnglishName" runat="server" SkinID="Label_DefaultNormal" Text="Employee Name"
                                                                Width="100px" meta:resourcekey="lblEnglishNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblDescEnglishName" runat="server" SkinID="TextBox_LargeNormalC"
                                                                ReadOnly="True" meta:resourcekey="lblDescEnglishNameResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEndOfServiceDate" runat="server" SkinID="Label_DefaultNormal" Text="End of Service Date"
                                                                Width="100px" meta:resourcekey="lblEndOfServiceDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igsch:WebDateChooser ID="wdcEndOfServiceDate" runat="server" BorderColor="#CCCCCC"
                                                                BorderStyle="Solid" BorderWidth="1px" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1"
                                                                NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" Width="100%">
                                                                <AutoPostBack ValueChanged="True" />
                                                            </igsch:WebDateChooser>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td  class="SeparArea">
                                                        </td>
                                                        <td  class="LabelArea">
                                                            <asp:Label ID="lbWorkingDays" runat="server" SkinID="Label_DefaultNormal" Text="Working Days"
                                                                Width="100px" meta:resourcekey="lbWorkingDaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblWorkingDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="lblWorkingDaysResource1"></asp:TextBox>
                                                        </td>

                                                       
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEndofService" runat="server" SkinID="Label_DefaultNormal" Text="End of Service Type"
                                                                Width="100px" meta:resourcekey="lblEndofServiceResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlEndofService" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlEndofServiceResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                     <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lbVactionTotalDays" runat="server" SkinID="Label_DefaultNormal" Text="Vacation total days"
                                                                Width="100px" meta:resourcekey="lbVactionTotalDaysResource1" Height="16px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           
                                                                   <asp:TextBox ID="txtVactionTotalDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="txtVactionTotalDaysResource1"></asp:TextBox>
                                                              
                                                              </td>
                                                        <!--new td-->
                                                               <td class="LabelArea">
                                                            <asp:Label ID="lblVactionExceededDays" runat="server" SkinID="Label_DefaultNormal" Text="exceeded days"
                                                                Width="100px" meta:resourcekey="lblVactionExceededDaysResource1" Height="16px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           
                                                                   <asp:TextBox ID="txtVactionExceededDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="txtVactionExceededDaysResource1"></asp:TextBox>
                                                              
                                                              </td>




                                                    </tr>
                                                    
                                                    
                                                       <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                                          <!--new td-->
                                                               <td class="LabelArea">
                                                            <asp:Label ID="lblVactionNetDays" runat="server" SkinID="Label_DefaultNormal" Text="Net days"
                                                                Width="100px" meta:resourcekey="lblVactionNetDaysResource1" Height="16px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           
                                                                   <asp:TextBox ID="txtVactionNetDays" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="txtVactionNetDaysResource1"></asp:TextBox>
                                                              
                                                              </td>
                                                           
                                                           <td class="LabelArea">
                                                            <asp:Label ID="lbVacationDue" runat="server" SkinID="Label_DefaultNormal" Text="Vacation Due"
                                                                Width="100px" meta:resourcekey="lbVacationDueResource1" Height="16px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           
                                                                   <asp:TextBox ID="lblVacationDue" runat="server" ReadOnly="True" SkinID="TextBox_SmalltNormalC"
                                                                meta:resourcekey="lblVacationDueResource1"></asp:TextBox>
                                                              
                                                              </td>
                                         




                                                    </tr>


                                                     
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                           
                                                        </td>
                                                        <td class="DataArea">
                                                         
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                
                                                <igtxt:WebImageButton ID="BtExcute" runat="server" Height="5px" meta:resourcekey="btnFindRes"
                                                    Overflow="NoWordWrap" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                    color: Black" Text=" تنفيذ " UseBrowserDefaults="False" Width="80px" >
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                       <ButtonStyle Cursor="Hand" BorderStyle="Solid" BorderColor="White"></ButtonStyle>
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                            ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                            WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                         
                                                        </td>
                                                        <td class="DataArea">
                                                              
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%" colspan="3">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 33%">
                                                            <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                border-bottom: 1px solid black">
                                                                <tr>
                                                                    <td style="vertical-align: bottom">
                                                                        <asp:Label ID="Label_Title1" runat="server" meta:resourcekey="Label_Title1Resource1"
                                                                            SkinID="Label_DefaultBold" Text="Benfits Info."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                border-bottom: 1px solid black">
                                                                <tr>
                                                                    <td style="vertical-align: bottom">
                                                                        <asp:Label ID="Label_Title2" runat="server" meta:resourcekey="Label_Title2Resource1"
                                                                            SkinID="Label_DefaultBold" Text="Vacations Info."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                                border-bottom: 1px solid black">
                                                                <tr>
                                                                    <td style="vertical-align: bottom">
                                                                        <asp:Label ID="Label_Title3" runat="server" meta:resourcekey="Label_Title3Resource1"
                                                                            SkinID="Label_DefaultBold" Text="Deductions Info."></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 33%">
                                                            <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgTransactions" runat="server" EnableAppStyling="False"
                                                                Height="180px" SkinID="Default" Width="100%" meta:resourcekey="uwgTransactionsResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="180px"
                                                                        Width="100%">
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                        Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
                                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px" Height="300px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px">
                                                                            <Padding Left="2px" />
                                                                        </FilterOperandDropDownStyle>
                                                                    </FilterOptionsDefault>
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="TransactionTypeID"
                                                                                Key="Transaction Type" Type="DropDownList" Width="70%" meta:resourcekey="UltraGridColumnResource8">
                                                                                <Header Caption="Transaction">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Amount" DataType="System.Double"
                                                                                Format="###,###,###.##" Key="Value" Width="30%" meta:resourcekey="UltraGridColumnResource9">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <Header Caption="Benefit">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="DAmount" DataType="System.Double"
                                                                                Format="###,###,###.##" Hidden="True" Key="Value" Width="0px" meta:resourcekey="UltraGridColumnResource10">
                                                                                <Header Caption="Deduction">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Description" Hidden="True"
                                                                                Key="Description" Width="0px" meta:resourcekey="UltraGridColumnResource11">
                                                                                <Header Caption="Desc">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <igtbl:UltraWebGrid   Browser="UpLevel"  ID="uwgVacationsTransactions" runat="server" EnableAppStyling="False"
                                                                Height="180px" SkinID="Default" Width="100%" meta:resourcekey="uwgVacationsTransactionsResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="180px"
                                                                        Width="100%">
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                        Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
                                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px" Height="300px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px">
                                                                            <Padding Left="2px" />
                                                                        </FilterOperandDropDownStyle>
                                                                    </FilterOptionsDefault>
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand AllowAdd="No" AllowDelete="No" AllowUpdate="No" meta:resourcekey="UltraGridBandResource3">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="TransactionTypeID"
                                                                                Key="Transaction Type" Type="DropDownList" Width="70%" meta:resourcekey="UltraGridColumnResource12">
                                                                                <Header Caption="Transaction">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Amount" DataType="System.Double" Format="###,###,###.##"
                                                                                Key="Value" Width="30%" meta:resourcekey="UltraGridColumnResource13">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <Header Caption="Benefit">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DAmount" DataType="System.Double" Format="###,###,###.##"
                                                                                Hidden="True" Key="Value" Width="0px" meta:resourcekey="UltraGridColumnResource14">
                                                                                <Header Caption="Deduction">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Description" Hidden="True" Key="Description"
                                                                                Width="0px" meta:resourcekey="UltraGridColumnResource15">
                                                                                <Header Caption="Desc">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="Sign" DataType="System.Boolean" Key="Sign"
                                                                                Width="0px" Hidden="True" meta:resourcekey="UltraGridColumnResource16">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgPayabilities" runat="server" EnableAppStyling="False"
                                                                Height="180px" SkinID="Default" Width="100%" meta:resourcekey="uwgPayabilitiesResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="180px"
                                                                        Width="100%">
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                        Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
                                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px" Height="300px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px">
                                                                            <Padding Left="2px" />
                                                                        </FilterOperandDropDownStyle>
                                                                    </FilterOptionsDefault>
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource4">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn AllowGroupBy="No" AllowUpdate="No" BaseColumnName="TransactionTypeID"
                                                                                Key="Transaction Type" Type="DropDownList" Width="70%" meta:resourcekey="UltraGridColumnResource17">
                                                                                <Header Caption="Transaction">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Remain" DataType="System.Double"
                                                                                Format="###,###,###.##" Key="Value" Width="30%" meta:resourcekey="UltraGridColumnResource18">
                                                                                <Header Caption="Remain">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                                <CellStyle HorizontalAlign="Right">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Remain" Format="###,###,###.##"
                                                                                Key="Remain" Width="30%" Hidden="True" meta:resourcekey="UltraGridColumnResource19">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <Header Caption="Amount">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Description" Hidden="True"
                                                                                Key="Description" Width="0px" meta:resourcekey="UltraGridColumnResource11">
                                                                                <Header Caption="Desc">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                    </tr>
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                
                                                

                                                    <tr>
                                                        <td style="width: 33%">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 70px">
                                                                        <asp:Label ID="lblTotal1" runat="server" SkinID="Label_DefaultBold" Text="Total"
                                                                            meta:resourcekey="lblTotal1Resource1"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTotalBenefit1" runat="server" SkinID="Label_DefaultBold" meta:resourcekey="lblTotalBenefit1Resource1"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 70px">
                                                                        <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultBold" Text="Total" meta:resourcekey="Label1Resource1"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblTotalBenefits2" runat="server" SkinID="Label_DefaultBold" meta:resourcekey="lblTotalBenefits2Resource1"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <table style="width: 100%">
                                                                <tr>
                                                                    <td style="width: 70px">
                                                                        <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultBold" Text="Total" meta:resourcekey="Label2Resource1"></asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblRemainAmount" runat="server" SkinID="Label_DefaultBold" meta:resourcekey="lblRemainAmountResource1"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td style="width: 33%">
                                                             &nbsp;<asp:Label ID="Label11" runat="server" 
                                                                            SkinID="Label_DefaultBold" Text="اضافات اخرى"  meta:resourcekey="lblOtherAddsResource1"></asp:Label>
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgExtraBenfits" runat="server" EnableAppStyling="False"
                                                                Height="150px" SkinID="Default" Width="100%" 
                                                                meta:resourcekey="uwgTransactionsResource1" >
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy"  AllowAddNewDefault="Yes">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="150px"
                                                                        Width="100%">
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                        Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
                                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px" Height="300px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px">
                                                                            <Padding Left="2px" />
                                                                        </FilterOperandDropDownStyle>
                                                                    </FilterOptionsDefault>
                                                                     
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2" AllowAdd ="Yes" >
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                          
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Amount" DataType="System.Double"
                                                                                Format="###,###,###.##" Key="Amount" Width="15%" meta:resourcekey="UltraGridColumnResource49">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <Header Caption="Amount">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                            
                                                                            <igtbl:UltraGridColumn AllowUpdate="yes" BaseColumnName="description"
                                                                                Key="description" Width="60px" meta:resourcekey="UltraGridColumnResource50">
                                                                                <Header Caption="Desc">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            
                                                                           
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                                
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            <asp:Label ID="Label12" runat="server" 
                                                                            SkinID="Label_DefaultBold" Text=" خصومات اخرى" meta:resourcekey="lblOtherDeducResource1"></asp:Label>
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgExtraDeduction" runat="server" EnableAppStyling="False"
                                                                Height="150px" SkinID="Default" Width="100%" 
                                                                meta:resourcekey="uwgTransactionsResource1" >
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgOnlyForProfession"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy"  AllowAddNewDefault="Yes">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="150px"
                                                                        Width="100%">
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
                                                                        Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </HeaderStyleDefault>
                                                                    <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                        Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                                        <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                            CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px" Height="300px" Width="200px">
                                                                            <Padding Left="2px" />
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                        </FilterHighlightRowStyle>
                                                                        <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid"
                                                                            BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                            Font-Size="11px">
                                                                            <Padding Left="2px" />
                                                                        </FilterOperandDropDownStyle>
                                                                    </FilterOptionsDefault>
                                                                   
                                                                </DisplayLayout>
                                                                <Bands>
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2" AllowAdd ="Yes" >
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                            </FilterHighlightRowStyle>
                                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                                Font-Size="11px" Width="200px">
                                                                                <Padding Left="2px" />
                                                                            </FilterDropDownStyle>
                                                                        </FilterOptions>
                                                                        <Columns>
                                                                         
                                                                            <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Amount" DataType="System.Double"
                                                                                Format="###,###,###.##" Key="Amount" Width="15%" meta:resourcekey="UltraGridColumnResource49">
                                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                                <Header Caption="Amount">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                                <CellStyle HorizontalAlign="Center">
                                                                                </CellStyle>
                                                                            </igtbl:UltraGridColumn>
                                                                             <igtbl:UltraGridColumn AllowUpdate="yes" BaseColumnName="description" 
                                                                                Key="description" Width="60px" meta:resourcekey="UltraGridColumnResource50">
                                                                                <Header Caption="Desc">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                           
                                                                           
                                                                             
                                                                        </Columns>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                                
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                        <td>
                                                        </td>
                                                        <td style="width: 33%">
                                                            
                                                        </td>
                                                    </tr>
                                                     <td style="width: 10px">
      <asp:Label ID="Label3" runat="server" SkinID="Label_DefaultBold" Text="Total" meta:resourcekey="Label1Resource1"></asp:Label>
                                                         <asp:Label ID="LblTotaExtraBenefits" runat="server" SkinID="Label_DefaultBold" meta:resourcekey="lblTotalBenefits2Resource1"></asp:Label>
  </td>
  <td>
      
  </td>
                                                     
                                                      <td  >
      <asp:Label ID="Label5" runat="server" SkinID="Label_DefaultBold" Width="50" Text="Total" meta:resourcekey="Label1Resource1"></asp:Label>
  
                                                            <asp:Label ID="LblTotalExtraDeductions" runat="server" SkinID="Label_DefaultBold" meta:resourcekey="lblTotalBenefits2Resource1"></asp:Label>

                                                      </td>
  <td>
  </td>
                                                           <td style="width: 33%">
       
                                                             </td>
                                                    <tr>


                                                    </tr>
                                                    

                                                    



                                                    
                                                    


                                                    




                                                    
                                                </table>
                                            </td>
                                        </tr>
                                    


                                        

                                        <tr>
                                            <td style="width: 100%" colspan="3">
                                                <table style="border: 1px solid #BFCDDB; background-color: #FFFFFF; width: 100%;">
                                                    <tr>
                                                        <td class="td1">
                                                        </td>
                                                        <td class="td1" style="text-align: center;">
                                                            <asp:Label ID="lblDays" runat="server" SkinID="Label_DefaultBold" Text="Days" meta:resourcekey="lblDaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="td1" style="text-align: center;">
                                                            <asp:Label ID="lblMonth" runat="server" SkinID="Label_DefaultBold" Text="Month" meta:resourcekey="lblMonthResource1"></asp:Label>
                                                        </td>
                                                        <td class="td1" style="text-align: center;">
                                                            <asp:Label ID="lblYear" runat="server" SkinID="Label_DefaultBold" Text="Year" meta:resourcekey="lblYearResource1"></asp:Label>
                                                        </td>
                                                        <td class="td1" style="text-align: center;">
                                                            <asp:Label ID="lblTotal7" runat="server" SkinID="Label_DefaultBold" Text="Total"
                                                                meta:resourcekey="lblTotal7Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                 <tr>
                                                        <td class="td1">
                                                            <asp:Label ID="lbldesServiceDays" runat="server" SkinID="Label_DefaultBold" Text="AllServicedays" meta:resourcekey="lbldesServiceDaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblservicedays" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lbllblservicedaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblservicemonths" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lbllbltotalmonthsResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblserviceYears" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lblTotalWorkingYearsResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblTotalservicedays" runat="server" SkinID="Label_DefaultBold" Text="0"
                                                                meta:resourcekey="lblDescPaidTotalResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    
                                                       
                                                    <tr>
                                                        <td class="td1">
                                                            <asp:Label ID="lblNotPaid" runat="server" SkinID="Label_DefaultBold" Text="Not Paid"
                                                                meta:resourcekey="lblNotPaidResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblNotPaidDays" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lblNotPaidDaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblNotPaidMonth" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lblNotPaidMonthResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblDescNotPaidYear" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lblDescNotPaidYearResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblNotPaidTotal" runat="server" SkinID="Label_DefaultBold" Text="0"
                                                                meta:resourcekey="lblNotPaidTotalResource1"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="td1">
                                                            <asp:Label ID="lblPaid" runat="server" SkinID="Label_DefaultBold" Text="Paid" meta:resourcekey="lblPaidResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblDescPaidDays" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lblDescPaidDaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblDescPaidMonth" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lblDescPaidMonthResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblDescPaidYear" runat="server" SkinID="Label_DefaultNormal" Text="0"
                                                                meta:resourcekey="lblDescPaidYearResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblDescPaidTotal" runat="server" SkinID="Label_DefaultBold" Text="0"
                                                                meta:resourcekey="lblDescPaidTotalResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td1">
                                                            <asp:Label ID="lblAmount" runat="server" SkinID="Label_DefaultBold" Text="Amount"
                                                                meta:resourcekey="lblAmountResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                        </td>
                                                        <td class="td2">
                                                        </td>
                                                        <td class="td2">
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblDescAmount" runat="server" SkinID="Label_DefaultBold" Text="0"
                                                                meta:resourcekey="lblDescAmountResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="td1">
                                                            <asp:Label ID="lblTotalDue" runat="server" SkinID="Label_DefaultBold" Text="Total Due"
                                                                meta:resourcekey="lblTotalDueResource1"></asp:Label>
                                                        </td>
                                                        <td class="td2">
                                                        </td>
                                                        <td class="td2">
                                                        </td>
                                                        <td class="td2">
                                                            &nbsp;
                                                        </td>
                                                        <td class="td2">
                                                            <asp:Label ID="lblDescTotalDue" runat="server" SkinID="Label_DefaultBold" Text="0"
                                                                meta:resourcekey="lblDescTotalDueResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                            </td>
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

    <script type="text/javascript">


        function getCellvalue(grid, args) {
           

          
            alert(grid);
            var cell = igtbl_getCellById(args);
            alert(cell.getValue());
            
            var cc = document.getElementById("UltraWebTab1__ctl0_lblDescTotalDue").innerHTML;
            alert(cc);
           
            return false;
        }
    </script>
</body>
</html>
