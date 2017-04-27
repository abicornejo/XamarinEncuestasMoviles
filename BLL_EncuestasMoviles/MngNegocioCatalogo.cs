using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioCatalogo
    {
        public static List<THE_Catalogo> ObtieneTodosCatalogos()
        {
            return (List<THE_Catalogo>)MngDatosCatalogo.ObtieneTodosCatalogos();
        }

        public static Boolean GuardaCatalogo(THE_Catalogo cat)
        {
            return MngDatosCatalogo.GuardaCatalogo(cat);
        }

        public static Boolean EliminaCatalogo(THE_Catalogo Catalogo)
        {
            return MngDatosCatalogo.EliminaCatalogo(Catalogo);
        }

        public static List<THE_Catalogo> ObtieneCatalogoPorId(int IdCatalogo)
        {
            return (List<THE_Catalogo>)MngDatosCatalogo.ObtieneCatalogoPorId(IdCatalogo);
        }

        public static Boolean ActualizaCatalogo(THE_Catalogo Catalogo)
        {
            return MngDatosCatalogo.ActualizaCatalogo(Catalogo);
        }
    }
}
