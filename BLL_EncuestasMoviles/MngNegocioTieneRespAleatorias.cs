using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;


namespace BLL_EncuestasMoviles
{
    public class MngNegocioTieneRespAleatorias
    {
        public static List<TDI_TieneRespAleatorias> ObtieneRespAleatorias()
        {
            return (List<TDI_TieneRespAleatorias>)MngDatosTieneRespAleatorias.ObtieneRespAleatorias();
        }
    }
}
