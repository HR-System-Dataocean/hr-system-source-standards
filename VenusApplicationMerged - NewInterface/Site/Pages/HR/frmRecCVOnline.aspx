<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRecCVOnline.aspx.vb"
    Inherits="frmRecCVOnline" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<%@ Register Assembly="Infragistics35.WebUI.UltraWebGrid.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebGrid" TagPrefix="igtbl" %>
<%@ Register TagPrefix="igmisc" Namespace="Infragistics.WebUI.Misc" Assembly="Infragistics35.WebUI.Misc.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDateChooser.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~Employees Documents</title>
    <script src="Scripts/App_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_M.js" type="text/javascript"></script>
    <script src="Scripts/App_OtherFields_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_Search_JScript.js" type="text/javascript"></script>
    <script src="Scripts/App_JScript_PayRoll.js" type="text/javascript"></script>
    
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
    </script>

</head>
<body style="height: 100%; margin: 0; padding: 0;" onload='adjustHeight()'>
    <form id="frmRecCVOnline" runat="server">
    <div style="display: none">
    <%--<igmisc:WebAsyncRefreshPanel ID="WebAsyncRefreshPanel1" runat="server">--%>
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1"  Width="99px" meta:resourcekey="nameResource1"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1" TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1" Width="91px" meta:resourcekey="valueResource1"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px" meta:resourcekey="TargetControlResource1"></asp:Label>
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
        </asp:ScriptManager>
          <igsch:WebDateChooser ID="WebDateChooser" runat="server" AllowNull="False" EnableTheming="True"
            Height="18px" meta:resourcekey="WebDateChooser1Resource1" Style="left: 436px;
            position: absolute; top: 29px; z-index: 122; width: 150px;" Value="2008-01-27"
            HideDropDowns="False" ShowDropDown="True">
            <CalendarLayout NextMonthImageUrl="ig_cal_blueN0.gif" PrevMonthImageUrl="ig_cal_blueP0.gif"
                ShowMonthDropDown="False" ShowYearDropDown="False" TitleFormat="Month">
                <CalendarStyle BackColor="#CCDDFF" BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px"
                    Font-Bold="False" Font-Italic="False" Font-Names="Verdana" Font-Overline="False"
                    Font-Size="9pt" Font-Strikeout="False" Font-Underline="False">
                </CalendarStyle>
                <DayHeaderStyle BackColor="#E0EEFF" Font-Bold="True" Font-Size="8pt" ForeColor="#8080A0"
                    Height="1pt">
                    <BorderDetails ColorBottom="LightSteelBlue" StyleBottom="Solid" WidthBottom="1px" />
                </DayHeaderStyle>
                <NextPrevStyle BackgroundImage="ig_cal_blue2.gif" />
                <OtherMonthDayStyle ForeColor="SlateGray" />
                <SelectedDayStyle BackColor="SteelBlue" />
                <TitleStyle BackColor="#CCDDFF" BackgroundImage="ig_cal_blue2.gif" Font-Bold="True"
                    Font-Size="10pt" ForeColor="#505080" Height="18pt" />
                <TodayDayStyle BackColor="#E0EEFF" />
                <FooterStyle BackgroundImage="ig_cal_blue1.gif" Font-Size="8pt" ForeColor="#505080"
                    Height="16pt">
                    <BorderDetails ColorTop="LightSteelBlue" StyleTop="Solid" WidthTop="1px" />
                </FooterStyle>
            </CalendarLayout>
            <ExpandEffects Type="Slide" />
        </igsch:WebDateChooser>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
    
        <table align="center" style="width: 100%;">
            <tr>
                <td class="Details">
                    <igtab:UltraWebTab ID="UltraWebTab1" runat="server" EnableAppStyling="True" SkinID="Default"
                        meta:resourcekey="UltraWebTab1Resource1">
                        <Tabs>
                            <igtab:Tab Text="عام" meta:resourcekey="TabResource1">
                                <ContentTemplate>
                                    
                                
                                        <table style="width: 100%;  vertical-align: top"
                                        cellspacing="0">
                                            <tr>
                                                <td style="height: 18px">
                                                    <table style="width: 100%; height: 18px; vertical-align: top; border-bottom: 1px solid silver">
                                                        <tr>
                                                            <td style="width: 5px">
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <igtxt:WebImageButton ID="btnSave" runat="server" Style="cursor: pointer;"
                                                                    Height="18px" Overflow="NoWordWrap" UseBrowserDefaults="False" 
                                                                    Width="24px" meta:resourcekey="btnSaveResource1">
                                                                    <Alignments TextImage="ImageBottom" />
                                                                    <Appearance>
                                                                        <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/SaveN.png" />
                                                                    </Appearance>
                                                                </igtxt:WebImageButton>
                                                            </td>
                                                            <td style="width: 5px">
                                                                <asp:Label ID="Label_TSP3" runat="server" meta:resourcekey="Label_TSP1Resource1"
                                                                    Text="|"></asp:Label>
                                                            </td>
                                                            <td style="width: 40px; text-align: center;">
                                                                <asp:ImageButton ID="ImageButton_Print" runat="server" CommandArgument="Print" Height="16px" meta:resourcekey="ImageButton_PrintResource1" SkinID="HrPrint_Command" Width="16px" />
                                                            </td>
                                                            <td style="width: 5px">
                                                                &nbsp;</td>
                                                            <td style="width: 40px; text-align: center;">
                                                                &nbsp;</td>
                                                  
                                                            
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            
                                            </table>
                                        <table style="width: 100%; height: 100%;vertical-align: top" cellspacing="0">
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label7" runat="server" Width="95px" Text="Code" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label7Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtCode" runat="server" SkinID="TextBox_SmalltNormalC" 
                                                                meta:resourcekey="txtCodeResource1" AutoPostBack="True" ></asp:TextBox>
                                                        </td>
                                                      
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                <asp:Label ID="lblMSG" runat="server" meta:resourcekey="lblMSGResource1" 
                                                    SkinID="Label_WarningBold" Width="100%"></asp:Label>
                                                
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 11%;">
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="Label9" runat="server" Text="Name" SkinID="Label_DefaultNormal" 
                                                                meta:resourcekey="lblNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="lblFatherName" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Father Name" meta:resourcekey="lblFatherNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="lblGrandName" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Grand Name" meta:resourcekey="lblGrandNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 21%; text-align: center;">
                                                            <asp:Label ID="lblFamilyName" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Family Name" meta:resourcekey="lblFamilyNameResource1"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="Label10" runat="server"
                                                                SkinID="Label_DefaultNormal" Text="English name" Width="95px"
                                                                meta:resourcekey="lblEngNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngFathername" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngFathernameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngGrandName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngGrandNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtEngFamilyName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtEngFamilyNameResource1"></asp:TextBox>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px; vertical-align: top" colspan="3">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 10%;">
                                                            <asp:Label ID="Label11" runat="server"
                                                                SkinID="Label_DefaultNormal" Text="Arabic name" Width="95px"
                                                                meta:resourcekey="lblArbNameResource1"></asp:Label>
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbFatherName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbFatherNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbGrandName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbGrandNameResource1"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 1%;">
                                                            &nbsp;</td>
                                                        <td style="width: 20%;">
                                                            <asp:TextBox ID="txtArbFamilyName" runat="server" SkinID="TextBox_LargeNormalC" 
                                                                meta:resourcekey="txtArbFamilyNameResource1"></asp:TextBox>
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
                                                            <asp:Label ID="Label1" runat="server" Width="95px" Text="E Mail" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label1Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEMail" runat="server"  SkinID="TextBox_LargeNormalC" meta:resourcekey="txtEMailResource1" ></asp:TextBox>
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
                                                            <asp:Label ID="Label12" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Nationality" Width="95px" meta:resourcekey="Label12Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlNationality" runat="server" AutoPostBack="True" 
                                                                SkinID="DropDownList_LargNormal" meta:resourcekey="ddlNationalityResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label2" runat="server" Width="95px" Text="Marital Status" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label2Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" AutoPostBack="True"  SkinID="DropDownList_LargNormal" meta:resourcekey="ddlMaritalStatusResource1"  
                                                                >
                                                                <asp:ListItem Value="S" meta:resourcekey="ListItemResource1">Single</asp:ListItem>
                                                                <asp:ListItem Value="M" meta:resourcekey="ListItemResource2">Married</asp:ListItem>
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
                                                            <asp:Label ID="Label13" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Suit Position" Width="95px" meta:resourcekey="Label13Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="True" 
                                                                SkinID="DropDownList_LargNormal" meta:resourcekey="ddlPositionResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label3" runat="server" Width="95px" Text="Last Job" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label3Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtLastJob" runat="server"  SkinID="TextBox_LargeNormalC" meta:resourcekey="txtLastJobResource1" ></asp:TextBox>
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
                                                            <asp:Label ID="Label14" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="NO Dependancies" Width="95px" meta:resourcekey="Label14Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtNODependancies" runat="server"  SkinID="WebNumericEdit_Default" meta:resourcekey="txtNODependanciesResource1"  >
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label4" runat="server" Width="95px" Text="Driver Lic" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label4Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="HasDriverLic" runat="server" Width="135px" meta:resourcekey="HasDriverLicResource1" />
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
                                                            <asp:Label ID="Label15" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Last Salary" Width="95px" meta:resourcekey="Label15Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtLastSalary" runat="server"  SkinID="WebNumericEdit_Default" meta:resourcekey="txtLastSalaryResource1"  >
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label5" runat="server" Width="95px" Text="Address" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label5Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtAddress" runat="server"  SkinID="TextBox_LargeNormalC" meta:resourcekey="txtAddressResource1" ></asp:TextBox>
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
                                                            <asp:Label ID="Label16" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Expected Salary" Width="95px" meta:resourcekey="Label16Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <igtxt:WebNumericEdit ID="txtExpSalary" runat="server"  SkinID="WebNumericEdit_Default" meta:resourcekey="txtExpSalaryResource1"  >
                                                            </igtxt:WebNumericEdit>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label6" runat="server" Width="95px" Text="IQama No" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label6Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtIQamaNo" runat="server"  SkinID="TextBox_LargeNormalC" meta:resourcekey="txtIQamaNoResource1" ></asp:TextBox>
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
                                                            <asp:Label ID="Label17" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Passport No" Width="95px" meta:resourcekey="Label17Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtPassportNo" runat="server"  SkinID="TextBox_LargeNormalC" meta:resourcekey="txtPassportNoResource1" ></asp:TextBox>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label8" runat="server" Width="95px" Text="Phone No" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label8Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server"  SkinID="TextBox_LargeNormalC" meta:resourcekey="txtPhoneNoResource1" ></asp:TextBox>
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
                                                            <asp:Label ID="Label18" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Mobile No" Width="95px" meta:resourcekey="Label18Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtMobileNo" runat="server"  SkinID="TextBox_LargeNormalC" meta:resourcekey="txtMobileNoResource1" ></asp:TextBox>
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                <table cellspacing="6" 
                                                    style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label19" runat="server" meta:resourcekey="Label_Title3Resource1" 
                                                                SkinID="Label_DefaultBold" Text="Non Saudi / Special Condition"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label20" runat="server" Width="180px" Text="Has Transfeerable Iqama" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label20Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="HasTransIqama" runat="server" meta:resourcekey="HasTransIqamaResource1"  />
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
                                                            <asp:Label ID="Label26" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Has No Objection From Pre Sponsor" Width="200px" meta:resourcekey="Label26Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="HasNOSponsor" runat="server" meta:resourcekey="HasNOSponsorResource1" />
                                                        </td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label25" runat="server" SkinID="Label_DefaultNormal" 
                                                                Text="Special Condition Affect Work" Width="180px" meta:resourcekey="Label25Resource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="HasSConditions" runat="server" meta:resourcekey="HasSConditionsResource1" />
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
                                                            &nbsp;</td>
                                                        <td class="DataArea">
                                                            &nbsp;</td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                <table style="width: 100%; vertical-align: top" cellspacing="0">
                                                    <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label24" runat="server" Width="95px" Text="Special Conditions" 
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="Label24Resource1" 
                                                                ></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtSConditions" runat="server" Height="40px"
                                                                TextMode="MultiLine" Width="100%" BorderColor="#CCCCCC" style="font-family: Tahoma; font-size: 8pt;font-weight: Normal; color:black; vertical-align:middle; text-align:center" BorderStyle="Solid" BorderWidth="1px" meta:resourcekey="txtSConditionsResource1"  ></asp:TextBox>
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
                                                            &nbsp;</td>
                                                        <td class="DataArea">
                                                            &nbsp;</td>
                                                        
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width:5px;">
                                                        </td> 
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                <table cellspacing="6" 
                                                    style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label21" runat="server" meta:resourcekey="Label_Title4Resource1" 
                                                                SkinID="Label_DefaultBold" Text="Please Add References"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" vertical-align: top" colspan="4">

                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgReferences" runat="server" EnableAppStyling="False" 
                                                    Height="100px" meta:resourcekey="uwgReferencesResource1" SkinID="Default" 
                                                    Width="100%">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" 
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" 
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" 
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" 
                                                        Name="uwgForNationality" RowHeightDefault="18px" RowSelectorsDefault="No" 
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" 
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" 
                                                        ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" 
                                                            Font-Size="8.25pt" Height="100px" Width="100%">
                                                        </FrameStyle>
                                                        <ClientSideEvents AfterCellUpdateHandler="AddRow" />
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" 
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" 
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" 
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource1">
                                                            <RowEditTemplate>
                                                                <p align="right">
                                                                    <br />
                                                                </p>
                                                                <br />
                                                                <p align="center">
                                                                </p>
                                                            </RowEditTemplate>
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" 
                                                                    meta:resourcekey="UltraGridColumnResource1" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName" 
                                                                    meta:resourcekey="UltraGridColumnResource2" Width="30%">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Reference English" Title="EngName">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" EditorControlID="WebTextEdit1" 
                                                                    Key="ArbName" meta:resourcekey="UltraGridColumnResource3" Type="Custom" 
                                                                    Width="30%">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Reference Arabic" Title="ArbName">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Phone" Key="Phone" 
                                                                    meta:resourcekey="UltraGridColumnResource4" Width="10%">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Phone" Title="Phone">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Fax" Key="Fax" 
                                                                    meta:resourcekey="UltraGridColumnResource5" Width="10%">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Fax" Title="Fax">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="E_Mail" Key="E_Mail" 
                                                                    meta:resourcekey="UltraGridColumnResource6" Width="20%">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="E_Mail" Title="E_Mail">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" 
                                                                BorderStyle="Ridge">
                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                                                    WidthTop="3px" />
                                                            </RowTemplateStyle>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                <table cellspacing="6" 
                                                    style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label22" runat="server" meta:resourcekey="Label_Title5Resource1" 
                                                                SkinID="Label_DefaultBold" Text="Please Add Language"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" vertical-align: top" colspan="4">

                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgLanguage" runat="server" EnableAppStyling="False" 
                                                    Height="100px" meta:resourcekey="uwgLanguageResource1" SkinID="Default" 
                                                    Width="100%">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" 
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" 
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" 
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" 
                                                        Name="uwgForNationality" RowHeightDefault="18px" RowSelectorsDefault="No" 
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" 
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" 
                                                        ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" 
                                                            Font-Size="8.25pt" Height="100px" Width="100%">
                                                        </FrameStyle>
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" 
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" 
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" 
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                        <ClientSideEvents AfterCellUpdateHandler="uwgLanguage_AfterCellUpdateHandler" 
                                                            AfterEnterEditModeHandler="uwg_AfterEnterEditModeHandler" />
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource2">
                                                            <RowEditTemplate>
                                                                <p align="right">
                                                                    <br />
                                                                </p>
                                                                <br />
                                                                <p align="center">
                                                                </p>
                                                            </RowEditTemplate>
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" 
                                                                    meta:resourcekey="UltraGridColumnResource7" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Language_ID" DataType="System.Int32" 
                                                                    Key="Language_ID" meta:resourcekey="UltraGridColumnResource8" 
                                                                    Type="DropDownList" Width="50%">
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Header Caption="Language" Title="">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="SLevel_ID" DataType="System.Int32" 
                                                                    Key="SLevel_ID" meta:resourcekey="UltraGridColumnResource9" Type="DropDownList" 
                                                                    Width="25%">
                                                                    <Header Caption="Write Level">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="WLevel_ID" DataType="System.Int32" 
                                                                    Key="WLevel_ID" meta:resourcekey="UltraGridColumnResource10" 
                                                                    Type="DropDownList" Width="25%">
                                                                    <Header Caption="Read Level">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" 
                                                                BorderStyle="Ridge">
                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                                                    WidthTop="3px" />
                                                            </RowTemplateStyle>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                <table cellspacing="6" 
                                                    style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label23" runat="server" meta:resourcekey="Label_Title6Resource1" 
                                                                SkinID="Label_DefaultBold" Text="Please Add Educational Degree"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" vertical-align: top" colspan="4">

                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgCertifications" runat="server" 
                                                    EnableAppStyling="False" Height="100px" 
                                                    meta:resourcekey="uwgCertificationsResource1" SkinID="Default" Width="100%">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" 
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" 
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" 
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" 
                                                        Name="uwgForNationality" RowHeightDefault="18px" RowSelectorsDefault="No" 
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" 
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" 
                                                        ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" 
                                                            Font-Size="8.25pt" Height="100px" Width="100%">
                                                        </FrameStyle>
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" 
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" 
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" 
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                        <ClientSideEvents AfterCellUpdateHandler="uwgCertifications_AfterCellUpdateHandler" 
                                                            AfterEnterEditModeHandler="uwg_AfterEnterEditModeHandler" />
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource3">
                                                            <RowEditTemplate>
                                                                <p align="right">
                                                                    <br />
                                                                </p>
                                                                <br />
                                                                <p align="center">
                                                                </p>
                                                            </RowEditTemplate>
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="300px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" 
                                                                    meta:resourcekey="UltraGridColumnResource11" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EDegree_ID" DataType="System.Int32" 
                                                                    Key="EDegree_ID" meta:resourcekey="UltraGridColumnResource12" 
                                                                    Type="DropDownList" Width="40%">
                                                                    <Header Caption="Education Degree">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="GDateFrom" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="" Key="GDateFrom" 
                                                                    meta:resourcekey="UltraGridColumnResource13" Type="Custom" Width="15%">
                                                                    <Header Caption="From Gerg">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HDateFrom" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="dd/MM/yyyy" Key="HDateFrom" 
                                                                    meta:resourcekey="UltraGridColumnResource14" Type="Custom" Width="15%">
                                                                    <Header Caption="From Hijri">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="GDateTo" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="" Key="GDateTo" 
                                                                    meta:resourcekey="UltraGridColumnResource15" Type="Custom" Width="15%">
                                                                    <Header Caption="To Gerg">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HDateTo" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="dd/MM/yyyy" Key="HDateTo" 
                                                                    meta:resourcekey="UltraGridColumnResource16" Type="Custom" Width="15%">
                                                                    <Header Caption="To Hijri">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn AllowUpdate="No" 
                                                                    meta:resourcekey="UltraGridColumnResource17" Width="1px">
                                                                    <Header>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" 
                                                                BorderStyle="Ridge">
                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                                                    WidthTop="3px" />
                                                            </RowTemplateStyle>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                <table cellspacing="6" 
                                                    style="width: 100%; height: 30px; vertical-align: bottom; border-bottom: 1px solid black">
                                                    <tr>
                                                        <td style="vertical-align: bottom">
                                                            <asp:Label ID="Label27" runat="server" meta:resourcekey="Label_Title7Resource1" 
                                                                SkinID="Label_DefaultBold" Text="Please Add Employment History"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 6%; height: 16px; vertical-align: top">
                                            </td>
                                            <td style="width: 47%; height: 16px; vertical-align: top">
                                                
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style=" vertical-align: top" colspan="4">

                                                <igtbl:UltraWebGrid  Browser="UpLevel"   ID="uwgHistory" runat="server" EnableAppStyling="False" 
                                                    Height="100px" meta:resourcekey="uwgHistoryResource1" SkinID="Default" 
                                                    Width="100%">
                                                    <DisplayLayout AllowColSizingDefault="Free" AllowColumnMovingDefault="OnServer" 
                                                        AllowDeleteDefault="Yes" AllowSortingDefault="OnClient" 
                                                        AllowUpdateDefault="Yes" AutoGenerateColumns="False" 
                                                        BorderCollapseDefault="Separate" HeaderClickActionDefault="SortSingle" 
                                                        Name="uwgForNationality" RowHeightDefault="18px" RowSelectorsDefault="No" 
                                                        SelectTypeRowDefault="Extended" StationaryMargins="Header" 
                                                        StationaryMarginsOutlookGroupBy="True" TableLayout="Fixed" Version="4.00" 
                                                        ViewType="OutlookGroupBy">
                                                        <FrameStyle BackColor="Window" BorderColor="InactiveCaption" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Names="Microsoft Sans Serif" 
                                                            Font-Size="8.25pt" Height="100px" Width="100%">
                                                        </FrameStyle>
                                                        <Pager MinimumPagesForDisplay="2">
                                                            <PagerStyle BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                            </PagerStyle>
                                                        </Pager>
                                                        <EditCellStyleDefault BorderStyle="None" BorderWidth="0px">
                                                        </EditCellStyleDefault>
                                                        <FooterStyleDefault BackColor="LightGray" BorderStyle="Solid" BorderWidth="1px">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </FooterStyleDefault>
                                                        <HeaderStyleDefault BackColor="#DFDFDF" BorderStyle="Solid" Font-Names="tahoma" 
                                                            Font-Size="9pt" Height="20px" HorizontalAlign="Center" VerticalAlign="Middle">
                                                            <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                WidthTop="1px" />
                                                        </HeaderStyleDefault>
                                                        <RowStyleDefault BackColor="Window" BorderColor="Silver" BorderStyle="Solid" 
                                                            BorderWidth="1px" Font-Names="tahoma" Font-Size="8pt" Height="18px">
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
                                                            <BoxStyle BackColor="Window" BorderColor="InactiveCaption" BorderStyle="Solid" 
                                                                BorderWidth="1px">
                                                                <BorderDetails ColorLeft="White" ColorTop="White" WidthLeft="1px" 
                                                                    WidthTop="1px" />
                                                            </BoxStyle>
                                                        </AddNewBox>
                                                        <ActivationObject BorderColor="" BorderWidth="">
                                                        </ActivationObject>
                                                        <FilterOptionsDefault>
                                                            <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Height="300px" 
                                                                Width="200px">
                                                                <Padding Left="2px" />
                                                            </FilterDropDownStyle>
                                                            <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                            </FilterHighlightRowStyle>
                                                            <FilterOperandDropDownStyle BackColor="White" BorderColor="Silver" 
                                                                BorderStyle="Solid" BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px">
                                                                <Padding Left="2px" />
                                                            </FilterOperandDropDownStyle>
                                                        </FilterOptionsDefault>
                                                        <ClientSideEvents AfterCellUpdateHandler="uwgHistory_AfterCellUpdateHandler" 
                                                            AfterEnterEditModeHandler="uwg_AfterEnterEditModeHandler" />
                                                    </DisplayLayout>
                                                    <Bands>
                                                        <igtbl:UltraGridBand AllowAdd="Yes" meta:resourcekey="UltraGridBandResource4">
                                                            <RowEditTemplate>
                                                                <p align="right">
                                                                    <br />
                                                                </p>
                                                                <br />
                                                                <p align="center">
                                                                </p>
                                                            </RowEditTemplate>
                                                            <AddNewRow View="NotSet" Visible="NotSet">
                                                            </AddNewRow>
                                                            <FilterOptions AllString="" EmptyString="" NonEmptyString="">
                                                                <FilterDropDownStyle BackColor="White" BorderColor="Silver" BorderStyle="Solid" 
                                                                    BorderWidth="1px" CustomRules="overflow:auto;" 
                                                                    Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="11px" Width="200px">
                                                                    <Padding Left="2px" />
                                                                </FilterDropDownStyle>
                                                                <FilterHighlightRowStyle BackColor="#151C55" ForeColor="White">
                                                                </FilterHighlightRowStyle>
                                                            </FilterOptions>
                                                            <Columns>
                                                                <igtbl:UltraGridColumn BaseColumnName="ID" Hidden="True" Key="ID" 
                                                                    meta:resourcekey="UltraGridColumnResource18" Width="0px">
                                                                    <Header Caption="ID">
                                                                    </Header>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="EngName" Key="EngName" 
                                                                    meta:resourcekey="UltraGridColumnResource19" Width="20%">
                                                                    <Header Caption="English Name">
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="1" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="ArbName" EditorControlID="WebTextEdit1" 
                                                                    Key="ArbName" meta:resourcekey="UltraGridColumnResource20" Type="Custom" 
                                                                    Width="20%">
                                                                    <Header Caption="Arabic Name">
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="2" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="GDateFrom" DataType="System.DateTime" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="dd/MM/yyyy" Key="GDateFrom" 
                                                                    meta:resourcekey="UltraGridColumnResource21" Type="Custom" Width="10%">
                                                                    <Header Caption="From Gerg">
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="3" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HDateFrom" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="dd/MM/yyyy" Key="HDateFrom" 
                                                                    meta:resourcekey="UltraGridColumnResource22" Type="Custom" Width="10%">
                                                                    <Header Caption="From Hijri">
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="4" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="GDateTo" DataType="System.DateTime" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="dd/MM/yyyy" Key="GDateTo" 
                                                                    meta:resourcekey="UltraGridColumnResource23" Type="Custom" Width="10%">
                                                                    <Header Caption="To Gerg">
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="5" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="HDateTo" 
                                                                    EditorControlID="WebDateTimeEdit1" Format="dd/MM/yyyy" Key="HDateTo" 
                                                                    meta:resourcekey="UltraGridColumnResource24" Type="Custom" Width="10%">
                                                                    <Header Caption="To Hijri">
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="6" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Years" Format="" Key="Years" 
                                                                    meta:resourcekey="UltraGridColumnResource25" Width="7%">
                                                                    <Header Caption="Years">
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="7" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                                <igtbl:UltraGridColumn BaseColumnName="Position_ID" EditorControlID="" 
                                                                    Key="Position_ID" meta:resourcekey="UltraGridColumnResource26" 
                                                                    Type="DropDownList" Width="13%">
                                                                    <Header Caption="Position">
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Header>
                                                                    <FooterStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <CellStyle HorizontalAlign="Center">
                                                                    </CellStyle>
                                                                    <Footer>
                                                                        <RowLayoutColumnInfo OriginX="8" />
                                                                    </Footer>
                                                                </igtbl:UltraGridColumn>
                                                            </Columns>
                                                            <RowTemplateStyle BackColor="Transparent" BorderColor="Transparent" 
                                                                BorderStyle="Ridge">
                                                                <BorderDetails WidthBottom="3px" WidthLeft="3px" WidthRight="3px" 
                                                                    WidthTop="3px" />
                                                            </RowTemplateStyle>
                                                        </igtbl:UltraGridBand>
                                                    </Bands>
                                                </igtbl:UltraWebGrid>

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
