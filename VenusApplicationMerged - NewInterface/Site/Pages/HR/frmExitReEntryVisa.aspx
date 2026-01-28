<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmExitReEntryVisa.aspx.vb"
    Inherits="Interfaces_frmExitReEntryVisa" Culture="auto" UICulture="auto" EnableSessionState="True" %>

<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebToolbar" TagPrefix="igtbar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>* Venus Payroll * ~Standard Government Form</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <link href="app_styles.css" type="text/css" rel="stylesheet" />
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_frmExitReEntryVisa.js"></script>
    <link href="~/ig_common/20071CLR20/Styles/Office2007Blue/ig_WebTab.css " rel="Stylesheet"
        type="text/css" />
    <script language="javascript" type="text/javascript">
        function OpenPrintedScreen(v) {
            var hight = window.screen.availHeight - 35;
            var width = window.screen.availWidth - 10;
            var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=ID&preview=1&ReportCode=ExitReEntryVisa&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
            win.focus();
        }
        function btnPrint() {
            var hdnEmpTrans = window.document.getElementById("hdnEmpTrans")
            if (parseInt(hdnEmpTrans.value) > 0) {
                OpenPrintedScreen(parseInt(hdnEmpTrans.value));
            }
        }
        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("hdnEmpID");
            if (CheckID == false || (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")) {
                var page = pageurl + "EmpID=" + ctrId.value;
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
        .igwgFrameBlue2k7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            background-color: Transparent;
            border: solid 1px #000000;
        }
        .igwgHdrBlue2k7
        {
            border-style: none;
            border-color: inherit;
            border-width: 0px;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 11px;
            background-color: #D4D7DB;
            background-image: url('../ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/igwgHeader.jpg');
            background-repeat: repeat-x;
            height: 23px;
            font-weight: normal;
            cursor: hand;
        }
        
        .igwgRowBlue2k7
        {
            border-top: 1px solid white;
            border-bottom: 1px solid #E3EFFF;
            font-size: 11px;
        }
        .igwgRowAltBlue2k7
        {
            border-top: 1px solid #F9F9F9;
            border-bottom: 1px solid #E3EFFF;
            background-color: #f9f9f9;
            font-size: 11px;
        }
    </style>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" onload="VisaTypeFirst('rbtnTravelVisa')">
    <form id="frmExitReEntryVisa" runat="server" onsubmit="return Do_Submit_Flag()">
    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
        <tr>
            <td>
                <div>
                    &nbsp;
                    <div align="Center" style="border-style: outset; border-color: inherit; border-width: 0px;
                        z-index: 102; left: 0px; width: 100%; position: absolute; top: 32px; height: 593px;
                        background-color: #BFDBFF" id="DIV" runat="server">
                        &nbsp;
                        <igtab:UltraWebTab meta:resourcekey="UltraWebTab1Recourcekey" ID="UltraWebTab1" runat="server"
                            Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Style="position: absolute; top: 1px; left: 0px; height: 590px;
                            width: 100%;" ThreeDEffect="False" CssClass="igwtMainBlue2k7">
                            <Tabs>
                                <igtab:Tab Text="Standard Government Form" meta:resourceKey="tab1Res">
                                    <ContentTemplate>
                                        &nbsp;
                                        <asp:Label meta:resourcekey="lblOtherRecourcekey" ID="lblOther" runat="server" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="Small" Style="z-index: 101; left: 444px; position: absolute;
                                            top: 128px; height: 22px; width: 45px;" Text="Other"></asp:Label>
                                        <asp:Label meta:resourcekey="lblDayRecourcekey" ID="lblDay" runat="server" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="Small" Style="z-index: 101; left: 325px; position: absolute;
                                            top: 128px; height: 20px; width: 39px;" Text="Day"></asp:Label>
                                        <asp:Label meta:resourcekey="lblMonthRecourcekey" ID="lblMonth" runat="server" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="Small" Style="z-index: 101; left: 120px; position: absolute;
                                            top: 128px; height: 20px; width: 39px;" Text="Month"></asp:Label>
                                        <asp:Label meta:resourcekey="lblDayPeriodRecourcekey" ID="lblDayPeriod" runat="server"
                                            BorderStyle="None" BorderWidth="1px" Font-Size="Small" Style="z-index: 101; left: 224px;
                                            position: absolute; top: 128px; height: 20px; width: 39px;" Text="Period"></asp:Label>
                                        <asp:Label meta:resourcekey="lblMonthPeriodRecourcekey" ID="lblMonthPeriod" runat="server"
                                            BorderStyle="None" BorderWidth="1px" Font-Size="Small" Style="z-index: 101; left: 15px;
                                            position: absolute; top: 128px; height: 20px; width: 42px;" Text="Period"></asp:Label>
                                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp;
                                        <asp:TextBox meta:resourcekey="txtOtherRecourcekey" ID="txtOther" runat="server"
                                            BorderColor="White" BorderStyle="Inset" BorderWidth="1px" Style="z-index: 106;
                                            left: 495px; position: absolute; top: 128px; height: 17px; width: 150px;"></asp:TextBox>
                                        <asp:CheckBox meta:resourcekey="chkDamagedRecourcekey" ID="chkDamaged" runat="server"
                                            Width="90px" Style="z-index: 1; left: 110px; top: 100px; position: absolute"
                                            Text="Damaged" />
                                        <asp:CheckBox meta:resourcekey="chkLostRecourcekey" ID="chkLost" runat="server" Width="90px"
                                            Style="z-index: 1; left: 110px; top: 75px; position: absolute" Text="Lost" />
                                        <asp:CheckBox meta:resourcekey="chkReNewRecourcekey" ID="chkReNew" runat="server"
                                            Width="90px" Style="z-index: 1; left: 13px; top: 97px; position: absolute" Text="Re-New" />
                                        <asp:CheckBox meta:resourcekey="chkTravelMoreRecourcekey" ID="chkTravelMore" runat="server"
                                            Width="90px" Style="z-index: 1; left: 323px; top: 77px; position: absolute" Text="More Travel" />
                                        <asp:CheckBox meta:resourcekey="chkFinalExitRecourcekey" ID="chkFinalExit" runat="server"
                                            Width="90px" Style="z-index: 1; left: 222px; top: 102px; position: absolute"
                                            Text="Final Exit" />
                                        <asp:CheckBox meta:resourcekey="chkSecondTimeRecourcekey" ID="chkSecondTime" runat="server"
                                            Width="90px" Style="z-index: 1; left: 537px; top: 74px; position: absolute" Text="Second Time" />
                                        <asp:CheckBox meta:resourcekey="chkThirdTimeRecourcekey" ID="chkThirdTime" runat="server"
                                            Width="90px" Style="z-index: 1; left: 439px; top: 104px; position: absolute"
                                            Text="Third Time" />
                                        <asp:CheckBox meta:resourcekey="chkFirstTimeRecourcekey" ID="chkFirstTime" runat="server"
                                            Width="90px" Style="z-index: 1; left: 439px; top: 74px; position: absolute; right: 269px;
                                            height: 22px;" Text="First Time" />
                                        <asp:CheckBox meta:resourcekey="chkTravelOnceRecourcekey" ID="chkTravelOnce" runat="server"
                                            Style="z-index: 1; left: 222px; top: 76px; position: absolute" Width="116px"
                                            Text="Travel Once" />
                                        <asp:CheckBox meta:resourcekey="chkNewRecourcekey" ID="chkNew" runat="server" Width="116px"
                                            Style="z-index: 1; left: 13px; top: 72px; position: absolute" Text="New" />
                                        <asp:RadioButton meta:resourcekey="rbtnAddDependantRecourcekey" ID="rbtnAddDependant"
                                            runat="server" Width="116px" Style="z-index: 1; top: 43px; position: absolute;
                                            height: 20px" Text="Add Dependant" GroupName="Type" />
                                        <asp:RadioButton meta:resourcekey="rbtnTransferBailRecourcekey" ID="rbtnTransferBail"
                                            runat="server" Width="116px" Style="z-index: 1; left: 434px; top: 43px; position: absolute;
                                            height: 20px" Text="Transfer Bail" GroupName="Type" />
                                        <asp:RadioButton meta:resourcekey="rbtnTravelVisaRecourcekey" ID="rbtnTravelVisa"
                                            runat="server" Width="116px" Style="z-index: 1; left: 223px; top: 43px; position: absolute;
                                            height: 20px" Text="Travel Visa" GroupName="Type" Checked="True" />
                                        <asp:RadioButton meta:resourcekey="rbtnResidentIssueRecourcekey" ID="rbtnResidentIssue"
                                            runat="server" Width="116px" Style="z-index: 1; left: 11px; top: 43px; position: absolute"
                                            Text="Resident Issue" GroupName="Type" />
                                        <igtab:UltraWebTab meta:resourcekey="UltraWebTab2Recourcekey" ID="UltraWebTab2" runat="server"
                                            CssClass="igwtMainBlue2k7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" ThreeDEffect="False" ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebTab/"
                                            Style="z-index: 1; left: 0px; top: 170px; position: absolute; height: 414px;
                                            width: 100%">
                                            <DefaultTabStyle Height="22px">
                                            </DefaultTabStyle>
                                            <Tabs>
                                                <igtab:Tab Text="Personal Info" meta:resourceKey="PersonalInfoTab">
                                                    <ContentTemplate>
                                                        <asp:Image meta:resourcekey="Image1Recourcekey" ID="Image1" runat="server" Style="left: 18px;
                                                            position: absolute; top: 33px; height: 91px; width: 93px;" />
                                                        <asp:Label meta:resourcekey="lblIssueDateRecourcekey" ID="lblIssueDate" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 17px; position: absolute; top: 172px; height: 21px;
                                                            width: 87px;" Text="Issue Date"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblExpireDateRecourcekey" ID="lblExpireDate" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 17px; position: absolute; top: 201px; height: 21px;
                                                            width: 87px;" Text="Expire Date"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblIssueCountryRecourcekey" ID="lblIssueCountry" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 17px; position: absolute; top: 229px; height: 21px;
                                                            width: 87px;" Text="Issue Country"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblCode1003Recourcekey" ID="lblCode1003" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 17px; position: absolute; top: 331px; height: 21px;
                                                            width: 87px;" Text="Sponsor No"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblResidentExpireDateRecourcekey" ID="lblResidentExpireDate"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="X-Small" Style="z-index: 102; left: 17px; position: absolute; top: 301px;
                                                            height: 21px; width: 87px;" Text="Expire Date"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblResidentNoRecourcekey" ID="lblResidentNo" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 17px; position: absolute; top: 272px; height: 21px;
                                                            width: 87px;" Text="Resident No"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblEntDateRecourcekey" ID="lblEntDate" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 322px; position: absolute; top: 331px; height: 21px;
                                                            width: 86px;" Text="Ent Date"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblEntOutletRecourcekey" ID="lblEntOutlet" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 321px; position: absolute; top: 362px; height: 21px;
                                                            width: 87px;" Text="Ent Outlet"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblBorderEntNoRecourcekey" ID="lblBorderEntNo" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 321px; position: absolute; top: 301px; height: 21px;
                                                            width: 87px;" Text="Border Ent No"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblResidentSponsorTelRecourcekey" ID="lblResidentSponsorTel"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="X-Small" Style="z-index: 102; left: 321px; position: absolute; top: 235px;
                                                            height: 21px; width: 87px;" Text="Tel"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblResidentSponsorAddressRecourcekey" ID="lblResidentSponsorAddress"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="X-Small" Style="z-index: 102; left: 321px; position: absolute; top: 207px;
                                                            height: 21px; width: 87px;" Text="Address"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblResidentSponsorNameRecourcekey" ID="lblResidentSponsorName"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="X-Small" Style="z-index: 102; left: 321px; position: absolute; top: 179px;
                                                            height: 21px; width: 87px;" Text="Sponsor Name"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblNewResidentIssueRecourcekey" ID="lblNewResidentIssue"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="Medium" Style="z-index: 102; left: 319px; position: absolute; top: 268px;
                                                            height: 22px; width: 430px;" Text="This Part For New Resident Issue" Font-Bold="True"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblPassportNoRecourcekey" ID="lblPassportNo" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 17px; position: absolute; top: 143px; height: 21px;
                                                            width: 87px;" Text="Passport No"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblBirthDateRecourcekey" ID="lblBirthDate" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 476px; position: absolute; top: 87px; height: 19px;
                                                            width: 87px;" Text="Birth Date"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblReligionRecourcekey" ID="lblReligion" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 476px; position: absolute; top: 60px; height: 23px;
                                                            width: 87px;" Text="Religion"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblProfessionRecourcekey" ID="lblProfession" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 476px; position: absolute; top: 33px; height: 21px;
                                                            width: 87px;" Text="Profession"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblNameRecourcekey" ID="lblName" runat="server" BackColor="Transparent"
                                                            BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small" Style="z-index: 102;
                                                            left: 118px; position: absolute; top: 60px; height: 18px; width: 87px;" Text="Name"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblEmpCodeRecourcekey" ID="lblEmpCode" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 119px; position: absolute; top: 33px; height: 18px;
                                                            width: 87px; right: 1099px;" Text="Emp Code"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblNationalityRecourcekey" ID="lblNationality" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 119px; position: absolute; top: 87px; height: 19px;
                                                            width: 87px;" Text="Nationality"></asp:Label>
                                                        <asp:Label meta:resourcekey="txtBirthDateRecourcekey" ID="txtBirthDate" runat="server"
                                                            BorderStyle="Solid" BorderWidth="1px" Style="z-index: 105; left: 570px; position: absolute;
                                                            top: 87px; height: 20px; width: 161px;" TabIndex="1"></asp:Label>
                                                        <asp:Label meta:resourcekey="txtReligionRecourcekey" ID="txtReligion" runat="server"
                                                            BorderStyle="Solid" BorderWidth="1px" Style="z-index: 105; left: 570px; position: absolute;
                                                            top: 60px; height: 20px; width: 161px;" TabIndex="1"></asp:Label>
                                                        <asp:TextBox meta:resourcekey="txtResidentNoRecourcekey" ID="txtResidentNo" runat="server"
                                                            BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 112px; position: absolute; top: 272px; height: 18px;
                                                            width: 161px;" TabIndex="1"></asp:TextBox>
                                                        <asp:Label meta:resourcekey="txtProfessionRecourcekey" ID="txtProfession" runat="server"
                                                            BorderStyle="Solid" BorderWidth="1px" Style="z-index: 105; left: 570px; position: absolute;
                                                            top: 33px; height: 20px; width: 161px;" TabIndex="1"></asp:Label>
                                                        <asp:TextBox meta:resourcekey="txtEntOutletRecourcekey" ID="txtEntOutlet" runat="server"
                                                            BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 415px; position: absolute; top: 362px; height: 18px;
                                                            width: 161px;" TabIndex="1"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtBorderEntNoRecourcekey" ID="txtBorderEntNo" runat="server"
                                                            BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 415px; position: absolute; top: 301px; height: 18px;
                                                            width: 161px;" TabIndex="1"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtResidentSponsorTelRecourcekey" ID="txtResidentSponsorTel"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 415px; position: absolute; top: 236px; height: 18px;
                                                            width: 161px;" TabIndex="1"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtResidentSponsorAddressRecourcekey" ID="txtResidentSponsorAddress"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 415px; position: absolute; top: 209px; height: 18px;
                                                            width: 249px;" TabIndex="1"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtPassportNoRecourcekey" ID="txtPassportNo" runat="server"
                                                            BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 112px; position: absolute; top: 143px; height: 18px;
                                                            width: 161px;" TabIndex="1"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtResidentSponsorNoRecourcekey" ID="txtResidentSponsorNo"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 112px; position: absolute; top: 331px; height: 18px;
                                                            width: 161px;" TabIndex="1"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtResidentSponsorNameRecourcekey" ID="txtResidentSponsorName"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 415px; position: absolute; top: 180px; height: 18px;
                                                            width: 249px; bottom: 214px;" TabIndex="1"></asp:TextBox>
                                                        <asp:Label meta:resourcekey="txtNationalityRecourcekey" ID="txtNationality" runat="server"
                                                            BorderStyle="Solid" BorderWidth="1px" Style="z-index: 105; left: 213px; position: absolute;
                                                            top: 87px; height: 20px; width: 161px;" TabIndex="1"></asp:Label>
                                                        <asp:Label meta:resourcekey="txtNameRecourcekey" ID="txtName" runat="server" BorderStyle="Solid"
                                                            BorderWidth="1px" Style="z-index: 105; left: 213px; position: absolute; top: 60px;
                                                            height: 20px; width: 239px;" TabIndex="1"></asp:Label>
                                                        <asp:TextBox meta:resourcekey="txtEmpCodeRecourcekey" ID="txtEmpCode" Height="18px"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 213px; position: absolute; top: 33px; width: 125px;
                                                            right: 422px;" TabIndex="1" AutoPostBack="True"></asp:TextBox>
                                                        <igtxt:WebImageButton meta:resourcekey="btnSearchEmpCodeRecourcekey" ID="btnSearchEmpCode"
                                                            runat="server" Overflow="NoWordWrap" Style="z-index: 121; left: 345px; position: absolute;
                                                            top: 33px; height: 18px;" UseBrowserDefaults="False" Width="26px" AutoSubmit="false">
                                                            <HoverAppearance>
                                                                <style backcolor="Silver" cursor="Hand">
                                                                    
                                                                </style>
                                                                <ButtonStyle BackColor="Silver" Cursor="Hand">
                                                                </ButtonStyle>
                                                            </HoverAppearance>
                                                            <Alignments TextImage="ImageBottom" />
                                                            <Appearance>
                                                                <Image Url="./Img/forum_search.gif" />
                                                                <style bordercolor="White" borderstyle="Solid" borderwidth="1px" cursor="Hand" font-bold="True">
                                                                    
                                                                </style>
                                                                <ButtonStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Cursor="Hand"
                                                                    Font-Bold="True">
                                                                </ButtonStyle>
                                                            </Appearance>
                                                        </igtxt:WebImageButton>
                                                        <igtxt:WebMaskEdit meta:resourcekey="wdcPassportIssueDateRecourcekey" ID="wdcPassportIssueDate"
                                                            runat="server" Height="16px" HorizontalAlign="Left" InputMask="##/##/####" Style="left: 112px;
                                                            position: absolute; top: 172px" TabIndex="4" Width="155px">
                                                        </igtxt:WebMaskEdit>
                                                        <igtxt:WebMaskEdit meta:resourcekey="wdcPassportExpireDateRecourcekey" ID="wdcPassportExpireDate"
                                                            runat="server" Height="16px" HorizontalAlign="Left" InputMask="##/##/####" Style="left: 112px;
                                                            position: absolute; top: 201px;" TabIndex="4" Width="155px">
                                                        </igtxt:WebMaskEdit>
                                                        <igtxt:WebMaskEdit meta:resourcekey="wdcEntDateRecourcekey" ID="wdcEntDate" runat="server"
                                                            Height="16px" HorizontalAlign="Left" InputMask="##/##/####" Style="left: 415px;
                                                            position: absolute; top: 331px;" TabIndex="4" Width="155px">
                                                        </igtxt:WebMaskEdit>
                                                        <igtxt:WebMaskEdit meta:resourcekey="wdcResidentExpireDateRecourcekey" ID="wdcResidentExpireDate"
                                                            runat="server" Height="16px" HorizontalAlign="Left" InputMask="##/##/####" Style="left: 112px;
                                                            position: absolute; top: 301px;" TabIndex="4" Width="155px">
                                                        </igtxt:WebMaskEdit>
                                                        <asp:CheckBox meta:resourcekey="chkResidentGovernmentRecourcekey" ID="chkResidentGovernment"
                                                            runat="server" Style="z-index: 1; left: 317px; top: 151px; position: absolute;
                                                            width: 101px;" Text="Government" />
                                                        <asp:CheckBox meta:resourcekey="chkResidentPersonsRecourcekey" ID="chkResidentPersons"
                                                            runat="server" Width="90px" Style="z-index: 1; left: 656px; top: 151px; position: absolute;
                                                            height: 20px;" Text="Persons" />
                                                        <asp:CheckBox meta:resourcekey="chkResidentCompaniesRecourcekey" ID="chkResidentCompanies"
                                                            runat="server" Width="90px" Style="z-index: 1; left: 550px; top: 151px; position: absolute;
                                                            height: 20px;" Text="Companies" />
                                                        <asp:CheckBox meta:resourcekey="chkResidentInstitutionsRecourcekey" ID="chkResidentInstitutions"
                                                            runat="server" Width="90px" Style="z-index: 1; left: 434px; top: 151px; position: absolute;
                                                            height: 20px;" Text="Institutions" />
                                                        <hr meta:resourcekey="sep4Recourcekey" id="sep4" runat="server" style="z-index: 1;
                                                            left: 19px; top: 265px; position: absolute; height: 2px; width: 297px" />
                                                        <asp:DropDownList meta:resourcekey="ddlIssueCountryRecourcekey" ID="ddlIssueCountry"
                                                            runat="server" BackColor="Silver" Height="23px" Style="left: 112px; position: absolute;
                                                            top: 229px; z-index: 116;" TabIndex="47" Width="161px">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Transfer Bail" meta:resourceKey="TransferBailTab">
                                                    <ContentTemplate>
                                                        <asp:CheckBox meta:resourcekey="chkTransferBailPersonsRecourcekey" ID="chkTransferBailPersons"
                                                            runat="server" Width="90px" Style="z-index: 1; left: 373px; top: 49px; position: absolute"
                                                            Text="Persons" />
                                                        <asp:CheckBox meta:resourcekey="chkTransferBailCompaniesRecourcekey" ID="chkTransferBailCompanies"
                                                            runat="server" Width="90px" Style="z-index: 1; left: 259px; top: 49px; position: absolute"
                                                            Text="Companies" />
                                                        <asp:CheckBox meta:resourcekey="chkTransferBailInstitutionsRecourcekey" ID="chkTransferBailInstitutions"
                                                            runat="server" Width="90px" Style="z-index: 1; left: 141px; top: 48px; position: absolute"
                                                            Text="Institutions" />
                                                        <asp:CheckBox meta:resourcekey="chkTransferBailGovernmentRecourcekey" ID="chkTransferBailGovernment"
                                                            runat="server" Width="90px" Style="z-index: 1; left: 28px; top: 49px; position: absolute;
                                                            height: 18px; width: 99px;" Text="Government" />
                                                        <asp:Label meta:resourcekey="lblCode1009Recourcekey" ID="lblCode1009" runat="server"
                                                            BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 360px; position: absolute; top: 110px; height: 18px;
                                                            width: 18px;" Text="/&#1578;"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblCode1010Recourcekey" ID="lblCode1010" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 6px; position: absolute; top: 135px; height: 21px;
                                                            width: 109px;" Text="Transferor Sponsor" Visible="False"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblTransferBailNewSponsorNoRecourcekey" ID="lblTransferBailNewSponsorNo"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="X-Small" Style="z-index: 102; left: 6px; position: absolute; top: 107px;
                                                            height: 21px; width: 109px;" Text="Sponsor No"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblTransferorSponsorNoRecourcekey" ID="lblTransferorSponsorNo"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="X-Small" Style="z-index: 102; left: 340px; position: absolute; top: 135px;
                                                            height: 21px; width: 109px;" Text="Sponsor No" Visible="False"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblCode1007Recourcekey" ID="lblCode1007" runat="server"
                                                            BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px" Font-Size="X-Small"
                                                            Style="z-index: 102; left: 340px; position: absolute; top: 81px; height: 21px;
                                                            width: 109px;" Text="Sponsor Address"></asp:Label>
                                                        <asp:Label meta:resourcekey="lblTransferBailNewSponsorNameRecourcekey" ID="lblTransferBailNewSponsorName"
                                                            runat="server" BackColor="Transparent" BorderStyle="Outset" BorderWidth="1px"
                                                            Font-Size="X-Small" Style="z-index: 102; left: 6px; position: absolute; top: 80px;
                                                            height: 21px; width: 109px;" Text="New Sponsor Name"></asp:Label>
                                                        <asp:TextBox meta:resourcekey="txtTransferorSponsorRecourcekey" ID="txtTransferorSponsor"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 127px; position: absolute; top: 135px" TabIndex="1"
                                                            Width="197px" Visible="False"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtTransferBailNewSponsorNoRecourcekey" ID="txtTransferBailNewSponsorNo"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 127px; position: absolute; top: 107px" TabIndex="1"
                                                            Width="197px"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txTransferorSponsorNoRecourcekey" ID="txTransferorSponsorNo"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 456px; position: absolute; top: 135px" TabIndex="1"
                                                            Width="197px" Visible="False"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtTransferBailNewSponsorNo2Recourcekey" ID="txtTransferBailNewSponsorNo2"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 456px; position: absolute; top: 106px" TabIndex="1"
                                                            Width="197px"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtTransferBailSponsorAddressRecourcekey" ID="txtTransferBailSponsorAddress"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 456px; position: absolute; top: 81px" TabIndex="1"
                                                            Width="197px"></asp:TextBox>
                                                        <asp:TextBox meta:resourcekey="txtTransferBailNewSponsorNameRecourcekey" ID="txtTransferBailNewSponsorName"
                                                            runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                            Style="z-index: 105; left: 127px; position: absolute; top: 80px; height: 20px;
                                                            width: 197px;" TabIndex="1"></asp:TextBox>
                                                    </ContentTemplate>
                                                </igtab:Tab>
                                                <igtab:Tab Text="Add Dependant" meta:resourceKey="AddDependantTab">
                                                    <ContentTemplate>
                                                        <igtxt:WebMaskEdit meta:resourcekey="wdcCBirthDateRecourcekey" ID="wdcCBirthDate"
                                                            runat="server" Height="16px" HorizontalAlign="Left" InputMask="##/##/####" TabIndex="4"
                                                            Width="155px">
                                                            <ClientSideEvents KeyDown="wme_EditKeyDown" />
                                                        </igtxt:WebMaskEdit>
                                                        <igtxt:WebMaskEdit meta:resourcekey="wdcCExpiryDateRecourcekey" ID="wdcCExpiryDate"
                                                            runat="server" Height="16px" HorizontalAlign="Left" InputMask="##/##/####" TabIndex="4"
                                                            Width="155px">
                                                            <ClientSideEvents KeyDown="wme_EditKeyDown" />
                                                        </igtxt:WebMaskEdit>
                                                        <igtbl:UltraWebGrid   meta:resourcekey="uwgDependentsRecourcekey" ID="uwgDependents"
                                                            runat="server"  Browser="UpLevel" ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/"
                                                            Style="left: 9px; position: absolute; top: 37px; z-index: 118" TabIndex="48"
                                                            Height="343px" Width="738px">
                                                            <Bands>
                                                                <igtbl:UltraGridBand>
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
                                                                        <igtbl:UltraGridColumn meta:resourcekey="NoRecourcekey" Width="30px" BaseColumnName="No"
                                                                            Key="No" AllowUpdate="No">
                                                                            <Header Caption="M">
                                                                            </Header>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Name" Key="Name" meta:resourcekey="cNameRecourcekey"
                                                                            Width="180px">
                                                                            <Header Caption="Name Like Passport">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="CBirthDateRecourcekey" Type="Custom" EditorControlID="wdcCBirthDate"
                                                                            Width="70px" Key="CBirthDate">
                                                                            <Header Caption="BirthDate">
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="RelationIDRecourcekey" Width="90px" BaseColumnName="RelationID"
                                                                            Key="RelationID" Type="DropDownList">
                                                                            <Header Caption="Relation">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="PassportNoRecourcekey" Width="90px" BaseColumnName="PassportNo"
                                                                            Key="PassportNo">
                                                                            <Header Caption="Passport No">
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="CExpiryDateRecourcekey" Key="CExpiryDate"
                                                                            Width="70px" Type="Custom" EditorControlID="wdcCExpiryDate">
                                                                            <Header Caption="Expiry Date">
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="EntOutletRecourcekey" BaseColumnName="EntOutlet"
                                                                            Width="95px" Key="EntOutlet">
                                                                            <Header Caption="Ent Outlet">
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="BorderEntNoRecourcekey" BaseColumnName="BorderEntNo"
                                                                            Width="70px" Key="BorderEntNo">
                                                                            <Header Caption="Border Ent No">
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="BirthDateRecourcekey" BaseColumnName="BirthDate"
                                                                            Hidden="True" Key="BirthDate">
                                                                            <Header>
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="BirthDate_DRecourcekey" BaseColumnName="BirthDate_D"
                                                                            Hidden="True" Key="BirthDate_D">
                                                                            <Header>
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="ExpiryDateRecourcekey" BaseColumnName="ExpiryDate"
                                                                            Key="ExpiryDate" Hidden="True">
                                                                            <Header>
                                                                                <RowLayoutColumnInfo OriginX="10" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="10" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="ExpiryDate_DRecourcekey" BaseColumnName="ExpiryDate_D"
                                                                            Key="ExpiryDate_D" Hidden="True">
                                                                            <Header>
                                                                                <RowLayoutColumnInfo OriginX="11" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="11" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn meta:resourcekey="IDRecourcekey" BaseColumnName="ID" Key="ID"
                                                                            Hidden="True">
                                                                            <Header>
                                                                                <RowLayoutColumnInfo OriginX="12" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="12" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                            <DisplayLayout AllowAddNewDefault="Yes" AllowColSizingDefault="Free" AllowSortingDefault="OnClient"
                                                                BorderCollapseDefault="Separate"   GridLinesDefault="NotSet"
                                                                HeaderClickActionDefault="SortMulti" Name="uwgDependents" RowHeightDefault="20px"
                                                                RowSelectorsDefault="No" SelectTypeCellDefault="Single" SelectTypeRowDefault="Single"
                                                                StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                Version="4.00" ViewType="OutlookGroupBy" AllowUpdateDefault="Yes">
                                                                <GroupByBox Hidden="True">
                                                                    <BandLabelStyle CssClass="igwgGrpBoxBandLblBlue2k7">
                                                                    </BandLabelStyle>
                                                                    <BoxStyle BackColor="ActiveBorder" BorderColor="Window">
                                                                    </BoxStyle>
                                                                </GroupByBox>
                                                                <GroupByRowStyleDefault CssClass="igwgGrpRowBlue2k7">
                                                                </GroupByRowStyleDefault>
                                                                <ActivationObject BorderColor="181, 196, 223" BorderWidth="">
                                                                    <BorderDetails WidthLeft="0px" WidthRight="0px" />
                                                                </ActivationObject>
                                                                <FooterStyleDefault CssClass="igwgFooterBlue2k7">
                                                                </FooterStyleDefault>
                                                                <RowStyleDefault CssClass="igwgRowBlue2k7">
                                                                    <BorderDetails WidthBottom="1px" WidthTop="1px" />
                                                                    <Padding Left="3px" />
                                                                </RowStyleDefault>
                                                                <FilterOptionsDefault DropDownRowCount="15">
                                                                    <FilterHighlightRowStyle CssClass="igwgFltrRowHiLtBlue2k7">
                                                                    </FilterHighlightRowStyle>
                                                                    <FilterDropDownStyle CssClass="igwgFltrDrpDwnBlue2k7">
                                                                    </FilterDropDownStyle>
                                                                </FilterOptionsDefault>
                                                                <HeaderStyleDefault CssClass="igwgHdrBlue2k7" Height="23px">
                                                                </HeaderStyleDefault>
                                                                <RowAlternateStyleDefault CssClass="igwgRowAltBlue2k7">
                                                                </RowAlternateStyleDefault>
                                                                <EditCellStyleDefault CssClass="igwgCellEdtBlue2k7">
                                                                </EditCellStyleDefault>
                                                                <FrameStyle BorderStyle="None" CssClass="igwgFrameBlue2k7" Height="343px" Width="738px">
                                                                </FrameStyle>
                                                                <Pager>
                                                                    <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </PagerStyle>
                                                                    <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </PagerStyle>
                                                                </Pager>
                                                                <AddNewBox>
                                                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </BoxStyle>
                                                                    <ButtonStyle CssClass="igwgAddNewBtnBlue2k7">
                                                                    </ButtonStyle>
                                                                    <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" BorderWidth="1px">
                                                                        <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                                    </BoxStyle>
                                                                </AddNewBox>
                                                                <Images ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebGrid/">
                                                                </Images>
                                                                <ClientSideEvents AfterCellUpdateHandler="uwgDependents_AfterCellUpdateHandler" BeforeCellChangeHandler="uwgDependents_BeforeCellChangeHandler"
                                                                    CellChangeHandler="uwgDependents_CellChangeHandler" EditKeyDownHandler="uwgDependents_EditKeyDownHandler"
                                                                    EditKeyUpHandler="uwgDependents_EditKeyUpHandler" CellClickHandler="uwgDependents_CellClickHandler"
                                                                    AfterEnterEditModeHandler="uwgDependents_AfterEnterEditModeHandler" />
                                                                <RowSelectorStyleDefault CssClass="igwgRowSlctrBlue2k7">
                                                                </RowSelectorStyleDefault>
                                                                <SelectedHeaderStyleDefault CssClass="igwgHdrSelBlue2k7">
                                                                </SelectedHeaderStyleDefault>
                                                                <SelectedGroupByRowStyleDefault CssClass="igwgGrpRowSelBlue2k7">
                                                                </SelectedGroupByRowStyleDefault>
                                                                <SelectedRowStyleDefault CssClass="igwgRowSelBlue2k7">
                                                                </SelectedRowStyleDefault>
                                                                <RowExpAreaStyleDefault CssClass="igwgRowExpBlue2k7">
                                                                </RowExpAreaStyleDefault>
                                                                <FixedCellStyleDefault CssClass="igwgCellFxdBlue2k7">
                                                                </FixedCellStyleDefault>
                                                                <FixedHeaderStyleDefault CssClass="igwgHdrFxdBlue2k7">
                                                                </FixedHeaderStyleDefault>
                                                                <FixedFooterStyleDefault CssClass="igwgFtrFxdBlue2k7">
                                                                </FixedFooterStyleDefault>
                                                                <FormulaErrorStyleDefault CssClass="igwgFormulaErrBlue2k7">
                                                                </FormulaErrorStyleDefault>
                                                            </DisplayLayout>
                                                        </igtbl:UltraWebGrid>
                                                    </ContentTemplate>
                                                </igtab:Tab>
                                            </Tabs>
                                            <RoundedImage FillStyle="LeftMergedWithCenter" NormalImage="none" SelectedImage="igwt_tab_selected.jpg"
                                                HoverImage="igwt_tab_hover.jpg" LeftSideWidth="14" RightSideWidth="14" />
                                            <HoverTabStyle CssClass="igwtTabHoverBlue2k7">
                                            </HoverTabStyle>
                                            <SelectedTabStyle CssClass="igwtTabSelectedBlue2k7">
                                            </SelectedTabStyle>
                                        </igtab:UltraWebTab>
                                        <hr meta:resourcekey="sep1Recourcekey" id="sep1" runat="server" style="width: 3px;
                                            height: 126px; z-index: 1; left: 667px; top: 50px; position: absolute" />
                                        <hr meta:resourcekey="sep2Recourcekey" id="sep2" runat="server" style="width: 3px;
                                            height: 126px; z-index: 1; left: 428px; top: 50px; position: absolute" />
                                        <hr meta:resourcekey="sep3Recourcekey" id="sep3" runat="server" style="width: 3px;
                                            height: 126px; z-index: 1; left: 201px; top: 50px; position: absolute" />
                                        <igtxt:WebNumericEdit meta:resourcekey="wneDaysRecourcekey" ID="wneDays" runat="server"
                                            Nullable="false" MinValue="0" Style="z-index: 1; left: 270px; top: 128px; position: absolute;
                                            height: 15px; width: 45px">
                                        </igtxt:WebNumericEdit>
                                        <igtxt:WebNumericEdit meta:resourcekey="wneMonthPeriodRecourcekey" ID="wneMonthPeriod"
                                            runat="server" Nullable="false" Style="z-index: 1; left: 63px; top: 128px; position: absolute;
                                            height: 15px; width: 45px" MinValue="0">
                                        </igtxt:WebNumericEdit>
                                    </ContentTemplate>
                                </igtab:Tab>
                            </Tabs>
                            <DefaultTabStyle Height="22px" CssClass="igwtTabNormalBlue2k7">
                            </DefaultTabStyle>
                            <RoundedImage FillStyle="LeftMergedWithCenter" NormalImage="none" SelectedImage="igwt_tab_selected.jpg"
                                HoverImage="igwt_tab_hover.jpg" LeftSideWidth="14" RightSideWidth="14" />
                            <HoverTabStyle CssClass="igwtTabHoverBlue2k7">
                            </HoverTabStyle>
                            <SelectedTabStyle CssClass="igwtTabSelectedBlue2k7">
                            </SelectedTabStyle>
                        </igtab:UltraWebTab>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </div>
                    <igtbar:UltraWebToolbar meta:resourcekey="TlbMainToolbarRecourcekey" ID="TlbMainToolbar"
                        runat="server" BackColor="#BFDBFF" BackgroundImage="none" BorderColor="Transparent"
                        BorderStyle="None" BorderWidth="0px" Font-Bold="False" Font-Names="Times New Roman"
                        Font-Size="Small" ForeColor="DimGray" Height="32px" ImageDirectory="" ItemWidthDefault="42px"
                        JavaScriptFileName="" JavaScriptFileNameCommon="" MovableImage="" Style="z-index: 103;
                        left: 0px; position: absolute; top: 0px" Width="100%">
                        <HoverStyle BackColor="LightSteelBlue" BackgroundImage="none" BorderStyle="None"
                            BorderWidth="1px" Cursor="Default" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black">
                        </HoverStyle>
                        <Items>
                            <igtbar:TBSeparator />
                            <igtbar:TBarButton meta:resourcekey="SaveRecourcekey" DisabledImage="Img/save.gif"
                                HoverImage="" Image="Img/save.gif" ImageAlign="NotSet" Key="Save" SelectedImage=""
                                Text="Save &amp; Print">
                                <Images>
                                    <DefaultImage Url="Img/save.gif" />
                                    <DisabledImage Url="Img/save.gif" />
                                </Images>
                                <SelectedStyle CssClass="OppositeMainApplicationToolbar" Width="72px">
                                </SelectedStyle>
                                <HoverStyle CssClass="HoverToolBarButton" Width="72px">
                                </HoverStyle>
                                <DefaultStyle BackColor="Transparent" CssClass="DefultToolBarButton" Width="100px">
                                </DefaultStyle>
                            </igtbar:TBarButton>
                            <igtbar:TBSeparator Width="0px" />
                        </Items>
                        <DefaultStyle BackColor="Transparent" BackgroundImage="none" BorderStyle="None" Font-Bold="True"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" TextAlign="Left">
                        </DefaultStyle>
                        <SelectedStyle BackColor="Transparent" BackgroundImage="none" BorderStyle="None"
                            BorderWidth="1px" Cursor="Default" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black">
                        </SelectedStyle>
                        <ClientSideEvents Click="MainToolBarSave_Other_Fields" />
                    </igtbar:UltraWebToolbar>
                    <asp:Label meta:resourcekey="nameRecourcekey" ID="name" runat="server" ForeColor="White"
                        Style="z-index: 101; left: 839px; position: absolute; top: 112px" TabIndex="-1"
                        Width="99px"></asp:Label>
                    <asp:Label meta:resourcekey="realnameRecourcekey" ID="realname" runat="server" ForeColor="White"
                        Style="z-index: 102; left: 1010px; position: absolute; top: 139px" TabIndex="-1"
                        Width="99px"></asp:Label>
                    <asp:Label meta:resourcekey="TargetControlRecourcekey" ID="TargetControl" runat="server"
                        ForeColor="White" Style="z-index: 104; left: 1179px; position: absolute; top: 241px"
                        TabIndex="-1" Width="99px"></asp:Label>
                    <asp:HiddenField ID="hdnEmpID" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnFirstTime" runat="server" Value="1" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
