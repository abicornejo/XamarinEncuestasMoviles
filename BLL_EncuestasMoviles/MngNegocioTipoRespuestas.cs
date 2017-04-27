using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioTipoRespuestas
    {

        public static List<THE_Tipo_Respuestas> ObtieneTipoRespuestas()
        {
            return (List<THE_Tipo_Respuestas>)MngDatosTipoRespuestas.ObtieneTipoRespuestas();
        }
    }
}
