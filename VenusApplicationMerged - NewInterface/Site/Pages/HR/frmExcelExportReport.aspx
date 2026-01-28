<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmExcelExportReport.aspx.vb"
    Inherits="frmExcelExportReport" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Assembly="Infragistics35.WebUI.UltraWebTab.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebTab" TagPrefix="igtab" %>
<%@ Register Assembly="Infragistics35.WebUI.WebDataInput.v11.1, Version=11.1.20111.1006, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebDataInput" TagPrefix="igtxt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>* Venus Payroll * ~ System Groups</title>
    <script language="javascript" src="Scripts/App_JScript.js"></script>
    <script language="javascript" src="Scripts/App_JScript_M.js"></script>
    <script language="javascript" src="Scripts/App_JScript_PayRoll.js"></script>
    <script language="javascript" src="Scripts/App_Search_JScript.js"></script>
    <script language="javascript" src="Scripts/App_OtherFields_JScript.js"></script>

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
    <form id="frmExcelExportReport" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
    </div>
    <div class="Div_MasterContainer" runat="server" id="DIV">
        <table align="center" style="width: 100%;">
            <tr>
                <td style="width: 100%; height: 60px; vertical-align: top" colspan="3">
                   
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
                                    <table style="width: 47%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20px"></td>
                                            <td   class="LabelArea">
                                                            <asp:Label ID="lblReport" runat="server" Width="120px" SkinID="Label_DefaultBold"
                                                                Text="التقرير" meta:resourcekey="lblReportResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlReport" runat="server"  SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="DdlReportResource1" TabIndex="3">
                                                            </asp:DropDownList>
                                                        </td>
                                           
                                        </tr>

                                        <%--<tr>
                                            <td style="width: 20px"></td>
                                            <td class="LabelArea" style="height: 16px">
                                                            <asp:Label ID="lblBranch" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblBranchResource1" Text="الفرع"></asp:Label>
                                                        </td>
                                                        <td class="DataArea" style="height: 16px">
                                                            

                                                        
                                                            <asp:DropDownList ID="ddlBranch" runat="server" Width="120px" meta:resourcekey="ddlBranchResource1"
                                                                SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        

                                                        </td>
                                        </tr>--%>
                                        <tr>
                                            <td style="width: 20px"></td>
                                            <td class="LabelArea">
                                                            <asp:Label ID="lblDepartment" runat="server" SkinID="Label_DefaultNormal" Text="القسم"
                                                                Width="90px" meta:resourcekey="lblDepartmentResource1"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" Width="120px" SkinID="DropDownList_LargNormal"
                                                                meta:resourcekey="ddlDepartmentResource1">
                                                            </asp:DropDownList>
                                                        </td>
                                           
                                        </tr>


                                        <tr>
    <td style="width: 20px"></td>
    <td class="LabelArea" style="height: 16px">
                    <asp:Label ID="lblUnit" runat="server" SkinID="Label_DefaultNormal" meta:resourcekey="lblUnitResource1" Text="الفرع"></asp:Label>
                </td>
                <td class="DataArea" style="height: 16px">
                    

                
                    <asp:DropDownList ID="ddlUnit" runat="server" Width="120px" meta:resourcekey="ddlUnitResource1"
                        SkinID="DropDownList_LargNormal" AutoPostBack="True">
                    </asp:DropDownList>
                

                </td>
