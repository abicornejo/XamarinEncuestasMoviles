using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioBloqueoUsuario
    {
        public static bool GuardaUsuarioBloqueado(THE_BloqueoUsuario oBloqueoUsuario)
        {
            return MngDatosBloqueoUsuario.GuardaUsuarioBloqueado(oBloqueoUsuario);
        }

        public static IList<THE_BloqueoUsuario> ConsultaUsuarioBloqueadoXIdUsuario(string numEmpleado, string tipoBloqueo)
        {
            return MngDatosBloqueoUsuario.ConsultaUsuarioBloqueadoXIdUsuario(numEmpleado, tipoBloqueo);
        }

        public static List<IntentosUsuario> ConsultaUltimoAccesosUsuario(string usuario)
        {
            return MngDatosBloqueoUsuario.ConsultaUltimoAccesosUsuario(usuario);
        }
    }
}
