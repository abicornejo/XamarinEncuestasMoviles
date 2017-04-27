using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosCatalogo
    {
        public static IList<THE_Catalogo> ObtieneTodosCatalogos()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Catalogo Catalogo WHERE CATALOGO_STAT = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Catalogo>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosCatalogo");
                return new List<THE_Catalogo>();
            }
        }

        public static Boolean GuardaCatalogo(THE_Catalogo Catalogo)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_Catalogo>(Catalogo);
        }

        public static Boolean EliminaCatalogo(THE_Catalogo Catalogo)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Catalogo>(Catalogo);
        }

        public static IList<THE_Catalogo> ObtieneCatalogoPorId(int IdCatalogo)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Catalogo Catalogo WHERE ID_CATALOGO = " + IdCatalogo + " AND CATALOGO_STAT = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Catalogo>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosCatalogo");
                return new List<THE_Catalogo>();
            }
        }

        public static Boolean ActualizaCatalogo(THE_Catalogo Catalogo)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Catalogo>(Catalogo);
        }
    }
}
