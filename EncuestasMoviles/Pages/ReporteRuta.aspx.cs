﻿using System;
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

namespace EncuestasMoviles.Pages
{
    public partial class ReporteRuta : System.Web.UI.Page
    {
        string script = "";
        List<string> arreglo = new List<string>();
        List<string> arregloH = new List<string>();
        List<string> arregloDatos = new List<string>();
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
                    trExportar.Visible = false;
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

                    this.txtCalendario.SelectedDate = DateTime.Now.AddDays(-7);
                    this.txtFechaFinal.SelectedDate = DateTime.Now;
                    FillDropDown(ref ddlEmpleados, dt, "UsuarioLlavePrimaria", "UsuarioNombre", true);
                    btnExportar.Visible = true;
                }
              
            }
            catch (Exception ms) { }
            finally { }

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

        private List<string> ExtraeArrayCoordenadasDatos(DataTable vDt)
        {
            if (vDt.Rows.Count > 0)
            {
                for (int i = 0; i < vDt.Rows.Count; i++)
                {
                    arregloDatos.Add(vDt.Rows[i][0].ToString());
                }
            }
            return arregloDatos;
        }

        protected void btnExporta_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnConsultar_Click1(object sender, EventArgs e)
        {
            arrayCoordenadas.Value = "";
            arrayDatos.Value = "";
            int c = 0;

            List<TDI_UbicacionDispositivo> Listcoordenadas = MngNegocioDispositivo.ObtieneCoordenadasDispositivo(int.Parse(ddlEmpleados.SelectedValue), Convert.ToDateTime(txtCalendario.SelectedDate).ToString("dd/MM/yyyy"), Convert.ToDateTime(txtFechaFinal.SelectedDate).ToString("dd/MM/yyyy"));

            DataTable dtCoor = new DataTable();
            DataTable dtDatos = new DataTable();
            
            dtCoor.Columns.Add(new DataColumn("Coordenadas"));
            dtDatos.Columns.Add(new DataColumn("Datos"));
            foreach (TDI_UbicacionDispositivo coordenada in Listcoordenadas)
            {
                DataRow row = dtCoor.NewRow();
                DataRow row2 = dtDatos.NewRow();
                row[0] = coordenada.Latitud + "," + coordenada.Longitud;
                row2[0] = coordenada.IdUsuario.UsuarioFoto + "," + coordenada.IdDispositivo.ImagenTelefono + "," + coordenada.IdUsuario.UsuarioLlavePrimaria + "," + coordenada.IdDispositivo.IdDispositivo + "," + coordenada.IdUsuario.UsuarioNombre + "," + coordenada.IdDispositivo.NumerodelTelefono.Substring(2);
                dtCoor.Rows.Add(row);
                dtDatos.Rows.Add(row2);
            }


            string[] arrIdAlias = ddlEmpleados.SelectedValue.Trim().ToUpper().Split('-');
            string[] dt = ExtraeArrayCoordenadas(dtCoor).ToArray();
            string[] data = ExtraeArrayCoordenadasDatos(dtDatos).ToArray();

            if (dt != null && dt.Length > 0)
            {
                for (int cc = 0; cc < dt.Length; cc++)
                {
                    if (c == dt.Length - 1)
                    {
                        arrayCoordenadas.Value += dt[cc].ToString();
                        arrayDatos.Value += data[cc].ToString();
                    }
                    else
                    {
                        arrayCoordenadas.Value += dt[cc].ToString() + '&';
                        arrayDatos.Value += data[cc].ToString() + '&';
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