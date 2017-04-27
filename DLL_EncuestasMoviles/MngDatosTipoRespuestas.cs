using System;
using System.Collections.Generic;
using System.Linq;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosTipoRespuestas
    {

        public static IList<THE_Tipo_Respuestas> ObtieneTipoRespuestas()
        {
            #region Query Armado
            List<THE_Tipo_Respuestas> listaRespuestas = new List<THE_Tipo_Respuestas>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " select id_tipo_resp id_resp, desc_tipo_resp desc_resp from seml_the_tipo_respuestas resp where resp.STATUS_TIPO_RESP='A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("id_resp", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("desc_resp", NHibernateUtil.String);//1
                

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Tipo_Respuestas oResp = new THE_Tipo_Respuestas();
                    oResp.IdTipoResp = System.Convert.ToInt32(obj[0]);
                    oResp.DescTipoResp = System.Convert.ToString(obj[1]);
                    listaRespuestas.Add(oResp);
                }

            }
            catch (Exception ex)
            {
                listaRespuestas = null;
                return listaRespuestas;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaRespuestas;
            #endregion
        }


    }
}
