using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioRespuestas
    {


        public static List<TDI_BaseRespuestas> ObtenerBaseRespuestas()
        {
            return (List<TDI_BaseRespuestas>)MngDatosRespuestas.ObtenerBaseRespuestas();
        }

        public static List<THE_Respuestas> ObtenerRespuestasPorPregunta(int IdPregunta)
        {
            return (List<THE_Respuestas>)MngDatosRespuestas.ObtenerRespuestasPorPregunta(IdPregunta);
        }

        public static Boolean GuardaRespuesta(THE_Respuestas Respu)
        {
            return MngDatosRespuestas.GuardaRespuesta(Respu);
        }

        public static Boolean EliminaRespuesta(THE_Respuestas IdRespuesta)
        {
            return MngDatosRespuestas.EliminaRespuesta(IdRespuesta);
        }

        public static List<THE_Respuestas> ObtieneRespuestaPorId(int IdRespuesta)
        {
            return (List<THE_Respuestas>)MngDatosRespuestas.ObtieneRespuestaPorId(IdRespuesta);
        }

        public static Boolean ActualizaRespuesta(THE_Respuestas respu)
        {
            return MngDatosRespuestas.ActualizaRespuesta(respu);
        }
    }
}
