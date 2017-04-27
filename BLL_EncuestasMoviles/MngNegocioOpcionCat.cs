using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioOpcionCat
    {
        public static List<TDI_OpcionCat> ObtieneOpcionesPorCatalogo(int IdCatalogo)
        {
            return (List<TDI_OpcionCat>)MngDatosOpcionCat.ObtieneOpcionesPorCatalogo(IdCatalogo);
        }

        public static Boolean EliminaOpcionDelCatalogo(TDI_OpcionCat opciCatalogo)
        {
            return MngDatosOpcionCat.EliminaOpcionDelCatalogo(opciCatalogo);
        }

        public static List<TDI_OpcionCat> ObtieneOpcionPorID(int IdOpcion)
        {
            return (List<TDI_OpcionCat>)MngDatosOpcionCat.ObtieneOpcionPorID(IdOpcion);
        }

        public static Boolean GuardaOpcionporCatalogo(TDI_OpcionCat opcionCat)
        {
            return MngDatosOpcionCat.GuardaOpcionporCatalogo(opcionCat);
        }

        public static Boolean ActualizaOpcionporCatalogo(TDI_OpcionCat opcionCat)
        {
            return MngDatosOpcionCat.ActualizaOpcionporCatalogo(opcionCat);
        }
    }
}
