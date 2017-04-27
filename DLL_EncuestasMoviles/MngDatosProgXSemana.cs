using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosProgXSemana
    {
        public static Boolean GuardaProgXSemana(THE_ProgXSemana progXSemana)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_ProgXSemana>(progXSemana);
        }

        public static IList<THE_ProgXSemana> ObtieneProgXSemanaPorIdProg(int idProgramacion)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_ProgXSemana ProgXSemana WHERE ID_PROGRAMACION = " + idProgramacion + " AND ESTATUS = 'A' ORDER BY ID_PROGXSEMANA ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_ProgXSemana>(strQuery);
            }
            catch (Exception ex)
            {
                return new List<THE_ProgXSemana>();
            }
        }

        public static THE_ProgXSemana ObtieneProgXSemanaPorIdProgXSemana(int idProgXSemana)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_ProgXSemana ProgXSemana WHERE ID_PROGXSEMANA = " + idProgXSemana + " AND ESTATUS = 'A' ORDER BY ID_PROGXSEMANA ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_ProgXSemana>(strQuery)[0];
            }
            catch (Exception ex)
            {
                return new THE_ProgXSemana();
            }
        }

        public static Boolean EliminaProgXSemana(THE_ProgXSemana progXSemana)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_ProgXSemana>(progXSemana);
        }
    }
}
