using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosDispositivo
    {
        public static IList<THE_Dispositivo> ObtenerDispositivoNumero(double NumeroTelefono)
        {
            try
            {
                GuardaLogTransacc("Conexión de dispositivo Android con el Web Service - No. Tel: " + NumeroTelefono.ToString(),26,NumeroTelefono);
                string strQuery = string.Empty;
                strQuery = "FROM THE_Dispositivo Encuesta WHERE (DISPO_NUMTELEFONO = " + NumeroTelefono + " OR DISPO_MEID = " + NumeroTelefono + ") AND DISPO_ESTATUS = 'A'";
                GuardaLogTransacc("Metodo consumido desde Android : ObtenerDispositivoNumero  - No. Tel: " + NumeroTelefono.ToString(), 27, NumeroTelefono);
                return NHibernateHelperORACLE.SingleSessionFind<THE_Dispositivo>(strQuery);
            }
            catch (Exception ex)
            {
              string s= ex.StackTrace.ToString();

                MngDatosLogErrores.GuardaError(ex, "MngDatosDispositivo");
                return new List<THE_Dispositivo>();
            }
        }

        public static Boolean GuardaAltaDispositivo(THE_Dispositivo dispo)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_Dispositivo>(dispo);
        }


        public static Int32 GuardaAltaDispo(THE_Dispositivo dispo)
        {           
            try
            {
                NHibernateHelperORACLE.SingleSessionSave<THE_Dispositivo>(dispo);
                return dispo.IdDispositivo;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public static Boolean GuardaVersionDispo(TDI_DispoApVersion dispoVersion) {

            return NHibernateHelperORACLE.SingleSessionSaveOrUpdate<TDI_DispoApVersion>(dispoVersion);        
        }
        public static Boolean ActualizaVersionDispo(TDI_DispoApVersion dispoVersion)
        {

            return NHibernateHelperORACLE.SingleSessionUpdate<TDI_DispoApVersion>(dispoVersion);        
        }

        public static IList<THE_Dispositivo> ObtieneTodosDispositivos()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Dispositivo Dispositivo WHERE DISPO_ESTATUS = 'A' ORDER BY ID_DISPOSITIVO ASC";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Dispositivo>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosDispositivo");
                return new List<THE_Dispositivo>();
            }
        }
        public static List<TDI_DispoApVersion> VerificaDispoIntoVersion(string numTelefono)
        {
           
               
                #region Query Armado
                List<TDI_DispoApVersion> lstDispoDisponibles = new List<TDI_DispoApVersion>();
                string strSQL = string.Empty;
                Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
                ISession session = NHibernateHelperORACLE.GetSession();

                strSQL += " select ver.ID_DISAPVER id, ver.NUM_TEL numtel, ver.VER_CODE vercode, ver.VER_NAME vername , ver.VER_DATE verdate ";
                strSQL += " from seml_TDI_DispoApVersion ver ";
                strSQL += " where VER.NUM_TEL=" + numTelefono;               
              

                try
                {
                    ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                    consultaIQRY.AddScalar("id", NHibernateUtil.Int32);//0
                    consultaIQRY.AddScalar("numtel", NHibernateUtil.String);//1
                    consultaIQRY.AddScalar("vercode", NHibernateUtil.Int32);//2
                    consultaIQRY.AddScalar("vername", NHibernateUtil.String);//3
                    consultaIQRY.AddScalar("verdate", NHibernateUtil.DateTime);//4
                   

                    IList lista = consultaIQRY.List();
                    foreach (Object[] obj in lista)
                    {
                        TDI_DispoApVersion oDispo = new TDI_DispoApVersion();
                        oDispo.ID_DISAPVER = System.Convert.ToInt32(obj[0]);
                        oDispo.NUM_TEL = System.Convert.ToString(obj[1]);
                        oDispo.NUMBER = System.Convert.ToInt32(obj[2]);
                        oDispo.VER_NAME = System.Convert.ToString(obj[3]);
                        oDispo.VER_DATE = System.Convert.ToDateTime(obj[4]);

                        lstDispoDisponibles.Add(oDispo);
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
                #endregion
           
        }
        public static IList<THE_Dispositivo> GetLastDispositivo()
        {
           
               
                #region Query Armado
                List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
                string strSQL = string.Empty;
                Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
                ISession session = NHibernateHelperORACLE.GetSession();

                strSQL += " select MAX(ID_DISPOSITIVO) id_dispo ";               
                strSQL += " FROM seml_the_dispositivo dispo ";
                strSQL += " where dispo.dispo_estatus = 'A' ";

                try
                {
                    ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                    consultaIQRY.AddScalar("id_dispo", NHibernateUtil.Int32);//0
                   

                    IList lista = consultaIQRY.List();
                    foreach (Object[] obj in lista)
                    {
                        THE_Dispositivo oDispo = new THE_Dispositivo();
                        oDispo.IdDispositivo = System.Convert.ToInt32(obj[0]);
                        lstDispoDisponibles.Add(oDispo);
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
                #endregion
           
        }

        public static Boolean EliminaDispositivo(THE_Dispositivo dispo)
        {
            return NHibernateHelperORACLE.SingleSessionSaveOrUpdate<THE_Dispositivo>(dispo);
        }

        public static Boolean ActualizaDispositivo(THE_Dispositivo dispo)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Dispositivo>(dispo);
        }

        public static Boolean GuardaCoordenadasDispositivo(TDI_UbicacionDispositivo LogPosicionDispositivo)
        {
            bool insertado= false;
            THE_Dispositivo dispositivo = LogPosicionDispositivo.IdDispositivo;            
            GuardaLogTransacc("Conexión de dispositivo Android con el Web Service - No. Tel: " + dispositivo.NumerodelTelefono.ToString(), 26, Convert.ToDouble(dispositivo.NumerodelTelefono));
            insertado= NHibernateHelperORACLE.SingleSessionSave<TDI_UbicacionDispositivo>(LogPosicionDispositivo);
            GuardaLogTransacc("Metodo consumido desde Android: GuardaCoordenadasDispositivo - No. Tel: " + dispositivo.NumerodelTelefono.ToString(), 31, Convert.ToDouble(dispositivo.NumerodelTelefono));
            return insertado;
        }

        public static List<TDI_UbicacionDispositivo> ObtieneCoordenadasDispositivo(int UsuarioId, string fechaInicial, string fechaFinal)
        {
            #region Query Armado
            List<TDI_UbicacionDispositivo> lstDispoCoordenadas = new List<TDI_UbicacionDispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL+=" SELECT   dispubi.dispoubicacion_latitud latitud, ";
            strSQL+=" dispubi.dispoubicacion_longitud longitud, disp.dispo_numtelefono numero, ";
            strSQL += " emp.usua_fotourl fotoemplado, emp.usua_nombre nombre, emp.usua_apellpaterno apaterno, ";
            strSQL += " emp.usua_apellmaterno amaterno, disp.dispo_imagenurl fotodisp,emp.USUA_LLAV_PR llave, disp.ID_DISPOSITIVO IdDispo ";
            strSQL+=" FROM seml_the_usuario emp, ";
            strSQL+=" seml_the_dispositivo disp, ";
            strSQL+=" seml_tdi_usuariodispositivo usuadisp, ";
            strSQL+=" seml_tdi_dispoubicacion dispubi ";
            strSQL+=" WHERE emp.usua_llav_pr = usuadisp.usua_llav_pr ";
            strSQL+=" AND disp.id_dispositivo = usuadisp.id_dispositivo ";
            strSQL+=" AND emp.usua_estatus = 'A' ";
            strSQL+=" AND disp.dispo_estatus = 'A' ";
            strSQL+=" AND usuadisp.usuadispo_estatus = 'A' ";
            strSQL+=" AND dispubi.id_dispositivo = disp.id_dispositivo ";

            if (!string.IsNullOrEmpty(fechaInicial) && !string.IsNullOrEmpty(fechaFinal))
            {
                strSQL += " AND TRUNC (dispubi.dispoubicacion_fecha) ";
                strSQL += " BETWEEN TO_DATE ('" + fechaInicial + "', 'DD/MM/YYYY')";
                strSQL += " AND TO_DATE ('" + fechaFinal + "', 'DD/MM/YYYY') ";
            }
            if (UsuarioId >0)
            {
                strSQL += " AND emp.usua_llav_pr = " + UsuarioId + "";
                strSQL += " ORDER BY dispubi.dispoubicacion_fecha ASC ";
            }

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("latitud", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("longitud", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("numero", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("fotoemplado", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("nombre", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("apaterno", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("amaterno", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("fotodisp", NHibernateUtil.String);//7
                consultaIQRY.AddScalar("llave", NHibernateUtil.Int32);//8
                consultaIQRY.AddScalar("IdDispo", NHibernateUtil.Int32);//9
                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_UbicacionDispositivo oDispo = new TDI_UbicacionDispositivo();
                    oDispo.Latitud = System.Convert.ToString(obj[0]);
                    oDispo.Longitud = System.Convert.ToString(obj[1]);

                    THE_Usuario user = new THE_Usuario();
                    user.UsuarioLlavePrimaria = System.Convert.ToInt32(obj[8]);
                    user.UsuarioNombre = System.Convert.ToString(obj[4]) + " " + System.Convert.ToString(obj[5] + " " + System.Convert.ToString(obj[6]));
                    user.UsuarioFoto = System.Convert.ToString(obj[3]);
                    oDispo.IdUsuario = user;
                    
                    THE_Dispositivo disp = new THE_Dispositivo();
                    disp.IdDispositivo = System.Convert.ToInt32(obj[9]);
                    disp.NumerodelTelefono = System.Convert.ToString(obj[2]);
                    disp.ImagenTelefono = System.Convert.ToString(obj[7]);

                    oDispo.IdDispositivo = disp;


                    lstDispoCoordenadas.Add(oDispo);
                }


            }
            catch (Exception ex)
            {

                lstDispoCoordenadas = null;
                return lstDispoCoordenadas;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return lstDispoCoordenadas;
            #endregion
        }

        public static IList<THE_Dispositivo> ObtieneDispositivosDisponibles()
        {
            #region Query Armado
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion dispodesc, dispo.dispo_imagenurl dispoimg, ";
            strSQL += " dispo.dispo_marca dispomarca, dispo.dispo_mdn dispomdn, ";
            strSQL += " dispo.dispo_meid dispomeid, dispo.dispo_modelo dispomodelo, ";
            strSQL += " dispo.dispo_numtelefono disponume, dispo.ID_DISPOSITIVO DispoID ";
            strSQL += " FROM seml_the_dispositivo dispo ";
            strSQL += " WHERE dispo.id_dispositivo not in ( select id_dispositivo from seml_tdi_usuariodispositivo) ";
            strSQL += " AND dispo.dispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("dispodesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("dispoimg", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dispomarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dispomdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dispomeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dispomodelo", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disponume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("DispoID", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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
            #endregion
        }

        public static IList<THE_Dispositivo> ObtieneDispositivoPorID(int IdDispo)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = " FROM THE_Dispositivo Encuesta WHERE ID_DISPOSITIVO = " + IdDispo + " AND DISPO_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Dispositivo>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosDispositivo");
                return new List<THE_Dispositivo>();
            }
        }
       
        public static IList<THE_Dispositivo> ObtieneDispositivosAsignadosUsuario()
        {
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT dispo.dispo_descripcion disdesc, dispo.dispo_estatus disestat, ";
            strSQL += " dispo.dispo_imagenurl disimagen, dispo.dispo_marca dismarca, ";
            strSQL += " dispo.dispo_mdn dismdn, dispo.dispo_meid dismeid, ";
            strSQL += " dispo.dispo_modelo dismodel, dispo.dispo_numtelefono disnume, ";
            strSQL += " dispo.id_dispositivo disid ";
            strSQL += " FROM seml_the_dispositivo dispo, seml_tdi_usuariodispositivo usuadis ";
            strSQL += " WHERE dispo.id_dispositivo = usuadis.id_dispositivo ";
            strSQL += " AND dispo.dispo_estatus = 'A'";
            strSQL += " AND usuadis.usuadispo_estatus = 'A' order by DISPO.ID_DISPOSITIVO ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("disdesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("disimagen", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dismarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dismdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dismeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dismodel", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disnume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("disid", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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

        public static IList<THE_Dispositivo> BuscaDispositivoFiltros(string NombUsuario, string NumeroTel, string NombEstado, string NombMuni, string[] Catalogos)
        {
            List<THE_Dispositivo> listaDispo = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion disdesc, dispo.dispo_estatus disestatus, ";
            strSQL += " dispo.dispo_imagenurl disimagen, dispo.dispo_marca dismarca, ";
            strSQL += " dispo.dispo_mdn dismdn, dispo.dispo_meid dismeid, ";
            strSQL += " dispo.dispo_modelo dismodelo, dispo.dispo_numtelefono disnumtel, ";
            strSQL += " dispo.id_dispositivo disid ";
            strSQL += " FROM seml_the_usuario usua, seml_the_dispositivo dispo, seml_tdi_usuariodispositivo usuadis, ";
            strSQL += " seml_tdi_estado estado, seml_tdi_municipios munic, seml_tdi_usuariocat usuacat, SEML_TDI_COLONIAS COL ";
            if (Catalogos.Length > 0 && Catalogos != null)
            {
                strSQL += ", ";
                for (int ini = 0; ini < Catalogos.Length; ini++)
                {
                    string IdOpcCat = Catalogos[ini].ToString().Split('|')[1];
                    if (ini == Catalogos.Length - 1)
                    {
                        strSQL += " seml_tdi_usuariocat  cat" + IdOpcCat;
                    }
                    else
                    {
                        strSQL += " seml_tdi_usuariocat  cat" + IdOpcCat + ", ";
                    }
                }
            }
            strSQL += " WHERE usua.usua_llav_pr = usuadis.usua_llav_pr ";
            strSQL += " AND estado.id_estado = munic.id_estado ";
            strSQL += " AND dispo.id_dispositivo = usuadis.id_dispositivo ";            
            strSQL += " AND USUA.ID_COLONIA = COL.ID_COLONIA ";
            strSQL += " AND COL.ID_MUNICIPIO = MUNIC.ID_MUNICIPIO ";

            if(NombUsuario != "")
                strSQL += " AND UPPER(usua.usua_nombre ||' '|| usua.usua_apellpaterno ||' '|| usua.usua_apellmaterno) LIKE " + " UPPER('%" + NombUsuario + "%')" + " ";
            if(NumeroTel != "")
                strSQL += " AND dispo.dispo_numtelefono = " + NumeroTel;
            if (NombEstado != "" && NombEstado != "0")
            {
                strSQL += " AND estado.ID_ESTADO = " + NombEstado;
                strSQL += " AND estado.estado_estatus = 'A' ";
            }
            if (NombMuni != "" && NombMuni != "0")
            {
                strSQL += " AND munic.ID_MUNICIPIO = " + NombMuni;
                strSQL += " AND munic.MUNICIPIO_ESTATUS = 'A' ";
            }

            if (Catalogos.Length > 0 && Catalogos != null)
            {
                for (int ini = 0; ini < Catalogos.Length; ini++)
                {
                    string IdOpcCat = Catalogos[ini].ToString().Split('|')[1];
                    string cat_LLaV_pr = " AND cat"+IdOpcCat+".usua_llaV_pr = usua.usua_llaV_pr ";
                    string id_OpcionCat = " AND cat"+IdOpcCat+".ID_OPCIONCAT="+IdOpcCat;                   
                    strSQL += cat_LLaV_pr + id_OpcionCat;   
                }
            }           

            strSQL += " AND usua.usua_estatus = 'A' ";
            strSQL += " AND usuadis.usuadispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("disdesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("disestatus", NHibernateUtil.AnsiChar);//1
                consultaIQRY.AddScalar("disimagen", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dismarca", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dismdn", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dismeid", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("dismodelo", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("disnumtel", NHibernateUtil.String);//7
                consultaIQRY.AddScalar("disid", NHibernateUtil.Int32);//8

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.DispositivoEstatus = System.Convert.ToChar(obj[1]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[2]);
                    oDispo.Marca = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[4]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[5]);
                    oDispo.Modelo = System.Convert.ToString(obj[6]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[7]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[8]);

                    listaDispo.Add(oDispo);
                }


            }
            catch (Exception ex)
            {
                listaDispo = null;
                return listaDispo;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaDispo;
        }

        public static IList<THE_Dispositivo> BusquedaDispositivoPorNumeroTel(string NumeroTelefono)
        {
            #region Query Armado
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion dispodesc, ";
            strSQL += " dispo.dispo_imagenurl dispoimg, dispo.dispo_marca dispomarca, ";
            strSQL += " dispo.dispo_mdn dispomdn, dispo.dispo_meid dispomeid, ";
            strSQL += " dispo.dispo_modelo dispomodelo, ";
            strSQL += " dispo.dispo_numtelefono disponume, ";
            strSQL += " dispo.id_dispositivo dispoid ";
            strSQL += " FROM seml_the_dispositivo dispo ";
            strSQL += " WHERE dispo.id_dispositivo NOT IN (SELECT id_dispositivo FROM seml_tdi_usuariodispositivo) ";
            if (NumeroTelefono != "")
            {
                strSQL += " AND dispo.dispo_numtelefono = " + NumeroTelefono;
            }
            strSQL += " AND dispo.dispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("dispodesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("dispoimg", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dispomarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dispomdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dispomeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dispomodelo", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disponume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("DispoID", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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
            #endregion
        }

        public static IList<THE_Dispositivo> BusquedaDispositivoPorNumeroTel(string NumeroTelefono, int idDispositivo)
        {
            #region Query Armado
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion dispodesc, ";
            strSQL += " dispo.dispo_imagenurl dispoimg, dispo.dispo_marca dispomarca, ";
            strSQL += " dispo.dispo_mdn dispomdn, dispo.dispo_meid dispomeid, ";
            strSQL += " dispo.dispo_modelo dispomodelo, ";
            strSQL += " dispo.dispo_numtelefono disponume, ";
            strSQL += " dispo.id_dispositivo dispoid ";
            strSQL += " FROM seml_the_dispositivo dispo ";
            strSQL += " WHERE dispo.id_dispositivo NOT IN (SELECT id_dispositivo FROM seml_tdi_usuariodispositivo) ";
            if (NumeroTelefono != "")
            {
                strSQL += " AND dispo.dispo_numtelefono = " + NumeroTelefono;
            }
            if (idDispositivo !=null)
            {
                strSQL += " AND dispo.id_dispositivo = " + idDispositivo;
            }
            strSQL += " AND dispo.dispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("dispodesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("dispoimg", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dispomarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dispomdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dispomeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dispomodelo", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disponume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("DispoID", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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
            #endregion
        }

        public static IList<THE_Dispositivo> BusquedaDispositivoPorMEID(string MEIDTelefono)
        {
            #region Query Armado
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion dispodesc, ";
            strSQL += " dispo.dispo_imagenurl dispoimg, dispo.dispo_marca dispomarca, ";
            strSQL += " dispo.dispo_mdn dispomdn, dispo.dispo_meid dispomeid, ";
            strSQL += " dispo.dispo_modelo dispomodelo, ";
            strSQL += " dispo.dispo_numtelefono disponume, ";
            strSQL += " dispo.id_dispositivo dispoid ";
            strSQL += " FROM seml_the_dispositivo dispo ";
            strSQL += " WHERE dispo.id_dispositivo NOT IN (SELECT id_dispositivo FROM seml_tdi_usuariodispositivo) ";
            if (MEIDTelefono != "")
            {
                strSQL += " AND dispo.dispo_meid = " + MEIDTelefono;
            }
            strSQL += " AND dispo.dispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("dispodesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("dispoimg", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dispomarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dispomdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dispomeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dispomodelo", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disponume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("DispoID", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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
            #endregion
        }

        public static IList<THE_Dispositivo> BusquedaDispositivoPorMEID(string MEIDTelefono, int idDispositivo)
        {
            #region Query Armado
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion dispodesc, ";
            strSQL += " dispo.dispo_imagenurl dispoimg, dispo.dispo_marca dispomarca, ";
            strSQL += " dispo.dispo_mdn dispomdn, dispo.dispo_meid dispomeid, ";
            strSQL += " dispo.dispo_modelo dispomodelo, ";
            strSQL += " dispo.dispo_numtelefono disponume, ";
            strSQL += " dispo.id_dispositivo dispoid ";
            strSQL += " FROM seml_the_dispositivo dispo ";
            strSQL += " WHERE dispo.id_dispositivo NOT IN (SELECT id_dispositivo FROM seml_tdi_usuariodispositivo) ";
            if (MEIDTelefono != "")
            {
                strSQL += " AND dispo.dispo_meid = " + MEIDTelefono;
            }
            if (idDispositivo != null)
            {
                strSQL += " AND dispo.id_dispositivo = " + idDispositivo;
            }

            strSQL += " AND dispo.dispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("dispodesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("dispoimg", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dispomarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dispomdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dispomeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dispomodelo", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disponume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("DispoID", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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
            #endregion
        }

        public static IList<THE_Dispositivo> BusquedaDispositivoPorMDN(string MDNTelefono)
        {
            #region Query Armado
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion dispodesc, ";
            strSQL += " dispo.dispo_imagenurl dispoimg, dispo.dispo_marca dispomarca, ";
            strSQL += " dispo.dispo_mdn dispomdn, dispo.dispo_meid dispomeid, ";
            strSQL += " dispo.dispo_modelo dispomodelo, ";
            strSQL += " dispo.dispo_numtelefono disponume, ";
            strSQL += " dispo.id_dispositivo dispoid ";
            strSQL += " FROM seml_the_dispositivo dispo ";
            strSQL += " WHERE dispo.id_dispositivo NOT IN (SELECT id_dispositivo FROM seml_tdi_usuariodispositivo) ";
            if (MDNTelefono != "")
            {
                strSQL += " AND dispo.dispo_mdn = " + MDNTelefono;
            }
            strSQL += " AND dispo.dispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("dispodesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("dispoimg", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dispomarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dispomdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dispomeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dispomodelo", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disponume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("DispoID", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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
            #endregion
        }

        public static IList<THE_Dispositivo> BusquedaDispositivoPorMDN(string MDNTelefono, int idDispositivo)
        {
            #region Query Armado
            List<THE_Dispositivo> lstDispoDisponibles = new List<THE_Dispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT DISTINCT dispo.dispo_descripcion dispodesc, ";
            strSQL += " dispo.dispo_imagenurl dispoimg, dispo.dispo_marca dispomarca, ";
            strSQL += " dispo.dispo_mdn dispomdn, dispo.dispo_meid dispomeid, ";
            strSQL += " dispo.dispo_modelo dispomodelo, ";
            strSQL += " dispo.dispo_numtelefono disponume, ";
            strSQL += " dispo.id_dispositivo dispoid ";
            strSQL += " FROM seml_the_dispositivo dispo ";
            strSQL += " WHERE dispo.id_dispositivo NOT IN (SELECT id_dispositivo FROM seml_tdi_usuariodispositivo) ";
            if (MDNTelefono != "")
            {
                strSQL += " AND dispo.dispo_mdn = " + MDNTelefono;
            }
            if (idDispositivo != null)
            {
                strSQL += " AND dispo.id_dispositivo = " + idDispositivo;
            }

            strSQL += " AND dispo.dispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("dispodesc", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("dispoimg", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("dispomarca", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("dispomdn", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("dispomeid", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("dispomodelo", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("disponume", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("DispoID", NHibernateUtil.Int32);//7

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Dispositivo oDispo = new THE_Dispositivo();
                    oDispo.DispositivoDesc = System.Convert.ToString(obj[0]);
                    oDispo.ImagenTelefono = System.Convert.ToString(obj[1]);
                    oDispo.Marca = System.Convert.ToString(obj[2]);
                    oDispo.DispositivoMdn = System.Convert.ToString(obj[3]);
                    oDispo.DispositivoMeid = System.Convert.ToString(obj[4]);
                    oDispo.Modelo = System.Convert.ToString(obj[5]);
                    oDispo.NumerodelTelefono = System.Convert.ToString(obj[6]);
                    oDispo.IdDispositivo = System.Convert.ToInt32(obj[7]);

                    lstDispoDisponibles.Add(oDispo);
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
