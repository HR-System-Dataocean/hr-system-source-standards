<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOtherVacationsRequestGeneralAdmin.aspx.vb"
    Inherits="frmOtherVacationsRequestGeneralAdmin" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Employee Execuse Request</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>
    <script language="javascript" src="Scripts/App_EmpVacations.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">


      
        function TlbMainToolbar_Click(oToolbar, oButton, oEvent) {
            var tlbControl = igtbar_getToolbarById("TlbMainToolbar");
            if (tlbControl.Items.fromKey("Payments").Selected == true) {
                btnVacationTransactionOn_Click();
            }
        }

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
        var empVacationId;


        function uwgEmployeeVacations_ClickCellButtonHandler(gridName, cellId) {

            var ultraTab = igtab_getTabById("UltraWebTab1");
            var EmployeeID;
            var Row = igtbl_getActiveRow(gridName);
            empVacationId = Row.getCellFromKey("ID").getValue();
            EmployeeID = Row.getCellFromKey("EmployeeID").getValue();


            OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_VacationRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
            //var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeTransactionID&preview=1&ReportCode=VacSlip&sq0=''&v=" + intPaymentTransID, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            //win.focus();
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

        // New method for vacation module
        function OpenModalNew(pageurl, height, width) {
            
            var page = pageurl + "ItemId=" + empVacationId

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
        function OpenModal3(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("lbVactionID");
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

        function OpenPrintedScreen(v) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=RequestSerial&preview=1&ReportCode=CF_101&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
    </script>
    <style type="text/css">
        .igWebDateChooserMainBlue2k7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            text-align: center;
            border: 1px solid #9BB7E0;
        }
        
        .igWebDateChooserMainBlue2k7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            text-align: center;
            border: 1px solid #9BB7E0;
        }
    </style>
</head>
<body style="margin: 0; padding: 0;">
    <form id="frmAnnualVacationsRequest" runat="server" defaultbutton="Button1">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:HiddenField ID="EmpVacationId" runat="server" />
        <asp:HiddenField ID="hdnWorkingHoursPerDay" runat="server" />
        <asp:HiddenField ID="hdnRequiredMonths" runat="server" />
        <asp:HiddenField ID="hdnAnnualVacId" runat="server" />
        <asp:HiddenField ID="hdnDurationDays" runat="server" />
        <asp:HiddenField ID="RemaningOPenBalanceDays" runat="server" Value="0" />
        <asp:HiddenField ID="OpenBalanceId" runat="server" Value="0" />
        <asp:LinkButton ID="LinkButton_OverDueMessage" runat="server" Visible="False" meta:resourcekey="LinkButton_OverDueMessageResource1">LinkButton</asp:LinkButton>
        <asp:TextBox ID="lbVactionID" runat="server" meta:resourcekey="lbVactionIDResource1"></asp:TextBox>
         <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="width: 10px; display: none">
                                <asp:Button ID="Button1" runat="server" OnClientClick="return false;" meta:resourcekey="Button1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                    TabIndex="-1" />
                            </td>
                            <td style="width: 10px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server" 
                                    SkinID="HrDelete_Command" meta:resourcekey="ImageButton_DeleteResource1" CommandArgument="Delete" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="15px" Height="12px" runat="server"
                                    ImageUrl="~/Pages/HR/Img/i.p.edit.gif" meta:resourcekey="ImageButton_PrintResource1"
                                    CommandArgument="Print" />
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP3" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 90px">
                                <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                    SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                    CommandArgument="Property" />
                                <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" meta:resourcekey="LinkButton_PropertiesResource1"
                                    CommandArgument="Property"></asp:LinkButton>
                            </td>
                            <td style="width: 90px">
                                <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                                <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" meta:resourcekey="LinkButton_RemarksResource1"
                                    CommandArgument="Remarks"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                           <%-- <td style="width: 90px">
                                <asp:ImageButton ID="ImageButton_Payments" Width="1px" Height="1px" runat="server"
                                    meta:resourcekey="ImageButton_PaymentsResource1" CommandArgument="Payments" ImageUrl="~/Pages/HR/Img/cal_year.gif" />
                                <asp:LinkButton ID="LinkButton_Payments" runat="server" Text="مستحقات" meta:resourcekey="LinkButton_PaymentsResource1"></asp:LinkButton>
                            </td>--%>
                            <td>
                                <asp:ImageButton ID="textbtn" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command" 
                                  meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" Visible="False" />
                            <asp:LinkButton ID="LinkButton1" runat="server" Visible="False" Text="مستحقات" CommandArgument="Payments2" meta:resourcekey="LinkButton_PaymentsResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Documents" runat="server" CommandArgument="Documents" Visible="False"
                                    meta:resourcekey="ImageButton_DocumentsRec" Height="16px" ImageUrl="./img/abook_add_1.gif"
                                    Width="16px" />
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Role" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRole_Command" OnClientClick="OpenModal1('CompanyRoles.aspx?FrmID=2',450,844,false,''); return false;" meta:resourcekey="ImageButton_RoleResource1" />
                                <asp:LinkButton ID="LinkButton_Role" runat="server" Text="الإجراءات" meta:resourcekey="LinkButton_RoleResource1"
                                    OnClientClick="OpenModal1('CompanyRoles.aspx?FrmID=2',450,844,false,''); return false;"></asp:LinkButton>
                            </td>
                            <td style="width: 50px">
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
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        &nbsp;
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
                            <igtab:Tab Text=" طلب اجازة اخري تشغيلي" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                                   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                     <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblRequestDate" Width="90px" runat="server" Text="تاريخ الطلب"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="lblRequestDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                         <igsch:WebDateChooser ID="ddlRequestDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                                <AutoPostBack ValueChanged="True" />
                                                            </igsch:WebDateChooser>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                      
                                                        
                                                               <td class="spacearea">

                                                        </td>              
                                                        <td class="LabelArea" style="min-width: 90px;">
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                     
                                                 
                                          
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEmployee" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
                                                        
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>  
                                                         <td class="SeparArea">
                                                     </td>
                                                          
                                                       <td style="width: 1px;">
                                                            <asp:TextBox ID="txtRequesterUser" runat="server" Visible="false" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="False" meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
                                                        </td>
                                                            <td class="SeparArea">
                                                     </td>
                                                           <td class="SeparArea">
                                                     </td>
                                                        
<%--                                               الموظف البديل         --%>
                                               <%--                                                          <td class="LabelArea">
                                                            <asp:Label ID="label2" Width="70px" runat="server" Text=" الموظف البديل"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelDescEnglishNameResource1"></asp:Label>
                                                        </td>
                                                       
                                                          <td>
                                                                         <asp:TextBox ID="txtAlternativeUser" Width="130px" runat="server"  MaxLength="15"
                                                                AutoPostBack="True" meta:resourcekey="txtAlternativeResource1">
                                                           </asp:TextBox>
                                                                    </td>
                                                          <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>  --%>
                                               <%--         <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                         
                                                        <td>
                                                         <asp:TextBox ID="TxtAlternativeEmpName" Width="160px" runat="server"  MaxLength="100"
                                                                AutoPostBack="false"  meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
                                                            </td>--%>

                                                    </tr>
                                                </table>
                                            </td>
                                          <%--  <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>--%>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                   <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                       <%-- <td class="DataArea">
                                                              <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 70%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                       
                                                         <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                       <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                       
                                                        <td class="LabelArea">
                                                            <asp:Label ID="labelDescEnglishName" Width="160px" runat="server" Text="اسم الموظف"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelDescEnglishNameResource1"></asp:Label>
                                                        </td>
                                                        
                                                       
                                                        
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lblDescEnglishName" Width="130px" runat="server" SkinID="TextBox_SmalltNormalc"
                                                                ReadOnly="True" MaxLength="255" meta:resourcekey="lblDescEnglishNameResource1"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                
                                                       <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>  <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>  <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                         <td class="LabelArea">
                                                                <asp:Label ID="Label3" runat="server" Width="65px" SkinID="Label_DefaultNormal"
                                                                    Text="رقم الاتصال " meta:resourcekey="lblContactNOResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="txtContactNo" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbConsumeValResource1"
                                                                    TabIndex="6" AutoPostBack="False"></asp:TextBox>
                                                            </td>
                                                       
                                                    </tr>
                                                </table>
                                            </td>
                                          
                                            
                                        </tr> 
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                    border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: top">
                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="LabelRequestDetailsResource2" SkinID="Label_DefaultBold"
                                                                Text="تفاصيل الطلب"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                    
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 50%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblVacationType" runat="server" Width="50px" SkinID="Label_DefaultNormal"
                                                                Text="نوع الاجازة" meta:resourcekey="lblVacationTypeResource1"></asp:Label>
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlVacationType" Width="200px" AutoPostBack="true" runat="server" SkinID="DropDownList_smallNormal"
                                                                meta:resourcekey="DdlVacationTypeResource1" TabIndex="3">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                      <%--  <td class="LabelArea">
                                                            <asp:Label ID="LabelZeroBalAfterVac" runat="server" meta:resourcekey="lblZeroBalAfterVacResource"
                                                                SkinID="Label_DefaultNormal" Text="تصفير رصيد الاجازة عند العودة"></asp:Label>
                                                        </td>--%>
                                                    <%--    <td class="DataArea">
                                                            <asp:CheckBox ID="chk_ZeroingBalance" runat="server" meta:resourcekey="chkZeroBalAfterVacResource" />
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                <table style="width: 47%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                     <%--   <td class="LabelArea">
                                                            <asp:Label ID="labelTotalVal" runat="server" Width="90px" SkinID="Label_DefaultNormal"
                                                                Text="الرصيد الحالي" meta:resourcekey="labelTotalValResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lbTotalVal" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbTotalValResource1"
                                                                TabIndex="4" ReadOnly="True"></asp:TextBox>
                                                            <asp:Label ID="lblTotalDays" runat="server" meta:resourcekey="lblRegDateValueResource1"
                                                                SkinID="Label_CopyRightsNormal"></asp:Label>
                                                        </td>--%>

                                                         <td class="LabelArea">
                                                            <asp:Label ID="lblWebDateChooser1" runat="server" Width="90px" meta:resourcekey="lblStartDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="تاريخ البداية"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igsch:WebDateChooser ID="WebDateChooser1" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                                <AutoPostBack ValueChanged="True" />
                                                            </igsch:WebDateChooser>
                                                        </td>
                                                          <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 47%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                          <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                       
                                                                      <td class="LabelArea">
                                                            <asp:Label ID="lblWebDateChooser2" runat="server" Width="90px" meta:resourcekey="lblEndDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="تاريخ الرجوع"></asp:Label>
                                                        </td>
                                            <td class="DataArea">
                                                            <igsch:WebDateChooser ID="WebDateChooser2" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                                <AutoPostBack ValueChanged="True" />
                                                            </igsch:WebDateChooser>
                                                        </td>




                                                    </tr>
                                                </table>
                                            </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 47%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="labelRemainVal" runat="server" Width="90px" SkinID="Label_DefaultNormal"
                                                                Text="ايام الاجازة" meta:resourcekey="labelVacationDaysResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="lbRemainVal" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbRemainValResource1"
                                                             autopostback="true"     TabIndex  ="5" ReadOnly="false"></asp:TextBox>
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                               



                                                    </tr>
                                                </table>
                                            </td>
                                       
                                                  
                                                
                                            </tr>
                                        <%--Remarks--%>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 50%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblRemarks" Width="85px" runat="server" Text="ملاحظات"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelRemarksResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="TxtRemarks" runat="server" Width="600px" 
                                                                ReadOnly="False" MaxLength="500" meta:resourcekey="lblDescEnglishNameResource1"
                                                                TabIndex="1"></asp:TextBox>
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
                                     </table>
                                                </td>
                                                <td style="width: 6%; height: 16px; vertical-align: top">
                                                </td>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                </td>
                                            </tr>
                                    <tr>
                                                <td style="height: 100%; vertical-align: top" colspan="3">
                                                    <igtbl:UltraWebGrid  Browser="UpLevel" ID="uwgEmployeeVacations" runat="server" EnableAppStyling="False"
                                                        Height="1500px" meta:resourcekey="uwgEmployeeVacationsResource1" SkinID="Default"
                                                        Width="99%">
                                                       
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                            CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                                                            HeaderClickActionDefault="SortMulti" Name="uwgEmployeeVacations" RowHeightDefault="15px"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowRowNumberingDefault="ByDataIsland">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="650px"
                                                                Width="99%">
                                                            </FrameStyle>
                                                           <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                            ClickCellButtonHandler="uwgEmployeeVacations_ClickCellButtonHandler" />
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
                                                            <RowSelectorStyleDefault Font-Size="7pt" Width="40px">
                                                            </RowSelectorStyleDefault>
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
                                                                    Font-Size="11px" Height="300px" Width="100px">
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
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ConfigID" DataType="System.Int32" Hidden="True"
                                                                        Key="ConfigID" meta:resourcekey="UltraGridColumnResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="ConfigID">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>

                                                                    <igtbl:UltraGridColumn BaseColumnName="FormCode" DataType="System.Int32" Hidden="True"
                                                                        Key="FormCode" meta:resourcekey="FormCodeResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="FormCode">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                        Key="ID" meta:resourcekey="IDResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="ID">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RequestSerial" Width="10%" DataType="System.Int32" Hidden="False"
                                                                        Key="RequestSerial" meta:resourcekey="RequestSerialResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="مسلسل الطلب ">
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                      <igtbl:UltraGridColumn BaseColumnName="EmployeeID" DataType="System.String" Hidden="True"
                                                                        Key="EmployeeID" meta:resourcekey="EmployeeIDResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption=" كود الموظف ">
                                                                        </Header>
                                                                           </igtbl:UltraGridColumn>
                                                                      <igtbl:UltraGridColumn BaseColumnName="EmployeeName" Width="40%" DataType="System.String" Hidden="False"
                                                                        Key="EmployeeName" meta:resourcekey="EmployeeNameResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption=" الأسم ">
                                                                        </Header>
                                                                           <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RequestDate" Width="15%" DataType="System.Date"
                                                                        Format="dd/MM/yyyy" Hidden="False" Key="RequestDate" meta:resourcekey="RequestDateResource2">
                                                                        
                                                                        <Header Caption="تاريخ الطلب">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RequestType" Width="15%" DataType="System.string"
                                                                          Hidden="False" Key="RequestType" meta:resourcekey="RequestTypeResource3">
                                                                        
                                                                        <Header Caption="نوع الطلب ">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    
                                                                    <igtbl:UltraGridColumn Type="Button" Width="15%" AllowRowFiltering="False" CellButtonDisplay="Always"
                                                                    meta:resourcekey="RequestLevelsResource16">
                                                                    <Header Caption="المرفقات">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                    <CellButtonStyle BackgroundImage="~/Common/Images/ToolBox/Hr_ToolBox/Print.png" BorderStyle="None"
                                                                        Cursor="Hand" Height="12px" Width="13px">
                                                                    </CellButtonStyle>
                                                                         </igtbl:UltraGridColumn>
                                                                                                                
                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                            
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
                                            </tr>
                                            <tr>
<%--                                                <td style="height: 16px; vertical-align: top" colspan="3">
                                                    <igtbl:UltraWebGrid   ID="uwgEmployeeVacations" runat="server" EnableAppStyling="False"
                                                        Height="150px" meta:resourcekey="uwgEmployeeVacationsResource1" SkinID="Default"
                                                        Width="99%">
                                                       
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                            CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                                                            HeaderClickActionDefault="SortMulti" Name="uwgEmployeeVacations" RowHeightDefault="15px"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowRowNumberingDefault="ByDataIsland">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="150px"
                                                                Width="99%">
                                                            </FrameStyle>
                                                           <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                            ClickCellButtonHandler="uwgEmployeeVacations_ClickCellButtonHandler" />
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
                                                            <RowSelectorStyleDefault Font-Size="7pt" Width="40px">
                                                            </RowSelectorStyleDefault>
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
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="EmployeeName" DataType="System.Int32" Hidden="False"
                                                                        Key="VacationTypeID" meta:resourcekey="UltraGridColumnResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="اسم الموظف">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    
                                                                  

                                                                    <igtbl:UltraGridColumn BaseColumnName="Action" DataType="System.String"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="Action" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="15%">
                                                                        <Header Caption="الاجراء">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>

                                                                     <igtbl:UltraGridColumn BaseColumnName="ActionDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActionDate" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="15%">
                                                                        <Header Caption="التاريخ">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                   

                                                                    
                                                                     <igtbl:UltraGridColumn BaseColumnName="ConfirmedNoOfdays" DataType="System.String"
                                                                        Key="ConfirmedNoOfdays" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="15%">
                                                                        <Header Caption="الايام المعتمدة">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                   
                                                                    
                                                                     <igtbl:UltraGridColumn BaseColumnName="ActionRemarks" DataType="System.String"
                                                                         Key="ActionRemarks" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="15%">
                                                                        <Header Caption="ملاحظات">
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="3" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>

                                                                  
                                                                      
                                                                   
                                                                   

                                                                    

                                                                    
                                                                   

                                                                     <igtbl:UltraGridColumn Type="Button" Width="10%" AllowRowFiltering="False" CellButtonDisplay="Always"
                                                                    meta:resourcekey="UltraGridColumnResource16">
                                                                    <Header Caption="">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                    <CellButtonStyle BackgroundImage="~/Common/Images/ToolBox/Hr_ToolBox/Print.png" BorderStyle="None"
                                                                        Cursor="Hand" Height="12px" Width="13px">
                                                                    </CellButtonStyle>
                                                                         </igtbl:UltraGridColumn>

                                                                   

                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                            <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                                                                <Columns>
                                                                    <igtbl:UltraGridColumn BaseColumnName="VacationTypeID" DataType="System.Int32" Key="VacationTypeID"
                                                                        meta:resourcekey="UltraGridColumnResource1" Type="DropDownList" Width="200px">
                                                                        <Header Caption="Vacation Type">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualStartDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualStartDate" meta:resourcekey="UltraGridColumnResource4"
                                                                        Width="220px">
                                                                        <Header Caption="Start Date">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="ActualEndDate" DataType="System.DateTime"
                                                                        Format="dd/MM/yyyy hh:mm tt" Key="ActualEndDate" meta:resourcekey="UltraGridColumnResource5"
                                                                        Width="220px">
                                                                        <Header Caption="End Date">
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Header>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="2" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                              
                                                                   
                                                                      </Columns>

                                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                                </AddNewRow>
                                                            </igtbl:UltraGridBand>
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>--%>
                                            </tr>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                                &nbsp;&nbsp;
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
    <script type="text/javascript" id="igClientScript">
        $(document).ready(function () {
            var Deletebtn = $("#<%=ImageButton_Delete.ClientID%>")

                Deletebtn.click(function () {
                    if (confirm("هل انت متأكد من الحذف؟") == false)
                 {
                      
                        return false;
                 } 
               
            })
                  

        });
        
    </script>
     
</body>
</html>
