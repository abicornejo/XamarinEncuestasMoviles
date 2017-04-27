using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL_EncuestasMoviles
{
    public class Funciones
    {
       
        public static string Alerta(string Mensaje)
        {
            return ("<script>alert('" + Mensaje + "')</script>");
        }

        public static string RegresaMes(int Mes)
        {
            switch (Mes)
            {
                case 1:
                    return "Enero";

                case 2:
                    return "Febrero";

                case 3:
                    return "Marzo";

                case 4:
                    return "Abril";

                case 5:
                    return "Mayo";

                case 6:
                    return "Junio";

                case 7:
                    return "Julio";

                case 8:
                    return "Agosto";

                case 9:
                    return "Septiembre";

                case 10:
                    return "Octubre";

                case 11:
                    return "Noviembre";

                case 12:
                    return "Diciembre";
            }
            return " ";
        }
    }
}
