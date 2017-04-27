using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioTipoAcceso
    {
        public static IList<TDI_TipoAcceso> ObtenerTipoAcceso()
        {
            return MngDatosTipoAcceso.ObtenerTipoAcceso("");
        }
    }
}
