<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true" CodeBehind="ReporteRespuestaPorEncuesta.aspx.cs" Inherits="EncuestasMoviles.Pages.ReporteRespuestaPorEncuesta" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.metadata.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Scripts/ZeroClipboard.js" type="text/javascript"></script>
    <script src="../Scripts/TableTools.js" type="text/javascript"></script>


    <link href="../Styles/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableTools.css" rel="stylesheet" type="text/css" />

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
               color: #00529B;
               background-color: #BDE5F8;
               background-image: url('../Images/info.png');
        }
        .exito {
               color: #4F8A10;
               background-color: #DFF2BF;
               background-image:url('../Images/exito.png');
        }
        .alerta {
               color: #9F6000;
               background-color: #FEEFB3;
               background-image: url('../Images/alerta.png');
        }
        .error {
               color: #D8000C;
               background-color: #FFBABA;
               background-image: url('../Images/error.png');
        }
    </style>

    <script type="text/javascript">

        function GetEncuestas(){

            $.ajax({
                type: "POST",
                url: "ReporteRespuestaPorEncuesta.aspx/GetEncuestas",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({}),
                dataType: "json",
                async: false,
                success: function (datos) {
                    var valores = eval(datos.d);                    
                    var select = "<select id='selectEncuesta'><option value='-1'>.:.SELECCION DE ENCUESTA.:.</option>";
                    $.each(valores, function (index, value) {
                        select += "<option value=" + value["IdEncuesta"] + ">" + value["NombreEncuesta"] + "</option>";
                    });
                    select += "</select>";
                    $("div.comboEncuestas").html("").html(select);
                    
                },
                error: function () {
                    alert("error al solicitar encuestas");
                }
            });
        
        }
        function GetReporte(idEnc) {

            $.ajax({
                type: "POST",
                url: "ReporteRespuestaPorEncuesta.aspx/GetReporte",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ idEncuesta: idEnc }),
                dataType: "json",
                async: false,
                success: function (datos) {
                    var ArrayData = new Array();
                    var valores = jQuery.parseJSON(datos.d);
                    if (valores != null) {
                        debugger;
                        var tablaNodoA = "<table cellpadding='0' cellspacing='0' border='0' class='display' id='tbUsuarios' width='100%'>";
                        tablaNodoA += "<thead>";
                        tablaNodoA += "<tr>";
                        var tablaNodoB = "";


                        var catMaximo = 0;
                        $.each(valores, function (index, value) {
                            var catCuenta = value["Catalogos"].split("|").length;
                            if (catCuenta > catMaximo) {
                                catMaximo = catCuenta;
                            }
                        });

                        $.each(valores, function (index, value) {

                            var catalogos = value["Catalogos"].split("|").length;
                            var incremento = parseInt(catMaximo) - parseInt(catalogos);
                            var contador = 0;
                            ArrayData[index] = new Array(value["UsuaNom"], value["UsuGen"], value["UsuGrEdad"], value["UsuEnc"]);

                            $.each(value["Catalogos"].split("|"), function (ind, val) {
                                var datoSplit = val.split(">");                                
                                ArrayData[index].push("<font color='#FF0000'>" + datoSplit[0] + "</font>");
                                ArrayData[index].push("<font color='#0000FF'>" + datoSplit[1]+"</font>");
                            });
                            if (incremento != 0) {
                                for (var i = 0; i < (parseInt(incremento)) * (2); i++) { 
                                    ArrayData[index].push("No asignado");
                                }
                            }
                        });


                        tablaNodoA += "<th align='center' title='Nombre'>Nom.</th><th align='center' title='Genero'>Gen.</th><th align='center' title='Grupo Edad'>G.Ed.</th><th align='center' title='Encuesta'>Enc.</th>";
                        for (var j = 0; j < (parseInt(catMaximo)) * (2); j++) {
                            tablaNodoA += "<th align='center'>C" + j.toString() + "</th>";
                        }
                        tablaNodoA += "</tr>";
                        tablaNodoA += "</thead>";
                        tablaNodoA += "<tbody id='bodytbUsuarios'>";                       
                        tablaNodoA += "</tbody>";
                        tablaNodoA += "</table>";
                        $("div.gridReporte").html("").html(tablaNodoA);


                        $('#tbUsuarios').dataTable({
                            "bDestroy": true,
                            "asStripClasses": null,
                            "aaData": ArrayData,
                            "fnDrawCallback":
                    		    function () {
                    		        this.css('width', '100%');
                    		    },
                            "sPaginationType": "full_numbers",
                            "bPaginate": false,
                            "iDisplayLength": 5,
                            "sScrollY": "500px",
                            "sScrollX": "900px",
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
                    } else {
                        showMensaje("info", "La solicitud no arrojo ningun resultado.");
                        $("div.gridReporte").html("");
                    }

                }, error: function () {

                    alert("error en solicitud");
                }
            });
        }


        $(document).on("ready", function () {

            GetEncuestas();

            $("#btnGetReporte").on("click", function (e) {
                e.preventDefault();
                var idEnc = $("#selectEncuesta option:selected").val();
                if (idEnc != -1) {
                    GetReporte(idEnc);
                }

            });

        });
        function showMensaje(tipoMensaje, textMensaje) {
            $(".mensajes").removeClass("exito alerta error info").addClass(tipoMensaje.toString()).html(textMensaje.toString()).fadeIn(500).fadeOut(7000);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
  
   <center><asp:Label runat="server" ID="lblEncabezado" Font-Size="X-Large" ForeColor="Black"
                    Text="REPORTE DE RESPUESTAS POR ENCUESTA"></asp:Label></center>
   <br />
   <div id="divMensaje" class="exito mensajes" style=" display:none;" runat="server">Mensaje de éxito de la operación realizada</div>
   <br />
    <center>    <div class="comboEncuestas"></div><button id="btnGetReporte" class="" style="cursor:pointer;" >Obtener Reporte</button> </center>

    <br />
   <br />
   
    <br />
    <div style=" width:100%; height:auto; margin-bottom:20px; padding-bottom:20px;  background-color: #FFFFFF; text-align: center">
        <div class="gridReporte"></div>
    </div>


    <br />
</asp:Content>
