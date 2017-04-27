using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioLogAcceso
    {
        public static Boolean GuardarLogAcceso(TDI_LogAcceso LogAcceso)
        {
            return MngDatosLogAcceso.GuardarLogAcceso(LogAcceso);
        }
    }
}
