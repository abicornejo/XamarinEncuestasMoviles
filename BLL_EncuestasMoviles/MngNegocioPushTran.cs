using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioPushTran
    {
        public static Boolean GuardarTranPush(THE_PUSTRAN tran)
        {
            return MngDatosPushTran.GuardarTranPush(tran);
        }        
    }
}
