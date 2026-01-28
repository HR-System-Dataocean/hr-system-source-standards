<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSignature.aspx.vb" Inherits="frmPictures"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmPictures" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV" style="background-color: #BEDCFF;
        width: 438px; height: 447px;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;&nbsp;
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource1" />
                                <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 212px; vertical-align: top;" cellspacing="0">
                        <tr>
                            <td style="width: 1%;">
                            </td>
                            <td style="width: 98%;">
                                <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                                    meta:resourcekey="UltraWebTab1Resource1" Height="150px" Width="100%">
                                    <Tabs>
                                        <igtab:Tab Text="خصائص الصورة" meta:resourcekey="TabResource1">
                                            <ContentTemplate>
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td style="height: 23px;">
                                                            <asp:Label ID="Label10" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="الاسم" meta:resourcekey="Label10Resource1"></asp:Label>
                                                        </td>
                                                        <td style="vertical-align: top;" rowspan="4">
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 1%;">
                                                                    </td>
                                                                    <td style="width: 99%;">
                                                                        <asp:Image ID="ImgEmployee" runat="server" Height="219px" Width="228px" 
                                                                            meta:resourcekey="ImgEmployeeResource1" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td style="height: 23px;">
                                                            <asp:Label ID="lblName" runat="server" SkinID="Label_DefaultNormal" 
                                                                meta:resourcekey="lblNameResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 20px">
                                                        </td>
                                                        <td style="width: 38%; vertical-align: top;" rowspan="2">
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgDocumentPictures" runat="server" EnableAppStyling="False"
                                                                Height="150px" SkinID="Default" Width="100%" 
                                                                meta:resourcekey="uwgDocumentPicturesResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                                    BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                                    RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                                                    StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                                                    Version="4.00" ViewType="OutlookGroupBy">
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
                                                                    <ClientSideEvents AfterSelectChangeHandler="uwgDocumentPictures_AfterSelectChangeHandler" />
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
                                                                            <igtbl:UltraGridColumn AllowRowFiltering="False" BaseColumnName="ID"
                                                                                Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                                <Header Caption="ID">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ObjectID" Hidden="True" Key="ObjectID" 
                                                                                meta:resourcekey="UltraGridColumnResource2">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="RecordID" Hidden="True" Key="RecordID" 
                                                                                meta:resourcekey="UltraGridColumnResource3">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="EngName" Hidden="True" Key="EngName" 
                                                                                meta:resourcekey="UltraGridColumnResource4">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ArbName" Hidden="True" Key="ArbName" 
                                                                                meta:resourcekey="UltraGridColumnResource5">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="FolderName" Hidden="True"
                                                                                Key="FolderName" meta:resourcekey="UltraGridColumnResource6">
                                                                                <Header Caption="FolderName">
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="FileName" Key="FileName" Width="50%" 
                                                                                meta:resourcekey="UltraGridColumnResource7">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExpiryDate" Hidden="True" 
                                                                                Key="ExpiryDate" meta:resourcekey="UltraGridColumnResource8">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IsProfilePicture" Hidden="True" 
                                                                                Key="IsProfilePicture" meta:resourcekey="UltraGridColumnResource9">
                                                                                <Header>
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IsImageView" Hidden="True" 
                                                                                Key="IsImageView" meta:resourcekey="UltraGridColumnResource10">
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
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 20px">
                                                        </td>
                                                        <td style="width: 38%; height: 23px;">
                                                        </td>
                                                        <td colspan="2">
                                                            <table style="width: 100%; height: 23px">
                                                                <tr>
                                                                    <td style="width: 15%; text-align: center;">
                                                                        <igtxt:WebImageButton ID="btnLast" runat="server" AutoSubmit="False" Height="15px"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="30px" 
                                                                            meta:resourcekey="btnLastResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="~/Pages/HR/Img/i.p.lastpg.gif" />
                                                                                <ButtonStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                                                    Cursor="Hand" Font-Bold="True">
                                                                                </ButtonStyle>
                                                                            </Appearance>
                                                                            <HoverAppearance>
                                                                                <ButtonStyle BackColor="Silver" Cursor="Hand">
                                                                                </ButtonStyle>
                                                                            </HoverAppearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                    <td style="width: 15%; text-align: center;">
                                                                        <igtxt:WebImageButton ID="btnNext" runat="server" AutoSubmit="False" Height="15px"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="30px" 
                                                                            meta:resourcekey="btnNextResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="~/Pages/HR/Img/i.p.nextpg.gif" />
                                                                                <ButtonStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                                                    Cursor="Hand" Font-Bold="True">
                                                                                </ButtonStyle>
                                                                            </Appearance>
                                                                            <HoverAppearance>
                                                                                <ButtonStyle BackColor="Silver" Cursor="Hand">
                                                                                </ButtonStyle>
                                                                            </HoverAppearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                    <td style="width: 40%;">
                                                                    </td>
                                                                    <td style="width: 15%; text-align: center;">
                                                                        <igtxt:WebImageButton ID="btnPrevious" runat="server" AutoSubmit="False" Height="15px"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="30px" 
                                                                            meta:resourcekey="btnPreviousResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="~/Pages/HR/Img/i.p.backpg.gif" />
                                                                                <ButtonStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                                                    Cursor="Hand" Font-Bold="True">
                                                                                </ButtonStyle>
                                                                            </Appearance>
                                                                            <HoverAppearance>
                                                                                <ButtonStyle BackColor="Silver" Cursor="Hand">
                                                                                </ButtonStyle>
                                                                            </HoverAppearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                    <td style="width: 15%; text-align: center;">
                                                                        <igtxt:WebImageButton ID="btnFirst" runat="server" AutoSubmit="False" Height="15px"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="30px" 
                                                                            meta:resourcekey="btnFirstResource1">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="~/Pages/HR/Img/i.p.firstpg.gif" />
                                                                                <ButtonStyle BorderColor="White" BorderStyle="Solid" BorderWidth="1px" 
                                                                                    Cursor="Hand" Font-Bold="True">
                                                                                </ButtonStyle>
                                                                            </Appearance>
                                                                            <HoverAppearance>
                                                                                <ButtonStyle BackColor="Silver" Cursor="Hand">
                                                                                </ButtonStyle>
                                                                            </HoverAppearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 25px">
                                                        </td>
                                                        <td style="width: 38%; height: 23px;">
                                                            <asp:Label ID="lblPhotoPath" runat="server" Text="الصورة" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="lblPhotoPathResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 55%;">
                                                            <asp:FileUpload ID="txtAttached" runat="server" Width="100%" 
                                                                meta:resourcekey="txtAttachedResource1" />
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%; height: 25px">
                                                        </td>
                                                        <td style="width: 38%; height: 23px;">
                                                            <asp:Label ID="lblExpireDate" runat="server" Text="تاريخ الانتهاء" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="lblExpireDateResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 55%;">
                                                            <igsch:WebDateChooser ID="txtExpireDate" runat="server" Width="130px" 
                                                                meta:resourcekey="txtExpireDateResource1">
                                                            </igsch:WebDateChooser>
                                                        </td>
                                                        <td style="width: 1%;">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 1%;">
                                                        </td>
                                                        <td style="width: 38%; height: 23px;">
                                                            <asp:Label ID="lblIsDefault" runat="server" Text="صورة الافتراضية" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="lblIsDefaultResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 55%;">
                                                            <asp:CheckBox ID="chkIsDefault" runat="server" 
                                                                meta:resourcekey="chkIsDefaultResource1" />
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
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:HiddenField ID="txtID" runat="server" />
                                                <asp:HiddenField ID="txtPhotoName" runat="server" />
                                            </ContentTemplate>
                                        </igtab:Tab>
                                    </Tabs>
                                </igtab:UltraWebTab>
                            </td>
                            <td style="width: 1%;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <table style="width: 100%; height: 30px; vertical-align: top" cellspacing="0">
                <tr>
                    <td style="width: 25%; text-align: center;">
                        <igtxt:WebImageButton ID="btnCancel" runat="server" ClickOnEnterKey="False" ClickOnSpaceKey="False"
                            Height="18px" Text="إلغاء" UseBrowserDefaults="False" 
                            meta:resourcekey="btnCancelResource1">
                            <RoundedCorners HeightOfBottomEdge="0" MaxHeight="22" MaxWidth="200" RenderingType="FileImages"
                                WidthOfRightEdge="5" />
                            <Appearance>
                                <Image Url="./Img/logoff_small.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
                    </td>
                    <td style="width: 25%; text-align: center;">
                        <igtxt:WebImageButton ID="btnSave" runat="server" ClickOnEnterKey="False" ClickOnSpaceKey="False"
                            Height="18px" Text="حفظ" UseBrowserDefaults="False" 
                            meta:resourcekey="btnSaveResource1">
                            <RoundedCorners HeightOfBottomEdge="0" MaxHeight="22" MaxWidth="200" RenderingType="FileImages"
                                WidthOfRightEdge="5" />
                            <Appearance>
                                <Image Url="./Img/save.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
                    </td>
                    <td style="width: 50%;">
                    </td>
                </tr>
            </table>
        </table>
    </div>
    </form>
</body>
</html>
