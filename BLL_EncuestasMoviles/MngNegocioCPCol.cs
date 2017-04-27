using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioCPCol
    {
        public static List<TDI_CPCol> ObtieneCodigoPostalPorColonia(int idColonia)
        {
            return (List<TDI_CPCol>)MngDatosCPCol.ObtieneCodigoPostalPorColonia(idColonia);
        }

      
    }
}
