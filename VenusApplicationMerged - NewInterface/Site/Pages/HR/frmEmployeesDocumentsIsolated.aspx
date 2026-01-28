<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesDocumentsIsolated.aspx.vb"
    Inherits="frmEmployeesDocumentsIsolated" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
<title>* Venus Payroll * ~Employees Documents</title>
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
                    __doPostBack("txtEmployee", "TextChanged");
                }
                catch (err) { console.log(err + ' my eerrr ' + window.document.getElementById(OSender)+'  ' + OSender.ID + window.document.getElementById(OSender).value)}
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
<body style="height: 389px; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmEmployeesDocumentsIsolated" runat="server">
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
        <asp:HiddenField ID="txtDocumentID" runat="server" />
        <asp:HiddenField ID="txtObjectId" runat="server" />
        <asp:HiddenField ID="txtRecordID" runat="server" />

     <asp:Label ID="lblArbName" runat="server" meta:resourcekey="lblArbNameResource1"
            SkinID="Label_DefaultNormal" Text="Att. Arabic Name" Width="95px"></asp:Label>
        <asp:TextBox ID="txtDocumentArbName" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalrtl"
            meta:resourcekey="txtDocumentArbNameResource1"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" meta:resourcekey="lblEngNameResource1"
            SkinID="Label_DefaultNormal" Text="Att. English Name" Width="95px"></asp:Label>
        <asp:TextBox ID="txtDocumentEngName" runat="server" MaxLength="255" SkinID="TextBox_LargeNormalltr"
            meta:resourcekey="txtDocumentEngNameResource1"></asp:TextBox>

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
                                <asp:ImageButton ID="ImageButton3" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton4" Width="16px" Height="16px" runat="server"
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
                        meta:resourcekey="UltraWebTab1Resource1" Height="90px">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                   <table style= "vertical-align: top" cellspacing="0">
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
                                                            <asp:Label ID="Label1" runat="server" Text="الكود الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEmpCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="SeparArea" style="width: 10px;">
                                                        </td>
                                                        <td class="DataArea">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtEmployee" runat="server" AutoPostBack="True" MaxLength="30" SkinID="TextBox_SmalltNormalC"
                                                                            meta:resourcekey="txtEmployeeResource1" ClientIDMode="Static"></asp:TextBox>
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
                                    <table style="width: 85%; height: 83%; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 38%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCode" runat="server" Width="95px" Text="Document Type" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDocumentType" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlDocumentTypeResource1">
                                                            </asp:DropDownList>
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
                                                            &nbsp;
                                                        </td>
                                                        <td class="DataArea">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 5px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblDocumentNumber0" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Document Number" meta:resourcekey="lblDocumentNumber0Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" >
                                                            
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" style="width: 400px">
                                                                <ContentTemplate>
                                                                    <asp:TextBox ID="txtDocumentNumber0" runat="server" meta:resourcekey="TextBox_MachineCodeResource1"
                                                                        SkinID="TextBox_LargeNormalC" AutoPostBack="True"></asp:TextBox>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
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
                                                            <asp:Label ID="lblAttachedFile" runat="server" meta:resourcekey="lblAttachedFileResource1"
                                                                SkinID="Label_DefaultNormal" Text="Attached File" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:FileUpload ID="txtAttachedFile" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" meta:resourcekey="txtAttachedFileResource1" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: black;" Width="100%" />
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
                                                            <asp:Label ID="lblIssuedCity" runat="server" meta:resourcekey="lblIssuedCityResource1"
                                                                SkinID="Label_DefaultNormal" Text="Issued City" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DdlIssuedCity" runat="server" meta:resourcekey="DdlIssuedCityResource1"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
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
                                                            <asp:HyperLink ID="lnkDownload" runat="server" meta:resourcekey="lnkDownloadResource1"
                                                                Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal; color: Black"
                                                                Target="_blank" Text="Download Attached" Width="95px"></asp:HyperLink>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="DdlAttachedFiles" runat="server" meta:resourcekey="DdlAttachedFilesResource1"
                                                                            SkinID="DropDownList_LargNormal">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 24px;">
                                                                        <igtxt:WebImageButton ID="btnDeleteAttachment" runat="server" Height="16px" meta:resourcekey="btnDeleteResource1"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="~/Pages/HR/Img/logoff_small.gif" />
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
                                                            <asp:Label ID="lblIssueDate" runat="server" meta:resourcekey="lblIssueDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Issue Date" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtIssueDate" runat="server" InputMask="##/##/####" meta:resourcekey="txtIssueDateResource1"
                                                                SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri2" runat="server" meta:resourcekey="lblGergResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
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
                                                            <asp:Label ID="lblIssueDate0" runat="server" meta:resourcekey="lblIssueDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Issue Date" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtIssueDateH" runat="server" InputMask="##/##/####" meta:resourcekey="txtIssueDateHResource1"
                                                                SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri" runat="server" meta:resourcekey="lblHijriResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
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
                                                            <asp:Label ID="lblExpiryDate" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Expiry Date" meta:resourcekey="lblExpiryDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtExpiryDate" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix"
                                                                meta:resourcekey="txtExpiryDateResource1">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri3" runat="server" meta:resourcekey="lblGergResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
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
                                                            <asp:Label ID="lblExpiryDate0" runat="server" meta:resourcekey="lblExpiryDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Expiry Date" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtExpiryDateH" runat="server" InputMask="##/##/####" meta:resourcekey="txtExpiryDateHResource1"
                                                                SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri0" runat="server" meta:resourcekey="lblHijriResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
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
                                                            <asp:Label ID="lblLastRenewalDate" runat="server" Width="95px" SkinID="Label_DefaultNormal"
                                                                Text="Last Renewal" meta:resourcekey="lblLastRenewalDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtLastRenewalDate" runat="server" InputMask="##/##/####"
                                                                SkinID="WebMaskEdit_Fix" meta:resourcekey="txtLastRenewalDateResource1">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri4" runat="server" meta:resourcekey="lblGergResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Gerg)"></asp:Label>
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
                                                            <asp:Label ID="lblLastRenewalDate0" runat="server" meta:resourcekey="lblLastRenewalDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="Last Renewal" Width="95px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebMaskEdit ID="txtLastRenewalDateH" runat="server" InputMask="##/##/####"
                                                                meta:resourcekey="txtLastRenewalDateHResource1" SkinID="WebMaskEdit_Fix">
                                                            </igtxt:WebMaskEdit>
                                                            &nbsp;<asp:Label ID="lblHijri1" runat="server" meta:resourcekey="lblHijriResource1"
                                                                SkinID="Label_CopyRightsBold" Text="(Hijri)"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="برجاء تحديد الجنسيات المرتبطة"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: top;">
                                            </td>
                                            <td style="width: 47%; vertical-align: top;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%; vertical-align: top" colspan="3">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <igtbl:UltraWebGrid  Browser="UpLevel"   ID="UwgEmployeeDocuments" runat="server" EnableAppStyling="True"
                                                                Height="100%" Width="98%" SkinID="Default" meta:resourcekey="uwgForNationalityResource1">
                                                                <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                                                    AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" BorderCollapseDefault="Separate"
                                                                    HeaderClickActionDefault="SortSingle" Name="uwgForNationality" RowHeightDefault="18px"
                                                                    SelectTypeRowDefault="Extended" StationaryMargins="Header" AutoGenerateColumns="False"
                                                                    StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" ViewType="OutlookGroupBy">
                                                                    <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                                        BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="100%"
                                                                        Width="98%">
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
                                                                    <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" HorizontalAlign="Center"
                                                                        Height="20px" VerticalAlign="Middle" Font-Names="tahoma" Font-Size="9pt">
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
                                                                    <igtbl:UltraGridBand meta:resourcekey="UltraGridBandResource1">
                                                                        <AddNewRow View="NotSet" Visible="NotSet">
                                                                        </AddNewRow>
                                                                        <Columns>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" meta:resourcekey="UltraGridColumnResource1">
                                                                                <Header Caption="ID">
                                                                                </Header>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DocumentID" Key="DocumentID" Type="DropDownList"
                                                                                Width="50%" meta:resourcekey="UltraGridColumnResource2">
                                                                                <Header Caption="Document">
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="1" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ObjectID" Hidden="True" Key="ObjectID" meta:resourcekey="UltraGridColumnResource3">
                                                                                <Header Caption="ObjectID">
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="2" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="RecordID" Hidden="True" Key="RecordID" meta:resourcekey="UltraGridColumnResource4">
                                                                                <Header Caption="RecordID">
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="3" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="DocumentNumber" Key="DocumentNumber" Width="50%"
                                                                                meta:resourcekey="UltraGridColumnResource5">
                                                                                <Header Caption="DocumentNumber">
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="4" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IssueDate" Hidden="True" DataType="System.Date"
                                                                                Format="dd/MM/yyyy" Key="IssueDate" Width="15%" meta:resourcekey="UltraGridColumnResource6">
                                                                                <Header Caption="IssueDate">
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="5" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="IssuedCityID" Hidden="True" Key="IssuedCityID"
                                                                                meta:resourcekey="UltraGridColumnResource7">
                                                                                <Header Caption="IssuedCityID">
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="6" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="LastRenewalDate" Hidden="True" DataType="System.Date"
                                                                                Format="dd/MM/yyyy" Key="LastRenewalDate" Width="10%" meta:resourcekey="UltraGridColumnResource8">
                                                                                <Header Caption="LastRenewalDate">
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="7" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                            <igtbl:UltraGridColumn BaseColumnName="ExpiryDate" Hidden="True" DataType="System.Date"
                                                                                Format="dd/MM/yyyy" Key="ExpiryDate" Width="10%" meta:resourcekey="UltraGridColumnResource9">
                                                                                <Header Caption="ExpiryDate">
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Header>
                                                                                <Footer>
                                                                                    <RowLayoutColumnInfo OriginX="8" />
                                                                                </Footer>
                                                                            </igtbl:UltraGridColumn>
                                                                        </Columns>
                                                                        <RowEditTemplate>
                                                                            <p align="right">
                                                                                Document
                                                                                <input id="igtbl_TextBox_0_1" columnkey="DocumentID" style="width: 150px" type="text" /><br />
                                                                                DocumentNumber
                                                                                <input id="igtbl_TextBox_0_4" columnkey="DocumentNumber" style="width: 150px" type="text" /><br />
                                                                                IssueDate
                                                                                <input id="igtbl_TextBox_0_5" columnkey="IssueDate" style="width: 150px" type="text" /><br />
                                                                                LastRenewalDate
                                                                                <input id="igtbl_TextBox_0_7" columnkey="LastRenewalDate" style="width: 150px" type="text" /><br />
                                                                                ExpiryDate
                                                                                <input id="igtbl_TextBox_0_8" columnkey="ExpiryDate" style="width: 150px" type="text" /><br />
                                                                            </p>
                                                                            <br />
                                                                            <p align="center">
                                                                                <input id="igtbl_reOkBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px"
                                                                                    type="button" value="OK" />&nbsp;
                                                                                <input id="igtbl_reCancelBtn" onclick="igtbl_gRowEditButtonClick(event);" style="width: 50px"
                                                                                    type="button" value="Cancel" /></p>
                                                                        </RowEditTemplate>
                                                                        <RowTemplateStyle BackColor="White" BorderColor="White" BorderStyle="Ridge">
                                                                            <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" WidthTop="3px" />
                                                                        </RowTemplateStyle>
                                                                    </igtbl:UltraGridBand>
                                                                </Bands>
                                                            </igtbl:UltraWebGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="0" style="width: 100%; vertical-align: top">
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
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
