using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioEstado
    {
        public static List<TDI_Estado> ObtieneEstadoPorId(int idEsta)
        {
            return (List<TDI_Estado>)MngDatosEstado.ObtieneEstadoPorId(idEsta);
        }

        public static List<TDI_Estado> ObtieneTodoslosEstados()
        {
            return (List<TDI_Estado>)MngDatosEstado.ObtieneTodoslosEstados();
        }

        public static List<TDI_Estado> ObtieneEstadoPorCP(int cdPostal)
        {
            return (List<TDI_Estado>)MngDatosEstado.ObtieneEstadoPorCP(cdPostal);
        }
    }
}
