using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosLogAcceso
    {
        public static Boolean GuardarLogAcceso(TDI_LogAcceso oTDI_LogAcceso)
        {
            return NHibernateHelperORACLE.SingleSessionSave<TDI_LogAcceso>(oTDI_LogAcceso);
        }
    }
}
