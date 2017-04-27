<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true" CodeBehind="FrmProgramacionDispos.aspx.cs" Inherits="EncuestasMoviles.Pages.FrmProgramacionDispos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="../js/jquery-1.5.1.js"></script>
    <script src="../Scripts/jquery.blockUI.js" type="text/javascript"></script>
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
               color: #FFFF00;
               background-color: #FEEFB3;
               background-image: url('../Images/alerta.png');
        }
        .error {
               color: #D8000C;
               background-color: #FFBABA;
               background-image: url('../Images/error.png');
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
        };
    </style>
    <script type="text/javascript">
        function sowModal() {
            $('div.modal').show();
        }
        function hideModal() {
            $('div.modal').hide();
        }
        function showMensaje(tipoMensaje, textMensaje) {
          
            $(".mensajes").removeClass("exito alerta error info").addClass(tipoMensaje.toString()).html(textMensaje.toString()).fadeIn(500).fadeOut(7000);

        }
        function menssage(idContenedor, tipoMensaje, textMensaje) {
          
            $("#"+idContenedor+"").removeClass("exito alerta error info").addClass(tipoMensaje.toString()).html(textMensaje.toString()).fadeIn(500).fadeOut(7000);
        }

        function tablaDisposProgramados() {

            $('div.modal').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "FrmProgramacionDispos.aspx/getDispositivosProgramados",
                data: "{}",
                dataType: "json",
                success: function (msg) {
                    var datos = jQuery.parseJSON(msg.d);
                    var Tabla = "";
                    Tabla += "<table cellpadding='0' cellspacing='0' border='0' class='display' id='tbDispoProgramado' width='100%'>";
                    Tabla += "<thead>";
                    Tabla += "<tr>";
                    Tabla += "<th class=''>Programacion</th>";
                    Tabla += "<th class=''>Encuesta</th>";
                    Tabla += "<th class=''>Tipo Programacion</th>";
                    Tabla += "<th class=''>Desc.Dispositivo</th>";
                    Tabla += "<th class=''>Estatus</th>";
                    Tabla += "<th class=''>Operaciones</th>";
                    Tabla += "</tr>";
                    Tabla += "</thead>";
                    Tabla += "<tbody id='bodytbDispoProgramado'>";
                    Tabla += "</tbody>";
                    Tabla += "</table>";
                    $("div.contentDisposProgramdos").html("").html(Tabla);
                    var Arreglo = new Array();
                    console.log(datos);
                    $.each(datos, function (index, value) {
                        var id = value["ID_DISPO"] + "|" + value["ID_ENC"] + "|" + value["ID_PROGRA"] + "|" + value["ID_PRO_DISPO"] + "|" + value["ID_TIP_PROGRA"];
                        Arreglo[index] = new Array(value['PROGRAMACION_NOMBRE'], value["ENCUESTA_NOMBRE"], value["TIPOPROGRAMACION_DESC"], value["DISPO_DESCRIPCION"], "<img src='" + value["ColorEstatus"] + "' id='" + id + "' style='cursor:pointer;' title='Estatus' />", "<img class='removeDispo' id='" + id + "' src='../Images/iconoeliminar.png' style='cursor:pointer;' title='Eliminar este dispositivo de la programacion?' />");

                    });
                    $('#tbDispoProgramado').dataTable({
                        //"bDestroy": true,
                        "fnDrawCallback":
			                function (oSettings) {
			                    this.css('width', '100%');
			                },
                        "bRetrieve": true,
                        "bPaginate": false,
                        "sPaginationType": "full_numbers",
                        "iDisplayLength": 5,
                        "aaData": Arreglo,
                        "bAutoWidth": false,
                                
                        "oLanguage": { "sProcessing": "Procesando, por favor espere...",
                            "sLengthMenu": "",
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
                        }

                    });

                    setInterval(function () { $('div.modal').hide(); }, 2000);
                }, error: function () {
                    setInterval(function () { $('div.modal').hide(); }, 2000);
                }
            });

        }


        function tablaProgramaciones() {
            $('div.modal').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "FrmProgramacionDispos.aspx/getProgramaciones",
                data: "{}",
                dataType: "json",
                success: function (msg) {
                    var datos = jQuery.parseJSON(msg.d);
                    var Tabla = "";
                    Tabla += "<table cellpadding='0' cellspacing='0' border='0' class='display' id='tbProgramaciones' width='100%'>";
                    Tabla += "<thead>";
                    Tabla += "<tr>";
                    Tabla += "<th class=''>Elegir</th>";
                    Tabla += "<th class=''>Programacion</th>";
                    Tabla += "<th class=''>Encuesta</th>";
                    Tabla += "<th class=''>Tipo Programacion</th>";
                    Tabla += "<th class=''>Fecha</th>";
                    Tabla += "<th class=''>Hora</th>";
                    Tabla += "<th class=''>Prev</th>";
                    Tabla += "</tr>";
                    Tabla += "</thead>";
                    Tabla += "<tbody id='bodytbProgramaciones'>";
                    Tabla += "</tbody>";
                    Tabla += "</table>";
                    $("div.contentProgramaciones").html("").html(Tabla);
                    var Arreglo = new Array();
                    console.log(datos);
                    $.each(datos, function (index, value) {
                        var id = value["IDENC"] + "|" + value["IDPROFECHASEMANA"] + "|" + value["IDTIPOPROGRA"] + "|" + value["IdProgramacion"];
                        Arreglo[index] = new Array("<input id='" + id + "' style='cursor:pointer;' name='rad' type='radio' class='rdoPrograDispo' value='" + value["IdProgramacion"] + "'/>", value['ProgramacionNombre'], value["ENCUESTANOMBRE"], value["DESCTIPOPROGRAMACION"], value["FECHA"], value["HORA"], "<img src='../Images/iconovistaorevia.png' class='prevDispos' id='" + value["IdProgramacion"] +"|"+id+ "' style='cursor:pointer;' title='Visualizar dispositivos asignados' />");

                    });
                    $('#tbProgramaciones').dataTable({
                        //"bDestroy": true,
                        "fnDrawCallback":
			                function (oSettings) {
			                    this.css('width', '100%');
			                },
                        "bRetrieve": true,
                        "bPaginate": false,
                        "sPaginationType": "full_numbers",
                        "iDisplayLength": 5,
                        "aaData": Arreglo,
                        "bAutoWidth": false,
                        "sScrollY": "250px",
                        "oLanguage": { "sProcessing": "Procesando, por favor espere...",
                            "sLengthMenu": "",
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
                        }

                    });

                    $("input[@type=radio].rdoPrograDispo:eq(0)").trigger("click");
                    setInterval(function () { $('div.modal').hide(); }, 2000);
                }, error: function () {
                    setInterval(function () { $('div.modal').hide(); }, 2000);
                }
            });
            
            
        }
        function getDispoXProgramacion(idEncuesta) {
            $('div.modal').show();
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "FrmProgramacionDispos.aspx/getDispoByProgramacion",
                data: "{'idEncuesta':'" + idEncuesta + "'}",
                dataType: "json",
                success: function (msg) {
                    var datos = jQuery.parseJSON(msg.d);
                    var Tabla = "";
                    Tabla += "<table cellpadding='0' cellspacing='0' border='0' class='display' id='tbDispositivos' width='100%'>";
                    Tabla += "<thead>";
                    Tabla += "<tr>";
                    Tabla += "<th class=''>Elegir</th>";
                    Tabla += "<th class=''>Desc.Dispositivo</th>";
                    Tabla += "<th class=''>Status</th>";
                    Tabla += "</tr>";
                    Tabla += "</thead>";
                    Tabla += "<tbody id='bodytbDispositivos'>";
                    Tabla += "</tbody>";
                    Tabla += "</table>";
                    $("div.contentbDispositivos").html("").html(Tabla);
                    var Arreglo = new Array();
                
                    $.each(datos, function (index, value) {

                        Arreglo[index] = new Array("<input style='cursor:pointer;' type='checkbox' class='checkDispo' value='" + value["IdDispo"] + "'/>", value['DescTel'], "<img src='" + value["ColorEstatus"] + "' style='cursor:pointer;' title='estatus' />");

                    });
                    $('#tbDispositivos').dataTable({
                        // "bDestroy": true,
                        "fnDrawCallback":
                    	function (oSettings) {
                    	    this.css('width', '100%');
                    	},
                        "aaSorting": [[1, "asc"]],
                        "bRetrieve": true,
                        "bPaginate": false,
                        "sPaginationType": "full_numbers",
                        "iDisplayLength": 5,
                        "sScrollY": "250px",
                        "aaData": Arreglo,
                        "bAutoWidth": false,
                        "oLanguage": { "sProcessing": "Procesando, por favor espere...",
                            "sLengthMenu": "",
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
                        }
                    });
                    setInterval(function () { $('div.modal').hide(); }, 2000);
                    
                }, error: function (request, status, error) {
                  
                    setInterval(function () { $('div.modal').hide(); }, 2000);
                }
            });
        }

        function signar() {
            $('div.modal').show();
            var idEnc = "", idTipoProg = "", idProg = "";
            var radio = $("input[@type=radio].rdoPrograDispo:checked");           
            var datos = $(radio).attr("id");
            idEnc = datos.split("|")[0];
            idTipoProg = datos.split("|")[2];
            idProg = datos.split("|")[3];

            if (idProg != "") {
                if ($("input[@type=checkbox].checkDispo:checked").length > 0) {
                    var fallidas = 0;
                    var correctas = 0;
                    var reco = $("input[@type=checkbox].checkDispo:checked");
                    $.each(reco, function (i, v) {                        
                        $(function () {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "FrmProgramacionDispos.aspx/asignarDispoProgramado",
                                data: "{'idProgram':'" + idProg + "','idDispo':'" + $(v).val() + "','idEnc':'" + idEnc + "','idTipoProgram':'" + idTipoProg + "'}",
                                dataType: "json",
                                async: false,
                                success: function (msg) {
                                    var respuesta = (msg.d);
                                    if (respuesta == "True") {
                                        $(this).attr("checked", false);
                                        correctas++;
                                    } else if (respuesta == "False") {
                                        fallidas++;
                                    }
                                }
                            });
                        });
                    });
                   
                    setInterval(function () { $('div.modal').hide(); }, 2000);
                    showMensaje("info", "De " + $("input[@type=checkbox].checkDispo:checked").length.toString()+" dispositivo(s) seleccionado(s) ,  "+fallidas+" fueron fallido(s) y "+ correctas+" se asignaron correctamente");
                    tablaDisposProgramados();
                } else {
                    setInterval(function () { $('div.modal').hide(); }, 2000);
                    showMensaje("info", "Favor de seleccionar por lo menos un dispositivo.");
                }
                
            } else {
                showMensaje("info", "Debe elegir la programacion.");
            }

        }
        function showDispositivos(datos) {
          
          $('div.modal').show();
          $.ajax({
              type: "POST",
              contentType: "application/json; charset=utf-8",
              url: "FrmProgramacionDispos.aspx/getDispoProgramedByProgram",
              data: "{'idProgramacion':'" + datos.split("|")[0] + "'}",
              dataType: "json",
              async: false,
              success: function (msg) {
                  var datos = jQuery.parseJSON(msg.d);                 
                  var Tabla = "";
                  Tabla += "<table cellpadding='0' cellspacing='0' border='0' class='display' id='tbModalDispos' width='100%'>";
                  Tabla += "<thead>";
                  Tabla += "<tr>";
                  Tabla += "<th class=''>Programacion</th>";
                  Tabla += "<th class=''>Encuesta</th>";
                  Tabla += "<th class=''>Tipo Programacion</th>";
                  Tabla += "<th class=''>Desc.Dispositivo</th>";
                  Tabla += "</tr>";
                  Tabla += "</thead>";
                  Tabla += "<tbody id='bodytbModalDispos'>";
                  Tabla += "</tbody>";
                  Tabla += "</table>";
                  $("div.contentDispoPreview").html("").html(Tabla);
                  var Arreglo = new Array();

                  $.each(datos, function (index, value) {

                      Arreglo[index] = new Array(value['PROGRAMACION_NOMBRE'], value["ENCUESTA_NOMBRE"], value["TIPOPROGRAMACION_DESC"], value["DISPO_DESCRIPCION"]);

                  });
                  $('#tbModalDispos').dataTable({
                      "bDestroy": true,
                      "fnDrawCallback":
                        function (oSettings) {
                            this.css('width', '100%');
                        },
                      //"aaSorting": [[1, "asc"]],
                      //"bRetrieve": true,
                      "bPaginate": false,
                      "sPaginationType": "full_numbers",
                      "iDisplayLength": 5,
                      "sScrollY": "360px",
                      "aaData": Arreglo,
                      "bAutoWidth": false,
                      "oLanguage": { "sProcessing": "Procesando, por favor espere...",
                          "sLengthMenu": "",
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
                      }
                  });
                  $("#Body_showdispo").trigger("click");
                  setInterval(function () { $('div.modal').hide(); }, 2000);

              }, error: function (request, status, error) {

                  setInterval(function () { $('div.modal').hide(); }, 2000);
              }
          });

      }

      function removeDispositivo(respuesta) {
        
             $('div.modal').show();
             var idProgra=respuesta.split("|")[2];
             var idEnc=respuesta.split("|")[1];
             var idDisp=respuesta.split("|")[0];
             var idTipoProgra=respuesta.split("|")[4];
             var idPrograDispo=respuesta.split("|")[3];
             $.ajax({
                 type: "POST",
                 contentType: "application/json; charset=utf-8",
                 url: "FrmProgramacionDispos.aspx/eliminaDispoByProgram",
                 data: "{'idProgramacion':'" + idProgra + "','idEncuesta':'" + idEnc + "','idDispositivo':'" + idDisp + "','idTipoProgramacion':'" + idTipoProgra + "','IdProDispo':'" + idPrograDispo + "'}",
                 dataType: "json",
                 async: false,
                 success: function (msg) {
                     var resp = (msg.d);
                     if (resp == "True") {
                         setInterval(function () { $('div.modal').hide(); }, 2000);
                         showMensaje("exito", "Dispositivo fue eliminado correctamente");
                         tablaDisposProgramados();
                     } else if (resp == "False") {
                         setInterval(function () { $('div.modal').hide(); }, 2000);
                         showMensaje("error", "El dispositivo no se elimino, favor de intentar nuevamente");
                     }

                 }, error: function () {
                     showMensaje("error", "Erro al procesar solicitud ");
                     setInterval(function () { $('div.modal').hide(); }, 2000);
                 }
             });    
      }
      $(document).ready(function () {
          tablaProgramaciones();
          tablaDisposProgramados();
          $("#Body_btnAsignaDispos").die().live("click", function (e) {
              e.preventDefault();
              signar();
          });
          $("img").die().live("click", function () {
              var data = $(this).attr("id");
              if ($(this).hasClass("prevDispos")) {
                  showDispositivos(data);
              } else if ($(this).hasClass("removeDispo")) {
                if (confirm("¿Seguro que desea eliminar este registro?")) {
                    removeDispositivo(data);
                }
                  
              }
          });

          $("input[@type=radio].rdoPrograDispo").die().live("click", function () {

              var idEncuesta = $(this).val();
              getDispoXProgramacion(idEncuesta.toString());

          });

      });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="modal"></div>
    <div style="  margin-bottom:9px;">
    <br />
    <br />
    <center><asp:Label runat="server" ID="lblEncabezado" Font-Size="X-Large" ForeColor="Black"
                    Text="ASIGNACIÓN DE DISPOSITIVOS A PROGRAMACIONES"></asp:Label></center>

    <br />
    <div id="divMensaje" class="exito mensajes" style=" display:none; " runat="server">Mensaje de éxito de la operación realizada</div>
    <div style=" float:left; width:100%;">
        <div style=" float:left; width:80%;" >
       
        </div>
        <span style=" float:right; width:15%;">
            <asp:Button OnClick="btnAsignar_Click"  Text="Asignar Dispositivos" style=" cursor:pointer;"  runat="server" ID="btnAsignaDispos"/>
        </span>
     </div>
    <br />
    <div style=" width:100%; float:left;">        
        <div style=" width:64%; float:left;height:330px;margin-left:4px; margin-right:4px;">     
                <div class="contentProgramaciones" style=" width:100%;"></div>
        </div>
        <div style=" width:34%; float:right; height:330px; margin-left:4px; margin-right:4px;">
            <div class="contentbDispositivos" style=" width:100%;"></div>
        </div>
    </div>
    
  
 <center><asp:Label runat="server" ID="Label1" Font-Size="X-Large" ForeColor="Black"
                    Text="DISPOSITIVOS PROGRAMADOS"></asp:Label></center>

 <div style="width: 100%; height: auto; margin-bottom:20px; padding-bottom:20px; background-color: #F2F2F2; text-align: center"> 
 
    <div class="contentDisposProgramdos" style=" width:100%;"></div>
 </div>
 </div>
 <div style="display: none">
 <asp:Label ID="showdispo" runat="server" Text=""></asp:Label>
 </div>
  <asp:Panel ID="pnlDispoAsig" runat="server" CssClass="ContenedorGeneral" style = "display:none;width: 700px; height:500px;">
    <asp:Panel runat="server" ID="pnlTitulopnlDispoAsig" CssClass="MasterTituloContenedor">
        <table width="100%" id="Table1" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="left"><h2 runat="server" id="headEditAddEnch2">.:: PREVIEW - DISPOSITIVOS ASIGNADOS ::.</h2></td>
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
            <h2></h2>
        </div>
        <div style="display: inline-block; padding: 0 0 10px -10px;">
            <h2><asp:Label runat="server" ID="lblDescProgramacion"></asp:Label></h2>            
        </div>
        
    </div>
    <center>
        <div class="contentDispoPreview" style=" width:100%;" ></div>                  
    </center>

</asp:Panel>


<asp:ModalPopupExtender ID="popupDisposAsignados" runat="server" DropShadow="false"
    PopupControlID="pnlDispoAsig" TargetControlID = "showdispo" PopupDragHandleControlID="pnlTitulopnlDispoAsig"
    BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
</asp:Content>
