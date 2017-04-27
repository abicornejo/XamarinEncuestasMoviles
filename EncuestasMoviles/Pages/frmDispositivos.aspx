<%@ Page Title="" EnableViewState="true" Language="C#" MasterPageFile="~/MasterEncuesta.Master"
    AutoEventWireup="true" CodeBehind="frmDispositivos.aspx.cs" Inherits="EncuestasMoviles.Pages.frmDispositivos" %>
<%@ Register TagName="ctrlMensaje" TagPrefix="msje" Src="~/MessageBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">


<script src="../Scripts/jquery-1.9.1.js" type="text/javascript"></script>
<script src="../Scripts/jquery.metadata.js" type="text/javascript"></script>
<script src="../Scripts/jquery.dataTables.js" type="text/javascript"></script>
<script src="../Scripts/ZeroClipboard.js" type="text/javascript"></script>
<script src="../Scripts/TableTools.js" type="text/javascript"></script>

           

<link href="../Styles/jquery.dataTables.css" rel="stylesheet" type="text/css" />
<link href="../Styles/TableTools.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

    $(document).on("ready", function () {

        $.metadata.setType("class");

        $("table.grid").each(function () {
            var grid = $(this);


            if (grid.find("tbody > tr > th").length > 0) {
                grid.find("tbody").before("<thead><tr></tr></thead>");
                grid.find("thead:first tr").append(grid.find("th"));
                grid.find("tbody tr:first").remove();
            }


            if (grid.hasClass("sortable") && grid.find("tbody:first > tr").length > 10) {
                grid.dataTable({
                    sPaginationType: "full_numbers",
                    aoColumnDefs: [
                                { bSortable: false, aTargets: grid.metadata().disableSortCols }
                            ]
                });
            }
        });

        $('table.grid').dataTable({
            "bDestroy": true,
            "asStripClasses": null,
            "fnDrawCallback":
			            function () {
			                this.css('width', '100%');
			            },
            "sPaginationType": "full_numbers",
            "iDisplayLength": 5,
            "bPaginate": false,
            "sScrollY": "350px",
            "oLanguage": { "sProcessing": "Procesando, por favor espere...",
                "sLengthMenu": "Mostrar <select><option value='5'>5</option><option value='10'>10</option><option value='25'>25</option><option value='50'>50</option><option value='100' selected='selected'>100</option></select> registros por p&aacute;gina",
                "sZeroRecords": "No se encontraron resultados",
                "sInfo": "&nbsp;&nbsp;Mostrando desde _START_ hasta _END_ de _TOTAL_ registros&nbsp;&nbsp;",
                "sInfoEmpty": "&nbsp;&nbsp;Mostrando desde 0 hasta 0 de 0 registros&nbsp;&nbsp;",
                "sInfoFiltered": "<br><em>( filtrado de _MAX_ registros en total )</em>",
                "sInfoPostFix": "",
                "sSearch": "Buscar: ",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sPrevious": "Anterior",
                    "sLast": "Ultimo",
                    "sNext": "Siguiente"
                }
            },
            "sDom": 'Tlfrtip',
            "oTableTools": {
                "sSwfPath": "../swf/copy_csv_xls_pdf.swf"
            }


        });



    });

