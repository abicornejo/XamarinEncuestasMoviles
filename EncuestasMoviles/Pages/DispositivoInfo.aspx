<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispositivoInfo.aspx.cs"
    Inherits="EncuestasMoviles.Pages.DispositivoInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function verImagen(imga) {
            debugger;
            document.getElementById("imgDispo").src = imga;
        }
    </script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="personPopupResult">
            <table width="100%">
                <tr>
                    <td align="left" class="textoLogin">Nombre:</td>
                    <td align="left" class="textoLogin"><asp:Label runat="server" ID="lblNombreUsua"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" class="textoLogin">Número:</td>
                    <td align="left" class="textoLogin"><asp:Label runat="server" ID="lblDispoNum"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" class="textoLogin">Estado:</td>
                    <td align="left" class="textoLogin"><asp:Label runat="server" ID="lblEstado"></asp:Label></td>
                </tr>                    
            </table>
        </div>
    </form>
</body>
</html>
