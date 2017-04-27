using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EncuestasMoviles.Controls;
using Entidades_EncuestasMoviles;
using Telerik.Charting;
using Telerik.Web.UI;
using Telerik.Charting.Styles;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.HtmlControls;
using BLL_EncuestasMoviles;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace EncuestasMoviles.Pages
{
    public partial class frmEncuestas : System.Web.UI.Page

    {
        #region Variables      
        static string[] ArrayPreguntas;
        static string[] ArrayPreguntasRespuestas;
        static int indexPregunta = 0;
        static int indexPreguntaRespuestas = 0;
        static string DivText = "";
        static string RespGraf = "";

        int _ID = 0;
        string DESC = "";
        #endregion

        public frmEncuestas()
        {
            try
            {
                Load += new EventHandler(frmEncuestas_Load);
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void frmEncuestas_Load(object sender, EventArgs e)
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
                
                try
                {
                    if (!IsPostBack)
                    {
                        List<TDI_BaseRespuestas> empl2 = MngNegocioBaseRespuestas.ObtenerRespFrecuentes();

                        TDI_LogPaginas logPaginas = new TDI_LogPaginas();
                        logPaginas.LogFecha = DateTime.Now;
                        logPaginas.LogIp = Session["UserIP"].ToString();
                        logPaginas.LogUrlPagina = Request.RawUrl;
                        logPaginas.EmpleadoLlavePrimaria = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                        MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);

                        hfchkCrea.Value = "radPorFechCrea";
                        this.txtBusqFechIni.SelectedDate = DateTime.Now.AddDays(-7);
                        this.txtBusqFechFin.SelectedDate = DateTime.Now;
                        txtFechaLimEnc.SelectedDate = DateTime.Now;
                        txtHoraLimEnc.SelectedDate = DateTime.Now;
                        CargaGrid();
                        CargaTipoEnc();
                        ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                        txtBusqNombEncu.Text = "";
                        indexPregunta = 0;
                        indexPreguntaRespuestas = 0;
                        ArrayPreguntas = null;
                        ArrayPreguntasRespuestas = null;
                        DivText = "";
                        RespGraf = "";                        
                    }
                    else
                    {                        
                        ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                        RadChart1.Click += new RadChart.ChartClickEventHandler(RadChart1_Click);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ms)
            {

            }
            finally 
            { 
            
            }
        }

        private void CargaTipoEnc()
        {
            List<TDI_TipoEncuesta> tipoEncuesta = MngNegocioTipoEncuesta.ObtieneTodoslosTiposEncuestas();
            ddlTipoEncuesta.DataSource = tipoEncuesta;
            ddlTipoEncuesta.DataTextField = "TipoEncuestaDescripcion";
            ddlTipoEncuesta.DataValueField = "IdTipoEncuesta";
            ddlTipoEncuesta.DataBind();
        }

        protected void Edit(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            {
                try
                {
                    //int id = Convert.ToInt32(row.Cells[0].Text);
                    txtnomEncID.Text = row.Cells[2].Text;
                    fechLimiteEncID.SelectedDate = Convert.ToDateTime(row.Cells[6].Text);
                    horaLimitID.SelectedDate = Convert.ToDateTime(row.Cells[8].Text);
                    txtPuntosEncID.Text = row.Cells[3].Text;
                    cbTipoEncID.SelectedValue = row.Cells[12].Text == String.Empty ? "1" : row.Cells[12].Text;
                    modalIdEnc.Value = row.RowIndex.ToString();
                    modalEditorAdd.Value = "2";
                    headEditAddEnch2.InnerText = ".:: Edita Encuesta ::.";
                    List<TDI_TipoEncuesta> tipoEncuesta = MngNegocioTipoEncuesta.ObtieneTodoslosTiposEncuestas();
                    cbTipoEncID.DataSource = tipoEncuesta;
                    cbTipoEncID.DataTextField = "TipoEncuestaDescripcion";
                    cbTipoEncID.DataValueField = "IdTipoEncuesta";
                    cbTipoEncID.DataBind();
                }catch(Exception ms){
                
                }

                popup.Show();
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            try
            {
                
                if (modalEditorAdd.Value == "1") {
                    THE_Encuesta encu = new THE_Encuesta();
                    encu.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                    encu.NombreEncuesta = txtnomEncID.Text;
                    encu.FechaCreaEncuesta = DateTime.Now;
                    encu.FechaLimiteEncuesta = Convert.ToDateTime(fechLimiteEncID.SelectedDate).ToString("dd/MM/yyyy");
                    encu.EncuestaStat = 'A';
                    encu.IdEstatus = new TDI_Estatus() { IdEstatus = 8 };
                    encu.PuntosEncuesta = System.Convert.ToInt32(txtPuntosEncID.Text);
                    encu.MinimoRequerido = 29; //mínimo establecido por default
                    encu.MaximoEsperado = 0;
                    encu.HoraLimiteEncuesta = Convert.ToDateTime(horaLimitID.SelectedDate).ToString("HH:mm");
                    encu.IdTipoEncuesta = new TDI_TipoEncuesta() { IdTipoEncuesta = int.Parse(cbTipoEncID.SelectedItem.Value) };

                    bool guardaEncuesta = MngNegocioEncuesta.GuardarEncuestas(encu);

                    if (guardaEncuesta)
                    {
                        Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
                        string EncEncrypt = _ChyperRijndael.Transmute((encu.IdEncuesta.ToString()), Azteca.Utility.Security.enmTransformType.intEncrypt);

                        TDI_EncEncrypt enc_encrypt = new TDI_EncEncrypt();
                        enc_encrypt.IdEncrypt = EncEncrypt;
                        enc_encrypt.IdEncuesta = new THE_Encuesta() { IdEncuesta = encu.IdEncuesta };

                        bool guardaIdEncriptada = MngNegocioEncuesta.GuardaIdEncEncriptada(enc_encrypt);

                        ctrlMessageBox.AddMessage("La Encuesta " + encu.NombreEncuesta + " se agregó correctamente.", MessageBox.enmMessageType.Success, "Agregar Encuesta");
                        GuardaLogTransacc("Se Creo una Nueva Encuesta " + encu.IdEncuesta, 7);
                    }
                }
                else if (modalEditorAdd.Value == "2")
                {
                    THE_Encuesta encu = new THE_Encuesta();
                    List<THE_Encuesta> encuEdita = Session["datasource"] as List<THE_Encuesta>;
                    encu = encuEdita[Convert.ToInt32(modalIdEnc.Value)] as THE_Encuesta;
                    encu.NombreEncuesta = txtnomEncID.Text;
                    encu.PuntosEncuesta = System.Convert.ToInt32(txtPuntosEncID.Text);
                    encu.FechaLimiteEncuesta = Convert.ToDateTime(fechLimiteEncID.SelectedDate).ToString("dd/MM/yyyy");
                    encu.MinimoRequerido = System.Convert.ToInt32(((TextBox)gvEncuestas.Rows[Convert.ToInt32(modalIdEnc.Value)].FindControl("txtEditMinReq")).Text);
                    encu.MaximoEsperado = System.Convert.ToInt32(((TextBox)gvEncuestas.Rows[Convert.ToInt32(modalIdEnc.Value)].FindControl("txtEditMaxEsp")).Text);
                    encu.HoraLimiteEncuesta = Convert.ToDateTime(horaLimitID.SelectedDate).ToString("HH:mm");
                    encu.IdTipoEncuesta = new TDI_TipoEncuesta() { IdTipoEncuesta =Convert.ToInt32(cbTipoEncID.SelectedValue) };

                    bool ActualizaEncuesta = MngNegocioEncuesta.ActualizaEncuesta(encu);

                    if (ActualizaEncuesta)
                    {                       
                        ctrlMessageBox.AddMessage("La Encuesta " + encu.NombreEncuesta + " se actualizo correctamente.", MessageBox.enmMessageType.Success, "Actualizar Encuesta");
                        GuardaLogTransacc("Se Actualizo la Encuesta " + encu.IdEncuesta, 8);
                    }
                    else
                    {

                    }
                }
                CargaGrid();
            }catch(Exception ms){
            
            }
            
        }

        protected void Add(object sender, EventArgs e)
        {
            List<TDI_TipoEncuesta> tipoEncuesta = MngNegocioTipoEncuesta.ObtieneTodoslosTiposEncuestas();
            cbTipoEncID.DataSource = tipoEncuesta;
            cbTipoEncID.DataTextField = "TipoEncuestaDescripcion";
            cbTipoEncID.DataValueField = "IdTipoEncuesta";
            cbTipoEncID.DataBind();
            headEditAddEnch2.InnerText = ".:: Agrega Nueva Encuesta ::.";           
            txtnomEncID.Text =string.Empty;
            fechLimiteEncID.SelectedDate = DateTime.Now;
            horaLimitID.SelectedDate = DateTime.Now;
            txtPuntosEncID.Text = string.Empty;           
            modalIdEnc.Value = "";
            modalEditorAdd.Value = "1";
            popup.Show();
        }

        [WebMethod]
        public static List<string> findRespFrecuentes(string prefixText, int count)
        {
            prefixText = prefixText.Trim();
            List<string> filtroResp = new List<string>();

            List<TDI_BaseRespuestas> empl =MngNegocioBaseRespuestas.ObtenerRespFrecuentes();

            var listEmpl = from RESP in empl
                           where RESP.RespuestasDesc.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase)
                           orderby RESP.RespuestasDesc
                           select RESP;

            foreach (var item in listEmpl)
            {
                filtroResp.Add(item.RespuestasDesc);
            }

            return filtroResp;
        }
               

        public void ctrlMessageBox_MsgBoxAnswered(object sender, MessageBox.MsgBoxEventArgs e)
        {
            if (e.Answer == MessageBox.enmAnswer.OK)
            {
                
            }
            else if (e.Answer == MessageBox.enmAnswer.Cancel)
            {
                           
            }
        }

        protected void gvPreguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Elimina")
                {
                    //Muestra Mensaje si se desea Eliminar la Pregunta
                    int val = int.Parse(e.CommandArgument.ToString());
                    int idPregunta = int.Parse(((GridView)sender).DataKeys[val].Value.ToString());

                    ViewState["Opcion"] = "Elimina Pregunta";
                    ViewState["IdPregunta"] = idPregunta;
                    ctrlMessageBox.AddMessage("¿Esta seguro(a) de eliminar la Pregunta?", MessageBox.enmMessageType.Attention, true, true, "Elimina", "Elimina Pregunta");
                }
                
                if (e.CommandName == "AddRespu")
                {
                    //Muestra Ventana de Respuestas                    
                    object idPregunta = (((System.Web.UI.WebControls.GridView)(sender))).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    List<THE_Preguntas> listPreguntas = Session["datasourceP"] as List<THE_Preguntas>;
                   
                    THE_Preguntas pregunta = null;
                    pregunta = MngNegocioPreguntas.ObtienePreguntaPorID(int.Parse(idPregunta.ToString()))[0];
                    
                    if (pregunta != null)
                    {
                        //Carga datos de la pregunta
                        lblRespuPreg.Text = pregunta.PreguntaDesc;
                        lblIdPregunta.Text = pregunta.IdPregunta.ToString();
                        ViewState["IdPregunta"] = pregunta.IdPregunta.ToString();
                        txtRespuesta.Text = string.Empty;

                        //Carga las respuestas de la pregunta
                        CargaRespuestasPorPregunta(pregunta.IdPregunta);
                                               
                        //Obtiene valores para cargar en el ddl de siguientes preguntas
                        var preguntasSig = from Preguntas in listPreguntas
                                            where Preguntas.IdPregunta > pregunta.IdPregunta
                                            orderby Preguntas.IdPregunta
                                            select Preguntas;
                        //Carga ddl
                        ddlRespuestasSig.Enabled = true;                        
                        ddlRespuestasSig.DataSource = preguntasSig;
                        ddlRespuestasSig.DataTextField = "PreguntaDesc";
                        ddlRespuestasSig.DataValueField = "IdPregunta";
                        ddlRespuestasSig.DataBind();
                        ddlRespuestasSig.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---Seleccione Pregunta Siguiente---", "0"));

                        //Valida el tipo de encuesta
                        int idEncuesta = pregunta.IdEncuesta.IdEncuesta;
                        int tipoEncuesta = MngNegocioEncuesta.ObtieneEncuestaPorID(idEncuesta)[0].IdTipoEncuesta.IdTipoEncuesta;
                        if (tipoEncuesta == 2)//Secuencial
                        {
                            if (preguntasSig.Count() == 0)
                            {
                                ddlRespuestasSig.SelectedIndex = 0;                                
                            }
                            else
                            {
                                ddlRespuestasSig.SelectedIndex = 1;
                            }
                            ddlRespuestasSig.Enabled = false;
                        }
                        //Muestra el div
                        mpeEncuestaRespuesta.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void gvEncuestas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Copiar")
                {
                    //Muestra Mensaje de Eliminar Encuesta
                    ViewState["Opcion"] = "Copia Encuesta";
                    ViewState["IDEncuesta"] = (((System.Web.UI.WebControls.GridView)(sender))).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    ctrlMessageBox.AddMessage("¿Esta seguro(a) de Copiar la Encuesta?", MessageBox.enmMessageType.Attention, true, true, "prueba", "Copia Encuesta");
                }

                if (e.CommandName == "Elimina")
                {
                    //Muestra Mensaje de Eliminar Encuesta
                    ViewState["Opcion"] = "Elimina Encuesta";
                    ViewState["IDElimina"] = int.Parse(e.CommandArgument.ToString());
                    ctrlMessageBox.AddMessage("¿Esta seguro(a) de Eliminar la Encuesta?", MessageBox.enmMessageType.Attention, true, true, "prueba", "Elimina Encuesta");
                }

                if (e.CommandName == "Agrega")
                {
                    
                    object valor = (((System.Web.UI.WebControls.GridView)(sender))).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    List<THE_Encuesta> encu =MngNegocioEncuesta.ObtieneEncuestaPorID(int.Parse(valor.ToString()));
                    if (encu[0].IdEstatus.IdEstatus==8)
                    {
                        encu[0].IdEstatus = new TDI_Estatus() { IdEstatus = 7 };
                    }
                    bool actuaEncu =MngNegocioEncuesta.ActualizaEncuesta(encu[0]);
                    //carga grid con preguntas y ddl de tipo de preguntas
                    CargaPreguntasPorEncuesta(encu[0].IdEncuesta);
                    lblEncuesta.Text = encu[0].NombreEncuesta;
                    txtPregunta.Text = "";
                    //Muestra el div para agregar preguntas
                    mpePreguntas.Show();
                }

                if (e.CommandName == "Programacion")
                {
                    //Obtiene id de encuesta seleccionada
                    object valor = ((System.Web.UI.WebControls.GridView)(sender)).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    int idEncuesta = int.Parse(valor.ToString());
                    THE_Encuesta encuesta = MngNegocioEncuesta.ObtieneEncuestaPorID(idEncuesta)[0];
                    CargaGridProgramaciones(idEncuesta);
                    lblEncuesta5.Text = encuesta.NombreEncuesta;
                    lblEncProgramacion.Text = idEncuesta.ToString();

                    //List<TDI_EncuestaDispositivo> lstTipoResp = MngNegocioEncuestaDispositivo.ObtieneDispositivosActivos();

                    //cbDispoToProgramate.DataSource = lstTipoResp;
                    //cbDispoToProgramate.DataTextField = "DescTel"; 
                    //cbDispoToProgramate.DataValueField = "IdDispo";
                    //cbDispoToProgramate.DataBind();



                    mpeProgramaciones.Show();
                }

                if (e.CommandName == "Publicar")
                {

                    object valor = (((System.Web.UI.WebControls.GridView)(sender))).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    List<THE_Encuesta> encu =MngNegocioEncuesta.ObtieneEncuestaPorID(int.Parse(valor.ToString()));
                    int idEncuesta = encu[0].IdEncuesta;
                    encu[0].IdEstatus = new TDI_Estatus() { IdEstatus = 9 };
                    bool publicaEncu =MngNegocioEncuesta.ActualizaEncuesta(encu[0]);

                    if (publicaEncu)
                    {
                        ctrlMessageBox.AddMessage("Se Publico Correctamente la Encuesta", MessageBox.enmMessageType.Success, "Publica Encuesta");
                        GuardaLogTransacc("Se Publico la Encuesta " + idEncuesta, 25);
                    }
                    CargaGrid();
                }

                if (e.CommandName == "Grafica")
                {

                    object valor = (((System.Web.UI.WebControls.GridView)(sender))).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;

                    List<THE_Encuesta> encuGra = MngNegocioEncuesta.ObtieneEncuestaPorID(int.Parse(valor.ToString()));
                    int idEncuesta = encuGra[0].IdEncuesta;
                    //InitRadChart(encuGra[0].IdEncuesta, 0);
                    List<THE_Preguntas> preguntasEncuesta = MngNegocioPreguntas.ObtienePreguntasPorEncuesta(idEncuesta);
                    if (preguntasEncuesta.Count <= 1)//no puede mostrar preview porque la encuesta no tiene preguntas para construir el árbol
                    {
                        ctrlMessageBox.AddMessage("No es posible graficar, la encuesta no tiene preguntas", MessageBox.enmMessageType.Success, "Preview encuesta");
                    }
                    else
                    {
                        lblTitleGraficaEncu.Text = encuGra[0].NombreEncuesta;
                        ViewState["NomEncuesta"] = encuGra[0].NombreEncuesta;
                        ViewState["FechaCreaEnc"] = encuGra[0].FechaCreaEncuesta;

                        //Encriptado
                        Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
                        //string EncEncrypt = _ChyperRijndael.Transmute(hdnIdEncuesta.Value, Azteca.Utility.Security.enmTransformType.intEncrypt);
                        string encEncrypt = _ChyperRijndael.Transmute(idEncuesta.ToString(), Azteca.Utility.Security.enmTransformType.intEncrypt);
                        string script = "muestraGrafica('" + encEncrypt + "')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect", script, true);
                    }                   
                }
                if (e.CommandName == "Preview")
                {
                    object valor = (((System.Web.UI.WebControls.GridView)(sender))).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;

                    hdnIdEncPreview.Value = valor.ToString();
                    //List<THE_Encuesta> encuGra =MngNegocioEncuesta.ObtieneEncuestaPorID(int.Parse(valor.ToString()));
                    //List<THE_ArbolEncuesta> arbolEncuesta = MngNegocioArbolEncuesta.ObtenerArbol(Convert.ToInt32(valor.ToString()));//encuGra[0].IdEncuesta
                    //if (arbolEncuesta.Count <= 1)//no puede mostrar preview porque la encuesta no tiene preguntas para construir el árbol
                    //{
                    //    ctrlMessageBox.AddMessage("La encuesta no tiene preguntas", MessageBox.enmMessageType.Success, "Preview encuesta");
                    //}
                    //else // Muestra el árbol
                    //{
                        
                    //    radPreviewTree.Nodes.Clear();
                    //    RadTreeNode NodoPA = new RadTreeNode(arbolEncuesta[1].Pregunta_Desc, arbolEncuesta[1].ID_Pregunta.ToString());
                    //    NodoPA.Expanded = true;
                    //    radPreviewTree.Nodes.Add(NodoPA);
                    //    ArboRecursivo(arbolEncuesta, arbolEncuesta[1].ID_Pregunta, 0, NodoPA);
                        

                   
                    //    mpePreview.Show();
                    //}
                    //mpePreview.Show();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "generarTree('"+valor.ToString()+"');", true);
                }

                if (e.CommandName == "Reenvio")
                {
                    object valor = ((System.Web.UI.WebControls.GridView)(sender)).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    int idEncuesta = int.Parse(valor.ToString());
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvEncuestas.Rows[index];
                    List<TDI_EncuestaDispositivo> lstTipoResp = MngNegocioEncuestaDispositivo.ObtieneDispositivosPorEncuesta(idEncuesta);

                    listaDispositivos.DataSource = lstTipoResp;
                    listaDispositivos.DataTextField = "DescTel";
                   // listaDispositivos.DataValueField = "IdDispo";
                   
                    listaDispositivos.DataValueField = "IdEnvio";
                    //listaDispositivos.ID = idEncuesta.ToString();
                    listaDispositivos.DataBind();
                    idEncHiden.Value = idEncuesta.ToString();
                    h1TitleReenvio.InnerText = "REENVIO DE ENCUESTA - " + row.Cells[2].Text;
                    
                    ModalReenvioEncuesta.Show();
                }
                if (e.CommandName == "Cancelacion")
                {
                    object valor = ((System.Web.UI.WebControls.GridView)(sender)).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    int idEncuesta = int.Parse(valor.ToString());
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvEncuestas.Rows[index];


                    List<TDI_EncuestaDispositivo> lstTocancel = MngNegocioEncuestaDispositivo.ObtieneDispositivosPorEncuesta(idEncuesta);
                    lstDispoTocancel.DataSource = lstTocancel;
                    lstDispoTocancel.DataTextField = "DescTel";
                    lstDispoTocancel.DataValueField = "IdDispo";
                    lstDispoTocancel.DataBind();
                    idEncHiden.Value = idEncuesta.ToString();
                    ModalCancelaEncuesta.Show();
                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        [WebMethod]
        public static string obtieneArbol(string idEncueta)
        {
            List<THE_ArbolEncuesta> arbolEncuesta = MngNegocioArbolEncuesta.ObtenerArbol(Convert.ToInt32(idEncueta));
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(arbolEncuesta);
            return resultjson; 
        }





        private void CargaGridProgramaciones(int idEncuesta)
        {
            try
            {
                //List<THE_Programacion> lstprogramaciones =MngNegocioProgramacion.ObtieneProgramacionesPorEncuesta(idEncuesta);
                List<THE_Programacion> lstprogramaciones = MngNegocioProgramacion.ObtieneProgramacionesbyEncuesta(idEncuesta.ToString());
                             
                if (lstprogramaciones != null)
                {
                    gvProgramaciones.DataSource = null;
                    gvProgramaciones.DataSource = lstprogramaciones;
                    ViewState["IdEncuesta"] = idEncuesta;                                        
                    gvProgramaciones.DataBind();
                }
               
                //Carga combo de tipo de programaciones de encuesta
                List<TDI_TipoProgramacion> lstTipoProg = MngNegocioTipoProgramacion.ObtieneTodoslosTiposProgramacion();
                cbTipoProgramacion.DataSource = lstTipoProg;
                cbTipoProgramacion.DataTextField = "TipoProgramacionDescripcion";
                cbTipoProgramacion.DataValueField = "IdTipoProgramacion";
                cbTipoProgramacion.DataBind();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        public void ArbolRecursivo(List<THE_ArbolEncuesta> miarbolito, int idPapa, int pregResp, TreeNode nodoPapa)//PREG:0 RESP:1
        {
            try
            {
                if (pregResp == 0)
                {
                    var listchildren = from Preguntas in miarbolito
                                       where Preguntas.ID_Pregunta == idPapa
                                       orderby Preguntas.ID_Respuesta
                                       select Preguntas;
                    foreach (var itemList in listchildren)
                    {
                        TreeNode nodoHijo = new TreeNode(itemList.Respuesta_Desc, itemList.ID_Respuesta.ToString());
                        nodoHijo.SelectAction = TreeNodeSelectAction.Expand;
                        nodoPapa.ChildNodes.Add(nodoHijo);
                        ArbolRecursivo(miarbolito, itemList.ID_Respuesta, 1, nodoHijo);
                    }
                }
                else if (pregResp == 1)
                {
                    var listchildren = from Respuestas in miarbolito
                                       where Respuestas.ID_Respuesta == idPapa
                                       orderby Respuestas.ID_PreguntaAnterior
                                       select Respuestas;
                    _ID = 0;
                    DESC = "";
                    foreach (var itemList in listchildren)
                    {
                        _ID = itemList.ID_PreguntaAnterior;

                    }
                    var getNodo = (from Resp in miarbolito
                                  where Resp.ID_Pregunta == _ID
                                  select Resp).Take(1);
                    foreach (var olist in getNodo)
                    {
                        DESC = olist.Pregunta_Desc;
                    }

                    TreeNode nodoHijo = new TreeNode(DESC, ID.ToString());
                    nodoHijo.SelectAction = TreeNodeSelectAction.None;
                    nodoPapa.ChildNodes.Add(nodoHijo);

                    if (DESC == "FIN DE LA ENCUESTA")
                    {
                        return;
                    }
                    else
                    {
                        ArbolRecursivo(miarbolito, _ID, 0, nodoHijo);
                    }
                }
            }
            catch (Exception msError)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(msError, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        
        }

        public void ArboRecursivo(List<THE_ArbolEncuesta> arbol, int idPadre, int PREG_RESP, RadTreeNode REFNODOPADRE)
        {
            if (PREG_RESP == 0)
            {
                var getChildsInToPre = from Preguntas in arbol
                                       where Preguntas.ID_Pregunta == idPadre
                                       orderby Preguntas.ID_Respuesta
                                       select Preguntas;

                if (getChildsInToPre.Count() > 0)
                {

                    foreach (var children in getChildsInToPre)
                    {

                        RadTreeNode nuevoHijo = new RadTreeNode(children.Respuesta_Desc, children.ID_Respuesta.ToString());
                        nuevoHijo.Expanded = true;
                        REFNODOPADRE.Nodes.Add(nuevoHijo);
                        ArboRecursivo(arbol, children.ID_Respuesta, 1, nuevoHijo);

                    }
                }
                else
                {
                    return;
                }
            }
            else if (PREG_RESP == 1)
            {

                var u = (from A in arbol
                         join B in arbol on A.ID_Pregunta equals B.ID_PreguntaAnterior
                         where B.ID_Respuesta == idPadre
                         orderby B.ID_PreguntaAnterior descending
                         select new { idHijo = B.ID_PreguntaAnterior, descHijo = A.Pregunta_Desc }).Take(1).ToArray();

                if (u.Count() > 0)
                {
                    int idhijo = u[0].idHijo;
                    string deschijo = u[0].descHijo;

                    RadTreeNode nodoHijo = new RadTreeNode(deschijo, idhijo.ToString());
                    nodoHijo.Expanded = true;
                    REFNODOPADRE.Nodes.Add(nodoHijo);

                    if (deschijo.ToUpper().Trim() == "FIN DE LA ENCUESTA")
                    {
                        return;
                    }
                    else
                    {
                        ArboRecursivo(arbol, idhijo, 0, nodoHijo);

                    }
                }
                else
                {

                    return;
                }

            }

        }
      

        void CargaGrid()
        {
            try
            {
                if (hfchkCrea.Value == "") return;
                string[] TipoFecha = hfchkCrea.Value.Remove(hfchkCrea.Value.Length - 1, 1).ToString().Split(',');

                List<THE_Encuesta> BuscaEncu =MngNegocioEncuesta.BuscaEncuestaPreguntasRespuestas(txtBusqNombEncu.Text,Convert.ToDateTime(txtBusqFechIni.SelectedDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtBusqFechFin.SelectedDate).ToString("dd/MM/yyyy"), TipoFecha[0]);

                if (BuscaEncu.Count > 0)
                {
                   
                    gvEncuestas.DataSource = null;
                    gvEncuestas.DataSource = BuscaEncu;
                    Session["datasource"] = BuscaEncu;
                    gvEncuestas.DataBind();
                }
                else
                {
                    gvEncuestas.DataSource = null;
                    gvEncuestas.EmptyDataText = "No se encontraron encuestas con los Filtros Seleccionados";
                    gvEncuestas.DataBind();
                }                
                //DivAltaEncuesta.Style.Add(HtmlTextWriterStyle.Display, "none");
                //DivBotonAlta.Style.Add(HtmlTextWriterStyle.Display, "Block");
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void Acepta_Evento(object sender, EventArgs e)
        {
            try
            {
                string opcion = ViewState["Opcion"].ToString();
                if (opcion == "SesionCaduca") 
                {
                    ViewState["Opcion"] = "";
                    Response.Redirect("~/Default.aspx");
                }

                if (opcion == "Copia Encuesta")
                {                    
                    int idEncuesta = int.Parse(ViewState["IDEncuesta"].ToString());
                    CopiaEncuesta(idEncuesta);                   
                }
            

                if (opcion == "Elimina Pregunta")
                {
                    int idPregunta = int.Parse(ViewState["IdPregunta"].ToString());
                    List<THE_Preguntas> obtPregu =MngNegocioPreguntas.ObtienePreguntaPorID(idPregunta);
                    obtPregu[0].Estatus = 'B';
                    bool eliminaPregunta =MngNegocioPreguntas.EliminaPregunta(obtPregu[0]);

                    if (eliminaPregunta)
                    {
                        ViewState["Opcion"] = "CorrectoEP";
                        CargaPreguntasPorEncuesta(int.Parse(ViewState["IdEncuesta"].ToString()));
                        mpePreguntas.Show();
                        GuardaLogTransacc("Se Elimino la Pregunta " + obtPregu[0].IdPregunta , 17);
                    }
                }

                if (opcion == "Elimina Programacion")
                {
                    int idProgramacion = int.Parse(ViewState["IdProgramacion"].ToString());
                    THE_Programacion editProgramacion = MngNegocioProgramacion.ObtieneProgramacionPorID(idProgramacion);
                    editProgramacion.ProgramacionEstatus = 'B';
                    bool eliminaProg = MngNegocioProgramacion.EliminaProgramacion(editProgramacion);

                    if (eliminaProg)
                    {                        
                        CargaGridProgramaciones(int.Parse(lblEncProgramacion.Text));
                        mpeProgramaciones.Show();
                        GuardaLogTransacc("Se Elimino la Programación " + editProgramacion.IdProgramacion, 35);
                    }
                }

                if (opcion == "Elimina ProgXFecha")
                {
                    int idProgXFecha = int.Parse(ViewState["IdProgXFecha"].ToString());
                    THE_ProgXFecha editProgXFecha = MngNegocioProgXFecha.ObtieneProgXFechaPorIdProgXFecha(idProgXFecha);
                    editProgXFecha.Estatus = 'B';
                    bool eliminaProgXFecha = MngNegocioProgXFecha.EliminaProgXFecha(editProgXFecha);

                    if (eliminaProgXFecha)
                    {
                        CargaProgXFechaPorProg(int.Parse(LblIdProgramacionF.Text));
                        mpeProgramacionDetFecha.Show();
                        GuardaLogTransacc("Se Elimino la Programación por fecha: " + editProgXFecha.IdProgXFecha, 35);
                    }
                }

                if (opcion == "Elimina ProgXSemana")
                {
                    int idProgXSemana = int.Parse(ViewState["IdProgXSemana"].ToString());
                    THE_ProgXSemana editProgXSemana = MngNegocioProgXSemana.ObtieneProgXSemanaPorIdProgXSemana(idProgXSemana);
                    editProgXSemana.Estatus = 'B';

                    bool eliminaProgXSemana = MngNegocioProgXSemana.EliminaProgXSemana(editProgXSemana);
                    if (eliminaProgXSemana)
                    {
                        CargaProgXSemanaPorProg(int.Parse(LblIdProgramacionS.Text));
                        mpeProgramacionDetSemana.Show();
                        GuardaLogTransacc("Se Elimino la Programación por fecha: " + editProgXSemana.IdProgXSemana, 35);
                    }
                }

                if (opcion == "CancelaRespu") 
                {
                    ViewState["Opcion"] = "CancelER";
                    ctrlMessageBox.AddMessage("Respuesta fue cancelada correctamente", MessageBox.enmMessageType.Info, true, false, "Cancelacion", "Cancelacion de respuesta");                 
                }

                if (opcion == "Elimina Respuesta")
                {
                    int idRespuesta = int.Parse(ViewState["IDRespuesta"].ToString());
                    List<THE_Respuestas> obtRespu = MngNegocioRespuestas.ObtieneRespuestaPorId(idRespuesta);
                    obtRespu[0].RespuestaEstatus = 'B';
                    bool eliminaRespuesta =MngNegocioRespuestas.EliminaRespuesta(obtRespu[0]);

                    if (eliminaRespuesta)
                    {
                        ViewState["Opcion"] = "CorrectoER";
                        ctrlMessageBox.AddMessage("Respuesta " + obtRespu[0].IdRespuesta + " eliminada Correctamente", MessageBox.enmMessageType.Success, true, false, "Elimina", "Eliminacion de respuesta");
                        GuardaLogTransacc("Se Elimino la Respuesta " + obtRespu[0].IdRespuesta, 20);
                    }
                }

                if (opcion == "Elimina Encuesta")
                {

                    List<THE_Encuesta> encuesta = Session["datasource"] as List<THE_Encuesta>;
                    THE_Encuesta encu = encuesta[int.Parse(ViewState["IDElimina"].ToString())] as THE_Encuesta;
                    encu.EncuestaStat = 'B';

                    bool eliminaEncuesta =MngNegocioEncuesta.EliminaEncuestas(encuesta[int.Parse(ViewState["IDElimina"].ToString())]);

                    if (eliminaEncuesta)
                    {
                        ctrlMessageBox.AddMessage("Encuesta eliminada Correctamente", MessageBox.enmMessageType.Success, true, false, "Elimina", "Eliminacion de Encuesta");
                       
                        ViewState["Opcion"] = "CorrectoEn";
                        GuardaLogTransacc("Se Elimino Encuesta " + encu.IdEncuesta, 9);
                       
                    }
                }

                if (opcion == "CorrectoEP")
                {
                    CargaPreguntasPorEncuesta(int.Parse(ViewState["IdEncuesta"].ToString()));
                    mpePreguntas.Show();
                }
                else if (opcion == "CorrectoER")
                {
                    CargaRespuestasPorPregunta(int.Parse(lblIdPregunta.Text.ToString()));
                    mpeEncuestaRespuesta.Show();
                }
                else if (opcion == "CorrectoEn")
                {
                    CargaGrid();
                    ViewState["Opcion"] = "";
                }
                else if (opcion == "CancelER")
                {
                    mpeEncuestaRespuesta.Show();
                    ViewState["Opcion"] = "";
                }
                else if (opcion == "CorrectoRespuesta")
                {
                    
                
                }
                else if (opcion == "CorrectoReenvioEnc")
                {
                    ViewState["Opcion"] = "";
                }
                else if (opcion=="CorrectoCancelEnc")
                {
                    ViewState["Opcion"] = "";
                }
                else if (opcion == "AbreReenvio")
                {
                    ViewState["Opcion"] = "";
                    ModalReenvioEncuesta.Show();
                }
               
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        private void CopiaEncuesta(int idEncuesta)
        {
            try
            {                
                List<THE_Encuesta> obtEncuesta = MngNegocioEncuesta.ObtieneEncuestaPorID(idEncuesta);
                //Encuesta a copiar
                THE_Encuesta encuesta = obtEncuesta[0];
                encuesta.NombreEncuesta += " Copia";
                encuesta.EncuestaStat = 'A';
                encuesta.IdEstatus = new TDI_Estatus() { IdEstatus = 8 };                
                //Inserta la copia de la encuesta
                bool guardaCopEnc = MngNegocioEncuesta.GuardarEncuestas(encuesta);
                if (guardaCopEnc)
                {
                    GuardaLogTransacc("Se Copio la Encuesta " + idEncuesta, 32);
                    //idEncuesta Copiada                        
                    int idEncCopia = encuesta.IdEncuesta;

                    //Preguntas a copiar
                    List<THE_Preguntas> obtPreguntas = MngNegocioPreguntas.ObtienePreguntasPorEncuesta(idEncuesta);
                    if (obtPreguntas.Count > 0)
                    {
                        //Guarda preguntas
                        //Barre las preguntas de la encuesta a copiar
                        foreach (THE_Preguntas pregunta in obtPreguntas)
                        {
                            //Crea copia de pregunta a insertar
                            THE_Preguntas pregCopia = new THE_Preguntas();
                            pregCopia.IdEncuesta = new THE_Encuesta() { IdEncuesta = idEncCopia };
                            pregCopia.PreguntaDesc = pregunta.PreguntaDesc;
                            pregCopia.FechaCrea = DateTime.Now;
                            pregCopia.Estatus = pregunta.Estatus;                            
                            pregCopia.IdTipoResp = new THE_Tipo_Respuestas() { IdTipoResp=pregunta.IdTipoResp==null?1:pregunta.IdTipoResp.IdTipoResp };
                            pregCopia.IdPreAleatoria = new TDI_TieneRespAleatorias() { IdPreAleatoria = pregunta.IdPreAleatoria==null?1:pregunta.IdPreAleatoria.IdPreAleatoria };
                            //Guarda la copia de la pregunta
                            MngNegocioPreguntas.GuardaPreguntaPorEncuesta(pregCopia);                            
                        }
                        
                        //Preguntas copiadas
                        List<THE_Preguntas> obtPregCopiadas = MngNegocioPreguntas.ObtienePreguntasPorEncuesta(idEncCopia);

                        if (obtPreguntas.Count == obtPregCopiadas.Count)
                        {
                            foreach (THE_Preguntas pregOrig in obtPreguntas)
                            {
                                //idPregunta Original
                                int idPregOriginal = pregOrig.IdPregunta;

                                //Asigna el valor de idPregunta  Copia
                                var q = from PregCop in obtPregCopiadas where PregCop.PreguntaDesc == pregOrig.PreguntaDesc select PregCop.IdPregunta;

                                int idPregCopia=0;
                                foreach (int e in q)
                                    idPregCopia = e;

                                //Por pregunta obtiene Respuestas a copiar
                                List<THE_Respuestas> obtRespuestas = MngNegocioRespuestas.ObtenerRespuestasPorPregunta(idPregOriginal);
                                if (obtRespuestas.Count > 0)
                                {
                                    foreach (THE_Respuestas respuesta in obtRespuestas)
                                    {                                        
                                        //Crea copia de respuesta a insertar
                                        THE_Respuestas respCopia = new THE_Respuestas();
                                        respCopia.RespuestaDescripcion = respuesta.RespuestaDescripcion;
                                        respCopia.RespuestaEstatus = respuesta.RespuestaEstatus;
                                        respCopia.IdPregunta = new THE_Preguntas() { IdPregunta = idPregCopia };                                        
                                        //Obtiene IdSiguiente Pregunta y Descripción de la siguiente pregunta                                        
                                        respCopia.IdSiguientePregunta = idPregCopia + (respuesta.IdSiguientePregunta - respuesta.IdPregunta.IdPregunta);
                                        //Guarda la pregunta copia
                                        MngNegocioRespuestas.GuardaRespuesta(respCopia);
                                    }
                                }                                
                            }
                        }
                    }                    
                }
                CargaGrid();                  
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void CargaDatos(int IdEncuesta)
        {
            try
            {
                List<THE_Preguntas> lstpreguntas =MngNegocioPreguntas.ObtienePreguntasPorEncuesta(IdEncuesta);
                
                if (lstpreguntas != null)
                {
                    gvPreguntas.DataSource = null;
                    gvPreguntas.DataSource = lstpreguntas;
                    ViewState["IdEncuesta"] = IdEncuesta;
                    Session["datasourceP"] = lstpreguntas;
                    gvPreguntas.DataBind();
                    int c = 0;
                    foreach (GridViewRow item in gvPreguntas.Rows)
                    {                        
                        DropDownList ddl = item.FindControl("cbEditTipoResp") as DropDownList;
                        DropDownList ddl2 = item.FindControl("cbEditRespAlea") as DropDownList;

                        List<THE_Tipo_Respuestas> lstTipoResp = MngNegocioTipoRespuestas.ObtieneTipoRespuestas();
                        List<TDI_TieneRespAleatorias> lstTipoRespAlea = MngNegocioTieneRespAleatorias.ObtieneRespAleatorias();

                       
                            ddl.DataSource = lstTipoResp;
                            ddl.DataTextField = "DescTipoResp";
                            ddl.DataValueField = "IdTipoResp";
                            ddl.SelectedValue = lstpreguntas[c].IdResp.ToString();
                            ddl.DataBind();

                            ddl2.DataSource = lstTipoRespAlea;
                            ddl2.DataTextField = "DescTieneRespAlea";
                            ddl2.DataValueField = "IdPreAleatoria";
                            ddl2.SelectedValue = lstpreguntas[c].IdRespAleatorias.ToString();
                            ddl2.DataBind();

                            c++;
                        
                    }

                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }        

        protected void btnCancelAEPreg_Click(object sender, EventArgs e)
        {
            try
            {
                mpePreguntas.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void CargaRespuestasPorPregunta(int IdPregunta)
        {
            try
            {
                List<THE_Respuestas> lstRespuestasGral = MngNegocioRespuestas.ObtenerRespuestasPorPregunta(IdPregunta);
                gvRespuestasGral.DataSource = null;
                gvRespuestasGral.DataSource = lstRespuestasGral;
                gvRespuestasGral.DataBind();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }
 
        void CargaPreguntasPorEncuesta(int IdEncuesta)
        {
            try
            {
                List<THE_Preguntas> lstpreguntas =MngNegocioPreguntas.ObtienePreguntasPorEncuesta(IdEncuesta);
                              
                if (lstpreguntas != null)
                {
                    gvPreguntas.DataSource = null;
                    gvPreguntas.DataSource = lstpreguntas;
                    ViewState["IdEncuesta"] = IdEncuesta;
                    Session["datasourceP"] = lstpreguntas;
                    
                    gvPreguntas.DataBind();
                }
               
                //Carga combo de tipo de respuestas que tendra la pregunta.
                List<THE_Tipo_Respuestas> lstTipoResp= MngNegocioTipoRespuestas.ObtieneTipoRespuestas();
                cbTipoPregunta.DataSource = lstTipoResp;
                cbTipoPregunta.DataTextField = "DescTipoResp";
                cbTipoPregunta.DataValueField = "IdTipoResp";
                cbTipoPregunta.DataBind();


                List<TDI_TieneRespAleatorias> lstRespalea = MngNegocioTieneRespAleatorias.ObtieneRespAleatorias();
                cbRestriccionResp.DataSource = lstRespalea;
                cbRestriccionResp.DataTextField = "DescTieneRespAlea";
                cbRestriccionResp.DataValueField = "IdPreAleatoria";
                cbRestriccionResp.DataBind();







            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnCerrarRespuestas_Click(object sender, EventArgs e)
        {
            try
            {
                mpePreguntas.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnCierraRespuEA_Click(object sender, EventArgs e)
        {
            try
            {
                mpeEncuestaRespuesta.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            CargaGrid();
        }

        protected void btnCancelRespuEA_Click(object sender, EventArgs e)
        {
            try
            {
                mpeEncuestaRespuesta.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        public int SelectIndexRespuesta(int itemRespuesta)
        {
            int index = -1;
            try
            {
                THE_Preguntas oItem = new THE_Preguntas();
                foreach (THE_Preguntas itmPreg in ((List<Entidades_EncuestasMoviles.THE_Preguntas>)Session["datasourceP"]))
                {
                    index += 1;
                    if (itmPreg.IdPregunta == itemRespuesta)
                    {
                        oItem = itmPreg;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
            return index;
        }

        protected void btnCancelarModalPreguntas_Click(object sender, EventArgs e)
        {
            try
            {
                mpePreguntas.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnCerrarPre_Click(object sender, EventArgs e)
        {
            mpePreguntas.Show();
        }

        protected void btnBuscaEncu_Click(object sender, EventArgs e)
        {
            CargaGrid();
        }

        protected void btnReenvioEnc_Click(object sender, EventArgs e)
        {
            try
            {
                var collection = listaDispositivos.CheckedItems;

                if (collection.Count != 0)
                {
                    foreach (var item in collection)
                    {                  
                        int idEnvio = Convert.ToInt32(item.Value);
                        List<TDI_EncuestaDispositivo> EncuDispoCancel = MngNegocioEncuestaDispositivo.ObtieneDispositivosPorIdEnvio(idEnvio);

                        if (EncuDispoCancel.Count > 0)
                        {
                            EncuDispoCancel[0].IdEstatus = new TDI_Estatus() { IdEstatus = 10 };

                            if (MngNegocioEncuestaDispositivo.ActualizaEstatusDispoEncu(EncuDispoCancel[0]))
                            {
                                TDI_EncuestaDispositivo EncuDispoInsert = new TDI_EncuestaDispositivo();
                                EncuDispoInsert.IdDispositivo = new THE_Dispositivo() { IdDispositivo = Convert.ToInt32(EncuDispoCancel[0].IdDispo) };
                                EncuDispoInsert.IdEncuesta = new THE_Encuesta() { IdEncuesta = Convert.ToInt32(EncuDispoCancel[0].IdEncuesta.IdEncuesta) };
                                EncuDispoInsert.IdEstatus = new TDI_Estatus() { IdEstatus = 2 };

                                if (MngNegocioEncuestaDispositivo.InsertNewDispoEncuesta(EncuDispoInsert))
                                {
                                    ViewState["Opcion"] = "CorrectoReenvioEnc";
                                    ctrlMessageBox.AddMessage("El reenvio de la encuesta se efectuo correctamente", MessageBox.enmMessageType.Info, true, false, "Cancelacion", "Cancelacion de respuesta");

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ms) { 
            
            }
        }

        protected void btnCancelEnc_Click(object sender, EventArgs e)
        {
            try
            {
                var collection = lstDispoTocancel.CheckedItems;

                if (collection.Count != 0)
                {
                    foreach (var item in collection)
                    {
                        int idEnvio = Convert.ToInt32(item.Value);
                        List<TDI_EncuestaDispositivo> EncuDispoCancel = MngNegocioEncuestaDispositivo.ObtieneDispositivosPorIdEnvio(idEnvio);
                        EncuDispoCancel[0].IdEstatus = new TDI_Estatus() { IdEstatus = 10 };

                        if (MngNegocioEncuestaDispositivo.ActualizaEstatusDispoEncu(EncuDispoCancel[0]))
                        {
                            ViewState["Opcion"] = "CorrectoCancelEnc";
                            ctrlMessageBox.AddMessage("La cancelacion de la encuesta se efectuo correctamente", MessageBox.enmMessageType.Info, true, false, "Cancelacion", "Cancelacion de respuesta");
                        }
                    }
                }
            }
            catch (Exception ms) { 
            
            }
        }

        protected void gvEncuestas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ViewState["EditIndex"] = e.NewEditIndex;
            gvEncuestas.EditIndex = e.NewEditIndex;
            
            gvEncuestas.Columns[0].Visible = true;
            gvEncuestas.Columns[1].Visible = true;
            gvEncuestas.Columns[2].Visible = false;
            gvEncuestas.Columns[3].Visible = false;
            gvEncuestas.Columns[4].Visible = true;
            gvEncuestas.Columns[5].Visible = true;
            gvEncuestas.Columns[6].Visible = false;
            gvEncuestas.Columns[7].Visible = true;
            gvEncuestas.Columns[8].Visible = false;
            gvEncuestas.Columns[9].Visible = true;
            gvEncuestas.Columns[10].Visible = false;
            gvEncuestas.Columns[11].Visible = false;
            gvEncuestas.Columns[12].Visible = false;
            gvEncuestas.Columns[13].Visible = false;
            gvEncuestas.Columns[14].Visible = false;
            gvEncuestas.Columns[15].Visible = true;

            CargaGrid();
            for (int ini = 0; ini < gvEncuestas.Rows.Count; ini++)
            {
                if (ini != e.NewEditIndex)
                    gvEncuestas.Rows[ini].Enabled = false;
            }
        }

        protected void gvEncuestas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEncuestas.EditIndex = -1;
            MuestraBoundFieldGV();
            CargaGrid();
        }

        private void MuestraBoundFieldGV()
        {
            gvEncuestas.Columns[0].Visible = false;
            gvEncuestas.Columns[1].Visible = false;
            gvEncuestas.Columns[2].Visible = true;
            gvEncuestas.Columns[3].Visible = true;
            gvEncuestas.Columns[4].Visible = true;
            gvEncuestas.Columns[5].Visible = false;
            gvEncuestas.Columns[6].Visible = true;
            gvEncuestas.Columns[7].Visible = false;
            gvEncuestas.Columns[8].Visible = true;
            gvEncuestas.Columns[9].Visible = true;
            gvEncuestas.Columns[10].Visible = false;
            gvEncuestas.Columns[11].Visible = false;            
            gvEncuestas.Columns[12].Visible = false;
            gvEncuestas.Columns[13].Visible = false;
            gvEncuestas.Columns[14].Visible = true;
            gvEncuestas.Columns[15].Visible = false;
        }

        protected void gvEncuestas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                THE_Encuesta encu = new THE_Encuesta();
                List<THE_Encuesta> encuEdita = Session["datasource"] as List<THE_Encuesta>;
                encu = encuEdita[e.RowIndex] as THE_Encuesta;
                encu.NombreEncuesta = ((TextBox)gvEncuestas.Rows[e.RowIndex].FindControl("txtNombreEncuestaGv")).Text;
                encu.PuntosEncuesta = System.Convert.ToInt32(((TextBox)gvEncuestas.Rows[e.RowIndex].FindControl("txtPointsEncu")).Text);
                encu.FechaLimiteEncuesta =Convert.ToDateTime(((RadDatePicker)gvEncuestas.Rows[e.RowIndex].FindControl("txtFechaLimiteGV")).SelectedDate).ToString("dd/MM/yyyy");
                encu.MinimoRequerido = System.Convert.ToInt32(((TextBox)gvEncuestas.Rows[e.RowIndex].FindControl("txtEditMinReq")).Text);
                encu.MaximoEsperado = System.Convert.ToInt32(((TextBox)gvEncuestas.Rows[e.RowIndex].FindControl("txtEditMaxEsp")).Text);
                encu.HoraLimiteEncuesta = Convert.ToDateTime(((RadTimePicker)gvEncuestas.Rows[e.RowIndex].FindControl("txtHoraLimiteGV")).SelectedDate).ToString("HH:mm");
                //encu.IdTipoEncuesta=new TDI_TipoEncuesta(){IdTipoEncuesta=}

                bool ActualizaEncuesta =MngNegocioEncuesta.ActualizaEncuesta(encu);

                if (ActualizaEncuesta)
                {
                    MuestraBoundFieldGV();
                    gvEncuestas.EditIndex = -1;
                    CargaGrid();
                    ctrlMessageBox.AddMessage("La Encuesta " + encu.NombreEncuesta + " se actualizo correctamente.", MessageBox.enmMessageType.Success, "Actualizar Encuesta");
                    GuardaLogTransacc("Se Actualizo la Encuesta " + encu.IdEncuesta, 8);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnGuardaNewEncuesta_Click(object sender, EventArgs e)
        {
            THE_Encuesta encu = new THE_Encuesta();
            encu.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
            encu.NombreEncuesta = txtNomEncuesta.Text;
            encu.FechaCreaEncuesta = DateTime.Now;
            encu.FechaLimiteEncuesta = Convert.ToDateTime(txtFechaLimEnc.SelectedDate).ToString("dd/MM/yyyy");
            encu.EncuestaStat = 'A';
            encu.IdEstatus = new TDI_Estatus() { IdEstatus = 8 };
            encu.PuntosEncuesta =System.Convert.ToInt32(txtPuntosEncuesta.Text);
            encu.MinimoRequerido = 29; //mínimo establecido por default
            encu.MaximoEsperado = 0;
            encu.HoraLimiteEncuesta= Convert.ToDateTime(txtHoraLimEnc.SelectedDate).ToString("HH:mm");
            encu.IdTipoEncuesta = new TDI_TipoEncuesta() { IdTipoEncuesta = int.Parse(ddlTipoEncuesta.SelectedItem.Value) };

            bool guardaEncuesta = MngNegocioEncuesta.GuardarEncuestas(encu);

            if (guardaEncuesta)
            {
                CargaGrid();
                ctrlMessageBox.AddMessage("La Encuesta " + encu.NombreEncuesta + " se agregó correctamente.", MessageBox.enmMessageType.Success, "Agregar Encuesta");
                txtNomEncuesta.Text = string.Empty;
                txtPuntosEncuesta.Text = string.Empty;
                //txtMinRequ.Text =string.Empty;
               // txtMaxEsp.Text = string.Empty;
                txtFechaLimEnc.SelectedDate = DateTime.Now;
               // DivAltaEncuesta.Style.Add(HtmlTextWriterStyle.Display, "none");
               // DivBotonAlta.Style.Add(HtmlTextWriterStyle.Display, "Block");
                GuardaLogTransacc("Se Creo una Nueva Encuesta " + encu.IdEncuesta, 7);
            }
        }

        protected void btnCancelaNew_Click(object sender, EventArgs e)
        {
            txtNomEncuesta.Text = string.Empty;
            txtFechaLimEnc.SelectedDate = DateTime.Now;
        }

        protected void gvEncuestas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEncuestas.PageIndex = e.NewPageIndex;
            CargaGrid();
            //DivAltaEncuesta.Style.Add(HtmlTextWriterStyle.Display, "none");
            //DivBotonAlta.Style.Add(HtmlTextWriterStyle.Display, "Block");
        }

        protected void btnGuardaPregunta_Click(object sender, EventArgs e)
        {                      
            string idEncuesta = ViewState["IdEncuesta"].ToString();
            THE_Preguntas pregGuarda = new THE_Preguntas();
            pregGuarda.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(idEncuesta) };
            pregGuarda.PreguntaDesc = txtPregunta.Text;
            pregGuarda.FechaCrea = DateTime.Now;
            pregGuarda.Estatus = 'A';
            pregGuarda.IdTipoResp = new THE_Tipo_Respuestas() { IdTipoResp = System.Convert.ToInt32(cbTipoPregunta.SelectedValue.ToString()) };

            pregGuarda.IdPreAleatoria = new TDI_TieneRespAleatorias {  IdPreAleatoria = System.Convert.ToInt32(cbRestriccionResp.SelectedValue.ToString()) };

            bool guardaPregunta =MngNegocioPreguntas.GuardaPreguntaPorEncuesta(pregGuarda);
            if (guardaPregunta)
            {
                GuardaLogTransacc("Se Creo la Pregunta " + pregGuarda.IdPregunta, 15);
                ViewState["Opcion"] = "CorrectoRespuesta";
            }
            ctrlMessageBox.AddMessage("Respuesta Agregada Correctamente", MessageBox.enmMessageType.Success, true, false, "Nueva Respuesta", "Nueva Respuesta");
            CargaDatos(int.Parse(idEncuesta));
            txtPregunta.Text = "";
            mpePreguntas.Show();
        }

        protected void gvPreguntas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPreguntas.EditIndex = e.NewEditIndex;
            gvPreguntas.Columns[0].Visible = true;
            gvPreguntas.Columns[1].Visible = false;

            gvPreguntas.Columns[2].Visible = false;
            gvPreguntas.Columns[3].Visible = true;

            gvPreguntas.Columns[4].Visible = false;
            gvPreguntas.Columns[5].Visible = true;


            gvPreguntas.Columns[7].Visible = false;
            gvPreguntas.Columns[8].Visible = true;
           // string val = gvPreguntas.Rows[gvPreguntas.EditIndex].Cells[7].Text;
           // THE_Preguntas Pregunta = new THE_Preguntas();
           // Pregunta = (THE_Preguntas)gvPreguntas.SelectedRow.DataItem;

           // string val=((TextBox)gvPreguntas.Rows[gvPreguntas.EditIndex].FindControl("txtTipoRespEdita")).Text;
            ViewState["tipoResp"] = gvPreguntas.EditIndex;
            CargaDatos(int.Parse(ViewState["IdEncuesta"].ToString()));
            for (int ini = 0; ini < gvPreguntas.Rows.Count; ini++)
            {
                if (ini != e.NewEditIndex)
                    gvPreguntas.Rows[ini].Enabled = false;
            }          
            mpePreguntas.Show();
        }

        protected void gvPreguntas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            OcultaColumnasGV();
            CargaDatos(int.Parse(ViewState["IdEncuesta"].ToString()));
            mpePreguntas.Show();
        }

        protected void gvRespuestasGral_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            OcultaColumnasRespu();
            CargaRespuestasPorPregunta(int.Parse(ViewState["IdPregunta"].ToString()));
            mpeEncuestaRespuesta.Show();
        }

        private void OcultaColumnasGV()
        {
            gvPreguntas.EditIndex = -1;
          
            gvPreguntas.Columns[0].Visible = false;
            gvPreguntas.Columns[1].Visible = true;

            gvPreguntas.Columns[2].Visible = true;
            gvPreguntas.Columns[3].Visible = false;

            gvPreguntas.Columns[4].Visible = true;
            gvPreguntas.Columns[5].Visible = false;

            gvPreguntas.Columns[7].Visible = true;
            gvPreguntas.Columns[8].Visible = false;
        }

        private void OcultaColumnasGVProgramaciones()
        {
            gvProgramaciones.EditIndex = -1;

            gvProgramaciones.Columns[0].Visible = false;
            gvProgramaciones.Columns[1].Visible = true;
            gvProgramaciones.Columns[2].Visible = true;
            gvProgramaciones.Columns[3].Visible = true;
            gvProgramaciones.Columns[4].Visible = true;
            gvProgramaciones.Columns[5].Visible = false;            
        }

        private void OcultaColumnasRespu()
        {
            gvRespuestasGral.EditIndex = -1;
            gvRespuestasGral.Columns[0].Visible = true;
            gvRespuestasGral.Columns[1].Visible = false;
            gvRespuestasGral.Columns[2].Visible = true;
            gvRespuestasGral.Columns[3].Visible = false;
            gvRespuestasGral.Columns[4].Visible = true;
            gvRespuestasGral.Columns[5].Visible = false;
        }

        protected void gvPreguntas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            THE_Preguntas preg = new THE_Preguntas();
            List<THE_Preguntas> usuario = Session["datasourceP"] as List<THE_Preguntas>;
            preg = usuario[e.RowIndex] as THE_Preguntas;
            preg.PreguntaDesc = ((TextBox)gvPreguntas.Rows[e.RowIndex].FindControl("txtDescPreguntaGV")).Text;
            string resp = ((DropDownList)gvPreguntas.Rows[e.RowIndex].FindControl("cbEditTipoResp")).SelectedValue;
            string respAletoria = ((DropDownList)gvPreguntas.Rows[e.RowIndex].FindControl("cbEditRespAlea")).SelectedValue;
            preg.IdTipoResp = new THE_Tipo_Respuestas() { IdTipoResp = System.Convert.ToInt32(resp) };
            preg.IdPreAleatoria = new TDI_TieneRespAleatorias() { IdPreAleatoria = System.Convert.ToInt32(respAletoria) };

            bool actuPregunta =MngNegocioPreguntas.ActualizaPregunta(preg);
            if (actuPregunta)
            {
                GuardaLogTransacc("Se Actualizo la Pregunta " + preg.IdPregunta, 16);
            }
            OcultaColumnasGV();
            CargaDatos(int.Parse(ViewState["IdEncuesta"].ToString()));
            mpePreguntas.Show();
        }

        protected void btnGuardaRespuesta_Click(object sender, EventArgs e)
        {
            THE_Respuestas respu = new THE_Respuestas();
            respu.RespuestaDescripcion = txtRespuesta.Text;
            respu.IdPregunta = new THE_Preguntas() { IdPregunta = int.Parse(ViewState["IdPregunta"].ToString()) };
            
            if (ddlRespuestasSig.SelectedIndex == 0)
            {
                ctrlMessageBox.AddMessage("Esta Pregunta Dara Fin a la Encuesta", MessageBox.enmMessageType.Info, "Pregunta Fin de Encuesta");
               

                int idPreg =MngNegocioPreguntas.ObtienePreguntaFinEncuesta(int.Parse(ViewState["IdEncuesta"].ToString()));
                if (idPreg != -1)
                {
                    respu.IdSiguientePregunta = idPreg;
                    respu.DescSigPreg = ddlRespuestasSig.DataTextField.ToString();
                }
                else
                {
                    return;
                }
            }
            else
            {
                respu.IdSiguientePregunta = int.Parse(ddlRespuestasSig.SelectedValue.ToString());
                respu.DescSigPreg = ddlRespuestasSig.SelectedItem.Text;
            }
                        
           
            respu.RespuestaEstatus = 'A';
            bool guardaRespuesta =MngNegocioRespuestas.GuardaRespuesta(respu);
            txtRespuesta.Text = "";

            if (guardaRespuesta)
            {
                CargaRespuestasPorPregunta(int.Parse(ViewState["IdPregunta"].ToString()));
                mpeEncuestaRespuesta.Show();
                GuardaLogTransacc("Se Creo la Respuesta " + respu.IdRespuesta, 18);
            }
        }

        protected void gvRespuestasGral_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Elimina")
                {
                    int val = int.Parse(e.CommandArgument.ToString());
                    int idRespuesta = int.Parse(((GridView)sender).DataKeys[val].Value.ToString());

                    ViewState["Opcion"] = "Elimina Respuesta";
                    ViewState["IDRespuesta"] = idRespuesta;
                    ctrlMessageBox.AddMessage("¿Esta seguro(a) de Eliminar la Respuesta?", MessageBox.enmMessageType.Attention, true, true, "prueba", "Elimina Respuestas");
                }

                if (e.CommandName == "Editar")
                {
                    int val = int.Parse(e.CommandArgument.ToString());
                    int idRespuesta = int.Parse(((GridView)sender).DataKeys[val].Value.ToString());
                    ViewState["IDRespuesta"] = idRespuesta;
                    List<THE_Respuestas> edtRespuesta = MngNegocioRespuestas.ObtieneRespuestaPorId(idRespuesta);

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnCancelGraficaEncu_Click(object sender, EventArgs e)
        {
            mpeGraficaEncuesta.Hide();
        }

        protected void exportPDF_Click(object sender, EventArgs e)
        {
            //Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            //string EncEncrypt = _ChyperRijndael.Transmute(hdnIdEncuesta.Value, Azteca.Utility.Security.enmTransformType.intEncrypt);

            //Response.Redirect("ExportaPDF.aspx?EncId=" + hdnIdEncuesta.Value);

            //#region Coment
            //Document pdfDoc = new Document();
            //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            //Phrase phrase = new iTextSharp.text.Phrase("Encuesta: ", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 12));
            //HeaderFooter header = new HeaderFooter(phrase, false);
            //pdfDoc.Header = header;

            ////Crear Tabla
            //iTextSharp.text.Table table = new iTextSharp.text.Table(RadChart1.Chart.Series.Count);
            //table.Cellpadding = 3;
            //table.Width = 100;
            
            //HeaderFooter footer = new HeaderFooter(new Phrase("Pagina: "), true);
            //footer.Alignment = Element.ALIGN_CENTER;
            //footer.Border = iTextSharp.text.Rectangle.ALIGN_CENTER;
            //pdfDoc.Footer = footer;

            //iTextSharp.text.Table tableheader = new iTextSharp.text.Table(2, 1);
            //tableheader.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //tableheader.Width = 100;
            //tableheader.SetWidths(new int[2] { 1, 3 });
            //iTextSharp.text.Cell cellheader = new iTextSharp.text.Cell();
            //cellheader.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //cellheader.HorizontalAlignment = Element.ALIGN_CENTER;
            //cellheader.VerticalAlignment = Element.ALIGN_CENTER;
            //string map = Server.MapPath("~/Images/Header.png");
            //iTextSharp.text.Image VMgif = iTextSharp.text.Image.GetInstance(map);
            //VMgif.ScalePercent(50f);
            //VMgif.SpacingAfter = 10f;
            //VMgif.SpacingBefore = 10f;
            //cellheader.Add(VMgif);
            //tableheader.AddCell(cellheader);
            //cellheader = new Cell();
           
            //cellheader.HorizontalAlignment = Element.ALIGN_TOP;
            //cellheader.VerticalAlignment = Element.ALIGN_LEFT;
            //tableheader.AddCell(cellheader);
            //tableheader.Spacing = 15;



            //pdfDoc.Open();


            
            //try
            //{
            //    iTextSharp.text.Table tableimg = new iTextSharp.text.Table(1, 1);
            //    tableimg.Spacing = 50;
            //    tableimg.Border = iTextSharp.text.Rectangle.ALIGN_CENTER;
            //    tableimg.Width = 100;
            //    iTextSharp.text.Cell cellimg = new Cell();
            //    cellimg.HorizontalAlignment = Element.ALIGN_BOTTOM;
            //    cellimg.VerticalAlignment = Element.ALIGN_CENTER;

               
            //    RadChart rads = RadChart1;
            //    var imgStream = new MemoryStream();
            //    rads.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
            //    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgStream.ToArray());

            //    string imgMap = Server.MapPath("~/Images/encuestaMovilLogo.png");
            //    iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(img);
            //    gif.ScalePercent(100f);
            //    cellimg.Add(gif);
            //    tableimg.AddCell(cellimg);
            //    pdfDoc.Add(tableheader);
            //    pdfDoc.Add(tableimg);
            //    pdfDoc.NewPage();
            //    pdfDoc.Add(table);

            //    pdfDoc.Close();
            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("content-disposition", "attachment;" + "filename=Monitoreo.pdf");
            //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //    Response.Write(pdfDoc);
            //    Response.End();
            //}
            //catch (Exception ex)
            //{
            //}

            //#endregion Coment
        }

        void InitRadChart(int idEncuesta, int idSiguientePregunta)
        {
            //Obtiene todas las relaciones Pregunta-Respuesta del idEncuesta 
            List<TDI_GraficasEncuesta> graficaEncu = MngNegocioGraficas.GraficarEncuesta(idEncuesta, true,false,"");

            //Limpia el objeto de la gráfica
            RadChart1.Clear();
            RadChart1.Skin = "Outlook";
            if (ArrayPreguntas == null)
            {
                ArrayPreguntas = new string[graficaEncu.Count];
                ArrayPreguntasRespuestas=new string[graficaEncu.Count];
            }

            hdnIdEncuesta.Value = idEncuesta.ToString();

            RadChart itmChar = new RadChart();
            ChartSeries itmChartSer;

            if (idSiguientePregunta == 0) // si la siguente pregunta no existe, es decir, Fin de la encuesta
            {
                RadChart1.ChartTitle.TextBlock.Text = "";
                divTituloGraf.InnerText = graficaEncu[0].PreguntaDescripcion;

                var q = from Graf in graficaEncu where Graf.IdPregunta == graficaEncu[0].IdPregunta select Graf.Contador;
                int suma = 0;
                foreach (int e in q)
                    suma += e;

                var q1 = from Graf in graficaEncu where Graf.IdPregunta == graficaEncu[0].IdPregunta select Graf;

                foreach (TDI_GraficasEncuesta prin in q1)
                {
                    SetXAxis(prin.PreguntaDescripcion, suma, itmChar);
                    itmChar.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;
                    itmChar.PlotArea.YAxis.AutoScale = false;

                    itmChartSer = new ChartSeries(prin.RespuestaDescripcion, ChartSeriesType.Bar);
                    itmChartSer.AddItem(prin.Contador);
                    itmChartSer.ActiveRegionAttributes = prin.IdSiguientePregunta.ToString();
                    SetSeriesAppearance(itmChartSer);

                    hfUltimaPos.Value = prin.IdPregunta.ToString();
                    ArrayPreguntas[indexPregunta] = prin.IdPregunta.ToString() + "|" + prin.PreguntaDescripcion;
                    RadChart1.AddChartSeries(itmChartSer);
                    
                }
                indexPreguntaRespuestas++;
                indexPregunta++;
            }
            else // Si no es la ultima pregunta de la encuesta 
            {
                btnAtras.Visible = true;
                
                var q2 = from Graf in graficaEncu where Graf.IdPregunta == idSiguientePregunta select Graf;
                int index=0;
                if(indexPreguntaRespuestas!=0)
                    index = indexPreguntaRespuestas - 1;

                int idPregAnterior = int.Parse(ArrayPreguntas[index].ToString().Split('|')[0]);
                var q3 = from Graf in graficaEncu where Graf.IdPregunta == idPregAnterior select Graf;

                
                DivText = "";
                string tabs = "";
                for (int inipr = 0; inipr < ArrayPreguntasRespuestas.Length; inipr++)
                {
                    try
                    {
                        if (ArrayPreguntasRespuestas[inipr] != null)
                        {
                            tabs += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                            if (index == 0 && ArrayPreguntas[inipr] != null)
                            {
                                DivText += tabs + ArrayPreguntas[index].ToString().Split('|')[1] + "<br/>" + tabs + ArrayPreguntasRespuestas[index].ToString() + "<br/>";
                                break;
                            }
                            else if (ArrayPreguntas[inipr] != null)
                                DivText += tabs + ArrayPreguntas[inipr].ToString().Split('|')[1] + "<br/>" + tabs + ArrayPreguntasRespuestas[inipr].ToString() + "<br/>";
                        }
                    }
                    catch(Exception exept)
                    {
                        DivText = "";
                    }
                   
                }

                var q = from Graf in graficaEncu where Graf.IdPregunta == graficaEncu[0].IdPregunta select Graf.Contador;

                int suma = 0;
                
                foreach (int e in q)
                    suma += e;

                foreach (TDI_GraficasEncuesta grafica2 in q2)
                {
                    RadChart1.ChartTitle.TextBlock.Text = "";
                    divTituloGraf.InnerText = grafica2.PreguntaDescripcion;

                    SetXAxis(grafica2.PreguntaDescripcion, suma, itmChar);
                    itmChar.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;
                    itmChar.PlotArea.YAxis.AutoScale = false;

                    ArrayPreguntas[indexPregunta] = grafica2.IdPregunta.ToString() + "|" + grafica2.PreguntaDescripcion;
                    itmChartSer = new ChartSeries(grafica2.RespuestaDescripcion, ChartSeriesType.Bar);
                    itmChartSer.AddItem(grafica2.Contador);
                    itmChartSer.ActiveRegionAttributes = grafica2.IdSiguientePregunta.ToString();
                    SetSeriesAppearance(itmChartSer);
                    RadChart1.AddChartSeries(itmChartSer);
                    
                }

                divDatos.InnerHtml = DivText + "<br/>";
                tabs += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                indexPregunta = indexPreguntaRespuestas+1;
                indexPreguntaRespuestas++;
            }          

            RadChart1.DataBind();
        }

        void RadChart1_Click(object sender, ChartClickEventArgs args)
        {
            if (args.Series != null)
            {
                if (args.Series.Name.Equals(args.Series.Name))
                {
                    if (args.SeriesItem != null)
                    {
                        RespGraf = args.Series.Name;
                        int index = indexPreguntaRespuestas - 1;
                        ArrayPreguntasRespuestas[index] = RespGraf;
                        InitRadChart(int.Parse(hdnIdEncuesta.Value), int.Parse(args.Series.ActiveRegionAttributes));
                    }
                }
            }
            mpeGraficaEncuesta.Show();
        }

        static void SetSeriesAppearance(ChartSeries series)
        {
            series.Appearance.Border.Color = System.Drawing.Color.Transparent;
            series.Appearance.LabelAppearance.Border.Color = System.Drawing.Color.White;
            series.Appearance.LabelAppearance.FillStyle.MainColor = System.Drawing.Color.White;
            series.Appearance.TextAppearance.TextProperties.Font = new System.Drawing.Font("Arial", 5);
            series.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black;
            series.Appearance.LabelAppearance.Dimensions.Margins = new ChartMargins(5);
            series.Appearance.FillStyle.MainColor = System.Drawing.Color.Blue;
          
            series.DefaultLabelValue = "#Y{N0}";
        }

        void SetXAxis(string pregunta, int numeroDePreguntas, RadChart grafica)
        {
            grafica.ChartTitle.TextBlock.Text = pregunta; 
            grafica.ChartTitle.Appearance.FillStyle.MainColor = System.Drawing.Color.DeepSkyBlue;
            grafica.PlotArea.XAxis.AutoScale = false;
            grafica.PlotArea.XAxis.Clear();
            grafica.PlotArea.XAxis.AddItem(numeroDePreguntas.ToString("#,##0") + " Total");
        }

        void SetYAxis(int minSalary, RadChart itemChart)
        {
            itemChart.PlotArea.YAxis.AddRange(minSalary, minSalary + 100, 10);
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            
            indexPregunta--;
            indexPreguntaRespuestas=indexPregunta;
            indexPreguntaRespuestas--;
            string[] ArrayPregRespAux = new string[ArrayPreguntasRespuestas.Length];
            int tamañoinicial=ArrayPreguntasRespuestas.Length;
            for (int ini = 0; ini < ArrayPreguntasRespuestas.Length; ini++)
            {
                if (ini != indexPreguntaRespuestas)
                    ArrayPregRespAux[ini] = ArrayPreguntasRespuestas[ini];
                else
                    break;
            }
            ArrayPreguntasRespuestas = ArrayPregRespAux;
            if(ArrayPreguntasRespuestas==null)
                ArrayPreguntasRespuestas = new string[tamañoinicial];

            string[] ArrayPregAux = new string[ArrayPreguntas.Length];
            for (int ini = 0; ini < ArrayPreguntas.Length; ini++)
            {
                if (ini <= (indexPreguntaRespuestas))
                    ArrayPregAux[ini] = ArrayPreguntas[ini];
                else
                    break;
            }
            ArrayPreguntas = ArrayPregAux;

            int preguntaID = int.Parse(ArrayPreguntas[indexPreguntaRespuestas].ToString().Split('|')[0]);
            InitRadChart(int.Parse(hdnIdEncuesta.Value), preguntaID);
            if (indexPregunta == 0)
            {
                btnAtras.Visible = false;
                
            }
            else
            {
                btnAtras.Visible = true;
               
            }
            mpeGraficaEncuesta.Show();
        }

        protected void gvPreguntas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPreguntas.PageIndex = e.NewPageIndex;
            CargaPreguntasPorEncuesta(int.Parse(ViewState["IdEncuesta"].ToString()));            
            mpePreguntas.Show();
        }

        protected void gvRespuestasGral_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRespuestasGral.EditIndex = e.NewEditIndex;
            gvRespuestasGral.Columns[0].Visible = false;
            gvRespuestasGral.Columns[1].Visible = true;
            gvRespuestasGral.Columns[2].Visible = false;
            gvRespuestasGral.Columns[3].Visible = true;
            gvRespuestasGral.Columns[4].Visible = false;
            gvRespuestasGral.Columns[5].Visible = true;
            CargaRespuestasPorPregunta(int.Parse(ViewState["IdPregunta"].ToString()));
            for (int ini = 0; ini < gvRespuestasGral.Rows.Count; ini++)
            {
                if (ini != e.NewEditIndex)

                    gvRespuestasGral.Rows[ini].Enabled = false;
            }
            mpeEncuestaRespuesta.Show();
        }

        protected void gvRespuestasGral_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropLst = ((DropDownList)e.Row.FindControl("ddlRespuesta"));
                List<THE_Preguntas> listaPreguntas = ((List<Entidades_EncuestasMoviles.THE_Preguntas>)Session["datasourceP"]);

                //List<THE_Preguntas> listaPreguntas = ((List<Entidades_EncuestasMoviles.THE_Preguntas>)Session["datasourceP"]);
                var preguntasSig = from Preguntas in listaPreguntas
                                   where Preguntas.IdPregunta > int.Parse(ViewState["IdPregunta"].ToString())
                                   orderby Preguntas.IdPregunta
                                   select Preguntas;

                dropLst.Enabled = true;
                dropLst.DataSource = preguntasSig;
                dropLst.DataTextField = "PreguntaDesc";
                dropLst.DataValueField = "IdPregunta";
                dropLst.DataBind();
                dropLst.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---Seleccione Pregunta Siguiente---", "0"));
                dropLst.SelectedValue = gvRespuestasGral.DataKeys[e.Row.RowIndex].Values["IdSiguientePregunta"].ToString();

                //Valida el tipo de encuesta 
                THE_Preguntas pregunta = MngNegocioPreguntas.ObtienePreguntaPorID(int.Parse(ViewState["IdPregunta"].ToString()))[0];
                int idEncuesta = pregunta.IdEncuesta.IdEncuesta;
                int tipoEncuesta = MngNegocioEncuesta.ObtieneEncuestaPorID(idEncuesta)[0].IdTipoEncuesta.IdTipoEncuesta;

                if (tipoEncuesta == 2) //Secuencial
                {
                    if (preguntasSig.Count() == 0)
                    {
                        dropLst.SelectedIndex = 0;
                    }
                    else
                    {
                        dropLst.SelectedIndex = 1;
                    }
                    dropLst.Enabled = false;
                }
            }
        }

        protected void gvRespuestasGral_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            THE_Respuestas Respu = new THE_Respuestas();
            Respu.RespuestaDescripcion = ((TextBox)gvRespuestasGral.Rows[e.RowIndex].FindControl("txtDescRespuestaGV")).Text;
            Respu.IdPregunta = new THE_Preguntas() { IdPregunta = int.Parse(ViewState["IdPregunta"].ToString()) };
            Respu.IdRespuesta = int.Parse(gvRespuestasGral.DataKeys[e.RowIndex].Values["IdRespuesta"].ToString());
            
            if (((DropDownList)gvRespuestasGral.Rows[e.RowIndex].FindControl("ddlRespuesta")).SelectedIndex == 0)
            {
                ctrlMessageBox.AddMessage("Esta Pregunta Dara Fin a la Encuesta", MessageBox.enmMessageType.Info, "Pregunta Fin de Encuesta");


                int IdPreg = MngNegocioPreguntas.ObtienePreguntaFinEncuesta(int.Parse(ViewState["IdEncuesta"].ToString()));
                if (IdPreg != -1)
                {
                    Respu.IdSiguientePregunta = IdPreg;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Respu.IdSiguientePregunta = int.Parse(((DropDownList)gvRespuestasGral.Rows[e.RowIndex].FindControl("ddlRespuesta")).SelectedValue.ToString());
            }


            Respu.RespuestaEstatus = 'A';
            bool GuardaRespuesta =MngNegocioRespuestas.ActualizaRespuesta(Respu);
            if (GuardaRespuesta)
            {
                GuardaLogTransacc("Se Actualizo la Respuesta " + Respu.IdRespuesta, 19);
                ctrlMessageBox.AddMessage("Respuesta " + Respu.IdRespuesta + " eliminada Correctamente", MessageBox.enmMessageType.Success, true, false, "Elimina", "Eliminacion de respuesta"); 
                      
            }
            txtRespuesta.Text = "";                
            
            gvRespuestasGral.Columns[0].Visible = true;
            gvRespuestasGral.Columns[1].Visible = false;
            gvRespuestasGral.Columns[2].Visible = true;
            gvRespuestasGral.Columns[3].Visible = false;
            gvRespuestasGral.Columns[4].Visible = true;
            gvRespuestasGral.Columns[5].Visible = false;
            gvRespuestasGral.EditIndex = -1;
            CargaRespuestasPorPregunta(int.Parse(ViewState["IdPregunta"].ToString()));
            mpeEncuestaRespuesta.Show();
        }

        public void GuardaLogTransacc(string desc, int idTran)
        {
            THE_LogTran oLogTran = new THE_LogTran();
            oLogTran.LogtDesc = desc;
            oLogTran.LogtDomi = "encuestasmoviles";
            oLogTran.LogtFech = DateTime.Now;
            oLogTran.LogtMach = Session["userMachineName"].ToString();
            oLogTran.LogtUsIp = Session["UserIP"].ToString();
            oLogTran.LogtUsua = Session["UserName"].ToString();
            oLogTran.TranLlavPr = new TDI_Transacc() { TranLlavPr = idTran };
            oLogTran.EmplLlavPr = new THE_Empleado() { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };

            MngNegocioTransacciones.GuardaLogTransaccion(oLogTran);
        }

        protected void btnCancelarModalProgramaciones_Click(object sender, EventArgs e)
        {
            try
            {
                mpeProgramaciones.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnCerrar5_Click(object sender, ImageClickEventArgs e)
        {
            CargaGrid();
        }

        protected void btnGuardaProgramacion_Click(object sender, EventArgs e)
        {
            try 
            {
                //var collection = cbDispoToProgramate.CheckedItems;

                //if (collection.Count != 0)
                //{ 
                //    foreach(var item in collection){
                        //int idDispositiv =Convert.ToInt32(item.Value);
                        THE_Programacion programacion = new THE_Programacion();
                        programacion.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(lblEncProgramacion.Text) };
                        programacion.IdTipoProgramacion = new TDI_TipoProgramacion() { IdTipoProgramacion = int.Parse(cbTipoProgramacion.SelectedItem.Value) };
                        programacion.ProgramacionNombre = txtProgramacion.Text;
                        programacion.ProgramacionEstatus = 'A';
                        //programacion.ID_Dispositivo = new THE_Dispositivo() { IdDispositivo = idDispositiv };
                
                        bool guardaProgramacion = MngNegocioProgramacion.GuardaProgramacionPorEncuesta(programacion);
                                
                        if (guardaProgramacion)
                        {
                            GuardaLogTransacc("Se Creo la Programacion: " + programacion.IdProgramacion, 33);
                            txtNomEncuesta.Text = string.Empty;                    
                        }
                 //   }
                //}

               
                CargaGridProgramaciones(int.Parse(lblEncProgramacion.Text));
                mpeProgramaciones.Show();
            }
            catch(Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void gvProgramaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProgramaciones.PageIndex = e.NewPageIndex;
            CargaGridProgramaciones(int.Parse(lblEncProgramacion.Text));
            mpeProgramaciones.Show();
        }

        protected void gvProgramaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            OcultaColumnasGVProgramaciones();
            CargaGridProgramaciones(int.Parse(lblEncProgramacion.Text));
            mpeProgramaciones.Show();
        }

        protected void gvProgramaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Elimina")
                {
                    //Muestra Mensaje si se desea Eliminar la Programación
                    int val = int.Parse(e.CommandArgument.ToString());
                    int idProgramacion = int.Parse(((GridView)sender).DataKeys[val].Value.ToString());

                    ViewState["Opcion"] = "Elimina Programacion";
                    ViewState["IdProgramacion"] = idProgramacion;
                    ctrlMessageBox.AddMessage("¿Esta seguro(a) de eliminar la Programación?", MessageBox.enmMessageType.Attention, true, true, "Elimina", "Elimina Programación");
                }

                if (e.CommandName == "AddConfiguracion")
                {
                    //Dependiento del tipo de programación Muestra Ventana semanal o por fecha                     
                    object idProgramacion = (((System.Web.UI.WebControls.GridView)(sender))).DataKeys[int.Parse(e.CommandArgument.ToString())].Value;
                    THE_Programacion programacion = MngNegocioProgramacion.ObtieneProgramacionPorID(int.Parse(idProgramacion.ToString()));
                    
                    if (programacion != null)
                    {
                        if (programacion.IdTipoProgramacion.TipoProgramacionDescripcion == "Por Fecha")
                        {
                            //CargaDatos de Programacion
                            LblProgramacionXFecha.Text = programacion.ProgramacionNombre;
                            LblIdProgramacionF.Text = idProgramacion.ToString();

                            //Carga las respuestas de la pregunta
                            CargaProgXFechaPorProg(programacion.IdProgramacion);

                            //Muestra el div
                            mpeProgramacionDetFecha.Show();
                        }
                        if (programacion.IdTipoProgramacion.TipoProgramacionDescripcion == "Por Semana")
                        {
                            //CargaDatos de Programacion
                            LblProgramacionXSemana.Text = programacion.ProgramacionNombre;
                            LblIdProgramacionS.Text = idProgramacion.ToString();
                            //Carga las respuestas de la pregunta
                            CargaProgXSemanaPorProg(programacion.IdProgramacion);

                            //Muestra el div
                            mpeProgramacionDetSemana.Show();
                        }                      
                    }
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        private void CargaProgXSemanaPorProg(int p)
        {
            try
            {
                List<THE_ProgXSemana> lstProgXSemana = MngNegocioProgXSemana.ObtieneProgXSemanaPorIdProg(int.Parse(LblIdProgramacionS.Text));

                if (lstProgXSemana != null)
                {
                    gvProgXSemana.DataSource = null;
                    gvProgXSemana.DataSource = lstProgXSemana;                    
                    gvProgXSemana.DataBind();
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }            
        }

        private void CargaProgXFechaPorProg(int p)
        {
            try
            {
                List<THE_ProgXFecha> lstProgXfecha = MngNegocioProgXFecha.ObtieneProgXFechaPorIdProg(int.Parse(LblIdProgramacionF.Text));

                if (lstProgXfecha != null)
                {
                    gvProgXFecha.DataSource = null;
                    gvProgXFecha.DataSource = lstProgXfecha;
                    gvProgXFecha.DataBind();
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }        
        }

        protected void gvProgramaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvProgramaciones.EditIndex = e.NewEditIndex;

            gvProgramaciones.Columns[0].Visible = true;
            gvProgramaciones.Columns[1].Visible = false;
            gvProgramaciones.Columns[2].Visible = false;
            gvProgramaciones.Columns[3].Visible = false;
            gvProgramaciones.Columns[4].Visible = false;
            gvProgramaciones.Columns[5].Visible = true;

            CargaGridProgramaciones(int.Parse(lblEncProgramacion.Text));
            for (int ini = 0; ini < gvProgramaciones.Rows.Count; ini++)
            {
                if (ini != e.NewEditIndex)
                    gvProgramaciones.Rows[ini].Enabled = false;
            }

            mpeProgramaciones.Show();
        }

        protected void gvProgramaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            THE_Programacion programacion = new THE_Programacion();
            int idProgramacion = int.Parse(e.Keys[0].ToString());
            programacion = MngNegocioProgramacion.ObtieneProgramacionPorID(idProgramacion);

            programacion.ProgramacionNombre = ((TextBox)gvProgramaciones.Rows[e.RowIndex].FindControl("txtNomProgramacionGV")).Text;
            
            bool actuProgramacion = MngNegocioProgramacion.ActualizaProgramacion(programacion);
            if (actuProgramacion)
            {
                GuardaLogTransacc("Se Actualizo la Programación " + programacion.IdProgramacion, 34);
            }
            OcultaColumnasGVProgramaciones();
            CargaGridProgramaciones(int.Parse(lblEncProgramacion.Text));
            mpeProgramaciones.Show();
        }

        protected void btnCerrarProgramacionSemana_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeProgramaciones.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void BtnGuardarProgSem_Click(object sender, EventArgs e)
        {
            LblMsgSem.Text = "";
            LblMsgSem.Visible = false;
            try
            {
                //Valida que se selecciono almenos un día
                if (CheckBoxLDiasSemana.SelectedItem != null) //almenos selecciono alguno
                {
                    //Valida si se guarda la misma hora para todos los días
                    if (CheckBMismaHoraSem.Checked == true) //misma hora
                    {                        
                        //valida hora no sea nula
                        if (RadTimePHoraSem.SelectedDate != null)
                        {
                            DateTime hora = DateTime.Parse(RadTimePHoraSem.SelectedDate.ToString());

                            //Barre los días para ver cual esta seleccionado
                            for (int i = 0; i < CheckBoxLDiasSemana.Items.Count; i++)
                            {
                                if (CheckBoxLDiasSemana.Items[i].Selected == true)
                                {
                                    THE_ProgXSemana progXSem = new THE_ProgXSemana();
                                    //Asigna valores
                                    progXSem.Dia = CheckBoxLDiasSemana.Items[i].Value;
                                    progXSem.Hora = hora.ToString("HH:mm");
                                    progXSem.IdProgramacion = new THE_Programacion() { IdProgramacion = int.Parse(LblIdProgramacionS.Text) };
                                    progXSem.Estatus = 'A';

                                    //Inserta registro
                                    bool insertProgXSem = MngNegocioProgXSemana.GuardaProgXSemana(progXSem);
                                    if (insertProgXSem)
                                    {
                                        //Desmarca el día.
                                        CheckBoxLDiasSemana.Items[i].Selected = false;
                                    }
                                }
                            }
                            RadTimePHoraSem.SelectedDate = null;
                        }
                        else
                        {
                            LblMsgSem.Text = "Debe seleccionar la hora ";
                            LblMsgSem.Visible = true;
                        }
                    }
                    else //diferente hora para cada fecha 
                    {
                        //valida hora no sea nula
                        if (RadTimePHoraSem.SelectedDate != null)
                        {
                            DateTime hora = DateTime.Parse(RadTimePHoraSem.SelectedDate.ToString());
                    
                            THE_ProgXSemana progXSem = new THE_ProgXSemana();
                            
                            //Asigna valores
                            progXSem.Dia = LblDia.Text;
                            progXSem.Hora = hora.ToString("HH:mm");
                            progXSem.IdProgramacion = new THE_Programacion() { IdProgramacion = int.Parse(LblIdProgramacionS.Text) };
                            progXSem.Estatus = 'A';

                            //Inserta registro
                            bool insertProgXSem = MngNegocioProgXSemana.GuardaProgXSemana(progXSem);
                            if (insertProgXSem)
                            {
                                //Encuentra el primer objeto seleccionado checkboxlist
                                for (int i = 0; i < CheckBoxLDiasSemana.Items.Count; i++)
                                {
                                    if (CheckBoxLDiasSemana.Items[i].Selected == true)
                                    {
                                        //Desmarca el 1er día que encuentre del checkboxlist. 
                                        CheckBoxLDiasSemana.Items[i].Selected = false;                                       
                                        RadTimePHoraSem.SelectedDate = null;

                                        //Datos
                                        LblEtiqDia.Visible = false;
                                        LblDia.Text = string.Empty;
                                        LblDia.Visible = false;
                                        LblHoraSem.Visible = false;
                                        RadTimePHoraSem.Visible = false;                                       

                                        //Valida si existen más dias por asignar
                                        if (CheckBoxLDiasSemana.SelectedItem != null)
                                        {
                                            //Botones
                                            BtnAsignar.Visible = true;
                                            BtnGuardarProgSem.Visible = false;
                                        }
                                        else
                                        {
                                            //Botones
                                            BtnAsignar.Visible = false;
                                            BtnGuardarProgSem.Visible = true;
                                            CheckBMismaHoraSem.Checked = true;
                                            CheckBMismaHoraSem.Visible = true;
                                            LblHoraSem.Visible = true;
                                            RadTimePHoraSem.Visible = true;
                                        }

                                        CargaProgXSemanaPorProg(int.Parse(LblIdProgramacionS.Text));
                                        mpeProgramacionDetSemana.Show();
                                        return;
                                    }
                                }
                                
                            }
                        }
                        else
                        {
                            LblMsgSem.Text = "Debe seleccionar la hora ";
                            LblMsgSem.Visible = true;
                        }
                    }
                }
                else 
                {
                    LblMsgSem.Text = "Debe seleccionar el día de la semana";
                    LblMsgSem.Visible = true;
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }

            CargaProgXSemanaPorProg(int.Parse(LblIdProgramacionS.Text));
            mpeProgramacionDetSemana.Show();
        }

        protected void CheckBMismaHoraSem_CheckedChanged(object sender, EventArgs e)
        {
            LblMsgSem.Text = string.Empty;
            LblMsgSem.Visible = false;
            if (CheckBMismaHoraSem.Checked == true) //Misma hora para todos los días seleccionados
            {
                LblEtiqDia.Visible = false;
                LblDia.Text = string.Empty;
                LblDia.Visible = false;

                LblHoraSem.Visible = true;
                RadTimePHoraSem.Visible = true;

                BtnAsignar.Visible = false;
                BtnGuardarProgSem.Visible = true;
            }
            else //Diferente hora para cada dia seleccionado
            {
                LblEtiqDia.Visible = false;
                LblDia.Text = string.Empty;
                LblDia.Visible = false;

                LblHoraSem.Visible = false;
                RadTimePHoraSem.Visible = false;

                BtnAsignar.Visible = true;
                BtnGuardarProgSem.Visible = false;
            }
            mpeProgramacionDetSemana.Show();
        }

        protected void gvProgXSemana_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvProgXSemana_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Elimina")
                {
                    //Muestra Mensaje si se desea Eliminar la Programación x fecha
                    int val = int.Parse(e.CommandArgument.ToString());
                    int idProgXSemana = int.Parse(((GridView)sender).DataKeys[val].Value.ToString());

                    ViewState["Opcion"] = "Elimina ProgXSemana";
                    ViewState["IdProgXSemana"] = idProgXSemana;
                    ctrlMessageBox.AddMessage("¿Esta seguro(a) de eliminar el día programado?", MessageBox.enmMessageType.Attention, true, true, "Elimina", "Elimina día programado");
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void gvProgXSemana_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvProgXSemana_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnCerrarProgramacionFecha_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeProgramaciones.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void CheckBTipoCarga_CheckedChanged(object sender, EventArgs e)
        {
            LblMsg.Text = string.Empty;
            LblMsg.Visible = false;

            if (CheckBTipoCarga.Checked == true)//Carga una sola fecha
            {
                LblFecha.Visible = true;
                RadDatePFechaProg.Visible = true;
                LblHora.Visible = true;
                RadTimePHoraProg.Visible = true;

                LblTitSelecFechas.Visible = false;
                RadCalProgXFecha.Visible = false;
                CheckBHoras.Visible = false;

                LblFechaProgXFecha1.Visible = false;
                LblFechaProgXFecha2.Visible = false;
                LblHoraProgFechas.Visible = false;
                RadTimePHora.Visible = false;
                btnAgregarHoras.Visible = false;
                BtnGuardarProgFec.Visible = true;
            }
            else //Carga multiples fechas
            {
                LblFecha.Visible = false;
                RadDatePFechaProg.Visible = false;
                LblHora.Visible = false;
                RadTimePHoraProg.Visible = false;

                LblTitSelecFechas.Visible = true;
                RadCalProgXFecha.Visible = true;
                CheckBHoras.Visible = true;

                CheckBHoras.Checked = true;

                LblFechaProgXFecha1.Visible = false;
                LblFechaProgXFecha2.Visible = false;
                LblHoraProgFechas.Visible = true;
                RadTimePHora.Visible = true;
                btnAgregarHoras.Visible = false;
                BtnGuardarProgFec.Visible = true;
            }
            mpeProgramacionDetFecha.Show();
        }

        protected void btnAgregarHoras_Click(object sender, EventArgs e)
        {            
            LblMsg.Text = string.Empty;
            LblMsg.Visible = false;
            //Validar fechas seleccionadas
            if(RadCalProgXFecha.SelectedDates.Count>0)
            {
                //Asigna horas a cada fecha
                //Carga primer fecha
                LblFechaProgXFecha1.Visible = true;
                LblFechaProgXFecha2.Text = RadCalProgXFecha.SelectedDates[0].Date.ToString("dd/MM/yyyy");
                LblFechaProgXFecha2.Visible = true;
                LblHoraProgFechas.Visible = true;
                RadTimePHora.Visible = true;
                btnAgregarHoras.Visible = false;
                BtnGuardarProgFec.Visible = true;
            }
            else
            {
                LblFechaProgXFecha1.Visible = false;
                LblFechaProgXFecha2.Text =string.Empty;
                LblFechaProgXFecha2.Visible = false;
                LblHoraProgFechas.Visible = false;
                RadTimePHora.Visible = false;
                btnAgregarHoras.Visible = true;
                BtnGuardarProgFec.Visible = false;
                LblMsg.Text = "Para asignar la hora es necesario seleccionar las fechas  ";
                LblMsg.Visible = true;
            }

            //Carga Grid y muestra panel de detalle Programación XFecha
            CargaProgXFechaPorProg(int.Parse(LblIdProgramacionF.Text));
            mpeProgramacionDetFecha.Show();
        }

        protected void BtnGuardarProgFec_Click(object sender, EventArgs e)
        {
            LblMsg.Text = "";
            LblMsg.Visible = false;
            try
            {
                //Valida que tipo de almacenamiento es
                if (CheckBTipoCarga.Checked == true) // solo una fecha
                {
                    //Valida que el campo de fecha/hora de carga no sean nulos
                    if (RadDatePFechaProg.SelectedDate.ToString() == "" | RadTimePHoraProg.SelectedDate.ToString() == "")
                    {
                        LblMsg.Text = "Son necesarias la fecha y hora ";
                        LblMsg.Visible = true;
                    }
                    else 
                    {
                        //valida la fecha y hora
                        DateTime fecha= Convert.ToDateTime(RadDatePFechaProg.SelectedDate.ToString());
                        DateTime hora = Convert.ToDateTime(RadTimePHoraProg.SelectedDate.ToString());

                        if(Valida(fecha,hora))
                        {
                            THE_ProgXFecha progXFecha = new THE_ProgXFecha();

                            progXFecha.Fecha = fecha;
                            progXFecha.Hora = hora.ToString("HH:mm");
                            progXFecha.IdProgramacion = new THE_Programacion() { IdProgramacion = int.Parse(LblIdProgramacionF.Text) };
                            progXFecha.Estatus = 'A';

                            bool guardaProgXFecha = MngNegocioProgXFecha.GuardaProgXFecha(progXFecha);

                            if (guardaProgXFecha)
                            {
                                GuardaLogTransacc("Se Creo la Programacion por fecha: " + progXFecha.IdProgramacion, 33);
                                RadDatePFechaProg.SelectedDate = null;
                                RadTimePHoraProg.SelectedDate = null;
                            }
                        }
                        else
                        {
                            LblMsg.Text = "La fecha y hora no son validas ";
                            LblMsg.Visible = true;
                        }
                    }
                }
                else //Varias fechas
                {
                    //Valida que existan fechas seleccionadas
                    if (RadCalProgXFecha.SelectedDates.Count > 0)
                    {
                        if (CheckBHoras.Checked == true) //Se carga la misma hora para todas las fechas 
                        {
                            //Valida que la hora no este vacia
                            if(RadTimePHora.SelectedDate != null)
                            {
                                DateTime hora = Convert.ToDateTime(RadTimePHora.SelectedDate);
                                
                                //Obtener los días seleccionadas                            
                                var dias = RadCalProgXFecha.SelectedDates;

                                for (int i = 0; i < RadCalProgXFecha.SelectedDates.Count; i++)
                                {
                                    DateTime fecha = dias[i].Date;

                                    if (Valida(fecha, hora))
                                    {
                                        //Asigna valores
                                        THE_ProgXFecha progXFecha = new THE_ProgXFecha();

                                        progXFecha.Fecha = fecha;
                                        progXFecha.Hora = hora.ToString("HH:mm");
                                        progXFecha.IdProgramacion = new THE_Programacion() { IdProgramacion = int.Parse(LblIdProgramacionF.Text) };
                                        progXFecha.Estatus = 'A';

                                        //inserta en base de datos
                                        bool insertProgXFecha = MngNegocioProgXFecha.GuardaProgXFecha(progXFecha);

                                        if (insertProgXFecha)
                                        {
                                            //Elimina la selección del control 
                                            RadCalProgXFecha.SelectedDates.Remove(dias[i]);
                                        }
                                    }
                                    else
                                    {
                                        LblMsg.Text = "La fecha: " + fecha.ToString("dd/MM/yyyy") + " y hora: " + hora.ToString("HH:mm") + " no son validas. <br/> ";
                                        LblMsg.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                LblMsg.Text = "Debe seleccionar la hora ";
                                LblMsg.Visible = true;
                            }
                        }                    
                        else //Se carga diferente hora para cada fecha seleccionada
                        {
                            //Valida que la hora no este vacia
                            if (RadTimePHora.SelectedDate != null)
                            {
                                DateTime hora = Convert.ToDateTime(RadTimePHora.SelectedDate);

                                //Obtener los día
                                DateTime fecha = RadCalProgXFecha.SelectedDates[0].Date;
                                
                                //Valida fecha y hora
                                if (Valida(fecha, hora))
                                    {
                                        //Asigna valores
                                        THE_ProgXFecha progXFecha = new THE_ProgXFecha();

                                        progXFecha.Fecha = fecha;
                                        progXFecha.Hora = hora.ToString("HH:mm");
                                        progXFecha.IdProgramacion = new THE_Programacion() { IdProgramacion = int.Parse(LblIdProgramacionF.Text) };
                                        progXFecha.Estatus = 'A';

                                        //inserta en base de datos
                                        bool insertProgXFecha = MngNegocioProgXFecha.GuardaProgXFecha(progXFecha);

                                        if (insertProgXFecha)
                                        {
                                            //Elimina la selección del control 
                                            RadCalProgXFecha.SelectedDates.Remove(RadCalProgXFecha.SelectedDates[0]);
                                            //Datos
                                            LblFechaProgXFecha1.Visible = false;
                                            LblFechaProgXFecha2.Text = string.Empty;
                                            LblFechaProgXFecha2.Visible = false;
                                            LblHoraProgFechas.Visible = false;
                                            RadTimePHora.Visible = false;

                                            //Valida si existen más fechas por asignar
                                            if (RadCalProgXFecha.SelectedDates.Count > 0)
                                            {
                                                //Botones
                                                btnAgregarHoras.Visible = true;
                                                BtnGuardarProgFec.Visible = false;
                                            }
                                            else
                                            {
                                                //Botones
                                                btnAgregarHoras.Visible = false;
                                                BtnGuardarProgFec.Visible = true;
                                                CheckBHoras.Checked = true;
                                                CheckBHoras.Visible = true;
                                                LblHoraProgFechas.Visible = true;
                                                RadTimePHora.Visible = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        LblMsg.Text = "La fecha: " + fecha.ToString("dd/MM/yyyy") + " y hora: " + hora.ToString("HH:mm") + " no son validas.";
                                        LblMsg.Visible = true;

                                        //Elimina la selección del control porq la fecha no es valida 
                                        RadCalProgXFecha.SelectedDates.Remove(RadCalProgXFecha.SelectedDates[0]);
                                        //Datos
                                        LblFechaProgXFecha1.Visible = false;
                                        LblFechaProgXFecha2.Text = string.Empty;
                                        LblFechaProgXFecha2.Visible = false;
                                        LblHoraProgFechas.Visible = false;
                                        RadTimePHora.Visible = false;

                                        //Valida si existen más fechas por asignar
                                        if (RadCalProgXFecha.SelectedDates.Count > 0)
                                        {
                                            //Botones
                                            btnAgregarHoras.Visible = true;
                                            BtnGuardarProgFec.Visible = false;
                                        }
                                        else
                                        {
                                            //Botones
                                            btnAgregarHoras.Visible = false;
                                            BtnGuardarProgFec.Visible = true;
                                            CheckBHoras.Checked = true;
                                            CheckBHoras.Visible = true;
                                            LblHoraProgFechas.Visible = true;
                                            RadTimePHora.Visible = true;
                                        }
                                    }                                
                            }
                            else
                            {
                                LblMsg.Text = "Debe seleccionar la hora ";
                                LblMsg.Visible = true;                               
                            }
                        }
                    }
                    else
                    {
                        LblMsg.Text = "Debe seleccionar las fechas ";
                        LblMsg.Visible = true;
                    }
                }
                //Carga Grid y muestra panel de detalle Programación XFecha
                CargaProgXFechaPorProg(int.Parse(LblIdProgramacionF.Text));
                mpeProgramacionDetFecha.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        private bool Valida(DateTime fecha, DateTime hora)
        {
            bool result = false;
            //Valida fecha
            if (Convert.ToDateTime(fecha.ToString("dd/MM/yyyy")) < Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"))) //Fecha menor a hoy
            {
                result = false;
                return result;
            }
            else
            {
                if (fecha.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")) //Si la fecha es igual a hoy
                {
                    //Validar hora
                    if (hora.Hour < DateTime.Now.Hour) //Si la hora es menor a la de este momento
                    {
                        result = false;
                        return result;
                    }
                    else
                    {
                        if (hora.Hour == DateTime.Now.Hour) //Si la hora es la misma
                        {
                            //Validar minutos
                            if (hora.Minute > DateTime.Now.Minute+5)
                            {
                                result = true;
                                return result;
                            }
                            else
                            {
                                result = false;
                                return result;
                            }
                        }
                        else //la hora es mayor 
                        {
                            result = true;
                            return result;
                        }
                    }
                }
                else //Si la fecha es mayor a hoy, ya no es necesario validar nada
                {
                    result = true;
                    return result;
                }
            }          
        }

        protected void CheckBHoras_CheckedChanged(object sender, EventArgs e)
        {
            LblMsg.Text = string.Empty;
            LblMsg.Visible = false;
            if (CheckBHoras.Checked == true) //Misma hora para todas las fechas seleccionadas
            {
                LblTitSelecFechas.Visible = true;
                RadCalProgXFecha.Visible = true;

                LblFechaProgXFecha1.Visible = false;
                LblFechaProgXFecha2.Visible = false;
                LblHoraProgFechas.Visible = true;
                RadTimePHora.Visible = true;
                btnAgregarHoras.Visible = false;
                BtnGuardarProgFec.Visible = true;
            }
            else //Diferente hora para cada fecha seleccionada
            {
                LblTitSelecFechas.Visible = true;
                RadCalProgXFecha.Visible = true;

                LblFechaProgXFecha1.Visible = false;
                LblFechaProgXFecha2.Visible = false;
                LblHoraProgFechas.Visible = false;
                RadTimePHora.Visible = false;
                btnAgregarHoras.Visible = true;
                BtnGuardarProgFec.Visible = false;
            }
            mpeProgramacionDetFecha.Show();
        }

        protected void gvProgXFecha_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvProgXFecha_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Elimina")
                {
                    //Muestra Mensaje si se desea Eliminar la Programación x fecha
                    int val = int.Parse(e.CommandArgument.ToString());
                    int idProgXFecha = int.Parse(((GridView)sender).DataKeys[val].Value.ToString());

                    ViewState["Opcion"] = "Elimina ProgXFecha";
                    ViewState["IdProgXFecha"] = idProgXFecha;
                    ctrlMessageBox.AddMessage("¿Esta seguro(a) de eliminar la fecha de programación?", MessageBox.enmMessageType.Attention, true, true, "Elimina", "Elimina fecha programada");
                }               
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void gvProgXFecha_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvProgXFecha_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void gvProgXFecha_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvProgXFecha_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvProgXSemana_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void BtnAsignar_Click(object sender, EventArgs e)
        {
            LblMsgSem.Text = string.Empty;
            LblMsgSem.Visible = false;
            //Validar dias seleccionados
            if (CheckBoxLDiasSemana.SelectedItem != null) //al menos selecciono un día
            {
                //Asigna horas a cada día
                //Carga primer dia
                LblEtiqDia.Visible = true;
                //Encuentra el primer objeto seleccionado checkboxlist
                for (int i = 0; i < CheckBoxLDiasSemana.Items.Count; i++)
                {
                    if (CheckBoxLDiasSemana.Items[i].Selected == true)
                    {
                        LblDia.Text = CheckBoxLDiasSemana.Items[i].Value;
                        LblDia.Visible = true;

                        LblHoraSem.Visible = true;
                        RadTimePHoraSem.Visible = true;

                        BtnAsignar.Visible = false;
                        BtnGuardarProgSem.Visible = true;

                        //Carga Grid y muestra panel de detalle Programación XSemana
                        CargaProgXSemanaPorProg(int.Parse(LblIdProgramacionS.Text));
                        mpeProgramacionDetSemana.Show();
                        return;
                    }
                }                
            }
            else
            {
                LblEtiqDia.Visible = false;
                LblDia.Text = string.Empty;
                LblDia.Visible = false;               
                BtnAsignar.Visible = true;
                BtnGuardarProgSem.Visible = false;

                LblHoraSem.Visible = false;
                RadTimePHoraSem.Visible = false;

                LblMsgSem.Text = "Para asignar la hora es necesario seleccionar el día  ";
                LblMsgSem.Visible = true;
            }

            //Carga Grid y muestra panel de detalle Programación XSemana
            CargaProgXSemanaPorProg(int.Parse(LblIdProgramacionS.Text));
            mpeProgramacionDetSemana.Show();
        }
       
    }
}
