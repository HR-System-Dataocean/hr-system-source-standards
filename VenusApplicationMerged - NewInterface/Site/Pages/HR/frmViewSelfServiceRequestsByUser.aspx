<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmViewSelfServiceRequestsByUser.aspx.vb"
    Inherits="frmEmployeesVacations" Culture="auto" meta:resourcekey="PageResource1"
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
    <title>* Venus Payroll * ~View SelfService Requests</title>
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
            //var cp = document.getElementById(tab.ID + '_cp');
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
            debugger
            var cell = igtbl_getCellById(cellId);
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var Row = igtbl_getActiveRow(gridName);
            var mode = window.location.search.split('&')[0];
            var RequestSerial = Row.getCellFromKey("ID").getValue();
            var FormCode = Row.getCellFromKey("FormCode").getValue();
            var Type = 2;
            var CanBeCanceled = false;

            if (cell.Column.Index == 8) {
                if (FormCode == "SS_0011" || FormCode == "SS_0012" || FormCode == "SS_0013" || FormCode == "SS_0018" || FormCode == "SS_0030" || FormCode == "SS_0031" || FormCode == "SS_0032" || FormCode == "SS_0033" || FormCode == "SS_0034" || FormCode == 'SS_0035' || FormCode == 'SS_0036' || FormCode == 'SS_0037') {

                    OpenModal1("frmAnnualVacationsRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");
                }
                else if (FormCode == "SS_0014") {
                    OpenModal1("frmExecuseRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_0015" || FormCode == "SS_0019") {
                    OpenModal1("frmEndServiceRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00191") {
                    OpenModal1("frmExitEntryRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00192") {
                    OpenModal1("frmVisaRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00193") {
                    OpenModal1("FrmLoanLetterRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00194") {
                    OpenModal1("FrmOtherLetterRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00195") {
                    OpenModal1("FrmTrainingRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00196") {
                    OpenModal1("FrmGrievanceFormRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00197") {
                    OpenModal1("FrmInterviewEvaluationFormRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00198") {
                    OpenModal1("FrmAssaultEscalationFormRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_00199") {
                    OpenModal1("FrmConflictofInterestFormRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001910") {
                    OpenModal1("FrmPhysiciansPrivilegingFormRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001911") {
                    OpenModal1("FrmDaycareSupportReaquestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001912") {
                    OpenModal1("FrmEducationSupportRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001913") {
                    OpenModal1("FrmAdvanceHousingRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001914") {
                    OpenModal1("frmAdvanceSalaryRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001915") {
                    OpenModal1("FrmChamberofCommerceLetterRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001916") {
                    OpenModal1("FrmSCFHSLetterRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001917") {
                    OpenModal1("FrmPaySlipRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001918") {
                    OpenModal1("frmOccurrenceVarianceReportingLettersStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001919") {
                    OpenModal1("frmOvertimeRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001920") {
                    OpenModal1("frmEducationFeesCompensationApplicationStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001921") {
                    OpenModal1("frmBankAccountUpdateStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }

                else if (FormCode == "SS_001922") {
                    OpenModal1("frmContactInformationUpdateStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001923") {
                    OpenModal1("frmDependentsInformationUpdateStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001924") {
                    OpenModal1("frmMedicalInsuranceAdjustmentsStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001925") {
                    OpenModal1("frmOtherLegalDocumentUpdatesStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001926") {
                    OpenModal1("frmEmployeeFileUpdateStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001927") {
                    OpenModal1("frmBusinessORTrainingTravelStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001928") {
                    OpenModal1("frmAnnualTicketRelatedRequestsStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
                else if (FormCode == "SS_001929") {
                    OpenModal1("frmChangeWorkHoursRequestStatus.aspx?RequestSerial=" + RequestSerial + "&Type=" + Type + "&FormCode=" + FormCode + "&CanBeCanceled=" + CanBeCanceled, 560, 800, false, "");

                }
            }
            else {
                var ultraTab = igtab_getTabById("UltraWebTab1");
                var EmployeeID;
                var Row = igtbl_getActiveRow(gridName);
                empVacationId = Row.getCellFromKey("ID").getValue();
                EmployeeID = Row.getCellFromKey("EmployeeID").getValue();
                //OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_VacationRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                if (FormCode == "SS_0014") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_ExecuseRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_0015") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_EndOfServiceRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00191") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_ExitEntryRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00192") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_VisaRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00193") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_LoanLetterRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00194") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_OtherLetterRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00195") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_TrainingRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00196") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_GrievanceFormRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00197") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_InterviewEvaluationFormRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00198") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_AssaultEscalationFormRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_00199") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_ConflictofInterestFormRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001910") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_PhysiciansPrivilegingFormRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001911") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_DaycareSupportReaquest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001912") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_EducationSupportRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001913") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_AdvanceHousingRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001914") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_AdvanceSalaryRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001915") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_ChamberofCommerceLetterRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001916") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_SCFHSLetterRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001917") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_PaySlipRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001918") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_OccurrenceVarianceReportingLetters&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001919") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_OvertimeRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001920") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_EducationFeesCompensationApplication&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001921") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_BankAccountUpdate&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001922") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_ContactInformationUpdate&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001923") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_DependentsInformationUpdate&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001924") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_MedicalInsuranceAdjustments&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001925") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_OtherLegalDocumentUpdates&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001926") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_EmployeeFileUpdate&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001927") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_BusinessORTrainingTravel&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001928") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_AnnualTicketRelatedRequests&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else if (FormCode == "SS_001929") {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_ChangeWorkHoursRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }
                else {
                    OpenModalNew('FrmSelfServiceDocuments.aspx?TB=SS_VacationRequest&EmployeeID=' + EmployeeID + '&SV=' + empVacationId + '&', 495, 800)
                }


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
            debugger
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
                    <table style="width: 45%; height: 42px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource1" />
                            </td>
                               <td class="SeparArea">
                                   </td>
                               <td class="SeparArea">
                                                        </td>
                                                        <td  style="width:20px" class="LabelArea">
                                                            <asp:Label ID="lblRequestType" runat="server" Width="120px" SkinID="Label_LargtNormal"
                                                                Text="نوع الطلب" meta:resourcekey="lblRequestTypeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlRequestType" runat="server" AutoPostBack="True" SkinID="DropDownList_DefaultNormal"
                                                                meta:resourcekey="RequestTypeResource1" TabIndex="3">
                                                            </asp:DropDownList>
                                                        </td>
                                    <td  style="width:10px" class="LabelArea">
                                                            <asp:Label ID="Label1" runat="server" Width="50px" SkinID="Label_LargtNormal"
                                                                Text="من تاريخ" meta:resourcekey="lblYearResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           <igsch:WebDateChooser ID="ddlFromDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
       BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
       font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
       Width="130px">
       <AutoPostBack ValueChanged="True" />
   </igsch:WebDateChooser>
                                                        </td>
                                <td  style="width:20px" class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" Width="50px" SkinID="Label_LargtNormal"
                                                                Text="الي تاريخ" meta:resourcekey="lblMonthResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                        <igsch:WebDateChooser ID="ddlToDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
       BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
       font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
       Width="130px">
       <AutoPostBack ValueChanged="True" />
   </igsch:WebDateChooser>
                                                        </td>
                            <%--حالة الطلب--%>
                              <td  style="width:20px" class="LabelArea">
                              <asp:Label ID="lblStatus" runat="server" Width="50px" SkinID="Label_LargtNormal"
                                  Text="حالة الطلب" meta:resourcekey="lblStatusResource1"></asp:Label>
                          </td>
                          <td class="DataArea">
                              <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" SkinID="DropDownList_DefaultNormal"
                                  meta:resourcekey="RequestStatusResource1" TabIndex="3">
                              </asp:DropDownList>
                          </td>


                            <%--كود الموظف--%>



                                                           <td class="LabelArea">
                                                                  <asp:Label ID="lblEmpCode" runat="server" Width="50px" meta:resourcekey="lblEmpCodeResource1"
                                                                    SkinID="Label_DefaultNormal" Text="كود الموظف " style="margin-right: 10px;"></asp:Label>
                                                                 <%--   <asp:TextBox ID="txtEmpCode" Width="50px" runat="server"  MaxLength="15"
                                                                 AutoPostBack="True" meta:resourcekey="txtEmpCodeResource1">
                                                                 </asp:TextBox>--%>
                                                            </td>
                                                               <td>
                  <asp:TextBox ID="txtEmpCode" Width="50px" runat="server"  MaxLength="15"
         AutoPostBack="True" meta:resourcekey="txtAlternativeResource1">
    </asp:TextBox>
             </td>
   <td>
                 <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                     meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                     Width="24px">
                     <Alignments TextImage="ImageBottom" />
                     <Appearance>
                         <Image Url="./Img/forum_search.gif" />
                     </Appearance>
                 </igtxt:WebImageButton>
             </td>  
                                                               <td class="spacearea">

</td>
 
<td>
 <asp:TextBox ID="TxtEmpName" Width="160px" runat="server"  MaxLength="100"
        AutoPostBack="false"  meta:resourcekey="txtEmployeeResource1">
   </asp:TextBox>
    </td>


                        <%--    <td style="width: 50%; vertical-align: middle"></td>
                            <td style="width: 50%; vertical-align: middle">--%>

                                <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                            </td>
                            <td style="width: 50%; vertical-align: middle">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                               <%-- <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <%--<td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblRegUserResource1"></asp:Label>
                                                    </td>--%>
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
                            <igtab:Tab Text="متابعة الطلبات للمدير المباشر" meta:resourcekey="FollowupMyRequestsTabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 700px; vertical-align: top"
                                        cellspacing="0">
                                <%--   <tr>
                                            <td style="height: 100%" colspan="3">
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>--%>
                                       
                                       
                                            <tr>
                                                <td style="height: 100%; vertical-align: auto" colspan="3">
                                                    <igtbl:UltraWebGrid    Browser="UpLevel"  ID="uwgEmployeeVacations" runat="server" EnableAppStyling="False"
                                                        Height="100%" meta:resourcekey="uwgEmployeeVacationsResource1" SkinID="Default"
                                                        Width="99%">
                                                       
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                            CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                                                            HeaderClickActionDefault="SortMulti" Name="uwgEmployeeVacations" RowHeightDefault="15px"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowRowNumberingDefault="ByDataIsland">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
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
                                                                    <Header Caption="مراحل الطلب">
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
                                                                                                     
                                                                    <igtbl:UltraGridColumn Type="Button" Key="Attachment" Width="15%" AllowRowFiltering="False" CellButtonDisplay="Always"
                                                                    meta:resourcekey="AttachmentResource16">
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
