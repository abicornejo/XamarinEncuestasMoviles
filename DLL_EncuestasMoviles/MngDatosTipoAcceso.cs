using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosTipoAcceso
    {
        public static IList<TDI_TipoAcceso> ObtenerTipoAcceso(String condicion)
        {
            try
            {
                String sbQuery = "from TDI_TipoAcceso TipoAcceso " + condicion;
                return NHibernateHelperORACLE.SingleSessionFind<TDI_TipoAcceso>(sbQuery);
            }
            catch (Exception ex)
            {
                return new List<TDI_TipoAcceso>();
            }
        }
    }
}
