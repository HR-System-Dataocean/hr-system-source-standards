<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.Web.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.Web.UI.LayoutControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Common/Script/JQuery/CalcSS3.css" rel="stylesheet" type="text/css" />
    <link href="Common/Script/JQuery/reveal.css" rel="stylesheet" type="text/css" />
    <script src="Common/Script/JQuery/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="Common/Script/JQuery/jquery.reveal.js" type="text/javascript"></script>
    <script src="Common/Script/JQuery/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="Common/Script/JQuery/animate.css" rel="stylesheet" type="text/css" />
    <script src="Common/Script/JQuery/jquery.noty.packaged.min.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="form1" runat="server" defaultbutton="Button1">
    <div style="display: none">
        <asp:Button ID="Button1" runat="server" Text="" OnClientClick="return false;" />
    </div>
    <script type="text/javascript">
        function keepSessionAlive() {
            $.post("ping.html");
        }
        $(function () { window.setInterval("keepSessionAlive()", 60000); });
        function getReportTypeByCode(code, cb) {
            try {
                $.ajax({
                    url: 'Common/WebServices/ReportsWs.asmx/GetReportType',
                    type: 'POST',
                    data: JSON.stringify({ Code: code }),
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (res) { cb(res && res.d ? res.d : ''); },
                    error: function () { cb(''); }
                });
            } catch (e) { cb(''); }
        }
        function GetValue(Expression, Find) {
            var StrString;
            var IntLocation;
            var DblLenght;
            var StrRightPart;
            var StrFinalResult;
            var IntNextSeparator;
            StrString = Find
            IntLocation = Expression.indexOf(StrString)
            DblLenght = StrString.length
            if (IntLocation < 0) {
                return ""
            }
            StrRightPart = Expression.substring(IntLocation + DblLenght + 1)
            IntNextSeparator = StrRightPart.indexOf(';')
            if (IntNextSeparator > 0) {
                StrRightPart = StrRightPart.substring(0, IntNextSeparator)
            }
            StrFinalResult = StrRightPart
            return StrFinalResult
        }
        function OnTreeClick(evt) {
            debugger 
            var src = window.event != window.undefined ? window.event.srcElement : evt.target;
            var nodeClick = src.tagName.toLowerCase() == "a";
            
            var ref = src.href
            var count = 0;
            for (var i = 0; i < ref.length; i++) {
                if (i.innerText == '/') {
                    count = count + 1;
                };

            }
            if (nodeClick) {
                debugger
                var nodeText = src.innerText || src.innerHTML;
                var nodeValue = GetNodeValue(src);
                debugger
                if (nodeValue != null) {
                    var startvalue = nodeValue.substring(0, 2)
                    if (startvalue == 'r=') {
                        var reportCode = nodeValue.substring(2);
                        getReportTypeByCode(reportCode, function (rt) {
                            var t = (rt || '').toUpperCase();
                            if (t === 'STI') {
                                CallStiReport(reportCode, nodeText);
                            } else {
                                ShowReportScreen(reportCode, nodeText);
                            }
                        });
                        return false;
                    }
                    else {
                        var RelatedForm = GetValue(nodeValue, 'RelatedForm');
                        var Height = GetValue(nodeValue, 'Height');
                        var Width = GetValue(nodeValue, 'Width');
                        var OperationName = GetValue(nodeValue, 'OperationName');
                        var paymentID = GetValue(nodeValue, 'PaymentTypeID');
                        var Header = nodeText;
                        var LinkTarget = GetValue(nodeValue, 'LinkTarget');
                        var LinkUrl = GetValue(nodeValue, 'LinkUrl');
                        var FrmID = GetValue(nodeValue, 'FrmID');
                        var MainID = GetValue(nodeValue, 'MainID');
                        var flg = 0;
                        if (MainID != "") {
                            flg = 1;
                        }
                        if (RelatedForm != "") {
                            ShowScreen(RelatedForm, Height, Width, OperationName, paymentID, flg, LinkTarget, LinkUrl, FrmID, MainID, Header);
                            return false;
                        }
                    }
                }
            }
        }
        function ShowScreen(formName, hight, width, OperationName, PaymentID, flg, LinkTarget, LinkUrl, FrmID, MainID, Header) {
            if (flg == 0) {
                addTab("Pages/HR/" + formName + "?PaymentTypeID=" + PaymentID + "&OperationName=" + OperationName + "&ID=" + FrmID, Header);
            }
            else if (flg == 1) {
                if (LinkTarget == "_Blank") {
                    window.showModalDialog(LinkUrl, "", "dialogWidth:800px;dialogHeight:600px;center: Yes;resizable: No;");
                }
                else if (LinkTarget == "_Self") {
                    var win = window.open("Interfaces/" + formName + "?PaymentTypeID=" + PaymentID + "&OperationName=" + OperationName, formName.substring(0, formName.length - 5), "height=" + hight + ",width=" + width + ",top=160,left=240,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=1");
                    win.focus();
                }
                else if (LinkTarget == "_Self0") {
                    var win = window.open(LinkUrl, formName.substring(0, formName.length - 5), "height=" + hight + ",width=" + width + ",top=160,left=240,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=1");
                    win.focus();
                }
                else {
                    window.location = LinkUrl;
                }
            }
        }

        function ShowSelfServiceNotificationScreen() {
            debugger
            
            window.showModalDialog("Pages/HR/frmAnnualVacationsNotification.aspx", "", "dialogWidth:800px;dialogHeight:600px;center: Yes;resizable: No;");
            

            //if (flg == 0) {
            //    addTab("Pages/HR/" + formName + "?PaymentTypeID=" + PaymentID + "&OperationName=" + OperationName + "&ID=" + FrmID, Header);
            //}
            //else if (flg == 1) ShowSelfServiceNotificationScreen
            //    if (LinkTarget == "_Blank") {
            //        window.showModalDialog(LinkUrl, "", "dialogWidth:800px;dialogHeight:600px;center: Yes;resizable: No;");
            //    }
            //    else if (LinkTarget == "_Self") {
            //        var win = window.open("Interfaces/" + formName + "?PaymentTypeID=" + PaymentID + "&OperationName=" + OperationName, formName.substring(0, formName.length - 5), "height=" + hight + ",width=" + width + ",top=160,left=240,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=1");
            //        win.focus();
            //    }
            //    else if (LinkTarget == "_Self0") {
            //        var win = window.open(LinkUrl, formName.substring(0, formName.length - 5), "height=" + hight + ",width=" + width + ",top=160,left=240,resizable=0,menubar=0,toolbar=0,location=0,directories=0,scrollbars=0,status=0,center=1");
            //        win.focus();
            //    }
            //    else {
            //        window.location = LinkUrl;
            //    }
            //}
        }
        function GetNodeValue(node) {
            var nodeValue = "";

            // Extract path from href (after the first comma, trimming two chars at the end)
            var nodePath = node.href.substring(node.href.indexOf(",") + 2, node.href.length - 2);

            // Split path by backslash (Windows-style path)
            var nodeValues = nodePath.split("\\");

            if (nodeValues.length > 1) {
                nodeValue = nodeValues[nodeValues.length - 1];
            } else {
                nodeValue = nodeValues[0].substr(1);
            }

            //Mousa Fix MainID string if needed 4 chrome error in frmEmployeesSelector
            if (nodeValue.includes("MainID=%2")) {
                nodeValue = nodeValue.replace("MainID=%2", "MainID=");
            }

            return nodeValue;
        }

        function addTab(Url, name) {
            var webTab = $find("<%=WebTab1.ClientID%>");
            var CuTab = webTab.addTab(name, Url, true);
        }
        function ShowReportScreen(ReportCode, Header) {
            addTab("Interfaces/" + "frmReportsViewerCriterias.aspx?Code=" + ReportCode, Header);
        }
        function CallStiReport(ReportCode, Header) {
            addTab("Interfaces/" + "frmReportsViewerCriteriasSti.aspx?Code=" + ReportCode, Header);
        }
        function ShowReportProjectReportArb() {
            var ReportCode = "Projects";
            var Header = "المشاريع";
            addTab("Interfaces/" + "frmReportsViewerCriterias.aspx?Code=" + ReportCode, Header);
        }
        function ShowReportProjectReportEng() {
            var ReportCode = "Projects";
            var Header = "Projects";
            addTab("Interfaces/" + "frmReportsViewerCriterias.aspx?Code=" + ReportCode, Header);
        }
        function ShowReportProjectReportEng2() {
            var ReportCode = "تنبيهات الخدمة الذاتية";
            var Header = "تنبيهات الخدمة الذاتية";
            addTab("Pages/HR/frmAnnualVacationsNotification.aspx", ReportCode, Header);
        }
        function ShowReportProjectReportEng3() {
            var ReportCode = "Self Service Notifications";
            var Header = "Self Service Notifications";
            addTab("Pages/HR/frmAnnualVacationsNotification.aspx", ReportCode, Header);
        }
        function ShowAppraisalNotifications1() {
            var ReportCode = "تنبيهات التقييمات";
            var Header = "تنبيهات التقييمات ";
            addTab("Pages/HR/FrmappraislaNotifications.aspx", ReportCode, Header);
        }
        function ShowAppraisalNotifications2() {
            var ReportCode = "Appraisal Notifications";
            var Header = "Appraisal Notifications";
            addTab("Pages/HR/FrmappraislaNotifications.aspx", ReportCode, Header);
        }
        function OpenTargetTab(Idx) {
            OfficeWebUI.Ribbon.ToggleOnBackstage(Idx); return false;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="myModal_Calc" class="reveal-modal">
        <div class="calc-main">
            <div class="calc-display">
                <span>0</span>
                <div class="calc-rad">
                    Rad</div>
                <div class="calc-hold">
                </div>
                <div class="calc-buttons">
                    <div class="calc-info">
                        ?</div>
                    <div class="calc-smaller">
                        &gt;</div>
                    <div class="calc-ln">
                        .</div>
                </div>
            </div>
            <div class="calc-left">
                <div>
                    <div>
                        2nd</div>
                </div>
                <div>
                    <div>
                        (</div>
                </div>
                <div>
                    <div>
                        )</div>
                </div>
                <div>
                    <div>
                        %</div>
                </div>
                <div>
                    <div>
                        1/x</div>
                </div>
                <div>
                    <div>
                        x<sup>2</sup></div>
                </div>
                <div>
                    <div>
                        x<sup>3</sup></div>
                </div>
                <div>
                    <div>
                        y<sup>x</sup></div>
                </div>
                <div>
                    <div>
                        x!</div>
                </div>
                <div>
                    <div>
                        &radic;</div>
                </div>
                <div>
                    <div class="calc-radxy">
                        <sup>x</sup><em>&radic;</em><span>y</span>
                    </div>
                </div>
                <div>
                    <div>
                        log</div>
                </div>
                <div>
                    <div>
                        sin</div>
                </div>
                <div>
                    <div>
                        cos</div>
                </div>
                <div>
                    <div>
                        tan</div>
                </div>
                <div>
                    <div>
                        ln</div>
                </div>
                <div>
                    <div>
                        sinh</div>
                </div>
                <div>
                    <div>
                        cosh</div>
                </div>
                <div>
                    <div>
                        tanh</div>
                </div>
                <div>
                    <div>
                        e<sup>x</sup></div>
                </div>
                <div>
                    <div>
                        Deg</div>
                </div>
                <div>
                    <div>
                        &pi;</div>
                </div>
                <div>
                    <div>
                        EE</div>
                </div>
                <div>
                    <div>
                        Rand</div>
                </div>
            </div>
            <div class="calc-right">
                <div>
                    <div>
                        mc</div>
                </div>
                <div>
                    <div>
                        m+</div>
                </div>
                <div>
                    <div>
                        m-</div>
                </div>
                <div>
                    <div>
                        mr</div>
                </div>
                <div class="calc-brown">
                    <div>
                        AC</div>
                </div>
                <div class="calc-brown">
                    <div>
                        +/&#8211;</div>
                </div>
                <div class="calc-brown calc-f19">
                    <div>
                        &divide;</div>
                </div>
                <div class="calc-brown calc-f21">
                    <div>
                        &times;</div>
                </div>
                <div class="calc-black">
                    <div>
                        7</div>
                </div>
                <div class="calc-black">
                    <div>
                        8</div>
                </div>
                <div class="calc-black">
                    <div>
                        9</div>
                </div>
                <div class="calc-brown calc-f18">
                    <div>
                        &#8211;</div>
                </div>
                <div class="calc-black">
                    <div>
                        4</div>
                </div>
                <div class="calc-black">
                    <div>
                        5</div>
                </div>
                <div class="calc-black">
                    <div>
                        6</div>
                </div>
                <div class="calc-brown calc-f18">
                    <div>
                        +</div>
                </div>
                <div class="calc-black">
                    <div>
                        1</div>
                </div>
                <div class="calc-black">
                    <div>
                        2</div>
                </div>
                <div class="calc-black">
                    <div>
                        3</div>
                </div>
                <div class="calc-blank">
                    <textarea></textarea></div>
                <div class="calc-orange calc-eq calc-f17">
                    <div>
                        <div class="calc-down">
                            =</div>
                    </div>
                </div>
                <div class="calc-black calc-zero">
                    <div>
                        <span>0</span>
                    </div>
                </div>
                <div class="calc-black calc-f21">
                    <div>
                        .</div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal_Cale" class="reveal-modal">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table align="center" cellspacing="0" cellpadding="0" class="MainContainerMaster">
                    <tr>
                        <td style="width: 20%;">
                        </td>
                        <td style="width: 40%;">
                            <igtxt:WebMaskEdit ID="txtGDate" runat="server" InputMask="##/##/####" Width="250"
                                Height="40" Font-Size="20" Font-Bold="True">
                            </igtxt:WebMaskEdit>
                        </td>
                        <td style="width: 5%; text-align: center">
                            <asp:Label ID="lblG" runat="server" meta:resourcekey="lblGResource1" SkinID="Label_CopyRightsBold"
                                Text="G"></asp:Label>
                        </td>
                        <td style="width: 15%;">
                            <asp:Button ID="Button2" runat="server" Text="&gt;&gt;" Width="60" Height="40" Font-Size="20"
                                ForeColor="#666666" />
                        </td>
                        <td style="width: 20%;">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%;">
                        </td>
                        <td style="width: 40%;">
                            <igtxt:WebMaskEdit ID="txtHDate" runat="server" InputMask="##/##/####" Width="250"
                                Height="40" Font-Size="20" Font-Bold="True">
                            </igtxt:WebMaskEdit>
                        </td>
                        <td style="width: 5%; text-align: center">
                            <asp:Label ID="lblH" runat="server" meta:resourcekey="lblHResource1" SkinID="Label_CopyRightsBold"
                                Text="H"></asp:Label>
                        </td>
                        <td style="width: 15%;">
                            <asp:Button ID="Button3" runat="server" Font-Size="20" ForeColor="#666666" Height="40"
                                Text="&gt;&gt;" Width="60" />
                        </td>
                        <td style="width: 20%;">
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script src="Common/Script/JQuery/CalcSS3.js" type="text/javascript"></script>
    <OfficeWebUI:Manager ID="Manager1" runat="server" ChromeUI="True" DirectionMode="LTR"
        IncludeJQuery="false" UITheme="Office2010Silver" CustomCSSFile="">
    </OfficeWebUI:Manager>
    <div style="float: right background: url(/library/img/apptitle.png) top center no-repeat;">
    </div>
    <OfficeWebUI:OfficeRibbon ID="OfficeRibbon1" runat="server" ApplicationMenuDirection="RTL"
        ApplicationMenuType="None" ShowHelpButton="True" ShowToggleButton="True" ApplicationMenuColor="blue"
        ApplicationMenuText="" ExtraText="" HelpButtonClientClick="">
        <ApplicationMenu>
            <BackstagePages>
                <OfficeWebUI:BackstagePage ID="BackstagePage5" runat="server" Text="" UserControl="~/Pages/General/widgets/Main.ascx" />
            </BackstagePages>
        </ApplicationMenu>
        <Contexts>
            <OfficeWebUI:RibbonContext ID="RibbonContext1" runat="server" ContextColor="transparent"
                Text="">
                <Tabs>
                    <OfficeWebUI:RibbonTab ID="RibbonTab101" runat="server" Text="General" ApplicationMenuDirection="RTL">
                        <Groups>
                            <OfficeWebUI:RibbonGroup ID="RibbonGroup3" runat="server" Text="">
                                <Zones>
                                    <OfficeWebUI:GroupZone ID="GroupZone1" runat="server" Text="Zone 1">
                                        <Content>
                                            <OfficeWebUI:LargeItem ID="LargeItem1" runat="server" ImageUrl="Common/Images/HomeTB/Log-Out-icon.png"
                                                NavigateUrl="Pages/General/SignOut.aspx" Text="Signout" />
                                            <OfficeWebUI:LargeItem ID="LargeItem2" runat="server" ImageUrl="Common/Images/HomeTB/Desktop-Folder-icon.png"
                                                Text="Hide<br/>Ribbon" ClientClick="OfficeWebUI.Ribbon.ToggleRibbon(); return false;" />
                                        </Content>
                                    </OfficeWebUI:GroupZone>
                                </Zones>
                            </OfficeWebUI:RibbonGroup>
                            <OfficeWebUI:RibbonGroup ID="RibbonGroup1" runat="server" Text="">
                                <Zones>
                                    <OfficeWebUI:GroupZone ID="GroupZone2" runat="server" Text="Zone 1">
                                        <Content>
                                            <OfficeWebUI:LargeItem ID="LargeItem5" runat="server" ImageUrl="Common/Images/HomeTB/collapse.png"
                                                Text="Collapsed<br/>Layout" ClientClick="OfficeWebUI.Workspace.LPanelShow(); return false;" />
                                            <OfficeWebUI:LargeItem ID="LargeItem6" runat="server" ImageUrl="Common/Images/HomeTB/expand.png"
                                                Text="Expanded<br/>Layout" ClientClick="OfficeWebUI.Workspace.LPanelHide(); return false;" />
                                        </Content>
                                    </OfficeWebUI:GroupZone>
                                </Zones>
                            </OfficeWebUI:RibbonGroup>
                            <OfficeWebUI:RibbonGroup ID="RibbonGroup2" runat="server" Text="Interface Language">
                                <Zones>
                                    <OfficeWebUI:GroupZone ID="GroupZone3" runat="server" Text="Zone 1">
                                        <Content>
                                            <OfficeWebUI:MediumItem ID="MediumItem1" runat="server" ImageUrl="Common/Images/HomeTB/Saudi-Arabia-icon.png"
                                                Text="Arabic Interface" OnClick="MediumItem1_Click" />
                                            <OfficeWebUI:MediumItem ID="MediumItem2" runat="server" ImageUrl="Common/Images/HomeTB/United-States-of-Americ-icon.png"
                                                Text="English Interface" OnClick="MediumItem2_Click" />
                                        </Content>
                                    </OfficeWebUI:GroupZone>
                                </Zones>
                            </OfficeWebUI:RibbonGroup>
                            <OfficeWebUI:RibbonGroup ID="RibbonGroup5" runat="server" Text="Interface Theme">
                                <Zones>
                                    <OfficeWebUI:GroupZone ID="GroupZone4" runat="server" Text="Zone 1">
                                        <Content>
                                            <OfficeWebUI:MediumItem ID="MediumItem3" runat="server" ImageUrl="Common/Images/HomeTB/Places-folder-blue-icon.png"
                                                Text="Blue Theme" OnClick="MediumItem3_Click" />
                                            <OfficeWebUI:MediumItem ID="MediumItem4" runat="server" ImageUrl="Common/Images/HomeTB/Places-folder-black-icon.png"
                                                Text="Silver Theme" OnClick="MediumItem4_Click" />
                                        </Content>
                                    </OfficeWebUI:GroupZone>
                                </Zones>
                            </OfficeWebUI:RibbonGroup>
                            <OfficeWebUI:RibbonGroup ID="RibbonGroup4" runat="server" Text="">
                                <Zones>
                                    <OfficeWebUI:GroupZone ID="GroupZone5" runat="server" Text="Zone 1">
                                        <Content>
                                        </Content>
                                    </OfficeWebUI:GroupZone>
                                </Zones>
                            </OfficeWebUI:RibbonGroup>
                        </Groups>
                    </OfficeWebUI:RibbonTab>
                </Tabs>
            </OfficeWebUI:RibbonContext>
        </Contexts>
    </OfficeWebUI:OfficeRibbon>
    <table align="center" cellspacing="0" cellpadding="0" class="MainContainerMaster">
        <tr>
            <td style="width: 100%;">
                <table>
                    <tr>
                        <td style="min-width: 70px;">
                            <a href="#" data-reveal-id="myModal_Calc">
                                <asp:Label ID="Label2" runat="server" Text="ألة حاسبة" Font-Size="8pt"></asp:Label>
                            </a>
                        </td>
                        <td style="min-width: 10px;">
                        </td>
                        <td style="min-width: 100px;">
                            <a href="#" data-reveal-id="myModal_Cale">
                                <asp:Label ID="Label3" runat="server" Text="محول التواريخ" Font-Size="8pt"></asp:Label>
                            </a>
                        </td>
                        <td style="width: 90%">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Timer ID="Timer1" runat="server" Interval="10800000">
                                    </asp:Timer>
                                    <marquee id="MyMovingText" onmouseover="this.stop()" onmouseout="this.start()" scrolldelay="100"
                                        scrollamount="5" runat="server">
                                                  <asp:Label ID="Label1" runat="server" Text="Label" 
                            SkinID="Label_MarqueeNormal" meta:resourcekey="Label1Resource1"></asp:Label></marquee>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <OfficeWebUI:OfficeWorkspace ID="OfficeWorkspace1" runat="server" ShowStatusBar="False"
                    ShowRightPanel="False" ShowLeftPanel="True" LeftPanelWidth="250px" RightPanelWidth="0px">
                    <Content>
                        <ig:WebTab ID="WebTab1" runat="server" Width="100%" Height="100%" SkinID="Default"
                            BorderStyle="None" meta:resourcekey="WebTab1Resource1">
                            <CloseButton Enabled="True" />
                            <PostBackOptions EnableAjax="True" EnableLoadOnDemand="True" />
                        </ig:WebTab>
                    </Content>
                </OfficeWebUI:OfficeWorkspace>
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            setTimeout(function () {
                $.noty.closeAll();
            }, 9000);
        });
    </script>
</body>
</html>
