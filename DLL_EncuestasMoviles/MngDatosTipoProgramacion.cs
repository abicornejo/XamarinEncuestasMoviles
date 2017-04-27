using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosTipoProgramacion
    {

        public static IList<TDI_TipoProgramacion> ObtieneTodoslosTiposProgramacion()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_TipoProgramacion TipoProgramacion WHERE TipoProgramacionEstatus = 'A' ORDER BY TipoProgramacionDescripcion ASC";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_TipoProgramacion>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosTipoProgramacion");
                return new List<TDI_TipoProgramacion>();
            }
        }
    }
}
