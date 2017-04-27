using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosColonias
    {
        public static IList<TDI_Colonias> ObtieneColoniasPorCP(int CP)
        {
            #region Query Armado
            List<TDI_Colonias> lstColonias = new List<TDI_Colonias>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT cpcol.id_colonia Idcolonia, colo.colonia_nombre namecolonia, ";
            strSQL += " colo.id_municipio idmuni, colo.id_asentamiento idasen, ";
            strSQL += " colo.id_zona idzona ";
            strSQL += " FROM seml_tdi_cpcol cpcol, seml_tdi_colonias colo ";
            strSQL += " WHERE cpcol.id_codigopostal = " + CP;
            strSQL += " AND colo.id_colonia = cpcol.id_colonia ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("Idcolonia", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("namecolonia", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("idmuni", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("idasen", NHibernateUtil.Int32);//3
                consultaIQRY.AddScalar("idzona", NHibernateUtil.Int32);//4

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_Colonias oCPCol = new TDI_Colonias();
                    oCPCol.IdColonia = System.Convert.ToInt32(obj[0]);
                    oCPCol.ColoniaNombre = System.Convert.ToString(obj[1]);
                    oCPCol.IdMunicipio = new TDI_Municipios() { IdMunicipio = System.Convert.ToInt32(obj[2]) };
                    oCPCol.IdAsentamiento = new THE_TipoAsenta() { IdAsentamiento = System.Convert.ToInt32(obj[3]) };
                    oCPCol.IdZona = new THE_TipoZona() { IdZona = System.Convert.ToInt32(obj[4]) };

                    lstColonias.Add(oCPCol);
                }
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosColonias");
                lstColonias = null;
                return lstColonias;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstColonias;
            #endregion
        }

        public static IList<TDI_Colonias> ObtieneColoniaPorId(int IdCol)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Colonias Colonias WHERE ID_COLONIA = " + IdCol;
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Colonias>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosColonias");
                return new List<TDI_Colonias>();
            }
        }

        public static IList<TDI_Colonias> ObtieneColoniasPorMunicipio(int idMunicipio)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Colonias Colonias WHERE ID_MUNICIPIO = " + idMunicipio + " ORDER BY COLONIA_NOMBRE ASC";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Colonias>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosColonias");
                return new List<TDI_Colonias>();
            }
        }
    }
}
