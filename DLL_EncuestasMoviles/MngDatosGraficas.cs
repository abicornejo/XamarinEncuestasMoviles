using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;
using System.Data.OracleClient;
using System.Data;
using System.Net;

namespace DLL_EncuestasMoviles
{
    public class MngDatosGraficas
    {

        public static IList<THE_EncuestaEstatus> ConsultaEncuestasEstatus(int IdEncuesta, bool MostrarTodos)
        {
            List<THE_EncuestaEstatus> lstEstatusEncuesta = new List<THE_EncuestaEstatus>();

            string strSQL = string.Empty;

            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT COUNT(*) NUMERO, encdis.ID_ESTATUS FROM SEML_TDI_ENCUESTADISPOSITIVO encdis,  seml_the_dispositivo dispo";
            strSQL += " WHERE  encdis.ID_ENCUESTA = " + IdEncuesta;
            if (MostrarTodos)
                strSQL += " and dispo.DISPO_GRAFICA = 1 ";
            strSQL += " and encdis.id_dispositivo=dispo.id_dispositivo ";

            strSQL += " GROUP BY ID_ESTATUS";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("NUMERO", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("ID_ESTATUS", NHibernateUtil.Int32);//1
                
                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_EncuestaEstatus oGrafica = new THE_EncuestaEstatus();
                    oGrafica.IdEstatus = (Int32)obj[1];
                    oGrafica.Numero = (Int32)obj[0];
                    lstEstatusEncuesta.Add(oGrafica);
                }

                return lstEstatusEncuesta;
            }            
            catch 
            {
                lstEstatusEncuesta = null;
                return lstEstatusEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }    
        }


