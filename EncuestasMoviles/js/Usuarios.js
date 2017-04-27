$(function () {
    var hideDelay = 500;
    var currentID;
    var hideTimer = null;

    // One instance that's reused to show info for the current person
    var container = $('<div id="personPopupContainer">'
      + '<table width="" border="0" cellspacing="0" cellpadding="0" align="center" class="personPopupPopup">'
      + '<tr>'
      + '   <td class="corner topLeft"></td>'
      + '   <td class="top"></td>'
      + '   <td class="corner topRight"></td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="left"></td>'
      + '   <td align="center"><div id="personPopupContent"></div></td>'
      + '   <td class="right"></td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="corner bottomLeft"></td>'
      + '   <td class="bottom"></td>'
      + '   <td class="corner bottomRight"></td>'
      + '</tr>'
      + '</table>'
      + '</div>');

    $('body').append(container);

    $('.personPopupTrigger').bind('mouseover', function () {

        // format of 'rel' tag: pageid,personguid
        alert("asdasdas");
        var settings = $(this).attr('rel').split(',');
        var pageID = settings[0];
        currentID = "NO"; //settings[1];

        // If no guid in url rel tag, don't popup blank
        if (currentID == '')
            return;

        if (hideTimer)
            clearTimeout(hideTimer);

        var pos = $(this).offset();
        var width = $(this).width();
        container.css({
            left: (pos.left + width) + 'px',
            top: pos.top - 5 + 'px'
        });

        $('#personPopupContent').html('&nbsp;');

        $.ajax({
            type: 'GET',
            url: 'DispositivoInfo.aspx',
            data: 'data=' + pageID + '&guid=' + currentID,
            success: function (data) {
                // Verify that we're pointed to a page that returned the expected results.
                if (data.indexOf('personPopupResult') < 0) {
                    alert("A");
                    $('#personPopupContent').html('<span >Page ' + pageID + ' did not return a valid result for person ' + currentID + '.<br />Please have your administrator check the error log.</span>');
                } else {
                    alert("Nooo");
                }

                // Verify requested person is this person since we could have multiple ajax
                // requests out if the server is taking a while.
                if (data.indexOf(currentID) > 0) {
                    alert("b");
                    var text = $(data).find('.personPopupResult').html();
                    alert(text);
                    $('#personPopupContent').html(text);
                } else {
                    alert("dsfdsf");
                }
                alert("sdasda");
            }, error: function () {
                alert("ocurrio un error");
            }
        });

        container.css('display', 'block');
    });

    $('.personPopupTrigger').bind('mouseout', function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });

    // Allow mouse over of details without hiding details
    $('#personPopupContainer').mouseover(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
    });

    // Hide after mouseout
    $('#personPopupContainer').mouseout(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });
});
function clickRButton(id) {

    document.getElementById("Body_hfIdRButton").value = id;
}

function limpiacampoBuscaDispo() {
    document.getElementById("Body_txtNumDispo").value = "";
}