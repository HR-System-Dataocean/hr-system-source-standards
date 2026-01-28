<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesDecisions.aspx.vb"
    Inherits="frmEmployeesDecisions" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

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
    <title>* Venus Payroll * ~Employees Decisions</title>
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
    <form id="frmEmployeesDecisions" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="TargetControlResource1"></asp:Label>
        <asp:HiddenField ID="txtFormPermission" runat="server" />
        <asp:HiddenField ID="txtEmpID" runat="server" />
        <asp:HiddenField ID="txtID" runat="server" />
        <asp:TextBox ID="txtID1" runat="server" BackColor="SteelBlue" Style="left: 639px;
            position: absolute; top: 188px" Width="123px" Visible="False" meta:resourcekey="txtIDResource1"
            TabIndex="-1"></asp:TextBox>
        <igtxt:WebDateTimeEdit ID="WebDateTimeEdit1" runat="server" TabIndex="-1" meta:resourcekey="WebDateTimeEdit1Resource1">
        </igtxt:WebDateTimeEdit>
        <igtxt:WebDateTimeEdit ID="WebDateTimeEdit2" runat="server" TabIndex="-1" meta:resourcekey="WebDateTimeEdit2Resource1">
        </igtxt:WebDateTimeEdit>
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
                            <td style="width: 24px">
                                &nbsp;
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                    OValidationGroup="G" nClientClick="SaveOtherFieldsData();" />
                                <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" CommandArgument="SaveNew"
                                    meta:resourcekey="LinkButton_SaveNResource1" ValidationGroup="G" OnClientClick="SaveOtherFieldsData();"></asp:LinkButton>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                        SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                        ValidationGroup="G" OnClientClick="SaveOtherFieldsData();" />
                            </td>
                            <td style="width: 10px">
                                <asp:Label ID="Label33" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
                                    SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" />
                            </td>
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP1" runat="server" Text="|" meta:resourcekey="Label_TSP1Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" />
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                    SkinID="HrProperties_Command" CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" />
                                <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" CommandArgument="Property"
                                    meta:resourcekey="LinkButton_PropertiesResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" />
                                <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" CommandArgument="Remarks"
                                    meta:resourcekey="LinkButton_RemarksResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Documents" runat="server" CommandArgument="Documents"
                                    meta:resourcekey="ImageButton_DocumentsRec" Height="16px" ImageUrl="./img/abook_add_1.gif"
                                    Width="16px" />
                            </td> 
                            <td style="width: 40px">
                                <asp:Label ID="Label_TSP2" runat="server" Text="|" meta:resourcekey="Label_TSP2Resource1"></asp:Label>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                    SkinID="HrLast_Command" CommandArgument="Last" meta:resourcekey="ImageButton_LastResource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                    SkinID="HrNext_Command" CommandArgument="Next" meta:resourcekey="ImageButton_NextResource1" />
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                    SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                    SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" />
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
                                    <table style= vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 90%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea" style="width: 350px;">
                                                            <asp:Label ID="lblCode" runat="server" Text="الكود الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="SeparArea" style="width: 10px;">
                                                        </td>
                                                        <td class="DataArea">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmployee" runat="server" AutoPostBack="True" MaxLength="30" SkinID="TextBox_SmalltNormalC"
                                                                            meta:resourcekey="txtEmployeeResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 50%;">
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
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
                                            <td style=" height: 16px; vertical-align: top">
                                                &nbsp;
                                            </td>
                                            <td style=" height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea" >
                                                            <asp:Label ID="lblEngName" runat="server" Text="اسم الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalC" ReadOnly="True"
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                           
                                            
                                        </tr>

                                        <tr>
                                            <td style="width: 80%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea" >
                                                            <asp:Label ID="lblAraName" runat="server" Text="اسم الموظف عربي" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblAraNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtAraName" runat="server" SkinID="TextBox_LargeNormalC" ReadOnly="True"
                                                                meta:resourcekey="txtAraNameResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                           
                                        </tr>
                                        </table>
                                    <div id="accordion">
                                       <h3>
                                                <asp:Label ID="Label17" runat="server" Text="Personal information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label_Title2Resource1"></asp:Label>
                                            </h3>
                                            <div id="DivMainInfo">
                                               
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                 <td id="myTd1" runat="server"  style="background:Gainsboro;width: 50%;" >
                                                                     <table style="width: 100%;" >
     <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                                   <asp:Label ID="Label8" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Nationality" meta:resourcekey="lblNationalityResource1"></asp:Label>
           </td>
           <td class="DataArea">
              <asp:DropDownList ID="ddlOldNationality" runat="server" SkinID="DropDownList_LargNormal"
    meta:resourcekey="DdlNationalityResource1">
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
                                 <asp:Label ID="Label9" runat="server" SkinID="Label_DefaultNormal" Text="Marital Status"
     Width="80px" meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
           </td>
           <td class="DataArea">
                <asp:DropDownList ID="ddlOldMaritalStatus" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="DdlMaritalStatusResource1">
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
                       <asp:Label ID="Label10" runat="server" SkinID="Label_DefaultNormal" Text="Email"
      Width="80px" meta:resourcekey="lblEmailResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:TextBox ID="txtOldEmail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
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
                    <asp:Label ID="Label11" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Mobile No" meta:resourcekey="lblMobileResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:TextBox ID="txtOldMobile" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtMobileResource1"></asp:TextBox>
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
                     <asp:Label ID="Label12" runat="server" SkinID="Label_DefaultNormal" Text="Work E-Mail"
    Width="80px" meta:resourcekey="LblWorkEmailResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                    <asp:TextBox ID="txtOldWorkEmail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                 </td>
             </tr>
         </table>
     </td>
     <td style="width: 6%; height: 16px; vertical-align: top">
     </td>
    
 </tr>
