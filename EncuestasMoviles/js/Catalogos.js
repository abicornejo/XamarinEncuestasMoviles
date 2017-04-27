//frmCatalogo


//ctrlOpcionCatalogo
function ValidaCampo() {
    if (document.getElementById("Body_ctrlOpcionCat_txtNomOpcCat").value == '') {
        alert('El Nombre de la Opción no puede ser vació');
        return;
    }
    else {
        document.getElementById("Body_ctrlOpcionCat_btnAceptar").click();
    }
}

//ctrlNewCatalogo
function ValidaCampoCat() {
    if (document.getElementById("Body_ctrlNewCat_txtNomCat").value == '') {
        alert('El Nombre del Catalogo no puede ser vació');
        return;
    }
    else {
        document.getElementById("Body_ctrlNewCat_btnAceptarCat").click();
    }
}
