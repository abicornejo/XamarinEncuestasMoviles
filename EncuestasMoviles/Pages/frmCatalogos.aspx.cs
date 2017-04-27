using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;
using Telerik.Web.UI;



namespace EncuestasMoviles.Pages
{
    public partial class frmCatalogos : System.Web.UI.Page
    {
        #region Variables
        GridView[] grid;
        #endregion

        protected void EventoOpciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.ToString() != string.Empty)
                {
                    List<TDI_OpcionCat> opciCatalogo =MngNegocioOpcionCat.ObtieneOpcionPorID(int.Parse(ViewState["IdCatalogoOpc"].ToString()));
                    opciCatalogo[0].OpcionCatDesc = sender.ToString();

                    bool ActualizaOpcCatalogo = MngNegocioOpcionCat.ActualizaOpcionporCatalogo(opciCatalogo[0]);

                    if (ActualizaOpcCatalogo)
                    {
                        EncuestasMoviles.Controls.ctrlOpcionCatalogo Editar = (EncuestasMoviles.Controls.ctrlOpcionCatalogo)ctrlOpcionCat;
                        ctrlMessageBox.AddMessage("Se ha Actualizado Correctamente la Opción del Catalogo", MessageBox.enmMessageType.Success, "Actualiza Opción de Catalogo");
                        ViewState["Opcion"] = "Correcto";
                        GuardaLogTransacc("Se Actualizo Opcion de Catalogo " + opciCatalogo[0].IdOpcionCat, 22);
                    }
                }
                else
                {
                    ctrlMessageBox.AddMessage("Se Agrego Correctamente la Opción del Catalogo", MessageBox.enmMessageType.Success, "Agrega Opción Catalogo");
                    ViewState["Opcion"] = "Correcto";
                    GuardaLogTransacc("Se Creo Opcion de Catalogo " + sender.ToString(), 21);
                }                
                CargaDatos();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void EventoAcepta_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.ToString() != "SaveOk")
                {
                    List<THE_Catalogo> Catalogo =MngNegocioCatalogo.ObtieneCatalogoPorId(int.Parse(ViewState["IdCatalogo"].ToString()));
                    Catalogo[0].CatalogoDesc = sender.ToString();
                    bool ActualizaCatalogo = MngNegocioCatalogo.ActualizaCatalogo(Catalogo[0]);
                    if (ActualizaCatalogo == true)
                    {
                        EncuestasMoviles.Controls.ctrlNewCatalogo Editar = (EncuestasMoviles.Controls.ctrlNewCatalogo)ctrlNewCat;
                        ctrlMessageBox.AddMessage("Se ha Actualizado Correctamente el Catalogo", MessageBox.enmMessageType.Success, "Actualiza Catalogo");
                        ViewState["Opcion"] = "Correcto";
                        GuardaLogTransacc("Se Actualizo el Catalogo " + Catalogo[0].IdCatalogo, 2);
                    }
                }
                else
                {
                    ctrlMessageBox.AddMessage("Se Agrego Correctamente el Catalogo", MessageBox.enmMessageType.Success, "Alta de Catalogo");
                    GuardaLogTransacc("Se Creo el Catalogo " + sender.ToString(), 1);
                }                
                CargaDatos();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }            
        }

        protected void Acepta_Evento(object sender, EventArgs e)
        {
            try
            {
                string Opcion = ViewState["Opcion"].ToString();
                if (Opcion == "Elimina")
                {
                    int ID = int.Parse(ViewState["IndexSeleccionado"].ToString());
                    List<TDI_OpcionCat> opCatalogo = MngNegocioOpcionCat.ObtieneOpcionPorID(ID);
                    opCatalogo[0].OpcionCatStat = 'B';
                    bool EliminaOpcion = MngNegocioOpcionCat.EliminaOpcionDelCatalogo(opCatalogo[0]);

                    if (EliminaOpcion)
                    {                        
                        CargaDatos();
                        ViewState["Opcion"] = "Correcto";
                        ctrlMessageBox.AddMessage("Se Elimino Correctamente la opcion del Catalogo", MessageBox.enmMessageType.Success, "Elimina Catalogo");
                        GuardaLogTransacc("Se Elimino Opcion del Catalogo " + opCatalogo[0].IdOpcionCat, 23);
                    }
                }

                if (Opcion == "Actualiza")
                {                    
                    CargaDatos();
                }

                if (Opcion == "Correcto")
                {                    
                    CargaDatos();                  
                }

                if (Opcion == "Elimina Catalogo")
                {
                    string IdCatalogo = hdnIdCatalogo.Value;
                    List<THE_Catalogo> Catalogo =MngNegocioCatalogo.ObtieneCatalogoPorId(int.Parse(IdCatalogo));
                    Catalogo[0].CatalogoStat = 'B';
                    bool EliminaCatalogo =MngNegocioCatalogo.EliminaCatalogo(Catalogo[0]);

                    if (EliminaCatalogo)
                    {
                        ctrlMessageBox.AddMessage("Se Elimino Correctamente el Catalogo", MessageBox.enmMessageType.Success, "Elimina Catalogo");
                        ViewState["Opcion"] = "Correcto";
                        GuardaLogTransacc("Se Elimino el Catalogo " + Catalogo[0].IdCatalogo, 3);
                    }   
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }            
            CargaDatos();          
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Response.Redirect("~/Default.aspx");
                }

                List<THE_SesionUsuario> existeSesion =MngNegocioUsuarioSesion.VerExisteSesionUsuario(Int32.Parse(Session["numeroUsuario"].ToString()), Session["UserIP"].ToString());
                if (existeSesion.Count == 0)
                {
                    return;
                }

                if (!IsPostBack)
                {
                    TDI_LogPaginas logPaginas = new TDI_LogPaginas();
                    logPaginas.LogFecha = DateTime.Now;
                    logPaginas.LogIp = Session["UserIP"].ToString();
                    logPaginas.LogUrlPagina = Request.RawUrl;
                    logPaginas.EmpleadoLlavePrimaria = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                    MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);
                }
                
                try
                {                    
                    CargaDatos();                    
                    ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                }
                catch (Exception ex)
                {
                    EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
                }
            }
            catch (Exception ms)
            {

            }
            finally { 
            
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                AjaxControlToolkit.ModalPopupExtender extNewCatalogo = new AjaxControlToolkit.ModalPopupExtender();
                Label lblTitulo = new Label();
                Label lblMensaje = new Label();
                TextBox txtNomCat = new TextBox();
                HiddenField hfAccion = new HiddenField();

                extNewCatalogo = ((AjaxControlToolkit.ModalPopupExtender)ctrlNewCat.FindControl("mpeNewCatalogo"));
                lblTitulo = ((Label)ctrlNewCat.FindControl("Titulo"));
                hfAccion = ((HiddenField)ctrlNewCat.FindControl("Accion"));
                txtNomCat = ((TextBox)ctrlNewCat.FindControl("txtNomCat"));
                txtNomCat.Text = string.Empty;
                hfAccion.Value = "1";
                lblTitulo.Text = "NUEVO CATALOGO";
                extNewCatalogo.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void CargaDatos()
        {
            try
            {
                acModules.Items.Clear();

                List<THE_Catalogo> listCatalogos = MngNegocioCatalogo.ObtieneTodosCatalogos();

                grid = new GridView[listCatalogos.Count];
                
                int count = 0;
                if (listCatalogos.Count > 0)
                {
                    foreach (THE_Catalogo itemCatalogo in listCatalogos)
                    {
                        List<TDI_OpcionCat> lstOpcioCatalogo = MngNegocioOpcionCat.ObtieneOpcionesPorCatalogo(itemCatalogo.IdCatalogo);
                        
                        if (lstOpcioCatalogo != null)
                        {
                            if (itemCatalogo != null)
                            {
                                RadPanelItem Item = new RadPanelItem();
                                Item.Text = itemCatalogo.CatalogoDesc;
                                Item.Value = itemCatalogo.IdCatalogo.ToString();

                                #region Tabla
                                Table tabla = new Table();
                                tabla.CellSpacing = 10;
                                tabla.CellPadding = 0;
                                tabla.Width = Unit.Percentage(100);

                                TableRow tr = new TableRow();
                                TableCell celda = new TableCell();
                                celda.HorizontalAlign = HorizontalAlign.Right;

                                Image btnAgregaNuevo = new Image();
                                btnAgregaNuevo.ImageUrl = "~/Images/iconoagregar.png";
                                btnAgregaNuevo.ToolTip = "Agregar Nuevo Resgistro al Catalogo";
                                btnAgregaNuevo.Width = Unit.Pixel(22);
                                btnAgregaNuevo.Style.Add(HtmlTextWriterStyle.Cursor, "hand");
                                btnAgregaNuevo.ID = "Agrega" + itemCatalogo.IdCatalogo.ToString();
                                btnAgregaNuevo.Attributes.Add("onclick", "AgregaNuevo(" + itemCatalogo.IdCatalogo.ToString() + ",'" + itemCatalogo.CatalogoDesc + "');");
                                celda.Controls.Add(btnAgregaNuevo);

                                Image btnEditar = new Image();
                                btnEditar.ImageUrl = "~/Images/iconoeditar.png";
                                btnEditar.ToolTip = "Editar Catalogo";
                                btnEditar.Width = Unit.Pixel(22);
                                btnEditar.Style.Add(HtmlTextWriterStyle.Cursor, "hand");
                                btnEditar.ID = "Edita" + itemCatalogo.IdCatalogo.ToString();                                
                                btnEditar.Attributes.Add("onclick", "Modifica(" + itemCatalogo.IdCatalogo.ToString() + ",'" + itemCatalogo.CatalogoDesc + "');");
                                celda.Controls.Add(btnEditar);

                                Image btnEliminar = new Image();
                                btnEliminar.ImageUrl = "~/Images/iconoeliminar.png";
                                btnEliminar.ToolTip = "Elimina Catalogo";
                                btnEliminar.Width = Unit.Pixel(22);
                                btnEliminar.Style.Add(HtmlTextWriterStyle.Cursor, "hand");
                                btnEliminar.ID = "Elimina" + itemCatalogo.IdCatalogo.ToString();
                                btnEliminar.Attributes.Add("onclick", "Elimina(" + itemCatalogo.IdCatalogo.ToString() + ",'" + itemCatalogo.CatalogoDesc + "');");
                                celda.Controls.Add(btnEliminar);
                                
                                tr.Cells.Add(celda);
                                tabla.Rows.Add(tr);
                                #endregion

                                GridView grd = new GridView();                                
                                grd.RowEditing += new GridViewEditEventHandler(grd_RowEditing);
                                grd.RowDeleting += new GridViewDeleteEventHandler(grd_RowDeleting);
                                grd.RowDataBound += new GridViewRowEventHandler(grd_RowDataBound);
                                
                                DataTable dataTab = new DataTable();
                                dataTab.Columns.Add(new DataColumn("IdOpcionCat"));
                                dataTab.Columns.Add(new DataColumn("OpcionCatDesc"));
                                dataTab.Columns.Add(new DataColumn("IdCatalogo"));

                                foreach(TDI_OpcionCat item in lstOpcioCatalogo)
                                {
                                    DataRow dr = dataTab.NewRow();
                                    dr[0] = item.IdOpcionCat;
                                    dr[1] = item.OpcionCatDesc;
                                    dr[2] = itemCatalogo.IdCatalogo.ToString();

                                    dataTab.Rows.Add(dr);
                                    dataTab.AcceptChanges();
                                }
                                
                                if (lstOpcioCatalogo != null)
                                {
                                    #region Grid
                                    grd.ClientIDMode = System.Web.UI.ClientIDMode.AutoID;
                                    grd.Width = Unit.Percentage(100);
                                    grd.AutoGenerateColumns = false;
                                    grd.ID = "GridVw";

                                    BoundField bound = new BoundField();
                                    bound.DataField = "IdOpcionCat";
                                    bound.HeaderText = "ID";
                                    grd.Columns.Add(bound);

                                    BoundField boundOp = new BoundField();
                                    boundOp.DataField = "OpcionCatDesc";
                                    boundOp.HeaderText = "Opción";
                                    grd.Columns.Add(boundOp);

                                    CommandField CmdFielEdit = new CommandField();
                                    CmdFielEdit.ButtonType = System.Web.UI.WebControls.ButtonType.Image;
                                    CmdFielEdit.EditImageUrl = "~/Images/iconoeditar.png";
                                    CmdFielEdit.HeaderText = "";                                    
                                    CmdFielEdit.ShowEditButton = true;
                                    CmdFielEdit.ControlStyle.Width = Unit.Pixel(22);
                                    grd.Columns.Add(CmdFielEdit);

                                    CommandField CmdFielDelete = new CommandField();
                                    CmdFielDelete.ButtonType = System.Web.UI.WebControls.ButtonType.Image;
                                    CmdFielDelete.DeleteImageUrl = "~/Images/iconoeliminar.png";
                                    CmdFielDelete.HeaderText = "";                                    
                                    CmdFielDelete.ShowDeleteButton = true;
                                    CmdFielDelete.ControlStyle.Width = Unit.Pixel(22);
                                    grd.Columns.Add(CmdFielDelete);
                                    
                                    BoundField boundCat = new BoundField();
                                    boundCat.DataField = "IdCatalogo";
                                    boundCat.HeaderText = "ID Cat";
                                    grd.Columns.Add(boundCat);

                                    grd.Columns[1].ItemStyle.Width = Unit.Percentage(80);
                                    grd.Columns[2].ItemStyle.Width = Unit.Percentage(10);
                                    grd.Columns[2].ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                                    grd.Columns[3].ItemStyle.Width = Unit.Percentage(10);
                                    grd.Columns[3].ItemStyle.HorizontalAlign = HorizontalAlign.Center;

                                    if (itemCatalogo != null)
                                    {
                                        grd.DataSource = dataTab;
                                        grd.RowStyle.CssClass = "RowsGrid";
                                        grd.HeaderStyle.CssClass = "headerGrid";
                                        grd.EmptyDataText = "<p>Sin opciones enlazadas a este catalogo</p>";
                                        
                                        grd.DataBind();
                                        grd.Columns[0].Visible = false;
                                        grd.Columns[4].Visible = false;
                                    }
                                    #endregion
                                }

                                RadPanelItem ItemHijo = new RadPanelItem();
                                ItemHijo.Controls.Add(tabla);
                                ItemHijo.Controls.Add(grd);
                                Item.Items.Add(ItemHijo);
                                
                                acModules.Items.Add(Item);                                
                            }
                            
                        }
                        count++;
                    }  
                }                
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnEliminaCatalogo_Click(object sender, EventArgs e)
        {
            try
            {
                string IdCatalogo = hdnIdCatalogo.Value;

                ctrlMessageBox.AddMessage("¿Esta seguro que desea eliminar el Catalogo Seleccionado?", MessageBox.enmMessageType.Attention, true, true, "prueba", "Elimina Catalogo");
                ViewState["Opcion"] = "Elimina Catalogo";

                RadPanelItem item = acModules.FindItemByValue(IdCatalogo);
                item.Expanded = true;
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnModificaCatalogo_Click(object sender, EventArgs e)
        {
            try
            {

                string IdCatalogo = hdnIdCatalogo.Value;
                string DescCatalogo = hdnDescCatalogo.Value;

                AjaxControlToolkit.ModalPopupExtender extNewCatalogo = new AjaxControlToolkit.ModalPopupExtender();
                Label lblTitulo = new Label();
                Label lblMensaje = new Label();
                TextBox txtNombreCatalogo = new TextBox();
                HiddenField hfAccion = new HiddenField();
                extNewCatalogo = ((AjaxControlToolkit.ModalPopupExtender)ctrlNewCat.FindControl("mpeNewCatalogo"));
                lblTitulo = ((Label)ctrlNewCat.FindControl("Titulo"));
                hfAccion = ((HiddenField)ctrlNewCat.FindControl("Accion"));
                txtNombreCatalogo = ((TextBox)ctrlNewCat.FindControl("txtNomCat"));
                hfAccion.Value = "2";
                lblTitulo.Text = "EDITA CATALOGO";
                txtNombreCatalogo.Text = DescCatalogo;
                ViewState["IdCatalogo"] = IdCatalogo;
                RadPanelItem item = acModules.FindItemByValue(IdCatalogo);
                item.Expanded = true;
                
                extNewCatalogo.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnAgregaNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                AjaxControlToolkit.ModalPopupExtender extNuevaOpcionCatalogo = new AjaxControlToolkit.ModalPopupExtender();
                Label lblTitulo = new Label();
                Label lblMensaje = new Label();
                TextBox txtNombOpc = new TextBox();
                HiddenField hfAccion = new HiddenField();
                HiddenField hfIdCatalogo = new HiddenField();

                extNuevaOpcionCatalogo = ((AjaxControlToolkit.ModalPopupExtender)ctrlOpcionCat.FindControl("mpeOpcionCatalogo"));
                lblTitulo = ((Label)ctrlOpcionCat.FindControl("Titulo"));
                hfAccion = ((HiddenField)ctrlOpcionCat.FindControl("Accion"));
                hfIdCatalogo = ((HiddenField)ctrlOpcionCat.FindControl("IdCatalogo"));
                txtNombOpc = ((TextBox)ctrlOpcionCat.FindControl("txtNomOpcCat"));
                txtNombOpc.Text = string.Empty;
                hfAccion.Value = "1";
                hfIdCatalogo.Value = hdnIdCatalogo.Value;
                lblTitulo.Text = "NUEVA OPCIÓN CATALOGO";

                RadPanelItem item = acModules.FindItemByValue(hfIdCatalogo.Value);
                item.Expanded = true;

                extNuevaOpcionCatalogo.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        public void ctrlMessageBox_MsgBoxAnswered(object sender, MessageBox.MsgBoxEventArgs e)
        {
            if (e.Answer == MessageBox.enmAnswer.OK)
            {
            }
            else
            {
                ctrlMessageBox.AddMessage("Ha Cancelado la Operación", MessageBox.enmMessageType.Info, "Operación Cancelada");
            }
        }

        public void GuardaLogTransacc(string Desc, int IdTran)
        {
            THE_LogTran oLogTran = new THE_LogTran();
            oLogTran.LogtDesc = Desc;
            oLogTran.LogtDomi = Session["UserDomain"].ToString();
            oLogTran.LogtFech = DateTime.Now;
            oLogTran.LogtMach = Session["userMachineName"].ToString();
            oLogTran.LogtUsIp = Session["UserIP"].ToString();
            oLogTran.LogtUsua = Session["UserName"].ToString();
            oLogTran.TranLlavPr = new TDI_Transacc() { TranLlavPr = IdTran };
            oLogTran.EmplLlavPr = new THE_Empleado() { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };

            MngNegocioTransacciones.GuardaLogTransaccion(oLogTran);
        }



        protected void grd_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                string IdCatalogo = ((GridView)(sender)).Rows[e.NewEditIndex].Cells[0].Text;
                
                string DescOpcionCatalogo = ((GridView)(sender)).Rows[e.NewEditIndex].Cells[1].Text;

                AjaxControlToolkit.ModalPopupExtender extNuevaOpcionCatalogo = new AjaxControlToolkit.ModalPopupExtender();
                Label lblTitulo = new Label();
                Label lblMensaje = new Label();
                HiddenField hfAccion = new HiddenField();
                HiddenField hfIdCatalogo = new HiddenField();
                TextBox txtDescOpcionCatalogo = new TextBox();

                
                extNuevaOpcionCatalogo = ((AjaxControlToolkit.ModalPopupExtender)ctrlOpcionCat.FindControl("mpeOpcionCatalogo"));
                lblTitulo = ((Label)ctrlOpcionCat.FindControl("Titulo"));
                hfAccion = ((HiddenField)ctrlOpcionCat.FindControl("Accion"));
                hfIdCatalogo = ((HiddenField)ctrlOpcionCat.FindControl("IdCatalogo"));
                txtDescOpcionCatalogo = ((TextBox)ctrlOpcionCat.FindControl("txtNomOpcCat"));
                hfAccion.Value = "2";
                hfIdCatalogo.Value = ((GridView)(sender)).Rows[e.NewEditIndex].Cells[4].Text;
                txtDescOpcionCatalogo.Text = DescOpcionCatalogo;                    
                lblTitulo.Text = "EDITA OPCIÓN CATALOGO";
                ViewState["IdCatalogoOpc"] = IdCatalogo;

                RadPanelItem item = acModules.FindItemByValue(hfIdCatalogo.Value);
                item.Expanded = true;

                extNuevaOpcionCatalogo.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void grd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                ctrlMessageBox.AddMessage("Desea Eliminar la Opción del Catalogo Seleccionado", MessageBox.enmMessageType.Attention, true, true, "prueba", "Elimina Opción Catalogo");

                RadPanelItem item = acModules.FindItemByValue(((GridView)(sender)).Rows[e.RowIndex].Cells[4].Text);
                item.Expanded = true;

                ViewState["IndexSeleccionado"] = int.Parse(((System.Web.UI.WebControls.GridView)(sender)).Rows[e.RowIndex].Cells[0].Text);
                ViewState["Opcion"] = "Elimina";
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.AccessKey = e.Row.RowIndex.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }

}
