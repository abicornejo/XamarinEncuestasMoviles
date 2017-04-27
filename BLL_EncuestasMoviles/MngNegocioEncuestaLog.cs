using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioEncuestaLog
    {
        public static Boolean GuardaLogEncuesta(TDI_EncuestaLog encuLog)
        {
            return MngDatosEncuestaLog.GuardaLogEncuesta(encuLog);
        }
    }
}
