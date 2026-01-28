<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocumentView.aspx.vb" Inherits="DocumentView"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<%@ Register Assembly="C1.Web.Wijmo.Controls.3" Namespace="C1.Web.Wijmo.Controls.C1ReportViewer"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Documents View</title>
</head>
<body>
    <cc1:C1ReportViewer ID="C1ReportViewer1" runat="server" Width="100%" Height="100%"
        AvailableTools="Search, Thumbs" CollapseToolsPanel="True" FullScreen="True" PagedView="False"
        ExpandedTool="Search" Culture="ar-SA" Zoom="100%" />
    <form id="form1" runat="server" defaultbutton="ImageButton_Enter">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        <Services>
            <asp:ServiceReference Path="../../Common/WebServices/CustomFormsWs.asmx" />
            <asp:ServiceReference Path="../../Common/WebServices/PoliciesWs.asmx" />
        </Services>
    </asp:ScriptManager>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.mouse.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.position.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.draggable.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.droppable.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.sortable.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.selectable.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.resizable.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.ui.accordion.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/UI/jquery.effects.core.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery.event.drag-1.5.min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/colorPicker.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/alerts.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/masks.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/Cal/jquery.calendars.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/Cal/jquery.calendars.plus.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/Cal/jquery.calendars.picker.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/Cal/jquery.calendars.islamic.js" type="text/javascript"></script>
      <script src="../../Common/Script/JQuery/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript">
        var ODialoge;
        function OpenModal1(pageurl, height, width) {
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
        function CloseIt(retvalue) {
            var $dialog = ODialoge;
            $dialog.dialog('close');
        }

        var _nct = "none";
        var _active_control_id = "none";
        var cont_counter = 1;
        var cont_array = new Array(1);
        var IsWorkFlow = false;
        var CurrStage = '';
        var CurrTrnsID = '';
        var CurrWF = '';
        var CodeVal = '';

        var CurAction = '';
        var CurDocument = '';
        var CurLen = '';
        var CurLen1 = '';
        $(function () {
            $('#<%= ImageButton_Send.ClientID%>').click(function () {
                $.blockUI({ message: '' });
            });
            $("#Peoplesdialog-form").dialog({
                autoOpen: false,
                height: 410,
                width: 520,
                modal: true,
                buttons: {
                    "Save": function () {
                        grid = igtbl_getGridById("UWG_Peoples");
                        if (grid.Rows.length == CurLen) {
                            var Columns = "";
                            jQuery.each(cont_array, function (i, val) {
                                if (i < cont_array.length - 1) {
                                    if (val[16] != '1') {
                                        var ElementVal = $('#' + cont_array[i][0])[0].value;
                                        if (val[1] == "Radio" || val[1] == "CheckBox") {
                                            ElementVal = $('#' + cont_array[i][0])[0].checked;
                                        }
                                        Columns = Columns + "|" + cont_array[i][0] + ";" + cont_array[i][1] + ";" + ElementVal;
                                    }
                                }
                            });
                            var PeopleList = "0";
                            for (x = 0; x < grid.Rows.length; x++) {
                                row = grid.Rows.getRow(x);
                                PeopleList = PeopleList + ";" + row.getCell(0).getValue();
                            }
                            CustomFormsWs.CheckStageNotify(CurDocument, CurrStage, CurAction, PeopleList, AfterCheckNotify);
                            $(this).dialog("close");
                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#PeoplesNotifydialog-form").dialog({
                autoOpen: false,
                height: 410,
                width: 520,
                modal: true,
                buttons: {
                    "Save": function () {
                        grid = igtbl_getGridById("UWG_PeoplrNotify");
                        if (grid.Rows.length == CurLen1) {
                            var Columns = "";
                            jQuery.each(cont_array, function (i, val) {
                                if (i < cont_array.length - 1) {
                                    if (val[16] != '1') {
                                        var ElementVal = $('#' + cont_array[i][0])[0].value;
                                        if (val[1] == "Radio" || val[1] == "CheckBox") {
                                            ElementVal = $('#' + cont_array[i][0])[0].checked;
                                        }
                                        Columns = Columns + "|" + cont_array[i][0] + ";" + cont_array[i][1] + ";" + ElementVal;
                                    }
                                }
                            });
                            var PeopleList1 = "0";
                            for (x = 0; x < grid.Rows.length; x++) {
                                row = grid.Rows.getRow(x);
                                PeopleList1 = PeopleList1 + ";" + row.getCell(0).getValue();
                            }
                            grid = igtbl_getGridById("UWG_Peoples");
                            var PeopleList = "0";
                            for (x = 0; x < grid.Rows.length; x++) {
                                row = grid.Rows.getRow(x);
                                PeopleList = PeopleList + ";" + row.getCell(0).getValue();
                            }
                            CustomFormsWs.SendData(CurDocument, CurrStage, CurAction, Columns, CodeVal, CurrWF, PeopleList, PeopleList1, AfterSend);
                            $(this).dialog("close");
                        }
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });

        function AddControlsArray(id, type, fc, bc, Value, Title, MaxLen, top, left, RfID) {
            var Width = $("#" + id).width();
            var height = $("#" + id).height();

            cont_array[cont_array.length - 1] = new Array(31)
            cont_array[cont_array.length - 1][0] = id;
            cont_array[cont_array.length - 1][1] = type;
            cont_array[cont_array.length - 1][2] = fc;
            cont_array[cont_array.length - 1][3] = bc;
            cont_array[cont_array.length - 1][4] = top;
            cont_array[cont_array.length - 1][5] = left;
            cont_array[cont_array.length - 1][6] = Width;
            cont_array[cont_array.length - 1][7] = height;
            cont_array[cont_array.length - 1][8] = '0';
            cont_array[cont_array.length - 1][9] = '';

            cont_array[cont_array.length - 1][10] = MaxLen;
            cont_array[cont_array.length - 1][11] = Title;
            cont_array[cont_array.length - 1][12] = 'left';
            cont_array[cont_array.length - 1][13] = 'ltr';
            cont_array[cont_array.length - 1][14] = Value;
            cont_array[cont_array.length - 1][15] = '0';
            cont_array[cont_array.length - 1][16] = '0';

            cont_array[cont_array.length - 1][17] = '10';
            cont_array[cont_array.length - 1][18] = false;
            cont_array[cont_array.length - 1][19] = false;
            cont_array[cont_array.length - 1][20] = '0';
            cont_array[cont_array.length - 1][21] = '';
            cont_array[cont_array.length - 1][22] = '';

            cont_array[cont_array.length - 1][23] = '';
            cont_array[cont_array.length - 1][24] = '';
            cont_array[cont_array.length - 1][25] = 'S';
            cont_array[cont_array.length - 1][26] = 'G';
            cont_array[cont_array.length - 1][27] = '';
            cont_array[cont_array.length - 1][28] = true;
            cont_array[cont_array.length - 1][29] = id;
            cont_array[cont_array.length - 1][30] = RfID;
            cont_array.length = cont_array.length + 1;

            div = $("<div>");
            Ctrl = $(id);
            var code = 'cont_' + id;
            $("#" + code.substring(5, 12)).prepend(div);
        }
        function ApplyChanges(PropArrays) {
            zero_pos = $("#WorkSpaceView").offset();
            if (_active_control_id != "none" && (_active_control_id != "")) {
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[0] == (_active_control_id) && val[16] != '1') {
                            cont_array[i][1] = PropArrays[1];
                            cont_array[i][2] = PropArrays[2];
                            cont_array[i][3] = PropArrays[3];
                            cont_array[i][4] = PropArrays[4];
                            cont_array[i][5] = PropArrays[5];
                            cont_array[i][6] = PropArrays[6];
                            cont_array[i][7] = PropArrays[7];
                            cont_array[i][8] = PropArrays[8];
                            cont_array[i][9] = PropArrays[9];
                            cont_array[i][10] = PropArrays[10];
                            cont_array[i][11] = PropArrays[11];
                            cont_array[i][12] = PropArrays[12];
                            cont_array[i][13] = PropArrays[13];
                            cont_array[i][14] = PropArrays[14];
                            cont_array[i][15] = PropArrays[15];
                            cont_array[i][16] = PropArrays[16];
                            cont_array[i][17] = PropArrays[17];
                            cont_array[i][18] = PropArrays[18];
                            cont_array[i][19] = PropArrays[19];
                            cont_array[i][20] = PropArrays[20];
                            cont_array[i][21] = PropArrays[21];
                            cont_array[i][22] = PropArrays[22];
                            cont_array[i][23] = PropArrays[24];
                            cont_array[i][24] = PropArrays[25];
                            cont_array[i][25] = PropArrays[26];
                            cont_array[i][26] = PropArrays[27];
                            cont_array[i][27] = PropArrays[28];
                            cont_array[i][28] = PropArrays[29];
                            cont_array[i][29] = PropArrays[30];

                            if (cont_array[i][1] == 'TextBox' || cont_array[i][1] == 'Label') {
                                $('#' + _active_control_id)[0].value = cont_array[i][14];
                                $('#' + _active_control_id).css({ "color": cont_array[i][2] });
                                $('#' + _active_control_id).css({ "background-color": cont_array[i][3] });
                                $('#' + _active_control_id).css({ "font-size": cont_array[i][17] + "px" });
                            }
                            else if (cont_array[i][1] == 'TextArea') {
                                $('#' + _active_control_id)[0].value = cont_array[i][14];
                                $('#' + _active_control_id).css({ "color": cont_array[i][2] });
                                $('#' + _active_control_id).css({ "background-color": cont_array[i][3] });
                                $('#' + _active_control_id).css({ "font-size": cont_array[i][17] + "px" });
                            }
                            else if (cont_array[i][1] == 'ComboBox') {
                                $('#' + _active_control_id).css({ "color": cont_array[i][2] });
                                $('#' + _active_control_id).css({ "background-color": cont_array[i][3] });
                                $('#' + _active_control_id).css({ "font-size": cont_array[i][17] + "px" });
                            }
                            else if (cont_array[i][1] == 'Image') {
                                $('#' + _active_control_id)[0].src = cont_array[i][9];
                                $('#' + _active_control_id)[0].align = cont_array[i][12];
                            }
                            else if (cont_array[i][1] == 'Line') {
                                $('#' + _active_control_id)[0].align = cont_array[i][12];
                                $('#' + _active_control_id).css({ "color": cont_array[i][2] });
                            }
                            $('#cont_' + _active_control_id).css({ "top": parseInt(cont_array[i][4]) + zero_pos.top + "px" });
                            $('#cont_' + _active_control_id).css({ "left": parseInt(cont_array[i][5]) + zero_pos.left + "px" });

                            $('#' + _active_control_id).width(cont_array[i][6] + "px");
                            $('#' + _active_control_id).height(cont_array[i][7] + "px");
                            $("#" + _active_control_id).parent().css({ "width": cont_array[i][6] + "px", "height": cont_array[i][7] + "px" });
                            $('#' + _active_control_id)[0].Title = cont_array[i][11];
                            $('#' + _active_control_id)[0].dir = cont_array[i][13];
                            activate_control(_active_control_id);
                        }
                    }
                });
            }
        }
        function activate_control(id) {
            var x = $("#" + id).width();
            var y = $("#" + id).height();
            $("#cont_" + id).css({ "width": (x) + "px", "height": (y) + "px" });
            _active_control_id = id;
        }
        $(window).load(function () {
            var DocumentID = "0";
            var FrmID = "";
            Url = window.location.search.substring(1);
            Qstring = Url.split("&");
            for (i = 0; i < Qstring.length; i++) {
                Qs = Qstring[i].split("=");
                if (Qs[0] == "DocumentID") {
                    DocumentID = Qs[1];
                }
                else if (Qs[0] == "FrmID") {
                    FrmID = Qs[1];
                }
            }
            StageProp = document.getElementById('<%=HiddenField_WFStage.ClientID%>').value.split(";");
            if (StageProp[0] == "0") { IsWorkFlow = false; }
            else {
                IsWorkFlow = true;
                if (StageProp.length > 1) {
                    CurrStage = StageProp[1];
                    CurrTrnsID = StageProp[2];
                    CurrWF = StageProp[3];
                    CustomFormsWs.RetStepEmployees(CurrTrnsID, ShowStepEmployee);
                }
            }
            if (DocumentID != "0") {
                CustomFormsWs.LoadControls(DocumentID, BuildCntrols);
            }
            if (FrmID == "") {
                document.getElementById('<%=ImageButton_New.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Save.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Delete.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_First.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Back.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Next.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Last.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Print.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Properties.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Remarks.ClientID%>').disabled = true;
                isnew = 0;
                issave = 0;
                isupdate = 0;
                isdelete = 0;
                isfirst = 0;
                isback = 0;
                isnext = 0;
                islast = 0;
                isprint = 0;
                isproperities = 0;
                isremark = 0;
                IsReturnCode = 0;
                CheckButtons();
            }
            else {
                if (IsWorkFlow == true) {
                    try {
                        document.getElementById('<%=ImageButton_Send.ClientID%>').style.visibility = 'hidden';
                        document.getElementById('<%=ImageButton_Approve.ClientID%>').style.visibility = 'hidden';
                        document.getElementById('<%=ImageButton_Reject.ClientID%>').style.visibility = 'hidden';
                        document.getElementById('<%=ImageButton_Return.ClientID%>').style.visibility = 'hidden';
                        document.getElementById('<%=ImageButton_Printing.ClientID%>').style.visibility = 'hidden';
                        document.getElementById('<%=ImageButton_Attach.ClientID%>').style.visibility = 'hidden';
                        document.getElementById('<%=ImageButton_WFRemarks.ClientID%>').style.visibility = 'hidden';
                        document.getElementById('<%=ImageButton_Forward.ClientID%>').style.visibility = 'hidden';
                        CustomFormsWs.ReturnDocumentStageActions(DocumentID, CurrStage, CheckActions);
                    }
                    catch (err) {
                    }
                }
                else {
                    PoliciesWs.AuthorizedPagesToolBox(FrmID, CheckToolsBoxes);
                }
            }
        })
        function ShowStepEmployee(result) {
            if (result != "") {
                document.getElementById('<%=Label_StepEmployees.ClientID%>').innerHTML = "الموظف البديل : " + result;
            }
            else {
                document.getElementById('<%=Label_StepEmployees.ClientID%>').innerHTML = "";
            }
        }
        function CheckActions(result) {
           
            if (result == null) {
                jFailled("<%=Resources.Message.NotValidWF%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>", function (r) {
                    if (r == true) {
                        window.close();
                    }
                });
            }
            else {
                for (i = 0; i < result.length; i++) {
                    if (result[i] == "1") {
                        document.getElementById('<%=ImageButton_Send.ClientID%>').style.visibility = 'visible';
                    }
                    else if (result[i] == "2") {
                        document.getElementById('<%=ImageButton_Approve.ClientID%>').style.visibility = 'visible';
                    }
                    else if (result[i] == "3") {
                        document.getElementById('<%=ImageButton_Reject.ClientID%>').style.visibility = 'visible';
                    }
                    else if (result[i] == "4") {
                        document.getElementById('<%=ImageButton_Return.ClientID%>').style.visibility = 'visible';
                    }
                    else if (result[i] == "5") {
                        document.getElementById('<%=ImageButton_Printing.ClientID%>').style.visibility = 'visible';
                    }
                    else if (result[i] == "6") {
                        document.getElementById('<%=ImageButton_Attach.ClientID%>').style.visibility = 'visible';
                    }
                    else if (result[i] == "7") {
                        document.getElementById('<%=ImageButton_WFRemarks.ClientID%>').style.visibility = 'visible';
                    }
                      else if (result[i] == "8") {
                        document.getElementById('<%=ImageButton_Forward.ClientID%>').style.visibility = 'visible';
                    }
                }
            }
        }
        function SetInitInfo(result) {
            if (result != null) {
                for (i = 0; i < result.split("|").length; i++) {
                    CurrentInitInfoArr = result.split("|")[i].split(";");
                    if (CurrentInitInfoArr.length == 2) {
                        var Element = CurrentInitInfoArr[0];
                        var Val = CurrentInitInfoArr[1];
                        document.getElementById(Element).value = Val;
                    }
                }
            }
        }
        function CheckElements(result) {
            if (result != null) {
                for (i = 0; i < result.length; i++) {
                    document.getElementById(result[i]).style.visibility = 'hidden';
                }
            }
        }
        function CheckElementsEna(result) {
            if (result != null) {
                for (i = 0; i < result.length; i++) {
                    document.getElementById(result[i]).disabled = true;
                }
            }
            if (IsWorkFlow == true) {
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                var MainElement;
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[18] == "True") {
                            MainElement = val[0];
                        }
                    }
                });
                CustomFormsWs.RetCode(DocumentID, CurrStage, MainElement, CurrTrnsID, BindObjects);
            }
        }
        function BuildCntrols(result) {
            zero_pos = $("#WorkSpaceView").offset();
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays1 = CtrlArrays[i].split("*");
                PropArrays = PropArrays1[0].split(",");
                var ctrl_type;
                var ctrl_value = '';
                var ctrl_Title = '';
                var MaxLen = '';
                div = $("<div>");
                if (PropArrays[0].indexOf("menu_tb") != -1) { _nct = "menu_tb"; }
                else if (PropArrays[0].indexOf("menu_ta") != -1) { _nct = "menu_ta"; }
                else if (PropArrays[0].indexOf("menu_cb") != -1) { _nct = "menu_cb"; }
                else if (PropArrays[0].indexOf("menu_rb") != -1) { _nct = "menu_rb"; }
                else if (PropArrays[0].indexOf("menu_so") != -1) { _nct = "menu_so"; }
                else if (PropArrays[0].indexOf("menu_lb") != -1) { _nct = "menu_lb"; }
                else if (PropArrays[0].indexOf("menu_ln") != -1) { _nct = "menu_ln"; }
                else if (PropArrays[0].indexOf("menu_im") != -1) { _nct = "menu_im"; }
                else if (PropArrays[0].indexOf("menu_sc") != -1) { _nct = "menu_sc"; }
                else if (PropArrays[0].indexOf("menu_zm") != -1) { _nct = "menu_zm"; }
                else if (PropArrays[0].indexOf("menu_lk") != -1) { _nct = "menu_lk"; }

                var SrcEvnt = "";
                for (x = 0; x < CtrlArrays.length; x++) {
                    PropAys1 = CtrlArrays[x].split("*");
                    PropAys = PropAys1[0].split(",");
                    if (PropAys[1] == "Srch" && PropAys[21] == PropArrays[0]) {
                        SrcEvnt = " onblur='FillSearchAtt(" + PropAys[23] + ");'";
                    }
                }
                var Evnt = "";
                if (PropArrays[18] == "True") {
                    Evnt = " onblur='GetRetrnedCode();'";
                }
                if (PropArrays[0].indexOf("menu_tb") != -1) {
                    Evnt = " onblur='FireChangeEvent()'";
                }
                if (PropArrays[0].indexOf("menu_sc") != -1) {
                    Evnt = " onclick='viewSearch(" + PropArrays[23] + ");'";
                }
                if (PropArrays[0].indexOf("menu_zm") != -1) {
                    Evnt = " onclick='viewZoom(" + PropArrays[23] + ");'";
                }
                if (PropArrays[0].indexOf("menu_lk") != -1) {
                    Evnt = " onclick='viewLink(" + PropArrays[23] + ");'";
                }
                var DisProp = "";
                if (PropArrays[29] == "True") {
                    DisProp = disabled = "disabled";
                }
                switch (_nct) {
                    case "menu_tb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<input class="tb" type="text" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" align="left" maxlength="' + PropArrays[10] + '" tabindex="' + PropArrays[8] + '"' + DisProp + ' style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC; font-size: 10px" ' + Evnt + SrcEvnt + '>'); ctrl_type = 'TextBox'; MaxLen = '255'; break; }
                    case "menu_ta": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<textarea class="ta" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" dir="ltr" tabindex="' + PropArrays[8] + '"' + DisProp + ' style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC; font-size: 10px" ' + Evnt + SrcEvnt + '></textarea>'); ctrl_type = 'TextArea'; MaxLen = '0'; break; }
                    case "menu_cb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<input class="cb" type="checkbox"  name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" tabindex="' + PropArrays[8] + '"' + DisProp + ' style="width:16px;hieght:16px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;" ' + Evnt + SrcEvnt + '>'); ctrl_type = 'CheckBox'; MaxLen = '0'; break; }
                    case "menu_rb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<input class="rb"  type="radio" name="' + PropArrays[22] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" tabindex="' + PropArrays[8] + '"' + DisProp + ' style="width:16px;hieght:16px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;" ' + Evnt + SrcEvnt + '>'); ctrl_type = 'Radio'; MaxLen = '0'; break; }
                    case "menu_so": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<select class="ddl" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" tabindex="' + PropArrays[8] + '"' + DisProp + ' style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC; font-size: 10px" ' + Evnt + SrcEvnt + '>'); ctrl_type = 'ComboBox'; MaxLen = '0'; break; }
                    case "menu_lb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<div class="lbl" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="' + PropArrays[13] + '" align="' + PropArrays[12] + '" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;" ' + Evnt + '>' + PropArrays[14] + '</div>'); ctrl_type = 'Label'; MaxLen = '0'; break; }
                    case "menu_ln": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<div class="ln" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;" ' + Evnt + '></div>'); ctrl_type = 'Line'; MaxLen = '0'; break; }
                    case "menu_im": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="im" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;" ' + Evnt + '>'); ctrl_type = 'Image'; MaxLen = '0'; break; }
                    case "menu_sc": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="sc" src="../../Common/Images/filefind.png" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;" ' + Evnt + '>'); ctrl_type = 'Srch'; MaxLen = '0'; break; }
                    case "menu_zm": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="zm" src="../../Common/Images/fileZoom.png" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;"' + Evnt + '>'); ctrl_type = 'Zoom'; MaxLen = '0'; break; }
                    case "menu_lk": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="lk" src="../../Common/Images/fileLink.png" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;"' + Evnt + '>'); ctrl_type = 'Link'; MaxLen = '0'; break; }
                }
                div.attr("id", 'cont_' + PropArrays[0]);
                div.css({ "position": "absolute", "text-align": "left", "left": PropArrays[5] + zero_pos.left + "px", "top": PropArrays[4] + zero_pos.top + "px" });
                $("#WorkSpaceView").prepend(div);
                AddControlsArray(PropArrays[0], ctrl_type, '#000000', '#FFFFFF', ctrl_value, ctrl_Title, MaxLen, PropArrays[4], PropArrays[5], PropArrays[23]);
                activate_control(PropArrays[0]);
                cont_counter = cont_counter + 1
                ApplyChanges(PropArrays);
                if (PropArrays[0].indexOf("menu_tb") != -1) {
                    var ctrl = document.getElementById(PropArrays[0]);
                    if (PropArrays[26] == "N0") {
                        oStringMask = new Mask("####", "number");
                        oStringMask.attach(ctrl);
                    }
                    else if (PropArrays[26] == "N1") {
                        oStringMask = new Mask("####.0", "number");
                        oStringMask.attach(ctrl);
                    }
                    else if (PropArrays[26] == "N2") {
                        oStringMask = new Mask("####.00", "number");
                        oStringMask.attach(ctrl);
                    }
                    else if (PropArrays[26] == "N3") {
                        oStringMask = new Mask("####.000", "number");
                        oStringMask.attach(ctrl);
                    }
                    else if (PropArrays[26] == "N4") {
                        oStringMask = new Mask("####.0000", "number");
                        oStringMask.attach(ctrl);
                    }
                    else if (PropArrays[26] == "D") {
                        oStringMask = new Mask("##/##/####");
                        oStringMask.attach(ctrl);
                        var cal = PropArrays[27];
                        if (cal == "G") {
                            $(ctrl).calendarsPicker();
                        }
                        else {
                            $(ctrl).calendarsPicker({ calendar: $.calendars.instance('islamic') });
                        }
                    }
                }
                if (PropArrays[0].indexOf("menu_so") != -1) {
                    var select = document.getElementById(PropArrays[0]);
                    select.options.length = 0;
                    SelectArrays = PropArrays1[1].split("$");
                    for (s = 0; s < SelectArrays.length; s++) {
                        SelectArrayss = SelectArrays[s].split(";");
                        valMember = SelectArrayss[0];
                        DisMember = document.getElementById('<%=HiddenField_Lang.ClientID%>').value == "Ar" ? SelectArrayss[1] : SelectArrayss[2];
                        select.options.add(new Option(DisMember, valMember));
                    }
                }
                if (IsWorkFlow == true && PropArrays[18] == "True") {
                    document.getElementById(PropArrays[0]).disabled = true;
                }
            }
            if (IsWorkFlow == true) {
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                if (DocumentID != "0") {
                    CustomFormsWs.ReturnDocumentStageElements(DocumentID, CurrStage, CheckElements);
                    CustomFormsWs.ReturnDocumentStageElementsEna(DocumentID, CurrStage, CheckElementsEna);
                }
            }
        }

        //Set Page Functionality
        var SecSave = 1;
        var SecUpdate = 1;
        var SecDelete = 1;
        var SecPrint = 1;

        var isnew = 1;
        var issave = 1;
        var isupdate = 1;
        var isdelete = 1;
        var isfirst = 1;
        var isback = 1;
        var isnext = 1;
        var islast = 1;
        var isprint = 1;
        var isproperities = 1;
        var isremark = 1;
        var IsReturnCode = 1;

        function CheckButtons() {
            if (isnew == 0) { document.getElementById('<%=ImageButton_New.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/G13.png"; }
            else { document.getElementById('<%=ImageButton_New.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/13.png"; }

            if (issave == 0 && isupdate == 0) { document.getElementById('<%=ImageButton_Save.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/G86.png"; }
            else { document.getElementById('<%=ImageButton_Save.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/86.png"; }

            if (isdelete == 0) { document.getElementById('<%=ImageButton_Delete.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/G33.png"; }
            else { document.getElementById('<%=ImageButton_Delete.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/33.png"; }

            if (isfirst == 0) { document.getElementById('<%=ImageButton_First.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/GFirst.png"; }
            else { document.getElementById('<%=ImageButton_First.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/First.png"; }

            if (isback == 0) { document.getElementById('<%=ImageButton_Back.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/GBack.png"; }
            else { document.getElementById('<%=ImageButton_Back.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/Back.png"; }

            if (isnext == 0) { document.getElementById('<%=ImageButton_Next.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/GForward.png"; }
            else { document.getElementById('<%=ImageButton_Next.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/Forward.png"; }

            if (islast == 0) { document.getElementById('<%=ImageButton_Last.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/GLast.png"; }
            else { document.getElementById('<%=ImageButton_Last.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/Last.png"; }

            if (isprint == 0) { document.getElementById('<%=ImageButton_Print.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/G16.png"; }
            else { document.getElementById('<%=ImageButton_Print.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/16.png"; }

            if (isproperities == 0) { document.getElementById('<%=ImageButton_Properties.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/GEquipment.png"; }
            else { document.getElementById('<%=ImageButton_Properties.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/Equipment.png"; }

            if (isremark == 0) { document.getElementById('<%=ImageButton_Remarks.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/GNotes.png"; }
            else { document.getElementById('<%=ImageButton_Remarks.ClientID%>').src = "../../Common/Images/DocumentWF/CustomFormsView/Notes.png"; }
        }

        function NewMode() {
            isnew = 0;
            if (SecSave == 1) issave = 1;
            else issave = 0;
            isupdate = 0;
            isdelete = 0;
            isfirst = 1;
            isback = 0;
            isnext = 0;
            islast = 0;
            isprint = 0;
            isproperities = 0;
            isremark = 0;

            if (IsWorkFlow == false) {
                document.getElementById('<%=ImageButton_New.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Save.ClientID%>').disabled = issave == 1 ? false : true;
                document.getElementById('<%=ImageButton_Delete.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_First.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Back.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Next.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Last.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Print.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Properties.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Remarks.ClientID%>').disabled = true;
                CheckButtons();
            }
        }
        function NormalMode() {
            isnew = 1;
            issave = 0;
            if (SecUpdate == 1) isupdate = 1;
            else isupdate = 0;
            if (SecDelete == 1) isdelete = 1;
            else isdelete = 0;
            if (SecPrint == 1) isprint = 1;
            else isprint = 0;
            isfirst = 1;
            isback = 1;
            isnext = 1;
            islast = 1;
            isproperities = 1;
            isremark = 1;
            if (document.getElementById('<%=HiddenField_WFStage.ClientID%>').value == "0") {
                document.getElementById('<%=ImageButton_New.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Save.ClientID%>').disabled = isupdate == 1 ? false : true;
                document.getElementById('<%=ImageButton_Delete.ClientID%>').disabled = isdelete == 1 ? false : true;
                document.getElementById('<%=ImageButton_First.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Back.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Next.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Last.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Print.ClientID%>').disabled = isprint == 1 ? false : true;
                document.getElementById('<%=ImageButton_Properties.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Remarks.ClientID%>').disabled = false;
                CheckButtons();
            }
        }
        function CancelMode() {
            isnew = 1;
            issave = 0;
            isupdate = 0;
            isdelete = 0;
            isfirst = 1;
            isback = 1;
            isnext = 1;
            islast = 1;
            isprint = 0;
            isproperities = 1;
            isremark = 0;

            if (document.getElementById('<%=HiddenField_WFStage.ClientID%>').value == "0") {
                document.getElementById('<%=ImageButton_New.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Save.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Delete.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_First.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Back.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Next.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Last.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Print.ClientID%>').disabled = true;
                document.getElementById('<%=ImageButton_Properties.ClientID%>').disabled = false;
                document.getElementById('<%=ImageButton_Remarks.ClientID%>').disabled = true;
                CheckButtons();
            }
        }
        function CheckToolsBoxes(result) {
            if (result != null) {
                SecSave = result[0];
                SecUpdate = result[1];
                SecDelete = result[2];
                SecPrint = result[3];
                NewCommand();
            }
        }
        function NewCommand() {
            if (isnew == 1) {
                NewMode();
                ClearControls();
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[18] == "True") {
                            document.getElementById(val[0]).focus();
                        }
                    }
                });
            }
        }
        function ClearControls() {
            document.getElementById('<%=HiddenField_ID.ClientID%>').value = "";
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[1] == "TextBox" || val[1] == "TextArea") {
                        if (val[14].startsWith('=Eval[')) {
                            document.getElementById(val[0]).value = 0;
                        }
                        else if (val[14].startsWith('=fn[')) {
                            document.getElementById(val[0]).value = 0;
                        }
                        else {
                            document.getElementById(val[0]).value = val[14];
                        }
                    }
                    else if (val[1] == "Radio" || val[1] == "CheckBox") {
                        document.getElementById(val[0]).checked = false;
                    }
                    else if (val[1] == "ComboBox") {
                        document.getElementById(val[0]).value = '0;';
                    }
                }
            });
        }
        function SaveCommand() {
            if ((issave == 1 && isnew == 0) || (isupdate == 1 && isnew == 1)) {
                if (ValidateControls() != "0") return;
                var ID = 0;
                if (document.getElementById('<%=HiddenField_ID.ClientID%>').value != "") {
                    ID = document.getElementById('<%=HiddenField_ID.ClientID%>').value
                }

                var DocumentID = "0";
                var Columns = "";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                if (DocumentID != "0") {
                    jQuery.each(cont_array, function (i, val) {
                        if (i < cont_array.length - 1) {
                            if (val[16] != '1') {
                                var ElementVal = $('#' + cont_array[i][0])[0].value;
                                if (val[1] == "Radio" || val[1] == "CheckBox") {
                                    ElementVal = $('#' + cont_array[i][0])[0].checked;
                                }
                                Columns = Columns + "|" + cont_array[i][0] + ";" + cont_array[i][1] + ";" + ElementVal;
                            }
                        }
                    });
                    if (ID == 0) {
                        CustomFormsWs.SaveData(DocumentID, Columns, AfterSave);
                    }
                    else {
                        CustomFormsWs.UpdateData(DocumentID, Columns, ID, AfterSave);
                    }
                }
            }
        }
        function AfterSave(result) {
            if (result > 0) {
                NormalMode();
                if (document.getElementById('<%=HiddenField_ID.ClientID%>').value == "") {
                    document.getElementById('<%=HiddenField_ID.ClientID%>').value = result;
                }
                jSuccess("<%=Resources.Message.Done%>", "<%=Resources.MessageSetting.SuccessMessage%>", "<%=Resources.MessageSetting.Close%>");
            }
            else {
                jFailled("<%=Resources.Message.Failed%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
            }
        }
        function DeleteCommand() {
            if (isdelete == 1) {
                jQuestion("<%=Resources.Message.IsDelete%>", "<%=Resources.MessageSetting.QuestionMessage%>", "<%=Resources.MessageSetting.Yes%>", "<%=Resources.MessageSetting.No%>", function (r) {
                    if (r == true) {
                        var ID = 0;
                        if (document.getElementById('<%=HiddenField_ID.ClientID%>').value != "") {
                            ID = document.getElementById('<%=HiddenField_ID.ClientID%>').value
                        }
                        var DocumentID = "0";
                        Url = window.location.search.substring(1);
                        Qstring = Url.split("&");
                        for (i = 0; i < Qstring.length; i++) {
                            Qs = Qstring[i].split("=");
                            if (Qs[0] == "DocumentID") {
                                DocumentID = Qs[1];
                            }
                        }
                        if (DocumentID != "0") {
                            CustomFormsWs.DeleteData(DocumentID, ID, AfterDelete);
                        }
                    }
                });
            }
        }
        function AfterDelete(result) {
            if (result > 0) {
                NewCommand();
                jSuccess("<%=Resources.Message.Done%>", "<%=Resources.MessageSetting.SuccessMessage%>", "<%=Resources.MessageSetting.Close%>");
            }
            else {
                jFailled("<%=Resources.Message.Failed%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
            }
        }
        function FirstCommand() {
            if (isfirst == 1) {
                var ID = 0;
                if (document.getElementById('<%=HiddenField_ID.ClientID%>').value != "") {
                    ID = document.getElementById('<%=HiddenField_ID.ClientID%>').value
                }
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                if (DocumentID != "0") {
                    CustomFormsWs.SelectFirst(DocumentID, CurrStage, BindObjects);
                }
            }
        }
        function BackCommand() {
            if (isback == 1) {
                var ID = 0;
                if (document.getElementById('<%=HiddenField_ID.ClientID%>').value != "") {
                    ID = document.getElementById('<%=HiddenField_ID.ClientID%>').value
                }
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                if (DocumentID != "0") {
                    CustomFormsWs.SelectBack(DocumentID, CurrStage, ID, BindObjects);
                }
            }
        }
        function NextCommand() {
            if (isnext == 1) {
                var ID = 0;
                if (document.getElementById('<%=HiddenField_ID.ClientID%>').value != "") {
                    ID = document.getElementById('<%=HiddenField_ID.ClientID%>').value
                }
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                if (DocumentID != "0") {
                    CustomFormsWs.SelectNext(DocumentID, CurrStage, ID, BindObjects);
                }
            }
        }
        function LastCommand() {
            if (islast == 1) {
                var ID = 0;
                if (document.getElementById('<%=HiddenField_ID.ClientID%>').value != "") {
                    ID = document.getElementById('<%=HiddenField_ID.ClientID%>').value
                }
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                if (DocumentID != "0") {
                    CustomFormsWs.SelectLast(DocumentID, CurrStage, BindObjects);
                }
            }
        }
        function FillSearchAtt(ElemID) {
            var MainVal;
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[30] == ElemID) {
                        MainVal = document.getElementById(val[21]).value;
                    }
                }
            });
            CustomFormsWs.FillSearchAtt(ElemID, MainVal, AfterFillSearchAtt);
        }
        function AfterFillSearchAtt(result) {
            if (result != null) {
                var Ary1 = result.split("|");
                for (i = 0; i < Ary1.length; i++) {

                    Elname = Ary1[i].split(",")[0];
                    Eltype = Ary1[i].split(",")[1];
                    Elvalue = Ary1[i].split(",")[2];

                    if (Eltype == "System.Boolean") {
                        $('#' + Elname)[0].checked = Elvalue == "True" ? true : false;
                    }
                    else {
                        $('#' + Elname)[0].value = Elvalue;
                    }
                }
            }
        }
        function GetRetrnedCode() {
            if (IsReturnCode == 1 && IsWorkFlow == false) {
                var MainVal;
                var MainElement;
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[18] == "True") {
                            MainVal = document.getElementById(val[0]).value;
                            MainElement = val[0];
                        }
                    }
                });
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                if (DocumentID != "0") {
                    CustomFormsWs.RetCode(DocumentID, CurrStage, MainElement, MainVal, BindObjects);
                }
            }
        }
        function FireChangeEvent() {
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[1] == "TextBox" && val[14] != "") {
                        if (val[14].startsWith('=Eval[')) {
                            var CurrFormula = val[14].replace("=Eval[", "").replace("]", "");
                            CurrFormula = CurrFormula.replace("(", "").replace(")", "");
                            var myArray = CurrFormula.split(/[X~+-]+/g);
                            for (var i = 0; i < myArray.length; i++) {
                                var ItemVal = "0";
                                try {
                                    ItemVal = document.getElementById(myArray[i]).value;
                                    if (ItemVal == "" || ItemVal == null || ItemVal == "undefined") {
                                        ItemVal = 0;
                                    }
                                    CurrFormula = CurrFormula.replace(myArray[i], ItemVal);
                                }
                                catch (err) {
                                }
                            }
                            document.getElementById(val[0]).value = eval(CurrFormula.replace("X", "*").replace("~", "/"));
                        }
                        if (val[14].startsWith('=fn[')) {
                            var CurrFormula = val[14].replace("=fn[", "").replace("]", "");
                            var FstFnBody = CurrFormula.substr(0, CurrFormula.indexOf('(')) + "(";
                            CurrFormula = CurrFormula.replace(FstFnBody, "").replace("(", "").replace(")", "");
                            var Ary1 = CurrFormula.split(";");
                            for (i = 0; i < Ary1.length; i++) {
                                if (i > 0) {
                                    FstFnBody = FstFnBody + ",";
                                }
                                FstFnBody = FstFnBody + "'" + document.getElementById(Ary1[i]).value + "'";
                            }
                            FstFnBody = FstFnBody + ")";
                            document.getElementById(val[0]).value = 0;
                            CustomFormsWs.ExecFunction(FstFnBody, val[0], RetctrlValues);
                        }
                    }
                }
            });
        }
        function RetctrlValues(result) {
            if (result != null && result != "") {
                var Ary1 = result.split("|");
                document.getElementById(Ary1[0]).value = Ary1[1];
            }
        }
        function BindObjects(result) {
            if (result != null) {
                if (result[1] == 1) {
                    document.getElementById('<%=HiddenField_ID.ClientID%>').value = result[2].split(";")[0];
                    for (i = 3; i < result.length; i++) {
                        if (result[i] != null) {
                            if (result[i].split(";").length == 3) {
                                Elvalue = result[i].split(";")[0];
                                Eltype = result[i].split(";")[1];
                                Elname = result[i].split(";")[2];

                                if (Eltype == "System.Boolean") {
                                    $('#' + Elname)[0].checked = Elvalue == "True" ? true : false;
                                }
                                else {
                                    if (Elname != "Stage") {
                                        $('#' + Elname)[0].value = Elvalue;
                                    }
                                }
                            }
                        }
                    }
                    NormalMode();
                    ValidateControls();
                    if (result[6].split(";")[0] != "") {
                        CancelMode();
                        document.getElementById('<%=Label_CancelDate.ClientID%>').innerHTML = result[6].split(";")[0];
                    }
                    else {
                        document.getElementById('Label_CancelDate').innerHTML = "";
                    }
                }
                else if (result[1] == 0 && result[0] == 2) {
                    jWarning("<%=Resources.Message.NoBack%>", "<%=Resources.MessageSetting.WarnMessage%>", "<%=Resources.MessageSetting.Close%>");
                }
                else if (result[1] == 0 && result[0] == 3) {
                    jWarning("<%=Resources.Message.NoNext%>", "<%=Resources.MessageSetting.WarnMessage%>", "<%=Resources.MessageSetting.Close%>");
                }
                else if (result[1] == 0 && result[0] == 5) {
                    var MainVal;
                    var MainElement;
                    jQuery.each(cont_array, function (i, val) {
                        if (i < cont_array.length - 1) {
                            if (val[18] == "True") {
                                MainVal = document.getElementById(val[0]).value;
                                MainElement = val[0];
                            }
                        }
                    });
                    NewCommand();
                    if (MainVal == "") {
                        var now = new Date();
                        MainVal = now.getTime();
                    }
                    document.getElementById(MainElement).value = MainVal;
                }
            }
            if (CurrStage == "") {
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                CustomFormsWs.ReturnDocumentInitInfo(DocumentID, SetInitInfo);
            }
        }
        function PrintCommand() {
            if (isprint == 1) {
                var DocumentID = "0";
                Url = window.location.search.substring(1);
                Qstring = Url.split("&");
                for (i = 0; i < Qstring.length; i++) {
                    Qs = Qstring[i].split("=");
                    if (Qs[0] == "DocumentID") {
                        DocumentID = Qs[1];
                    }
                }
                CustomFormsWs.PrintData(DocumentID, document.getElementById('<%=HiddenField_ID.ClientID%>').value, AfterPrint);
            }
        }
        function AfterPrint(result) {
            if (result != "") {
                var hight = window.screen.availHeight - 35;
                var width = window.screen.availWidth - 10;
                var win = window.open(result, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
                win.focus();
            }
        }
        function viewSearch(CtrlID) {
            CustomFormsWs.SearchForm(CtrlID, document.getElementById('<%=HiddenField_Lang.ClientID%>').value, AfterSearch);
        }
        function AfterSearch(result) {
            if (result != "") {
                var hight = 525;
                var width = 725;
                var win = window.open(result, "_NEW", "height=" + hight + ",width=" + width + ",resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=0");
                win.focus();
            }
        }
        function viewZoom(CtrlID) {
            CustomFormsWs.ZoomForm(CtrlID, document.getElementById('<%=HiddenField_Lang.ClientID%>').value, AfterZoom);
        }
        function AfterZoom(result) {
            if (result != "") {
                var hight = result.split("|")[1];
                var width = result.split("|")[2]; ;
                var win = window.open(result.split("|")[0], "_NEW", "height=" + hight + ",width=" + width + "center=0");
                win.focus();
            }
        }
        function viewLink(CtrlID) {
            if (document.getElementById("HiddenField1").value == 1) {
                CustomFormsWs.LinkForm(CtrlID, document.getElementById('<%=HiddenField_Lang.ClientID%>').value, AfterLink);
            }
        }
        function AfterLink(result) {
            if (result != "") {
                var CURL = "";
                CURL = "../HR/" + result.split("|")[0] + "?";
                Qstring = result.split("|");
                var HasQS = 0
                for (i = 1; i < Qstring.length; i++) {
                    if (HasQS == 1) {
                        var ElementVal = Qstring[i]
                        try {
                            ElementVal = document.getElementById(Qstring[i]).value;
                        }
                        catch (err) {
                        }
                        CURL = CURL + ElementVal + "&";
                        HasQS = 0
                    }
                    else {
                        CURL = CURL + Qstring[i] + "=";
                        HasQS = HasQS + 1;
                    }
                }
                OpenModal1(CURL, 400, 800);
            }
        }
        function PropertiesCommand() {
            if (isproperities == 1) {
                return false;
            }
        }
        function RemarksCommand() {
            if (isremark == 1) {
                return false;
            }
        }
        var IsValid = 0;
        function ValidateControls() {
            IsValid = 0;
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    document.getElementById(val[0]).style.border = "1px solid #CCCCCC";
                }
            });
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[1] == "TextBox" && val[24] != "") {
                        if (val[24].startsWith('=Vali[')) {
                            var CurrFormula = val[14].replace("=Eval[", "").replace("]", "");
                            CurrFormula = CurrFormula.replace("(", "").replace(")", "");
                            var myArray = CurrFormula.split(/[X~+-]+/g);
                            for (var i = 0; i < myArray.length; i++) {
                                var ItemVal = "0";
                                try {
                                    ItemVal = document.getElementById(myArray[i]).value;
                                    if (ItemVal == "" || ItemVal == null || ItemVal == "undefined") {
                                        ItemVal = 0;
                                    }
                                    CurrFormula = CurrFormula.replace(myArray[i], ItemVal);
                                }
                                catch (err) {
                                }
                            }
                            try {
                                if (!eval(CurrFormula.replace("X", "*").replace("~", "/"))) {
                                    document.getElementById(val[0]).style.border = "1px solid red";
                                    IsValid = 1;
                                }
                            }
                            catch (err) {
                            }
                        }
                    }
                    if (val[19] == "True") {
                        if (!document.getElementById(val[0]).disabled) {
                            if (val[1] == "CheckBox") {
                                if (document.getElementById(val[0]).checked == false) {
                                    jFailled(val[14], "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
                                    IsValid = 1;
                                }
                            }
                            else {
                                if (document.getElementById(val[0]).value == "") {
                                    document.getElementById(val[0]).style.border = "1px solid red";
                                    IsValid = 1;
                                }
                            }
                        }
                    }
                }
            });
            return IsValid;
        }
        //Work Flow Commands
        function SendCommand(Action) {

            debugger;
            console.log(cont_array);
            debugger;
            if (CurrTrnsID == '') {
                var now = new Date();
                CodeVal = now.getTime();
                var CodeElement = '';
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[18] == "True") {
                            CodeVal = document.getElementById(val[0]).value;
                            CodeElement = val[0];
                        }
                    }
                });
                if (ValidateControls() != "0") {
                    setTimeout($.unblockUI, 2000);
                    return;
                }
            }
            else {
                if (ValidateControls() != "0") {
                    return;
                }
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[18] == "True") {
                            CodeVal = document.getElementById(val[0]).value;
                        }
                    }
                });
            }
            var DocumentID = "0";
            var Columns = "";
            Url = window.location.search.substring(1);
            Qstring = Url.split("&");
            for (i = 0; i < Qstring.length; i++) {
                Qs = Qstring[i].split("=");
                if (Qs[0] == "DocumentID") {
                    DocumentID = Qs[1];
                }
            }
            var ForwordTo
            if (DocumentID != "0") {
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[16] != '1') {
                            var ElementVal = $('#' + cont_array[i][0])[0].value;
                            if (val[1] == "Radio" || val[1] == "CheckBox") {
                                ElementVal = $('#' + cont_array[i][0])[0].checked;
                            }
                            Columns = Columns + "|" + cont_array[i][0] + ";" + cont_array[i][1] + ";" + ElementVal;
                        }

                        if (val[0] === 'menu_tb13') {
                             ForwordTo = $('#' + cont_array[i][0])[0].value
                          
                        }
                    }
                    
                   
                });
           
                
                console.log(ForwordTo);
                CustomFormsWs.CheckStage(DocumentID, CurrStage, Action, CodeVal, ForwordTo, AfterCheck);
            }
        }
        function AfterCheck(result) {
            
            CurAction = '';
            CurDocument = '';
            CurLen = '';
            CurLen1 = '';
            if (result != "") {
                if (result.split(";")[0] == "0") {
                    console.log(result);
                    var PeopleList = "0";
                    CustomFormsWs.CheckStageNotify(result.split(";")[1], CurrStage, result.split(";")[2], PeopleList, AfterCheckNotify);
                }
                else {
                    if (result.split(";")[3] != "") {
                        var Peopl = result.split(";")[3];
                        grid = igtbl_getGridById("UWG_Peoples");
                        var Len = grid.Rows.length;
                        for (x = 0; x < Len; x++) {
                            grid.Rows.remove(Len - x - 1);
                        }
                        for (x = 0; x < Peopl.split("|").length; x++) {
                            if (Peopl.split("|")[x] != "") {
                                row = grid.Rows.addNew();
                                row.getCell(0).setValue(Peopl.split("|")[x]);
                            }
                        }
                    }
                    
                    CurAction = result.split(";")[2];
                    CurDocument = result.split(";")[1];
                    CurLen = result.split(";")[0];
                    $("#Peoplesdialog-form").dialog("open");
                }
            }
        }
        function AfterCheckNotify(result) {
            console.log(result);
        
            CurAction = '';
            CurDocument = '';
            CurLen = '';
            CurLen1 = '';
            if (result != "") {
                if (result.split(";")[0] == "0") {
                    var Columns = "";
                    jQuery.each(cont_array, function (i, val) {
                        if (i < cont_array.length - 1) {
                            if (val[16] != '1') {
                                var ElementVal = $('#' + cont_array[i][0])[0].value;
                                if (val[1] == "Radio" || val[1] == "CheckBox") {
                                    ElementVal = $('#' + cont_array[i][0])[0].checked;
                                }
                                Columns = Columns + "|" + cont_array[i][0] + ";" + cont_array[i][1] + ";" + ElementVal;
                            }
                        }
                    });
                    var PeopleNoList = "0";
                    console.log(result);
                    CustomFormsWs.SendData(result.split("|")[0].split(";")[1], CurrStage, result.split("|")[0].split(";")[2], Columns, CodeVal, CurrWF, result.split("|")[1], PeopleNoList, AfterSend);
                }
                else {
                    grid = igtbl_getGridById("UWG_PeoplrNotify");
                    var Len = grid.Rows.length;
                    for (x = 0; x < Len; x++) {
                        grid.Rows.remove(Len - x - 1);
                    }
                    CurAction = result.split("|")[0].split(";")[2];
                    CurDocument = result.split("|")[0].split(";")[1];
                    CurLen1 = result.split("|")[0].split(";")[0];
                    $("#PeoplesNotifydialog-form").dialog("open");
                }
            }
        }
        function SaveStagePeople() {
            grid = igtbl_getGridById("UWG_Peoples");
            if (grid.Rows.length < CurLen) {
                row = grid.Rows.addNew();
                row.getCell(0).setValue(document.getElementById('<%=DropDownList_Employee.ClientID%>').value);
            }
        }
        function SaveStagePeople1() {
            grid = igtbl_getGridById("UWG_PeoplrNotify");
            if (grid.Rows.length < CurLen1) {
                row = grid.Rows.addNew();
                row.getCell(0).setValue(document.getElementById('<%=DropDownList_PeopleNotify.ClientID%>').value);
            }
        }
        function AfterSend(result) {
            if (result != null) {
                var Msg = "";
                if (result.split("|")[1] == 1) {
                    Msg = "<%=Resources.Message.SendDone%>" + result.split("|")[0];
                }
                else if (result.split("|")[1] == 2) {
                    Msg = "<%=Resources.Message.ApproveDone%>";
                }
                else if (result.split("|")[1] == 3) {
                    Msg = "<%=Resources.Message.RejectDone%>";
                }
                else if (result.split("|")[1] == 4) {
                    Msg = "<%=Resources.Message.ReturnDone%>";
                }
                
                jSuccess(Msg, "<%=Resources.MessageSetting.SuccessMessage%>", "<%=Resources.MessageSetting.Close%>", function (r) {
                    if (r == true) {
                        parent.updatemyPage();
                        return false;
                    }
                });
            }
            else {
                if (CurrTrnsID == '') {
                    jQuery.each(cont_array, function (i, val) {
                        if (i < cont_array.length - 1) {
                            if (val[18] == "True") {
                                document.getElementById(val[0]).value = "";
                            }
                        }
                    });
                }
                jFailled("<%=Resources.Message.Failed%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
            }
        }
        function WfPrintCommand() {
            //window.print();
        }
        function WfattachCommand() {
            var CodeVal = '';
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[18] == "True") {
                        CodeVal = document.getElementById(val[0]).value;
                    }
                }
            });
            var ctrId = CodeVal;
            if (ctrId != "" && ctrId != null && ctrId != "0")
                OpenModal1("../../Pages/HR/frmAttachDocuments.aspx?OId=" + 417 + "&RId=" + ctrId, 300, 600);
        }
        function WfRemarksCommand() {
            var CodeVal = '';
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[18] == "True") {
                        CodeVal = document.getElementById(val[0]).value;
                    }
                }
            });
            var ctrId = CodeVal;
            if (ctrId != "" && ctrId != null && ctrId != "0")
                OpenModal1("../../Pages/HR/frmRemarks.aspx?Src=1&OId=" + 417 + "&RId=" + ctrId, 250, 510);
        }

    </script>
    <div id="Message" style="display: none; text-align: center">
    </div>
    <div id="Peoplesdialog-form" title="Document Stage Peolpes">
        <table id="Table2" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 100%; height: 80px; text-align: left; vertical-align: top">
                    <table align="left" style="width: 490px; height: 80px">
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label7" runat="server" Text="Employee" SkinID="Label_DefaultBold"></asp:Label>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_Employee" runat="server" SkinID="DropDownList_LargBold">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 490px; height: 20px; vertical-align: middle; text-align: center"
                                colspan="2">
                                <asp:Button ID="Button_SaveStagePeople" runat="server" Text="Save Stage Peoples"
                                    OnClientClick="SaveStagePeople(); return false;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 100%; text-align: left; vertical-align: top">
                    <igtbl:UltraWebGrid   ID="UWG_Peoples" runat="server" CaptionAlign="Top" Width="490px"
                        Height="200px">
                        <DisplayLayout BorderCollapseDefault="Separate" Name="UltraWebGrid1" RowHeightDefault="20px"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" AllowColSizingDefault="Free"
                            AllowRowNumberingDefault="Continuous" AutoGenerateColumns="False" CellClickActionDefault="RowSelect"
                            AllowAddNewDefault="Yes" AllowDeleteDefault="Yes" AllowSortingDefault="OnClient">
                            <FrameStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt"
                                Width="490px" Height="200px">
                            </FrameStyle>
                            <RowAlternateStyleDefault BackColor="#FFFFC0">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="255, 255, 192" ColorTop="255, 255, 192" />
                            </RowAlternateStyleDefault>
                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                            </EditCellStyleDefault>
                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </FooterStyleDefault>
                            <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </HeaderStyleDefault>
                            <RowStyleDefault BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="8pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="White" ColorTop="White" />
                            </RowStyleDefault>
                            <AddNewBox>
                                <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </BoxStyle>
                            </AddNewBox>
                            <ActivationObject BorderColor="Black" BorderWidth="">
                            </ActivationObject>
                            <FilterOptionsDefault>
                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Width="200px">
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
                            <igtbl:UltraGridBand>
                                <Columns>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="EmployeeID" Type="DropDownList"
                                        Width="350px">
                                        <Header Caption="Employee">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="350px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="350px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="350px"
                                            Wrap="True">
                                        </SelectedCellStyle>
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
    </div>
    <div id="PeoplesNotifydialog-form" title="Document Stage Peoples Notifications">
        <table id="Table1" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 100%; height: 80px; text-align: left; vertical-align: top">
                    <table align="left" style="width: 490px; height: 80px">
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label1" runat="server" Text="Employee" SkinID="Label_DefaultBold"></asp:Label>
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_PeopleNotify" runat="server" SkinID="DropDownList_LargBold">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 490px; height: 20px; vertical-align: middle; text-align: center"
                                colspan="2">
                                <asp:Button ID="Button1" runat="server" Text="Save Stage Peoples Notification" OnClientClick="SaveStagePeople1(); return false;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 100%; text-align: left; vertical-align: top">
                    <igtbl:UltraWebGrid   ID="UWG_PeoplrNotify" runat="server" CaptionAlign="Top" Width="490px"
                        Height="200px">
                        <DisplayLayout BorderCollapseDefault="Separate" Name="UltraWebGrid1" RowHeightDefault="20px"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" AllowColSizingDefault="Free"
                            AllowRowNumberingDefault="Continuous" AutoGenerateColumns="False" CellClickActionDefault="RowSelect"
                            AllowAddNewDefault="Yes" AllowDeleteDefault="Yes" AllowSortingDefault="OnClient">
                            <FrameStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt"
                                Width="490px" Height="200px">
                            </FrameStyle>
                            <RowAlternateStyleDefault BackColor="#FFFFC0">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="255, 255, 192" ColorTop="255, 255, 192" />
                            </RowAlternateStyleDefault>
                            <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                            </EditCellStyleDefault>
                            <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </FooterStyleDefault>
                            <HeaderStyleDefault BackColor="LightGray" BorderStyle="Solid">
                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                            </HeaderStyleDefault>
                            <RowStyleDefault BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana"
                                Font-Size="8pt">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="White" ColorTop="White" />
                            </RowStyleDefault>
                            <AddNewBox>
                                <BoxStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                    <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" WidthTop="1px" />
                                </BoxStyle>
                            </AddNewBox>
                            <ActivationObject BorderColor="Black" BorderWidth="">
                            </ActivationObject>
                            <FilterOptionsDefault>
                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    CustomRules="overflow:auto;" Font-Names="Verdana,Arial,Helvetica,sans-serif"
                                    Font-Size="11px" Width="200px">
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
                            <igtbl:UltraGridBand>
                                <Columns>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="EmployeeID" Type="DropDownList"
                                        Width="350px">
                                        <Header Caption="Employee">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="350px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="350px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="350px"
                                            Wrap="True">
                                        </SelectedCellStyle>
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
    </div>
    <table align="center" style="border: thin solid #CCCCCC;">
        <tr>
            <td style="height: 30px">
                <table id="TBHEADER" runat="server" style="display: block; width: 100%; height: 30px;
                    text-align: left; vertical-align: top">
                    <tr>
                        <td style="display: none">
                            <asp:ImageButton ID="ImageButton_Enter" runat="server" Width="0px" Height="0px" ImageAlign="Middle"
                                CausesValidation="False" OnClientClick="return false" />
                        </td>
                        <td style="width: 5%; vertical-align: top; text-align: center">
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Close" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Door.png"
                                align="middle" ToolTip="Close" OnClientClick="parent.updatemyPage(); return false;"
                                meta:resourcekey="ImageButton_CloseResource1" />
                        </td>
                        <td style="width: 20px; vertical-align: top; text-align: center">
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_New" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="NewCommand(); return false"
                                meta:resourcekey="ImageButton_NewResource1" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Save" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="SaveCommand(); return false"
                                meta:resourcekey="ImageButton_SaveResource1" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Delete" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="DeleteCommand(); return false"
                                meta:resourcekey="ImageButton_DeleteResource1" />
                        </td>
                        <td style="width: 20px; vertical-align: top; text-align: center">
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_First" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="FirstCommand(); return false"
                                meta:resourcekey="ImageButton_FirstResource1" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Back" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="BackCommand(); return false"
                                meta:resourcekey="ImageButton_BackResource1" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Next" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="NextCommand(); return false"
                                meta:resourcekey="ImageButton_NextResource1" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Last" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="LastCommand(); return false"
                                meta:resourcekey="ImageButton_LastResource1" />
                        </td>
                        <td style="width: 20px; vertical-align: top; text-align: center">
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Print" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="PrintCommand(); return false"
                                meta:resourcekey="ImageButton_PrintResource1" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Properties" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="PropertiesCommand(); return false"
                                meta:resourcekey="ImageButton_PropertiesResource1" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Remarks" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="RemarksCommand(); return false"
                                meta:resourcekey="ImageButton_RemarksResource1" />
                        </td>
                        <td style="width: 5%; vertical-align: top; text-align: center">
                        </td>
                    </tr>
                </table>
                <%--<table id="TBPRINT" runat="server" style="display: block; width: 100%; height: 30px;
                    text-align: left; vertical-align: top">
                    <tr>
                        <td style="width: 30px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Printing" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClick="WorkFlowPrint" 
                                meta:resourcekey="ImageButton_PrintingResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/16.png" />
                        </td>
                    </tr>
                </table>--%>
                <table id="TBHEADERWF" runat="server" style="display: block; width: 100%; height: 30px;
                    text-align: left; vertical-align: top">
                    <tr>
                        <td style="width: 5%; vertical-align: top; text-align: center">
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton2" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Door.png"
                                align="middle" ToolTip="Close" OnClientClick="parent.updatemyPage(); return false;"
                                meta:resourcekey="ImageButton_CloseResource1" />
                        </td>
                        <td style="width: 20px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Send" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="SendCommand(1); return false"
                                meta:resourcekey="ImageButton_SendResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Send.png" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Approve" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="SendCommand(2); return false"
                                meta:resourcekey="ImageButton_ApproveResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Approve.png" />
                        </td>
                        <td style="width: 40px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Reject" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="SendCommand(3); return false"
                                meta:resourcekey="ImageButton_RejectResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Reject.png" />
                        </td>
                        <td style="width: 30px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Return" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="SendCommand(4); return false"
                                meta:resourcekey="ImageButton_ReturnResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Return.png" />
                        </td>
                        <td style="width: 30px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Printing" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClick="ImageButton_PrintReport_Click"
                                meta:resourcekey="ImageButton_PrintingResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/16.png" />
                        </td>
                        <td style="width: 30px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Attach" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="WfattachCommand(); return false"
                                meta:resourcekey="ImageButton_AttachResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Attach.png" />
                        </td>
                        <td style="width: 5%; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_WFRemarks" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="WfRemarksCommand(); return false"
                                meta:resourcekey="ImageButton_WFRemarksResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Notes.png" />
                        </td>
                        <td style="width: 30px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_Forward" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClientClick="SendCommand(8); return false"
                                meta:resourcekey="ImageButton_Forward" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Forword2.png" />
                        </td>
                    </tr>
                </table>
                <table id="TBHEADERPRINT" runat="server" style="display: block; width: 100%; height: 30px;
                    text-align: left; vertical-align: top">
                    <tr>
                        <td style="width: 30px; vertical-align: top; text-align: center">
                            <asp:ImageButton ID="ImageButton_PrintReport" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" OnClick="ImageButton_PrintReport_Click"
                                meta:resourcekey="ImageButton_PrintingResource1" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/16.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <asp:HiddenField ID="HiddenField_ID" runat="server" />
            <asp:HiddenField ID="HiddenField_Lang" runat="server" />
            <asp:HiddenField ID="HiddenField_WFStage" runat="server" />
            <asp:HiddenField ID="HiddenField_Cal" runat="server" />
            <td style="width: 768px; vertical-align: top;">
                <asp:Label ID="Label_CancelDate" runat="server" SkinID="Label_WarningBold" meta:resourcekey="Label_CancelDateResource1"></asp:Label>
                <div id="WorkSpaceView">
                    <div style="float: right;">
                        <asp:Label ID="Label_StepEmployees" runat="server" SkinID="Label_WarningBold" Text=""></asp:Label>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
