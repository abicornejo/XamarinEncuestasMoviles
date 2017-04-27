using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
  public  class MngNegocioBloqueoIP
    {

        public static List<IntentosUserXIP> ConsultaUltimoAccesos()
        {
            return MngDatosBloqueoIP.ConsultaUltimoAccesos();
        }
        public static List<IntentosUserXIP> ConsultaUltimosAccesos()
        {
            return MngDatosBloqueoIP.ConsultaUltimosAccesos();
        }
        public static bool GuardaIPBloqueada(THE_BloqueoIP oBloqueoIP)
        {
            return MngDatosBloqueoIP.GuardaIPBloqueada(oBloqueoIP);
        }
        public static List<IntentosUserXIP> ConsultaUltimoAccesos(string IP)
        {
            return MngDatosBloqueoIP.ConsultaUltimoAccesos(IP);
        }
    }
}
