var offsetxpoint = -60 //Customize x offset of tooltip
var offsetypoint = 20 //Customize y offset of tooltip
var ie = document.all
var ns6 = document.getElementById && !document.all
var enabletip = false
var ventanaAbierta;
if (ie || ns6)
    var tipobj = document.all ? document.all["dhtmltooltip"] : document.getElementById ? document.getElementById("dhtmltooltip") : ""

function __doPostBack(eventTarget, eventArgument) {
    if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
        theForm.__EVENTTARGET.value = eventTarget;
        theForm.__EVENTARGUMENT.value = eventArgument;
        theForm.submit();
    }
}

function CloseAll() {
    if (ventanaAbierta)
        cerrarVentanaInicio();

    __doPostBack('__Page', 'CloseAll');
}


function ModFunction(Main, Modulus) {
    var ModFunction = Main - (Math.floor((Main / Modulus)) * Modulus);
    return ModFunction;
}

function Mult(x, p, m) {
    var y = 1;
    var i = 0;
    for (i = p; i > 0; i--) {
        while ((i / 2) == (Math.floor((i / 2)))) {
            x = ModFunction((x * x), m);
            i = (i / 2);
        }
        y = ModFunction((x * y), m);
    }
    return y;
}

function encrypt() {    
    var text = document.getElementById('txtContraseña').value;    
    var user = document.getElementById('txtUsuario').value;

    //Desarrollo    
    document.getElementById('txtContraseña').value = doEncrypt('fcbe82b9526d059a', 1, 'CAD2Qle3CH8IF3KiutapQvMF6PlTETlPtvFuuUs4INoBp1ajFOmPQFXz0AfGy0OplK33TGSGSfgMg71l6RfUodNQ+PVZX9x2Uk89PY3bzpnhV5JZzf24rnRPxfx2vIPFRzBhznzJZv8V+bv9kV7HAarTW56NoKVyOtQa8L9GAFgr5fSI/VhOSdvNILSd5JEHNmszbDgNRR0PfIizHHxbLY7288kjwEPwpVsYjY67VYy4XTjTNP18F1dDox0YbN4zISy1Kv884bEpQBgRjXyEpwpy1obEAxnIByl6ypUM2Zafq9AKUJsCRtMIPWakXUGfnHy9iUsiGSa6q6Jew1XpMgs7AAICCADd/l+nczegGS54V0rQNP4j7BC6ACTjo26cy0F0QLCRlLzWI0Q6Xq5nePiq964HselXNwj9pC9T0nBRBdal5mVQCYEOYaJUU+68u7NkCDl683IJKMZt9wmqjBgFTCAlC/4M/sa4R5kMsBGlvbqcSN83UlvyQBVXy7pkiI3HQpqEI+B7DFGzGL9naboItlo1OTFv2U52cTcinNUJibD2c1SPMiY+xGOXeeZKDC6yiG6rMt1/JnsA1SYOiPqMjlr8dxdONk9LTqVKvixFfimQjvSmz3uc37J64oVKYh08Dq0uCKMM/viPowGfR2OWsx/B1h6C6OnAHGXU8dO9aOeimW2w', text);
    document.getElementById('txtUsuario').value = doEncrypt('fcbe82b9526d059a', 1, 'CAD2Qle3CH8IF3KiutapQvMF6PlTETlPtvFuuUs4INoBp1ajFOmPQFXz0AfGy0OplK33TGSGSfgMg71l6RfUodNQ+PVZX9x2Uk89PY3bzpnhV5JZzf24rnRPxfx2vIPFRzBhznzJZv8V+bv9kV7HAarTW56NoKVyOtQa8L9GAFgr5fSI/VhOSdvNILSd5JEHNmszbDgNRR0PfIizHHxbLY7288kjwEPwpVsYjY67VYy4XTjTNP18F1dDox0YbN4zISy1Kv884bEpQBgRjXyEpwpy1obEAxnIByl6ypUM2Zafq9AKUJsCRtMIPWakXUGfnHy9iUsiGSa6q6Jew1XpMgs7AAICCADd/l+nczegGS54V0rQNP4j7BC6ACTjo26cy0F0QLCRlLzWI0Q6Xq5nePiq964HselXNwj9pC9T0nBRBdal5mVQCYEOYaJUU+68u7NkCDl683IJKMZt9wmqjBgFTCAlC/4M/sa4R5kMsBGlvbqcSN83UlvyQBVXy7pkiI3HQpqEI+B7DFGzGL9naboItlo1OTFv2U52cTcinNUJibD2c1SPMiY+xGOXeeZKDC6yiG6rMt1/JnsA1SYOiPqMjlr8dxdONk9LTqVKvixFfimQjvSmz3uc37J64oVKYh08Dq0uCKMM/viPowGfR2OWsx/B1h6C6OnAHGXU8dO9aOeimW2w', user);

    //Produccion            
    //document.getElementById('txtContraseña').value = doEncrypt('22ab5a2aeb076947', 1, 'CAD2Qle3CH8IF3KiutapQvMF6PlTETlPtvFuuUs4INoBp1ajFOmPQFXz0AfGy0OplK33TGSGSfgMg71l6RfUodNQ+PVZX9x2Uk89PY3bzpnhV5JZzf24rnRPxfx2vIPFRzBhznzJZv8V+bv9kV7HAarTW56NoKVyOtQa8L9GAFgr5fSI/VhOSdvNILSd5JEHNmszbDgNRR0PfIizHHxbLY7288kjwEPwpVsYjY67VYy4XTjTNP18F1dDox0YbN4zISy1Kv884bEpQBgRjXyEpwpy1obEAxnIByl6ypUM2Zafq9AKUJsCRtMIPWakXUGfnHy9iUsiGSa6q6Jew1XpMgs7AAICB/9fEndKPOw3U/yKxxmLD/x/uR6FO+5XkNGkMvolk/SU0PKB8gyf+YuZ0dxJ8nZ8hfJ0vvWzpX240YCDqI6RCU/eKedYz1IQy/Tt5IufL4RdVeyQvlzSr6Kl9jFxwoJt9R8cUfT3bfy/X4WtN6uxIsoZFrHrYNyy2wKu8o+Sr6+04jjX9c8mxy6an/OMg/LTp0Nxbl46Cw3A1b1YPjz3cKmjKN8+6bt10Wa/6RdEf6psyI/CHJKJcVZTnkEHCHuPjlt8QRe64kHywQeDGOvyJ+JqtAlUAJisUKm8e3yy8yeGLs0CrUziQIWQczN3a3Z+b5y056vAmLSvAsPgerROH6CR', text);
    //document.getElementById('txtUsuario').value = doEncrypt('22ab5a2aeb076947', 1, 'CAD2Qle3CH8IF3KiutapQvMF6PlTETlPtvFuuUs4INoBp1ajFOmPQFXz0AfGy0OplK33TGSGSfgMg71l6RfUodNQ+PVZX9x2Uk89PY3bzpnhV5JZzf24rnRPxfx2vIPFRzBhznzJZv8V+bv9kV7HAarTW56NoKVyOtQa8L9GAFgr5fSI/VhOSdvNILSd5JEHNmszbDgNRR0PfIizHHxbLY7288kjwEPwpVsYjY67VYy4XTjTNP18F1dDox0YbN4zISy1Kv884bEpQBgRjXyEpwpy1obEAxnIByl6ypUM2Zafq9AKUJsCRtMIPWakXUGfnHy9iUsiGSa6q6Jew1XpMgs7AAICB/9fEndKPOw3U/yKxxmLD/x/uR6FO+5XkNGkMvolk/SU0PKB8gyf+YuZ0dxJ8nZ8hfJ0vvWzpX240YCDqI6RCU/eKedYz1IQy/Tt5IufL4RdVeyQvlzSr6Kl9jFxwoJt9R8cUfT3bfy/X4WtN6uxIsoZFrHrYNyy2wKu8o+Sr6+04jjX9c8mxy6an/OMg/LTp0Nxbl46Cw3A1b1YPjz3cKmjKN8+6bt10Wa/6RdEf6psyI/CHJKJcVZTnkEHCHuPjlt8QRe64kHywQeDGOvyJ+JqtAlUAJisUKm8e3yy8yeGLs0CrUziQIWQczN3a3Z+b5y056vAmLSvAsPgerROH6CR', user);
}

