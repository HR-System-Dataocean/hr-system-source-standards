<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAttachDocuments.aspx.vb"
    Inherits="frmAttachDocuments" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Pictures</title>
    <script language="javascript" src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_Pictures.js" type="text/javascript"></script>
</head>
<body style="margin: 0; padding: 0;">
    <form id="frmAttachDocuments" runat="server">
    <script type="text/javascript">
        function uwgDocumentPictures_CellClickHandler(gridName, cellId, button) {
            var grid = igtbl_getGridById(gridName);
            var cell = igtbl_getCellById(cellId);
            var Row = igtbl_getRowById(cellId);
            var ss
            
            var link = document.getElementById("<%= lnkDownload.ClientID %>");
            if (document.getElementById("<%= HiddenField1.ClientID %>").value == 0) {
                link.href = "../../Uploads/" + document.getElementById("<%= txtObject.ClientID %>").value + "_" + document.getElementById("<%= txtRecord.ClientID %>").value + "/" + Row.getCell(6).getValue();
            }
            else {
                link.href = "../../Uploads/" + Row.getCell(6).getValue();
            }
        }
    </script>
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="txtObject" runat="server" />
        <asp:HiddenField ID="txtRecord" runat="server" />
    </div>
    <table style="width: 100%; vertical-align: top" cellspacing="0">
        <tr>
            <td style="width: 1%; height: 25px">
            </td>
            <td style="width: 38%; height: 23px;">
                <asp:Label ID="lblPhotoPath" runat="server" Text="الصورة" SkinID="Label_DefaultNormal"
                    meta:resourcekey="lblPhotoPathResource1"></asp:Label>
            </td>
            <td style="width: 30%;" colspan="2">
                <asp:FileUpload ID="txtAttached" runat="server" Width="100%" meta:resourcekey="txtAttachedResource1" />
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
        <tr>
            <td style="width: 1%; height: 25px">
            </td>
            <td style="height: 23px;" colspan="3">
                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgDocumentPictures" runat="server" EnableAppStyling="True"
                    meta:resourcekey="uwgDocumentPicturesResource1" SkinID="Default" Width="100%">
                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                        AllowRowNumberingDefault="Continuous">
                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Width="100%">
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
                        <ClientSideEvents AfterSelectChangeHandler="uwgDocumentPictures_AfterSelectChangeHandler"
                            CellClickHandler="uwgDocumentPictures_CellClickHandler" />
                    </DisplayLayout>
                    <Bands>
                        <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
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
                                <igtbl:UltraGridColumn AllowRowFiltering="False" BaseColumnName="ID" Hidden="True"
                                    Key="ID">
                                    <Header Caption="ID">
                                    </Header>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="ObjectID" Hidden="True" Key="ObjectID">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="1" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="1" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="RecordID" Hidden="True" Key="RecordID">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="2" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="2" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="EngName" Hidden="False" Key="EngName">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="3" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="3" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="ArbName" Hidden="True" Key="ArbName">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="4" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="4" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="FolderName" Hidden="true" Key="FolderName">
                                    <Header Caption="FolderName">
                                        <RowLayoutColumnInfo OriginX="5" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="5" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="FileName" Key="FileName" Width="100%">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="6" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="6" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="ExpiryDate" Hidden="True" Key="ExpiryDate">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="7" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="7" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="IsProfilePicture" Hidden="True" Key="IsProfilePicture">
                                    <Header>
                                        <RowLayoutColumnInfo OriginX="8" />
                                    </Header>
                                    <Footer>
                                        <RowLayoutColumnInfo OriginX="8" />
                                    </Footer>
                                </igtbl:UltraGridColumn>
                                <igtbl:UltraGridColumn BaseColumnName="IsImageView" Hidden="True" Key="IsImageView">
                                    <Header>
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
            <td style="width: 1%;">
            </td>
        </tr>
        <tr>
            <td style="width: 1%;">
            </td>
            <td style="width: 38%; height: 23px;">
                <igtxt:WebImageButton ID="btnCancel" runat="server" ClickOnEnterKey="False" ClickOnSpaceKey="False"
                    Height="18px" meta:resourcekey="btnCancelResource1" Text="إلغاء" UseBrowserDefaults="False">
                    <RoundedCorners HeightOfBottomEdge="0" MaxHeight="22" MaxWidth="200" RenderingType="FileImages"
                        WidthOfRightEdge="5" />
                    <Appearance>
                        <Image Url="./Img/logoff_small.gif" />
                    </Appearance>
                </igtxt:WebImageButton>
                <asp:HiddenField ID="txtID" runat="server" />
            </td>
            <td style="width: 30%;">
                <igtxt:WebImageButton ID="btnSave" runat="server" ClickOnEnterKey="False" ClickOnSpaceKey="False"
                    Height="18px" meta:resourcekey="btnSaveResource1" Text="حفظ" UseBrowserDefaults="False">
                    <RoundedCorners HeightOfBottomEdge="0" MaxHeight="22" MaxWidth="200" RenderingType="FileImages"
                        WidthOfRightEdge="5" />
                    <Appearance>
                        <Image Url="./Img/save.gif" />
                    </Appearance>
                </igtxt:WebImageButton>
                <asp:HiddenField ID="txtPhotoName" runat="server" />
            </td>
            <td style="width: 25%;">
                <asp:HyperLink ID="lnkDownload" runat="server" meta:resourcekey="btnshowResource1"
                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
                    Target="_blank" Text="Download Attached" Width="95px"></asp:HyperLink>
            </td>
            <td style="width: 1%;">
            </td>
        </tr>
        <tr>
            <td style="width: 1%;">
            </td>
            <td style="width: 38%; height: 16px;">
            </td>
            <td style="width: 55%;">
            </td>
            <td style="width: 1%;">
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
