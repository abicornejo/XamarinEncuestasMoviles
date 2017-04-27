using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosTipoAsenta
    {
        public static IList<THE_TipoAsenta> ObtieneTipoAsentamientoPorId(int idAsen)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_TipoAsenta TipoAsenta WHERE ID_ASENTAMIENTO = " + idAsen;
                return NHibernateHelperORACLE.SingleSessionFind<THE_TipoAsenta>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosTipoAsenta");
                return new List<THE_TipoAsenta>();
            }
        }
    }
}
