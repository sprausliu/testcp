(function ($) {
    $.fn.HtmlEncode = function (inValue) {
        return $('<div/>').text(inValue).html();
    };
    $.fn.HtmlDecode = function (inValue) {
        return $('<div/>').html(inValue).text();
    };

    $.fn.RefreshToolTip = function () {
        setTimeout(function () {
            try { $("#tooltip").css("display", "none"); } catch (e) { }
        }, 500);
    };

    $.fn.AlertMsg = function (inMsg) {
        alert(inMsg);
        $(this).focus();
        return false;
    };
})(jQuery);

function MovePageWithNoHistory(url) {
    window.location.replace(url);
    return false;
}



