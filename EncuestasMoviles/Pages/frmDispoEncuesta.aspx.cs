using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using AjaxControlToolkit;
using BLL_EncuestasMoviles;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace EncuestasMoviles.Pages
{
    public partial class frmDispoEncuesta : System.Web.UI.Page
    {
        #region Variables
        
        AjaxControlToolkit.ModalPopupExtender Mensaje = new AjaxControlToolkit.ModalPopupExtender();
        HtmlTextWriterStyle style = HtmlTextWriterStyle.Display;
        #endregion

        public class ViewTemplate : ITemplate
        {
            private DataControlRowType templateType;
            private string columnName;
            UserControl ctrlthis;

            public ViewTemplate(DataControlRowType type, string colname)
            {
                templateType = type;
                columnName = colname;
            }

            public void InstantiateIn(System.Web.UI.Control contenedor)
            {
                switch (templateType)
                {
                    case DataControlRowType.Header:
                        Panel pnl = new Panel();
                        Table tbl = new Table();
                        tbl.Width = Unit.Percentage(100);
                        TableRow rw;
                        TableCell cll;
                        contenedor.Controls.Add(pnl);
                        pnl.HorizontalAlign = HorizontalAlign.Center;
                        rw = new TableRow();
                        cll = new TableCell();
                        cll.Text = columnName;
                        cll.HorizontalAlign = HorizontalAlign.Left;
                        rw.Controls.Add(cll);
                        cll = new TableCell();
                        cll.HorizontalAlign = HorizontalAlign.Right;
                        rw.Controls.Add(cll);
                        tbl.Controls.Add(rw);
                        tbl.GridLines = GridLines.None;
                        pnl.Controls.Add(tbl);
                        break;
                    default:
                        break;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Response.Redirect("~/Default.aspx");
                }
                try
                {
                    if (!IsPostBack)
                    {
                        VistaActiva.Value = "vd";
                        tdViewDetails.Style.Add(style, "block");
                        tdViewImagen.Style.Add(style, "none");
                        TDI_LogPaginas logPaginas = new TDI_LogPaginas();
                        logPaginas.LogFecha = DateTime.Now;
                        logPaginas.LogIp = Session["UserIP"].ToString();
                        logPaginas.LogUrlPagina = Request.RawUrl;
                        logPaginas.EmpleadoLlavePrimaria = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                        MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);
                        CargaGrid();
                        ObtieneTodosDispos();
                        txtFechaIni.SelectedDate = DateTime.Now;
                        txtFechaFin.SelectedDate = DateTime.Now;
                        txtBuscaEncuesta.Text = "";
                        CargaDatosCatalogos();
                    }
                    else
                    {                        
                        if (VistaActiva.Value == "vd")
                        {
                            tdViewDetails.Style.Add(style, "block");
                            tdViewImagen.Style.Add(style, "none");
                        }
                        else
                        {
                            tdViewDetails.Style.Add(style, "none");
                            tdViewImagen.Style.Add(style, "block");
                        }

                        ObtieneTodosDispos();
                        CargaDatosCatalogos();
                        if (hfIdEncuestaUnico.Value == "")
                        {
                            CargaGrid();
                        }

                    }

                }
                catch (Exception ex)
                {
                    EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
                }
            }
            catch (Exception ms)
            {

            }
            finally { 
            
            }
        }

        void CargaGrid()
        {
            try
            {
                List<THE_Encuesta> encu =MngNegocioEncuesta.ObtieneTodasEncuestasActivas();
                gvDispoEncu.DataSource = encu;
                Session["datasource"] = encu;
                gvDispoEncu.DataBind();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        private void ObtieneTodosDispos()
        {
            try
            {
                List<THE_Dispositivo> lstDisposDisponi =MngNegocioDispositivo.ObtieneDispositivosAsignadosUsuario();

                for (int ini = 0; ini < lstDisposDisponi.Count; ini++)
                {
                    if (lstDisposDisponi[ini].ImagenTelefono == null)
                        lstDisposDisponi[ini].ImagenTelefono = "../Images/no_foto.jpg";
                    else
                        lstDisposDisponi[ini].ImagenTelefono = "../Media/Dispositivos/" + lstDisposDisponi[ini].IdDispositivo + "/" + lstDisposDisponi[ini].ImagenTelefono;
                }

                lvDispositivosEncu.DataSource = null;
                for (int ini = 0; ini < lstDisposDisponi.Count; ini++)
                {
                   
                    lstDisposDisponi[ini].EstatusCheck = "";
                    lstDisposDisponi[ini].ChkEnabled = "";
                    lstDisposDisponi[ini].ColorEstatus = "../Images/not.jpg";
                    lstDisposDisponi[ini].StrColor = "Rojo";
                }
                lvDispositivosEncu.DataSource = lstDisposDisponi;
                lvDispositivosEncu.DataBind();


                ListView1.DataSource = lstDisposDisponi;
                ListView1.DataBind();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        private void ObtieneDispoDisponiblesEncuesta(int IdEncuSel)
        {
            try
            {
                List<THE_Dispositivo> lstDispoEncu = new List<THE_Dispositivo>();
             
                List<THE_Dispositivo> lstDispoDispo =MngNegocioDispositivo.ObtieneDispositivosAsignadosUsuario();

                for (int ini = 0; ini < lstDispoDispo.Count; ini++)
                {
                    if (lstDispoDispo[ini].ImagenTelefono == null)
                        lstDispoDispo[ini].ImagenTelefono = "../Images/no_foto.jpg";
                    else
                        lstDispoDispo[ini].ImagenTelefono = "../Media/Dispositivos/" + lstDispoDispo[ini].IdDispositivo + "/" + lstDispoDispo[ini].ImagenTelefono;
                }

                foreach (THE_Dispositivo itmDispo in lstDispoDispo)
                {
                    List<TDI_EncuestaDispositivo> itmEncuDis = MngNegocioEncuestaDispositivo.ObtieneEstatusDispoEncu(itmDispo.IdDispositivo, IdEncuSel);
                    if (itmEncuDis.Count > 0)
                    {
                        itmDispo.ColorEstatus = itmEncuDis[0].IdDispositivo.ColorEstatus;
                        itmDispo.EstatusCheck = itmEncuDis[0].IdDispositivo.EstatusCheck;
                        itmDispo.ChkEnabled = itmEncuDis[0].IdDispositivo.ChkEnabled;                        
                    }
                    else
                    {
                        itmDispo.ColorEstatus = "../Images/not.jpg";
                        itmDispo.EstatusCheck = "";
                        itmDispo.ChkEnabled = "";
                        itmDispo.StrColor = "Rojo";
                    }

                    lstDispoEncu.Add(itmDispo);
                }
                lvDispositivosEncu.DataSource = null;
                lvDispositivosEncu.DataSource = lstDispoEncu;
                lvDispositivosEncu.DataBind();

               
                ListView1.DataSource = lstDispoEncu;
                ListView1.DataBind();
                
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnEnviaAsignados_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfIdsEncuestas.Value == "" || hfIdsDispositivos.Value == "") return;
                string[] idsEncuestas = hfIdsEncuestas.Value.Remove(hfIdsEncuestas.Value.Length - 1, 1).ToString().Split(',');

                bool enviado = false;
                foreach (string itmEncu in idsEncuestas)
                {
                    string[] idsDispositivo = hfIdsDispositivos.Value.Remove(hfIdsDispositivos.Value.Length - 1, 1).ToString().Split(',');

                    string[] filtro = idsDispositivo.Distinct().ToArray(); 


                    TDI_EncuestaDispositivo encuDispo = new TDI_EncuestaDispositivo();

                    foreach (string itmDispo in filtro)
                    {
                        
                        List<TDI_EncuestaDispositivo> lst = MngNegocioEncuestaDispositivo.ObtieneEstatusDispoEncu(int.Parse(itmDispo), int.Parse(itmEncu));

                        if (lst.Count == 0)
                        {

                            encuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(itmEncu) };
                            encuDispo.IdEstatus = new TDI_Estatus() { IdEstatus = 2 };
                            encuDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = int.Parse(itmDispo) };


                            bool AsignaEncuDispo = MngNegocioEncuestaDispositivo.AlmacenaDispoEncuesta(encuDispo);
                            if (AsignaEncuDispo)
                            {
                                enviado = true;
                                ViewState["Opcion"] = "Asigna Correcto";
                                GuardaLogTransacc("Se Envio la Encuesta " + encuDispo.IdEncuesta.IdEncuesta + " al Dispositivo " + encuDispo.IdDispositivo.IdDispositivo, 13);
                            }
                        }
                    }

                    if(enviado)
                        GuardaLogTransacc("Encuesta enviada " + encuDispo.IdEncuesta.IdEncuesta, 27);
                }

                if (enviado)
                    ctrlMessageBox.AddMessage("Se Envio Correctamente la Encuesta", MessageBox.enmMessageType.Attention, true, false, "Envia", "Envia Encuesta Dispositivo");
                

                btnEncuChk_Click(null, null);
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void Acepta_Evento(object sender, EventArgs e)
        {
            try
            {
                string Opcion = ViewState["Opcion"].ToString();

                if (Opcion == "Asigna Correcto")
                {                    
                    //CargaDatosCatalogos();
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnEncuChk_Click(object sender, EventArgs e)
        {
            try
            {
                int idEncuestaSel = int.Parse(hfIdEncuestaUnico.Value);

                ObtieneDispoDisponiblesEncuesta(idEncuestaSel);

                hfIdEncuestaUnico.Value = "";
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnBuscaEncu_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (hfTipoFecha.Value == "") return;
                string[] TipoFecha = hfTipoFecha.Value.Remove(hfTipoFecha.Value.Length - 1, 1).ToString().Split(',');
                    
                List<THE_Encuesta> encuBusq = new List<THE_Encuesta>();

                if (txtFechaIni.SelectedDate.ToString() != string.Empty && txtFechaFin.SelectedDate.ToString() != string.Empty)
                {
                    encuBusq =MngNegocioEncuesta.BuscaEncuestaPorNombre(txtBuscaEncuesta.Text.Trim(), Convert.ToDateTime(txtFechaIni.SelectedDate).ToString("dd/MM/yyyy"),
                       Convert.ToDateTime(txtFechaFin.SelectedDate).ToString("dd/MM/yyyy"), TipoFecha[0]);
                    txtBuscaEncuesta.Text = "";   
                }

                else
                {
                    encuBusq =MngNegocioEncuesta.ObtieneTodasEncuestasActivas();
                }

                if (encuBusq.Count > 0)
                {
                    
                    gvDispoEncu.DataSource = encuBusq;
                    Session["datasource"] = encuBusq;
                    gvDispoEncu.DataBind();
                }
                else
                {
                    gvDispoEncu.DataSource = null;
                    gvDispoEncu.EmptyDataText = "No se encontraron encuestas con los Filtros Seleccionados";
                    gvDispoEncu.DataBind();
                }
               
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnSelecTodos_Click(object sender, EventArgs e)
        {

        }
       
        [WebMethod]
        public static string getEstados()
        {           
            List<TDI_Estado> getStates = MngNegocioEstado.ObtieneTodoslosEstados();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(getStates);
            return resultjson;  
      
        }

        [WebMethod]
        public static string getMunicipios(int IdEstado)
        {            
            List<TDI_Municipios> listMuni = MngNegocioMunicipios.ObtieneMunicipiosPorEstado(IdEstado);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(listMuni);
            return resultjson;
        }  
        
        protected void btnBuscaDispo_Click(object sender, EventArgs e)
        {

            string idEstado = HiddenFieldSelect.Value.ToString();
            string idMunicipio = HiddenFieldMuni.Value.ToString();
           
            string[] IdCatalogo;

            if (hfIdCat.Value != "")
            {
                 IdCatalogo = hfIdCat.Value.Remove(hfIdCat.Value.Length - 1, 1).ToString().Split('&');
            }
            else
            {
                IdCatalogo = new string[0];
            }
                       

            List<THE_Dispositivo> BusquedaDispo =MngNegocioDispositivo.BuscaDispositivoFiltros(txtBusUsua.Text, txtBusNumTel.Text, idEstado, idMunicipio, IdCatalogo);
           
            if (BusquedaDispo.Count > 0 && BusquedaDispo.Count != null)
            {
                lvDispositivosEncu.DataSource = null;
                for (int ini = 0; ini < BusquedaDispo.Count; ini++)
                {
                    BusquedaDispo[ini].EstatusCheck = "";
                    BusquedaDispo[ini].ChkEnabled = "";
                    BusquedaDispo[ini].ColorEstatus = "../Images/not.jpg";
                    BusquedaDispo[ini].StrColor = "Rojo";

                    if (BusquedaDispo[ini].ImagenTelefono == null)
                        BusquedaDispo[ini].ImagenTelefono = "../Images/no_foto.jpg";
                    else
                        BusquedaDispo[ini].ImagenTelefono = "../Media/Dispositivos/" + BusquedaDispo[ini].IdDispositivo + "/" + BusquedaDispo[ini].ImagenTelefono;
                }

                lvDispositivosEncu.DataSource = BusquedaDispo;
                lvDispositivosEncu.DataBind();

                ListView1.DataSource = BusquedaDispo;
                ListView1.DataBind();
               
            }
            else
            {
                lvDispositivosEncu.DataSource = null;
                lvDispositivosEncu.DataBind();
                ListView1.DataSource = null;
                ListView1.DataBind();
            }
        }

        void CargaDatosCatalogos()
        {
            try
            {
                List<THE_Catalogo> listCatalogos =MngNegocioCatalogo.ObtieneTodosCatalogos();

                DropDownList drop = new DropDownList();
                Label labl = new Label();

                foreach (THE_Catalogo itemCatalogo in listCatalogos)
                {
                    List<TDI_OpcionCat> listaOpcionesCat =MngNegocioOpcionCat.ObtieneOpcionesPorCatalogo(itemCatalogo.IdCatalogo);

                    drop = new DropDownList();
                    labl = new Label();
                    if (listaOpcionesCat != null)
                    {                        
                        List<TDI_OpcionCat> Opciones = new List<TDI_OpcionCat>();
                        foreach (TDI_OpcionCat opci in listaOpcionesCat)
                        {
                            Opciones.Add(new TDI_OpcionCat() { IdOpcionCat = opci.IdOpcionCat, OpcionCatDesc = opci.OpcionCatDesc});
                            labl.CssClass = "styleCombosCat";
                            labl.Text = itemCatalogo.CatalogoDesc + ":";
                            drop.DataSource = Opciones;
                            drop.CssClass = "combosCatalogos";
                            drop.DataValueField = "IdOpcionCat";
                            drop.DataTextField = "OpcionCatDesc";
                            drop.Attributes.Add("IdCatalogo", itemCatalogo.IdCatalogo.ToString());
                            drop.ID = itemCatalogo.IdCatalogo.ToString() + "" + opci.IdOpcionCat;
                            drop.DataBind();
                            drop.Items.Add(new ListItem("Selecciona Opcion", "0"));
                            drop.SelectedIndex = drop.Items.Count - 1;
                        }

                        
                    }
                    if (drop.DataSource != null)
                    {
                        Table tablas = new Table();
                        tablas.Width = Unit.Percentage(100);
                        TableRow fila = new TableRow();
                        fila.Width = Unit.Percentage(100);
                        TableCell celda1 = new TableCell();
                        celda1.Width = Unit.Percentage(30);
                        TableCell celda2 = new TableCell();
                        celda2.Width = Unit.Percentage(70);

                        celda1.Controls.Add(labl);
                        celda2.Controls.Add(drop);
                        fila.Controls.Add(celda1);
                        fila.Controls.Add(celda2);
                        tablas.Controls.Add(fila);
                        pnlCombos.Controls.Add(tablas);
                    }
                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnReenviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (hfIdsEncuestas.Value == "" || hfIdsDispositivos.Value == "") return;
                string[] idsEncuestas = hfIdsEncuestas.Value.Remove(hfIdsEncuestas.Value.Length - 1, 1).ToString().Split(',');


                foreach (string itmEncu in idsEncuestas)
                {

                    string[] idsDispositivo = hfIdsDispositivos.Value.Remove(hfIdsDispositivos.Value.Length - 1, 1).ToString().Split(',');

                    TDI_EncuestaDispositivo encuDispo = new TDI_EncuestaDispositivo();
                    foreach (string itmDispo in idsDispositivo)
                    {
                        encuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(itmEncu) };
                        encuDispo.IdEstatus = new TDI_Estatus() { IdEstatus = 2 };
                        encuDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = int.Parse(itmDispo) };

                        bool AsignaEncuDispo =MngNegocioEncuestaDispositivo.ActualizaEstatusDispoEncu(encuDispo);
                        if (AsignaEncuDispo)
                        {
                            ctrlMessageBox.AddMessage("Se Reenvio Correctamente la Encuesta", MessageBox.enmMessageType.Attention, true, false, "Envia", "Reenvia Encuesta Dispositivo");
                            ViewState["Opcion"] = "Asigna Correcto";
                            GuardaLogTransacc("Se Reenvio la Encuesta " + encuDispo.IdEncuesta + "al Dispositivo" + encuDispo.IdDispositivo, 14);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
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

        protected void btnGraficaEncuesta_Click(object sender, EventArgs e)
        {
            try
            {
                Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
                string EncEncrypt = _ChyperRijndael.Transmute((hfIdEncuGrafica.Value), Azteca.Utility.Security.enmTransformType.intEncrypt);


                string script = "muestraGrafica('" + EncEncrypt + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", script, true);
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispoEncuesta", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

    }
}
