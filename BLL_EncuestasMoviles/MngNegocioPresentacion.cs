using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioPresentacion
    {
        public static List<TDI_Presentacion> ObtieneTodaslasPresentaciones()
        {
            return (List<TDI_Presentacion>)MngDatosPresentacion.ObtieneTodaslasPresentaciones();
        }
    }
}
