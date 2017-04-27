<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true" CodeBehind="ReportePosicion.aspx.cs" Inherits="EncuestasMoviles.Pages.ReportePosicion" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />     
<style type="text/css">       
   /* html { height: 100% }       
    body { height: 100%; margin: 0; padding: 0 }    */   
    #map_canvas { height: 100% }    
 
.btnMasterRectangular
{	font-family:Arial, Helvetica, sans-serif;

	font-size:10px;

	color:#29549F;

	font-weight:bold;

	text-align:center;

	text-shadow:0px 1px 0px #FFFFFF; 
    
	min-width:58px;

	margin:0px;

	padding:0px 5px;

	height:21px;

	border:1.5px solid #969696;

	/* Firefox 3.6 */

    -ms-filter: "progid:DXImageTransform.Microsoft.gradient(GradientType=0,startColorstr='#E5E5E5', endColorstr='#C9C9C9')";
    background-image: -webkit-gradient(linear,left bottom,left top,color-stop(0, #E5E5E5),color-stop(1, #C9C9C9));/* Safari & Chrome */
} 
    </style>     
    <script type="text/javascript"  src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBm-xviwppO8uicDJPpuWlKFLZtwTbkTa4&sensor=true">    
     </script>     
<script type="text/javascript">
    var map;
    var tamZoom = 13;
    var imagen = new Image
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
    function hide(o) {
        document.getElementById(o).style.display = "none";
    }
    function showMessage_Info(msg) {
        var o = document.getElementById('mensaje');
        o.style.display = "block";
        o.innerHTML = "<table><tr><td><img src='../Images/att.png' alt='Message'/></td><td><strong>" + msg + "</strong></td></tr></table>";
        window.setTimeout("hide('mensaje');", 5000);
    }
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
    function muestraPosicion(latitud, longitud, numero, nombre, rutaimgpersona, rutaimgdisp) {
        initialize();
        var myLatlng = new google.maps.LatLng(latitud, longitud);
        var myOptions = { zoom: 12, center: myLatlng, mapTypeId: google.maps.MapTypeId.ROADMAP }
        var map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        map.setCenter(new google.maps.LatLng(latitud, longitud), 13);
        var marker = new google.maps.Marker({ position: myLatlng, map: map, title: "Aquí se encuentra el dispositivo!" });
        var HTML = "<div><div><h3>Numero: " + numero + "</h3></div><div>Nombre: " + nombre + "</div></div>";
        var infowindow = new google.maps.InfoWindow({ content: HTML });
        google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker) });
    }

    function pintaLineaMapa() {

        cuenta = 20;
        coor = document.getElementById("Body_arrayCoordenadas").value.split('&');
        valores = document.getElementById("Body_arrayDatos").value.split('&');
        map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);
        var c = 0;
        var marker;
        for (c = 0; c < coor.length; c++) {
            if (c == 0) {
            }
            var spl = new Array();
            spl = coor[c].toString().split(',');
            valor = valores[c].toString().split(',');
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
            var polyline = new google.maps.Polyline({
                path: routes,
                map: map,
                strokeColor: '#ff0000',
                strokeWeight: 3,
                strokeOpacity: 1.0,
                clickable: false
            });
            marker.setTitle(spl[0].toString()); //.replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').toUpperCase());
            DireccionMedico(marker, "MENSAJE A MOSTRAR", "HORA DE CAPTURA", valor[0], valor[1], valor[2], valor[3], valor[4], valor[5]); // spl[2].toString().replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').toUpperCase(), spl[3].toString());
        }
        ObtenerHospitales();
    }

    function ObtenerHospitales() {
        cuentaH = 20;

        coorH = document.getElementById("Body_arrayCoordenadasH").value.split('&');

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
                DireccionHospital(markerH, "LA DIRECCION DE UBICACION"); //" splH[2].toString().replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').replace('-', ',').toUpperCase());
            }
        }
        url = staticmap.trim() + centerstatic.trim() + markerstatics.trim() + linestatics.trim() + sensorstatic.trim();
        imagen.src = url;
        document.getElementById("Body_URLimagen").value = imagen.src;

    }

    function DireccionMedico(marker, msj, hora, fotoEmpleado, fotoDispositivo, idUsuario, idDispositivo, Nombre, Numero) {        
        var point;
        point = map.getCenter();

        var infowindow = new google.maps.InfoWindow(
                {
                    content: "<div style='width: 250px; height: 150px; border: 1px solid #000;'>" +
                             "<table width='100%' border = '0'>" +
                             "<tr>" +
                             "<td>" +
                             "<img src='http://encuestasmoviles/media/usuarios/" + idUsuario + "/" + fotoEmpleado + "' border='0' width='40' height='40'/>" +
                             "</td>" +
                             "<td>" +
                             "<b>" + Nombre + "</b>" +
                             "</td>" +
                             "<tr>" +
                             "<td>" +
                             "<img src='http://encuestasmoviles/media/Dispositivos/" + idDispositivo + "/" + fotoDispositivo + "' border='0' width='40' height='40'>" +
                             "</td>" +
                             "<td>" +
                             "<b>" + Numero + "</b>" +
                             "</td>" +
                             "</tr>" +
                             "</table>" +
                             "</div>",
                    size: new google.maps.Size(50, 100)
                }
                );
        google.maps.event.addListener(marker, 'click', function (overlay, point) {
            infowindow.open(map, marker);
        });
    }
    
  </script>  
  <br />
 <table style="width: 100%;" align="center">
        <tr align="center">
            <td style="width:20%">
                &nbsp;
            </td>
            <td style="width:60%" align="center">
            <table width="100%">
            <tr align="center">
                <td style="width:50%"><b>Usuario a consultar :</b></td>
                <td style="width:50%" align="left"><asp:DropDownList runat="server" ID="ddlEmpleados" Width="250px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr align="center">
                <td style="width:50%">
                    Fecha Inicial: <br />
                    <telerik:raddatepicker ID="txtCalendario" runat="server" Width="190px"></telerik:raddatepicker>
                </td>
                <td style="width:50%" align="left">
                    Fecha Final: <br />
                    <telerik:raddatepicker ID="txtFechaFinal" runat="server" Width="190px"></telerik:raddatepicker>
                </td>
            </tr>
            </table>
            </td>
            <td style="width:20%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="center">    
            <br />            
                <asp:Button ID="btnConsultar" CssClass="btnMasterRectangular" runat="server" Text="Consultar" 
                    onclick="btnConsultar_Click" />
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
        <asp:HiddenField ID="URLimagen" runat="server" /> 
            <asp:HiddenField ID="arrayCoordenadas" runat="server" /> 
            <asp:HiddenField ID="arrayCoordenadasH" runat="server" />       
            <asp:HiddenField ID="arrayDatos" runat="server" />                                     
 </table>
 <div id="map_canvas" style="width:100%; height:500px"></div> 
</asp:Content>
