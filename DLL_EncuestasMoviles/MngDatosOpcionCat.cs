using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosOpcionCat
    {
        public static IList<TDI_OpcionCat> ObtieneOpcionesPorCatalogo(int IdCatalogo)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_OpcionCat OpcionCat WHERE ID_CATALOGO = " + IdCatalogo + " AND OPCIONCAT_STAT = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_OpcionCat>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosOpcionCat");
                return new List<TDI_OpcionCat>();
            }
        }

        public static Boolean EliminaOpcionDelCatalogo(TDI_OpcionCat opciCatalogo)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<TDI_OpcionCat>(opciCatalogo);
        }

        public static IList<TDI_OpcionCat> ObtieneOpcionPorID(int IdOpcion)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_OpcionCat OpcionCat WHERE ID_OPCIONCAT = " + IdOpcion + " AND OPCIONCAT_STAT = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_OpcionCat>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosOpcionCat");
                return new List<TDI_OpcionCat>();
            }
        }

        public static Boolean GuardaOpcionporCatalogo(TDI_OpcionCat opcionCat)
        {
            return NHibernateHelperORACLE.SingleSessionSave<TDI_OpcionCat>(opcionCat);
        }

        public static Boolean ActualizaOpcionporCatalogo(TDI_OpcionCat opcionCat)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<TDI_OpcionCat>(opcionCat);
        }
    }
}
