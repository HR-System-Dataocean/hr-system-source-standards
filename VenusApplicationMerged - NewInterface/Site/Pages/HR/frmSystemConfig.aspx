<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSystemConfig.aspx.vb" Inherits="frmSystemConfig"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~TicketsAgencies</title>
     <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/Float.js" type="text/javascript"></script>
    <script type="text/javascript">
        // flag to add listener for resize events
        var _onloadFlag = true;
        function adjustHeight() {
            var myHeight = 0;
            if (typeof (window.innerWidth) == 'number') {
                myHeight = window.innerHeight;
            } else if (document.documentElement && (document.documentElement.clientWidth || document.documentElement.clientHeight)) {
                myHeight = document.documentElement.clientHeight;
            } else if (document.body && (document.body.clientWidth || document.body.clientHeight)) {
                myHeight = document.body.clientHeight;
            }
            var tab = igtab_getTabById('UltraWebTab1');
            // <td> which is used as content pane
            var cp = document.getElementById(tab.ID + '_cp');
            // <table> of tab
            var table = tab.element;
            // <div> container of tab
            var container = table.parentNode;
            // height available for tab
            var height = container.clientHeight;
            height = (myHeight - 85);
            if (!height) return;
            // difference between heights of tab and content pane
            var heightShift = tab._myHeightShift;
            // 4 - is adjustment for top/bottom borders of tab
            if (!heightShift)
                heightShift = tab._myHeightShift = (table.offsetHeight - cp.offsetHeight + 4);
            // calculate height for content pane (can be improved for different browsers)
            height -= heightShift;
            if (height < 0) return;
            // set height of content pane to make height of tab to fit with container
            if (table.offsetHeight < (myHeight - 85)) {
                cp.style.height = height + 'px';
            }
            if (!_onloadFlag)
                return;
            _onloadFlag = false;
            // process onresize events
            ig_shared.addEventListener(window, 'resize', adjustHeight);
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
                try {
                    __doPostBack(window.document.getElementById(OSender), "TextChanged");
                }
                catch (err) { }
                Sender.focus();
            }
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }

        function OpenModal12(pageurl, height, width) {
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
        $(function () {
            var icons = {
                header: "",
                activeHeader: ""
            };
            $("#accordion").accordion({ icons: icons, autoHeight: false });
            $("#accordion").accordion("option", "icons", "");
        });

        $(function () {
            try {
                $(".Validators").Float();
            }
            catch (err) { }
        });
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmSystemConfig" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display:none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                    OnClientClick="SaveOtherFieldsData();" />
                            </td>
                           
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" meta:resourcekey="ImageButton_PrintResource1" CommandArgument="Print" />
                            </td>
                            
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                           
                            <td style="width: 30%">
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 5%">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 42px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource1" />
                            </td>
                            <td style="width: 50%; vertical-align: middle">
                                <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                            </td>
                            <td style="width: 50%; vertical-align: middle">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblRegUserResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegUserValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء" SkinID="Label_CopyRightsBold"
                                                            meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal"
                                                            meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;">
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
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
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource0">
    <ContentTemplate>
        <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
            cellspacing="0">
            <tr>
                <td style="height: 10px" colspan="3">
                </td>
            </tr>
            <tr>
                <td>
                     <table style="width: 45%; height: 30px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                
                            </td>
                               <td class="SeparArea">
                                   </td>
                               <td class="SeparArea">
                                                        </td>
                                                        <td  style="width:20px" class="LabelArea">
                                                            <asp:Label ID="lblCompany" runat="server" Width="120px" SkinID="Label_DefaultBold"
                                                                Text="الشركة" meta:resourcekey="lblCompanyResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlCompany" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="CompanyResource1" TabIndex="3">
                                                            </asp:DropDownList>
                                                        </td>
                            </tr>
                         <tr>
                             <td style="width: 32px; vertical-align: top">
                                
                            </td>
                               <td class="SeparArea">
                                   </td>
                               <td class="SeparArea">
                                                        </td>
                             <td>

                             </td>
                             <td></td>
                         </tr>
                         </table>

                    <div id="accordion">
                                       <h3>
                                                <asp:Label ID="Label17" runat="server" Text="Personal information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="TabResource2"></asp:Label>
                                            </h3>
                                            <div id="DivMainInfo">
                                               
                                                        <table style="width: 100%;">
                                                             <tr>
                <td style="width: 47%; height: 16px; vertical-align: top">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="SeparArea">
                            </td>
                            <td class="LabelArea">
                                <asp:Label ID="LblPreventChangeEndDate" runat="server" Text="UseCostCenter" SkinID="Label_DefaultNormal"
                                    meta:resourcekey="LblPreventChangeEndDateResource1"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:CheckBox ID="ChkPreventChangeEndDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="SeparArea">
                            </td>
                            <td class="LabelArea">
                                <asp:Label ID="lblMultiBranchedPosition" runat="server" Text="MultiBranchedPosition" SkinID="Label_DefaultNormal"
                                    meta:resourcekey="MultiBranchedPositionResource1"></asp:Label>
                            </td>
                            <td class="DataArea">
                                <asp:CheckBox ID="chkMultiBranchedPosition" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 6%; height: 16px; vertical-align: top">
                </td>
                <td style="width: 47%; height: 16px; vertical-align: top">
                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                        <tr>
                            <td class="SeparArea">
                            </td>
                            <td class="LabelArea">
                            </td>
                            <td class="DataArea">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

                                                            </table>
                                                </div>

                        <h3>
                                                <asp:Label ID="Label3" runat="server" Text="Organization information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="TabResource1"></asp:Label>
                                            </h3>
                                            <div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                             <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblUseCostCenter" runat="server" Text="UseCostCenter" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblUseCostCenterResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="chkUseCostCenter" runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
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
                                                            <asp:Label ID="lblMinimumCostCentersCount" runat="server" Text="MinimumCostCentersCount" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblMinimumCostCentersCountResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" >
                                                            <asp:TextBox ID="txtMinimumCostCentersCount" runat="server" SkinID="TextBox_SmallNormalrtl" MaxLength="2"
                                                                meta:resourcekey="txtMinimumCostCentersCountResource1"></asp:TextBox>
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                         <td class="SeparArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                                            </table>
                                                        </ContentTemplate></asp:UpdatePanel></div>

                        </div>
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
