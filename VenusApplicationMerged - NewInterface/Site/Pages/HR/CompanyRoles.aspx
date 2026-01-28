<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompanyRoles.aspx.vb" Inherits="CompanyRoles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Common/Script/JQuery/jquery-1.6.2.js" type="text/javascript"></script>
</head>
<body style="height: 100%; margin: 0; padding: 0;">
    <form id="CompanyRoles" runat="server">
    <link href="<%=GetStyleSheet()%>" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var X;
        var Y;
        var li_size;
        var int_val;
        var permition = true;
        var height_size;
        var li_child;
        var li_Parent;
        var main_nav;
        var coloring;
        var back = true;
        var null_;
        var part = 0;
        $(window).load(function () {
            main_nav = $('div.navigation>ul').html();
            $('div.navigation>div').html($('div.navigation>div').html() + '<div id = "back"></div>');
            $("div.navigation>ul>li").live('click', function () {
                if (permition == true && $(this).children('ul').html().length) {
                    int_val = 300;
                    Y = $(this).children("a").html();
                    li_size = $("div.navigation>ul>li").size();
                    for (i = 1; i <= li_size; i++) {
                        X = $("div.navigation>ul>li:nth-child(" + String(i) + ")").children("a").html();
                        if (X != Y)
                            $("div.navigation>ul>li:nth-child(" + String(i) + ")").addClass('animated flipIn_left');
                        setTimeout(function () {
                            $("div.navigation>ul>li:nth-child(" + String(i) + ")").removeClass('animated flipIn_left');
                        }, int_val);
                        int_val = 300 * i;
                    }
                    $('div.navigation>ul').animate({ opacity: '0.1', marginLeft: '40px' }, 600);
                    $("div.navigation>div>span").animate({ opacity: '0.1', top: '-10px' }, 600, function () {
                        $("div.navigation>div>span").html(Y);
                        $("div.navigation>div>span").animate({ opacity: '1', top: '0px' }, 600);
                    });
                    permition = false;
                    back = true;
                    li_child = $(this).children('ul').html();
                    coloring = $(this).css("background-color");
                    setTimeout(function () {
                        $('div.navigation>ul').html(li_child);
                        $('div#back').show(300);
                        $('div.navigation>ul>li').css('background-color', coloring);
                        $('div.navigation>ul').animate({ opacity: '1', marginLeft: '0px' }, 600);
                        $('div.navigation>ul>li').addClass('animated flipIn_right_back');
                        setTimeout(function () {
                            $('div.navigation>ul>li').removeClass('animated flipIn_right_back');
                        }, 600);
                    }, 600);
                }
            });
            $("div#back").live('click', function () {
                if (back == true) {
                    $('div.navigation>ul>li').addClass('animated flipIn_left');
                    $('div.navigation>ul').animate({ opacity: '0.1', marginLeft: '40px' }, 400);
                    setTimeout(function () {
                        $('div.navigation>ul').removeClass('animated flipIn_left');
                        $('div.navigation>ul').html(main_nav);
                        $('div.navigation>ul>li').addClass('animated flipIn_right_back');
                        $('div.navigation>ul').animate({ opacity: '1', marginLeft: '0px' }, 600);
                        $("div#back").hide(300);
                        setTimeout(function () {
                            $('div.navigation>ul>li').removeClass('animated flipIn_right_back');
                        }, 400);
                    }, 400);
                    permition = true;
                    $("div.navigation>div>span").animate({ opacity: '0.1', top: '-10px' }, 600, function () {
                        $("div.navigation>div>span").html('<%=Resources.MessageSetting.CompanyRole%>');
                        $("div.navigation>div>span").animate({ opacity: '1', top: '0px' }, 600);
                    });
                    back = false;
                }
            });
            $("div.navigation>ul>li").live('mouseleave', function () {
                $(this).removeClass('animated flipIn_right');
                $(this).removeClass('animated flipIn_left');
                $(this).removeClass('animated flipIn_top');
                $(this).removeClass('animated flipIn_bottom');
                $(this).removeClass('do');
                part = 0;
            });
            $("div.navigation>ul>li").live('mousemove', function (e) {
                if ($.browser.webkit) {
                    if (part != 5) {
                        $(this).addClass('do');
                        part = 5;
                    }
                }
                else {
                    if (((e.clientY - $(this).offset().top) / (e.clientX - $(this).offset().left)) < 1) {
                        if (((e.clientY - $(this).offset().top) / ($(this).width() - (e.clientX - $(this).offset().left))) > 1) {
                            if (part != 1) {
                                $(this).removeClass('animated flipIn_bottom');
                                $(this).removeClass('animated flipIn_top');
                                $(this).removeClass('animated flipIn_right');
                                $(this).addClass('animated flipIn_right');
                                part = 1;
                            }
                        }
                        else {
                            if (part != 2) {
                                $(this).removeClass('animated flipIn_bottom');
                                $(this).removeClass('animated flipIn_right');
                                $(this).removeClass('animated flipIn_left');
                                $(this).addClass('animated flipIn_top');
                                part = 2;
                            }
                        }

                    }
                    else if (((e.clientY - $(this).offset().top) / (e.clientX - $(this).offset().left)) > 1) {
                        if (((e.clientX - $(this).offset().left) / ($(this).height() - (e.clientY - $(this).offset().top))) > 1) {
                            if (part != 3) {
                                $(this).removeClass('animated flipIn_top');
                                $(this).removeClass('animated flipIn_left');
                                $(this).removeClass('animated flipIn_right');
                                $(this).addClass('animated flipIn_bottom');
                                part = 3;
                            }
                        }
                        else {
                            if (part != 4) {
                                $(this).removeClass('animated flipIn_top');
                                $(this).removeClass('animated flipIn_right');
                                $(this).removeClass('animated flipIn_bottom');
                                $(this).addClass('animated flipIn_left');
                                part = 4;
                            }
                        }
                    }
                    else {
                        $(this).removeClass('animated flipIn_right');
                        $(this).removeClass('animated flipIn_left');
                        $(this).removeClass('animated flipIn_top');
                        $(this).removeClass('animated flipIn_bottom');
                        part = 0;
                    }
                }
            });
            $("div#back").mouseenter(function () {
                $('div#back').css('background-position', '50px -50px');
            });
            $("div#back").mouseleave(function () {
                $('div#back').css('background-position', '0px 0px');
            });
            $("div#back").mousedown(function () {
                $('div#back').css('background-position', '50px -100px');
            });
            $("div#back").mouseup(function () {
                $('div#back').css('background-position', '50px -50px');
            });
        });
    </script>
    <table class="Role" style="width: 800px; height: 400px; vertical-align: top;">
        <tr>
            <td style="width: 10px; height: 400px; vertical-align: top">
            </td>
            <td style="width: 780px; height: 400px; vertical-align: top;">
                <div class="navigation" runat="server" id="Nav">
                </div>
            </td>
            <td style="width: 10px; height: 400px; vertical-align: top">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