function Encode() {
    var encoded_field = "";
    var original_field = document.getElementById('txtContraseña').value

    if (original_field != "") {
        var Enc = document.getElementById('hdnE').value;
        var Mod = document.getElementById('hdnN').value;

        for (i = 0; i <= original_field.length - 1; i++) {
            // charCodeAt gives the ASC value of character in position i
            //Aqui encripta el password caracter por caracter y lo deja en la cadena 'encoded_field'
            encoded_field = encoded_field + Mult((original_field.charCodeAt(i)), Enc, Mod) + ","
        }

        document.getElementById('txtContraseña').value = "";
        document.getElementById('passEncode').value = encoded_field;
    }
    else {
        alertModal('Ingrese la contraseña ¡¡¡');
        return false;
    }
}

function MM_swapImgRestore() { //v3.0
    var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
}

function MM_preloadImages() { //v3.0
    var d = document; if (d.images) {
        if (!d.MM_p) d.MM_p = new Array();
        var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
            if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; }
    }
}

function MM_findObj(n, d) { //v4.01
    var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
        d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
    }
    if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
    for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
    if (!x && d.getElementById) x = d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
    var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
        if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
}

function mostrardiv() {
    div = document.getElementById('flotante');
    div.style.display = "";
}

function cerrar() {
    div = document.getElementById('flotante');
    div.style.display = 'none';
}

function Marco(id) {
    id.style.borderStyle = 'solid';
    id.style.borderWidth = '1px';
    id.style.borderColor = 'Black';
}

function QuitarMarco(id) {
    id.style.borderWidth = '0px';
}