</table>
</td>
                                                                <td style="width: 50%;">
                                                                    <table style="width: 100%;">
     <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                                   <asp:Label ID="lblNationality" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Nationality" meta:resourcekey="lblNationalityResource1"></asp:Label>
           </td>
           <td class="DataArea">
              <asp:DropDownList ID="DdlNationality" runat="server" SkinID="DropDownList_LargNormal"
    meta:resourcekey="DdlNationalityResource1">
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
                                 <asp:Label ID="lblMaritalStatus" runat="server" SkinID="Label_DefaultNormal" Text="Marital Status"
     Width="80px" meta:resourcekey="lblMaritalStatusResource1"></asp:Label>
           </td>
           <td class="DataArea">
                <asp:DropDownList ID="DdlMaritalStatus" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="DdlMaritalStatusResource1">
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
                       <asp:Label ID="lblEmail" runat="server" SkinID="Label_DefaultNormal" Text="Email"
      Width="80px" meta:resourcekey="lblEmailResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:TextBox ID="txtEmail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
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
                    <asp:Label ID="lblMobile" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Mobile No" meta:resourcekey="lblMobileResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:TextBox ID="txtMobile" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtMobileResource1"></asp:TextBox>
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
                     <asp:Label ID="LblWorkEmail" runat="server" SkinID="Label_DefaultNormal" Text="Work E-Mail"
    Width="80px" meta:resourcekey="LblWorkEmailResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                    <asp:TextBox ID="txtWorkE_Mail" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                 </td>
             </tr>
         </table>
     </td>
     <td style="width: 6%; height: 16px; vertical-align: top">
     </td>
    
 </tr>
