<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjectAttendanceAudit.aspx.vb"
    Inherits="frmProjectAttendanceAudit" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ frmProjectAttendanceAudit</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
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
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjectAttendanceAudit" runat="server" defaultbutton="ImageButton1">
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= ImageButton_Prepare.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= LinkButton_Prepare.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ImageButton1.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });
    </script>
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="realnameResource1"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 18px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Prepare" Width="14px" Height="12px" runat="server"
                                    CommandArgument="Prepare" ImageUrl="~/Pages/HR/Img/save.gif" meta:resourcekey="ImageButton_PrepareResource1" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Prepare" runat="server" Text="إعتماد التعديلات" CommandArgument="Prepare"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;" meta:resourcekey="LinkButton_PrepareResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 10px;">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
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
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                                                    <asp:Label ID="lblCode" runat="server" Text="بيانات المشرف" Width="90px" SkinID="Label_DefaultNormal"
                                                                        meta:resourcekey="lblCodeResource1"></asp:Label>
                                                                </td>
                                                                <td class="DataArea">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="lblDesc" runat="server" SkinID="Label_DefaultNormal" Text="بيانات المشرف"
                                                                                    meta:resourcekey="lblDescResource1"></asp:Label>
                                                                                <asp:CheckBox ID="CheckBox_Confirmed" runat="server" Font-Bold="True" ForeColor="#CC3300"
                                                                                    Text="تم إعتماد الإدخالات" />
                                                                            </td>
                                                                            <td style="width: 25px;">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 47%; height: 16px; vertical-align: top">
                                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                            <tr>
                                                                <td class="SeparArea">
                                                                </td>
                                                                <td class="LabelArea">
                                                                    <asp:Label ID="lblCode1" runat="server" SkinID="Label_DefaultNormal" Text="التاريخ"
                                                                        Width="90px" meta:resourcekey="lblCode1Resource1"></asp:Label>
                                                                </td>
                                                                <td class="DataArea">
                                                                    <igtxt:WebMaskEdit ID="txtDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default"
                                                                        AutoPostBack="True" meta:resourcekey="txtDateResource1">
                                                                    </igtxt:WebMaskEdit>
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
                                                                    <asp:Label ID="lblProject" runat="server" SkinID="Label_DefaultNormal" Text="إختر المشروع"
                                                                        Width="90px" meta:resourcekey="lblProjectResource1"></asp:Label>
                                                                </td>
                                                                <td class="DataArea">
                                                                    <asp:DropDownList ID="ddlProject" runat="server" SkinID="DropDownList_LargNormal"
                                                                        AutoPostBack="True" meta:resourcekey="ddlProjectResource1">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 47%; height: 16px; vertical-align: top">
                                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                            <tr>
                                                                <td class="SeparArea">
                                                                </td>
                                                                <td class="LabelArea">
                                                                    <asp:Label ID="lblLocations" runat="server" SkinID="Label_DefaultNormal" Text="إختر الموقع"
                                                                        Width="90px" meta:resourcekey="lblLocationsResource1"></asp:Label>
                                                                </td>
                                                                <td class="DataArea">
                                                                    <asp:DropDownList ID="ddlLocation" runat="server" SkinID="DropDownList_LargNormal"
                                                                        AutoPostBack="True" meta:resourcekey="ddlLocationResource1">
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
                                                                    <asp:Label ID="Label_Shift" runat="server" SkinID="Label_DefaultNormal" Text="إختر الوردية"
                                                                        Width="90px" meta:resourcekey="Label_ShiftResource1"></asp:Label>
                                                                </td>
                                                                <td class="DataArea">
                                                                    <asp:DropDownList ID="DropDownList_Shift" runat="server" SkinID="DropDownList_LargNormal"
                                                                        AutoPostBack="True" meta:resourcekey="DropDownList_ShiftResource1">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="SeparArea" style="height: 16px">
                                                                </td>
                                                                <td class="LabelArea" style="height: 16px">
                                                                    <asp:Label ID="Label_EmpCode" runat="server" SkinID="Label_DefaultNormal" Text="الرقم الوظيفى"
                                                                        Width="90px" meta:resourcekey="Label_EmpCodeResource1"></asp:Label>
                                                                </td>
                                                                <td class="DataArea" style="height: 16px">
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="txtCode" runat="server" MaxLength="30" meta:resourcekey="txtCodeResource1"
                                                                                    SkinID="TextBox_LargeNormalC"></asp:TextBox>
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
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 6%; height: 16px; vertical-align: top">
                                                        &nbsp;
                                                    </td>
                                                    <td style="width: 47%; height: 16px; vertical-align: top">
                                                        <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                            <tr>
                                                                <td class="SeparArea">
                                                                </td>
                                                                <td class="LabelArea">
                                                                    <asp:Button ID="Button1" runat="server" BackColor="#FFCC00" BorderStyle="Solid" BorderWidth="1px"
                                                                        Width="30px" />
                                                                </td>
                                                                <td class="DataArea">
                                                                    <asp:Label ID="Label_EmpDayOff" runat="server" meta:resourcekey="Label_EmpDayOffResource1"
                                                                        SkinID="Label_DefaultNormal" Text="حركات المسجلة فى النظام" Width="240px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="SeparArea">
                                                                </td>
                                                                <td class="LabelArea">
                                                                    <asp:Button ID="Button2" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                        Width="30px" />
                                                                </td>
                                                                <td class="DataArea">
                                                                    <asp:Label ID="Label_EmpEntered" runat="server" meta:resourcekey="Label_EmpEnteredResource1"
                                                                        SkinID="Label_DefaultNormal" Text="الحركات الغير مسجلة فى النظام" Width="224px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 47%; height: 30px; vertical-align: top;">
                                                        <table style="width: 100%; height: 20px; vertical-align: bottom; border-bottom: 1px solid black"
                                                            cellspacing="6">
                                                            <tr>
                                                                <td style="vertical-align: bottom">
                                                                    <asp:Label ID="Label_Title1" runat="server" Text="برجاء إدخال دوامات الموظفين" SkinID="Label_DefaultBold"
                                                                        meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="width: 6%; vertical-align: middle; text-align: center;">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td style="width: 47%; vertical-align: middle;">
                                                        <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" Style="font-family: Tahoma;
                                                            font-size: 8pt; font-weight: Normal; color: Black" Overflow="NoWordWrap" Text=" عرض "
                                                            UseBrowserDefaults="False" Width="80px" meta:resourcekey="btnFindResource1">
                                                            <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                            <Appearance>
                                                                <Image Url="./img/forum_search.gif" />
                                                                <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                                    ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                                    WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                            </Appearance>
                                                        </igtxt:WebImageButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="vertical-align: top" colspan="3">
                                                        <igtbl:UltraWebGrid   Browser="UpLevel"   ID="UwgSearchEmployees" runat="server" EnableAppStyling="True"
                                                            Height="280px" SkinID="Default" Width="325px">
                                                            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowUpdateDefault="Yes"
                                                                AutoGenerateColumns="False" BorderCollapseDefault="Separate" Name="uwgForNationality"
                                                                RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                                StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                                AllowRowNumberingDefault="Continuous">
                                                                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="280px"
                                                                    Width="325px">
                                                                </FrameStyle>
                                                                <ClientSideEvents ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler" />
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
                                                                <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
                                                                    <AddNewRow View="NotSet" Visible="NotSet">
                                                                    </AddNewRow>
                                                                    <FilterOptions EmptyString="" AllString="" NonEmptyString="">
                                                                        <FilterDropDownStyle BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" Font-Size="11px"
                                                                            Font-Names="Verdana,Arial,Helvetica,sans-serif" BackColor="SteelBlue" Width="200px"
                                                                            CustomRules="overflow:auto;">
                                                                            <Padding Left="2px"></Padding>
                                                                        </FilterDropDownStyle>
                                                                        <FilterHighlightRowStyle ForeColor="White" BackColor="#151C55">
                                                                        </FilterHighlightRowStyle>
                                                                    </FilterOptions>
                                                                    <Columns>
                                                                        <igtbl:UltraGridColumn Width="80px" AllowUpdate="No" BaseColumnName="Marked" Key="Marked"
                                                                            Type="CheckBox" meta:resourcekey="UltraGridColumnResource0">
                                                                            <Header Caption="مؤشر">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True">
                                                                            <Header Caption="ID">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code" Width="20%"
                                                                            meta:resourcekey="UltraGridColumnResource2">
                                                                            <Header Caption="الرقم الوظيفى">
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Header>
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="1" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="EmployeeName" Key="EmployeeName"
                                                                            Width="30%" meta:resourcekey="UltraGridColumnResource3">
                                                                            <Header Caption="إسم الموظف">
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Header>
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="2" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn Width="100px" AllowUpdate="Yes" BaseColumnName="Absent" Key="Absent"
                                                                            Type="CheckBox" meta:resourcekey="UltraGridColumnResource1">
                                                                            <Header Caption="غياب">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn Width="100px" AllowUpdate="Yes" BaseColumnName="Attend" Key="Attend"
                                                                            Type="CheckBox" meta:resourcekey="UltraGridColumnResource4">
                                                                            <Header Caption="حضور">
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="3" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn Width="100px" AllowUpdate="Yes" BaseColumnName="Sick" Key="Sick"
                                                                            Type="CheckBox" meta:resourcekey="UltraGridColumnResource5">
                                                                            <Header Caption="مرضى">
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="4" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn Width="100px" AllowUpdate="Yes" BaseColumnName="Leave" Key="Leave"
                                                                            Type="CheckBox" meta:resourcekey="UltraGridColumnResource6">
                                                                            <Header Caption="إنسحاب">
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="5" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn Width="100px" AllowUpdate="No" BaseColumnName="DayOff" Key="DayOff"
                                                                            Type="CheckBox" meta:resourcekey="UltraGridColumnResource7">
                                                                            <Header Caption="راحة">
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="6" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Delay" Key="Delay" Width="150px" DataType="System.Int32"
                                                                            meta:resourcekey="UltraGridColumnResource8">
                                                                            <Header Caption="دقائق التأخير">
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="7" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="Overtime" Key="Overtime" Width="150px" DataType="System.Int32"
                                                                            meta:resourcekey="UltraGridColumnResource9">
                                                                            <Header Caption="دقائق الإضافى">
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Header>
                                                                            <HeaderStyle HorizontalAlign="Center" />
                                                                            <CellStyle HorizontalAlign="Center">
                                                                            </CellStyle>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="8" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="RefID" Key="RefID" Hidden="True" meta:resourcekey="UltraGridColumnResource10">
                                                                            <Header Caption="RefID">
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                        <igtbl:UltraGridColumn BaseColumnName="ShiftID" Key="ShiftID" Hidden="true" meta:resourcekey="UltraGridColumnResource11">
                                                                            <Header Caption="ShiftID">
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Header>
                                                                            <Footer>
                                                                                <RowLayoutColumnInfo OriginX="9" />
                                                                            </Footer>
                                                                        </igtbl:UltraGridColumn>
                                                                    </Columns>
                                                                </igtbl:UltraGridBand>
                                                            </Bands>
                                                        </igtbl:UltraWebGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 100%" colspan="3">
                                                        <asp:HiddenField ID="CLocation" runat="server" />
                                                        <asp:HiddenField ID="CShift" runat="server" />
                                                        <asp:HiddenField ID="CProject" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
