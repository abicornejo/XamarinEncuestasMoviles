<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true"
    CodeBehind="frmCatalogos.aspx.cs" Inherits="EncuestasMoviles.Pages.frmCatalogos" %>
<%@ Register TagName="messageBox" TagPrefix="mesBx" Src="~/MessageBox.ascx" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="<%=ResolveUrl("../js/Catalogos.js")%>"></script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
    <style type="text/css">
     /* RadPanelBar needs to know the height of the images to calculate 
     its height accordingly when the images are not loaded yet. */
     .RadPanelBar .rpImage
     {
         height: 19px;
     }
     .RadPanelBar .rpLevel1 .rpImage
     {
         height: 16px;
     }
    </style>
    <script language="javascript">
        function Elimina(id, Desc) {            
            document.getElementById("Body_hdnIdCatalogo").value = id;
            document.getElementById("Body_hdnDescCatalogo").value = Desc;
            document.getElementById("Body_btnEliminaCatalogo").click();
        }
        function Modifica(id, Desc) {            
            document.getElementById("Body_hdnIdCatalogo").value = id;
            document.getElementById("Body_hdnDescCatalogo").value = Desc;
            document.getElementById("Body_btnModificaCatalogo").click();
        }
        function AgregaNuevo(id, Desc) {            
            document.getElementById("Body_hdnIdCatalogo").value = id;
            document.getElementById("Body_hdnDescCatalogo").value = Desc;
            document.getElementById("Body_btnAgregaNuevo").click();
        }
    </script>
    <link href="../Skins/Windows7/PanelBar.Windows7.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <mesBx:messageBox runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento"/>
    <div style="display:inherit">
    <ctrls:NuevaOpcionCat runat="server" ID="ctrlOpcionCat" OnEventoBotonOpcionesClick="EventoOpciones_Click" />
    <ctrls:NewCatalogo runat="server" ID="ctrlNewCat" OnEventoBotonClick="EventoAcepta_Click" />
    </div>    
    <div style="display:none">
        <asp:Button runat="server" ToolTip="Elimina Catalogo" ID="btnEliminaCatalogo" OnClick="btnEliminaCatalogo_Click" />
        <asp:Button runat="server" ToolTip="Edita Catalogo" ID="btnModificaCatalogo" OnClick="btnModificaCatalogo_Click" />
        <asp:Button runat="server" ToolTip="Agrega Opciones al Catalogo" ID="btnAgregaNuevo"
            OnClick="btnAgregaNuevo_Click" />
        <input type="hidden" id="hdnIdCatalogo" runat="server" />
        <input type="hidden" id="hdnDescCatalogo" runat="server" />
    </div>
    <br />    
    <table style="vertical-align: middle; padding-top: 15px; margin-top:15px" align="center" width="95%">
        <tr>
            <td align="right">
                <asp:Button CssClass="btnMasterRectangular" runat="server" ID="btnNuevo" Text="Nuevo"
                OnClick="btnNuevo_Click" />
            </td>
        </tr>
        <tr>
            <td>                
                <telerik:radpanelbar runat="server" ID="acModules" Skin="Windows7"
                        ExpandMode="SingleExpandedItem" Width="100%" EnableViewState="true">
                </telerik:radpanelbar>            
            </td>
        </tr>        
    </table>
</asp:Content>
