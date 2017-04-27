<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EncuestasMoviles._Default" %>
<%@ Register TagName="messageBox" TagPrefix="mesBx" Src="~/MessageBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <link href="Styles/Site.css" type="text/css" rel="Stylesheet" />
    <title>.:: Encuestas Móviles ::. </title>
    <meta http-equiv="CACHE-CONTROL" content="NO-CACHE" />
    <meta http-equiv="PRAGMA" content="NO-CACHE" />
    <style type="text/css">
        img, div, table, input
        {
            behavior: url("js/iepngfix.htc");
        }
        .textogeneralnegrobold
        {
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
        }
        
        .textoSecciones
        {
            font-family: Arial;
            font-size: 14px;            font-weight: bold;
            padding-left: 10px;
        }
        
        .ErrorValidator
        {
            color: red;
            font-family: Arial;
            font-size: 8px;
            font-weight: bold;
            padding-left: 10px;
        }
        
        .fecha
        {
            font-family: Arial;
            font-size: 10px;
            font-weight: bold;
            padding-left: 10px;
        }
        
        .contenido
        {
            position: relative;
            margin-top: 5px;
            z-index: 1;
        }
        #imgFoto
        {
            position: fixed;
            top: -171;
            left: 0;
            width: 100%;
            height: 100%;
        }
        
        #dhtmltooltip
        {
            position: absolute;
            width: 150px;
            border: 1px solid darkgray;
            padding: 2px;
            background-color: yellow;
            visibility: hidden;
            z-index: 100; /*Remove below line to remove shadow. Below line should always appear last within this CSS*/
            filter: progid:DXImageTransform.Microsoft.Shadow(color=gray,direction=135);
        }
        
        div.wn
        {
            position: relative;
            width: 184px;
            height: 52px;
            overflow: hidden;
        }
        body
        {
            color: #505050;
            background-color: #025F8F;
            background-image: url(Images/FondoLogin1.png);
            background-repeat: no-repeat;
            background-position: center;
            margin: auto;
            float: none;
            width: 1024px;
            height: 768px;
        }
        .style1
        {
            width: 84px;
        }
        .style3
        {
            font-family: Arial;
            font-size: 12px;
            font-weight: bold;
            width: 84px;
        }
        
    </style>
    <script src="js/jquery-1.5.1.js" type="text/javascript"></script>
    <script language="Javascript" src="js/Login.js" type="text/javascript"></script>
    <script language="Javascript" src="js/rsa.js" type="text/javascript"></script>
    <script language="Javascript" src="js/aes-enc.js" type="text/javascript"></script>
    <script language="Javascript" src="js/sha1.js" type="text/javascript"></script>
    <script language="Javascript" src="js/base64.js" type="text/javascript"></script>
    <script language="Javascript" src="js/PGpubkey.js" type="text/javascript"></script>
    <script language="Javascript" src="js/PGencode.js" type="text/javascript"></script>
    <script language="Javascript" src="js/mouse.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }

        function ModFunction(Main, Modulus) {
            var ModFunction = Main - (Math.floor((Main / Modulus)) * Modulus);
            return ModFunction;
        }

        function Mult(x, p, m) {
            var y = 1;
            var i = 0;
            for (i = p; i > 0; i--) {
                while ((i / 2) == (Math.floor((i / 2)))) {
                    x = ModFunction((x * x), m);
                    i = (i / 2);
                }
                y = ModFunction((x * y), m);
            }
            return y;
        }


        function Encode() {
            var encoded_field = "";
            var original_field = document.getElementById('txtContraseña').value

            if (original_field != "") {
                var Enc = document.getElementById('hdnE').value;
                var Mod = document.getElementById('hdnN').value;

                for (i = 0; i <= original_field.length - 1; i++) {
                    // charCodeAt gives the ASC value of character in position i
                    //Aqui encripta el password caracter por caracter y lo deja en la cadena 'encoded_field'
                    encoded_field = encoded_field + Mult((original_field.charCodeAt(i)), Enc, Mod) + ","
                }

                document.getElementById('txtContraseña').value = "";
                document.getElementById('passEncode').value = encoded_field;
            }
            else {
                alert('Ingrese la contraseña ¡¡¡');
                return false;
            }
        }            
    </script>
    <script language="JavaScript" type="text/JavaScript">

        function cerrar() {
            div = document.getElementById('flotante');
            div.style.display = 'none';
        }

        function LogOutUsuario() {
            if (document.getElementById('hdnLogIn').value != "") {
                document.getElementById('btnLogOutUsuario').click();
            }
        }
    </script>
