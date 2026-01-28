
//------------------------
//© 2012 www.aspsnippets.com All rights reserved.
//------------------------
function GetCoordinates(a) { var b = {}; b.x = a.offsetLeft; b.y = a.offsetTop; while (a.offsetParent) { b.x = b.x + a.offsetParent.offsetLeft; b.y = b.y + a.offsetParent.offsetTop; if (a == document.getElementsByTagName("body")[0]) { break } else { a = a.offsetParent } } return b } (function (a) {
    a.fn.Float = function (b) {
        var c = {}; var b = a.extend(c, b); return this.each(function () {
            var b = a(this); var c = a("[id*=" + b[0].controltovalidate + "]");
                var d = GetCoordinates(c[0]);
                b.css({ position: "absolute", top: d.y + 3, left: d.x - 20, backgroundColor: "" });
                b.click(function () { b.hide(); c.focus() });
                c.click(function () { b.hide(); c.focus() });
                c.focus(function () { c.val(c.val()) })
        })
    }
})(jQuery)