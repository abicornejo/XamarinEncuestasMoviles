using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;

namespace EncuestasMoviles.Pages.Reportes
{
    public partial class CoordenadasPrueba3 : System.Web.UI.Page
    {
        string script = "";
        List<string> arreglo = new List<string>();
        List<string> arregloH = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Response.Redirect("~/Default.aspx");
                }

                if (!IsPostBack)
                {
                    txtCalendario.DatePopupButton.Attributes.Add("onclick", "PopupLoc(event, '" + txtCalendario.ClientID + "');return false;"); 
                    TDI_LogPaginas logPaginas = new TDI_LogPaginas();
                    logPaginas.LogFecha = DateTime.Now;
                    logPaginas.LogIp = Session["UserIP"].ToString();
                    logPaginas.LogUrlPagina = Request.RawUrl;
                    logPaginas.EmpleadoLlavePrimaria = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                    MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);
                    trExportar.Visible = false;
                    List<TDI_UsuarioDispositivo> Usuarios= MngNegocioUsuarioDispositivo.ObtieneUsuariosConDispositivoAsignado();
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("UsuarioNombre"));
                    dt.Columns.Add(new DataColumn("UsuarioLlavePrimaria"));
                    foreach (TDI_UsuarioDispositivo dispo in Usuarios)
                    {
                        DataRow nuevo = dt.NewRow();
                        nuevo[0] = dispo.UsuarioLlavePrimaria.UsuarioNombre;
                        nuevo[1] = dispo.UsuarioLlavePrimaria.UsuarioLlavePrimaria;
                        dt.Rows.Add(nuevo);
                    }

                    this.txtCalendario.SelectedDate = DateTime.Now.AddDays(-7);
                    this.txtFechaFinal.SelectedDate = DateTime.Now;
                    FillDropDown(ref ddlEmpleados, dt, "UsuarioLlavePrimaria", "UsuarioNombre", true);
                    btnExportar.Visible = true;
                }
            }
            catch(Exception ms) {
            
            }finally{
            
            
            }
        }
        private void FillDropDown(ref DropDownList ddl, DataTable dtSource, string DataValue, string TextValue, bool BlankRegister)
        {
            ddl.DataSource = dtSource;
            ddl.DataTextField = TextValue;
            ddl.DataValueField = DataValue;
            ddl.DataBind();
            if (BlankRegister)
            {
                ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --- Todos los Registros --- ", "-1"));
            }
        }
        private void ConsultaDireccionesHospitales()
        {
            int c = 0;

            DataTable dtCoor = new DataTable();

            dtCoor.Columns.Add(new DataColumn("Coordenadas"));
            DataRow row = dtCoor.NewRow();
            row[0] = "19.4595719,-99.2158663";
            dtCoor.Rows.Add(row);

            row = dtCoor.NewRow();
            row[0] = "19.4587829,-99.203158";
            dtCoor.Rows.Add(row);


            row = dtCoor.NewRow();
            row[0] = "19.4700922,-99.1905999";
            dtCoor.Rows.Add(row);


            row = dtCoor.NewRow();
            row[0] = "19.479226,-99.19016";
            dtCoor.Rows.Add(row);

            string[] dt = ExtraeDirecciones(dtCoor).ToArray();
            if (dt != null && dt.Length > 0)
            {
                for (int cc = 0; cc < dt.Length; cc++)
                {
                    if (c == dt.Length - 1)
                    {
                        arrayCoordenadasH.Value += dt[cc].ToString();
                    }
                    else
                    {
                        arrayCoordenadasH.Value += dt[cc].ToString() + '&';
                        c++;
                    }
                }
            }

        }
        
        private List<string> ExtraeDirecciones(DataTable vDt)
        {
            if (vDt.Rows.Count > 0)
            {
                for (int i = 0; i < vDt.Rows.Count; i++)
                {
                    arregloH.Add(vDt.Rows[i][0].ToString());
                }
            }
            return arregloH;
        }
        private List<string> ExtraeArrayCoordenadas(DataTable vDt)
        {
            if (vDt.Rows.Count > 0)
            {
                for (int i = 0; i < vDt.Rows.Count; i++)
                {
                     arreglo.Add(vDt.Rows[i][0].ToString());
                }
            }
            return arreglo;
        }
        protected void btnExporta_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnConsultar_Click1(object sender, EventArgs e)
        {
            arrayCoordenadas.Value = "";
            int c = 0;

            List<TDI_UbicacionDispositivo> Listcoordenadas = MngNegocioDispositivo.ObtieneCoordenadasDispositivo(int.Parse(ddlEmpleados.SelectedValue), Convert.ToDateTime(txtCalendario.SelectedDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtFechaFinal.SelectedDate).ToString("dd/MM/yyyy"));

            DataTable dtCoor = new DataTable();
            dtCoor.Columns.Add(new DataColumn("Coordenadas"));
            foreach (TDI_UbicacionDispositivo coordenada in Listcoordenadas)
            {
                DataRow row = dtCoor.NewRow();
                row[0] = coordenada.Latitud + "," + coordenada.Longitud;
                dtCoor.Rows.Add(row);
            }


            string[] arrIdAlias = ddlEmpleados.SelectedValue.Trim().ToUpper().Split('-');
            string[] dt = ExtraeArrayCoordenadas(dtCoor).ToArray();
            if (dt != null && dt.Length > 0)
            {
                for (int cc = 0; cc < dt.Length; cc++)
                {
                    if (c == dt.Length - 1)
                    {
                        arrayCoordenadas.Value += dt[cc].ToString();
                    }
                    else
                    {
                        arrayCoordenadas.Value += dt[cc].ToString() + '&';
                        c++;
                    }
                }
            }
            if (arrayCoordenadas.Value != "")
            {
                trExportar.Visible = false;
                string script = " pintaLineaMapa();";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pinta_scripts", script, true);
            }
            else
            {
                script = " showMessage_Info('No existen registros con la busqueda especificada');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err_cmp_scripts", script, true);
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            
        }
    }
}