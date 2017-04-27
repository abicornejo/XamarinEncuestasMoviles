using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using System.Collections;
using Entidades_EncuestasMoviles;

namespace DLL_EncuestasMoviles
{
    public class MngDatosArbolEncuesta
    {
        public static IList<THE_ArbolEncuesta> ObtenerArbol(int id_Encuesta)
        { 
            List<THE_ArbolEncuesta> lstArbolEncuesta = new List<THE_ArbolEncuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += "  SELECT DISTINCT preg.id_pregunta, preg.pregunta_desc, resp.id_respuesta, ";
            strSQL += "  resp.respuesta_desc, resp.id_siguientepregunta ";
            strSQL += "  FROM seml_the_respuestas resp, ";
            strSQL += "  seml_the_preguntas preg, ";
            strSQL += "  seml_the_encuesta enc ";
            strSQL += "  WHERE enc.id_encuesta = preg.id_encuesta ";
            strSQL += "  AND preg.id_pregunta = resp.id_pregunta(+) ";
            strSQL += "  AND enc.id_encuesta = " + id_Encuesta;
            strSQL += "  AND preg.preg_estatus = 'A' ";
            strSQL += "  AND RESP.RESP_ESTATUS (+)= 'A' ";
            strSQL += "  ORDER BY id_pregunta ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_Pregunta", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("Pregunta_Desc", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_Respuesta", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("Respuesta_Desc", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("ID_SIGUIENTEPREGUNTA", NHibernateUtil.Int32);//4                

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_ArbolEncuesta oNodo = new THE_ArbolEncuesta();
                    oNodo.ID_Pregunta = System.Convert.ToInt32(obj[0]);
                    oNodo.Pregunta_Desc = System.Convert.ToString(obj[1]);
                    oNodo.ID_Respuesta = System.Convert.ToInt32(obj[2]);
                    oNodo.Respuesta_Desc = System.Convert.ToString(obj[3]);
                    oNodo.ID_PreguntaAnterior = System.Convert.ToInt32(obj[4]);
                    lstArbolEncuesta.Add(oNodo);
                }

            }
            catch
            {
                lstArbolEncuesta = null;
                return lstArbolEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstArbolEncuesta;
        }
    }
}
