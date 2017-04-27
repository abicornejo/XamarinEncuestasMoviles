using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosTipoZona
    {
        public static IList<THE_TipoZona> ObtieneTipoZonaPorId(int idZona)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_TipoZona TipoZona WHERE ID_ZONA = " + idZona;
                return NHibernateHelperORACLE.SingleSessionFind<THE_TipoZona>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosTipoAsenta");
                return new List<THE_TipoZona>();
            }
        }
    }
}
