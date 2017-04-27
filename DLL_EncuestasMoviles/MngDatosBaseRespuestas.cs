using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosBaseRespuestas
    {
        public static List<TDI_BaseRespuestas> ObtenerRespFrecuentes()
        {
            #region Query Armado
            string QueryEmpl = string.Empty;

            List<TDI_BaseRespuestas> lstRespuestas = new List<TDI_BaseRespuestas>();
            
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            QueryEmpl += " select distinct id_baserespuestas brid, baserespuestas_desc brdesc ";
            QueryEmpl += " from seml_tdi_baserespuestas ";
            QueryEmpl += " where baserespuestas_stat='A' order by id_baserespuestas ";
           

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(QueryEmpl);

                consultaIQRY.AddScalar("brid", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("brdesc", NHibernateUtil.String);//1

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_BaseRespuestas oResp = new TDI_BaseRespuestas();
                    oResp.IdRespuesta = System.Convert.ToInt32(obj[0]);                  
                    oResp.RespuestasDesc = System.Convert.ToString(obj[1]);
                    lstRespuestas.Add(oResp);
                }

            }
            catch (Exception ex)
            {

                lstRespuestas = null;
                return lstRespuestas;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }



            return lstRespuestas;
            #endregion
        }
    }
}
