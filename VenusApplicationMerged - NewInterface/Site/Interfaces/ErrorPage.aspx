<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ErrorPage.aspx.vb" Inherits="Interfaces_ErrorPage" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title><meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" cellpadding="3" cellspacing="5" style="z-index: 102; left: 0px;
            width: 576px; position: absolute; top: 0px; height: 376px" width="576">
            <tr>
                <td id="tableProps2" align="left" style="height: 54px" valign="middle" width="360">
                    <span id="Span1">&nbsp;<span id="Span2">
                        <div designtimedragdrop="123" ms_positioning="GridLayout" style="width: 552px; position: relative;
                            height: 42px">
                            <img align="middle" alt="refresh.gif (82 bytes)" border="0" src="discuss_16.gif"
                                style="z-index: 105; left: 12px; width: 23px; position: absolute; top: 12px;
                                height: 22px" />
                            <div ms_positioning="FlowLayout" style="font-size: medium; z-index: 105; left: 43px;
                                width: 321px; font-family: Tahoma; position: absolute; top: 13px; height: 20px">
                                <span id="errorText">The page cannot be displayed</span></div>
                        </div>
                    </span></span>
                </td>
            </tr>
            <tr>
                <td id="Td1" colspan="2" style="height: 37px" width="400">
                    <p>
                        <font style="font: 8pt/11pt verdana; color: black">An Error Occure While you are processing
                            Please Contact the Administrator For Solving this proplem</font></p>
                </td>
            </tr>
            <tr>
                <td id="tablePropsWidth" colspan="2" width="400">
                    <font id="LID1" style="font: 8pt/11pt verdana; color: black">
                        <hr color="#c0c0c0" noshade="noshade" />
                        <div ms_positioning="GridLayout" style="width: 552px; position: relative; height: 448px">
                            <div ms_positioning="FlowLayout" style="display: inline; font-size: xx-small; z-index: 101;
                                left: 8px; width: 240px; position: absolute; top: 8px; height: 16px">
                                <p id="P1" style="font-weight: bold">
                                    Please try the following:</p>
                            </div>
                            <div ms_positioning="FlowLayout" style="z-index: 102; left: 32px; width: 280px; position: absolute;
                                top: 32px; height: 24px">
                                <p id="LID2">
                                    Click the&nbsp; <a target="_self" xhref="javascript:location.reload()">Refresh</a>
                                    button, or try again later.</p>
                            </div>
                            <div dir="ltr" ms_positioning="FlowLayout" style="z-index: 103; left: 32px; width: 99px;
                                position: absolute; top: 96px; height: 24px">
                                <p id="P2">
                                    Error Message :</p>
                            </div>
                            <div ms_positioning="FlowLayout" style="z-index: 104; left: 32px; width: 280px; position: absolute;
                                top: 64px; height: 24px">
                                <p id="P3">
                                    Click the&nbsp; <a target="_self" xhref="javascript:location.reload()">Send</a>
                                    E-Mail , to Notify The provide</p>
                            </div>
                            <div ms_positioning="FlowLayout" style="z-index: 105; left: 32px; width: 98px; position: absolute;
                                top: 128px; height: 24px">
                                <p id="P4">
                                    Page Name &nbsp; &nbsp; :</p>
                            </div>
                            <div dir="ltr" ms_positioning="FlowLayout" style="z-index: 106; left: 32px; width: 99px;
                                position: absolute; top: 160px; height: 24px">
                                <p id="P5">
                                    Module Name &nbsp;:</p>
                            </div>
                            <div id="txtErrorHandler" runat="server" ms_positioning="FlowLayout" style="display: inline;
                                z-index: 107; left: 137px; width: 407px; position: absolute; top: 96px; height: 24px">
                                Error Handler</div>
                            <div id="txtPageName" runat="server" ms_positioning="FlowLayout" style="display: inline;
                                z-index: 108; left: 138px; width: 406px; position: absolute; top: 128px; height: 24px">
                                Page Name</div>
                            <div id="txtModuleName" runat="server" ms_positioning="FlowLayout" style="display: inline;
                                z-index: 109; left: 139px; width: 405px; position: absolute; top: 160px; height: 24px">
                                Module Name</div>
                            <hr color="#c0c0c0" noshade="noshade" size="2" style="z-index: 110; left: 16px; width: 280px;
                                position: absolute; top: 400px; height: 2px" />
                            <div ms_positioning="FlowLayout" style="z-index: 111; left: 16px; width: 280px; position: absolute;
                                top: 408px; height: 16px">
                                <p id="P6">
                                    Please be pation and try to follow the steps
                                </p>
                            </div>
                            <div dir="ltr" ms_positioning="FlowLayout" style="z-index: 112; left: 16px; width: 280px;
                                position: absolute; top: 424px; height: 16px">
                                in order to Fix your problem.</div>
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="X-Small" Font-Underline="True"
                                NavigateUrl="Login.aspx" Style="z-index: 113; left: 330px; position: absolute;
                                top: 38px" meta:resourcekey="HyperLink1Resource2">Refresh</asp:HyperLink>
                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="X-Small" Font-Underline="True"
                                Style="z-index: 115; left: 329px; position: absolute; top: 67px" meta:resourcekey="LinkButton1Resource2">Send Mail</asp:LinkButton>
                        </div>
                    </font>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
