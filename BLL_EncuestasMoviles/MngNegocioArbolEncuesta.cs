using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioArbolEncuesta
    {
        public static List<THE_ArbolEncuesta> ObtenerArbol(int id_Encuesta)
        {            
            return (List<THE_ArbolEncuesta>)MngDatosArbolEncuesta.ObtenerArbol(id_Encuesta);
        }
    }
}
