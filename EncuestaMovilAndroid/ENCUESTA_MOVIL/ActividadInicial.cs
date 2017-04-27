using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Mono.Data.Sqlite;
using System.Net;
using Android.Telephony;

namespace ENCUESTA_MOVIL
{
    [Activity(Label = "Encuestas", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Splash", NoHistory = true)]
    public class ActividadInicial : Activity, Java.Lang.IRunnable
    {
        Java.Lang.Thread threadConfiguraInicial;
        ProgressDialog configuraInicio;
        SqliteConnection connection;
        Servicio.Listening _receiver;
        tva.WSEncuestaMovil asmxEM = null;        
        bool tieneEncuesta = false;        
        public static List<Encuesta> FillEncuesta = new List<Encuesta>();
        tva.TDI_EncuestaDispositivo[] TDI_EncDisp = null;
        bool exists = false;
        int idDispositivo = 0;
        double numetel = 0;
        string[] arrayIds;
        public struct Encuesta
        {
            public string Id;
            public string Pregunta;
            public string[] Respuesta;
        }
               
        /// <summary>
        /// Metodo que se ejecuta toda vez que se manda a llamar al Activity ActividadInicial
        /// Este metodo inicia el renderizado de la pantalla con sus controles.
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle)
        {               
            base.OnCreate(bundle);            
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Inicial);            
            ImageButton btnIniciar = FindViewById<ImageButton>(Resource.Id.btnAceptarInicio);
            btnIniciar.Click += new EventHandler(btnIniciar_Click);

            /// Inicializa la BD en caso de existir las tablas necesarias 
            /// guarda los valores iniciales del dispositivo para no solicitarlos
            /// después de la primera vez que se ejecute la aplicación
            ConfiguraDB();
            IniciaDemonio();
            DatosGenerales();            
            TieneEncuestas();            
        }

        private void TieneEncuestas()
        {
            try
            {
                tva.THE_Dispositivo[] dispos = null;
                try
                {                    
                    #region trae encuestas                       
                    TDI_EncDisp = asmxEM.ObtieneEncuestaPorDispositivo(idDispositivo, numetel);
                    FillEncuesta = new List<Encuesta>();

                    if (TDI_EncDisp != null)
                    {

                        for (int disp = 0; disp < TDI_EncDisp.Length; disp++)
                        {
                            

                            if (TDI_EncDisp[disp].ListaEncuesta != null)
                            {
                                

                                for (int enc = 0; enc < TDI_EncDisp[disp].ListaEncuesta.Length; enc++)
                                {
                                    tieneEncuesta = true;

                                    if (TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg != null)
                                    {
                                        for (int preg = 0; preg < TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg.Length; preg++)
                                        {
                                            arrayIds = new string[TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg.Length];

                                            Encuesta Preguntas = new Encuesta();                                                
                                            Preguntas.Id = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].IdPregunta.ToString();                                                
                                            Preguntas.Pregunta = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].PreguntaDesc;
                                            Preguntas.Respuesta = new string[TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas.Length]; //{ "" + TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].IdRespuesta + "|" + TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].RespuestaDescripcion + "|3", "4|No|4" };

                                            if (TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas != null)
                                            {
                                                for (int resp = 0; resp < TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas.Length; resp++)
                                                {
                                                    Preguntas.Respuesta[resp] = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].IdRespuesta + "|" +
                                                                                TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].RespuestaDescripcion + "|" +
                                                                                TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].IdSiguientePregunta;
                                                }
                                            }

                                            FillEncuesta.Add(Preguntas);
                                            string RespuestasInserta = "";
                                            int cuenta = 0;
                                            foreach (string presp in Preguntas.Respuesta)
                                            {
                                                cuenta++;
                                                int totalResp = Preguntas.Respuesta.Length;
                                                if (totalResp == cuenta)
                                                {
                                                    RespuestasInserta += presp.ToString();
                                                }
                                                else
                                                {
                                                    RespuestasInserta += presp.ToString() + "&";
                                                }

                                            }
                                            using (SqliteCommand com = connection.CreateCommand())
                                            {
                                                String strQuery = "INSERT INTO [DatosEncuesta] ([IdEncuesta],[DescEncuesta],[IdPregunta],[DescPregunta],[Respuestas]) values('" +
                                                    TDI_EncDisp[disp].ListaEncuesta[enc].IdEncuesta + "','" + TDI_EncDisp[disp].ListaEncuesta[enc].NombreEncuesta + "','" + Preguntas.Id + "','" + Preguntas.Pregunta + "','" + RespuestasInserta + "');";
                                                com.CommandText = strQuery;
                                                com.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                catch
                {                    
                }
            }
            catch
            {
            }
        }

        private void IniciaDemonio()
        {
            _receiver = new Servicio.Listening();

            var intentFilter = new IntentFilter(Intent.ActionBootCompleted);
            intentFilter.AddAction(Intent.ActionBootCompleted);
            RegisterReceiver(_receiver, intentFilter);
            StartService(new Intent(this, typeof(Servicio.ClsDemonio)));
        }

        private void DatosGenerales()
        {
            //if (!exists)
            //{                
                tva.AuthHeader Credentials = new tva.AuthHeader();
                try
                {
                    asmxEM = new tva.WSEncuestaMovil();                    

                    string settings = "";
                    string userpassword = "";
                    try
                    {
                        using (StreamReader sr = new StreamReader(Assets.Open("Validator.archive")))
                        {
                            settings = sr.ReadToEnd();
                            sr.Close();
                        }
                    }
                    catch
                    {
                    }

                    Servicio.ClsRijndaels _encript = new Servicio.ClsRijndaels();
                    userpassword = _encript.Transmute(settings, Servicio.ClsRijndaels.enmTransformType.intDecrypt);
                    Credentials.UserName = userpassword;
                    asmxEM.PreAuthenticate = true;
                    Credentials.Password = userpassword;
                    asmxEM.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap11;
                    asmxEM.UnsafeAuthenticatedConnectionSharing = true;
                    asmxEM.AuthHeaderValue = Credentials;

                    System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                    System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                    {
                        return true;
                    };
                }
                catch
                { }

                TelephonyManager tm = (TelephonyManager)GetSystemService(TelephonyService);
                var deviceId = tm.DeviceId;
                var serialNumber = tm.SimSerialNumber;
                string numero = tm.Line1Number;
                string imei = tm.DeviceId;


                tva.THE_Dispositivo[] disp = null;

                try
                {
                    if (numero != "")
                    {
                        numetel = Convert.ToDouble(numero.ToString());
                        disp = asmxEM.ObtenerDispositivoNumero(Convert.ToDouble(imei));
                        idDispositivo = Convert.ToInt32(disp[0].IdDispositivo);
                    }
                    else
                    {
                        disp = asmxEM.ObtenerDispositivoNumero(Convert.ToDouble(imei));    
                        numetel = Convert.ToDouble(disp[0].NumerodelTelefono);
                        idDispositivo = Convert.ToInt32(disp[0].IdDispositivo);
                    }

                    //using (SqliteCommand com = connection.CreateCommand())
                    //{
                    //    if (numero != "")
                    //    {
                    //        String strQuery = "INSERT INTO [DatosDispositivo] ([NumTel],[IMEI]) values('" + Servicio.ClsVariables.NumeroTel.ToString() + "','" + imei + "');";
                    //        com.CommandText = strQuery;
                    //        com.ExecuteNonQuery();
                    //    }
                    //    else
                    //    {

                    //    }
                    //}
                }
                catch
                { }
            //}
            //else
            //{

            //}
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnregisterReceiver(_receiver);
        }

        /// <summary>
        /// Este metodo verifica si existe la base de datos para el almacenamiento de información en el dispositivo
        /// Si no existe la crea
        /// </summary>
        public void ConfiguraDB()
        {
            //Buscamos el directorio personal en el dispositivo
            var personalFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //Buscamos el directorio database en la carpeta personal
            var databaseFolderPath = string.Format(@"{0}/database", personalFolderPath.Substring(0, personalFolderPath.LastIndexOf('/')));
            if (!Directory.Exists(databaseFolderPath))
            {
                Directory.CreateDirectory(databaseFolderPath);//Si no existe la creamos
            }
            if (!Directory.Exists(databaseFolderPath))
                throw new Exception(string.Format("{0} no existe!", databaseFolderPath));//Si no existe quiere decir que en el paso anterior no se creo correctamente el dir

            //var paths = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).ToString() + "/Encuestas.db3";

            string dbPath = Path.Combine(databaseFolderPath, "Encuestadb.db3");
            //Verifica si existe el archivo mencionado en la parte de arriba
            exists = File.Exists(dbPath);
            if (!exists)
                SqliteConnection.CreateFile(dbPath);//Si no existe crea la base de datos
            
            connection = new SqliteConnection("Data Source=" + dbPath); //Abre la cadena de conexion con la base de datos creado anteriormente

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            connection.Open();//Abre la conexion a la base de datos creado con anterioridad

            if (!exists)
            {
                //Por cada comando se realiza la accion en la base de datos (Creacion de tablas, insertar, actualizar, eliminar)
                using (SqliteCommand c = connection.CreateCommand())
                {
                    c.CommandText = "CREATE TABLE [DatosEncuesta] (IdEncuesta ntext, DescEncuesta ntext,IdPregunta ntext,DescPregunta ntext,Respuestas ntext);";
                    c.ExecuteNonQuery();
                }
                using (SqliteCommand cc = connection.CreateCommand())
                {
                    cc.CommandText = "CREATE TABLE [DatosEncuestaEnvia] (IdDispositivo ntext,IdEncuesta ntext,IdsPreguntaRespuesta ntext);";
                    cc.ExecuteNonQuery();
                }
                using (SqliteCommand ccc = connection.CreateCommand())
                {
                    ccc.CommandText = "CREATE TABLE [DatosDispositivo] (NumTel ntext, IMEI ntext);";
                    ccc.ExecuteNonQuery();
                }
                configuraInicio = ProgressDialog.Show(this, "Configurando aplicación", "Instalando y configurando las herramientas" +
               " necesarias para el buen funcionamiento del sistema, espere unos momentos por favor....", true, true);
                threadConfiguraInicial = new Java.Lang.Thread(this);
                threadConfiguraInicial.Start();
            }
        }

        /// <summary>
        /// Esta función se ejecuta cuando algun proceso se manda en segundo plano
        /// para no interrumpir las actividades que tiene prioridad el telefono.
        /// La finalidad de esta función es enviar los resultados de las encuestas contestadas.
        /// </summary>
        public void Run()
        {
            Java.Lang.Thread.Sleep(10000);
            threadConfiguraInicial.Stop();
            configuraInicio.Dismiss();
        }

        /// <summary>
        /// Evento que da inicio a contestar las encuestas asignadas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnIniciar_Click(object sender, EventArgs e)
        {
            Intent inicia = new Intent(this, typeof(Activity1));            
            inicia.SetFlags(ActivityFlags.ClearTop);
            inicia.PutExtra("tieneEncuesta", tieneEncuesta);
            inicia.PutExtra("IdDispositivo", idDispositivo);
            inicia.PutExtra("arrayIds", arrayIds);
            StartActivity(inicia);
            this.Finish();
        }
    }
}