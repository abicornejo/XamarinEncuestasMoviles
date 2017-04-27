using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;


namespace BLL_EncuestasMoviles
{
    public class MngNegocioTipoProgramacion
    {
        public static List<TDI_TipoProgramacion> ObtieneTodoslosTiposProgramacion()
        {
            return (List<TDI_TipoProgramacion>)MngDatosTipoProgramacion.ObtieneTodoslosTiposProgramacion();
        }
    }
}
