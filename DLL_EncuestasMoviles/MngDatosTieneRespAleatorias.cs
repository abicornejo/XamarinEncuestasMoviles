using System;
using System.Collections.Generic;
using System.Linq;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
   public class MngDatosTieneRespAleatorias
    {

       public static IList<TDI_TieneRespAleatorias> ObtieneRespAleatorias()
       {
           #region Query Armado
           List<TDI_TieneRespAleatorias> listaRespuestas = new List<TDI_TieneRespAleatorias>();
           string strSQL = string.Empty;
           Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
           ISession session = NHibernateHelperORACLE.GetSession();

           strSQL += " select ID_PRE_ALEATORIA id_pre_alea , DESC_PRE_ALEATORIA desc_pre_alea from seml_tdi_es_pre_alea resp where resp.STATUS_PRE_ALEATORIA='A' ";

           try
           {
               ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

               consultaIQRY.AddScalar("id_pre_alea", NHibernateUtil.Int32);//0
               consultaIQRY.AddScalar("desc_pre_alea", NHibernateUtil.String);//1


               IList lista = consultaIQRY.List();

               foreach (Object[] obj in lista)
               {
                   TDI_TieneRespAleatorias oResp = new TDI_TieneRespAleatorias();
                   oResp.IdPreAleatoria = System.Convert.ToInt32(obj[0]);
                   oResp.DescTieneRespAlea = System.Convert.ToString(obj[1]);
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
