using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosPreguntas
    {
        public static IList<THE_Preguntas> ObtienePreguntasPorEncuesta(int IdEncuesta)
        {

            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Preguntas Pregunta WHERE ID_ENCUESTA = " + IdEncuesta + "AND PREGUNTA_DESC <> 'FIN DE LA ENCUESTA' AND PREG_ESTATUS = 'A' ORDER BY ID_PREGUNTA ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Preguntas>(strQuery);
            }
            catch (Exception ex)
            {
                return new List<THE_Preguntas>();
            }
        }

        public static IList<THE_Preguntas> ObtienePreguntasPorEncuestaConFinEncu(int IdEncuesta)
        {

            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Preguntas Pregunta WHERE ID_ENCUESTA = " + IdEncuesta + "AND PREG_ESTATUS = 'A' ORDER BY ID_PREGUNTA ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Preguntas>(strQuery);
            }
            catch (Exception ex)
            {
                return new List<THE_Preguntas>();
            }
        }

        public static Boolean GuardaPreguntaPorEncuesta(THE_Preguntas pregu)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_Preguntas>(pregu);
        }

        public static Boolean ActualizaPregunta(THE_Preguntas pregu)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Preguntas>(pregu);
        }

        public static IList<THE_Preguntas> ObtienePreguntaPorID(int IdPregunta)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Preguntas Pregunta WHERE ID_PREGUNTA = " + IdPregunta + "AND PREG_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Preguntas>(strQuery);
            }
            catch (Exception ex)
            {
                return new List<THE_Preguntas>();
            }
        }

        public static Boolean EliminaPregunta(THE_Preguntas Pregunta)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Preguntas>(Pregunta);
        }

        public static int ObtienePreguntaFinEncuesta(int IdEncuesta)
        {
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();
            int IdPreg = -1;

            strSQL += " SELECT MIN (id_pregunta)IDPreg ";
            strSQL += " FROM seml_the_preguntas ";
            strSQL += " WHERE id_encuesta = " + IdEncuesta;
            strSQL += " AND preg_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);
                consultaIQRY.AddScalar("IDPreg", NHibernateUtil.Int32);//0
                IList lista = consultaIQRY.List();

                if (lista.Count > 0)
                {
                    IdPreg = System.Convert.ToInt32(lista[0].ToString());
                }

            }
            catch (Exception ex)
            {
                return IdPreg;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return IdPreg;
        }
    }
}
