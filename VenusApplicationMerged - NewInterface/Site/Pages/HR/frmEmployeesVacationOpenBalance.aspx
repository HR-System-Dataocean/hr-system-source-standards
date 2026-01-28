<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesVacationOpenBalance.aspx.vb"
    Inherits="frmEmployeesSelector" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

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
    <title>* Venus Payroll * ~Employees Selector</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_frmEmployeeVacationOpenBalance.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        function ddlDepartment_Change() {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var ddlDepartment = igtab_getElementById("ddlDepartment", ultraTab.element);
            PageMethods.GetRelatedDepartment(ddlDepartment.value, OnSucceeded, OnFailed);
        }

        function OnSucceeded(result, userContext, methodName) {
            if (methodName == 'GetRelatedDepartment') {
                var ultraTab = igtab_getTabById("UltraWebTab1");
                var ddlBranch = igtab_getElementById("ddlBranche", ultraTab.element);
                ddlBranch.outerHTML = result;
            }
        }

        function OnFailed(error) {
            alert();
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
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmEmployeesSelector" runat="server" defaultbutton="ImageButton1">
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= ImageButton_Save.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= LinkButton_Save.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ImageButton_Delete.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= LinkButton_Delete.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $('#<%= ImageButton1.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
        });
    </script>
    <div style="display: none">
        <igtxt:WebTextEdit ID="txtEmployee" runat="server" Text="00" meta:resourcekey="txtEmployeeResource1">
            <ClientSideEvents KeyDown="txtEmployee_KeyDown" />
        </igtxt:WebTextEdit>
        <asp:HiddenField ID="txtLang" runat="server" Value="Eng" />
        <asp:HiddenField ID="hdnFiscalDays" runat="server" Value="0" />
        <asp:TextBox ID="CodeCode" runat="server" Width="52px" Visible="False" meta:resourcekey="CodeCodeResource1"></asp:TextBox>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
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
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Save" Width="13px" Height="13px" runat="server"
                                    CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1" ImageUrl="~/Pages/HR/Img/save.gif" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Save" runat="server" Text="Save" CommandArgument="Save"
                                    meta:resourcekey="LinkButton_SaveResource1" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                    CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" ImageUrl="~/Pages/HR/Img/logoff_small.gif" />
                            </td>
                            <td style="width: 100px">
                                <asp:LinkButton ID="LinkButton_Delete" runat="server" Text="Delete" CommandArgument="Delete"
                                    meta:resourcekey="LinkButton_DeleteResource1" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal;"></asp:LinkButton>
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
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 42px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource1" />
                            </td>
                            <td style="vertical-align: middle">
                                <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:Label>
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
                                                            <asp:Label ID="lblCode" runat="server" Text="Employees Code" Width="90px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
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
                                                            <asp:Label ID="lblVacationType" runat="server" SkinID="Label_DefaultNormal" Text="Vacation Type"
                                                                Width="90px" meta:resourcekey="lblVacationTypeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlVacationType" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlVacationTypeResource1">
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
                                                            <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                                SkinID="Label_DefaultNormal" Text="Department" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
                                                                SkinID="DropDownList_LargNormal">
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
                                                            <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                Text="Branch" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBranche" runat="server" meta:resourcekey="ddlBranchResource1"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Please select employees" SkinID="Label_DefaultBold"
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
                                                    font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="btnFindRes"
                                                    Overflow="NoWordWrap" Text=" Search " UseBrowserDefaults="False" Width="80px">
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
                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UWGEmployeesProjects" runat="server" EnableAppStyling="True"
                                                    Height="100%" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                    Width="100%">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended"
                                                        StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                        Version="4.00" ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
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
                                                            <Columns>
                                                                <igtbl:UltraGridColumn Key="ID" Width="60px" BaseColumnName="ID" Hidden="True" meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn Key="EmployeeID" Width="60px" BaseColumnName="EmployeeID"
                                                                    Hidden="True" meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header Caption="EmployeeID">
                                                                        <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn EditorControlID="txtEmployee" AllowUpdate="No" Key="Employees"
                                                                    Type="Custom" Width="100px" BaseColumnName="EmployeeCode" meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="Code">
                                                                        <RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" Key="EmployeesName" Width="100%" BaseColumnName="EmployeesName"
                                                                    meta:resourcekey="UltraGridColumnResource4">
                                                                    <CellStyle Font-Names="Tahoma" Font-Size="8pt">
                                                                    </CellStyle>
                                                                    <Header Caption="Employee Name">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="PeriodID" Key="PeriodID" Width="120px" meta:resourcekey="UltraGridColumnResource5">
                                                                    <Header Caption="Period">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" DataType="System.int" BaseColumnName="Days"
                                                                    Key="Days" Format="###,###,###.##" Width="110px" meta:resourcekey="UltraGridColumnResource6"
                                                                   >
                                                                    <Header Caption="Deserved Days">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Right">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5"></RowLayoutColumnInfo>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="Yes" DataType="System.int" BaseColumnName="OverDue"
                                                                    Key="OverDue" Format="###,###,###" Width="110px" meta:resourcekey="UltraGridColumnResource7"
                                                                    Hidden="false">
                                                                    <Header Caption="OverDue Days">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Right">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6"></RowLayoutColumnInfo>
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="Yes"  DataType="System.Double" BaseColumnName="VacationBalance"
                                                                    Key="VacationBalance" Format="###,###,###.##" Width="130px" meta:resourcekey="UltraGridColumnResource8"
                                                                    Hidden="True">
                                                                    <Header Caption="Vacation Balance">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Right" Font-Bold="False">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7"></RowLayoutColumnInfo>
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
