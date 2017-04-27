using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Entidades_EncuestasMoviles;
using NHibernate;
using BLL_EncuestasMoviles;
using System.Net;
using PushSharp;
using PushSharp.Android;
//using System.Threading.Tasks;

namespace WS_EncuestaMovil
{
    public class Global : System.Web.HttpApplication
    {
       // public static PushService PushClient = new PushService();

        void HandleOnNotificationSent(PushSharp.Common.Notification notification)
        {
           
        }


        protected void Application_Start(object sender, EventArgs e)
        {
        
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //string currentRequest = Request.RawUrl.Trim().ToLower();
            //if (currentRequest.IndexOf("?wsdl") > 0 || currentRequest.IndexOf("?disco") > 0)
            //{
            //    Response.Redirect("~/WSEncuestaMovil.asmx");
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
          
        }

        protected void Application_Error(object sender, EventArgs e)
        {
           
        }

        protected void Session_End(object sender, EventArgs e)
        {
           
        }

        protected void Application_End(object sender, EventArgs e)
        {
            
        }
    }
}
