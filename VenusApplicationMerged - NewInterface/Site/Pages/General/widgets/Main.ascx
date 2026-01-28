<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Main.ascx.vb" Inherits="Main" %>
<script src="../../../Common/Script/JQuery/jquery-1.9.1.js" type="text/javascript"></script>
<script language="JavaScript">
    function autoResize(id) {
        var newheight;
        var newwidth;
        if (document.getElementById) {
            newheight = $(window).height();
            newwidth = $(window).width();
        }
        document.getElementById(id).height = (newheight-100) + "px";
        document.getElementById(id).width = (newwidth-200) + "px";
    }
    function Returned(idx) {
        var str = $('#iframe0').attr('src');

        var res = str.replace("?PageIdx=0", "");
        res = res.replace("?PageIdx=1", "");
        res = res.replace("?PageIdx=2", "");
        res = res.replace("?PageIdx=3", "");
        res = res.replace("?PageIdx=4", "");
        res = res.replace("?PageIdx=5", "");
        res = res.replace("?PageIdx=6", "");
        res = res.replace("?PageIdx=7", "");
        res = res.replace("?PageIdx=8", "");
        $('#iframe0').attr('src', res + "?PageIdx=" + idx);
    }
</script>
<iframe id="iframe0" src="Pages/General/widgets/Main.aspx" width="100%" height="100%"
    marginheight="0" frameborder="0" onload="autoResize('iframe0');"></iframe>
