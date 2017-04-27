using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioPreguntas
    {
        public static List<THE_Preguntas> ObtienePreguntasPorEncuesta(int IdEncuesta)
        {
            return (List<THE_Preguntas>)MngDatosPreguntas.ObtienePreguntasPorEncuesta(IdEncuesta);
        }
      
        public static Boolean GuardaPreguntaPorEncuesta(THE_Preguntas pregu)
        {
            return MngDatosPreguntas.GuardaPreguntaPorEncuesta(pregu);
        }

        public static Boolean ActualizaPregunta(THE_Preguntas pregu)
        {
            return MngDatosPreguntas.ActualizaPregunta(pregu);
        }

        public static List<THE_Preguntas> ObtienePreguntaPorID(int IdPregunta)
        {
            return (List<THE_Preguntas>)MngDatosPreguntas.ObtienePreguntaPorID(IdPregunta);
        }

        public static Boolean EliminaPregunta(THE_Preguntas Pregunta)
        {
            return MngDatosPreguntas.EliminaPregunta(Pregunta);
        }

        public static int ObtienePreguntaFinEncuesta(int IdEncuesta)
        {
            return MngDatosPreguntas.ObtienePreguntaFinEncuesta(IdEncuesta);
        }
    }
}
