<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StagesDesign.aspx.vb" Inherits="StagesDesign"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.ListControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.WebUI.WebCombo.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebCombo" TagPrefix="igcmbo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Documents Stages Design</title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="ImageButton_Enter">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        <Services>
            <asp:ServiceReference Path="../../Common/WebServices/CustomFormsWs.asmx" />
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
    <script src="../../Common/Script/JQuery/jquery.jsPlumb-1.3.3-all-min.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/alerts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _nct = "none";
        var active_counter = 1;
        var _active_control_id = "none";
        var cont_array = new Array(1);
        var StagesElements_array = new Array(1);
        var StagesPeople_array = new Array(1);
        var StagesAction_array = new Array(1);
        var StagesActionNotify_array = new Array(1);
        var StagesActionPlugin_array = new Array(1);
        var StagesActionPluginPar_array = new Array(1);
        var MainRow = null;
        $(document.documentElement).keyup(function (event) {
            if (event.keyCode == 13) {
                ApplyChanges();
            }
        });
        $(function () {
            $("#accordion").accordion({
                autoHeight: false
            });
            $("#Start").draggable({
                helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_st";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#End").draggable({
                helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_en";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Stage").draggable({
                helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_sg";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#MStage").draggable({
                helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_msg";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            jsPlumb.bind("dblclick", function (conn) {
                jQuestion("<%=Resources.Message.ConnectDelete%>" + conn.sourceId + " to " + conn.targetId + "' ?", "<%=Resources.MessageSetting.QuestionMessage%>", "<%=Resources.MessageSetting.Yes%>", "<%=Resources.MessageSetting.No%>", function (r) {
                    if (r == true) {
                        jsPlumb.detach(conn);
                    }
                });
            });
            $("#Elementsdialog-form").dialog({
                autoOpen: false,
                height: 300,
                width: 480,
                modal: true,
                buttons: {
                    "Save": function () {
                        jQuery.each(StagesElements_array, function (i, val) {
                            if (i < StagesElements_array.length - 1 && val[0] == _active_control_id && val[3] == 1) {
                                val[3] = 0;
                            }
                        });
                        jQuery.each(StagesElements_array, function (i, val) {
                            if (i < StagesElements_array.length - 1 && val[0] == _active_control_id && val[2] == 1) {
                                val[2] = 0;
                            }
                        });
                        grid = igtbl_getGridById("UWG_Elements");
                        for (x = 0; x < grid.Rows.length; x++) {
                            row = grid.Rows.getRow(x);
                            AddStagesElementsarray(_active_control_id, row.getCell(2).getValue(), (row.getCell(0).getValue() == true ? 1 : 0), (row.getCell(1).getValue() == true ? 1 : 0));
                        }
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#Peoplesdialog-form").dialog({
                autoOpen: false,
                height: 410,
                width: 520,
                modal: true,
                buttons: {
                    "Save": function () {
                        jQuery.each(StagesPeople_array, function (i, val) {
                            if (i < StagesPeople_array.length - 1 && val[0] == _active_control_id && val[4] == 1) {
                                val[4] = 0;
                            }
                        });
                        grid = igtbl_getGridById("UWG_Peoples");
                        for (x = 0; x < grid.Rows.length; x++) {
                            row = grid.Rows.getRow(x);
                            AddStagesPeoplesarray(_active_control_id, row.getCell(0).getValue(), row.getCell(1).getValue(), row.getCell(2).getValue());
                        }
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#ActionNotifydialog-form").dialog({
                autoOpen: false,
                height: 550,
                width: 520,
                modal: true,
                buttons: {
                    "Save": function () {
                        jQuery.each(StagesActionNotify_array, function (i, val) {
                            if (i < StagesActionNotify_array.length - 1 && val[0] == _active_control_id && val[1] == MainRow.getCell(1).getValue() && val[7] == 1) {
                                val[7] = 0;
                            }
                        });
                        grid = igtbl_getGridById("UWG_ActionNotify");
                        for (x = 0; x < grid.Rows.length; x++) {
                            row = grid.Rows.getRow(x);
                            AddStagesActionNotifyarray(_active_control_id, MainRow.getCell(1).getValue(), row.getCell(0).getValue(), row.getCell(1).getValue(), row.getCell(2).getValue(), row.getCell(3).getValue(), row.getCell(4).getValue());
                        }
                        jQuery.each(StagesActionPlugin_array, function (i, val) {
                            if (i < StagesActionPlugin_array.length - 1 && val[0] == _active_control_id && val[1] == MainRow.getCell(1).getValue() && val[3] == 1) {
                                val[3] = 0;
                            }
                        });
                        jQuery.each(StagesActionPluginPar_array, function (i, val) {
                            if (i < StagesActionPluginPar_array.length - 1 && val[0] == _active_control_id && val[1] == MainRow.getCell(1).getValue() && val[5] == 1) {
                                val[5] = 0;
                            }
                        });
                        grid1 = igtbl_getGridById("UWG_ActionPlugin");
                        for (x = 0; x < grid1.Rows.length; x++) {
                            row = grid1.Rows.getRow(x);
                            if (row.getCell(0).getValue() == true) {
                                AddStagesActionPluginarray(_active_control_id, MainRow.getCell(1).getValue(), row.getCell(1).getValue());
                                var rowsChilds = row.getChildRows();
                                for (z = 0; z < rowsChilds.length; z++) {
                                    row1 = igtbl_getRowById(rowsChilds[z].id);
                                    AddStagesActionPluginPararray(_active_control_id, MainRow.getCell(1).getValue(), row1.getCell(1).getValue(), row1.getCell(0).getValue(), row1.getCell(3).getValue());
                                }
                            }
                        }
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#Actionsdialog-form").dialog({
                autoOpen: false,
                height: 300,
                width: 480,
                modal: true,
                buttons: {
                    "Save": function () {
                        jQuery.each(StagesAction_array, function (i, val) {
                            if (i < StagesAction_array.length - 1 && val[0] == _active_control_id && val[2] == 1) {
                                val[2] = 0;
                            }
                        });
                        grid = igtbl_getGridById("UWG_Actions");
                        for (x = 0; x < grid.Rows.length; x++) {
                            row = grid.Rows.getRow(x);
                            if (row.getCell(0).getValue() == true) {
                                AddStagesActionsarray(_active_control_id, row.getCell(1).getValue());
                            }
                        }
                        $(this).dialog("close");
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
            $("#InitInfodialog-form").dialog({
                autoOpen: false,
                height: 300,
                width: 520,
                modal: true,
                buttons: {
                    Close: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });

        function create_control(DivPos) {
            var check = false;
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1 && val[8] != '1') {
                    if ((_nct == "menu_st") && (val[1] == ("Start"))) {
                        check = true;
                        jFailled("<%=Resources.Message.OnlyOneStart%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
                    }
                    else if ((_nct == "menu_en") && (val[1] == ("End"))) {
                        check = true;
                        jFailled("<%=Resources.Message.OnlyOneEnd%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
                    }
                }
            });
            if (check == false) {
                var ctrl_type;
                var ctrl_value = '';
                var ctrl_Title = '';
                var MaxLen = '';
                div = $("<div>");
                active_counter = 1;
                checkID();
                switch (_nct) {
                    case "menu_st": { ctrl_value = ''; ctrl_Title = 'Start' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="st" src="../../Common/Images/DocumentWF/ToolBox/Start.png" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="width:100px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Start'; MaxLen = '0'; break; }
                    case "menu_en": { ctrl_value = ''; ctrl_Title = 'End' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="en" src="../../Common/Images/DocumentWF/ToolBox/End.png" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="width:100px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'End'; MaxLen = '0'; break; }
                    case "menu_sg": { ctrl_value = ''; ctrl_Title = 'Stage' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="sg" src="../../Common/Images/DocumentWF/ToolBox/Stage.png" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="width:120px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Stage'; MaxLen = '0'; break; }
                    case "menu_msg": { ctrl_value = ''; ctrl_Title = 'MStage' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="sg" src="../../Common/Images/DocumentWF/ToolBox/MStage.png" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="width:120px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'MStage'; MaxLen = '0'; break; }

                }

                div.attr("id", 'cont_' + _nct + active_counter);
                div.attr("class", 'window');
                div.css({ "position": "absolute", "text-align": "left", "left": DivPos.left + "px", "top": DivPos.top + "px" });
                $("#WorkSpace").prepend(div);
                add_action(_nct + active_counter);
                zero_pos = $("#WorkSpace").offset();
                AddControlsArray(_nct + active_counter, ctrl_type, '', DivPos.top - zero_pos.top, DivPos.left - zero_pos.left, active_counter);
                activate_control(_nct + active_counter);
                ApplyChanges();
                CreateConnection('cont_' + _nct + active_counter);
            }
        }
        function checkID() {
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (cont_array[i][0] == _nct + active_counter) {
                        active_counter = active_counter + 1;
                        checkID()
                    }
                }
            });
        }
        function AddControlsArray(id, type, Title, top, left, Rank) {
            cont_array[cont_array.length - 1] = new Array(12)
            cont_array[cont_array.length - 1][0] = id;
            cont_array[cont_array.length - 1][1] = type;
            cont_array[cont_array.length - 1][2] = top;
            cont_array[cont_array.length - 1][3] = left;
            cont_array[cont_array.length - 1][4] = Rank;
            cont_array[cont_array.length - 1][5] = Title;
            cont_array[cont_array.length - 1][6] = '';
            cont_array[cont_array.length - 1][7] = '';
            cont_array[cont_array.length - 1][8] = '0';
            cont_array[cont_array.length - 1][9] = false;
            cont_array[cont_array.length - 1][10] = '0';
            cont_array[cont_array.length - 1][11] = '';
            cont_array.length = cont_array.length + 1;

            div = $("<div>");
            var code = 'cont_' + id;
            div.bind('click', function () { activate_control(code.substring(5)); });
            $("#" + code.substring(5, 12)).prepend(div);
        }
        function AddStagesElementsarray(StageName, ElementName, IsHide, IsDisable) {
            StagesElements_array[StagesElements_array.length - 1] = new Array(4)
            StagesElements_array[StagesElements_array.length - 1][0] = StageName;
            StagesElements_array[StagesElements_array.length - 1][1] = ElementName;
            StagesElements_array[StagesElements_array.length - 1][2] = IsHide;
            StagesElements_array[StagesElements_array.length - 1][3] = IsDisable;
            StagesElements_array.length = StagesElements_array.length + 1;
        }
        function AddStagesPeoplesarray(StageName, Peopletype, Position, Employee) {
            StagesPeople_array[StagesPeople_array.length - 1] = new Array(5)
            StagesPeople_array[StagesPeople_array.length - 1][0] = StageName;
            StagesPeople_array[StagesPeople_array.length - 1][1] = Peopletype;
            StagesPeople_array[StagesPeople_array.length - 1][2] = Position;
            StagesPeople_array[StagesPeople_array.length - 1][3] = Employee;
            StagesPeople_array[StagesPeople_array.length - 1][4] = 1;
            StagesPeople_array.length = StagesPeople_array.length + 1;
        }
        function AddStagesActionsarray(StageName, Action) {
            StagesAction_array[StagesAction_array.length - 1] = new Array(3)
            StagesAction_array[StagesAction_array.length - 1][0] = StageName;
            StagesAction_array[StagesAction_array.length - 1][1] = Action;
            StagesAction_array[StagesAction_array.length - 1][2] = 1;
            StagesAction_array.length = StagesAction_array.length + 1;
        }
        function AddStagesActionNotifyarray(StageName, Action, NotiTarget, NotifyStage, NotifyPosition, NotifyEmployee, Notification) {
            StagesActionNotify_array[StagesActionNotify_array.length - 1] = new Array(8)
            StagesActionNotify_array[StagesActionNotify_array.length - 1][0] = StageName;
            StagesActionNotify_array[StagesActionNotify_array.length - 1][1] = Action;
            StagesActionNotify_array[StagesActionNotify_array.length - 1][2] = NotiTarget;
            StagesActionNotify_array[StagesActionNotify_array.length - 1][3] = NotifyStage;
            StagesActionNotify_array[StagesActionNotify_array.length - 1][4] = NotifyPosition;
            StagesActionNotify_array[StagesActionNotify_array.length - 1][5] = NotifyEmployee;
            StagesActionNotify_array[StagesActionNotify_array.length - 1][6] = Notification;
            StagesActionNotify_array[StagesActionNotify_array.length - 1][7] = 1;
            StagesActionNotify_array.length = StagesActionNotify_array.length + 1;
        }
        function AddStagesActionPluginarray(StageName, Action, EventPluginID) {
            StagesActionPlugin_array[StagesActionPlugin_array.length - 1] = new Array(4)
            StagesActionPlugin_array[StagesActionPlugin_array.length - 1][0] = StageName;
            StagesActionPlugin_array[StagesActionPlugin_array.length - 1][1] = Action;
            StagesActionPlugin_array[StagesActionPlugin_array.length - 1][2] = EventPluginID;
            StagesActionPlugin_array[StagesActionPlugin_array.length - 1][3] = 1;
            StagesActionPlugin_array.length = StagesActionPlugin_array.length + 1;
        }
        function AddStagesActionPluginPararray(StageName, Action, EventPluginID, EventPluginParameterID, ElementCode) {
            StagesActionPluginPar_array[StagesActionPluginPar_array.length - 1] = new Array(6)
            StagesActionPluginPar_array[StagesActionPluginPar_array.length - 1][0] = StageName;
            StagesActionPluginPar_array[StagesActionPluginPar_array.length - 1][1] = Action;
            StagesActionPluginPar_array[StagesActionPluginPar_array.length - 1][2] = EventPluginID;
            StagesActionPluginPar_array[StagesActionPluginPar_array.length - 1][3] = EventPluginParameterID;
            StagesActionPluginPar_array[StagesActionPluginPar_array.length - 1][4] = ElementCode;
            StagesActionPluginPar_array[StagesActionPluginPar_array.length - 1][5] = 1;
            StagesActionPluginPar_array.length = StagesActionPluginPar_array.length + 1;
        }
        function ApplyChanges() {
            zero_pos = $("#WorkSpace").offset();
            if (_active_control_id != "none" && (_active_control_id != "")) {
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[0] == (_active_control_id)) {
                            cont_array[i][1] = document.getElementById('<%=TextBox_Type.ClientID%>').value;
                            cont_array[i][4] = document.getElementById('<%=TextBox_Rank.ClientID%>').value;
                            cont_array[i][5] = document.getElementById('<%=TextBox_CommentAr.ClientID%>').value;
                            cont_array[i][9] = document.getElementById('<%=CheckBox_WFA.ClientID%>').checked;
                            cont_array[i][10] = document.getElementById('<%=TextBox_EsclationTime.ClientID%>').value;
                            cont_array[i][11] = document.getElementById('<%=TextBox_CommentEn.ClientID%>').value;
                            pos = $("#cont_" + _active_control_id).offset();
                            cont_array[i][2] = pos.top - zero_pos.top;
                            cont_array[i][3] = pos.left - zero_pos.left;

                            if (cont_array[i][5] != "" && cont_array[i][1] == "Stage") {
                                $('#cont_' + _active_control_id).html(cont_array[i][5] + "|" + cont_array[i][11]);
                            }
                            else if (cont_array[i][5] == "" && cont_array[i][1] == "Stage") {
                                $('#cont_' + _active_control_id).html('<img class="sg" src="../../Common/Images/DocumentWF/ToolBox/Stage.png" name="' + cont_array[i][0] + '" id="' + cont_array[i][0] + '" dir="ltr" align="left" style="width:120px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">');
                            }
                            else if (cont_array[i][5] == "" && cont_array[i][1] == "MStage") {
                                $('#cont_' + _active_control_id).html('<img class="sg" src="../../Common/Images/DocumentWF/ToolBox/MStage.png" name="' + cont_array[i][0] + '" id="' + cont_array[i][0] + '" dir="ltr" align="left" style="width:120px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">');
                            }
                            $('#cont_' + _active_control_id).css({ "top": parseInt(cont_array[i][2]) + zero_pos.top + "px" });
                            $('#cont_' + _active_control_id).css({ "left": parseInt(cont_array[i][3]) + zero_pos.left + "px" });
                            activate_control(_active_control_id);
                        }
                    }
                });
            }
        }
        function ShowProperties(id) {
            document.getElementById('<%=TextBox_ElementlID.ClientID%>').value = '';
            document.getElementById('<%=TextBox_Type.ClientID%>').value = '';
            document.getElementById('<%=TextBox_Rank.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_CommentAr.ClientID%>').value = '';
            document.getElementById('<%=TextBox_CommentEn.ClientID%>').value = '';
            document.getElementById('<%=CheckBox_WFA.ClientID%>').checked = false;
            document.getElementById('<%=TextBox_EsclationTime.ClientID%>').value = '0';

            document.getElementById('<%=TextBox_Type.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_Rank.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_CommentAr.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_CommentEn.ClientID%>').disabled = true;
            document.getElementById('<%=CheckBox_WFA.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_EsclationTime.ClientID%>').disabled = true;

            document.getElementById('<%=ImageButton_People.ClientID%>').disabled = true;
            document.getElementById('<%=ImageButton_Actions.ClientID%>').disabled = true;
            document.getElementById('<%=ImageButton_Controls.ClientID%>').disabled = true;

            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[0] == (id) && val[8] != '1') {
                        document.getElementById('<%=TextBox_ElementlID.ClientID%>').value = id;
                        document.getElementById('<%=TextBox_Type.ClientID%>').value = cont_array[i][1];
                        document.getElementById('<%=TextBox_Rank.ClientID%>').value = cont_array[i][4];
                        document.getElementById('<%=TextBox_CommentAr.ClientID%>').value = cont_array[i][5];
                        document.getElementById('<%=TextBox_CommentEn.ClientID%>').value = cont_array[i][11];
                        document.getElementById('<%=CheckBox_WFA.ClientID%>').checked = cont_array[i][9];
                        document.getElementById('<%=TextBox_EsclationTime.ClientID%>').value = cont_array[i][10];

                        if (cont_array[i][1] == 'Stage') {
                            document.getElementById('<%=TextBox_Rank.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_CommentAr.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_CommentEn.ClientID%>').disabled = false;
                            document.getElementById('<%=ImageButton_People.ClientID%>').disabled = false;
                            document.getElementById('<%=ImageButton_Actions.ClientID%>').disabled = false;
                            document.getElementById('<%=ImageButton_Controls.ClientID%>').disabled = false;
                            document.getElementById('<%=CheckBox_WFA.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_EsclationTime.ClientID%>').disabled = false;
                        }
                    }
                }
            });
        }
        function add_action(id) {
            $("#cont_" + id).click(function (e) {
                activate_control(id);
            });
            $("#cont_" + id).dblclick(function (e) {
                jQuestion("<%=Resources.Message.ElementDelete%>", "<%=Resources.MessageSetting.QuestionMessage%>", "<%=Resources.MessageSetting.Yes%>", "<%=Resources.MessageSetting.No%>", function (r) {
                    if (r == true) {
                        jsPlumb.detachAll($("#cont_" + id));
                        jsPlumb.removeAllEndpoints($("#cont_" + id));
                        $("#cont_" + id).remove();
                        jQuery.each(cont_array, function (i, val) {
                            if (i < cont_array.length - 1) {
                                if (val[0] == (id)) {
                                    cont_array[i][8] = '1';
                                }
                            }
                        });
                    }
                    ShowProperties(id);
                });
            });
        }
        function activate_control(id) {
            var x = $("#" + id).width();
            var y = $("#" + id).height();
            $("#cont_" + id).css({ "width": (x) + "px", "height": (y) + "px" });
            ShowProperties(id);
            _active_control_id = id;
        }
        $(window).load(function () {
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
                CustomFormsWs.LoadStages(DocumentID, BuildCntrols);
                CustomFormsWs.ReturnStageElements(DocumentID, FillStageElements);
                CustomFormsWs.ReturnStagePeoples(DocumentID, FillStagePeoples);
                CustomFormsWs.ReturnStageActions(DocumentID, FillStageActions);
                CustomFormsWs.ReturnStageActionsNotify(DocumentID, FillStageActionNotify);
                CustomFormsWs.ReturnStageActionsPlugin(DocumentID, FillStageActionPlugin);
                CustomFormsWs.ReturnStageActionsPluginParam(DocumentID, FillStageActionPluginPar);
            }
        })
        function FillStageElements(result) {
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(";");
                AddStagesElementsarray(PropArrays[0], PropArrays[1], PropArrays[2], PropArrays[3]);
            }
        }
        function FillStagePeoples(result) {
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(";");
                PeopleType = parseInt(PropArrays[1]);
                Position = PropArrays[2] == "" ? null : parseInt(PropArrays[2]);
                Employee = PropArrays[3] == "" ? null : parseInt(PropArrays[3]);
                AddStagesPeoplesarray(PropArrays[0], PeopleType, Position, Employee);
            }
        }
        function FillStageActions(result) {
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(";");
                AddStagesActionsarray(PropArrays[0], PropArrays[1]);
            }
        }
        function FillStageActionNotify(result) {
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(";");
                Action = PropArrays[1];
                Target = PropArrays[2] == "" ? null : parseInt(PropArrays[2]);
                Position = PropArrays[4] == "" ? null : parseInt(PropArrays[4]);
                Employee = PropArrays[5] == "" ? null : parseInt(PropArrays[5]);
                Notif = PropArrays[6] == "" ? null : parseInt(PropArrays[6]);
                AddStagesActionNotifyarray(PropArrays[0], Action, Target, PropArrays[3], Position, Employee, Notif);
            }
        }
        function FillStageActionPlugin(result) {
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(";");
                Action = PropArrays[1];
                Plugin = PropArrays[2] == "" ? null : parseInt(PropArrays[2]);
                AddStagesActionPluginarray(PropArrays[0], Action, Plugin);
            }
        }
        function FillStageActionPluginPar(result) {
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(";");
                Action = PropArrays[1];
                Plugin = PropArrays[2] == "" ? null : parseInt(PropArrays[2]);
                PluginPar = PropArrays[3] == "" ? null : parseInt(PropArrays[3]);
                Element = PropArrays[4] == "" ? null : PropArrays[4];
                AddStagesActionPluginPararray(PropArrays[0], Action, Plugin, PluginPar, Element);
            }
        }
        function BuildCntrols(result) {
            zero_pos = $("#WorkSpace").offset();
            if (result == "") return;
            CtrlArrays = result.split("|");
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(",");
                var ctrl_type;
                var ctrl_value = '';
                var ctrl_Title = '';
                var MaxLen = '';
                div = $("<div>");
                if (PropArrays[0].indexOf("menu_st") != -1) { _nct = "menu_st"; }
                else if (PropArrays[0].indexOf("menu_en") != -1) { _nct = "menu_en"; }
                else if (PropArrays[0].indexOf("menu_sg") != -1) { _nct = "menu_sg"; }

                switch (_nct) {
                    case "menu_st": { ctrl_value = ''; ctrl_Title = ''; div.html('<img alt="' + ctrl_value + '"  class="st" src="../../Common/Images/DocumentWF/ToolBox/Start.png" name="Image' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:100px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Start'; MaxLen = '0'; break; }
                    case "menu_en": { ctrl_value = ''; ctrl_Title = ''; div.html('<img alt="' + ctrl_value + '"  class="en" src="../../Common/Images/DocumentWF/ToolBox/End.png" name="Image' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:100px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'End'; MaxLen = '0'; break; }
                    case "menu_sg": { ctrl_value = ''; ctrl_Title = ''; div.html('<img alt="' + ctrl_value + '"  class="sg" src="../../Common/Images/DocumentWF/ToolBox/Stage.png" name="Image' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:120px;hieght:100px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Stage'; MaxLen = '0'; break; }
                }

                div.attr("id", 'cont_' + PropArrays[0]);
                div.attr("class", 'window');
                div.css({ "position": "absolute", "text-align": "left", "left": parseInt(PropArrays[3]) + zero_pos.left + "px", "top": parseInt(PropArrays[2]) + zero_pos.top + "px" });
                $("#WorkSpace").prepend(div);
                add_action(PropArrays[0]);
                AddControlsArray(PropArrays[0], ctrl_type, '', PropArrays[2], PropArrays[3], PropArrays[4]);
                activate_control(PropArrays[0]);

                document.getElementById('<%=TextBox_ElementlID.ClientID%>').value = PropArrays[0];
                document.getElementById('<%=TextBox_Type.ClientID%>').value = PropArrays[1];
                document.getElementById('<%=TextBox_Rank.ClientID%>').value = PropArrays[4];
                document.getElementById('<%=TextBox_CommentAr.ClientID%>').value = PropArrays[5];
                document.getElementById('<%=TextBox_CommentEn.ClientID%>').value = PropArrays[11];
                document.getElementById('<%=CheckBox_WFA.ClientID%>').checked = PropArrays[9] == "False" ? false : true;
                document.getElementById('<%=TextBox_EsclationTime.ClientID%>').value = PropArrays[10];

                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (cont_array[i][0] == PropArrays[0]) {
                            cont_array[i][6] = PropArrays[6];
                            cont_array[i][7] = PropArrays[7];
                        }
                    }
                });
                ApplyChanges();
            }
            CreateConnection("");
        }
        function CreateConnection(DivCtrl) {
            window.jsPlumbDemo = {
                init: function () {
                    jsPlumb.Defaults.DragOptions = { cursor: 'pointer', zIndex: 2000 };
                    jsPlumb.Defaults.EndpointStyles = [{ fillStyle: '#225588' }, { fillStyle: '#558822'}];
                    jsPlumb.Defaults.Endpoints = [["Dot", { radius: 7}], ["Dot", { radius: 11}]];
                    jsPlumb.setMouseEventsEnabled(true);
                    jsPlumb.Defaults.Overlays = [
				["Arrow", { location: 0.9}],
				["Label", {
				    location: 0.1,
				    label: function (label) {
				        return label.connection.labelText || "";
				    },
				    cssClass: "aLabel"
				}]
                    ];
                    var connectorPaintStyle = {
                        lineWidth: 5,
                        strokeStyle: "#deea18",
                        joinstyle: "round"
                    },
			connectorHoverStyle = {
			    lineWidth: 7,
			    strokeStyle: "#2e2aF8"
			},
			sourceEndpoint = {
			    endpoint: "Dot",
			    paintStyle: { fillStyle: "#225588", radius: 7 },
			    isSource: true,
			    connector: ["Flowchart", { stub: 40}],
			    connectorStyle: connectorPaintStyle,
			    hoverPaintStyle: connectorHoverStyle,
			    connectorHoverStyle: connectorHoverStyle
			},
			bottomSource = jsPlumb.extend({ anchor: ["BottomCenter", "TopCenter"] }, sourceEndpoint),
            BottomRight = jsPlumb.extend({ anchor: "BottomRight" }, sourceEndpoint),
            BottomLeft = jsPlumb.extend({ anchor: "BottomLeft" }, sourceEndpoint),

			targetEndpoint = {
			    endpoint: "Dot",
			    paintStyle: { fillStyle: "#558822", radius: 11 },
			    hoverPaintStyle: connectorHoverStyle,
			    maxConnections: -1,
			    dropOptions: { hoverClass: "hover", activeClass: "active" },
			    isTarget: true,
			    anchor: ["LeftMiddle", "RightMiddle", "TopCenter", "BottomCenter"]
			},

			    targetpoint = {
			        endpoint: "Dot",
			        paintStyle: { fillStyle: "#558822", radius: 11 },
			        hoverPaintStyle: connectorHoverStyle,
			        maxConnections: -1,
			        dropOptions: { hoverClass: "hover", activeClass: "active" },
			        isTarget: true,
			        anchor: ["LeftMiddle", "RightMiddle"]
			    },

            			targetTopCenter = {
            			    endpoint: "Dot",
            			    paintStyle: { fillStyle: "#558822", radius: 11 },
            			    hoverPaintStyle: connectorHoverStyle,
            			    maxConnections: -1,
            			    dropOptions: { hoverClass: "hover", activeClass: "active" },
            			    isTarget: true,
            			    anchor: "TopCenter"
            			},

                        			targetTopRight = {
                        			    endpoint: "Dot",
                        			    paintStyle: { fillStyle: "#558822", radius: 11 },
                        			    hoverPaintStyle: connectorHoverStyle,
                        			    maxConnections: -1,
                        			    dropOptions: { hoverClass: "hover", activeClass: "active" },
                        			    isTarget: true,
                        			    anchor: "TopRight"
                        			},

                                    			targetTopLeft = {
                                    			    endpoint: "Dot",
                                    			    paintStyle: { fillStyle: "#558822", radius: 11 },
                                    			    hoverPaintStyle: connectorHoverStyle,
                                    			    maxConnections: -1,
                                    			    dropOptions: { hoverClass: "hover", activeClass: "active" },
                                    			    isTarget: true,
                                    			    anchor: "TopLeft"
                                    			},
			init = function (connection) {
			    connection.labelText = connection.sourceId + "-" + connection.targetId;
			};
                    if (DivCtrl == "") {
                        jQuery.each(cont_array, function (i, val) {
                            if (i < cont_array.length - 1) {
                                jsPlumb.draggable($("#cont_" + val[0]));
                                var HasSourceConnect = false;
                                var HasTargConnect = false;

                                jQuery.each(cont_array, function (x, valx) {
                                    if (x < cont_array.length - 1) {
                                        if (valx[6] != "" && valx[6] == "cont_" + val[0]) {
                                            HasSourceConnect = true;
                                        }
                                        if (valx[7] != "" && valx[7] == "cont_" + val[0]) {
                                            HasTargConnect = true;
                                        }
                                    }
                                });

                                if (val[1] == "Start") {
                                    if (HasSourceConnect == false) {
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), bottomSource);
                                    }
                                }
                                if (val[1] == "End") {
                                    if (HasTargConnect == false) {
                                        var targetEndpoints = jsPlumb.addEndpoint($("#cont_" + val[0]), targetEndpoint);
                                    }
                                }
                                if (val[1] == "Stage") {
                                    if (HasSourceConnect == false) {
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), bottomSource);
                                    }
                                    if (HasTargConnect == false) {
                                        var targetEndpoints = jsPlumb.addEndpoint($("#cont_" + val[0]), targetEndpoint);
                                    }
                                }
                            }
                        });
                    }
                    else {
                        jQuery.each(cont_array, function (i, val) {
                            if (i < cont_array.length - 1) {
                                if ("cont_" + val[0] == DivCtrl) {
                                    jsPlumb.draggable($("#cont_" + val[0]));
                                    if (val[1] == "Start") {
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), bottomSource);
                                    }
                                    if (val[1] == "End") {
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), targetEndpoint);

                                    }
                                    if (val[1] == "Stage") {
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), bottomSource);
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), targetpoint);


                                    }
                                    if (val[1] == "MStage") {
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), bottomSource);
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), BottomRight);
                                        jsPlumb.addEndpoint($("#cont_" + val[0]), BottomLeft);

                                        jsPlumb.addEndpoint($("#cont_" + val[0]), targetTopCenter);

                                    }
                                }
                            }
                        });
                    }
                    jsPlumb.bind("jsPlumbConnection", function (connInfo) {
                        init(connInfo.connection);
                    });
                    if (DivCtrl == "") {
                        jQuery.each(cont_array, function (i, val) {
                            if (i < cont_array.length - 1) {
                                if (val[7] != "") {
                                    var sourceEndpoints = jsPlumb.addEndpoint($("#cont_" + val[0]), bottomSource);
                                    var targetEndpoints = jsPlumb.addEndpoint($("#" + val[7]), targetEndpoint);
                                    jsPlumb.connect({
                                        source: sourceEndpoints,
                                        target: targetEndpoints
                                    });
                                }
                            }
                        });
                    }
                }
            }
            jsPlumb.bind("ready", function () {
                document.onselectstart = function () { return false; };
                var resetRenderMode = function (desiredMode) {
                    var newMode = jsPlumb.setRenderMode(desiredMode);
                    $(".rmode").removeClass("selected");
                    $(".rmode[mode='" + newMode + "']").addClass("selected");
                    var disableList = (newMode === jsPlumb.VML) ? ".rmode[mode='canvas'],.rmode[mode='svg']" : ".rmode[mode='vml']";
                    $(disableList).attr("disabled", true);
                    jsPlumbDemo.init();
                };
                $(".rmode").bind("click", function () {
                    var desiredMode = $(this).attr("mode");
                    if (jsPlumbDemo.reset) jsPlumbDemo.reset();
                    jsPlumb.reset();
                    resetRenderMode(desiredMode);
                });
                resetRenderMode(jsPlumb.CANVAS);
            });
        }
        function SaveCntrls() {
            zero_pos = $("#WorkSpace").offset();
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
                        if (val[8] != '1') {
                            pos = $("#cont_" + val[0]).offset();
                            cont_array[i][2] = pos.top - zero_pos.top;
                            cont_array[i][3] = pos.left - zero_pos.left;
                            cont_array[i][6] = "";
                            cont_array[i][7] = "";
                            var AllConn = jsPlumb.getAllConnections();
                            for (z = 0; z < AllConn._jsPlumb_DefaultScope.length; z++) {
                                if (AllConn._jsPlumb_DefaultScope[z].sourceId == "cont_" + val[0]) {
                                    cont_array[i][7] = AllConn._jsPlumb_DefaultScope[z].targetId;
                                }
                                if (AllConn._jsPlumb_DefaultScope[z].targetId == "cont_" + val[0]) {
                                    cont_array[i][6] = AllConn._jsPlumb_DefaultScope[z].sourceId;
                                }
                            }
                        }
                    }
                });
                var InitInfo_array = new Array(1);
                grid = igtbl_getGridById("UWG_InitInfo");
                for (x = 0; x < grid.Rows.length; x++) {
                    row = grid.Rows.getRow(x);
                    if (row.getCell(1).getValue() != null) {
                        InitInfo_array[InitInfo_array.length - 1] = new Array(2)
                        InitInfo_array[InitInfo_array.length - 1][0] = row.getCell(0).getValue();
                        InitInfo_array[InitInfo_array.length - 1][1] = row.getCell(1).getValue();
                        InitInfo_array.length = InitInfo_array.length + 1;
                    }
                }
                CustomFormsWs.SaveStages(cont_array, DocumentID, StagesElements_array, StagesPeople_array, StagesAction_array, InitInfo_array, StagesActionNotify_array, StagesActionPlugin_array, StagesActionPluginPar_array, afterSaveControls);
            }
        }
        function afterSaveControls(result) {
            if (result > 0) {
                jSuccess("<%=Resources.Message.Done%>", "<%=Resources.MessageSetting.SuccessMessage%>", "<%=Resources.MessageSetting.Close%>");
            }
            else {
                jFailled("<%=Resources.Message.Failed%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
            }
        }
        function ShowPeoples() {
            grid = igtbl_getGridById("UWG_Peoples");
            var Len = grid.Rows.length;
            for (x = 0; x < Len; x++) {
                grid.Rows.remove(Len - x - 1);
            }
            jQuery.each(StagesPeople_array, function (i, val) {
                if (i < StagesPeople_array.length - 1 && val[0] == _active_control_id && val[4] == 1) {
                    row = grid.Rows.addNew();
                    row.getCell(0).setValue(val[1]);
                    row.getCell(1).setValue(val[2]);
                    row.getCell(2).setValue(val[3]);
                }
            });
            document.getElementById('<%=DropDownList_PeopleType.ClientID%>').value = 1;
            ToogleView();
            $("#Peoplesdialog-form").dialog("open");
        }
        function ToogleView() {
            if (document.getElementById('<%=DropDownList_PeopleType.ClientID%>').value == "1") {
                document.getElementById('<%=DropDownList_Positions.ClientID%>').disabled = true;
            }
            else if (document.getElementById('<%=DropDownList_PeopleType.ClientID%>').value == "2") {
                document.getElementById('<%=DropDownList_Positions.ClientID%>').disabled = false;
            }
            else if (document.getElementById('<%=DropDownList_PeopleType.ClientID%>').value == "3") {
                document.getElementById('<%=DropDownList_Positions.ClientID%>').disabled = true;
            }
        }
        function ToogleView1() {
            if (document.getElementById('<%=DropDownList_NotifTarget.ClientID%>').value == "1") {
                document.getElementById('<%=DropDownList_NotSatges.ClientID%>').disabled = false;
                document.getElementById('<%=DropDownList_NotiPositions.ClientID%>').disabled = true;
            }
            else if (document.getElementById('<%=DropDownList_NotifTarget.ClientID%>').value == "2") {
                document.getElementById('<%=DropDownList_NotSatges.ClientID%>').disabled = true;
                document.getElementById('<%=DropDownList_NotiPositions.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=DropDownList_NotSatges.ClientID%>').disabled = true;
                document.getElementById('<%=DropDownList_NotiPositions.ClientID%>').disabled = true;
            }
        }
        function SaveStagePeople() {
            Peopletype = null;
            Position = null;
            employee = null;
            Peopletype = document.getElementById('<%=DropDownList_PeopleType.ClientID%>').value;
            if (document.getElementById('<%=DropDownList_Positions.ClientID%>').disabled == false) {
                Position = document.getElementById('<%=DropDownList_Positions.ClientID%>').value;
            }
            grid = igtbl_getGridById("UWG_Peoples");
            row = grid.Rows.addNew();
            row.getCell(0).setValue(Peopletype);
            row.getCell(1).setValue(Position);
            row.getCell(2).setValue(employee);
        }
        function SaveStageActionNotify() {
            Target = null;
            Stage = null;
            Position = null;
            employee = null;
            Notification = null;
            Target = document.getElementById('<%=DropDownList_NotifTarget.ClientID%>').value;
            if (document.getElementById('<%=DropDownList_NotSatges.ClientID%>').disabled == false) {
                Stage = document.getElementById('<%=DropDownList_NotSatges.ClientID%>').value;
            }
            if (document.getElementById('<%=DropDownList_NotiPositions.ClientID%>').disabled == false) {
                Position = document.getElementById('<%=DropDownList_NotiPositions.ClientID%>').value;
            }
            Notification = document.getElementById('<%=DropDownList_NotifWays.ClientID%>').value;
            grid = igtbl_getGridById("UWG_ActionNotify");
            row = grid.Rows.addNew();
            row.getCell(0).setValue(Target);
            row.getCell(1).setValue(Stage);
            row.getCell(2).setValue(Position);
            row.getCell(3).setValue(employee);
            row.getCell(4).setValue(Notification);
        }
        function ShowActions() {
            grid = igtbl_getGridById("UWG_Actions");
            for (x = 0; x < grid.Rows.length; x++) {
                row = grid.Rows.getRow(x);
                row.getCell(0).setValue(false);
            }
            jQuery.each(StagesAction_array, function (i, val) {
                if (i < StagesAction_array.length - 1 && val[0] == _active_control_id && val[2] == 1) {
                    for (x = 0; x < grid.Rows.length; x++) {
                        row = grid.Rows.getRow(x);
                        if (row.getCell(1).getValue() == val[1]) {
                            row.getCell(0).setValue(true);
                        }
                    }
                }
            });
            $("#Actionsdialog-form").dialog("open");
        }
        function ShowControls() {
            grid = igtbl_getGridById("UWG_Elements");
            for (x = 0; x < grid.Rows.length; x++) {
                row = grid.Rows.getRow(x);
                row.getCell(0).setValue(false);
                row.getCell(1).setValue(false);
            }
            jQuery.each(StagesElements_array, function (i, val) {
                if (i < StagesElements_array.length - 1 && val[0] == _active_control_id) {
                    for (x = 0; x < grid.Rows.length; x++) {
                        row = grid.Rows.getRow(x);
                        if (row.getCell(2).getValue() == val[1]) {
                            if (val[2] == 1) {
                                row.getCell(0).setValue(true);
                            }
                            if (val[3] == 1) {
                                row.getCell(1).setValue(true);
                            }
                        }
                    }
                }
            });
            $("#Elementsdialog-form").dialog("open");
        }
        function ShowInitInfo() {
            $("#InitInfodialog-form").dialog("open");
        }
    </script>
    <script type="text/javascript" id="igClientScript">
        function UWG_Actions_DblClickHandler(gridName, cellId) {
            var grid = igtbl_getGridById(gridName);
            var cell = igtbl_getCellById(cellId);
            var Row = igtbl_getRowById(cellId);
            MainRow = Row;
            var rowIndex = Row.getIndex();
            var select = document.getElementById('<%=DropDownList_NotSatges.ClientID%>')
            for (i = select.options.length - 1; i >= 0; i--) {
                select.remove(i);
            }
            if (cell.Column.Index == 2) {
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1 && val[8] != '1') {
                        if (val[1] == "Stage") {
                            valMember = val[0];
                            DisMember = val[0];
                            select.options.add(new Option(DisMember, valMember));
                        }
                    }
                });
                grid1 = igtbl_getGridById("UWG_ActionNotify");
                var Len = grid1.Rows.length;
                for (x = 0; x < Len; x++) {
                    grid1.Rows.remove(Len - x - 1);
                }
                jQuery.each(StagesActionNotify_array, function (i, val) {
                    if (i < StagesActionNotify_array.length - 1 && val[0] == _active_control_id && val[1] == Row.getCell(1).getValue() && val[7] == 1) {
                        row1 = grid1.Rows.addNew();
                        row1.getCell(0).setValue(val[2]);
                        row1.getCell(1).setValue(val[3]);
                        row1.getCell(2).setValue(val[4]);
                        row1.getCell(3).setValue(val[5]);
                        row1.getCell(4).setValue(val[6]);
                    }
                });
                document.getElementById('<%=DropDownList_NotifTarget.ClientID%>').value = 1;
                ToogleView1();

                grid2 = igtbl_getGridById("UWG_ActionPlugin");
                for (i = 0; i < grid2.Rows.length; i++) {
                    row = grid2.Rows.getRow(i);
                    row.getCell(0).setValue(false);
                    var rowsChilds = row.getChildRows();
                    for (x = 0; x < rowsChilds.length; x++) {
                        row1 = igtbl_getRowById(rowsChilds[x].id);
                        row1.getCell(3).setValue(null);
                    }
                }
                jQuery.each(StagesActionPlugin_array, function (i, val) {
                    if (i < StagesActionPlugin_array.length - 1 && val[0] == _active_control_id && val[1] == Row.getCell(1).getValue() && val[3] == 1) {
                        for (x = 0; x < grid2.Rows.length; x++) {
                            row = grid2.Rows.getRow(x);
                            if (row.getCell(1).getValue() == val[2]) {
                                row.getCell(0).setValue(true);
                                var rowsChilds = row.getChildRows();
                                jQuery.each(StagesActionPluginPar_array, function (m, valm) {
                                    if (m < StagesActionPluginPar_array.length - 1 && valm[0] == _active_control_id && valm[2] == val[2] && valm[1] == Row.getCell(1).getValue() && valm[5] == 1) {
                                        for (z = 0; z < rowsChilds.length; z++) {
                                            row1 = igtbl_getRowById(rowsChilds[z].id);
                                            if (row1.getCell(0).getValue() == valm[3]) {
                                                row1.getCell(3).setValue(valm[4]);
                                            }
                                        }
                                    }
                                });
                            }
                        }
                    }
                });
                $("#ActionNotifydialog-form").dialog("open");
            }
        }
    </script>
    <table align="center" style="border: thin solid #CCCCCC;">
        <tr>
            <td style="height: 30px" colspan="2">
                <table id="TBHEADER" runat="server" style="width: 100%; height: 30px; text-align: left;
                    vertical-align: top">
                    <tr>
                        <td style="display: none">
                            <asp:ImageButton ID="ImageButton_Enter" runat="server" Width="0px" Height="0px" ImageAlign="Middle"
                                CausesValidation="False" OnClientClick="ApplyChanges(); return false" />
                        </td>
                        <td style="width: 10px; height: 30px; text-align: left; vertical-align: top">
                        </td>
                        <td style="width: 24px; height: 30px; text-align: left; vertical-align: top">
                            <asp:ImageButton ID="ImageButton_Close" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Door.png"
                                align="middle" ToolTip="Close" OnClientClick="window.close(); return false" />
                        </td>
                        <td style="width: 24px; height: 30px; text-align: left; vertical-align: top">
                            <asp:ImageButton ID="ImageButton_Save" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/86.png"
                                align="middle" ToolTip="Save" OnClientClick="SaveCntrls(); return false" />
                        </td>
                        <td style="width: 1000px; height: 30px; text-align: left; vertical-align: top">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 768px; vertical-align: top">
                <div id="WorkSpace">
                </div>
            </td>
            <td style="width: 380px; vertical-align: top;">
                <div id="accordion" style="width: 380px; background-color: transparent; border: 0px solid #777;
                    padding: 1px; float: left; left: 780px; top: 0px; vertical-align: top; text-align: left;">
                    <h3>
                        <a href="#">Select Elements</a></h3>
                    <table>
                        <tr>
                            <td>
                                <div id="Start" style="position: static; width: 300px; height: 100px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 80px">
                                        <tr>
                                            <td style="width: 80px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_TextBox" runat="server" ImageUrl="../../Common/Images/DocumentWF/ToolBox/Start.png"
                                                    Width="80px" Height="80px" />
                                            </td>
                                            <td style="width: 20px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label1" runat="server" Text="|" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 80px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label2" runat="server" Text="Start" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="End" style="position: static; width: 300px; height: 100px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 80px">
                                        <tr>
                                            <td style="width: 80px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_TextArea" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/End.png"
                                                    Width="80px" Height="80px" />
                                            </td>
                                            <td style="width: 20px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label3" runat="server" Text="|" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 80px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label4" runat="server" Text="End" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Stage" style="position: static; width: 300px; height: 100px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 80px">
                                        <tr>
                                            <td style="width: 80px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_Selector" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Stage.png"
                                                    Width="80px" Height="80px" />
                                            </td>
                                            <td style="width: 20px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label5" runat="server" Text="|" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 80px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label6" runat="server" Text="STage" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="MStage" style="position: static; width: 300px; height: 100px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 80px">
                                        <tr>
                                            <td style="width: 80px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/MStage.png"
                                                    Width="80px" Height="80px" />
                                            </td>
                                            <td style="width: 20px; height: 80px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label16" runat="server" Text="|" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 80px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label17" runat="server" Text="Multi STage" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <h3>
                        <a href="#">Set Elements Properites</a></h3>
                    <table style="width: 300px; vertical-align: middle; text-align: left">
                        <tr>
                            <td style="width: 300px; vertical-align: middle; text-align: left">
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label18" runat="server" Text="(ID)" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_ElementlID" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label21" runat="server" Text="Type" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Type" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="false"
                                                TabIndex="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label43" runat="server" Text="Rank" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Rank" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="8" onchange="ApplyChanges();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label35" runat="server" Text="Arabic Comment" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_CommentAr" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="11" onchange="ApplyChanges();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label13" runat="server" Text="English Comment" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_CommentEn" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="11" onchange="ApplyChanges();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label55" runat="server" Text="Waiting For All" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:CheckBox ID="CheckBox_WFA" runat="server" onclick="ApplyChanges();" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label10" runat="server" Text="Esclation Time (Day)" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_EsclationTime" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="11" onchange="ApplyChanges();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Initiator On  Load Information"
                                                OnClientClick="ShowInitInfo(); return false;"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 50px">
                                    <tr>
                                        <td style="width: 100px; height: 40px; vertical-align: middle; text-align: center">
                                            <asp:ImageButton ID="ImageButton_People" runat="server" Width="48px" Height="48px"
                                                ImageUrl="~/Common/Images/DocumentWF/Peoples.png" CausesValidation="False" OnClientClick="ShowPeoples(); return false;" />
                                        </td>
                                        <td style="width: 100px; height: 40px; vertical-align: middle; text-align: center">
                                            <asp:ImageButton ID="ImageButton_Actions" runat="server" Width="48px" Height="48px"
                                                ImageUrl="~/Common/Images/DocumentWF/Actions.png" CausesValidation="False" OnClientClick="ShowActions(); return false;" />
                                        </td>
                                        <td style="width: 100px; height: 40px; vertical-align: middle; text-align: center">
                                            <asp:ImageButton ID="ImageButton_Controls" runat="server" Width="48px" Height="48px"
                                                ImageUrl="~/Common/Images/DocumentWF/Controls.png" CausesValidation="False" OnClientClick="ShowControls(); return false;" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="Elementsdialog-form" title="Document Stage Elements">
        <table id="Table1" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 100%; height: 100%; text-align: left; vertical-align: top">
                    <igtbl:UltraWebGrid   ID="UWG_Elements" runat="server" CaptionAlign="Top" Width="450px"
                        Height="200px">
                        <DisplayLayout BorderCollapseDefault="Separate" Name="UltraWebGrid1" RowHeightDefault="20px"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" AllowColSizingDefault="Free"
                            AllowRowNumberingDefault="Continuous" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                            CellClickActionDefault="RowSelect">
                            <FrameStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt"
                                Width="450px" Height="200px">
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
                                    <igtbl:UltraGridColumn BaseColumnName="IsHide" DataType="System.Byte" Key="IsHide"
                                        Type="CheckBox" Width="60px">
                                        <Header Caption="Is Hide">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </SelectedCellStyle>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="IsDiabled" DataType="System.Byte" Key="IsDisabled"
                                        Type="CheckBox" Width="60px">
                                        <Header Caption="Is Disabled">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </SelectedCellStyle>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="Code" Key="Code" AllowUpdate="No" Width="120px">
                                        <Header Caption="Element Name">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"
                                            Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="FriendlyName" Key="FriendlyName" Width="200px"
                                        AllowUpdate="No">
                                        <Header Caption="Element Friendly Name">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px"
                                            Wrap="True">
                                        </SelectedCellStyle>
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
    </div>
    <div id="Peoplesdialog-form" title="Document Stage Peolpes">
        <table id="Table2" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 100%; height: 80px; text-align: left; vertical-align: top">
                    <table align="left" style="width: 490px; height: 80px">
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label7" runat="server" Text="People Type" SkinID="Label_DefaultBold"></asp:Label>
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_PeopleType" runat="server" onchange="ToogleView(); return false;"
                                    SkinID="DropDownList_LargBold">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label8" runat="server" Text="Positions" SkinID="Label_DefaultBold"></asp:Label>
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_Positions" runat="server" SkinID="DropDownList_LargBold">
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
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="PeopleType" Type="DropDownList"
                                        Width="120px">
                                        <Header Caption="People Type">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px">
                                        </SelectedCellStyle>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Type="DropDownList" Key="PositionID"
                                        Width="150px" EditorControlID="">
                                        <Header Caption="Position">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"
                                            Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Type="DropDownList" Key="EmployeeID"
                                        Width="150px">
                                        <Header Caption="Employee">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="150px"
                                            Wrap="True">
                                        </SelectedCellStyle>
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
    </div>
    <div id="ActionNotifydialog-form" title="Document Stage Action Notification">
        <table id="Table5" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 100%; height: 80px; text-align: left; vertical-align: top">
                    <table align="left" style="width: 490px; height: 80px">
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label9" runat="server" Text="Notification Target" SkinID="Label_DefaultBold"></asp:Label>
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_NotifTarget" runat="server" onchange="ToogleView1(); return false;"
                                    SkinID="DropDownList_LargBold">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label12" runat="server" Text="Stages" SkinID="Label_DefaultBold"></asp:Label>
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_NotSatges" runat="server" SkinID="DropDownList_LargBold">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label11" runat="server" Text="Positions" SkinID="Label_DefaultBold"></asp:Label>
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_NotiPositions" runat="server" SkinID="DropDownList_LargBold">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:Label ID="Label14" runat="server" Text="Notification" SkinID="Label_DefaultBold"></asp:Label>
                            </td>
                            <td style="width: 370px; height: 20px; vertical-align: middle; text-align: left">
                                <asp:DropDownList ID="DropDownList_NotifWays" runat="server" SkinID="DropDownList_LargBold">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 490px; height: 20px; vertical-align: middle; text-align: center"
                                colspan="2">
                                <asp:Button ID="Button1" runat="server" Text="Save Stage Action Notify" OnClientClick="SaveStageActionNotify(); return false;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 100%; text-align: left; vertical-align: top">
                    <igtbl:UltraWebGrid   ID="UWG_ActionNotify" runat="server" CaptionAlign="Top" Width="490px"
                        Height="140px">
                        <DisplayLayout BorderCollapseDefault="Separate" Name="UltraWebGrid1" RowHeightDefault="20px"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" AllowColSizingDefault="Free"
                            AllowRowNumberingDefault="Continuous" AutoGenerateColumns="False" CellClickActionDefault="RowSelect"
                            AllowAddNewDefault="Yes" AllowDeleteDefault="Yes" AllowSortingDefault="OnClient">
                            <FrameStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt"
                                Width="490px" Height="140px">
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
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="Target" Type="DropDownList" Width="100px">
                                        <Header Caption="Target">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px">
                                        </SelectedCellStyle>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.String" Key="NoStage" Width="100px" EditorControlID="">
                                        <Header Caption="Stage">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px"
                                            Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Type="DropDownList" Key="NoPosition"
                                        Width="120px">
                                        <Header Caption="Position">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"
                                            Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn Key="NoEmployee" Width="50px" Hidden="true">
                                        <Header Caption="Employee">
                                            <RowLayoutColumnInfo OriginX="3" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="3" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="Notification" Type="DropDownList"
                                        Width="120px">
                                        <Header Caption="Notification">
                                            <RowLayoutColumnInfo OriginX="4" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"
                                            Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="4" />
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
            <tr>
                <td style="width: 100%; height: 30px; text-align: left; vertical-align: top">
                    <table align="left" style="width: 490px; height: 20px">
                        <tr>
                            <td style="width: 490px; height: 20px; vertical-align: middle; text-align: left"
                                colspan="2">
                                <asp:Label ID="Label15" runat="server" Text="Actions Events"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 100%; height: 100%; text-align: left; vertical-align: top">
                    <igtbl:UltraWebGrid   ID="UWG_ActionPlugin" runat="server" CaptionAlign="Top" Width="490px"
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
                                    <igtbl:UltraGridColumn DataType="System.Byte" Key="IsChoose" Type="CheckBox" Width="60px"
                                        AllowUpdate="Yes">
                                        <Header Caption="Choose">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </SelectedCellStyle>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="ID" Type="DropDownList" Width="0px"
                                        Hidden="true" BaseColumnName="ID">
                                        <Header Caption="ID">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn Key="EventPluginName" Type="DropDownList" Width="300px" BaseColumnName="EngName">
                                        <Header Caption="Event Plugin">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300px">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                </Columns>
                                <AddNewRow View="NotSet" Visible="NotSet">
                                </AddNewRow>
                            </igtbl:UltraGridBand>
                            <igtbl:UltraGridBand>
                                <Columns>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="ID" Type="DropDownList" Width="0px"
                                        Hidden="true" BaseColumnName="ID">
                                        <Header Caption="ID">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.Int32" Key="PluginID" Type="DropDownList"
                                        Width="0px" Hidden="true" BaseColumnName="PluginID">
                                        <Header Caption="PluginID">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="0px">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1"></RowLayoutColumnInfo>
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn Key="ParameterName" Width="170px" BaseColumnName="EngName">
                                        <Header Caption="Plugin Parameter">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="2"></RowLayoutColumnInfo>
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.String" Key="ElementCode" Type="DropDownList"
                                        Width="170px" AllowUpdate="Yes" BaseColumnName="ElementCode">
                                        <Header Caption="Document Element">
                                            <RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="170px">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="3"></RowLayoutColumnInfo>
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
    </div>
    <div id="Actionsdialog-form" title="Document Stage Actions">
        <table id="Table3" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 100%; height: 100%; text-align: left; vertical-align: top">
                    <igtbl:UltraWebGrid   ID="UWG_Actions" runat="server" CaptionAlign="Top" Width="450px"
                        Height="200px">
                        <DisplayLayout BorderCollapseDefault="Separate" Name="UltraWebGrid1" RowHeightDefault="20px"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" AllowColSizingDefault="Free"
                            AllowRowNumberingDefault="Continuous" AllowUpdateDefault="Yes" AutoGenerateColumns="False"
                            CellClickActionDefault="RowSelect">
                            <FrameStyle BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt"
                                Width="450px" Height="200px">
                            </FrameStyle>
                            <RowAlternateStyleDefault BackColor="#FFFFC0">
                                <Padding Left="3px" />
                                <BorderDetails ColorLeft="255, 255, 192" ColorTop="255, 255, 192" />
                            </RowAlternateStyleDefault>
                            <ClientSideEvents DblClickHandler="UWG_Actions_DblClickHandler" />
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
                                    <igtbl:UltraGridColumn BaseColumnName="IsChoose" DataType="System.Byte" Key="IsChoose"
                                        Type="CheckBox" Width="60px">
                                        <Header Caption="Choose">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px">
                                        </SelectedCellStyle>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="Action" Key="Action" AllowUpdate="No" Width="120px"
                                        Hidden="True">
                                        <Header Caption="Action">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="120px"
                                            Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Footer>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn BaseColumnName="ActionDescription" Key="ActionDescription"
                                        Width="200px" AllowUpdate="No">
                                        <Header Caption="Action Description">
                                            <RowLayoutColumnInfo OriginX="2" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="200px"
                                            Wrap="True">
                                        </SelectedCellStyle>
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
    </div>
    <div id="InitInfodialog-form" title="Document Stage Initiator Information">
        <table id="Table4" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 100%; height: 100%; text-align: left; vertical-align: top">
                    <igtbl:UltraWebGrid   ID="UWG_InitInfo" runat="server" CaptionAlign="Top" Width="490px"
                        Height="200px">
                        <DisplayLayout BorderCollapseDefault="Separate" Name="UltraWebGrid1" RowHeightDefault="20px"
                            StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" AllowColSizingDefault="Free"
                            AllowRowNumberingDefault="Continuous" AutoGenerateColumns="False" CellClickActionDefault="RowSelect"
                            AllowAddNewDefault="Yes" AllowSortingDefault="OnClient" AllowUpdateDefault="Yes">
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
                                    <igtbl:UltraGridColumn DataType="System.String" Key="ElementCode" Type="DropDownList"
                                        Width="220px" AllowUpdate="No">
                                        <Header Caption="Document Element">
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="220px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="220px">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="220px">
                                        </SelectedCellStyle>
                                    </igtbl:UltraGridColumn>
                                    <igtbl:UltraGridColumn DataType="System.String" Type="DropDownList" Key="SrcField"
                                        Width="220px" EditorControlID="" AllowUpdate="Yes">
                                        <Header Caption="Source Field">
                                            <RowLayoutColumnInfo OriginX="1" />
                                        </Header>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="220px" />
                                        <CellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="220px" Wrap="True">
                                        </CellStyle>
                                        <SelectedCellStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="220px"
                                            Wrap="True">
                                        </SelectedCellStyle>
                                        <Footer>
                                            <RowLayoutColumnInfo OriginX="1" />
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
    </div>
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-15400992-4']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
    </form>
</body>
</html>
