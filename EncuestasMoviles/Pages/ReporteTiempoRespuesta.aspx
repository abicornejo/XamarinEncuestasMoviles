<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true" CodeBehind="ReporteTiempoRespuesta.aspx.cs" Inherits="EncuestasMoviles.Pages.ReporteTiempoRespuesta" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>

    <script src="../Scripts/jquery.metadata.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../Scripts/ZeroClipboard.js" type="text/javascript"></script>
    <script src="../Scripts/TableTools.js" type="text/javascript"></script>


    <link href="../Styles/jquery.dataTables.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/TableTools.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        $(document).ready(function () {

            $.metadata.setType("class");

            $("table.grid").each(function () {
                var grid = $(this);

              
                if (grid.find("tbody > tr > th").length > 0) {
                    grid.find("tbody").before("<thead><tr></tr></thead>");
                    grid.find("thead:first tr").append(grid.find("th"));
                    grid.find("tbody tr:first").remove();
                }

                // Si el GridView tiene la clase "sortable" aplicar el plugin DataTables si tiene más de 10 elementos.
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
                "fnDrawCallback":
			        function () {
			            this.css('width', '100%');
			        },
                "sPaginationType": "full_numbers",
                "iDisplayLength": 5,
                "oLanguage": { "sProcessing": "Procesando, por favor espere...",
                    "sLengthMenu": "Mostrar <select><option value='5' selected='selected'>5</option><option value='10'>10</option><option value='25'>25</option><option value='50'>50</option><option value='100'>100</option></select> registros por p&aacute;gina",
                    "sZeroRecords": "No se encontraron resultados",
                    "sInfo": "&nbsp;&nbsp;Mostrando desde _START_ hasta _END_ de _TOTAL_ registros&nbsp;&nbsp;",
                    "sInfoEmpty": "&nbsp;&nbsp;Mostrando desde 0 hasta 0 de 0 registros&nbsp;&nbsp;",
                    "sInfoFiltered": "<br><em>( filtrado de _MAX_ registros en total )</em>",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar: ",
                    //"sUrl": "",
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
<br /><br />

<center><asp:Label runat="server" ID="lblEncabezado" Font-Size="X-Large" ForeColor="Black"
                    Text="REPORTE TIEMPO DE RESPUESTA"></asp:Label></center>

    <br /><br />
    <center>
    <div class="contTiempoRespPrincipal" style=" float:left; width:100%;">
        <div class="izquierdo" style=" float:left; width:50%; height:100px;">
            <telerik:RadAjaxPanel runat="server" ID="RadAjaxPanel1">
                <telerik:RadComboBox ID="RadComboBox1" runat="server" CheckBoxes="false"
                    Width="250" Label="Seleccion de encuesta:" Height="400" Skin="Metro">                                                                       
                </telerik:RadComboBox>    
                <telerik:RadButton ID="Button1" runat="server" Text="Buscar" OnClick="Btnbuscar_Click"  />       
            </telerik:RadAjaxPanel>
        </div>
        <div  style=" float: left; width:40%;">        
             <asp:ImageButton ID="ImgExportExcel" runat="server" 
                    onclick="ImgExportExcel_Click" Width="25px" CausesValidation="false" 
                    Height="25px" ImageUrl="~/Images/icono_excel.jpg"  />
        </div>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
        
    </div>
    </center>
    <div class="GvTiempos" style=" height:auto; width:100%;background-color: #FFFFFF; padding-bottom:20px; margin-bottom:20px; text-align: center">
              <asp:GridView CssClass="grid sortable {disableSortCols: [4]}" runat="server" ID="gvTiempoRespuesta" ForeColor="#333333"
                GridLines="Vertical" AutoGenerateColumns="false" Width="100%" AllowPaging="False" 
                >
                <Columns>  
                    <asp:BoundField HeaderText="NombreUsuario" DataField="UsuaNom"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />      
                                                                                 
                    <asp:BoundField HeaderText="Genero" DataField="UsuGen" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />            
                                            
                    <asp:BoundField HeaderText="Grupo Edad" DataField="UsuGrEdad" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="NSE" DataField="UsuNse" HeaderStyle-HorizontalAlign="Center"
                        ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Encuesta" DataField="UsuEnc" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Fecha Envio" DataField="UsuFeEnv" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Fecha Empiezo" DataField="UsuFeEmp" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Fecha Termino" DataField="UsuFeTer" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />

                    <asp:BoundField HeaderText="Fecha Recepcion" DataField="UsuFeResp" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" />
                               
                </Columns>
             
            </asp:GridView>        
        </div>
    
</asp:Content>
