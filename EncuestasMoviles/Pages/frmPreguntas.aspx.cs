using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;

namespace EncuestasMoviles.Pages
{
    public partial class frmPreguntas : System.Web.UI.Page
    {
        WebService_EncuestasMoviles client = new WebService_EncuestasMoviles();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            string NombreEncuesta = client.ObtieneEncuestaPorID(1)[0].NombreEncuesta;
            List<THE_Preguntas> lst = client.ObtienePreguntasPorEncuesta(1);
            Grid.DataSource = lst;
            Grid.DataBind();
            lblTituEncuesta.InnerText = NombreEncuesta;
        }
    }
}