</script>


    <msje:ctrlMensaje runat="server" ID="ctrlMessageBox" OnAcepta_Evento="Acepta_Evento" />
    <asp:PlaceHolder runat="server" ID="phMen"></asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phCtrlDis"></asp:PlaceHolder>
    <br />
    
    <table width="95%" align="center">
        <tr>
            <td align="right">                 
                <asp:Button CausesValidation="false" CssClass="btnMasterRectangular" runat="server"
                    ID="btnNuevoDispositivo" Text="Alta Dispositivo" OnClick="btnNuevoDispositivo_Click" />
            </td>
        </tr>
    </table>
    <br />
    <table width="95%" align="center" id="Table1" style="height: auto; background-color: #F2F2F2; text-align: center">
        <tr>
            <td class="ContenedorGeneral" style="height: auto; width: 100%; position: static; border: 1px solid #3F3F3F;">
                <asp:GridView runat="server" ID="gvAltaDispositivo" CellPadding="4" ForeColor="#333333"
                    GridLines="Both" AutoGenerateColumns="false" Width="100%" OnRowCommand="gvAltaDispositivo_RowCommand" PageSize="8"
                    EmptyDataText="No se Encontraron Dispositivos" CssClass="grid sortable {disableSortCols: [4]}">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="IdDispositivo" Visible="false" />
                        <asp:TemplateField HeaderText="Foto">
                            <ItemTemplate>
                                <img alt="imagen" id="imgDisp" width="40px" height="40px" src="<%# Eval("ImagenTelefono") %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Número Teléfono" DataField="NumerodelTelefono" />
                        <asp:BoundField HeaderText="Modelo" DataField="Modelo" />
                        <asp:BoundField HeaderText="Marca" DataField="Marca" />
                        <asp:BoundField HeaderText="Descripción" DataField="DispositivoDesc" />
                        <asp:BoundField HeaderText="IMEI" DataField="DispositivoMeid" />
                        <asp:BoundField HeaderText="MDN" DataField="DispositivoMdn" />
                        <asp:TemplateField HeaderText="Operaciones">
                            <ItemTemplate>
                                <asp:ImageButton Width="22px" ID="btnEditar" UseSubmitBehavior="true" runat="server"
                                    ImageUrl="~/Images/iconoeditar.png" ToolTip="Editar Dispositivo" CommandName="Editar"
                                    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                <asp:ImageButton Width="22px" ID="btnEliminar" UseSubmitBehavior="true" runat="server"
                                    ImageUrl="~/Images/iconoeliminar.png" ToolTip="Eliminar Dispositivo" CommandName="Elimina"
                                    CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
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
            </td>
        </tr>
    </table>
    <%--DispoAlta--%>
    <div>
        <asp:HiddenField runat="server" ID="hfRutaImg" />
        <asp:HiddenField runat="server" ID="txtIdDispo" />
        <asp:HiddenField runat="server" ID="txtImgaDispo" />
        <div>
            <asp:Label ID="ModalAltaDispositivo" runat="server"></asp:Label>
            <asp:ModalPopupExtender ID="mpeAltaDispositivo" runat="server" TargetControlID="ModalAltaDispositivo"
                PopupControlID="panelAltaDispositivo" BackgroundCssClass="modalBackground" CancelControlID="btnCancelarAltaDispositivo"
                DropShadow="true" PopupDragHandleControlID="panelAltaDispositivoHead" />
        </div>
        <div>
            <asp:Panel ID="panelAltaDispositivo" runat="server" CssClass="ContenedorGeneral"
                Style="display: block; width: 600px">
                <asp:Panel ID="panelAltaDispositivoHead" CssClass="MasterTituloContenedor" runat="server"
                    Width="100%">
                    <div style="float: left">
                        <h2>
                            <asp:Label runat="server" ID="lblTituloModalDispositivo"></asp:Label></h2>
                    </div>
                    <div style="float: right">
                        <asp:ImageButton CausesValidation="false" runat="server" ID="btnCerrarAltDisp" ImageUrl="~/Images/Iconocerrar.png"
                            OnClick="btnCerrarAltDisp_Click" />
                    </div>
                </asp:Panel>
                <asp:UpdatePanel runat="server" ID="upAltaDisp">
                    <ContentTemplate>
                        <div style="margin-top: 25px; margin-left: 25px">
                            <div id="DivErrorres" style="width:100%; color:Red; text-align:center;" runat="server"></div>
                            <div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            Número de Teléfono:
                                        </td>
                                        <td>
                                            <asp:TextBox ValidationGroup="groupDispositivo" runat="server" ID="txtNumeroTelefono"
                                                onkeypress="return ValidaNumeros(event)" MaxLength="10" Width="300px" CssClass="inputtext"></asp:TextBox>
                                            <asp:ValidatorCalloutExtender runat="server" ID="vcetxtNumeroTelefono" TargetControlID="rfvtxtNumeroTelefono"
                                                Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                            <asp:RequiredFieldValidator ValidationGroup="groupDispositivo" Display="None" ID="rfvtxtNumeroTelefono"
                                                ControlToValidate="txtNumeroTelefono" SetFocusOnError="True" runat="server" ErrorMessage="<div>El número de teléfono es obligatorio</div>"
                                                ForeColor="Black"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                Modelo:
                                            </td>
                                            <td>
                                                <asp:TextBox ValidationGroup="groupDispositivo" runat="server" ID="txtModeloTelefono"
                                                onkeypress="return validaCaracteresNoEncuestas(event)" MaxLength="40" Width="300px" CssClass="inputtext"></asp:TextBox>
                            </div>
                                    <asp:ValidatorCalloutExtender runat="server" ID="vcetxtModeloTelefono" TargetControlID="rfvtxtModeloTelefono"
                                        Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                    <asp:RequiredFieldValidator ValidationGroup="groupDispositivo" Display="None" ID="rfvtxtModeloTelefono"
                                        ControlToValidate="txtModeloTelefono" SetFocusOnError="True" runat="server" ErrorMessage="<div>El modelo de teléfono es obligatorio</div>"
                                        ForeColor="Black"></asp:RequiredFieldValidator>
                                     </td>
                            </tr>
                            <tr>
                                <td>
                                    Marca:
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMarcaTelefono" ValidationGroup="groupDispositivo"
                                        onkeypress="return validaCaracteresNoEncuestas(event)" MaxLength="40" Width="300px" CssClass="inputtext"></asp:TextBox>
                        </div>
                        <asp:ValidatorCalloutExtender runat="server" ID="vcetxtMarcaTelefono" TargetControlID="rfvtxtMarcaTelefono"
                            Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                        <asp:RequiredFieldValidator ValidationGroup="groupDispositivo" Display="None" ID="rfvtxtMarcaTelefono"
                            ControlToValidate="txtMarcaTelefono" SetFocusOnError="True" runat="server" ErrorMessage="<div>La marca de teléfono es obligatorio</div>"
                            ForeColor="Black"></asp:RequiredFieldValidator></td> </tr>
                        <tr>
                            <td>
                                Imagen:
                            </td>
                            <td>
                                <asp:FileUpload EnableViewState="true" ID="subeImagenTelefono" runat="server" Width="300px"
                                    CssClass="inputtext" />

                                <asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="<div style='color:red'>Solo se permiten imagenes JPEG, PNG, GIF</div>" 
                                                            ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$" 
                                                            ControlToValidate="subeImagenTelefono">
                                                        </asp:RegularExpressionValidator>
                                                      
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Descripción:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ValidationGroup="groupDispositivo" ID="txtDescTelefono"
                                    onkeypress="return validaCaracteresNoEncuestas(event)" MaxLength="70" Width="300px" CssClass="inputtext"></asp:TextBox></div>
                                <asp:ValidatorCalloutExtender runat="server" ID="vcetxtDescTelefono" TargetControlID="rfvtxtDescTelefono"
                                    Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                <asp:RequiredFieldValidator ValidationGroup="groupDispositivo" Display="None" ID="rfvtxtDescTelefono"
                                    ControlToValidate="txtDescTelefono" SetFocusOnError="True" runat="server" ErrorMessage="<div>La descripción del teléfono es obligatoria</div>"
                                    ForeColor="Black"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                IMEI:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ValidationGroup="groupDispositivo" ID="txtMeidTelefono"
                                    onkeypress="return ValidaNumeros(event)" MaxLength="15" Width="300px" CssClass="inputtext"></asp:TextBox></div>
                                <asp:ValidatorCalloutExtender runat="server" ID="vcetxtMeidTelefono" TargetControlID="rfvtxtMeidTelefono"
                                    Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                <asp:RequiredFieldValidator ValidationGroup="groupDispositivo" Display="None" ID="rfvtxtMeidTelefono"
                                    ControlToValidate="txtMeidTelefono" SetFocusOnError="True" runat="server" ErrorMessage="<div>El meid del teléfono es obligatorio</div>"
                                    ForeColor="Black"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                MDN:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ValidationGroup="groupDispositivo" ID="txtMdnTelefono"
                                    onkeypress="return ValidaNumeros(event)" MaxLength="15" Width="300px" CssClass="inputtext"></asp:TextBox></div>
                                <asp:ValidatorCalloutExtender runat="server" ID="ValidatorCalloutExtender5" TargetControlID="rfvtxtMdnTelefono"
                                    Width="350px" CssClass="CustomValidatorCalloutStyle" Enabled="True" />
                                <asp:RequiredFieldValidator ValidationGroup="groupDispositivo" Display="None" ID="rfvtxtMdnTelefono"
                                    ControlToValidate="txtMdnTelefono" SetFocusOnError="True" runat="server" ErrorMessage="<div>El mdn de teléfono es obligatorio</div>"
                                    ForeColor="Black"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="margin-top: 25px; margin-bottom: 25px; text-align: center">
                                    <div style="text-align: center; display: inline;">
                                        <input class="btnMasterRectangular" type="button" id="btnGuarda" value="Guardar"
                                            onclick="obtImagen()" />
                                        <script type="text/javascript">
                                            function obtImagen() {
                                                document.getElementById("Body_hfRutaImg").value = document.getElementById("Body_subeImagenTelefono").value;
                                                document.getElementById("Body_btnGuardaAltaDispositivo").click();
                                            }
                                        </script>
                                        <div style="display: none">
                                            <asp:Button runat="server" ValidationGroup="groupDispositivo" CssClass="btnMasterRectangular"
                                                ID="btnGuardaAltaDispositivo" Text="Guardar" OnClick="btnGuardaAltaDispositivo_Click" />
                                        </div>
                                        <asp:Button CausesValidation="false" runat="server" CssClass="btnMasterRectangular"
                                            ID="btnCancelarAltaDispositivo" Text="Cancelar" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                        </table> </div> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
