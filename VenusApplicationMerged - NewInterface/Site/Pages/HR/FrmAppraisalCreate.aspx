<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAppraisalCreate.aspx.vb" Inherits="frmHIPolicy"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" ResponseEncoding="UTF-8" %>

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
    <title>* Venus Payroll * ~HIPolicy</title>
    <style type="text/css">
        body {
            font-family: Tahoma;
            font-size: 9pt;
            margin: 0;
            padding: 0;
        }
        .Div_MasterContainer {
            width: 98%;
            margin: 0 auto;
            padding: 5px;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        .Details {
            padding: 5px;
        }
        .LabelArea {
            width: 20%;
            text-align: right;
            padding-right: 10px;
            font-weight: bold;
            vertical-align: middle;
        }
        .SeparArea {
            width: 2%;
        }
        .search {
            width: 5%;
            text-align: center;
        }
        input[type="text"], select {
            width: 95%;
            padding: 4px;
            border: 1px solid #ccc;
        }
        .radio-option {
            display: inline-block;
            margin-right: 15px;
        }
        #txtNotes {
            width: 98%;
            height: 80px;
        }
        #btnSearch {
            padding: 5px 15px;
            background-color: #d9534f;
            color: white;
            border: none;
            font-weight: bold;
            margin: 10px auto;
            display: block;
        }
        .toolbar-button {
            margin: 0 2px;
            vertical-align: middle;
        }
        .toolbar-separator {
            margin: 0 5px;
        }
<style type="text/css">
    /* تحسينات المسافات بين العناصر */
    .form-row {
        margin-bottom: 12px; /* زيادة المسافة بين الصفوف */
    }
    
    .LabelArea {
        width: 20%;
        text-align: right;
        padding-right: 15px; /* زيادة المسافة بين التسمية والحقل */
        font-weight: bold;
        vertical-align: middle;
    }
    
    .form-control {
        width: 95%;
        padding: 6px 8px;
        border: 1px solid #ccc;
        border-radius: 3px;
    }
    
    /* تحسين محاذاة العناصر في الصفوف المزدوجة */
    .dual-control-row {
        display: flex;
        align-items: center;
    }
    
    .dual-control-item {
        display: flex;
        align-items: center;
        width: 50%;
    }
    
    .dual-control-label {
        width: 40%;
        text-align: right;
        padding-right: 10px;
        font-weight: bold;
    }
    
    .dual-control-field {
        width: 60%;
    }
    
    /* تحسين منطقة الملاحظات */
    #txtNotes {
        width: 98%;
        height: 100px;
        padding: 8px;
        margin-top: 5px;
    }
    
    /* تحسين الأزرار */
    .search-button {
        margin-left: 10px;
    }