</table>
</td>

                                                                

                            </tr></table></div>

                                      <h3>
                                                <asp:Label ID="Label3" runat="server" Text="Organization information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="lblOrgInfoResource1"></asp:Label>
                                            </h3>
                                            <div>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <table style="width: 100%;">
                                                             <tr>
                                                                  <td id="myTd2" runat="server" style="background:Gainsboro;width: 50%;">
                                                                    <table style="width: 100%;">
                                         <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                                    <asp:Label ID="Label13" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
   Text="Branch"></asp:Label>
           </td>
           <td class="DataArea">
               <asp:DropDownList ID="ddlOldBranch" runat="server" SkinID="DropDownList_LargNormal"
    AutoPostBack="True">
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
                                    <asp:Label ID="Label14" runat="server" meta:resourcekey="lblDepartmentResource1"
    SkinID="Label_DefaultNormal" Text="Department"></asp:Label>
           </td>
           <td class="DataArea">
                <asp:DropDownList ID="ddlOldDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
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
                                  <asp:Label ID="Label15" runat="server" Width="80px" SkinID="Label_DefaultNormal"
   Text="Sectors" meta:resourcekey="lblSectorsResource1"></asp:Label>
           </td>
           <td class="DataArea">
               <asp:DropDownList ID="ddlOldSectors" runat="server" SkinID="DropDownList_LargNormal">
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
                                   <asp:Label ID="Label16" runat="server" meta:resourcekey="lblLocationResource1"
    SkinID="Label_DefaultNormal" Text="Location" Width="80px"></asp:Label>
           </td>
           <td class="DataArea">
               <asp:DropDownList ID="ddlOldLocation" runat="server" meta:resourcekey="ddlLocationResource1"
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
                     <asp:Label ID="Label18" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Cost 1" meta:resourcekey="lblCost1Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                        <asp:textbox ID="txtOldCost1" runat="server" autopostback="true"  OnTextChanged="TxtCostCode1_TextChanged">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton5" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="Textbox2" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
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
                   <asp:Label ID="Label19" runat="server" SkinID="Label_DefaultNormal" Text="Cost 2"
    Width="80px" meta:resourcekey="lblCost2Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                        <asp:textbox ID="txtOldCost2" runat="server" autopostback="true">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton6" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="Textbox4" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
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
                    <asp:Label ID="Label20" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Cost 3" meta:resourcekey="lblCost3Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                         <asp:textbox ID="txtOldCost3" runat="server" autopostback="true">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton7" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="Textbox6" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
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
                      <asp:Label ID="Label21" runat="server" SkinID="Label_DefaultNormal" Text="Cost 4"
     Width="80px" meta:resourcekey="lblCost4Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                        <asp:textbox ID="txtOldCost4" runat="server" autopostback="true">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton8" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="Textbox8" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
                 </td>
             </tr>
         </table>
     </td>
 </tr>
                                                                        </table>
                                                                    </td>
                                                                <td style="width: 50%;">
                                                                    <table style="width: 100%;">
                                         <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                                    <asp:Label ID="Label1" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
   Text="Branch"></asp:Label>
           </td>
           <td class="DataArea">
               <asp:DropDownList ID="ddlBranch" runat="server" SkinID="DropDownList_LargNormal"
    AutoPostBack="True">
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
                                    <asp:Label ID="Label2" runat="server" meta:resourcekey="lblDepartmentResource1"
    SkinID="Label_DefaultNormal" Text="Department"></asp:Label>
           </td>
           <td class="DataArea">
                <asp:DropDownList ID="ddlDepartment" runat="server" meta:resourcekey="ddlDepartmentResource1"
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
                                  <asp:Label ID="Label6" runat="server" Width="80px" SkinID="Label_DefaultNormal"
   Text="Sectors" meta:resourcekey="lblSectorsResource1"></asp:Label>
           </td>
           <td class="DataArea">
               <asp:DropDownList ID="ddlSectors" runat="server" SkinID="DropDownList_LargNormal">
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
                                   <asp:Label ID="Label5" runat="server" meta:resourcekey="lblLocationResource1"
    SkinID="Label_DefaultNormal" Text="Location" Width="80px"></asp:Label>
           </td>
           <td class="DataArea">
               <asp:DropDownList ID="ddlLocation" runat="server" meta:resourcekey="ddlLocationResource1"
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
                     <asp:Label ID="LblCost1" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Cost 1" meta:resourcekey="lblCost1Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                        <asp:textbox ID="TxtCostCode1" runat="server" autopostback="true"  OnTextChanged="TxtCostCode1_TextChanged">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton1" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="TxtCostName1" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
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
                   <asp:Label ID="lblCostCode2" runat="server" SkinID="Label_DefaultNormal" Text="Cost 2"
    Width="80px" meta:resourcekey="lblCost2Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                        <asp:textbox ID="TxtCostCode2" runat="server" autopostback="true">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton2" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="TxtCostName2" runat="server"  Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
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
                    <asp:Label ID="lblCostCode3" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Cost 3" meta:resourcekey="lblCost3Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                         <asp:textbox ID="TxtCostCode3" runat="server" autopostback="true">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton3" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="TxtCostName3" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
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
                      <asp:Label ID="lblCostCode4" runat="server" SkinID="Label_DefaultNormal" Text="Cost 4"
     Width="80px" meta:resourcekey="lblCost4Resource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                        <asp:textbox ID="TxtCostCode4" runat="server" autopostback="true">
    </asp:textbox>
    <igtxt:WebImageButton ID="WebImageButton4" runat="server" AutoSubmit="False" Height="10px"
                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                            Width="20px">
                            <Alignments TextImage="ImageBottom" />
                            <Appearance>
                                <Image Url="./Img/forum_search.gif" />
                            </Appearance>
                        </igtxt:WebImageButton>
     <asp:textbox ID="TxtCostName4" runat="server" autopostback="true" Width="330" EnableTheming="True" Height="18px" meta:resourcekey="txtBirthDateResource1" NullDateLabel="" Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;
color: Black; border: solid 1px #CCCCCC" Value="" >
    </asp:textbox>
                 </td>
             </tr>
         </table>
     </td>
 </tr>
                                                                        </table>
                                                                    </td>

                                                                

                                                                 </tr>
