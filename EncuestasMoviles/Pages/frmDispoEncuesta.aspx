<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true"
    CodeBehind="frmDispoEncuesta.aspx.cs" Inherits="EncuestasMoviles.Pages.frmDispoEncuesta" enableEventValidation="false"  %>
<%@ Import Namespace="System.Web.Services" %>
<%@ Register TagName="messageBox" TagPrefix="mesBx" Src="~/MessageBox.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
    <asp:UpdatePanel runat="server" ID="upgral">
        <ContentTemplate>
        <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
            <script src="../js/jquery-1.5.1.js" type="text/javascript"></script>
            <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
            <style type="text/css">
                #personPopupContainer3
                {
                    position: absolute;
                    left: 0;
                    top: 0;
                    display: none;
                    z-index: 2;
                }
                
                .personPopupPopup3
                {
                }
                
                #personPopupContent3
                {
                    background-color: #FFF;
                    min-width: 175px;
                    min-height: 50px;
                }
                
                .personPopupPopup3 .personPopupImage
                {
                    margin: 15px;
                    margin-right: 15px;
                }
                
                .personPopupPopup3 .corner
                {
                    width: 19px;
                    height: 15px;
                }
                
                .personPopupPopup3 .topLeft
                {
                    background: url(../Images/ToolTip/balloon_topLeft.png) no-repeat;
                }
                
                .personPopupPopup3 .bottomLeft
                {
                    background: url(../Images/ToolTip/balloon_bottomLeft.png) no-repeat;
                }
                
                .personPopupPopup3 .left
                {
                    background: url(../Images/ToolTip/balloon_left.png) repeat-y;
                }
                
                .personPopupPopup3 .right
                {
                    background: url(../Images/ToolTip/balloon_right.png) repeat-y;
                }
                
                .personPopupPopup3 .topRight
                {
                    background: url(../Images/ToolTip/balloon_topRight.png) no-repeat;
                }
                
                .personPopupPopup3 .bottomRight
                {
                    background: url(../Images/ToolTip/balloon_bottomRight.png) no-repeat;
                }
                
                .personPopupPopup3 .top
                {
                    background: url(../Images/ToolTip/balloon_top.png) repeat-x;
                }
                
                .personPopupPopup3 .bottom
                {
                    background: url(../Images/ToolTip/balloon_bottom.png) repeat-x;
                    text-align: center;
                }
                .ListDispositivos ul
                {
                    vertical-align: top;
                }
                .ListDispositivos li
                {
                    margin-left: 1px;
                    margin-bottom: 1px;
                    color: Black;
                    text-align: center;
                }
            </style>
            <script type="text/javascript">
                $(function () {
                    var hideDelay = 500;
                    var currentID;
                    var hideTimer = null;


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
                      

                        var settings = $(this).attr('src');
                        var pageID = settings;
                        currentID = $(this).attr('id');


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
                                if (data.indexOf('personPopupResult') < 0) {
                                    $('#personPopupContent3').html('<span >Page ' + pageID + ' did not return a valid result for person ' + currentID + '.<br />Please have your administrator check the error log.</span>');
                                }


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


                    $('#personPopupContainer3').mouseover(function () {
                        if (hideTimer)
                            clearTimeout(hideTimer);
                    });


                    $('#personPopupContainer3').mouseout(function () {
                        if (hideTimer)
                            clearTimeout(hideTimer);
                        hideTimer = setTimeout(function () {
                            container.css('display', 'none');
                        }, hideDelay);
                    });
                });
            </script>
            <script type="text/javascript">

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
//                    
                    // var CombCat = document.getElementsByTagName('select');
                   
                    var combos = $("select.combosCatalogos");

                    var datos = "";
                    if (combos.length > 0) {

                        for (var i = 0; i < combos.length; i++) {
                            var dato = combos[i];
                            var idCatalogo = $(dato).val();
                            var option = $(dato).find("option:selected").val()!=0;

                            if (option) {
                                var IdOpciCat = $(dato).find("option:selected").val();
                                datos += idCatalogo + "|" + IdOpciCat + "&";                                
                            }                          
                        }
                    }
                    $(":hidden#Body_hfIdCat").val(datos);
                   
                    document.getElementById("Body_btnBuscaDispo").click();
                }

                function Reenvia() {
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
                    document.getElementById("Body_btnReenviar").click();
                }
                $(document).ready(function () {

                    $(":checkbox").die().live("click", function () {

                        var valor = $(this).attr("value");
                        var id = $(this).attr("class");                       
                        if (valor == "chklist") {
                            if ($(this).is(':checked')) {
                                $(":checkbox." + id).attr("checked", true);
                            } else {
                                $(":checkbox." + id).attr("checked", false);
                            }
                        }

                    });
                    $("div#PnelFiltro").css('display', "none");
                    $("div#PnelFiltro2").css('display', "none");

                    $("h2#linkShowPanel").toggle(function () {
                        $("div#PnelFiltro").slideToggle('slow');
                        $("h2#linkShowPanel").text("Filtros de Búsqueda Dispositivos (-)");
                    }, function () {
                        $("div#PnelFiltro").slideToggle('slow');
                        $("h2#linkShowPanel").text("Filtros de Búsqueda Dispositivos (+)");
                    });

                    $("h2#linkShowPanel2").toggle(function () {
                        $("div#PnelFiltro2").slideToggle('slow');
                        $("h2#linkShowPanel2").text("Filtros de Búsqueda de Encuestas (-)");
                    }, function () {
                        $("div#PnelFiltro2").slideToggle('slow');
                        $("h2#linkShowPanel2").text("Filtros de Búsqueda de Encuestas (+)");
                    });
                    $("select#Body_cboBusCiudad").html("<option value=''>== Municipios ==</option>");
                    $("select#Body_cboBusEstado").die().live("change", function () {
                        var idEstado = $(this).val();
                        $.ajax({
                            type: "POST",
                            url: "frmDispoEncuesta.aspx/getMunicipios",
                            contentType: "application/json; charset=utf-8",
                            data: "{IdEstado:'" + idEstado + "'}",
                            dataType: "json",
                            success: function (msg) {
                                var c = eval(msg.d);
                                var Lista = "<option value=''>== Seleccine Municipio ==</option>";
                                $.each(c, function (index, val) {
                                    Lista += "<option value=" + val['IdMunicipio'] + ">" + val['MunicipioNombre'] + "</option>"
                                });
                                $("select#Body_cboBusCiudad").html("").html(Lista);
                                $("select#Body_cboBusCiudad option[value=" + $(":hidden#Body_HiddenFieldMuni").val() + "]").attr("selected", true);

                            }
                        });
                        $(":hidden#Body_HiddenFieldSelect").val(idEstado);
                    });
                    $("select#Body_cboBusCiudad").die().live("change", function () {
                        var idMunicipio = $(this).val();
                        $(":hidden#Body_HiddenFieldMuni").val(idMunicipio);

                    });


                    $.ajax({
                        type: "POST",
                        url: "frmDispoEncuesta.aspx/getEstados",
                        contentType: "application/json; charset=utf-8",
                        data: "{}",
                        dataType: "json",
                        success: function (msg) {
                            var c = eval(msg.d);
                            var Lista = "<option value=''>== Seleccione Estado ==</option>";
                            $.each(c, function (index, val) {
                                Lista += "<option value=" + val['IdEstado'] + ">" + val['EstadoNombre'] + "</option>"
                            });
                            $("select#Body_cboBusEstado").html("").html(Lista);
                            $("select#Body_cboBusEstado option[value=" + parseInt($(":hidden#Body_HiddenFieldSelect").val()) + "]").attr("selected", true);
                            $("select#Body_cboBusEstado").trigger('change');
                        }
                    });

                });
                
                function chkGrafica(id) {
                    document.getElementById("Body_hfIdEncuGrafica").value = "";
                    document.getElementById("Body_hfIdEncuGrafica").value = id;
                    document.getElementById("Body_btnGraficaEncuesta").click();
                }

                function muestraGrafica(EncId) {
                    window.open("Grafica.aspx?EncId=" + EncId, "_blank");
                }

                function CambioDia(sender, args) {
                    $find("Body_CalFechFinal").set_selectedDate(sender._selectedDate);

                }

            </script>
            <%--<asp:ScriptManager runat="server" EnableScriptGlobalization="true" EnablePartialRendering="false" ID="scrptdisooencu"></asp:ScriptManager>--%>
               
            <div>
                 <mesBx:messageBox runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento" />
                <asp:HiddenField runat="server" ID="hfIdsEncuestas" />
                <asp:HiddenField runat="server" ID="hfIdsDispositivos" />
                <asp:HiddenField runat="server" ID="hfIdEncuestaUnico" />
                <asp:HiddenField runat="server" ID="VistaActiva" />
                <asp:HiddenField runat="server" ID="hfTipoFecha" />
                <asp:HiddenField runat="server" ID="hfIdCat" />
                <asp:HiddenField runat="server" ID="hfIdEncuGrafica" />
                <br /><br /><br />
                <table style="width: 100%;">
                <tr>
                <td></td>
                </tr>
                <tr>
                 <td></td>
                </tr>
                    <tr>
                        <td>
                            <div style="height: auto; cursor: hand; width: 465px; position: absolute;">
                                <div style="background-color: #A9D0F5">
                                    <div class="ContenedorGeneral" style="height: auto; width: 100%">
                                        <table width="100%" cellpadding="0" border="0" cellspacing="0">
                                            <tr>
                                                <td class="MasterTituloContenedor">
                                                    <h2 id="linkShowPanel2" style="cursor: hand">
                                                        Filtros de Búsqueda de Encuestas (+)
                                                    </h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="PnelFiltro2">
                                                        <table align="center" cellpadding="5" style="height: auto; width: 100%">
                                                            <tr>
                                                                <td>
                                                                    Nombre de la Encuesta:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ValidationGroup="buscaEncDisp" runat="server" onkeypress="return validaCaracteresNoEncuestas(event)" ID="txtBuscaEncuesta"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Fecha Inicial:
                                                                </td>
                                                                <td>
                                                                    <telerik:RadDatePicker ID="txtFechaIni" runat="server" Width="120px"></telerik:RadDatePicker>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Fecha Final:
                                                                </td>
                                                                <td>
                                                                   <telerik:RadDatePicker ID="txtFechaFin" runat="server" Width="120px"></telerik:RadDatePicker>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Por Fecha de Creación:
                                                                    <input type="radio" id="radPorFechCrea" name="Radio" checked="checked" />
                                                                </td>
                                                                <td>
                                                                    Por Fecha Límite:
                                                                    <input type="radio" id="radPorFechLimit" name="Radio" />
                                                                    <div style="display: none">
                                                                        <asp:Button runat="server" ID="btnBuscaEncu"  ValidationGroup="buscaEncDisp"  Text="Buscar" OnClick="btnBuscaEncu_Click" />
                                                                    </div>
                                                                </td>
                                                                <tr>
                                                                    <td colspan="2" align="center">
                                                                        <input type="button" id="btnBusca" class="btnMasterRectangular" onclick="chkCrea()"
                                                                            value="Buscar" />

                                                                        <div style="display: none">
                                                                            <asp:Button runat="server" ID="Button3" CssClass="btnMasterRectangular" Text="Buscar"
                                                                                OnClick="btnBuscaEncu_Click" ValidationGroup="buscaEncDisp" />
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                        </table>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>                                        
                                    </div>
                                </div>
                                <div style="text-align: left; padding: 5px 0 5px 0; width: 150px">
                                </div>
                            </div>
                        </td>
                        <td>
                            <div style="height: auto; width: 465px; cursor: hand; position: absolute;">
                                <div>
                                    <div class="ContenedorGeneral">                                    
                                        <table width="100%" cellpadding="0" border="0" cellspacing="0">
                                            <tr>
                                                <td class="MasterTituloContenedor">
                                                    <h2 id="linkShowPanel" style="cursor: hand">
                                                        Filtros de Búsqueda Dispositivos (+)
                                                    </h2>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div id="PnelFiltro">
                                                        <table align="center" cellpadding="5" style="height: auto; width: 100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label1" Text="Nombre de Usuario:" runat="server"></asp:Label>
                                                         
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtBusUsua" onkeypress="return validaCaracteresNoEncuestas(event)" Height="13px" EnableViewState="true"
                                                                        ViewStateMode="Enabled"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label2" Text="Número de Teléfono:" runat="server"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtBusNumTel" onkeypress="return ValidaNumeros(event)" Height="13px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <asp:UpdatePanel runat="server" ID="updEsta" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblbusqEstado" Text="Estado:" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>  
                                                               
                                                                            <asp:HiddenField runat="server" ID="HiddenFieldSelect" />
                                                                            <asp:HiddenField runat="server" ID="HiddenFieldMuni" />                                                                    
                                                            
                                                                            <asp:DropDownList runat="server"  ID="cboBusEstado" Height="20px" name="cboBusEstado" EnableViewState="true" >
                                                                    
                                                                            </asp:DropDownList>                                                               
                                                               
                                                                            <br />
                                                               
                                                               
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblBusqMuni" Text="Municipio:" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList runat="server" ID="cboBusCiudad" Height="20px" name="cboBusCiudad" EnableViewState="true">
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </table>
                                                        <fieldset>
                                                            <legend>
                                                                <p>                                                    
                                                                    Catálogos
                                                                </p>
                                                            </legend> 
                                                            <asp:Panel runat="server" ID="pnlCombos">
                                                            </asp:Panel>                                               
                                                        </fieldset>                                           
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="right">
                                                                    <input class="btnMasterRectangular" type="button" id="btnBusqDispo" class="btnMasterRectangular"
                                                                        onclick="BuscaDispo()" value="Buscar" />
                                                                    <div style="text-align: right; display: none">
                                                                        <asp:Button CssClass="btnMasterRectangular" runat="server" ID="btnBuscaDispo" Text="Buscar"
                                                                            OnClick="btnBuscaDispo_Click" />
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                        </table>                                                        
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div style="text-align: left; padding: 5px 0 5px 0; width: 150px">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; vertical-align: top; text-align: left; border-width: 1px;">
                            &nbsp;
                        </td>
                        <td id="Td2" runat="server" style="width: 50%; vertical-align: top; text-align: left;
                            border-width: 1px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; vertical-align: top; text-align: left; border-width: 1px;">
                            &nbsp;
                        </td>
                        <td id="Td3" runat="server" style="width: 50%; vertical-align: top; text-align: left;
                            border-width: 1px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%; vertical-align: top; text-align: left; border-width: 1px;
                            border-style: dashed">
                            <table>
                                <tr>
                                    <td colspan="2" align="left">
                                        
                                             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                             &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    </td>
                                    <td align="right">
                                     <input type="button" class="btnMasterRectangular" id="Button2" value="Enviar Encuesta"
                                            onclick="EnviarAsignados();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td id="Td1" runat="server" style="width: 50%; vertical-align: top; text-align: left;
                            border-width: 1px; border-style: dashed">
                            <table>
                                <tr>
                                    <td>
                                        Selecciona Vista:
                                    </td>
                                    <td>
                                        <select id="Select1" onchange="MuestraVista(this)">                                            
                                            <option id="Option2">Vista Imagen</option>
                                            <option id="Option3" selected="selected">Vista Detalle</option>                                            
                                        </select>
                                    </td>
                                    <td>
                                        Todos los Dispositivos:
                                    </td>
                                    <td>
                                        <input type="checkbox" id="Checkbox1" onclick="clickTodos(this);" />                                      
                                        <input type="button" id="Button1" class="btnMasterRectangular" value="Reenviar" onclick="Reenvia()" />
                                        <div style="display: none">
                                            <asp:Button runat="server" ID="btnReenviar" Text="Reenviar" OnClick="btnReenviar_Click" />
                                        </div>
                                        <script type="text/javascript">
                                         
                                            function MuestraVista(seleccionado) {                                               
                                                if (seleccionado.selectedIndex == 0) {
                                                    document.getElementById('Body_tdViewImagen').style.display = 'block';
                                                    document.getElementById('Body_tdViewDetails').style.display = 'none';
                                                    document.getElementById('Body_VistaActiva').value = "vi";
                                                }
                                                else {
                                                    document.getElementById('Body_tdViewImagen').style.display = 'none';
                                                    document.getElementById('Body_tdViewDetails').style.display = 'block';
                                                    document.getElementById('Body_VistaActiva').value = "vd";
                                                }
                                            }
                                        </script>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left; border-width: 1px;
                            border-style: dashed">
                            <div style="display: none">
                                <asp:ImageButton Width="22px" ToolTip="Visualizar Grafica" ImageUrl="~/Images/iconograficar.png"
                                    Style="text-align: center" ID="btnGraficaEncuesta" runat="server" OnClick="btnGraficaEncuesta_Click" />
                            </div>
                            <div style="display: none">
                                <asp:Button runat="server" ID="btnEncuChk" Style="display: none" OnClick="btnEncuChk_Click" />
                            </div>
                            <div style="display: none">
                                <asp:Button runat="server" ID="btnSelecTodos" Style="display: none" OnClick="btnSelecTodos_Click" />
                            </div>
                            <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%; position: static;
                                    border: 1px solid #3F3F3F;">
                                    <div id="contrls" style="width: 100%; height: 220px; background-color: #F2F2F2; overflow: auto;
                                        text-align: center">
                                        <asp:GridView runat="server" CssClass="someClass" ID="gvDispoEncu" CellPadding="4"
                                            ForeColor="#333333" GridLines="Vertical" AutoGenerateColumns="false" Width="100%"
                                            DataKeyNames="IdEncuesta" EmptyDataText="No se encontrarón registros">
                                            <Columns>
                                                <asp:BoundField HeaderText="Encuesta" DataField="NombreEncuesta" />
                                                <asp:BoundField HeaderText="Fecha Creación" DataField="FechaCreaEncuesta" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Fecha Límite" DataField="FechaLimiteEncuesta" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField HeaderText="Grafica" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <img src="../Images/iconograficar.png" style="cursor:hand" width="22px" height="22px" alt="Graficar"
                                                            onclick="chkGrafica(<%# Eval("IdEncuesta") %>)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Asigna" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <input name="chkEncuestas" id="<%# Eval("IdEncuesta") %>"  type="checkbox" style="text-align: center"   />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Revisar">
                                                    <ItemTemplate>
                                                        <input value="Revisar" name="btnRevisar" id="<%# Eval("IdEncuesta") %>" type="button"
                                                            style="vertical-align: middle" onclick="chkEncu(<%# Eval("IdEncuesta") %>)" />
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
                        </td>
                        <td id="tdViewImagen" runat="server" style="vertical-align: top; text-align: left;
                            border-width: 1px; border-style: dashed">
                            <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%; position: static;
                                    border: 1px solid #3F3F3F;">
                                    <div id="Div1" style="width: 100%; height: 220px; background-color: #F2F2F2; overflow: auto;
                                        text-align: center">
                                        <asp:ListView runat="server" ID="lvDispositivosEncu" DataKeyNames="IdDispositivo">
                                            <LayoutTemplate>
                                                <ul>
                                                    <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                                </ul>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <li style="display: inline-block; width: 150px; vertical-align: top; text-align: center;">
                                                    <div>
                                                        <div style="text-align: right">
                                                            <img id="val" src='<%# Eval("ColorEstatus")%>' />
                                                        </div>
                                                        <img class="personPopupTrigger3" id='<%# Eval("IdDispositivo") %>' src='<%# Eval("ImagenTelefono") %>'
                                                            alt="imagen" width="100px" /><br />
                                                        <input name="chkDispositivos|<%# Eval("StrColor") %>" id='<%# Eval("IdDispositivo") %>'
                                                            type="checkbox" onselect="clickChk(<%# Eval("IdDispositivo") %>);" <%# Eval("EstatusCheck") %>
                                                            <%# Eval("ChkEnabled") %> class="<%# Eval("IdDispositivo") %>" value="chklist"  />
                                                        <%# Eval("DispositivoDesc") %>
                                                    </div>
                                                </li>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div>
                                                    Sin Dispositivos Disponibles!!!
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td id="tdViewDetails" runat="server" style="display: none; vertical-align: top;
                            text-align: left; border-width: 1px; border-style: dashed">
                            <div style="margin-top: 10px; width: 100%; vertical-align: middle; text-align: center">
                                <div class="ContenedorGeneral" style="height: auto; width: 95%; position: static;
                                    border: 1px solid #3F3F3F;">
                                    <div id="Div2" style="width: 100%; height: 220px; background-color: #F2F2F2; overflow: auto;
                                        text-align: center">
                                        <asp:ListView runat="server" ID="ListView1" Style="display: none" DataKeyNames="IdDispositivo">
                                            <LayoutTemplate>
                                                <asp:PlaceHolder runat="server" ID="itemPlaceholder"></asp:PlaceHolder>
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <table style="text-align: left; width: 100%; border: 1px solid">
                                                    <tr>
                                                        <td style="text-align: center; width: 30%;">
                                                            <img class="personPopupTrigger3" id="<%# Eval("IdDispositivo") %>" src="<%# Eval("ImagenTelefono") %>"
                                                                alt="imagen" width="40px" height="40px" /><br />
                                                        </td>
                                                        <td style="text-align: left; width: 50%;">
                                                            <%# Eval("DispositivoDesc") %>
                                                        </td>
                                                        <td style="text-align: left; width: 10%;">
                                                            <input name="chkDispositivos|<%# Eval("StrColor") %>" id="<%# Eval("IdDispositivo") %>" type="checkbox" onselect="clickChk(<%# Eval("IdDispositivo") %>);"
                                                                <%# Eval("EstatusCheck") %> <%# Eval("ChkEnabled") %> class="<%# Eval("IdDispositivo") %>" value="chklist" />
                                                        </td>
                                                        <td style="text-align: left; width: 10%;">
                                                            <img id="Img1" alt="Foto" src="<%# Eval("ColorEstatus")%>" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                            <EmptyDataTemplate>
                                                <div>
                                                    Sin Dispositivos Disponibles!!!
                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:ListView>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="display: none">
                                <asp:Button runat="server" ID="btnEnviaAsignados" OnClick="btnEnviaAsignados_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <input type="hidden" id="hdnIdEncuesta" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
