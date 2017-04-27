using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;

namespace EncuestasMoviles.Controls
{
    public partial class ctrlOpcionCatalogo : System.Web.UI.UserControl
    {
        #region Variables
       
        public event EventHandler EventoBotonOpcionesClick;
        AjaxControlToolkit.ModalPopupExtender mpeMensajes = new AjaxControlToolkit.ModalPopupExtender();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch(Exception ms)
            {

            }
            finally
            {

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (EventoBotonOpcionesClick != null)
            {
                //Accion.Value=1 = Nuevo Registro
                if (int.Parse(Accion.Value) == 1)
                {
                    TDI_OpcionCat opCatalogo = new TDI_OpcionCat();
                    opCatalogo.OpcionCatDesc = txtNomOpcCat.Text;
                    opCatalogo.OpcionCatStat = 'A';

                    List<THE_Catalogo> Catalogo =MngNegocioCatalogo.ObtieneCatalogoPorId(int.Parse(IdCatalogo.Value));
                    opCatalogo.IdCatalogo = Catalogo[0];

                    bool Guardar =MngNegocioOpcionCat.GuardaOpcionporCatalogo(opCatalogo);

                    EventoBotonOpcionesClick("", e);
                }
                else
                {
                    //Accion.Value=2 = Edita Registro
                    EventoBotonOpcionesClick(txtNomOpcCat.Text, e);
                }
            }
        }

        protected void Acepta_Evento(object sender, EventArgs e)
        {
            
        }
    }
}