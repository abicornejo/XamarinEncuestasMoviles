using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosTipoEncuesta
    {

        public static IList<TDI_TipoEncuesta> ObtieneTodoslosTiposEncuestas()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_TipoEncuesta TipoEncuesta WHERE TipoEncuestaEstatus = 'A' ORDER BY TipoEncuestaDescripcion ASC";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_TipoEncuesta>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosTipoEncuesta");
                return new List<TDI_TipoEncuesta>();
            }
        }
    }
}
