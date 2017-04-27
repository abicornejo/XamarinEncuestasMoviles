using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioTipoZona
    {
        public static List<THE_TipoZona> ObtieneTipoZonaPorId(int idZona)
        {
            return (List<THE_TipoZona>)MngDatosTipoZona.ObtieneTipoZonaPorId(idZona);
        }
    }
}
