using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
  public class MngNegocioBaseRespuestas
    {

      public static List<TDI_BaseRespuestas> ObtenerRespFrecuentes()
      {
          return (List<TDI_BaseRespuestas>)MngDatosBaseRespuestas.ObtenerRespFrecuentes();
      }
    }
}
