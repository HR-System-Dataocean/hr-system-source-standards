<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserProfile.aspx.vb" Inherits="_UserProfile"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../Common/Script/JQuery/jquery-1.6.2.js"></script>
    <script src="../../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js"></script>
    <script type="text/javascript" id="igClientScript">
        var ODialoge;
        function OpenModal1(pageurl, height, width) {
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
        function CloseIt(retvalue) {
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" cellspacing="1" cellpadding="1" class="MainContainerMaster">
            <tr>
                <td style="width: 100%; height: 615px; vertical-align: top;">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%; height: 100%; vertical-align: top" cellspacing="0">
                                <tr>
                                    <td style="height: 16px; vertical-align: top">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 85%; vertical-align: top;">
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label_Title1" runat="server" Text="Personal information" SkinID="Label_DefaultBold"
                                                                                meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; height: 16px; vertical-align: top">
                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LblCode" runat="server" SkinID="Label_DefaultNormal" Text="Employee No "
                                                                                meta:resourcekey="LblCodeResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelName" runat="server" SkinID="Label_DefaultNormal" Text="Employee Name "
                                                                                meta:resourcekey="LabelNameResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelBirthDate" runat="server" SkinID="Label_DefaultNormal" Text="BirthDate "
                                                                                meta:resourcekey="LabelBirthDateResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label17" runat="server" Text="Organization information" SkinID="Label_DefaultBold"
                                                                                meta:resourcekey="Label17Resource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; height: 16px; vertical-align: top">
                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelBranch" runat="server" SkinID="Label_DefaultNormal" Text="Current Branch "
                                                                                meta:resourcekey="LabelBranchResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="Label_Department" runat="server" SkinID="Label_DefaultNormal" Text="Current Department "
                                                                                meta:resourcekey="Label_DepartmentResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="Label_Position" runat="server" SkinID="Label_DefaultNormal" Text="Current Position "
                                                                                meta:resourcekey="Label_PositionResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelJoinDate" runat="server" SkinID="Label_DefaultNormal" Text="Join Date "
                                                                                meta:resourcekey="LabelJoinDateResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                                    cellspacing="6">
                                                                    <tr>
                                                                        <td style="vertical-align: bottom">
                                                                            <asp:Label ID="Label2" runat="server" Text="Contact information" SkinID="Label_DefaultBold"
                                                                                meta:resourcekey="Label2Resource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; height: 16px; vertical-align: top">
                                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelPhone" runat="server" SkinID="Label_DefaultNormal" Text="Phone "
                                                                                meta:resourcekey="LabelPhoneResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelMobile" runat="server" SkinID="Label_DefaultNormal" Text="Mobile "
                                                                                meta:resourcekey="LabelMobileResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelMail" runat="server" SkinID="Label_DefaultNormal" Text="E-Mail "
                                                                                meta:resourcekey="LabelMailResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="SeparArea">
                                                                        </td>
                                                                        <td class="DataArea">
                                                                            <asp:Label ID="LabelNationality" runat="server" SkinID="Label_DefaultNormal" Text="Nationality "
                                                                                meta:resourcekey="LabelNationalityResource1"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 15%; height: 100%; text-align: center; vertical-align: top">
                                                    <table style="width: 100%; height: 100%; text-align: center; vertical-align: top">
                                                        <tr>
                                                            <td style="height: 100%; text-align: center; vertical-align: top">
                                                                <asp:Image ID="Image1" runat="server" Height="135px" Width="135px" ImageUrl="~/Common/Images/DefaultPicture.jpg"
                                                                    meta:resourcekey="Image1Resource1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="2">
                                                    <table style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle; ">
                                                                <img alt="" style="display:none" src="../../../Common/Images/SServices/Accountability.png"
                                                                    height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton1" runat="server" meta:resourcekey="LinkButton1Resource1"
                                                                    OnClientClick="OpenModal1('LoginAccountability.aspx',300,450); return false;"
                                                                    SkinID="LinkButton_DefaultNormal" Visible="False"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle; ">
                                                                <img alt="" style="display:none" src="../../../Common/Images/SServices/Contacts.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton2" runat="server" meta:resourcekey="LinkButton2Resource1"
                                                                    OnClientClick="OpenModal1('LoginContacts.aspx',300,450); return false;" SkinID="LinkButton_DefaultNormal" Visible="False"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="2">
                                                    <table style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                                <img alt="" style="display:none" src="../../../Common/Images/SServices/CRoles.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                

                                                                    <asp:HyperLink ID="lnkDownload" runat="server" meta:resourcekey="LinkButton4Resource1"
                                                                Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Chocolate"
                                                                Target="_blank" Text="Download Attached" Width="95px" Visible="False"></asp:HyperLink>
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                                <img alt="" src="../../../Common/Images/SServices/CPassword.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton3" runat="server" meta:resourcekey="LinkButton3Resource1"
                                                                    OnClientClick="OpenModal1('../ChangePassword.aspx',220,550); return false;" SkinID="LinkButton_DefaultNormal"></asp:LinkButton>
                                                            </td>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="2">
                                                    <table style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                                <img alt="" src="../../../Common/Images/SServices/Brief.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton9" runat="server" meta:resourcekey="LinkButton9Resource1"
                                                                    SkinID="LinkButton_DefaultNormal">Show My Profile</asp:LinkButton>
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                                <img alt="" src="../../../Common/Images/SServices/Attachements.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton8" runat="server" meta:resourcekey="LinkButton8Resource1"
                                                                    SkinID="LinkButton_DefaultNormal">Show My Documents</asp:LinkButton>
                                                            </td>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="2">
                                                    <table style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                                <img alt="" src="../../../Common/Images/SServices/AttScheduel.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton7" runat="server" meta:resourcekey="LinkButton7Resource1"
                                                                    SkinID="LinkButton_DefaultNormal">Show My Last Month Attendance</asp:LinkButton>
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                                <img alt="" src="../../../Common/Images/SServices/Salary.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton5" runat="server" meta:resourcekey="LinkButton5Resource1"
                                                                    SkinID="LinkButton_DefaultNormal">Show Last Prepared Salary</asp:LinkButton>
                                                            </td>
                                                            
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; vertical-align: top" colspan="2">
                                                    <table style="width: 100%; vertical-align: top">
                                                        <tr>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                                <img alt="" src="../../../Common/Images/SServices/FinanceSched.png" height="38" width="38" />
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                                <asp:LinkButton ID="LinkButton6" runat="server" meta:resourcekey="LinkButton6Resource1"
                                                                    SkinID="LinkButton_DefaultNormal">Show My Finance Scheduels</asp:LinkButton>
                                                            </td>
                                                            <td style="width: 5%; vertical-align: middle">
    <img alt="" src="../../../Common/Images/SServices/Letter.jpeg" height="38" width="38" />
</td>
                                                            <td style="width: 35%; vertical-align: middle">
    <asp:LinkButton ID="LinkButtonSFCHS" runat="server" meta:resourcekey="LinkButtonSFCHSResource1"
        SkinID="LinkButton_DefaultNormal">Print SFCHS Letter</asp:LinkButton>
</td>
                                                            <td style="width: 5%; vertical-align: middle">
                                                            </td>
                                                            <td style="width: 35%; vertical-align: middle">
                                                            </td>
                                                            <td style="width: 10%; vertical-align: middle">
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
