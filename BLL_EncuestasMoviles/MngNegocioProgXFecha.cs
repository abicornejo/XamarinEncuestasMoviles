using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;


namespace BLL_EncuestasMoviles
{
    public class MngNegocioProgXFecha
    {
        public static Boolean GuardaProgXFecha(THE_ProgXFecha progXFecha)
        {
            return MngDatosProgXFecha.GuardaProgXFecha(progXFecha);
        }

        public static List<THE_ProgXFecha> ObtieneProgXFechaPorIdProg(int idProgramacion)
        {
            return (List<THE_ProgXFecha>)MngDatosProgXFecha.ObtieneProgXFechaPorIdProg(idProgramacion);
        }

        public static THE_ProgXFecha ObtieneProgXFechaPorIdProgXFecha(int idProgXFecha)
        {
            return MngDatosProgXFecha.ObtieneProgXFechaPorIdProgXFecha(idProgXFecha);
        }

        public static Boolean EliminaProgXFecha(THE_ProgXFecha progXFecha)
        {
            return MngDatosProgXFecha.EliminaProgXFecha(progXFecha);
        }
    }
}
