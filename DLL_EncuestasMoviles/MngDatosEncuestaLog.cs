using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosEncuestaLog
    {
        public static Boolean GuardaLogEncuesta(TDI_EncuestaLog encuLog)
        {
            return NHibernateHelperORACLE.SingleSessionSave<TDI_EncuestaLog>(encuLog);
        }
    }
}
