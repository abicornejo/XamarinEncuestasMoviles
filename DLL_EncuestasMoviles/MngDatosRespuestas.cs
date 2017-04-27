using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Entidades_EncuestasMoviles;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosRespuestas
    {
        public static IList<TDI_BaseRespuestas> ObtenerBaseRespuestas()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM TDI_BaseRespuestas Respuesta WHERE Respuesta.Estatus = 'A' ORDER BY Respuesta.RespuestasDesc Asc ";
                return NHibernateHelperORACLE.SingleSessionFind<TDI_BaseRespuestas>(strQuery);
            }
            catch (Exception ex)
            {
                return new List<TDI_BaseRespuestas>();
            }
        }

        public static IList<THE_Respuestas> ObtenerRespuestasPorPregunta(int IdPregunta)
        {
            List<THE_Respuestas> lstDispoDisponibles = new List<THE_Respuestas>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT respu.id_pregunta idpreg, respu.id_respuesta idrespu, ";
            strSQL += " respu.id_siguientepregunta idsigpreg, respu.resp_estatus respuestat, ";
            strSQL += " respu.respuesta_desc respdesc ";
            strSQL += " FROM seml_the_respuestas respu ";
            strSQL += " WHERE respu.id_pregunta = " + IdPregunta;
            strSQL += " AND respu.resp_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("idpreg", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("idrespu", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("idsigpreg", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("respuestat", NHibernateUtil.Character);//3
                consultaIQRY.AddScalar("respdesc", NHibernateUtil.String);//4

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Respuestas oResp = new THE_Respuestas();
                    oResp.IdPregunta = new THE_Preguntas() { IdPregunta = System.Convert.ToInt32(obj[0]) };
                    oResp.IdRespuesta = System.Convert.ToInt32(obj[1]);
                    oResp.IdSiguientePregunta = System.Convert.ToInt32(obj[2]);
                    oResp.RespuestaEstatus = System.Convert.ToChar(obj[3]);
                    oResp.RespuestaDescripcion = System.Convert.ToString(obj[4]);
                    List<THE_Preguntas> PreguntaDesc = (List<THE_Preguntas>)MngDatosPreguntas.ObtienePreguntaPorID(oResp.IdSiguientePregunta);
                    oResp.DescSigPreg = PreguntaDesc[0].PreguntaDesc;
                    lstDispoDisponibles.Add(oResp);
                }


            }
            catch (Exception ex)
            {

                lstDispoDisponibles = null;
                return lstDispoDisponibles;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstDispoDisponibles;
        }


        public static IList<THE_Respuestas> ObtenerRespuestasPorPregunta(int IdPregunta, int IdRespuesta)
        {
            List<THE_Respuestas> lstDispoDisponibles = new List<THE_Respuestas>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT respu.id_pregunta idpreg, respu.id_respuesta idrespu, ";
            strSQL += " respu.id_siguientepregunta idsigpreg, respu.resp_estatus respuestat, ";
            strSQL += " respu.respuesta_desc respdesc ";
            strSQL += " FROM seml_the_respuestas respu ";
            strSQL += " WHERE respu.id_pregunta = " + IdPregunta;
            strSQL += " AND respu.resp_estatus = 'A' AND respu.id_respuesta <> '" + IdRespuesta + "'";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("idpreg", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("idrespu", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("idsigpreg", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("respuestat", NHibernateUtil.AnsiChar);//3
                consultaIQRY.AddScalar("respdesc", NHibernateUtil.String);//4

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Respuestas oResp = new THE_Respuestas();
                    oResp.IdPregunta = new THE_Preguntas() { IdPregunta = System.Convert.ToInt32(obj[0]) };
                    oResp.IdRespuesta = System.Convert.ToInt32(obj[1]);
                    oResp.IdSiguientePregunta = System.Convert.ToInt32(obj[2]);
                    oResp.RespuestaEstatus = System.Convert.ToChar(obj[3]);
                    oResp.RespuestaDescripcion = System.Convert.ToString(obj[4]);
                    List<THE_Preguntas> PreguntaDesc = (List<THE_Preguntas>)MngDatosPreguntas.ObtienePreguntaPorID(oResp.IdSiguientePregunta);
                    oResp.DescSigPreg = PreguntaDesc[0].PreguntaDesc;
                    lstDispoDisponibles.Add(oResp);
                }


            }
            catch (Exception ex)
            {

                lstDispoDisponibles = null;
                return lstDispoDisponibles;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstDispoDisponibles;
        }

        public static Boolean GuardaRespuesta(THE_Respuestas Respu)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_Respuestas>(Respu);
        }

        public static Boolean EliminaRespuesta(THE_Respuestas IdRespuesta)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Respuestas>(IdRespuesta);
        }

        public static IList<THE_Respuestas> ObtieneRespuestaPorId(int IdRespuesta)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Respuestas Respuesta WHERE ID_RESPUESTA = " + IdRespuesta + "AND RESP_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Respuestas>(strQuery);
            }
            catch (Exception ex)
            {
                
                return new List<THE_Respuestas>();
            }
        }

        public static Boolean ActualizaRespuesta(THE_Respuestas respu)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Respuestas>(respu);
        }
    }
}
