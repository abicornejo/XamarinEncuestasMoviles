using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using EncuestasMoviles.Controls;
using System.Data;
using BLL_EncuestasMoviles;
using Azteca.Utility.Security;
using AjaxControlToolkit;
using System.Configuration;
using TVAzteca.Common.Utilities;
using Telerik.Web.UI;
using System.Text;
using System.IO;
using System.Web.UI.HtmlControls;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Services;
using System.Web.Script.Serialization;

namespace EncuestasMoviles.Pages
{
    public partial class frmUsuarios : System.Web.UI.Page
    {
        #region Variables
        int UsuarioLlavePrimaria;        
        HtmlTextWriterStyle style = HtmlTextWriterStyle.Display;
        string RutaFoto;
        GridView[] grid;
        #endregion

        public class GridViewTemplate : ITemplate
        {
            private DataControlRowType templateType;
            private List<TDI_OpcionCat> LstOpcioCatalogo;

            public GridViewTemplate(DataControlRowType type, List<TDI_OpcionCat> lstOpcioCatalogo)
            {
                templateType = type;
                LstOpcioCatalogo = lstOpcioCatalogo;
            }

            public void InstantiateIn(System.Web.UI.Control container)
            {
                // Create the content for the different row types.
                switch (templateType)
                {                    
                    case DataControlRowType.DataRow:
                        // Create the controls to put in a data row
                        // section and set their properties.
                        RadioButtonList rbl = new RadioButtonList();
                        rbl.ID = "listaradios";
                        foreach (TDI_OpcionCat op in LstOpcioCatalogo)
                        {
                            rbl.Items.Add(new System.Web.UI.WebControls.ListItem(op.OpcionCatDesc, op.IdOpcionCat.ToString()));
                            rbl.SelectedIndex = 0;
                        }                        
                        
                        container.Controls.Add(rbl);
                        break;

                    // Insert cases to create the content for the other 
                    // row types, if desired.

                    default:
                        // Insert code to handle unexpected values.
                        break;
                }
            }
        }
                    
        public frmUsuarios()
        {
           
            try
            {
                Load += new EventHandler(frmUsuarios_Load);
                //subeFotoUsuario.FileContent.
                
            }
            catch(Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void frmUsuarios_Load(object sender, EventArgs e)
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
                        txtFechNacimiento.SelectedDate = DateTime.Now;
                        MngNegocioLogPaginas.GuardarLogPaginas(logPaginas);
                        CargaGrid();
                        CargaDatosCatalogos();
                        ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                    }
                    else
                    {
                        
                        CargaDatosCatalogos();

                        ctrlMessageBox.MsgBoxAnswered += new MessageBox.MsgBoxEventHandler(ctrlMessageBox_MsgBoxAnswered);
                    }
                }
                catch (Exception ex)
                {
                    EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["numeroUsuario"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
                }
            }
            catch (Exception ms)
            {
            }
            finally { 
            
            }
        }

