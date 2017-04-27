<%@ Page Title=".:: Preguntas ::." Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true" CodeBehind="frmPreguntas.aspx.cs" Inherits="EncuestasMoviles.Pages.frmPreguntas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server">
<div id="Titulo" style="color: #000000; background-color:Lime;">Preguntas de Encuesta</div>
    <div id="Contoles">
        <label id="lblEncuPreg" style="color:Black">Encuesta:</label>
        <label id="lblTituEncuesta" runat="server" style="color:Red"></label>
    <div>
        <label id="lblPregunta" style="color:Black">Pregunta:</label>
        <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo"/>
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
    </div>
    </div>
<div id="Body">
    <asp:DataGrid ID="Grid" runat="server" ForeColor="Black"  
        AutoGenerateColumns="False" BorderColor="#009933" BorderStyle="Double" 
        onselectedindexchanged="Grid_SelectedIndexChanged" Width="100%">

        <Columns>
            <asp:BoundColumn HeaderStyle-Width="8%" HeaderText="No. Pregunta" DataField="IdPregunta" ></asp:BoundColumn>
            <asp:BoundColumn HeaderStyle-Width="60%" HeaderText="Pregunta" DataField="PreguntaDesc"></asp:BoundColumn>
            <asp:BoundColumn HeaderStyle-Width="8%" HeaderText="Estado" DataField="Estatus"></asp:BoundColumn>
            <asp:TemplateColumn HeaderStyle-Width="24%" HeaderText="Operación">
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" Text="Editar" Width="50px" />
                    <asp:Button ID="Button2" runat="server" Text="Agregar" Width="51px"/>
                    <asp:Button ID="Button3" runat="server" Text="Eliminar" Width="52px"/>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
        
    </asp:DataGrid>
</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>
