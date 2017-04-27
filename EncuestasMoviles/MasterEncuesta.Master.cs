using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.UI;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;
using System.Web.UI.WebControls;

namespace EncuestasMoviles
{
    public partial class MasterEncuesta : System.Web.UI.MasterPage
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {     
   
            Response.AddHeader("Cache-Control", "no-store");
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Cache-Control", "no-cache, must-revalidate");
            Response.Expires = -1;
            if (Session["UserName"] == null || Session["UserName"].ToString() == "")
            {               
                Response.Redirect("~/Default.aspx");
            }

            List<THE_SesionUsuario> existeSesion =MngNegocioUsuarioSesion.VerExisteSesionUsuario(Int32.Parse(Session["numeroUsuario"].ToString()), Session["UserIP"].ToString());
            if (existeSesion.Count == 0)
            {
                Session["nombreUsuario"] = null;
                Session["numeroUsuario"] = null;
                Session["UserName"] = null;
                Session["UserDomain"] = null;
                Session["UserIP"] = null;
                Session["UserPuesto"] = null;
                Session["NomPuesto"] = null;
                ctrlMessageBox.AddMessage("Su sesion ha expirado", MessageBox.enmMessageType.Info, true, false, "Sesion", "Sesion Expirada");                
            }


            if (!Page.IsPostBack)
            {
                LlenaMenu();
            }
        }
        protected void Acepta_Evento(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }
        protected void z_btnAceptar_Click(object sender, EventArgs e)
        {
            ctrlMessageBox.AddMessage("Su sesion ha expirado", MessageBox.enmMessageType.Attention, true, true, "Sesion", "Sesion Expirada");
        }
        private void LlenaMenu()
        {
            try
            {
                List<TDI_Menu> mnu =MngNegocioMenu.ObtieneMenuPuesto(Session["UserPuesto"].ToString());

                foreach (TDI_Menu item in mnu)
                {
                    MenuItem menIT = new MenuItem();
                    if (item.MenuDepe == 0)
                    {
                        menIT.Text = item.MenuDesc;
                        menIT.Selectable = false;
                        //menIT.NavigateUrl = item.MenuUrl;
                        foreach (TDI_Menu ichild in mnu)
                        {
                            if (ichild.MenuDepe == item.MenuLlavPr)
                            {
                                MenuItem childMenu = new MenuItem();
                                childMenu.Text = ichild.MenuDesc;
                                childMenu.NavigateUrl = ichild.MenuUrl;
                                menIT.ChildItems.Add(childMenu);
                            }
                        }

                        MenuPrincipal.Items.Add(menIT);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        protected void btnRediPrincipal_Click(object sender, EventArgs e)
        {
            Response.Redirect("Principal.aspx");
        }       
        protected void CerrarSesion_Click(object sender, EventArgs e)
        {           
        }
        protected void lblClick(object sender, EventArgs e)
        {
            THE_SesionUsuario ObjSession = new THE_SesionUsuario();
            ObjSession.EmplLlavPr = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
            ObjSession.IdSesion = int.Parse(Session["IdSesion"].ToString());
            bool actualizo =MngNegocioUsuarioSesion.EliminaSesionUsuario(ObjSession);

           
            Session["UserName"] = null;
            Session["nombreUsuario"] = null;
            Session["numeroUsuario"] = null;
            Session["UserName"] = null;
            Session["UserDomain"] = null;
            Session["UserIP"] = null;
            Session["UserPuesto"] = null;
            Session["NomPuesto"] = null;

            Session.Abandon();
            Session.Clear();
           

            Response.Clear();

            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

           
            Response.Redirect("~/Default.aspx");

        }       
    }
}
