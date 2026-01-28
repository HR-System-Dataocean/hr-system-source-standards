<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAppraisalTypes.aspx.vb" Inherits="frmProfession"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Professions</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>

    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
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
                Sender.focus();
            }
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmProfession" runat="server">
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
                            <td style="display:none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                    OnClientClick="SaveOtherFieldsData();" />
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                    OnClientClick="SaveOtherFieldsData();" />
                                <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" meta:resourcekey="LinkButton_SaveNResource1"
                                    CommandArgument="SaveNew" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                    SkinID="HrDelete_Command" meta:resourcekey="ImageButton_DeleteResource1" CommandArgument="Delete" />
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" meta:resourcekey="ImageButton_PrintResource1" CommandArgument="Print" />
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                    SkinID="HrProperties_Command" meta:resourcekey="ImageButton_PropertiesResource1"
                                    CommandArgument="Property" />
                                <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" meta:resourcekey="LinkButton_PropertiesResource1"
                                    CommandArgument="Property"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRemarks_Command" meta:resourcekey="ImageButton_RemarksResource1" CommandArgument="Remarks" />
                                <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" meta:resourcekey="LinkButton_RemarksResource1"
                                    CommandArgument="Remarks"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                    SkinID="HrLast_Command" meta:resourcekey="ImageButton_LastResource1" CommandArgument="Last" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                    SkinID="HrNext_Command" meta:resourcekey="ImageButton_NextResource1" CommandArgument="Next" />
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                    SkinID="HrBack_Command" meta:resourcekey="ImageButton_BackResource1" CommandArgument="Previous" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                    SkinID="HrFirest_Command" meta:resourcekey="ImageButton_FirstResource1" CommandArgument="First" />
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
            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                <ContentTemplate>
                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top" cellspacing="0">
                        <tr>
                            <td style="height: 10px" colspan="3"></td>
                        </tr>
                        <tr>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea">
                                            <asp:Label ID="lblCode" runat="server" Text="Code" SkinID="Label_DefaultNormal"
                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_CodeResource1"></asp:Label>
                                        </td>
                                        <td class="DataAreawithsearch">
                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                AutoPostBack="True" meta:resourcekey="txtCodeResource1"></asp:TextBox>
                                        </td>
                                        <td class="search">
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
                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea"></td>
                                        <td class="DataArea"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea">
                                            <asp:Label ID="lblEngName" runat="server" Text="English Describtion" SkinID="Label_DefaultNormal"
                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                        </td>
                                        <td class="DataArea">
                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalltr" MaxLength="255"
                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea">
                                            <asp:Label ID="lblArbName" runat="server" Text="Arab Describtion" SkinID="Label_DefaultNormal"
                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                        </td>
                                        <td class="DataArea">
                                            <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalC" MaxLength="255"
                                                meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                           
                                </table>
                            </td>
                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea"></td>
                                        <td class="DataArea"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                     
                                                                         <tr>
     <td class="SeparArea"></td>
     <td class="LabelArea" style="  width: 100px;">
             <asp:Label ID="LblIsOneTimeOnly" runat="server" SkinID="Label_DefaultNormal" Text="One Time Only"
        meta:resourcekey="LblIsOneTimeOnlyResource1"></asp:Label>
</td>
     </td>
     <td class="DataArea" style="  width: 20px;">
         <asp:CheckBox ID="ChkIsOneTimeOnlye" runat="server" meta:resourcekey="IsOneTimeOnlyResource1"
    AutoPostBack="true" />
     </td>
    <td></td>
     <td class="LabelArea" style="width: 90px; padding-left: 20px;">
         <asp:Label ID="LblOneTimePeriod" runat="server" SkinID="Label_DefaultNormal" Text="One Time Period"
     meta:resourcekey="LblOneTimePeriodResource1"></asp:Label>
     </td>
     <td class="DataArea" style="  width: 150px;">
         <asp:TextBox ID="TxtOneTimePeriod" runat="server" SkinID="TextBox_LargeNormalC" 
             MaxLength="50" Width="40px" meta:resourcekey="txtAppraisalFrequencyResource1"></asp:TextBox>
     </td>
   <td class="LblDays" style="  padding-right: 10px; width: 40px;">
    <asp:Label ID="Label8" runat="server" SkinID="Label_DefaultNormal" Text="Days"
        meta:resourcekey="LblDaysResource1"></asp:Label>
