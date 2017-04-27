using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosMunicipios
    {
        public static IList<TDI_Municipios> ObtieneMunicipiosPorID(int idMuni)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Municipios Municipios WHERE ID_MUNICIPIO = " + idMuni + "AND MUNICIPIO_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Municipios>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosMunicipios");
                return new List<TDI_Municipios>();
            }
        }

        public static IList<TDI_Municipios> ObtieneMunicipiosPorEstado(int idEstado)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Municipios Municipios WHERE ID_ESTADO = " + idEstado + "AND MUNICIPIO_ESTATUS = 'A' ORDER BY MUNI_NOMBRE ASC";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Municipios>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosMunicipios");
                return new List<TDI_Municipios>();
            }
        }
    }
}
