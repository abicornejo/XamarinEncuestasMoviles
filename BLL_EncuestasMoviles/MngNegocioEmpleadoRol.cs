using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;
using System.Xml;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioEmpleadoRol
    {
        public static XmlDocument GetUserDataByNumEmpleado(string numEmpl, string emplUsua)
        {
            return MngDatosEmpleadoRol.GetUserDataByNumEmpleado(numEmpl, emplUsua);
        }

        public static List<THE_Empleado> GetEmailEmpleados()
        {
            return (List<THE_Empleado>)MngDatosEmpleadoRol.GetEmailEmpleados();
        }
    }
}
