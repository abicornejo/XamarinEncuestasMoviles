using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioTipoEncuesta
    {
        public static List<TDI_TipoEncuesta> ObtieneTodoslosTiposEncuestas()
        {
            return (List<TDI_TipoEncuesta>)MngDatosTipoEncuesta.ObtieneTodoslosTiposEncuestas();
        }
    }
}
