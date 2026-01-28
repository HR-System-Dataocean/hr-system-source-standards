<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmModalSearchScreen.aspx.vb"
    Inherits="Interfaces_frmModalSearchScreen" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbar" Namespace="Infragistics.WebUI.UltraWebToolbar" Assembly="Infragistics35.WebUI.UltraWebToolbar.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtxt" Namespace="Infragistics.WebUI.WebDataInput" Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register TagPrefix="igtbl" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
    <script src="../../Common/Script/JQuery/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>

</head>
<body style="height: 100%; width: 100%; margin: 0; padding: 0;">
    <form id="form1" runat="server">
        <script type="text/javascript" language="javascript">
            $(function () {
                var icons = {
                    header: "",
                    activeHeader: ""
                };
                $("#accordion").accordion({ icons: icons });
                $("#accordion").accordion("option", "icons", "");
            });
            function MainSearchLoc_btn_Click(oButton, oEvent) {
                MainSearchLoc_Start();
            }
            function MainSearchLoc_Start() {
                var textAll = window.document.getElementById("txtSearchAll");
                var Value = $('#<%=name.ClientID%>').html();
                var realValue = $('#<%=realname.ClientID%>').html();
                var Arr = Value.split("|");
                var realArr = realValue.split("|");
                var Final_Value = "";
                var FinalTest = "";
                for (i = 0; i < Arr.length; i++) {
                    var str = Arr[i].substring(0, 3);
                    switch (str) {
                        case ("WV_"):
                            {
                                var Control = window.document.getElementById(Arr[i]);
                                if (textAll.value.length > 0) {
                                    Final_Value += " or  IsNull(" + realArr[i] + ",'') like '%" + textAll.value + "%'";
                                }
                                else {
                                    if (Control.value.length > 0) {
                                        Final_Value += " And " + realArr[i] + " like '%" + Control.value + "%'";
                                    }
                                }
                                break;
                            }
                        case ("WN_"):
                            {
                                var Control = window.document.getElementById(Arr[i]);
                                if (Control.value.length > 0)
                                    if (realArr[i].indexOf("Select") > 0)
                                        Final_Value += " And " + realArr[i] + "= '" + Control.value + "')";
                                    else
                                        Final_Value += " And " + realArr[i] + "=" + Control.value;
                                break;
                            }
                        case ("WD_"):
                            {
                                var Control = igdrp_getComboById(Arr[i]);
                                if (Control.getValue() != undefined)
                                    Final_Value += " And " + realArr[i] + "='" + Control.getText() + "'";
                                break;
                            }
                        case ("WB_"):
                            {
                                var Control = window.document.getElementById(Arr[i]);
                                if (Control.value.length > 0)
                                    Final_Value += " And " + realArr[i] + "=" + Control.value;
                                break;
                            }
                    }
                }
                var Target_File = window.document.getElementById("value");
                Target_File.value = Final_Value.substring(5);
                if (Target_File.value == "") {
                    Target_File.value = "0";
                }
            }
            function MainSearchLoc_DblClickHandler(gridName, cellId) {
                var row = igtbl_getRowById(cellId);
                var cell = row.getCell(0).getValue();
                parent.CloseIt(cell);
            }
            function FilterGrid() {
                var gdv = document.getElementById('GridView1');
                if (gdv != null) {
                    if (gdv.rows.length > 0) {
                        for (i = 1; i < gdv.rows.length; i++) {
                            //var Char = String.fromCharCode(event.keyCode);
                            var textAll = window.document.getElementById("txtSearchAll").value.toUpperCase();
                            gdv.rows[i].runtimeStyle.display = 'block';
                            var IsHide = true;
                            for (x = 0; x < gdv.rows[i].cells.length; x++) {
                                if (IsHide == false) {
                                    break;
                                }
                                var cell = gdv.rows[i].cells[x].innerHTML.toUpperCase();
                                if (cell.indexOf(textAll) >= 0) {
                                    IsHide = false;
                                }
                                else {
                                    IsHide = true;
                                }
                            }
                            if (IsHide != true) {
                                gdv.rows[i].runtimeStyle.display = 'block';
                            }
                            else {
                                gdv.rows[i].runtimeStyle.display = 'none';
                            }
                        }
                    }
                }
            }
            var oldgridSelectedColor;
            function setMouseOverColor(element) {
                oldgridSelectedColor = element.style.backgroundColor;
                element.style.cursor = 'hand';
                element.style.backgroundColor = '#93A3B0';
            }
            function setMouseOutColor(element) {
                element.style.backgroundColor = oldgridSelectedColor;
                element.style.cursor = 'cursor';
            }
            function setGridValue(val) {
             
                parent.CloseIt(val);
            }
        </script>
        <div style="display: none">
            <asp:Label ID="realname" runat="server" Style="z-index: 100; left: 610px; position: absolute; top: 128px"
                Width="99px" ForeColor="White" TabIndex="-1" meta:resourcekey="realnameResource1"></asp:Label>
            <asp:TextBox ID="value" runat="server" Style="z-index: 101; left: 612px; position: absolute; top: 155px"
                Width="91px" BorderStyle="None" ForeColor="White" TabIndex="-1" meta:resourcekey="valueResource1"></asp:TextBox>
            <asp:Label ID="TargetControl" runat="server" Style="z-index: 102; left: 613px; position: absolute; top: 184px"
                Width="99px" ForeColor="White" TabIndex="-1" meta:resourcekey="TargetControlResource1"></asp:Label>
            <asp:Label ID="lblMainHeader" runat="server" Font-Size="Small" Height="27px" Style="z-index: 101; left: 6px; position: absolute; top: 7px"
                Width="711px" CssClass="SearchHeadColors"
                meta:resourcekey="lblMainHeaderResource1" BorderColor="#8080FF" BorderStyle="Solid"
                BorderWidth="1px" ForeColor="Black"></asp:Label>
            <asp:Label ID="name" runat="server" Style="z-index: 103; left: 609px; position: absolute; top: 101px"
                Width="99px" ForeColor="White" TabIndex="-1"></asp:Label>
            <asp:HiddenField ID="txtRankofCodeCell" runat="server" />
        </div>
        <div class="Div_MasterContainer" runat="server" id="DIV">
            <div id="accordion">
                <h3>
                    <asp:Label ID="Label1" runat="server" Text="Quick Search" SkinID="Label_DefaultBold" meta:resourcekey="QuickResource1"></asp:Label></h3>
                <div style="height: 150px; vertical-align: bottom">
                    <asp:TextBox ID="txtSearchAll" runat="server" SkinID="TextBox_LargeNormalC" onkeyup="FilterGrid();"></asp:TextBox>
                    <asp:CheckBox ID="chkShowCancel" Text="Show Deleted Files On Search" meta:resourcekey="chkShowCancelResource1" runat="server" />
                </div>
                <h3>
                    <asp:Label ID="Label2" runat="server" Text="Advanced Search" SkinID="Label_DefaultBold" meta:resourcekey="AdvancedResource1"></asp:Label></h3>
                <div>
                    <asp:Panel ID="pnlCriterias" runat="server" Height="150" Width="100%" BorderStyle="None">
                    </asp:Panel>
                </div>
            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <igtxt:WebImageButton ID="btnSearch" runat="server" UseBrowserDefaults="False" meta:resourcekey="btnSearchResource1"
                        Width="0px" Height="0px">
                        <ClientSideEvents Click="MainSearchLoc_btn_Click" />
                    </igtxt:WebImageButton>
                    <div style="overflow: auto; width: 687px; height: 190px">
                        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" Width="687px"
                            EnableModelValidation="true">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