</td>
 </tr>
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea" style="  width: 50px;">
                                            <asp:Label ID="LblAppraisalFrequency" runat="server" SkinID="Label_DefaultNormal" 
                                                Text="Appraisal Frequency" meta:resourcekey="lblAppraisalFrequencyResource1"></asp:Label>
                                        </td>
                                        <td class="DataArea" style="  width: 10px;">
                                            <asp:TextBox ID="txtAppraisalFrequency" runat="server" SkinID="TextBox_LargeNormalC" 
                                                MaxLength="50" Width="20px" meta:resourcekey="txtAppraisalFrequencyResource1"></asp:TextBox>
                                        </td>
                                        <td class="LabelArea" style="  padding-right: 10px; width: 40px;">
                                            <asp:Label ID="LblFrqDays" runat="server" SkinID="Label_DefaultNormal" Text="Days"
                                                meta:resourcekey="LblDaysResource1"></asp:Label>
                                        </td>

                                        <td class="LabelArea" style="width: 120px; padding-left: 20px;">
                                            <asp:Label ID="LblNotificationPeriod" runat="server" SkinID="Label_DefaultNormal" 
                                                Text="Notification Period" meta:resourcekey="LblNotificationPeriodResource1"></asp:Label>
                                        </td>
                                        <td class="DataArea" style="  width: 90px;">
                                            <asp:TextBox ID="TxtNotificationPeriod" runat="server" SkinID="TextBox_LargeNormalC" 
                                                MaxLength="50" Width="40px" meta:resourcekey="txtAppraisalFrequencyResource1"></asp:TextBox>
                                        </td>
                                        <td class="LabelArea" style="  padding-right: 10px; width: 40px;">
                                            <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultNormal" Text="Days"
                                                meta:resourcekey="LblDaysResource1"></asp:Label>
                                        </td>
                                    </tr>
                                <%--     <tr>
     <td class="SeparArea"></td>
     <td class="LabelArea" style="  width: 100px;">
         <asp:Label ID="LblEscelationMail" runat="server" SkinID="Label_DefaultNormal" 
             Text="Escelation Mail" meta:resourcekey="LblEscelationMailResource1"></asp:Label>
     </td>
     <td class="DataArea" style="  width: 20px;">
         <asp:TextBox ID="TxtEscelationMail" runat="server" SkinID="TextBox_LargeNormalC" 
             MaxLength="250" Width="170px" meta:resourcekey="txtAppraisalFrequencyResource1"></asp:TextBox>
     </td>
    <td></td>
     <td class="LblEscelationPeriod" style="width: 90px; padding-left: 20px;">
         <asp:Label ID="Label5" runat="server" SkinID="Label_DefaultNormal" 
             Text="Escelation Period" meta:resourcekey="lLblEscelationPeriodResource1"></asp:Label>
     </td>
     <td class="DataArea" style="  width: 150px;">
         <asp:TextBox ID="TXtEscelationPeriod" runat="server" SkinID="TextBox_LargeNormalC" 
             MaxLength="50" Width="40px" meta:resourcekey="txtAppraisalFrequencyResource1"></asp:TextBox>
     </td>
   <td class="LabelArea" style="  padding-right: 10px; width: 40px;">
    <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" Text="Days"
        meta:resourcekey="LblDaysResource1"></asp:Label>
