<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ThemeLangManager.ascx.vb"
    Inherits="ThemeLangManager" %>
<table align="center" class="ThemeLangManager">
    <tr>
        <td style="width: 120px; height: 11px">
        </td>
        <td style="width: 20px; height: 11px">
            <asp:ImageButton ID="ImageButton_Arabic" runat="server" ImageUrl="~/Common/Images/sa.png"
                CausesValidation="False" CommandName="Ar" meta:resourcekey="ImageButton_ArabicResource1" />
        </td>
        <td style="width: 20px; height: 11px">
            <asp:ImageButton ID="ImageButton_English" runat="server" ImageUrl="~/Common/Images/us.png"
                CausesValidation="False" CommandName="En" meta:resourcekey="ImageButton_EnglishResource1" />
        </td>
        <td style="text-align:center; width: 870px; height: 11px">
            <asp:Label ID="Label_Header" runat="server" Text="Venus Application" 
                meta:resourcekey="Label_HeaderResource1" ForeColor="White"></asp:Label>
        </td>
        <td style="width: 20px; height: 11px">
            <asp:ImageButton ID="ImageButton_Blue" runat="server" ImageUrl="~/Common/Images/Blue.png"
                CausesValidation="False" CommandName="Blue" meta:resourcekey="ImageButton_BlueResource1" />
        </td>
        <td style="width: 20px; height: 11px">
            <asp:ImageButton ID="ImageButton_Silver" runat="server" ImageUrl="~/Common/Images/Silver.png"
                CausesValidation="False" CommandName="Silver" meta:resourcekey="ImageButton_SilverResource1" />
        </td>
        <td style="width: 20px; height: 11px">
        </td>
    </tr>
</table>
