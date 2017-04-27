using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosBloqueoIP
    {
        public static List<IntentosUserXIP> ConsultaUltimosAccesos()
        {
            string strSql = string.Empty;
            List<IntentosUserXIP> Accesos = new List<IntentosUserXIP>();
            ISession session = NHibernateHelperORACLE.GetSession();

           

            strSql += " SELECT DISTINCT ID_TIPOACCESO AS acceso, LOG_IP AS ip, ";
            strSql += " COUNT (EMPL_LLAV_PR) intentos ";
            strSql += " FROM SEML_TDI_LOGACCESO ";
            strSql += "  ORDER BY LOG_FECHAACCESO DESC) ";
            strSql += " WHERE ROWNUM <= 10 ";
            strSql += "  GROUP BY ID_TIPOACCESO, LOG_IP ";
            strSql += "  ORDER BY 2, 1 ";
            try
            {
                ISQLQuery consultaImagenesOT = session.CreateSQLQuery(strSql);

                consultaImagenesOT.AddScalar("ACCESO", NHibernateUtil.Int32);
                consultaImagenesOT.AddScalar("IP", NHibernateUtil.String);
                consultaImagenesOT.AddScalar("INTENTOS", NHibernateUtil.Int32);

                IList listaImagenesOT = consultaImagenesOT.List();

                if (listaImagenesOT != null)
                {
                    if (listaImagenesOT.Count > 1)
                    {
                        foreach (Object[] Tmp in listaImagenesOT)
                        {
                            IntentosUserXIP IUAdd = new IntentosUserXIP();
                            IUAdd.TipoIntento = (Int32)Tmp[0];
                            IUAdd.NoIP = Tmp[1].ToString();
                            IUAdd.NumIntento = (Int32)Tmp[2];
                            Accesos.Add(IUAdd);
                        }
                    }
                    else
                    {
                        IntentosUserXIP IUAdd = new IntentosUserXIP();
                        IUAdd.TipoIntento = (Int32)((object[])((listaImagenesOT[0])))[0];
                        IUAdd.NoIP = ((object[])((listaImagenesOT[0])))[1].ToString();
                        IUAdd.NumIntento = (Int32)((object[])((listaImagenesOT[0])))[2];

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

        public static bool GuardaIPBloqueada(THE_BloqueoIP oBloqueoIP)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_BloqueoIP>(oBloqueoIP);
        }

        public static List<IntentosUserXIP> ConsultaUltimoAccesos()
        {
            string strSql = string.Empty;
            List<IntentosUserXIP> Accesos = new List<IntentosUserXIP>();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSql += " SELECT DISTINCT ID_TIPOACCESO AS ACCESO,LOG_IP AS IP , COUNT(EMPL_LLAV_PR) INTENTOS FROM ";
            strSql += " ( SELECT * FROM SEML_TDI_LOGACCESO ";
            strSql += " ORDER BY LOG_FECHAACCESO DESC ) WHERE ROWNUM <= 10 ";
            strSql += " GROUP BY ID_TIPOACCESO,LOG_IP ORDER BY 2,1 ";

            try
            {
                ISQLQuery consultaImagenesOT = session.CreateSQLQuery(strSql);

                consultaImagenesOT.AddScalar("ACCESO", NHibernateUtil.Int32);
                consultaImagenesOT.AddScalar("IP", NHibernateUtil.String);
                consultaImagenesOT.AddScalar("INTENTOS", NHibernateUtil.Int32);

                IList listaImagenesOT = consultaImagenesOT.List();

                if (listaImagenesOT != null)
                {
                    if (listaImagenesOT.Count > 1)
                    {
                        foreach (Object[] Tmp in listaImagenesOT)
                        {
                            IntentosUserXIP IUAdd = new IntentosUserXIP();
                            IUAdd.TipoIntento = (Int32)Tmp[0];
                            IUAdd.NoIP = Tmp[1].ToString();
                            IUAdd.NumIntento = (Int32)Tmp[2];
                            Accesos.Add(IUAdd);
                        }
                    }
                    else
                    {
                        IntentosUserXIP IUAdd = new IntentosUserXIP();
                        IUAdd.TipoIntento = (Int32)((object[])((listaImagenesOT[0])))[0];
                        IUAdd.NoIP = ((object[])((listaImagenesOT[0])))[1].ToString();
                        IUAdd.NumIntento = (Int32)((object[])((listaImagenesOT[0])))[2];

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

        public static List<IntentosUserXIP> ConsultaUltimoAccesos(string IP)
        {
            string strSql = string.Empty;
            List<IntentosUserXIP> Accesos = new List<IntentosUserXIP>();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSql += " select INTENTOS, ACCESO, IP from ( " +
                        " SELECT loac.loac_llav_pr as INTENTOS, tiac.tiac_llav_pr AS acceso, loac_usip AS ip " +
                        " FROM secn_the_loac loac, secn_tdi_tiac tiac " +
                        " where " +
                        " loac_usip = '" + IP + "' " +
                        " order by loac_llav_pr desc) " +
                        " where rownum <= 10 ";

            try
            {
                ISQLQuery consultaImagenesOT = session.CreateSQLQuery(strSql);
                consultaImagenesOT.AddScalar("INTENTOS", NHibernateUtil.Int32);
                consultaImagenesOT.AddScalar("ACCESO", NHibernateUtil.Int32);
                consultaImagenesOT.AddScalar("IP", NHibernateUtil.String);

                IList listaImagenesOT = consultaImagenesOT.List();

                if (listaImagenesOT != null)
                {
                    if (listaImagenesOT.Count > 1)
                    {
                        foreach (Object[] Tmp in listaImagenesOT)
                        {
                            IntentosUserXIP IUAdd = new IntentosUserXIP();
                            IUAdd.TipoIntento = (Int32)Tmp[0];
                            IUAdd.NoIP = Tmp[1].ToString();
                            IUAdd.NumIntento = (Int32)Tmp[2];
                            Accesos.Add(IUAdd);
                        }
                    }
                    else
                    {
                        IntentosUserXIP IUAdd = new IntentosUserXIP();
                        IUAdd.TipoIntento = (Int32)((object[])((listaImagenesOT[0])))[0];
                        IUAdd.NoIP = ((object[])((listaImagenesOT[0])))[1].ToString();
                        IUAdd.NumIntento = (Int32)((object[])((listaImagenesOT[0])))[2];

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