</style>    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
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

        var Row;
        var IsEdit = true;
        function uwg_AfterCellUpdateHandler(gridName, cellId) {
            debugger;
            if (IsEdit == true) {
                var cell = igtbl_getCellById(cellId);
                Row = igtbl_getRowById(cellId);
                if (cell.Column.Index == 1) {
                    var cellCode = Row.getCellFromKey("Code")
                    PageMethods.GetEmpName(cellCode.getValue(), OnSucceeded, OnFailed);
                    IsEdit = false;
                }
                var count = igtbl_getGridById(gridName).Rows.length - 1;
                var rowIndex = igtbl_getRowById(cellId).Id.split("_")[2];

                if (rowIndex == count) {

                    igtbl_addNew(gridName, 0, true, false);

                }
            }
        }

        var cell;
        function uwgEnterCellEdit(gridName, cellId) {
            cell = cellId;

        }

        function Open_Search(oEdit, keyCode, oEvent) {
            var cF9 = 120;
            switch (keyCode) {
                case (cF9):
                    {
                        var SearchID = window.document.all.item("lblSearchID").innerText;
                        Open_Search_KeyDown(SearchID, cell);
                    }
            }

        }

        function OnSucceeded(result, userContext, methodName) {
            if (methodName == 'GetEmpName') {
                if (result != '') {
                    var cell = Row.getCellFromKey("FullName");
                    cell.setValue(result);

                    var mList = window.document.all.item("lblList");
                    var cellHICompanyClasses = Row.getCellFromKey("HICompanyClasses");
                    cellHICompanyClasses.setValue(mList.innerText);

                    var cellNext = Row.getCellFromKey("CompanyAmt");
                    cellNext.activate();
                }
                else {
                    var mLang = window.document.all.item("lblLage");
                    if (mLang.innerText == '0')
                        alert("This code not found");
                    else
                        alert("هذا الرقم غير موجود");

                    var cell = Row.getCellFromKey("FullName");
                    cell.setValue("");

                    var cellCode = Row.getCellFromKey("Code");
                    cellCode.setValue("");
                    cellCode.activate();
                }
                IsEdit = true;
            }
        }

        function OnFailed(error) {
            //alert();
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
    <form id="frmHIPolicy" runat="server">
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
        <asp:Label ID="lblSearchID" runat="server" meta:resourcekey="lblSearchIDResource1"></asp:Label>
        <asp:Label ID="lblList" runat="server" meta:resourcekey="lblListResource1"></asp:Label>
        <igtxt:WebDateTimeEdit ID="txtDate" runat="server" meta:resourcekey="txtDateResource1"
            DisplayModeFormat="dd/MM/yyyy" EditModeFormat="dd/MM/yyyy">
        </igtxt:WebDateTimeEdit>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                        <tr>
                            <td style="display:none">
                                <asp:ImageButton ID="ImageButton1" Width="0px" Height="0px" runat="server" CommandArgument="N"
                                    meta:resourcekey="ImageButton1Resource1" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Save" Width="16px" Height="16px" runat="server"
                                    SkinID="HrSave_Command" CommandArgument="Save" meta:resourcekey="ImageButton_SaveResource1"
                                    OnClientClick="SaveOtherFieldsData();" CssClass="toolbar-button" />
                            </td>
                            <td style="width: 120px">
                                <asp:ImageButton ID="ImageButton_SaveN" Width="16px" Height="16px" runat="server"
                                    CommandArgument="SaveNew" SkinID="HrSaveN_Command" meta:resourcekey="ImageButton_SaveNResource1"
                                    OnClientClick="SaveOtherFieldsData();" CssClass="toolbar-button" />
                                <asp:LinkButton ID="LinkButton_SaveN" runat="server" Text="حفظ مع جديد" CommandArgument="SaveNew"
                                    meta:resourcekey="LinkButton_SaveNResource1" OnClientClick="SaveOtherFieldsData();" CssClass="toolbar-button"></asp:LinkButton>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_New" Width="16px" Height="16px" runat="server" SkinID="HrNew_Command"
                                    CommandArgument="New" meta:resourcekey="ImageButton_NewResource1" CssClass="toolbar-button" />
                            </td>
                            <td style="width: 24px">
       <asp:ImageButton ID="ImageButton_Delete" Width="16px" Height="16px" runat="server"
           SkinID="HrDelete_Command" CommandArgument="Delete" meta:resourcekey="ImageButton_DeleteResource1" />
   </td>
                            <td style="width: 40px">
                                <span class="toolbar-separator">|</span>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Print" Width="16px" Height="16px" runat="server"
                                    SkinID="HrPrint_Command" CommandArgument="Print" meta:resourcekey="ImageButton_PrintResource1" CssClass="toolbar-button" />
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Properties" Width="16px" Height="16px" runat="server"
                                    SkinID="HrProperties_Command" CommandArgument="Property" meta:resourcekey="ImageButton_PropertiesResource1" CssClass="toolbar-button" />
                                <asp:LinkButton ID="LinkButton_Properties" runat="server" Text="خصائص" CommandArgument="Property"
                                    meta:resourcekey="LinkButton_PropertiesResource1" CssClass="toolbar-button"></asp:LinkButton>
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Remarks" Width="16px" Height="16px" runat="server"
                                    SkinID="HrRemarks_Command" CommandArgument="Remarks" meta:resourcekey="ImageButton_RemarksResource1" CssClass="toolbar-button" />
                                <asp:LinkButton ID="LinkButton_Remarks" runat="server" Text="ملاحظات" CommandArgument="Remarks"
                                    meta:resourcekey="LinkButton_RemarksResource1" CssClass="toolbar-button"></asp:LinkButton>
                            </td>
                            <td style="width: 40px">
                                <span class="toolbar-separator">|</span>
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Last" Width="16px" Height="16px" runat="server"
                                    SkinID="HrLast_Command" CommandArgument="Last" meta:resourcekey="ImageButton_LastResource1" CssClass="toolbar-button" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Next" Width="16px" Height="16px" runat="server"
                                    SkinID="HrNext_Command" CommandArgument="Next" meta:resourcekey="ImageButton_NextResource1" CssClass="toolbar-button" />
                            </td>
                            <td style="width: 10px">
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_Back" Width="16px" Height="16px" runat="server"
                                    SkinID="HrBack_Command" CommandArgument="Previous" meta:resourcekey="ImageButton_BackResource1" CssClass="toolbar-button" />
                            </td>
                            <td style="width: 24px">
                                <asp:ImageButton ID="ImageButton_First" Width="16px" Height="16px" runat="server"
                                    SkinID="HrFirest_Command" CommandArgument="First" meta:resourcekey="ImageButton_FirstResource1" CssClass="toolbar-button" />
                            </td>
                            <td style="width: 30%">
                            </td>
                            <td style="width: 80px">
                                <asp:ImageButton ID="ImageButton_Help" Width="16px" Height="16px" runat="server"
                                    SkinID="HrHelp_Command" meta:resourcekey="ImageButton_HelpResource1" CssClass="toolbar-button" />
                                <asp:LinkButton ID="LinkButton_Help" runat="server" Text="مساعدة" meta:resourcekey="LinkButton_HelpResource1" CssClass="toolbar-button"></asp:LinkButton>
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
                                                    <td style="width: 40%; height: 16px; vertical-align: middle; text-align: right;">
                                                        <asp:Label ID="lblRegDate" runat="server" Text="سجل فى" meta:resourcekey="lblRegDateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle; text-align: right;">
                                                        <asp:Label ID="lblRegDateValue" runat="server" meta:resourcekey="lblRegDateValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle; text-align: right;">
                                                        <asp:Label ID="lblRegUser" runat="server" Text="سجل بواسطة" meta:resourcekey="lblRegUserResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle; text-align: right;">
                                                        <asp:Label ID="lblRegUserValue" runat="server" meta:resourcekey="lblRegUserValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle; text-align: right;">
                                                        <asp:Label ID="lblCancelDate" runat="server" Text="تاريخ الالغاء" meta:resourcekey="lblCancelDateResource1"></asp:Label>
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle; text-align: right;">
                                                        <asp:Label ID="lblCancelDateValue" runat="server" meta:resourcekey="lblCancelDateValueResource1"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 2%; vertical-align: top">
                                        </td>
                                        <td style="width: 49%; vertical-align: top">
                                            <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                <tr>
                                                    <td style="width: 40%; height: 16px; vertical-align: middle; text-align: right;">
                                                    </td>
                                                    <td style="width: 60%; height: 16px; vertical-align: middle; text-align: right;">
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
                                    <table style="width: 100%; font-family: Tahoma; font-size: 9pt;" cellpadding="2" cellspacing="2">
                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td>
                                                <asp:Label ID="LblCode" runat="server" Text="كود" meta:resourcekey="LblCodeResource1"></asp:Label>
                                            </td>
                                            <td style="width: 20%;">
                                                <asp:TextBox ID="TxtCode" runat="server" MaxLength="30" AutoPostBack="True" meta:resourcekey="txtCodeResource1" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="search" colspan="2">
                                                <igtxt:WebImageButton ID="btnSearchCode" runat="server" AutoSubmit="False" Height="18px"
                                                    Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchCodeResource1">
                                                    <Alignments TextImage="ImageBottom" />
                                                    <Appearance>
                                                        <Image Url="./Img/forum_search.gif" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td class="LabelArea">

                                                <asp:Label ID="Label3" runat="server" Text="التوصيف إنجليزي" SkinID="Label_DefaultNormal" meta:resourcekey="LblEngNameResource1"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtEngName" runat="server"  Width="95%" AutoPostBack="False" ></asp:TextBox>
                                            </td>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="SeparArea"></td>


                                        </tr>

                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td class="LabelArea">


                                          <asp:Label ID="LblArbName" runat="server" Text="التوصيف عربي" SkinID="Label_DefaultNormal"  meta:resourcekey="LblArbNameResource1"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="TxtArbName" runat="server"   Width="95%" AutoPostBack="False" ></asp:TextBox>
                                            </td>
                                                                                        <td class="SeparArea"></td>
                                                                                        <td class="SeparArea"></td>


                                        </tr>

                                       <%--  <tr>
    <td class="SeparArea"></td>
    <td class="LabelArea">
        <asp:Label ID="LblFromDate" runat="server" Text="من تاريخ" SkinID="Label_DefaultNormal"  meta:resourcekey="LblFromDateResource1"></asp:Label>
    </td>
    <td style="width: 30%;">
        <igsch:WebDateChooser ID="txtFromDate" runat="server" CssClass="form-control" />
    </td>
    <td>
        <asp:Label ID="LblToDate" runat="server" Text="إلى تاريخ" SkinID="Label_DefaultNormal"  meta:resourcekey="LblToDateResource1"></asp:Label>
    </td>
    <td style="width: 30%;">
        <igsch:WebDateChooser ID="txtToDate" runat="server" CssClass="form-control" />
    </td>
</tr>--%>

                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td class="LabelArea">
                                                <asp:Label ID="LblAppraisalType" runat="server" Text=" نوع التقييم" SkinID="Label_DefaultNormal"  meta:resourcekey="LblAppraisalTypeResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlappraisalType" runat="server" Width="95%" />
                                            </td>
                                            <td class="LabelArea">
                                                <asp:Label ID="LblAppCriteria" runat="server" Text=" معايير التقييم" SkinID="Label_DefaultNormal"  meta:resourcekey="LblAppCriteriaResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblByDepartment" runat="server" Text="بالقسم" SkinID="Label_DefaultNormal"  meta:resourcekey="lblByDepartmentResource1" />
                                                <asp:RadioButton ID="chkDepartment" runat="server" GroupName="EvalType" CssClass="radio-option" />
                                                <asp:Label ID="LblByPosition" runat="server" Text="بالوظيفة" meta:resourcekey="LblByPositionResource1" />
                                                <asp:RadioButton ID="ChkPosition" runat="server" GroupName="EvalType" CssClass="radio-option" />
                                            </td>
                                        </tr>

                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td>
                                                <asp:Label ID="LblBranch" runat="server" Text=" الفرع" SkinID="Label_DefaultNormal"  meta:resourcekey="LblBranchResource1"></asp:Label>
                                            </td>
                                            <td class="LabelArea">
                                                <asp:DropDownList ID="ddlBranch" runat="server" Width="95%" />
                                            </td>
                                            <td class="LabelArea">
                                                <asp:Label ID="LblPosition" runat="server" Text=" الوظيفة" meta:resourcekey="LblPositionResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPosition" runat="server" Width="95%" />
                                            </td>
                                        </tr>

                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td class="LabelArea">
                                                <asp:Label ID="LblSector" runat="server" Text=" القطاع" meta:resourcekey="LblSectorResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSector" runat="server" Width="95%" />
                                            </td>
                                            <td class="LabelArea">
                                                <asp:Label ID="LblDepartment" runat="server" Text=" القسم" meta:resourcekey="LblDepartmentResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddldepartment" runat="server" Width="95%" />
                                            </td>
                                        </tr>

                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td class="LabelArea">
                                                    <asp:Label ID="LblEmpCode" runat="server" Text="كود الموظف" meta:resourcekey="LblEmpCodeResource1"></asp:Label>
                                                </td>
                                                <td style="width: 10%;">
                                                    <asp:TextBox ID="TxtEmpCode" runat="server" MaxLength="30" AutoPostBack="True" meta:resourcekey="txtCodeResource1" Width="25%"></asp:TextBox>
                                                         <igtxt:WebImageButton ID="BtnSearchEmp" runat="server" AutoSubmit="False" Height="18px"
                                                         Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px" meta:resourcekey="btnSearchCodeResource1">
                                                         <Alignments TextImage="ImageBottom" />
                                                         <Appearance>
                                                             <Image Url="./Img/forum_search.gif" />
                                                         </Appearance>
                                                     </igtxt:WebImageButton>
                                                     <asp:Label ID="LblEmpName" runat="server" Text=" "></asp:Label>
                                                </td>

                                                
                                            <td class="LabelArea">
                                                <asp:Label ID="LblUnit" runat="server" Text=" الوحدة" meta:resourcekey="LblUnitResource1"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProject" runat="server" Width="95%" />
                                            </td>
                                        </tr>

                                         <tr>
                                            <td class="SeparArea"></td>
                                            <td class="LabelArea">              
                                                <asp:Label ID="LblRemarsk" runat="server" Text="الملاحظات" meta:resourcekey="LblRemarskResource1"></asp:Label>
                                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" Rows="2" Width="500px"  />
                                            </td>
                                               <td class="SeparArea"></td>
  <td class="SeparArea"></td>
                                        </tr>

                                         <tr>
                                            <td colspan="5" style="text-align: center; padding-top: 8px;">
<%--                                                <asp:Button ID="btnSearch" runat="server" Text="بحث" CssClass="search-button" meta:resourcekey="btnSearchResource1" />--%>
                                                       <igtxt:WebImageButton ID="btnSearch" runat="server" Height="5px" Style="font-family: Tahoma;
           font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="btnSearchResource1"
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
                                    </table>
                                     <tr>
     <td style="width: 50%; height: 16px; vertical-align: top">
         <table style="width: 100%; vertical-align: top" cellspacing="0">
             <tr>
                               <td style="width: 50%; height: 16px; vertical-align: top">                                     
                                <div style="margin-top: 20px;">
                                    <igtbl:UltraWebGrid Browser="UpLevel" ID="UwgCriteria" runat="server" EnableAppStyling="False"
                                        Height="250px" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                        Width="50%">
                                        <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                            AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                            BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                            RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                            StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                            Version="4.00" ViewType="OutlookGroupBy">
                                            <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="250px"
                                                Width="50%">
                                            </FrameStyle>
                                            <ClientSideEvents AfterCellUpdateHandler="uwg_AfterCellUpdateHandler" AfterEnterEditModeHandler="uwgEnterCellEdit" />
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
                                            <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1"
                                                AllowAdd="Yes">
                                                <AddNewRow View="NotSet" Visible="NotSet">
                                                </AddNewRow>
                                                <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                    <FilterDropDownStyle BackColor="SteelBlue" BorderColor="Silver" BorderStyle="Solid"
                                                        BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                        Font-Size="11px" Width="200px">
                                                        <Padding Left="2px" />
                                                    </FilterDropDownStyle>
                                                    <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                    </FilterHighlightRowStyle>
                                                </FilterOptions>
                                                <Columns>
                                                    <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True">
                                                        <Header Caption="">
                                                        </Header>
                                                    </igtbl:UltraGridColumn>
                                                  
                                                    <igtbl:UltraGridColumn AllowUpdate="Yes" type="DropDownList" BaseColumnName="CriteriaName" Key="CriteriaName"
                                                        meta:resourcekey="CriteriaNameResource3" Width="50%">
                                                        <Header Caption="المعيار">
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Header>
                                                        <CellStyle HorizontalAlign="Center">
                                                        </CellStyle>
                                                        <Footer>
                                                            <RowLayoutColumnInfo OriginX="2" />
                                                        </Footer>
                                                    </igtbl:UltraGridColumn>
                                                     
                                                     
                                                              <igtbl:UltraGridColumn BaseColumnName="ByValue" DataType="System.Boolean" Key="ByValue" meta:resourcekey="ByValueResource1"
                        Width="20%" Type="CheckBox"  >
                 <Header Caption="قيمة">
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

        <igtbl:UltraGridColumn BaseColumnName="ByPercentage" DataType="System.Boolean" Key="ByPercentage" meta:resourcekey="ByPercentageResource1"
                            Width="20%" Type="CheckBox"  >
                     <Header Caption="نسبة">
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
      
        <igtbl:UltraGridColumn BaseColumnName="MinimumScore"  Key="MinimumScore" DataType="System.string" meta:resourcekey="MinimumScoreResource1" >
                     <Header Caption="أقل قيمة">
                         <RowLayoutColumnInfo OriginX="5" />
                     </Header>
                     <CellStyle HorizontalAlign="Center">
                     </CellStyle>
                     <Footer>
                         <RowLayoutColumnInfo OriginX="3" />
                     </Footer>
     
     
   
                </igtbl:UltraGridColumn>
        <igtbl:UltraGridColumn BaseColumnName="MaximumScore"  Key="MaximumScore" DataType="System.string" meta:resourcekey="MaximumResource1" >
                 <Header Caption="أكبر قيمة">
                     <RowLayoutColumnInfo OriginX="5" />
                 </Header>
                 <CellStyle HorizontalAlign="Center">
                 </CellStyle>
                 <Footer>
                     <RowLayoutColumnInfo OriginX="3" />
                 </Footer>
     
     
   
            </igtbl:UltraGridColumn>
                                                     
                                                   <igtbl:UltraGridColumn AllowUpdate="Yes" BaseColumnName="Weight" Key="Weight" DataType="System.Decimal"
     meta:resourcekey="WeightColumnResource3" Width="50%">
     <Header Caption="النسبة من اجمالي التقييم">
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
                                </div>
</td>
                 <td></td>
                 <td style="width: 50%; height: 16px; vertical-align: top">                                     
                                   <div style="margin-top: 20px;">
                                       <igtbl:UltraWebGrid Browser="UpLevel" ID="UwgSearchEmployees" runat="server" EnableAppStyling="False"
                                           Height="250px" meta:resourcekey="uwgForNationalityResource1" SkinID="Default"
                                           Width="50%">
                                           <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" AllowDeleteDefault="Yes"
                                               AllowSortingDefault="OnClient" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                                               BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" Name="uwgForNationality"
                                               RowHeightDefault="18px" RowSelectorsDefault="No" SelectTypeRowDefault="Extended"
                                               StationaryMargins="Header" StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed"
                                               Version="4.00" ViewType="OutlookGroupBy">
                                               <FrameStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid"
                                                   BorderWidth="1px" Font-Names="Microsoft Sans Serif" Font-Size="8.25pt" Height="250px"
                                                   Width="50%">
                                               </FrameStyle>
                                               <ClientSideEvents AfterCellUpdateHandler="uwg_AfterCellUpdateHandler" AfterEnterEditModeHandler="uwgEnterCellEdit" />
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
                                               <igtbl:UltraGridBand AllowSorting="No" meta:resourcekey="UltraGridBandResource1"
                                                   AllowAdd="Yes">
                                                   <AddNewRow View="NotSet" Visible="NotSet">
                                                   </AddNewRow>
                                                   <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                       <FilterDropDownStyle BackColor="SteelBlue" BorderColor="Silver" BorderStyle="Solid"
                                                           BorderWidth="1px" CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                                           Font-Size="11px" Width="200px">
                                                           <Padding Left="2px" />
                                                       </FilterDropDownStyle>
                                                       <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                       </FilterHighlightRowStyle>
                                                   </FilterOptions>
                                                   <Columns>
                                                       <igtbl:UltraGridColumn BaseColumnName="ID" Key="ID" Hidden="True">
                                                           <Header Caption="">
                                                           </Header>
                                                       </igtbl:UltraGridColumn>
                                                       <igtbl:UltraGridColumn BaseColumnName="Code" Key="Code" meta:resourcekey="EmpCodeColumnResource2"
                                                           Width="90px" EditorControlID="txtEmpCode" Type="Custom">
                                                           <Header Caption="كود الموظف">
                                                               <RowLayoutColumnInfo OriginX="1" />
                                                           </Header>
                                                           <Footer>
                                                               <RowLayoutColumnInfo OriginX="1" />
                                                           </Footer>
                                                       </igtbl:UltraGridColumn>
                                                       <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="FullName" Key="FullName"
                                                           meta:resourcekey="EmpNameColumnResource3" Width="50%">
                                                           <Header Caption="اسم الموظف">
                                                               <RowLayoutColumnInfo OriginX="2" />
                                                           </Header>
                                                           <CellStyle HorizontalAlign="Center">
                                                           </CellStyle>
                                                           <Footer>
                                                               <RowLayoutColumnInfo OriginX="2" />
                                                           </Footer>
                                                       </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn AllowUpdate="No" BaseColumnName="Department" Key="Department"
                                                                      meta:resourcekey="DepartmentColumnResource3" Width="50%">
                                                                      <Header Caption="القسم">
                                                                          <RowLayoutColumnInfo OriginX="2" />
                                                                      </Header>
                                                                      <CellStyle HorizontalAlign="Center">
                                                                      </CellStyle>
                                                                      <Footer>
                                                                          <RowLayoutColumnInfo OriginX="2" />
                                                                      </Footer>
                                                                  </igtbl:UltraGridColumn>
                                                            <igtbl:UltraGridColumn Type="CheckBox" DataType="System.Boolean" AllowUpdate="yes" BaseColumnName="Select" Key="Select"
                                                                    meta:resourcekey="SelectGridColumnResource3" Width="50%">
                                                                    <Header Caption="تحديد">
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
                                   </div>
   </td>
             </tr>
         </table>
     </td>
     <td style="width: 6%; height: 16px; vertical-align: top">
     </td>
                                        

                              </tr>
                                </ContentTemplate>
                            </igtab:Tab>

                        </Tabs>
                    </igtab:UltraWebTab>
                   

                </td>
            </tr>
        </table>
    </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    var Deletebtn = $("#<%= ImageButton_Delete.ClientID %>");
        var confirmMessage = "<%= GetLocalResourceObject("ConfirmDelete").ToString().Replace("""", "\""") %>";

        Deletebtn.click(function () {
            if (!confirm(confirmMessage)) {
                return false;
            }
        });
    });
            </script>
    </form>
</body>
</html>