using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using Telerik.Charting;
using Telerik.Web.UI;
using Telerik.Charting.Styles;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using com.dsi.pgp;
using System.Configuration;
using Microsoft.Win32;
using BLL_EncuestasMoviles;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.Services;
using DataStructures.AutoComplete;
using System.Web.Hosting;

namespace EncuestasMoviles.Pages
{

    public partial class Grafica : System.Web.UI.Page
    {
        #region Variables
        static string[] ArrayPreguntas;
        static string[] ArrayPreguntasRespuestas;
        int index = 0;
        static int indexPregunta = 0;
        static int indexPreguntaRespuestas = 0;
        static string DivText = "";
        static string tabs = "";
        static string RespGraf = "";
        int IdEnc = 0;
        bool Refresh = false;
        int min;        
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Session["UserName"] = "20451";
                //if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                //{
                //    Response.Redirect("~/Default.aspx");
                //}
                
                //List<THE_SesionUsuario> existeSesion =MngNegocioUsuarioSesion.VerExisteSesionUsuario(Int32.Parse(Session["numeroUsuario"].ToString()), Session["UserIP"].ToString());
                //if (existeSesion.Count == 0)
                //{
                //    return;
                //}
                
                try
                {
                    if (!IsPostBack)
                    {   
                        if (Request.QueryString["EncId"] != null)
                            hdnIdEncuesta.Value = Request.Url.Query.Substring(Request.Url.Query.IndexOf("EncId=") + 6);
                       
                        Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
                        string EncEncrypt = _ChyperRijndael.Transmute((hdnIdEncuesta.Value), Azteca.Utility.Security.enmTransformType.intDecrypt);

                        IdEnc = Convert.ToInt32(EncEncrypt);
                        hdnIdEncuesta.Value = EncEncrypt;
                       
                        divDatos.InnerHtml = "";
                        hdnTemporizador.Value = "55000"; 
                        indexPregunta = 0;
                        indexPreguntaRespuestas = 0;
                        ArrayPreguntas = null;
                        ArrayPreguntasRespuestas = null;
                        DivText = "";
                        RespGraf = "";
                        CargaDdlPresentacion();
                        LlenaDatosEncuesta(IdEnc);
                        InitRadChart(IdEnc, 0, int.Parse(ddlPresentacion.SelectedItem.Value));
                       
                        ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                        
                    }
                    else
                    {
                        if (hdndivEmail.Value == "0")
                        {
                            DvEmail.Style.Add(HtmlTextWriterStyle.Display, "none");
                            btnEnviaCorreos.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                        }                      
                        ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception ms) { }
            finally { }
        }

        void CargaDdlPresentacion()
        {
            List<TDI_Presentacion> tipoPresentacion = MngNegocioPresentacion.ObtieneTodaslasPresentaciones() ;
            ddlPresentacion.DataSource = tipoPresentacion;
            ddlPresentacion.DataTextField = "PresentacionDescripcion";
            ddlPresentacion.DataValueField = "IdPresentacion";
            ddlPresentacion.DataBind();
        }

        [WebMethod]
        public static string DescencriptaCad(string cadena) {
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            string cadEncrypt = _ChyperRijndael.Transmute((cadena), Azteca.Utility.Security.enmTransformType.intDecrypt);
            return cadEncrypt;
        }

        [WebMethod]
        public static List<string> SearchEmail(string prefixText, int count)
        {
            prefixText = prefixText.Trim();
            List<string> filtroEmpl = new List<string>();
            List<THE_Empleado> empl = MngNegocioEmpleadoRol.GetEmailEmpleados();

            var listEmpl =  from EMPL in empl
                            where EMPL.EmpleadoMail.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase)
                            orderby EMPL.EmpleadoMail
                            select EMPL;

            foreach (var item in listEmpl)
            {
                filtroEmpl.Add(item.EmpleadoMail);
            }

            return filtroEmpl;
          
        }

        void RadChartGrafica_Click(object sender, ChartClickEventArgs args)
        {
            //if (args.Series != null)
            //{
            //    if (args.Series.Name.Equals(args.Series.Name))
            //    {
            //        if (args.SeriesItem != null)
            //        {
            //            if (int.Parse(ddlPresentacion.SelectedItem.Value) == 1)
            //            {
            //                string texto = "";
            //                if (args.SeriesItem.Name.IndexOf("[[") >= 0)
            //                    texto = args.SeriesItem.Name.Substring(args.SeriesItem.Name.LastIndexOf("]]") + 2).Trim();
            //                else
            //                    texto = args.SeriesItem.Name;

            //                RespGraf = texto; //Almacena en la variable el valor de la respuesta seleccionada

            //                index = indexPregunta - 1; //Obtine index (posición del arreglo de Respuestas)
            //                ArrayPreguntasRespuestas[index] = RespGraf;
            //                Refrescar(int.Parse(hdnIdEncuesta.Value), int.Parse(args.SeriesItem.ActiveRegion.Attributes), int.Parse(ddlPresentacion.SelectedItem.Value));
            //            }
            //            if (int.Parse(ddlPresentacion.SelectedItem.Value) == 2) //Pregunta
            //            {
            //                divDatos.InnerHtml = "";
            //                //Valida ultima pregunta
            //                if (indexPregunta == GridVPreguntas.Rows.Count - 1)
            //                {
            //                    GridVPreguntas.SelectRow(0); //Selecciona Primera pregunta
            //                }
            //                else
            //                {
            //                    GridVPreguntas.SelectRow(indexPregunta + 1);
            //                }
            //                //btnAtras.Visible = false;
            //            }
            //        }
            //    }
            //}
        }

