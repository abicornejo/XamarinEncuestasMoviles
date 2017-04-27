<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoDispo.aspx.cs" Inherits="EncuestasMoviles.Pages.InfoDispo" %>

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
</head>
<body>
    <form id="form1" runat="server">
    <div class="personPopupResult">
        <div style="height: 50px; width: 50px">
            <div style="display: inline-block; float: right; vertical-align: text-bottom; background-color: White;">
                <div>
                    Número:
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="lblDispoNum"></asp:Label></div>
                </div>
                <div>
                    Modelo:
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="lblDispoModelo"></asp:Label></div>
                </div>
                <div>
                    Marca:
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="lblDispoMarca"></asp:Label></div>
                </div>
                <div>
                    Descripcion:
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="lblDispoDesc"></asp:Label></div>
                </div>
                <div>
                    MEID:
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="lblDispoMeid"></asp:Label></div>
                </div>
                <div>
                    MDN:
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="lblDispoMdn"></asp:Label></div>
                </div>
                <div>
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="Label1" Text="         "></asp:Label></div>
                </div>
                <div>
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="Label2" Text="         "></asp:Label></div>
                </div>
                <div>
                    <div style="display: inline-block">
                        <asp:Label runat="server" ID="Label3" Text="         "></asp:Label></div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
