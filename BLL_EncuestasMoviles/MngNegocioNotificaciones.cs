using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioNotificaciones
    {
        public static List<TDI_Notificaciones> ObtieneNotificaciones()
        {
            return (List<TDI_Notificaciones>)MngDatosNotificaciones.ObtieneNotificaciones();
        }

        public static bool EliminaNotificacion(int IdNotificacion, string tokenDispo, int periodo, string mensaje, int idencuesta)
        {

            TDI_Notificaciones notificacion = new TDI_Notificaciones();
            notificacion.IdNotificacion = IdNotificacion;
            notificacion.Status = 3;
            notificacion.Periodo = periodo;
            notificacion.TokenDispositivo = tokenDispo;
            notificacion.Mensaje = mensaje;
            notificacion.IdEncuesta = idencuesta;
                       
            return NHibernateHelperORACLE.SingleSessionUpdate<TDI_Notificaciones>(notificacion);
        }
    }
}
