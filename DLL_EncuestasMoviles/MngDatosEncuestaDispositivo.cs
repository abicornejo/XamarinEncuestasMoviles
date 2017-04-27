using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Entidades_EncuestasMoviles;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosEncuestaDispositivo
    {
        public static IList<TDI_EncuestaDispositivo> ObtieneEncuestaPorDispositivo(double NumeroTel)
        {
            #region Query Armado
            GuardaLogTransacc("Conexión de dispositivo Android con el Web Service - No. Tel: " + NumeroTel.ToString(), 26, NumeroTel);
            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
            int IdDispositivo = 0;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encudis.ID_ENCUESTA IDEncuesta, encuesta.ENCUESTA_NOMBRE NombreEncuesta,  DISP.ID_DISPOSITIVO  IdDispositivo, ENCUDIS.ID_ENVIO IdEnvio";
            strSQL += " FROM seml_tdi_encuestadispositivo encudis, seml_the_encuesta encuesta, seml_the_dispositivo disp";
            strSQL += " WHERE encuesta.id_encuesta = encudis.id_encuesta";
            strSQL += " and ENCUDIS.ID_DISPOSITIVO = DISP.ID_DISPOSITIVO";
            strSQL += " AND encudis.ID_ESTATUS = 2";
            strSQL += " AND DISP.DISPO_NUMTELEFONO = " + NumeroTel.ToString() + " ORDER BY IDEncuesta";

            try
            {
                 ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);
                 
                consultaIQRY.AddScalar("IDEncuesta", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("NombreEncuesta", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("IdDispositivo", NHibernateUtil.Int32);//2
                consultaIQRY.AddScalar("IdEnvio", NHibernateUtil.Int32);//3
                
                IList lista = consultaIQRY.List();
                TDI_EncuestaDispositivo EncuDispo = new TDI_EncuestaDispositivo();
                List<THE_Preguntas> lstPregun = new List<THE_Preguntas>();
                List<THE_Respuestas> lstRespu = new List<THE_Respuestas>();
                EncuDispo.ListaEncuesta= new List<THE_Encuesta>();
                EncuDispo.ListPeriodos=new List<THE_PeriodoEncuesta>();
                foreach (Object[] obj in lista)
                {
                    
                    foreach (THE_Encuesta consulta in MngDatosEncuesta.ObtieneEncuestaPorID(Convert.ToInt32(obj[0])))
                    {
                        lstPregun = new List<THE_Preguntas>();
                        foreach(THE_Preguntas oPreg in MngDatosPreguntas.ObtienePreguntasPorEncuestaConFinEncu(consulta.IdEncuesta))
                        {
                            lstRespu = new List<THE_Respuestas>();
                            foreach(THE_Respuestas oResp in MngDatosRespuestas.ObtenerRespuestasPorPregunta(oPreg.IdPregunta))
                            {
                                lstRespu.Add(oResp);
                            }
                            oPreg.ListaRespuestas = lstRespu;
                            IdDispositivo = Convert.ToInt32(Convert.ToInt32(obj[2]));
                            lstPregun.Add(oPreg);
                            consulta.LstPreg = lstPregun;                           
                        }

                        foreach (THE_PeriodoEncuesta periodos in MngDatosPeriodoEncuesta.ObtienePeriodosPorEncuesta(consulta.IdEncuesta))
                        {
                            if (periodos !=null){
                                periodos.IdEncuesta =new THE_Encuesta() { IdEncuesta=Convert.ToInt32(consulta.IdEncuesta.ToString())};
                                EncuDispo.ListPeriodos.Add(periodos);
                            }                        
                        }
                        consulta.IdEnvio = obj[3] == null ? 0 : Int32.Parse(obj[3].ToString());
                        EncuDispo.ListaEncuesta.Add(consulta);
                        EncuDispo.IdDispositivo = new THE_Dispositivo(){IdDispositivo= IdDispositivo};
                        EncuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(obj[0].ToString())};
                        EncuDispo.IdEstatus = new TDI_Estatus() { IdEstatus = 3 };
                        EncuDispo.IdEnvio = obj[3] == null ? 0 : Int32.Parse(obj[3].ToString());
                        MngDatosEncuestaDispositivo.ActualizaEstatusDispoEncu(EncuDispo);
                        
                    }
                  
                }
                lstEncuestasDispo.Add(EncuDispo);
                
            }
            catch
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
            GuardaLogTransacc("Metodo consumido desde Android: ObtieneEncuestaPorDispositivo  - No. Tel: " + NumeroTel.ToString(), 29, NumeroTel);
            return lstEncuestasDispo;
            #endregion

        }
               
        public static IList<TDI_EncuestaDispositivo> ObtieneDispositivosPorEncuesta(int idEncuesta) {

            #region Query Armado
          
            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
            int IdDispositivo = 0;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();



               strSQL += "  select EDISPO.ID_DISPOSITIVO id_Dispo, EDISPO.ID_ENCUESTA id_enc, ";
               strSQL += "  DISPO.DISPO_NUMTELEFONO dispoNum, DISPO.DISPO_DESCRIPCION dispoDesc, edispo.id_envio id_envio";
               strSQL += "  from seml_tdi_encuestadispositivo edispo, seml_the_dispositivo dispo ";
               strSQL += "  where EDISPO.ID_DISPOSITIVO=DISPO.ID_DISPOSITIVO and ";
               strSQL += "  EDISPO.ID_ENCUESTA=" + idEncuesta.ToString() + " AND (EDISPO.ID_ESTATUS = 2  OR  EDISPO.ID_ESTATUS=3)";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("id_Dispo", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("id_enc", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("dispoNum", NHibernateUtil.Double);//2
                consultaIQRY.AddScalar("dispoDesc", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("id_envio", NHibernateUtil.Int32);//4

                IList lista = consultaIQRY.List();
                
                foreach (Object[] obj in lista)
                {
                    TDI_EncuestaDispositivo EncuDispo = new TDI_EncuestaDispositivo();
                    EncuDispo.IdDispo = int.Parse(obj[0].ToString());
                    EncuDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = int.Parse(obj[0].ToString()) };
                    EncuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(obj[1].ToString()) };
                    EncuDispo.NumTel = Convert.ToDouble(obj[2].ToString());
                    EncuDispo.DescTel = (obj[3].ToString());
                    EncuDispo.IdEnvio = Convert.ToInt32(obj[4].ToString());
                    lstEncuestasDispo.Add(EncuDispo);
                }

            }
            catch
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

        public static IList<TDI_EncuestaDispositivo> ObtieneDispositivosActivos(string idEncuesta)
        {

            #region Query Armado

            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
            
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT dis.id_dispositivo, NULL ESTATUS, dis.DISPO_DESCRIPCION "; 
            strSQL += " FROM seml_the_dispositivo dis "; 
            strSQL += " WHERE dis.ID_DISPOSITIVO NOT IN (SELECT ed.ID_DISPOSITIVO ";
            strSQL += " FROM seml_tdi_encuestadispositivo ed, seml_the_dispositivo d ";
            strSQL += " WHERE ed.ID_DISPOSITIVO=d.ID_DISPOSITIVO AND ed.ID_ENCUESTA="+idEncuesta+" ) AND DIS.DISPO_ESTATUS='A' ";
            strSQL += " UNION ";
            strSQL += " SELECT ed.ID_DISPOSITIVO, ed.ID_ESTATUS,  D.DISPO_DESCRIPCION   ";
            strSQL += " FROM seml_tdi_encuestadispositivo ed, seml_the_dispositivo d ";
            strSQL += " WHERE ed.ID_DISPOSITIVO=d.ID_DISPOSITIVO AND ed.ID_ENCUESTA=" + idEncuesta + " ";
            strSQL += " AND D.DISPO_ESTATUS='A'  order by 1 asc  ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_DISPOSITIVO", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("ESTATUS", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("DISPO_DESCRIPCION", NHibernateUtil.String);//2


                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_EncuestaDispositivo EncuDispo = new TDI_EncuestaDispositivo();
                    
                    
                    EncuDispo.DescTel = (obj[2].ToString());
                    EncuDispo.IdDispo = int.Parse(obj[0].ToString());

                    if (obj[1] == null)
                    {
                        EncuDispo.ColorEstatus = "../Images/notnot.jpg";
                    }
                    else
                    {
                        if (int.Parse(obj[1].ToString()) == 2)
                        {
                            EncuDispo.ColorEstatus = "../Images/not.jpg";
                        }
                        else if (int.Parse(obj[1].ToString()) == 3)
                        {
                            EncuDispo.ColorEstatus = "../Images/notyet.jpg";
                        }
                        else if (int.Parse(obj[1].ToString()) == 4)
                        {
                            EncuDispo.ColorEstatus = "../Images/yes.jpg";
                        }
                        else
                        {
                            EncuDispo.ColorEstatus = "../Images/notnot.jpg";
                        }
                    }
                    lstEncuestasDispo.Add(EncuDispo);
                }

            }
            catch
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

        
        public static IList<TDI_EncuestaDispositivo> ObtieneDispositivosPorIdEnvio(int idEnvio)
        {

            #region Query Armado

            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
            int IdDispositivo = 0;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();



            strSQL += "  select distinct EDISPO.ID_DISPOSITIVO id_Dispo, EDISPO.ID_ENCUESTA id_enc, ";
            strSQL += "  DISPO.DISPO_NUMTELEFONO dispoNum, DISPO.DISPO_DESCRIPCION dispoDesc, edispo.id_envio id_envio";
            strSQL += "  from seml_tdi_encuestadispositivo edispo, seml_the_dispositivo dispo ";
            strSQL += "  where EDISPO.ID_DISPOSITIVO=DISPO.ID_DISPOSITIVO and ";
            strSQL += "  EDISPO.ID_ENVIO=" + idEnvio.ToString() + "  AND (EDISPO.ID_ESTATUS = 2  OR  EDISPO.ID_ESTATUS=3);";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("id_Dispo", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("id_enc", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("dispoNum", NHibernateUtil.Double);//2
                consultaIQRY.AddScalar("dispoDesc", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("id_envio", NHibernateUtil.Int32);//4

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_EncuestaDispositivo EncuDispo = new TDI_EncuestaDispositivo();
                    EncuDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo=int.Parse(obj[0].ToString())};
                    EncuDispo.IdDispo = int.Parse(obj[0].ToString());
                    EncuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(obj[1].ToString()) };
                    EncuDispo.NumTel = Convert.ToDouble(obj[2].ToString());
                    EncuDispo.DescTel = (obj[3].ToString());
                    EncuDispo.IdEnvio = Convert.ToInt32(obj[4].ToString());
                    lstEncuestasDispo.Add(EncuDispo);
                }

            }
            catch
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

        public static IList<TDI_EncuestaDispositivo> ObtieneDispoByIdEncNumTel(string idEncuesta, string numTelefonico)
        {

            #region Query Armado

            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
         
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();



              strSQL += " select distinct EDISPO.ID_DISPOSITIVO id_Dispo, EDISPO.ID_ENCUESTA id_enc, edispo.id_envio id_envio ";
              strSQL += " from seml_tdi_encuestadispositivo edispo, seml_the_dispositivo dispo ";
              strSQL += " where EDISPO.ID_DISPOSITIVO=DISPO.ID_DISPOSITIVO and ";
              strSQL += " DISPO.DISPO_NUMTELEFONO="+numTelefonico+" AND EDISPO.ID_ENCUESTA="+idEncuesta+ " ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("ID_DISPO", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("ID_ENC", NHibernateUtil.Int32);//1               
                consultaIQRY.AddScalar("ID_ENVIO", NHibernateUtil.Int32);//2

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_EncuestaDispositivo EncuDispo = new TDI_EncuestaDispositivo();
                    EncuDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = int.Parse(obj[0].ToString()) };
                    EncuDispo.IdDispo = int.Parse(obj[0].ToString());
                    EncuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(obj[1].ToString()) };
                    EncuDispo.IdEnvio = Convert.ToInt32(obj[2].ToString());
                    lstEncuestasDispo.Add(EncuDispo);
                }

            }
            catch
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


        //public static IList<TDI_EncuestaDispositivo> ObtDispoByIdDispoNumTel(int idDispo, double numTelefono)
        //{

        //    #region Query Armado

        //    List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
        //    string strSQL = string.Empty;
        //    int IdDispositivo = 0;
        //    Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
        //    ISession session = NHibernateHelperORACLE.GetSession();



        //    strSQL += "  select EDISPO.ID_DISPOSITIVO id_Dispo, EDISPO.ID_ENCUESTA id_enc, ";
        //    strSQL += "  DISPO.DISPO_NUMTELEFONO dispoNum, DISPO.DISPO_DESCRIPCION dispoDesc, edispo.id_envio id_envio";
        //    strSQL += "  from seml_tdi_encuestadispositivo edispo, seml_the_dispositivo dispo ";
        //    strSQL += "  where EDISPO.ID_DISPOSITIVO=DISPO.ID_DISPOSITIVO and ";
        //    strSQL += "  EDISPO.ID_ENVIO=" + idEnvio.ToString() + "  AND (EDISPO.ID_ESTATUS = 2  OR  EDISPO.ID_ESTATUS=3);";

        //    try
        //    {
        //        ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

        //        consultaIQRY.AddScalar("id_Dispo", NHibernateUtil.Int32);//0
        //        consultaIQRY.AddScalar("id_enc", NHibernateUtil.Int32);//1
        //        consultaIQRY.AddScalar("dispoNum", NHibernateUtil.Double);//2
        //        consultaIQRY.AddScalar("dispoDesc", NHibernateUtil.String);//3
        //        consultaIQRY.AddScalar("id_envio", NHibernateUtil.Int32);//4

        //        IList lista = consultaIQRY.List();

        //        foreach (Object[] obj in lista)
        //        {
        //            TDI_EncuestaDispositivo EncuDispo = new TDI_EncuestaDispositivo();
        //            EncuDispo.IdDispositivo = new THE_Dispositivo() { IdDispositivo = int.Parse(obj[0].ToString()) };
        //            EncuDispo.IdDispo = int.Parse(obj[0].ToString());
        //            EncuDispo.IdEncuesta = new THE_Encuesta() { IdEncuesta = int.Parse(obj[1].ToString()) };
        //            EncuDispo.NumTel = Convert.ToDouble(obj[2].ToString());
        //            EncuDispo.DescTel = (obj[3].ToString());
        //            EncuDispo.IdEnvio = Convert.ToInt32(obj[4].ToString());
        //            lstEncuestasDispo.Add(EncuDispo);
        //        }

        //    }
        //    catch
        //    {
        //        lstEncuestasDispo = null;
        //        return lstEncuestasDispo;
        //    }
        //    finally
        //    {
        //        session.Close();
        //        session.Dispose();
        //        session = null;
        //    }
        //    return lstEncuestasDispo;
        //    #endregion
        //}



        public static Boolean AlmacenaDispoEncuesta(TDI_EncuestaDispositivo DispoEncu)
        {
            try
            {
                NHibernateHelperORACLE.SingleSessionSave<TDI_EncuestaDispositivo>(DispoEncu);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean ActualizaEstatusDispoEncu(TDI_EncuestaDispositivo DispoEncu)
        {
            try
            {
                if (NHibernateHelperORACLE.SingleSessionUpdate<TDI_EncuestaDispositivo>(DispoEncu))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean InsertNewDispoEncuesta(TDI_EncuestaDispositivo DispoEncu)
        {
            try
            {
                NHibernateHelperORACLE.SingleSessionSave<TDI_EncuestaDispositivo>(DispoEncu);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static IList<TDI_EncuestaDispositivo> ObtieneEstatusDispoEncu(int IdDispositivo, int IdEncuSel)
        {
            #region Query Armado
            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encudis.ID_DISPOSITIVO disId, encudis.ID_ENCUESTA encuId, ";
            strSQL += " encudis.ID_ESTATUS estatId ";
            strSQL += " FROM seml_the_dispositivo dispo, seml_tdi_encuestadispositivo encudis ";
            strSQL += " WHERE dispo.id_dispositivo = " + IdDispositivo;
            strSQL += " AND dispo.id_dispositivo = encudis.id_dispositivo ";
            strSQL += " AND encudis.ID_ENCUESTA = " + IdEncuSel;
            
            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("disId", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("encuId", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("estatId", NHibernateUtil.Int32);//2

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_EncuestaDispositivo encuDis = new TDI_EncuestaDispositivo();
                    encuDis.IdDispositivo = new THE_Dispositivo() { IdDispositivo = System.Convert.ToInt32(obj[0]) };
                    encuDis.IdEncuesta = new THE_Encuesta() { IdEncuesta = System.Convert.ToInt32(obj[1]) };
                    encuDis.IdEstatus = new TDI_Estatus() { IdEstatus = System.Convert.ToInt32(obj[2]) };
                    if (int.Parse(obj[2].ToString()) == 4)
                    {
                        encuDis.IdDispositivo.ColorEstatus = "../Images/yes.jpg";
                        encuDis.IdDispositivo.EstatusCheck = "checked='checked'";
                        encuDis.IdDispositivo.ChkEnabled = "disabled='disabled'";
                    }
                    else
                    {
                        if (int.Parse(obj[2].ToString()) == 2) { 
                            encuDis.IdDispositivo.ColorEstatus = "../Images/not.jpg";
                        }
                        else if (int.Parse(obj[2].ToString()) == 3) {
                            encuDis.IdDispositivo.ColorEstatus = "../Images/notyet.jpg";
                        }
                        
                    }

                    if(int.Parse(obj[2].ToString()) == 2 || int.Parse(obj[2].ToString()) == 3)
                    {
                        encuDis.IdDispositivo.EstatusCheck = "checked='checked'";
                        encuDis.IdDispositivo.ChkEnabled = "disabled='disabled'";
                    }

                    else if (int.Parse(obj[2].ToString()) == 1)
                    {
                        encuDis.IdDispositivo.EstatusCheck = "";
                        encuDis.IdDispositivo.ChkEnabled = "";
                    }

                    lstEncuestasDispo.Add(encuDis);
                }


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


        public static IList<TDI_EncuestaDispositivo> ObtieneRegistroByDispoByEnc(int IdDispositivo, int IdEncuSel)
        {
            #region Query Armado
            List<TDI_EncuestaDispositivo> lstEncuestasDispo = new List<TDI_EncuestaDispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT encudis.ID_DISPOSITIVO disId, encudis.ID_ENCUESTA encuId, ";
            strSQL += " encudis.ID_ESTATUS estatId ";
            strSQL += " FROM seml_the_dispositivo dispo, seml_tdi_encuestadispositivo encudis ";
            strSQL += " WHERE dispo.id_dispositivo = " + IdDispositivo;
            strSQL += " AND dispo.id_dispositivo = encudis.id_dispositivo ";
            strSQL += " AND encudis.ID_ENCUESTA = " + IdEncuSel + " AND (encudis.ID_ESTATUS = 2  OR  encudis.ID_ESTATUS=3)";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("disId", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("encuId", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("estatId", NHibernateUtil.Int32);//2

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_EncuestaDispositivo encuDis = new TDI_EncuestaDispositivo();
                    encuDis.IdDispositivo = new THE_Dispositivo() { IdDispositivo = System.Convert.ToInt32(obj[0]) };
                    encuDis.IdEncuesta = new THE_Encuesta() { IdEncuesta = System.Convert.ToInt32(obj[1]) };
                    encuDis.IdEstatus = new TDI_Estatus() { IdEstatus = System.Convert.ToInt32(obj[2]) };
                   

                    lstEncuestasDispo.Add(encuDis);
                }


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
