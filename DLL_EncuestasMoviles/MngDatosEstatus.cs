using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosEstatus
    {
        public static IList<TDI_Estatus> ObtieneTodosEstatus()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Estatus Estatus";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Estatus>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosEstatus");
                return new List<TDI_Estatus>();
            }
        }

        public static IList<TDI_Estatus> ObtieneEstatusEncuesta()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Estatus Estatus WHERE ESTATUS_DISPO = 2 AND ID_ESTATUS NOT IN (7, 8)";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Estatus>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosEstatus");
                return new List<TDI_Estatus>();
            }
        }

        public static IList<TDI_Estatus> ObtieneEstatusPorIDEncuesta(int IdEstatus)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Estatus Estatus WHERE ID_ESTATUS = " + IdEstatus + " AND ESTATUS_DISPO = 2";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Estatus>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosEstatus");
                return new List<TDI_Estatus>();
            }
        }
    }
}
