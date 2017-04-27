using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;


namespace BLL_EncuestasMoviles
{
    public class MngNegocioProgXSemana
    {
        public static Boolean GuardaProgXSemana(THE_ProgXSemana progXSemana)
        {
            return MngDatosProgXSemana.GuardaProgXSemana(progXSemana);
        }

        public static List<THE_ProgXSemana> ObtieneProgXSemanaPorIdProg(int idProgramacion)
        {
            return (List<THE_ProgXSemana>)MngDatosProgXSemana.ObtieneProgXSemanaPorIdProg(idProgramacion);
        }

        public static THE_ProgXSemana ObtieneProgXSemanaPorIdProgXSemana(int idProgXSemana)
        {
            return MngDatosProgXSemana.ObtieneProgXSemanaPorIdProgXSemana(idProgXSemana);
        }

        public static Boolean EliminaProgXSemana(THE_ProgXSemana progXSemana)
        {
            return MngDatosProgXSemana.EliminaProgXSemana(progXSemana);
        }
    }
}
