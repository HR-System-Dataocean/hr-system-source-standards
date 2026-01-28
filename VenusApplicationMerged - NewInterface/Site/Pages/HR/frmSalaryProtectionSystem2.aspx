<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSalaryProtectionSystem2.aspx.vb"
    Inherits="frmSalaryProtectionSystem2" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

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
    <title>* Venus Payroll * ~Sending Salary Notification</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
        var ODialoge;
        var OSender;
        var IsEdit = true;
        function UwgSearchEmployees_AfterCellUpdateHandler(gridName, cellId) {
            var grid = igtbl_getGridById(gridName);
            var gridLength = grid.Rows.length;
            var cell = igtbl_getCellById(cellId);
            var row = cell.getRow();
            var blSign = cell.getValue();
            var CellToChange;
            if (IsEdit) {
                if (row.Id == gridName + "_r_0") {
                    IsEdit = false;
                    for (i = 0; i < gridLength; i++) {
                        var currRow = grid.Rows.rows[i];
                        var currRow = igtbl_getCellById(gridName + "_r" + i);
                        CellToChange = igtbl_getCellById(gridName + "_rc_" + i + "_1");
                        CellToChange.setValue(blSign)
                    }
                }
                else {
                    IsEdit = false;
                    CellToChange = igtbl_getCellById(gridName + "_rc_0_1");
                    CellToChange.setValue(false)
                }
                IsEdit = true;
            }
        }

        function ddlDepartment_Change() {
            var ultraTab = igtab_getTabById("UltraWebTab1");
            var ddlDepartment = igtab_getElementById("ddlDepartment", ultraTab.element);
            PageMethods.GetRelatedDepartment(ddlDepartment.value, OnSucceeded, OnFailed);
        }

        function OnSucceeded(result, userContext, methodName) {
            if (methodName == 'GetRelatedDepartment') {
                var ultraTab = igtab_getTabById("UltraWebTab1");
                var ddlBranch = igtab_getElementById("ddlBranche", ultraTab.element);
                ddlBranch.outerHTML = result;
            }
        }

        function OnFailed(error) {
            alert();
        }

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
    </script>
    <style type="text/css">
        .style1
        {
            height: 21px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmSalaryProtectionSystem" runat="server" defaultbutton="ImageButton1">
    <script type="text/javascript" id="Script1">
        $(function () {
            $('#<%= DdlPeriods.ClientID%>').change(function () {
                $.blockUI({ message: '' });
            });
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
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Prepare" Width="14px" Height="12px" runat="server"
                                    CommandArgument="Prepare" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif" />
                            </td>
                            <td style="width: 200px">
                                <asp:LinkButton ID="LinkButton_Prepare" runat="server" Text="Export Salary Protection Sheet"
                                    CommandArgument="Prepare" meta:resourcekey="LinkButton_PrepareResource1" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Update" Width="14px" Height="12px" runat="server"
                                    CommandArgument="Update" ImageUrl="~/Pages/HR/Img/user.png" />
                            </td>
                            <td style="width: 200px">
                                <asp:LinkButton ID="LinkButton_Update" runat="server" OnClientClick="OpenModal1('frmUpdateLaborOfficeNo.aspx',510,830,false,''); return false;"
                                    Text="Update Labor Office No" CommandArgument="Update" meta:resourcekey="LinkButton_UpdateResource1"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" runat="server" Text="Export Special Sheet" CommandArgument="Prepare"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                            </td>
                        </tr>
                    </table>
                    <table style="width: 100%; height: 42px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource1" />
                            </td>
                            <td style="vertical-align: middle">
                                <asp:Label ID="Label_Header" runat="server" meta:resourcekey="Label_HeaderResource1"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;"></asp:Label>
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
                                                            <asp:Label ID="lblCode" runat="server" Text="Labor Office No" Width="90px" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCode" runat="server" MaxLength="30" meta:resourcekey="txtCodeResource1"
                                                                            SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
    Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchCodeResource1">
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
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode1" runat="server" SkinID="Label_DefaultNormal" Text="Fisical Periods"
                                                                Width="90px" meta:resourcekey="lblCode1Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlPeriods" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlPeriodsResource1">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="lblDepartment" runat="server" meta:resourcekey="lblDepartmentResource1"
                                                                SkinID="Label_DefaultNormal" Text="Department" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
                                                                SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                Text="Branch" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBranche" runat="server" meta:resourcekey="ddlBranchResource1"
                                                                SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                            </asp:DropDownList>
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
                                                            <asp:Label ID="Label_Bank" runat="server" meta:resourcekey="Label_BankResource1"
                                                                SkinID="Label_DefaultNormal" Text="Bank" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DropDownList_Bank" runat="server" SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Sponsor" runat="server" meta:resourcekey="Label_SponsorResource1"
                                                                SkinID="Label_DefaultNormal" Text="Sponsor" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Sponsor" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC" AutoPostBack="True"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Sponsor" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
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
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="BankAccountTypeLabel" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Bank Account Type" meta:resourcekey="BankAccountTypeLabelResource"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBankAccountType" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1" AutoPostBack="True">
                                                                <%--<asp:ListItem meta:resourcekey="SelectResourceLabel" Value="0">حدد الخيار</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="RegularAccountLabel" Value="Regular">بطاقة بنك</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="SalaryAccountLabel" Value="Salary">بطاقة راتب</asp:ListItem>--%>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <%--<td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" meta:resourcekey="Label_SponsorResource1"
                                                                SkinID="Label_DefaultNormal" Text="Sponsor" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox1" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton1" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
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
                                            </td>--%>

                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="lblBankFileType" SkinID="Label_DefaultNormal"
                                                                Text="File Format" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="dllFileFormat" runat="server" meta:resourcekey="ddlBranchResource1"
                                                                SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    <%--Rabie--%>


                                          <tr>
      <td style="width: 47%; height: 16px; vertical-align: top">
          <table style="width: 100%; vertical-align: top" cellspacing="0">
              <tr>
                  <td class="SeparArea">
                  </td>
                  <td class="LabelArea">
                            <asp:Label ID="LblPaymentType" runat="server" Width="80px" SkinID="Label_DefaultNormal"
        Text="Payment Type" meta:resourcekey="LblPaymentTypeResource"></asp:Label>
                  </td>
                  <td class="DataArea">
                      <asp:DropDownList ID="ddlLblPaymentType" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
                         
                      </asp:DropDownList>
                  </td>
                             
              </tr>
          </table>
      </td>
      <td style="width: 6%; height: 16px; vertical-align: top">
          &nbsp;
      </td>

      <td style="width: 47%; height: 16px; vertical-align: top">
          <table style="width: 100%; vertical-align: top" cellspacing="0">
              <tr>
                  <td class="SeparArea">
                  </td>
                  <td class="LabelArea">
                     <asp:Label ID="lblGradeStep" runat="server" SkinID="Label_DefaultNormal" Text="Grade Steps"
     Width="80px" meta:resourcekey="lblGradeStepResource1"></asp:Label>
                  </td>
                  <td class="DataArea">
                     <asp:DropDownList ID="ddlGradeStep" runat="server" AutoPostBack="True" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlGradeStepResource1">
 </asp:DropDownList>
                  </td>
              </tr>
     
          </table>
      </td>
  </tr>

                                        <%--end--%> 
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Contract" runat="server" meta:resourcekey="Label_ContractResource1"
                                                                SkinID="Label_DefaultNormal" Text="Contract Type" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Contract" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Cont" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
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
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblStatues" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                                Text="Statues" meta:resourcekey="StatuesLabelResource"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlStatues" runat="server"  AutoPostBack="True"  SkinID="DropDownList_LargNormal" meta:resourcekey="ddlStatuesResource1">
                                                                <asp:ListItem meta:resourcekey="AllResourceLabel" Value="0">الكل</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="PreparedLabel" Value="1">مرسل</asp:ListItem>
                                                                <asp:ListItem meta:resourcekey="unPreparedLabel" Value="2">غير مرسل</asp:ListItem>
                                                            </asp:DropDownList>
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
                                                           
                                                        </td>
                                                        <td class="DataArea">
                                                             <asp:Label ID="LblHasBankNo" runat="server" Style="margin-right: 20px; width: 21%; font-size: small" Text="HasBankNo" SkinID="TextBox_LargeNormalC"
        meta:resourcekey="LblHasBankNoResource1"></asp:Label>
   <asp:CheckBox ID="ChkHasBankNo" runat="server" AutoPostBack="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                   
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 20px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom" class="style1">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Please select employees" SkinID="Label_DefaultBold"
                                                                meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: middle; text-align: center;">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td style="width: 47%; vertical-align: middle;">
                                                <igtxt:WebImageButton ID="btnFind" runat="server" Height="5px" Style="font-family: Tahoma;
                                                    font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="btnFindRes"
                                                    Overflow="NoWordWrap" Text=" Search " UseBrowserDefaults="False" Width="80px">
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
                                                <igtbl:UltraWebGrid   Browser="UpLevel"  ID="UwgSearchEmployees" runat="server" EnableAppStyling="True"
                                                    Height="100%" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                    Width="325px">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                        AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                                        RowHeightDefault="18px" SelectTypeRowDefault="Extended" StationaryMargins="Header"
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy"
                                                        AllowRowNumberingDefault="Continuous">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                            BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                            Width="325px">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                                                            ClickCellButtonHandler="UwgSearchEmployees_ClickCellButtonHandler" />
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <RowSelectorStyleDefault Width="40px" Font-Names="Arial" Font-Size="7pt">
                                                        </RowSelectorStyleDefault>
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
                                                        <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1">
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
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True" meta:resourcekey="UltraGridColumnResource1">
                                                                    <Header Caption="Payroll Id">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                 <igtbl:UltraGridColumn BaseColumnName="Select" Key="Select" Width="23px" AllowUpdate="Yes"  Type="CheckBox"
                                                                    meta:resourcekey="UltraGridColumnResource2">
                                                                    <Header Caption="√">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Code" Key="Code" Width="90px"
                                                                    meta:resourcekey="UltraGridColumnResource3">
                                                                    <Header Caption="Code">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="FullName" Key="FullName"
                                                                    Width="100%" meta:resourcekey="UltraGridColumnResource4">
                                                                    <Header Caption="Employee Name">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" Key="NetSalary" BaseColumnName="NetSalary"
                                                                    Width="80px" meta:resourcekey="UltraGridColumnResource5">
                                                                    <Header Caption="Net Salary">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" CellButtonDisplay="Always" Key="Prepared"
                                                                    BaseColumnName="Prepared" Type="CheckBox" Width="80px" meta:resourcekey="PreparedGridColumnResource6">
                                                                    <Header Caption="Prepared">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
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
