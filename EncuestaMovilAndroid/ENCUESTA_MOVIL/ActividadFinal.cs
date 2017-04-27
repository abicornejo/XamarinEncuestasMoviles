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
using Android.Database.Sqlite;
using System.Threading;
using Java.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


using System.Collections;
using System.IO.IsolatedStorage;

namespace ENCUESTA_MOVIL
{
    [Activity(Label = "My Activity")]
    public class ActividadFinal : Activity, Java.Lang.IRunnable
    {
        SqliteConnection connection;
        int bandera = 0;
        ProgressDialog progress;
        Java.Lang.Thread threadEnviaDatos;
        int IdDispositivo;
        string[] arrayIds;
        int IdEncuesta = 0;
        bool exists = false;
        Servicio.Listening _receiver;
        tva.WSEncuestaMovil asmxEM = null;
        //localhost.WSEncuestaMovil asmxEM = null;
        /// <summary>
        /// Metodo que se ejecuta toda vez que se manda a llamar al Activity ActividadFinal
        /// Este metodo inicia el renderizado de la pantalla con sus controles.
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            IdDispositivo = Intent.GetIntExtra("IdDispositivo", 0);
            IdEncuesta = Intent.GetIntExtra("IdEncuesta", 0);
            arrayIds = Intent.GetStringArrayExtra("arrayIds");

            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.FinEncuesta);
            
            ImageButton btnEnviar = FindViewById<ImageButton>(Resource.Id.btnAceptarFin);
            btnEnviar.Click += new EventHandler(btnEnviar_Click);
            try
            {
                asmxEM = new tva.WSEncuestaMovil();
                //asmxEM = new localhost.WSEncuestaMovil();
                //asmxEM.Url = asmxEM.Url.Replace("localhost","10.0.2.2");
            }
            catch (WebException error)
            { }
            _receiver = new Servicio.Listening();

            var intentFilter = new IntentFilter(Intent.ActionBootCompleted);
            intentFilter.AddAction(Intent.ActionBootCompleted);
            RegisterReceiver(_receiver, intentFilter);
            StartService(new Intent(this, typeof(Servicio.ClsDemonio)));


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
            //Agrega el nombre de la base de datos al path de la carpeta local y subcarpeta database
            string dbPath = Path.Combine(databaseFolderPath, "Encuestadb.db3");
            //Verifica si existe el archivo mencionado en la parte de arriba

            exists = System.IO.File.Exists(dbPath);
            if (!exists)
            {
                SqliteConnection.CreateFile(dbPath);//Si no existe crea la base de datos
                bandera = 0;//Configura;               
            }
            connection = new SqliteConnection("Data Source=" + dbPath); //Abre la cadena de conexion con la base de datos creado anteriormente

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            connection.Open();//Abre la conexion a la base de datos creado con anterioridad
            try
            {
                tva.AuthHeader Credentials = new tva.AuthHeader();
                //localhost.AuthHeader Credentials = new localhost.AuthHeader();
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
                catch (Exception exep)
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
            catch (WebException errorWS)
            { }
        }
        
        /// <summary>
        /// Esta función se ejecuta cuando algun proceso se manda en segundo plano
        /// para no interrumpir las actividades que tiene prioridad el telefono.
        /// La finalidad de esta función es enviar los resultados de las encuestas contestadas.
        /// </summary>
        public void Run()
        {
            //System.Console.WriteLine("Inicio Run");
            Looper.Prepare();
            if (bandera == 1)
            {
                bool result = false;
                int intentos = 0;
                while (result == false)
                {
                    try
                    {
                        this.MoveTaskToBack(true);
                        int idDisp = IdDispositivo;
                        int idEnc=Servicio.ClsVariables.IdEncuesta;
                        result = asmxEM.GuardaEncuestaContestada(idDisp, idEnc, Servicio.ClsVariables.arrayIds,Servicio.ClsVariables.NumeroTel);
                        //System.Console.WriteLine("Valores IDDISP= " + idDisp + "Valores IDENC= " + idEnc + "Resultado: "  + result);
                    }
                    catch (WebException catchExep)
                    {
                        //System.Console.WriteLine("Error: " + catchExep.Message);
                        int cuentapr = 0;
                        string pr = "";
                        foreach (string presp in Servicio.ClsVariables.arrayIds)
                        {
                            cuentapr++;
                            int totalResp = Servicio.ClsVariables.arrayIds.Length;
                            if (presp != null)
                                if (totalResp == cuentapr)
                                {
                                    pr += presp.ToString();
                                }
                                else
                                {
                                    pr += presp.ToString() + "&";
                                }

                        }
                        SqliteCommand contentsInsert = connection.CreateCommand();
                        String strQuery = "INSERT INTO [DatosEncuestaEnvia] ([IdDispositivo],[IdEncuesta],[IdsPreguntaRespuesta]) values('" + Servicio.ClsVariables.IdDispositivo.ToString() + "','" + Servicio.ClsVariables.IdEncuesta.ToString() + "','" + pr + "');";
                        contentsInsert.CommandText = strQuery;
                        contentsInsert.ExecuteNonQuery();
                        int codigo = this.GetHashCode();
                        StartService(new Intent(this, typeof(Servicio.ClsEnviaDatosProgramados)));
                        threadEnviaDatos.Stop();                        
                        result = true;
                        
                        this.Finish();
                    }
                    if (result == true)
                    {
                        SqliteCommand contents = connection.CreateCommand();
                        contents.CommandText = "DELETE from [DatosEncuesta]";
                        contents.ExecuteNonQuery();
                        threadEnviaDatos.Stop();                        
                        this.Finish();
                    }
                    intentos++;
                    if (intentos == 3)
                    {
                        //System.Console.WriteLine("Se programo envio automatico");
                        int cuentapr = 0;
                        string pr = "";
                        foreach (string presp in Servicio.ClsVariables.arrayIds)
                        {
                            cuentapr++;
                            int totalResp = Servicio.ClsVariables.arrayIds.Length;
                            if (presp != null)
                                if (totalResp == cuentapr)
                                {
                                    pr += presp.ToString();
                                }
                                else
                                {
                                    pr += presp.ToString() + "&";
                                }
                        }
                        SqliteCommand contentsInsert = connection.CreateCommand();
                        String strQuery = "INSERT INTO [DatosEncuestaEnvia] ([IdDispositivo],[IdEncuesta],[IdsPreguntaRespuesta]) values('" + Servicio.ClsVariables.IdDispositivo.ToString() + "','" + Servicio.ClsVariables.IdEncuesta.ToString() + "','" + pr + "');";
                        contentsInsert.CommandText = strQuery;
                        contentsInsert.ExecuteNonQuery();
                        int codigo = this.GetHashCode();
                        StartService(new Intent(this, typeof(Servicio.ClsEnviaDatosProgramados)));
                        threadEnviaDatos.Stop();
                        result = true;
                        this.Finish();
                    }
                }
            }
            Looper.Loop();
            Looper.MyLooper().Quit();
        }    
        
        /// <summary>
        /// Este evento inicia el proceso de envio de datos, que al usar hilos
        /// ejecuta en segundo plano la funcion Run(), para que envie las respuestas.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnEnviar_Click(object sender, EventArgs e)
        {
            bandera = 1;//Envia Datos
            //progress = ProgressDialog.Show(this, "Progreso de envio de datos", "Enviando datos al servidor....", true, true);

            threadEnviaDatos = new Java.Lang.Thread(this);
            threadEnviaDatos.Start();            
        }
    }
}