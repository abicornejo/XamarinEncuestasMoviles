using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using EncuestasMoviles.Controls;
using BLL_EncuestasMoviles;

namespace EncuestasMoviles.Controls
{
    public partial class ctrlNewCatalogo : System.Web.UI.UserControl
    {
        #region Variables
       
        AjaxControlToolkit.ModalPopupExtender mpeMensajes = new AjaxControlToolkit.ModalPopupExtender();
        public event EventHandler EventoBotonClick;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {

                    Response.Redirect("~/Default.aspx");
                }
                else 
                {
                    List<THE_SesionUsuario> existeSesion =MngNegocioUsuarioSesion.VerExisteSesionUsuario(Int32.Parse(Session["numeroUsuario"].ToString()), (Session["UserIP"].ToString()));
                    if (existeSesion.Count == 0)
                    {
                        Session["nombreUsuario"] = null;
                        Session["numeroUsuario"] = null;
                        Session["UserName"] = null;
                        Session["UserDomain"] = null;
                        Session["UserIP"] = null;
                        Session["UserPuesto"] = null;
                        Session["NomPuesto"] = null;
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }
            catch
            {

            }
            finally { 
            
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if(EventoBotonClick!=null)
            {
                //Accion.Value=1 = Nuevo Registro
                if (int.Parse(Accion.Value) == 1)
                {
                    THE_Catalogo catalog = new THE_Catalogo();
                    catalog.CatalogoDesc = txtNomCat.Text;
                    catalog.CatalogoStat = 'A';
                    bool Guardado =MngNegocioCatalogo.GuardaCatalogo(catalog);
                    if (Guardado)
                    {

                        EventoBotonClick("SaveOk", e);
                    }
                }
                else
                {
                    //Accion.Value=2 = Edita Registro
                    EventoBotonClick(txtNomCat.Text, e);
                }
            }            
        }

        protected void Acepta_Evento(object sender, EventArgs e)
        {

        }
    }
}