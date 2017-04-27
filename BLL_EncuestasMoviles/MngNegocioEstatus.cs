using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioEstatus
    {
        public static List<TDI_Estatus> ObtieneTodosEstatus()
        {
            return (List<TDI_Estatus>)MngDatosEstatus.ObtieneTodosEstatus();
        }

        public static List<TDI_Estatus> ObtieneEstatusEncuesta()
        {
            return (List<TDI_Estatus>)MngDatosEstatus.ObtieneEstatusEncuesta();
        }

        public static List<TDI_Estatus> ObtieneEstatusPorIDEncuesta(int IdEstatus)
        {
            return (List<TDI_Estatus>)MngDatosEstatus.ObtieneEstatusPorIDEncuesta(IdEstatus);
        }
    }
}
