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

namespace ENCUESTA_MOVIL.Servicio
{
    class Listening : BroadcastReceiver
    {
        
        /// <summary>
        /// Metodo OnReceive se ejecuta cada vez que el dispositivo se enciende
        /// </summary>
        /// <param name="context">Este parametro es el id que le asigna Android a la aplicacion</param>
        /// <param name="intent">Este es el parametro con el id del intent que lo manda a invocar</param>
        public override void OnReceive(Context context, Intent intent)
        {
            var intentService = new Intent(context, typeof(ClsDemonio));            
            context.StartService(intentService);
        }
    }
}