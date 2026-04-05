<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmDeleteSelfSrviceAddition_Deduction.aspx.vb"
    Inherits="frmAttendancePreparation" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~FrmAnnual Vacation Request Action</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>

    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var IsEdit = true;
        function UWGEmployeesAttend_AfterCellUpdateHandler(gridName, cellId) {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var grid = igtbl_getGridById(gridName);
            var cell = igtbl_getCellById(cellId);
            var Row = igtbl_getRowById(cellId);
            var cellkey = cell.Column.Key;
            if (cellkey == 'PermitLate' || cellkey == 'NotpermitLate') {
                var TotalLateSum = 0;
                var PermitLateSum = 0;
                var NotpermitLateSum = 0;
                var HTotalLateVal = Row.getCellFromKey('TotalLate').getValue();
                var HPermitLateVal = Row.getCellFromKey('PermitLate').getValue();
                var HNotpermitLateVal = Row.getCellFromKey('NotpermitLate').getValue();
                var lblDescTotalLate = igtab_getElementById("lblDescTotalLate", ultraTab.element);
                var cTotalLateVal = Row.getCellFromKey('TotalLate');
                TotalLateSum = (HNotpermitLateVal - HPermitLateVal);
                if (TotalLateSum < 0) {
                    TotalLateSum = 0;
                }
                cTotalLateVal.setValue(ConvertToNumber(TotalLateSum));
                TotalLateSum = (ConvertToNumber(lblDescTotalLate.innerText) - HTotalLateVal);
                if (TotalLateSum < 0) {
                    TotalLateSum = 0;
                }
                TotalLateSum = TotalLateSum + (HNotpermitLateVal - HPermitLateVal);
                if (TotalLateSum < 0) {
                    TotalLateSum = 0;
                }
                lblDescTotalLate.innerText = TotalLateSum;
            }
            else if (cellkey == 'IsAbsent' || cellkey == 'IsVacation') {
                var TotalVac = ConvertToNumber(igtab_getElementById("lblDescTotalVacation", ultraTab.element).innerText);
                var TotalAbsent = ConvertToNumber(igtab_getElementById("lblDescTotalAbsent", ultraTab.element).innerText);
                var HTotalVac = Row.getCellFromKey('IsVacation').getValue();
                var HTotalAbsent = Row.getCellFromKey('IsAbsent').getValue();
                if (HTotalAbsent == true) {
                    ++TotalAbsent;

                }
                else if (HTotalAbsent == false) {
                    --TotalAbsent;

                }
                igtab_getElementById("lblDescTotalAbsent", ultraTab.element).innerText = TotalAbsent;
            }
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
        function CloseMe() {
            if (typeof (parent.CloseIt) === 'function') {
                parent.CloseIt("");
            }
            parent.CloseIt("");
            parent.location.reload();
            window.close();

        }
        function ClosWindow() {
            self.CloseMe();
            parent.location.reload();
            window.reload();

        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmNationalities" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px" meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px" meta:resourcekey="TargetControlResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                       
                            </td>
                            <td style="width: 12px">
                            
                            </td>
                            <td style="width: 24px">
                           <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
        SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Delete"
        OnClientClick="SaveOtherFieldsData();" />
                            </td>
                            <td style="width: 24px">
                                
                            </td>
                            <td style="width: 40px">
                                
                            </td>
                            <td style="width: 24px">
                             
                            </td>
                            <td style="width: 80px">
                          
                            </td>
                            <td style="width: 80px">
                       
                            </td>
                            <td style="width: 40px">
                            </td>
                            <td style="width: 24px">
                            </td>
                            <td style="width: 24px">
                             </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                              </td>
                            <td style="width: 24px">
                             </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 80px">
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
                                                            <asp:Label ID="LblDeleteReason" runat="server" Text=" سبب الحذف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="LblDeleteReasonResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="TxtDeleteReason" runat="server" TextMode="MultiLine" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                           
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                 
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        
                                                </table>
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
