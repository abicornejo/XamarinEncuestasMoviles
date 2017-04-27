using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
   public class MngNegocioUsuarioSesion
    {
       public static Boolean GuardaSesionUsuario(THE_SesionUsuario usuario)
       {
           return MngDatosUsuarioSesion.GuardaSesionUsuario(usuario);           
       }

       public static List<THE_SesionUsuario> ExisteSesionUsuario(int usuario)
       {
           return (List<THE_SesionUsuario>)MngDatosUsuarioSesion.ExisteSesionUsuario(usuario);
       }
       public static List<THE_SesionUsuario> VerExisteSesionUsuario(int usuario, string ipMaquina)
       {
           return (List<THE_SesionUsuario>)MngDatosUsuarioSesion.VerExisteSesionUsuario(usuario, ipMaquina);
       }

       public static Boolean ActualizaSesionUsuario(THE_SesionUsuario usuario)
       {
           return MngDatosUsuarioSesion.ActualizaSesionUsuario(usuario);
       }

       public static Boolean EliminaSesionUsuario(THE_SesionUsuario usuario)
       {
           return MngDatosUsuarioSesion.EliminaSesionUsuario(usuario);
       }

    }
}
