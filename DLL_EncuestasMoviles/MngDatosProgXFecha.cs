using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosProgXFecha
    {
        public static Boolean GuardaProgXFecha(THE_ProgXFecha progXFecha)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_ProgXFecha>(progXFecha);
        }

        public static IList<THE_ProgXFecha> ObtieneProgXFechaPorIdProg(int idProgramacion)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_ProgXFecha ProgXFecha WHERE ID_PROGRAMACION = " + idProgramacion + " AND ESTATUS = 'A' ORDER BY ID_PROGXFECHA ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_ProgXFecha>(strQuery);
            }
            catch (Exception ex)
            {
                return new List<THE_ProgXFecha>();
            }
        }

        public static THE_ProgXFecha ObtieneProgXFechaPorIdProgXFecha(int idProgXFecha)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_ProgXFecha ProgXFecha WHERE ID_PROGXFECHA = " + idProgXFecha + " AND ESTATUS = 'A' ORDER BY ID_PROGXFECHA ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_ProgXFecha>(strQuery)[0];
            }
            catch (Exception ex)
            {
                return new THE_ProgXFecha();
            }
        }

        public static Boolean EliminaProgXFecha(THE_ProgXFecha progXFecha)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_ProgXFecha>(progXFecha);
        }
    }
}
