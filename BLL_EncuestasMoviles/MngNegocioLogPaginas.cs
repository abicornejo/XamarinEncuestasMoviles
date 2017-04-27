using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioLogPaginas
    {
        public static Boolean GuardarLogPaginas(TDI_LogPaginas LogPaginas)
        {
            return MngDatosLogPaginas.GuardarLogPaginas(LogPaginas);
        }
    }
}
