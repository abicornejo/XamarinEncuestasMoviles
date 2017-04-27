<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteHistorialPosicionDispositivo.aspx.cs" Inherits="EncuestasMoviles.Pages.Reportes.CoordenadasPrueba3" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Monitoreo de Medicos Supervisores</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no"  /> 
<meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    
<link href="~/js/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />   
<script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false&language=es"></script>
<script type="text/javascript" src="~/js/gears_init.js"></script>
<script type="text/javascript">
    var coor = new Array();
    var coorH = new Array();
    var map;
    var tamZoom = 13;
    var imagen = new Image;
    var url;
    var geocoder;
    var staticmap = 'http://maps.googleapis.com/maps/api/staticmap?';
    var sensorstatic = '&sensor=false';
    var markerstatics = '&markers=color:red';
    var linestatics = '&path=color:red|weight:3';
    var centerstatic = '';

    var directionsDisplay;
    var directionsService = new google.maps.DirectionsService();
    var oldDirections = [];
    var oldDirectionsP = [];
    var currentDirections = null;

    var polyline;
    var arre = new Array();
    var routes = new google.maps.MVCArray();
    var infowindow = new google.maps.InfoWindow();
    var myOptions = {
        backgroundColor: '#000',
        zoom: tamZoom,
        center: new google.maps.LatLng(19.39, -99.16),
        disableDefaultUI: false,
        noClear: true,
        navigationControl: true,
        navigationControlOptions:
            {
                position: google.maps.ControlPosition.TOP_RIGHT,
                style: google.maps.NavigationControlStyle.ANDROID
            },
        scaleControl: true,
        scaleControlOptions:
            {
                position: google.maps.ControlPosition.TOP_LEFT,
                style: google.maps.ScaleControlStyle.DEFAULT
            },
        mapTypeControl: true,
        mapTypeControlOptions:
            {
                style: google.maps.MapTypeControlStyle.HORIZONTAL_MENU,
                position: google.maps.ControlPosition.TOP_LEFT,
                mapTypeIds: [google.maps.MapTypeId.ROADMAP]
            },
            mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    function initialize() {
        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        directionsDisplay = new google.maps.DirectionsRenderer({
            'map': map,
            'preserveViewport': true,
            'draggable': true
        });
    }
    function ObtenerHospitales() {
        cuentaH = 20;

        coorH = document.getElementById("arrayCoordenadasH").value.split('&');

        var cH = 0;
        var markerH;
        for (cH = 0; cH < coorH.length; cH++) {
            if (cH == 0) {
            }
            var splH = new Array();
            splH = coorH[cH].toString().split(',');
            var splAuxH = new Array();
            if (cH < coorH.length - 1) {
                splAuxH = coorH[cH + 1].toString().split(',');
            }
            else {
                splAuxH = coorH[cH].toString().split(',');
            }
            if (splH[0].toString() != '') {
                initialLocationH = new google.maps.LatLng(splH[0].toString(), splH[1].toString());
                map.setCenter(new google.maps.LatLng(splH[0].toString(), splH[1].toString()), 13);
                markerH = new google.maps.Marker({ position: initialLocationH, map: map, icon: 'http://gmaps-samples.googlecode.com/svn/trunk/markers/green/marker' + (cH + 1) + '.png' });
                var latLngH = new google.maps.LatLng(splH[0].toString(), splH[1].toString());
                DireccionHospital(markerH, "LA DIRECCION De HUBICACION"); //" splH[2].toString().replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').toUpperCase());
            }
        }
        url = staticmap.trim() + centerstatic.trim() + markerstatics.trim() + linestatics.trim() + sensorstatic.trim();
        imagen.src = url;
        document.getElementById("URLimagen").value = imagen.src;

    }
    function pintaLineaMapa() {
        cuenta = 20;
        coor = document.getElementById("arrayCoordenadas").value.split('&');
        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        var c = 0;
        var marker;
        for (c = 0; c < coor.length; c++) {
            if (c == 0) {
            }
            var spl = new Array();
            spl = coor[c].toString().split(',');
            var splAux = new Array();
            if (c < coor.length - 1) {
                splAux = coor[c + 1].toString().split(',');
            }
            else {
                splAux = coor[c].toString().split(',');
            }
            initialLocation = new google.maps.LatLng(spl[0].toString(), spl[1].toString());
            map.setCenter(new google.maps.LatLng(spl[0].toString(), spl[1].toString()), 13);
            var routes = [new google.maps.LatLng(spl[0].toString(), spl[1].toString()), new google.maps.LatLng(splAux[0].toString(), splAux[1].toString())];
            marker = new google.maps.Marker({ position: initialLocation, map: map, icon: 'http://gmaps-samples.googlecode.com/svn/trunk/markers/red/marker' + (c + 1) + '.png' });
            var latLng = new google.maps.LatLng(spl[0].toString(), spl[1].toString());
            centerstatic = 'center=' + spl[0].toString().substring(0, 7).trim() + ',' + spl[1].toString().substring(0, 8).trim() + '&zoom=13&size=400x500&maptype=roadmap';
            markerstatics += '|' + spl[0].toString().substring(0, 7).trim() + ',' + spl[1].toString().substring(0, 8).trim();
            linestatics += '|' + spl[0].toString().substring(0, 7).trim() + ',' + spl[1].toString().substring(0, 8).trim();
            var polyline = new google.maps.Polyline(
        {
            path: routes,
            map: map,
            strokeColor: '#ff0000',
            strokeWeight: 3,
            strokeOpacity: 1.0,
            clickable: false
        });
            marker.setTitle(spl[0].toString()); //.replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').toUpperCase());
            DireccionMedico(marker, "MENSAJE A MOSTRAR", "HORA DE CAPTURA"); // spl[2].toString().replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').toUpperCase(), spl[3].toString());
        }
        ObtenerHospitales();
    }
    function DireccionMedico(marker, msj, hora) {

        var infowindow = new google.maps.InfoWindow(
    {
        content: "<div style='width: 250px; height: 100px; border: 1px solid #000;'> <b>DIRECCION:</b> <br/>" + msj + "<br/> <b>HORA:</b> <br/>" + hora + "</div>",
        size: new google.maps.Size(50, 50)
    });
        google.maps.event.addListener(marker, 'click', function () {
            infowindow.open(map, marker);
        });
    }
    function DireccionHospital(markerH, msjH) {

        var infowindowH = new google.maps.InfoWindow(
    {
        content: "<div style='width: 250px; height: 100px; border: 1px solid #000;'>" + +"DIRECCION: <br/>" + msjH + "</div>",
        size: new google.maps.Size(50, 50)
    });
        google.maps.event.addListener(markerH, 'click', function () {
            infowindowH.open(map, markerH);
        });
    }
    function codeLatLng(initlocation) {
        var input = initlocation;
        var latlngStr = input.split(",", 2);
        var lat = parseFloat(latlngStr[0]);
        var lng = parseFloat(latlngStr[1]);
        var latlng = new google.maps.LatLng(lat, lng);
        geocoder.geocode({ 'latLng': latlng }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[1]) {
                    map.setZoom(11);
                    marker = new google.maps.Marker({
                        position: latlng,
                        map: map
                    });
                    infowindow.setContent(results[1].formatted_address);
                    infowindow.open(map, marker);
                }
            } else {
                alert("Geocoder failed due to: " + status);
            }
        });
    }
    function ShowModalPopup(rowIndex) {
        var gridView = new String();
        var cell = new String();
        var Imgsrc = new String();
        gridView = document.getElementById("gvRecorrido");
        cell = gridView.rows[parseInt(rowIndex) + 1].cells[0];
        Imgsrc = cell.childNodes[0].defaultValue;
        var modal = $find('mpeConceptos');
        document.getElementById('imgMedico').src = "";
        var hreff = document.getElementById('imgMedico').src;
        document.getElementById('imgMedico').src = hreff.substring(0, hreff.length - 1) + Imgsrc;
        modal.show();
    }
    function HideModalPopup() {
        var modal = $find('mpeConceptos');
        modal.hide();
    }
    function CambiaZoom() {
        SelZoom = document.getElementById("selZoom");
        tamZoom = SelZoom.value;
        if (imagen.src != "") {
            imagen.src = url.replace('zoom=13', 'zoom=' + SelZoom.value);
            document.getElementById("URLimagen").value = imagen.src;
        }
    }
    function hide(o) {
        document.getElementById(o).style.display = "none";
    }
    function showMessage_Info(msg) {       
        var o = document.getElementById('mensaje');
        o.style.display = "block";
        o.innerHTML = "<table><tr><td><img src='../../Images/att.png' alt='Message'/></td><td><strong>" + msg + "</strong></td></tr></table>";
        window.setTimeout("hide('mensaje');", 5000);
    }
    function PopupLoc(e, pickerID) {
        datePicker = $find("<%# txtCalendario.ClientID %>");
        datePicker.showPopup(10, 200);   // Set the position    
    }   