</tr>
                                        <tr>
                                            <td style="width: 20px"></td>
                                            <td>
                                                       <asp:Label ID="Label_Contract" runat="server" meta:resourcekey="Label_ContractResource1"
                                                                SkinID="Label_DefaultNormal" Text="Contract Type" Width="90px"></asp:Label>
                                                        </td>
                                                     <td>
                                                                        <asp:TextBox ID="TextBox_Contract" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Cont" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>
                                        </tr>
                                         <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblBranch" runat="server" meta:resourcekey="lblBranchResource1" SkinID="Label_DefaultNormal"
                                                                Text="Branch" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlBranche" runat="server" meta:resourcekey="ddlBranchResource1"
                                                                SkinID="DropDownList_LargNormal" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

                                        <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Sponsor" runat="server" meta:resourcekey="Label_SponsorResource1"
                                                                SkinID="Label_DefaultNormal" Text="Sponsor" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="TextBox_Sponsor" runat="server" MaxLength="30" SkinID="TextBox_LargeNormalC"></asp:TextBox>
                                                                    </td>
                                                                    <td style="width: 25px;">
                                                                        <igtxt:WebImageButton ID="WebImageButton_Sponsor" runat="server" Height="18px" AutoSubmit="False"
                                                                            Overflow="NoWordWrap" UseBrowserDefaults="False" Width="24px">
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
                                        <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="Label_Project" runat="server" meta:resourcekey="lblFilterResource1"
                                                                SkinID="Label_DefaultNormal" Text="المشاريع" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="DropDownList_Project" runat="server" SkinID="DropDownList_LargNormal"
                                                                AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                         <tr>
                                                        <td class="SeparArea">
                                                        </td>
                                                        <td class="LabelArea">
                                                            <asp:Label ID="lblNationality" runat="server" meta:resourcekey="lblNationalityResource1" SkinID="Label_DefaultNormal"
                                                                Text="Nationality" Width="90px"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:DropDownList ID="ddlNationality" runat="server" meta:resourcekey="ddlNationalityResource1"
                                                                SkinID="DropDownList_LargNormal">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                     <%--   <tr>
                                            <td style="width: 20px"></td>
                                            <td>
                                                        <asp:Label ID="lblPosition" runat="server" SkinID="Label_DefaultNormal" Text="Position"
                                                            Width="80px" meta:resourcekey="lblPositionResource1"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPosition" runat="server" SkinID="DropDownList_LargNormal"
                                                            meta:resourcekey="ddlPositionResource1">
                                                        </asp:DropDownList>
                                                    </td>
                                        </tr>--%>
                                       <%-- <tr>
                                            <td style="width: 20px"></td>
                                            <td>
                                                        <asp:Label ID="lblNationality" runat="server" Width="80px" SkinID="Label_DefaultNormal"
                                                            Text="Nationality" meta:resourcekey="lblNationalityResource1"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DdlNationality" runat="server" SkinID="DropDownList_LargNormal"
                                                            meta:resourcekey="DdlNationalityResource1">
                                                        </asp:DropDownList>
                                                    </td>
                                        </tr>--%>


                                        <tr>
                                            <td style="width: 20px"></td>
                                            <td class="LabelArea">
                                                            <asp:Label ID="Label1_LogFromDate" runat="server" SkinID="Label_DefaultNormal" Text="من تاريخ"
                                                                Width="120px" meta:resourcekey="Label1_LogFromDateResource1"></asp:Label>
                                                        </td>
                                            <td>
                                                                        <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                                                    </td>
                                        </tr>

                                         <tr>
                                            <td style="width: 20px"></td>
                                             <td class="LabelArea">
                                                            <asp:Label ID="Label_LogToDate" runat="server" meta:resourcekey="Label_LogToDateResource1"
                                                                SkinID="Label_DefaultNormal" Text="الى تاريخ" Width="120px"></asp:Label>
                                                        </td>
                                              <td>
                                                                        <asp:TextBox ID="TxtToDate" runat="server"></asp:TextBox>
                                                                    </td>
                                             </tr>

                                        <tr>
                                            <td style="width: 20px"></td>
                                            <td class="LabelArea">
                                                            <asp:Label ID="label2" Width="70px" runat="server" Text=" الموظف "
                                                                SkinID="Label_DefaultNormal" meta:resourcekey="labelAlternativeUserResource1"></asp:Label>
                                                        </td>
                                                       
                                                          <td>
                                                              <table><tr>
                                                                  <td>
                                                                         <asp:TextBox ID="txtAlternativeUser" Width="130px" runat="server"  MaxLength="15"
                                                                AutoPostBack="True" meta:resourcekey="txtAlternativeResource1">
                                                           </asp:TextBox>
                                                                    </td>
                                                                  <td>
                                                                        <igtxt:WebImageButton ID="btnSearchCode" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                   
                                                       
                                                         </td>
                                                        <td>
                                                         <asp:TextBox ID="TxtAlternativeEmpName" Width="380px" runat="server"  MaxLength="100"
                                                                AutoPostBack="false"  meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
                                                            </td>

                                                                  </tr>
                                                                  <tr style="height: 100%"></tr>
                                                              </table>

                                                          </td>
                                             </tr>

                                        <%--<tr>
                                            <td style="width: 20px"></td>
                                            <td class="LabelArea">
                                                                    <asp:Label ID="lblProject" runat="server" SkinID="Label_DefaultNormal" Text=" المشروع"
                                                                        Width="90px" meta:resourcekey="lblProjectResource1"></asp:Label>
                                                                </td>
                                                                <td class="DataArea">
                                                                    <asp:DropDownList ID="ddlProject" runat="server" SkinID="DropDownList_LargNormal"
                                                                        AutoPostBack="True" meta:resourcekey="ddlProjectResource1">
                                                                    </asp:DropDownList>
                                                                </td>
                                             </tr>--%>
                                     <%--   <tr>
                                            <td style="width: 20px"></td>
                                            <td class="LabelArea">
                                                                                <asp:Label ID="Label3" runat="server" meta:resourcekey="lblShiftResource1" SkinID="Label_DefaultNormal" Text="الوردية" Width="90px"></asp:Label>
                                                                            </td>
                                                                            <td class="DataArea">
                                                                                <asp:DropDownList ID="DdlShift" runat="server" meta:resourcekey="ddl_PrantBranchResource1" SkinID="DropDownList_SmalltNormal">
                                                                                    <asp:ListItem Value="0">حدد الخيار</asp:ListItem>
                                                                                    <asp:ListItem meta:resourcekey="lblFirstShiftResource1" Value="1">الأولى</asp:ListItem>
                                                                                    <asp:ListItem meta:resourcekey="lblSecondShiftResource1" Value="2">الثانية</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                </td>
                                             </tr>--%>
                                        <tr>
                                            <td style="width: 20px"></td>

                                             </tr>
                                        <tr>
                                            <td style="width: 20px"></td>
                                            <td style="width: 120px"></td>
                                            <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <igtxt:WebImageButton ID="btnHR" runat="server" Height="10px" Style="font-family: Tahoma;
                                                    font-size: 8pt; font-weight: bold; color: Black" meta:resourcekey="btnHR"
                                                    Overflow="NoWordWrap" Text=" Update HR " UseBrowserDefaults="False" Width="180px">
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                        <Image Url="~/Common/Images/Pages.png" />
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                            ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                            WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
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
