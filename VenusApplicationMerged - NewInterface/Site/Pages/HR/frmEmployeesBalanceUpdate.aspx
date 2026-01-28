<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesBalanceUpdate.aspx.vb"
    Inherits="frmEmployeesBalanceUpdate" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Employees Decisions</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>
    <script language="javascript" src="Scripts/App_EmpVacations.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript" id="igClientScript">
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
    <form id="frmEmployeesBalanceUpdate" runat="server">
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
                                                            <asp:Label ID="lblEngName"  runat="server" Text="اسم الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEngName" Enabled="false" runat="server" SkinID="TextBox_LargeNormalC" ReadOnly="True"
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
                                                            <asp:TextBox ID="txtAraName" Enabled="false" runat="server" SkinID="TextBox_LargeNormalC" ReadOnly="True"
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
                                                                 <td id="myTd1" runat="server"  style="background:Gainsboro;width: 40%;" >
                                                                     <table style="width: 100%;" >
     <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                                   <asp:Label ID="lblNewBalance" runat="server" Width="150px" SkinID="Label_DefaultNormal"
    Text="New Balance Expire Date" meta:resourcekey="lblNewBalanceResource1"></asp:Label>
           </td>
           <td class="DataArea">
             <igtxt:WebMaskEdit ID="txtExpireDate" Enabled="false" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
</igtxt:WebMaskEdit>

                 </td>
                 
                                                        <td class="DataArea">
                                                            

                                                            <asp:Label ID="lblNewYear" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="جديد" meta:resourcekey="lblBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblNewBalanceNew" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                               </td>
                 <td class="SeparArea">
                                                        </td>
                                                        <td class="DataArea">

                                                             <asp:Label ID="lblConsumed" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="مستهلك" meta:resourcekey="lblConsumedBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblNewConsumed" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                               </td>
                 <td class="SeparArea">
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblRemain" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="متبقي" meta:resourcekey="lblRemainBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblNewRemain" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                            </td>
                 <td class="SeparArea">
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
                                   <asp:Label ID="lblTransferBalance" runat="server" Width="150px" SkinID="Label_DefaultNormal"
    Text="Transfered Balance Expire Date" meta:resourcekey="lblTransferBalanceResource1"></asp:Label>
           </td>
           <td class="DataArea">
             
               <igtxt:WebMaskEdit ID="txtTransferBalanceExpire" Enabled="false" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
</igtxt:WebMaskEdit>

                 </td>
                 
                                                        <td class="DataArea">
                                                            

                                                            <asp:Label ID="lblTransfer" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="مرحل" meta:resourcekey="lblBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblTransferValue" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                               </td>
                 <td class="SeparArea">
                                                        </td>
                                                        <td class="DataArea">

                                                             <asp:Label ID="Label5" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="مستهلك" meta:resourcekey="lblConsumedBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblTransferConsumedValue" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                               </td>
                 <td class="SeparArea">
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblTransferRemain" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="متبقي" meta:resourcekey="lblRemainBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblTransferRemainValue" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                            </td>
                 <td class="SeparArea">
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
                                   <asp:Label ID="lblOverlapExpire" runat="server" Width="150px" SkinID="Label_DefaultNormal"
    Text="Overlap Balance Expire Date" meta:resourcekey="lblOverlapResource1"></asp:Label>
           </td>
           <td class="DataArea">
             
                <igtxt:WebMaskEdit ID="txtOverlapExpire" Enabled="false" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
</igtxt:WebMaskEdit>
                 </td>
                 
                                                        <td class="DataArea">
                                                            

                                                            <asp:Label ID="lblOverlapBalance" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="تعويضي" meta:resourcekey="lblBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblOverlapValue" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                               </td>
                 <td class="SeparArea">
                                                        </td>
                                                        <td class="DataArea">

                                                             <asp:Label ID="lblOverlapConsumed" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="مستهلك" meta:resourcekey="lblConsumedBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblOverlapConsumedValue" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                               </td>
                 <td class="SeparArea">
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblOverlapRemain" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="متبقي" meta:resourcekey="lblRemainBalanceResource1"></asp:Label>
                                                            </td>
                                                        <td class="DataArea">
                                                            <asp:Label ID="lblOverlapRemainValue" runat="server" SkinID="Label_DefaultNormal"
                                                                Text="0" ></asp:Label>
                                                            </td>
                 <td class="SeparArea">
                                                        </td>
             </tr>
         </table>
     </td>
         </tr>                                        

