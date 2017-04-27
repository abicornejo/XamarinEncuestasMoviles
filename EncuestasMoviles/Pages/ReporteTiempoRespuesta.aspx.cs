using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL_EncuestasMoviles;
using EncuestasMoviles.Controls;
using Entidades_EncuestasMoviles;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;

namespace EncuestasMoviles.Pages
{
    public partial class ReporteTiempoRespuesta : System.Web.UI.Page
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

                    List<THE_Encuesta> encuGra = MngNegocioEncuesta.ObtieneEncuestasActivas();

                    if (encuGra.Count > 0)
                    {
                        RadComboBox1.DataSource = encuGra;
                        RadComboBox1.DataTextField = "Nombreencuesta";
                        RadComboBox1.DataValueField = "IdEncuesta";
                        RadComboBox1.DataBind();

                        //List<THE_Usuario> Usuario = MngNegocioUsuario.ObtieneTodosUsuarios();

                        //foreach (var us in Usuario)
                        //{
                        //    us.UsuarioNombre = us.UsuarioNombre + " " + us.UsuarioApellPaterno + " " + us.UsuarioApellMaterno;
                        //}

                        //if (Usuario.Count > 0)
                        //{
                        //    RadComboBox2.DataSource = Usuario;
                        //    RadComboBox2.DataTextField = "UsuarioNombre";
                        //    RadComboBox2.DataValueField = "UsuarioLlavePrimaria";
                        //    RadComboBox2.DataBind();
                        //}
                    }
                }
            }
            catch (Exception msException)
            {

            }
        }
        protected void ImgExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportaGridExcel();
        }
        protected void Btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
              //  if (gvTiempoRespuesta.Rows.Count > 0)
                {
                    string idEncuesta = RadComboBox1.SelectedValue.ToString();
                    
                    List<THE_Usuario> Usuario = MngNegocioEncuesta.ReporteTiempoRespuesta(idEncuesta, "");
                    gvTiempoRespuesta.DataSource = Usuario;
                    gvTiempoRespuesta.DataBind();
                }
            }catch(Exception ex){
            
            }

            //}            
        }


        private void ExportaGridExcel()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pag = new Page();
            HtmlForm form = new HtmlForm();

            gvTiempoRespuesta.AllowPaging = false;
            gvTiempoRespuesta.EnableViewState = false;
            pag.EnableEventValidation = false;
            gvTiempoRespuesta.AllowSorting = false;
           
            pag.DesignerInitialize();
            pag.Controls.Add(form);

            form.Controls.Add(gvTiempoRespuesta);
            pag.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=BusqUsuarios.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();

           // gvTiempoRespuesta.AllowPaging = true;

        }

       


    }
}