</script>

<style type="text/css">

.ContenedorGeneral
{

	width:100%;

	border:2px solid #3F3F3F;

	float:none;
	
	margin:0 auto;

	background-color:#FFFFFF;

	color:#505050;
	
    clear:both;

}
.MasterTituloContenedor
{	

	float:left;

	height:22px;

    width:100%;
	
    border:0px solid #3F3F3F;
	
	border-bottom:2px solid #3F3F3F;
	
	padding:0;

	background-color:#D0D0D0;

}
#content {
        margin: 0 auto;
        position: relative;
        width: 500px;
        height: 20px;
       /* border:1px solid #000000;*/
        }

        #izquierda{
        width:250px;
        height:20px;
       
        float: left;
        }

        #derecha{
        width:250px;
        height:20px;
        margin-top:auto;     
        float: right;
        }
</style>
</head>
<body style="background-color:White;color: #505050;">
    <form id="form1" runat="server">    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>   
   <%-- <asp:ScriptManager runat="server" ID="scrpm"></asp:ScriptManager>--%>
   
  
  <%-- <div id="content">
        <div id="izquierda">
            fecha de prueba
        </div>
        <div id="derecha">
        <telerik:RadDatePicker ID="RadDatePicker1" runat="server"></telerik:RadDatePicker>
        </div>
   </div>--%>
   <div class="ContenedorGen">
    <table style="width: 500px;" align="center">
        <tr align="left">            
            <td><b>Empleado a monitorear:</b></td>
            <td valign="middle"><asp:DropDownList runat="server" ID="ddlEmpleados" Width="250px"></asp:DropDownList></td>
        </tr>
        <tr>           
            <td style="width: 250px;"><b>Fecha Inicial:</b></td>           
            <td style="width: 200px;">  
                <div>                    
                    <telerik:RadDatePicker ID="txtCalendario" runat="server" Width="190px"></telerik:RadDatePicker>                       
                </div>
            </td>
        </tr> 
        <tr>                  
            <td style="width: 250px;"><b>Fecha Final:</b></td>
            <td style="width: 200px;"> 
                <div style="display: inline-block">
                    <telerik:RadDatePicker ID="txtFechaFinal" runat="server" Width="190px" ></telerik:RadDatePicker>
                </div>
            </td>
        </tr> 
        <tr>
            <td colspan="2" align="center">    
            <br /> 
            <asp:HiddenField ID="URLimagen" runat="server" /> 
            <asp:HiddenField ID="arrayCoordenadas" runat="server" /> 
            <asp:HiddenField ID="arrayCoordenadasH" runat="server" />                      
                <asp:Button ID="btnConsultar" CssClass="btnMasterRectangular" runat="server" Text="Consultar" 
                    onclick="btnConsultar_Click1" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="trMensaje">
            <td>
                &nbsp;
            </td>
            <td align="center">    
                <div id="mensaje" style="display:none"></div>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr align="center" runat="server" id="trExportar"> 
            <td>
                &nbsp;
            </td>            
            <td align="center" valign="middle" class="tablaTitulo">
            <asp:Label ID="Label1" runat="server" Text="Tamaño de mapa a exportar :"></asp:Label>                      
                <select id="selZoom" onchange="CambiaZoom()">
                <option value="0" label="--- Zoom de mapa ---"></option>
                <option value="1" label="1"></option>
                <option value="2" label="2"></option>
                <option value="3" label="3"></option>
                <option value="4" label="4"></option>
                <option value="5" label="5"></option>
                <option value="6" label="6"></option>
                <option value="7" label="7"></option>
                <option value="8" label="8"></option>
                <option value="9" label="9"></option>
                <option value="10" label="10"></option>
                <option value="11" label="11"></option>
                <option value="12" label="12"></option>
                <option value="13" label="13"></option>
                <option value="14" label="14"></option>
                <option value="15" label="15"></option>
                <option value="16" label="16"></option>
                <option value="17" label="17"></option>
                <option value="18" label="18"></option>
                <option value="19" label="19"></option>
                <option value="20" label="20"></option>
                <option value="21" label="21"></option>
                </select> 
                <asp:Button runat="server" ID="btnExportar" Text="Exportar" onclick="btnExportar_Click"/>               
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
       
    </table> 
    </div>
    
   <div id="map_canvas" style="width:100%;height:100%"></div>  
    </form>     
</body>

</html>
