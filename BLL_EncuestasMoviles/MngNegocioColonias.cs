using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioColonias
    {
        public static List<TDI_Colonias> ObtieneColoniasPorCP(int CP)
        {
            return (List<TDI_Colonias>)MngDatosColonias.ObtieneColoniasPorCP(CP);
        }

        public static List<TDI_Colonias> ObtieneColoniaPorId(int idCol)
        {
            return (List<TDI_Colonias>)MngDatosColonias.ObtieneColoniaPorId(idCol);
        }

        public static List<TDI_Colonias> ObtieneColoniasPorMunicipio(int idMunicipio)
        {
            return (List<TDI_Colonias>)MngDatosColonias.ObtieneColoniasPorMunicipio(idMunicipio);
        }
    }
}
