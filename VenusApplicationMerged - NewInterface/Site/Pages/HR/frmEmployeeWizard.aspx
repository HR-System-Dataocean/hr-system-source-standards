<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeeWizard.aspx.vb"
    Inherits="frmEmployeeWizard" Culture="auto" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ frmEmployeeWizard</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
<script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
<script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
<script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>

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
        var win = window.open("../../Interfaces/frmReportsGridViewer.aspx?Language=false&Criteria=EmployeeCode&preview=1&ReportCode=EmployeeDetails&sq0=''&v=" + v, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
        win.focus();
    }
    function DisplayImageScreenEmp(IntObjectId) {
        var webTab = igtab_getTabById("UltraWebTab1");
        if (webTab == null)
            return;
        var ctrId = window.document.getElementById("txtEmpId");

        if (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")
            window.open("frmPictures.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value, "_blank", "height=435,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
    }
    function DisplayImageScreenEmp2(IntObjectId) {
        var webTab = igtab_getTabById("UltraWebTab1");
        if (webTab == null)
            return;
        var ctrId = window.document.getElementById("txtEmpId");

        if (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")
            window.open("frmSignature.aspx?OId=" + IntObjectId + "&RId=" + ctrId.value, "_blank", "height=435,width=447,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,dependent=No");
    }

    var ODialoge;
    var OSender;
    function OpenModal1(pageurl, height, width, CheckID, CheckContract, SenderCtrl) {
        var ctrId = 100;
        var ContraId = 100;
        if (CheckContract == true) {
            if (ContraId.value == "" || ContraId.value == null || ContraId.value == "0") {
                return;
            }
        }

        if (CheckID == false || (ctrId.value != "" && ctrId.value != null && ctrId.value != "0")) {

            var page = pageurl + "EmpID=" + ctrId.value + "&ContID=" + ContraId.value;
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
    $(function () {
        var icons = {
            header: "",
            activeHeader: ""
        };
        $("#accordion").accordion({ icons: icons, autoHeight: false });
        $("#accordion").accordion("option", "icons", "");
    });
    $(function () {
        try {
            $(".Validators").Float();
        }
        catch (err) { }
    });


    <%--$(function () {
        $('#<%= LinkButton16.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
          $('#<%= LinkButton16.ClientID%>').click(function () {
              $.blockUI({ message: '' });
          });
      });--%>




    function CheckNumeric(e) {

        if (window.event) // IE 
        {
            if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                event.returnValue = false;
                return false;

            }

        }
        else { // Fire Fox
            if ((e.which < 48 || e.which > 57) & e.which != 8) {
                e.preventDefault();
                return false;

            }
        }
    }
</script>
     <style type="text/css">
        .auto-style1 {
            width: 50%;
            height: 21px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEmployeeWizard" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="display: none">
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td style="height: 18px">
                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                    <tr>
                                        <td style="width: 5px">
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCode" runat="server" Width="80px" SkinID="Label_CopyRightsBold" meta:resourcekey="lblCodeResource16"
                                                Text="الكود"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProjectCode" runat="server" Width="80px" SkinID="Label_CopyRightsNormal"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server" SkinID="Label_CopyRightsBold" Text="التوصيف"  meta:resourcekey="lblNameResource16"
                                                Width="80px"></asp:Label>
                                        </td>
                                        <td style="width: 40%;">
                                            <asp:Label ID="lblProjectName" runat="server" Width="100%" SkinID="Label_CopyRightsNormal"></asp:Label>
                                        </td>
                                        <td style="width: 60%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <igtab:UltraWebTab ID="UltraWebTab1" TabOrientation="LeftTop" runat="server" EnableAppStyling="True"
                        SkinID="Default" style="margin-bottom: 0px">
                        <Tabs>
                            <igtab:Tab Enabled="true" Text="معلومات التعديل" meta:resourcekey="TabEdit">
                                <ContentTemplate>
                                    <asp:HiddenField ID="CCHangeID" runat="server" />
                                    <asp:HiddenField ID="txtEmpId" runat="server" />
                                     <asp:HiddenField ID="txtContractId" runat="server" />
                                    <asp:HiddenField ID="NewChangeID" runat="server" />
                                    <asp:HiddenField ID="ToNewChangeID" runat="server" />
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: left; width: 50%">
                                                <asp:LinkButton ID="LinkButton2" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى"  meta:resourcekey="linkNext"
                                                    ValidationGroup="G1"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 2%">
                                            </td>
                                            <td style="width: 96%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
    <asp:TextBox ID="txtCode" dir="ltr" runat="server" AutoPostBack="True" MaxLength="30"
        meta:resourcekey="txtCodeResource1" SkinID="TextBox_LargeNormalC" Text="0"></asp:TextBox>
</td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label1" runat="server" Text="تاريخ بداية التفعيل" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtStartDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="G1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label31" runat="server" Text="تاريخ نهاية التفعيل" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtEndDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%">
                                            </td>
                                            <td style="width: 96%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label2" runat="server" Text="سبب التعديل" SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtCompanyConditions" runat="server" Height="60px" MaxLength="8000"
                                                                TextMode="MultiLine" Width="80%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator"
                                                                ControlToValidate="txtCompanyConditions" SetFocusOnError="True" ValidationGroup="G1">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; width: 100%; vertical-align: bottom; border-bottom: 1px solid black">
                                                <asp:Label ID="Label_SubTitle1" runat="server" SkinID="Label_DefaultBold" Text="تعديلات سابقة فى نفس العقد"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%">
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgChanges" runat="server" EnableAppStyling="True" Height="100px"
                                                    SkinID="Default" Width="100%" EnableTheming="True">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgSuperVisors"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                        AllowAddNewDefault="Yes" CellClickActionDefault="Edit">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100px"
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
                                                        <igtbl:UltraGridBand>
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                    Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="FromDate" Key="FromDate" Width="40%" Format="dd/MM/yyyy">
                                                                    <Header Caption="من تاريخ">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Remarks" Key="Remarks" Width="60%">
                                                                    <Header Caption="أسباب التعديل">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="البيانات الشخصية" meta:resourcekey="TabPersonal">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                                                                        <td style="height: 18px;  width: 50%">
</td>
                                                                                                                                                  <td style="height: 18px;  width: 35%">
</td>
                                            <td style="height: 18px; text-align: '<%# GetAlignmentNext() %>'; width: 15%">
                                                <asp:LinkButton ID="LinkButton4" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى" meta:resourcekey="linkNext"></asp:LinkButton>
                                            </td>
                                           
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">

                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="lblEmployeeCode" runat="server" meta:resourcekey="lblCodeResource1" SkinID="Label_DefaultNormal"
                                                                                Text="Code "></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEmployeeCode" dir="ltr" runat="server" AutoPostBack="True" MaxLength="30"
                                                                                meta:resourcekey="txtCodeResource1" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                               
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_ArbName" runat="server" Text="الاسم عربي" meta:resourcekey="lblAraNameRes"
                                                                SkinID="Label_ArbName"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebTextEdit ID="txtArbName" runat="server" DataMode="Text" SkinID="we" ValueText="">
                                                            </igtxt:WebTextEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_EngName" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="الاسم انجليزي" meta:resourcekey="lblEngNameRes"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebTextEdit ID="txtEngName" runat="server" DataMode="Text"
                                                                  ValueText="">
                                                            </igtxt:WebTextEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_FamilyArbName" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="اسم العائلة عربي" meta:resourcekey="lblFamilyNameRes"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebTextEdit ID="txtFamilyArbName" runat="server" DataMode="Text"
                                                                ValueText="">
                                                            </igtxt:WebTextEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_FamilyEngName" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="اسم العائلة انجليزي" meta:resourcekey="lblEngFamilyNameRes"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebTextEdit ID="txtFamilyEngName" runat="server" DataMode="Text"
                                                                  ValueText="">
                                                            </igtxt:WebTextEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_FatherArbName" runat="server" Text="اسم الاب عربي" meta:resourcekey="lblAraFatherNameRes"
                                                                SkinID="Label_DefaultNormal"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebTextEdit ID="txtFatherArbName" runat="server" DataMode="Text"
                                                                ValueText="">
                                                            </igtxt:WebTextEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%">
                                            </td>
                                            <td style="width: 47%">
                                                <table style="width: 100%; height: 100%">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td style="width: 90px;">
                                                            <asp:Label ID="Label_FatherEngName" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblEngFatherNameRes"
                                                                Text="اسم الاب انجليزي"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebTextEdit ID="txtFatherEngName" runat="server" DataMode="Text"
                                                                 ValueText="">
                                                            </igtxt:WebTextEdit>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        
                                        <tr>
     <td style="width: 47%">
         <table style="width: 100%; height: 100%">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td style="width: 90px;">
                     <asp:Label ID="lblGrandArbName" runat="server" Text="اسم الجد عربي" meta:resourcekey="lblAraGrandNameRes"
                         SkinID="Label_DefaultNormal"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <igtxt:WebTextEdit ID="txtGrandArbName" runat="server" DataMode="Text"
                         ValueText="">
                     </igtxt:WebTextEdit>
                 </td>
             </tr>
         </table>
     </td>
     <td style="width: 6%">
     </td>
     <td style="width: 47%">
         <table style="width: 100%; height: 100%">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td style="width: 90px;">
                     <asp:Label ID="lblGrandEngName" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblEngGrandNameRes"
                         Text="اسم الجد انجليزي"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <igtxt:WebTextEdit ID="txtGrandEngName" runat="server" DataMode="Text"
                          ValueText="">
                     </igtxt:WebTextEdit>
                 </td>
             </tr>
         </table>
     </td>
 </tr>

                                        <tr>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                    <asp:Label ID="lblBirthDate" runat="server" Text="تاريخ الميلاد" meta:resourcekey="lblBirthRes"
                        SkinID="Label_DefaultNormal"></asp:Label>
                </td>
                <td class="DataArea">
                        <igtxt:WebTextEdit ID="txtBirthDate" runat="server" DataMode="Text"
      ValueText="">
</igtxt:WebTextEdit>
                </td>
            </tr>
        </table>
    </td>
    <td style="width: 6%">
    </td>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                    <asp:Label ID="lblNationalityID" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblNationalityRes"
                        Text="الجنسية"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:DropDownList ID="DdlNationality" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="DdlNationalityResource1">
 </asp:DropDownList>
                </td>
            </tr>
        </table>
    </td>
</tr>

                                                                                <tr>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                     <asp:Label ID="lblBirthCitys" runat="server" SkinID="Label_DefaultNormal" Text="Birth City"
     Width="80px" meta:resourcekey="lblBirthCitysResource1"></asp:Label>
                </td>
                <td class="DataArea">
                       <asp:DropDownList ID="DdlBirthCity" runat="server" SkinID="DropDownList_LargNormal"
    meta:resourcekey="DdlBirthCityResource1">
</asp:DropDownList>
                </td>
            </tr>
        </table>
    </td>
    <td style="width: 6%">
    </td>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                    <asp:Label ID="lblReligion" runat="server" SkinID="Label_DefaultNormal" Text="Religion"
    Width="80px" meta:resourcekey="lblReligionResource1"></asp:Label>
                </td>
                <td class="DataArea">
                     <asp:DropDownList ID="DdlReligion" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="DdlReligionResource1">
 </asp:DropDownList>
                </td>
            </tr>
        </table>
    </td>
</tr>

                                                                                <tr>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                        <asp:Label ID="lblGender" runat="server" Width="80px" SkinID="Label_DefaultNormal"
        Text="Gender" meta:resourcekey="lblGenderResource1"></asp:Label>
</td>
<td class="DataArea">
    <asp:DropDownList ID="DdlGender" runat="server" SkinID="DropDownList_LargNormal"
        meta:resourcekey="DdlGenderResource1">
        <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True" Text="Male" Value="M"></asp:ListItem>
        <asp:ListItem meta:resourcekey="ListItemResource2" Text="Female" Value="F"></asp:ListItem>
    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </td>
    <td style="width: 6%">
    </td>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                        <asp:Label ID="lblMaritalStatus" runat="server" SkinID="Label_DefaultNormal" Text="Marital Status"
        Width="80px" meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
</td>
<td class="DataArea">
    <asp:DropDownList ID="DdlMaritalStatus" runat="server" SkinID="DropDownList_LargNormal"
        meta:resourcekey="DdlMaritalStatusResource1">
    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </td>
</tr>
                                                                                <tr>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                      <asp:Label ID="lblBloodGroups" runat="server" SkinID="Label_DefaultNormal" Text="Blood Group"
        Width="80px" meta:resourcekey="lblBloodGroupsResource1"></asp:Label>
</td>
<td class="DataArea">
    <asp:DropDownList ID="DdlBloodGroups" runat="server" SkinID="DropDownList_LargNormal"
        meta:resourcekey="DdlBloodGroupsResource1">
    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </td>
    <td style="width: 6%">
    </td>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                        <asp:Label ID="lblEmail" runat="server" SkinID="Label_DefaultNormal" Text="Email"
        Width="80px" meta:resourcekey="lblEmailResource1"></asp:Label>
</td>
<td class="DataArea">
    <asp:TextBox ID="txtEmail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
</tr>
                                                                                <tr>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                        <asp:Label ID="lblMobile" runat="server" Width="80px" SkinID="Label_DefaultNormal"
        Text="Mobile No" meta:resourcekey="lblMobileResource1"></asp:Label>
</td>
<td class="DataArea">
    <asp:TextBox ID="txtMobile" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtMobileResource1"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
    <td style="width: 6%">
    </td>
    <td style="width: 47%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                    <asp:Label ID="lblPassport" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Passport No." meta:resourcekey="lblPassportResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="txtPassport" runat="server" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
</tr>
                                                                               



 

                                                                                

                                    </table>
                                    <table>
                                           <tr>
    <td style="width: 100%">
        <table style="width: 100%; height: 100%">
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                     <asp:Label ID="lblAddressAsPerContract" runat="server" Width="50px" SkinID="Label_DefaultNormal"
     Text="AddressAsPerContract" meta:resourcekey="lblAddressAsPerContractResource1"></asp:Label>
                </td>
                <td class="DataArea">
                         <asp:TextBox ID="txtAddressAsPerContract" runat="server"  MaxLength="500" Width="160%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SeparArea">
                </td>
                <td style="width: 90px;">
                     <asp:Label ID="lblRemarks" runat="server" Width="50px" SkinID="Label_DefaultNormal"
     Text="Remarks" meta:resourcekey="lblRemarksResource1"></asp:Label>
                </td>
                <td class="DataArea">
                         <asp:TextBox ID="txtRemarks" runat="server" Height="50px" MaxLength="8000"
    TextMode="MultiLine" Width="160%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
</tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="البيانات التنظيمية" meta:resourcekey="TabOrganizData">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: '<%# GetAlignmentPre() %>'; width: 50%">
                                                <asp:LinkButton ID="LinkButton5" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق" meta:resourcekey="linkPrevious"></asp:LinkButton>
                                            </td>
                                                                                               <td style="height: 18px;  width: 35%">
</td>
                                            <td style="height: 18px; text-align: '<%# GetAlignmentNext() %>'; width: 15%">
                                                <asp:LinkButton ID="LinkButton6" runat="server" ValidationGroup="G2" SkinID="LinkButton_DefaultBold"
                                                    Text="التالى" meta:resourcekey="linkNext"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        
                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
    <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
        Text="Branch" Width="80px"></asp:Label>
</td>
<td style="width: 39%;">
    <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownList_LargNormal"
        AutoPostBack="True">
    </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
    <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
        SkinID="Label_DefaultNormal" Text="Department" Width="80px"></asp:Label>
</td>
<td style="width: 39%;">
    <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
        SkinID="DropDownList_LargNormal" AutoPostBack="True">
    </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                                                              <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
        <asp:Label ID="lblSectors" runat="server" Width="80px" SkinID="Label_DefaultNormal"
        Text="Sectors" meta:resourcekey="lblSectorsResource1"></asp:Label>
</td>
<td style="width: 39%;">
   <asp:DropDownList ID="ddlSectors" runat="server" SkinID="DropDownList_LargNormal">
</asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

      <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
       <asp:Label ID="Label_Location" runat="server" meta:resourcekey="lblLocationResource101"
    SkinID="Label_DefaultNormal" Text="Location" Width="80px"></asp:Label>
</td>
<td style="width: 39%;">
    <asp:DropDownList ID="DropDownList_Location" runat="server" meta:resourcekey="ddlLocationResource1"
     SkinID="DropDownList_LargNormal" AutoPostBack="True">
 </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>
                                          <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
       <asp:Label ID="LblCost1" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                        Text="Cost 1" meta:resourcekey="lblCost1Resource1"></asp:Label>
</td>
<td style="width: 39%;">
     <asp:textbox ID="TxtCostCode1" runat="server" autopostback="true"  OnTextChanged="TxtCostCode1_TextChanged">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton1" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName1" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                         <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
       <asp:Label ID="lblCostCode2" runat="server" SkinID="Label_DefaultNormal" Text="Cost 2"
                                                                        Width="80px" meta:resourcekey="lblCost2Resource1"></asp:Label>
</td>
<td style="width: 39%;">
     <asp:textbox ID="TxtCostCode2" runat="server" autopostback="true">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton2" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName2" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                        <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
      <asp:Label ID="lblCostCode3" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                        Text="Cost 3" meta:resourcekey="lblCost3Resource1"></asp:Label>
</td>
<td style="width: 39%;">
    <asp:textbox ID="TxtCostCode3" runat="server" autopostback="true">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton3" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName3" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                        <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
       <asp:Label ID="lblCostCode4" runat="server" SkinID="Label_DefaultNormal" Text="Cost 4"
                                                                        Width="80px" meta:resourcekey="lblCost4Resource1"></asp:Label>
</td>
<td style="width: 39%;">
     <asp:textbox ID="TxtCostCode4" runat="server" autopostback="true">
                                                                    </asp:textbox>
                                                                    <igtxt:WebImageButton ID="WebImageButton4" runat="server" AutoSubmit="False" Height="10px"
                                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                                            Width="20px">
                                                                                            <Alignments TextImage="ImageBottom" />
                                                                                            <Appearance>
                                                                                                <Image Url="./Img/forum_search.gif" />
                                                                                            </Appearance>
                                                                                        </igtxt:WebImageButton>
                                                                     <asp:textbox ID="TxtCostName4" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
                                                                color: Black; border: solid 1px #CCCCCC" Value="" >
                                                                    </asp:textbox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Enabled="false" Text="بيانات اخرى" meta:resourcekey="TabOtherData">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: '<%# GetAlignmentPre() %>'; width: 50%">
                                                <asp:LinkButton ID="LinkButton7" runat="server" SkinID="LinkButton_DefaultBold" Text="السابق" meta:resourcekey="linkPrevious"></asp:LinkButton>
                                            </td>
                                                                                                                                           <td style="height: 18px;  width: 35%">
</td>
                                            <td style="height: 18px; text-align: '<%# GetAlignmentNext() %>'; width: 15%">
                                                <asp:LinkButton ID="LinkButton8" runat="server" SkinID="LinkButton_DefaultBold" Text="التالى" meta:resourcekey="linkNext"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                 <table style="width: 100%; vertical-align: top" cellspacing="0">
                                     
                                                        <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
                                                            <asp:Label ID="lblManager" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Manager" meta:resourcekey="lblManagerResource1"></asp:Label>
                                                        </td>
<td>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 50%;">
                                                                        <asp:TextBox ID="txtManager" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtManagerResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 50%;">
                                                                        <igtxt:WebImageButton ID="btnManager" runat="server" AutoSubmit="False" Height="18px"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnManagerResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>&nbsp;&nbsp;
    </td>
                                                    </tr>

                                    <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblJoinDate" runat="server" Width="80px" SkinID="Label_DefaultNormal"
        Text="Join Date" meta:resourcekey="lblJoinDateResource1"></asp:Label>
</td>
<td>
    <igtxt:WebTextEdit ID="txtJoinDate" runat="server" Width="246px" >
    </igtxt:WebTextEdit>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                    <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblSponsor" runat="server" SkinID="Label_DefaultNormal" Text="Sponsor"
        Width="80px" meta:resourcekey="lblSponsorResource1"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlSponsor" runat="server" SkinID="DropDownList_LargNormal"
        meta:resourcekey="ddlSponsorResource1">
    </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                    <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
    <asp:Label ID="lblIdentity" runat="server" SkinID="Label_DefaultNormal" Text="Identity No."
        Width="80px" meta:resourcekey="lblIdentityResource1"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtIdentity" runat="server" SkinID="TextBox_LargeNormalC" onkeypress="CheckNumeric(event);"></asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                          <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
                     <asp:Label ID="lblBank" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Bank" meta:resourcekey="lblBankResource1"></asp:Label>
                </td>
                <td class="DataArea">
                         <asp:DropDownList ID="ddlBank" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
 </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>
     <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
                     <asp:Label ID="lblBankAccount" runat="server" SkinID="Label_DefaultNormal" Text="Bank Account"
     Width="80px" meta:resourcekey="lblBankAccountResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="txtBankAccount" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtBankAccountResource1"></asp:TextBox>
               </td>
    <td>&nbsp;&nbsp;
    </td>
</tr>
                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
      <asp:Label ID="BankAccountTypeLabel" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Bank Account Type" meta:resourcekey="BankAccountTypeLabelResource"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlBankAccountType" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
                                                                <asp:ListItem  meta:resourcekey="SelectOptionBankLabel" Value="0">حدد الخيار</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="RegularAccountLabel" Value="Regular">بطاقة بنك</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="SalaryAccountLabel" Value="Salary">بطاقة راتب</asp:ListItem>
                                                            </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>
                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
   <asp:Label ID="LblPaymentType" runat="server" Width="80px" SkinID="Label_DefaultNormal"
         Text="Payment Type" meta:resourcekey="LblPaymentTypeResource"></asp:Label>
</td>
<td>
     <asp:DropDownList ID="ddlLblPaymentType" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
       
     </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
   <asp:Label ID="lblMachineCode" runat="server" meta:resourcekey="lblMachineCodeResource1"
                                                                SkinID="Label_DefaultNormal" Text="Machine Code" Width="80px"></asp:Label>
</td>
<td>
      <asp:TextBox ID="TextBox_MachineCode" runat="server" meta:resourcekey="TextBox_MachineCodeResource1"
                                                                SkinID="TextBox_LargeNormalC"></asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
   <asp:Label ID="lblGosiNumber" runat="server" meta:resourcekey="lblGosiNumberResource1"
                                                                SkinID="Label_DefaultNormal" Text="GOSI Number" Width="80px"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtGosiNumber" runat="server" meta:resourcekey="txtGosiNumberResource1"
                                                                SkinID="TextBox_LargeNormalC"></asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>
                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
  <td>
   <asp:Label ID="lblGOSIJoinDate" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="GOSI Join Date" meta:resourcekey="lblGOSIJoinDateResource1"></asp:Label>
</td>
<td>
     <asp:TextBox ID="GOSIJoinDate" runat="server" SkinID="TextBox_LargeNormalC">
                                                            </asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>


                                                                         <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblLastEducations" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Last Educations" meta:resourcekey="lblLastEducationsResource1"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlLastEducations" runat="server" SkinID="DropDownList_LargNormal">
 </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                                                         <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblGraduationDate" runat="server" SkinID="Label_DefaultNormal" Text="Graduation Date"
    Width="80px" meta:resourcekey="lblGraduationDateResource1"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtGraduationDate" runat="server" SkinID="TextBox_LargeNormalC">
</asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                     <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblSSNOIssueDate" runat="server" SkinID="Label_DefaultNormal" Text="SSNOIssueDate"
    Width="80px" meta:resourcekey="lblSSNOIssueDateResource1"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtSSNOIssueDate" runat="server" SkinID="TextBox_LargeNormalC">
</asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>
                                     
                                     <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblSSNOExpireDate" runat="server" SkinID="Label_DefaultNormal" Text="SSNOExpireDate"
    Width="80px" meta:resourcekey="lblSSNOExpireDateResource1"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtSSNOExpireDate" runat="server" SkinID="TextBox_LargeNormalC">
</asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                     <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblPassportIssueDate" runat="server" SkinID="Label_DefaultNormal" Text="PassportIssueDate"
    Width="80px" meta:resourcekey="lblPassportIssueDateResource1"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtPassportIssueDate" runat="server" SkinID="TextBox_LargeNormalC">
</asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                      <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td>
    <asp:Label ID="lblPassportExpireDate" runat="server" SkinID="Label_DefaultNormal" Text="PassportExpireDate"
    Width="80px" meta:resourcekey="lblPassportExpireDateResource1"></asp:Label>
</td>
<td>
    <asp:TextBox ID="txtPassportExpireDate" runat="server" SkinID="TextBox_LargeNormalC">
</asp:TextBox>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                         
                            <igtab:Tab Enabled="false" Text="بيانات العقد الحالي" meta:resourcekey="TabContract">
                                <ContentTemplate>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 18px; text-align: '<%# GetAlignmentPre() %>'; width: 50%">
                                                <asp:LinkButton ID="LinkButton15" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="السابق" meta:resourcekey="linkPrevious"></asp:LinkButton>
                                            </td>
                                                                                                                                          <td style="height: 18px;  width: 35%">
</td>
                                            <td style="height: 18px; text-align: '<%# GetAlignmentNext() %>'; width: 15%">
                                                <asp:LinkButton ID="LinkButton16" runat="server" SkinID="LinkButton_DefaultBold"
                                                    Text="إنهاء"  meta:resourcekey="linkEnd"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="width: 100%; vertical-align: top" cellspacing="0">

                                        
                                        <tr>
    <td>&nbsp;
    </td>
   
    
   
   <td style="width: 10%;">
     <asp:Label ID="lblCode2" runat="server" SkinID="Label_DefaultNormal" Text="Contract No"
         Width="80px" meta:resourcekey="lblCode2Resource1"></asp:Label>
 </td>
 <td style="width: 39%;">
     <igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel3" runat="server" meta:resourcekey="WebAsyncRefreshPanel3Resource1">
         <table style="width: 100%;">
             <tr>
                 <td class="auto-style1">
                     <asp:Label ID="lblContractNo" runat="server" meta:resourcekey="lblNoContractNotifyResource1"
                         SkinID="Label_WarningBold"></asp:Label>
                 </td>
                 <td class="auto-style1">
                     <igtxt:WebImageButton ID="btnAddContract" runat="server" Height="18px" Overflow="NoWordWrap"
                         UseBrowserDefaults="False" Width="24px" ToolTip="Add New Contract" meta:resourcekey="btnAddContractResource1">
                         <Alignments TextImage="ImageBottom" />
                         <Appearance>
                             <Image Url="./img/abook_add_1.gif" />
                         </Appearance>
                     </igtxt:WebImageButton>


                 </td>
             </tr>
         </table>
     </igmisc:WebAsyncRefreshPanel>
 </td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                        <tr>
    <td>&nbsp;
    </td>
   
    
   
    <td style="width: 10%;">
        <asp:Label ID="lblStartWorkDate" runat="server" SkinID="Label_DefaultNormal" Text="Start Date"
            Width="80px" meta:resourcekey="lblStartDateResource1"></asp:Label>
    </td>
    <td style="width: 39%;">
        <igtxt:WebTextEdit ID="txtStartWorkDate" runat="server" >
        </igtxt:WebTextEdit>
    </td>
    <td>&nbsp;&nbsp;
    </td>
</tr>


                                         <tr>
    <td>&nbsp;
    </td>
   
    
   
    <td>
    <asp:Label ID="lblContractType" runat="server" SkinID="Label_DefaultNormal" Text="Contract Type"
        Width="80px" meta:resourcekey="lblContractTypeResource1"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlContractType" runat="server" SkinID="DropDownList_LargNormal"
        meta:resourcekey="ddlContractTypeResource1">
    </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                         <tr>
    <td>&nbsp;
    </td>
   
    
   
     <td>
     <asp:Label ID="lblProfessions" runat="server" SkinID="Label_DefaultNormal" Text="Profession"
         Width="80px" meta:resourcekey="lblProfessionsResource1"></asp:Label>
 </td>
 <td>
     <asp:DropDownList ID="ddlProfessions" runat="server" SkinID="DropDownList_LargNormal"
         meta:resourcekey="ddlProfessionsResource1">
     </asp:DropDownList>
 </td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                                                                 <tr>
    <td>&nbsp;
    </td>
   
    
   
     <td>
     <asp:Label ID="lblGradeStep" runat="server" SkinID="Label_DefaultNormal" Text="Grade Steps"
         Width="80px" meta:resourcekey="lblGradeStepResource1"></asp:Label>
 </td>
 <td>
     <asp:DropDownList ID="ddlGradeStep" runat="server" SkinID="DropDownList_LargNormal"
         meta:resourcekey="ddlGradeStepResource1">
     </asp:DropDownList>
 </td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                                                                 <tr>
    <td>&nbsp;
    </td>
   
    
   
     <td>
    <asp:Label ID="lblPosition" runat="server" SkinID="Label_DefaultNormal" Text="Position"
        Width="80px" meta:resourcekey="lblPositionResource1"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlPosition" runat="server" SkinID="DropDownList_LargNormal"
        meta:resourcekey="ddlPositionResource1">
    </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
    </td>
</tr>

                                                                                 <tr>
    <td>&nbsp;
    </td>
   
    
   
    <td>
    <asp:Label ID="lblEmployeeClass" runat="server" SkinID="Label_DefaultNormal" Text="Employee Class"
        Width="80px" meta:resourcekey="lblEmployeeClassResource1"></asp:Label>
</td>
<td>
    <asp:DropDownList ID="ddlEmployeeClass" runat="server" SkinID="DropDownList_LargNormal"
        meta:resourcekey="ddlEmployeeClassResource1">
    </asp:DropDownList>
</td>
    <td>&nbsp;&nbsp;
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
