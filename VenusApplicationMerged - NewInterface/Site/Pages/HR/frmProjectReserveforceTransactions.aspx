<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProjectReserveforceTransactions.aspx.vb"
    Inherits="frmProjectReserveforceTransactions" Culture="auto" UICulture="auto"  %>

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
    <title>* Venus Payroll * ~ frmProjectReserveforceTransactions</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        var IsEdit = true;

        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            var ctrId = window.document.getElementById("txtCode");
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
            else {
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
        function CloseIt() {
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
    <script type="text/javascript" id="Script2">
        function uwg_ClickCellButtonHandler(gridName, cellId) {
            var cell = igtbl_getCellById(cellId);
            if (cell.Index == 3) {
                var Row = cell.getRow();
                var cellID = Row.getCellFromKey("ID")
                var currProjectEmployee = cellID.getValue()
                if (currProjectEmployee != null)
                    OpenModal1('frmProjectReserveforceTransfeer.aspx?ID=' + currProjectEmployee, 500, 1000, true, '');
                return false;
            }
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmProjectReserveforceTransactions" runat="server" defaultbutton="ImageButton1">
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= ddlBranche.ClientID%>').change(function () {
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
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" ></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" ></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" ></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" 
            Width="99px" ></asp:Label>
        <asp:Label ID="lblLage" runat="server" ></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" 
                                    CommandArgument="N"  />
                            </td>
                            <td style="width: 20px">
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
                                    SkinID="HrHelp_Command"  />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;" 
                                    ></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 42px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" 
                                    ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png" 
                                     />
                            </td>
                            <td style="vertical-align: middle">
                                <asp:Label ID="Label_Header" runat="server" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal;" >نموذج حركات موظفى المشاريع</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" 
                        SkinID="Default" >
                        <Tabs>
                            <igtab:Tab Text="عام" >
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
                                                            <asp:Label ID="lblCode" runat="server" Text="الرقم الوظيفى" Width="90px" 
                                                                SkinID="Label_DefaultNormal" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCode" runat="server" MaxLength="30" 
                                                                            SkinID="TextBox_LargeNormalC" ></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" 
                                                                            >
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
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblBranch" runat="server" SkinID="Label_DefaultNormal" Text="الفرع"
                                                                Width="90px" ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBranche" runat="server" AutoPostBack="True" 
                                                                SkinID="DropDownList_LargNormal" >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="SeparArea">
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
                                                            <asp:Label ID="Label_Title1" runat="server" Text="برجاء اختيار موظف من القائمة" 
                                                                SkinID="Label_DefaultBold" ></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: middle; text-align: center;">
                                            </td>
                                            <td style="width: 47%; vertical-align: middle;">
                                                <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" Style="font-family: Tahoma;
                                                    font-size: 8pt; font-weight: Normal; color: Black" Overflow="NoWordWrap" Text="عرض"
                                                    UseBrowserDefaults="False" Width="80px" 
                                                    >
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
                                                <igtbl:UltraWebGrid    Browser="UpLevel"  ID="UwgSearchEmployees" runat="server" EnableAppStyling="True"
                                                    Height="280px" SkinID="Default" Width="325px" 
                                                    >
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="280px"
                                                            Width="325px">
                                                        </FrameStyle>
                                                        <ClientSideEvents ClickCellButtonHandler="uwg_ClickCellButtonHandler" />
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
                                                        <igtbl:UltraGridBand AllowSorting="No" 
                                                            >
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True" 
                                                                    >
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code" 
                                                                    Width="100px" >
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
                                                                    Width="100%" >
                                                                    <Header Caption="إسم الموظف">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn CellButtonDisplay="Always" Type="Button" Width="100px" 
                                                                    >
                                                                    <Header Caption="التحركات">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <CellButtonStyle BackgroundImage="~/Pages/HR/Img/cal_date_picker.gif" Height="20px"
                                                                        HorizontalAlign="Center" Width="23px">
                                                                    </CellButtonStyle>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <SelectedCellStyle HorizontalAlign="Center">
                                                                    </SelectedCellStyle>
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