        void LlenaDatosEncuesta(int IdEncuesta)
        {
            List<THE_EncuestaEstatus> EstatusEncuesta = MngNegocioGraficas.ConsultaEncuestasEstatus(IdEncuesta, chkMostrarTodos.Checked);
            List<THE_Encuesta> Encuesta = MngNegocioEncuesta.ObtieneEncuestaPorID(IdEncuesta);

            int Todos = 0;
            int Contestados = 0;
            int SinContestar = 0;
            foreach (THE_EncuestaEstatus encuestaEstatus in EstatusEncuesta)
            {
                if (encuestaEstatus.IdEstatus == 2 || encuestaEstatus.IdEstatus == 3)
                {
                    SinContestar = SinContestar + encuestaEstatus.Numero;
                }
                else if (encuestaEstatus.IdEstatus == 4)
                {
                    Contestados = encuestaEstatus.Numero;
                }
            }
            Todos = Contestados + SinContestar;
            //lbl_N.Text = Todos.ToString();
            //lbl_NoN.Text = SinContestar.ToString();
            divTitulo.InnerText = Encuesta[0].NombreEncuesta;
        }

        [WebMethod]
        public static string FillDatosEncuesta(int IdEncuesta, bool checboxTodos)
        {
            List<THE_EncuestaEstatus> EstatusEncuesta = MngNegocioGraficas.ConsultaEncuestasEstatus(IdEncuesta, checboxTodos);
            List<THE_Encuesta> Encuesta = MngNegocioEncuesta.ObtieneEncuestaPorID(IdEncuesta);

            int Todos = 0;
            int Contestados = 0;
            int SinContestar = 0;
            foreach (THE_EncuestaEstatus encuestaEstatus in EstatusEncuesta)
            {
                if (encuestaEstatus.IdEstatus == 2 || encuestaEstatus.IdEstatus == 3)
                {
                    SinContestar = SinContestar + encuestaEstatus.Numero;
                }
                else if (encuestaEstatus.IdEstatus == 4)
                {
                    Contestados = encuestaEstatus.Numero;
                }
            }
            Todos = Contestados + SinContestar;
          
            string json = "{'Todos':'" + Todos.ToString() + "','Contestado':'" + Contestados.ToString() + "','SinContestar':'" + SinContestar.ToString() + "','NombreEnc':'" + Encuesta[0].NombreEncuesta.ToString()+ "'}";
            return json;
        }

        [WebMethod]
        public static string PintaGrafica(int idEncuesta, bool checboxTodos, bool checboxHorario, string Catalogos)
        {
            List<TDI_GraficasEncuesta> graficaEncu = MngNegocioGraficas.GraficarEncuesta(idEncuesta, checboxTodos, checboxHorario, Catalogos);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(graficaEncu);
            return resultjson;          
        }

        [WebMethod]
        public static string DibujaGrafica(int idEncuesta,  bool checboxHorario, string Catalogos,string idPregunta)
        {
            List<TDI_GraficasEncuesta> graficaEncu = MngNegocioGraficas.DibujaGrafica(idEncuesta,  checboxHorario, Catalogos, idPregunta);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(graficaEncu);
            return resultjson;
        }


        [WebMethod]
        public static string TopOfMind(string idPregunta, string idsDispos) {
            List<TDI_GraficasEncuesta> Top = MngNegocioGraficas.GeTopOfMind(idPregunta, idsDispos);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(Top);
            return resultjson;          
        }