        public static IList<TDI_GraficasEncuesta> GraficarEncuesta(int IdEncuesta, bool MostrarTodos, bool FueraHorario,string Catalogos)
        {
            List<TDI_GraficasEncuesta> lstGraficarEncuesta = new List<TDI_GraficasEncuesta>();
            string strSQL = string.Empty;

            ISession session = NHibernateHelperORACLE.GetSession();
            
            strSQL += "  SELECT PREGUNTAS.ID_PREGUNTA, ";
            strSQL += "  PREGUNTAS.PREGUNTA_DESC, ";
            strSQL += "  RESPUESTAS.ID_RESPUESTA, ";
            strSQL += "  RESPUESTAS.RESPUESTA_DESC, ";
            if (FueraHorario)
            {
                if (Catalogos != "")
                {
                    strSQL += "  fn_cuenta_pregrespdisp_CATA( PREGUNTAS.ID_PREGUNTA,  RESPUESTAS.ID_RESPUESTA, " + IdEncuesta + ", 1 ,'" + Catalogos + "' ) CONTADOR, ";

                   
                }
                else
                {
                    strSQL += "  fn_cuenta_pregrespdisp( PREGUNTAS.ID_PREGUNTA,  RESPUESTAS.ID_RESPUESTA, " + IdEncuesta + ", 1 ) CONTADOR, ";
                }
            }
            else {
                if (Catalogos != "")
                {
                    strSQL += "  fn_cuenta_pregrespdisp_CATA( PREGUNTAS.ID_PREGUNTA,  RESPUESTAS.ID_RESPUESTA, " + IdEncuesta + ", 0 ,'" + Catalogos + "') CONTADOR, ";
                }
                else
                {
                    strSQL += "  fn_cuenta_pregrespdisp( PREGUNTAS.ID_PREGUNTA,  RESPUESTAS.ID_RESPUESTA, " + IdEncuesta + ", 0 ) CONTADOR, ";
                }
                 
            }
            strSQL += "  RESPUESTAS.ID_SIGUIENTEPREGUNTA, ";
            if (Catalogos != "")
            {
                strSQL += "  fn_cuenta_total_CATA(" + IdEncuesta + ", 0,'" + Catalogos + "' )    Total, ";

                strSQL += "  (SELECT DISPOS_ENC_CATOLOGO (RESPUESTAS.ID_RESPUESTA, " + ((FueraHorario) ? "1" : "0") + ",'" + Catalogos + "') FROM DUAL) Dispositivos, ";
            }
            else
            {
                strSQL += "  fn_cuenta_total(" + IdEncuesta + ", 0)    Total, ";
                strSQL += "  (SELECT DISPOSITIVOS_ENCUESTAss (RESPUESTAS.ID_RESPUESTA, " + ((FueraHorario) ? "1" : "0") + ") FROM DUAL) Dispositivos, ";

            }
                
            strSQL += "  PREGUNTAS.ID_TIPO_RESP, (SELECT DISPOSITIVOS_NUM_TEL (RESPUESTAS.ID_RESPUESTA, " + ((FueraHorario) ? "1" : "0") + ") FROM DUAL) TELEFONOS ";
            strSQL += "  FROM (  SELECT PREG.ID_PREGUNTA, PREG.PREGUNTA_DESC, PREG.ID_TIPO_RESP  ";
            strSQL += "  FROM SEML_THE_PREGUNTAS PREG ";
            strSQL += "  WHERE PREG.ID_ENCUESTA = " + IdEncuesta + " ";
            strSQL += "  GROUP BY PREG.ID_PREGUNTA, PREG.PREGUNTA_DESC, PREG.ID_TIPO_RESP) PREGUNTAS, ";
            strSQL += "  (SELECT ID_RESPUESTA, ID_PREGUNTA, RESPUESTA_DESC,ID_SIGUIENTEPREGUNTA ";
            strSQL += "  FROM SEML_THE_RESPUESTAS WHERE resp_estatus = 'A') RESPUESTAS ";
            strSQL += "  WHERE PREGUNTAS.ID_PREGUNTA = RESPUESTAS.ID_PREGUNTA(+) ";
            strSQL += "  ORDER BY PREGUNTAS.ID_PREGUNTA ASC, RESPUESTAS.ID_RESPUESTA ASC ";


            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_PREGUNTA", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PREGUNTA_DESC", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_RESPUESTA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("RESPUESTA_DESC", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("CONTADOR", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("ID_SIGUIENTEPREGUNTA", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("Total", NHibernateUtil.Int32);//6
                consultaIQRY.AddScalar("Dispositivos", NHibernateUtil.String);//7
                consultaIQRY.AddScalar("ID_TIPO_RESP", NHibernateUtil.Int32);//8
                consultaIQRY.AddScalar("TELEFONOS", NHibernateUtil.String);//9

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_GraficasEncuesta oGrafica = new TDI_GraficasEncuesta();
                    oGrafica.IdPregunta = obj[0] == null || obj[0].ToString() == string.Empty ? 0 : (Int32)obj[0];
                    oGrafica.PreguntaDescripcion = obj[1] == null ? string.Empty : (String)obj[1];
                    oGrafica.IdRespuesta = obj[2] == null || obj[2].ToString() == string.Empty ? 0 : (Int32)obj[2];
                    oGrafica.RespuestaDescripcion = obj[3] == null ? string.Empty : (String)obj[3];
                    oGrafica.Contador = obj[4] == null || obj[4].ToString() == string.Empty ? 0 : (Int32)obj[4];
                    oGrafica.IdSiguientePregunta = obj[5] == null || obj[5].ToString() == string.Empty ? 0 : (Int32)obj[5];
                    oGrafica.Total = obj[6] == null || obj[6].ToString() == string.Empty ? 0 : (Int32)obj[6];
                    oGrafica.Dispositivos = obj[7] == null || obj[7].ToString() == string.Empty ? string.Empty : (String)obj[7];
                    oGrafica.IDTipoResp = obj[8] == null ||  Convert.ToInt32(obj[8].ToString()) == 0 ? 0: (Int32)obj[8];
                    oGrafica.Num_Telefonicos = obj[9] == null || obj[9].ToString() == string.Empty ? string.Empty : (String)obj[9];

                    lstGraficarEncuesta.Add(oGrafica);
                }

                return lstGraficarEncuesta;
            }
            catch (Exception ex)
            {
                lstGraficarEncuesta = null;
                return lstGraficarEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }            
        }


        public static IList<TDI_GraficasEncuesta> DibujaGrafica(int IdEncuesta, bool DentroHorario, string Catalogos, string idPregunta)
        {
            List<TDI_GraficasEncuesta> lstGraficarEncuesta = new List<TDI_GraficasEncuesta>();
            string strSQL = string.Empty;

            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += "    SELECT PRE.ID_PREGUNTA,PRE.PREGUNTA_DESC, RESP.ID_RESPUESTA, RESP.RESPUESTA_DESC, ";
            //if (DentroHorario)
            //{
            //    strSQL += "   FN_CUENTA_PREGRESPDISP_CATA (PRE.ID_PREGUNTA, RESP.ID_RESPUESTA, "+IdEncuesta.ToString()+", 1,'" + Catalogos + "') CONTADOR,    ";
            //}
            //else {
            //    strSQL += "   FN_CUENTA_PREGRESPDISP_CATA (PRE.ID_PREGUNTA, RESP.ID_RESPUESTA, "+IdEncuesta.ToString()+", 0,'" + Catalogos + "') CONTADOR,    ";
            //}

            strSQL += "           FN_CUENTA_TOTAL_CATA ("+IdEncuesta+", 0,'"+Catalogos+"') TOTAL, ";
            strSQL += "   RESP.ID_SIGUIENTEPREGUNTA, DISPO.ID_DISPOSITIVO,OPTUSRCAT.OPCIONCAT_DESC,OPTUSRCAT.ID_OPCIONCAT, PRE.ID_TIPO_RESP ";
            strSQL += "      FROM SEML_THE_PREGUNTASRESPUESTAS PRE_RESP ,SEML_THE_PREGUNTAS PRE, SEML_THE_ENCUESTA ENC,  ";
            strSQL += "                SEML_THE_RESPUESTAS RESP, SEML_THE_DISPOSITIVO DISPO,SEML_THE_USUARIO USR, SEML_TDI_USUARIOCAT USRCAT, SEML_TDI_OPCIONCAT OPTUSRCAT, ";
            strSQL += "               SEML_THE_CATALOGO CAT,   SEML_TDI_USUARIODISPOSITIVO USRDISPO ";
            strSQL += "      WHERE PRE_RESP.ID_PREGUNTA(+)=PRE.ID_PREGUNTA ";
            strSQL += "      AND PRE.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "      AND PRE_RESP.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "      AND PRE_RESP.ID_RESPUESTA=RESP.ID_RESPUESTA ";
            strSQL += "      AND PRE.ID_PREGUNTA=RESP.ID_PREGUNTA(+) ";
            strSQL += "      AND PRE_RESP.ID_DISPOSITIVO=DISPO.ID_DISPOSITIVO ";
            strSQL += "      AND ENC.ID_ENCUESTA="+IdEncuesta.ToString();
            if (idPregunta != string.Empty)
            {
                strSQL += "  AND PRE.ID_PREGUNTA="+idPregunta;
            }
            strSQL += "      AND OPTUSRCAT.ID_OPCIONCAT IN (" + Catalogos + ")  ";

            strSQL += "      AND USR.USUA_LLAV_PR = USRCAT.USUA_LLAV_PR ";
            strSQL += "      AND USRCAT.ID_OPCIONCAT = OPTUSRCAT.ID_OPCIONCAT ";
            strSQL += "      AND CAT.ID_CATALOGO = OPTUSRCAT.ID_CATALOGO     ";
            strSQL += "      AND USRDISPO.USUA_LLAV_PR=  USR.USUA_LLAV_PR ";
            strSQL += "      AND USRDISPO.ID_DISPOSITIVO= DISPO.ID_DISPOSITIVO  ";
            strSQL += "      AND USR.USUA_ESTATUS = 'A' ";
            strSQL += "      AND USRCAT.USUACAT_STAT = 'A' ";
            strSQL += "      AND OPTUSRCAT.OPCIONCAT_STAT = 'A' ";
            strSQL += "      AND CAT.CATALOGO_STAT = 'A' ";
            strSQL += "      AND DISPO.DISPO_ESTATUS = 'A' ";
            strSQL += "      AND USRDISPO.USUADISPO_ESTATUS = 'A'  ";
            if (DentroHorario){
            ///*EN CASO DE QUE ESTE DENTRO DEL HORARIO ESCRIBO LAS SIGUIENTES LINEAS*/            
                strSQL += "      AND DISPO.DISPO_GRAFICA=1 ";
                strSQL += "     AND PRE_RESP.PREGRESP_FECR <=  to_date(to_char(ENC.ENCUESTA_FECHLIMITE|| ' ' ||nvl(ENC.ENCUESTA_HORALIMITE,'23:59')),'dd/MM/RRRR HH24:MI')  ";
            ///*EN CASO DE QUE ESTE DENTRO DEL HORARIO*/            
            }
            strSQL += "      GROUP BY   PRE.ID_PREGUNTA,PRE.PREGUNTA_DESC, RESP.ID_RESPUESTA, RESP.RESPUESTA_DESC,DISPO.ID_DISPOSITIVO,OPTUSRCAT.OPCIONCAT_DESC, ";
            strSQL += "      OPTUSRCAT.ID_OPCIONCAT, RESP.ID_SIGUIENTEPREGUNTA, PRE.ID_TIPO_RESP ";
            strSQL += "      ORDER BY RESP.ID_RESPUESTA ";


            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_PREGUNTA", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PREGUNTA_DESC", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_RESPUESTA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("RESPUESTA_DESC", NHibernateUtil.String);//3
              //  consultaIQRY.AddScalar("CONTADOR", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("TOTAL", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("ID_SIGUIENTEPREGUNTA", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("ID_DISPOSITIVO", NHibernateUtil.Int32);//6
                consultaIQRY.AddScalar("OPCIONCAT_DESC", NHibernateUtil.String);//7
                consultaIQRY.AddScalar("ID_OPCIONCAT", NHibernateUtil.Int32);//8
                consultaIQRY.AddScalar("ID_TIPO_RESP", NHibernateUtil.Int32);//9
             

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_GraficasEncuesta oGrafica = new TDI_GraficasEncuesta();
                    oGrafica.IdPregunta = obj[0] == null || obj[0].ToString() == string.Empty ? 0 : (Int32)obj[0];
                    oGrafica.PreguntaDescripcion = obj[1] == null ? string.Empty : (String)obj[1];
                    oGrafica.IdRespuesta = obj[2] == null || obj[2].ToString() == string.Empty ? 0 : (Int32)obj[2];
                    oGrafica.RespuestaDescripcion = obj[3] == null ? string.Empty : (String)obj[3];
                    //oGrafica.Contador = obj[4] == null || obj[4].ToString() == string.Empty ? 0 : (Int32)obj[4];
                    oGrafica.Total = obj[4] == null || obj[4].ToString() == string.Empty ? 0 : (Int32)obj[4];
                    oGrafica.IdSiguientePregunta = obj[5] == null || obj[5].ToString() == string.Empty ? 0 : (Int32)obj[5];


                    oGrafica.ID_Dispo = obj[6] == null || obj[6].ToString() == string.Empty ? 0 : (Int32)obj[6];
                    oGrafica.Desc_Catalogo = obj[7] == null ? string.Empty : (String)obj[7];
                    oGrafica.ID_Opcion_Catalogo =  (Int32)obj[8];
                    oGrafica.IDTipoResp = obj[9] == null || Convert.ToInt32(obj[9].ToString()) == 0 ? 0 : (Int32)obj[9];
                    

                    lstGraficarEncuesta.Add(oGrafica);
                }

                return lstGraficarEncuesta;
            }
            catch (Exception ex)
            {
                lstGraficarEncuesta = null;
                return lstGraficarEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
        }


