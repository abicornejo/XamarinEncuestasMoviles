using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;


namespace DLL_EncuestasMoviles
{
    public class MngDatosPresentacion
    {

        public static IList<TDI_Presentacion> ObtieneTodaslasPresentaciones()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Presentacion Presentacion WHERE PRESENTACION_ESTATUS = 'A' ORDER BY PRESENTACION_DESCRIPCION ASC";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Presentacion>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosPresentacion");
                return new List<TDI_Presentacion>();
            }
        }       
    }
}
