<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesLoansRewardsInquiries.aspx.vb"
    Inherits="frmEmployeesLoansRewardsInquiries" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~Documents Types</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_frmEmployeeLoans.js" type="text/javascript"></script>
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
        var ODialoge;
        var OSender;

        function OpenModal3(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("hdnID");
            if (CheckID == false || (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")) {
                var page = pageurl + "RId=" + ctrId.value;
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


        function OpenPrintedScreen(v) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeesPayabilitiesID&preview=1&ReportCode=LoanSlip&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
        function btnPrint() {
           
            var hdnEmpTrans = window.document.getElementById("hdnEmpTrans")
            if (parseInt(hdnEmpPayablites.value) > 0) {
                
                OpenPrintedScreen(parseInt(hdnEmpPayablites.value));
            }
        }


        function uwgScheduleTemplet_ClickCellButtonHandler(gridName, cellId) {
       
            var ultraTab = igtab_getTabById("UltraWebTab1");
          
            var Row = igtbl_getActiveRow(gridName);
            var PayablitySettalmentID = Row.getCellFromKey("SettlmentID").getValue();
            var FileName = Row.getCellFromKey("objectFileName").getValue();
            var ObjectID = Row.getCellFromKey("ObjectID").getValue();
            var mode = window.location.search.split('&')[0];
           
            if (PayablitySettalmentID > 0 && FileName != null) {
                
                window.open("../../Uploads/" + ObjectID + "_" + PayablitySettalmentID + "/" + FileName);
              
                
            }
            else{
                //alert("notpaid");
            }
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployeesLoansRewardsInquiries" runat="server">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
         <asp:HiddenField ID="hdnEmpPayablites" runat="server" Value="0" />
        <asp:Label ID="lblCurrentInstalmentvalue" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Times New Roman" Font-Size="Small" Height="20px" Style="z-index: 124;
            left: 23px; position: absolute; top: 294px" Width="172px" meta:resourcekey="lblCurrentInstalmentvalueResource1"></asp:Label>
        <asp:Label ID="lbltotalTransactionvalue" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Times New Roman" Font-Size="Small" Height="20px" Style="z-index: 125;
            left: 198px; position: absolute; top: 294px" Width="175px" meta:resourcekey="lbltotalTransactionvalueResource1"></asp:Label>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource2"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:HiddenField ID="hdnEmpID" runat="server" />
        <asp:HiddenField ID="hdnID" runat="server" />
        <asp:HiddenField ID="hdnPaid" runat="server" />
        <asp:HiddenField ID="hdnSettelment" runat="server" Value="0" />
        <asp:Label ID="lblEmployee" runat="server" Style="position: absolute; left: 114px;
            top: 28px;" Text="Label" Visible="False" meta:resourcekey="lblEmployeeResource1"></asp:Label>
        <igsch:WebDateChooser Format="Short"  ID="txtDueDate" runat="server" BackColor="Silver" BorderColor="White"
            BorderStyle="Solid" Height="0px" Width="0px" EnableTheming="True" 
            meta:resourcekey="WebDateChooser1Resource1" ShowDropDown="True" NullDateLabel="">
            <ExpandEffects Type="Slide" />
            <CalendarLayout  DayNameFormat="FirstLetter" FooterFormat="" ShowFooter="False" ShowTitle="False">
                <SelectedDayStyle BackColor="#0054E3" />
                <DayStyle BackColor="White" Font-Names="Arial" Font-Size="9pt" />
                <OtherMonthDayStyle ForeColor="#ACA899" />
                <DayHeaderStyle BackColor="#7A96DF" ForeColor="White" />
                <TitleStyle BackColor="#9EBEF5" />
                <CalendarStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False">
                </CalendarStyle>
            </CalendarLayout>
        </igsch:WebDateChooser>
        <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" BorderColor="White" BorderStyle="Solid"
            BorderWidth="1px" CellSpacing="1" EditModeFormat="" UseBrowserDefaults="False"
            Width="0px" BackColor="#FFE0C0" Height="0px" meta:resourcekey="WebDateTimeEdit1Resource1"
            Visible="False">
            <ButtonsAppearance CustomButtonDefaultTriangleImages="Arrow" CustomButtonDisabledImageUrl="[ig_edit_01b.gif]"
                CustomButtonImageUrl="[ig_edit_0b.gif]">
                <ButtonStyle BackColor="#C5D5FC" BorderColor="#ABC1F4" BorderStyle="Solid" BorderWidth="1px"
                    Width="13px">
                </ButtonStyle>
                <ButtonDisabledStyle BackColor="#F1F1ED" BorderColor="#E4E4E4">
                </ButtonDisabledStyle>
                <ButtonHoverStyle BackColor="#DCEDFD">
                </ButtonHoverStyle>
                <ButtonPressedStyle BackColor="#83A6F4">
                </ButtonPressedStyle>
            </ButtonsAppearance>
            <SpinButtons DefaultTriangleImages="ArrowSmall" LowerButtonDisabledImageUrl="[ig_edit_21b.gif]"
                LowerButtonImageUrl="[ig_edit_2b.gif]" UpperButtonDisabledImageUrl="[ig_edit_11b.gif]"
                UpperButtonImageUrl="[ig_edit_1b.gif]" Width="15px" />
        </igtxt:WebDateTimeEdit>
        <asp:TextBox ID="txtTransactionDate1" runat="server" BackColor="Silver" BorderColor="White"
            BorderStyle="Solid" BorderWidth="1px" Height="18px" Style="z-index: 114; left: 436px;
            position: absolute; top: 152px" Width="105px" Visible="False" meta:resourcekey="txtTransactionDate1Resource1"></asp:TextBox>
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
                               
                            </td>
                            <td style="width: 120px">
                                
                            </td>
                            <td style="width: 24px">
                                
                            </td>
                            <td style="width: 24px">
                                
                            </td>
                            <td style="width: 20px">
                               
                            </td>
                            <td style="width: 24px">
                                
                            </td>
                            <td style="width: 80px">
                                
                            </td>
                            <td style="width: 80px">
                              
                            </td>
                            <td style="width: 20px">
                                
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                    SkinID="HrLast_Command" CommandArgument="Last" meta:resourcekey="ImageButton_LastResource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                    SkinID="HrNext_Command" CommandArgument="Next" meta:resourcekey="ImageButton_NextResource1" />
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                    SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                    SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" />
                            </td>
                            <td>
                            </td>
                            <td style="width: 80px">
                            
                            </td>
                            <td style="width: 80px">
                                
                            </td>
                            <td style="width: 80px">
                                
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
                                                       
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
<%--                                                        <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>--%>
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
                                    <tr>
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
                                                            <asp:Label ID="lblCode" runat="server" Text="Code" SkinID="Label_DefaultNormal" Width="90px"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataAreawithsearch">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" ReadOnly="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                                        </td>
                                                        <td class="search">
                                                          
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <asp:Label ID="lblError" runat="server" SkinID="Label_WarningBold" meta:resourcekey="lblErrorResource1"></asp:Label>                               
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEnglishName" runat="server" Text="Employee Name" SkinID="Label_DefaultNormal"
                                                                Width="90px" meta:resourcekey="lblEnglishNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblDescEnglishName" runat="server" SkinID="TextBox_LargeNormalc"
                                                                ReadOnly="True" MaxLength="255" meta:resourcekey="lblDescEnglishNameResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblLoanCode" runat="server" SkinID="Label_DefaultNormal" Text="Transaction No."
                                                                Width="90px" meta:resourcekey="lblLoanCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblDescLoanCode" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalc"
                                                                ReadOnly="True" meta:resourcekey="lblDescLoanCodeResource1"></asp:TextBox>
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
            <asp:Label ID="lblTransactionType" runat="server" SkinID="Label_DefaultNormal" Text="Transaction Type"
                Width="100px" meta:resourcekey="lblTransactionTypeResource1"></asp:Label>
        </td>
        <td class="DataArea">
            <asp:DropDownList ID="txtTransactionType" runat="server" Enabled="false" ReadOnly="True" SkinID="DropDownList_LargNormal"
                meta:resourcekey="txtTransactionTypeResource1">
            </asp:DropDownList>
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
                                                            <asp:Label ID="lblDateChooser1" runat="server" meta:resourcekey="lblDateChooser1Resource1"
                                                                SkinID="Label_DefaultNormal" Text="Start Date" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igsch:WebDateChooser ID="txtStartdate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" EnableAppStyling="False" EnableTheming="True" Height="18px"
                                                                meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Value="" Width="100%">
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
        <td class="SeparArea">
        </td>
        <td class="LabelArea">
            &nbsp;<asp:Label ID="lblTransactionAmount" runat="server" meta:resourcekey="lblTransactionAmountResource1"
                SkinID="Label_DefaultNormal" Text="Amount" Width="90px"></asp:Label>
        </td>
        <td class="DataArea">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <asp:TextBox ID="txtTransactionAmount" runat="server" MaxLength="255" meta:resourcekey="txtTransactionAmountResource1"
                            SkinID="TextBox_LargeNormalc"></asp:TextBox>
                    </td>
                    <td style="width: 25px;">
                    </td>
                </tr>
            </table>
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
                     <asp:Label ID="lblTransactionDate" runat="server" SkinID="Label_DefaultNormal" Text="Transaction Date"
                         Width="90px" meta:resourcekey="lblTransactionDateResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <%--<igsch:WebDateChooser ID="txtTransactionDate" runat="server" BorderColor="#CCCCCC"
                         BorderStyle="Solid" BorderWidth="1px" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1"
                         NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                         color: Black; border: solid 1px #CCCCCC" Value="" Width="100%" EnableAppStyling="False">
                     </igsch:WebDateChooser>--%>
                      <asp:TextBox ID="txtTransactionDate" runat="server" SkinID="TextBox_LargeNormalc"
     ReadOnly="True" MaxLength="255" meta:resourcekey="txtBirthDateResource1"></asp:TextBox>
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
             <asp:Label ID="lblRegUserCode" runat="server" Text="Employee Name" SkinID="Label_DefaultNormal"
                 Width="90px" meta:resourcekey="lblRegUserResource1"></asp:Label>
         </td>
         <td class="DataArea">
             <asp:TextBox ID="txtRegUser" runat="server" SkinID="TextBox_LargeNormalc"
                 ReadOnly="True" MaxLength="255" meta:resourcekey="lblRegUserResource1"></asp:TextBox>
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
</body>
</html>