</head>
<body onunload="LogOutUsuario()" style="color: #505050;
            background-color: #025F8F;
            background-image: url(Images/FondoLogin1.png);
            background-repeat: no-repeat;
            background-position: center;
            margin: auto;
            float: none;
            width: 1024px;
            height: 768px;">
    <script language="javascript" type="text/javascript">

        var offsetxpoint = -60 //Customize x offset of tooltip
        var offsetypoint = 20 //Customize y offset of tooltip
        var ie = document.all
        var ns6 = document.getElementById && !document.all
        var enabletip = false
        if (ie || ns6)
            var tipobj = document.all ? document.all["dhtmltooltip"] : document.getElementById ? document.getElementById("dhtmltooltip") : ""

        function ietruebody() {
            return (document.compatMode && document.compatMode != "BackCompat") ? document.documentElement : document.body
        }

        function ddrivetip(thetext, thecolor, thewidth) {
            if (ns6 || ie) {
                if (typeof thewidth != "undefined") tipobj.style.width = thewidth + "px"
                if (typeof thecolor != "undefined" && thecolor != "") tipobj.style.backgroundColor = thecolor
                tipobj.innerHTML = thetext
                enabletip = true
                return false
            }
        }

        function positiontip(e) {
            if (enabletip) {
                var curX = (ns6) ? e.pageX : event.clientX + ietruebody().scrollLeft;
                var curY = (ns6) ? e.pageY : event.clientY + ietruebody().scrollTop;
                //Find out how close the mouse is to the corner of the window
                var rightedge = ie && !window.opera ? ietruebody().clientWidth - event.clientX - offsetxpoint : window.innerWidth - e.clientX - offsetxpoint - 20
                var bottomedge = ie && !window.opera ? ietruebody().clientHeight - event.clientY - offsetypoint : window.innerHeight - e.clientY - offsetypoint - 20

                var leftedge = (offsetxpoint < 0) ? offsetxpoint * (-1) : -1000

                //if the horizontal distance isn't enough to accomodate the width of the context menu
                if (rightedge < tipobj.offsetWidth)
                //move the horizontal position of the menu to the left by it's width
                    tipobj.style.left = ie ? ietruebody().scrollLeft + event.clientX - tipobj.offsetWidth + "px" : window.pageXOffset + e.clientX - tipobj.offsetWidth + "px"
                else if (curX < leftedge)
                    tipobj.style.left = "5px"
                else
                //position the horizontal position of the menu where the mouse is positioned
                    tipobj.style.left = curX + offsetxpoint + "px"

                //same concept with the vertical position
                if (bottomedge < tipobj.offsetHeight)
                    tipobj.style.top = ie ? ietruebody().scrollTop + event.clientY - tipobj.offsetHeight - offsetypoint + "px" : window.pageYOffset + e.clientY - tipobj.offsetHeight - offsetypoint + "px"
                else
                    tipobj.style.top = curY + offsetypoint + "px"
                tipobj.style.visibility = "visible"

                enabletip = false;
            }
        }

        function hideddrivetip() {
            if (ns6 || ie) {
                enabletip = false
                tipobj.style.visibility = "hidden"
                tipobj.style.left = "-1000px"
                tipobj.style.backgroundColor = ''
                tipobj.style.width = ''
            }
        }

        var ventanaAbierta;
        function abrirVentanaInicio() {

            ventanaAbierta = window.open('Pages/Principal.aspx', 'Encuestas', 'top=0,left=0,width=1024, height=720, status=yes');
        }

        function cerrarVentanaInicio() {
            ventanaAbierta.close();
        }

        function TerminaSession() {
            if (ventanaAbierta)
                cerrarVentanaInicio();

            ShowLogin();
            document.getElementById('btnCloseSession').click();
        }

        function ShowLogin() {

            document.getElementById('txtUsuario').value = '';

        }
        function ShowLogout() {

        }

        function abrirVentana(url, titulo) {
            window.open(url, titulo, 'top=0,left=0,width=1024, height=720, status=yes');
        }

        function abrirVentanaNormal(url, titulo) {
            window.open(url, titulo, 'top=0,left=0,width=1024, height=720, status=yes, toolbar=yes, menubar= yes, scrollbars=yes, Location=yes, Resizable=yes');
        }

        document.onmousemove = positiontip
    </script>
    <form id="Form1" runat="server" style="height: 768px; width: 100%">
    <asp:ScriptManager runat="server" ID="defa" EnablePartialRendering="false"></asp:ScriptManager>    
    <mesBx:messageBox runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento"  />
    


    <table width="100%">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label Font-Bold="true" Font-Size="Small" runat="server" ID="lblFecha" Font-Names="Arial"></asp:Label>
            </td>
        </tr>
    </table>        
    <br />
    <br />    
    <div id="txtError" runat="server" style="text-align:center; color:Red"></div>
    <table align="center" cellpadding="5" width="266" height="382" style="background: url(Images/Cuadrologin2.png) no-repeat;">
        <tr>
            <td runat="server" valign="top" id="tdError" visible="false" class="ErrorValidator">
            </td>                        
        </tr>
        <tr>
            <td height="382" valign="top">
                <br />
                <table width="100%" cellpadding="5">                    
                    <tr>
                        <td colspan="2" align="center" class="textoLogin">
                            Llave Maestra
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="textoLogin">
                            Usuario:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUsuario" runat="server" CssClass="inputtext" BorderStyle="Solid"
                                BorderWidth="1" Width="130px" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsuario"
                                ErrorMessage="Ingresa tu Usuario" ValidationGroup="login">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>                    
                    <tr>
                        <td class="textoLogin">
                            Contraseña:
                        </td>
                        <td>
                            <asp:TextBox ID="txtContraseña" runat="server" CssClass="inputtext" TextMode="Password"
                                BorderStyle="Solid" BorderWidth="1" Width="130px" >
                            </asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContraseña"
                                ErrorMessage="Ingresa tu PassWord" ValidationGroup="login">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:ImageButton ID="btnLogin" style="cursor:pointer;" runat="server" ImageUrl="~/Images/BtnEntrar.png" OnClick="btnLogin_Click"
                                ValidationGroup="login" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" id="tdError2" class="ErrorValidator">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="justify" colspan="2"  class="textoLogin">
                            El usuario y contraseña son los de Llave maestra.&nbsp; Si tienes duda sobre tu
                            usuario y contraseña, comunícate a *5.
                            <br /><br />
                            Recuerda que tu clave de usuario y
                            contraseña son únicas e intransferibles.
                            <br /><br />
                            Es importante que renueves tu contraseña
                            cada 90 días Conoce las mejores prácticas de seguridad en la sección Seguridad de
                            la Información
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div>
        <input type="hidden" runat="server" id="hdnE" />
        <input type="hidden" runat="server" id="hdnN" />
        <asp:HiddenField ID="hdnSkin" runat="server" />
        <asp:HiddenField ID="hdnLogIn" runat="server" Value="" />
        <asp:HiddenField ID="hdnNumUsuario" runat="server" Value="" />
        <asp:HiddenField ID="B" runat="server" Value="" />
        <asp:Button runat="server" ID="btnLogOutUsuario" Text="Log" Visible="true" Style="display: none" />
        <asp:Button runat="server" ID="btnCloseSession" Text="CloseSession" OnClick="btnCloseSession_Click"
            Visible="true" Style="display: none" />
    </div>
    </form>
    <input type="hidden" id="HidUsr" runat="server" value="0" />
</body>
</html>
