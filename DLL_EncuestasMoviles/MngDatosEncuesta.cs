using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosEncuesta
    {
        public static List<THE_Usuario> ReporteTiempoRespuesta(string idsencuestas, string idsPersonas)
        {

            #region Query Armado
            List<THE_Usuario> listaUsr = new List<THE_Usuario>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT NOMBRE, ";
            strSQL += " GENERO, ";
            strSQL += " GRUPOS_DE_EDAD, ";
            strSQL += " NSE, ";
            strSQL += " ENCUESTA, ";
            strSQL += " NVL ( ";
            strSQL += " (SELECT MAX (LOGT_FECH) ";
            strSQL += " FROM SEML_THE_LOGT ";
            strSQL += " WHERE (LOGT_DESC = 'Se Envio la Encuesta " + idsencuestas.ToString() + " al Dispositivo ' || ID_DISPOSITIVO)), NULL) FECHA_ENVIO,";
            strSQL += " (SELECT MIN(FECHA_EVENTO)  ";
            strSQL += " FROM SEML_THE_LOG_RESP_SELECTED LSEL, SEML_The_DISPOSITIVO DISP ";
            strSQL += " WHERE DISP.DISPO_NUMTELEFONO = NUM_TEL  ";
            strSQL += " AND  DISP.ID_DISPOSITIVO =   TABLA.ID_DISPOSITIVO ";
            strSQL += " AND REF_ID_ENCUESTA= " + idsencuestas.ToString();
            strSQL += " ) ";
            strSQL += " FECHA_EMPIEZA, ";
            strSQL += " (SELECT MAX(FECHA_EVENTO)  ";
            strSQL += " FROM SEML_THE_LOG_RESP_SELECTED LSEL, SEML_The_DISPOSITIVO DISP ";
            strSQL += " WHERE DISP.DISPO_NUMTELEFONO = NUM_TEL  ";
            strSQL += " AND  DISP.ID_DISPOSITIVO =   TABLA.ID_DISPOSITIVO ";
            strSQL += " AND REF_ID_ENCUESTA= " + idsencuestas.ToString()+" ) ";
            strSQL += " FECHA_TERMINA, ";
            strSQL += " FECHA_RECEPCION ";
            strSQL += " FROM (SELECT USUA_NOMBRE NOMBRE, ";
            strSQL += " CASE ";
            strSQL += " WHEN SEXO = 'M' THEN 'HOMBRE' ";
            strSQL += " WHEN SEXO = 'F' THEN 'MUJER' ";
            strSQL += " END ";
            strSQL += " GENERO, ";
            strSQL += " (SELECT OPCIONCAT_DESC ";
            strSQL += " FROM SEML_TDI_OPCIONCAT OPCION, ";
            strSQL += " SEML_TDI_USUARIOCAT OPCIONES ";
            strSQL += " WHERE     OPCION.ID_OPCIONCAT = OPCIONES.ID_OPCIONCAT ";
            strSQL += " AND USUA_LLAV_PR = USUARIO.USUA_LLAV_PR ";
            strSQL += " AND ID_CATALOGO = 4) ";
            strSQL += " GRUPOS_DE_EDAD, ";
            strSQL += " (SELECT OPCIONCAT_DESC ";
            strSQL += " FROM SEML_TDI_OPCIONCAT OPCION, ";
            strSQL += " SEML_TDI_USUARIOCAT OPCIONES ";
            strSQL += " WHERE     OPCION.ID_OPCIONCAT = OPCIONES.ID_OPCIONCAT ";
            strSQL += " AND USUA_LLAV_PR = USUARIO.USUA_LLAV_PR ";
            strSQL += " AND ID_CATALOGO = 5) ";
            strSQL += " NSE, ";
            strSQL += " (SELECT ENCUESTA_NOMBRE ";
            strSQL += " FROM SEML_THE_ENCUESTA ";
            strSQL += "  WHERE ID_ENCUESTA = " + idsencuestas.ToString() + ") ENCUESTA, ";
            strSQL += " (SELECT MIN (PREGRESP_FECR) ";
            strSQL += " FROM SEML_THE_PREGUNTASRESPUESTAS RESP, ";
            strSQL += " SEML_TDI_USUARIODISPOSITIVO USUADISP ";
            strSQL += " WHERE     RESP.ID_DISPOSITIVO = USUADISP.ID_DISPOSITIVO ";
            strSQL += " AND ID_ENCUESTA = " + idsencuestas.ToString();
            strSQL += " AND USUA_LLAV_PR = USUARIO.USUA_LLAV_PR) ";
            strSQL += " FECHA_RECEPCION, ";
            strSQL += " ID_DISPOSITIVO ";
            strSQL += " FROM SEML_THE_USUARIO USUARIO, SEML_TDI_USUARIODISPOSITIVO USUADISP ";
            strSQL += " WHERE USUARIO.USUA_LLAV_PR = USUADISP.USUA_LLAV_PR ";
            strSQL += " AND SEXO IS NOT NULL) TABLA ";
            
            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("NOMBRE", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("GENERO", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("GRUPOS_DE_EDAD", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("NSE", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("ENCUESTA", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("FECHA_ENVIO", NHibernateUtil.DateTime);//5
                consultaIQRY.AddScalar("FECHA_EMPIEZA", NHibernateUtil.DateTime);//6
                consultaIQRY.AddScalar("FECHA_TERMINA", NHibernateUtil.DateTime);//7
                consultaIQRY.AddScalar("FECHA_RECEPCION", NHibernateUtil.DateTime);//8

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {                    
                    THE_Usuario oUsuario = new THE_Usuario();

                    oUsuario.UsuaNom = obj[0] == null || obj[0].ToString() == string.Empty ? string.Empty : (String)obj[0];
                    oUsuario.UsuGen = obj[1] == null || obj[1].ToString() == string.Empty ? string.Empty : (String)obj[1];
                    oUsuario.UsuGrEdad = obj[2] == null || obj[2].ToString() == string.Empty ? string.Empty : (String)obj[2];
                    oUsuario.UsuNse = obj[3] == null || obj[3].ToString() == string.Empty ? string.Empty : (String)obj[3];
                    oUsuario.UsuEnc = obj[4] == null || obj[4].ToString() == string.Empty ? string.Empty : (String)obj[4];
                    oUsuario.UsuFeEnv = obj[5] == null ? string.Empty : (obj[5].ToString());
                    oUsuario.UsuFeEmp = obj[6] == null ? string.Empty : (obj[6].ToString());
                    oUsuario.UsuFeTer = obj[7] == null ? string.Empty : (obj[7].ToString());
                    oUsuario.UsuFeResp = obj[8] == null ? string.Empty : (obj[8].ToString());

                    listaUsr.Add(oUsuario);
                }
            }
            catch (Exception ex)
            {
                listaUsr = null;
                return listaUsr;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaUsr;
            #endregion

        }

        public static List<THE_Usuario> ReporteRespuestaByEncuesta(string idEncuesta)
        {

            #region Query Armado
            List<THE_Usuario> listaUsr = new List<THE_Usuario>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL+="  SELECT USUA_NOMBRE NOMBRE, ";
            strSQL+=" CASE WHEN SEXO = 'M' THEN 'HOMBRE' WHEN SEXO = 'F' THEN 'MUJER' END ";
            strSQL+="   GENERO, ";
            strSQL+=" (SELECT OPCIONCAT_DESC ";
            strSQL+=" FROM SEML_TDI_OPCIONCAT OPCION, SEML_TDI_USUARIOCAT OPCIONES ";
            strSQL+=" WHERE     OPCION.ID_OPCIONCAT = OPCIONES.ID_OPCIONCAT ";
            strSQL+=" AND USUA_LLAV_PR = USUARIO.USUA_LLAV_PR ";
            strSQL+=" AND ID_CATALOGO = 4) ";
            strSQL+=" GRUPOS_DE_EDAD, ";
            strSQL+=" (SELECT OPCIONCAT_DESC ";
            strSQL+=" FROM SEML_TDI_OPCIONCAT OPCION, SEML_TDI_USUARIOCAT OPCIONES ";
            strSQL+=" WHERE     OPCION.ID_OPCIONCAT = OPCIONES.ID_OPCIONCAT ";
            strSQL+=" AND USUA_LLAV_PR = USUARIO.USUA_LLAV_PR ";
            strSQL+=" AND ID_CATALOGO = 5) ";
            strSQL+=" NSE, ";
            strSQL+=" ENCUESTA_NOMBRE ENCUESTA, ";
            strSQL+=" PREGS.PREGUNTA_DESC PREGUNTA, ";
            strSQL+=" RESPS.RESPUESTA_DESC RESPUESTA, PREGS.ID_PREGUNTA ID_PREG, RESPS.ID_RESPUESTA, USUARIO.USUA_LLAV_PR ";
            strSQL+=" FROM SEML_THE_USUARIO USUARIO, ";
            strSQL+=" SEML_THE_PREGUNTASRESPUESTAS PREG, ";
            strSQL+=" SEML_TDI_USUARIODISPOSITIVO USUADISP, ";
            strSQL+=" SEML_THE_PREGUNTAS PREGS, ";
            strSQL+=" SEML_THE_RESPUESTAS RESPS, ";
            strSQL+=" SEML_THE_ENCUESTA ENCU ";
            strSQL+=" WHERE     USUARIO.USUA_LLAV_PR = USUADISP.USUA_LLAV_PR ";
            strSQL+=" AND USUADISP.ID_DISPOSITIVO = PREG.ID_DISPOSITIVO ";
            strSQL+=" AND PREG.ID_PREGUNTA = PREGS.ID_PREGUNTA ";
            strSQL+=" AND RESPS.ID_RESPUESTA = PREG.ID_RESPUESTA ";
            strSQL+=" AND PREGS.ID_ENCUESTA = ENCU.ID_ENCUESTA ";
            strSQL+=" AND SEXO IS NOT NULL ";
            strSQL += " AND PREG.ID_ENCUESTA =" + idEncuesta.ToString() + " ";
            strSQL += " ORDER BY USUARIO.USUA_LLAV_PR, PREGS.ID_PREGUNTA, RESPS.ID_RESPUESTA ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("NOMBRE", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("GENERO", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("GRUPOS_DE_EDAD", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("NSE", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("ENCUESTA", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("PREGUNTA", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("RESPUESTA", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("ID_PREG", NHibernateUtil.Int32);//7
                consultaIQRY.AddScalar("ID_RESPUESTA", NHibernateUtil.Int32);//8
                consultaIQRY.AddScalar("USUA_LLAV_PR", NHibernateUtil.Int32);//9

                IList lista = consultaIQRY.List();
                string cadena = "";
                int idUsr = 0;
                foreach (Object[] obj in lista)
                {
                    if (System.Convert.ToInt32(obj[9]) != idUsr)
                    {
                        idUsr = System.Convert.ToInt32(obj[9]);
                        cadena += idUsr + "<" + System.Convert.ToString(obj[5]) + ">" + System.Convert.ToString(obj[6]) + ";";
                    }
                    else
                    {
                        cadena = cadena.Substring(0, cadena.Length - 1);
                        cadena += "|" + System.Convert.ToString(obj[5]) + ">" + System.Convert.ToString(obj[6]) + ";";
                    }

                }

                cadena = cadena.Substring(0, cadena.Length - 1);

                string[] arreglo = cadena.Split(';');

                foreach (var datos in arreglo)
                {
                    int idUser = System.Convert.ToInt32(datos.Split('<')[0]);
                    string catalogUser = datos.Split('<')[1];
                    foreach (Object[] obj in lista)
                    {
                        if (System.Convert.ToInt32(obj[9]) == idUser)
                        {
                            THE_Usuario oUsuario = new THE_Usuario();

                            oUsuario.UsuaNom = obj[0] == null || obj[0].ToString() == string.Empty ? string.Empty : (String)obj[0];
                            oUsuario.UsuGen = obj[1] == null || obj[1].ToString() == string.Empty ? string.Empty : (String)obj[1];
                            oUsuario.UsuGrEdad = obj[2] == null || obj[2].ToString() == string.Empty ? string.Empty : (String)obj[2];
                            oUsuario.UsuNse = obj[3] == null || obj[3].ToString() == string.Empty ? string.Empty : (String)obj[3];
                            oUsuario.UsuEnc = obj[4] == null || obj[4].ToString() == string.Empty ? string.Empty : (String)obj[4];
                            oUsuario.Pregunta = obj[5] == null || obj[5].ToString() == string.Empty ? string.Empty : (String)obj[5];
                            oUsuario.Respuesta = obj[6] == null || obj[6].ToString() == string.Empty ? string.Empty : (String)obj[6];
                            oUsuario.UsuarioLlavePrimaria = idUser;
                            oUsuario.Catalogos = catalogUser; 
                            listaUsr.Add(oUsuario);
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                listaUsr = null;
                return listaUsr;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaUsr;
            #endregion

        }



        public static IList<THE_Encuesta> ObtieneEncuestaPorID(int IdEncuesta)
        {
            #region Query Armado
            List<THE_Encuesta> listaEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.empl_llav_pr empl, encuesta.encuesta_fechcrea fechcrea, ";
            strSQL += " encuesta.encuesta_fechlimite fechlimite, ";
            strSQL += " encuesta.encuesta_nombre nombreencu, encuesta.id_encuesta idencu, ";
            strSQL += " encuesta.id_estatus estatus, encuesta.encuesta_stat Stat, encuesta.puntos_encuesta Puntos, ";
            strSQL += " encuesta.minrequerido minreq, encuesta.maxesperado maxesp, encuesta.encuesta_horalimite horalimite, encuesta.id_tipoencuesta idtipoencuesta";
            strSQL += " FROM seml_the_encuesta encuesta ";
            strSQL += " WHERE encuesta.id_encuesta = " + IdEncuesta;
            strSQL += " AND encuesta.ENCUESTA_STAT = 'A'" ;

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("empl", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("fechcrea", NHibernateUtil.DateTime);//1
                consultaIQRY.AddScalar("fechlimite", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("nombreencu", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("idencu", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("estatus", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("Stat", NHibernateUtil.Character);//6
                consultaIQRY.AddScalar("Puntos", NHibernateUtil.Int32);//7
                consultaIQRY.AddScalar("minreq", NHibernateUtil.Int32);//8
                consultaIQRY.AddScalar("maxesp", NHibernateUtil.Int32);//9
                consultaIQRY.AddScalar("horalimite", NHibernateUtil.String);//10
                consultaIQRY.AddScalar("idtipoencuesta", NHibernateUtil.Int32);//11

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Encuesta oEncuesta = new THE_Encuesta();
                    oEncuesta.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                    oEncuesta.FechaCreaEncuesta = Convert.ToDateTime(obj[1]);
                    oEncuesta.FechaLimiteEncuesta = System.Convert.ToString(obj[2]);
                    oEncuesta.NombreEncuesta = System.Convert.ToString(obj[3]);
                    oEncuesta.IdEncuesta = System.Convert.ToInt32(obj[4]);
                    oEncuesta.EncuestaStat = System.Convert.ToChar(obj[6]);
                    oEncuesta.IdEstatus = new TDI_Estatus { IdEstatus = System.Convert.ToInt32(obj[5]) };
                    oEncuesta.PuntosEncuesta = System.Convert.ToInt32(obj[7]);
                    oEncuesta.MinimoRequerido = System.Convert.ToInt32(obj[8]);
                    oEncuesta.MaximoEsperado = System.Convert.ToInt32(obj[9]);
                    oEncuesta.HoraLimiteEncuesta = System.Convert.ToString(obj[10]);
                    oEncuesta.IdTipoEncuesta = new TDI_TipoEncuesta { IdTipoEncuesta = System.Convert.ToInt32(obj[11]) };
                    listaEncuesta.Add(oEncuesta);
                }


            }
            catch (Exception ex)
            {
                listaEncuesta = null;
                return listaEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaEncuesta;
            #endregion           
        }

        public static Boolean GuardarEncuestas(THE_Encuesta enc)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_Encuesta>(enc);
        }

        public static Boolean GuardaIdEncEncriptada(TDI_EncEncrypt encu_encrypt)
        {
            return NHibernateHelperORACLE.SingleSessionSaveOrUpdate<TDI_EncEncrypt>(encu_encrypt);
        }

        public static IList<THE_Encuesta> ObtieneEncuestaPorEmpleado(int Empl_LLav_Pr)
        {
            List<THE_Encuesta> listaEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.empl_llav_pr empl, encuesta.encuesta_fechcrea fechcrea, ";
            strSQL += " encuesta.encuesta_fechlimite fechlimite, ";
            strSQL += " encuesta.encuesta_nombre nombreencu, encuesta.id_encuesta idencu, ";
            strSQL += " encuesta.id_estatus estatus, encuesta.encuesta_stat Stat ";
            strSQL += " FROM seml_the_encuesta encuesta ";
            strSQL += " WHERE encuesta.EMPL_LLAV_PR = " + Empl_LLav_Pr;
            strSQL += " AND encuesta.ENCUESTA_STAT = 'A'";
            strSQL += " ORDER BY encuesta.ID_ENCUESTA DESC ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("empl", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("fechcrea", NHibernateUtil.DateTime);//1
                consultaIQRY.AddScalar("fechlimite", NHibernateUtil.DateTime);//2
                consultaIQRY.AddScalar("nombreencu", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("idencu", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("estatus", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("Stat", NHibernateUtil.Character);//6

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Encuesta oEncuesta = new THE_Encuesta();
                    oEncuesta.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                    oEncuesta.FechaCreaEncuesta = System.Convert.ToDateTime(obj[1]);
                    oEncuesta.FechaLimiteEncuesta = Convert.ToDateTime(obj[2]).ToString("dd/MM/yyyy");
                    oEncuesta.NombreEncuesta = System.Convert.ToString(obj[3]);
                    oEncuesta.IdEncuesta = System.Convert.ToInt32(obj[4]);
                    oEncuesta.IdEstatus = new TDI_Estatus() { IdEstatus = System.Convert.ToInt32(obj[5]) };
                    oEncuesta.EncuestaStat = System.Convert.ToChar(obj[6]);
                    List<TDI_Estatus> nombEstatus = (List<TDI_Estatus>)MngDatosEstatus.ObtieneEstatusPorIDEncuesta(System.Convert.ToInt32(obj[5]));
                    oEncuesta.NombreEstatus = nombEstatus[0].EstatusDescripcion;
                    listaEncuesta.Add(oEncuesta);
                }


            }
            catch (Exception ex)
            {
                listaEncuesta = null;
                return listaEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaEncuesta;
        }

        public static Boolean EliminaEncuestas(THE_Encuesta encu)
        {
            return NHibernateHelperORACLE.SingleSessionSaveOrUpdate<THE_Encuesta>(encu);
        }

        public static Boolean ActualizaEncuesta(THE_Encuesta encu)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Encuesta>(encu);
        }
        
        public static int NotificacionEncuestaPendiente(int IdDispositivo, double NumeroTel)
        {
            GuardaLogTransacc("Conexión de dispositivo Android con el Web Service - No. Tel: " + NumeroTel.ToString(), 26, NumeroTel);
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();
            int Encuesta = -1;

                    strSQL += " SELECT enc.ID_ENCUESTA, enc.puntos_encuesta  ";//abraham agregue puntos enc.puntos_encuesta
                    strSQL += " FROM seml_the_encuesta enc, seml_tdi_encuestadispositivo encdis, seml_the_dispositivo disp   ";
                    strSQL += " WHERE encdis.ID_DISPOSITIVO = DISP.ID_DISPOSITIVO ";
                    strSQL += " AND ENC.ID_ENCUESTA =  ENCDIS.ID_ENCUESTA ";
                    strSQL += " AND    enc.ENCUESTA_STAT = 'A' ";
                    strSQL += " AND DISP.ID_DISPOSITIVO = " + IdDispositivo  + "  ";
                    strSQL += " AND ENCDIS.ID_ESTATUS=2 ";
                    strSQL += " AND ROWNUM =1 ";
                    strSQL += " ORDER BY enc.ENCUESTA_FECHCREA ASC ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);
                consultaIQRY.AddScalar("ID_ENCUESTA", NHibernateUtil.String);//0
                IList lista = consultaIQRY.List();

                if (lista.Count > 0)
                {
                    Encuesta = System.Convert.ToInt32(lista[0].ToString());
                }

            }
            catch (Exception ex)
            {
                return Encuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            GuardaLogTransacc("Metodo consumido desde Android: NotificacionEncuestaPendiente - No. Tel: " + NumeroTel.ToString(), 28, NumeroTel);
            return Encuesta;


        }

        public static IList<THE_Encuesta> ObtieneTodasEncuestasActivas()
        {
            List<THE_Encuesta> listaEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.empl_llav_pr empl, encuesta.encuesta_fechcrea fechcrea, ";
            strSQL += " encuesta.encuesta_fechlimite fechlimite, ";
            strSQL += " encuesta.encuesta_nombre nombreencu, encuesta.id_encuesta idencu, ";
            strSQL += " encuesta.id_estatus estatus, encuesta.encuesta_stat stat ";
            strSQL += " FROM seml_the_encuesta encuesta ";
            strSQL += " WHERE encuesta.id_estatus = 9 ";
            strSQL += " AND encuesta.encuesta_stat = 'A' ";
            //strSQL += " AND TO_DATE(ENCUESTA_FECHLIMITE,'DD/MM/YYYY') >= TRUNC(SYSDATE)";
            strSQL += " ORDER BY encuesta.id_encuesta DESC";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("empl", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("fechcrea", NHibernateUtil.DateTime);//1
                consultaIQRY.AddScalar("fechlimite", NHibernateUtil.DateTime);//2
                consultaIQRY.AddScalar("nombreencu", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("idencu", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("estatus", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("Stat", NHibernateUtil.Character);//6

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Encuesta oEncuesta = new THE_Encuesta();
                    oEncuesta.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                    oEncuesta.FechaCreaEncuesta = System.Convert.ToDateTime(obj[1]);
                    oEncuesta.FechaLimiteEncuesta = Convert.ToDateTime(obj[2]).ToString("dd/MM/yyyy");
                    oEncuesta.NombreEncuesta = System.Convert.ToString(obj[3]);
                    oEncuesta.IdEncuesta = System.Convert.ToInt32(obj[4]);
                    oEncuesta.IdEstatus = new TDI_Estatus() { IdEstatus = System.Convert.ToInt32(obj[5]) };
                    oEncuesta.EncuestaStat = System.Convert.ToChar(obj[6]);
                    List<TDI_Estatus> nombEstatus = (List<TDI_Estatus>)MngDatosEstatus.ObtieneEstatusPorIDEncuesta(System.Convert.ToInt32(obj[5]));
                    oEncuesta.NombreEstatus = nombEstatus[0].EstatusDescripcion;
                    listaEncuesta.Add(oEncuesta);
                }


            }
            catch (Exception ex)
            {
                listaEncuesta = null;
                return listaEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaEncuesta;
        }

        public static IList<THE_Encuesta> ObtieneEncuestasActivas()
        {
            List<THE_Encuesta> listaEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.empl_llav_pr empl, encuesta.encuesta_fechcrea fechcrea, ";
            strSQL += " encuesta.encuesta_fechlimite fechlimite, ";
            strSQL += " encuesta.encuesta_nombre nombreencu, encuesta.id_encuesta idencu, ";
            strSQL += " encuesta.id_estatus estatus, encuesta.encuesta_stat stat ";
            strSQL += " FROM seml_the_encuesta encuesta ";
            // strSQL += " WHERE encuesta.id_estatus = 9 ";
            strSQL += " WHERE encuesta.encuesta_stat = 'A' ";
            // strSQL += " AND TO_DATE(ENCUESTA_FECHLIMITE,'DD/MM/YYYY') >= TRUNC(SYSDATE)";
            strSQL += " ORDER BY encuesta.id_encuesta DESC ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("empl", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("fechcrea", NHibernateUtil.DateTime);//1
                consultaIQRY.AddScalar("fechlimite", NHibernateUtil.DateTime);//2
                consultaIQRY.AddScalar("nombreencu", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("idencu", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("estatus", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("Stat", NHibernateUtil.Character);//6

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Encuesta oEncuesta = new THE_Encuesta();
                    oEncuesta.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                    oEncuesta.FechaCreaEncuesta = System.Convert.ToDateTime(obj[1]);
                    oEncuesta.FechaLimiteEncuesta = Convert.ToDateTime(obj[2]).ToString("dd/MM/yyyy");
                    oEncuesta.NombreEncuesta = System.Convert.ToString(obj[3]);
                    oEncuesta.IdEncuesta = System.Convert.ToInt32(obj[4]);
                    oEncuesta.IdEstatus = new TDI_Estatus() { IdEstatus = System.Convert.ToInt32(obj[5]) };
                    oEncuesta.EncuestaStat = System.Convert.ToChar(obj[6]);
                    List<TDI_Estatus> nombEstatus = (List<TDI_Estatus>)MngDatosEstatus.ObtieneEstatusPorIDEncuesta(System.Convert.ToInt32(obj[5]));
                    oEncuesta.NombreEstatus = nombEstatus[0].EstatusDescripcion;
                    listaEncuesta.Add(oEncuesta);
                }


            }
            catch (Exception ex)
            {
                listaEncuesta = null;
                return listaEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaEncuesta;
        }


        public static IList<THE_Encuesta> BuscaEncuestaPorNombre(string NombreEncuesta, string FechIni, string FechFin, string TipoFecha)
        {
            List<THE_Encuesta> listaEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.empl_llav_pr encuempl, encuesta.encuesta_fechcrea fechcrea, ";
            strSQL += " encuesta.encuesta_fechlimite fechlimit, ";
            strSQL += " encuesta.encuesta_nombre encunombre, encuesta.encuesta_stat encustat, ";
            strSQL += " encuesta.id_encuesta encuid, encuesta.id_estatus encuestatus ";
            strSQL += " FROM seml_the_encuesta encuesta ";
            strSQL += " WHERE ";
            if(NombreEncuesta != string.Empty)
                strSQL += " UPPER(encuesta.encuesta_nombre) LIKE " + " UPPER('%" + NombreEncuesta + "%')" + " " + "AND";

            if (TipoFecha != "")
                if (TipoFecha == "radPorFechCre")
                {
                    if (FechIni != null)
                        strSQL += " encuesta.encuesta_fechcrea BETWEEN TO_DATE ('" + FechIni + "', 'DD/MM/YYYY') ";
                    if (FechFin != null)
                        strSQL += " AND TO_DATE ('" + FechFin + "', 'DD/MM/YYYY') ";
                }
                else
                {
                    if (FechIni != null)
                        strSQL += " encuesta.ENCUESTA_FECHLIMITE BETWEEN TO_DATE ('" + FechIni + "', 'DD/MM/YYYY') ";
                    if (FechFin != null)
                        strSQL += " AND TO_DATE ('" + FechFin + "', 'DD/MM/YYYY') ";
                }
            
            strSQL += " AND encuesta.ENCUESTA_STAT = 'A' ";
            strSQL += " AND encuesta.ID_ESTATUS = 9 ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("encuempl", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("fechcrea", NHibernateUtil.DateTime);//1
                consultaIQRY.AddScalar("fechlimit", NHibernateUtil.DateTime);//2
                consultaIQRY.AddScalar("encunombre", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("encuid", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("encuestatus", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("encustat", NHibernateUtil.Character);//6

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Encuesta oEncuesta = new THE_Encuesta();
                    oEncuesta.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                    oEncuesta.FechaCreaEncuesta = System.Convert.ToDateTime(obj[1]);
                    oEncuesta.FechaLimiteEncuesta = System.Convert.ToString(obj[2]);
                    oEncuesta.NombreEncuesta = System.Convert.ToString(obj[3]);
                    oEncuesta.IdEncuesta = System.Convert.ToInt32(obj[4]);
                    oEncuesta.EncuestaStat = System.Convert.ToChar(obj[6]);

                    listaEncuesta.Add(oEncuesta);
                }


            }
            catch (Exception ex)
            {
                listaEncuesta = null;
                return listaEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaEncuesta;

           
        }

        public static IList<THE_Encuesta> ObtieneDatosEncuestaPreview(int IdEncuesta)
        {
            #region Query Armado
            List<THE_Encuesta> lstEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.ID_ENCUESTA encuId, encuesta.ENCUESTA_NOMBRE encuNombre ";
            strSQL += " FROM seml_the_encuesta encuesta ";
            strSQL += " WHERE encuesta.id_encuesta = " + IdEncuesta;
            strSQL += " AND encuesta.encuesta_stat = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("encuId", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("encuNombre", NHibernateUtil.String);//1

                IList lista = consultaIQRY.List();

                List<THE_Preguntas> lstPregun = new List<THE_Preguntas>();
                List<THE_Respuestas> lstRespu = new List<THE_Respuestas>();
                THE_Encuesta ItemEncuesta = new THE_Encuesta();
                foreach (Object[] obj in lista)
                {
                    foreach (THE_Preguntas oPreg in MngDatosPreguntas.ObtienePreguntasPorEncuesta(System.Convert.ToInt32(obj[0])))
                    {
                        lstRespu = new List<THE_Respuestas>();
                        foreach (THE_Respuestas oResp in MngDatosRespuestas.ObtenerRespuestasPorPregunta(oPreg.IdPregunta))
                        {
                            lstRespu.Add(oResp);
                        }
                        oPreg.ListaRespuestas = lstRespu;
                        lstPregun.Add(oPreg);
                        ItemEncuesta.LstPreg = lstPregun;
                    }
                }

                lstEncuesta.Add(ItemEncuesta);

            }
            catch (Exception ex)
            {
                lstEncuesta = null;
                return lstEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstEncuesta;
            #endregion
        }

        public static IList<THE_Encuesta> BuscaEncuestaPreguntasRespuestas(string NombreEncuesta, string FechIni, string FechFin, string TipoFecha)
        {
            List<THE_Encuesta> listaEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT encuesta.empl_llav_pr encuempl, ";
            strSQL += " encuesta.encuesta_fechcrea fechcrea, encuesta.encuesta_fechlimite fechlimit, encuesta.encuesta_horalimite horlimit, ";
            strSQL += " encuesta.encuesta_nombre encunombre, encuesta.encuesta_stat encustat, ";
            strSQL += " encuesta.id_encuesta encuid, encuesta.id_estatus encuestatus, encuesta.puntos_encuesta puntos, encuesta.minrequerido minreq, encuesta.maxesperado maxesp, encuesta.id_tipoencuesta tipoenc, tipoencuesta.tipo_descripcion desctipoenc ";
            strSQL += " FROM seml_the_encuesta encuesta, seml_the_preguntas preguntas, seml_the_respuestas respuestas, seml_tdi_tipoencuesta tipoencuesta ";
            strSQL += " WHERE ENCUESTA.ID_ENCUESTA= PREGUNTAS.ID_PREGUNTA (+)";
            strSQL += " AND preguntas.id_pregunta = respuestas.id_pregunta (+)";

            if(TipoFecha != "")
                if (TipoFecha == "radPorFechCre")
                {
                    if (FechIni != "")
                        strSQL += " AND TRUNC(encuesta.encuesta_fechcrea) BETWEEN TO_DATE ('" + FechIni + "', 'DD/MM/YYYY') ";
                    if (FechFin != "")
                        strSQL += " AND TO_DATE ('" + FechFin + "', 'DD/MM/YYYY') ";
                }
                else
                {
                    if (FechIni != "")
                        strSQL += " AND TO_DATE(encuesta.ENCUESTA_FECHLIMITE) BETWEEN TO_DATE ('" + FechIni + "', 'DD/MM/YYYY') ";
                    if (FechFin != "")
                        strSQL += " AND TO_DATE ('" + FechFin + "', 'DD/MM/YYYY') ";
                }
            
            strSQL += " AND preguntas.preg_estatus (+)= 'A' ";
            strSQL += " AND respuestas.resp_estatus (+)= 'A' ";
            strSQL += " AND encuesta.encuesta_stat = 'A' ";

            strSQL += " AND encuesta.id_tipoencuesta = tipoencuesta.id_tipoencuesta ";

            
            if (NombreEncuesta != "")
            {
                strSQL += " AND (UPPER(encuesta.encuesta_nombre) LIKE " + " UPPER('%" + NombreEncuesta + "%')" + " ";
                strSQL += " OR UPPER(preguntas.pregunta_desc) LIKE " + " UPPER('%" + NombreEncuesta + "%')" + " ";
                strSQL += " OR UPPER(respuestas.RESPUESTA_DESC) LIKE " + " UPPER('%" + NombreEncuesta + "%'))" + " ";
            }

            strSQL += " ORDER BY encuesta.ID_ENCUESTA DESC ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("encuempl", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("fechcrea", NHibernateUtil.DateTime);//1
                consultaIQRY.AddScalar("fechlimit", NHibernateUtil.DateTime);//2
                consultaIQRY.AddScalar("horlimit", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("encunombre", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("encuid", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("encuestatus", NHibernateUtil.Int32);//6
                consultaIQRY.AddScalar("encustat", NHibernateUtil.Character);//7 
                consultaIQRY.AddScalar("puntos", NHibernateUtil.Int32);//8
                consultaIQRY.AddScalar("minreq", NHibernateUtil.Int32);//9
                consultaIQRY.AddScalar("maxesp", NHibernateUtil.Int32);//10
                consultaIQRY.AddScalar("tipoenc", NHibernateUtil.Int32);//11
                consultaIQRY.AddScalar("desctipoenc", NHibernateUtil.String);//12

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Encuesta oEncuesta = new THE_Encuesta();
                    oEncuesta.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                    oEncuesta.FechaCreaEncuesta = System.Convert.ToDateTime(obj[1]);
                    oEncuesta.FechaLimiteEncuesta = System.Convert.ToDateTime(obj[2]).ToString("dd/MM/yyyy");
                    oEncuesta.HoraLimiteEncuesta = System.Convert.ToString(obj[3]);
                    oEncuesta.NombreEncuesta = System.Convert.ToString(obj[4]);
                    oEncuesta.IdEncuesta = System.Convert.ToInt32(obj[5]);
                    List<TDI_Estatus> nombEstatus = (List<TDI_Estatus>)MngDatosEstatus.ObtieneEstatusPorIDEncuesta(System.Convert.ToInt32(obj[6]));
                    oEncuesta.NombreEstatus = nombEstatus[0].EstatusDescripcion;
                    oEncuesta.IdEstatus = new TDI_Estatus { IdEstatus = System.Convert.ToInt32(obj[6]) };
                    oEncuesta.EncuestaStat = System.Convert.ToChar(obj[7]);
                    oEncuesta.PuntosEncuesta = System.Convert.ToInt32(obj[8]);
                    oEncuesta.MinimoRequerido = System.Convert.ToInt32(obj[9]);
                    oEncuesta.MaximoEsperado = System.Convert.ToInt32(obj[10]);
                    oEncuesta.IdTipoEncuesta = new TDI_TipoEncuesta() { IdTipoEncuesta = System.Convert.ToInt32(obj[11]) };
                    oEncuesta.IdTipoEnc = System.Convert.ToInt32(obj[11]);
                    oEncuesta.DescIdTipoEnc = System.Convert.ToString(obj[12]);
                    listaEncuesta.Add(oEncuesta);
                }


            }
            catch (Exception ex)
            {
                listaEncuesta = null;
                return listaEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaEncuesta;

        }

        public static IList<THE_Encuesta> ObtieneTodasEncuestasMostrar()
        {
            List<THE_Encuesta> listaEncuesta = new List<THE_Encuesta>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.empl_llav_pr empl, encuesta.encuesta_fechcrea fechcrea, ";
            strSQL += " encuesta.encuesta_fechlimite fechlimite, ";
            strSQL += " encuesta.encuesta_nombre nombreencu, encuesta.id_encuesta idencu, ";
            strSQL += " encuesta.id_estatus estatus, encuesta.encuesta_stat stat ";
            strSQL += " FROM seml_the_encuesta encuesta ";
            strSQL += " WHERE encuesta.id_estatus = 8 ";
            strSQL += " OR encuesta.id_estatus = 7 AND encuesta.encuesta_stat = 'A' ";
            strSQL += " ORDER BY encuesta.id_encuesta DESC ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("empl", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("fechcrea", NHibernateUtil.DateTime);//1
                consultaIQRY.AddScalar("fechlimite", NHibernateUtil.DateTime);//2
                consultaIQRY.AddScalar("nombreencu", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("idencu", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("estatus", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("Stat", NHibernateUtil.Character);//6

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Encuesta oEncuesta = new THE_Encuesta();
                    oEncuesta.EmpleadoLlavePrimaria = new THE_Empleado() { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                    oEncuesta.FechaCreaEncuesta = System.Convert.ToDateTime(obj[1]);
                    oEncuesta.FechaLimiteEncuesta = Convert.ToDateTime(obj[2]).ToString("dd/MM/yyyy");
                    oEncuesta.NombreEncuesta = System.Convert.ToString(obj[3]);
                    oEncuesta.IdEncuesta = System.Convert.ToInt32(obj[4]);
                    oEncuesta.IdEstatus = new TDI_Estatus() { IdEstatus = System.Convert.ToInt32(obj[5]) };
                    oEncuesta.EncuestaStat = System.Convert.ToChar(obj[6]);
                    List<TDI_Estatus> nombEstatus = (List<TDI_Estatus>)MngDatosEstatus.ObtieneEstatusPorIDEncuesta(System.Convert.ToInt32(obj[5]));
                    oEncuesta.NombreEstatus = nombEstatus[0].EstatusDescripcion;
                    listaEncuesta.Add(oEncuesta);
                }
            }
            catch (Exception ex)
            {
                listaEncuesta = null;
                return listaEncuesta;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaEncuesta;
        }

        public static IList<TDI_EncuestaDispositivo> ObtieneEncuestaDispositivoPorIdEncuesta(int IdEncuesta)
        {
            #region Query Armado
            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encuesta.id_encuesta idencuesta, ";
            strSQL += " encuesta.encuesta_nombre nombreencuesta ";
            strSQL += " FROM seml_the_encuesta encuesta ";
            strSQL += " WHERE encuesta.id_encuesta = " + IdEncuesta;

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("IDEncuesta", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("NombreEncuesta", NHibernateUtil.String);//1

                IList lista = consultaIQRY.List();
                TDI_EncuestaDispositivo EncuDispo = new TDI_EncuestaDispositivo();
                List<THE_Preguntas> lstPregun = new List<THE_Preguntas>();
                List<THE_Respuestas> lstRespu = new List<THE_Respuestas>();
                EncuDispo.ListaEncuesta = new List<THE_Encuesta>();
                foreach (Object[] obj in lista)
                {
                    foreach (THE_Encuesta consulta in MngDatosEncuesta.ObtieneEncuestaPorID(Convert.ToInt32(obj[0])))
                    {
                        foreach (THE_Preguntas oPreg in MngDatosPreguntas.ObtienePreguntasPorEncuesta(consulta.IdEncuesta))
                        {
                            lstRespu = new List<THE_Respuestas>();
                            foreach (THE_Respuestas oResp in MngDatosRespuestas.ObtenerRespuestasPorPregunta(oPreg.IdPregunta))
                            {
                                lstRespu.Add(oResp);
                            }
                            oPreg.ListaRespuestas = lstRespu;
                            lstPregun.Add(oPreg);
                            consulta.LstPreg = lstPregun;
                        }

                        EncuDispo.ListaEncuesta.Add(consulta);
                        EncuDispo.IdDispositivo = new THE_Dispositivo();
                        EncuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(obj[0].ToString()) };
                        EncuDispo.IdEstatus = new TDI_Estatus() { IdEstatus = 3 };
                        MngDatosEncuestaDispositivo.ActualizaEstatusDispoEncu(EncuDispo);
                    }

                }
                lstEncuestasDispo.Add(EncuDispo);

            }
            catch (Exception ex)
            {
                lstEncuestasDispo = null;
                return lstEncuestasDispo;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstEncuestasDispo;
            #endregion

        }

        public static void GuardaLogTransacc(string Desc, int IdTran, double NumTel)
        {
            THE_LogTran oLogTran = new THE_LogTran();
            oLogTran.LogtDesc = Desc;
            oLogTran.LogtDomi = "Android";
            oLogTran.LogtFech = DateTime.Now;
            oLogTran.LogtMach = "Android";
            oLogTran.LogtUsIp = NumTel.ToString();
            oLogTran.LogtUsua = "Android";
            oLogTran.TranLlavPr = new TDI_Transacc() { TranLlavPr = IdTran };
            MngDatosTransacciones.GuardaLogTransaccion(oLogTran);
        }
    }
}
