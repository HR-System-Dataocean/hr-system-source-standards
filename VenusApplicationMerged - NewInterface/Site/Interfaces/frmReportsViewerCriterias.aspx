<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReportsViewerCriterias.aspx.vb"
    Inherits="Interfaces_frmReportsViewerCriterias" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Criteria Screen </title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <link href="app_styles.css" type="text/css" rel="stylesheet" />
    <script src="App_RpwScripts.js" type="text/javascript"></script>
    <script src="../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var ODialoge;
        var OSender;
        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
           
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
    <script id="igClientScript" type="text/javascript">
        function btnCancel_Click(oButton, oEvent) {
            window.close();
        }
        function btnExportExcel_Click(oButton, oEvent) {
            PageMethods.SetSessione("XLS", OnSucceeded, OnFailed);
        }
        function btnExportWord_Click(oButton, oEvent) {
            PageMethods.SetSessione("RTF", OnSucceeded, OnFailed);
        }
        function btnExportPDF_Click(oButton, oEvent) {
            PageMethods.SetSessione("PDF", OnSucceeded, OnFailed);
        }
        function OnSucceeded(result, userContext, methodName) {
           
            btnCriteriaDisplay_Click();
        }
        function OnFailed(error) {
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="left: 0px; width: 732px; position: absolute; top: 0px; height: 499px;
        background-color: #bfdbff">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <igtab:UltraWebTab ID="UltraWebTab1" runat="server" BorderColor="#BFCDDB" Font-Bold="False"
            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
            Height="415px" meta:resourcekey="UltraWebTab1Resource2" Style="z-index: 102;
            left: 1px; direction: ltr; position: absolute; top: 36px; text-align: left" ThreeDEffect="False"
            Width="730px" CssClass="igwtMainBlue2k7" ImageDirectory="~/ig_common/20071CLR20/Styles/Office2007Blue/WebTab/"
            BackColor="#DBE7F5">
            <Tabs>
                <igtab:Tab meta:resourceKey="tabRecHisRes" Text="Criteria Fields">
                    <ContentTemplate>
                        <asp:Panel ID="pnlCriterias" runat="server" BorderStyle="None" BorderWidth="1px"
                            Height="374px" Style="z-index: 99; left: 18px; position: absolute; top: 31px"
                            Width="685px" meta:resourcekey="pnlCriteriasResource1">
                            &nbsp;
                        </asp:Panel>
                        <asp:CheckBox ID="ChkArabicView" runat="server" Font-Bold="True" Font-Size="Small"
                            Style="left: 30px; position: absolute; top: 361px; z-index: 103; width: 122px;"
                            Text="Arabic View" TabIndex="100" meta:resourcekey="ChkArabicViewResource1" />
                    </ContentTemplate>
                    <DisabledStyle BackColor="#CCCCFF">
                    </DisabledStyle>
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
        <igtxt:WebImageButton ID="btnSave" runat="server" meta:resourcekey="btnSaveResource1"
            Style="z-index: 100; left: 813px; position: absolute; top: 252px" Text="Print"
            UseBrowserDefaults="False" Width="96px" Visible="False">
            <Appearance>
                <style font-bold="True"></style>
            </Appearance>
            <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                WidthOfRightEdge="13" />
            <ClientSideEvents Click="btnCriteriaPreview_Click" />
        </igtxt:WebImageButton>
        <igtxt:WebImageButton Visible="true" ID="btnExportToExcel" runat="server" meta:resourcekey="btnExportToExcel"
            Style="z-index: 100; position: absolute; top: 466px; width: 131px; right: 263px;"
            Text="Export to excel" UseBrowserDefaults="False" AutoSubmit="False">
            <Appearance>
                <style font-bold="True"></style>
            </Appearance>
            <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                WidthOfRightEdge="13" />
            <ClientSideEvents Click="btnExportExcel_Click" />
        </igtxt:WebImageButton>
        <igtxt:WebImageButton ID="btnPreview" runat="server" meta:resourcekey="btnPreviewResource1"
            Style="z-index: 100; left: 488px; position: absolute; top: 466px" Text="Privew"
            UseBrowserDefaults="False" Width="96px" AutoSubmit="False">
            <Appearance>
                <style font-bold="True"></style>
            </Appearance>
            <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                WidthOfRightEdge="13" />
            <ClientSideEvents Click="btnExportPDF_Click" />
        </igtxt:WebImageButton>
        <igtxt:WebImageButton ID="btnCancel" runat="server" meta:resourcekey="btnCancelResource1"
            Style="z-index: 101; left: 608px; position: absolute; top: 466px" Text="Exit"
            UseBrowserDefaults="False" Width="96px" Visible="False">
            <Appearance>
                <style font-bold="True"></style>
            </Appearance>
            <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                WidthOfRightEdge="13" />
            <ClientSideEvents Click="btnCancel_Click" />
        </igtxt:WebImageButton>
        <igtxt:WebImageButton Visible="false" ID="btnExportToWord" runat="server" meta:resourcekey="btnExportToWord"
            Style="z-index: 100; left: 184px; position: absolute; top: 466px; width: 131px;"
            Text="Export to word" UseBrowserDefaults="False" AutoSubmit="False">
            <Appearance>
                <style font-bold="True"></style>
            </Appearance>
            <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                WidthOfRightEdge="13" />
            <ClientSideEvents Click="btnExportWord_Click" />
        </igtxt:WebImageButton>
    </div>
    <asp:HiddenField ID="txtSqlNames" runat="server" />
    <asp:HiddenField ID="txtRealNames" runat="server" />
    <asp:HiddenField ID="txtOperations" runat="server" />
    <asp:HiddenField ID="txtReportCode" runat="server" />
    </form>
</body>
</html>
