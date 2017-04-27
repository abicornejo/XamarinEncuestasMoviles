using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioEncuesta
    {
        public static List<THE_Usuario> ReporteTiempoRespuesta(string idsEncuestas, string idsPersonas)
        {
            return (List<THE_Usuario>)MngDatosEncuesta.ReporteTiempoRespuesta(idsEncuestas, idsPersonas);        
        }

        public static List<THE_Usuario> ReporteRespuestaByEncuesta(string idEncuesta)
        {
            return (List<THE_Usuario>)MngDatosEncuesta.ReporteRespuestaByEncuesta(idEncuesta);
        }

        public static List<THE_Encuesta> ObtieneEncuestaPorID(int IdEncuesta)
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.ObtieneEncuestaPorID(IdEncuesta);
        }

        public static Boolean GuardarEncuestas(THE_Encuesta encu)
        {
            return MngDatosEncuesta.GuardarEncuestas(encu);
        }

        public static Boolean GuardaIdEncEncriptada(TDI_EncEncrypt enc_encrypt) {

            return MngDatosEncuesta.GuardaIdEncEncriptada(enc_encrypt);
        }

        public static List<THE_Encuesta> ObtieneEncuestaPorEmpleado(int Empl_Llav_Pr)
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.ObtieneEncuestaPorEmpleado(Empl_Llav_Pr);
        }

        public static Boolean EliminaEncuestas(THE_Encuesta encu)
        {
            return MngDatosEncuesta.EliminaEncuestas(encu);
        }

        public static Boolean ActualizaEncuesta(THE_Encuesta encu)
        {
            return MngDatosEncuesta.ActualizaEncuesta(encu);
        }

        public static int NotificacionEncuestaPendiente(int IdDispositivo, double NumeroTel)
        {
            return MngDatosEncuesta.NotificacionEncuestaPendiente(IdDispositivo, NumeroTel);
        }

        public static List<THE_Encuesta> ObtieneTodasEncuestasActivas()
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.ObtieneTodasEncuestasActivas();
        }

        public static List<THE_Encuesta> ObtieneEncuestasActivas()
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.ObtieneEncuestasActivas();
        }

        public static List<THE_Encuesta> BuscaEncuestaPorNombre(string NombreEncuesta, string FechIni, string FechFin, string TipoFecha)
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.BuscaEncuestaPorNombre(NombreEncuesta, FechIni, FechFin, TipoFecha);
        }

        public static List<THE_Encuesta> ObtieneDatosEncuestaPreview(int IdEncuesta)
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.ObtieneDatosEncuestaPreview(IdEncuesta);
        }

        public static List<THE_Encuesta> BuscaEncuestaPreguntasRespuestas(string NombreEncuesta, string FechIni, string FechFin, string TipoFecha)
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.BuscaEncuestaPreguntasRespuestas(NombreEncuesta, FechIni, FechFin, TipoFecha);
        }

        public static List<THE_Encuesta> ObtieneTodasEncuestasMostrar()
        {
            return (List<THE_Encuesta>)MngDatosEncuesta.ObtieneTodasEncuestasMostrar();
        }

        public static List<TDI_EncuestaDispositivo> ObtieneEncuestaDispositivoPorIdEncuesta(int IdEncuesta)
        {
            return (List<TDI_EncuestaDispositivo>)MngDatosEncuesta.ObtieneEncuestaDispositivoPorIdEncuesta(IdEncuesta);
        }
    }
}
