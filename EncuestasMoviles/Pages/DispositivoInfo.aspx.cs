using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;

namespace EncuestasMoviles.Pages
{
    public partial class DispositivoInfo : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
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
                    TDI_LogPaginas logPaginas = new TDI_LogPaginas();
                    logPaginas.LogFecha = DateTime.Now;
                    logPaginas.LogIp = Session["UserIP"].ToString();
                    logPaginas.LogUrlPagina = Request.RawUrl;
                    logPaginas.EmpleadoLlavePrimaria = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                    MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);
                }

                string IdUsuario = Request.QueryString["data"];
                string IdDispositivo = Request.QueryString["guid"];
                if (IdDispositivo != "NO")
                {
                    List<TDI_UsuarioDispositivo> usuaDis =MngNegocioUsuarioDispositivo.ObtieneDispoUsuarioPorIdDispo(int.Parse(IdDispositivo));
                    if (usuaDis.Count > 0)
                    {
                        lblNombreUsua.Text = usuaDis[0].UsuarioLlavePrimaria.UsuarioNombre;
                        lblDispoNum.Text = usuaDis[0].IdDispositivo.NumerodelTelefono;
                        lblEstado.Text = usuaDis[0].UsuarioLlavePrimaria.EstadoInfo.EstadoNombre;
                    }
                }
                else
                {
                    List<TDI_UsuarioDispositivo> usuDispo = MngNegocioUsuarioDispositivo.ObtieneDispositivoPorUsuario(int.Parse(IdUsuario));
                    if (usuDispo.Count > 0)
                    {
                        lblNombreUsua.Text = usuDispo[0].UsuarioLlavePrimaria.UsuarioNombre;
                        lblDispoNum.Text = usuDispo[0].IdDispositivo.NumerodelTelefono;
                        lblEstado.Text = usuDispo[0].UsuarioLlavePrimaria.EstadoInfo.EstadoNombre;
                    }
                    else
                    {

                    }
                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "DispositivoInfo", int.Parse(Session["numeroUsuario"].ToString()));
            }
            finally { 
            
            }
        }
    }
}