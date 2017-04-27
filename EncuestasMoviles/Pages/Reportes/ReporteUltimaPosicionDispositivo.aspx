<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ReporteUltimaPosicionDispositivo.aspx.cs" Inherits="EncuestasMoviles.Pages.Reportes.coordenadaspruebas2" %>

<html>   
<head>     
<meta name="viewport" content="initial-scale=1.0, user-scalable=no" />     
<style type="text/css">       
    html { height: 100% }       
    body { height: 100%; margin: 0; padding: 0 }       
    #map_canvas { height: 100% }    
            .btnMasterRectangular

{


	

	font-family:Arial, Helvetica, sans-serif;

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
        o.innerHTML = "<table><tr><td><img src='../../Images/att.png' alt='Message'/></td><td><strong>" + msg + "</strong></td></tr></table>";
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
  </script>  
   </head>
      
   <body style="background-color:White;color: #505050;">
   <form runat="server" id="formulario">
  <table style="width: 100%;">
        <tr align="center">
            <td style="width:20%">
                &nbsp;
            </td>
            <td style="width:60%" align="center">
            <table width="100%">
            <tr align="center">
            <td style="width:50%" align="right">Empleado a consultar :</td>
            <td style="width:50%" align="left"><asp:DropDownList runat="server" ID="ddlEmpleados" Width="250px"></asp:DropDownList></td>
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

 </table>
 <div id="map_canvas" style="width:100%; height:100%"></div> 
     </form>
     
   </body>
   </html>