using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosProgramacion
    {
        public static Boolean GuardaProgramacionPorEncuesta(THE_Programacion programacion)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_Programacion>(programacion);
        }

        public static IList<THE_Programacion> ObtieneProgramacionesPorEncuesta(int idEncuesta)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Programacion Programacion WHERE ID_ENCUESTA = " + idEncuesta + " AND PROGRAMACION_ESTATUS = 'A' ORDER BY ID_PROGRAMACION ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Programacion>(strQuery);
            }
            catch (Exception ex)
            {
                return new List<THE_Programacion>();
            }
        }

        public static IList<THE_Programacion> ObtieneProgramaciones(string idEncuesta)
        {
            #region Query Armado
            List<THE_Programacion> lstProgramaciones = new List<THE_Programacion>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXFECHASEMANA.ID_PROGXFECHA IDPROGXFECHASEMANA, ";
            strSQL += " ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA , ";
            strSQL += " TIPO_PRO.TIPOPROGRAMACION_DESC, ";
            strSQL += " TIPO_PRO.ID_TIPOPROGRAMACION, PROXFECHASEMANA.HORA, TO_CHAR(PROXFECHASEMANA.FECHA) FECHA, PRO.PROGRAMACION_ESTATUS ESTATUS ";   
            strSQL += " FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXFECHA PROXFECHASEMANA, SEML_THE_ENCUESTA ENC, ";
            strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO ";
            strSQL += " WHERE PRO.ID_PROGRAMACION=PROXFECHASEMANA.ID_PROGRAMACION ";
            strSQL += " AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += " AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION AND PRO.PROGRAMACION_ESTATUS='A' ";
            strSQL += " AND PROXFECHASEMANA.ESTATUS='A' ";
            strSQL += " AND TIPO_PRO.PROGRAMACION_ESTATUS='A' ";
            if (idEncuesta != "") { strSQL += " AND ENC.ID_ENCUESTA=" + idEncuesta.ToString() + " "; }
            strSQL += " UNION ";
            strSQL += " SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXFECHASEMANA.ID_PROGXSEMANA IDPROGXFECHASEMANA , ";
            strSQL += " ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA , ";
            strSQL += " TIPO_PRO.TIPOPROGRAMACION_DESC, ";
            strSQL += " TIPO_PRO.ID_TIPOPROGRAMACION, PROXFECHASEMANA.HORA, PROXFECHASEMANA.DIA FECHA, PRO.PROGRAMACION_ESTATUS ESTATUS  ";  
            strSQL += " FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXSEMANA PROXFECHASEMANA, SEML_THE_ENCUESTA ENC, ";
            strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO ";
            strSQL += " WHERE PRO.ID_PROGRAMACION=PROXFECHASEMANA.ID_PROGRAMACION ";
            strSQL += " AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += " AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION  AND PRO.PROGRAMACION_ESTATUS='A' ";
            strSQL += " AND PROXFECHASEMANA.ESTATUS='A' ";
            strSQL += " AND TIPO_PRO.PROGRAMACION_ESTATUS='A' ";
            if (idEncuesta != "") { strSQL += " AND ENC.ID_ENCUESTA=" + idEncuesta.ToString() + " "; }

            strSQL += "  order by 1 desc  ";


            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_PROGRAMACION", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PROGRAMACION_NOMBRE", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("IDPROGXFECHASEMANA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("ENCUESTA_NOMBRE", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("ID_ENCUESTA", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("TIPOPROGRAMACION_DESC", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("ID_TIPOPROGRAMACION", NHibernateUtil.Int32);//6
                consultaIQRY.AddScalar("HORA", NHibernateUtil.String);//7
                consultaIQRY.AddScalar("FECHA", NHibernateUtil.String);//8
                consultaIQRY.AddScalar("ESTATUS", NHibernateUtil.Character);//9
                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Programacion oProgra = new THE_Programacion();
                    oProgra.IdProgramacion = System.Convert.ToInt32(obj[0]);
                    oProgra.ProgramacionNombre = System.Convert.ToString(obj[1]);
                    oProgra.IDPROFECHASEMANA = System.Convert.ToInt32(obj[2]);
                    oProgra.ENCUESTANOMBRE = System.Convert.ToString(obj[3]);
                    oProgra.IDENC = System.Convert.ToInt32(obj[4]);
                    oProgra.DESCTIPOPROGRAMACION = System.Convert.ToString(obj[5]);
                    oProgra.IdTipoProgramacion = new TDI_TipoProgramacion() { IdTipoProgramacion = System.Convert.ToInt32(obj[6]) };
                    oProgra.IDTIPOPROGRA = System.Convert.ToInt32(obj[6]);
                    oProgra.HORA = System.Convert.ToString(obj[7]);
                    oProgra.FECHA = System.Convert.ToString(obj[8]);
                    oProgra.ProgramacionEstatus = System.Convert.ToChar(obj[9]);
                    lstProgramaciones.Add(oProgra);
                }

            }
            catch (Exception ex)
            {

                lstProgramaciones = null;
                return lstProgramaciones;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstProgramaciones;
            #endregion
        }

        public static IList<THE_Programacion> ObtieneProgramacionesbyEncuesta(string idEncuesta)
        {
            #region Query Armado
            List<THE_Programacion> lstProgramaciones = new List<THE_Programacion>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

         
            strSQL += "  SELECT PRO.ID_PROGRAMACION,  ";
            strSQL += "  PRO.PROGRAMACION_NOMBRE,  ";
            strSQL += "  ENC.ENCUESTA_NOMBRE,  ";
            strSQL += "  ENC.ID_ENCUESTA,  ";
            strSQL += "  TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            strSQL += "  TIPO_PRO.ID_TIPOPROGRAMACION,  ";
            strSQL += "  PRO.PROGRAMACION_ESTATUS ESTATUS  ";
            strSQL += "  FROM SEML_THE_PROGRAMACION PRO,  ";
            strSQL += "  SEML_THE_ENCUESTA ENC,  ";
            strSQL += "  SEML_TDI_TIPOPROGRAMACION TIPO_PRO  ";
            strSQL += "  WHERE     PRO.ID_ENCUESTA = ENC.ID_ENCUESTA  ";
            strSQL += "  AND PRO.ID_TIPOPROGRAMACION = TIPO_PRO.ID_TIPOPROGRAMACION  ";
            strSQL += "  AND PRO.PROGRAMACION_ESTATUS = 'A'  ";
            strSQL += "  AND TIPO_PRO.PROGRAMACION_ESTATUS = 'A'  ";
            if (idEncuesta != "") { strSQL += " AND ENC.ID_ENCUESTA=" + idEncuesta.ToString() + " "; }
            strSQL += "  ORDER BY  PRO.ID_PROGRAMACION DESC  ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_PROGRAMACION", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PROGRAMACION_NOMBRE", NHibernateUtil.String);//1              
                consultaIQRY.AddScalar("ENCUESTA_NOMBRE", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("ID_ENCUESTA", NHibernateUtil.Int32);//3
                consultaIQRY.AddScalar("TIPOPROGRAMACION_DESC", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("ID_TIPOPROGRAMACION", NHibernateUtil.Int32);//5            
                consultaIQRY.AddScalar("ESTATUS", NHibernateUtil.Character);//6
                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Programacion oProgra = new THE_Programacion();
                    oProgra.IdProgramacion = System.Convert.ToInt32(obj[0]);
                    oProgra.ProgramacionNombre = System.Convert.ToString(obj[1]);                    
                    oProgra.ENCUESTANOMBRE = System.Convert.ToString(obj[2]);
                    oProgra.IDENC = System.Convert.ToInt32(obj[3]);
                    oProgra.DESCTIPOPROGRAMACION = System.Convert.ToString(obj[4]);
                    oProgra.IDTIPOPROGRA = System.Convert.ToInt32(obj[5]);                   
                    oProgra.ProgramacionEstatus = System.Convert.ToChar(obj[6]);
                    lstProgramaciones.Add(oProgra);
                }

            }
            catch (Exception ex)
            {

                lstProgramaciones = null;
                return lstProgramaciones;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstProgramaciones;
            #endregion
        }

        public static IList<THE_PrograDispositivo> ObtieneDispProgramados(string idProgramacion)
        {

            #region Query Armado

            List<THE_PrograDispositivo> lstPrograDispo = new List<THE_PrograDispositivo>();
            string strSQL = string.Empty;
            int IdDispositivo = 0;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();



            strSQL += " SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXFECHA.ID_PROGXFECHA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            strSQL += "                     TIPO_PRO.ID_TIPOPROGRAMACION, ";
            strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXFECHA PROXFECHA, SEML_THE_ENCUESTA ENC, ";
            strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXFECHA.ID_PROGRAMACION ";
            strSQL += "        AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            strSQL += "        AND PRO_DISPO.ESTATUS='A' ";
            if (idProgramacion != "") { strSQL += "        AND PRO.ID_PROGRAMACION=" + idProgramacion + " "; }
            strSQL += "        UNION ";
            strSQL += "        SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXSEMANA.ID_PROGXSEMANA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            strSQL += "                    TIPO_PRO.ID_TIPOPROGRAMACION, ";
            strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXSEMANA PROXSEMANA, SEML_THE_ENCUESTA ENC, ";
            strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXSEMANA.ID_PROGRAMACION ";
            strSQL += "       AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            strSQL += "        AND PRO_DISPO.ESTATUS='A' ";
            if (idProgramacion != "") { strSQL += "        AND PRO.ID_PROGRAMACION=" + idProgramacion + " "; }

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);


                consultaIQRY.AddScalar("ID_PROGRAMACION", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PROGRAMACION_NOMBRE", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_PROGRA_FECHA_SEMANA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("ID_PRO_DISPO", NHibernateUtil.Int32);//3
                consultaIQRY.AddScalar("ENCUESTA_NOMBRE", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("ID_ENCUESTA", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("TIPOPROGRAMACION_DESC", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("ID_TIPOPROGRAMACION", NHibernateUtil.Int32);//7
                consultaIQRY.AddScalar("DISPO_DESCRIPCION", NHibernateUtil.String);//8
                consultaIQRY.AddScalar("ID_DISPOSITIVO", NHibernateUtil.Int32);//9

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_PrograDispositivo objPrograDispo = new THE_PrograDispositivo();

                    objPrograDispo.ID_PROGRA = Convert.ToInt32(obj[0].ToString());
                    objPrograDispo.PROGRAMACION_NOMBRE = (obj[1].ToString());
                    objPrograDispo.ID_PROGXFECHASEMANA = Convert.ToInt32(obj[2].ToString());
                    objPrograDispo.ID_PRO_DISPO = Convert.ToInt32(obj[3].ToString());
                    objPrograDispo.ENCUESTA_NOMBRE = (obj[4].ToString());
                    objPrograDispo.ID_ENC = Convert.ToInt32(obj[5].ToString());
                    objPrograDispo.TIPOPROGRAMACION_DESC = (obj[6].ToString());
                    objPrograDispo.ID_TIP_PROGRA = Convert.ToInt32(obj[7].ToString());
                    objPrograDispo.DISPO_DESCRIPCION = (obj[8].ToString());
                    objPrograDispo.ID_DISPO = Convert.ToInt32(obj[9].ToString());


                    lstPrograDispo.Add(objPrograDispo);
                }

            }
            catch
            {
                lstPrograDispo = null;
                return lstPrograDispo;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            return lstPrograDispo;
            #endregion
        }


        public static IList<THE_PrograDispositivo> ObtieneDispositivosProgramados()
        {

            #region Query Armado

            List<THE_PrograDispositivo> lstPrograDispo = new List<THE_PrograDispositivo>();
            string strSQL = string.Empty;
            int IdDispositivo = 0;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();



            //strSQL += " SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXFECHA.ID_PROGXFECHA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            //strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            //strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            //strSQL += "                     TIPO_PRO.ID_TIPOPROGRAMACION, ";
            //strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            //strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXFECHA PROXFECHA, SEML_THE_ENCUESTA ENC, ";
            //strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            //strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXFECHA.ID_PROGRAMACION ";
            //strSQL += "        AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            //strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            //strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            //strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            //strSQL += "        AND PRO_DISPO.ESTATUS='A' ";
            //if (idProgramacion != "") { strSQL += "        AND PRO.ID_PROGRAMACION=" + idProgramacion + " "; }
            //strSQL += "        UNION ";
            //strSQL += "        SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXSEMANA.ID_PROGXSEMANA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            //strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            //strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            //strSQL += "                    TIPO_PRO.ID_TIPOPROGRAMACION, ";
            //strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            //strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXSEMANA PROXSEMANA, SEML_THE_ENCUESTA ENC, ";
            //strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            //strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXSEMANA.ID_PROGRAMACION ";
            //strSQL += "       AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            //strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            //strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            //strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            //strSQL += "        AND PRO_DISPO.ESTATUS='A' ";
            //if (idProgramacion != "") { strSQL += "        AND PRO.ID_PROGRAMACION=" + idProgramacion + " "; }


strSQL += " SELECT  PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE,  ";
strSQL += " PROXFECHA.ID_PROGXFECHA ID_PROGRA_FECHA_SEMANA, ";
strSQL += " PRO_DISPO.ID_PRO_DISPO, ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,   ";
strSQL += " TIPO_PRO.TIPOPROGRAMACION_DESC, TIPO_PRO.ID_TIPOPROGRAMACION,  ";
strSQL += " DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO, NULL ESTATUS_E ";
strSQL += " FROM    SEML_THE_PROGRAMACION PRO,  ";
strSQL += " SEML_THE_PROGXFECHA PROXFECHA,  ";
strSQL += " SEML_THE_ENCUESTA ENC,  ";
strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO,  ";
strSQL += " SEML_TDI_PROGRAMACION_DISPO PRO_DISPO,  ";
strSQL += " SEML_THE_DISPOSITIVO DISPO  ";
strSQL += " WHERE   PRO.ID_PROGRAMACION     =   PROXFECHA.ID_PROGRAMACION       AND  ";
strSQL += " PRO.ID_ENCUESTA         =   ENC.ID_ENCUESTA                 AND  ";
strSQL += " PRO.ID_TIPOPROGRAMACION =   TIPO_PRO.ID_TIPOPROGRAMACION    AND  ";
strSQL += " PRO.ID_PROGRAMACION     =   PRO_DISPO.ID_PROGRAMACION       AND  ";
strSQL += " DISPO.ID_DISPOSITIVO    =   PRO_DISPO.ID_DISPOSITIVO        AND  ";
strSQL += " PRO_DISPO.ESTATUS       =   'A'   AND  ";
strSQL += " DISPO.DISPO_ESTATUS='A' AND ";
strSQL += " TIPO_PRO.PROGRAMACION_ESTATUS='A'    AND ";
strSQL += " ENC.ENCUESTA_STAT='A' AND ";
strSQL += " PRO.PROGRAMACION_ESTATUS='A' AND ";
strSQL += " PROXFECHA.ESTATUS='A' ";                 
strSQL += " MINUS ";
strSQL += " SELECT  PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE,  ";
strSQL += " PROXFECHA.ID_PROGXFECHA ID_PROGRA_FECHA_SEMANA, ";
strSQL += " PRO_DISPO.ID_PRO_DISPO, ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  "; 
strSQL += " TIPO_PRO.TIPOPROGRAMACION_DESC, TIPO_PRO.ID_TIPOPROGRAMACION,  ";
strSQL += " DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO, NULL ESTATUS_E ";
strSQL += " FROM    SEML_THE_PROGRAMACION PRO,  ";
strSQL += " SEML_THE_PROGXFECHA PROXFECHA,  ";
strSQL += " SEML_THE_ENCUESTA ENC,  ";
strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO,  ";
strSQL += " SEML_TDI_PROGRAMACION_DISPO PRO_DISPO,  ";
strSQL += " SEML_THE_DISPOSITIVO DISPO, ";
strSQL += " SEML_TDI_ENCUESTADISPOSITIVO ED  ";
strSQL += " WHERE   PRO.ID_PROGRAMACION         =   PROXFECHA.ID_PROGRAMACION       AND  ";
strSQL += " PRO.ID_ENCUESTA             =   ENC.ID_ENCUESTA                 AND  ";
strSQL += " PRO.ID_TIPOPROGRAMACION     =   TIPO_PRO.ID_TIPOPROGRAMACION    AND  ";
strSQL += " PRO.ID_PROGRAMACION         =   PRO_DISPO.ID_PROGRAMACION       AND  ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   DISPO.ID_DISPOSITIVO            AND  ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   ED.ID_DISPOSITIVO               AND  ";
strSQL += " PRO.ID_ENCUESTA             =   ED.ID_ENCUESTA AND  ";
strSQL += " PRO_DISPO.ESTATUS           =   'A'                             AND  ";
strSQL += " DISPO.DISPO_ESTATUS='A' AND ";
strSQL += " TIPO_PRO.PROGRAMACION_ESTATUS='A'    AND ";
strSQL += " ENC.ENCUESTA_STAT='A' AND ";
strSQL += " PRO.PROGRAMACION_ESTATUS='A' AND ";
strSQL += " PROXFECHA.ESTATUS='A' ";       
strSQL += " UNION ";
strSQL += " SELECT  PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE,  ";
strSQL += " PROXFECHA.ID_PROGXFECHA ID_PROGRA_FECHA_SEMANA, ";
strSQL += " PRO_DISPO.ID_PRO_DISPO, ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,   ";
strSQL += " TIPO_PRO.TIPOPROGRAMACION_DESC, TIPO_PRO.ID_TIPOPROGRAMACION,  ";
strSQL += " DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO, ED.ID_ESTATUS ESTATUS_E ";
strSQL += " FROM    SEML_THE_PROGRAMACION PRO,  ";
strSQL += " SEML_THE_PROGXFECHA PROXFECHA,  ";
strSQL += " SEML_THE_ENCUESTA ENC,  ";
strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO,  ";
strSQL += " SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, "; 
strSQL += " SEML_THE_DISPOSITIVO DISPO, ";
strSQL += " SEML_TDI_ENCUESTADISPOSITIVO ED  ";
strSQL += " WHERE   PRO.ID_PROGRAMACION         =   PROXFECHA.ID_PROGRAMACION       AND  ";
strSQL += " PRO.ID_ENCUESTA             =   ENC.ID_ENCUESTA                 AND  ";
strSQL += " PRO.ID_TIPOPROGRAMACION     =   TIPO_PRO.ID_TIPOPROGRAMACION    AND  ";
strSQL += " PRO.ID_PROGRAMACION         =   PRO_DISPO.ID_PROGRAMACION       AND  ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   DISPO.ID_DISPOSITIVO            AND  ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   ED.ID_DISPOSITIVO               AND  ";
strSQL += " PRO.ID_ENCUESTA             =   ED.ID_ENCUESTA  AND ";
strSQL += " PRO_DISPO.ESTATUS           =   'A'                             AND   ";       
strSQL += " DISPO.DISPO_ESTATUS='A' AND ";
strSQL += " TIPO_PRO.PROGRAMACION_ESTATUS='A'    AND ";
strSQL += " ENC.ENCUESTA_STAT='A' AND ";
strSQL += " PRO.PROGRAMACION_ESTATUS='A' AND ";
strSQL += " PROXFECHA.ESTATUS='A' ";
strSQL += " UNION   ";
strSQL += " SELECT  PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE,  ";
strSQL += " PROXSEMANA.ID_PROGXSEMANA ID_PROGRA_FECHA_SEMANA, ";
strSQL += " PRO_DISPO.ID_PRO_DISPO,ENC.ENCUESTA_NOMBRE,  ";
strSQL += " ENC.ID_ENCUESTA ,TIPO_PRO.TIPOPROGRAMACION_DESC,   ";
strSQL += " TIPO_PRO.ID_TIPOPROGRAMACION,DISPO.DISPO_DESCRIPCION, ";
strSQL += " DISPO.ID_DISPOSITIVO, NULL ESTATUS_E  ";
strSQL += " FROM    SEML_THE_PROGRAMACION PRO,  ";
strSQL += " SEML_THE_PROGXSEMANA PROXSEMANA,  ";
strSQL += " SEML_THE_ENCUESTA ENC,  ";
strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO,  ";
strSQL += " SEML_TDI_PROGRAMACION_DISPO PRO_DISPO,  ";
strSQL += " SEML_THE_DISPOSITIVO DISPO  ";
strSQL += " WHERE   PRO.ID_PROGRAMACION     =   PROXSEMANA.ID_PROGRAMACION          AND  ";
strSQL += " PRO.ID_ENCUESTA         =   ENC.ID_ENCUESTA                     AND ";
strSQL += " PRO.ID_TIPOPROGRAMACION =   TIPO_PRO.ID_TIPOPROGRAMACION        AND ";
strSQL += " PRO.ID_PROGRAMACION     =   PRO_DISPO.ID_PROGRAMACION           AND ";
strSQL += " DISPO.ID_DISPOSITIVO    =   PRO_DISPO.ID_DISPOSITIVO            AND ";
strSQL += " PRO_DISPO.ESTATUS       =   'A'  AND ";
strSQL += " DISPO.DISPO_ESTATUS='A' AND ";
strSQL += " TIPO_PRO.PROGRAMACION_ESTATUS='A'    AND ";
strSQL += " ENC.ENCUESTA_STAT='A' AND ";
strSQL += " PRO.PROGRAMACION_ESTATUS='A' AND ";
strSQL += " PROXSEMANA.ESTATUS='A'  ";       
strSQL += " MINUS ";
strSQL += " SELECT  PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE,  ";
strSQL += " PROXSEMANA.ID_PROGXSEMANA ID_PROGRA_FECHA_SEMANA, ";
strSQL += " PRO_DISPO.ID_PRO_DISPO,ENC.ENCUESTA_NOMBRE,  ";
strSQL += " ENC.ID_ENCUESTA ,TIPO_PRO.TIPOPROGRAMACION_DESC, ";  
strSQL += " TIPO_PRO.ID_TIPOPROGRAMACION, DISPO.DISPO_DESCRIPCION,  ";
strSQL += " DISPO.ID_DISPOSITIVO, NULL ESTATUS_E  ";
strSQL += " FROM    SEML_THE_PROGRAMACION PRO,  ";
strSQL += " SEML_THE_PROGXSEMANA PROXSEMANA,  ";
strSQL += " SEML_THE_ENCUESTA ENC,  ";
strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO,  ";
strSQL += " SEML_TDI_PROGRAMACION_DISPO PRO_DISPO,  ";
strSQL += " SEML_THE_DISPOSITIVO DISPO, ";
strSQL += " SEML_TDI_ENCUESTADISPOSITIVO ED  ";
strSQL += " WHERE   PRO.ID_PROGRAMACION         =   PROXSEMANA.ID_PROGRAMACION      AND ";
strSQL += " PRO.ID_ENCUESTA             =   ENC.ID_ENCUESTA                 AND ";
strSQL += " PRO.ID_TIPOPROGRAMACION     =   TIPO_PRO.ID_TIPOPROGRAMACION    AND ";
strSQL += " PRO.ID_PROGRAMACION         =   PRO_DISPO.ID_PROGRAMACION       AND ";
strSQL += " PRO_DISPO.ESTATUS           =   'A'                             AND ";
strSQL += " DISPO.DISPO_ESTATUS='A' AND ";
strSQL += " TIPO_PRO.PROGRAMACION_ESTATUS='A'    AND ";
strSQL += " ENC.ENCUESTA_STAT='A' AND ";
strSQL += " PRO.PROGRAMACION_ESTATUS='A' AND ";
strSQL += " PROXSEMANA.ESTATUS='A' AND ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   DISPO.ID_DISPOSITIVO            AND ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   ED.ID_DISPOSITIVO               AND ";
strSQL += " PRO.ID_ENCUESTA             =   ED.ID_ENCUESTA ";                  
strSQL += " UNION ";
strSQL += " SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE,  ";
strSQL += " PROXSEMANA.ID_PROGXSEMANA ID_PROGRA_FECHA_SEMANA, ";
strSQL += " PRO_DISPO.ID_PRO_DISPO,ENC.ENCUESTA_NOMBRE,  ";
strSQL += " ENC.ID_ENCUESTA ,TIPO_PRO.TIPOPROGRAMACION_DESC,   ";
strSQL += " TIPO_PRO.ID_TIPOPROGRAMACION, DISPO.DISPO_DESCRIPCION,  ";
strSQL += " DISPO.ID_DISPOSITIVO, ED.ID_ESTATUS ESTATUS_E  ";
strSQL += " FROM   SEML_THE_PROGRAMACION PRO,  ";
strSQL += " SEML_THE_PROGXSEMANA PROXSEMANA,  ";
strSQL += " SEML_THE_ENCUESTA ENC,  ";
strSQL += " SEML_TDI_TIPOPROGRAMACION TIPO_PRO,  ";
strSQL += " SEML_TDI_PROGRAMACION_DISPO PRO_DISPO,  ";
strSQL += " SEML_THE_DISPOSITIVO DISPO, ";
strSQL += " SEML_TDI_ENCUESTADISPOSITIVO ED  ";
strSQL += " WHERE  PRO.ID_PROGRAMACION         =   PROXSEMANA.ID_PROGRAMACION       AND ";
strSQL += " PRO.ID_ENCUESTA             =   ENC.ID_ENCUESTA                  AND ";
strSQL += " PRO.ID_TIPOPROGRAMACION     =   TIPO_PRO.ID_TIPOPROGRAMACION     AND ";
strSQL += " PRO.ID_PROGRAMACION         =   PRO_DISPO.ID_PROGRAMACION        AND ";
strSQL += " PRO_DISPO.ESTATUS           =   'A'                              AND  ";
strSQL += " DISPO.DISPO_ESTATUS='A' AND ";
strSQL += " TIPO_PRO.PROGRAMACION_ESTATUS='A'    AND ";
strSQL += " ENC.ENCUESTA_STAT='A' AND ";
strSQL += " PRO.PROGRAMACION_ESTATUS='A' AND ";
strSQL += " PROXSEMANA.ESTATUS='A' AND ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   DISPO.ID_DISPOSITIVO             AND ";
strSQL += " PRO_DISPO.ID_DISPOSITIVO    =   ED.ID_DISPOSITIVO                AND ";
strSQL += " PRO.ID_ENCUESTA             =   ED.ID_ENCUESTA ";




            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);
                consultaIQRY.AddScalar("ID_PROGRAMACION", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PROGRAMACION_NOMBRE", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_PROGRA_FECHA_SEMANA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("ID_PRO_DISPO", NHibernateUtil.Int32);//3
                consultaIQRY.AddScalar("ENCUESTA_NOMBRE", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("ID_ENCUESTA", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("TIPOPROGRAMACION_DESC", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("ID_TIPOPROGRAMACION", NHibernateUtil.Int32);//7
                consultaIQRY.AddScalar("DISPO_DESCRIPCION", NHibernateUtil.String);//8
                consultaIQRY.AddScalar("ID_DISPOSITIVO", NHibernateUtil.Int32);//9
                consultaIQRY.AddScalar("ESTATUS_E", NHibernateUtil.Int32);//10

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_PrograDispositivo objPrograDispo = new THE_PrograDispositivo();

                    objPrograDispo.ID_PROGRA = Convert.ToInt32(obj[0].ToString());
                    objPrograDispo.PROGRAMACION_NOMBRE = (obj[1].ToString());
                    objPrograDispo.ID_PROGXFECHASEMANA = Convert.ToInt32(obj[2].ToString());
                    objPrograDispo.ID_PRO_DISPO = Convert.ToInt32(obj[3].ToString());
                    objPrograDispo.ENCUESTA_NOMBRE = (obj[4].ToString());
                    objPrograDispo.ID_ENC = Convert.ToInt32(obj[5].ToString());
                    objPrograDispo.TIPOPROGRAMACION_DESC = (obj[6].ToString());
                    objPrograDispo.ID_TIP_PROGRA = Convert.ToInt32(obj[7].ToString());
                    objPrograDispo.DISPO_DESCRIPCION = (obj[8].ToString());
                    objPrograDispo.ID_DISPO = Convert.ToInt32(obj[9].ToString());

                    if (obj[10] == null)
                    {
                        objPrograDispo.ColorEstatus = "../Images/notnot.jpg";
                    }
                    else {
                        if (int.Parse(obj[10].ToString()) == 2)
                        {
                            objPrograDispo.ColorEstatus = "../Images/not.jpg";
                        }
                        else if (int.Parse(obj[10].ToString()) == 3)
                        {
                            objPrograDispo.ColorEstatus = "../Images/notyet.jpg";
                        }
                        else if (int.Parse(obj[10].ToString()) == 4)
                        {
                            objPrograDispo.ColorEstatus = "../Images/yes.jpg";
                        }
                        else {
                            objPrograDispo.ColorEstatus = "../Images/notnot.jpg";
                        }
                    }
                    //objPrograDispo.ColorEstatus=

                    lstPrograDispo.Add(objPrograDispo);
                }

            }
            catch
            {
                lstPrograDispo = null;
                return lstPrograDispo;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            return lstPrograDispo;
            #endregion
        }

        public static IList<THE_PrograDispositivo> ObtenDispoProgramadosByProgramacion(string idProgramacion)
        {

            #region Query Armado

            List<THE_PrograDispositivo> lstPrograDispo = new List<THE_PrograDispositivo>();
            string strSQL = string.Empty;
            int IdDispositivo = 0;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();



            strSQL += " SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXFECHA.ID_PROGXFECHA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            strSQL += "                     TIPO_PRO.ID_TIPOPROGRAMACION, ";
            strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXFECHA PROXFECHA, SEML_THE_ENCUESTA ENC, ";
            strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXFECHA.ID_PROGRAMACION ";
            strSQL += "        AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            strSQL += "        AND PRO_DISPO.ESTATUS='A' AND PRO.PROGRAMACION_ESTATUS='A' ";
            strSQL += "        AND PROXFECHA.ESTATUS='A' ";
            strSQL += "        AND ENC.ENCUESTA_STAT='A' ";
            strSQL += "        AND TIPO_PRO.PROGRAMACION_ESTATUS='A' ";
            strSQL += "        AND DISPO.DISPO_ESTATUS='A' ";
            if (idProgramacion != "") { strSQL += "        AND PRO.ID_PROGRAMACION=" + idProgramacion + " "; }
            strSQL += "        UNION ";
            strSQL += "        SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXSEMANA.ID_PROGXSEMANA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            strSQL += "                    TIPO_PRO.ID_TIPOPROGRAMACION, ";
            strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXSEMANA PROXSEMANA, SEML_THE_ENCUESTA ENC, ";
            strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXSEMANA.ID_PROGRAMACION ";
            strSQL += "       AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            strSQL += "        AND PRO_DISPO.ESTATUS='A' ";
            strSQL += "        AND PRO.PROGRAMACION_ESTATUS='A' ";
            strSQL += "        AND PROXSEMANA.ESTATUS='A' ";
            strSQL += "        AND ENC.ENCUESTA_STAT='A' ";
            strSQL += "        AND TIPO_PRO.PROGRAMACION_ESTATUS='A' ";
            strSQL += "        AND DISPO.DISPO_ESTATUS='A' ";

            if (idProgramacion != "") { strSQL += "        AND PRO.ID_PROGRAMACION=" + idProgramacion + " "; }




            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);
                consultaIQRY.AddScalar("ID_PROGRAMACION", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PROGRAMACION_NOMBRE", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_PROGRA_FECHA_SEMANA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("ID_PRO_DISPO", NHibernateUtil.Int32);//3
                consultaIQRY.AddScalar("ENCUESTA_NOMBRE", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("ID_ENCUESTA", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("TIPOPROGRAMACION_DESC", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("ID_TIPOPROGRAMACION", NHibernateUtil.Int32);//7
                consultaIQRY.AddScalar("DISPO_DESCRIPCION", NHibernateUtil.String);//8
                consultaIQRY.AddScalar("ID_DISPOSITIVO", NHibernateUtil.Int32);//9
               // consultaIQRY.AddScalar("ESTATUS_E", NHibernateUtil.Int32);//10

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_PrograDispositivo objPrograDispo = new THE_PrograDispositivo();

                    objPrograDispo.ID_PROGRA = Convert.ToInt32(obj[0].ToString());
                    objPrograDispo.PROGRAMACION_NOMBRE = (obj[1].ToString());
                    objPrograDispo.ID_PROGXFECHASEMANA = Convert.ToInt32(obj[2].ToString());
                    objPrograDispo.ID_PRO_DISPO = Convert.ToInt32(obj[3].ToString());
                    objPrograDispo.ENCUESTA_NOMBRE = (obj[4].ToString());
                    objPrograDispo.ID_ENC = Convert.ToInt32(obj[5].ToString());
                    objPrograDispo.TIPOPROGRAMACION_DESC = (obj[6].ToString());
                    objPrograDispo.ID_TIP_PROGRA = Convert.ToInt32(obj[7].ToString());
                    objPrograDispo.DISPO_DESCRIPCION = (obj[8].ToString());
                    objPrograDispo.ID_DISPO = Convert.ToInt32(obj[9].ToString());

                    lstPrograDispo.Add(objPrograDispo);
                }

            }
            catch
            {
                lstPrograDispo = null;
                return lstPrograDispo;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            return lstPrograDispo;
            #endregion
        }


        public static Boolean existeDispoInProgramacion(string idProgramacion, string idDispositivo, string idEncuesta, string idTipoProgramacion)
        {
            #region Query Armado

            Boolean Existen = false;
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();



            strSQL += " SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXFECHA.ID_PROGXFECHA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            strSQL += "                     TIPO_PRO.ID_TIPOPROGRAMACION, ";
            strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXFECHA PROXFECHA, SEML_THE_ENCUESTA ENC, ";
            strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXFECHA.ID_PROGRAMACION ";
            strSQL += "        AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            strSQL += "        AND PRO_DISPO.ESTATUS='A' ";
            if (idProgramacion != "") { strSQL += " AND PRO_DISPO.ID_PROGRAMACION=" + idProgramacion + " "; }
            if (idDispositivo != "") { strSQL += "  AND PRO_DISPO.ID_DISPOSITIVO=" + idDispositivo + " "; }
            if (idEncuesta != "") { strSQL += "     AND PRO_DISPO.ID_ENCUESTA=" + idEncuesta + " "; }
            if (idTipoProgramacion != "") { strSQL += " AND PRO_DISPO.ID_TIPOPROGRAMACION=" + idTipoProgramacion + " "; }
            strSQL += "        UNION ";
            strSQL += "        SELECT PRO.ID_PROGRAMACION, PRO.PROGRAMACION_NOMBRE, PROXSEMANA.ID_PROGXSEMANA ID_PROGRA_FECHA_SEMANA,PRO_DISPO.ID_PRO_DISPO, ";
            strSQL += "                    ENC.ENCUESTA_NOMBRE, ENC.ID_ENCUESTA ,  ";
            strSQL += "                    TIPO_PRO.TIPOPROGRAMACION_DESC,  ";
            strSQL += "                    TIPO_PRO.ID_TIPOPROGRAMACION, ";
            strSQL += "                    DISPO.DISPO_DESCRIPCION, DISPO.ID_DISPOSITIVO ";
            strSQL += "        FROM SEML_THE_PROGRAMACION PRO, SEML_THE_PROGXSEMANA PROXSEMANA, SEML_THE_ENCUESTA ENC, ";
            strSQL += "                 SEML_TDI_TIPOPROGRAMACION TIPO_PRO, SEML_TDI_PROGRAMACION_DISPO PRO_DISPO, SEML_THE_DISPOSITIVO DISPO ";
            strSQL += "        WHERE PRO.ID_PROGRAMACION=PROXSEMANA.ID_PROGRAMACION ";
            strSQL += "       AND PRO.ID_ENCUESTA=ENC.ID_ENCUESTA ";
            strSQL += "        AND PRO.ID_TIPOPROGRAMACION=TIPO_PRO.ID_TIPOPROGRAMACION ";
            strSQL += "        AND PRO.ID_PROGRAMACION=PRO_DISPO.ID_PROGRAMACION ";
            strSQL += "        AND DISPO.ID_DISPOSITIVO=PRO_DISPO.ID_DISPOSITIVO ";
            strSQL += "        AND PRO_DISPO.ESTATUS='A' ";
            if (idProgramacion != "") { strSQL += " AND PRO_DISPO.ID_PROGRAMACION=" + idProgramacion + " "; }
            if (idDispositivo != "") { strSQL += "  AND PRO_DISPO.ID_DISPOSITIVO=" + idDispositivo + " "; }
            if (idEncuesta != "") { strSQL += "     AND PRO_DISPO.ID_ENCUESTA=" + idEncuesta + " "; }
            if (idTipoProgramacion != "") { strSQL += " AND PRO_DISPO.ID_TIPOPROGRAMACION=" + idTipoProgramacion + " "; }

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);


                consultaIQRY.AddScalar("ID_PROGRAMACION", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("PROGRAMACION_NOMBRE", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("ID_PROGRA_FECHA_SEMANA", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("ID_PRO_DISPO", NHibernateUtil.Int32);//3
                consultaIQRY.AddScalar("ENCUESTA_NOMBRE", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("ID_ENCUESTA", NHibernateUtil.Int32);//5
                consultaIQRY.AddScalar("TIPOPROGRAMACION_DESC", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("ID_TIPOPROGRAMACION", NHibernateUtil.Int32);//7
                consultaIQRY.AddScalar("DISPO_DESCRIPCION", NHibernateUtil.String);//8
                consultaIQRY.AddScalar("ID_DISPOSITIVO", NHibernateUtil.Int32);//9

                IList lista = consultaIQRY.List();

                if (lista.Count > 0)
                    Existen = true;
               
            }
            catch
            {
                Existen = false;
                return Existen;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            return Existen;
            #endregion
        }

        public static Boolean ActualizaProgramacion(THE_Programacion programacion)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Programacion>(programacion);
        }

        public static Boolean AgregaDispositivoProgramados(THE_PrograDispositivo programacion)
        {
            return NHibernateHelperORACLE.SingleSessionSaveOrUpdate<THE_PrograDispositivo>(programacion);
        }
        public static Boolean EliminaDispositivoProgramados(THE_PrograDispositivo programacion)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_PrograDispositivo>(programacion);
        }
        public static THE_Programacion ObtieneProgramacionPorID(int idProgramacion)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Programacion Programacion WHERE ID_PROGRAMACION = " + idProgramacion + " AND PROGRAMACION_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Programacion>(strQuery)[0];
            }
            catch (Exception ex)
            {
                return new THE_Programacion();
            }
        }

        public static Boolean EliminaProgramacion(THE_Programacion programacion)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Programacion>(programacion);
        }
        
    }
}
