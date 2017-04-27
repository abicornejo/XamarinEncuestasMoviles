using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioUsuario
    {
        public static int GuardaAltaUsuario(THE_Usuario usua)
        {
            return MngDatosUsuario.GuardaAltaUsuario(usua);
        }

        public static List<THE_Usuario> ObtieneTodosUsuarios()
        {
            return (List<THE_Usuario>)MngDatosUsuario.ObtieneTodosUsuarios();
        }

        public static List<THE_Usuario> BuscaUsuarios(THE_Usuario usuario)
        {
            return (List<THE_Usuario>)MngDatosUsuario.BuscaUsuarios(usuario);
        }
        public static List<THE_Usuario> BuscaUsuarios2(THE_Usuario usuario, string Catalogos)
        {
            return (List<THE_Usuario>)MngDatosUsuario.BuscaUsuarios2(usuario, Catalogos);
        }
        public static List<THE_Usuario> BuscaUsuariosEsp(THE_Usuario usuario, List<TDI_OpcionCat> listOpCat)
        {
            return (List<THE_Usuario>)MngDatosUsuario.BuscaUsuariosEsp(usuario, listOpCat);
        }

        public static Boolean ActualizaUsuario(THE_Usuario usua)
        {
            return MngDatosUsuario.ActualizaUsuario(usua);
        }

        public static Boolean EliminaUsuario(THE_Usuario usua)
        {
            return MngDatosUsuario.EliminaUsuario(usua);
        }

        public static List<THE_Usuario> ObtieneUsuarioPorLlavPr(int LlavPr)
        {
            return (List<THE_Usuario>)MngDatosUsuario.ObtieneUsuarioPorLlavPr(LlavPr);
        }
    }
}
