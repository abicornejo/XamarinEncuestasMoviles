using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;

namespace EncuestasMoviles.Clases
{
    public class Error
    {
        public static void ManejadorErrores(Exception Ex, string EmplUsua, string Dominio, string NombMaquina, string IpCliente, string Pantalla, int EmplLlavPr)
        {
            THE_LogError saveError = new THE_LogError();
            saveError.EmplUsua = EmplUsua;
            saveError.DirIP = IpCliente;
            saveError.Dominio = Dominio;
            saveError.Error = Ex.Message + "\n" + Ex.StackTrace + "\n" + Ex.Data;
            saveError.FechaCreacion = DateTime.Now;
            saveError.MachineName = NombMaquina;
            saveError.Pantalla = Pantalla;
            saveError.EmplLlavPr = new THE_Empleado() { EmpleadoLlavePrimaria = EmplLlavPr };
            MngNegocioLogErrores.GuardarLogErrores(saveError);
        }
    }
}