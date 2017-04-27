using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL_EncuestasMoviles;
using Entidades_EncuestasMoviles;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace EncuestasMoviles.Pages
{
    public partial class FrmProgramacionDispos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //cargaProgramaciones();
                //cargaDispositivosProgramadios();
                //int idEnc = 0;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptA", "showModal();", true);
                //for (int i = 0; i < gvProgra.Rows.Count; i++)
                //{
                //    RadioButton chbTemp = gvProgra.Rows[i].FindControl("rdoPrograDispo") as RadioButton;

                //    if (chbTemp.Checked)
                //    {
                //        var colsNoVisible = gvProgra.DataKeys[i].Values;

                //        idEnc = (int)gvProgra.DataKeys[i][1];                       
                //        break;
                //    }
                //}
                     
                //cargaDispositivos(idEnc.ToString());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "scriptA", "hideModal();", true);

            }
           
        }

        protected void radio_cheked(object sender, EventArgs e) {
            //using (GridViewRow row = (GridViewRow)((RadioButton)sender).Parent.Parent)
            //{
            //    try
            //    {
            //        var colsNoVisible = gvProgra.DataKeys[row.RowIndex].Values;
            //        string idEnc=colsNoVisible[1].ToString();
            //        cargaDispositivos(idEnc);
            //    }
            //    catch (Exception ms)
            //    { }
            //}
            
        }


        public void cargaProgramaciones() {
            //List<THE_Programacion> Programaciones = MngNegocioProgramacion.ObtieneProgramaciones("");
            //gvProgra.DataSource = Programaciones;
            //gvProgra.DataBind();
        }


        [WebMethod]
        public static string getProgramaciones() {
            List<THE_Programacion> Programaciones = MngNegocioProgramacion.ObtieneProgramaciones("");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(Programaciones);
            return resultjson; 
        }
        [WebMethod]
        public static string getDispoByProgramacion(string idEncuesta)
        {
            List<TDI_EncuestaDispositivo> lstTipoResp = MngNegocioEncuestaDispositivo.ObtieneDispositivosActivos(idEncuesta);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(lstTipoResp);
            return resultjson;
        }

        [WebMethod]
        public static string getDispositivosProgramados()
        {
            List<THE_PrograDispositivo> lstPrograDispo = MngNegocioProgramacion.ObtieneDispositivosProgramados();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(lstPrograDispo);
            return resultjson;
        }

        [WebMethod]
        public static string asignarDispoProgramado(string idProgram, string idDispo, string idEnc, string idTipoProgram)
        { 
            Boolean guardo=false;
            if (!MngNegocioProgramacion.existeDispoInProgramacion(idProgram.ToString(), idDispo.ToString(), idEnc.ToString(), idTipoProgram.ToString()))
            {
                THE_PrograDispositivo objPrograDispo = new THE_PrograDispositivo();
                objPrograDispo.ID_DISPOSITIVO = new THE_Dispositivo() { IdDispositivo =Convert.ToInt32(idDispo) };
                objPrograDispo.ID_ENCUESTA = new THE_Encuesta() { IdEncuesta = Convert.ToInt32(idEnc) };
                objPrograDispo.ID_PROGRAMACION = new THE_Programacion() { IdProgramacion = Convert.ToInt32(idProgram) };
                objPrograDispo.ID_TIPOPROGRAMACION = new TDI_TipoProgramacion() { IdTipoProgramacion =Convert.ToInt32(idTipoProgram) };
                objPrograDispo.ESTATUS = 'A';
                guardo = MngNegocioProgramacion.AgregaDispositivoProgramados(objPrograDispo);
                                
            }
            return guardo.ToString();
        }

        [WebMethod]
        public static string getDispoProgramedByProgram(string idProgramacion) {
            List<THE_PrograDispositivo> lstPrograDispo = MngNegocioProgramacion.ObtenDispoProgramadosByProgramacion(idProgramacion);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string resultjson = "";
            resultjson = serializer.Serialize(lstPrograDispo);
            return resultjson;
        }

        [WebMethod]
        public static string eliminaDispoByProgram(string idProgramacion, string idEncuesta, string idDispositivo, string idTipoProgramacion, string IdProDispo)
        {
            Boolean guardo=false;
            THE_PrograDispositivo objPrograDispo = new THE_PrograDispositivo();
            objPrograDispo.ID_DISPOSITIVO = new THE_Dispositivo() { IdDispositivo = Convert.ToInt32(idDispositivo) };
                    objPrograDispo.ID_ENCUESTA = new THE_Encuesta() { IdEncuesta = Convert.ToInt32(idEncuesta) };
                    objPrograDispo.ID_PROGRAMACION = new THE_Programacion() { IdProgramacion = Convert.ToInt32(idProgramacion) };
                    objPrograDispo.ID_TIPOPROGRAMACION = new TDI_TipoProgramacion() { IdTipoProgramacion = Convert.ToInt32(idTipoProgramacion) };
                    objPrograDispo.ID_PRO_DISPO = Convert.ToInt32(IdProDispo);
                    objPrograDispo.ESTATUS = 'B';
            guardo = MngNegocioProgramacion.EliminaDispositivoProgramados(objPrograDispo);
            return guardo.ToString();
        }

        public void cargaDispositivos(string idEncuesta) {
            List<TDI_EncuestaDispositivo> lstTipoResp = MngNegocioEncuestaDispositivo.ObtieneDispositivosActivos(idEncuesta);
           // gvDispositivos.DataSource = lstTipoResp;
           // gvDispositivos.DataBind();
        }
        public void cargaDispositivosProgramadios()
        {
            List<THE_PrograDispositivo> lstPrograDispo = MngNegocioProgramacion.ObtieneDispositivosProgramados();
            //gvPrograDispo.DataSource = lstPrograDispo;
            //gvPrograDispo.DataBind();

        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        int idProgram = 0;
        //        int inserccionesFallidas = 0;
        //        int idEnc = 0;
        //        int idTipoProgram = 0;
        //        int band = 0, contador=0;
        //        for (int i = 0; i < gvProgra.Rows.Count; i++)
        //        {
        //            RadioButton chbTemp = gvProgra.Rows[i].FindControl("rdoPrograDispo") as RadioButton;
        //            if (chbTemp.Checked)
        //            {
        //                var colsNoVisible = gvProgra.DataKeys[i].Values;

        //                idEnc = (int)gvProgra.DataKeys[i][1];
        //                idTipoProgram =(int)gvProgra.DataKeys[i][2];
        //                idProgram = (int)gvProgra.DataKeys[i][0];
        //                break;
        //            }
        //        }
        //        if (idProgram != 0)
        //        {
        //            for (int i = 0; i < gvDispositivos.Rows.Count; i++)
        //            {
        //                CheckBox chbTemp = gvDispositivos.Rows[i].FindControl("checkPrograDispo") as CheckBox;
        //                if (chbTemp.Checked)
        //                {
                            
        //                    int idDispo = (int)gvDispositivos.DataKeys[i][0];

        //                    if (!MngNegocioProgramacion.existeDispoInProgramacion(idProgram.ToString(), idDispo.ToString(), idEnc.ToString(), idTipoProgram.ToString()))
        //                    {
        //                        THE_PrograDispositivo objPrograDispo = new THE_PrograDispositivo();
        //                        objPrograDispo.ID_DISPOSITIVO = new THE_Dispositivo() { IdDispositivo = idDispo };
        //                        objPrograDispo.ID_ENCUESTA = new THE_Encuesta() { IdEncuesta = idEnc };
        //                        objPrograDispo.ID_PROGRAMACION = new THE_Programacion() { IdProgramacion = idProgram };
        //                        objPrograDispo.ID_TIPOPROGRAMACION = new TDI_TipoProgramacion() { IdTipoProgramacion = idTipoProgram };
        //                        objPrograDispo.ESTATUS = 'A';
        //                        Boolean guardo = MngNegocioProgramacion.AgregaDispositivoProgramados(objPrograDispo);
        //                        if (guardo)
        //                        {
        //                            chbTemp.Checked = false;
        //                            contador++;
        //                        }
        //                        else
        //                        {
        //                            inserccionesFallidas++;
        //                        }
        //                    }
        //                    else {
        //                        band++;
        //                        chbTemp.Checked = false;
        //                    }
        //                }
        //            }
                   
        //            if (inserccionesFallidas > 0)
        //            {  
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('error','Hubo Asignaciones fallidas, intentelo de nuevo.');", true);
                   
        //            }
        //            else
        //            {
        //               // cargaDispositivos();
        //            }
        //            cargaProgramaciones();
        //            cargaDispositivosProgramadios();

        //            if (contador > 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('exito','Asignaciones guardadas satisfactoriamente');", true);
        //            }
                
        //        }
        //        else {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('info','Seleccione una programacion');", true);
        //        }
        //    }
        //    catch (Exception ms)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('error','" + ms.Message + "', intentelo de nuevo.');", true);
                  
        //    }
        }

        protected void Edit(object sender, EventArgs e)
        {
            //using (GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            //{
            //    try
            //    {
            //        var colsNoVisible = gvPrograDispo.DataKeys[row.RowIndex].Values;

            //        THE_PrograDispositivo objPrograDispo = new THE_PrograDispositivo();
            //        objPrograDispo.ID_DISPOSITIVO = new THE_Dispositivo() { IdDispositivo = Convert.ToInt32(colsNoVisible[5]) };
            //        objPrograDispo.ID_ENCUESTA = new THE_Encuesta() { IdEncuesta = Convert.ToInt32(colsNoVisible[3]) };
            //        objPrograDispo.ID_PROGRAMACION = new THE_Programacion() { IdProgramacion = Convert.ToInt32(colsNoVisible[0]) };
            //        objPrograDispo.ID_TIPOPROGRAMACION = new TDI_TipoProgramacion() { IdTipoProgramacion = Convert.ToInt32(colsNoVisible[4]) };
            //        objPrograDispo.ID_PRO_DISPO = Convert.ToInt32(colsNoVisible[2]);
            //        objPrograDispo.ESTATUS = 'B';
             // Boolean guardo = MngNegocioProgramacion.EliminaDispositivoProgramados(objPrograDispo);
            //        if (guardo)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('exito','Eliminacion generada satisfactoriamente');", true);
            //        }
            //        else
            //        {
                     
            //          ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('error','Error al momento de eliminar dispositivo, intentelo de nuevo', intentelo de nuevo.');", true);
            //        }
            //       // cargaDispositivos();
            //        cargaProgramaciones();
            //        cargaDispositivosProgramadios();

            //    }
            //    catch (Exception ms)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('error','" + ms.Message + "', intentelo de nuevo.');", true);
           
            //    }
            //}
        }

        protected void Visualizar(object sender, EventArgs e)
        {
            //using (GridViewRow row = (GridViewRow)((ImageButton)sender).Parent.Parent)
            //{
            //    try
            //    {
                    
            //        var cols = gvProgra.DataKeys[row.RowIndex].Values;


            List<THE_PrograDispositivo> lstPrograDispo = MngNegocioProgramacion.ObtenDispoProgramadosByProgramacion("idprogramacion");
            //        gvPrograDispoPreview.DataSource = lstPrograDispo;
            //        gvPrograDispoPreview.DataBind();

            //        if (lstPrograDispo.Count > 0)
            //        {
            //            lblDescProgramacion.Text="PROGRAMACION: "+cols[3].ToString();
            //            popupDisposAsignados.Show();
            //        }
            //        else {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('info','No existen dispositivos asignados para esta programacion');", true);
                      
            //        }

                   
                  
            //    }
            //    catch (Exception ms)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "showMensaje('error','" + ms.Message + "', intentelo de nuevo.');", true);                 
                     
            //    }

                
            //}
        }


    }
}