using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosTransacciones
    {
        public static Boolean GuardaLogTransaccion(THE_LogTran oLogTran)
        {             
              bool Res=  NHibernateHelperORACLE.SingleSessionSave<THE_LogTran>(oLogTran);
              return Res;
        }
    }
}