</table></ContentTemplate></asp:UpdatePanel></div>
                                                                                <h3>
                                                <asp:Label ID="Label4" runat="server" Text="Others information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="lblOtherInfoResource1"></asp:Label>
                                            </h3>
                                            <div>
                                                <table style="width: 100%;">
                                                     <tr>
                                                           <td id="myTd3" runat="server" style="background:Gainsboro;width: 50%;">
                                                                    <table style="width: 100%;">
                                         <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                     <asp:Label ID="Label22" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Manager" meta:resourcekey="lblManagerResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <table style="width: 100%;">
     <tr>
         <td style="width: 50%;">
             <asp:TextBox ID="txtOldManager" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtManagerResource1"></asp:TextBox>
         </td>
         <td style="width: 50%;">
             <igtxt:WebImageButton ID="WebImageButton9" runat="server" AutoSubmit="False" Height="18px"
                 Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnManagerResource1">
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
                     <asp:Label ID="Label23" runat="server" SkinID="Label_DefaultNormal" Text="Sponsor"
    Width="80px" meta:resourcekey="lblSponsorResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                      <asp:DropDownList ID="ddlOldSponsor" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlSponsorResource1">
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
                     <asp:Label ID="Label24" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Bank" meta:resourcekey="lblBankResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlOldBank" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
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
                     <asp:Label ID="Label25" runat="server" SkinID="Label_DefaultNormal" Text="Bank Account"
    Width="80px" meta:resourcekey="lblBankAccountResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:TextBox ID="txtOldBankAccount" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtBankAccountResource1"></asp:TextBox>
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
                    <asp:Label ID="Label26" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Passport No." meta:resourcekey="lblPassportResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                    <asp:TextBox ID="txtOldPassport" runat="server" SkinID="TextBox_LargeNormalC"></asp:TextBox>
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
                    <asp:Label ID="lblIdentity" runat="server" SkinID="Label_DefaultNormal" Text="Identity No."
                                                                Width="80px" meta:resourcekey="lblIdentityResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                    <asp:TextBox ID="txtOldIdentity" runat="server" SkinID="TextBox_LargeNormalC"></asp:TextBox>
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
                      <asp:Label ID="Label27" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Last Educations" meta:resourcekey="lblLastEducationsResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                      <asp:DropDownList ID="ddlOldLastEducations" runat="server" SkinID="DropDownList_LargNormal">
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
                      <asp:Label ID="Label28" runat="server" SkinID="Label_DefaultNormal" Text="Graduation Date"
      Width="80px" meta:resourcekey="lblGraduationDateResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <igtxt:WebMaskEdit ID="txtOldGraduationDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
 </igtxt:WebMaskEdit>
                 </td>
             </tr>
         </table>
     </td>
    
    
 </tr>
                                                                        </table>

                                                           </td>
                                                                <td style="width: 50%;">
                                                                    <table style="width: 100%;">
                                         <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                     <asp:Label ID="lblManager" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Manager" meta:resourcekey="lblManagerResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <table style="width: 100%;">
     <tr>
         <td style="width: 50%;">
             <asp:TextBox ID="txtManager" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtManagerResource1"></asp:TextBox>
         </td>
         <td style="width: 50%;">
             <igtxt:WebImageButton ID="btnManager" runat="server" AutoSubmit="False" Height="18px"
                 Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnManagerResource1">
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
                     <asp:Label ID="lblSponsor" runat="server" SkinID="Label_DefaultNormal" Text="Sponsor"
    Width="80px" meta:resourcekey="lblSponsorResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                      <asp:DropDownList ID="ddlSponsor" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlSponsorResource1">
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
                     <asp:Label ID="lblBank" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Bank" meta:resourcekey="lblBankResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlBank" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlBankResource1">
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
                     <asp:Label ID="lblBankAccount" runat="server" SkinID="Label_DefaultNormal" Text="Bank Account"
    Width="80px" meta:resourcekey="lblBankAccountResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:TextBox ID="txtBankAccount" runat="server" SkinID="TextBox_LargeNormalC" meta:resourcekey="txtBankAccountResource1"></asp:TextBox>
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
                    <asp:Label ID="lblPassport" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Passport No." meta:resourcekey="lblPassportResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                    <asp:TextBox ID="txtPassport" runat="server" SkinID="TextBox_LargeNormalC"></asp:TextBox>
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
                    <asp:Label ID="Label34" runat="server" SkinID="Label_DefaultNormal" Text="Identity No."
                                                                Width="80px" meta:resourcekey="lblIdentityResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                    <asp:TextBox ID="txtIdentity" runat="server" SkinID="TextBox_LargeNormalC"></asp:TextBox>
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
                      <asp:Label ID="lblLastEducations" runat="server" Width="80px" SkinID="Label_DefaultNormal"
     Text="Last Educations" meta:resourcekey="lblLastEducationsResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                      <asp:DropDownList ID="ddlLastEducations" runat="server" SkinID="DropDownList_LargNormal">
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
                      <asp:Label ID="lblGraduationDate" runat="server" SkinID="Label_DefaultNormal" Text="Graduation Date"
      Width="80px" meta:resourcekey="lblGraduationDateResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <igtxt:WebMaskEdit ID="txtGraduationDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Default">
 </igtxt:WebMaskEdit>
                 </td>
             </tr>
         </table>
     </td>
    
    
 </tr>
                                                                        </table></td>

                                                       

                                                         </tr>
                                                    </table></div>

                                         <h3>
                                                <asp:Label ID="Label7" runat="server" Text="Current contract information" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="lblContractResource1"></asp:Label>
                                            </h3>
                                            <div id="DivContract" runat="server">
                                                <table style="width: 100%;">
                                                    <tr>
                                                           <td id="myTd4" runat="server" style="background:Gainsboro;width: 50%;">
                                                                    <table style="width: 100%;">
                                         <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                     <asp:Label ID="Label29" runat="server" SkinID="Label_DefaultNormal" Text="Contract Type"
    Width="80px" meta:resourcekey="lblContractTypeResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlOLdContractType" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlContractTypeResource1">
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
                      <asp:Label ID="Label30" runat="server" SkinID="Label_DefaultNormal" Text="Profession"
     Width="80px" meta:resourcekey="lblProfessionsResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlOLdProfessions" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlProfessionsResource1">
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
                     <asp:Label ID="Label31" runat="server" SkinID="Label_DefaultNormal" Text="Position"
     Width="80px" meta:resourcekey="lblPositionResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlOLdPosition" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlPositionResource1">
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
                   <asp:Label ID="Label32" runat="server" SkinID="Label_DefaultNormal" Text="Employee Class"
    Width="80px" meta:resourcekey="lblEmployeeClassResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlOLdEmployeeClass" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlEmployeeClassResource1">
 </asp:DropDownList>
                 </td>
             </tr>
         </table>
     </td>
 </tr>

                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                            </td>
                                        </tr>
                                                                        </table></td>
                                                                <td style="width: 50%;">
                                                                    <table style="width: 100%;">
                                         <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                     <asp:Label ID="lblContractType" runat="server" SkinID="Label_DefaultNormal" Text="Contract Type"
    Width="80px" meta:resourcekey="lblContractTypeResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlContractType" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlContractTypeResource1">
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
                      <asp:Label ID="lblProfessions" runat="server" SkinID="Label_DefaultNormal" Text="Profession"
     Width="80px" meta:resourcekey="lblProfessionsResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlProfessions" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlProfessionsResource1">
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
                     <asp:Label ID="lblPosition" runat="server" SkinID="Label_DefaultNormal" Text="Position"
     Width="80px" meta:resourcekey="lblPositionResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlPosition" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlPositionResource1">
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
                   <asp:Label ID="lblEmployeeClass" runat="server" SkinID="Label_DefaultNormal" Text="Employee Class"
    Width="80px" meta:resourcekey="lblEmployeeClassResource1"></asp:Label>
                 </td>
                 <td class="DataArea">
                     <asp:DropDownList ID="ddlEmployeeClass" runat="server" SkinID="DropDownList_LargNormal"
     meta:resourcekey="ddlEmployeeClassResource1">
 </asp:DropDownList>
                 </td>
             </tr>
         </table>
     </td>
 </tr>

                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                            </td>
                                        </tr>
                                                                        </table></td>

                                                     

                                                        </tr>
                                                    </table></div>

                                    <%--</table>--%>

                                        </div>
                                </ContentTemplate>
                            </igtab:Tab>
                        </Tabs>
                    </igtab:UltraWebTab>
                </td>
            </tr>
        </table>
    </div>
          <script type="text/javascript" id="igClientScript">
              $(document).ready(function () {
                  var Deletebtn = $("#<%=ImageButton_Delete.ClientID%>")

            Deletebtn.click(function () {
                if (confirm("هل انت متأكد من الحذف؟") == false) {

                    return false;
                }

            })


        });

          </script>
    </form>
</body>
</html>