        public static bool EnviaGraficaCorreo(string NomRemitente, string Destinatario, string NombDestinatario,string Remitente, int IdEncuesta)
        {

            bool blnBand;
            string strSQL = string.Empty;
            int intresp;
            System.Data.OracleClient.OracleParameter[] Parametros = new System.Data.OracleClient.OracleParameter[5];

            try
            {

                Parametros[0] = new System.Data.OracleClient.OracleParameter();
                Parametros[0].OracleType = System.Data.OracleClient.OracleType.VarChar;
                Parametros[0].Value = Remitente;
                Parametros[0].Direction = System.Data.ParameterDirection.Input;
                Parametros[0].ParameterName = "CorreoRemitente";

                Parametros[1] = new System.Data.OracleClient.OracleParameter();
                Parametros[1].OracleType = System.Data.OracleClient.OracleType.VarChar;
                Parametros[1].Value = NomRemitente;
                Parametros[1].Direction = System.Data.ParameterDirection.Input;
                Parametros[1].ParameterName = "NombreRemitente";

                Parametros[2] = new System.Data.OracleClient.OracleParameter();
                Parametros[2].OracleType = System.Data.OracleClient.OracleType.VarChar;
                Parametros[2].Value = Destinatario;
                Parametros[2].Direction = System.Data.ParameterDirection.Input;
                Parametros[2].ParameterName = "CorreoDestinatario";

                Parametros[3] = new System.Data.OracleClient.OracleParameter();
                Parametros[3].OracleType = System.Data.OracleClient.OracleType.VarChar;
                Parametros[3].Value = NombDestinatario;
                Parametros[3].Direction = System.Data.ParameterDirection.Input;
                Parametros[3].ParameterName = "NombreDestinatario";

                Parametros[4] = new System.Data.OracleClient.OracleParameter();
                Parametros[4].OracleType = System.Data.OracleClient.OracleType.Number;
                Parametros[4].Value = IdEncuesta;
                Parametros[4].Direction = System.Data.ParameterDirection.Input;
                Parametros[4].ParameterName = "IdEncuesta";

                intresp = NHibernateHelperORACLE.ExecuteNonQuery("ENVIA_CORREO_GRAFICA", Parametros);
                if (intresp == 1)
                {
                    blnBand = true;
                }
                else
                {
                    blnBand = false;
                }

            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosGraficas");
                blnBand = false;
            }
            finally
            {
                Parametros = null;
            }

            return blnBand; 
        }

