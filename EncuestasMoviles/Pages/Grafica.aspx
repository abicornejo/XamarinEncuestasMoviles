<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grafica.aspx.cs" Inherits="EncuestasMoviles.Pages.Grafica" %>
<%@ Register TagName="messageBox" TagPrefix="mesBx" Src="~/MessageBox.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>.:: Grafica ::.</title>     
     <link rel="stylesheet" type="text/css" href="../Styles/Site.css" />   
    <style type="text/css">
         .info, .exito, .alerta, .error {
               font-family:Arial, Helvetica, sans-serif; 
               font-size:13px;
               border: 1px solid;
               margin: 10px 0px;
               padding:15px 10px 15px 50px;
               background-repeat: no-repeat;
               background-position: 10px center;
               position:relative;
        }
        .info {
                color:#000000;
               background-color: #BDE5F8;
               background-image: url('../Images/info.png');
        }
        .exito {
               color: #4F8A10;
               background-color: #DFF2BF;
               background-image:url('../Images/exito.png');
        }
        .alerta {
               /*color: #FFFF00;*/
               color:#000000;
               background-color: #FEEFB3;
               background-image: url('../Images/alerta.png');
              
        }
        .error {
               color:#000000;
               background-color: #FFBABA;
               background-image: url('../Images/error.png');
        }
        .autocomplete_completionListElement 
        {  
	        margin : 0px!important;
	        background-color : inherit;
	        color : windowtext;
	        border : buttonshadow;
	        border-width : 1px;
	        border-style : solid;
	        cursor : 'default';
	        overflow : auto;
	        height : auto;
            text-align : left; 
            list-style-type :  none;
            padding-left:1px;
        }
        
        .textoRespuestas
        {
	        font-family:Arial;
	        font-size:12px;
	        font-weight:bold;            
        }
        
        .tituloEncuesta
        {
	        font-family:Arial;
	        font-size:12px;
	        font-weight:bold;            
        }
        
        .textoEncuesta
        {
	        font-family:Arial;
	        font-size:12px;	        
        }

        /* AutoComplete highlighted item */
        .autocomplete_highlightedListItem
        {
	        background-color:Aqua;/*#aaff90*/
	        color: black;
	        padding: 1px;
	        cursor:hand;
        }

        /* AutoComplete item */
        .autocomplete_listItem 
        {
	        background-color : window;
	        color : windowtext;
	        padding:1px;
        }
        
         /*autocomplete 2*/
        
         .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left:10px;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        #divwidth
        {
          width: 150px !important;    
        }
        #divwidth div
       {
        width: 150px !important;   
       }      
      
        .btnMasterRectangular
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
            color: #29549F;
            font-weight: bold;
            text-align: center;
           /* text-shadow: 0px 1px 0px #FFFFFF;*/
            min-width: 58px;
            margin: 0px;
            padding: 0px 5px;
            height: 21px;
            border: 1.5px solid #969696; /* Firefox 3.6 */
            -ms-filter: "progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr='#E5E5E5', endColorstr='#C9C9C9')";
            background-image: -webkit-gradient(linear,left bottom,left top,color-stop(0, #E5E5E5),color-stop(1, #C9C9C9)); /* Safari & Chrome */
        }
        h2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #053E5E;
            margin: 5px 0 0 10px;
            padding: 0;
            font-weight: bold;
            text-align: left;
        }        
        .ContenedorGeneral
        {
            width: 100%;
            border: 2px solid #3F3F3F;
            float: none;
            margin: 0 auto;
            background-color: #FFFFFF;
            color: #505050;
            clear: both;
        }        
        .MasterTituloContenedor
        {
            float: left;
            height: 22px;
            width: 100%;
            border: 0px solid #3F3F3F;
            border-bottom: 2px solid #3F3F3F;
            padding: 0;
            background-color: #D0D0D0;
        }
        .inputtext
        {
            border: 1px solid #505050;
            margin-left: 0px;
        }
        .styleCombosCat
        {}
        
       body
        {
	        color:#505050;
	        background-color:#025F8F;
	        background-image:url(../Images/Body.png);/*Images/Body.png*/
	        background-repeat:no-repeat;
	        background-position:center;
	        margin:auto;
	        float:none;
	        width:1024px;
	        height:768px;
        }
      
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
       }
     
    .accordion{}.accordion dd,.accordion dt
    {
       /* padding:10px;*/
        border:1px solid #000;
        border-bottom:0;
        margin-left: 0px;
         font-family:Arial;
        font-weight:bold;
    }.accordion dd:last-of-type,.accordion dt:last-of-type
    {
        border-bottom:1px solid #000
    }.accordion dd a,.accordion dt a
    {
        display:block;
        color:#000;
        font-weight:bold;
        background: white;
       
    }.accordion dd
    {
        border-top:0;font-size:12px
    }.accordion dd:last-of-type
    {
        border-top:1px solid #fff;
        position:relative;
        top:-1px
    }a
    {
        text-decoration:none
    }
  
 
  
    </style>
  
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/financial-data.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.bar.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.bipolar.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.annotate.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.context.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.core.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.dynamic.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.effects.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.key.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.resizing.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.tooltips.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.common.zoom.js" type="text/javascript"></script>
    <script src="../Scripts/Graphics/RGraph.cornergauge.js" type="text/javascript"></script>
   
    <script src="../Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.hotkeys.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.jstree.js" type="text/javascript"></script>
    <link href="../themes/default/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">


        function showMensaje(tipoMensaje, textMensaje) {
            $(".mensajes").removeClass("exito alerta error info").addClass(tipoMensaje.toString()).html(textMensaje.toString()).fadeIn(500).fadeOut(7000);
        }
       
        function ObtenerCatalogos() {

            $.ajax({
                type: "POST",
                url: "Grafica.aspx/ObtenerCatalogos",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                dataType: "json",
                async: false,
                success: function (datos) {
                    var datosCatalogo = jQuery.parseJSON(datos.d);
                    if (datosCatalogo != null) {
                        var html = "<dl class='accordion'>";
                        var contador = 0;
                        $.each(datosCatalogo, function (index, value) {
                            html += "<dt><a href='javascript:void(0)'>" + index.toString() + "</a></dt>";
                            html += "<dd>";
                            var items = value.split(";");
                            $.each(items, function (ind, val) {
                                var otherItem = val.split("|");
                                html += "<label style=' cursor:pointer;' for='" + otherItem[0] + "'><input class='catalogoCheck' id='" + otherItem[0] + "' type='checkbox' name='" + otherItem[0] + "' value='" + otherItem[0] + "'/>" + otherItem[1] + "</label><br />";
                            });
                            html += "</dd>";
                            contador++;
                        });
                        html += "</dl>";
                    }
                    $("td.panelesCatalogos").html("").html(html);
                }, error: function () {
                    alert("Error en el proceso");
                }
            });

        }
        var flag = 0;
        var band = 0;
        function addNodoPrincipal(arbol) {
            $("#arbolPrev").jstree('destroy');
            $("#arbolPrev").jstree({

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
                "plugins": ["themes", "html_data", "ui", "crrm","core", "types"]
            });
        }
        var forward = new Array();
        $(document).ready(function () {

            ObtenerCatalogos();
            var allPanels = $('.accordion > dd').hide();

            $('.accordion > dt > a').on("click", function () {
                $this = $(this);
                $target = $this.parent().next();

                if (!$target.hasClass('active')) {
                    allPanels.removeClass('active').slideUp();
                    $target.addClass('active').slideDown();
                }
                return false;
            });

            idEncuestaEncrypt = "";
            idEncuestaDecrypt = "";
            var visible = 0;
            $.get = function (key) {
                key = key.replace(/[\[]/, '\\[');
                key = key.replace(/[\]]/, '\\]');
                var pattern = "[\\?&]" + key + "=([^&#]*)";
                var regex = new RegExp(pattern);
                var url = unescape(window.location.href);
                var results = regex.exec(url);
                if (results === null) {
                    return null;
                } else {
                    return results[1];
                }
            }
            $("button#btnAtras_Adelante").hide();

            $("button#btnAtras_Adelante").unbind().bind("click", function () {
                $("#selectTop option[value=1]").prop("selected", true);
                var idEncHidden = $("#hdnIdEncuesta").val();
                RGraph.ObjectRegistry.Clear();
                var idPreg = $(this).data('idPreAnterior');
                flag--;
                band--;
                forward.pop();
                globalNodo = forward[band];
                arrayAtras.pop();
                $("li#" + forward[band] + ">ul").remove();
                Graficar(idEncHidden, idPreg, arrayAtras[flag]);
                arrayAtras.pop();

                if (flag <= 0) {
                    $(this).hide();
                }
            });

            idEncuesta = (getUrlVar("EncId"));

            var idEncHidden = $("#hdnIdEncuesta").val();

            RGraph.ObjectRegistry.Clear();


            var checks = $(":checkbox.catalogoCheck");
            var cadCatalogos = "";

            $.each(checks, function (ind, val) {
                cadCatalogos += $(val).val() + ",";
            });
            cadCatalogos = cadCatalogos.substring(0, cadCatalogos.length - 1);



            Graficar(idEncHidden, 0, "");
            //DibujaGrafica(idEncHidden, "", cadCatalogos, "");
            $('select#ddlPresentacion').unbind().bind("change", function (e) {
                e.preventDefault();
                RGraph.Clear(document.getElementById("graficaBarras"));
                RGraph.ObjectRegistry.Clear();
                var idEncHidden = $("#hdnIdEncuesta").val();
                Graficar(idEncHidden, 0, "");
            });

            $("img#printPdf").unbind().bind("click", function (e) {
                PrintToPDF();
            });

            $("img#imgEnviaMail").unbind().bind("click", function (e) {
                $("#btnEnviaCorreos").trigger("click");
            });

            $(":checkbox.catalogoCheck").on("click", function () {
                $("div.modal").show();
                var checks = $(":checkbox.catalogoCheck:checked");
                var cadCatalog = "";

                $.each(checks, function (ind, val) {
                    cadCatalog += $(val).val() + ",";
                });
                cadCatalog = cadCatalog.substring(0, cadCatalog.length - 1);

                var idEncHidden = $("#hdnIdEncuesta").val();
                RGraph.ObjectRegistry.Clear();
                //Graficar(idEncHidden, 0, "");
                DibujaGrafica(idEncHidden, "", cadCatalog, "");

            });

            $("select#selectTop").on("change", function () {
                var idE = $("select#selectTop").data("idEnc");
                var idsResp = $("select#selectTop").data("idsResp");
                var opt = $(this).find("option:selected");
                if ($(opt).val() == 1) {
                    var idP = $("#selectTop").data("idPreg");
                    var disposIds = $("#selectTop").data("idsDispos");
                    RGraph.ObjectRegistry.Clear();
                    globalNodo = forward[band];
                    $("li#" + forward[band] + ">ul").remove();
                    Graficar(idE, idP, disposIds);
                }
                else if ($(opt).val() == 2) {

                    var disp = $("#selectTop").data("disp");
                    var idP = $("#selectTop").data("idPreg");

                    var idsNextQuestion = $("#selectTop").data("idsSigPreg");
                    var idsQuestions = $("#selectTop").data("idsPreguntas");
                    var idsCount = $("#selectTop").data("idsContador");
                    var idsdispositivs = $("#selectTop").data("idsDisp");
                    var descQuestions = $("#selectTop").data("desPreg");
                    var descAnswer = $("#selectTop").data("descResp");

                    var presentacion = $('select#ddlPresentacion option:selected').val();

                    if (presentacion == 1)
                        disp = "";

                    $.ajax({
                        type: "POST",
                        url: "Grafica.aspx/TopOfMind",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ idPregunta: idP, idsDispos: disp }),
                        dataType: "json",
                        async: false,
                        success: function (datos) {

                            var data = jQuery.parseJSON(datos.d);
                            if (data.length > 0) {
                                var Colorea = new Array("#ff0000", "#00ff00", "#0000ff", "#ffff00", "#00ffff", "#ff00ff", "#c86400", "#800080", "#800000", "#c0c0c0", "#000080", "#643200", "#800080", "#ffe4c4", "#dc143c", "#ffd700", "#adff2f", "#778899", "#6b8e23", "#ff4500", "#800080", "#d8bfd8");
                                var descGrafica = new Array();
                                var numeroGRafica = new Array();
                                var colorGrafica = new Array();
                                $.each(data, function (index, value) {
                                    debugger;
                                    var result = buscar_indice(value["RespuestaDescripcion"], descGrafica);
                                    if (result == -1) {
                                        descGrafica.push(value["RespuestaDescripcion"]);
                                        numeroGRafica.push(1);
                                        colorGrafica.push(Colorea[index]);
                                    } else {
                                        var c = 0;
                                        c = numeroGRafica[result];
                                        c++;
                                        numeroGRafica[result] = c;
                                    }
                                });

                                RGraph.ObjectRegistry.Clear();
                                RGraph.Clear(document.getElementById("graficaBarras"));
                                var graficaRespuestas = new RGraph.Bar('graficaBarras', numeroGRafica);
                                graficaRespuestas.Set('chart.hmargin', 15);
                                graficaRespuestas.Set('chart.linewidth', 2);
                                graficaRespuestas.Set('chart.labels', descGrafica);
                                graficaRespuestas.Set('chart.labels.above', true);
                                graficaRespuestas.Set('chart.labels.above.size', 13);
                                //graficaRespuestas.Set('chart.ymax', parseInt(reglaY));
                                graficaRespuestas.Set('chart.colors', colorGrafica);
                                graficaRespuestas.Set('chart.colors.sequential', true);

                                graficaRespuestas.Set('chart.shadow', true);
                                graficaRespuestas.Set('chart.title.color', 'black');

                                graficaRespuestas.Set('chart.background.barcolor1', 'white');
                                graficaRespuestas.Set('chart.background.barcolor2', 'white');
                                graficaRespuestas.Set('chart.background.grid.color', 'white');

                                graficaRespuestas.Set('chart.tooltips', descGrafica);
                                graficaRespuestas.Set('chart.tooltips.event', 'onmousemove');
                                graficaRespuestas.Set('chart.gutter.top', 60);
                                graficaRespuestas.Set('chart.text.color', 'black');
                                graficaRespuestas.Set('chart.text.size', 8);
                                graficaRespuestas.Set('chart.text.angle', 45);
                                graficaRespuestas.Set('chart.text.font', "Arial");

                                graficaRespuestas.Set('chart.background.grid.vlines', false);
                                graficaRespuestas.Set('chart.zoom.background.color', '#FFFFFF');
                                graficaRespuestas.Set('chart.zoom.vdir', 'center');
                                graficaRespuestas.Set('chart.zoom.hdir', 'center');
                                graficaRespuestas.Set('chart.contextmenu', [['Vista Previa', RGraph.Zoom]]);

                                graficaRespuestas.Set('chart.hmargin.grouped', 1);
                                graficaRespuestas.Set('chart.variant', '3d');
                                graficaRespuestas.Set('chart.strokestyle', 'rgba(0,0,0,0.1)');

                                graficaRespuestas.Set('chart.gutter.left', 65);
                                graficaRespuestas.Set('chart.gutter.right', 5);
                                graficaRespuestas.Set('chart.gutter.bottom', 160);

                                graficaRespuestas.Set('chart.events.click', function (e, bar) {
                                    $("button#btnAtras_Adelante").show();
                                    var idR = "";
                                    var idP = "";
                                    var idNP = "";
                                    var idsDipo = "";
                                    var desDipo = "";
                                    RGraph.ObjectRegistry.Clear();
                                    $.each($("#selectTop").data("idsDisp").split("|"), function (index, value) {

                                        if (value != "") {
                                            if (value.split(";")[3].toString().toUpperCase().trim() == bar["tooltip"].toString().toUpperCase().trim()) {

                                                idR = value.split(";")[0];
                                                idP = value.split(";")[4];
                                                idNP = value.split(";")[5];
                                                idsDipo = value.split(";")[1];
                                                desDipo = value.split(";")[3];
                                                return;
                                            }
                                        }

                                    });
                                    var idx = bar[5];


                                    var idEncHidden = $("#hdnIdEncuesta").val();
                                    graficaRespuestas = null;
                                    forward.push(idR);
                                    flag = arrayAtras.length;
                                    band = forward.length - 1;
                                    var sub = idsDipo.substring(0, idsDipo.toString().length - 1);
                                    $("button#btnAtras_Adelante").data('idPreAnterior', 0);
                                    $("button#btnAtras_Adelante").data('idPreAnterior', idP);
                                    $("button#btnAtras_Adelante").data('Dispos', $("#selectTop").data("idsDispos"));
                                    $('div.modal').show();
                                    var rel = "resp";
                                    if (desDipo == "FIN DE LA ENCUESTA") {
                                        rel = "fin";
                                    }
                                    $("#arbolPrev").jstree("create", $("#" + globalNodo), "last", { "attr": { "class": "", "rel": "resp", "id": idR }, data: desDipo }, false, true);
                                    globalNodo = idR;
                                    Graficar(idEncHidden, parseInt(idNP), sub);

                                });
                                graficaRespuestas.Draw();
                            } else {
                                showMensaje("info", "No se encontraron resultados.");
                                $("#selectTop option[value=1]").prop("selected", true);
                            }
                        }
                    });
                }
            });

        });

        function buscar_indice(cadena, donde) {
            var POSICION_INICIAL = 0
            for (i = POSICION_INICIAL; i < donde.length; i++) {
                if (donde[i] == cadena)
                    return i;
            }
            return -1
        }

        function PrintToPDF() {
            $('div.modal').show();
            var cadCatalogos = "";
            var checkTodos = $('input[type=checkbox][name=chkMostrarTodos]').is("checked");
            var checkdentroHorario = $('input[type=checkbox][name=chkDentroHorario]').is("checked");
            var idEncHidden = $("#hdnIdEncuesta").val();
            var Colorea = new Array("#ff0000", "#00ff00", "#0000ff", "#ffff00", "#00ffff", "#ff00ff", "#c86400", "#800080", "#800000", "#c0c0c0", "#000080", "#643200", "#800080", "#ffe4c4", "#dc143c", "#ffd700", "#adff2f", "#778899", "#6b8e23", "#ff4500", "#800080", "#d8bfd8");
            var listImagenes = new Array();
            var imagenToparser = "";
            $.ajax({
                type: "POST",
                url: "Grafica.aspx/PintaGrafica",
                contentType: "application/json; charset=utf-8",
                data: "{'idEncuesta':'" + parseInt(idEncHidden.toString()) + "','checboxTodos':'" + true + "','checboxHorario':'" + checkdentroHorario + "','Catalogos':'" + cadCatalogos + "'}",
                dataType: "json",
                async: false,
                success: function (datos) {

                    var grafica = jQuery.parseJSON(datos.d);

                    var respuesta = new Array();
                    var idToFind = "";
                    var html = "";
                    var cont = 0;

                    $.each(grafica, function (index, value) {

                        if (index != 0 && idToFind != value["IdPregunta"]) {
                            idToFind = value["IdPregunta"];
                            var Pregunta = value["IdPregunta"];
                            var Titulo = value["PreguntaDescripcion"];
                            var DescHijos = new Array();
                            var AlturaHijos = new Array();
                            var ColorHijo = new Array();
                            var indiceColor = 0;
                            $.each(grafica, function (ind, val) {
                                if (Pregunta == val["IdPregunta"]) {
                                    DescHijos.push(val["RespuestaDescripcion"]);
                                    AlturaHijos.push(val["Contador"]);
                                    ColorHijo.push(Colorea[indiceColor]);
                                    indiceColor++;
                                }
                            });

                            RGraph.Clear(document.getElementById("soporteCanvasPDF"));
                            var barras = new RGraph.Bar('soporteCanvasPDF', AlturaHijos);

                            barras.Set('chart.title', Titulo);
                            barras.Set('chart.hmargin', 15);
                            barras.Set('chart.linewidth', 2);
                            barras.Set('chart.title.bold', true);
                            barras.Set('chart.title.size', 10);
                            barras.Set('chart.title.font', 'Arial');
                            barras.Set('chart.labels', DescHijos);
                            barras.Set('chart.labels.above', true);
                            barras.Set('chart.labels.above.size', 13);
                            barras.Set('chart.colors', ColorHijo);
                            barras.Set('chart.colors.sequential', true);
                            barras.Set('chart.shadow', true);
                            barras.Set('chart.title.color', 'black');
                            barras.Set('chart.background.barcolor1', 'white');
                            barras.Set('chart.background.barcolor2', 'white');
                            barras.Set('chart.background.grid.color', 'gray');
                            barras.Set('chart.shadow', true);
                            barras.Set('chart.shadow.offsetx', 1);
                            barras.Set('chart.shadow.offsety', 0);
                            barras.Set('chart.shadow.blur', 1);
                            barras.Set('chart.text.color', 'black');
                            barras.Set('chart.text.size', 8);
                            barras.Set('chart.text.angle', 45);
                            barras.Set('chart.text.font', "Arial");
                            barras.Set('chart.background.grid.vlines', false);
                            barras.Set('chart.zoom.background.color', '#FFFFFF');
                            barras.Set('chart.zoom.vdir', 'center');
                            barras.Set('chart.zoom.hdir', 'center');
                            barras.Set('chart.hmargin.grouped', 1);
                            barras.Set('chart.variant', '3d');
                            barras.Set('chart.strokestyle', 'rgba(0,0,0,0.1)');
                            barras.Set('chart.gutter.left', 65);
                            barras.Set('chart.gutter.right', 5);
                            barras.Set('chart.gutter.bottom', 160);
                            barras.Draw();
                            var canvas = document.getElementById("soporteCanvasPDF");
                            var img = canvas.toDataURL('image/png');

                            img = img.replace('data:image/png;base64,', '');
                            listImagenes.push(img);

                        }
                    });


                }, //Termina Success
                error: function (request, status, error) {
                    $('div.modal').hide();
                    showMensaje("error", request.statusText);
                }
            });

           
            var canvas = document.getElementById("soporteCanvasPDF");
            var context = canvas.getContext("2d");

            context.clearRect(0, 0, canvas.width, canvas.height);
           
            
            $.ajax({
                type: 'POST',
                url: 'ExportaPDF.aspx/crearImagenes',
                data: JSON.stringify({ imageData: listImagenes }),
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                async: false,
                success: function (msg) {
                    
                    var json = JSON.stringify(eval("(" + msg.d + ")"));
                    json = JSON.parse(json);
                    json = json.nImagenes;
                    window.location.href = '<%= Page.ResolveUrl("~/Pages/ExportaPDF.aspx") %>';
                }
            });
        }


        var arrayAtras = new Array();

        function DibujaGrafica(idEncuesta,idPregunta,idscatalogos,idsDispositivos) {

            var checkCatalogos = "";
            var cboPresentacion = "";
            var vigencia = "";

//            $.ajax({
//                type: "POST",
//                url: "Grafica.aspx/FillDatosEncuesta",
//                contentType: "application/json; charset=utf-8",
//                data: JSON.stringify({ IdEncuesta: parseInt(idEncuesta.toString()), checboxTodos: true }),
//                dataType: "json",
//                async: false,
//                success: function (data) {
//                    var json = JSON.stringify(eval("(" + data.d + ")"));
//                    json = JSON.parse(json);
//                    var todos = json.Todos;
//                    var contestados = json.Contestado;
//                    var sinContestar = json.SinContestar;
//                    var NombreEnc = json.NombreEnc;

//                    var enviados = parseInt(contestados) + parseInt(sinContestar);
//                    var tasaRespuesta = ((contestados) / (enviados)) * 100;

//                    $("span#totalResp").html("").html(todos.toString());
//                    $("span#tasaResp").html("").html(contestados.toString() + "  /  " + enviados.toString() + " = " + parseFloat(tasaRespuesta).toFixed(2).toString() + "%");

               //     var checks = $(":checkbox.catalogoCheck:checked");
                   
                    var checkdentroHorario = $('input[type=checkbox][name=chkDentroHorario]').attr("checked");
                    var dentro = checkdentroHorario == 'checked' ? true : false;

                    $.ajax({
                        type: "POST",
                        url: "Grafica.aspx/DibujaGrafica",
                        contentType: "application/json; charset=utf-8",
                        data: JSON.stringify({ idEncuesta: parseInt(idEncuesta.toString()), checboxHorario: dentro, Catalogos: idscatalogos, idPregunta: idPregunta }),
                        dataType: "json",
                        async: false,
                        success: function (refDatos) {
                            var datosGrafica = jQuery.parseJSON(refDatos.d);
                            console.log(datosGrafica);
                            debugger;
                            if (datosGrafica != null) {

                                var descBarra = new Array();
                                var alturaBarra = new Array();
                                var idsRespuestas = new Array();
                                var idsPregunta = new Array();
                                var idsSigPregunta = new Array();
                                var colorBarra = new Array();
                                var dispositivos = new Array();
                                var tmp = new Array();

                                var idToFind = 0;
                                var tituloGrafica = "";

                                var graficaBarras = new Array();

                                //if (idPregunta == "") {
                                idToFind = datosGrafica[0]["IdPregunta"];
                                tituloGrafica = datosGrafica[0]["PreguntaDescripcion"];
                                //}
                                $.each(datosGrafica, function (index, value) {

                                    if (value["IdPregunta"].toString() == idToFind.toString()) {
                                        if (buscar_indice(value["IdRespuesta"], idsRespuestas) == -1) {
                                            descBarra.push(value["RespuestaDescripcion"]);
                                            colorBarra.push(get_random_color());
                                            idsRespuestas.push(value["IdRespuesta"]);
                                            idsPregunta.push(value["IdPregunta"]);
                                            idsSigPregunta.push(value["IdSiguientePregunta"]);
                                            var cadenaDispos = "";
                                            tmp = new Array();
                                            $.each(datosGrafica, function (ind, val) {
                                                if (value["IdRespuesta"] == val["IdRespuesta"]) {
                                                    if (buscar_indice(val["ID_Dispo"], tmp) == -1) {
                                                        tmp.push(val["ID_Dispo"]);
                                                    }
                                                }
                                            });

                                            $.each(tmp, function (j, k) {
                                                cadenaDispos += k + ",";
                                            });
                                            var alto = tmp.length;

                                            cadenaDispos = cadenaDispos.substring(0, cadenaDispos.length - 1);
                                            dispositivos.push(cadenaDispos);
                                            alturaBarra.push(parseInt(alto));
                                        }
                                    }
                                });


                                RGraph.ObjectRegistry.Clear();
                                RGraph.Clear(document.getElementById("graficaBarras"));
                                var graficaRespuestas = new RGraph.Bar('graficaBarras', alturaBarra); //idCanvas donde se dibujara la grafica                             

                                graficaRespuestas.Set('chart.hmargin', 15);
                                graficaRespuestas.Set('chart.linewidth', 2);
                                graficaRespuestas.Set('chart.labels', descBarra);
                                graficaRespuestas.Set('chart.labels.above', true);
                                graficaRespuestas.Set('chart.labels.above.size', 13);
                                //graficaRespuestas.Set('chart.ymax', parseInt(reglaY));
                                graficaRespuestas.Set('chart.colors', colorBarra);
                                graficaRespuestas.Set('chart.colors.sequential', true);

                                graficaRespuestas.Set('chart.shadow', true);
                                graficaRespuestas.Set('chart.title.color', 'black');

                                graficaRespuestas.Set('chart.background.barcolor1', 'white');
                                graficaRespuestas.Set('chart.background.barcolor2', 'white');
                                graficaRespuestas.Set('chart.background.grid.color', 'white');

                                graficaRespuestas.Set('chart.tooltips', dispositivos);

                                graficaRespuestas.Set('chart.tooltips.event', 'onmousemove');


                                graficaRespuestas.Set('chart.gutter.top', 60);
                                graficaRespuestas.Set('chart.text.color', 'black');
                                graficaRespuestas.Set('chart.text.size', 8);
                                graficaRespuestas.Set('chart.text.angle', 45);
                                graficaRespuestas.Set('chart.text.font', "Arial");

                                graficaRespuestas.Set('chart.background.grid.vlines', false);
                                graficaRespuestas.Set('chart.zoom.background.color', '#FFFFFF');
                                graficaRespuestas.Set('chart.zoom.vdir', 'center');
                                graficaRespuestas.Set('chart.zoom.hdir', 'center');
                                graficaRespuestas.Set('chart.contextmenu', [['Vista Previa', RGraph.Zoom]]);

                                graficaRespuestas.Set('chart.hmargin.grouped', 1);
                                graficaRespuestas.Set('chart.variant', '3d');
                                graficaRespuestas.Set('chart.strokestyle', 'rgba(0,0,0,0.1)');

                                graficaRespuestas.Set('chart.gutter.left', 65);
                                graficaRespuestas.Set('chart.gutter.right', 5);
                                graficaRespuestas.Set('chart.gutter.bottom', 160);

                                graficaRespuestas.Set('chart.events.click', function (e, bar) {
                                    RGraph.ObjectRegistry.Clear();
                                    debugger;
                                    var indice = bar[5];
                                    var refIdR = idsRespuestas[bar[5]];
                                    var refIdP = idsPregunta[bar[5]];
                                    var refIdSP = idsSigPregunta[bar[5]];
                                    var refIdsDispo = dispositivos[bar[5]];
                                    var checks = $(":checkbox.catalogoCheck:checked");
                                    var cadCatalog = "";

                                    $.each(checks, function (ind, val) {
                                        cadCatalog += $(val).val() + ",";
                                    });
                                    cadCatalog = cadCatalog.substring(0, cadCatalog.length - 1);

                                    DibujaGrafica(idEncuesta, refIdP, cadCatalog, refIdsDispo);

                                });

                                graficaRespuestas.Draw();

                            } else {


                            }
                        }, error: function () {
                            alert("ocurrio un error");
                        }
                    });






//                }
//            });




        
        }

        function existeIdREspuesta(arregloRespuestas) { 
            

        
        }
        function Graficar(idEncuesta, idNextQuestion, disposToFind) {
           
            if (disposToFind == "") {
                $("button#btnAtras_Adelante").hide();
            }
                
            try {
            $('div.modal').show();
            var checkTodos = $('input[type=checkbox][name=chkMostrarTodos]').is("checked");
            var checkdentroHorario = $('input[type=checkbox][name=chkDentroHorario]').attr("checked");
            var dentro = checkdentroHorario == 'checked'?true:false;
            var presentacion = $('select#ddlPresentacion option:selected').val();

            if (presentacion == 2)
                $("#arbolPrev").hide();
            else
                $("#arbolPrev").show();

            $.ajax({
                type: "POST",
                url: "Grafica.aspx/FillDatosEncuesta",
                contentType: "application/json; charset=utf-8",
                data: "{'IdEncuesta':'" + JSON.stringify(parseInt(idEncuesta.toString())) + "','checboxTodos':'" + JSON.stringify(dentro) + "'}",
                dataType: "json",
                async: false,
                success: function (data) {

                    var json = JSON.stringify(eval("(" + data.d + ")"));
                    json = JSON.parse(json);

                    var todos = json.Todos;
                    var contestados = json.Contestado;
                    var sinContestar = json.SinContestar;
                    var NombreEnc = json.NombreEnc;

                    var enviados = parseInt(contestados) + parseInt(sinContestar);
                    var tasaRespuesta = ((contestados) / (enviados)) * 100;

                    $("span#totalResp").html("").html(todos.toString());
                    $("span#tasaResp").html("").html(contestados.toString() + "  /  " + enviados.toString() + " = " + parseFloat(tasaRespuesta).toFixed(2).toString() + "%");


                    var checks = $(":checkbox.catalogoCheck:checked");
                    var cadCatalogos = "";


                    if (checks != undefined) {
                        $.each(checks, function (ind, val) {
                            cadCatalogos += $(val).val() + ",";
                        });
                        cadCatalogos = cadCatalogos.substring(0, cadCatalogos.length - 1);
                    }


                    $.ajax({
                        type: "POST",
                        url: "Grafica.aspx/PintaGrafica",
                        contentType: "application/json; charset=utf-8",
                        data: "{'idEncuesta':'" + parseInt(idEncuesta.toString()) + "','checboxTodos':'" + true + "','checboxHorario':'" + dentro + "','Catalogos':'" + cadCatalogos + "'}",
                        dataType: "json",
                        async: false,
                        success: function (datostoPaint) {

                            var Colorea = new Array("#ff0000", "#00ff00", "#0000ff", "#ffff00", "#00ffff", "#ff00ff", "#c86400", "#800080", "#800000", "#c0c0c0", "#000080", "#643200", "#800080", "#ffe4c4", "#dc143c", "#ffd700", "#adff2f", "#778899", "#6b8e23", "#ff4500", "#800080", "#d8bfd8");
                            var indiceColor = 0;
                            var grafica = jQuery.parseJSON(datostoPaint.d);
                            if (grafica != null) {
                                var contador = 0;
                                var idFind = "";
                                var tituloGrafica = "";
                                var respuestas = new Array();
                                idFind = idNextQuestion == 0 ? grafica[1]["IdPregunta"] : idNextQuestion;
                                var encontre = false;
                                var c = 0;
                                var valorBoton = $("button#btnAtras_Adelante").data('idPreAnterior');
                                
                                var bandera = 0;
                                $.each(grafica, function (i, v) {

                                    if (v["IdPregunta"].toString() == idFind.toString()) {
                                        respuestas[c] = { Contador: v["Contador"], Dispositivos: v["Dispositivos"], IdPregunta: v["IdPregunta"], IdRespuesta: v["IdRespuesta"], IdSiguientePregunta: v["IdSiguientePregunta"], PreguntaDescripcion: v["PreguntaDescripcion"], RespuestaDescripcion: v["RespuestaDescripcion"], Total: v["Total"], IDTipoResp: v["IDTipoResp"], Telefonos: v["Num_Telefonicos"] };

                                        if (v["PreguntaDescripcion"].toString().toUpperCase() != "FIN DE LA ENCUESTA") {
                                            if (v["IdSiguientePregunta"].toString() == idFind) {
                                                $("button#btnAtras_Adelante").data('idPreAnterior', v["IdPregunta"]);
                                            }
                                        }
                                        c++;
                                        encontre = true;
                                    }

                                    if (v["IdSiguientePregunta"].toString() == idFind) {
                                        var esfinEncuesta = false;
                                        $.each(grafica, function (j, k) {
                                            if (k["IdPregunta"].toString() == idFind.toString()) {
                                                if (k["PreguntaDescripcion"].toString().toUpperCase() == "FIN DE LA ENCUESTA") {
                                                    esfinEncuesta = true;
                                                }
                                            }
                                        });
                                        if (!esfinEncuesta) {
                                            $("button#btnAtras_Adelante").data('idPreAnterior', v["IdPregunta"]);
                                        }
                                    }
                                });

                                var dis = "";
                                var num_tele = "";
                                if (presentacion == 1 && disposToFind != "") {
                                
                                    $.each(respuestas, function (s, t) {
                                        var disposFromResp = t["Dispositivos"].substring(0, t["Dispositivos"].toString().length - 1);
                                        var telFromREsp = t["Telefonos"].substring(0, t["Telefonos"].toString().length - 1);
                                        var existeDispo = false;
                                        var descDispo = "";
                                        var idi = "";

                                        disposFromResp = disposFromResp.split(",");
                                        var arregloTmp = new Array();
                                        var contador = 0;

                                        $.each(disposToFind.split(","), function (u, v) {
                                            $.each(disposFromResp, function (w, x) {
                                                if (v == x) {
                                                    existeDispo = true;
                                                    contador++;
                                                    descDispo += v + ",";
                                                    idi += telFromREsp.split(",")[u] + ",";

                                                }
                                            });
                                        });

                                        if (existeDispo) {
                                            respuestas[s]["Contador"] = contador;
                                            respuestas[s]["Dispositivos"] = descDispo;
                                            respuestas[s]["Telefonos"] = idi;
                                            num_tele += idi + ";";
                                            //dis += descDispo;
                                        } else {
                                            respuestas[s]["Contador"] = 0;
                                        }

                                    });

                                    var rel = "preg";
                                    if (respuestas[0]["PreguntaDescripcion"].toString() == "FIN DE LA ENCUESTA") { rel = "fin" }

                                    $("#arbolPrev").jstree("create", $("#" + globalNodo), "last", { "attr": { "class": "", "rel": rel, "id": respuestas[0]["IdPregunta"] }, data: respuestas[0]["PreguntaDescripcion"] }, false, true);
                                    globalNodo = respuestas[0]["IdPregunta"];
                                } else {
                                    var param = "<li id=" + grafica[1]["IdPregunta"] + " rel='preg'><a href='#'>" + grafica[1]["PreguntaDescripcion"] + "</a></li>";
                                    addNodoPrincipal(param.toString());
                                    globalNodo = grafica[1]["IdPregunta"];
                                    forward.push(grafica[1]["IdPregunta"]);
                                }

                                tituloGrafica = idNextQuestion == 0 ? grafica[1]["PreguntaDescripcion"] : respuestas[0]["PreguntaDescripcion"];

                                var AlturaxRespuestas = new Array();
                                var DescxRespuestas = new Array();
                                var arrayNumeros = new Array();
                                var ejeY = new Array();
                                var Colores = new Array();
                                var descNumTel = new Array();

                                arrayAtras.push(disposToFind);

                                var idsResp = "";

                                var idsSigPreg = "";
                                var idsPreguntas = "";
                                var idsContador = "";
                                var idsDisp = "";
                                var desPreg = "";
                                var descResp = "";
                                var phones = "";
                                var telphone = new Array();
                                var d_ispos = "";
                              
                                var arrayNum = new Array();
                                $.each(respuestas, function (index, value) {

                                    AlturaxRespuestas.push(parseInt(value["Contador"].toString()));
                                    ejeY.push(parseInt(value["Contador"].toString()));
                                    DescxRespuestas.push(value["RespuestaDescripcion"].toString());
                                    Colores.push(Colorea[indiceColor]);
                                    telphone.push(value["Telefonos"]);
                                    idsResp += value["IdRespuesta"] + ",";
                                    arrayNum.push(value["Dispositivos"]);
                                    idsPreguntas += value["IdPregunta"] + "|";
                                    idsContador += value["Contador"] + "|";
                                    idsSigPreg += value["IdSiguientePregunta"] + "|";
                                    d_ispos += value["Dispositivos"];
                                    idsDisp += value["IdRespuesta"] + ";" + value["Dispositivos"] +
                                            ";" + value["Contador"] + ";" + value["RespuestaDescripcion"] +
                                            ";" + value["IdPregunta"] + ";" + value["IdSiguientePregunta"] +
                                            ";" + value["Telefonos"] + "|";


                                    desPreg += value["PreguntaDescripcion"] + "|";
                                    descResp += value["RespuestaDescripcion"] + "|";

                                    indiceColor++;

                                });

                                if (respuestas.length > 0) {
                                    if (respuestas[0]["IDTipoResp"] == 2) {
                                       
                                        $("#selectTop").show();
                                        $("#selectTop").data("idPreg", idFind);
                                        idsResp = idsResp.substring(0, idsResp.length - 1);
                                        //$("#selectTop").data("num_tele", num_tele);
                                        $("#selectTop").data("idEnc", idEncuesta);
                                        $("#selectTop").data("idsResp", idsResp);
                                        $("#selectTop").data("idsDispos", disposToFind);
                                        //$("#selectTop").data("disp",d_ispos);
                                        $("#selectTop").data("idsSigPreg", idsSigPreg);
                                        $("#selectTop").data("idsPreguntas", idsPreguntas);
                                        $("#selectTop").data("idsContador", idsContador);
                                        $("#selectTop").data("idsDisp", idsDisp);
                                        $("#selectTop").data("desPreg", desPreg);
                                        $("#selectTop").data("descResp", descResp);

                                        /*
                                        idsSigPreg = "";idsPreguntas = "";
                                        idsContador = "";    idsDisp = ""; desPreg = ""; descResp
                                        */



                                    } else {
                                        $("#selectTop").hide();
                                    }
                                } else {
                                    $("#selectTop").hide();
                                }


                                var reglaY = elementoMaximo(AlturaxRespuestas);
                                reglaY = reglaY + 5;
                                ejeY.shift();
                                ejeY.push(parseInt(reglaY));
                                var sizeTitulo = 10;
                                $("td.tdTituloGrafica").html("").html(tituloGrafica.toString());
                                RGraph.Clear(document.getElementById("graficaBarras"));
                                var graficaRespuestas = new RGraph.Bar('graficaBarras', AlturaxRespuestas); //idCanvas donde se dibujara la grafica                             

                                graficaRespuestas.Set('chart.hmargin', 15);
                                graficaRespuestas.Set('chart.linewidth', 2);
                                graficaRespuestas.Set('chart.labels', DescxRespuestas);
                                graficaRespuestas.Set('chart.labels.above', true);
                                graficaRespuestas.Set('chart.labels.above.size', 13);
                                graficaRespuestas.Set('chart.ymax', parseInt(reglaY));
                                graficaRespuestas.Set('chart.colors', Colores);
                                graficaRespuestas.Set('chart.colors.sequential', true);

                                graficaRespuestas.Set('chart.shadow', true);
                                graficaRespuestas.Set('chart.title.color', 'black');

                                graficaRespuestas.Set('chart.background.barcolor1', 'white');
                                graficaRespuestas.Set('chart.background.barcolor2', 'white');
                                graficaRespuestas.Set('chart.background.grid.color', 'white');

                                graficaRespuestas.Set('chart.tooltips', DescxRespuestas);
                                graficaRespuestas.Set('chart.paramsGrafica', telphone);
                              
                                graficaRespuestas.Set('chart.tooltips.event', 'onmousemove');

                               
                                graficaRespuestas.Set('chart.gutter.top', 60);
                                graficaRespuestas.Set('chart.text.color', 'black');
                                graficaRespuestas.Set('chart.text.size', 8);
                                graficaRespuestas.Set('chart.text.angle', 45);
                                graficaRespuestas.Set('chart.text.font', "Arial");

                                graficaRespuestas.Set('chart.background.grid.vlines', false);
                                graficaRespuestas.Set('chart.zoom.background.color', '#FFFFFF');
                                graficaRespuestas.Set('chart.zoom.vdir', 'center');
                                graficaRespuestas.Set('chart.zoom.hdir', 'center');
                                graficaRespuestas.Set('chart.contextmenu', [['Vista Previa', RGraph.Zoom]]);

                                graficaRespuestas.Set('chart.hmargin.grouped', 1);
                                graficaRespuestas.Set('chart.variant', '3d');
                                graficaRespuestas.Set('chart.strokestyle', 'rgba(0,0,0,0.1)');

                                graficaRespuestas.Set('chart.gutter.left', 65);
                                graficaRespuestas.Set('chart.gutter.right', 5);
                                graficaRespuestas.Set('chart.gutter.bottom', 160);

                                graficaRespuestas.Set('chart.events.click', function (e, bar) {
                                    $("button#btnAtras_Adelante").show();
                                    debugger;
                                    var idx = bar[5];
                                    var idEncHidden = $("#hdnIdEncuesta").val();
                                    graficaRespuestas = null;
                                    var dispos = respuestas[idx]["Dispositivos"];
                                    var numDispos = respuestas[idx]["Contador"];
                                    RGraph.ObjectRegistry.Clear();
                                    forward.push(respuestas[idx]["IdRespuesta"]);
                                    flag = arrayAtras.length;
                                    band = forward.length - 1;
                                    var sub = respuestas[idx]["Dispositivos"].substring(0, respuestas[idx]["Dispositivos"].toString().length - 1);
                                    $("button#btnAtras_Adelante").data('idPreAnterior', 0);
                                    $("button#btnAtras_Adelante").data('idPreAnterior', respuestas[idx]["IdPregunta"]);
                                    $("button#btnAtras_Adelante").data('Dispos', disposToFind);
                             
                                    var di = respuestas[idx]["Dispositivos"];
                                    di = di.substring(0,di.length-1);
                                    $("#selectTop").data("disp", di);

                                    $('div.modal').show();
                                    var rel = "resp";
                                    if (respuestas[idx]["RespuestaDescripcion"] == "FIN DE LA ENCUESTA") {
                                        rel = "fin";
                                    }

                                    $("#arbolPrev").jstree("create", $("#" + globalNodo), "last", { "attr": { "class": "", "rel": "resp", "id": respuestas[idx]["IdRespuesta"] }, data: respuestas[idx]["RespuestaDescripcion"] }, false, true);
                                    globalNodo = respuestas[idx]["IdRespuesta"];

                                    Graficar(idEncHidden, parseInt(respuestas[idx]["IdSiguientePregunta"].toString()), sub);
                                });

                                graficaRespuestas.Draw();
                                setInterval(function () { $('div.modal').hide(); }, 2000);
                            } else {
                                $('div.modal').hide();
                                $("span#totalResp").html("Ninguna");
                                $("span#tasaResp").html("Ninguna");
                                showMensaje("alerta", "Solicitud no arrojo resultados.");
                            }
                        }, error: function (request, status, error) {
                            $('div.modal').hide();
                        }
                    });
                }, error: function (request, status, error) {
                    $('div.modal').hide();
                    showMensaje("error", request.statusText);
                }
            });

            }catch(e){
                $('div.modal').hide();
                showMensaje("error", e.message.ToString());
            }
        }

        function elementoMaximo(aItems){
            var nMax = 0;
            for (var i = 0; i <aItems.length; i++){
                if (aItems[i] > nMax) {
                    nMax = aItems[i];
                }
            }
            return nMax;
        }

        var globalNodo = "";
        var globalNodoBack = "";
        function VisualizaTree(dato) {

        }
        
      
        function get_random_color() {
            var letters = '0123456789ABCDEF'.split('');
            var color = '#';
            for (var i = 0; i < 6; i++) {
                color += letters[Math.round(Math.random() * 15)];
            }
            return color;
        }
        function getRandomColor() {
            // creating a random number between 0 and 255
            var r = Math.floor(Math.random() * 256);
            var g = Math.floor(Math.random() * 256);
            var b = Math.floor(Math.random() * 256);

            // going from decimal to hex
            var hexR = r.toString(16);
            var hexG = g.toString(16);
            var hexB = b.toString(16);

            // making sure single character values are prepended with a "0"
            if (hexR.length == 1) {
                hexR = "0" + hexR;
            }

            if (hexG.length == 1) {
                hexG = "0" + hexG;
            }

            if (hexB.length == 1) {
                hexB = "0" + hexB;
            }

            // creating the hex value by concatenatening the string values
            var hexColor = "#" + hexR + hexG + hexB;
            return hexColor.toUpperCase();
        }

       
        function getUrlVar(key) {
            var result = new RegExp(key + "=([^&]*)", "i").exec(window.location.search);
            return result && unescape(result[1]) || "";
        }
        function getParameterByName(name) {
            var match = RegExp('[?&]' + name + '=([^&]*)').exec(window.location.search);
            return match && decodeURIComponent(match[1].replace(/\+/g, ' '));
        }
        function Consultar() {            
            setInterval(function () { myTimer() }, document.getElementById('hdnTemporizador').value);
        }

        function myTimer() {
            document.getElementById('btnConsultar').click();
        }
    
        function NuevoEnviaCorreo() {
            document.getElementById("btnEnviaEmail").click();
        }

        function limpiacampoEnviaGrafica() {
            document.getElementById("txtEmailPara").value = "";
            document.getElementById("txtNombreDe").value = "";
            document.getElementById("txtEmailDe").value = "";
        }

        function CancelaEnvioGrafica() {
            document.getElementById("DvEmail").style.display = 'none';
            document.getElementById("btnEnviaCorreos").style.visibility = 'visible';
            document.getElementById("hdndivEmail").value = "0";
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
            var searchText = $get('<%=txtEmailPara.ClientID %>').value;
            searchText = searchText.replace('null', '');

            sender.get_element().value = searchText + value;
        }

        function ShowImage() {
            document.getElementById('txtEmailPara')
      .style.backgroundImage = 'url(../Images/loader.gif)';

            document.getElementById('txtEmailPara')
                    .style.backgroundRepeat = 'no-repeat';

            document.getElementById('txtEmailPara')
                    .style.backgroundPosition = 'right';
        }
        function HideImage() {
            document.getElementById('txtEmailPara')
                      .style.backgroundImage = 'none';
        } 

        /*Finalizaa Metodos Autocomplete*/

    </script>


