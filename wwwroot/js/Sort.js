
function appendPaging2(totPages) {

    TotalPages = totPages;
    var pageLink = $('.Paging');
    var curPage = urlParam("PAGE2");
    if (curPage == '')
        curPage = 1;

    pageLink.empty();
    pageLink.append();

    var disable5 = "";
    var disableLast = "";
    var disableNext = "";
    var disableDecr5 = "";
    var disablePrev = ""
    var disableNext = "";
    var disableFirst = "";
    if (TotalPages < 5)
        disable5 = "disabled";
    if (curPage == TotalPages) {
        disableLast = "disabled";
        disableNext = "disabled";
        disable5 = "disabled";
    }
    if (curPage == 1) {
        disableDecr5 = "disabled";
        disablePrev = "disabled";
        disableFirst = "disabled";
    }


    pageLink.append('<input type="submit" name="FIRST" onclick="page(event);" style="margin-left:4px;" class="page-link" value="<<"' + disableFirst + ' />');
    pageLink.append('<input type="submit" name="PREV1" onclick="page(event);"  style="margin-left:4px;" class="page-link" value="<"' + disablePrev + ' />');
    pageLink.append('<input type="submit" name="PREV5" onclick="page(event);" style="margin-left:4px;" class="page-link" value="<5"' + disableDecr5 + ' />');
    pageLink.append('<input type="submit" name="NEXT5" onclick="page(event);" style="margin-left:4px;" class="page-link" value="5>"' + disable5 + ' />');
    pageLink.append('<input type="submit" name="NEXT1" onclick="page(event);" style="margin-left:4px;" class="page-link" value=">"' + disableNext + ' />');
    pageLink.append('<input type="submit" name="LAST" onclick="page(event);" style="margin-left:4px;" class="page-link" value=">>"' + disableLast + ' />');
    pageLink.append('<input type="submit" class="page-link" style="background-color:#ccc; margin-left:4px;"  value="1" disabled />');
    pageLink.append('<input type="submit" class="page-link" style="background-color:#ccc;  margin-left:4px;"  value="' + TotalPages + '" disabled />');
    pageLink.children().eq(6).val(curPage);

}

function appendSorting2(liColumns) {
    var allColAry = liColumns.split(";");
    for (let i = 0; i < allColAry.length; i++) {
        $('tr > th > a[href*="SORTCOL2=' + allColAry[i].toString().replace(" ", "") + '"]').attr("onclick", 'setSort2(this);');
    }
    var sortCol = urlParam("SORTCOL2");
    var sortDir = urlParam("SORTDIR2");
    if (sortDir != "") {
        if (sortDir == "DESC") {
            txt = $('tr > th > a[href*="SORTCOL2=' + sortCol + '"]').attr('href').toString();
            txt = txt.replace("SORTDIR2=DESC", "SORTDIR2=ASC");
            $('tr > th > a[href*="SORTCOL2=' + sortCol + '"]').attr('href', txt);
            $('tr > th > a[href*="SORTCOL2=' + sortCol + '"]').parent().append("▼");
        }
        else {
            txt = $('tr > th > a[href*="SORTCOL2=' + sortCol + '"]').attr('href').toString();
            txt = txt.replace("SORTDIR2=ASC", "SORTDIR2=DESC");
            $('tr > th > a[href*="SORTCOL2=' + sortCol + '"]').attr('href', txt);
            $('tr > th > a[href*="SORTCOL2=' + sortCol + '"]').parent().append("▲");

        }
    }
}

