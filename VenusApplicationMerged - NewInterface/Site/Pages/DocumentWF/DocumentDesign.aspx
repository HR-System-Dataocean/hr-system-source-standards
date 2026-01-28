<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocumentDesign.aspx.vb"
    Inherits="DocumentDesign" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Documents Design</title>
</head>
<body>
    <form id="form1" runat="server">
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
    <script src="../../Common/Script/JQuery/colorPicker.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/alerts.js" type="text/javascript"></script>
    <script type="text/javascript">
        var _nct = "none";
        var active_counter = 1;
        var _active_control_id = "none";
        var cont_array = new Array(1);
        $(document.documentElement).keyup(function (event) {
            if (event.keyCode == 13) {
                ApplyChanges();
            }
        });
        $(function () {
            $("#accordion").accordion({
                autoHeight: false
            });

            $("#TextBox").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_tb";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#TextArea").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_ta";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Selector").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_so";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Label").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_lb";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Check").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_cb";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Radio").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_rb";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Line").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_ln";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Img").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_im";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#srch").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_sc";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Zoom").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_zm";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#Link").draggable({ helper: "clone", cursor: 'auto',
                start: function (event, ui) {
                    _nct = "menu_lk";
                }, stop: function (event, ui) {
                    DivPos = ui.helper.offset();
                    create_control(DivPos);
                }
            });
            $("#dialog-form").dialog({
                autoOpen: false,
                height: 300,
                width: 500,
                modal: true,
                buttons: {
                    "Save": function () {
                        document.getElementById('<%=TextBox_KeyRelated.ClientID%>').value = document.getElementById('<%=Text_ElementArea.ClientID%>').value;
                        $(this).dialog("close");
                        ApplyChanges();
                    },
                    Cancel: function () {
                        $(this).dialog("close");
                    }
                }
            });
        });
        function ShowDialog() {
            if (document.getElementById('<%=DropDownList_KeyTable.ClientID%>').value != "") {
                CustomFormsWs.ReturnObjectFields(document.getElementById('<%=DropDownList_KeyTable.ClientID%>').value, AfterShowDialog);
            }
        }
        var BoolCheck = 0;
        function AfterShowDialog(result) {
            document.getElementById('<%=ListBox_ObjectFields.ClientID%>').options.length = 0;
            document.getElementById('<%=ListBox_ObjectCtrl.ClientID%>').options.length = 0;
            document.getElementById('<%=Text_ElementArea.ClientID%>').value = document.getElementById('<%=TextBox_KeyRelated.ClientID%>').value;
            for (i = 0; i < result.length; i++) {
                document.getElementById('<%=ListBox_ObjectFields.ClientID%>').add(new Option(result[i], result[i]));
            }
            jQuery.each(cont_array, function (x, valx) {
                if (x < cont_array.length - 1) {
                    if (valx[16] != '1') {
                        if ((cont_array[x][1] == "TextBox") || (cont_array[x][1] == "TextArea") || (cont_array[x][1] == "CheckBox") || (cont_array[x][1] == "Radio") || (cont_array[x][1] == "ComboBox")) {
                            valMember = cont_array[x][0];
                            DisMember = cont_array[x][29];
                            document.getElementById('<%=ListBox_ObjectCtrl.ClientID%>').add(new Option(DisMember, valMember));
                        }
                    }
                }
            });
            BoolCheck = 0;
            $("#dialog-form").dialog("open");
        }
        function AddObjectCtrl() {
            if (BoolCheck == 1) {
                var src = document.getElementById('<%=ListBox_ObjectCtrl.ClientID%>');
                for (var count = 0; count < src.options.length; count++) {
                    if (src.options[count].selected == true) {
                        var option = src.options[count];
                        document.getElementById('<%=Text_ElementArea.ClientID%>').value = document.getElementById('<%=Text_ElementArea.ClientID%>').value + "$" + option.value;
                    }
                }
                BoolCheck = 0;
            }
        }
        function AddObjectFields() {
            if (BoolCheck == 0) {
                var src = document.getElementById('<%=ListBox_ObjectFields.ClientID%>');
                for (var count = 0; count < src.options.length; count++) {
                    if (src.options[count].selected == true) {
                        var option = src.options[count];
                        var StrSepart = "";
                        if (document.getElementById('Text_ElementArea').value != "") {
                            StrSepart = "^";
                        }
                        document.getElementById('<%=Text_ElementArea.ClientID%>').value = document.getElementById('<%=Text_ElementArea.ClientID%>').value + StrSepart + option.value;
                    }
                }
                BoolCheck = 1;
            }
        }
        function ClearFrm() {
            document.getElementById('<%=Text_ElementArea.ClientID%>').value = "";
            BoolCheck = 0;
        }
        function create_control(DivPos) {
            var ctrl_type;
            var ctrl_value = '';
            var ctrl_Title = '';
            var MaxLen = '';
            div = $("<div>");
            active_counter = 1;
            checkID();
            switch (_nct) {
                case "menu_tb": { ctrl_value = ''; ctrl_Title = 'textbox' + active_counter; div.html('<input class="tb" type="text" name="textbox' + active_counter + '" id="' + _nct + active_counter + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" align="left" maxlength="255" readonly="readonly" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px">'); ctrl_type = 'TextBox'; MaxLen = '255'; break; }
                case "menu_ta": { ctrl_value = ''; ctrl_Title = 'textarea' + active_counter; div.html('<textarea class="ta" name="textarea' + active_counter + '" id="' + _nct + active_counter + '" title="' + ctrl_Title + '" dir="ltr" readonly="readonly" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px"></textarea>'); ctrl_type = 'TextArea'; MaxLen = '0'; break; }
                case "menu_cb": { ctrl_value = ''; ctrl_Title = 'checkbox' + active_counter; div.html('<input class="cb" type="checkbox"  name="checkbox' + active_counter + '" id="' + _nct + active_counter + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" disabled="disabled" tabindex="0" style=""width:16px;hieght:16px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'CheckBox'; MaxLen = '0'; break; }
                case "menu_rb": { ctrl_value = ''; ctrl_Title = 'radio' + active_counter; div.html('<input class="rb"  type="radio" name="radio' + active_counter + '" id="' + _nct + active_counter + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" disabled="disabled" tabindex="0" style=""width:16px;hieght:16px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Radio'; MaxLen = '0'; break; }
                case "menu_so": { ctrl_value = ''; ctrl_Title = 'select' + active_counter; div.html('<select class="ddl" name="select' + active_counter + '" id="' + _nct + active_counter + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" disabled="disabled" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px">'); ctrl_type = 'ComboBox'; MaxLen = '0'; break; }
                case "menu_lb": { ctrl_value = 'Label'; ctrl_Title = 'label' + active_counter; div.html('<input class="lbl"  type="text" name="label' + active_counter + '" id="' + _nct + active_counter + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" align="left" maxlength="255" readonly="readonly" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px">'); ctrl_type = 'Label'; MaxLen = '255'; break; }
                case "menu_ln": { ctrl_value = ''; ctrl_Title = 'line' + active_counter; div.html('<div class="ln" name="line' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;"></div>'); ctrl_type = 'Line'; MaxLen = '0'; break; }
                case "menu_im": { ctrl_value = ''; ctrl_Title = 'Image' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="im" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Image'; MaxLen = '0'; break; }
                case "menu_sc": { ctrl_value = ''; ctrl_Title = 'Search' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="sc" src="../../Common/Images/filefind.png" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Srch'; MaxLen = '0'; break; }
                case "menu_zm": { ctrl_value = ''; ctrl_Title = 'FileZoom' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="zm" src="../../Common/Images/fileZoom.png" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Zoom'; MaxLen = '0'; break; }
                case "menu_lk": { ctrl_value = ''; ctrl_Title = 'FileLink' + active_counter; div.html('<img alt="' + ctrl_value + '"  class="lk" src="../../Common/Images/fileLink.png" name="Image' + active_counter + '" id="' + _nct + active_counter + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Link'; MaxLen = '0'; break; }
            }

            div.attr("id", 'cont_' + _nct + active_counter);
            div.css({ "position": "absolute", "text-align": "left", "left": DivPos.left + "px", "top": DivPos.top + "px" });
            $("#WorkSpace").prepend(div);
            add_action(_nct + active_counter);
            zero_pos = $("#WorkSpace").offset();
            AddControlsArray(_nct + active_counter, ctrl_type, '#000000', '#FFFFFF', ctrl_value, ctrl_Title, MaxLen, DivPos.top - zero_pos.top, DivPos.left - zero_pos.left);
            document.getElementById("ColorPicker_FColor").style.background = '#000000';
            document.getElementById("ColorPicker_BColor").style.background = '#FFFFFF';
            activate_control(_nct + active_counter);
            ApplyChanges();
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
        function AddControlsArray(id, type, fc, bc, Value, Title, MaxLen, top, left) {
            var Width = $("#" + id).width();
            var height = $("#" + id).height();

            cont_array[cont_array.length - 1] = new Array(30)
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
            cont_array[cont_array.length - 1][11] = '';
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
            cont_array[cont_array.length - 1][28] = false;
            cont_array[cont_array.length - 1][29] = id;

            cont_array.length = cont_array.length + 1;

            div = $("<div>");
            var code = 'cont_' + id;
            div.bind('click', function () { activate_control(code.substring(5)); });
            $("#" + code.substring(5, 12)).prepend(div);
        }
        function ApplyChanges() {
            zero_pos = $("#WorkSpace").offset();
            if (_active_control_id != "none" && (_active_control_id != "")) {
                jQuery.each(cont_array, function (i, val) {
                    if (i < cont_array.length - 1) {
                        if (val[0] == (_active_control_id) && val[16] != '1') {
                            cont_array[i][1] = document.getElementById('<%=TextBox_Type.ClientID%>').value;
                            cont_array[i][2] = document.getElementById("ColorPicker_FColor").value;
                            cont_array[i][3] = document.getElementById("ColorPicker_BColor").value;
                            cont_array[i][4] = document.getElementById('<%=TextBox_Top.ClientID%>').value;
                            cont_array[i][5] = document.getElementById('<%=TextBox_Left.ClientID%>').value;
                            cont_array[i][6] = document.getElementById('<%=TextBox_Width.ClientID%>').value;
                            cont_array[i][7] = document.getElementById('<%=TextBox_Height.ClientID%>').value;
                            cont_array[i][8] = document.getElementById('<%=TextBox_TabIndex.ClientID%>').value;
                            cont_array[i][9] = document.getElementById('<%=FileUpload_Imgsrc.ClientID%>').value;
                            cont_array[i][10] = document.getElementById('<%=TextBox_MaxLen.ClientID%>').value;
                            cont_array[i][11] = document.getElementById('<%=TextBox_Tooltip.ClientID%>').value;
                            cont_array[i][12] = document.getElementById('<%=DropDownList_Align.ClientID%>').value;
                            cont_array[i][13] = document.getElementById('<%=DropDownList_Dir.ClientID%>').value;
                            cont_array[i][14] = document.getElementById('<%=TextBox_DValue.ClientID%>').value;
                            cont_array[i][15] = document.getElementById('<%=DropDownList_LOV.ClientID%>').value;

                            cont_array[i][17] = document.getElementById('<%=TextBox_FontSize.ClientID%>').value;
                            cont_array[i][18] = document.getElementById('<%=CheckBox_IsMainKey.ClientID%>').checked;
                            cont_array[i][19] = document.getElementById('<%=CheckBox_Required.ClientID%>').checked;
                            cont_array[i][20] = document.getElementById('<%=DropDownList_SearchSrc.ClientID%>').value;
                            cont_array[i][21] = document.getElementById("Select_KeyCode").value;
                            cont_array[i][22] = document.getElementById('<%=TextBox_RadioGroups.ClientID%>').value;

                            cont_array[i][23] = document.getElementById('<%=DropDownList_KeyTable.ClientID%>').value;
                            if (cont_array[i][1] == 'TextBox') {
                                cont_array[i][24] = document.getElementById('<%=TextBox_SValidation.ClientID%>').value;
                            }
                            else {
                                cont_array[i][24] = document.getElementById('<%=TextBox_KeyRelated.ClientID%>').value;
                            }                          
                            cont_array[i][25] = document.getElementById('<%=DropDownList_CtrlFormat.ClientID%>').value;
                            cont_array[i][26] = document.getElementById('<%=DropDownList_Calendar.ClientID%>').value;
                            if (cont_array[i][1] == 'Zoom') {
                                cont_array[i][27] = document.getElementById('<%=DropDownList_Zooming.ClientID%>').value;
                            }
                            else {
                                cont_array[i][27] = document.getElementById('<%=TextBox_QueryString.ClientID%>').value;
                            }
                            cont_array[i][28] = document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').checked;
                            cont_array[i][29] = document.getElementById('<%=TextBox_FriendlyName.ClientID%>').value;

                            if (cont_array[i][1] == 'TextBox' || cont_array[i][1] == 'Label') {
                                $('#' + _active_control_id)[0].tabindex = cont_array[i][8];
                                $('#' + _active_control_id)[0].maxlength = cont_array[i][10];
                                $('#' + _active_control_id)[0].align = cont_array[i][12];
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
                                $('#' + _active_control_id)[0].tabindex = cont_array[i][8];
                            }
                            else if (cont_array[i][1] == 'Radio' || cont_array[i][1] == 'CheckBox') {
                                $('#' + _active_control_id)[0].tabindex = cont_array[i][8];
                            }
                            else if (cont_array[i][1] == 'ComboBox') {
                                $('#' + _active_control_id)[0].tabindex = cont_array[i][8];
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

                            var select1 = document.getElementById("Select_KeyCode");
                            select1.options.length = 0;
                            jQuery.each(cont_array, function (x, valx) {
                                if (x < cont_array.length - 1) {
                                    if (valx[16] != '1') {
                                        if ((cont_array[x][1] == "TextBox") || (cont_array[x][1] == "TextArea") || (cont_array[x][1] == "CheckBox") || (cont_array[x][1] == "Radio") || (cont_array[x][1] == "ComboBox")) {
                                            valMember = cont_array[x][0];
                                            DisMember = cont_array[x][29];
                                            select1.options.add(new Option(DisMember, valMember));
                                        }
                                    }
                                }
                            });
                            activate_control(_active_control_id);
                        }
                    }
                });
            }
        }
        function ShowProperties(id) {
            document.getElementById('<%=TextBox_ElementlID.ClientID%>').value = '';
            document.getElementById('<%=TextBox_Type.ClientID%>').value = '';
            document.getElementById("ColorPicker_FColor").value = '#000000';
            document.getElementById("ColorPicker_BColor").value = '#FFFFFF';
            document.getElementById('<%=TextBox_Top.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_Left.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_Width.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_Height.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_TabIndex.ClientID%>').value = 0;
            document.getElementById('<%=FileUpload_Imgsrc.ClientID%>').value = '';
            document.getElementById('<%=TextBox_MaxLen.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_Tooltip.ClientID%>').value = '';
            document.getElementById('<%=DropDownList_Align.ClientID%>').value = 'left';
            document.getElementById('<%=DropDownList_Dir.ClientID%>').value = 'ltr';
            document.getElementById('<%=TextBox_DValue.ClientID%>').value = '';
            document.getElementById('<%=DropDownList_LOV.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_FontSize.ClientID%>').value = '10';
            document.getElementById('<%=CheckBox_IsMainKey.ClientID%>').checked = false;
            document.getElementById('<%=CheckBox_Required.ClientID%>').checked = false;
            document.getElementById('<%=DropDownList_SearchSrc.ClientID%>').value = '';
            document.getElementById("Select_KeyCode").value = '';
            document.getElementById('<%=TextBox_RadioGroups.ClientID%>').value = '';
            document.getElementById('<%=DropDownList_KeyTable.ClientID%>').value = '';
            document.getElementById('<%=TextBox_KeyRelated.ClientID%>').value = '';
            document.getElementById('<%=TextBox_SValidation.ClientID%>').value = '';
            document.getElementById('<%=DropDownList_CtrlFormat.ClientID%>').value = 'S';
            document.getElementById('<%=DropDownList_Calendar.ClientID%>').value = 'G';
            document.getElementById('<%=DropDownList_Zooming.ClientID%>').value = 0;
            document.getElementById('<%=TextBox_QueryString.ClientID%>').value = '';
            document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').checked = false;
            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').value = '';

            document.getElementById('<%=TextBox_Type.ClientID%>').disabled = true;
            document.getElementById("ColorPicker_FColor").disabled = true;
            document.getElementById("ColorPicker_BColor").disabled = true;
            document.getElementById('<%=TextBox_Top.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_Left.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_Width.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_Height.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_TabIndex.ClientID%>').disabled = true;
            document.getElementById('<%=FileUpload_Imgsrc.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_MaxLen.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_Tooltip.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_Align.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_Dir.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_DValue.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_LOV.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_FontSize.ClientID%>').disabled = true;
            document.getElementById('<%=CheckBox_IsMainKey.ClientID%>').disabled = true;
            document.getElementById('<%=CheckBox_Required.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_SearchSrc.ClientID%>').disabled = true;
            document.getElementById("Select_KeyCode").disabled = true;
            document.getElementById('<%=TextBox_RadioGroups.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_KeyTable.ClientID%>').disabled = true;
            document.getElementById('<%=Button_KeyRelaed.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_CtrlFormat.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_Calendar.ClientID%>').disabled = true;
            document.getElementById('<%=DropDownList_Zooming.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_QueryString.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_SValidation.ClientID%>').disabled = true;
            document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').disabled = true;
            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = true;

            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[0] == (id) && val[16] != '1') {
                        document.getElementById('<%=TextBox_ElementlID.ClientID%>').value = id;
                        document.getElementById('<%=TextBox_Type.ClientID%>').value = cont_array[i][1];
                        document.getElementById("ColorPicker_FColor").value = cont_array[i][2];
                        document.getElementById("ColorPicker_BColor").value = cont_array[i][3];
                        document.getElementById('<%=TextBox_Top.ClientID%>').value = cont_array[i][4];
                        document.getElementById('<%=TextBox_Left.ClientID%>').value = cont_array[i][5];
                        document.getElementById('<%=TextBox_Width.ClientID%>').value = cont_array[i][6];
                        document.getElementById('<%=TextBox_Height.ClientID%>').value = cont_array[i][7];
                        document.getElementById('<%=TextBox_TabIndex.ClientID%>').value = cont_array[i][8];
                        document.getElementById('<%=FileUpload_Imgsrc.ClientID%>').value = cont_array[i][9];
                        document.getElementById('<%=TextBox_MaxLen.ClientID%>').value = cont_array[i][10];
                        document.getElementById('<%=TextBox_Tooltip.ClientID%>').value = cont_array[i][11];
                        document.getElementById('<%=DropDownList_Align.ClientID%>').value = cont_array[i][12];
                        document.getElementById('<%=DropDownList_Dir.ClientID%>').value = cont_array[i][13];
                        document.getElementById('<%=TextBox_DValue.ClientID%>').value = cont_array[i][14];
                        document.getElementById('<%=DropDownList_LOV.ClientID%>').value = cont_array[i][15];
                        document.getElementById('<%=TextBox_FontSize.ClientID%>').value = cont_array[i][17];
                        document.getElementById('<%=CheckBox_IsMainKey.ClientID%>').checked = cont_array[i][18];
                        document.getElementById('<%=CheckBox_Required.ClientID%>').checked = cont_array[i][19];
                        document.getElementById('<%=DropDownList_SearchSrc.ClientID%>').value = cont_array[i][20];
                        document.getElementById("Select_KeyCode").value = cont_array[i][21];
                        document.getElementById('<%=TextBox_RadioGroups.ClientID%>').value = cont_array[i][22];
                        document.getElementById('<%=DropDownList_KeyTable.ClientID%>').value = cont_array[i][23];
                                              
                        document.getElementById('<%=DropDownList_CtrlFormat.ClientID%>').value = cont_array[i][25];
                        document.getElementById('<%=DropDownList_Calendar.ClientID%>').value = cont_array[i][26];
                                               
                        document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').checked = cont_array[i][28];
                        document.getElementById('<%=TextBox_FriendlyName.ClientID%>').value = cont_array[i][29];

                        if (cont_array[i][1] == 'TextBox') {
                            document.getElementById('<%=TextBox_KeyRelated.ClientID%>').value = '';
                            document.getElementById('<%=TextBox_SValidation.ClientID%>').value = cont_array[i][24];
                            document.getElementById('<%=TextBox_TabIndex.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_MaxLen.ClientID%>').disabled = false;
                            document.getElementById('<%=DropDownList_Align.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_DValue.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FontSize.ClientID%>').disabled = false;
                            document.getElementById('<%=DropDownList_CtrlFormat.ClientID%>').disabled = false;
                            document.getElementById('<%=DropDownList_Calendar.ClientID%>').disabled = false;
                            document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_SValidation.ClientID%>').disabled = false;

                            var hasmKey = 0;
                            var MainKeyID = "";
                            jQuery.each(cont_array, function (x, val) {
                                if (x < cont_array.length - 1) {
                                    if (cont_array[x][18] == true) {
                                        hasmKey = 1;
                                        MainKeyID = cont_array[x][0];
                                    }
                                }
                            });
                            if (MainKeyID == cont_array[i][0]) {
                                document.getElementById('<%=CheckBox_IsMainKey.ClientID%>').disabled = false;
                            }
                            else {
                                document.getElementById('<%=CheckBox_IsMainKey.ClientID%>').disabled = hasmKey == 0 ? false : true;
                            }
                            document.getElementById('<%=CheckBox_Required.ClientID%>').disabled = false;
                            document.getElementById("ColorPicker_FColor").disabled = false;
                            document.getElementById("ColorPicker_BColor").disabled = false;
                            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = false;
                        }
                        if (cont_array[i][1] == 'Label') {
                            document.getElementById('<%=TextBox_TabIndex.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_MaxLen.ClientID%>').disabled = false;
                            document.getElementById('<%=DropDownList_Align.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_DValue.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FontSize.ClientID%>').disabled = false;
                            document.getElementById("ColorPicker_FColor").disabled = false;
                            document.getElementById("ColorPicker_BColor").disabled = false;
                            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'TextArea') {
                            document.getElementById('<%=TextBox_TabIndex.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_DValue.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FontSize.ClientID%>').disabled = false;
                            document.getElementById('<%=CheckBox_Required.ClientID%>').disabled = false;
                            document.getElementById("ColorPicker_FColor").disabled = false;
                            document.getElementById("ColorPicker_BColor").disabled = false;
                            document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'Radio') {
                            document.getElementById('<%=TextBox_TabIndex.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_RadioGroups.ClientID%>').disabled = false;
                            document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'CheckBox') {
                            document.getElementById('<%=TextBox_TabIndex.ClientID%>').disabled = false;
                            document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = false;
                            document.getElementById('<%=CheckBox_Required.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_DValue.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'ComboBox') {
                            document.getElementById('<%=TextBox_TabIndex.ClientID%>').disabled = false;
                            document.getElementById('<%=DropDownList_LOV.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FontSize.ClientID%>').disabled = false;
                            document.getElementById('<%=CheckBox_Required.ClientID%>').disabled = false;
                            document.getElementById("ColorPicker_FColor").disabled = false;
                            document.getElementById("ColorPicker_BColor").disabled = false;
                            document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'Image') {
                            document.getElementById('<%=FileUpload_Imgsrc.ClientID%>').disabled = false;
                            document.getElementById('<%=DropDownList_Align.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_FriendlyName.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'Line') {
                            document.getElementById('<%=DropDownList_Align.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'Srch') {
                            document.getElementById('<%=TextBox_KeyRelated.ClientID%>').value = cont_array[i][24];
                            document.getElementById('<%=TextBox_SValidation.ClientID%>').value = '';
                            document.getElementById('<%=DropDownList_SearchSrc.ClientID%>').disabled = false;
                            document.getElementById("Select_KeyCode").disabled = false;
                            document.getElementById('<%=DropDownList_KeyTable.ClientID%>').disabled = false;
                            document.getElementById('<%=Button_KeyRelaed.ClientID%>').disabled = false;
                        }
                        else if (cont_array[i][1] == 'Zoom') {
                            document.getElementById('<%=DropDownList_Zooming.ClientID%>').value = cont_array[i][27];
                            document.getElementById('<%=DropDownList_Zooming.ClientID%>').disabled = false;
                            document.getElementById('<%=TextBox_QueryString.ClientID%>').value = '';
                        }
                        else if (cont_array[i][1] == 'Link') {
                            document.getElementById('<%=TextBox_QueryString.ClientID%>').value = cont_array[i][27];
                            document.getElementById('<%=TextBox_QueryString.ClientID%>').disabled = false;
                            document.getElementById('<%=DropDownList_Zooming.ClientID%>').value = 0;
                        }
                        document.getElementById('<%=TextBox_Top.ClientID%>').disabled = false;
                        document.getElementById('<%=TextBox_Left.ClientID%>').disabled = false;
                        document.getElementById('<%=TextBox_Width.ClientID%>').disabled = false;
                        document.getElementById('<%=TextBox_Height.ClientID%>').disabled = false;
                        document.getElementById('<%=DropDownList_Dir.ClientID%>').disabled = false;
                        document.getElementById('<%=TextBox_Tooltip.ClientID%>').disabled = false;
                    }
                }
            });
        }
        function add_action(id) { $("#cont_" + id).click(function (e) { activate_control(id) }); }
        function activate_control(id) {
            var x = $("#" + id).width();
            var y = $("#" + id).height();
            deslect_old(_active_control_id);
            $("#cont_" + id).css({ "width": (x) + "px", "height": (y) + "px" });
            ShowProperties(id);
            create_handler(id);
            create_delete(id);
            dragable(id);
            resizable(id);
            _active_control_id = id;
        }
        function deslect_old(id) {
            $("#handle_" + id).click(function (e) { });
            $('img[id^="idel_"]').remove();
            $('img[id^="imov_"]').remove();
        }
        function create_handler(id) {
            var x = $("#" + id).width();
            img = $("<img>");
            img.attr("id", 'imov_' + id);
            img.attr("src", '../../Common/Images/DocumentWF/move.png');
            img.attr("name", 'imov_' + id);
            img.addClass(" handle bar imov");
            img.css({ "left": x + 6 + "px" });
            $("#cont_" + id).prepend(img);
        }
        function create_delete(id) {
            var x = $("#" + id).width();
            img = $("<img>");
            img.attr("id", 'idel_' + id);
            img.attr("src", '../../Common/Images/DocumentWF/delete.png');
            img.attr("name", 'idel_' + id);
            img.bind('click', function () { del(id); });
            img.addClass("imov");
            img.css({ "left": x + 26 + "px" });
            $("#cont_" + id).prepend(img);
        }
        function del(id) {
            $("#cont_" + id).remove();
            $("#m_cont_" + id).remove();
            $("#idel_" + id).remove();
            $("#imov_" + id).remove();
            $("#" + id).remove();
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[0] == (id)) {
                        cont_array[i][16] = '1';
                    }
                }
            });
            ShowProperties(id);
        }
        function resizable(id) {
            $("#" + id).resizable();
            $("#cont_" + id).bind('resizestop', function (event) {
                $("#idel_" + id).remove();
                $("#imov_" + id).remove();
                create_handler(id);
                create_delete(id);
                mouse_move(id);
            });
        }

        function dragable(id) {
            $("#cont_" + id).bind('dragstart', function (event) { if (!$(event.target).is('.handle')) return false; });
            $("#cont_" + id).bind('drag', function (event) {
                $(event.dragProxy).css({ top: event.offsetY, left: event.offsetX });
                $("#idel_" + id).remove();
                $("#imov_" + id).remove();
            });
            $("#cont_" + id).bind('dragend', function (event) {
                $(this).animate({ top: event.offsetY, left: event.offsetX, opacity: 1 });
                create_handler(id);
                create_delete(id);
                mouse_move(id);
            });
        }
        function mouse_move(id) {
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (val[0] == (id) && val[16] != '1') {
                        pos = $("#cont_" + id).offset();
                        zero_pos = $("#WorkSpace").offset();

                        var Width = $("#" + id).width();
                        var height = $("#" + id).height();
                        var top = pos.top - zero_pos.top
                        var left = pos.left - zero_pos.left
                        cont_array[i][4] = top;
                        cont_array[i][5] = left;
                        cont_array[i][6] = Width;
                        cont_array[i][7] = height;
                    }
                }
            });
            ShowProperties(id);
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
                CustomFormsWs.LoadControls(DocumentID, BuildCntrols);
            }
        })
        function BuildCntrols(result) {
            zero_pos = $("#WorkSpace").offset();
            if (result == "") return;
            CtrlArrays = result.split("|");

            var select1 = document.getElementById("Select_KeyCode");
            select1.options.length = 0;
            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(",");
                if ((PropArrays[1] == "TextBox") || (PropArrays[1] == "TextArea") || (PropArrays[1] == "CheckBox") || (PropArrays[1] == "Radio") || (PropArrays[1] == "ComboBox")) {
                    valMember = PropArrays[0];
                    DisMember = PropArrays[30];
                    select1.options.add(new Option(DisMember, valMember));
                }
            }

            for (i = 0; i < CtrlArrays.length; i++) {
                PropArrays = CtrlArrays[i].split(",");
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
                switch (_nct) {
                    case "menu_tb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<input class="tb" type="text" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" align="left" maxlength="255" readonly="readonly" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px">'); ctrl_type = 'TextBox'; MaxLen = '255'; break; }
                    case "menu_ta": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<textarea class="ta" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" dir="ltr" readonly="readonly" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px"></textarea>'); ctrl_type = 'TextArea'; MaxLen = '0'; break; }
                    case "menu_cb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<input class="cb" type="checkbox"  name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" disabled="disabled" tabindex="0" style="width:16px;hieght:16px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'CheckBox'; MaxLen = '0'; break; }
                    case "menu_rb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<input class="rb"  type="radio" name="' + PropArrays[22] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" disabled="disabled" tabindex="0" style="width:16px;hieght:16px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Radio'; MaxLen = '0'; break; }
                    case "menu_so": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<select class="ddl" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" disabled="disabled" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px">'); ctrl_type = 'ComboBox'; MaxLen = '0'; break; }
                    case "menu_lb": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<input class="lbl"  type="text" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" title="' + ctrl_Title + '" value="' + ctrl_value + '" dir="ltr" align="left" maxlength="255" readonly="readonly" tabindex="0" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;font-size: 10px">'); ctrl_type = 'Label'; MaxLen = '255'; break; }
                    case "menu_ln": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<div class="ln" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;"></div>'); ctrl_type = 'Line'; MaxLen = '0'; break; }
                    case "menu_im": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="im" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Image'; MaxLen = '0'; break; }
                    case "menu_sc": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="sc" src="../../Common/Images/filefind.png" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Srch'; MaxLen = '0'; break; }
                    case "menu_zm": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="zm" src="../../Common/Images/fileZoom.png" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Zoom'; MaxLen = '0'; break; }
                    case "menu_lk": { ctrl_value = PropArrays[14]; ctrl_Title = PropArrays[11]; div.html('<img alt="' + ctrl_value + '"  class="lk" src="../../Common/Images/fileLink.png" name="' + PropArrays[0] + '" id="' + PropArrays[0] + '" dir="ltr" align="left" style="width:22px;hieght:22px;font-family: Tahoma;vertical-align:top;border: 1px solid #CCCCCC;">'); ctrl_type = 'Link'; MaxLen = '0'; break; }
                }
                div.attr("id", 'cont_' + PropArrays[0]);
                div.css({ "position": "absolute", "text-align": "left", "left": PropArrays[5] + zero_pos.left + "px", "top": PropArrays[4] + zero_pos.top + "px" });
                $("#WorkSpace").prepend(div);
                add_action(PropArrays[0]);
                AddControlsArray(PropArrays[0], ctrl_type, '#000000', '#FFFFFF', ctrl_value, ctrl_Title, MaxLen, PropArrays[4], PropArrays[5]);
                activate_control(PropArrays[0]);

                document.getElementById('<%=TextBox_ElementlID.ClientID%>').value = PropArrays[0];
                document.getElementById('<%=TextBox_Type.ClientID%>').value = PropArrays[1];
                document.getElementById("ColorPicker_FColor").value = PropArrays[2];
                document.getElementById("ColorPicker_BColor").value = PropArrays[3];
                document.getElementById('<%=TextBox_Top.ClientID%>').value = PropArrays[4];
                document.getElementById('<%=TextBox_Left.ClientID%>').value = PropArrays[5];
                document.getElementById('<%=TextBox_Width.ClientID%>').value = PropArrays[6];
                document.getElementById('<%=TextBox_Height.ClientID%>').value = PropArrays[7];
                document.getElementById('<%=TextBox_TabIndex.ClientID%>').value = PropArrays[8];
                document.getElementById('<%=FileUpload_Imgsrc.ClientID%>').value = PropArrays[9];
                document.getElementById('<%=TextBox_MaxLen.ClientID%>').value = PropArrays[10];
                document.getElementById('<%=TextBox_Tooltip.ClientID%>').value = PropArrays[11];
                document.getElementById('<%=DropDownList_Align.ClientID%>').value = PropArrays[12];
                document.getElementById('<%=DropDownList_Dir.ClientID%>').value = PropArrays[13];
                document.getElementById('<%=TextBox_DValue.ClientID%>').value = PropArrays[14];
                document.getElementById('<%=DropDownList_LOV.ClientID%>').value = PropArrays[15];
                document.getElementById('<%=TextBox_FontSize.ClientID%>').value = PropArrays[17];
                document.getElementById('<%=CheckBox_IsMainKey.ClientID%>').checked = PropArrays[18] == "True" ? true : false;
                document.getElementById('<%=CheckBox_Required.ClientID%>').checked = PropArrays[19] == "True" ? true : false;
                document.getElementById('<%=DropDownList_SearchSrc.ClientID%>').value = PropArrays[20];
                document.getElementById("Select_KeyCode").value = PropArrays[21];
                document.getElementById('<%=TextBox_RadioGroups.ClientID%>').value = PropArrays[22];
                document.getElementById('<%=DropDownList_KeyTable.ClientID%>').value = PropArrays[24];
                document.getElementById('<%=TextBox_KeyRelated.ClientID%>').value = PropArrays[25];
                document.getElementById('<%=TextBox_SValidation.ClientID%>').value = PropArrays[25];
                document.getElementById('<%=DropDownList_CtrlFormat.ClientID%>').value = PropArrays[26];
                document.getElementById('<%=DropDownList_Calendar.ClientID%>').value = PropArrays[27];
                document.getElementById('<%=DropDownList_Zooming.ClientID%>').value = PropArrays[28];
                document.getElementById('<%=TextBox_QueryString.ClientID%>').value = PropArrays[28];
                document.getElementById('<%=CheckBox_IsDisabled.ClientID%>').checked = PropArrays[29] == "True" ? true : false;
                document.getElementById('<%=TextBox_FriendlyName.ClientID%>').value = PropArrays[30];
                ApplyChanges();
            }
        }
        function SaveCntrls() {
            var hasmKey = 0;
            jQuery.each(cont_array, function (i, val) {
                if (i < cont_array.length - 1) {
                    if (cont_array[i][18] == true) {
                        hasmKey = 1;
                    }
                }
            });
            if (hasmKey == 0) {
                jFailled("<%=Resources.Message.NoMainKey%>", "<%=Resources.MessageSetting.FailledMessage%>", "<%=Resources.MessageSetting.Close%>");
                return;
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
                CustomFormsWs.SaveControls(cont_array, DocumentID, afterSaveControls);
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
    </script>
    <div id="dialog-form">
        <table id="Table1" runat="server" style="width: 100%; height: 90%; text-align: left;
            vertical-align: top">
            <tr>
                <td style="width: 25%; height: 90%; text-align: left; vertical-align: top">
                    <asp:ListBox ID="ListBox_ObjectCtrl" runat="server" Width="100%" Height="100%" onclick="AddObjectCtrl(); return false;">
                    </asp:ListBox>
                </td>
                <td style="width: 2%; height: 90%; text-align: left; vertical-align: top">
                </td>
                <td style="width: 46%; height: 90%; text-align: left; vertical-align: top">
                    <asp:TextBox ID="Text_ElementArea" runat="server" Height="100%" Width="100%" TextMode="MultiLine"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td style="width: 2%; height: 90%; text-align: left; vertical-align: top">
                </td>
                <td style="width: 25%; height: 90%; text-align: left; vertical-align: top">
                    <asp:ListBox ID="ListBox_ObjectFields" runat="server" Width="100%" Height="100%"
                        onclick="AddObjectFields(); return false;"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td style="width: 25%; height: 10%; text-align: left; vertical-align: top" colspan="5">
                    <asp:LinkButton ID="LinkButton_Clr" runat="server" SkinID="LinkButton_DefaultBold"
                        Text="Reset" OnClientClick="ClearFrm(); return false;"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <table align="center" style="border: thin solid #CCCCCC;">
        <tr>
            <td style="height: 30px" colspan="2">
                <table id="TBHEADER" runat="server" style="width: 100%; height: 30px; text-align: left;
                    vertical-align: top">
                    <tr>
                        <td style="width: 10px; height: 30px; text-align: left; vertical-align: top">
                        </td>
                        <td style="width: 24px; height: 30px; text-align: left; vertical-align: top">
                            <asp:ImageButton ID="ImageButton_Close" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/Door.png"
                                align="middle" ToolTip="Close" OnClientClick="window.close(); return false" meta:resourcekey="ImageButton1Resource1" />
                        </td>
                        <td style="width: 24px; height: 30px; text-align: left; vertical-align: top">
                            <asp:ImageButton ID="ImageButton_Save" runat="server" Width="24px" ImageAlign="Middle"
                                Height="24px" CausesValidation="False" ImageUrl="~/Common/Images/DocumentWF/CustomFormsView/86.png"
                                align="middle" ToolTip="Save" OnClientClick="SaveCntrls(); return false" meta:resourcekey="ImageButton2Resource1" />
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
                                <div id="TextBox" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_TextBox" runat="server" ImageUrl="../../Common/Images/DocumentWF/ToolBox/TBox.png"
                                                    meta:resourcekey="Image_TextBoxResource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label1" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label1Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label2" runat="server" Text="TextBox" SkinID="Label_DefaultBold" meta:resourcekey="Label2Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="TextArea" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_TextArea" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/TArea.png"
                                                    meta:resourcekey="Image_TextAreaResource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label3" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label3Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label4" runat="server" Text="TextArea" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label4Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Selector" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_Selector" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Drop.png"
                                                    meta:resourcekey="Image_SelectorResource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label5" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label5Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label6" runat="server" Text="ComboBox" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label6Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Label" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_Label" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Lbl.png"
                                                    meta:resourcekey="Image_LabelResource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label7" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label7Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label8" runat="server" Text="Label" SkinID="Label_DefaultBold" meta:resourcekey="Label8Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Check" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_Check" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Chk.png"
                                                    meta:resourcekey="Image_CheckResource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label9" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label9Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label10" runat="server" Text="CheckBox" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label10Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Radio" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_Radio" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Rdio.png"
                                                    meta:resourcekey="Image_RadioResource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label11" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label11Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label12" runat="server" Text="RadioButton" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label12Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Line" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image_Line" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Line.png"
                                                    meta:resourcekey="Image_LineResource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label13" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label13Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label14" runat="server" Text="Line Separator" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label14Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Img" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image7" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Img.png"
                                                    meta:resourcekey="Image7Resource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label15" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label15Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label16" runat="server" Text="Image" SkinID="Label_DefaultBold" meta:resourcekey="Label16Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="srch" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Srch.png"
                                                    meta:resourcekey="Image1Resource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label49" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label49Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label50" runat="server" Text="Search" SkinID="Label_DefaultBold" meta:resourcekey="Label50Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Zoom" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Zoom.png"
                                                    meta:resourcekey="Image1Resource1" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label34" runat="server" Text="|" SkinID="Label_DefaultBold" meta:resourcekey="Label49Resource1"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label36" runat="server" Text="Zooming" SkinID="Label_DefaultBold"
                                                    meta:resourcekey="Label36Resource1"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Link" style="position: static; width: 300px; height: 30px; background-color: transparent;
                                    border: 0px solid #777; padding: 1px; float: left; vertical-align: middle; text-align: center">
                                    <table align="center" style="width: 300px; height: 20px">
                                        <tr>
                                            <td style="width: 80px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Common/Images/DocumentWF/ToolBox/Link.png" />
                                            </td>
                                            <td style="width: 20px; height: 20px; vertical-align: middle; text-align: center">
                                                <asp:Label ID="Label38" runat="server" Text="|" SkinID="Label_DefaultBold"></asp:Label>
                                            </td>
                                            <td style="width: 200px; height: 20px; vertical-align: middle; text-align: left">
                                                <asp:Label ID="Label40" runat="server" Text="Link" SkinID="Label_DefaultBold"></asp:Label>
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
                                            <asp:Label ID="Label18" runat="server" Text="(ID)" SkinID="Label_DefaultBold" meta:resourcekey="Label18Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_ElementlID" runat="server" SkinID="TextBox_SmalltNormalC"
                                                meta:resourcekey="TextBox_ElementlIDResource1" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label21" runat="server" Text="Type" SkinID="Label_DefaultBold" meta:resourcekey="Label21Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Type" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="false"
                                                TabIndex="1" meta:resourcekey="TextBox_TypeResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label20" runat="server" Text="Friendly Name" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label20Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_FriendlyName" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="false" TabIndex="1" meta:resourcekey="TextBox_TypeResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label25" runat="server" Text="ForeColor" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label25Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <input class="color" type="text" id="ColorPicker_FColor" tabindex="2" disabled="disabled"
                                                style="height: 16px; width: 100px" />
                                            <asp:Button ID="Button1" runat="server" Height="16px" Width="20px" OnClientClick="ApplyChanges(); return false;"
                                                meta:resourcekey="Button1Resource1" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label27" runat="server" Text="BackColor" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label27Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <input class="color" type="text" id="ColorPicker_BColor" tabindex="3" disabled="disabled"
                                                style="height: 16px; width: 100px" />
                                            <asp:Button ID="Button2" runat="server" Height="16px" Width="20px" OnClientClick="ApplyChanges(); return false;"
                                                meta:resourcekey="Button2Resource1" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label53" runat="server" Text="Font-Size" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label53Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_FontSize" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="4" onchange="ApplyChanges();" meta:resourcekey="TextBox_FontSizeResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label55" runat="server" Text="Is Main Key" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label55Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:CheckBox ID="CheckBox_IsMainKey" runat="server" onclick="ApplyChanges();" meta:resourcekey="CheckBox_IsMainKeyResource1" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label57" runat="server" Text="Is Required" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label57Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:CheckBox ID="CheckBox_Required" runat="server" onclick="ApplyChanges();" meta:resourcekey="CheckBox_RequiredResource1" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label32" runat="server" Text="Is Disabled" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label32Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:CheckBox ID="CheckBox_IsDisabled" runat="server" onclick="ApplyChanges();" meta:resourcekey="CheckBox_RequiredResource1" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label19" runat="server" Text="Location-Top" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label19Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Top" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="4" onchange="ApplyChanges();" meta:resourcekey="TextBox_TopResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label23" runat="server" Text="Location-Left" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label23Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Left" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="5" onchange="ApplyChanges();" meta:resourcekey="TextBox_LeftResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label29" runat="server" Text="Size-Width" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label29Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Width" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="6" onchange="ApplyChanges();" meta:resourcekey="TextBox_WidthResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label31" runat="server" Text="Size-Height" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label31Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Height" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="7" onchange="ApplyChanges();" meta:resourcekey="TextBox_HeightResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label43" runat="server" Text="TabIndex" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label43Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_TabIndex" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="8" onchange="ApplyChanges();" meta:resourcekey="TextBox_TabIndexResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label33" runat="server" Text="Image-Src" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label33Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:FileUpload ID="FileUpload_Imgsrc" runat="server" Width="130px" Enabled="False"
                                                TabIndex="9" onchange="ApplyChanges();" meta:resourcekey="FileUpload_ImgsrcResource1" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label45" runat="server" Text="MaxLength" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label45Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_MaxLen" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="10" onchange="ApplyChanges();" meta:resourcekey="TextBox_MaxLenResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label35" runat="server" Text="Title" SkinID="Label_DefaultBold" meta:resourcekey="Label35Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_Tooltip" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="11" onchange="ApplyChanges();" meta:resourcekey="TextBox_TooltipResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label41" runat="server" Text="Align" SkinID="Label_DefaultBold" meta:resourcekey="Label41Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_Align" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="12" onchange="ApplyChanges();" meta:resourcekey="DropDownList_AlignResource1">
                                                <asp:ListItem Text="bottom" Value="bottom"></asp:ListItem>
                                                <asp:ListItem Text="left" Value="left"></asp:ListItem>
                                                <asp:ListItem Text="middle" Value="middle"></asp:ListItem>
                                                <asp:ListItem Text="right" Value="right"></asp:ListItem>
                                                <asp:ListItem Text="top" Value="top"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label47" runat="server" Text="Dir" SkinID="Label_DefaultBold" meta:resourcekey="Label47Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_Dir" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="13" onchange="ApplyChanges();" meta:resourcekey="DropDownList_DirResource1">
                                                <asp:ListItem Text="ltr" Value="ltr"></asp:ListItem>
                                                <asp:ListItem Text="rtl" Value="rtl"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label37" runat="server" Text="Value" SkinID="Label_DefaultBold" meta:resourcekey="Label37Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_DValue" runat="server" SkinID="TextBox_SmalltNormalC" Enabled="False"
                                                TabIndex="14" onchange="ApplyChanges();" meta:resourcekey="TextBox_DValueResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label26" runat="server" Text="Input Format" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label26Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_CtrlFormat" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="13" onchange="ApplyChanges();" meta:resourcekey="DropDownList_DirResource1">
                                                <asp:ListItem Text="String" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Numiric 0" Value="N0"></asp:ListItem>
                                                <asp:ListItem Text="Numiric 1" Value="N1"></asp:ListItem>
                                                <asp:ListItem Text="Numiric 2" Value="N2"></asp:ListItem>
                                                <asp:ListItem Text="Numiric 3" Value="N3"></asp:ListItem>
                                                <asp:ListItem Text="Numiric 4" Value="N4"></asp:ListItem>
                                                <asp:ListItem Text="Date" Value="D"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label28" runat="server" Text="Date Calendar" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label28Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_Calendar" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="13" onchange="ApplyChanges();" meta:resourcekey="DropDownList_DirResource1">
                                                <asp:ListItem Text="Gregorian" Value="G"></asp:ListItem>
                                                <asp:ListItem Text="Hijri" Value="H"></asp:ListItem>
                                                <asp:ListItem Text="Both" Value="B"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label39" runat="server" Text="List DataSource" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label39Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_LOV" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="15" onchange="ApplyChanges();" meta:resourcekey="DropDownList_LOVResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label30" runat="server" Text="Target Form" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label30Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_Zooming" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="15" onchange="ApplyChanges();" meta:resourcekey="DropDownList_LOVResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label51" runat="server" Text="SearchSrc" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label51Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_SearchSrc" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="16" onchange="ApplyChanges();" meta:resourcekey="DropDownList_SearchSrcResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label59" runat="server" Text="Key Code" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label59Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <select id="Select_KeyCode" style="width: 120px; height: 20px" onchange="ApplyChanges();">
                                                <option></option>
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label22" runat="server" Text="Key Table" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label22Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:DropDownList ID="DropDownList_KeyTable" runat="server" SkinID="DropDownList_SmalltNormal"
                                                Enabled="False" TabIndex="15" onchange="ApplyChanges();" meta:resourcekey="DropDownList_LOVResource1">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label24" runat="server" Text="Key Related" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label24Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_KeyRelated" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="14" onchange="ApplyChanges();" meta:resourcekey="TextBox_RadioGroupsResource1"></asp:TextBox>
                                            <asp:LinkButton ID="Button_KeyRelaed" runat="server" SkinID="LinkButton_DefaultBold"
                                                Text="(....)" OnClientClick="ShowDialog(); return false;"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label17" runat="server" Text="Radio Group" SkinID="Label_DefaultBold"
                                                meta:resourcekey="Label17Resource1"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_RadioGroups" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="14" onchange="ApplyChanges();" meta:resourcekey="TextBox_RadioGroupsResource1"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label42" runat="server" Text="QueryString" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_QueryString" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="14" onchange="ApplyChanges();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                                                <table align="left" style="width: 300px; height: 20px">
                                    <tr>
                                        <td style="width: 100px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:Label ID="Label44" runat="server" Text="SpecialValidation" SkinID="Label_DefaultBold"></asp:Label>
                                        </td>
                                        <td style="width: 190px; height: 20px; vertical-align: middle; text-align: left">
                                            <asp:TextBox ID="TextBox_SValidation" runat="server" SkinID="TextBox_SmalltNormalC"
                                                Enabled="False" TabIndex="14" onchange="ApplyChanges();"></asp:TextBox>
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
    <script type="text/javascript">
        var inputTags = document.getElementsByTagName('input');
        for (var n = inputTags.length; n--; ) {
            if (inputTags[n].className && inputTags[n].className.indexOf('color') > -1) {
                inputTags[n].onclick = function (e) { e = e || window.event; colorPicker(e); colorPicker.cP.style.zIndex = 1; }
            }
        }
        ApplyChanges();
    </script>
    </form>
</body>
</html>
