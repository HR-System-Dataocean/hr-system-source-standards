<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDataExport.aspx.vb" Inherits="frmDataExport"
    Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igsch" Namespace="Infragistics.WebUI.WebSchedule" Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtab" Namespace="Infragistics.WebUI.UltraWebTab" Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebNavigator.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ Data Export</title>
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
        function UltraWebTree1_NodeChecked(treeId, nodeId, bChecked) {
            var selectedNode = igtree_getNodeById(nodeId);
            var childNodes = selectedNode.getChildNodes();
            if (bChecked) {
                for (n in childNodes) {
                    childNodes[n].setChecked(true);
                }
            }
            else {
                for (n in childNodes) {
                    childNodes[n].setChecked(false);
                }
            }
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
        .auto-style2 {
            width: 25px;
            height: 16px;
        }
    </style>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmDataExport" runat="server">
    <div style="display: none">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" Width="99px"
            meta:resourcekey="realnameResource1"></asp:Label>
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
                            </td>
                            <td style="width: 20px">
                                <asp:ImageButton ID="ImageButton_Prepare" Width="14px" Height="12px" runat="server"
                                    CommandArgument="Prepare" ImageUrl="~/Pages/HR/Img/BttnExpnd.gif" meta:resourcekey="ImageButton_PrepareResource1" />
                            </td>
                            <td style="width: 200px">
                                <asp:LinkButton ID="LinkButton_Prepare" runat="server" Text="Export Data" CommandArgument="Prepare"
                                    Style="font-family: Tahoma; font-size: 8pt; font-weight: Normal;" meta:resourcekey="LinkButton_PrepareResource1"></asp:LinkButton>
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 200px">
                                &nbsp;
                            </td>
                            <td style="width: 20px">
                                &nbsp;
                            </td>
                            <td style="width: 100px">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" Style="font-family: Tahoma;
                                    font-size: 8pt; font-weight: Normal;" meta:resourcekey="LinkButton_HelpResource1"></asp:LinkButton>
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
                                <asp:Label ID="Label_Header" runat="server" Style="font-family: Tahoma; font-size: 8pt;
                                    font-weight: Normal;" meta:resourcekey="Label_HeaderResource1"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="Details" style="width: 100%; height: 100%; vertical-align: top">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                       height="100%"  meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="بيانات الموظفين" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 400px; vertical-align: top"
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
                                                            <asp:Label ID="Label_Nationality" runat="server" SkinID="Label_DefaultNormal" Text="الجنسية"
                                                                Width="90px" meta:resourcekey="Label_NationalityResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList_Nationality" runat="server" SkinID="DropDownList_LargNormal"
                                                                            meta:resourcekey="DropDownList_NationalityResource1">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
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
                                                            <asp:Label ID="lblBranch" runat="server" SkinID="Label_DefaultNormal" Text="الفرع"
                                                                Width="90px" meta:resourcekey="lblBranchResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBranche" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlBrancheResource1">
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
                                                            <asp:Label ID="Label_Location" runat="server" SkinID="Label_DefaultNormal" Text="الموقع"
                                                                Width="90px" meta:resourcekey="Label_LocationResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList_Location" runat="server" SkinID="DropDownList_LargNormal"
                                                                            meta:resourcekey="DropDownList_LocationResource1">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 25px;">
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
                                                            <asp:Label ID="lblDepartment" runat="server" SkinID="Label_DefaultNormal" Text="القسم"
                                                                Width="90px" meta:resourcekey="lblDepartmentResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlDepartmentResource1">
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
                                                            <asp:Label ID="Label_Education" runat="server" SkinID="Label_DefaultNormal" Text="نوع التعليم"
                                                                Width="90px" meta:resourcekey="Label_EducationResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList_Education" runat="server" SkinID="DropDownList_LargNormal"
                                                                            meta:resourcekey="DropDownList_EducationResource1">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 25px;">
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
                                                            <asp:Label ID="Label4" runat="server" SkinID="Label_DefaultNormal" Text="الفئة الوظيفية"
                                                                Width="90px" meta:resourcekey="Label4Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlClass" runat="server" SkinID="DropDownList_LargNormal" meta:resourcekey="ddlClassResource1">
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
                                                            <asp:Label ID="Label_CType" runat="server" SkinID="Label_DefaultNormal" Text="نوع التعاقد"
                                                                Width="90px" meta:resourcekey="Label_CTypeResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Ctype" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"
                                                                            meta:resourcekey="TextBox_CtypeResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton1" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="WebImageButton1Resource1">
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
                                                            <asp:Label ID="Label_Sponsor" runat="server" SkinID="Label_DefaultNormal" Text="الكفيل"
                                                                Width="90px" meta:resourcekey="Label_SponsorResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Sponsor" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"
                                                                            meta:resourcekey="TextBox_SponsorResource1"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Sponsor" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="WebImageButton_SponsorResource1">
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
                                                            <asp:Label ID="lblProject" runat="server" SkinID="Label_DefaultNormal" Text="من تاريخ تعيين" meta:resourcekey="lblFromappDateResource"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                           
                                                      

                                                              <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                         <asp:DropDownList ID="DropDownList_Project" runat="server" SkinID="DropDownList_LargNormal" >
                                                        </asp:DropDownList>

                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                      
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
                                                            <asp:Label ID="Label_FromJoinDate" runat="server" SkinID="Label_DefaultNormal" Text="من تاريخ تعيين" meta:resourcekey="Label_FromJoinDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 20%;">
                                                                        <igtxt:WebMaskEdit ID="WebMaskEdit_FromDate" runat="server" InputMask="##/##/####"
                                                                            SkinID="WebMaskEdit_Fix">
                                                                        </igtxt:WebMaskEdit>
                                                                    </td>
                                                                    <td >
                                                                        <asp:Label ID="Label_ToJoinDate" runat="server" meta:resourcekey="Label_ToJoinDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="الى تاريخ تعيين"></asp:Label>
                                                                    </td>
                                                                    <td style="width:20%;">
                                                                        <igtxt:WebMaskEdit ID="WebMaskEdit_ToDate" runat="server" InputMask="##/##/####"
                                                                            SkinID="WebMaskEdit_Fix">
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
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label1" runat="server" SkinID="Label_DefaultNormal" Text="من راتب" meta:resourcekey="Label_FromSalaryDateResource1" Height="16px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 35%;">
                                                                       <asp:TextBox ID="txtFromSalary" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"
                                                                            meta:resourcekey="TextBox_FromSalaryResource1"></asp:TextBox> 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="Label2" runat="server" SkinID="Label_DefaultNormal" Text="الى راتب" meta:resourcekey="Label_TOSalaryDateResource1"></asp:Label>
                                                                    </td>
                                                                     <td style="width: 35%;">
                                                                       <asp:TextBox ID="txtToSalary" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"
                                                                            meta:resourcekey="TextBox_ToSalaryResource1"></asp:TextBox> 
                                                                    </td>
                                                                    <td style="width: 25px;">
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
                                                            
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 30px; vertical-align: top;">
                                                <table style="width: 100%; height: 20px; vertical-align: bottom; border-bottom: 1px solid black"
                                                    cellspacing="6">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label_Title1" runat="server" Text="Please select Needed Informations"
                                                                SkinID="Label_DefaultBold" meta:resourcekey="Label_Title1Resource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; vertical-align: middle; text-align: center;">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td style="width: 47%; vertical-align: middle;">
                                                &nbsp;
                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="بدون موظفين نهاية الخدمة" meta:resourcekey="CheckBox1Resource1"
                                                    Font-Bold="True" Font-Size="10pt" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="Height: 100%; vertical-align: top" colspan="3">
                                                <ignav:UltraWebTree ID="UltraWebTree1" runat="server" CheckBoxes="True" EnableAppStyling="False"
                                                    Font-Names="Tahoma" Font-Size="10pt" Height="100%" Selectable="False" StyleSetName="Default"
                                                    DefaultImage="" HiliteClass="" HoverClass="" Indentation="20" meta:resourcekey="UltraWebTree1Resource1">
                                                    <ClientSideEvents NodeChecked="UltraWebTree1_NodeChecked" />
                                                    <NodePaddings Bottom="8px" />
                                                </ignav:UltraWebTree>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 100%" colspan="3">
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </igtab:Tab>
                            <igtab:Tab Text="نشاط المستخدمين" meta:resourcekey="TabResource2">
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
                                                            <asp:Label ID="Label_User" runat="server" SkinID="Label_DefaultNormal" Text="المستخدم"
                                                                Width="90px" meta:resourcekey="Label_UserResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:DropDownList ID="DropDownList_User" runat="server" SkinID="DropDownList_LargNormal"
                                                                            meta:resourcekey="DropDownList_UserResource1">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td style="width: 25px;">
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
                                                            <asp:Label ID="Label_Object" runat="server" SkinID="Label_DefaultNormal" Text="مجال النشاط"
                                                                Width="90px" meta:resourcekey="Label_ObjectResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DropDownList_Object" runat="server" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DropDownList_ObjectResource1">
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
                                                            <asp:Label ID="Label1_LogFromDate" runat="server" SkinID="Label_DefaultNormal" Text="من تاريخ"
                                                                Width="120px" meta:resourcekey="Label1_LogFromDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <igtxt:WebMaskEdit ID="WebMaskEdit_LogFrom" runat="server" InputMask="##/##/####"
                                                                            SkinID="WebMaskEdit_Fix">
                                                                        </igtxt:WebMaskEdit>
                                                                    </td>
                                                                    <td style="width: 25px;">
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
                                                            <asp:Label ID="Label_LogToDate" runat="server" meta:resourcekey="Label_LogToDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="الى تاريخ" Width="120px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <igtxt:WebMaskEdit ID="WebMaskEdit_LogTo" runat="server" InputMask="##/##/####" SkinID="WebMaskEdit_Fix">
                                                                        </igtxt:WebMaskEdit>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
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
