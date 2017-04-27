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
    public class MngDatosBloqueoUsuario
    {
        public static bool GuardaUsuarioBloqueado(THE_BloqueoUsuario oBloqueoUsuario)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_BloqueoUsuario>(oBloqueoUsuario);
        }

        public static IList<THE_BloqueoUsuario> ConsultaUsuarioBloqueadoXIdUsuario(string numEmpleado, string tipoBloqueo)
        {
            return NHibernateHelperORACLE.SingleSessionFind<THE_BloqueoUsuario>(" from THE_BloqueoUsuario BloqueoUsuario Where EMPL_USUA = '" + numEmpleado + "' and TIBL_LLAV_PR = " + tipoBloqueo);
        }

        public static List<IntentosUsuario> ConsultaUltimoAccesosUsuario(string usuario)
        {
            string strSql = string.Empty;
            List<IntentosUsuario> Accesos = new List<IntentosUsuario>();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSql += " SELECT DISTINCT(ID_TIPOACCESO) AS ACCESO, COUNT(EMPL_LLAV_PR) AS INTENTOS FROM ";
            strSql += " ( SELECT * FROM SEML_TDI_LOGACCESO ";
            strSql += " WHERE EMPL_USUA = '" + usuario + "'  ";
            strSql += " ORDER BY LOG_FECHAACCESO DESC ) WHERE ROWNUM <= 3 GROUP BY ID_TIPOACCESO";

            try
            {
                ISQLQuery consultaImagenesOT = session.CreateSQLQuery(strSql);

                consultaImagenesOT.AddScalar("ACCESO", NHibernateUtil.Int32);
                consultaImagenesOT.AddScalar("INTENTOS", NHibernateUtil.Int32);

                IList listaImagenesOT = consultaImagenesOT.List();

                if (listaImagenesOT != null)
                {
                    if (listaImagenesOT.Count > 1)
                    {
                        foreach (Object[] Tmp in listaImagenesOT)
                        {
                            IntentosUsuario IUAdd = new IntentosUsuario();
                            IUAdd.TipoIntento = (Int32)Tmp[0];
                            IUAdd.NumIntento = (Int32)Tmp[1];
                            Accesos.Add(IUAdd);
                        }
                    }
                    else
                    {
                        IntentosUsuario IUAdd = new IntentosUsuario();
                        IUAdd.TipoIntento = (Int32)((object[])((listaImagenesOT[0])))[0];
                        IUAdd.NumIntento = (Int32)((object[])((listaImagenesOT[0])))[1];

                        Accesos.Add(IUAdd);
                    }
                }

                return Accesos;
            }
            catch (Exception ex)
            {
             
                return Accesos;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
        }
    }
}