        public static IList<TDI_GraficasEncuesta> GraficaEncuestaExportaPDF(int IdEncuesta)
        {
            List<TDI_GraficasEncuesta> lstGraficarEncuesta = new List<TDI_GraficasEncuesta>();
            string strSQL = string.Empty;

            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT PREGUNTAS.ID_PREGUNTA, ";
            strSQL += " PREGUNTAS.PREGUNTA_DESC, ";
            strSQL += " RESPUESTAS.ID_RESPUESTA, ";
            strSQL += " RESPUESTAS.RESPUESTA_DESC, ";
            strSQL += " (SELECT COUNT (*) ";
            strSQL += " FROM SEML_THE_PREGUNTASRESPUESTAS ";
            strSQL += " WHERE     ID_ENCUESTA = " + IdEncuesta + " ";
            strSQL += " AND ID_PREGUNTA = PREGUNTAS.ID_PREGUNTA ";
            strSQL += " AND ID_RESPUESTA = RESPUESTAS.ID_RESPUESTA) ";
            strSQL += " CONTADOR, ";
            strSQL += " RESPUESTAS.ID_SIGUIENTEPREGUNTA, ";
            strSQL += " (SELECT count (*) ";
            strSQL += " FROM seml_the_preguntasrespuestas prsres, seml_tdi_encuestadispositivo encdis ";
            strSQL += " WHERE prsres.id_encuesta = " + IdEncuesta + " ";
            strSQL += " AND encdis.ID_ENCUESTA = prsres.id_encuesta ";
            strSQL += " AND encdis.ID_ESTATUS = 4) Total ";
            strSQL += " FROM (  SELECT PREG.ID_PREGUNTA, PREG.PREGUNTA_DESC ";
            strSQL += " FROM SEML_THE_PREGUNTAS PREG ";
            strSQL += " WHERE PREG.ID_ENCUESTA = " + IdEncuesta + " ";
            strSQL += " GROUP BY PREG.ID_PREGUNTA, PREG.PREGUNTA_DESC) PREGUNTAS, ";
            strSQL += " (SELECT ID_RESPUESTA, ";
            strSQL += " ID_PREGUNTA, ";
            strSQL += " RESPUESTA_DESC, ";
            strSQL += " ID_SIGUIENTEPREGUNTA ";
            strSQL += " FROM SEML_THE_RESPUESTAS) RESPUESTAS ";
            strSQL += " WHERE PREGUNTAS.ID_PREGUNTA = RESPUESTAS.ID_PREGUNTA(+) ";
            strSQL += " AND  preguntas.id_pregunta <> (select MIN(id_pregunta) from seml_the_preguntas where id_Encuesta= " + IdEncuesta +") ";
            strSQL += " ORDER BY PREGUNTAS.ID_PREGUNTA ASC, RESPUESTAS.ID_RESPUESTA ASC ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_PREGUNTA", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PREGUNTA_DESC", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_RESPUESTA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("RESPUESTA_DESC", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("CONTADOR", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("ID_SIGUIENTEPREGUNTA", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("Total", NHibernateUtil.Int32);//6

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_GraficasEncuesta oGrafica = new TDI_GraficasEncuesta();
                    oGrafica.IdPregunta = obj[0] == null || obj[0].ToString() == string.Empty ? 0 : (Int32)obj[0];
                    oGrafica.PreguntaDescripcion = obj[1] == null ? string.Empty : (String)obj[1];
                    oGrafica.IdRespuesta = obj[2] == null || obj[2].ToString() == string.Empty ? 0 : (Int32)obj[2];
                    oGrafica.RespuestaDescripcion = obj[3] == null ? string.Empty : (String)obj[3];
                    oGrafica.Contador = obj[4] == null || obj[4].ToString() == string.Empty ? 0 : (Int32)obj[4];
                    oGrafica.IdSiguientePregunta = obj[5] == null || obj[5].ToString() == string.Empty ? 0 : (Int32)obj[5];
                    oGrafica.Total = obj[6] == null || obj[6].ToString() == string.Empty ? 0 : (Int32)obj[6];
                    lstGraficarEncuesta.Add(oGrafica);
                }
            }
            catch (Exception ex)
            {
                lstGraficarEncuesta = null;
                return lstGraficarEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            return lstGraficarEncuesta;
        }
        
        public static IList<TDI_GraficasEncuesta> GeTopOfMind(string IdPregunta, string IdsDispositivos)
        {
            List<TDI_GraficasEncuesta> lstGraficarEncuesta = new List<TDI_GraficasEncuesta>();
            string strSQL = string.Empty;

            ISession session = NHibernateHelperORACLE.GetSession();

            //strSQL += " SELECT MIN(RSEL.ORDEN_RESP_SELECTED) VALOR, RSEL.DESC_RESP_SELECTED DESCRIPCION FROM SEML_THE_LOG_RESP_SELECTED RSEL WHERE RSEL.REF_ID_ENCUESTA=234";
            //strSQL += " AND RSEL.RESP_ID_SELECTED IN (4736,4737,4738,4739,4740,4741)";
            //strSQL += " GROUP BY RSEL.DESC_RESP_SELECTED";


            strSQL += " SELECT  LRESP.ORDEN_RESP_SELECTED VALOR, ";
            strSQL += " LRESP.DESC_RESP_SELECTED DESCRIPCION ";
            strSQL += " FROM SEML_THE_LOG_RESP_SELECTED LRESP , SEML_THE_RESPUESTAS RESP, SEML_THE_ENCUESTA ENC, SEML_THE_PREGUNTAS PRE, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += " WHERE  ";
            strSQL += " LRESP.ORDEN_RESP_SELECTED IN( ";
            strSQL += " 	SELECT MIN (LRESP.ORDEN_RESP_SELECTED) MINIMO  ";
            strSQL += " 	FROM SEML_THE_LOG_RESP_SELECTED LRESP , SEML_THE_RESPUESTAS RESP, SEML_THE_ENCUESTA ENC, SEML_THE_PREGUNTAS PRE, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += " 	WHERE LRESP.RESP_ID_SELECTED=RESP.ID_RESPUESTA ";
            strSQL += " 	AND LRESP.REF_ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += " 	AND RESP.ID_PREGUNTA=PRE.ID_PREGUNTA ";
            strSQL += " 	AND RESP.RESP_ESTATUS='A' ";
            strSQL += " 	AND ENC.ENCUESTA_STAT='A' ";
            strSQL += " 	AND PRE.PREG_ESTATUS='A' ";
            strSQL += " 	AND DISPO.DISPO_ESTATUS='A' ";
            strSQL += " 	AND PRE.ID_PREGUNTA="+IdPregunta;
            strSQL += " 	AND LRESP.NUM_TEL(+)=DISPO.DISPO_NUMTELEFONO ";
            if (IdsDispositivos != string.Empty)
            {
                strSQL += " 	AND DISPO.ID_DISPOSITIVO IN (" + IdsDispositivos + ") ";
            }
            strSQL += " ) ";
            strSQL += " AND LRESP.RESP_ID_SELECTED=RESP.ID_RESPUESTA ";
            strSQL += " AND LRESP.REF_ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += " AND RESP.ID_PREGUNTA=PRE.ID_PREGUNTA ";
            strSQL += " AND RESP.RESP_ESTATUS='A' ";
            strSQL += " AND ENC.ENCUESTA_STAT='A' ";
            strSQL += " AND PRE.PREG_ESTATUS='A' ";
            strSQL += " AND DISPO.DISPO_ESTATUS='A' ";
            strSQL += " AND PRE.ID_PREGUNTA="+IdPregunta;
            strSQL += " AND LRESP.NUM_TEL(+)=DISPO.DISPO_NUMTELEFONO ";
            if (IdsDispositivos != string.Empty)
            {
                strSQL += " AND DISPO.ID_DISPOSITIVO IN (" + IdsDispositivos + ") ";
            }
            strSQL += " ORDER BY LRESP.ORDEN_RESP_SELECTED ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("VALOR", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("DESCRIPCION", NHibernateUtil.String);//1
               

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_GraficasEncuesta oGrafica = new TDI_GraficasEncuesta();
                    oGrafica.Contador = obj[0] == null || obj[0].ToString() == string.Empty ? 0 : (Int32)obj[0];
                    oGrafica.RespuestaDescripcion = obj[1] == null ? string.Empty : (String)obj[1];
                    
                 
                    lstGraficarEncuesta.Add(oGrafica);
                }
            }
            catch (Exception ex)
            {
                lstGraficarEncuesta = null;
                return lstGraficarEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            return lstGraficarEncuesta;
        }
    
    }
}
