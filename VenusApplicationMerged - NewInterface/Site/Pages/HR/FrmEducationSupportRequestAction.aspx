<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmEducationSupportRequestAction.aspx.vb"
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
            parent.CloseIt("");
            parent.location.reload();

        }
        function ClosWindow() {
            self.CloseMe();
            parent.location.reload();
            window.reload();

        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="FrmAnnualVacationRequestAction" runat="server">
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
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
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
                                            <td style="height: 18px">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                        <td style="width: 5px">
                                                        </td>
                                                        <td style="width: 90px; text-align: center;">
                                                            <igtxt:WebImageButton ID="btnSave" runat="server" Style="cursor: pointer;" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSaveResource1">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                        <td style="width: 50px">
                                                            <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                Text="|"></asp:Label>
                                                        </td>
                                                        <td  style="width: 5px" class="LabelArea">
                                                            <asp:Label ID="Label8" runat="server" Width="90px" SkinID="Label_DefaultBold"
                                                                Text="الاجراء" meta:resourcekey="lblactionTypeResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 5px" class="DataArea">
                                                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="True" SkinID="DropDownList_DefaultBold"
                                                                meta:resourcekey="DdlVacationTypeResource1" TabIndex="3">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            <igtxt:WebImageButton ID="btnDelete" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnDeleteResource1">
                                                                <Alignments TextImage="ImageBottom" />
                                                                <Appearance>
                                                                    <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/Delete.png" />
                                                                </Appearance>
                                                            </igtxt:WebImageButton>
                                                        </td>
                                                        <td style="width: 5px">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 40px; text-align: center;">
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblMSG" runat="server" meta:resourcekey="lblMSGResource1" SkinID="Label_WarningBold"
                                                                Width="100%"></asp:Label>
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
                   
                </td>
             
            </tr>
        </table>
    </td>
   
    <td style="width: 47%; height: 16px; vertical-align: top">
        <table style="width: 100%; vertical-align: top" cellspacing="0">
            <tr>
               <%-- <td class="SeparArea">
                </td>
                <td class="LabelArea">
                </td>--%>
               <%-- <td class="DataArea">
                      <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                </td>--%>
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
                                                            <asp:Label ID="lblDelegated" Width="120px" runat="server" Text=" المفوض اليه"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="labelDelegatedResource1"></asp:Label>
                                                        </td>
                                                       
                                                          <td>
                                                                         <asp:TextBox ID="txtDelegated" Width="110px" runat="server"  MaxLength="15"
                                                                AutoPostBack="True" meta:resourcekey="txtDelegatedResource1">
                                                           </asp:TextBox>
                                                                    
                                                                        <igtxt:WebImageButton ID="btnDelegatedSearch" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnDelegatedSearchResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>  
                                                        <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

                                                        </td>
                                                         
                                                        <td>
                                                         <asp:TextBox ID="txtDelegatedName" Width="260px" runat="server"  MaxLength="100"
                                                                AutoPostBack="false"  meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
                                                            </td>
                                                          <td class="spacearea">

                                                        </td>
                                                           <td class="spacearea">

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
        <table style="width: 98%; vertical-align: top" cellspacing="0">
            <tr>
                 <td class="SeparArea">
                </td>
                <td class="LabelArea">
                    <asp:Label ID="LblRemarks" runat="server" Width="125px" Text="ملاحظات الاجراء" SkinID="Label_DefaultBold"
                        meta:resourcekey="lblRemarksResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="TxtRemarks" runat="server" SkinID="TextBox_LargeNormalC" 
                        ReadOnly="false"  Width="400px" MaxLength="500"  meta:resourcekey="TxtRemarksResource1">
                   </asp:TextBox>
                </td>
             
            </tr>
        </table>
    </td>
   
    <td style="width: 47%; height: 16px; vertical-align: top">
        <table style="width: 100%; vertical-align: top" cellspacing="0">
            <tr>
               <%-- <td class="SeparArea">
                </td>
                <td class="LabelArea">
                </td>--%>
               <%-- <td class="DataArea">
                      <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                </td>--%>
            </tr>
        </table>
    </td>
</tr>

                                                   <tr>
               <td>
                   <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                       border-bottom: 1px solid black">
                       <tr>
                           <td style="vertical-align: bottom">
                               <asp:Label ID="Label4" runat="server" meta:resourcekey="LblActionDetails" SkinID="Label_DefaultBold"
                                   Text="تفاصيل الطلب"></asp:Label>
                           </td>
                       </tr>
                      
                   </table>
               </td>
           </tr>
                                        <tr>
                                            <td style="height: 18px">
                                                <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                    <tr>
                                                       <%-- <td style="width: 5px">
                                                        </td>--%>
                                                       <td style="width: 100%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <%--<td class="SeparArea">
                                                        </td>--%>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="LblRequestSerial" Width="120px" runat="server" Text="مسلسل الطلب"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="LblRequestSerialResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                         <Asp:TextBox ID="TxtRequestSerial" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                            </Asp:TextBox>
                                                        </td>
                                                       <td class="SeparArea">
                                                        &nbsp;&nbsp;
                                                        </td>
                                                      <td class="LabelArea">
                                                            <asp:Label ID="Label3" Width="90px" runat="server" Text="تاريخ الطلب"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelRequestDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                         <Asp:TextBox ID="txtRequestDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                            </Asp:TextBox>
                                                        </td>

                                                     </table>
                                            </td>         
                                                    </tr>

                                                        
                                                            <tr>
          <td style="width: 47%; height: 16px; vertical-align: top">
              <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                       
        <td class="SeparArea">
       </td>
       <td class="LabelArea" style="min-width: 90px;">
           <asp:Label ID="lblRequestType" runat="server" Text="Request Type" SkinID="Label_DefaultNormal"
               meta:resourcekey="lblRequestTypeResource1"></asp:Label>
       </td>
       <td class="DataArea">
           <asp:TextBox ID="txtRequestType" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
               AutoPostBack="True" meta:resourcekey="txtRequestTypeResource1" ReadOnly="true">
          </asp:TextBox>
       </td>



                                                          
                                                           <td class="SeparArea">
             </table>
</td>   
    </tr>

                             
                                        <tr>
                                            
                                           
                                            <td style="width: 47%; height: 6px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <%--<td class="SeparArea">
                                                        </td>--%>
                                                      <%--  <td class="LabelArea">
                                                        </td>--%>
                                                     
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
                                                        <td class="LabelArea" style="min-width: 90px;">
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEmployee" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
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
                                                       <%-- <td class="DataArea">
                                                              <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                                                        </td>--%>
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
                                                            <asp:Label ID="labelDescEnglishName" Width="90px" runat="server" Text="اسم الموظف"
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelDescEnglishNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtDescEnglishName" runat="server" SkinID="TextBox_LargeNormalC"
                                                                ReadOnly="True" MaxLength="255" meta:resourcekey="lblDescEnglishNameResource1"
                                                                TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <%--<td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>--%>
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
                    <asp:Label ID="Label5" runat="server" Width="90px" Text="الوظيفة" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblPositionResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="TxtPosition" runat="server" SkinID="TextBox_LargeNormalC" 
                        ReadOnly="True" MaxLength="255"  meta:resourcekey="TxtPositionResource1">
                   </asp:TextBox>
                </td>
             
            </tr>
        </table>
    </td>
   
    <td style="width: 47%; height: 16px; vertical-align: top">
        <table style="width: 100%; vertical-align: top" cellspacing="0">
            <tr>
               <%-- <td class="SeparArea">
                </td>
                <td class="LabelArea">
                </td>--%>
               <%-- <td class="DataArea">
                      <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                </td>--%>
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
                <td class="LabelArea" style="min-width: 90px;">
                    <asp:Label ID="Label9" runat="server" Text="القسم" SkinID="Label_DefaultNormal"
                        meta:resourcekey="lblDepartmentResource1"></asp:Label>
                </td>
                <td class="DataArea">
                    <asp:TextBox ID="TxtDepartment" runat="server" SkinID="TextBox_LargeNormalC"  MaxLength="30"
                        AutoPostBack="True" meta:resourcekey="TxtDepartmentResource1">
                   </asp:TextBox>
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
               <%-- <td class="DataArea">
                      <asp:ImageButton ID="ImageButton_Refresh" runat="server" ImageUrl="~/Common/Images/refresh.png" />
                </td>--%>
            </tr>
        </table>
    </td>
</tr>


                                        <tr>
                                            <td style="width: 47%; height: 6px; vertical-align: top">
                                            
                                            </td>
                                            <td style="width: 6%; height: 6px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 6px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <%--<td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            
                                                        </td>
                                                        <td class="DataArea">
                                                            
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="6" style="width: 100%; height: 10px; vertical-align: bottom;
                                                    border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label1" runat="server" meta:resourcekey="Label_Title1Resource2" SkinID="Label_DefaultBold"
                                                                Text="تفاصيل الطلب"></asp:Label>
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
                                                   
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                  
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                          
                                           
<%--                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblExecuseDate" runat="server" Width="90px" meta:resourcekey="lblWebDateChooser1Resource1"
                                                                SkinID="Label_DefaultNormal" Text="تاريخ الاذن"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExecuseDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                   <%--         <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="labelExecuseType" runat="server" Width="90px" SkinID="Label_DefaultNormal"
                                                                Text="نوع الاذن" meta:resourcekey="labelExecuseTypeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtExecuseType" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbRemainValResource1"
                                                                TabIndex="5" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                           <%-- <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>--%>
<%--                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                   
                                                    <tr>
                                                        <td class="SeparArea"> </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" Width="90px" meta:resourcekey="lblExecuseTimeResource1"
                                                                SkinID="Label_DefaultNormal" Text="مدة الاذن"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           <asp:TextBox ID="TxtExecuseTime" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbRemainValResource1"
                                                                TabIndex="5" ReadOnly="False"></asp:TextBox>
                                                        </td>
                                                        <td class="SeparArea"> </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>--%>
                                            <tr>
                                                <%--<td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea">
                                                            </td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblExecuseShift" runat="server" Width="90px" SkinID="Label_DefaultNormal"
                                                                    Text="الوردية" meta:resourcekey="llblExecuseShiftResource1"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="TxtExecuseShift" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbConsumeValResource1"
                                                                    TabIndex="6" AutoPostBack="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                                <%--<td style="width: 6%; height: 16px; vertical-align: top">
                                                </td>--%>
                                                <%--<td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                        <tr>
                                                            <td class="SeparArea">
                                                            </td>
                                                            <td class="LabelArea">
                                                                <asp:Label ID="lblWebDateChooser3" runat="server" Width="90px" meta:resourcekey="lblExecuseReasonResource1"
                                                                    SkinID="Label_DefaultNormal" Text=" سبب الاذن"></asp:Label>
                                                            </td>
                                                            <td class="DataArea">
                                                                <asp:TextBox ID="TxtExecuseReason" runat="server" SkinID="TextBox_SmalltNormalc" meta:resourcekey="lbConsumeValResource1"
                                                                    TabIndex="6" AutoPostBack="True"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>--%>
                                            </tr>--%>
                                               
                                                    <%--Remarks--%>



                                                     <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
              <td class="LabelArea">
    <asp:Label ID="LblRequestRemarks" runat="server" Width="90px" SkinID="Label_DefaultNormal"
        Text="ملاحظات " meta:resourcekey="lblLblRemarksResource1"></asp:Label>
</td>
                    <td class="DataArea">
        <asp:TextBox ID="TxtRequestRemarks" runat="server" Width="450"  meta:resourcekey="LblRemarksResource1"
            TabIndex="6" AutoPostBack="false"></asp:TextBox>
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
           
             </tr>
         </table>
     </td>
 </tr>
                                            <tr>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                    <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom;
                                                        border-bottom: 1px solid black">
                                                        <tr>
                                                            <td style="vertical-align: bottom">
                                                            <asp:Label ID="lblAccepts" runat="server" meta:resourcekey="Label_Title2Resource2" SkinID="Label_DefaultBold"
                                                                Text="الإعتمـــــادات"></asp:Label>
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <%--<td style="width: 6%; height: 16px; vertical-align: top">
                                                </td>--%>
                                                <td style="width: 47%; height: 16px; vertical-align: top">
                                                </td>
                                            </tr>
                                   
                                        </tr>
                                                </table>
                                            </td>
                                                       
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                       
                                            <tr>
    <td style="height: 100%; vertical-align: top" colspan="3">
        <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgEmployeeVacations" runat="server" EnableAppStyling="False"
            Height="100%" meta:resourcekey="uwgEmployeeVacationsResource1" SkinID="Default"
            Width="99%">
           
            <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                HeaderClickActionDefault="SortMulti" Name="uwgEmployeeVacations" RowHeightDefault="15px"
                SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowRowNumberingDefault="ByDataIsland">
                <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                    Width="99%">
                </FrameStyle>
               <ClientSideEvents AfterCellUpdateHandler="UwgSearchEmployees_AfterCellUpdateHandler"
                ClickCellButtonHandler="uwgEmployeeVacations_ClickCellButtonHandler" />
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
                <HeaderStyleDefault BackColor="#e2eefa" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
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
            </DisplayLayout>
            <Bands>
                <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                    <AddNewRow View="NotSet" Visible="NotSet">
                    </AddNewRow>
                    <Columns>
                        <igtbl:UltraGridColumn BaseColumnName="EmployeeName" Width="35%" DataType="System.Int32" Hidden="False"
                            Key="VacationTypeID" meta:resourcekey="EmployeeNameColumnResource1" Type="DropDownList">
                            
                            <Header Caption="اسم الموظف">
                            </Header>
                             <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                        </igtbl:UltraGridColumn>
                        
                      

                        <igtbl:UltraGridColumn BaseColumnName="Action"  DataType="System.String"
                            Format="dd/MM/yyyy hh:mm tt" Key="Action" meta:resourcekey="ActionColumnResource4"
                            Width="20%">
                            <Header Caption="الاجراء">
                                <RowLayoutColumnInfo OriginX="3" />
                            </Header>
                             <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                            <Footer>
                                <RowLayoutColumnInfo OriginX="3" />
                            </Footer>
                        </igtbl:UltraGridColumn>

                         <igtbl:UltraGridColumn BaseColumnName="ActionDate" DataType="System.DateTime"
                            Format="dd/MM/yyyy hh:mm tt" Key="ActionDate" meta:resourcekey="ActionDateColumnResource4"
                            Width="15%">
                            <Header Caption="التاريخ">
                                <RowLayoutColumnInfo OriginX="3" />
                            </Header>
                              <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                            <Footer>
                                <RowLayoutColumnInfo OriginX="3" />
                            </Footer>
                        </igtbl:UltraGridColumn>
                       

                        
                         <igtbl:UltraGridColumn BaseColumnName="ConfirmedNoOfdays" DataType="System.String"
                            Key="ConfirmedNoOfdays" meta:resourcekey="ConfirmedNoOfdaysColumnResource4"
                            Width="15%">
                            <Header Caption="الساعات المعتمدة">
                                <RowLayoutColumnInfo OriginX="3" />
                            </Header>
                              <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                            <Footer>
                                <RowLayoutColumnInfo OriginX="3" />
                            </Footer>
                        </igtbl:UltraGridColumn>
                       
                        
                         <igtbl:UltraGridColumn BaseColumnName="ActionRemarks" DataType="System.String"
                             Key="ActionRemarks" meta:resourcekey="ActionRemarksColumnResource4"
                            Width="20%">
                            <Header Caption="ملاحظات">
                                <RowLayoutColumnInfo OriginX="3" />
                            </Header>
                              <CellStyle HorizontalAlign="Center">
                        </CellStyle>
                            <Footer>
                                <RowLayoutColumnInfo OriginX="3" />
                            </Footer>
                        </igtbl:UltraGridColumn>
                                                                                          
                    </Columns>
                </igtbl:UltraGridBand>
                <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource2">
                    <Columns>
                        <igtbl:UltraGridColumn BaseColumnName="VacationTypeID" DataType="System.Int32" Key="VacationTypeID"
                            meta:resourcekey="UltraGridColumnResource1" Type="DropDownList" Width="200px">
                            <Header Caption="Vacation Type">
                            </Header>
                        </igtbl:UltraGridColumn>
                        <igtbl:UltraGridColumn BaseColumnName="ActualStartDate" DataType="System.DateTime"
                            Format="dd/MM/yyyy hh:mm tt" Key="ActualStartDate" meta:resourcekey="UltraGridColumnResource4"
                            Width="220px">
                            <Header Caption="Start Date">
                                <RowLayoutColumnInfo OriginX="1" />
                            </Header>
                            <Footer>
                                <RowLayoutColumnInfo OriginX="1" />
                            </Footer>
                        </igtbl:UltraGridColumn>
                        <igtbl:UltraGridColumn BaseColumnName="ActualEndDate" DataType="System.DateTime"
                            Format="dd/MM/yyyy hh:mm tt" Key="ActualEndDate" meta:resourcekey="UltraGridColumnResource5"
                            Width="220px">
                            <Header Caption="End Date">
                                <RowLayoutColumnInfo OriginX="2" />
                            </Header>
                            <Footer>
                                <RowLayoutColumnInfo OriginX="2" />
                            </Footer>
                        </igtbl:UltraGridColumn>
                  
                       
                          </Columns>

                    <AddNewRow View="NotSet" Visible="NotSet">
                    </AddNewRow>
                </igtbl:UltraGridBand>
            </Bands>
        </igtbl:UltraWebGrid>
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
