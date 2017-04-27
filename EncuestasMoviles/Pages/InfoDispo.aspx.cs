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
    public partial class InfoDispo : System.Web.UI.Page
    {
       
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
                        List<THE_Dispositivo> dispo = MngNegocioDispositivo.ObtieneDispositivoPorID(int.Parse(IdDispositivo));

                        lblDispoDesc.Text = dispo[0].DispositivoDesc;
                        lblDispoMarca.Text = dispo[0].Marca;
                        lblDispoMdn.Text = dispo[0].DispositivoMdn;
                        lblDispoMeid.Text = dispo[0].DispositivoMeid;
                        lblDispoModelo.Text = dispo[0].Modelo;
                        lblDispoNum.Text = dispo[0].NumerodelTelefono;
                    }
                    else
                    {
                        List<TDI_UsuarioDispositivo> usuDispo =MngNegocioUsuarioDispositivo.ObtieneDispositivoPorUsuario(int.Parse(IdUsuario));
                        if (usuDispo.Count > 0)
                        {

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
            }
            catch (Exception ms) { }
            finally { }
        }
    }
}