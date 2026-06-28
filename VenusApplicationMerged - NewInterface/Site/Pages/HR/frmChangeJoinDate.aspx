<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmChangeJoinDate.aspx.vb" Inherits="frmChangeJoinDate"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" 
    EnableSessionState="True" %>

<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Change Join Date</title>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        // =============================================
        // Modal Search Dialog
        // =============================================
        var ODialoge;
        var OSender;

        function OpenModal1(pageurl, height, width, CheckID, SenderCtrl) {
            if (CheckID == false) {
                var page = pageurl;
                var $dialog = $('<div></div>')
                    .html('<iframe style="border: 0px;" src="' + page + '" width="100%" height="100%"></iframe>')
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
                if (Sender) {
                    Sender.value = retvalue;
                    Sender.focus();
                    // trigger AutoPostBack لو الـ TextBox عنده AutoPostBack
                    if (Sender.onchange) {
                        Sender.onchange();
                    } else {
                        __doPostBack(Sender.name, '');
                    }
                }
            }
            if (ODialoge) {
                ODialoge.dialog('close');
            }
        }
    </script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="frmChangeJoinDate" runat="server">
    
    <!-- رسائل للـ JavaScript (مخبأة) -->
    <div style="display: none">
        <asp:Label ID="lblEmployeeCodeMsg" runat="server" Text="Employee Code is required / كود الموظف مطلوب"></asp:Label>
        <asp:Label ID="lblNewJoinDateMsg" runat="server" Text="New Join Date is required / تاريخ المباشرة الجديد مطلوب"></asp:Label>
        <asp:Label ID="lblReasonJoinDateMsg" runat="server" Text="Reason for changing Join Date is required / سبب تغيير تاريخ المباشرة مطلوب"></asp:Label>
        <asp:Label ID="lblReasonVacationMsg" runat="server" Text="Reason for changing Vacation Balance is required / سبب تغيير رصيد الاجازات مطلوب"></asp:Label>
        <asp:Label ID="lblValidationTitle" runat="server" Text="Please complete the following data / الرجاء إكمال البيانات التالية"></asp:Label>
        <asp:Label ID="lblConfirmMsg" runat="server" Text="Are you sure you want to change the Join Date? / هل أنت متأكد من رغبتك في تغيير تاريخ المباشرة؟"></asp:Label>
        
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1" Width="91px"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
    </div>
    
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <!-- شريط الأدوات -->
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display: none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" meta:resourcekey="ImageButton_SaveResource1" CommandArgument="Save"
                                    OnClientClick="return ValidateForm();" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    meta:resourcekey="ImageButton_NewResource1" CommandArgument="New" />
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
                    
                    <!-- Header -->
                    <table style="width: 100%; height: 42px; vertical-align: top">
                        <tr>
                            <td style="width: 32px; vertical-align: top">
                                <asp:Image ID="Image_Logo" runat="server" ImageAlign="Middle" ImageUrl="~/Common/Images/ToolBox/Hr_ToolBox/edit.png"
                                    meta:resourcekey="Image_LogoResource2" />
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
                                                            meta:resourcekey="lblRegDateResource2"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegDateValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegDateValueResource2"></asp:Label>
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
                                                            meta:resourcekey="lblRegUserResource2"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblRegUserValue" runat="server" SkinID="Label_CopyRightsNormal" meta:resourcekey="lblRegUserValueResource2"></asp:Label>
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
                                                            meta:resourcekey="lblCancelDateResource2"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;">
                                                        <asp:Label ID="lblCancelDateValue" runat="server" SkinID="Label_CopyRightsNormal"
                                                            meta:resourcekey="lblCancelDateValueResource2"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle;"></td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle;"></td>
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
                        meta:resourcekey="UltraWebTab1Resource2">
                        <Tabs>
                            <igtab:Tab Text="تغيير تاريخ المباشرة" meta:resourcekey="TabResource2">
                                <ContentTemplate>
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="4"></td>
                                        </tr>

                                        <!-- كود الموظف -->
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEmployeeCode" runat="server" Text="كود الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEmployeeCodeResource1"></asp:Label>
                                                            <asp:Label ID="Label_Star1" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star1Resource2"></asp:Label>
                                                        </td>
                                                        <td class="DataAreawithsearch">
                                                            <asp:TextBox ID="txtEmployeeCode" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtEmployeeCodeResource1"></asp:TextBox>
                                                        </td>
                                                        <td class="search">
                                                            <igtxt:WebImageButton ID="btnSearchEmployee" runat="server" AutoSubmit="False" Height="18px"
                                                                Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchEmployeeResource1">
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
                                            <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                        </tr>

                                        <!-- اسم الموظف تحت كود الموظف -->
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblEmployeeName" runat="server" Text="اسم الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblEmployeeNameResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEmployeeName" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtEmployeeNameResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                        </tr>

                                        <!-- معلومات الموظف الحالية -->
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="lblCurrentInfo" runat="server" meta:resourcekey="lblCurrentInfoResource1"
                                                                SkinID="Label_DefaultBold" Text="معلومات الموظف الحالية"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                        </tr>

                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblCurrentJoinDate" runat="server" Text="تاريخ المباشرة الحالي" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCurrentJoinDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtCurrentJoinDate" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtCurrentJoinDateResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblLastSalary" runat="server" Text="آخر راتب تم صرفه" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblLastSalaryResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtLastSalary" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtLastSalaryResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblCurrentCategory" runat="server" Text="الفئة الحالية" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCurrentCategoryResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtCurrentCategory" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtCurrentCategoryResource1"></asp:TextBox>
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
                                                            <asp:Label ID="lblCurrentBalance" runat="server" Text="الرصيد الحالي" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCurrentBalanceResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtCurrentBalance" runat="server" SkinID="TextBox_LargeNormalltr" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtCurrentBalanceResource1"></asp:TextBox>
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

                                        <!-- رصيد الاجازات المنفصل مع تاريخ الانتهاء في نفس السطر -->
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea" style="width: 22.5%;">
                                                            <asp:Label ID="lblAnnualVacation" runat="server" Text="الرصيد السنوي" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblAnnualVacationResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" style="width: 30%;">
                                                            <asp:TextBox ID="txtAnnualVacation" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtAnnualVacationResource1"></asp:TextBox>
                                                        </td>
                                                        <td class="LabelArea" style="width: 20%;">
                                                            <asp:Label ID="lblAnnualExpireDate" runat="server" Text="تاريخ الانتهاء" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblAnnualExpireDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" style="width: 30%;">
                                                            <asp:TextBox ID="txtAnnualExpireDate" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtAnnualExpireDateResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea" style="width: 22.5%;"></td>
                                                        <td class="DataArea" style="width: 30%;"></td>
                                                        <td class="LabelArea" style="width: 20%;"></td>
                                                        <td class="DataArea" style="width: 30%;"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea" style="width: 22.5%;">
                                                            <asp:Label ID="lblTransferredVacation" runat="server" Text="الرصيد المرحل" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblTransferredVacationResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" style="width: 30%;">
                                                            <asp:TextBox ID="txtTransferredVacation" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtAnnualVacationResource1"></asp:TextBox>
                                                        </td>
                                                        <td class="LabelArea" style="width: 20%;">
                                                            <asp:Label ID="txtTransferredExpireDate" runat="server" Text="تاريخ الانتهاء" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblAnnualExpireDateResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" style="width: 30%;">
                                                            <asp:TextBox ID="TextBox2" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                ReadOnly="True" BackColor="#F0F0F0" meta:resourcekey="txtAnnualExpireDateResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea" style="width: 22.5%;"></td>
                                                        <td class="DataArea" style="width: 30%;"></td>
                                                        <td class="LabelArea" style="width: 20%;"></td>
                                                        <td class="DataArea" style="width: 30%;"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <!-- البيانات الجديدة -->
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="lblNewData" runat="server" meta:resourcekey="lblNewDataResource1"
                                                                SkinID="Label_DefaultBold" Text="البيانات الجديدة"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                        </tr>

                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblNewJoinDate" runat="server" Text="تاريخ المباشرة الجديد" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblNewJoinDateResource1"></asp:Label>
                                                            <asp:Label ID="Label_Star2" runat="server" Text="*" Style="color: #FF0000" meta:resourcekey="Label_Star2Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igsch:WebDateChooser ID="txtNewJoinDate" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"
                                                                BorderWidth="1px" Height="18px" NullDateLabel="" Style="font-family: Tahoma;
                                                                font-size: 8pt; font-weight: Normal; color: Black; border: solid 1px #CCCCCC"
                                                                Width="130px">
                                                                <AutoPostBack ValueChanged="True" />
                                                            </igsch:WebDateChooser>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 1%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea"></td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblNewCategory" runat="server" Text="الفئة الجديدة" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblNewCategoryResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlNewCategory" runat="server" SkinID="DropDownList_LargNormal" 
                                                                Width="100%" meta:resourcekey="ddlNewCategoryResource1"></asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <!-- سبب التغيير تحت تاريخ المباشرة الجديد -->
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="lblReasonSection" runat="server" meta:resourcekey="lblReasonSectionResource1"
                                                                SkinID="Label_DefaultBold" Text="سبب التغيير"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                        </tr>

                                        <tr>
                                            <td style="width: 100%;" colspan="4">
                                                <table style="width: 100%;" cellspacing="5">
                                                    <tr>
                                                        <td style="width: 15%; font-weight: bold; vertical-align: top; padding: 5px;">
                                                            <asp:Label ID="lblReasonJoinDate" runat="server" 
                                                                Text="سبب تغيير تاريخ المباشرة:"
                                                                SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblReasonJoinDateResource1"></asp:Label>
                                                            <asp:Label ID="Label_Star4" runat="server" Text="*" Style="color: #FF0000"></asp:Label>
                                                        </td>
                                                        <td style="width: 85%; padding: 5px;">
                                                            <asp:TextBox ID="txtReasonJoinDate" runat="server" 
                                                                TextMode="MultiLine" SkinID="TextBox_LargeNormalrtl" 
                                                                Rows="3" Width="100%"
                                                                meta:resourcekey="txtReasonJoinDateResource1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                        <!-- رصيد الاجازات -->
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table cellspacing="6" style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="lblVacationSection" runat="server" meta:resourcekey="lblVacationSectionResource1"
                                                                SkinID="Label_DefaultBold" Text="رصيد الاجازات السنوي"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top"></td>
                                            <td style="width: 47%; height: 16px; vertical-align: top"></td>
                                        </tr>

                                        <!-- خيارات رصيد الاجازات -->
                                        <tr>
                                            <td style="width: 100%;" colspan="4">
                                                <div id="divVacationOptions" style="margin: 5px 10px; padding: 10px; background-color: #FFF3CD; border: 1px solid #FFC107; border-radius: 5px;">
                                                    <table style="width: 100%;" cellspacing="5">
                                                        <tr>
                                                            <td colspan="2" style="font-weight: bold; color: #856404;">
                                                                <asp:Label ID="lblVacationMessage" runat="server" 
                                                                    Text="يوجد رصيد إجازات متبقي. ماذا تريد أن تفعل بالرصيد؟"
                                                                    meta:resourcekey="lblVacationMessageResource1"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:RadioButton ID="rdoKeepVacation" runat="server" 
                                                                    Text="الإبقاء على الرصيد" 
                                                                    GroupName="VacationAction"
                                                                    meta:resourcekey="rdoKeepVacationResource1" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:RadioButton ID="rdoZeroVacation" runat="server" 
                                                                    Text="تصفير الرصيد" 
                                                                    GroupName="VacationAction"
                                                                    meta:resourcekey="rdoZeroVacationResource1" />
                                                            </td>
                                                        </tr>
                                                    </table>

                                                    <!-- سبب تصفير الرصيد - مخفي حتى يضغط على تصفير الرصيد -->
                                                    <div id="divVacationReason" style="display: none; margin-top: 10px;">
                                                        <table style="width: 100%;" cellspacing="5">
                                                            <tr>
                                                                <td style="width: 25%; font-weight: bold; vertical-align: top;">
                                                                    <asp:Label ID="lblVacationReason" runat="server" 
                                                                        Text="سبب تغيير رصيد الاجازات:"
                                                                        meta:resourcekey="lblVacationReasonResource1"></asp:Label>
                                                                    <asp:Label ID="Label_Star3" runat="server" Text="*" Style="color: #FF0000"></asp:Label>
                                                                </td>
                                                                <td style="width: 75%;">
                                                                    <asp:TextBox ID="txtReasonVacation" runat="server" 
                                                                        TextMode="MultiLine" SkinID="TextBox_LargeNormalrtl" 
                                                                        Rows="3" Width="100%"
                                                                        meta:resourcekey="txtReasonVacationResource1"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>

                                        <!-- رسائل النتائج -->
                                        <tr>
                                            <td style="width: 100%;" colspan="4">
                                                <asp:Label ID="lblMessage" runat="server" SkinID="Label_DefaultBold" ForeColor="Green" />
                                                <asp:Label ID="lblErrorMessage" runat="server" SkinID="Label_DefaultBold" ForeColor="Red" />
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

    <!-- JavaScript -->
    <script type="text/javascript">

        function ToggleVacationReason() {
            var rdoZero = document.getElementById('<%= rdoZeroVacation.ClientID %>');
            var divReason = document.getElementById('divVacationReason');
            if (rdoZero.checked) {
                divReason.style.display = 'block';
            } else {
                divReason.style.display = 'none';
            }
        }

        function ValidateForm() {
            var isValid = true;
            var errorMsg = '';

            var empCode = document.getElementById('<%= txtEmployeeCode.ClientID %>').value;
            if (empCode == '') {
                errorMsg += '- ' + document.getElementById('<%= lblEmployeeCodeMsg.ClientID %>').innerHTML + '\n';
                isValid = false;
            }

            var newDate = document.getElementById('<%= txtNewJoinDate.ClientID %>').value;
            if (newDate == '') {
                errorMsg += '- ' + document.getElementById('<%= lblNewJoinDateMsg.ClientID %>').innerHTML + '\n';
                isValid = false;
            }

            var reasonJoin = document.getElementById('<%= txtReasonJoinDate.ClientID %>').value;
            if (reasonJoin == '') {
                errorMsg += '- ' + document.getElementById('<%= lblReasonJoinDateMsg.ClientID %>').innerHTML + '\n';
                isValid = false;
            }

            var rdoZero = document.getElementById('<%= rdoZeroVacation.ClientID %>');
            var reasonVac = document.getElementById('<%= txtReasonVacation.ClientID %>').value;
            if (rdoZero.checked && reasonVac == '') {
                errorMsg += '- ' + document.getElementById('<%= lblReasonVacationMsg.ClientID %>').innerHTML + '\n';
                isValid = false;
            }

            if (!isValid) {
                var title = document.getElementById('<%= lblValidationTitle.ClientID %>').innerHTML;
                alert(title + ':\n' + errorMsg);
                return false;
            }

            var confirmMsg = document.getElementById('<%= lblConfirmMsg.ClientID %>').innerHTML;
            return confirm(confirmMsg);
        }

        $(document).ready(function () {
            $('#<%= rdoZeroVacation.ClientID %>').click(function () {
                ToggleVacationReason();
            });

            $('#<%= rdoKeepVacation.ClientID %>').click(function () {
                document.getElementById('divVacationReason').style.display = 'none';
            });
        });

    </script>
    
    </form>
</body>
</html>