</td>
 </tr>--%>
                                  
                                      <tr>
      <td class="SeparArea"></td>
      <td class="LabelArea" style=" ">
          <asp:Label ID="LblMinimumPercForImprovement" runat="server" Text="Minimum Perc For Improvement" SkinID="Label_DefaultNormal"
              meta:resourcekey="LblMinimumPercForImprovementResource1"></asp:Label>
      </td>
                                        <td class="LabelArea">
    <asp:TextBox ID="TxtMinPrecImprove" runat="server" SkinID="TextBox_LargeNormalC" 
        MaxLength="50" Width="40px" meta:resourcekey="txtAppraisalFrequencyResource1"></asp:TextBox>
                                               </td>
 
                                                    <td class="LabelArea" style="  padding-right: 10px; width: 40px;">

  <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultNormal" Text="%"
 meta:resourcekey="LblPercMarkResource1"></asp:Label>
                </td>                               
 

                                        <td class="LabelArea" style="  width: 100px;">
     <asp:Label ID="LblKPIWeight" runat="server" SkinID="Label_DefaultNormal" 
         Text="KPI's Weight" meta:resourcekey="LblKPIWeightResource1"></asp:Label>
 </td>
 <td class="DataArea" style="  width: 150px;">
     <asp:TextBox ID="TxtKPIsWeight" runat="server" SkinID="TextBox_LargeNormalC" 
         MaxLength="50" Width="10px" meta:resourcekey="txtAppraisalFrequencyResource1"></asp:TextBox>
   
 </td>
                                        <td class="LabelArea" style="  padding-right: 10px; width: 40px;">
                                                <asp:Label ID="LblPercMark" runat="server" SkinID="Label_DefaultNormal" Text="%"
 meta:resourcekey="LblPercMarkResource1"></asp:Label>
                                          </td>
  </tr>
                                                            <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea" style=" ">
                                            <asp:Label ID="LblAppraisalTypeGroup" runat="server" Text="Appraisal Type Group" SkinID="Label_DefaultNormal"
                                                meta:resourcekey="LblAppraisalTypeGroupResource1"></asp:Label>
                                        </td>
                                        <td class="DataArea" style="  width: 60px;">
                                                                <asp:DropDownList ID="ddlAppraisalTypeGroup" Width="200px" runat="server" AutoPostBack="False" SkinID="DropDownList_smallNormal"
    meta:resourcekey="ddlFormCodeResource1" TabIndex="3">
