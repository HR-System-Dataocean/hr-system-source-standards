<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRemarks.aspx.vb" Inherits="Interfaces_frmRemarks"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Remark</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="left" style="border-right: 1px outset; border-top: 1px outset; z-index: 102;
            left: 0px; border-left: 1px outset; width: 480px; border-bottom: 1px outset;
            position: absolute; top: 0px; height: 207px; background-color: lightsteelblue">
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="lbRemarksfor" runat="server" Style="z-index: 100; left: 254px; position: absolute;
                top: 16px" Text="Remark For Record :" meta:resourcekey="lbRemarksforResource1"
                Width="131px"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="lblRecordID" runat="server" Height="19px" Style="z-index: 106; left: 171px;
                position: absolute; top: 14px" Width="50px" meta:resourcekey="lblRecordIDResource1"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="txtRemarks" runat="server" BackColor="Silver" BorderColor="White"
                BorderStyle="Solid" BorderWidth="1px" Height="120px" Style="z-index: 102; left: 34px;
                position: absolute; top: 40px" TextMode="MultiLine" Width="335px" meta:resourcekey="txtRemarksResource1"></asp:TextBox>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
            <igtxt:WebImageButton ID="btnSave" runat="server" Style="z-index: 103; left: 252px;
                position: absolute; top: 173px" Text="Save" UseBrowserDefaults="False" Width="114px"
                meta:resourcekey="btnSaveResource1">
                <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                    MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                    WidthOfRightEdge="13" />
                <Appearance>
                    <style font-bold="True"></style>
                </Appearance>
            </igtxt:WebImageButton>
            <igtxt:WebImageButton ID="btnCancel" runat="server" Style="z-index: 104; left: 33px;
                position: absolute; top: 173px" Text="Cancel" UseBrowserDefaults="False" Width="114px"
                meta:resourcekey="btnCancelResource1">
                <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                    MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                    WidthOfRightEdge="13" />
                <Appearance>
                    <style font-bold="True"></style>
                </Appearance>
            </igtxt:WebImageButton>
            <asp:Label ID="lbFrom" runat="server" Style="z-index: 105; left: 126px; position: absolute;
                top: 14px" Text="Form :" Width="45px" Height="19px" meta:resourcekey="lbFromResource1"></asp:Label>
        </div>
        <asp:Label ID="lblTableName" runat="server" Height="19px" Style="z-index: 103; left: 28px;
            position: absolute; top: 15px" Width="50px" meta:resourcekey="lblTableNameResource1"></asp:Label>
    </div>
    </form>
</body>
</html>
