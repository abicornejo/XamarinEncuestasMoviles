using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosNotificaciones
    {
        public static List<TDI_Notificaciones> ObtieneNotificaciones()
        {

            #region Query Armado
            List<TDI_Notificaciones> listaEncuesta = new List<TDI_Notificaciones>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " select noti.TOKEN tkn, noti.MENSAJE msj, NOTI.ID_NOTIFICACION_TEMP idnoti, noti.periodo peri, noti.id_encuesta encuesta,  DISPO.DISPO_NUMTELEFONO tel, DISPO.ID_DISPOSITIVO idDispo, noti.ID_ENVIO idEnvio "; 
            strSQL += " from seml_tdi_notificaciones_temp noti, seml_the_dispositivo dispo ";
            strSQL += " where noti.estatus=2 and  NOTI.TOKEN=DISPO.TOKEN_DISPOSITIVO ";         

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

               
                consultaIQRY.AddScalar("tkn", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("msj", NHibernateUtil.String);//1               
                consultaIQRY.AddScalar("idnoti", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("peri", NHibernateUtil.Int32);//3 
                consultaIQRY.AddScalar("encuesta", NHibernateUtil.Int32);//4
                consultaIQRY.AddScalar("tel", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("idDispo", NHibernateUtil.Int32);//6
                consultaIQRY.AddScalar("idEnvio", NHibernateUtil.Int32);//7
                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_Notificaciones oNotifica = new TDI_Notificaciones();
                    oNotifica.TokenDispositivo = System.Convert.ToString(obj[0]);
                    oNotifica.Mensaje = System.Convert.ToString(obj[1]);
                    oNotifica.IdNotificacion = System.Convert.ToInt32(obj[2]);
                    oNotifica.Periodo = System.Convert.ToInt32(obj[3]);
                    oNotifica.IdEncuesta = System.Convert.ToInt32(obj[4]);
                    oNotifica.Telefono = System.Convert.ToString(obj[5]);
                    oNotifica.IdDispo = System.Convert.ToInt32(obj[6]);
                    oNotifica.IdEnvio = System.Convert.ToInt32(obj[7]);
                    listaEncuesta.Add(oNotifica);
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

        public bool EliminaNotificacion(TDI_Notificaciones notificacion)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<TDI_Notificaciones>(notificacion);
        }
    }
}