</table>
</td>
                                                                <td style="width: 60%;">
                                                                    <table style="width: 100%;">
     <tr>
     <td style="width: 47%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                 <td class="SeparArea">
                 </td>
                 <td class="LabelArea">
                                   <asp:Label ID="lblNewExpireNew" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Expire Date" meta:resourcekey="lblNewExpireNewResource1"></asp:Label>
           </td>
           <td class="DataArea">
              
               <igtxt:WebMaskEdit ID="txtNewExpireNew" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
</igtxt:WebMaskEdit>
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
                                   <asp:Label ID="lblTransferExpireNew" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Expire Date" meta:resourcekey="lblNewExpireNewResource1"></asp:Label>
           </td>
           <td class="DataArea">
              
               <igtxt:WebMaskEdit ID="txtTransferExpireNew" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
</igtxt:WebMaskEdit>
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
                                   <asp:Label ID="lblOverlapExpireNew" runat="server" Width="80px" SkinID="Label_DefaultNormal"
    Text="Expire Date" meta:resourcekey="lblNewExpireNewResource1"></asp:Label>
           </td>
           <td class="DataArea">
              
               <igtxt:WebMaskEdit ID="txtOverlapExpireNew" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
</igtxt:WebMaskEdit>
                 </td>
             </tr>
         </table>
     </td>
         </tr>



</table>
</td>

                                                                

                            </tr>

                                                            <tr>
                                                <td style="height: 100%; vertical-align: top" colspan="3">
                                                    <igtbl:UltraWebGrid Browser="UpLevel" ID="uwgEmployeeVacations" runat="server" EnableAppStyling="False"
                                                        Height="1500px" meta:resourcekey="uwgEmployeeVacationsResource1" SkinID="Default"
                                                        Width="99%">
                                                       
                                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowSortingDefault="OnClient"
                                                            AutoGenerateColumns="False" BorderCollapseDefault="Separate" CellClickActionDefault="RowSelect"
                                                            CellPaddingDefault="1" CellSpacingDefault="1"   GridLinesDefault="NotSet"
                                                            HeaderClickActionDefault="SortMulti" Name="uwgEmployeeVacations" RowHeightDefault="15px"
                                                            SelectTypeRowDefault="Extended" StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True"
                                                            TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy" AllowRowNumberingDefault="ByDataIsland">
                                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="650px"
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
                                                            <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" Font-Size="9pt"
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
                                                                    Font-Size="11px" Height="300px" Width="100px">
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
                                                                    <igtbl:UltraGridColumn BaseColumnName="ConfigID" DataType="System.Int32" Hidden="True"
                                                                        Key="ConfigID" meta:resourcekey="UltraGridColumnResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="ConfigID">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>

                                                                   
                                                                    <igtbl:UltraGridColumn BaseColumnName="ID" DataType="System.Int32" Hidden="True"
                                                                        Key="ID" meta:resourcekey="IDResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="ID">
                                                                        </Header>
                                                                    </igtbl:UltraGridColumn>
                                                                   
                                                                    <igtbl:UltraGridColumn BaseColumnName="BalanceType" Width="20%" DataType="System.String" Hidden="False"
                                                                        Key="BalanceType" meta:resourcekey="BalanceTypeResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="نوع الرصيد ">
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                        <Footer>
                                                                   <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="OldExpireDate" Width="20%" DataType="System.Date"
                                                                        Format="dd/MM/yyyy" Hidden="False" Key="OldExpireDate" meta:resourcekey="OldExpireDateResource2">
                                                                        
                                                                        <Header Caption="OldExpireDate">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="NewExpireDate" Width="20%" DataType="System.Date"
                                                                        Format="dd/MM/yyyy" Hidden="False" Key="NewExpireDate" meta:resourcekey="NewExpireDateResource2">
                                                                        
                                                                        <Header Caption="OldExpireDate">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    <igtbl:UltraGridColumn BaseColumnName="RegUser" Width="20%" DataType="System.String" Hidden="False"
                                                                        Key="RegUser" meta:resourcekey="RegUserResource1" Type="DropDownList">
                                                                        
                                                                        <Header Caption="المستخدم ">
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                        <Footer>
                                                                   <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                    
                                                                    <igtbl:UltraGridColumn BaseColumnName="RegDate" Width="20%" DataType="System.Date"
                                                                        Format="dd/MM/yyyy" Hidden="False" Key="RegDate" meta:resourcekey="RegDateResource2">
                                                                        
                                                                        <Header Caption="RegDate">
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Header>
                                                                        <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                        <Footer>
                                                                            <RowLayoutColumnInfo OriginX="1" />
                                                                        </Footer>
                                                                    </igtbl:UltraGridColumn>
                                                                                                                
                                                                </Columns>
                                                            </igtbl:UltraGridBand>
                                                            
                                                        </Bands>
                                                    </igtbl:UltraWebGrid>
                                                </td>
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
