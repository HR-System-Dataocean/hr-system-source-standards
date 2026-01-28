<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmployeesVacationsAdvanceFix.aspx.vb"
    Inherits="frmEmployeesVacationsAdvanceFix" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
    <form id="frmEmployeesVacationsAdvanceFix" runat="server">
    <div style="display: none">
        <asp:Label ID="name" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="realname" runat="server" ForeColor="White" meta:resourcekey="realnameResource1"
            TabIndex="-1" Width="99px"></asp:Label>
        <asp:TextBox ID="value" runat="server" BorderStyle="None" ForeColor="White" TabIndex="-1"
            Width="91px"></asp:TextBox>
        <asp:Label ID="TargetControl" runat="server" ForeColor="White" TabIndex="-1" Width="99px"></asp:Label>
        <asp:Label ID="lblLage" runat="server" meta:resourcekey="lblLageResource1"></asp:Label>
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
                                    <table style="width: 100%; height: 100%; min-height: 350px; vertical-align: top"
                                        cellspacing="0">
                                        <tr>
                                            <td style="height: 10px" colspan="3">
                                            </td>
                                        </tr>

                                         <tr>
                                                       <td class="SeparArea">

                                                       </td>
                                                              
                                                        <td class="LabelArea" style="min-width: 90px;">
                                                            <asp:Label ID="lblAllEmployee" runat="server" Text="AllEmployee" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblAllEmployeeResource"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:CheckBox ID="ChkAllEmployee" runat="server" AutoPostBack="True" />
                                                                    </td>  
                                                        <td class="LabelArea">              
    
                </td>

                                         </tr>
            
                                           <tr>
                                                       <td class="SeparArea">

                                                       </td>
                                                              
                                                        <td class="LabelArea" style="min-width: 90px;">
                                                            <asp:Label ID="lblCode" runat="server" Text="كود الموظف" SkinID="Label_DefaultNormal"
                                                                meta:resourcekey="lblCodeResource"></asp:Label>
                                                        </td>
                                                        <td class="DataArea">
                                                            <asp:TextBox ID="txtEmployee" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                                AutoPostBack="True" meta:resourcekey="txtEmployeeResource1">
                                                           </asp:TextBox>
                                                        
                                                                        <igtxt:WebImageButton ID="btnSearchCodeEmp" runat="server" Height="18px" AutoSubmit="False"
                                                                            meta:resourcekey="btnSearchCodeResource1" Overflow="NoWordWrap" UseBrowserDefaults="False"
                                                                            Width="24px">
                                                                            <Alignments TextImage="ImageBottom" />
                                                                            <Appearance>
                                                                                <Image Url="./Img/forum_search.gif" />
                                                                            </Appearance>
                                                                        </igtxt:WebImageButton>
                                                                    </td>  
                                                        <td class="LabelArea">              
    <asp:Label ID="LblRemarsk" runat="server" Text="" meta:resourcekey="LblRemarskResource1"></asp:Label>
                </td>

                                         </tr>

                                                                               <tr>
                                                   <td class="SeparArea">

                                                   </td>
                                                          
                                                    <td class="LabelArea" style="min-width: 90px;">
                                                        <asp:Label ID="lblYear" runat="server" Text="السنة" SkinID="Label_DefaultNormal"
                                                            meta:resourcekey="lblYearResource"></asp:Label>
                                                    </td>
                                                    <td class="DataArea">
                                                        <asp:TextBox ID="txtYear" runat="server" SkinID="TextBox_SmalltNormalC" MaxLength="30"
                                                            AutoPostBack="False" meta:resourcekey="txtYearResource1">
                                                       </asp:TextBox>
                                                    
                                                                </td>  
                                                    <td class="LabelArea"> 
            </td>

                                     </tr>
                                                                                 <tr>
                                                  
                                             <td class="SeparArea">

                                             </td>
  
                                                        
                                                 
                                      </tr>
                                         <tr>
                                                  
                                             <td class="SeparArea">

                                             </td>
  
                                                        
                                                         
                                                     <td class="LabelArea" style="min-width: 90px;">
                                                         <asp:Label ID="lblClass" runat="server" Text="الفئة" SkinID="Label_DefaultNormal"
                                                             meta:resourcekey="lblCodeResource"></asp:Label>
                                                     </td>
                                                  
                                              
                                                    <td>
    <asp:DropDownList ID="ddlClass" runat="server" Width="35%" />
                                            
</td>
                       
                                                 <td class="SeparArea">

                                                 </td>
                                      </tr>
                                                                                                                         <tr>
                                                  
                                             <td class="SeparArea">

                                             </td>
  
                                                        
                                                         
                                                    <td class="SeparArea">

</td>
                                                  
        <td class="SeparArea">

</td>
                       
                                                 <td class="SeparArea">

                                                 </td>
                                      </tr>
                                        <tr>
                                            <td class="SeparArea"></td><td class="SeparArea"></td>
                                             
                                            <td>
                                                
                                                <igtxt:WebImageButton ID="btnHR" runat="server" Height="5px" Style="font-family: Tahoma;
                                                    font-size: 8pt; font-weight: Normal; color: Black" meta:resourcekey="btnHR"
                                                    Overflow="NoWordWrap" Text=" Update HR " UseBrowserDefaults="False" Width="180px">
                                                    <Alignments TextImage="TextRightImageLeft" VerticalImage="Middle" />
                                                    <Appearance>
                                                        <Image Url="~/Common/Images/ToolBox/Hr_ToolBox/updateSelfService.png" />
                                                        <InnerBorder ColorBottom="160, 160, 160" ColorLeft="White" ColorRight="160, 160, 160"
                                                            ColorTop="White" StyleBottom="Solid" StyleLeft="Solid" StyleRight="Solid" StyleTop="Solid"
                                                            WidthBottom="1px" WidthLeft="1px" WidthRight="1px" WidthTop="1px" />
                                                    </Appearance>
                                                </igtxt:WebImageButton>
                                            </td>
                                          
                                        </tr>

                                       
                                         <tr>
                                            <td></td>
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
