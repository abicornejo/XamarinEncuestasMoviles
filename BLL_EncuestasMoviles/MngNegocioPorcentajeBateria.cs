using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
   public class MngNegocioPorcentajeBateria
    {

       public static Boolean GuardaPorcentajeBateria(double numeroTel, int porcentajeBateria, string fechaLog)
       {
           return MngDatosPorcentajeBateria.GuardaPorcentajeBateria(numeroTel, porcentajeBateria, fechaLog);
       }
    }
}
