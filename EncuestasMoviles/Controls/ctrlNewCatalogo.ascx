<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlNewCatalogo.ascx.cs" Inherits="EncuestasMoviles.Controls.ctrlNewCatalogo" %>
<%@ Register TagName="messageBox" TagPrefix="mesBx" Src="~/MessageBox.ascx" %>

<div>
    <mesBx:messageBox runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento"/>
    <div style="display:none">
    <asp:Button runat="server" ID="elimina" />
    </div>
    <asp:HiddenField runat="server" ID="Accion"/>

<asp:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="mpeNewCatalogo"
     runat="server" TargetControlID="elimina" PopupControlID="DivMensajes"
     CancelControlID="btnCerrarModCat"></asp:ModalPopupExtender>

<asp:Panel CssClass="ContenedorGeneral" runat="server" ID="DivMensajes" Style="display: none; width:450px">
    <div class="MasterTituloContenedor">
        <div id="PopupHeader">
            <div style="float:left">
                <h2><asp:Label runat="server" ID="Titulo"></asp:Label></h2>
            </div>
            <div style="float:right">
            <asp:ImageButton runat="server" ID="btnCerrarModCat" ImageUrl="~/Images/Iconocerrar.png" />                
            
            </div>
        </div>
   </div>
        <div style="text-align:center; padding:10px 0 10px 0">
            <div style="display:inline-block;padding:10px 0 10px 0">
            Nombre del catalogo : <div style="display:inline-block">
            <asp:TextBox runat="server" ID="txtNomCat" ValidationGroup="grupoAltCat" MaxLength="50" onkeypress="return validaCaracteresNoEncuestas(event)"></asp:TextBox>             
                        <asp:ValidatorCalloutExtender  runat="server" ID="vcetxtNomCat" 
                                    TargetControlID="rfvtxtNomCat" Width="350px" 
                                    CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="grupoAltCat" ID="rfvtxtNomCat" 
                                    ControlToValidate="txtNomCat" SetFocusOnError="True" runat="server" 
                                    ErrorMessage="<div>El campo es obligatorio</div>" 
                                    ForeColor="Black"></asp:RequiredFieldValidator>    
            </div>
            </div>               
        </div>
        <div style="text-align:center">
            
                <asp:Button id="btnAceptarCat" ValidationGroup="grupoAltCat" CssClass="btnMasterRectangular" runat="server" Text="Aceptar" onclick="btnAceptar_Click" />
            
             <input class="btnMasterRectangular" id="btnCancelarCat" value="Limpiar" onclick="LimpiaAltCat()" type="button" />
             <script language="javascript" type="text/javascript">
                 function LimpiaAltCat() {
                     document.getElementById("Body_ctrlNewCat_txtNomCat").value="";
                 }
             </script>
         </div>      
 </asp:Panel>
</div>
