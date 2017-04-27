using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.Configuration;
using Microsoft.Win32;
using System.IO;
using com.dsi.pgp;
using BLL_EncuestasMoviles;
using Newtonsoft.Json;
using TVAzteca.Common.Authentication;
using System.Net;
using Entidades_EncuestasMoviles;
using System.Net.NetworkInformation;

namespace EncuestasMoviles
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Variables
        string userDesencriptado = "";
        string TipoUsuario="";
        private string usuario = string.Empty;
        private string CurrGuid = string.Empty;
        private string IPUsr;
        XmlDocument xml;
        bool isUserName = false;
        string UsuarioTVA = string.Empty;
        string EmId = string.Empty;
        string NumeroEmpleado = string.Empty;     
        XmlDocument GLOBALDATOSUSER = new XmlDocument();
        IList<IntentosUserXIP> IntentosXIP;
        IList<THE_BloqueoUsuario> UserBlock;
        IList<THE_SesionUsuario> Existe;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
                if (!Page.IsPostBack)
                {
                    btnLogin.Attributes.Add("onclick", "encrypt();");
                    lblFecha.Text = CargaFecha();
                }
            }catch(Exception ms){
            
            }finally{
            
            }
        }
        public void ctrlMessageBox_MsgBoxAnswered(object sender, MessageBox.MsgBoxEventArgs e)
        {
            if (e.Answer == MessageBox.enmAnswer.OK)
            {

            }
            else if (e.Answer == MessageBox.enmAnswer.Cancel)
            {

            }
        }
        protected void Acepta_Evento(object sender, EventArgs e)
        {
            try
            {
                string Opcion = ViewState["Opcion"].ToString();

                if (Opcion == "ContinuaSesion")
                {   
                    THE_SesionUsuario ObjSession = new THE_SesionUsuario();
                    ObjSession.DirIP = ViewState["IPUsr"].ToString();
                    ObjSession.FechaCreacion = DateTime.Now;
                    ObjSession.EmplLlavPr = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(ViewState["vsIdUser"].ToString()) };
                    ObjSession.IdSesion =int.Parse(ViewState["vsIdSesion"].ToString());
                    bool actualizo=MngNegocioUsuarioSesion.ActualizaSesionUsuario(ObjSession);
                    if(actualizo){
                        XmlDocument xmls = new XmlDocument();
                        xmls.LoadXml((string)ViewState["vsUserdata"]);
                        string numeroUsuario = xmls.FirstChild.ChildNodes[2].InnerText;
                        XmlDocument UserData=null;
                        if (isNumeric(ViewState["TipoUsuario"].ToString()))
                        {
                            isUserName = false;
                            UsuarioTVA = ViewState["userDesencriptado"].ToString().ToUpper();
                            usuario = ViewState["userDesencriptado"].ToString().ToUpper();

                            UserData = MngNegocioEmpleadoRol.GetUserDataByNumEmpleado(ViewState["TipoUsuario"].ToString(), "");
                            string NumUsua = (UserData.GetElementsByTagName("NUMUSUA").Count > 0) ? UserData.GetElementsByTagName("NUMUSUA")[0].InnerText : "";
                            EmId = (UserData.GetElementsByTagName("NUMEMPL").Count > 0) ? UserData.GetElementsByTagName("NUMEMPL")[0].InnerText : "";
                            if (ViewState["userDesencriptado"].ToString().ToUpper().Contains("TVA"))
                                UsuarioTVA = "TVA" + TipoUsuario;
                            else if (ViewState["userDesencriptado"].ToString().ToUpper().Contains("PTV"))
                                UsuarioTVA = "PTV" + TipoUsuario;
                            else
                                UsuarioTVA = "TVA" + TipoUsuario;
                        }
                        else
                        {
                            isUserName = true;
                            usuario = ViewState["userDesencriptado"].ToString().ToUpper();
                            UserData = MngNegocioEmpleadoRol.GetUserDataByNumEmpleado("", usuario);
                            string NumUsua = (UserData.GetElementsByTagName("NUMUSUA").Count > 0) ? UserData.GetElementsByTagName("NUMUSUA")[0].InnerText : "";
                            EmId = (UserData.GetElementsByTagName("NUMEMPL").Count > 0) ? UserData.GetElementsByTagName("NUMEMPL")[0].InnerText : "";
                            if (ViewState["userDesencriptado"].ToString().ToUpper().Contains("TVA"))
                                UsuarioTVA = "TVA" + NumUsua;
                            else if (ViewState["userDesencriptado"].ToString().ToUpper().Contains("PTV"))
                                UsuarioTVA = "PTV" + NumUsua;
                            else
                                UsuarioTVA = "TVA" + NumUsua;
                        }                       
                        isUserName = true;
                        ObtieneDatosUsuario(UserData);
                        ViewState["Opcion"] = "";
                    }
                }
                else {
                    return;
                    txtContraseña.Text = "";
                    txtUsuario.Text = "";                
                }

            }
            catch (Exception ex)
            {
                EncuestasMoviles.Clases.Error.ManejadorErrores(ex, Session["UserName"].ToString(), Session["UserDomain"].ToString(), Session["userMachineName"].ToString(), Session["UserIP"].ToString(), "frmEncuestas", int.Parse(Session["numeroUsuario"].ToString()));
            }
        }

        public int ValidaIP(string IP, IList<IntentosUserXIP> source)
        {
            int validacion = 0;

            foreach (IntentosUserXIP item in source)
            {
                if (item.NoIP == IP && (item.TipoIntento == 2 || item.TipoIntento == 3))
                {
                    validacion += item.NumIntento;
                }
            }

            return validacion;
        }

        protected void btnLogin_Click(object sender, ImageClickEventArgs e)
        {
            xml = new XmlDocument();
            DataSet ds = new DataSet();
            string Pass_Desencriptado = "";
           
            IPUsr = ObtenerIPCliente();
            ViewState["IPUsr"] = IPUsr;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            try
            {

                try
                {

                    string ruta = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlavePrivada"], Azteca.Utility.Security.enmTransformType.intDecrypt);
                    string Passphrase = "";
                    try
                    {
                        Passphrase = (string)Registry.LocalMachine.OpenSubKey(_ChyperRijndael.Transmute(ConfigurationSettings.AppSettings["Registro"], Azteca.Utility.Security.enmTransformType.intDecrypt)).GetValue("passphrase");
                    }
                    catch
                    {
                        //Esto es para Win 7 64 bits

                        RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                        localKey = localKey.OpenSubKey(_ChyperRijndael.Transmute(ConfigurationSettings.AppSettings["Registro"], Azteca.Utility.Security.enmTransformType.intDecrypt));
                        Passphrase = localKey.GetValue("passphrase").ToString();
                        localKey.Dispose();
                    }
                    StreamReader stream = new StreamReader(PGPUtil.DesencriptarTexto(txtContraseña.Text,
                                             File.OpenRead(ruta),
                                             null, Passphrase.ToCharArray()).Datos);
                    StreamReader streamUser = new StreamReader(PGPUtil.DesencriptarTexto(txtUsuario.Text,
                                             File.OpenRead(ruta),
                                             null, Passphrase.ToCharArray()).Datos);

                    Pass_Desencriptado = stream.ReadToEnd();
                    userDesencriptado = streamUser.ReadToEnd();
                    ViewState["userDesencriptado"] = userDesencriptado;
                    TipoUsuario = userDesencriptado.ToUpper().Replace("TVA", "").Replace("PTV", "");
                    ViewState["TipoUsuario"] = TipoUsuario;
                    if (isNumeric(TipoUsuario))
                    {
                        isUserName = false;
                        UsuarioTVA = userDesencriptado.ToUpper();
                        usuario = userDesencriptado.ToUpper();

                        XmlDocument DatosUsua = MngNegocioEmpleadoRol.GetUserDataByNumEmpleado(TipoUsuario, "");
                        string NumUsua = (DatosUsua.GetElementsByTagName("NUMUSUA").Count > 0) ? DatosUsua.GetElementsByTagName("NUMUSUA")[0].InnerText : "";
                        EmId = (DatosUsua.GetElementsByTagName("NUMEMPL").Count > 0) ? DatosUsua.GetElementsByTagName("NUMEMPL")[0].InnerText : "";
                        if (userDesencriptado.ToUpper().Contains("TVA"))
                            UsuarioTVA = "TVA" + TipoUsuario;
                        else if (userDesencriptado.ToUpper().Contains("PTV"))
                            UsuarioTVA = "PTV" + TipoUsuario;
                        else
                            UsuarioTVA = "TVA" + TipoUsuario;
                    }
                    else
                    {
                        isUserName = true;
                        usuario = userDesencriptado.ToUpper();
                        XmlDocument DatosUsua =MngNegocioEmpleadoRol.GetUserDataByNumEmpleado("", usuario);
                        string NumUsua = (DatosUsua.GetElementsByTagName("NUMUSUA").Count > 0) ? DatosUsua.GetElementsByTagName("NUMUSUA")[0].InnerText : "";
                        EmId = (DatosUsua.GetElementsByTagName("NUMEMPL").Count > 0) ? DatosUsua.GetElementsByTagName("NUMEMPL")[0].InnerText : "";
                        if (userDesencriptado.ToUpper().Contains("TVA"))
                            UsuarioTVA = "TVA" + NumUsua;
                        else if (userDesencriptado.ToUpper().Contains("PTV"))
                            UsuarioTVA = "PTV" + NumUsua;
                        else
                            UsuarioTVA = "TVA" + NumUsua;
                    }

                    //Primeras Validacion Tipo de Usuario (Red o TVA)
                    IntentosXIP = MngNegocioBloqueoIP.ConsultaUltimoAccesos();

                    if (ValidaIP(IPUsr, IntentosXIP) >= 10)
                    {
                        string strMessage = string.Empty;
                        strMessage += strMessage == string.Empty ? "" : "<br>";
                        strMessage += " * Su IP ha sido bloqueada";
                        strMessage += "<br>";
                        tdError.InnerHtml = strMessage;
                        tdError.Visible = true;
                        txtUsuario.Text = usuario;
                        GuardaLogAcceso(8);
                        return;
                    }


                    //Aqui se debe de mandar a validar si el usuario esta bloqueado por Intentos fallidos
                    UserBlock = MngNegocioBloqueoUsuario.ConsultaUsuarioBloqueadoXIdUsuario(usuario.ToUpper().ToString(), "1");

                    if (UserBlock.Count > 0)
                    {
                        //El Usuario ya ha sido bloqueado
                        string strMessage = string.Empty;
                        strMessage += " * El Usuario ha sido " + UserBlock[0].TipoBloqueo.DescTipoBloqueo;
                        strMessage += "; Su cuenta se Desbloqueara Automaticamente en 10 Minutos";
                        txtUsuario.Text = usuario;
                        txtError.InnerText = strMessage;
                        GuardaLogAcceso(9);
                        return;
                    }

                    string respuesta = string.Empty;

                    #region Login

                    LlaveMaestra.Service objServicio = new LlaveMaestra.Service();
                    //LDAPUser ldapUser = new LDAPUser();
                    string resp = "";
                    try
                    {
                        resp = objServicio.gsc_llave(userDesencriptado.ToUpper(), Pass_Desencriptado);
                    }
                    catch
                    {
                        
                    }
                    
                   

                    if (resp.Contains("[OK]"))
                    {
                        if (AutenticaUsuario.Validar("", userDesencriptado, Pass_Desencriptado))
                        {
                            
                            XmlDocument UserData = new XmlDocument();
                            if (isUserName)
                                UserData =MngNegocioEmpleadoRol.GetUserDataByNumEmpleado("",userDesencriptado);
                            else
                            {
                                string temp = resp;
                                temp = temp.Substring(temp.IndexOf("NumEmp=")).Replace("NumEmp=","").Substring(1);
                                temp = temp.Substring(0, temp.IndexOf("\""));


                                UserData = MngNegocioEmpleadoRol.GetUserDataByNumEmpleado(temp, "");
                                isUserName = true;
                            }
                            string NumEmpl = (UserData.GetElementsByTagName("NUMEMPL").Count > 0) ? UserData.GetElementsByTagName("NUMEMPL")[0].InnerText : "";

                            List<THE_SesionUsuario> existeSesion =MngNegocioUsuarioSesion.ExisteSesionUsuario(Int32.Parse(NumEmpl));

                            if (existeSesion.Count > 0)
                            {
                                var listusr = (from Atributos in existeSesion
                                               orderby Atributos.IdSesion
                                              select Atributos).Take(1);
                                foreach (var itemList in listusr)
                                {
                                   ViewState["vsIdSesion"]=itemList.IdSesion;
                                   Session["IdSesion"] = itemList.IdSesion;
                                }
                                ViewState["vsIdUser"] = "";
                                ViewState["Opcion"] = "ContinuaSesion";

                                var sw = new StringWriter();
                                var xw = new XmlTextWriter(sw);
                                UserData.WriteTo(xw);

                                ViewState["vsUserdata"] = sw.ToString();
                                ViewState["vsIdUser"] = NumEmpl;
                                ctrlMessageBox.AddMessage("Usted tiene una sesion abierta ¿Desea iniciar otra sesion en este equipo?", MessageBox.enmMessageType.Attention, true, true, "Sesion", "Valida Sesion Existente");
                            }
                            else
                            {
                                THE_SesionUsuario ObjSession = new THE_SesionUsuario();
                                ObjSession.DirIP = IPUsr;
                                ObjSession.FechaCreacion = DateTime.Now;
                                ObjSession.EmplLlavPr = new THE_Empleado { EmpleadoLlavePrimaria = int.Parse(NumEmpl) };
                                MngNegocioUsuarioSesion.GuardaSesionUsuario(ObjSession);

                                List<THE_SesionUsuario> SesionExist = MngNegocioUsuarioSesion.ExisteSesionUsuario(Int32.Parse(NumEmpl));
                                if (SesionExist.Count > 0)
                                {
                                    var listu = (from Atributos in SesionExist
                                                   orderby Atributos.IdSesion
                                                   select Atributos).Take(1);
                                    foreach (var itemList in listu)
                                    {
                                        Session["IdSesion"] = itemList.IdSesion;
                                    }
                                }
                                ObtieneDatosUsuario(UserData);
                            }
                            
                        }
                        else
                        { 
                            ValidaBloqueosErrorPass(); 
                        }
                    }
                    else
                    {

                        try
                        {
                            if (isNumeric(TipoUsuario))
                                respuesta = Llave.validaEmpleado(userDesencriptado, Pass_Desencriptado);
                            else
                                respuesta = Llave.validaEmpleado(UsuarioTVA, Pass_Desencriptado);
                            xml.LoadXml(respuesta);
                        }
                        catch (Exception ex)
                        {
                            THE_LogError oLogErrores = new THE_LogError();
                            oLogErrores.EmplUsua = UsuarioTVA.Replace("TVA", "").Replace("PTV", "");
                            oLogErrores.DirIP = IPUsr;
                            oLogErrores.Error = ex.Message + "\n" + ex.StackTrace.ToString();
                            oLogErrores.Pantalla = "Default.aspx";
                            oLogErrores.MachineName = "";
                            oLogErrores.FechaCreacion = DateTime.Now;
                            oLogErrores.Dominio = Request.Url.Host.ToLower();
                            MngNegocioLogErrores.GuardarLogErrores(oLogErrores);
                        }

                        if ((respuesta.IndexOf("Respuesta=\"[OK]\"") != -1) || respuesta.IndexOf("0 - [") != -1)
                        {

                            //Aqui se debe de mandar a validar si el usuario esta bloqueado por Intentos fallidos
                            IList<THE_BloqueoUsuario> UserBlockInactivo = MngNegocioBloqueoUsuario.ConsultaUsuarioBloqueadoXIdUsuario(usuario.ToUpper().ToString(), "2");
                            if (UserBlockInactivo.Count > 0)
                            {
                                //El Usuario ya ha sido bloqueado
                                string strMessage = string.Empty;
                                strMessage += strMessage == string.Empty ? "" : "<br>";
                                strMessage += " * El Usuario ha sido bloqueado por : " + UserBlockInactivo[0].TipoBloqueo.DescTipoBloqueo;
                                strMessage += "<br>";
                                strMessage += "El bloqueo se quitará automáticamente después los 10 minutos";                                
                                ClientScript.RegisterStartupScript(Page.GetType(), "AlertBloqueoInactivo" + 3, "<script>alert('El Usuario ha sido bloqueado por " + UserBlock[0].TipoBloqueo.DescTipoBloqueo + ". Para desbloquearlo deberá realizar la solicitud en DATASEC');</script>");
                                tdError.InnerHtml = strMessage;
                                tdError.Visible = true;
                                txtUsuario.Text = usuario;
                                GuardaLogAcceso(9);
                                return;
                            }

                            tdError.Visible = false;
                            tdError.InnerHtml = "";

                            string numeroUsuario = xml.FirstChild.ChildNodes[0].Attributes["NumEmp"].Value;
                            XmlDocument UserData =MngNegocioEmpleadoRol.GetUserDataByNumEmpleado(numeroUsuario, "");

                            ObtieneDatosUsuario(UserData);
                        }
                        else
                        {
                            ValidaBloqueosErrorPass();
                        }
                    }

                }
                catch (Exception ex)
                {
                    THE_LogError oLogErrores = new THE_LogError();
                    oLogErrores.EmplUsua = UsuarioTVA.Replace("TVA", "").Replace("PTV", "");
                    oLogErrores.DirIP = IPUsr;
                    oLogErrores.Error = ex.Message + "\n" + ex.StackTrace.ToString();
                    oLogErrores.Pantalla = "Default.aspx";
                    oLogErrores.MachineName = "";
                    oLogErrores.FechaCreacion = DateTime.Now;
                    oLogErrores.Dominio = Request.Url.Host.ToLower();
                    MngNegocioLogErrores.GuardarLogErrores(oLogErrores);
                    txtUsuario.Text = "";
                    GuardaLogAcceso(2);
                }
            }
            catch (Exception ex)
            {
                THE_LogError oLogErrores = new THE_LogError();
                oLogErrores.EmplUsua = UsuarioTVA.Replace("TVA", "").Replace("PTV", "");
                oLogErrores.DirIP = IPUsr;
                oLogErrores.Error = ex.Message + "\n" + ex.StackTrace.ToString();
                oLogErrores.Pantalla = "Default.aspx";
                oLogErrores.MachineName = "";
                oLogErrores.FechaCreacion = DateTime.Now;
                oLogErrores.Dominio = Request.Url.Host.ToLower();
                MngNegocioLogErrores.GuardarLogErrores(oLogErrores);
            }
                    #endregion  
        }

        protected void btnCloseSession_Click(object sender, EventArgs e)
        {
            this.txtUsuario.Text = "";            
            Session.Clear();
            HidUsr.Value = "0";            
        }

        private string ObtenerIPCliente()
        {
            string IP1 = string.Empty;
            IP1 = Request.ServerVariables["REMOTE_ADDR"].ToString();
            return IP1;
        }

        private string CargaFecha()
        {
            string Fecha = "";
            string Dia = " ";

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    Dia = "Lunes";
                    break;
                case DayOfWeek.Tuesday:
                    Dia = "Martes";
                    break;
                case DayOfWeek.Wednesday:
                    Dia = "Miercoles";
                    break;
                case DayOfWeek.Thursday:
                    Dia = "Jueves";
                    break;
                case DayOfWeek.Friday:
                    Dia = "Viernes";
                    break;
                case DayOfWeek.Saturday:
                    Dia = "Sábado";
                    break;
                case DayOfWeek.Sunday:
                    Dia = "Domingo";
                    break;
            }


            Fecha = "Hoy es: " + Dia + " " + DateTime.Now.Day + " de " + BLL_EncuestasMoviles.Funciones.RegresaMes(DateTime.Now.Month) + " de " + DateTime.Now.Year;

            return Fecha;
        }

        public bool isNumeric(string expression)
        {
            if (expression == null)
                return false;
            try
            {
                int number = int.Parse(expression);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void ObtieneDatosUsuario(XmlDocument UserData)
        {
            string UserName = (UserData.GetElementsByTagName("NUMUSUA").Count > 0) ? UserData.GetElementsByTagName("NUMUSUA")[0].InnerText : "";
            string Puesto = (UserData.GetElementsByTagName("PUESTO").Count > 0) ? UserData.GetElementsByTagName("PUESTO")[0].InnerText : "";
            string NumEmpl = (UserData.GetElementsByTagName("NUMEMPL").Count > 0) ? UserData.GetElementsByTagName("NUMEMPL")[0].InnerText : "";
            string NombEmpl = (UserData.GetElementsByTagName("NOMBEMPL").Count > 0) ? UserData.GetElementsByTagName("NOMBEMPL")[0].InnerText : "";
            string PuestoNomb = (UserData.GetElementsByTagName("PUESTONOMB").Count > 0) ? UserData.GetElementsByTagName("PUESTONOMB")[0].InnerText : "";


           if (!isUserName) usuario = xml.FirstChild.ChildNodes[0].Attributes["IdUsuario"].Value;

            if (UserName != string.Empty)
            {
                string CurrentGuid = string.Empty;
                CurrentGuid = Guid.NewGuid().ToString();
                CurrGuid = CurrentGuid;
                byte[] hash = Utilerias.GetHashKey(CurrentGuid);

                System.Net.IPAddress LongIp = System.Net.IPAddress.Parse(ViewState["IPUsr"].ToString());

                Session["nombreUsuario"] = NombEmpl;
                Session["numeroUsuario"] = NumEmpl;
                Session["UserName"] = UserName;
                Session["UserDomain"] = Request.Url.Host.ToLower();
                Session["UserIP"] = ViewState["IPUsr"].ToString();
                 try
                { Session.Add("userMachineName", Dns.GetHostEntry(LongIp).HostName); }
                catch
                { Session.Add("userMachineName", "N/A"); }
                Session["UserPuesto"] = Puesto;
                Session["NomPuesto"] = PuestoNomb;

                NumeroEmpleado = NumEmpl;
                hdnNumUsuario.Value = NumEmpl;
                GuardaLogAcceso(1);
                hdnLogIn.Value = usuario;
                HidUsr.Value = "Active";

                AccessOK();                                
                ClientScript.RegisterStartupScript(this.GetType(), "ShowLogout" + 2, "<script>ShowLogout();</script>");
            }
            else
            {                
                txtError.InnerText = "El usuario no tiene permisos para acceder al sistema";
                GuardaLogAcceso(2);
            }
        }

        private void ValidaBloqueosErrorPass()
        {
            //Error de Autenticacion por Contraseña          
            string val = UsuarioTVA.ToUpper();
            txtError.InnerText = "Error al escribir el Usuario ó Contraseña !!";
            GuardaLogAcceso(3);

            //Aqui va la consulta para validar si ya tiene los logs
            List<IntentosUsuario> IntentosXUsuario = MngNegocioBloqueoUsuario.ConsultaUltimoAccesosUsuario(UsuarioTVA.ToUpper());
            if (IntentosXUsuario.Count == 1)
            {
                if (IntentosXUsuario[0].TipoIntento == 3 && IntentosXUsuario[0].NumIntento == 3) //Bloquea Usuario
                {
                    //Aqui se manda a Bloquear
                    THE_BloqueoUsuario oBloqueadoUsuario = new THE_BloqueoUsuario();

                    TDI_TipoBloqueo oTipoBloqueo = new TDI_TipoBloqueo();
                    oTipoBloqueo.CveTipoBloqueo = 1;
                    oBloqueadoUsuario.EmplLlavPr = new THE_Empleado() { EmpleadoLlavePrimaria = int.Parse(EmId) };
                    oBloqueadoUsuario.TipoBloqueo = oTipoBloqueo;
                    oBloqueadoUsuario.Usuario = usuario.ToUpper();
                    if (MngNegocioBloqueoUsuario.GuardaUsuarioBloqueado(oBloqueadoUsuario))
                    {
                        IList<THE_BloqueoUsuario> UserBlocked = MngNegocioBloqueoUsuario.ConsultaUsuarioBloqueadoXIdUsuario(UsuarioTVA.ToUpper().ToString(), "1");
                        string strMessage = string.Empty;
                        strMessage += strMessage == string.Empty ? "" : "<br>";
                        strMessage += "El Usuario ha sido bloqueado por : " + UserBlocked[0].TipoBloqueo.DescTipoBloqueo;
                        strMessage += "<br>";
                        strMessage += "Favor de Solicitar su desbloqueo por DATASEC";
                        
                        ClientScript.RegisterStartupScript(Page.GetType(), "AlertBloqueo" + 1, "<script>alert('El Usuario ha sido bloqueado por " + UserBlock[0].TipoBloqueo.DescTipoBloqueo + ". Para desbloquearlo deberá realizar la solicitud en DATASEC');</script>");
                        tdError.InnerHtml = strMessage;
                        tdError.InnerHtml = strMessage;
                        tdError.Visible = true;
                    }
                }
            }

            IntentosXIP = MngNegocioBloqueoIP.ConsultaUltimoAccesos();
            if (ValidaIP(IPUsr, IntentosXIP) >= 10)
            {
                TDI_TipoBloqueo oTipoBloqueo = new TDI_TipoBloqueo();
                oTipoBloqueo.CveTipoBloqueo = 3;
                THE_BloqueoIP oBloqueoIP = new THE_BloqueoIP();
                oBloqueoIP.TipoBloqueo = oTipoBloqueo;
                oBloqueoIP.IP = IPUsr;
                if (MngNegocioBloqueoIP.GuardaIPBloqueada(oBloqueoIP))
                {
                    string strMessage = string.Empty;
                    strMessage += strMessage == string.Empty ? "" : "<br>";
                    strMessage += " * Su IP ha sido bloqueada";
                    strMessage += "<br>";                    
                }
            }
            if (isUserName)
                txtUsuario.Text = usuario;
            else
                txtUsuario.Text = UsuarioTVA;
        }

        private void GuardaLogAcceso(int TipoLogAcceso)
        {
            try
            {
                List<TDI_TipoAcceso> lstTipoAcceso = (List<TDI_TipoAcceso>)MngNegocioTipoAcceso.ObtenerTipoAcceso();
                TDI_LogAcceso oLog;
                if (lstTipoAcceso.Count > 0)
                {
                    var tipoAcceso = from oTipo in lstTipoAcceso where oTipo.IdTipoAcceso == TipoLogAcceso select oTipo;
                    TDI_TipoAcceso oTipoAcceso = tipoAcceso.ToList<TDI_TipoAcceso>().First<TDI_TipoAcceso>();

                    if (oTipoAcceso != null)
                    {
                        oLog = new TDI_LogAcceso();
                        oLog.EmpleadoUsua = UsuarioTVA.ToUpper();
                        oLog.IdTipoAcceso = oTipoAcceso;
                        oLog.LogAccesoDominio = Environment.UserDomainName;
                        oLog.LogAccesoFecha = DateTime.Now;
                        oLog.LogAccesoIP = IPUsr;
                        oLog.EmplLlavPr = new THE_Empleado() { EmpleadoLlavePrimaria = int.Parse(EmId) };

                        System.Net.IPAddress LongIp = System.Net.IPAddress.Parse(IPUsr);
                        try
                        {
                            oLog.LogAccesoIP = Request.ServerVariables["LOCAL_ADDR"];
                            oLog.LogAccesoMaquina = System.Web.HttpContext.Current.Request.UserHostName;
                        }
                        catch
                        {
                            oLog.LogAccesoIP = "";
                            oLog.LogAccesoMaquina = "";
                        }

                        TDI_UsuarioLogin oLogin = new TDI_UsuarioLogin();
                        oLogin.Usuario = UsuarioTVA;
                        oLogin.TipoAcceso = oTipoAcceso;
                        MngNegocioLogAcceso.GuardarLogAcceso(oLog);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        protected void AccessOK()
        {
            Response.Redirect("Pages/frmDispoEncuesta.aspx", false);
        }
    }
}
