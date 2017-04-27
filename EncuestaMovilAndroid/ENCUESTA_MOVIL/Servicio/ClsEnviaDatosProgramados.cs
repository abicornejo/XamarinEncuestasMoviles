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
using Mono.Data.Sqlite;
using System.IO;
using System.Net;

namespace ENCUESTA_MOVIL.Servicio
{
    [Service]
    public class ClsEnviaDatosProgramados : Service
    {
        System.Threading.Timer _timer;
        bool exists;
        int bandera;
        SqliteConnection connection;
        tva.WSEncuestaMovil asmxEM = null;
        /// <summary>
        /// Inicia el renderizado de la aplicación para pintarlo en pantalla
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="startId"></param>
        public override void OnStart(Android.Content.Intent intent, int startId)
        {
            base.OnStart(intent, startId);
            try
            {
                asmxEM = new tva.WSEncuestaMovil();
            
            tva.AuthHeader Credentials = new tva.AuthHeader();
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
            catch (WebException error)
            { }
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
            DoStuff();

        }
        /// <summary>
        /// Metodo que se ejecuta al inicar el servicio, este ejecuta un hilo de tipo Timer
        /// que determinado tiempo intenta enviar los datos que 
        /// estan aun en base de datos
        /// esto sucede unicamente si la conexion ha internet del dispositivo ha fallado.
        /// </summary>
        public void DoStuff()
        {
            _timer = new System.Threading.Timer((o) =>
            {
                SqliteCommand leedatos = connection.CreateCommand();
                leedatos.CommandText = "SELECT [IdDispositivo],[IdEncuesta],[IdsPreguntaRespuesta] from [DatosEncuestaEnvia]";
                try
                {
                    int IdDispositivo=0;
                    int IdEncuesta=0;
                    string[] strRespuesta=null;
                    var rr = leedatos.ExecuteReader();
                    while (rr.Read())
                    {
                        IdDispositivo = int.Parse(rr["IdDispositivo"].ToString());
                        IdEncuesta = int.Parse(rr["IdEncuesta"].ToString());
                        strRespuesta = rr["IdsPreguntaRespuesta"].ToString().Split('&');                        
                    }
                    if (strRespuesta.Length > 0)
                    {
                        bool result = false;
                        try
                        {
                            result = asmxEM.GuardaEncuestaContestada(IdDispositivo, IdEncuesta, strRespuesta,ClsVariables.NumeroTel);
                        }
                        catch(WebException exep)
                        {}
                        if (result == true)
                        {
                            SqliteCommand contents = connection.CreateCommand();
                            contents.CommandText = "DELETE from [DatosEncuesta]";
                            contents.ExecuteNonQuery();

                            SqliteCommand contentsEnc = connection.CreateCommand();
                            contentsEnc.CommandText = "DELETE from [DatosEncuestaEnvia]";
                            contentsEnc.ExecuteNonQuery();

                            _timer.Dispose();
                        }
                    }
                }
                catch (Exception exep)
                {
                    
                }
            }
            , null, 0, 60000);
        }

        /// <summary>
        /// Este metodo se ejecuta al pasar datos de una actividad a otra,
        /// en estos casos no se utiliza pero es necesario tenerla por la clase Service
        /// </summary>
        /// <param name="intent"></param>
        /// <returns></returns>
        public override Android.OS.IBinder OnBind(Android.Content.Intent intent)
        {
            throw new NotImplementedException();
        }
    }
}