        void ctrlAgregaEdita_ActualizacionCorrecta(object sender, EventArgs e)
        {
            try
            {
                CargaGrid();
            }
            catch(Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        void ctrlAgregaEdita_GuardadoCorrecto(object sender, EventArgs e)
        {
            try
            {
                CargaGrid();
            }
            catch(Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void Nuevo_Agregado(object sender, EventArgs e)
        { 
            CargaGrid();
            ctrlMessageBox.AddMessage("Se agrego Correctamente a la Persona", MessageBox.enmMessageType.Success, "Agregar Persona");
            
        }

        protected void Aceptado_Evento(object sender, EventArgs e)
        {
            try
            {
                string Opcion = ViewState["OpcionAcepta"].ToString();

                if (Opcion == "AsociaDispo")
                {
                    List<TDI_UsuarioDispositivo> UsuDis = ViewState["AsigDis"] as List<TDI_UsuarioDispositivo>;
                    bool EliminaDisp =MngNegocioUsuarioDispositivo.EliminaDispoUsuario(UsuDis[0]);
                    if (EliminaDisp)
                    {
                        TDI_UsuarioDispositivo usuaDispo = ViewState["UsuaDis"] as TDI_UsuarioDispositivo;
                        bool AsignaDispoUsua = MngNegocioUsuarioDispositivo.AsignaDispoUsuario(usuaDispo);
                        
                        if (AsignaDispoUsua)
                        {
                            ctrlMessageBox.AddMessage("Se Asigno Correctamente el Nuevo Dispositivo", MessageBox.enmMessageType.Success, "Asignar Dispositivo");
                            GuardaLogTransacc("Se Asigno el Dispositivo " + usuaDispo.IdDispositivo + " a la Persona " + usuaDispo.UsuarioLlavePrimaria, 24);
                        }
                    }
                }
                if (Opcion == "ElimPersona")
                {
                    List<THE_Usuario> usuario = Session["datasource"] as List<THE_Usuario>;
                    THE_Usuario usua = usuario[int.Parse(ViewState["IDElimina"].ToString())] as THE_Usuario;
                    usua.UsuarioEstatus = 'B';
                    bool Elimina =MngNegocioUsuario.EliminaUsuario(usua);
                    if (Elimina)
                    {
                        CargaGrid();                       
                        GuardaLogTransacc("Se Elimino a la Persona " + usua.UsuarioLlavePrimaria, 6);
                    }
                }
                if (Opcion == "Actualiza")
                {
                    CargaGrid();
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                AltaEdicionUsuario(null, "Nuevo");
                mpeAltaUsuario.Show();
            }
            catch(Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void gvAltaUsuario_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Elimina")
                {
                    ViewState["IDElimina"] = int.Parse(e.CommandArgument.ToString());
                    ctrlMessageBox.AddMessage("Realmente Desea Eliminar a la Persona Seleccionada", MessageBox.enmMessageType.Attention, true, true, "prueba", "Elimina Persona");
                    ViewState["OpcionAcepta"] = "ElimPersona";
                }
                if (e.CommandName == "Edita")
                {
                  
                    Tabs.Style.Add(style, "block");
                    ViewState["IDEdita"] = int.Parse(e.CommandArgument.ToString());
                    List<THE_Usuario> usuario = Session["datasource"] as List<THE_Usuario>;
                    THE_Usuario usua = usuario[int.Parse(ViewState["IDEdita"].ToString())] as THE_Usuario;
                   
                    AltaEdicionUsuario(usua, "Edita");
                    mpeAltaUsuario.Show();
                } 
                if (e.CommandName == "Asignar")
                {
                    ViewState["UsuarioLlavePrimaria"] = int.Parse(((GridView)sender).Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text);
                    
                    if (ObtieneDispoDisponibles() != false)
                    {
                        lblTitAsigna.Text = "Asigna Dispositivo Persona";
                        mpeAsignaDisp.Show();
                    }
                    else
                    {
                        ctrlMessageBox.AddMessage("Sin Dispositivos Disponibles", MessageBox.enmMessageType.Attention, "Asigna Dispositivo");
                    }
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void ObtieneDispoUsuario(int IdDispo)
        {
            try
            {

                List<THE_Dispositivo> DispoUsua =MngNegocioDispositivo.ObtieneDispositivoPorID(IdDispo);
                if (DispoUsua.Count > 0)
                {
                    lvDispositivos.DataSource = null;
                    lvDispositivos.DataSource = DispoUsua;
                    lvDispositivos.DataBind();
               
                }
                
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected bool ObtieneDispoDisponibles()
        {
            bool ConDispos = false;

            try
            {
                List<THE_Dispositivo> lstDispoDispo =MngNegocioDispositivo.ObtieneDispositivosDisponibles();
                if (lstDispoDispo.Count > 0)
                {
                    AppSettingsReader appRdr = new AppSettingsReader();
                    Rijndael _ChyperRijndael = new Rijndael();
                    string tmpPath = string.Empty;
                    string destinationPath = string.Empty;
                    destinationPath = _ChyperRijndael.Transmute(appRdr.GetValue("RutaArchivosServer", typeof(string)).ToString(), enmTransformType.intDecrypt);

                    for (int ini = 0; ini < lstDispoDispo.Count; ini++)
                    {
                        if (lstDispoDispo[ini].ImagenTelefono == null)
                        {
                            lstDispoDispo[ini].ImagenTelefono = destinationPath + @"\Dispositivos" + @"\no_foto.jpg";
                        }
                        else
                        {
                            lstDispoDispo[ini].ImagenTelefono = destinationPath + @"\Dispositivos" + @"\" + lstDispoDispo[ini].IdDispositivo + @"\" + lstDispoDispo[ini].ImagenTelefono;
                        }
                    }

                    lvDispositivos.DataSource = lstDispoDispo;
                    lvDispositivos.DataBind();
                    ConDispos = true;
                }
                else
                {
                    ConDispos = false;
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }

            return ConDispos;
        }

        [WebMethod]
        public static string CargaGridUsers(string nombre, string apePaterno, string apeMaterno, string sexo, string Catalogos)
        {
            try
            {
                THE_Usuario usuario = new THE_Usuario();
               
                if (nombre!=string.Empty)
                    usuario.UsuarioNombre = nombre;
                if (apePaterno!=string.Empty)
                    usuario.UsuarioApellPaterno = apePaterno;
                if(apeMaterno!=string.Empty)
                    usuario.UsuarioApellMaterno = apeMaterno;
                if (sexo!=string.Empty)
                    usuario.UsuarioSexo = Convert.ToChar(sexo);   
                List<THE_Usuario> usuarios = new List<THE_Usuario>();
              
                usuarios = MngNegocioUsuario.BuscaUsuarios2(usuario, Catalogos);  

                for (int ini = 0; ini < usuarios.Count; ini++)
                {
                    if (usuarios[ini].UsuarioFoto == null)
                        usuarios[ini].UsuarioFoto = "../images/no_foto.jpg";
                    else
                        usuarios[ini].UsuarioFoto = "../Media/Usuarios/" + usuarios[ini].UsuarioLlavePrimaria + "/" + usuarios[ini].UsuarioFoto;
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string resultjson = "";
                resultjson = serializer.Serialize(usuarios);
                return resultjson;  

             
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public void CargaGrid()
        {
            try
            {   
                THE_Usuario usuario = new THE_Usuario();
                //Asigna valores del usuario a buscar
                usuario.UsuarioNombre = txtBusqNombUsu.Text;
                
                usuario.UsuarioSexo = Convert.ToChar(DdlSexo.SelectedItem.Value);

                List<THE_Usuario> usuarios = new List<THE_Usuario>();

                //Busca usuario
                if (RadPBusqCatalogos.Visible == false)//Busqueda sencilla
                {
                    usuarios = MngNegocioUsuario.BuscaUsuarios(usuario);
                }
                else //Busqueda especializada
                {    
                    List<TDI_OpcionCat> listOpCatalogo = new List<TDI_OpcionCat>();
                    foreach(RadPanelItem item in RadPBusqCatalogos.Items)
                    {                     
                        CheckBoxList checkBoxL = ((CheckBoxList)item.Items[0].FindControl("listachecks"));
                       
                        for(int j=0; j<checkBoxL.Items.Count;j++)
                        {
                            if (checkBoxL.Items[j].Selected == true)
                            {
                                TDI_OpcionCat opcion = new TDI_OpcionCat();
                                opcion.IdOpcionCat = int.Parse(checkBoxL.Items[j].Value);
                                opcion.OpcionCatDesc = checkBoxL.Items[j].Text;
                                listOpCatalogo.Add(opcion);
                            }                            
                        }                                                                        
                       
                    }
                    usuarios = MngNegocioUsuario.BuscaUsuariosEsp(usuario, listOpCatalogo);
                }

                for (int ini = 0; ini < usuarios.Count; ini++)
                {
                    if (usuarios[ini].UsuarioFoto == null)
                        usuarios[ini].UsuarioFoto = "../images/no_foto.jpg";
                    else
                        usuarios[ini].UsuarioFoto = "../Media/Usuarios/" + usuarios[ini].UsuarioLlavePrimaria + "/" + usuarios[ini].UsuarioFoto; 
                }
                gvAltaUsuario.DataSource = null;
                gvAltaUsuario.DataSource = usuarios;
                Session["datasource"] = usuarios;
                gvAltaUsuario.EmptyDataText = "No se encontrarón Usuarios";
                gvAltaUsuario.DataBind();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmUsuarios", int.Parse(Session["numeroUsuario"].ToString()));
            }
         
        }

        public void ctrlMessageBox_MsgBoxAnswered(object sender, MessageBox.MsgBoxEventArgs e)
        {
            if (e.Answer == MessageBox.enmAnswer.OK)
            {
            }
            else
            {
                ctrlMessageBox.AddMessage("Ha Cancelado la Operación", MessageBox.enmMessageType.Info, "Operación Cancelada");
            }
        }

        protected void gvAltaUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAltaUsuario.PageIndex = e.NewPageIndex;
            CargaGrid();
        }

        protected void btnAceptaAsignado_Click(object sender, EventArgs e)
        {
            int IdDispo = 0;
            bool AsignaDispoUsua = false;
            if (hfIdRButton.Value != string.Empty)
            {
               IdDispo = int.Parse(hfIdRButton.Value);
            }
            else
            {
                return;
            }


            TDI_UsuarioDispositivo usuaDispo = new TDI_UsuarioDispositivo();
            usuaDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = IdDispo };
            usuaDispo.UsuarioLlavePrimaria = new THE_Usuario() { UsuarioLlavePrimaria = int.Parse(ViewState["UsuarioLlavePrimaria"].ToString()) };
            usuaDispo.UsuaDispoEstatus = 'A';

            List<TDI_UsuarioDispositivo> disp = MngNegocioUsuarioDispositivo.ObtieneDispositivoPorUsuario(int.Parse(ViewState["UsuarioLlavePrimaria"].ToString()));

            if (disp==null || disp.Count <= 0)
            {
                AsignaDispoUsua = MngNegocioUsuarioDispositivo.AsignaDispoUsuario(usuaDispo);
            }
            else
            {
                ctrlMessageBox.AddMessage("¿Realmente deseas asociar un Nuevo Dispositivo a la Persona?", MessageBox.enmMessageType.Attention, true, true, "prueba", "Asocia Dispositivo");
                ViewState["OpcionAcepta"] = "AsociaDispo";
                ViewState["AsigDis"] = disp;
                ViewState["UsuaDis"] = usuaDispo;
            }                
            
            if (AsignaDispoUsua == true)
            {
                ctrlMessageBox.AddMessage("Se Asigno Correctamente el Dispositivo a la Persona", MessageBox.enmMessageType.Success, "Asignar Dispositivo");
            }
        }

        protected void btnCerrarAsigna_Click(object sender, ImageClickEventArgs e)
        {
            mpeAsignaDisp.Hide();
        }

        void CargaDatosCatalogos()
        {
            btnGuardaAltaUsuario.Visible = true;
            try
            {
                acModules.Items.Clear();
                RadPBusqCatalogos.Items.Clear();

                List<THE_Catalogo> listCatalogos = MngNegocioCatalogo.ObtieneTodosCatalogos();

                grid = new GridView[listCatalogos.Count];

                int count = 0;
                if (listCatalogos.Count > 0)
                {
                    foreach (THE_Catalogo itemCatalogo in listCatalogos)
                    {
                        List<TDI_OpcionCat> lstOpcioCatalogo = MngNegocioOpcionCat.ObtieneOpcionesPorCatalogo(itemCatalogo.IdCatalogo);

                        if (lstOpcioCatalogo != null)
                        {
                            if (itemCatalogo != null)
                            {
                                //Item Usuario
                                RadPanelItem item = new RadPanelItem();
                                item.Text = itemCatalogo.CatalogoDesc;
                                item.Value = itemCatalogo.IdCatalogo.ToString();
                             
                                //Item Busqueda Usuario
                                RadPanelItem itemB = new RadPanelItem();
                                itemB.Text = itemCatalogo.CatalogoDesc;
                                itemB.Value = itemCatalogo.IdCatalogo.ToString();
                             
                                //item Hijo Usuario (radio button list)
                                RadioButtonList rbl = new RadioButtonList();
                                rbl.ID = "listaradios";
                               
                                //item Hijo Busqueda Usuario (check box list)
                                CheckBoxList cbl = new CheckBoxList();
                                cbl.ID = "listachecks";
                              


                                foreach (TDI_OpcionCat op in lstOpcioCatalogo)
                                {
                                    
                                    rbl.Items.Add(new System.Web.UI.WebControls.ListItem(op.OpcionCatDesc, op.IdOpcionCat.ToString()));
                                    rbl.SelectedIndex = 0;
                                    cbl.Items.Add(new System.Web.UI.WebControls.ListItem(op.OpcionCatDesc, op.IdOpcionCat.ToString()));
                                    cbl.SelectedIndex = 0;
                                }

                                RadPanelItem itemHijo = new RadPanelItem();
                               
                                itemHijo.Controls.Add(rbl);
                                item.Items.Add(itemHijo);

                                RadPanelItem itemHijoB = new RadPanelItem();
                            
                                itemHijoB.Controls.Add(cbl);
                                itemB.Items.Add(itemHijoB);

                                acModules.Items.Add(item);
                                RadPBusqCatalogos.Items.Add(itemB);
                            }

                        }
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmCatalogos", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        [WebMethod]
        public static string ObtenerCatalogos()
        {
            Dictionary<string, string> Catalogos = new Dictionary<string, string>();
            try
            {

                List<THE_Catalogo> listCatalogos = MngNegocioCatalogo.ObtieneTodosCatalogos();

                if (listCatalogos.Count > 0)
                {
                    foreach (THE_Catalogo itemCatalogo in listCatalogos)
                    {
                        List<TDI_OpcionCat> lstOpcioCatalogo = MngNegocioOpcionCat.ObtieneOpcionesPorCatalogo(itemCatalogo.IdCatalogo);

                        if (lstOpcioCatalogo != null)
                        {
                            if (itemCatalogo != null)
                            {
                                string opciones = "";
                                foreach (TDI_OpcionCat op in lstOpcioCatalogo)
                                {
                                    opciones += op.IdOpcionCat.ToString() + "|" + op.OpcionCatDesc.ToString() + ";";
                                }
                                opciones = opciones.Substring(0, opciones.Length - 1);
                                Catalogos.Add(itemCatalogo.CatalogoDesc.ToString(), opciones);
                            }

                        }

                    }                   
                }
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string resultjson = "";
                resultjson = serializer.Serialize(Catalogos);
                return resultjson;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        [WebMethod]
        public static string SaveUsuario(string Parametros, string Catalogos,string Opcion) {

            try
            {
                string[] Elementos = Parametros.Split(';');
                string nombre = "", telefono = "", modelo = "", marca = "", desc = "", imei = "",  genero = "";
                nombre = Elementos[0].ToString();
                telefono = Elementos[1].ToString();
                modelo = Elementos[2].ToString();
                marca = Elementos[3].ToString();
                desc = Elementos[4].ToString();
                imei = Elementos[5].ToString();               
                genero = Elementos[6].ToString();
                
                THE_Usuario usua = new THE_Usuario();
                THE_Dispositivo dispo = new THE_Dispositivo();
      
                usua.UsuarioNombre = nombre;
                usua.UsuarioApellMaterno = ".";
                usua.UsuarioApellPaterno = ".";
                usua.UsuarioCalleNum = ".";
                usua.UsuarioEmail = "nuevo usuario";
                usua.UsuarioEstatus = 'A';
                usua.UsuarioSexo = Convert.ToChar(genero);
                usua.UsuarioNumCelularPersonal = ".";
                usua.UsuarioObse = ".";
                usua.UsuarioTelCasa = ".";
                usua.IdColonia = new TDI_Colonias() { IdColonia = 1 };
                usua.UsuarioCodigoPostal = "39845";
                usua.UsuarioFechNacimiento = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");

                List<THE_Dispositivo> EncontreNumero = MngNegocioDispositivo.BusquedaDispositivoPorNumeroTel(telefono);
                List<THE_Dispositivo> EncontreMEID = MngNegocioDispositivo.BusquedaDispositivoPorMEID(imei.Trim());

                dispo.DispositivoDesc = desc.Trim();
                dispo.DispositivoEstatus = 'A';
                dispo.DispositivoMdn = "01";
                dispo.DispositivoMeid = imei.Trim();
                dispo.Marca = marca.Trim();
                dispo.Modelo = modelo.Trim();
                dispo.NumerodelTelefono = telefono.Trim();
                dispo.IdDispositivo = 0;           


                if (Opcion == "1")//OPCION PARA GUARDAR EL USUARIO
                {
                    int idUsuario = -1;
                    int idDispositivo = -1;
                    if (EncontreNumero.Count > 0)
                    {
                        return "[{'Success':'Error','Description':'" + "Ya existe el numero telefonico: " + telefono.Trim() + "'}]";
                    }
                    if (EncontreMEID.Count > 0)
                    {
                        return "[{'Success':'Error','Description':'" + "Ya existe el MEID: " + imei.Trim() + "'}]";
                    }

                    idUsuario = MngNegocioUsuario.GuardaAltaUsuario(usua);

                    if (idUsuario != -1)
                    {
                        idDispositivo = MngNegocioDispositivo.GuardaAltaDispo(dispo);

                        if (idDispositivo != -1)
                        {
                            string[] Cata = Catalogos.Split(',');
                            foreach (var item in Cata)
                            {
                                TDI_UsuarioCat usuaCat = new TDI_UsuarioCat();
                                usuaCat.IdOpcionCat = new TDI_OpcionCat() { IdOpcionCat = int.Parse(item) };
                                usuaCat.UsuaLlavPr = new THE_Usuario() { UsuarioLlavePrimaria = idUsuario };
                                usuaCat.UsuaCatStat = 'A';
                                bool GuardaOpCatUsuario = MngNegocioUsuarioCat.GuardaOpcionCatalogoPorUsuario(usuaCat);
                            }
                            TDI_UsuarioDispositivo usuaDispo = new TDI_UsuarioDispositivo();
                            usuaDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = idDispositivo };
                            usuaDispo.UsuarioLlavePrimaria = new THE_Usuario() { UsuarioLlavePrimaria = idUsuario };
                            usuaDispo.UsuaDispoEstatus = 'A';
                            Boolean asignoDispoUsr = MngNegocioUsuarioDispositivo.AsignaDispoUsuario(usuaDispo);

                            if (asignoDispoUsr)
                            {
                                return "[{'Success':'OK','Description':'Usuario Agregado Satisfactoriamente'}]";
                            }
                            else
                            {
                                //Borro Usuario/ Catalogos/ Dispositivos
                                return "[{'Success':'Error','Description':'Error al agregar usuario'}]";
                            }
                        }
                        else
                        {
                            //Borro Usuario
                            return "[{'Success':'Error','Description':'Error al agregar usuario'}]";
                        }
                    }
                    else { 
                     return "[{'Success':'Error','Description':'Error al agregar usuario'}]";
                    }

                   
                }
                else if (Opcion == "2") { // OPCION PARA ACTULAIZAR LOS DATOS DEL USUARIO, DISPOSITIVO Y CATALOGOS DEL USUARIO

                    usua.UsuarioLlavePrimaria = int.Parse(Elementos[7].ToString());
                    dispo.IdDispositivo = Convert.ToInt32(Elementos[8].ToString());
                    bool ActualizaDispo = MngNegocioDispositivo.ActualizaDispositivo(dispo);
                    bool ActualizaUser = MngNegocioUsuario.ActualizaUsuario(usua);
                    if (ActualizaDispo)
                    {
                        if (ActualizaUser)
                        {
                            List<TDI_UsuarioCat> usuarioOpciones = MngNegocioUsuarioCat.ObtieneOpcionesCatalogoPorUsuario(usua.UsuarioLlavePrimaria);
                            foreach (var userOpcion in usuarioOpciones)
                            {
                                bool EliminaOpcionAgregaNew = MngNegocioUsuarioCat.EliminaCompletaOpcion(userOpcion);
                            }
                            string[] Cata = Catalogos.Split(',');
                            foreach (var item in Cata)
                            {
                                TDI_UsuarioCat usuaCat = new TDI_UsuarioCat();
                                usuaCat.IdOpcionCat = new TDI_OpcionCat() { IdOpcionCat = int.Parse(item) };
                                usuaCat.UsuaLlavPr = new THE_Usuario() { UsuarioLlavePrimaria = usua.UsuarioLlavePrimaria };
                                usuaCat.UsuaCatStat = 'A';
                                bool GuardaOpCatUsuario = MngNegocioUsuarioCat.GuardaOpcionCatalogoPorUsuario(usuaCat);
                            }
                            return "[{'Success':'OK','Description':'Cambios Realizados Satisfactoriamente'}]";
                        }
                        else {
                            return "[{'Success':'Error','Description':'Error al actualizar datos de usuario'}]";
                        }
                    }
                    else {
                        return "[{'Success':'Error','Description':'Error al actualizar datos de usuario'}]";
                    }
                }

                return "[{'Success':'Error','Description':'Error de prodecimiento'}]";
            }catch(Exception ex){

                return "[{'Success':'Error','Description':'" + ex.Message + "'}]";
            }      
        }

        [WebMethod]
        public static string DeleteUserDispo(string idUser, string datosDispo) {
            string[] valoresDisp = datosDispo.Split(';')[0].Split(':');
            THE_Usuario usua = new THE_Usuario();
            usua.UsuarioLlavePrimaria = Convert.ToInt32(idUser);
            usua.UsuarioEstatus='B';
            usua.IdColonia = new TDI_Colonias() {  IdColonia=1};
            usua.UsuarioNombre = valoresDisp[5];
            usua.UsuarioApellMaterno = ".";
            usua.UsuarioApellPaterno = ".";
            usua.UsuarioCalleNum = ".";
            usua.UsuarioEmail = "nuevo usuario";    
            usua.UsuarioSexo = Convert.ToChar('N');
            usua.UsuarioNumCelularPersonal = ".";
            usua.UsuarioObse = ".";
            usua.UsuarioTelCasa = ".";
            usua.IdColonia = new TDI_Colonias() { IdColonia = 1 };
            usua.UsuarioCodigoPostal = "39845";
            usua.UsuarioFechNacimiento = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy");
           
         
            THE_Dispositivo dispo = new THE_Dispositivo();
            dispo.IdDispositivo = Convert.ToInt32(datosDispo.Split(';')[2]);
            dispo.DispositivoMdn = "01";
            dispo.DispositivoMeid = valoresDisp[3];
            dispo.Marca=valoresDisp[2];
            dispo.Modelo = valoresDisp[1];
            dispo.NumerodelTelefono=valoresDisp[4];
            dispo.DispositivoDesc=valoresDisp[0];

            dispo.DispositivoEstatus='B';

            TDI_UsuarioDispositivo userDispo = new TDI_UsuarioDispositivo();
            userDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = Convert.ToInt32(datosDispo.Split(';')[2]) };
            userDispo.UsuarioLlavePrimaria = new THE_Usuario() { IdDisposisito=Convert.ToInt32(idUser)};
            userDispo.UsuaDispoEstatus='B';


           
            bool EliminaDispo = MngNegocioDispositivo.EliminaDispositivo(dispo);
            bool EliminaUser = MngNegocioUsuario.EliminaUsuario(usua);
            bool EliminaDispoUser = MngNegocioUsuarioDispositivo.EliminaUserDispo(userDispo);



            if (EliminaUser && EliminaDispo)
            {
                List<TDI_UsuarioCat> usuarioOpciones = MngNegocioUsuarioCat.ObtieneOpcionesCatalogoPorUsuario(usua.UsuarioLlavePrimaria);
                foreach (var userOpcion in usuarioOpciones)
                {
                    bool EliminaOpcionAgregaNew = MngNegocioUsuarioCat.EliminaCompletaOpcion(userOpcion);
                }

                return "[{'Success':'OK','Description':'El registro se elimino correctamente'}]";               
            }

            return "[{'Success':'Error','Description':'Error al eliminar usuario'}]";
        }

        public void AltaEdicionUsuario(THE_Usuario usu, string opcion)
        {
            try
            {
                if (opcion == "Nuevo")
                {
                    ObtieneTodosEstados();

                    //lblTituloModalUsuario.Text = "Alta de Usuario";
                    LimpiaControles();
                    btnGuardaAltaUsuario.Text = "Guardar";
                }
                else
                {
                    //lblTituloModalUsuario.Text = "Edicion de Usuario";
                    txtApellMaterno.Text = usu.UsuarioApellMaterno;
                    txtApellPaterno.Text = usu.UsuarioApellPaterno;
                    txtCelPersonal.Text = usu.UsuarioNumCelularPersonal;
                    txtDireccion.Text = usu.UsuarioCalleNum;
                    txtEmail.Text = usu.UsuarioEmail;
                    txtFechNacimiento.SelectedDate = Convert.ToDateTime(usu.UsuarioFechNacimiento);
                    DdGenero.SelectedValue = usu.UsuarioSexo.ToString();
                    txtNombreUsuario.Text = usu.UsuarioNombre;
                    txtObservaciones.Text = usu.UsuarioObse;
                    txtTelCasa.Text = usu.UsuarioTelCasa;
                    txtCodigoP.Text = usu.UsuarioCodigoPostal;
                 //   txtId.Text = usu.UsuarioLlavePrimaria.ToString();
                    btnGuardaAltaUsuario.Text = "Actualiza";
                    List<TDI_Colonias> col =MngNegocioColonias.ObtieneColoniaPorId(usu.IdColonia.IdColonia);

                    //List<TDI_UsuarioCat> usuarioOpciones =MngNegocioUsuarioCat.ObtieneOpcionesCatalogoPorUsuario(int.Parse(txtId.Text));
                    List<TDI_UsuarioCat> usuarioOpciones = MngNegocioUsuarioCat.ObtieneOpcionesCatalogoPorUsuario(2);
                    dpColonia.DataSource = col;
                    dpColonia.DataTextField = "ColoniaNombre";
                    dpColonia.DataValueField = "IdColonia";
                    dpColonia.DataBind();
                    style = HtmlTextWriterStyle.Display;                    
                    ObtieneEstadoPorCP(int.Parse(txtCodigoP.Text));
                    ObtieneMunicipio(col[0].IdMunicipio.IdMunicipio);
                    ObtieneAsentamiento(col[0].IdAsentamiento.IdAsentamiento);
                    ObtieneTipoZonaId(col[0].IdZona.IdZona);
                    ObtieneEstadoPorCP(int.Parse(txtCodigoP.Text));

                    int bandera = 0;
                    if (usuarioOpciones.Count > 0)
                    {
                        foreach (TDI_UsuarioCat usuaCat in usuarioOpciones)
                        {
                            bandera = 0;
                            int index = -1;
                          
                            foreach (RadPanelItem item in acModules.Items)
                            {
                                int OpcionCat = usuaCat.IdOpcionCat.IdOpcionCat;
                                RadioButtonList raButt = ((RadioButtonList)item.Items[0].FindControl("listaradios"));
                                
                                for (int i = 0; i < raButt.Items.Count; i++)
                                {
                                    raButt.Items[i].Attributes.CssStyle.Add(HtmlTextWriterAttribute.Class.ToString(), "sds");
                                    index += 1;
                                    try
                                    {
                                        if (OpcionCat.ToString() == raButt.Items[i].Value)
                                        {
                                            raButt.Items[i].Selected = true;
                                            bandera = 1;
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }

                                if (bandera == 1)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                mpeAltaUsuario.Show();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }       

        protected void btnGuardaAltaUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                THE_Usuario usua = new THE_Usuario();

                if (subeFotoUsuario.HasFile)
                {
                    string fileExt = System.IO.Path.GetExtension(subeFotoUsuario.FileName);

                    fileExt = fileExt.ToUpper();
                    if (fileExt != ".JPEG" && fileExt != ".JPG" && fileExt != ".GIF" && fileExt != ".PNG")
                    {
                      //  DivErrorresuSR.InnerText = "Favor de subir imagenes con extencion .jpeg| .jpg| .gif| .png ";
                        mpeAltaUsuario.Show();
                        return;
                    }
                    else
                    {
                        subeFotoUsuario.PostedFile.SaveAs(Server.MapPath("~/temporal/") + subeFotoUsuario.FileName);
                    }
                }

                string fn = subeFotoUsuario.FileName;

                if (subeFotoUsuario.FileName != "")
                {
                    usua.UsuarioFoto = fn;
                }
                else {
                    if (btnGuardaAltaUsuario.Text == "Actualiza")
                    {
                        List<THE_Usuario> Usuarios =MngNegocioUsuario.ObtieneTodosUsuarios();
                        var listUser = (from Users in Usuarios
                                        where Users.UsuarioLlavePrimaria == 2//int.Parse(txtId.Text)
                                        select Users).Take(1);
                        foreach (var itemList in listUser)
                        {
                            usua.UsuarioFoto = itemList.UsuarioFoto;
                        }
                    }
                }

                usua.UsuarioApellMaterno = txtApellMaterno.Text;
                usua.UsuarioApellPaterno = txtApellPaterno.Text;
                usua.UsuarioCalleNum = txtDireccion.Text;
                usua.UsuarioEmail = txtEmail.Text;
                usua.UsuarioEstatus = 'A';
                usua.UsuarioSexo = Convert.ToChar(DdGenero.SelectedValue.ToString());
                if (txtFechNacimiento.SelectedDate.ToString().Trim() != string.Empty)
                {
                    usua.UsuarioFechNacimiento =Convert.ToDateTime(txtFechNacimiento.SelectedDate).ToString("dd/MM/yyyy");
                }
                else
                {
                    usua.UsuarioFechNacimiento = DateTime.Now.ToString("dd/MM/yyyy");
                }
                usua.UsuarioNombre = txtNombreUsuario.Text;
                usua.UsuarioNumCelularPersonal = txtCelPersonal.Text;
                usua.UsuarioObse = txtObservaciones.Text;
                usua.UsuarioTelCasa = txtTelCasa.Text;
                usua.IdColonia = new TDI_Colonias() { IdColonia = int.Parse(dpColonia.SelectedItem.Value) };
                usua.UsuarioCodigoPostal = txtCodigoP.Text;

                if (btnGuardaAltaUsuario.Text == "Actualiza")
                {
                    //usua.UsuarioLlavePrimaria = int.Parse(txtId.Text);
                    bool Actualiza =MngNegocioUsuario.ActualizaUsuario(usua);
                    if (Actualiza == true)
                    {
                        //List<TDI_UsuarioCat> usuarioOpciones =MngNegocioUsuarioCat.ObtieneOpcionesCatalogoPorUsuario(int.Parse(txtId.Text));
                        List<TDI_UsuarioCat> usuarioOpciones = MngNegocioUsuarioCat.ObtieneOpcionesCatalogoPorUsuario(4);
                        if (usuarioOpciones.Count <= 0)
                        {
                            foreach (RadPanelItem item in acModules.Items)
                            {
                                RadioButtonList raButt = ((RadioButtonList)item.Items[0].FindControl("listaradios"));
                                string IdSeleccionadoRadio = raButt.SelectedItem.Value;
                                TDI_UsuarioCat usCate = new TDI_UsuarioCat();
                                usCate.IdOpcionCat = new TDI_OpcionCat() { IdOpcionCat = int.Parse(IdSeleccionadoRadio) };
                               // usCate.UsuaLlavPr = new THE_Usuario() { UsuarioLlavePrimaria = int.Parse(txtId.Text) };
                                usCate.UsuaCatStat = 'A';
                                bool GuardaOpCatUsuario = MngNegocioUsuarioCat.GuardaOpcionCatalogoPorUsuario(usCate);
                            }
                        }
                        else
                        {
                            foreach (var userOpcion in usuarioOpciones)
                            {
                                bool EliminaOpcionAgregaNew =MngNegocioUsuarioCat.EliminaCompletaOpcion(userOpcion);                            
                            }
                            foreach (RadPanelItem item in acModules.Items)
                            {
                                RadioButtonList raButt = ((RadioButtonList)item.Items[0].FindControl("listaradios"));
                                string IdSeleccionadoRadio = raButt.SelectedItem.Value;
                                TDI_UsuarioCat usCate = new TDI_UsuarioCat();
                                usCate.IdOpcionCat = new TDI_OpcionCat() { IdOpcionCat = int.Parse(IdSeleccionadoRadio) };
                              //  usCate.UsuaLlavPr = new THE_Usuario() { UsuarioLlavePrimaria = int.Parse(txtId.Text) };
                                usCate.UsuaCatStat = 'A';
                                bool GuardaOpCatUsuario = MngNegocioUsuarioCat.GuardaOpcionCatalogoPorUsuario(usCate);
                            }
                            
                        }

                        if (fn != "")
                        {
                            SubeFoto(usua.UsuarioLlavePrimaria.ToString());
                        }
                        
                        ctrlMessageBox.AddMessage("Se Actualizaron Correctamente los Datos", MessageBox.enmMessageType.Attention, true, false, "Actualiza", "Actualiza Persona");
                        ViewState["OpcionAcepta"] = "Actualiza";
                        GuardaLogTransacc("Actualiza Datos Persona " + usua.UsuarioLlavePrimaria, 5);
                    }                    
                }
                else
                {
                    int Guardado = -1;

                    Guardado =MngNegocioUsuario.GuardaAltaUsuario(usua);

                    if (Guardado != -1)
                    {
                        foreach (RadPanelItem item in acModules.Items)
                        {
                            RadioButtonList raButt = ((RadioButtonList)item.Items[0].FindControl("listaradios"));
                            string IdSeleccionadoRadio = raButt.SelectedItem.Value;
                            string texto = raButt.SelectedItem.Text;

                            TDI_UsuarioCat usuaCat = new TDI_UsuarioCat();
                            usuaCat.IdOpcionCat = new TDI_OpcionCat() { IdOpcionCat = int.Parse(IdSeleccionadoRadio) };
                            usuaCat.UsuaLlavPr = new THE_Usuario() { UsuarioLlavePrimaria = Guardado };
                            usuaCat.UsuaCatStat = 'A';
                            bool GuardaOpCatUsuario = MngNegocioUsuarioCat.GuardaOpcionCatalogoPorUsuario(usuaCat);

                            if (GuardaOpCatUsuario)
                            {
                                ViewState["OpcionAcepta"] = "Actualiza";
                            }
                        }

                        if (fn != "")
                        {
                            SubeFoto(usua.UsuarioLlavePrimaria.ToString());
                        }

                        ctrlMessageBox.AddMessage("Se Agrego Correctamente a la Persona", MessageBox.enmMessageType.Success, true, false, "Guarda", "Alta de Persona");
                        GuardaLogTransacc("Alta de Persona " + usua.UsuarioLlavePrimaria, 4);
                    }
                    else
                    {

                    }
                }
                mpeAltaUsuario.Hide();
                CargaGrid();
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }
        
        public void deleteFiles(string Directorio)
        {
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
            finally
            {

            }

        }

        protected void btnCP_Click(object sender, EventArgs e)
        {
            try
            {
                List<TDI_Colonias> Colonias =MngNegocioColonias.ObtieneColoniasPorCP(int.Parse(txtCodigoP.Text));
                if (Colonias.Count > 0)
                {
                    dpColonia.DataSource = Colonias;
                    dpColonia.DataTextField = "ColoniaNombre";
                    dpColonia.DataValueField = "IdColonia";
                    dpColonia.DataBind();
                    style = HtmlTextWriterStyle.Display;                    
                    ObtieneEstadoPorCP(int.Parse(txtCodigoP.Text));
                    ObtieneMunicipio(Colonias[0].IdMunicipio.IdMunicipio);
                    ObtieneAsentamiento(Colonias[0].IdAsentamiento.IdAsentamiento);
                    ObtieneTipoZonaId(Colonias[0].IdZona.IdZona);
                    mpeAltaUsuario.Show();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void dpColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<TDI_Colonias> colo =MngNegocioColonias.ObtieneColoniaPorId(int.Parse(dpColonia.SelectedItem.Value));
                if (colo.Count > 0)
                {
                    ObtieneAsentamiento(colo[0].IdAsentamiento.IdAsentamiento);
                    ObtieneTipoZonaId(colo[0].IdZona.IdZona);
                    ObtieneCPColonia(colo[0].IdColonia);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
            mpeAltaUsuario.Show();
        }

        protected void dpEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                style = HtmlTextWriterStyle.Display;
                dpMunicipio.DataSource = null;
                List<TDI_Municipios> listMuni =MngNegocioMunicipios.ObtieneMunicipiosPorEstado(int.Parse(dpEstado.SelectedItem.Value));
                if (listMuni.Count > 0)
                {
                    dpMunicipio.DataSource = listMuni;
                    dpMunicipio.DataTextField = "MunicipioNombre";
                    dpMunicipio.DataValueField = "IdMunicipio";
                    dpMunicipio.DataBind();                    
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
            mpeAltaUsuario.Show();
        }

        protected void dpMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                style = HtmlTextWriterStyle.Display;
                dpColonia.DataSource = null;
                List<TDI_Colonias> listColo =MngNegocioColonias.ObtieneColoniasPorMunicipio(int.Parse(dpMunicipio.SelectedItem.Value));
                if (listColo.Count > 0)
                {
                    dpColonia.DataSource = listColo;
                    dpColonia.DataTextField = "ColoniaNombre";
                    dpColonia.DataValueField = "IdColonia";
                    dpColonia.DataBind();                    
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
            mpeAltaUsuario.Show();
        }

        void LimpiaControles()
        {
            style = HtmlTextWriterStyle.Display;
            txtApellMaterno.Text = string.Empty;
            txtApellPaterno.Text = string.Empty;
            txtCelPersonal.Text = string.Empty;
            txtCodigoP.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtFechNacimiento.SelectedDate =DateTime.Now;
            txtNombreUsuario.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            txtTelCasa.Text = string.Empty;
            dpAsentamiento.DataSource = null;
            dpColonia.DataSource = null;
            dpEstado.DataSource = null;
            dpMunicipio.DataSource = null;
            dpZona.DataSource = null;            
        }

        protected void ObtieneMunicipio(int idMun)
        {
            try
            {
                List<TDI_Municipios> Municipios =MngNegocioMunicipios.ObtieneMunicipiosPorID(idMun);
                if (Municipios.Count > 0)
                {
                    dpMunicipio.DataSource = Municipios;
                    dpMunicipio.DataTextField = "MunicipioNombre";
                    dpMunicipio.DataValueField = "IdMunicipio";
                    dpMunicipio.DataBind();                    
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void ObtieneAsentamiento(int idAsen)
        {
            try
            {
                style = HtmlTextWriterStyle.Display;
                List<THE_TipoAsenta> Asentamientos =MngNegocioTipoAsenta.ObtieneTipoAsentamientoPorId(idAsen);
                if (Asentamientos.Count > 0)
                {
                    dpAsentamiento.DataSource = null;
                    dpAsentamiento.DataSource = Asentamientos;
                    dpAsentamiento.DataTextField = "AsentamientoNombre";
                    dpAsentamiento.DataValueField = "IdAsentamiento";
                    dpAsentamiento.DataBind();                    
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void ObtieneEstadoPorCP(int codPost)
        {
            try
            {
                List<TDI_Estado> estadoCP =MngNegocioEstado.ObtieneEstadoPorCP(codPost);
                if (estadoCP.Count > 0)
                {
                    dpEstado.DataSource = estadoCP;
                    dpEstado.DataTextField = "EstadoNombre";
                    dpEstado.DataValueField = "IdEstado";
                    dpEstado.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void ObtieneTodosEstados()
        {
            try
            {
                List<TDI_Estado> TodosEstados =MngNegocioEstado.ObtieneTodoslosEstados();
                if (TodosEstados.Count > 0)
                {
                    dpEstado.DataSource = TodosEstados;
                    dpEstado.DataTextField = "EstadoNombre";
                    dpEstado.DataValueField = "IdEstado";
                    dpEstado.DataBind();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void ObtieneTipoZonaId(int idZone)
        {
            try
            {
                style = HtmlTextWriterStyle.Display;
                List<THE_TipoZona> TipoZona =MngNegocioTipoZona.ObtieneTipoZonaPorId(idZone);
                if (TipoZona.Count > 0)
                {
                    dpZona.DataSource = null;
                    dpZona.DataSource = TipoZona;
                    dpZona.DataTextField = "ZonaNombre";
                    dpZona.DataValueField = "IdZona";
                    dpZona.DataBind();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void ObtieneCPColonia(int idColonia)
        {
            try
            {
                style = HtmlTextWriterStyle.Display;
                List<TDI_CPCol> codPostal =MngNegocioCPCol.ObtieneCodigoPostalPorColonia(idColonia);
                if (codPostal.Count > 0)
                {
                    txtCodigoP.Text = codPostal[0].IdCodigoPostal.IdCodigoPostal.ToString();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "Alta Usuario", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        protected void tabCUsuario_ActiveTabChanged(object sender, EventArgs e)
        {
            if (((AjaxControlToolkit.TabContainer)sender).ActiveTabIndex == 1)
                CargaDatosCatalogos();
        }

        protected void btnCancelarAltaUsuario_Click(object sender, EventArgs e)
        {
            mpeAltaUsuario.Hide();
        }

        public bool SubeFoto(string rutaImagen)
        {
            
            bool UpCorrecto = false;

            AppSettingsReader appRdr = new AppSettingsReader();
            Rijndael _ChyperRijndael = new Rijndael();
            string tmpPath = string.Empty;
            string destinationPath = string.Empty;

            destinationPath = _ChyperRijndael.Transmute(appRdr.GetValue("RutaArchivosServer", typeof(string)).ToString(), enmTransformType.intDecrypt);

            try
            {
                string fn = System.IO.Path.GetFileName(hfRutaImg.Value);
                string directorio = Server.MapPath("~/temporal/");
                string SaveLocation = directorio + fn;

                FileComService filecom = new FileComService();
                
                filecom.CopyFile(SaveLocation, destinationPath + "\\Usuarios\\" + rutaImagen + "\\" + fn);
                filecom.DeleteFile(SaveLocation);

                UpCorrecto = true;
            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "AltaDispositivo", int.Parse(Session["numeroUsuario"].ToString()));
            }

            return UpCorrecto;
        }

        protected void btnCerrarAltaUser_Click(object sender, ImageClickEventArgs e)
        {
            mpeAltaUsuario.Hide();
        }

        protected void btnBuscaDispo_Click(object sender, EventArgs e)
        {
            List<THE_Dispositivo> BuscaDispo =MngNegocioDispositivo.BusquedaDispositivoPorNumeroTel(txtNumDispo.Text);

            if (BuscaDispo.Count > 0)
            {
                lvDispositivos.DataSource = null;
                lvDispositivos.DataSource = BuscaDispo;
                lvDispositivos.DataBind();
                txtNumDispo.Text = "";
            }
            else
            {
                lvDispositivos.DataSource = null;
                lvDispositivos.DataBind();
                txtNumDispo.Text = "";
            }
            mpeAsignaDisp.Show();
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

        protected void btnBuscaUsu_Click(object sender, EventArgs e)
        {
            LblValida.Text = "";
            LblValida.Visible = false;
            
            CargaGrid();
        }

        private bool ValidaEdades(string edad)
        {
            //Sencillamente, si se logra hacer la conversión, entonces es número
            try
            {
                decimal resp = Convert.ToDecimal(edad);
                return true;
            }
            catch //caso contrario, es falso.
            {
                return false;
            }
        }

        protected void BtnBusqEspecializada_Click(object sender, EventArgs e)
        {
            if (RadPBusqCatalogos.Visible == true)
                RadPBusqCatalogos.Visible = false;
            else
                RadPBusqCatalogos.Visible = true;
        }

        protected void ImgExportExcel_Click(object sender, ImageClickEventArgs e)
        {
            ExportaGridExcel();
        }

        private void ExportaGridExcel()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Page pag = new Page();
            HtmlForm form = new HtmlForm();

            gvAltaUsuario.AllowPaging = false;            
            gvAltaUsuario.EnableViewState = false;
            pag.EnableEventValidation = false;
            gvAltaUsuario.AllowSorting = false;
            gvAltaUsuario.Columns[1].Visible = false;
            gvAltaUsuario.Columns[2].Visible = false;
            gvAltaUsuario.Columns[9].Visible = false;

            pag.DesignerInitialize();
            pag.Controls.Add(form);

            form.Controls.Add(gvAltaUsuario);
            pag.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=BusqUsuarios.xls");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();

            gvAltaUsuario.AllowPaging = true;
           
        }

        protected void ImgExportPdf_Click(object sender, ImageClickEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page pag = new Page();
            HtmlForm form = new HtmlForm();

            gvAltaUsuario.AllowPaging = false;
            gvAltaUsuario.EnableViewState = false;            
            gvAltaUsuario.AllowSorting = false;
            gvAltaUsuario.Columns[1].Visible = false;
            gvAltaUsuario.Columns[2].Visible = false;
            gvAltaUsuario.Columns[9].Visible = false;
            pag.EnableEventValidation = false;

            pag.DesignerInitialize();
            pag.Controls.Add(form);

            form.Controls.Add(gvAltaUsuario);
            pag.RenderControl(htw);

            Response.ClearContent();
            string attachment = "attachment; filename=BusqUsuarios.pdf";            
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/pdf";
            
            Document document = new Document();
            PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();
            StringReader str = new StringReader(sw.ToString());
            HTMLWorker htmlworker = new HTMLWorker(document);
            htmlworker.Parse(str);
            document.Close();
            Response.Write(document);
            Response.End();             
            gvAltaUsuario.AllowPaging = true;
        }              
    }
}
