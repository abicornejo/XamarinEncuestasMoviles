using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;
using System.Data;

namespace EncuestasMoviles.Pages.Reportes
{
    public partial class coordenadaspruebas2 : System.Web.UI.Page
    {
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
                    TDI_LogPaginas logPaginas = new TDI_LogPaginas();
                    logPaginas.LogFecha = DateTime.Now;
                    logPaginas.LogIp = Session["UserIP"].ToString();
                    logPaginas.LogUrlPagina = Request.RawUrl;
                    logPaginas.EmpleadoLlavePrimaria = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };
                    MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);

                    List<TDI_UsuarioDispositivo> Usuarios = MngNegocioUsuarioDispositivo.ObtieneUsuariosConDispositivoAsignado();
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
                    FillDropDown(ref ddlEmpleados, dt, "UsuarioLlavePrimaria", "UsuarioNombre", true);
                }
            }
            catch (Exception ms)
            {

            }
            finally { 
            
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
                ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" --- Seleccione un registro --- ", "-1"));
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (ddlEmpleados.SelectedIndex > 0)
            {

                List<TDI_UbicacionDispositivo> Listcoordenadas = MngNegocioDispositivo.ObtieneCoordenadasDispositivo(int.Parse(ddlEmpleados.SelectedValue), DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"));

                DataTable dtCoor = new DataTable();
                dtCoor.Columns.Add(new DataColumn("Coordenadas"));
                int ultimo = 0;
                TDI_UbicacionDispositivo ultimoRow = null;
                foreach (TDI_UbicacionDispositivo coordenada in Listcoordenadas)
                {
                    if(ultimo==Listcoordenadas.Count-1)
                    {
                        ultimoRow=coordenada; 
                    }
                    ultimo++;
                }
                if (ultimoRow!=null)
                {
                    string script = " muestraPosicion('" + ultimoRow.Latitud + "','" + ultimoRow.Longitud + "','" + ultimoRow.IdDispositivo.NumerodelTelefono + "','" + ultimoRow.IdUsuario.UsuarioNombre + "','" + ultimoRow.IdUsuario.UsuarioFoto + "','" + ultimoRow.IdDispositivo.ImagenTelefono + "');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "pinta_scripts", script, true);
                }
                else
                {
                    string script = " showMessage_Info('No existen registros con la busqueda especificada');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "err_cmp_scripts", script, true);
                }
            }
            else
            {
                string script = " showMessage_Info('Debe elegir un empleado a consultar');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "err_cmp_scripts", script, true);
            }
        }
    }
}
