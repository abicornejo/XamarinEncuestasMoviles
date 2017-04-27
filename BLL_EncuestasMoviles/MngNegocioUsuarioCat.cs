using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioUsuarioCat
    {
        public static List<TDI_UsuarioCat> ObtieneCatalogosPorUsuario()
        {
            return (List<TDI_UsuarioCat>)MngDatosUsuarioCat.ObtieneCatalogosPorUsuario();
        }

        public static Boolean GuardaOpcionCatalogoPorUsuario(TDI_UsuarioCat usuaCat)
        {
            return MngDatosUsuarioCat.GuardaOpcionCatalogoPorUsuario(usuaCat);
        }

        public static List<TDI_UsuarioCat> ObtieneOpcionesCatalogoPorUsuario(int UsuaLlavPr)
        {
            return (List<TDI_UsuarioCat>)MngDatosUsuarioCat.ObtieneOpcionesCatalogoPorUsuario(UsuaLlavPr);
        }
        
        public static Boolean EliminaOpcionCatalogoPorUsuario(TDI_UsuarioCat usuaCat)
        {
            return MngDatosUsuarioCat.EliminaOpcionCatalogoPorUsuario(usuaCat);
        }

        public static Boolean EliminaCompletaOpcion(TDI_UsuarioCat usuaCat)
        {
            return MngDatosUsuarioCat.EliminaCompletaOpcion(usuaCat);
        }

    }
}
