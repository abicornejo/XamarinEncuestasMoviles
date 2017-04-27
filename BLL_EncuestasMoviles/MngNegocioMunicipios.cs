using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioMunicipios
    {
        public static List<TDI_Municipios> ObtieneMunicipiosPorID(int idMuni)
        {
            return (List<TDI_Municipios>)MngDatosMunicipios.ObtieneMunicipiosPorID(idMuni);
        }

        public static List<TDI_Municipios> ObtieneMunicipiosPorEstado(int idEstado)
        {
            return (List<TDI_Municipios>)MngDatosMunicipios.ObtieneMunicipiosPorEstado(idEstado);
        }
    }
}