function LogOutUsuario() {
    if (document.getElementById('hdnLogIn').value != "") {
        document.getElementById('btnLogOutUsuario').click();
        //return false;
    }
}
function CallModal(Option) {
    var validar = document.getElementById('HidUsr');
    if (validar.value == 0) {
        alertModal('Introduzca su Usuario y Contraseña ¡¡¡');
        return false;
    }

    switch (Option) {

        case 1:
            ShowModal('MVVCFia.aspx', 'Visión, Misión y Valores Azteca Noticias y Azteca Deportes', 570, 750, 40);
            break;
        case 2:
            ShowModal('ManualesFia.aspx', 'Manuales y Procesos', 600, 800, 40);
            break;
        case 3:
            ShowModal('MVVGrupoSal.aspx', 'Visión y Valores TV Azteca', 600, 750, 40);
            break;
        default:
            alertModal('Opción no Valida');
            break;
    }
}

function AbrirVentanaSoporte() {

    var validar = document.getElementById('HidUsr');

    if (validar.value == 0) {
        window.open('http://aztecasoporte/Eservice/AltaETicket.aspx?A=wqhwKlHjNB%2fTwDsYv3cPzQ%3d%3d');
    }
    else {
        var B = document.getElementById('B').value; //.replace(/=/gi, '%3d').replace('/', '%2f');
        var cadenadestino = 'http://aztecasoporte/Eservice/AltaETicket.aspx?A=wqhwKlHjNB%2fTwDsYv3cPzQ%3d%3d&B=' + encodeURIComponent(B);
        window.open(cadenadestino);
    }
    return false;
}

function ietruebody() {
    return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
}

function ddrivetip(thetext, thecolor, thewidth) {
    if (ns6 || ie) {
        if (typeof thewidth != "undefined") tipobj.style.width = thewidth + "px"
        if (typeof thecolor != "undefined" && thecolor != "") tipobj.style.backgroundColor = thecolor
        tipobj.innerHTML = thetext
        enabletip = true
        return false
    }
}

function positiontip(e) {
    if (enabletip) {
        var curX = (ns6) ? e.pageX : event.clientX + ietruebody().scrollLeft;
        var curY = (ns6) ? e.pageY : event.clientY + ietruebody().scrollTop;
        //Find out how close the mouse is to the corner of the window
        var rightedge = ie && !window.opera ? ietruebody().clientWidth - event.clientX - offsetxpoint : window.innerWidth - e.clientX - offsetxpoint - 20
        var bottomedge = ie && !window.opera ? ietruebody().clientHeight - event.clientY - offsetypoint : window.innerHeight - e.clientY - offsetypoint - 20

        var leftedge = (offsetxpoint < 0) ? offsetxpoint * (-1) : -1000

        //if the horizontal distance isn't enough to accomodate the width of the context menu
        if (rightedge < tipobj.offsetWidth)
        //move the horizontal position of the menu to the left by it's width
            tipobj.style.left = ie ? ietruebody().scrollLeft + event.clientX - tipobj.offsetWidth + "px" : window.pageXOffset + e.clientX - tipobj.offsetWidth + "px"
        else if (curX < leftedge)
            tipobj.style.left = "5px"
        else
        //position the horizontal position of the menu where the mouse is positioned
            tipobj.style.left = curX + offsetxpoint + "px"

        //same concept with the vertical position
        if (bottomedge < tipobj.offsetHeight)
            tipobj.style.top = ie ? ietruebody().scrollTop + event.clientY - tipobj.offsetHeight - offsetypoint + "px" : window.pageYOffset + e.clientY - tipobj.offsetHeight - offsetypoint + "px"
        else
            tipobj.style.top = curY + offsetypoint + "px"
        tipobj.style.visibility = "visible"

        enabletip = false;
    }
}

function hideddrivetip() {
    if (ns6 || ie) {
        enabletip = false
        tipobj.style.visibility = "hidden"
        tipobj.style.left = "-1000px"
        tipobj.style.backgroundColor = ''
        tipobj.style.width = ''
    }
}
function abrirVentanaInicio() {
    ventanaAbierta = window.location = "pages/Principal.aspx";
}

$(document).ready(function () {
    $("#txtUsuario,#txtContraseña").keypress(function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            $("#btnLogin").click();
        }
    });
});

function cerrarVentanaInicio() {
    ventanaAbierta.close();
}

function TerminaSession() {
    if (ventanaAbierta)
        cerrarVentanaInicio();

    ShowLogin();
    document.getElementById('btnCloseSession').click();
}

function ShowLogin() {
    document.getElementById('div_login').style.display = 'inline';
    document.getElementById('div_logout').style.display = 'none';
    document.getElementById('txtUsuario').value = '';

}
function ShowLogout() {
    document.getElementById('div_login').style.display = 'none';
    document.getElementById('div_logout').style.display = 'inline';
}

function abrirVentana(url, titulo) {
    window.open(url, titulo, 'top=0,left=0,width=1024, height=720, status=yes');
    window.close();

}

function abrirVentanaNormal(url, titulo) {
    window.open(url, titulo, 'top=0,left=0,width=1024, height=720, status=yes, toolbar=yes, menubar= yes, scrollbars=yes, Location=yes, Resizable=yes');
}


