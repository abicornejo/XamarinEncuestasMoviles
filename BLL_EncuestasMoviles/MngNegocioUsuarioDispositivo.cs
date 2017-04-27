using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioUsuarioDispositivo
    {
        public static Boolean AsignaDispoUsuario(TDI_UsuarioDispositivo UsuaDispo)
        {
            return MngDatosUsuarioDispositivo.AsignaDispoUsuario(UsuaDispo);
        }

        public static List<TDI_UsuarioDispositivo> ObtieneDispositivoPorUsuario(int IdUsuario)
        {
            return (List<TDI_UsuarioDispositivo>)MngDatosUsuarioDispositivo.ObtieneDispositivoPorUsuario(IdUsuario);
        }

        public static List<TDI_UsuarioDispositivo> ObtieneDispoUsuarioPorIdDispo(int IdDispo)
        {
            return (List<TDI_UsuarioDispositivo>)MngDatosUsuarioDispositivo.ObtieneDispoUsuarioPorIdDispo(IdDispo);
        }
        public static List<TDI_UsuarioDispositivo> ObtieneUsuariosConDispositivoAsignado()
        {
            return (List<TDI_UsuarioDispositivo>)MngDatosUsuarioDispositivo.ObtieneUsuariosConDispositivoAsignado();
        }
        public static Boolean EliminaDispoUsuario(TDI_UsuarioDispositivo UsuaDispo)
        {
            return MngDatosUsuarioDispositivo.EliminaDispoUsuario(UsuaDispo);
        }

        public static Boolean EliminaUserDispo(TDI_UsuarioDispositivo UsuaDispo)
        {
            return MngDatosUsuarioDispositivo.EliminaUserDispo(UsuaDispo);
        }
    }
}
