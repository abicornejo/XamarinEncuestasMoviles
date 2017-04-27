using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using EncuestasMoviles.Controls;
using System.Configuration;
using Azteca.Utility.Security;
using TVAzteca.Common.Utilities;
using System.IO;
using BLL_EncuestasMoviles;

namespace EncuestasMoviles.Pages
{
    public partial class frmDispositivos : System.Web.UI.Page
    {
        
        #region Variables        
        string rutImage;
        #endregion

        protected void btnCerrarAltDisp_Click(object sender, ImageClickEventArgs e)
        {
            mpeAltaDispositivo.Hide();
        }

        public frmDispositivos()
        {
            try
            {
                Load += new EventHandler(frmDispositivos_Load);

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void frmDispositivos_Load(object sender, EventArgs e)
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
                        CargaGrid();
                        ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
                }
            }
            catch (Exception ms)
            {

            }
            finally { 
            
            }
        }

        void ctrlMsj_RegistroEliminado(object sender, EventArgs e)
        {
            try
            {
                if (((bool)sender) == true)
                {
                    THE_Dispositivo elimiDispo = (THE_Dispositivo)ViewState["Dispositivo"];
                    elimiDispo.DispositivoEstatus = 'B';
                    bool Elimina = MngNegocioDispositivo.EliminaDispositivo(elimiDispo);
                    if (Elimina)
                    {
                        CargaGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void CargaNueva(object sender, EventArgs e)
        {
            try
            {
                CargaGrid();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        public Boolean ExisteTel_MEID_MED() {
            List<THE_Dispositivo> EncontreNumero = MngNegocioDispositivo.BusquedaDispositivoPorNumeroTel(txtNumeroTelefono.Text);
            List<THE_Dispositivo> EncontreMEID = MngNegocioDispositivo.BusquedaDispositivoPorMEID(txtMeidTelefono.Text);
            List<THE_Dispositivo> EncontreMDN = MngNegocioDispositivo.BusquedaDispositivoPorMDN(txtMdnTelefono.Text);
            if (EncontreNumero.Count > 0)
            {
                mpeAltaDispositivo.Show();
                DivErrorres.InnerText = "Ya existe el numero telefonico: " + txtNumeroTelefono.Text;
                return true;
            }
            if (EncontreMEID.Count > 0)
            {
                mpeAltaDispositivo.Show();
                DivErrorres.InnerText = "Ya existe el MEID: " + txtMeidTelefono.Text;
                return true;
            }
            if (EncontreMDN.Count > 0)
            {
                mpeAltaDispositivo.Show();
                DivErrorres.InnerText = "Ya existe el MDN: " + txtMdnTelefono.Text;
                return true;
            }
            return false;
        }

        protected void btnGuardaAltaDispositivo_Click(object sender, EventArgs e)
        {
            try
            {
               
                THE_Dispositivo dispo = new THE_Dispositivo();   
                string fn = "";
                if (btnGuardaAltaDispositivo.Text == "Guardar")
                {
                    List<THE_Dispositivo> EncontreNumero = MngNegocioDispositivo.BusquedaDispositivoPorNumeroTel(txtNumeroTelefono.Text.Trim());
                    List<THE_Dispositivo> EncontreMEID = MngNegocioDispositivo.BusquedaDispositivoPorMEID(txtMeidTelefono.Text.Trim());
                    List<THE_Dispositivo> EncontreMDN = MngNegocioDispositivo.BusquedaDispositivoPorMDN(txtMdnTelefono.Text.Trim());
                    if (EncontreNumero.Count > 0)
                    {
                        mpeAltaDispositivo.Show();
                        DivErrorres.InnerText = "Ya existe el numero telefonico: " + txtNumeroTelefono.Text.Trim();
                        return;
                    }
                    if (EncontreMEID.Count > 0)
                    {
                        mpeAltaDispositivo.Show();
                        DivErrorres.InnerText = "Ya existe el MEID: " + txtMeidTelefono.Text.Trim();
                        return;
                    }
                    if (EncontreMDN.Count > 0)
                    {
                        mpeAltaDispositivo.Show();
                        DivErrorres.InnerText = "Ya existe el MDN: " + txtMdnTelefono.Text.Trim();
                        return;
                    }
                }

                if (subeImagenTelefono.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(subeImagenTelefono.FileName);
                    fileExt = fileExt.ToUpper();
                    if (fileExt != ".JPEG" && fileExt != ".JPG" && fileExt != ".GIF" && fileExt != ".PNG")
                    {
                        DivErrorres.InnerText = "Ingrese una imagen valida con extencion  .jpeg| .jpg| .gif| .png ";
                        mpeAltaDispositivo.Show();
                        return;
                    }
                    else
                    {
                        subeImagenTelefono.PostedFile.SaveAs(Server.MapPath("~/temporal/") + subeImagenTelefono.FileName);
                        fn = subeImagenTelefono.FileName;
                    }
                }

                int Lada = 52;
                dispo.DispositivoDesc = txtDescTelefono.Text.Trim();
                dispo.DispositivoEstatus = 'A';
                dispo.DispositivoMdn = txtMdnTelefono.Text.Trim();
                dispo.DispositivoMeid = txtMeidTelefono.Text.Trim();
                dispo.Marca = txtMarcaTelefono.Text.Trim();
                dispo.Modelo = txtModeloTelefono.Text.Trim();
                dispo.NumerodelTelefono = Lada+txtNumeroTelefono.Text.Trim();
                dispo.IdDispositivo = 0;             
                
                if(fn != "")                
                    dispo.ImagenTelefono = fn;

                if (btnGuardaAltaDispositivo.Text == "Guardar")
                {
                    bool Guardado = MngNegocioDispositivo.GuardaAltaDispositivo(dispo);
                    SubeImagen(dispo.IdDispositivo.ToString(), fn);
                    ViewState["Opcion"] = "Correcto";
                    if (Guardado)
                    {
                        
                        GuardaLogTransacc("Se Crea Dispositivo " + dispo.IdDispositivo, 10);
                        ctrlMessageBox.AddMessage("Se ha Guardado Correctamente el Dispositivo " + dispo.DispositivoDesc, MessageBox.enmMessageType.Success, true, false, "Guarda", "Alta de Dispositivo");
                    }
                    else {
                        ctrlMessageBox.AddMessage("Erro al guardar dispositivo", MessageBox.enmMessageType.Error, true, false, "Error", "Erro de Insercion");                
                    }
                }
                else
                {
                    List<THE_Dispositivo> findNumero = MngNegocioDispositivo.BusquedaDispositivoPorNumeroTel(txtNumeroTelefono.Text.Trim(), int.Parse(txtIdDispo.Value));
                    List<THE_Dispositivo> findMEID = MngNegocioDispositivo.BusquedaDispositivoPorMEID(txtMeidTelefono.Text.Trim(), int.Parse(txtIdDispo.Value));
                    List<THE_Dispositivo> findMDN = MngNegocioDispositivo.BusquedaDispositivoPorMDN(txtMdnTelefono.Text.Trim(), int.Parse(txtIdDispo.Value));
                    if (findNumero.Count == 0)
                    {
                        List<THE_Dispositivo> findNumero2 = MngNegocioDispositivo.BusquedaDispositivoPorNumeroTel(txtNumeroTelefono.Text.Trim());
                        if (findNumero2.Count > 0)
                        {
                            mpeAltaDispositivo.Show();
                            DivErrorres.InnerText = "Ya existe el numero telefonico: " + txtNumeroTelefono.Text.Trim();
                            return;
                        }
                    }

                    if (findMEID.Count == 0)
                    {
                        List<THE_Dispositivo> findMEID2 = MngNegocioDispositivo.BusquedaDispositivoPorMEID(txtMeidTelefono.Text.Trim());
                        if (findMEID2.Count > 0)
                        {
                            mpeAltaDispositivo.Show();
                            DivErrorres.InnerText = "Ya existe el MEID: " + txtMeidTelefono.Text.Trim();
                            return;
                        }
                    }

                    if (findMDN.Count == 0)
                    {
                        List<THE_Dispositivo> findMDN2 = MngNegocioDispositivo.BusquedaDispositivoPorMDN(txtMdnTelefono.Text.Trim());
                        if (findMDN2.Count > 0)
                        {
                            mpeAltaDispositivo.Show();
                            DivErrorres.InnerText = "Ya existe el MDN: " + txtMdnTelefono.Text.Trim();
                            return;
                        }
                    }

                    dispo.IdDispositivo = int.Parse(txtIdDispo.Value);
                     
                    bool Actualiza = MngNegocioDispositivo.ActualizaDispositivo(dispo);
                    SubeImagen(dispo.IdDispositivo.ToString(), fn);
                    if (Actualiza)
                    {
                        ctrlMessageBox.AddMessage("Se ha Actualizado Correctamente el Dispositivo " + dispo.DispositivoDesc, MessageBox.enmMessageType.Success, true, false, "Guarda", "Actualiza Dispositivo");
                        ViewState["Opcion"] = "Correcto";
                        GuardaLogTransacc("Se Actualizo el Dispositivo " + dispo.IdDispositivo, 11);
                    }
                }                  
                
                LimpiaControles();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "AltaDispositivo", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        public void deleteFiles(string Directorio) {
            try
            {
                string[] files = System.IO.Directory.GetFiles(Directorio);
                foreach (string s in files)
                {
                    System.IO.File.Delete(s);
                }
            }
            catch (Exception ms)
            {

            }
            finally { 
            
            }
        
        }


        public void AltaEdicionDispositivo(THE_Dispositivo dispo, string opcion)
        {
            try
            {
                if (opcion == "Nuevo")
                {
                    lblTituloModalDispositivo.Text = "Alta de Dispositivo";
                    btnGuardaAltaDispositivo.Text = "Guardar";  
                }
                else
                {
                    lblTituloModalDispositivo.Text = "Edicion de Dispositivo";
                    btnGuardaAltaDispositivo.Text = "Actualiza";
                    txtDescTelefono.Text = dispo.DispositivoDesc;
                    txtMarcaTelefono.Text = dispo.Marca;
                    txtMdnTelefono.Text = dispo.DispositivoMdn;
                    txtMeidTelefono.Text = dispo.DispositivoMeid;
                    txtModeloTelefono.Text = dispo.Modelo;
                    txtNumeroTelefono.Text = dispo.NumerodelTelefono.Substring(2).Trim();
                    txtIdDispo.Value = dispo.IdDispositivo.ToString();
                    txtImgaDispo.Value = dispo.ImagenTelefono;
                }
                mpeAltaDispositivo.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "AltaDispositivo", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }


        public bool SubeImagen(string idTelefono, string nombrearchivo)
        {
            bool UpCorrecto = false;

            AppSettingsReader appRdr = new AppSettingsReader();
            Rijndael _ChyperRijndael = new Rijndael();
            string tmpPath = string.Empty;
            string destinationPath = string.Empty;

            destinationPath = _ChyperRijndael.Transmute(appRdr.GetValue("RutaArchivosServer", typeof(string)).ToString(), enmTransformType.intDecrypt);

            try
            {
                string fn = nombrearchivo;
                string directorio = Server.MapPath("~/temporal/");
                string SaveLocation = directorio + fn;

                FileComService filecom = new FileComService();
                filecom.CopyFile(SaveLocation, destinationPath + "\\Dispositivos\\" + idTelefono + "\\" + fn);
                filecom.DeleteFile(SaveLocation);

                UpCorrecto = true;
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "AltaDispositivo", int.Parse(Session["numeroUsuario"].ToString()));
            }

            return UpCorrecto;
        }

        void LimpiaControles()
        {
            txtDescTelefono.Text = string.Empty;
            txtMarcaTelefono.Text = string.Empty;
            txtMdnTelefono.Text = string.Empty;
            txtMeidTelefono.Text = string.Empty;
            txtModeloTelefono.Text = string.Empty;
            txtNumeroTelefono.Text = string.Empty;
            DivErrorres.InnerText = "";
        }

        protected void btnNuevoDispositivo_Click(object sender, EventArgs e)
        {
            try
            {
                lblTituloModalDispositivo.Text = "Alta de Dispositivo";
                LimpiaControles();
                mpeAltaDispositivo.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void CargaGrid()
        {
            try
            {
                List<THE_Dispositivo> Dispositivos = MngNegocioDispositivo.ObtieneTodosDispositivos();

                for (int ini = 0; ini < Dispositivos.Count; ini++)
                {
                    if (Dispositivos[ini].ImagenTelefono == null)                       
                        Dispositivos[ini].ImagenTelefono=  "../images/no_foto.jpg";
                    else
                        Dispositivos[ini].ImagenTelefono = "../Media/Dispositivos/" + Dispositivos[ini].IdDispositivo + "/" + Dispositivos[ini].ImagenTelefono; 
                }
                gvAltaDispositivo.DataSource = Dispositivos;
                Session["datasource"] = Dispositivos;
                gvAltaDispositivo.DataBind();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void gvAltaDispositivo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Elimina")
                {
                    ctrlMessageBox.AddMessage("¿Esta seguro que desea eliminar el Dispositivo Seleccionado?", MessageBox.enmMessageType.Attention, true, true, "prueba", "Elimina Dispositivo");
                    ViewState["Opcion"] = "Elimina Dispositivo";
                    ViewState["IDElimina"] = int.Parse(e.CommandArgument.ToString());
                }
                if (e.CommandName == "Editar")
                {
                    int IdEdita = int.Parse(e.CommandArgument.ToString());
                    lblTituloModalDispositivo.Text = "Edicion de Dispositivo";

                    List<THE_Dispositivo> usuario = Session["datasource"] as List<THE_Dispositivo>;
                   
                    THE_Dispositivo usua = usuario[IdEdita] as THE_Dispositivo;
                        
                    AltaEdicionDispositivo(usua, "Edita");
                    
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void Acepta_Evento(object sender, EventArgs e)
        {
            try
            {
                string Opcion = ViewState["Opcion"].ToString();

                if (Opcion == "Elimina Dispositivo")
                {
                    List<THE_Dispositivo> dispositivo = Session["datasource"] as List<THE_Dispositivo>;
                    THE_Dispositivo dispo = dispositivo[int.Parse(ViewState["IDElimina"].ToString())] as THE_Dispositivo;
                    dispo.DispositivoEstatus = 'B';

                    bool EliminaDispositivo = MngNegocioDispositivo.EliminaDispositivo(dispo);

                    if (EliminaDispositivo)
                    {
                        ctrlMessageBox.AddMessage("Se Eliminado Correctamente el Dispositivo " + dispo.DispositivoDesc, MessageBox.enmMessageType.Success, true, false, "Elimina", "Elimina Dispositivo");
                        ViewState["Opcion"] = "Correcto";
                        GuardaLogTransacc("Se Elimino Dispositivo " + dispo.IdDispositivo, 12);
                    }
                }

                if (Opcion == "Correcto")
                {                   
                    CargaGrid();
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmDispositivos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        public void ctrlMessageBox_MsgBoxAnswered(object sender, MessageBox.MsgBoxEventArgs e)
        {
            if (e.Answer == MessageBox.enmAnswer.OK)
            {
            }
            else
            {
                ctrlMessageBox.AddMessage("Ha Cancelado la Operación", MessageBox.enmMessageType.Info, "Prueba");
            }
        }

        public void GuardaLogTransacc(string Desc, int IdTran)
        {
            THE_LogTran oLogTran = new THE_LogTran();
            oLogTran.LogtDesc = Desc;
            oLogTran.LogtDomi = Session["UserDomain"].ToString();
            oLogTran.LogtFech = DateTime.Now;
            oLogTran.LogtMach = Session["userMachineName"].ToString();
            oLogTran.LogtUsIp = Session["UserIP"].ToString();
            oLogTran.LogtUsua = Session["UserName"].ToString();
            oLogTran.TranLlavPr = new TDI_Transacc() { TranLlavPr = IdTran };
            oLogTran.EmplLlavPr = new THE_Empleado() { EmpleadoLlavePrimaria = int.Parse(Session["numeroUsuario"].ToString()) };

            MngNegocioTransacciones.GuardaLogTransaccion(oLogTran);
        }
    }
}
