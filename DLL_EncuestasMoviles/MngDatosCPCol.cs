using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosCPCol
    {
        public static IList<TDI_CPCol> ObtieneCodigoPostalPorColonia(int idColonia)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_CPCol CPCol WHERE ID_COLONIA = " + idColonia;
                return NHibernateHelperORACLE.SingleSessionFind<TDI_CPCol>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosCPCol");
                return new List<TDI_CPCol>();
            }
        }

      
    }
}
