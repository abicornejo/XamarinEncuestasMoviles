using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BLL_EncuestasMoviles;
using EncuestasMoviles.Controls;
using Entidades_EncuestasMoviles;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace EncuestasMoviles.Pages
{
    public partial class ReporteRespuestaPorEncuesta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {
                Response.Redirect("~/Default.aspx");
            }

            List<THE_SesionUsuario> existeSesion = MngNegocioUsuarioSesion.VerExisteSesionUsuario(Int32.Parse(Session["numeroUsuario"].ToString()), Session["UserIP"].ToString());
            if (existeSesion.Count == 0)
            {
                return;
            }

            try { 
            
                if(!IsPostBack){

                    TDI_LogPaginas logPaginas = new TDI_LogPaginas();
                    logPaginas.LogFecha = DateTime.Now;
                    logPaginas.LogIp = Session["UserIP"].ToString();
                    logPaginas.LogUrlPagina = Request.RawUrl;
                    logPaginas.EmpleadoLlavePrimaria = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                    MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);

                    //List<THE_Encuesta> encuGra = MngNegocioEncuesta.ObtieneEncuestasActivas();

                    //if (encuGra.Count > 0)
                    //{
                    //    cbEncuestas.DataSource = encuGra;
                    //    cbEncuestas.DataTextField = "Nombreencuesta";
                    //    cbEncuestas.DataValueField = "IdEncuesta";
                    //    cbEncuestas.DataBind();
                    //}
                    
                }
            }
            catch(Exception ms)
            {
            
            }
        }


        //protected void Btnbuscar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string idEncuesta = cbEncuestas.SelectedValue.ToString();
        //        List<THE_Usuario> Usuario = MngNegocioEncuesta.ReporteRespuestaByEncuesta(idEncuesta);

        //        if (Usuario.Count == 0)
        //        {
        //            string script = "alert('No se encontraron los resultados')";                   
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), script, "MuestraAlert();", true);                    
        //        }
        //        else 
        //        {
        //            gdRespEncuestas.DataSource = Usuario;
        //            gdRespEncuestas.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        [WebMethod]
        public static string GetReporte(string idEncuesta)
        {
            List<THE_Usuario> Usuario = MngNegocioEncuesta.ReporteRespuestaByEncuesta(idEncuesta);

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(Usuario);
            return resultjson;  
        }


        [WebMethod]
        public static string GetEncuestas()
        {
            List<THE_Encuesta> encuGra = MngNegocioEncuesta.ObtieneEncuestasActivas();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(encuGra);
            return resultjson;
        }

    }
}