        [WebMethod]
        public static string ObtenerCatalogos()
        {
           
            Dictionary<string,string> Catalogos=new Dictionary<string,string>();            
            try
            {              

                List<THE_Catalogo> listCatalogos = MngNegocioCatalogo.ObtieneTodosCatalogos();

                if (listCatalogos.Count > 0)
                {
                    foreach (THE_Catalogo itemCatalogo in listCatalogos)
                    {
                        List<TDI_OpcionCat> lstOpcioCatalogo = MngNegocioOpcionCat.ObtieneOpcionesPorCatalogo(itemCatalogo.IdCatalogo);

                        if (lstOpcioCatalogo != null)
                        {
                            if (itemCatalogo != null)
                            {
                                string opciones = "";
                                foreach (TDI_OpcionCat op in lstOpcioCatalogo)
                                {
                                    opciones+= op.IdOpcionCat.ToString() + "|" + op.OpcionCatDesc.ToString()+";";
                                }
                                opciones=opciones.Substring(0, opciones.Length-1);
                                Catalogos.Add(itemCatalogo.CatalogoDesc.ToString(),opciones);
                            }

                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(Catalogos);
            return resultjson;  
        }


        void InitRadChart(int idEncuesta, int idSiguientePregunta, int idPresentacion)
        {
       
        }

        protected void exportPDF_Click(object sender, EventArgs e)
        {
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            string encEncrypt = _ChyperRijndael.Transmute(hdnIdEncuesta.Value,Azteca.Utility.Security.enmTransformType.intEncrypt);

            Response.Redirect("ExportaPDF.aspx?EncId=" + encEncrypt);
        }
                          
        protected void btnEnviaEmail_Click(object sender, EventArgs e)
        {
            //ClientScript.Register(this.GetType(),"MuestraEnvioGrafica", "<script type=\"text/javascript\">MuestraEnvioGrafica();</script>");
            DvEmail.Style.Add(HtmlTextWriterStyle.Display, "block");
            txtNombreDe.Text = Session["nombreUsuario"].ToString();
            btnEnviaCorreos.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
            hdndivEmail.Value = "1";

        }

        protected void btnEnviarGrafica_Click(object sender, EventArgs e)
        {


            bool estatusEnviaCorreo =MngNegocioGraficas.EnviaGraficaCorreo(txtNombreDe.Text, txtEmailPara.Text, txtEmailPara.Text, "Encuestas", int.Parse(hdnIdEncuesta.Value));

            if (estatusEnviaCorreo)
            {
                ctrlMessageBox.AddMessage("Se Envio Correctamente el Email a: " + txtEmailPara.Text, MessageBox.enmMessageType.Success, "Envia Email");
            }
            else
            {
                ctrlMessageBox.AddMessage("Ocurrio un Error al Enviar el Email a: " + txtEmailPara.Text, MessageBox.enmMessageType.Error, "Envia Email");
            }
           
            txtEmailPara.Text = "";
            
        }

        protected void Acepta_Evento(object sender, EventArgs e)
        {

        }

        public void ctrlMessageBox_MsgBoxAnswered(object sender, MessageBox.MsgBoxEventArgs e)
        {
            if (e.Answer == MessageBox.enmAnswer.OK)
            {

            }
            else if (e.Answer == MessageBox.enmAnswer.Cancel)
            {
                ctrlMessageBox.AddMessage("Ha Cancelado la Operación", MessageBox.enmMessageType.Info, "");
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {          
            
           
        }
     
        private System.Drawing.Color RegresaColor(int i)
        {
            System.Drawing.Color[] Color = new System.Drawing.Color[80]{
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange,
                       System.Drawing.Color.Goldenrod,
                       System.Drawing.Color.SteelBlue,
                       System.Drawing.Color.Khaki,
                       System.Drawing.Color.Yellow,
                       System.Drawing.Color.BurlyWood,
                       System.Drawing.Color.DarkOliveGreen,
                       System.Drawing.Color.SkyBlue,
                       System.Drawing.Color.Orange
                   };

            return Color[i];
        }

        private void Refrescar(int idEncuesta, int idSiguientePregunta, int idPresentacion)
        {

        }
                
        protected void RadChartGrafica_PreRender(object sender, EventArgs e)
        {            
            RadChart serie = (RadChart)sender;
            serie.Appearance.BarWidthPercent = 80;
        }

        protected void DdlPresentacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        void CargaGvPreguntas(int encuesta)
        {
           
        }

        protected void GridVPreguntas_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void GridVPreguntas_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            THE_Preguntas preg = new THE_Preguntas();
            List<THE_Preguntas> preguntas = Session["datasourceP"] as List<THE_Preguntas>;
            preg = preguntas[e.NewSelectedIndex] as THE_Preguntas;
            //Carga la gráfica de nuevo
            Refrescar(int.Parse(hdnIdEncuesta.Value), preg.IdPregunta, 2);
            divDatos.InnerHtml = "";
            indexPregunta = e.NewSelectedIndex;
            //btnAtras.Visible = false;
        }

        protected void GridVPreguntas_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        protected void chkMostrarTodos_CheckedChanged(object sender, EventArgs e)
        {
            LlenaDatosEncuesta(int.Parse(hdnIdEncuesta.Value));
            InitRadChart(int.Parse(hdnIdEncuesta.Value), 0, int.Parse(ddlPresentacion.SelectedItem.Value));
        }
        protected void chkMostrarTodos_DentroHorario(object sender, EventArgs e)
        {
            LlenaDatosEncuesta(int.Parse(hdnIdEncuesta.Value));
            InitRadChart(int.Parse(hdnIdEncuesta.Value), 0, int.Parse(ddlPresentacion.SelectedItem.Value));
        }

    }
}
