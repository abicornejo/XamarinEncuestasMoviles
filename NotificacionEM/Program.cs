using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PushSharp;
using PushSharp.Android;
using System.Threading;
using System.Net;

namespace NotificacionEM
{
    class Program
    {
        static void Main(string[] args)
        {            
            Notifica();
            Thread.Sleep(30000);
        }

        public static void Notifica() 
        {            
            try
            {

                List<Entidades_EncuestasMoviles.TDI_Notificaciones> notifica = BLL_EncuestasMoviles.MngNegocioNotificaciones.ObtieneNotificaciones();
                
                if (notifica.Count > 0)
                {
                    foreach (var element in notifica)
                    {
                        string json = "{'IdNotificacion':'" + element.IdNotificacion.ToString() + "','TokenDispositivo':'" + element.TokenDispositivo + "','Periodo':'" + element.Periodo.ToString() + "','Mensaje':'" + element.Mensaje.ToString() + "','IdEncuesta':'" + element.IdEncuesta.ToString() + "','Telefono':'" + element.Telefono.ToString() + "','idDispo':'" + element.IdDispo.ToString() + "','IdEnvio':'"+element.IdEnvio.ToString()+ "'}";
           
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
                        //idNotificacion, TokenDispositivo, Periodo, Mensaje, idEncuesta
                        //string deviceregistrationid = DeviceRegistrationID as string;
                        PushClient.QueueNotification(NotificationFactory.AndroidGcm()
                                                       .ForDeviceRegistrationId(element.TokenDispositivo)
                                                       .WithCollapseKey("NONE")
                                                       .WithJson(json)
                                                       .WithTag(element.IdNotificacion + "|" + element.TokenDispositivo + "|" + element.Periodo + "|" + element.Mensaje + "|" + element.IdEncuesta+"|"+ element.Telefono+"|"+ element.IdDispo));

                        Console.WriteLine("Waiting for Queue to Finish...");
                    }
                }
                else                
                    Console.WriteLine("NO SE REGISTRO NINGUN RESULTADO " + DateTime.Now);                
            }
            catch (Exception ms)
            {
                Console.WriteLine("ERROR " + ms.Message);
            }
            finally
            {
                
            }
        }
        static void Events_OnDeviceSubscriptionIdChanged(PushSharp.Common.PlatformType platform, string oldDeviceInfo, string newDeviceInfo, PushSharp.Common.Notification notification)
        {
           
            try
            {
                Console.WriteLine("Device Registration Changed:  Old-> " + oldDeviceInfo + "  New-> " + newDeviceInfo);
            }
            catch(Exception ms)
            {
                 Console.WriteLine("Error " + ms.Message);            
            }
        }
        
        static void Events_OnNotificationSent(PushSharp.Common.Notification notification)
        {
            try
            {
                bool EliminoNotificacion = BLL_EncuestasMoviles.MngNegocioNotificaciones.EliminaNotificacion(Convert.ToInt32(notification.Tag.ToString().Split('|')[0]), notification.Tag.ToString().Split('|')[1], Convert.ToInt32(notification.Tag.ToString().Split('|')[2]), notification.Tag.ToString().Split('|')[3], Convert.ToInt32(notification.Tag.ToString().Split('|')[4]));

                if (EliminoNotificacion)
                    Console.WriteLine("Notificacion " + notification.ToString().Split('|')[0] + " Eliminada correctamente "+DateTime.Now);
                else
                    Console.WriteLine("ERROR al actualizar status a 3 " + DateTime.Now);
                Console.WriteLine("Enviado a " + notification.Tag.ToString());
                Console.WriteLine("Sent: " + notification.Platform.ToString() + " -> " + notification.ToString());
            }
            catch
            {
                Console.WriteLine("ERROR al actualizar status a 3 " + DateTime.Now);
            }
            finally
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
            Console.WriteLine("Device Subscription Expired para el dispo: " +notification.Tag.ToString().Split('|')[6]+"|"+ platform.ToString() + " -> " + deviceInfo);
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
