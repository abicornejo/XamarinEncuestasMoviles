using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioTransacciones
    {
        public static Boolean GuardaLogTransaccion(THE_LogTran oLogTran)
        {
            return MngDatosTransacciones.GuardaLogTransaccion(oLogTran);
        }
    }
}