</head>
<body bgcolor="#FF0000">
<div class="modal"></div>

    <form id="form1" runat="server" > 
    <asp:ScriptManager runat="server" ID="runScrpt"></asp:ScriptManager>
    <mesBx:messageBox runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento" />    
    <table width="947px" align="center" style="background-color:#F2F2F2">
        <tr>
            <td colspan="3" height="90" background="../Images/nuevoHeader.png">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <div id="divMensaje" class="exito mensajes" style=" display:none; " runat="server">Mensaje de éxito de la operación realizada</div>
            </td>
        </tr>
        <tr>
            <td style="height:auto; width: 75%" valign="top" align="left">               
                
                <button type="button" id="btnAtras_Adelante" class="btnMasterRectangular" style=" cursor:pointer;">Atras</button>             
            </td>           
            <td style="height:auto; width: 25%">
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:ImageButton Width="22px" ToolTip="Exportar PDF" ImageUrl="~/Images/IconoPDF.png"
                            Style="text-align: center; cursor:pointer;" ID="btnAgregaPregunta" runat="server" Visible="false"  />
                            <img id="printPdf" src="../Images/IconoPDF.png" alt="Imprimir Grafica a PDF" title="Imprimir Grafica a PDF" style=" cursor:pointer;" />
                       
                        </td>
                        <td align="center">
                            <span style="display:none;">
                            <asp:ImageButton Width="24px" runat="server" ImageUrl="~/Images/IconoCorreo.png"
                            ToolTip="Envia Correo" ID="btnEnviaCorreos" CausesValidation="false" OnClick="btnEnviaEmail_Click" />
                            </span>
                            <img id="imgEnviaMail" src="../Images/IconoCorreo.png" alt="Envia Mail" title="Envia Mail" style=" cursor:pointer;" />
                       
                        </td>
                        <td align="center">
                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 75%">
                <div runat="server" id="DvEmail" style="display: none">
                    <div class="ContenedorGeneral" style="height: auto; width: 95%;">
                        <div class="MasterTituloContenedor">
                            <h2>
                                Envia Grafica por Correo
                            </h2>
                        </div>
                        <table>
                            <tr>
                                <td>
                                    <font size="2" face="Arial, Arial, Impact"/>Para:
                                </td>
                                <td>
                                    <div style="display: inline-block;">
                                        <asp:TextBox CssClass="inputtext" runat="server" ID="txtEmailPara" ValidationGroup="grupoEmail"
                                            Width="300px" AutoComplete="off" MaxLength="200"></asp:TextBox>                                        
                                    <asp:AutoCompleteExtender
                                        runat="server" 
                                        BehaviorID="AutoCompleteEx"
                                        ID="autoComplete1" 
                                        TargetControlID="txtEmailPara"
                                        ServiceMethod="SearchEmail"
                                        MinimumPrefixLength="1" 
                                        CompletionInterval="10"
                                        EnableCaching="false"
                                        CompletionSetCount="20"
                                        CompletionListCssClass="AutoExtender"
                                        CompletionListItemCssClass="AutoExtenderList"
                                        CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                        OnClientPopulated="acePopulated"
                                        OnClientItemSelected="aceSelected"
                                        DelimiterCharacters="; "
                                        ShowOnlyCurrentWordInCompletionListItem="true" >
                                    </asp:AutoCompleteExtender>
                                    </div>                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <font size="2" face="Arial, Arial, Impact"/>De:
                                </td>
                                <td>
                                    <div style="display: inline-block;">
                                        <asp:TextBox CssClass="inputtext" runat="server" ID="txtNombreDe" ValidationGroup="grupoEmail"
                                            Width="300px" ReadOnly="true"></asp:TextBox>
                                            
                                    </div>
                                  
                                </td>
                            </tr>                            
                        </table>
                        <div>
                            <asp:Button runat="server" CssClass="btnMasterRectangular" ID="btnEnviarGrafica"
                                ValidationGroup="grupoEmail" Text="Enviar" OnClick="btnEnviarGrafica_Click" />
                            <input type="button" id="btnLimpia" class="btnMasterRectangular" onclick="limpiacampoEnviaGrafica()"
                                value="Limpiar" />
                            <input type="button" id="btnCancelarEnvioGrafica" class="btnMasterRectangular" onclick="CancelaEnvioGrafica()"
                                value="Cancelar" />
                            <br />
                        </div>
                    </div>
                </div>
                <br />
                <div id="divTitulo" runat="server" style="text-align: center; font-size: medium; font-family: Arial; font-weight:bold; color: Black">
                </div>
                <div style=" text-align:right; float:right; width:100%;">
                  
                       
                </div>
                <br />
                <div id="divTituloGraf" runat="server" style="text-align: center; font-size: medium; font-family: Arial; font-weight:bold; color: Black">
                </div>
            </td>
            <td style="width: 25%" class="textoRespuestas" align="left" valign="center">
             Presentación:  
                <asp:DropDownList ID="ddlPresentacion" 
                    runat="server" Height="20px" Width="160px" CssClass="styleCombosCat" 
                    AutoPostBack="False"             
                    >
                    <asp:ListItem Value="1">Arbol</asp:ListItem>
                    <asp:ListItem Value="2">Pregunta</asp:ListItem>
                </asp:DropDownList><br />
                <select id="selectTop" class="styleCombosCat">
                    <option value="1" selected="selected">Share Of Mind</option>
                    <option value="2">Top Of Mind</option>
                </select>     
            </td >
            </tr>
            <tr>
            <td style="width: 50%">
            </td>
            <td style="width: 50%">
  <asp:CheckBox ID="chkMostrarTodos" runat="server" 
                        oncheckedchanged="chkMostrarTodos_CheckedChanged" 
                        Text="Mostrar Solo Datos de Muestra" AutoPostBack="true" Visible="false" style="font-family: Arial;font-size: 12px;font-weight: bold; cursor:pointer;" />
            <asp:CheckBox ID="chkDentroHorario" runat="server" 
                        Text="Dentro de vigencia" AutoPostBack="true" style="font-family: Arial;font-size: 12px;font-weight: bold; cursor:pointer;" 
                        oncheckedchanged="chkMostrarTodos_DentroHorario"  />
            </td>
        </tr>
        <tr>          
            <td style="width: 75%" align="left">
                <div id="divDatos" runat="server">
                </div>
            </td>
            <td style="width: 25%">


            
         
            </td>
        </tr>
        <tr style="display:block;">           
            <td rowspan="2" style="width: 100%; float:right;">               
                <div id="arbolPrev" class="demo" style="width:100%;height:auto;"></div>
            </td>        
        </tr>
        <tr style="display:block;">            
            
            <td>                
                                 
            </td>
        </tr>
        <tr>
            <td class="tdTituloGrafica" align="center" style="font-size: medium; font-family: Arial; font-weight:bold; color: Black;">                
             </td>
        </tr>
        <tr>
            <td>
                <canvas id="graficaBarras"  width="900px" height="700px" style=" overflow:auto;">[No canvas support]</canvas>  
            </td>
            <td class="panelesCatalogos" style=" display:block;">
           
                
           </td>        
        </tr>
        <tr>
        <td align="right">
            <font style="font-family: Arial;font-size: 12px;font-weight: bold;">N: </font>
            <font style="font-family: Arial;font-size: 12px;"><span id="totalResp"></span></font>
            <br />
            <font style="font-family: Arial;font-size: 12px;font-weight: bold;">Tasa de Respuesta: </font>
            <font style="font-family: Arial;font-size: 12px;"><span id="tasaResp"></span></font>
        </td>
        </tr>
    </table>
    <div style="display:none;">
        <asp:Button runat="server" ID="btnConsultar" onclick="btnConsultar_Click"/>
    </div>
    <div style="background-color:White;">
    
    </div>
    <asp:HiddenField runat="server" ID="hfUltimaPos" />    
    <input type="hidden" runat="server" id="hdnIdEncuesta" />    
    <input type="hidden" runat="server" id="hdndivEmail" />    
    <input type="hidden" runat="server" id="hdnIdSiguientePregunta" />
    <input type="hidden" runat="server" id="hdnTemporizador" />
    <div style=" display:none;">
    <canvas id="soporteCanvasPDF"  width="900px" height="700px" style=" overflow:auto;">[No canvas support]</canvas> 
    </div>
    <asp:HiddenField  runat="server" ID="valImages" />
    </form>
     <div class="imges" runat="server" id="contentImages">
            
     </div>   
   <div style=" display:none;">
        <canvas id="MyCanvas" width="400" height="400" > </canvas>
      <img id="MyPix">
    </div>
    </body>
</html>
