/* PopUp */

$(function () {
    var hideDelay = 500;
    var currentID;
    var hideTimer = null;

    // One instance that's reused to show info for the current person
    var container = $('<div id="personPopupContainer3">'
      + '<table width="" border="0" cellspacing="0" cellpadding="0" align="center" class="personPopupPopup">'
      + '<tr>'
      + '   <td class="corner topLeft"></td>'
      + '   <td class="top"></td>'
      + '   <td class="corner topRight"></td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="left">&nbsp;</td>'
      + '   <td align="center"><div id="personPopupContent3"></div></td>'
      + '   <td class="right">&nbsp;</td>'
      + '</tr>'
      + '<tr>'
      + '   <td class="corner bottomLeft">&nbsp;</td>'
      + '   <td class="bottom">&nbsp;</td>'
      + '   <td class="corner bottomRight"></td>'
      + '</tr>'
      + '</table>'
      + '</div>');

    $('body').append(container);

    $('.personPopupTrigger3').live('click', function () {

        // format of 'rel' tag: pageid,personguid

        var settings = $(this).attr('src');
        var pageID = settings;
        currentID = $(this).attr('id');

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

        $('#personPopupContent3').html('&nbsp;');

        $.ajax({
            type: 'GET',
            url: 'DispositivoInfo.aspx',
            data: 'data=' + pageID + '&guid=' + currentID,
            success: function (data) {
                // Verify that we're pointed to a page that returned the expected results.
                if (data.indexOf('personPopupResult') < 0) {
                    $('#personPopupContent3').html('<span >Page ' + pageID + ' did not return a valid result for person ' + currentID + '.<br />Please have your administrator check the error log.</span>');
                }

                // Verify requested person is this person since we could have multiple ajax
                // requests out if the server is taking a while.
                if (data.indexOf(currentID) > 0) {
                    var text = $(data).find('.personPopupResult').html();
                    $('#personPopupContent3').html(text);
                }
            }
        });

        container.css('display', 'block');
    });

    $('.personPopupTrigger3').live('mouseout', function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });

    // Allow mouse over of details without hiding details
    $('#personPopupContainer3').mouseover(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
    });

    // Hide after mouseout
    $('#personPopupContainer3').mouseout(function () {
        if (hideTimer)
            clearTimeout(hideTimer);
        hideTimer = setTimeout(function () {
            container.css('display', 'none');
        }, hideDelay);
    });
});

/* PopUp */

function EnviarAsignados() {
    var checks = document.getElementsByTagName('input');
    document.getElementById("Body_hfIdsEncuestas").value = '';
    document.getElementById("Body_hfIdsDispositivos").value = '';
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].type == "checkbox") {
            if (checks[i].name == "chkEncuestas") {
                if (checks[i].checked) {
                    document.getElementById("Body_hfIdsEncuestas").value += checks[i].id + ",";
                }
            }
            else if (checks[i].name.split('|')[0] == "chkDispositivos") {
                if (checks[i].checked) {
                    document.getElementById("Body_hfIdsDispositivos").value += checks[i].id + ",";
                }
            }
        }
    }
    document.getElementById("Body_btnEnviaAsignados").click();
}

function chkEncu(id) {
    document.getElementById("Body_hfIdEncuestaUnico").value = id;
    document.getElementById("Body_btnEncuChk").click();
}

function clickTodos(selecc) {
    var checksTodos = document.getElementsByTagName('input');
    document.getElementById("Body_hfIdsEncuestas").value = '';
    document.getElementById("Body_hfIdsDispositivos").value = '';
    for (var i = 0; i < checksTodos.length; i++) {
        if (checksTodos[i].type == "checkbox") {
            if (checksTodos[i].name == "chkEncuestas") {
                if (checksTodos[i].checked) {
                    document.getElementById("Body_hfIdsEncuestas").value += checksTodos[i].id + ",";
                }
            }
            else if (checksTodos[i].name.split('|')[0] == "chkDispositivos" && checksTodos[i].name.split('|')[1] == "Rojo") {
                if (!checksTodos[i].checked) {
                    checksTodos[i].checked = true;
                    document.getElementById("Body_hfIdsDispositivos").value += checksTodos[i].id + ",";
                }
                else {
                    checksTodos[i].checked = false;
                    document.getElementById("Body_hfIdsDispositivos").value = "";
                }
            }
        }
    }
}

function chkCrea() {
    var checks = document.getElementsByTagName('input');
    document.getElementById("Body_hfTipoFecha").value = '';
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].type == "radio") {
            if (checks[i].name == "Radio") {
                if (checks[i].checked) {
                    document.getElementById("Body_hfTipoFecha").value += checks[i].id;
                }
            }
        }
    }
    document.getElementById("Body_btnBuscaEncu").click();
}

function BuscaDispo() {
    debugger;
    var CombCat = document.getElementsByTagName('select');
    document.getElementById("Body_hfIdCat").value = '';
    for (var ini = 0; ini < CombCat.length; ini++) {
        if (CombCat[ini].IdCatalogo != undefined) {
            if (CombCat[ini].selectedIndex != CombCat[ini].length - 1) {
                var IdOpciCat = CombCat[ini].value;
                var idCatalogo = CombCat[ini].IdCatalogo;
                document.getElementById('Body_hfIdCat').value += idCatalogo + "|" + IdOpciCat + "&";
            }
        }
    }
    document.getElementById("Body_btnBuscaDispo").click();
}