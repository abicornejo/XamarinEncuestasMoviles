<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ctrlOpcionCatalogo.ascx.cs" Inherits="EncuestasMoviles.Controls.ctrlOpcionCatalogo" %>
<%@ Register TagName="messageBox" TagPrefix="mesBx" Src="~/MessageBox.ascx" %>


<div>
    <mesBx:messageBox runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento"/>
    <div style="display:none">
    <asp:Button runat="server" ID="elimina"/>
    </div>
    <asp:HiddenField runat="server" ID="Accion"/>
    <asp:HiddenField runat="server" ID="IdCatalogo" />
    
<asp:ModalPopupExtender BackgroundCssClass="ModalPopupBG" ID="mpeOpcionCatalogo"
        runat="server" TargetControlID="elimina" PopupControlID="DivMensajes"
       CancelControlID="btnCerrarModOpcCat"
        ></asp:ModalPopupExtender>

<asp:Panel runat="server" ID="DivMensajes" class="ContenedorGeneral" style="display:none; width:450px">
    <div class="MasterTituloContenedor">
        <div id="PopupHeader">
            <div style="float:left">
                <h2><asp:Label runat="server" ID="Titulo"></asp:Label></h2>
            </div>
                <div style="float:right">
                <asp:ImageButton runat="server" ID="btnCerrarModOpcCat" ImageUrl="~/Images/Iconocerrar.png" />                
                </div>
        </div>
    </div>
        <div>
            <div style="text-align:center">
                Nombre de la Opcion del Catalogo:
                <div style="display:inline-block; padding:10px 0 10px 0">
                    <asp:TextBox runat="server" ID="txtNomOpcCat" ValidationGroup="grupoAltOpcCat" onkeypress="return validaCaracteresNoEncuestas(event)" MaxLength="50"></asp:TextBox>
                    <asp:ValidatorCalloutExtender  runat="server" ID="vcetxtNomOpcCat" 
                                    TargetControlID="rfvtxtNomOpcCat" Width="350px" 
                                    CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="grupoAltOpcCat" ID="rfvtxtNomOpcCat" 
                                    ControlToValidate="txtNomOpcCat" SetFocusOnError="True" runat="server" 
                                    ErrorMessage="<div>El campo es obligatorio</div>" 
                                    ForeColor="Black"></asp:RequiredFieldValidator>   
                </div>
            </div>               
         </div>
         <div style="text-align:center">
                <asp:Button id="btnAceptar" ValidationGroup="grupoAltOpcCat" CssClass="btnMasterRectangular" runat="server" Text="Aceptar" onclick="btnAceptar_Click" />
            
                <input class="btnMasterRectangular" id="btnCancelarOpcCategoria" value="Limpiar" type="button" onclick="LimpiaCampoOpcCat();" />
                <script language="javascript" type="text/javascript">
                    function LimpiaCampoOpcCat() {
                        document.getElementById("Body_ctrlOpcionCat_txtNomOpcCat").value = "";
                    }
                </script>

         </div>    
</asp:Panel>
</div>
