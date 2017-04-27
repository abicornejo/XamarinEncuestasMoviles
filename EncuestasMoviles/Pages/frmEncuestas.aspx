<%@ Page Title="" MaintainScrollPositionOnPostback="true" ValidateRequest="false"
    EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterEncuesta.Master"
    AutoEventWireup="true" CodeBehind="frmEncuestas.aspx.cs" Inherits="EncuestasMoviles.Pages.frmEncuestas" %>

<%@ Register TagName="messageBox" TagPrefix="mesBx" Src="~/MessageBox.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<%--<%@ Register TagPrefix="iewc"    Namespace="Microsoft.Web.UI.WebControls"   Assembly="Microsoft.Web.UI.WebControls" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
 
    <asp:UpdatePanel runat="server" ID="upgral">
        <ContentTemplate>  
        
      
           
            <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
            <script src="../Scripts/jquery.metadata.js" type="text/javascript"></script>
            <script src="../Scripts/jquery.dataTables.js" type="text/javascript"></script>
            <script src="../Scripts/ZeroClipboard.js" type="text/javascript"></script>
            <script src="../Scripts/TableTools.js" type="text/javascript"></script>

            <script src="../Scripts/jquery.cookie.js" type="text/javascript"></script>
            <script src="../Scripts/jquery.hotkeys.js" type="text/javascript"></script>
            <script src="../Scripts/jquery.jstree.js" type="text/javascript"></script>


            <link href="../Styles/jquery.dataTables.css" rel="stylesheet" type="text/css" />
            <link href="../Styles/TableTools.css" rel="stylesheet" type="text/css" />
            <link href="../themes/default/style.css" rel="stylesheet" type="text/css" />
              
            <script type="text/javascript">

                $(document).ready(function () {

                 $(".imgPrev").on("click", function (e) {                      

                        
                        $("#Body_lblPreview").die().live("click");
                      
                        e.preventDefault();

                    });
                    $.metadata.setType("class");

                    $("table.grid").each(function () {
                        var grid = $(this);


                        if (grid.find("tbody > tr > th").length > 0) {
                            grid.find("tbody").before("<thead><tr></tr></thead>");
                            grid.find("thead:first tr").append(grid.find("th"));
                            grid.find("tbody tr:first").remove();
                        }


                        if (grid.hasClass("sortable") && grid.find("tbody:first > tr").length > 10) {
                            grid.dataTable({
                                sPaginationType: "full_numbers",
                                aoColumnDefs: [
                                { bSortable: false, aTargets: grid.metadata().disableSortCols }
                            ]
                            });
                        }
                    });


                    $('table.grid').dataTable({
                        "bDestroy": true,
                        "asStripClasses": null,
                        "fnDrawCallback":
			            function () {
			                this.css('width', '100%');
			            },
                        "sPaginationType": "full_numbers",
                        "iDisplayLength": 5,
                        "oLanguage": { "sProcessing": "Procesando, por favor espere...",
                            "sLengthMenu": "Mostrar <select><option value='5'>5</option><option value='10'>10</option><option value='25'>25</option><option value='50'>50</option><option value='100' selected='selected'>100</option></select> registros por p&aacute;gina",
                            "sZeroRecords": "No se encontraron resultados",
                            "sInfo": "&nbsp;&nbsp;Mostrando desde _START_ hasta _END_ de _TOTAL_ registros&nbsp;&nbsp;",
                            "sInfoEmpty": "&nbsp;&nbsp;Mostrando desde 0 hasta 0 de 0 registros&nbsp;&nbsp;",
                            "sInfoFiltered": "<br><em>( filtrado de _MAX_ registros en total )</em>",
                            "sInfoPostFix": "",
                            "sSearch": "Buscar: ",
                            "oPaginate": {
                                "sFirst": "Primero",
                                "sPrevious": "Anterior",
                                "sLast": "Ultimo",
                                "sNext": "Siguiente"
                            }
                        },
                        "sDom": 'Tlfrtip',
                        "oTableTools": {
                            "sSwfPath": "../swf/copy_csv_xls_pdf.swf"
                        }


                    });
                    var idEntryForm = '<%=pnlAddEdit.ClientID  %>';

                   // BlockUI("Body_pnlAddEdit");
                   // $.blockUI.defaults.css = {};

                    $("td.rcbArrowCell").unbind().bind("click", function () {
                        $("div.rcbCheckAllItems").html("<input type='checkbox' class='rcbCheckAllItemsCheckBox'></input>Seleccionar Todos");
                    });

                });

                /*inica test abraham*/

              
                function Hidepopup() {
                    $find("popup").hide();
                    return false;
                }

                /*finaliza test abraham*/
                function muestraGrafica(EncId) {
                    window.open("Grafica.aspx?EncId=" + EncId, "_blank");
                }
                function showMessage_Error(msg) {
                    var o = document.getElementById('divMessage');
                    o.style.display = "block";
                    o.innerHTML = "<table><tr><td><img src='../Images/err.png' alt='Error'/></td><td style='font-size: medium; color: red'><strong>" + msg + "</strong></td></tr></table>";
                    window.setTimeout("hide('divMessage');", 5000);
                }
                function showMessage_Ok(msg) {
                    var o = document.getElementById('divMessage');
                    o.style.display = "block";
                    o.innerHTML = "<table><tr><td><img src='../Images/suc.png' alt='Exito'/></td><td style='font-size: medium; color: green'><strong>" + msg + "</strong></td></tr></table>";
                    window.setTimeout("hide('divMessage');", 5000);
                }
                function chkCrea() {
                    var checks = document.getElementsByTagName('input');
                    document.getElementById("Body_hfchkCrea").value = '';
                    for (var i = 0; i < checks.length; i++) {
                        if (checks[i].type == "radio") {
                            if (checks[i].name == "Radio") {
                                if (checks[i].checked) {
                                    document.getElementById("Body_hfchkCrea").value += checks[i].id;
                                }
                            }
                        }
                    }
                    document.getElementById("Body_btnBuscaEncu").click();
                }
                function limpiacampoEncuesta() {
                    document.getElementById("Body_txtNomEncuesta").value = "";                    
                }
                function limpiacampoPregunta() {
                    document.getElementById("Body_txtPregunta").value = "";
                }
                function limpiacampoProgramacion() {
                    document.getElementById("Body_txtProgramacion").value = "";
                }                
                function limpiacampoRespuesta() {
                    document.getElementById("Body_txtRespuesta").value = "";
//                    document.getElementById("Body_ddlRespuestasSig").selectedIndex = 0;
                }
                function CancelaAlta() {
                    document.getElementById("DivAltaEncuesta").style.display = 'none';
                    document.getElementById("DivBotonAlta").style.display = 'block';
                }
                function NuevaAltaEncu() {
                    document.getElementById("DivAltaEncuesta").style.display = 'block';
                    document.getElementById("DivBotonAlta").style.display = 'none';
                }
                function CambioDia(sender, args) {
                    $find("Body_CalendarExtender2").set_selectedDate(sender._selectedDate);
                }

                /*Inicia Metodos Autocomplete*/

                function acePopulated(sender, e) {

                    var behavior = $find('AutoCompleteEx');

                    var target = behavior.get_completionList();
                    if (behavior._currentPrefix != null) {
                        var prefix = behavior._currentPrefix.toLowerCase();
                        var i;
                        for (i = 0; i < target.childNodes.length; i++) {
                            var sValue = target.childNodes[i].innerHTML.toLowerCase();
                            if (sValue.indexOf(prefix) != -1) {

                                var fstr = target.childNodes[i].innerHTML.substring(0, sValue.indexOf(prefix));
                                var pstr = target.childNodes[i].innerHTML.substring(fstr.length, fstr.length + prefix.length);
                                var estr = target.childNodes[i].innerHTML.substring(fstr.length + prefix.length, target.childNodes[i].innerHTML.length);
                                target.childNodes[i].innerHTML = "<div class='autocomplete-item'>" + fstr + '<B>' + pstr + '</B>' + estr + "</div>";


                            }
                        }
                    }

                }

                function aceSelected(sender, e) {
                    var value = e.get_value();
                    if (!value) {
                        if (e._item.parentElement && e._item.parentElement.tagName == "LI")
                            value = e._item.parentElement.attributes["_value"].value;
                        else if (e._item.parentElement && e._item.parentElement.parentElement.tagName == "LI")
                            value = e._item.parentElement.parentElement.attributes["_value"].value;
                        else if (e._item.parentNode && e._item.parentNode.tagName == "LI")
                            value = e._item.parentNode._value;
                        else if (e._item.parentNode && e._item.parentNode.parentNode.tagName == "LI")
                            value = e._item.parentNode.parentNode._value;
                        else value = "";
                    }
                    var searchText = $get('<%=txtRespuesta.ClientID %>').value;
                    searchText = searchText.replace('null', '');

                    sender.get_element().value = searchText + value;
                }              

                /*Finalizaa Metodos Autocomplete*/


                /* INICIALIZA FUNCION RECURSIVA*/
                /*
                ID_Pregunta: 831
                ID_PreguntaAnterior: 0
                ID_Respuesta: 0
                Pregunta_Desc: "FIN DE LA ENCUESTA"
                Respuesta_Desc                            
                */
                function recursivo(array, idPadre, descPadre, preg_resp) {
                try{
                    var padreHtml = "";
                    var obtieneHijos = new Array();
                    var contadorA = 0;
                    var contadorB = 0;
                    var buscar = 0;
                    $.each(array, function (i, v) {
                        if (preg_resp == 0) {
                            if (v["ID_Pregunta"] == idPadre) {
                                buscar = 1;
                                obtieneHijos[contadorA] = { "hijoId": v["ID_Respuesta"], "hijoDescripcion": v["Respuesta_Desc"] };
                                contadorA++;
                            }
                        }
                        else if (preg_resp == 1) {
                            if (v["ID_Respuesta"] == idPadre) {
                                buscar = 0;
                                var hijoDescripcion = "";
                                var encontre = 0;
                                $.each(array, function (ind, val) {
                                    if (val["ID_Pregunta"] == v["ID_PreguntaAnterior"]) {
                                        if (encontre == 0) {
                                            hijoDescripcion = val["Pregunta_Desc"];
                                        }
                                        encontre++;
                                    }
                                });

                                obtieneHijos[contadorB] = { "hijoId": v["ID_PreguntaAnterior"], "hijoDescripcion": hijoDescripcion };
                                contadorB++;
                            }
                        }
                    });

                    if (obtieneHijos.length > 0) {

                        $.each(obtieneHijos, function (indice, value) {

                            if (value['hijoDescripcion'] == "FIN DE LA ENCUESTA") {
                                padreHtml += "<li rel='fin' id='" + value['hijoId'] + "'><a href='#'>" + value['hijoDescripcion'] + "</a></li>";
                            } else {
                                var rel = " rel='resp' ";
                                if (preg_resp == 1) {
                                    rel = " rel='preg' ";
                                }
                                padreHtml += "<li " + rel + " id='" + value['hijoId'] + "'><a href='#'>" + value['hijoDescripcion'] + "</a><ul>";
                            }

                            if (value['hijoDescripcion'] != "FIN DE LA ENCUESTA") {
                                padreHtml += recursivo(array, value['hijoId'], value['hijoDescripcion'], buscar) + "</ul></li>";
                            }   
                        });                             
                    }

                    return padreHtml;
                } catch (error) {
                   
                    }
                }

                /* FINALIZA FUNCION RECURSIVA*/

                function generarTree(idEncuesta) {
                    try {
                        $('div.modal').show();

                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "frmEncuestas.aspx/obtieneArbol",
                            data: "{idEncueta:'" + idEncuesta + "'}",
                            dataType: "json",
                            async: false,
                            success: function (msg) {
                              
                                var arbol = "";
                                var idpadre = "";
                                var descpadre = "";
                                var preguntasRespuestas = eval(msg.d);
                            
                                if (preguntasRespuestas != null) {
                                    idpadre = preguntasRespuestas[1]["ID_Pregunta"];
                                    descpadre = preguntasRespuestas[1]["Pregunta_Desc"];
                                    var hijos = "";
                                    arbol = "<li id='root' rel='preg'><a href='#'>" + descpadre + "</a><ul>";
                                    $.each(preguntasRespuestas, function (index, value) {

                                        if (value["ID_Pregunta"].toString() == idpadre.toString()) {
                                            hijos += "<li contieneNodo='' rel='resp' buscahijo='1' id=" + value['ID_Respuesta'] + "><a href='#'>" + value['Respuesta_Desc'] + "</a></li>";
                                        }

                                    });
                                    arbol += hijos;
                                    arbol += "</ul></li>";
                                    console.log(arbol);
                                    $("#demo1").jstree('destroy');
                                    $("#demo1").jstree({

                                        "types": {
                                            "max_children": -1,
                                            "max_depth": -1,
                                            "valid_children": "all",
                                            "types": {
                                                "resp": {
                                                    "max_children": -1,
                                                    "max_depth": -1,
                                                    "valid_children": "all",
                                                    "icon": {
                                                        "image": "../Images/mail_flag_kmail.png"
                                                    }
                                                },
                                                "preg": {
                                                    "max_children": -1,
                                                    "max_depth": -1,
                                                    "valid_children": "all",
                                                    "icon": {
                                                        "image": "../Images/question_button.png"
                                                    }
                                                },
                                                "fin": {
                                                    "max_children": -1,
                                                    "max_depth": -1,
                                                    "valid_children": "all",
                                                    "icon": {
                                                        "image": "../Images/stop1.png"
                                                    }
                                                }
                                            }
                                        },
                                        "html_data": {
                                            "data": arbol
                                        },
                                        "plugins": ["themes", "html_data","ui", "crrm","core", "types"]
                                    }).bind("loaded.jstree", function (e, data) {
                                        data.inst.open_all(-1);
                                        $("ins.jstree-icon").html("");
                                        $('div.modal').hide();

                                        $("span#Body_lblPreview").trigger('click');

                                    }).bind("select_node.jstree", function (event, data) {

                                        var rel = data.rslt.obj.attr("buscahijo") == 0 ? "resp" : "preg";
                                        var find = data.rslt.obj.attr("buscahijo");
                                        var idNodo = data.rslt.obj.attr("id");
                                        debugger;
                                        if (data.rslt.obj.find("a").text().trim() != "FIN DE LA ENCUESTA") {
                                            if (data.rslt.obj.attr("contieneNodo") == "") {
                                                $.each(preguntasRespuestas, function (index, value) {
                                                    if (find == 0) {
                                                        if (value['Respuesta_Desc'].toString() == "FIN DE LA ENCUESTA") { rel = "fin" }

                                                        if (value["ID_Pregunta"].toString() == idNodo.toString()) {
                                                            $("#demo1").jstree("create", null, "last", { "attr": { "class": "openli", "rel": rel, "contieneNodo": "", "id": value['ID_Respuesta'], "buscahijo": "1" }, data: value['Respuesta_Desc'] }, false, true);
                                                        }
                                                    } else if (find == 1) {
                                                        if (value["ID_Respuesta"].toString() == idNodo.toString()) {

                                                            var hijoDescripcion = "";
                                                            var encontre = 0;
                                                            $.each(preguntasRespuestas, function (ind, val) {
                                                                if (val["ID_Pregunta"] == value["ID_PreguntaAnterior"]) {
                                                                    if (encontre == 0) {
                                                                        hijoDescripcion = val["Pregunta_Desc"];
                                                                        encontre++;
                                                                    }
                                                                }
                                                            });
                                                            if (hijoDescripcion == "FIN DE LA ENCUESTA") { rel = "fin" }
                                                            $("#demo1").jstree("create", null, "last", { "attr": { "class": "openli", "rel": rel, "contieneNodo": "", "id": value['ID_PreguntaAnterior'], "buscahijo": "0" }, data: hijoDescripcion }, false, true);
                                                        }
                                                    }
                                                });

                                            }
                                            data.rslt.obj.attr("contieneNodo", "si");
                                            $("ins.jstree-icon").html("");
                                            
                                        }


                                    }).delegate("a", "click", function (event, data) {
                                        event.preventDefault();
                                    });

                                } else {
                                    $('div.modal').hide();
                                }
                            }
                        })
                            // $('div.modal').hide();                      
                    } catch (Error) {
                        $('div.modal').hide();
                    } finally {
                       // 
                    }
                }


            </script>
           
           <style type="text/css">
            .modal {
                display:    none;
                position:   fixed;
                z-index:    1000;
                top:        0;
                left:       0;
                height:     100%;
                width:      100%;
                background-color:#000;
                background-image: url('../Images/image-loading.gif');
                background-position: 50% 50%;
                background-repeat: no-repeat;
                opacity: 0.80;
                -ms-filter: progid:DXImageTransform.Microsoft.Alpha(Opacity = 80);
                filter: alpha(opacity = 80);
               };
           </style>
           <div class="modal"></div>
            <div style="display: none">
            
                <asp:HiddenField runat="server" ID="hfchkCrea" />
                <asp:HiddenField runat="server" ID="hfUltimaPos" />
                <asp:HiddenField runat="server" ID="hdnIdEncPreview" />
                
            </div>
            <!--Mensaje Impresion-->           
            <asp:Label ID="lblShowPreviewTree" runat="server" Text=""></asp:Label>
             <mesBx:messageBox runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento" />
            <div style="display: inline-block">
                <asp:Label runat="server" ID="lblEncabezado" Font-Size="X-Large" ForeColor="Black"
                    Text="Encuesta"></asp:Label>
            </div>
            <!--fieldset de controles busqueda de Encuestas-->
            <div class="ContenedorGeneral" style="height: auto; width: 95%">
                <div class="MasterTituloContenedor">
                    <h2>
                        Filtros de Busqueda de Encuestas</h2>
                </div>
                <table style="width: 100%">
                    <tr>
                        <td style="padding-left: 10px">
                            Texto a Buscar:
                        </td>
                        <td>
                            <asp:TextBox CssClass="inputtext" ValidationGroup="grupoBusca" runat="server" ID="txtBusqNombEncu"
                                Width="250px" onkeypress="return validaCaracteresNoEncuestas(event)"></asp:TextBox>
                        </td>
                        <td>
                            Fecha Inicial:
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtBusqFechIni" runat="server" Width="120px"></telerik:RadDatePicker>
                        </td>
                        <td>
                            Fecha Final:
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtBusqFechFin" runat="server" Width="120px"></telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right" style="margin-right: 50px; padding-right: 50px">
                            Por Fecha de Creación:<input type="radio" id="radPorFechCrea" name="Radio" checked="checked" />
                        </td>
                        <td colspan="3" align="left" style="margin-left: -50px; padding-left: 50px">
                            Por Fecha Límite:<input type="radio" id="radPorFechLimit" name="Radio" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" style="height: 50px" colspan="6" align="center">
                            <input type="button" class="btnMasterRectangular" id="btnBusca" onclick="chkCrea()"
                                value="Buscar" />
                            <div style="display: none">
                                <asp:Button runat="server" ID="btnBuscaEncu" CssClass="btnMasterRectangular" Text="Buscar"
                                    OnClick="btnBuscaEncu_Click" ValidationGroup="grupoBusca" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <!--Boton Muestra Alta Encuestas onclick="NuevaAltaEncu()" -->
            <div id="DivBotonAlta">
                <table width="98%">
                    <tr>
                        <td align="right">
                             <input type="button" id="btnAltaEncuNueva" class="btnMasterRectangular" 
                            value="Nueva Encuesta" style=" visibility:hidden;" />
                            <asp:Button style="cursor:pointer;" title="Agregar Nueva Encuesta" class="btnMasterRectangular" ID="btnAdd" runat="server" Text="Nueva Encuesta" OnClick = "Add" />
                        </td>
                    </tr>
                </table>
            </div>            
            <%--div con fieldset de controles para alta de Encuestas--%>
            <div style="text-align: center; display: none" id="DivAltaEncuesta">
                <table class="ContenedorGeneral" style="height: auto; width: 95%;">
                    <tr>
                        <td colspan="2" class="MasterTituloContenedor">
                            <h2>Alta de Encuestas</h2>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Nombre de Encuesta:</td>
                        <td align="left">
                            <asp:TextBox CssClass="inputtext" runat="server" ID="txtNomEncuesta" ValidationGroup="grupoEnc"
                                Width="350px" onkeypress="return validaCaracteresNoEncuestas(event)"  ></asp:TextBox>
                            <asp:ValidatorCalloutExtender runat="server" ID="vcetxtNomEncuesta" TargetControlID="rfvtxtNomEncuesta"
                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                            <asp:RequiredFieldValidator Display="None" ID="rfvtxtNomEncuesta" ControlToValidate="txtNomEncuesta"
                                SetFocusOnError="True" runat="server" ErrorMessage="<div>El nombre de la encuesta es obligatorio</div>"
                                ForeColor="Black" ValidationGroup="grupoEnc"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Fecha Límite:</td>
                        <td align="left"> 
                            <telerik:RadDatePicker ID="txtFechaLimEnc" runat="server" Width="120px"></telerik:RadDatePicker>                                                       
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Hora Límite:</td>
                        <td align="left">
                            <telerik:RadTimePicker ID="txtHoraLimEnc" runat="server" ZIndex="10000000" PopupDirection="TopRight" TimeView-Interval="00:15:00" TimeView-Columns="3" DateInput-ToolTip="Hora final de la encuesta" TimePopupButton-ToolTip="Hora final de la encuesta" TimeView-ToolTip="Hora final de la encuesta" TimeView-TimeFormat="HH:mm"></telerik:RadTimePicker>
                        </td>
                    </tr>
                    <tr>                    
                        <td align="left">Puntos:</td>
                        <td align="left">
                            <asp:TextBox runat="server" Text="0" style="width:60px" ValidationGroup="grupoEnc" ID="txtPuntosEncuesta" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;"></asp:TextBox>
                            <asp:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender1" TargetControlID="rfvtxtPoEncuesta"
                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                            <asp:RequiredFieldValidator Display="None" ID="rfvtxtPoEncuesta" ControlToValidate="txtPuntosEncuesta"
                                SetFocusOnError="True" runat="server" ErrorMessage="<div>Los puntos de la encuesta son obligatorios</div>"
                                ForeColor="Black" ValidationGroup="grupoEnc"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>                    
                        <td align="left">Tipo de encuesta:</td>
                        <td align="left">
                            <asp:DropDownList ID="ddlTipoEncuesta" runat="server" CssClass="styleCombosCat" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                 <tr>
                        <td align="left">Mínimo requerido:</td>
                        <td align="left"><asp:TextBox runat="server" Text="0" ID="txtMinRequ" style="width:60px" ValidationGroup="grupoEnc" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;"></asp:TextBox></td>
                    </tr>
                   <tr>
                        <td align="left">Máximo esperado:</td>
                        <td align="left"><asp:TextBox runat="server" Text="0" ID="txtMaxEsp" style="width:60px" ValidationGroup="grupoEnc" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:UpdatePanel runat="server" ID="upbtnGuardaEnc">
                                <ContentTemplate>
                                    <asp:Button runat="server" CssClass="btnMasterRectangular" ID="btnGuardaNewEncuesta"
                                        ValidationGroup="grupoEnc" Text="Guardar" OnClick="btnGuardaNewEncuesta_Click" />
                                    <input type="button" id="btnLimpia" class="btnMasterRectangular" onclick="limpiacampoEncuesta()"
                                value="Limpiar" />

                                    <input type="button" id="btnCancelarAlta" class="btnMasterRectangular" onclick="CancelaAlta()"
                                value="Cancelar" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>                       
                    </tr>
                </table>                
            </div>
            <%--div con GridView donde muestra las Encuestas Clase someClass --%>
            <%--<div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                <div class="ContenedorGeneral" style="height: auto; width: 95%; position: static;
                    border: 1px solid #3F3F3F;">--%>
                 <div id="contrls" style="width: 100%; height: auto; margin-bottom:20px; padding-bottom:20px; background-color: #FFFFFF; text-align: center">
                        <asp:GridView CssClass="grid sortable {disableSortCols: [4]}" runat="server" ID="gvEncuestas" ForeColor="#333333"
                            GridLines="Vertical" AutoGenerateColumns="false" Width="100%" OnRowCommand="gvEncuestas_RowCommand"
                            DataKeyNames="IdEncuesta" OnRowEditing="gvEncuestas_RowEditing" OnRowUpdating="gvEncuestas_RowUpdating"
                            
                            >
                            <Columns >
                               <asp:TemplateField HeaderText="Encuestas" Visible="false" ItemStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNombreEncuestaGv" ValidationGroup="grupoEncuestaGv" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" runat="server"
                                            Text='<%# Eval("NombreEncuesta") %>'></asp:TextBox>
                                        <asp:ValidatorCalloutExtender runat="server" ID="vcetxtNombreEncuestaGv" TargetControlID="rfvtxtNombreEncuestaGv"
                                            Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="rfvtxtNombreEncuestaGv" ControlToValidate="txtNombreEncuestaGv"
                                            SetFocusOnError="True" runat="server" ErrorMessage="<div>El nombre de la encuesta es obligatorio</div>"
                                            ForeColor="Black" ValidationGroup="grupoEncuestaGv"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Pun.Encuesta" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox runat="server" style="width:60px" ID="txtPointsEncu" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" ValidationGroup="grupoEncuestaGv" Text='<%# Eval("PuntosEncuesta") %>'></asp:TextBox>
                                        <asp:ValidatorCalloutExtender runat="server" ID="vcetxtPuntosEncuestaGv" TargetControlID="rfvtxtPuntosEncuestaGv"
                                            Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="rfvtxtPuntosEncuestaGv" ControlToValidate="txtPointsEncu"
                                            SetFocusOnError="True" runat="server" ErrorMessage="<div>Los puntos  de la encuesta son obligatorios</div>"
                                            ForeColor="Black" ValidationGroup="grupoEncuestaGv"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Encuesta" DataField="NombreEncuesta"/>
                                <asp:BoundField HeaderText="Puntos Encuesta" DataField="PuntosEncuesta" HeaderStyle-Width="10" />
                                <asp:BoundField ReadOnly="true" HeaderText="Creada" DataField="FechaCreaEncuesta"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Fecha Límite" Visible="false">                                   
                                    <ItemTemplate>
                                             <telerik:RadDatePicker ID="txtFechaLimiteGV" runat="server" Width="120px" DbSelectedDate='<%# DataBinder.Eval(Container.DataItem,"FechaLimiteEncuesta") %>'></telerik:RadDatePicker>                                  
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:BoundField HeaderText="Fecha Límite" DataField="FechaLimiteEncuesta" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                 <asp:TemplateField HeaderText="Horas Límite" Visible="false">                                   
                                    <ItemTemplate>
                                            <telerik:RadTimePicker ID="txtHoraLimiteGV" runat="server" ZIndex="10000000" Width="120px" PopupDirection="TopRight" TimeView-Interval="00:15:00" TimeView-Columns="3" DateInput-ToolTip="Hora final de la encuesta" TimePopupButton-ToolTip="Hora final de la encuesta" TimeView-ToolTip="Hora final de la encuesta" TimeView-TimeFormat="HH:mm" DbSelectedDate='<%# DataBinder.Eval(Container.DataItem,"HoraLimiteEncuesta") %>'></telerik:RadTimePicker>                                            
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:BoundField HeaderText="Hora Límite" DataField="HoraLimiteEncuesta" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField ReadOnly="true" HeaderText="Estatus" DataField="NombreEstatus" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Tipo Encuesta" DataField="DescIdTipoEnc" Visible="true"/>
                                <asp:TemplateField HeaderText="Min.Req." Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox style="width:60px" runat="server" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" ID="txtEditMinReq" ValidationGroup="grupoEncuestaGv" Text='<%# Eval("MinimoRequerido") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:BoundField HeaderText="Tipo Encuesta" DataField="IdTipoEnc" Visible="false"/>
                                <asp:TemplateField HeaderText="Max.Esp." Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox style="width:60px" runat="server" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" ID="txtEditMaxEsp" ValidationGroup="grupoEncuestaGv" Text='<%# Eval("MaximoEsperado") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operaciones" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="210px" >
                                    <ItemTemplate>
                                        <asp:ImageButton Width="22px" ToolTip="Agregar Preguntas" ImageUrl="~/Images/iconoagregar.png"
                                            Style="text-align: center" ID="btnAgregaPregunta" CausesValidation="false" UseSubmitBehavior="true"
                                            runat="server" CommandName="Agrega" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton Width="22px" ToolTip="Editar Encuesta" ImageUrl="~/Images/iconoeditar.png"
                                            Style="text-align: center" ID="lnkEdit" runat="server" OnClick = "Edit"  />
                                        <asp:ImageButton Visible="false" Width="22px" ToolTip="Editar Encuesta" ImageUrl="~/Images/iconoeditar.png"
                                            Style="text-align: center" ID="ImageButton5" CausesValidation="false" UseSubmitBehavior="true"
                                            CommandName="edit" runat="server" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton Width="22px" ToolTip="Eliminar Encuesta" ImageUrl="~/Images/iconoeliminar.png"
                                            Style="text-align: center" ID="btnEliminaEncuesta" CausesValidation="false" CommandName="Elimina"
                                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" runat="server" />
                                        <asp:ImageButton Width="22px" ToolTip="Publicar Encuesta" ImageUrl="~/Images/iconopublicar.png"
                                            Style="text-align: center" ID="btnPublica" CausesValidation="false" runat="server"
                                            CommandName="Publicar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton Width="22px" ToolTip="Visualizar Grafica" ImageUrl="~/Images/iconograficar.png"
                                            Style="text-align: center" ID="btnGraficaEncuesta" CausesValidation="false" runat="server"
                                            CommandName="Grafica" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton CssClass="imgPrev" CommandName="Preview" Width="22px" ToolTip="Preview Encuesta" ImageUrl="~/Images/iconovistaorevia.png"
                                            Style="text-align: center" ID="btnPreview" CausesValidation="false" runat="server"
                                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton Width="22px" ToolTip="Copiar Encuesta" ImageUrl="~/Images/icono_copiar30x30.png"
                                            Style="text-align: center" ID="btnCopia" CausesValidation="false" runat="server"
                                            CommandName="Copiar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton Width="22px" ToolTip="Programación Encuesta" ImageUrl="~/Images/icono_copiar30x30.png"
                                        Style="text-align:center" ID="btnProgramacion" CausesValidation="false" UseSubmitBehavior="true"
                                        runat="server" CommandName="Programacion" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton Width="22px" ToolTip="Reenviar Encuesta" ImageUrl="~/Images/icono_copiar30x30.png"
                                        Style="text-align:center" ID="btnReenviar" CausesValidation="false" UseSubmitBehavior="true"
                                        runat="server" CommandName="Reenvio" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                        <asp:ImageButton Width="22px" ToolTip="Cancelar Encuesta" ImageUrl="~/Images/icono_copiar30x30.png"
                                        Style="text-align:center" ID="btnCancelar" CausesValidation="false" UseSubmitBehavior="true"
                                        runat="server" CommandName="Cancelacion" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                       <%-- <asp:LinkButton ID="lnkEdit" runat="server" Text = "Edit" OnClick = "Edit"></asp:LinkButton>--%>
                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    ControlStyle-CssClass="btnMasterRectangular" ValidationGroup="grupoEncuestaGv"
                                    Visible="false" EditText="Editar" CancelText="Cancelar" UpdateText="Actualizar"
                                    ButtonType="Button" ShowEditButton="true">
                                </asp:CommandField>
                                <asp:BoundField HeaderText="ID" DataField="IdEncuesta" Visible="false"/>
                            </Columns>
                          <%--  <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <FooterStyle BackColor="#A9D0F5" Font-Bold="True" ForeColor="Black" />
                            <HeaderStyle CssClass="headerGrid" />
                            <PagerStyle CssClass="headerGrid" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowsGrid" BorderColor="#c3cecc" BackColor="#F0F5FF" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                        </asp:GridView>


                        <!-- TEST ABI EMPIEZA class modalPopup-->

                         <asp:Panel ID="pnlAddEdit" runat="server" CssClass="ContenedorGeneral" style = "display:none;width: 500px; height:250px;">
                            <asp:Panel runat="server" ID="pnlTituloAddEdit" CssClass="MasterTituloContenedor">
                                <table width="100%" id="Table1" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left"><h2 runat="server" id="headEditAddEnch2">.:: Edita Encuesta ::.</h2></td>
                                        <td align="right">
                                            <asp:ImageButton runat="server" ID="ImageButton4" ImageUrl="~/Images/Iconocerrar.png"
                                              OnClientClick = "return Hidepopup()" />
                                        </td>
                                    </tr>
                                </table> 
                            </asp:Panel>
                         
                            <br />
                            <div>
                                <div style="display: inline-block; padding: 0 0 10px 10px;">
                                    <h2>Por favor de llenar los datos correspondientes.</h2>
                                </div>
                                <div style="display: inline-block; padding: 0 0 10px -10px;">
                                    <h2><asp:Label runat="server" ID="lblDescEnc"></asp:Label></h2>
                                </div>
                            </div>
                            <center>
                            <table border="1">
	                            <tr>
		                            <td colspan="2">Nombre Encuesta:</td>
		                            <td colspan="2" align="left" >                                   
                                        <asp:TextBox ID="txtnomEncID" ValidationGroup="ValidaCampoEnc" style=" width:97%;" Width = "90px"  runat="server"></asp:TextBox>
                                        <asp:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender2" TargetControlID="RFVtxtnomEncID"
                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="RFVtxtnomEncID" ControlToValidate="txtnomEncID"
                                SetFocusOnError="True" runat="server" ErrorMessage="<div>El nombre de la encuesta es obligatorio</div>"
                                ForeColor="Black" ValidationGroup="ValidaCampoEnc"></asp:RequiredFieldValidator>
                                    
                                    </td>
	                            </tr>
	                            <tr>
		                            <td>Fecha Limite:</td>
                                    <td>
                                        <telerik:RadDatePicker DateInput-ReadOnly="true" ValidationGroup="ValidaCampoEnc" ZIndex="10000000"  ID="fechLimiteEncID" runat="server" Width="120px"></telerik:RadDatePicker>  
		                                 <asp:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5" TargetControlID="RFVfechLimiteEncID"
                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="RFVfechLimiteEncID" ControlToValidate="fechLimiteEncID"
                                SetFocusOnError="True" runat="server" ErrorMessage="<div>La fecha limite es obligatoria</div>"
                                ForeColor="Black" ValidationGroup="ValidaCampoEnc"></asp:RequiredFieldValidator>
                                    </td>
		                            <td>Hora Limite:</td>
                                    <td>
                                      <telerik:RadTimePicker ValidationGroup="ValidaCampoEnc"  ID="horaLimitID" runat="server" ZIndex="10000000" PopupDirection="TopRight" TimeView-Interval="00:15:00" TimeView-Columns="3" DateInput-ToolTip="Hora final de la encuesta" TimePopupButton-ToolTip="Hora final de la encuesta" TimeView-ToolTip="Hora final de la encuesta" TimeView-TimeFormat="HH:mm"></telerik:RadTimePicker>
		                            
                                    <asp:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender6" TargetControlID="RFVhoraLimitID"
                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                    <asp:RequiredFieldValidator Display="None" ID="RFVhoraLimitID" ControlToValidate="horaLimitID"
                                SetFocusOnError="True" runat="server" ErrorMessage="<div>La Hora limite es obliogatoria</div>"
                                ForeColor="Black" ValidationGroup="ValidaCampoEnc"></asp:RequiredFieldValidator>
                                    
                                    </td>
	                            </tr>
	                            <tr>
		                            <td>Puntos Encuesta:</td>
		                            <td>
                                        <asp:TextBox ID="txtPuntosEncID" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" ValidationGroup="ValidaCampoEnc" Width = "40px"  runat="server"></asp:TextBox>
                                         <asp:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender3" TargetControlID="RFVtxtPuntosEncID"
                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="RFVtxtPuntosEncID" ControlToValidate="txtPuntosEncID"
                                SetFocusOnError="True" runat="server" ErrorMessage="<div>Los puntos de la encuesta son obligatorios</div>"
                                ForeColor="Black" ValidationGroup="ValidaCampoEnc"></asp:RequiredFieldValidator>
                                    </td>
		                            <td>Tipo Encuesta:</td>
		                            <td>
                                        <asp:DropDownList ValidationGroup="ValidaCampoEnc" runat="server" ID="cbTipoEncID" AutoPostBack="false"></asp:DropDownList>
                                         <asp:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender4" TargetControlID="RFVcbTipoEncID"
                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="RFVcbTipoEncID" ControlToValidate="cbTipoEncID"
                                SetFocusOnError="True" runat="server" ErrorMessage="<div>El tipo de encuesta es obligatorio</div>"
                                ForeColor="Black" ValidationGroup="ValidaCampoEnc"></asp:RequiredFieldValidator>
                                    </td>
	                            </tr>
                                <tr>
                                     <td colspan="4" align="center">
                                        <asp:Button ID="btnSave" runat="server" ValidationGroup="ValidaCampoEnc" Text="Guardar" OnClick = "Save"/>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClientClick = "return Hidepopup()"/>
                                     </td>
                                </tr>
                            </table>
                            <asp:HiddenField runat="server" ID="modalIdEnc" />
                            <asp:HiddenField runat="server" ID="modalEditorAdd" />
                            </center>

                        </asp:Panel>
                        <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                        <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                            PopupControlID="pnlAddEdit" TargetControlID = "lnkFake" PopupDragHandleControlID="pnlTituloAddEdit"
                            BackgroundCssClass="modalBackground">
                        </asp:ModalPopupExtender>


                        <!-- TEST ABI FINALIZA-->

                    </div>
               <%-- </div>
            </div>   --%>        
            <%--Modal Programaciones--%>
            
            <div style="display: none">
                <asp:Button runat="server" ID="btnProgramacion" Text="Agrega Programacion" />
                <asp:Button runat="server" ID="btnCancelarModalProgramaciones" OnClick="btnCancelarModalProgramaciones_Click" />
            </div>
            <div>
                <asp:Label ID="lblEncProgramacion" runat="server" Text=""></asp:Label>
                <asp:ModalPopupExtender ID="mpeProgramaciones" runat="server" DropShadow="true" BackgroundCssClass="modalBackground"
                    BehaviorID="bhavMod5" PopupControlID="pnl5" TargetControlID="lblEncProgramacion" PopupDragHandleControlID="pnlH5"
                    CancelControlID="btnCancelarModalProgramaciones" />
            </div>
            <div>
                <asp:Panel runat="server" ID="pnl5" CssClass="ContenedorGeneral" Style="display: none;
                    width: 750px; height:450px;">
                    <asp:Panel runat="server" ID="pnlH5" CssClass="MasterTituloContenedor">
                        <table width="100%" id="PopupHeader5" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left"><h2>.::Programaciones ::.</h2></td>
                                <td align="right"><asp:ImageButton runat="server" ID="btnCerrar5" ImageUrl="~/Images/Iconocerrar.png"
                                    OnClick="btnCerrar5_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>                        
                    </asp:Panel>
                    <div style="margin-top: 25px">
                        <div>
                            <div>
                                <div style="display: inline-block; padding: 0 0 10px 10px;">
                                    <h2>Encuesta:</h2>
                                </div>
                                <div style="display: inline-block; padding: 0 0 10px -10px;">
                                    <h2><asp:Label runat="server" ID="lblEncuesta5"></asp:Label></h2>
                                </div>
                            </div>
                            <div style="text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%">
                                    <div class="MasterTituloContenedor">
                                        <h2>
                                            Alta de Programaciones
                                        </h2>
                                    </div>
                                    <div style="display: inline-block; color: Black; padding: 5px 0 15px 0">
                                        Programación:
                                    </div>
                                    <div style="display: inline-block; padding: 5px 0 15px 0">
                                        <asp:TextBox CssClass="inputtext" ValidationGroup="grupoProgramacion" CausesValidation="true"
                                            onkeypress="return validaCaracteres(event)" runat="server" ID="txtProgramacion">
                                        </asp:TextBox>
                                    </div>
                                    <asp:ValidatorCalloutExtender runat="server" ID="vceProgramacion" TargetControlID="rfvProgramacion"
                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                    <asp:RequiredFieldValidator ValidationGroup="grupoProgramacion" Display="None" ID="rfvProgramacion"
                                        ControlToValidate="txtProgramacion" SetFocusOnError="True" runat="server" ErrorMessage="<div>La programación es obligatoria</div>"
                                        ForeColor="Black"></asp:RequiredFieldValidator>

                                    <div style="display: inline-block; color: Black; padding: 5px 0 15px 0">
                                        Tipo Programación:
                                    </div>
                                    <div style="display: inline-block; padding: 5px 0 15px 0">
                                        <asp:DropDownList ID="cbTipoProgramacion" runat="server"></asp:DropDownList>
                                    </div>
                                   <%-- <div style="display: inline-block; color: Black; padding: 5px 0 15px 0">
                                        Dispositivo asignar:
                                    </div>--%>
                                   <%-- <div style="display: inline-block; padding: 5px 0 15px 0">
                                        <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1">
                                            <telerik:RadComboBox ID="cbDispoToProgramate" runat="server" CheckBoxes="true" ZIndex="10000000"
                                                Width="250" Label="Seleccion de encuesta:" Text="textoooo" Height="400" Skin="Metro">                                                                       
                                            </telerik:RadComboBox>                                                    
                                        </telerik:RadAjaxPanel>
                                    </div>--%>
                                    <div style="display: inline-block; padding: 5px 0 15px 0">
                                        <asp:Button class="btnMasterRectangular" runat="server" ValidationGroup="grupoProgramacion"
                                            ID="btnGuardaProgramacion" Text="Guardar" OnClick="btnGuardaProgramacion_Click" />
                                    </div>
                                    <div style="display: inline-block">
                                        <input type="button" class="btnMasterRectangular" onclick="limpiacampoProgramacion();"
                                            id="btnCancelaProgramacion" value="Limpiar" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                            <div class="ContenedorGeneral" style="width: 95%; height: auto; position: static">
                                <div id="Div5" style="width: 100%; height: 280px; text-align: center">
                                     <asp:GridView ID="gvProgramaciones" runat="server" AllowPaging="True" 
                AutoGenerateColumns="false" CellPadding="2" CssClass="someClass" 
                DataKeyNames="IdProgramacion" ForeColor="#333333" GridLines="Vertical" 
                OnPageIndexChanging="gvProgramaciones_PageIndexChanging" 
                onrowcancelingedit="gvProgramaciones_RowCancelingEdit" 
                OnRowCommand="gvProgramaciones_RowCommand" 
                onrowediting="gvProgramaciones_RowEditing" 
                onrowupdating="gvProgramaciones_RowUpdating" PageSize="8" Width="100%">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField ControlStyle-Width="100%" HeaderText="PROGRAMACIÓN" 
                        Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNomProgramacionGV" runat="server" 
                                Text='<%# Eval("ProgramacionNombre") %>' ValidationGroup="grupoProgramacionGV"></asp:TextBox>
                            <asp:ValidatorCalloutExtender ID="vcetxtNomProgramacionGV" runat="server" 
                                CssClass="CustomValidatorCalloutStyle" Enabled="True" 
                                TargetControlID="rfvtxtNomProgramacionGV" Width="350px" />
                            <asp:RequiredFieldValidator ID="rfvtxtNomProgramacionGV" runat="server" 
                                ControlToValidate="txtNomProgramacionGV" Display="None" 
                                ErrorMessage="&lt;div&gt;El nombre de la programación es obligatorio&lt;/div&gt;" 
                                ForeColor="Black" SetFocusOnError="True" 
                                ValidationGroup="grupoProgramacionGV" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProgramacionNombre" 
                        HeaderStyle-HorizontalAlign="Center" HeaderText="PROGRAMACIÓN" 
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="DESCTIPOPROGRAMACION" HeaderStyle-HorizontalAlign="Center" 
                        HeaderText="TIPO PROGRAMACIÓN" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="ProgramacionEstatus" 
                        HeaderStyle-HorizontalAlign="Center" HeaderText="ESTADO" 
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" 
                        HeaderText="Operaciones" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAddConfiguracion" runat="server" 
                                CausesValidation="false" 
                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" 
                                CommandName="AddConfiguracion" ImageUrl="~/Images/iconoagregar.png" 
                                ToolTip="Configurar Programación" Width="22px" />
                            <asp:ImageButton ID="btnEdita" runat="server" CausesValidation="false" 
                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" CommandName="edit" 
                                ImageUrl="~/Images/iconoeditar.png" ToolTip="Editar Programación" 
                                Width="22px" />
                            <asp:ImageButton ID="btnElimina" runat="server" CausesValidation="false" 
                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" 
                                CommandName="Elimina" ImageUrl="~/Images/iconoeliminar.png" 
                                ToolTip="Eliminar Programación" Width="22px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Button" CancelText="Cancelar" 
                        ControlStyle-CssClass="btnMasterRectangular" EditText="Editar" 
                        ShowEditButton="true" UpdateText="Guardar" 
                        ValidationGroup="grupoProgramacionGV" Visible="false" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <FooterStyle BackColor="#A9D0F5" Font-Bold="True" ForeColor="Black" />
                <HeaderStyle CssClass="headerGrid" />
                <PagerStyle CssClass="headerGrid" HorizontalAlign="Center" />
                <RowStyle BackColor="#F0F5FF" BorderColor="#c3cecc" CssClass="RowsGrid" 
                    ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <%--Fin Modal Programaciones--%>
            <%--Modal Encuestas--%>
            <div style="display: none">
                <asp:Button runat="server" ID="btnPregunta" Text="Agrega Pregunta" />
                <asp:Button runat="server" ID="btnCancelarModalPreguntas" OnClick="btnCancelarModalPreguntas_Click" />
            </div>
            <div>
                <asp:Label ID="lblEncPreg" runat="server" Text=""></asp:Label>
                <asp:ModalPopupExtender ID="mpePreguntas" runat="server" DropShadow="true" BackgroundCssClass="modalBackground"
                    BehaviorID="bhavMod" PopupControlID="pnl1" TargetControlID="lblEncPreg" PopupDragHandleControlID="pnlH1"
                    CancelControlID="btnCancelarModalPreguntas" />
            </div>
            <div>
                <asp:Panel runat="server" ID="pnl1" CssClass="ContenedorGeneral" Style="display: none;
                    width: 750px; height:450px;">
                    <asp:Panel runat="server" ID="pnlH1" CssClass="MasterTituloContenedor">
                        <table width="100%" id="PopupHeader" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left"><h2>.::Preguntas ::.</h2></td>
                                <td align="right"><asp:ImageButton runat="server" ID="btnCerrar" ImageUrl="~/Images/Iconocerrar.png"
                                    OnClick="btnCerrar_Click" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>                        
                    </asp:Panel>
                    <div style="margin-top: 25px">
                        <div>
                            <div>
                                <div style="display: inline-block; padding: 0 0 10px 10px;">
                                    <h2>Encuesta:</h2>
                                </div>
                                <div style="display: inline-block; padding: 0 0 10px -10px;">
                                    <h2><asp:Label runat="server" ID="lblEncuesta"></asp:Label></h2>
                                </div>
                            </div>
                            <div style="text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%">
                                    <div class="MasterTituloContenedor">
                                        <h2>
                                            Alta de Preguntas
                                        </h2>
                                    </div>
                                    <div style="display: inline-block; color: Black; padding: 5px 0 15px 0">
                                        Pregunta:
                                    </div>
                                    <div style="display: inline-block; padding: 5px 0 15px 0">
                                        <asp:TextBox CssClass="inputtext" ValidationGroup="grupoPregunta" CausesValidation="true"
                                            onkeypress="return validaCaracteres(event)" runat="server" ID="txtPregunta">
                                        </asp:TextBox>
                                    </div>
                                    <asp:ValidatorCalloutExtender runat="server" ID="vcePregu" TargetControlID="rfvPregu"
                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                    <asp:RequiredFieldValidator ValidationGroup="grupoPregunta" Display="None" ID="rfvPregu"
                                        ControlToValidate="txtPregunta" SetFocusOnError="True" runat="server" ErrorMessage="<div>La pregunta es obligatoria</div>"
                                        ForeColor="Black"></asp:RequiredFieldValidator>
                                    <div style="display: inline-block; color: Black; padding: 5px 0 15px 0">
                                        Tipo Respuesta:
                                    </div>
                                     <div style="display: inline-block; padding: 5px 0 15px 0">
                                        <asp:DropDownList ID="cbTipoPregunta" runat="server"></asp:DropDownList>
                                     </div>

                                     <div style="display: inline-block; color: Black; padding: 5px 0 15px 0">
                                        Respuestas Aleatorias:
                                    </div>
                                     <div style="display: inline-block; padding: 5px 0 15px 0">
                                        <asp:DropDownList runat="server" ID="cbRestriccionResp"></asp:DropDownList>
                                     </div>


                                    <div style="display: inline-block; padding: 5px 0 15px 0">
                                       
                                    </div>
                                    <div style="display: inline-block; vertical-align: middle">
                                     <asp:Button class="btnMasterRectangular" runat="server" ValidationGroup="grupoPregunta"
                                            ID="btnGuardaPregunta" Text="Guardar" OnClick="btnGuardaPregunta_Click" />
                                         
                                        <input type="button" class="btnMasterRectangular" onclick="limpiacampoPregunta();"
                                            id="btnCancelaPregunta" value="Limpiar" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                            <div class="ContenedorGeneral" style="width: 95%; height: auto; position: static">
                                <div id="Div1" style="width: 100%; height: 280px; text-align: center">
                                    <asp:GridView CssClass="someClass" ID="gvPreguntas" runat="server" CellPadding="2"
                                        ForeColor="#333333" GridLines="Vertical" Width="100%" AutoGenerateColumns="false"
                                        OnRowCommand="gvPreguntas_RowCommand" DataKeyNames="IdPregunta" OnRowCancelingEdit="gvPreguntas_RowCancelingEdit"
                                        OnRowEditing="gvPreguntas_RowEditing" OnRowUpdating="gvPreguntas_RowUpdating"
                                        AllowPaging="True" PageSize="8" OnPageIndexChanging="gvPreguntas_PageIndexChanging">
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="PREGUNTA" ControlStyle-Width="100%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ValidationGroup="grupoPreguntaGV" ID="txtDescPreguntaGV"
                                                        Text='<%# Eval("PreguntaDesc") %>'></asp:TextBox>
                                                    <asp:ValidatorCalloutExtender runat="server" ID="vcetxtDescPreguntaGV" TargetControlID="rfvtxtDescPreguntaGV"
                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                    <asp:RequiredFieldValidator ValidationGroup="grupoPreguntaGV" Display="None" ID="rfvtxtDescPreguntaGV"
                                                        ControlToValidate="txtDescPreguntaGV" SetFocusOnError="True" runat="server" ErrorMessage="<div>La pregunta es obligatoria</div>"
                                                        ForeColor="Black" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="PreguntaDesc" HeaderText="PREGUNTA" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="DescResp" HeaderText="TIPO RESPUESTA" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center" />
                                            
                                            <asp:TemplateField HeaderText="TIPO RESPUESTA" ControlStyle-Width="100%" Visible="false">
                                                <ItemTemplate>                                                    
                                                    <asp:DropDownList ID="cbEditTipoResp" runat="server" AutoPostBack="false"></asp:DropDownList>
                                                   <%-- <asp:Panel runat="server" ID="pnlCombos" ></asp:Panel> --%>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                                                                        
                                            <asp:BoundField ReadOnly="true" Visible="true" DataField="DescRespAleatorias" HeaderText="Resp. Aleatoria" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Resp. Aleatoria" ControlStyle-Width="100%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="cbEditRespAlea" AutoPostBack="false"></asp:DropDownList>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>  
                                            <asp:BoundField ReadOnly="true" DataField="Estatus" HeaderText="ESTADO" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="Operaciones" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton Width="22px" ID="btnAddRespu" ImageUrl="~/Images/iconoagregar.png"
                                                        ToolTip="Agregar Respuesta" CausesValidation="false" runat="server" CommandName="AddRespu"
                                                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                                    <asp:ImageButton Width="22px" ID="btnEdita" ImageUrl="~/Images/iconoeditar.png" CausesValidation="false"
                                                        ToolTip="Editar Pregunta" runat="server" CommandName="edit" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                                    <asp:ImageButton Width="22px" ID="btnElimina" ImageUrl="~/Images/iconoeliminar.png"
                                                        ToolTip="Eliminar Pregunta" CausesValidation="false" runat="server" CommandName="Elimina"
                                                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ControlStyle-CssClass="btnMasterRectangular" ValidationGroup="grupoPreguntaGV"
                                                CancelText="Cancelar" ButtonType="Button" Visible="false" EditText="Editar" UpdateText="Guardar"
                                                ShowEditButton="true" />   
                                            <asp:BoundField ReadOnly="true" Visible="false" DataField="IdResp" HeaderText="re" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" />     
                                                                              
                                            <%--<asp:TemplateField HeaderText="tipo" ControlStyle-Width="100%" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ValidationGroup="grupoPreguntaGV" ID="txtTipoRespEdita"
                                                        Text='<%# Eval("IdResp") %>'></asp:TextBox>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>--%>

                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <FooterStyle BackColor="#A9D0F5" Font-Bold="True" ForeColor="Black" />
                                        <HeaderStyle CssClass="headerGrid" />
                                        <PagerStyle CssClass="headerGrid" HorizontalAlign="Center" />
                                        <RowStyle CssClass="RowsGrid" BorderColor="#c3cecc" BackColor="#F0F5FF" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
           
            <%--Fin Modal Encuestas--%>

            <%--Inicia Modal Preview--%>
                <div>
                <div>
                    <div style="display: none">
                        <asp:Button runat="server" ID="CancelButtonPreview" />
                        <asp:Label ID="lblPreview" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:ModalPopupExtender ID="mpePreview" runat="server" TargetControlID="lblPreview"
                        PopupControlID="PanelPreview" BackgroundCssClass="modalBackground" CancelControlID="CancelButtonPreview"
                        DropShadow="true" PopupDragHandleControlID="pnlPreviewHead" />
                </div>
                <div>
                    <asp:Panel ID="PanelPreview" runat="server" CssClass="ContenedorGeneral" Style="display: none"
                        Width="760px">
                        <asp:Panel ID="pnlPreviewHead" CssClass="MasterTituloContenedor" runat="server" Width="100%">
                            <div id="Div4">
                                <div style="float: left;">
                                    <h2 id="headPreview">
                                        Vista Previa Encuesta
                                    </h2>
                                </div>
                                <div style="float: right;">
                                    <asp:ImageButton CausesValidation="false" runat="server" ID="ImageButton1" ImageUrl="~/Images/Iconocerrar.png" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="margin-top: 25px">
                            <div>
                                <div style="width: 100%;">
                                    <div style="width: 95%">
                                        <div id="miArbol">
                                            <div style="display: none">
                                                <asp:Label runat="server" ID="Label3" />
                                            </div>
                                        </div>
                                        <div>
                                           <table width="100%" align="center" border="0">
                                               <tr>
                                                    <td>
                                                        <div  style="width:750px; height:600px; overflow: scroll; display:none;" >
                                                            
                                                            <telerik:RadTreeView runat="server" ID="radPreviewTree" Visible="false">
                                                            
                                                            </telerik:RadTreeView>                                                            
                                                              <asp:TreeView id="tvControl" runat="server" EnableViewState="False"  Visible="false">
                                                            </asp:TreeView>
                                                        </div>
                                                        <div id="demo1" class="demo" style="width:750px;height:600px;overflow: scroll; display:block;">
                                                         
                                                        </div>                                              
                                                    </td>
                                               </tr>
                                           </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <%--Finaliza Modal Preview--%>

            <%--Modal Programacion Detalle Fecha--%>
                <div>
                    <div>
                        <div style="display: none">
                            <asp:Button runat="server" ID="CancelDetFecha" />
                            <asp:Label ID="LblProgramacionFecha" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:ModalPopupExtender ID="mpeProgramacionDetFecha" runat="server" TargetControlID="lblProgramacionFecha"
                            PopupControlID="pnlProgDetFecha" BackgroundCssClass="modalBackground" CancelControlID="CancelDetFecha"
                            DropShadow="true" PopupDragHandleControlID="pnlProgDetFechaHead" />
                    </div>
                <div>
                    <asp:Panel ID="pnlProgDetFecha" runat="server" CssClass="ContenedorGeneral" Style="display: none"
                        Width="760px">
                        <asp:Panel ID="pnlProgDetFechaHead" CssClass="MasterTituloContenedor" runat="server"
                            Width="100%">
                            <div id="Div6">
                                <div style="float: left;">
                                    <h2>
                                        Programación de Encuesta Por Fecha
                                    </h2>
                                </div>
                                <div style="float: right;">
                                    <asp:ImageButton CausesValidation="false" runat="server" ID="btnCerrarProgramacionFecha"
                                        ImageUrl="~/Images/Iconocerrar.png" OnClick="btnCerrarProgramacionFecha_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="margin-top: 25px">
                            <div>
                                <div style="width: 100%;">
                                    <div style="width: 95%">
                                        <div>
                                            <div style="display: inline-block;">
                                                <h2>
                                                    Programación:</h2>
                                            </div>
                                            <div style="display: inline-block;">
                                                <h2>
                                                    <asp:Label runat="server" ID="LblProgramacionXFecha" Text="Programación de la encuesta"></asp:Label></h2>
                                            </div>
                                        </div>
                                        <div>
                                            <div style="display: none">
                                                <asp:Label runat="server" ID="LblIdProgramacionF" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: left;width: 100%; vertical-align:top">
                                     <div style="width: 95%;">
                                         <div style="display: inline-block; padding: 5px 0 5px 0">
                                            <h2>                                                                                           
                                                <asp:CheckBox ID="CheckBTipoCarga" runat="server" 
                                                Text="Capturar una sola fecha" AutoPostBack="true"
                                                oncheckedchanged="CheckBTipoCarga_CheckedChanged" Checked="true" />  
                                            </h2>
                                         </div>     
                                     </div>
                                </div> 
                                <div style="text-align: center;width: 100%;">                                    
                                    <div class="ContenedorGeneral" style="height: auto; width: 95%; text-align:left">
                                        <div class="MasterTituloContenedor">
                                            <h2>
                                                Alta de fechas en la Programación:
                                            </h2>                                                                                                                                                                
                                        </div>
                                        <div>
                                            <br />
                                            <br />
                                        </div>   
                                        <div style="text-align: right;width: 98%">
                                                <div style="display:inline-block; vertical-align:middle">
                                                    <asp:Label ID="LblMsg" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                                                </div>                                                
                                                <div style="display: inline-block; vertical-align:middle">
                                                    <asp:Button runat="server" ID="btnAgregarHoras" CssClass="btnMasterRectangular"
                                                    Text="Asignar hora" Visible="false" onclick="btnAgregarHoras_Click"/>
                                                </div>
                                                <div style="display: inline-block; vertical-align:middle">
                                                    <asp:Button runat="server" ID="BtnGuardarProgFec" CssClass="btnMasterRectangular"
                                                    Text="Guardar" onclick="BtnGuardarProgFec_Click"/>
                                                </div>                                               
                                        </div>
                                        <div style="text-align: center;width:98%">                                                                                                                        
                                            <div style="display: inline-block">
                                                <asp:Label ID="LblFecha" runat="server" Text="Fecha: " Visible="true"></asp:Label>
                                            </div>
                                            <div style="display: inline-block">
                                                <telerik:RadDatePicker ID="RadDatePFechaProg" runat="server" Width="120px" ZIndex="10000000" Visible="true"></telerik:RadDatePicker>      
                                            </div>
                                            <div style="display: inline-block">
                                                <asp:Label ID="LblHora" runat="server" Text="Hora: " Visible="true"></asp:Label>
                                            </div>
                                            <div style="display: inline-block">
                                                <telerik:RadTimePicker ID="RadTimePHoraProg" runat="server" ZIndex="10000000" PopupDirection="TopRight" TimeView-Interval="00:30:00" TimeView-Columns="3" DateInput-ToolTip="Hora de programación" TimePopupButton-ToolTip="Hora de programación" TimeView-ToolTip="Hora de programación" TimeView-TimeFormat="HH:mm" Visible ="true"></telerik:RadTimePicker>    
                                            </div>
                                        </div> 
                                        <div style="vertical-align:middle;width:98%">
                                            <div style="display:inline-block" align="left">  
                                                <asp:Label ID="LblTitSelecFechas" runat="server" Visible="false" Text="Selecciona las fechas:"></asp:Label>                                                                                                                                
                                            </div>
                                        </div>
                                        <div style="vertical-align:middle;width:98%">
                                            <div style="display:inline-block">  
                                                    <telerik:RadCalendar ID="RadCalProgXFecha" runat="server" TitleFormat="MMMM yyyy" Visible="false">
                                                    </telerik:RadCalendar>   
                                            </div>
                                            <div style="display: inline-block; vertical-align:middle">                                                
                                                    <div>
                                                        <asp:CheckBox ID="CheckBHoras" runat="server" Checked="true" Visible="false" AutoPostBack="true"
                                                        oncheckedchanged="CheckBHoras_CheckedChanged" Text="Misma hora para todas las fechas"  />  
                                                    </div>
                                                    <div>
                                                        <br />
                                                    </div>                                                     
                                                    <div style="display: inline-block">
                                                        <asp:Label ID="LblFechaProgXFecha1" runat="server" Visible="false" Text="Fecha: "></asp:Label>    
                                                    </div>
                                                    <div style="display: inline-block">
                                                        <asp:Label ID="LblFechaProgXFecha2" runat="server" Visible="false" Text=""></asp:Label>    
                                                    </div>
                                                    <div style="display: inline-block">
                                                        <asp:Label ID="LblHoraProgFechas" runat="server" Visible="false" Text="    Hora: "></asp:Label>    
                                                    </div>
                                                    <div style="display: inline-block">
                                                        <telerik:RadTimePicker ID="RadTimePHora" runat="server" ZIndex="10000000" PopupDirection="TopRight" TimeView-Interval="00:30:00" TimeView-Columns="3" DateInput-ToolTip="Hora de programación" TimePopupButton-ToolTip="Hora de programación" TimeView-ToolTip="Hora de programación" TimeView-TimeFormat="HH:mm" Visible ="false"></telerik:RadTimePicker>
                                                    </div>                                        
                                            </div>
                                        </div>
                                        <div>
                                            <br />
                                        </div>   
                                    </div>                                   
                                </div>
                            </div>                                
                        </div>
                        <div>
                            <br />
                        </div>
                        <div>
                            <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%; position: static;
                                    border: 1px solid #3F3F3F;">
                                    <div id="Div8" style="width: 100%; height: 220px; background-color: #F2F2F2; overflow: auto;
                                        text-align: center">
                                        <asp:GridView CssClass="someClass" GridLines="Vertical" ID="gvProgXFecha" runat="server"
                                            CellPadding="4" ForeColor="#333333" Width="100%" AutoGenerateColumns="false"
                                            DataKeyNames="IdProgXFecha" 
                                            onrowcancelingedit="gvProgXFecha_RowCancelingEdit" 
                                            onrowcommand="gvProgXFecha_RowCommand"                                             
                                            onrowediting="gvProgXFecha_RowEditing" 
                                            onrowupdating="gvProgXFecha_RowUpdating" 
                                            onselectedindexchanged="gvProgXFecha_SelectedIndexChanged">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="50%" />
                                            <Columns>
                                                <asp:BoundField DataField="Fecha" HeaderText="FECHA" />
                                                <%--<asp:TemplateField HeaderText="RESPUESTA" ControlStyle-Width="100%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtDescRespuestaGV" Text='<%# Eval("RespuestaDescripcion") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField HeaderText="HORA" DataField="Hora" />
                                                <%--<asp:TemplateField HeaderText="SIGUIENTE PREGUNTA" ControlStyle-Width="100%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlRespuesta">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField HeaderText="ESTATUS" DataField="Estatus" />
                                                <asp:TemplateField HeaderText="Operaciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton Width="22px" ImageUrl="~/Images/iconoeliminar.png" ID="btnEliminaResp"
                                                            ToolTip="Eliminar Fecha" runat="server" CausesValidation="false" CommandName="Elimina"
                                                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <FooterStyle BackColor="#A9D0F5" Font-Bold="True" ForeColor="Black" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <PagerStyle CssClass="headerGrid" HorizontalAlign="Center" />
                                            <RowStyle CssClass="RowsGrid" BorderColor="#c3cecc" BackColor="#F0F5FF" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div>
                                    <br />
                                </div>
                            </div>
                        </div>                     
                    </asp:Panel>
                </div>
            </div>
            <%--Fin Modal Programacion Detalle Fecha--%>

            <%--Modal Programacion Detalle Semana--%>
                <div>
                <div>
                    <div style="display: none">
                        <asp:Button runat="server" ID="CancelDetSemana" />
                        <asp:Label ID="LblProgramacionSemana" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:ModalPopupExtender ID="mpeProgramacionDetSemana" runat="server" TargetControlID="lblProgramacionSemana"
                        PopupControlID="pnlProgDetSemana" BackgroundCssClass="modalBackground" CancelControlID="CancelDetSemana"
                        DropShadow="true" PopupDragHandleControlID="pnlProgDetSemanaHead" />
                </div>
                <div>
                    <asp:Panel ID="pnlProgDetSemana" runat="server" CssClass="ContenedorGeneral" Style="display: none"
                        Width="760px">
                        <asp:Panel ID="PnlProgDetSemanaHead" CssClass="MasterTituloContenedor" runat="server"
                            Width="100%">
                            <div id="Div7">
                                <div style="float: left;">
                                    <h2>
                                        Programación de Encuesta Por Semana
                                    </h2>
                                </div>
                                <div style="float: right;">
                                    <asp:ImageButton CausesValidation="false" runat="server" ID="btnCerrarProgramacionSemana"
                                        ImageUrl="~/Images/Iconocerrar.png" OnClick="btnCerrarProgramacionSemana_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="margin-top: 25px">
                            <div>
                                <div style="width: 100%;">
                                    <div style="width: 95%">
                                        <div>
                                            <div style="display: inline-block;">
                                                <h2>
                                                    Programación:</h2>
                                            </div>
                                            <div style="display: inline-block;">
                                                <h2>
                                                    <asp:Label runat="server" ID="LblProgramacionXSemana" Text="Programación de la encuesta"></asp:Label></h2>
                                            </div>
                                        </div>
                                        <div>
                                            <div style="display: none">
                                                <asp:Label runat="server" ID="LblIdProgramacionS" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: center;width: 100%;">                                    
                                    <div class="ContenedorGeneral" style="height: auto; width: 95%; text-align:left">
                                        <div class="MasterTituloContenedor">
                                            <h2>
                                                Alta de Programación Semanal:
                                            </h2>                                                                                                                                                                
                                        </div>
                                        <div>
                                            <br />
                                            <br />
                                        </div>   
                                        <div style="text-align: right;width: 98%">
                                                <div style="display:inline-block; vertical-align:middle">
                                                    <asp:Label ID="LblMsgSem" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div style="display: inline-block; vertical-align:middle">
                                                    <asp:Button runat="server" ID="BtnAsignar" CssClass="btnMasterRectangular"
                                                    Text="Asignar hora" Visible="false" onclick="BtnAsignar_Click"/>
                                                </div>
                                                <div style="display: inline-block; vertical-align:middle">
                                                    <asp:Button runat="server" ID="BtnGuardarProgSem" CssClass="btnMasterRectangular"
                                                    Text="Guardar" onclick="BtnGuardarProgSem_Click"/>
                                                </div>                                              
                                        </div>                                                                              
                                        <div style="text-align: left;width: 98%">
                                            <div style="display: inline-block">                        
                                                  <asp:CheckBox ID="CheckBMismaHoraSem" runat="server"
                                                      Text="Misma hora para los días seleccionados" AutoPostBack="true"
                                                        oncheckedchanged="CheckBMismaHoraSem_CheckedChanged"  Checked="true"/>
                                            </div>
                                        </div>
                                        <div>
                                            <br />
                                            <br />
                                        </div>  
                                        <div style="text-align: left;width: 98%">
                                              <div style="display: inline-block">
                                                <asp:Label ID="LblDiasSem" runat="server" Visible="true" Text="Selecciona los días de la semana: "></asp:Label>
                                              </div>
                                        </div>
                                        <div style="text-align: left;width:98%">                                                                                
                                            <div style="display: inline-block">
                                                <asp:CheckBoxList ID="CheckBoxLDiasSemana" runat="server" 
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Sabado">Sábado</asp:ListItem>
                                                    <asp:ListItem>Domingo</asp:ListItem>
                                                    <asp:ListItem>Lunes</asp:ListItem>
                                                    <asp:ListItem>Martes</asp:ListItem>
                                                    <asp:ListItem Value="Miercoles">Miércoles</asp:ListItem>
                                                    <asp:ListItem>Jueves</asp:ListItem>
                                                    <asp:ListItem>Viernes</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </div>                                            
                                        </div> 
                                        <div style="text-align: left;width: 98%">
                                              <div style="display: inline-block">
                                                <br />
                                                <br />
                                              </div>
                                        </div>
                                        <div style="text-align: left;width: 98%">                                             
                                              <div style="display: inline-block">
                                                    <div style="display: inline-block">
                                                        <asp:Label ID="LblEtiqDia" runat="server" Text="Día: " Visible="false"></asp:Label>
                                                    </div>
                                                    <div style="display: inline-block">
                                                        <asp:Label ID="LblDia" runat="server" Text="" Visible="false"></asp:Label>
                                                    </div>
                                                    <div style="display: inline-block">
                                                        <asp:Label ID="LblHoraSem" runat="server" Text="   Hora: " Visible="true"></asp:Label>
                                                    </div>
                                                    <div style="display: inline-block">
                                                        <telerik:RadTimePicker ID="RadTimePHoraSem" runat="server" ZIndex="10000000" PopupDirection="TopRight" TimeView-Interval="00:30:00" TimeView-Columns="3" DateInput-ToolTip="Hora de programación" TimePopupButton-ToolTip="Hora de programación" TimeView-ToolTip="Hora de programación" TimeView-TimeFormat="HH:mm" Visible ="true"></telerik:RadTimePicker>    
                                                    </div>
                                              </div>
                                        </div>                                        
                                        <div style="text-align: left;width: 98%">
                                              <div style="display: inline-block">
                                                <br />
                                              </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <br />
                        </div>
                        <div>
                            <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%; position: static;
                                    border: 1px solid #3F3F3F;">
                                    <div id="Div9" style="width: 100%; height: 220px; background-color: #F2F2F2; overflow: auto;
                                        text-align: center">
                                            <asp:GridView CssClass="someClass" GridLines="Vertical" ID="gvProgXSemana" runat="server"
                                            CellPadding="4" ForeColor="#333333" Width="100%" AutoGenerateColumns="false"
                                            DataKeyNames="IdProgXSemana" 
                                                onrowcancelingedit="gvProgXSemana_RowCancelingEdit" 
                                                onrowcommand="gvProgXSemana_RowCommand"                                                 
                                                onrowediting="gvProgXSemana_RowEditing" 
                                                onrowupdating="gvProgXSemana_RowUpdating" 
                                                onselectedindexchanged="gvProgXSemana_SelectedIndexChanged" >
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="50%" />
                                                <Columns>
                                                    <asp:BoundField DataField="Dia" HeaderText="DIA" />
                                                    <%--<asp:TemplateField HeaderText="RESPUESTA" ControlStyle-Width="100%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtDescRespuestaGV" Text='<%# Eval("RespuestaDescripcion") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="HORA" DataField="Hora" />
                                                    <%--<asp:TemplateField HeaderText="SIGUIENTE PREGUNTA" ControlStyle-Width="100%" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:DropDownList runat="server" ID="ddlRespuesta">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="ESTATUS" DataField="Estatus" />
                                                    <asp:TemplateField HeaderText="Operaciones">
                                                        <ItemTemplate>
                                                            <asp:ImageButton Width="22px" ImageUrl="~/Images/iconoeliminar.png" ID="btnEliminaResp"
                                                                ToolTip="Eliminar Dia" runat="server" CausesValidation="false" CommandName="Elimina"
                                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <FooterStyle BackColor="#A9D0F5" Font-Bold="True" ForeColor="Black" />
                                                <HeaderStyle CssClass="headerGrid" />
                                                <PagerStyle CssClass="headerGrid" HorizontalAlign="Center" />
                                                <RowStyle CssClass="RowsGrid" BorderColor="#c3cecc" BackColor="#F0F5FF" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <%--Fin Modal Programacion Detalle Semana--%>

            <%--Modal Respuestas--%>
                <div>
                <div>
                    <div style="display: none">
                        <asp:Button runat="server" ID="CancelButton" />
                        <asp:Label ID="lblEncResp" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:ModalPopupExtender ID="mpeEncuestaRespuesta" runat="server" TargetControlID="lblEncResp"
                        PopupControlID="pnlRespuestas" BackgroundCssClass="modalBackground" CancelControlID="CancelButton"
                        DropShadow="true" PopupDragHandleControlID="pnlRespuestasHead" />
                </div>
                <div>
                    <asp:Panel ID="pnlRespuestas" runat="server" CssClass="ContenedorGeneral" Style="display: none"
                        Width="760px">
                        <asp:Panel ID="pnlRespuestasHead" CssClass="MasterTituloContenedor" runat="server"
                            Width="100%">
                            <div id="Div2">
                                <div style="float: left;">
                                    <h2>
                                        Respuesta de Encuesta</h2>
                                </div>
                                <div style="float: right;">
                                    <asp:ImageButton CausesValidation="false" runat="server" ID="btnCerrarRespuestas"
                                        ImageUrl="~/Images/Iconocerrar.png" OnClick="btnCerrarRespuestas_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="margin-top: 25px">
                            <div>
                                <div style="width: 100%;">
                                    <div style="width: 95%">
                                        <div>
                                            <div style="display: inline-block;">
                                                <h2>
                                                    Pregunta:</h2>
                                            </div>
                                            <div style="display: inline-block;">
                                                <h2>
                                                    <asp:Label runat="server" ID="lblRespuPreg" Text="Pregunta de la encuesta"></asp:Label></h2>
                                            </div>
                                        </div>
                                        <div>
                                            <div style="display: none">
                                                <asp:Label runat="server" ID="lblIdPregunta" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div style="text-align: center">
                                    <div class="ContenedorGeneral" style="height: auto; width: 95%;">
                                        <div class="MasterTituloContenedor">
                                            <h2>
                                                Alta de Respuesta</h2>
                                        </div>
                                        <div style="display: inline-block; padding: 5px 0 5px 0">
                                            Respuesta:
                                        </div>
                                        <div style="display: inline-block">
                                            <asp:TextBox CssClass="inputtext" ValidationGroup="grupoRespuesta" runat="server"
                                                ID="txtRespuesta"  onkeypress="return validaCaracteresNoEncuestas(event)" />
                                            <div ID="divwidth"></div>
                                            <asp:AutoCompleteExtender
                                                runat="server" 
                                                ID="AutoComplete1"
                                                BehaviorID="autoComplete" 
                                                TargetControlID="txtRespuesta"
                                                ServiceMethod="findRespFrecuentes"
                                                MinimumPrefixLength="1" 
                                                CompletionInterval="10"
                                                EnableCaching="true"
                                                CompletionSetCount="12"
                                                CompletionListCssClass="AutoExtender"
                                                CompletionListItemCssClass="AutoExtenderList"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListElementID="divwidth"
                                                DelimiterCharacters="; "
                                                >
                                            </asp:AutoCompleteExtender>                                            
                                        </div>
                                        <asp:ValidatorCalloutExtender runat="server" ID="vceRespuesta" TargetControlID="rfvRespuestas"
                                            Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="rfvRespuestas" ControlToValidate="txtRespuesta"
                                            SetFocusOnError="True" runat="server" ErrorMessage="<div>La respuesta es obligatoria</div>"
                                            ForeColor="Black" ValidationGroup="grupoRespuesta"></asp:RequiredFieldValidator>

                                        <div style="display: inline-block">
                                            Siguiente Pregunta:
                                        </div>
                                        <div style="display: inline-block">
                                            <asp:DropDownList runat="server" ID="ddlRespuestasSig" />
                                        </div>
                                        <div>
                                            <br />
                                        </div>
                                        <div>
                                            <div style="display: inline-block">
                                                <asp:Button runat="server" ID="btnGuardaRespuesta" CssClass="btnMasterRectangular"
                                                    Text="Guardar" ValidationGroup="grupoRespuesta" OnClick="btnGuardaRespuesta_Click" /></div>
                                            <div style="display: inline-block; vertical-align: middle">
                                                <input class="btnMasterRectangular" type="button" id="btnCancelaRespuesta" onclick="limpiacampoRespuesta();"
                                                    value="Limpiar" /></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%; position: static;
                                    border: 1px solid #3F3F3F;">
                                    <div id="Div3" style="width: 100%; height: 220px; background-color: #F2F2F2; overflow: auto;
                                        text-align: center">
                                        <asp:GridView CssClass="someClass" GridLines="Vertical" ID="gvRespuestasGral" runat="server"
                                            CellPadding="4" ForeColor="#333333" Width="100%" AutoGenerateColumns="false"
                                            OnRowCommand="gvRespuestasGral_RowCommand" DataKeyNames="IdRespuesta,IdSiguientePregunta"
                                            OnRowEditing="gvRespuestasGral_RowEditing" OnRowCancelingEdit="gvRespuestasGral_RowCancelingEdit"
                                            OnRowDataBound="gvRespuestasGral_RowDataBound" OnRowUpdating="gvRespuestasGral_RowUpdating">
                                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="50%" />
                                            <Columns>
                                                <asp:BoundField DataField="RespuestaDescripcion" HeaderText="RESPUESTA" />
                                                <asp:TemplateField HeaderText="RESPUESTA" ControlStyle-Width="100%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtDescRespuestaGV" Text='<%# Eval("RespuestaDescripcion") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="SIGUIENTE PREGUNTA" DataField="DescSigPreg" />
                                                <asp:TemplateField HeaderText="SIGUIENTE PREGUNTA" ControlStyle-Width="100%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlRespuesta">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Operaciones">
                                                    <ItemTemplate>
                                                        <asp:ImageButton Width="22px" ImageUrl="~/Images/iconoeditar.png" ID="btnEditaResp"
                                                            ToolTip="Editar Respuesta" runat="server" CausesValidation="false" CommandName="edit"
                                                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                                        <asp:ImageButton Width="22px" ImageUrl="~/Images/iconoeliminar.png" ID="btnEliminaResp"
                                                            ToolTip="Eliminar Respuesta" runat="server" CausesValidation="false" CommandName="Elimina"
                                                            CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField CausesValidation="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                    ControlStyle-CssClass="btnMasterRectangular" Visible="false" EditText="Editar"
                                                    CancelText="Cancelar" UpdateText="Actualizar" ButtonType="Button" ShowEditButton="true">
                                                </asp:CommandField>
                                            </Columns>
                                            <EditRowStyle BackColor="#999999" />
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <FooterStyle BackColor="#A9D0F5" Font-Bold="True" ForeColor="Black" />
                                            <HeaderStyle CssClass="headerGrid" />
                                            <PagerStyle CssClass="headerGrid" HorizontalAlign="Center" />
                                            <RowStyle CssClass="RowsGrid" BorderColor="#c3cecc" BackColor="#F0F5FF" ForeColor="#333333" />
                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <%--Fin Modal Respuesta--%>

            <%--Modal Grafica Encuesta--%>
                <div>
                <div>
                    <div style="display: none">
                        <asp:Button runat="server" ID="CancelButtonGrafica" />
                        <asp:Label ID="lblGraficaEncu" runat="server" Text=""></asp:Label>
                    </div>
                    <asp:ModalPopupExtender ID="mpeGraficaEncuesta" runat="server" TargetControlID="lblGraficaEncu"
                        PopupControlID="pnlGrafica" BackgroundCssClass="modalBackground" CancelControlID="CancelButtonGrafica"
                        DropShadow="true" PopupDragHandleControlID="pnlGraficaHead" />
                </div>
                <div>
                    <asp:Panel ID="pnlGrafica" runat="server" CssClass="ContenedorGeneral" Style="display: none;
                        width: 1100px; height: 550px">
                        <asp:Panel ID="pnlGraficaHead" runat="server" Width="100%" CssClass="MasterTituloContenedor">
                            <div id="dvGraficaTit">
                                <div style="float: left">
                                    <h2>
                                        <asp:Label runat="server" ID="lblTitleGraficaEncu" Text="Grafica Encuesta" ForeColor="White">
                                        </asp:Label>
                                    </h2>
                                </div>
                                <div style="float: right">
                                    <asp:ImageButton CausesValidation="false" runat="server" ID="btnCancelGraficaEncu"
                                        ImageUrl="~/Images/Iconocerrar.png" OnClick="btnCancelGraficaEncu_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div>
                            <asp:Button runat="server" ID="exportPDF" Text="Exportar PDF" OnClick="exportPDF_Click"
                                CausesValidation="false" />
                            <asp:Button runat="server" ID="btnAtras" Text="Atras" CausesValidation="false" OnClick="btnAtras_Click"
                                Visible="false" />
                        </div>
                        <div id="divTituloGraf" runat="server" style="text-align: center">
                        </div>
                        <div style="margin-top: 25px; width: 800px; height: 330px;">
                            <div id="divDatos" runat="server" style="font-size: medium">
                            </div>
                            <telerik:RadChart ID="RadChart1" SkinsOverrideStyles="true" runat="server" Width="879px"
                                Height="326px">
                                <PlotArea>
                                    <XAxis MaxValue="5" MinValue="1" Step="1">
                                    </XAxis>
                                    <YAxis MaxValue="3" Step="0.5" AxisMode="Extended">
                                    </YAxis>
                                    <YAxis2 MaxValue="5" MinValue="1" Step="1">
                                    </YAxis2>
                                </PlotArea>
                                <Series>
                                </Series>
                            </telerik:RadChart>
                        </div>
                    </asp:Panel>
                </div>
            </div>
            <%--Fin Modal Grafica Encuesta--%>

            <%-- Modal Reenvio de Encuesta --%>
                
                <div>
                    <div>
                        <div style="display: none">
                            <asp:Button runat="server" ID="CancelMdlReenvio" />
                            <asp:Label ID="lblShowMdlReenvio" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:ModalPopupExtender ID="ModalReenvioEncuesta" runat="server" TargetControlID="lblShowMdlReenvio"
                            PopupControlID="PanelReenvioEncuesta" BackgroundCssClass="modalBackground" CancelControlID="CancelMdlReenvio"
                            DropShadow="true" PopupDragHandleControlID="PanelReenvioEncHead" />
                    </div>
                    <div>
                        <asp:Panel ID="PanelReenvioEncuesta" runat="server" CssClass="ContenedorGeneral" Style="display: none"
                            Width="350px" Height="220px" >
                            <asp:Panel ID="PanelReenvioEncHead" CssClass="MasterTituloContenedor" runat="server" Width="100%">
                                <div id="Div10">
                                    <div style="float: left;">
                                        <h2 id="h1TitleReenvio" runat="server">
                                           REENVIO DE ENCUESTA
                                        </h2>
                                    </div>
                                    <div style="float: right;">
                                        <asp:ImageButton CausesValidation="false" runat="server" ID="ImageButton2" ImageUrl="~/Images/Iconocerrar.png" />
                                    </div>
                                </div>
                            </asp:Panel>
                            <div style="margin-top: 25px">
                                <div>
                                    <div style="width: 100%;">
                                        <div style="width: 95%">
                                            <div id="Div11">
                                                <div style="display: none">
                                                    <asp:Label runat="server" ID="Label2"  />
                                                    <asp:HiddenField runat="server" ID="idEncHiden" />
                                                </div>
                                            </div>
                                            <div>
                                                <center><h3 style=" color:Red;">Seleccione los dispositivos a los cuales desea reenviar la encuesta.</h3></center>
                                                <telerik:RadComboBox ID="listaDispositivos" runat="server" Width="200" Height="100"
                                                    EmptyMessage="Seleccion de Dispositivos" ZIndex="10000000"  CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
                                                </telerik:RadComboBox>
                                                <telerik:RadButton ID="btnReenvioEnc" runat="server" Text="Reenviar Encuesta" OnClick="btnReenvioEnc_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>

            <%-- Finaliza Modal Reenvio de Encuesta --%>

            <%-- INICIA MODAL CANCELAR ENCUESTA --%>

                <div>
                    <div>
                        <div style="display: none">
                            <asp:Button runat="server" ID="CancelMdlCancelEnc" />
                            <asp:Label ID="lblShowMdlCancelEnc" runat="server" Text=""></asp:Label>
                        </div>
                        <asp:ModalPopupExtender ID="ModalCancelaEncuesta" runat="server" TargetControlID="lblShowMdlCancelEnc" PopupControlID="PanelCancelaEncuesta" BackgroundCssClass="modalBackground" CancelControlID="CancelMdlCancelEnc"
                         DropShadow="true" PopupDragHandleControlID="PanelCancelarEncHead">
                        </asp:ModalPopupExtender>                    
                    </div>
                    <div>
                        <asp:Panel ID="PanelCancelaEncuesta" runat="server" CssClass="ContenedorGeneral" Style="display: none"
                            Width="350px" Height="220px">
                            <asp:Panel ID="PanelCancelarEncHead" CssClass="MasterTituloContenedor" runat="server" Width="100%">
                                <div id="Div12">
                                    <div style="float: left;">
                                        <h2 id="h1" runat="server">
                                           CANCELACION DE ENCUESTA
                                        </h2>
                                    </div>
                                    <div style="float: right;">
                                        <asp:ImageButton CausesValidation="false" runat="server" ID="ImageButton3" ImageUrl="~/Images/Iconocerrar.png" />
                                    </div>
                                </div>                            
                            </asp:Panel>
                             <div style="margin-top: 25px">
                                <div>
                                    <div style="width: 100%;">
                                        <div>
                                           <%-- <h1>Esta es la cancelacion de la encuesta</h1>--%>
                                            <center><h3 style=" color:Red;">Seleccione los dispositivos a los cuales desea cancelar la encuesta.</h3></center>
                                                <telerik:RadComboBox ID="lstDispoTocancel" runat="server" Width="200" Height="100"
                                                    EmptyMessage="Seleccion de Dispositivos" ZIndex="10000000"  CheckBoxes="true" EnableCheckAllItemsCheckBox="true">
                                                </telerik:RadComboBox>
                                                <telerik:RadButton ID="btnCancelenc" runat="server" Text="Cancelar Encuesta" OnClick="btnCancelEnc_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>                    
                    </div>
                
                </div>
       
            <%-- FINALIZA MODAL CANCELAR ENCUESTA --%>
           
            <input type="hidden" id="hdnIdEncuesta" runat="server" />
           
        </ContentTemplate>
       <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID = "gvEncuestas" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID = "btnSave" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
