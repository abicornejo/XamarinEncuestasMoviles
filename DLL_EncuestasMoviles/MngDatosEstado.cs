using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosEstado
    {
        public static IList<TDI_Estado> ObtieneEstadoPorId(int idEsta)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Estado Estado WHERE ID_ESTADO = " + idEsta + "AND ESTADO_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Estado>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosEstado");
                return new List<TDI_Estado>();
            }
        }

        public static IList<TDI_Estado> ObtieneTodoslosEstados()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_Estado Estado WHERE ESTADO_ESTATUS = 'A' ORDER BY ESTADO_NOMBRE ASC";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_Estado>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosEstado");
                return new List<TDI_Estado>();
            }
        }

        public static IList<TDI_Estado> ObtieneEstadoPorCP(int cdPostal)
        {
            #region Query Armado
            List<TDI_Estado> lstEstado = new List<TDI_Estado>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT est.ESTADO_NOMBRE Nombre, est.ID_ESTADO iD ";
            strSQL += " FROM seml_tdi_colonias col, ";
            strSQL += " seml_tdi_cpcol cpcol, ";
            strSQL += " seml_the_tipoasenta asen, ";
            strSQL += " seml_the_tipozona zon, ";
            strSQL += " seml_tdi_municipios mun, ";
            strSQL += " seml_tdi_estado est ";
            strSQL += " WHERE cpcol.id_codigopostal = " + cdPostal;
            strSQL += " AND cpcol.id_colonia = col.id_colonia ";
            strSQL += " AND col.id_asentamiento = asen.id_asentamiento ";
            strSQL += " AND zon.id_zona = col.id_zona ";
            strSQL += " AND mun.ID_MUNICIPIO = col.ID_MUNICIPIO ";
            strSQL += " and mun.ID_ESTADO = est.ID_ESTADO ";
            strSQL += " and mun.MUNICIPIO_ESTATUS = 'A' ";
            strSQL += " and est.ESTADO_ESTATUS = 'A' ";
            strSQL += " ORDER BY ESTADO_NOMBRE ASC ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("Nombre", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("iD", NHibernateUtil.Int32);//1

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_Estado oEstado = new TDI_Estado();
                    oEstado.EstadoNombre = System.Convert.ToString(obj[0]);
                    oEstado.IdEstado = System.Convert.ToInt32(obj[1]);

                    lstEstado.Add(oEstado);
                }
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosEstado");
                lstEstado = null;
                return lstEstado;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstEstado;
            #endregion
        }
    }
}
