using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioLogErrores
    {
        public static Boolean GuardarLogErrores(THE_LogError LogErrores)
        {
            return MngDatosLogErrores.GuardarLogErrores(LogErrores);
        }
    }
}
