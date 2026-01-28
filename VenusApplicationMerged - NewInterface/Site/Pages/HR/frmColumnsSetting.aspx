<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmColumnsSetting.aspx.vb"
    Inherits="Interfaces_frmColumnsSetting" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Columns Properties</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <script src="Scripts/App_JScript_frmSearchCustomization.js" type="text/javascript"></script>
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" onunload="frmColumnsSettings_Unload();">
    <form id="form1" runat="server">
    <div>
        <div align="left" style="border-right: 1px outset; border-top: 1px outset; z-index: 102;
            left: 0px; border-left: 1px outset; width: 408px; border-bottom: 1px outset;
            position: absolute; top: 0px; height: 250px; background-color: #dbe7f5">
            <asp:TextBox ID="txtCode" runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid"
                BorderWidth="1px" CssClass="TextBox" Font-Bold="False" Font-Names="Tahoma" Height="15px"
                Style="z-index: 100; left: 167px; position: absolute; top: 24px" Width="195px"
                meta:resourcekey="txtCodeResource1"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" BorderStyle="Outset" BorderWidth="1px" Font-Size="Small"
                ForeColor="Black" Height="18px" Style="z-index: 112; left: 32px; position: absolute;
                top: 24px" Text="Code" Width="123px" meta:resourcekey="Label1Resource1"></asp:Label>
            <igtxt:WebImageButton ID="btnSave" runat="server" Style="z-index: 102; left: 35px;
                position: absolute; top: 197px" Text="Save" UseBrowserDefaults="False" Width="114px"
                meta:resourcekey="btnSaveResource1" TabIndex="6">
                <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                    MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                    WidthOfRightEdge="13" />
                <Appearance>
                    <style font-bold="True"></style>
                </Appearance>
            </igtxt:WebImageButton>
            <igtxt:WebImageButton ID="btnCancel" runat="server" Style="z-index: 103; left: 248px;
                position: absolute; top: 197px" Text="Cancel" UseBrowserDefaults="False" Width="114px"
                meta:resourcekey="btnCancelResource1" TabIndex="7">
                <RoundedCorners HeightOfBottomEdge="0" HoverImageUrl="ig_butMac2.gif" ImageUrl="ig_butMac1.gif"
                    MaxHeight="23" MaxWidth="300" PressedImageUrl="ig_butMac4.gif" RenderingType="FileImages"
                    WidthOfRightEdge="13" />
                <Appearance>
                    <style font-bold="True"></style>
                </Appearance>
            </igtxt:WebImageButton>
            <asp:TextBox ID="txtEngName" runat="server" BackColor="Silver" BorderColor="White"
                BorderStyle="Solid" BorderWidth="1px" CssClass="TextBox " Font-Bold="False" Font-Names="Tahoma"
                Height="15px" Style="z-index: 104; left: 167px; position: absolute; top: 51px"
                TabIndex="1" Width="195px" meta:resourcekey="txtEngNameResource1"></asp:TextBox>
            <asp:TextBox ID="txtArbName" runat="server" BackColor="Silver" BorderColor="White"
                BorderStyle="Solid" BorderWidth="1px" CssClass="TextBox " Font-Bold="False" Font-Names="Tahoma"
                Height="15px" Style="z-index: 105; left: 167px; position: absolute; top: 78px"
                TabIndex="2" Width="195px" meta:resourcekey="txtArbNameResource1"></asp:TextBox>
            <asp:TextBox ID="txtLenght" runat="server" BackColor="Silver" BorderColor="White"
                BorderStyle="Solid" BorderWidth="1px" CssClass="TextBox " Font-Bold="False" Font-Names="Tahoma"
                Height="15px" Style="z-index: 106; left: 167px; position: absolute; top: 105px"
                TabIndex="3" Width="195px" meta:resourcekey="txtLenghtResource1"></asp:TextBox>
            <asp:DropDownList ID="DdlLanguage" runat="server" Style="z-index: 107; left: 167px;
                position: absolute; top: 159px" Width="199px" meta:resourcekey="DdlLanguageResource1"
                TabIndex="5">
                <asp:ListItem Value="0" meta:resourcekey="ListItemResource1" Text="English"></asp:ListItem>
                <asp:ListItem Value="1" meta:resourcekey="ListItemResource2" Text="Arabic"></asp:ListItem>
                <asp:ListItem Value="2" meta:resourcekey="ListItemResource3" Text="Both"></asp:ListItem>
            </asp:DropDownList>
            <asp:Label ID="Label3" runat="server" BorderStyle="Outset" BorderWidth="1px" Font-Size="Small"
                ForeColor="Black" Height="18px" Style="z-index: 108; left: 32px; position: absolute;
                top: 51px" Text="English Description" Width="123px" meta:resourcekey="Label3Resource1"></asp:Label>
            <asp:Label ID="Label4" runat="server" BorderStyle="Outset" BorderWidth="1px" Font-Size="Small"
                ForeColor="Black" Height="18px" Style="z-index: 109; left: 32px; position: absolute;
                top: 78px" Text="Arabic Description" Width="123px" meta:resourcekey="Label4Resource1"></asp:Label>
            <asp:Label ID="Label5" runat="server" BorderStyle="Outset" BorderWidth="1px" Font-Size="Small"
                ForeColor="Black" Height="18px" Style="z-index: 110; left: 32px; position: absolute;
                top: 105px" Text="Length" Width="123px" meta:resourcekey="Label5Resource1"></asp:Label>
            <asp:Label ID="Label6" runat="server" BorderStyle="Outset" BorderWidth="1px" Font-Size="Small"
                ForeColor="Black" Height="18px" Style="z-index: 111; left: 32px; position: absolute;
                top: 160px" Text="Language" Width="123px" meta:resourcekey="Label6Resource1"></asp:Label>
            <asp:TextBox ID="txtRank" runat="server" BackColor="Silver" BorderColor="White" BorderStyle="Solid"
                BorderWidth="1px" CssClass="TextBox " Font-Bold="False" Font-Names="Tahoma" Height="15px"
                meta:resourcekey="txtRankResource1" Style="z-index: 106; left: 167px; position: absolute;
                top: 132px" TabIndex="4" Width="195px"></asp:TextBox>
            <asp:Label ID="lblRank" runat="server" BorderStyle="Outset" BorderWidth="1px" Font-Size="Small"
                ForeColor="Black" Height="18px" meta:resourcekey="lblRankResource1" Style="z-index: 110;
                left: 32px; position: absolute; top: 132px" Text="Rank" Width="123px"></asp:Label>
        </div>
    </div>
    </form>
</body>
</html>
