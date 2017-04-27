function showMessage_Error(titulo, msg) {
    debugger;
    var tit = document.getElementById('divTitulo');
    tit.innerHTML = titulo;
    var o = document.getElementById('divMessage');
    o.style.display = "block";
    o.innerHTML = "<table><tr><td><img src='../Images/err.png' alt='Error'/></td><td style='font-size: medium; color: red'><strong>" + msg + "</strong></td></tr></table>";
}

function showMessage_Ok(titulo, msg) {
    debugger;
    var tit = document.getElementById('divTitulo');
    tit.innerHTML = titulo;
    var o = document.getElementById('divMessage');
    o.style.display = "block";
    o.innerHTML = "<table><tr><td><img src='../Images/suc.png' alt='Exito'/></td><td style='font-size: medium; color: green'><strong>" + msg + "</strong></td></tr></table>";
}
function showMessage_Info(titulo, msg) {
    debugger;
    var tit = document.getElementById('divTitulo');
    tit.innerHTML = titulo;
    var o = document.getElementById('divMessage');
    o.style.display = "block";
    o.innerHTML = "<table><tr><td><img src='../Images/att.png' alt='Message'/></td><td style='font-size: medium; color: blue'><strong>" + msg + "</strong></td></tr></table>";
    //            $find('mpeMensajes').show();

}