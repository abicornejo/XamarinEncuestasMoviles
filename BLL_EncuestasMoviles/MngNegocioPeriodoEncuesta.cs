using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
   public class MngNegocioPeriodoEncuesta
    {

       public static List<THE_PeriodoEncuesta> ObtienePeriodosPorEncuesta(int IdEncuesta)
       {

        return (List<THE_PeriodoEncuesta>)MngDatosPeriodoEncuesta.ObtienePeriodosPorEncuesta(IdEncuesta);
       }
    }
}
