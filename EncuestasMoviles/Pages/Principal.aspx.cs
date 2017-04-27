using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;
using System.Collections;

namespace EncuestasMoviles.Pages
{
    public partial class Principal : System.Web.UI.Page
    {
        public void ClearApplicationCache()
        {
            List<string> keys = new List<string>();

            // retrieve application Cache enumerator
            IDictionaryEnumerator enumerator = Cache.GetEnumerator();

            // copy all keys that currently exist in Cache
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

            // delete every key from cache
            for (int i = 0; i < keys.Count; i++)
            {
                Cache.Remove(keys[i]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    ClearApplicationCache();
                   

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
                }
                else { 
                
                }
            }
            catch (Exception ms) { }
            finally { }
        }
    }
}