using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosPushTran
    {

        public static Boolean GuardarTranPush(THE_PUSTRAN tran)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_PUSTRAN>(tran);
        }

    }
}
