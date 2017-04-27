using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosUsuarioCat
    {
        public static IList<TDI_UsuarioCat> ObtieneCatalogosPorUsuario()
        {
            return NHibernateHelperORACLE.SingleSessionFind<TDI_UsuarioCat>("");
        }

        public static Boolean GuardaOpcionCatalogoPorUsuario(TDI_UsuarioCat usuaCat)
        {
            return NHibernateHelperORACLE.SingleSessionSave<TDI_UsuarioCat>(usuaCat);
        }

        public static IList<TDI_UsuarioCat> ObtieneOpcionesCatalogoPorUsuario(int UsuaLlavPr)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_UsuarioCat UsuarioCat WHERE USUA_LLAV_PR = " + UsuaLlavPr + " AND USUACAT_STAT = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_UsuarioCat>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosDispositivo");
                return new List<TDI_UsuarioCat>();
            }
        }      

        public static Boolean EliminaOpcionCatalogoPorUsuario(TDI_UsuarioCat usuaCat)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<TDI_UsuarioCat>(usuaCat);
        }

        public static Boolean EliminaCompletaOpcion(TDI_UsuarioCat usuaCat)
        {
            return NHibernateHelperORACLE.SingleSessionDelete<TDI_UsuarioCat>(usuaCat);
        }
    }
}
