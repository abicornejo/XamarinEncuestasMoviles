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
using Android.Media;
using System.IO;
using System.Net;

namespace ENCUESTA_MOVIL.Servicio
{
    [Service]    
    class ClsDemonio : Service
    {
        System.Threading.Timer _timer;
        tva.WSEncuestaMovil asmxEM = new tva.WSEncuestaMovil();
        bool Notificado = false;
        /// <summary>
        /// Metodo que se ejecuta toda vez que se manda a llamar a la clase ClsDemonio
        /// Este inicia el servicio
        /// </summary>
        /// <param name="intent"></param>
        /// <param name="startId"></param>
        public override void OnStart(Android.Content.Intent intent, int startId)
        {
            base.OnStart(intent, startId);
            tva.AuthHeader Credentials = new tva.AuthHeader();
            string settings = "";
            string userpassword = "";
            try
            {
                using (StreamReader sr = new StreamReader(Assets.Open("Validator.archive")))
                {
                    settings = sr.ReadToEnd();
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

            DoStuff();
            
        }
        private MediaPlayer _player;
        NotificationManager _notificationManager;
        /// <summary>
        /// la función mada una notificación al usuario agregando un icono
        /// en la barra de estado del dispositivo.
        /// </summary>
        private void Notificaaciones()
        {
            _player = MediaPlayer.Create(this, Resource.Raw.destroy);
            _player.Looping = false;
            _notificationManager = this.GetSystemService(NotificationService) as NotificationManager;
            if (_notificationManager == null)
            { throw new Exception("No se encuentra la referencia a la notificacion"); }

            // Definimos icono, descripcion de la notificacion, y el tiempo en el que se mostrara
            Notification notification = new Notification(Resource.Drawable.Encuesta, "Notificaciones de encuestas activado",Java.Lang.JavaSystem.CurrentTimeMillis());

            // Indicamos la actividad que mostrara al pulsar la notificación
            PendingIntent contentIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(ActividadInicial)), 0);

            // Definimos descripción de la notificacion y la actividad a mostrar
            notification.SetLatestEventInfo(this, "NOTIFICACIÓN DE ENCUESTAS", "ENCUESTA PENDIENTE DE CONTESTAR", contentIntent);
            notification.Flags = NotificationFlags.OnlyAlertOnce | NotificationFlags.AutoCancel;                  
            //Ejecutanos la notificacion
            _notificationManager.Notify(666, notification);

            Servicio.ClsVariables.c++;
            //Toast.MakeText(this, "Encuesta nueva No. " + Servicio.ClsVariables.c, ToastLength.Short);
        }
        /// <summary>
        /// Esta función manda a llamar metodos del Web Service para verificar
        /// si existen encuestas asignadas pendientes de contestar, si existen
        /// encuestas manda a llamar a la función Notificaaciones.
        /// Esta función se manda a llamar periodicamente mediante un Timer.
        /// </summary>
        public void DoStuff()
        {
            _timer = new System.Threading.Timer((o) =>
            {
                tva.THE_Dispositivo[] disp = asmxEM.ObtenerDispositivoNumero(ClsVariables.NumeroTel);
                int idEncuestaPendiente = asmxEM.NotificacionEncuestapendiente(disp[0].IdDispositivo,ClsVariables.NumeroTel);
                if (idEncuestaPendiente != -1)
                {
                    if (!Notificado)
                    {
                        Notificado = true;
                        Notificaaciones();
                        _player.Start();
                    }
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