</asp:DropDownList>
  </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea"></td>
                                        <td class="DataArea"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
     
                        <tr>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea">&nbsp;</td>
                                        <td class="DataArea">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                            <td style="width: 47%; height: 16px; vertical-align: top">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td class="SeparArea"></td>
                                        <td class="LabelArea"></td>
                                        <td class="DataArea"></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 16px; vertical-align: top" colspan="3">
                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                    <tr>
                                        <td>
                                            <igtbl:UltraWebGrid Browser="UpLevel" ID="uwgAppraisalTypeConfiguration" runat="server" EnableAppStyling="True"
                                                Height="200px" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                                Width="100%">
                                                <DisplayLayout AllowColSizingDefault="Free" AllowDeleteDefault="Yes" AllowAddNewDefault="Yes" AllowColumnMovingDefault="OnServer"
                                                    AllowUpdateDefault="Yes" AutoGenerateColumns="False" BorderCollapseDefault="Separate"
                                                    Name="uwgSSConfiguration" RowHeightDefault="18px"
                                                    SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                    TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" CellClickActionDefault="Edit"
                                                    TabDirection="LeftToRight">
                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
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
                                                </DisplayLayout>
                                                <Bands>
                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1" AllowAdd="Yes">
                                                        <AddNewRow View="NotSet" Visible="Yes">
                                                        </AddNewRow>
                                                        <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                                                CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                                Font-Size="11px" Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                        </FilterOptions>
                                                        <Columns>
                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="true" Key="ID">
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="UserType" meta:resourcekey="UserTypeResource1" Key="UserType"
                                                                Type="DropDownList" Width="25%" AllowUpdate="Yes">
                                                                <Header Caption="Evaluator type">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <ValueList>
                                                                    <ValueListItems>
                                                                        <igtbl:ValueListItem DataValue="1" meta:resourcekey="Accept1Resource1" DisplayText="مدير مباشر" Key="1" />
                                                                        <igtbl:ValueListItem DataValue="2" meta:resourcekey="Accept2Resource1" DisplayText="درجة وظيفية" Key="2" />
                                                                        <igtbl:ValueListItem DataValue="3" meta:resourcekey="Accept3Resource1" DisplayText="موظف" Key="3" />
                                                                        <igtbl:ValueListItem DataValue="4" meta:resourcekey="Accept4Resource1" DisplayText="الموظف المقيم" Key="4" />
                                                                    </ValueListItems>
                                                                </ValueList>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="Position" Key="Position" Width="25%" meta:resourcekey="PositionUltraGridBandResource1"
                                                                Type="DropDownList">
                                                                <Header Caption="Position">
                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="25%" BaseColumnName="Employee" Key="Employee" meta:resourcekey="EmployeeUltraGridBandResource1" Type="DropDownList">
                                                                <Header Caption="Employee">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="25%" BaseColumnName="Rank" Key="Rank" meta:resourcekey="RankUltraGridBandResource1"
                                                                Type="DropDownList">
                                                                <Header Caption="Level">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="25%" BaseColumnName="StageWeight" Key="StageWeight" meta:resourcekey="StageWeightResource1">
                                                                <Header Caption="Weight">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn BaseColumnName="HasObjection" DataType="System.Boolean" Key="HasObjection" meta:resourcekey="HasObjectionUltraGridBandResource1"
                                                                Width="20%" Type="CheckBox" AllowUpdate="Yes">
                                                                <Header Caption="Has Objection">
                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                </Header>
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <SelectedCellStyle HorizontalAlign="Center">
                                                                </SelectedCellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="10" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="25%" BaseColumnName="NoOfObjections" Key="NoOfObjections" meta:resourcekey="NoOfObjectionsResource1" AllowUpdate="Yes">
                                                                <Header Caption="No Of Objections">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Width="25%" BaseColumnName="IsToConfirmOnly" DataType="System.Boolean" Key="IsToConfirmOnly" meta:resourcekey="IsToConfirmOnlyResource1" Type="CheckBox" AllowUpdate="Yes">
                                                                <Header Caption="Is To Confirm Only">
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Header>
                                                                <CellStyle HorizontalAlign="Center">
                                                                </CellStyle>
                                                                <Footer>
                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                </Footer>
                                                            </igtbl:UltraGridColumn>
                                                              <igtbl:UltraGridColumn Width="25%" BaseColumnName="CreateEscalation" DataType="System.Boolean" Key="CreateEscalation" meta:resourcekey="CreateEscalationResource1" Type="CheckBox" AllowUpdate="Yes">
                                                                      <Header Caption="Create Escalation">
                                                                          <RowLayoutColumnInfo OriginX="2" />
                                                                      </Header>
                                                                      <CellStyle HorizontalAlign="Center">
                                                                      </CellStyle>
                                                                      <Footer>
                                                                          <RowLayoutColumnInfo OriginX="2" />
                                                                      </Footer>
                                                                  </igtbl:UltraGridColumn>

                                                             <igtbl:UltraGridColumn Width="25%" BaseColumnName="EscalationPeriod" Key="EscalationPeriod" meta:resourcekey="EscalationPeriodResource1" AllowUpdate="Yes">
                                                                     <Header Caption="Escalation Period">
                                                                         <RowLayoutColumnInfo OriginX="2" />
                                                                     </Header>
                                                                     <CellStyle HorizontalAlign="Center">
                                                                     </CellStyle>
                                                                     <Footer>
                                                                         <RowLayoutColumnInfo OriginX="2" />
                                                                     </Footer>
                                                                 </igtbl:UltraGridColumn>

                                                                <igtbl:UltraGridColumn Width="25%" BaseColumnName="EscalationMail" Key="EscalationMail" meta:resourcekey="EscalationMailResource1" AllowUpdate="Yes">
            <Header Caption="Escalation Mail">
                <RowLayoutColumnInfo OriginX="2" />
            </Header>
            <CellStyle HorizontalAlign="Center">
            </CellStyle>
            <Footer>
                <RowLayoutColumnInfo OriginX="2" />
            </Footer>
        </igtbl:UltraGridColumn>
                                                        </Columns>
                                                    </igtbl:UltraGridBand>
                                                </Bands>
                                            </igtbl:UltraWebGrid>
                                        </td>
                                    </tr>
                                </table>
                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 100%" colspan="3"></td>
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