function page(event) {
    var txt = window.location.toString();
    switch (event.target.name.toString()) {
        case 'PREV1':
            var pageIndex = $(event.target).parent().children().eq(6).val();
            var intIndex = parseInt(pageIndex);
            if (intIndex == 1) {
                $(event.target).parent().children().eq(1).prop('disabled', true);
                break;
            }

            var intIndex2 = ((intIndex - 1) >= 1 ? intIndex - 1 : 1);
            $(event.target).parent().children().eq(6).val(intIndex2);
            txt = updateQueryStringParameter(txt, 'PAGE2', intIndex2);
            window.location = txt;
            break;
        case 'NEXT1':
            var pageIndex = $(event.target).parent().children().eq(6).val();
            var intIndex = parseInt(pageIndex);
            if (intIndex == TotalPages) {
                $(event.target).parent().children().eq(4).prop('disabled', true);
                break;
            }
            var intIndex2 = ((intIndex + 1) <= TotalPages ? intIndex + 1 : TotalPages);
            $(event.target).parent().children().eq(6).val(intIndex2);
            txt = updateQueryStringParameter(txt, 'PAGE2', intIndex2);
            window.location = txt;
            break;
        case 'PREV5':
            var pageIndex = $(event.target).parent().children().eq(6).val();
            var intIndex = parseInt(pageIndex);
            if (intIndex == 1) {
                $(event.target).parent().children().eq(2).prop('disabled', true);
                break;
            }
            var intIndex2 = ((intIndex - 5) >= 1 ? intIndex - 5 : 1);
            $(event.target).parent().children().eq(6).val(intIndex2);
            if (intIndex2 == 1) {
                $(event.target).parent().children().eq(6).attr('disabled', 'disabled');
            }
            txt = updateQueryStringParameter(txt, 'PAGE2', intIndex2);
            window.location = txt;
            break;
        case 'NEXT5':
            var pageIndex = $(event.target).parent().children().eq(6).val();
            var intIndex = parseInt(pageIndex.trim());
            if (intIndex == TotalPages) {
                $(event.target).parent().children().eq(3).prop('disabled', true);
                break;
            }
            var intIndex2 = ((intIndex + 5 < TotalPages) ? intIndex + 5 : TotalPages);
            $(event.target).parent().children().eq(6).val(intIndex2);
            txt = updateQueryStringParameter(txt, 'PAGE2', intIndex2);
            window.location = txt;
            break;
        case 'LAST':

            var pageIndex = $(event.target).parent().children().eq(6).val();
            var intIndex = parseInt(pageIndex.trim());
            if (intIndex == TotalPages) {
                $(event.target).parent().children().eq(5).prop('disabled', true);
                $(event.target).parent().children().eq(6).val(TotalPages);
                break;
            }
            txt = updateQueryStringParameter(txt, 'PAGE2', TotalPages);
            window.location = txt;
            break;
        case 'FIRST':
            var pageIndex = $(event.target).parent().children().eq(6).val();
            var intIndex = parseInt(pageIndex.trim());
            if (intIndex == 1) {
                $(event.target).parent().children().eq(0).prop('disabled', true);
                $(event.target).parent().children().eq(6).val(1);
                break;
            }
            txt = updateQueryStringParameter(txt, 'PAGE2', 1);
            window.location = txt;
            break;
        default:
        // code block
    }
}

function setSort2(e) {

    var type = e.text.toString().replace(" ", "").replace("%", "").replace("(", "").replace(")", "");
    var txtHref = $('tr > th > a[href*="SORTCOL2=' + type + '"]').attr('href').toString();
    txtHref = setURLParams(txtHref);
    txtHref = updateQueryStringParameter(txtHref, 'PAGE2', 1);

    $('tr > th > a[href*="SORTCOL2=' + type + '"]').attr('href', txtHref);
}

function setURLParams(txtHref) {
    var urlTxt = window.location.toString();
    var urlAry = urlTxt.split("?");
    if (urlAry.length > 1) {
        urlTxt = urlAry[1];
        const searchParams = new URLSearchParams(urlTxt);
        for (const key of searchParams.keys()) {
            if ((key != "SORTCOL2") && (key != "SORTDIR2") && (key != "PAGE2")) {
                var val = urlParam(key);
                txtHref = updateQueryStringParameter(txtHref, key, val);
            }
        }
    }
    return txtHref;
}

function updateQueryStringParameter(uri, key, value) {
    var re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
    var separator = uri.indexOf('?') !== -1 ? "&" : "?";
    if (uri.match(re)) {
        return uri.replace(re, '$1' + key + "=" + value + '$2');
    }
    else {
        return uri + separator + key + "=" + value;
    }
}
 function urlParam (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)')
        .exec(window.location.search);

    return (results !== null) ? results[1] || 0 : false;
}
