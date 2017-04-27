using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioTipoAsenta
    {
        public static List<THE_TipoAsenta> ObtieneTipoAsentamientoPorId(int idAsen)
        {
            return (List<THE_TipoAsenta>)MngDatosTipoAsenta.ObtieneTipoAsentamientoPorId(idAsen);
        }
    }
}
