using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosLogPaginas
    {
        public static Boolean GuardarLogPaginas(TDI_LogPaginas oTDI_LogPaginas)
        {
            return NHibernateHelperORACLE.SingleSessionSave<TDI_LogPaginas>(oTDI_LogPaginas);
        }
    }
}
