using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Diagnostics;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;
using PushSharp;
using PushSharp.Android;

namespace EncuestasMoviles
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            //Trace.WriteLine("Inicializando servicio.");
            //System.Timers.Timer t = new System.Timers.Timer();
            //t.Interval = 10000;
            //t.Enabled = true;
            //t.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            //t.Start();

        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //string currentRequest = Request.RawUrl.Trim().ToLower();
            //if (currentRequest.IndexOf("?wsdl") > 0 || currentRequest.IndexOf("?disco") > 0)
            //{
            //    Response.Redirect("~/ServicesAsmx/WebService_EncuestasMoviles.asmx");
            //}

        }
        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

        }

        void Session_End(object sender, EventArgs e)
        {
           
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
             THE_PUSTRAN objTran = new THE_PUSTRAN();
                objTran.DescTran = "Se ha enviado desde el global asax";
                objTran.FechaCreaEncuesta = DateTime.Now;
                MngNegocioPushTran.GuardarTranPush(objTran);
                

                List<TDI_Notificaciones> notifica = MngNegocioNotificaciones.ObtieneNotificaciones();


                objTran.DescTran = "Hay " + notifica.Count + " pendientes por enviar";
                MngNegocioPushTran.GuardarTranPush(objTran);



                if (notifica.Count > 0)
                {

                    foreach(TDI_Notificaciones element in notifica){

                PushService PushClient = new PushService();

                PushClient.Events.OnDeviceSubscriptionExpired += new PushSharp.Common.ChannelEvents.DeviceSubscriptionExpired(Events_OnDeviceSubscriptionExpired);
                PushClient.Events.OnDeviceSubscriptionIdChanged += new PushSharp.Common.ChannelEvents.DeviceSubscriptionIdChanged(Events_OnDeviceSubscriptionIdChanged);
                PushClient.Events.OnChannelException += new PushSharp.Common.ChannelEvents.ChannelExceptionDelegate(Events_OnChannelException);
                PushClient.Events.OnNotificationSendFailure += new PushSharp.Common.ChannelEvents.NotificationSendFailureDelegate(Events_OnNotificationSendFailure);
                PushClient.Events.OnNotificationSent += new PushSharp.Common.ChannelEvents.NotificationSentDelegate(Events_OnNotificationSent);
                PushClient.Events.OnChannelCreated += new PushSharp.Common.ChannelEvents.ChannelCreatedDelegate(Events_OnChannelCreated);
                PushClient.Events.OnChannelDestroyed += new PushSharp.Common.ChannelEvents.ChannelDestroyedDelegate(Events_OnChannelDestroyed);

                //PushClient.Events.OnNotificationSent += HandleOnNotificationSent;                        
                PushClient.StartGoogleCloudMessagingPushService(
                    new GcmPushChannelSettings("248326017313", "AIzaSyBl8zK7NlLAFc0AkWtuJgsEt3VlXGNARu4", "encuestas.moviles"));

                //PushClient.StartGoogleCloudMessagingPushService(new GcmPushChannelSettings("248326017313", "AIzaSyBl8zK7NlLAFc0AkWtuJgsEt3VlXGNARu4", "encuestas.moviles"));

        //        PushClient.QueueNotification(NotificationFactory.AndroidGcm()
        //.ForDeviceRegistrationId("APA91bFKLIddkts8DxU-qlLdrlOw3fhrIiPiWHIBsNRv8f0HosWF_e45WeFhjLYaEp-CaHnyR5sh9FLXcO1T-U-8-Pgkwy6YoylF9_NusAAbqHqme0F7mviyh1mthrn1hqP_PytyzpxSpir6pK96AIWa-CC8t5humg")
        //.WithCollapseKey("NONE")
        //.WithJson("{\"alert\":\"Alert Text!\",\"badge\":\"7\"}"));


                PushClient.QueueNotification(NotificationFactory.AndroidGcm()
                                               .ForDeviceRegistrationId(element.TokenDispositivo.ToString())
                                               .WithCollapseKey("NONE")
                                  .WithJson("{\"alert\":\"Alert Text!\",\"badge\":\"7\"}"));
                Console.WriteLine("Waiting for Queue to Finish...");
                
                PushClient.StopAllServices(true);
                    }
                }
                else
                {
                    Trace.WriteLine("El PROCESO NO ARROJO NINGUN RESULTADO");
                }



            }
            catch (Exception ms)
            {
                
                THE_PUSTRAN objTran = new THE_PUSTRAN();
                objTran.DescTran = "OCURRIO ERROR: " + ms.Message;
                objTran.FechaCreaEncuesta = DateTime.Now;                
                MngNegocioPushTran.GuardarTranPush(objTran);
                Console.WriteLine("OCURRIO ERROR: " + ms.Message);


            }
        }


        static void Events_OnDeviceSubscriptionIdChanged(PushSharp.Common.PlatformType platform, string oldDeviceInfo, string newDeviceInfo, PushSharp.Common.Notification notification)
        {
            //Currently this event will only ever happen for Android GCM
            Console.WriteLine("Device Registration Changed:  Old-> " + oldDeviceInfo + "  New-> " + newDeviceInfo);
        }

        static void Events_OnNotificationSent(PushSharp.Common.Notification notification)
        {
            try
            {
                Console.WriteLine("Enviado a " + notification.Tag.ToString());
                Console.WriteLine("Sent: " + notification.Platform.ToString() + " -> " + notification.ToString());
            }
            catch
            {

            }
        }

        static void Events_OnNotificationSendFailure(PushSharp.Common.Notification notification, Exception notificationFailureException)
        {
            Console.WriteLine("Failure: " + notification.Platform.ToString() + " -> " + notificationFailureException.Message + " -> " + notification.ToString());
        }

        static void Events_OnChannelException(Exception exception, PushSharp.Common.PlatformType platformType, PushSharp.Common.Notification notification)
        {
            Console.WriteLine("Channel Exception: " + platformType.ToString() + " -> " + exception.ToString());
        }

        static void Events_OnDeviceSubscriptionExpired(PushSharp.Common.PlatformType platform, string deviceInfo, PushSharp.Common.Notification notification)
        {
            Console.WriteLine("Device Subscription Expired: " + platform.ToString() + " -> " + deviceInfo);
        }

        static void Events_OnChannelDestroyed(PushSharp.Common.PlatformType platformType, int newChannelCount)
        {
            Console.WriteLine("Channel Destroyed for: " + platformType.ToString() + " Channel Count: " + newChannelCount);
        }

        static void Events_OnChannelCreated(PushSharp.Common.PlatformType platformType, int newChannelCount)
        {
            Console.WriteLine("Channel Created for: " + platformType.ToString() + " Channel Count: " + newChannelCount);
        }

    }
}
