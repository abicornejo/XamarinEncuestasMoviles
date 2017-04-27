<%@ Page Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true"
    CodeBehind="frmUsuarios.aspx.cs" Inherits="EncuestasMoviles.Pages.frmUsuarios" %>
<%@ Register TagName="ctrlMensaje" TagPrefix="msje" Src="~/MessageBox.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content11" ContentPlaceHolderID="Body" runat="server">
   
    <script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../js/Usuarios.js" type="text/javascript"></script>
    <script src="../js/AltaUsuario.js" type="text/javascript"></script>

    <script src="../Scripts/jquery.metadata.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Scripts/ZeroClipboard.js" type="text/javascript"></script>
    <script src="../Scripts/TableTools.js" type="text/javascript"></script>
    <link href="../Styles/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableTools.css" rel="stylesheet" type="text/css" />
    <style>
      .thumb {
        height: 75px;
        border: 1px dotted  #000;
        margin: 10px 5px 0 0;
  }
  
</style>
    <script type="text/javascript">
        function showMensaje(tipoMensaje, textMensaje) {
            $(".mensajes").removeClass("exito alerta error info").addClass(tipoMensaje.toString()).html(textMensaje.toString()).fadeIn(500).fadeOut(7000);
        }
        function showMsg(tipoMensaje, textMensaje) {
            $(".divMensaje").removeClass("exito alerta error info").addClass(tipoMensaje.toString()).html(textMensaje.toString()).fadeIn(500).fadeOut(7000);
        }
        function LoadImg() {
            var reader = new FileReader();
            reader.onload = (function (e) {
                $("#list").html("");          
                var span = document.createElement('span');
                span.innerHTML = ['<img class="thumb" src="', e.target.result,
                            '" title="', escape(e.name), '"/>'].join('');
                document.getElementById('list').insertBefore(span, null);
            });
            reader.readAsDataURL(document.getElementById('userImage').files[0]);
        }

        function CargarGridUsers(nombre, apePaterno, apeMaterno, sexo, catalogos) {

            $.ajax({
                type: "POST",
                url: "frmUsuarios.aspx/CargaGridUsers",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ nombre: nombre, apePaterno: apePaterno, apeMaterno: apeMaterno, sexo: sexo, Catalogos: catalogos }),
                dataType: "json",
                async: false,
                success: function (datos) {
                    var tablaNodoA = "<table cellpadding='0' cellspacing='0' border='0' class='display' id='tbUsuarios' width='100%'>";
                    tablaNodoA += "<thead>";
                    tablaNodoA += "<tr>";
                    var tablaNodoB = "";
                    var valores = jQuery.parseJSON(datos.d);

                    var catExterno = 0;
                    $.each(valores, function (index, value) {
                        var catInterno = value["Catalogos"].split("|").length;
                        if (catInterno > catExterno) {
                            catExterno = catInterno;
                        }
                    });
                    console.log(valores);
                    $.each(valores, function (index, value) {

                        var catalogos = value["Catalogos"].split("|").length;
                        var dataDispositivo = value["DispoDesc"] + ":" + value["DispoModelo"] + ":" + value["DispoMarca"] + ":" + value["DispoMeid"] + ":" + value["DispoTelefono"] + ":" + value["UsuaNom"];
                        var valToRef = dataDispositivo + ";" + value["UsuarioSexo"] + ";" + value["IdDisposisito"] + ";" + value["Catalogos"] + ";" + value["UsuarioLlavePrimaria"];
                        var incremento = parseInt(catExterno) - parseInt(catalogos);
                        var contador = 0;
                        tablaNodoB += "<tr id=" + value["UsuarioLlavePrimaria"] + ">";
                        tablaNodoB += "<td><img src='" + value["UsuarioFoto"] + "'/></td>";
                        tablaNodoB += "<td>" + value["UsuaNom"] + "</td>";

                        $.each(value["Catalogos"].split("|"), function (ind, val) {
                            tablaNodoB += "<td>" + val + "</td>";
                        });
                        if (incremento != 0) {
                            for (var i = 0; i < incremento; i++) {
                                tablaNodoB += "<td><strong>No asignado</strong></td>";
                            }
                        }

                        tablaNodoB += "<td>" + value["DescDispositivo"] + "</td>";
                        tablaNodoB += "<td><img class='opt editar' src='../Images/iconoeditar.png'  title='Editar Persona' style=' cursor:pointer;' value='" + valToRef + "' />" +
                                      "<img class='opt eliminar' src='../Images/iconoeliminar.png' title='Eliminar Persona' style=' cursor:pointer;' value='" + valToRef + "' /></td>";
                        tablaNodoB += "</tr>";
                    });


                    tablaNodoA += "<th>Foto</th><th>Nombre Usuario</th>";
                    for (var j = 0; j < catExterno; j++) {
                        tablaNodoA += "<th>Catalogo" + j.toString() + "</th>";
                    }
                    tablaNodoA += "<th>Desc.Dispo</th><th>Operaciones</th>";
                    tablaNodoA += "</tr>";
                    tablaNodoA += "</thead>";
                    tablaNodoA += "<tbody id='bodytbUsuarios'>";
                    tablaNodoA += tablaNodoB;
                    tablaNodoA += "</tbody>";
                    tablaNodoA += "</table>";
                    $("div.gridUsers").html("").html(tablaNodoA);
                    $('#tbUsuarios').dataTable({
                        "bDestroy": true,
                        "asStripClasses": null,
                        "fnDrawCallback":
                    		    function () {
                    		        this.css('width', '100%');
                    		    },
                        "sPaginationType": "full_numbers",
                        "bPaginate": false,
                        "iDisplayLength": 5,
                        "sScrollY": "200px",
                        "sScrollX": "898px",
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
                    $("img.opt").on("click", function (e) {
                        var valoresDisp = $(this).attr("value").split(";");

                        if ($(this).hasClass("editar")) {
                            $("#saveUsr").data("opcion", 2);
                            $(".pnlUsrDispo").val("");
                            $(".txtNameUsr").val(valoresDisp[0].split(":")[5]);
                            $(".txtTelDispo").val(valoresDisp[0].split(":")[4]);
                            $(".txtModeloDispo").val(valoresDisp[0].split(":")[1]);
                            $(".txtMarcaDispo").val(valoresDisp[0].split(":")[2]);
                            $(".txtDescDispo").val(valoresDisp[0].split(":")[0]);
                            $(".txtImeiDispo").val(valoresDisp[0].split(":")[3]);
                            var selects = $("select.selectUsrCata option");
                            $.each(selects, function (ind, val) {
                                $.each(valoresDisp[3].split("|"), function (index, valor) {
                                    if ($(val).text().toUpperCase() == valor.toUpperCase()) {
                                        var current = $(val).val();
                                        $('select.selectUsrCata option[value=' + current + ']').prop("selected", true);
                                    }
                                });
                            });

                            var selectSex = $("select.selectSexoCata option");

                            $.each(selectSex, function (a, b) {
                                if ($(b).val().toUpperCase() == valoresDisp[1].toString().toUpperCase()) {
                                    $(b).prop("selected", true);
                                }
                            });
                            $("#saveUsr").data("idUser", valoresDisp[4]);
                            $("#saveUsr").data("idDispo", valoresDisp[2]);
                            $("#Body_ModalAltaUsuario").trigger("click");
                        } else if ($(this).hasClass("eliminar")) {
                            if (confirm("ALERTA!! va a proceder a eliminar el registro "+valoresDisp[0].split(":")[5])) {

                                $("div.modal").show();
                               
                                $.ajax({
                                    type: "POST",
                                    url: "frmUsuarios.aspx/DeleteUserDispo",
                                    contentType: "application/json; charset=utf-8",
                                    data: JSON.stringify({ idUser: valoresDisp[4], datosDispo: $(this).attr("value") }),
                                    dataType: "json",
                                    //async: false,
                                    success: function (datos) {
                                        var valores = eval(datos.d);
                                        if (valores[0]["Success"] == "Error") {
                                            showMsg("error", valores[0]["Description"]);
                                            $("div.modal").hide();
                                        } else if (valores[0]["Success"] == "OK") {
                                            showMsg("exito", valores[0]["Description"]);
                                            catalogos = "";
                                            var items = $("div.pnlCatalogos").find(":checkbox");
                                            $.each(items, function (ind, value) {
                                                catalogos += $(value).val() + ",";
                                            });
                                            catalogos = catalogos.substr(0, catalogos.length - 1);
                                            CargarGridUsers("", "", "", "", catalogos);
                                            $("div.modal").hide();
                                        } else {
                                            showMsg("error", "Error inesperado del sistema");
                                            $("div.modal").hide();
                                        }
                                    },
                                    error: function () {
                                        $("div.modal").hide();

                                    }
                                });


                            } else { return false; }
                        }

                    });

                },
                error: function () {
                    alert("Ocurrio un error verifique");
                }
            });
        
        }

        $(document).on("ready", function () {

            ObtenerCatalogos();
            catalogos = "";
            var items = $("div.pnlCatalogos").find(":checkbox");
            $.each(items, function (ind, value) {
                catalogos += $(value).val() + ",";
            });

            catalogos = catalogos.substr(0, catalogos.length - 1);
            CargarGridUsers("", "", "", "", catalogos);

            var indica = 0;
            var arreglo = new Array("divA", "divB", "divC");

            $("button.btnUsrPnl").on("click", function (e) {
                e.preventDefault();
                if (indica == 0) {
                    var hayNulos = false;
                    $(".ctrlA").each(function (i, element) {
                        if ($(element).val() == "") {
                            hayNulos = true;
                        }
                    });
                    if (hayNulos) {
                        alert("Existen Campos Vacios por favor verifique");
                        return;
                    }
                }

                $("div.pnlUsr").hide();
                if ($(this).hasClass("btnForward")) {
                    indica--;
                } else if ($(this).hasClass("btnNext")) {
                    indica++;
                }
                $("div." + arreglo[indica]).show();
            });
            $("#btnBuscar").unbind().bind("click", function (e) {
                e.preventDefault();
                var catalogos = "";
                var nombre = $("#Body_txtBusqNombUsu").val();
                var sexo = $("select#Body_DdlSexo option:selected").val();
                var div = $("div.pnlCatalogos").css("display");
                if (div.toString() == "block") {
                    var items = $("div.pnlCatalogos").find(":checkbox:checked");
                    $.each(items, function (ind, value) {
                        catalogos += $(value).val() + ",";
                    });
                    catalogos = catalogos.substr(0, catalogos.length - 1);
                } else {
                    var items = $("div.pnlCatalogos").find(":checkbox");
                    $.each(items, function (ind, value) {
                        catalogos += $(value).val() + ",";
                    });
                    catalogos = catalogos.substr(0, catalogos.length - 1);
                }
                CargarGridUsers(nombre, "", "", sexo, catalogos);
            });
            var showPanel = false;
            $("#btnBusqEspe").unbind().bind("click", function (e) {
                e.preventDefault();

                if (!showPanel) {
                    $("div.pnlCatalogos").show();
                    showPanel = true;
                } else {
                    $("div.pnlCatalogos").hide();
                    showPanel = false;
                }
            });

            $("#btnNewUsr").on("click", function () {
                $(".pnlUsrDispo").val("");
                $("#saveUsr").data("opcion", 1);
                $("#Body_ModalAltaUsuario").trigger("click");
            });

            $("#saveUsr").on("click", function (e) {
                e.preventDefault();
                var opt = $("#saveUsr").data("opcion");
                sendDatos(opt);
            });
        });
        
        function sendDatos(OpcionUSer) {         
         var txts = $(".pnlUsrDispo");
                var campostxt = "";
                var opcionesCatalogo = "";
                var nulos = false;
                var elementDesc = "";
                $.each(txts, function (ind, value) {
                    if ($(value).val() == "") {
                        nulos = true;
                        elementDesc = $(value).attr("name");
                        return false;
                    } else {
                        campostxt += $(value).val() + ";";
                    }
                });

                if (nulos) {
                    showMensaje("info", "El campo " + elementDesc + " es requerido verifique.");
                } else {
                    var catalogos = $(".selectUsrCata");
                    $.each(catalogos, function (i, element) {
                        var option = "";
                        option = $(element).find("option:selected").val();

                        if (option == "" || option == -1) {
                            nulos = true;
                            elementDesc = $(element).attr("name");
                            return false;
                        } else {
                            opcionesCatalogo += option + ",";
                        }

                    });

                    if (nulos) {
                        showMensaje("info", "El catalogo " + elementDesc + " es requerido verifique.");
                    } else {
                        opcionesCatalogo = opcionesCatalogo.substring(0, opcionesCatalogo.length - 1);
                        campostxt = campostxt + $(".selectSexoCata option:selected").val();
                        if (OpcionUSer==2){
                            campostxt+=";"+$("#saveUsr").data("idUser")+";"+$("#saveUsr").data("idDispo");
                        }

                        $.ajax({
                            type: "POST",
                            url: "frmUsuarios.aspx/SaveUsuario",
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify({ Parametros: campostxt, Catalogos: opcionesCatalogo, Opcion: OpcionUSer }),
                            dataType: "json",
                            async: false,
                            success: function (datos) {
                                $("div.modal").show();
                                var valores = eval(datos.d);
                                if (valores[0]["Success"] == "Error") {
                                    $("div.modal").hide();
                                    showMensaje("error", valores[0]["Description"]);
                                } else if (valores[0]["Success"] == "OK") {
                                    $(".pnlUsrDispo").val("");
                                    if (OpcionUSer == 1) {
                                        $(".selectUsrCata [value=-1]").prop("selected", true);
                                    }
                                    showMensaje("exito", valores[0]["Description"]);
                                    $("div.modal").hide();

                                } else {
                                    showMensaje("error", "Error inesperado del sistema");
                                    $("div.modal").hide();
                                }
                            },
                            error: function () {
                                $("div.modal").hide();
                            }
                        });
                    }
                }
        }


        function ObtenerCatalogos() {

            $.ajax({
                type: "POST",
                url: "frmUsuarios.aspx/ObtenerCatalogos",
                contentType: "application/json; charset=utf-8",
                data: "{}",
                dataType: "json",
                async: false,
                success: function (datos) {

                    var datosCatalogo = jQuery.parseJSON(datos.d);
                    if (datosCatalogo != null) {
                        var html = "<dl class='accordion'>";
                        var contador = 0; console.log(datosCatalogo);
                        $.each(datosCatalogo, function (index, value) {
                            html += "<dt><a href='javascript:void(0)'>" + index.toString() + "</a></dt>";
                            html += "<dd>";
                            html += "<select class='selectUsrCata' name='" + index.toString() + "' ><option value='-1'>.:.Selecccione.:.</option>";
                            var items = value.split(";");
                            $.each(items, function (ind, val) {
                                var otherItem = val.split("|");
                                html += "<option value='" + otherItem[0] + "'>" + otherItem[1] + "</option>";
                            });
                            html += "</select>";
                            html += "</dd>";
                            contador++;
                        });
                        html += "</dl>";
                    }
                    $("div.divCatalogosUsr").html("").html(html);
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

                }, error: function () {
                    alert("Error en el proceso");
                }
            });

        }

        function showMensaje(tipoMensaje, textMensaje) {
            $(".mensajeMdlUsuario").removeClass("exito alerta error info").addClass(tipoMensaje.toString()).html(textMensaje.toString()).fadeIn(500).fadeOut(7000);
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
       }.accordion
  {
   width:200px;    
  
  }.accordion dd,.accordion dt
    {
       padding:10px;
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
    }.info, .exito, .alerta, .error {
               font-family:Arial, Helvetica, sans-serif; 
               font-size:13px;
               border: 1px solid;
              /* margin: 10px 0px;*/
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
    </style>

    <div class="modal"></div>
   
    <asp:HiddenField runat="server" ID="hfIdRButton" />
    <msje:ctrlMensaje runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Aceptado_Evento" />
    <div runat="server" id="Tabs" style="display: none">
    </div>    
    <br /> 
    <div id="divMensaje" class="exito divMensaje" style=" display:none; margin:10px 0px;" runat="server">Mensaje de éxito de la operación realizada</div>   
    <table width="95%" align="center">
        <tr>
            <td align="right">
                 <asp:Button Visible="false" runat="server" ID="btnNuevoUsuario" Width="120" CssClass="btnMasterRectangular" Text="Nueva Persona" OnClick="btnNuevoUsuario_Click" CausesValidation="false" />
                <button" id="btnNewUsr" class="btnMasterRectangular" style="cursor:pointer;">Nueva Persona</button>
            </td>
        </tr>
    </table>
    <br />
     
    <%--fieldset de controles busqueda de Usuarios--%>
    <div class="ContenedorGeneral" style="height: auto; width: 95%">
        <div class="MasterTituloContenedor">
            <h2>
                Filtros de Busqueda de Usuarios
            </h2>
        </div>
        <table width="100%">
            <tr>                
                <td style="width:80%">
                    <table style="width:80%" align="left">
                        <tr>
                            <td style="padding-left: 10px">
                                Nombre:
                            </td>
                            <td>
                                <asp:TextBox CssClass="inputtext" runat="server" ID="txtBusqNombUsu"
                                Width="300px"></asp:TextBox>
                            </td>    
                            <td rowspan="3">
                            
                            </td>                       
                        </tr>
                        <tr>
                            <td style="padding-left: 10px">
                                Sexo:
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="DdlSexo" runat="server" Width="100px" 
                                    CssClass="styleCombosCat">
                                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                                </asp:DropDownList> 
                            </td>                        
                        </tr>
                       
                        <tr>
                            <td style="padding-left: 10px">
                                <asp:Label ID="LblValida" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                                Seleccione Busqueda:
                            </td>
                            <td>
                            <button id="btnBusqEspe" class="btnMasterRectangular" style="cursor:pointer;">Busqueda Especializada</button>
                                <div class="pnlCatalogos" style=" display:none; position:absolute; width:120px; z-index:1000000000;">
                                    <telerik:radpanelbar runat="server" ID="RadPBusqCatalogos" Skin="Windows7"
                                    ExpandMode="MultipleExpandedItems" Width="100%" EnableViewState="true" >
                                    </telerik:radpanelbar>          
                                </div>
                            </td>
                        </tr> 
                                          
                        <tr>
                            <td valign="middle" style="height: 50px" colspan="6" align="center" >                              
                                
                            </td> 
                            <td></td>                           
                        </tr>
                    </table>
                </td> 
                <td style=" width:18%;">                                
                    <button id="btnBuscar" class='btnMasterRectangular' style=' cursor:pointer;'>Buscar</button>
                </td>              
            </tr>
        </table>
    </div>

    <table width="95%" align="center" id="contrls" style="height: auto; background-color: #F2F2F2; text-align: center">
        <tr align="right" >
            <td>
                <asp:ImageButton ID="ImgExportPdf" runat="server" CausesValidation="false" 
                    onclick="ImgExportPdf_Click" Height="25px" ImageUrl="~/Images/pdf.bmp" 
                    Width="25px" Visible="false"/>                    
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                    
                <asp:ImageButton ID="ImgExportExcel" runat="server" 
                    onclick="ImgExportExcel_Click" Width="25px" CausesValidation="false" 
                    Height="25px" ImageUrl="~/Images/icono_excel.jpg" Visible="false"  />
            </td>
        </tr>
        <tr>
            <td class="ContenedorGeneral" style="height: auto; width: 100%; position: static; border: 1px solid #3F3F3F;">
                <asp:GridView runat="server" ID="gvAltaUsuario" CellPadding="4" ForeColor="#333333"
                    AutoGenerateColumns="false" Width="100%" GridLines="Both"
                    OnRowCommand="gvAltaUsuario_RowCommand" CssClass="grid sortable {disableSortCols: [4]}" Visible="false">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="UsuarioLlavePrimaria"   Visible="false"/>
                        <asp:TemplateField HeaderText="Foto">
                            <ItemTemplate>
                                <img alt="imagen" id="imgDisp" width="40px" height="40px" src="<%# Eval("UsuarioFoto") %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dispositivo">
                            <ItemTemplate>
                                <a class="personPopupTrigger" href="#" rel="<%# Eval("UsuarioLlavePrimaria") %>"
                                    id="imgurl" style="color: Black">Ver</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Nombre" DataField="UsuarioNombre" />
                        <asp:BoundField HeaderText="A. Paterno" DataField="UsuarioApellPaterno" />
                        <asp:BoundField HeaderText="A. Materno" DataField="UsuarioApellMaterno" />
                        <asp:BoundField HeaderText="Email" DataField="UsuarioEmail" />
                        <asp:BoundField HeaderText="Teléfono Casa" DataField="UsuarioTelCasa" />
                        <asp:BoundField HeaderText="Celular Personal" DataField="UsuarioNumCelularPersonal" />
                        <asp:BoundField HeaderText="Desc. Cel" DataField="DescDispositivo" />
                        <asp:BoundField HeaderText="Catalogos" DataField="Catalogos" />
                        <asp:TemplateField HeaderText="Operaciones">
                            <ItemTemplate>
                                <asp:ImageButton Width="22px" ID="btnEditar" CausesValidation="false" UseSubmitBehavior="true"
                                    runat="server" ImageUrl="~/Images/iconoeditar.png" CommandName="Edita" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                    ToolTip="Editar Persona" />
                                <asp:ImageButton Width="22px" ID="btnEliminar" CausesValidation="false" UseSubmitBehavior="true"
                                    runat="server" ImageUrl="~/Images/iconoeliminar.png" CommandName="Elimina" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                    ToolTip="Eliminar Persona" />
                                <asp:ImageButton Width="22px" ID="btnAsignar" CausesValidation="false" UseSubmitBehavior="true"
                                    runat="server" ImageUrl="~/Images/IconoAsignar.png" CommandName="Asignar" CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                    ToolTip="Asigna Dispositivo" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                   <%-- <EditRowStyle BackColor="#999999" />
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
                <div class="gridUsers"></div>
            </td>
        </tr>
    </table>    
    <%--Modal Asigna Dispo--%>
    <div>
        <asp:Label ID="lblAsociaDispo" runat="server"></asp:Label>
        <asp:ModalPopupExtender ID="mpeAsignaDisp" runat="server" TargetControlID="lblAsociaDispo"
            PopupControlID="pnlAsociaDispo" BackgroundCssClass="modalBackground" DropShadow="true"
            PopupDragHandleControlID="pnlAsociaDispoHead" />
    </div>
    <div>
        <asp:Panel ID="pnlAsociaDispo" runat="server" CssClass="ContenedorGeneral" Style="display: block;
            width: 900px; height: 500px">
            <asp:Panel ID="pnlAsociaDispoHead" CssClass="MasterTituloContenedor" runat="server"
                Width="100%">
                <div style="float: left">
                    <h2>
                        <asp:Label runat="server" ID="lblTitAsigna"></asp:Label>
                    </h2>
                </div>
                <div style="float: right">
                    <asp:ImageButton runat="server" ID="btnCerrarAsigna" CausesValidation="false" ImageUrl="~/Images/Iconocerrar.png"
                        OnClick="btnCerrarAsigna_Click"/>
                </div>
            </asp:Panel>
            <div style="margin-top: 25px">
                <div align="center">
                    <br />
                    <br />
                    <table width="100%">
                        <tr>
                            <td>
                                <div class="MasterTituloContenedor">
                                    <h2>
                                        Busqueda de Dispositivos
                                    </h2>
                                </div>
                                <div>
                                    <div style="display: inline-block; text-align: left">
                                        Número de Teléfono:
                                    </div>
                                    <div style="display: inline-block;">
                                        <asp:TextBox CssClass="inputtext" onkeypress="return ValidaNumeros(event)" runat="server" ID="txtNumDispo" ValidationGroup="grouBuscaDis"
                                            Width="150px">
                                        </asp:TextBox>
                                        <asp:ValidatorCalloutExtender runat="server" ID="VCNumDispo" TargetControlID="rfvNumDispo"
                                            Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                        <asp:RequiredFieldValidator Display="None" ID="rfvNumDispo" ControlToValidate="txtNumDispo"
                                            SetFocusOnError="True" runat="server" ErrorMessage="<div>El Número de Teléfono es obligatorio</div>"
                                            ForeColor="Black">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div style="display: inline-block">
                                        <br />
                                        <asp:Button runat="server" CssClass="btnMasterRectangular" ID="btnBuscaDispo" Text="Buscar"
                                            OnClick="btnBuscaDispo_Click" ValidationGroup="grouBuscaDis" />
                                    </div>
                                    <div style="display: inline-block;">
                                        <input type="button" id="btnLimpia" class="btnMasterRectangular" onclick="limpiacampoBuscaDispo()"
                                            value="Limpiar" />
                                    </div>
                                    <div style="display: none;">
                                        <input type="button" id="btnCancelarAlta" class="btnMasterRectangular" onclick="CancelaBuscaDispo()"
                                            value="Cancelar" />
                                    </div>
                                    <br />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="MasterTituloContenedor">
                                    <h2>
                                    </h2>
                                </div>
                                <div style="text-align: center; display: block; text-align: right">
                                    <asp:Button ID="btnAceptaAsignado" runat="server" CssClass="btnMasterRectangular" OnClick="btnAceptaAsignado_Click"
                                        Text="Asignar" CausesValidation="false" />
                                    <asp:Button ID="btnCancelarAD" runat="server" CssClass="btnMasterRectangular" Text="Cancelar" CausesValidation="false" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <asp:ListView runat="server" ID="lvDispositivos" DataKeyNames="IdDispositivo">
                                    <LayoutTemplate>
                                        <ul class="ListDispositivos">
                                            <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                        </ul>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <img class="personPopupTrigger2" id="<%# Eval("IdDispositivo") %>" src="<%# Eval("ImagenTelefono") %>"
                                                alt="imagen" width="40px" height="40px"  /><br />
                                            <input name="Radio1" id="Radio1" type="radio" onclick="clickRButton(<%# Eval("IdDispositivo") %>)" />
                                            <%# Eval("DispositivoDesc") %></li>
                                    </ItemTemplate>
                                    <EmptyDataTemplate>
                                        <div>
                                            Sin Dispositivos Disponibles!!!
                                        </div>
                                    </EmptyDataTemplate>
                                </asp:ListView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
    
    <%--Modal Asigna Dispo--%>
    <%--Modal Alta Usuario--%>
    <asp:HiddenField runat="server" ID="hfRutaImg" />
    <div>
        <asp:Label ID="ModalAltaUsuario" runat="server"></asp:Label>
        <asp:ModalPopupExtender ID="mpeAltaUsuario" runat="server" TargetControlID="ModalAltaUsuario"
            PopupControlID="panelAltaUsuario" BackgroundCssClass="modalBackground" DropShadow="true"
            PopupDragHandleControlID="panelAltaUsuarioHead" CancelControlID="btnCancelarModal"/>
    </div>
    <div>
        <asp:Panel ID="panelAltaUsuario" runat="server" CssClass="ContenedorGeneral" Style="display: none;
            width: 800px; height:370px">  
            <asp:Panel ID="panelAltaUsuarioHead" CssClass="MasterTituloContenedor" runat="server"
                    Width="100%">
                    <div style="float: left">
                        <h2>
                            Alta/Edicion de Usuarios
                        </h2>
                    </div>
                    <div style="float: right">
                        <asp:ImageButton CausesValidation="false" runat="server" ImageUrl="~/Images/Iconocerrar.png"  OnClick="btnCerrarAltaUser_Click"
                           />
                    </div>
            </asp:Panel>
            
            <table width="100%" border="0" cellpadding="0" cellspacing="0" style=" display:none;">
            <tr>
            <td>
           
            </td>
            </tr>
                            
                <tr>
                    <td>
                       
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td class="MasterTituloContenedor">
                                            <h2>Datos Generales</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td width="35%" valign="top">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Nombre(s):
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtNombreUsuario" onkeypress="return validaLetras(event)"
                                                                        MaxLength="100" Width="250px" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaNombre" TargetControlID="ValidaNombre"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="ValidaNombre" ControlToValidate="txtNombreUsuario"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar el Nombre de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Apellido Paterno:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtApellPaterno" onkeypress="return validaLetras(event)"
                                                                        MaxLength="100" Width="250px" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaPater" TargetControlID="ValidaPater"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator ID="ValidaPater" ControlToValidate="txtApellPaterno"
                                                                        Display="None" SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar el Apellido Paterno de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Apellido Materno:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtApellMaterno" onkeypress="return validaLetras(event)"
                                                                        MaxLength="100" Width="250px" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaMater" TargetControlID="ValidaMater"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="ValidaMater" ControlToValidate="txtApellMaterno"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar el Apellido Materno de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Sexo:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="DdGenero" runat="server" Width="100px" 
                                                                        CssClass="styleCombosCat">
                                                                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                                                                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                                                                    </asp:DropDownList> 
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Fecha de Nacimiento:
                                                                </td>
                                                                <td>
                                                             
                                                                        <telerik:RadDatePicker ID="txtFechNacimiento" runat="server" Width="120px" ZIndex="19000" MinDate="01/01/1900">
                                                              
                                                                        </telerik:RadDatePicker>
                                                          
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Teléfono Casa:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtTelCasa" onkeypress="return ValidaNumeros(event)"
                                                                        MaxLength="15" Width="250px" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaTelCasa" TargetControlID="ValidaTelCasa"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="ValidaTelCasa" ControlToValidate="txtTelCasa"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar el Teléfono de Casa de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Celular Personal:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtCelPersonal" onkeypress="return ValidaNumeros(event)"
                                                                        MaxLength="15" Width="250px" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaCelPer" TargetControlID="ValidaCelPer"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="ValidaCelPer" ControlToValidate="txtCelPersonal"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar el Número de Celular Personal de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Email:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtEmail" MaxLength="100" Width="250px"
                                                                        ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaEmail" TargetControlID="ValidaEmail"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="ValidaEmail" ControlToValidate="txtEmail"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar el Email de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                        runat="server" ID="valEmai" ErrorMessage="El correo no es Valido" Display="None"></asp:RegularExpressionValidator>
                                                                    <asp:ValidatorCalloutExtender TargetControlID="valEmai" runat="server" ID="veEmai"
                                                                        CssClass="CustomValidatorCalloutStyle" Enabled="true">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Foto:
                                                                </td>
                                                                <td>
                                                                    <asp:FileUpload ID="subeFotoUsuario" CssClass="btnMasterRectangular" runat="server" EnableViewState="true"  />
                                                                    <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="<div style='color:red'>Solo se permiten imagenes JPEG, PNG, GIF</div>" 
                                                                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$" 
                                                                        ControlToValidate="subeFotoUsuario">
                                                                    </asp:RegularExpressionValidator>
                                                           
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Observaciones:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtObservaciones" Rows="3" TextMode="MultiLine"
                                                                        onkeypress="return validaCaracteresNoEncuestas(event)" MaxLength="200" Width="250px" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaObse" TargetControlID="ValidaObse"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="ValidaObse" ControlToValidate="txtObservaciones"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar Observaciones de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="35%" valign="top">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    Dirección:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtDireccion" onkeypress="return validaCaracteresNoEncuestas(event)"
                                                                        MaxLength="100" Width="250px" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaDirecc" TargetControlID="ValidaDirecc"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="ValidaDirecc" ControlToValidate="txtDireccion"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar la Dirección de la Persona</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Código Postal:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox CssClass="inputtext" runat="server" ID="txtCodigoP" TabIndex="0" onkeypress="return ValidaNumeros(event)"
                                                                        MaxLength="5" ValidationGroup="grouAltaUsua"></asp:TextBox>
                                                                    <asp:ValidatorCalloutExtender runat="server" ID="AValidaCP" TargetControlID="validaCP"
                                                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                                                    <asp:RequiredFieldValidator Display="None" ID="validaCP" ControlToValidate="txtCodigoP"
                                                                        SetFocusOnError="True" runat="server" ErrorMessage="<div>Necesita Ingresar el Codigo Postal</div>"
                                                                        ForeColor="Black" ValidationGroup="grouAltaUsua"></asp:RequiredFieldValidator>
                                                                    <asp:Button ID="btnCP" CssClass="btnMasterRectangular" runat="server" OnClick="btnCP_Click"
                                                                        Text="Buscar" Style="display: inline-block" ValidationGroup="grouAltaUsua" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Estado:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList Width="250px" runat="server" ID="dpEstado" OnSelectedIndexChanged="dpEstado_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                        <asp:ListItem>== SELECIONE ==</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Municipio:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList Width="250px" runat="server" ID="dpMunicipio" OnSelectedIndexChanged="dpMunicipio_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                        <asp:ListItem>== SELECIONE ==</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Asentamiento:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList Width="250px" runat="server" ID="dpAsentamiento">
                                                                        <asp:ListItem>== SELECIONE ==</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Zona:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList Width="250px" runat="server" ID="dpZona">
                                                                        <asp:ListItem>== SELECIONE ==</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Colonia:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList Width="250px" runat="server" ID="dpColonia" OnSelectedIndexChanged="dpColonia_SelectedIndexChanged"
                                                                        AutoPostBack="true">
                                                                        <asp:ListItem>== SELECIONE ==</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" valign="middle" style="text-align: center; height: 50px">
                                                                    <input class="btnMasterRectangular" type="button" id="btnGuarda" value="Guardar"
                                                                            onclick="obtImagen()" />
                                                                        <script type="text/javascript">
                                                                            function obtImagen() {
                                                                                document.getElementById("Body_hfRutaImg").value = document.getElementById("Body_subeFotoUsuario").value;
                                                                                document.getElementById("Body_btnGuardaAltaUsuario").click();
                                                                            }
                                                                        </script>
                                                                        <div style="display: none">
                                                                            <asp:Button CssClass="btnMasterRectangular" runat="server" ID="btnGuardaAltaUsuario"
                                                                                Visible="false" Text="Guardar" OnClick="btnGuardaAltaUsuario_Click" ValidationGroup="grouAltaUsua"
                                                                                TabIndex="8" />
                                                                        </div>
                                                                        <input class="btnMasterRectangular" type="button" id="btnCancelarModal" value="Cancelar" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="30%" valign="top">
                                                        <telerik:radpanelbar runat="server" ID="acModules" Skin="Windows7"
                                                                ExpandMode="MultipleExpandedItems" Width="100%" EnableViewState="true">
                                                        </telerik:radpanelbar>                                
                                                    </td>
                                                </tr>
                                            </table>        
                                        </td>
                                    </tr>
                                </table>                             
                    </td>
                </tr>
            </table>
               <div style="width:100%;float:left;">    
                    <div class="mensajeMdlUsuario" style="display:none;"> </div>
                    <div class="divA pnlUsr" style="float:left; width:68%;  border-width:0px; border-style:solid;border-color:#ff9900;">
                      <table width="100%" cellpadding="3px">
                      
	                	<tr>
						    <td>
							    Nombre(s) Usuario:
						    </td>
						    <td>
							    <input type="text" name="Nombre de Usuario" placeholder="Nombre(s)" style="width:290px" class="pnlUsrDispo txtNameUsr"   />
						    </td>
                           <%-- <td rowspan="8" class="cataUsr">Campo unificado</td>--%>
					    </tr>
                        <tr>
                            <td > 
                                Número de Teléfono:
                            </td>                                       
                            <td>
                                <input maxlength="12" type="text" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" name="Numero de Telefono" class="pnlUsrDispo txtTelDispo" style="width:290px" placeholder="Ejem.525532333485" />
                            </td>                                      
                        </tr>  
                        <tr>
                            <td >
                                Modelo:
                            </td>                                       
                            <td>
                                <input type="text" name="Modelo del Telefono" class="pnlUsrDispo txtModeloDispo" style="width:290px" placeholder="Ejem. Bold2 " />
                            </td>                                      
                        </tr>     
                        <tr>
                            <td>
                                Marca:
                            </td>                                       
                            <td>
                                <input type="text" name="Marca del Telefono" class="pnlUsrDispo txtMarcaDispo" style="width:290px" placeholder="Ejem. BlackBerry" />
                            </td>                                      
                        </tr>
                        <tr>
                            <td>
                                Descripción:
                            </td>                                       
                            <td>
                                <input type="text" name="Descripcion del Dispositivo" class="pnlUsrDispo txtDescDispo" style="width:290px" placeholder="Ejem.Memoria 8gb, etc"/>
                            </td>                                      
                        </tr>     
                        <tr>
                            <td>
                                IMEI:
                            </td>                                       
                            <td>
                                <input type="text" onKeypress="if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;" name="IMEI del Dispositivo" class="pnlUsrDispo txtImeiDispo" style="width:290px" placeholder="Código pre-grabado por el fabricante del teléfono" />
                            </td>                                      
                        </tr>  
                        <tr>
                            <td>
                                Sexo:
                            </td>                                       
                            <td>
                               <select name="Sexo" class="selectSexoCata">
                                <option value="M">Masculino</option>
                                <option value="F">Femenino</option>
                               </select>
                            </td>                                      
                        </tr>  
	                </table>  
                    </div>	
                
                    <div class="divB pnlUsr divCatalogosUsr" style=" float:left;width:30%;height:255px; overflow:auto; border-width:0px; border-style:solid;border-color:#ff9900; ">
                   	
                    </div>
                    <div class="botonera" style=" width:100%;text-align:center; float:left; margin:10px; padding-top:10px;">
                         <input type="submit" value="Agregar Usuario" class="btnMasterRectangular" style=" cursor:pointer;" id="saveUsr" />
                    </div>
                </div>

               
                            
        </asp:Panel>
    </div>
    <%--Modal Alta Usuario--%>
</asp:Content>
