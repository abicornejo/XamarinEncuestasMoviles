using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL_EncuestasMoviles
{
    public static class Llave
    {
        public static string validaEmpleado(string Usuario, string Contraseña)
        {
            LLaveMaestra.Service informacion = new LLaveMaestra.Service();

            return informacion.gsc_llave(Usuario, Contraseña);
        }
    }
}
