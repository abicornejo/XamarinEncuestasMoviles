using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
   public class MngDatosPeriodoEncuesta
    {
        public static IList<THE_PeriodoEncuesta> ObtienePeriodosPorEncuesta(int IdEncuesta)
        {
            #region Query Armado
            List<THE_PeriodoEncuesta> lstPeriodosEncuesta = new List<THE_PeriodoEncuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT PE.ID_PERIODO idp, PE.ID_ENCUESTA idenc, PE.PERIODO peri  from SEML_THE_PERIODOS_ENCUESTA PE ,SEML_THE_ENCUESTA ENC ";
            strSQL += " where PE.ID_ENCUESTA=ENC.ID_ENCUESTA AND PE.ID_ENCUESTA=" + IdEncuesta.ToString() + " ORDER BY idenc, peri "; 

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);
                consultaIQRY.AddScalar("idp", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("idenc", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("peri", NHibernateUtil.String);//2           

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_PeriodoEncuesta oPerEnc = new THE_PeriodoEncuesta();
                    oPerEnc.IdPeriodo = System.Convert.ToInt32(obj[0]);
                    oPerEnc.IdEncuesta = new THE_Encuesta() { IdEncuesta = System.Convert.ToInt32(obj[1]) };
                    oPerEnc.Periodo = System.Convert.ToInt32(obj[2]);
                    lstPeriodosEncuesta.Add(oPerEnc);
                }

            }
            catch (Exception ex)
            {

                lstPeriodosEncuesta = null;
                return lstPeriodosEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstPeriodosEncuesta;
            #endregion
        }
    }
}
