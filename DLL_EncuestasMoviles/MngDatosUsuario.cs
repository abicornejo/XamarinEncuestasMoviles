using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosUsuario
    {
        public static int GuardaAltaUsuario(THE_Usuario usua)
        {
                try 
                {	    
                    NHibernateHelperORACLE.SingleSessionSave<THE_Usuario>(usua);
                    return usua.UsuarioLlavePrimaria;		
                }
                catch (Exception)
                {		
                return -1;
                }
        }

        public static IList<THE_Usuario> ObtieneTodosUsuarios()
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Usuario Usuario WHERE USUA_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Usuario>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosUsuario");
                return new List<THE_Usuario>();
            }
        }

        public static IList<THE_Usuario> BuscaUsuariosEsp(THE_Usuario usuario, List<TDI_OpcionCat> listOpCat)
        {
            #region Query Armado
            List<THE_Usuario> listaUsuario = new List<THE_Usuario>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            try
            {
                //calcula valores
                int anio1, anio2, da, ma, aa;
                da = int.Parse(DateTime.Today.ToString("dd"));
                ma = int.Parse(DateTime.Today.ToString("MM"));
                aa = int.Parse(DateTime.Today.ToString("yyyy"));
                string idOpciones = "";
                int id=0;
                //Obtener idOpciones
                foreach (TDI_OpcionCat opCat in listOpCat)
                {
                    id=id+1;
                    if (id < listOpCat.Count)
                        idOpciones += opCat.IdOpcionCat + ",";
                    else
                        idOpciones += opCat.IdOpcionCat;
                }

                strSQL += " SELECT usu.usua_llav_pr, usu.usua_nombre, usu.usua_apellpaterno, usu.usua_apellmaterno, ";
                strSQL += " usu.usua_fechnacimiento, usu.usua_fotourl, usu.usua_email, usu.usua_callenum, ";
                strSQL += " usu.usua_telcasa, usu.usua_numcelpersonal, usu.usua_obse, usu.usua_estatus, ";
                strSQL += " usu.id_colonia, usu.usua_cp, usu.sexo ";
                strSQL += " FROM seml_the_usuario usu ";
                strSQL += " WHERE usu.usua_llav_pr in ( ";
                strSQL += " SELECT distinct (usucat.usua_llav_pr) ";
                strSQL += " FROM seml_tdi_opcioncat opc, seml_tdi_usuariocat usucat ";
                strSQL += " WHERE usucat.id_opcioncat = opc.id_opcioncat ";
                strSQL += " AND usucat.id_opcioncat in ( " + idOpciones + " ) " ;
                strSQL += " AND opc.opcioncat_stat='A' ) ";
                strSQL += " AND usu.usua_estatus='A' ";
                strSQL += " AND usu.sexo = '" + usuario.UsuarioSexo + "' ";
                if (usuario.UsuarioNombre != "")
                    strSQL += " AND UPPER(usu.usua_nombre) like UPPER('%" + usuario.UsuarioNombre + "%')";
                if (usuario.UsuarioApellPaterno != "")
                    strSQL += " AND UPPER(usu.usua_apellpaterno) like UPPER('%" + usuario.UsuarioApellPaterno + "%')";
                if (usuario.UsuarioApellMaterno != "")
                    strSQL += " AND UPPER(usu.usua_apellmaterno) like UPPER('%" + usuario.UsuarioApellMaterno + "%')";
                
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("usua_llav_pr", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("usua_nombre", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("usua_apellpaterno", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("usua_apellmaterno", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("usua_fechnacimiento", NHibernateUtil.String);//4               
                consultaIQRY.AddScalar("usua_fotourl", NHibernateUtil.String);//5               
                consultaIQRY.AddScalar("usua_email", NHibernateUtil.String);//6               
                consultaIQRY.AddScalar("usua_callenum", NHibernateUtil.String);//7               
                consultaIQRY.AddScalar("usua_telcasa", NHibernateUtil.String);//8               
                consultaIQRY.AddScalar("usua_numcelpersonal", NHibernateUtil.String);//9               
                consultaIQRY.AddScalar("usua_obse", NHibernateUtil.String);//10               
                consultaIQRY.AddScalar("usua_estatus", NHibernateUtil.Character);//11               
                consultaIQRY.AddScalar("id_colonia", NHibernateUtil.Int32);//12               
                consultaIQRY.AddScalar("usua_cp", NHibernateUtil.String);//13               
                consultaIQRY.AddScalar("sexo", NHibernateUtil.Character);//14               

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Usuario oUsuario = new THE_Usuario();
                    oUsuario.UsuarioLlavePrimaria = System.Convert.ToInt32(obj[0]);
                    oUsuario.UsuarioNombre = System.Convert.ToString(obj[1]);
                    oUsuario.UsuarioApellPaterno = System.Convert.ToString(obj[2]);
                    oUsuario.UsuarioApellMaterno = System.Convert.ToString(obj[3]);
                    oUsuario.UsuarioFechNacimiento = System.Convert.ToString(obj[4]);
                    oUsuario.UsuarioFoto = System.Convert.ToString(obj[5]);
                    oUsuario.UsuarioEmail = System.Convert.ToString(obj[6]);
                    oUsuario.UsuarioCalleNum = System.Convert.ToString(obj[7]);
                    oUsuario.UsuarioTelCasa = System.Convert.ToString(obj[8]);
                    oUsuario.UsuarioNumCelularPersonal = System.Convert.ToString(obj[9]);
                    oUsuario.UsuarioObse = System.Convert.ToString(obj[10]);
                    oUsuario.UsuarioEstatus = System.Convert.ToChar(obj[11]);
                    oUsuario.IdColonia = new TDI_Colonias { IdColonia = System.Convert.ToInt32(obj[12]) };
                    oUsuario.UsuarioCodigoPostal = System.Convert.ToString(obj[13]);
                    oUsuario.UsuarioSexo = System.Convert.ToChar(obj[14]);
                    listaUsuario.Add(oUsuario);
                }
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosUsuario");
                listaUsuario = null;
                return listaUsuario;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaUsuario;
            #endregion     
        }

        public static IList<THE_Usuario> BuscaUsuarios(THE_Usuario usuario)
        {
            #region Query Armado
            List<THE_Usuario> listaUsuario = new List<THE_Usuario>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            try
            {
                //calcula valores
                int anio1, anio2, da, ma, aa;
                da = int.Parse(DateTime.Today.ToString("dd"));
                ma = int.Parse(DateTime.Today.ToString("MM"));
                aa = int.Parse(DateTime.Today.ToString("yyyy"));
                                                
                strSQL += " SELECT usuario.usua_llav_pr, usuario.usua_nombre, usuario.usua_apellpaterno, usuario.usua_apellmaterno, ";
                strSQL += " usuario.usua_fechnacimiento, usuario.usua_fotourl, usuario.usua_email, usuario.usua_callenum, ";
                strSQL += " usuario.usua_telcasa, usuario.usua_numcelpersonal, usuario.usua_obse, usuario.usua_estatus,  ";
                strSQL += " usuario.id_colonia, usuario.usua_cp, usuario.sexo ";
                strSQL += " FROM seml_the_usuario usuario ";
                strSQL += " WHERE usuario.usua_estatus = 'A'" ;
                if(usuario.UsuarioSexo != 'T')
                    strSQL += " AND usuario.sexo = '" + usuario.UsuarioSexo + "' ";
                if (usuario.UsuarioNombre != "")
                    strSQL += " AND UPPER(usuario.usua_nombre) like UPPER('%" + usuario.UsuarioNombre + "%')";
                if(usuario.UsuarioApellPaterno != "")
                    strSQL += " AND UPPER(usuario.usua_apellpaterno) like UPPER('%" + usuario.UsuarioApellPaterno + "%')";
                if(usuario.UsuarioApellMaterno != "")
                    strSQL += " AND UPPER(usuario.usua_apellmaterno) like UPPER('%" + usuario.UsuarioApellMaterno + "%')";
            
 
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("usua_llav_pr", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("usua_nombre", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("usua_apellpaterno", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("usua_apellmaterno", NHibernateUtil.String);//3
                consultaIQRY.AddScalar("usua_fechnacimiento", NHibernateUtil.String);//4               
                consultaIQRY.AddScalar("usua_fotourl", NHibernateUtil.String);//5               
                consultaIQRY.AddScalar("usua_email", NHibernateUtil.String);//6               
                consultaIQRY.AddScalar("usua_callenum", NHibernateUtil.String);//7               
                consultaIQRY.AddScalar("usua_telcasa", NHibernateUtil.String);//8               
                consultaIQRY.AddScalar("usua_numcelpersonal", NHibernateUtil.String);//9               
                consultaIQRY.AddScalar("usua_obse", NHibernateUtil.String);//10               
                consultaIQRY.AddScalar("usua_estatus", NHibernateUtil.Character);//11               
                consultaIQRY.AddScalar("id_colonia", NHibernateUtil.Int32);//12               
                consultaIQRY.AddScalar("usua_cp", NHibernateUtil.String);//13               
                consultaIQRY.AddScalar("sexo", NHibernateUtil.Character);//14               

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    THE_Usuario oUsuario = new THE_Usuario();
                    oUsuario.UsuarioLlavePrimaria = System.Convert.ToInt32(obj[0]);
                    oUsuario.UsuarioNombre = System.Convert.ToString(obj[1]);
                    oUsuario.UsuarioApellPaterno = System.Convert.ToString(obj[2]);
                    oUsuario.UsuarioApellMaterno = System.Convert.ToString(obj[3]);
                    oUsuario.UsuarioFechNacimiento = System.Convert.ToString(obj[4]);                                  
                    oUsuario.UsuarioFoto = System.Convert.ToString(obj[5]);
                    oUsuario.UsuarioEmail = System.Convert.ToString(obj[6]);
                    oUsuario.UsuarioCalleNum = System.Convert.ToString(obj[7]);
                    oUsuario.UsuarioTelCasa = System.Convert.ToString(obj[8]);
                    oUsuario.UsuarioNumCelularPersonal = System.Convert.ToString(obj[9]);
                    oUsuario.UsuarioObse = System.Convert.ToString(obj[10]);
                    oUsuario.UsuarioEstatus = System.Convert.ToChar(obj[11]);
                    oUsuario.IdColonia = new TDI_Colonias { IdColonia = System.Convert.ToInt32(obj[12]) };
                    oUsuario.UsuarioCodigoPostal = System.Convert.ToString(obj[13]);
                    oUsuario.UsuarioSexo = System.Convert.ToChar(obj[14]);
                    listaUsuario.Add(oUsuario);
                }
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosUsuario");
                listaUsuario = null;
                return listaUsuario;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaUsuario;
            #endregion     
        }

        public static IList<THE_Usuario> BuscaUsuarios2(THE_Usuario usuario, string Catalogos)
        {
            #region Query Armado
            List<THE_Usuario> listaUsuario = new List<THE_Usuario>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            try
            {
                
                    strSQL += " SELECT DISPO.DISPO_DESCRIPCION, ";
                    strSQL += " USR.USUA_LLAV_PR, ";
                    strSQL += " OPTUSRCAT.OPCIONCAT_DESC, ";
                    strSQL += " OPTUSRCAT.ID_OPCIONCAT, ";
                    strSQL += " USR.USUA_NOMBRE, ";
                    strSQL += " USR.USUA_APELLPATERNO, ";
                    strSQL += " USR.USUA_APELLMATERNO, ";
                    strSQL += " USR.USUA_FECHNACIMIENTO, ";
                    strSQL += " USR.USUA_FOTOURL, ";
                    strSQL += " USR.USUA_EMAIL, ";
                    strSQL += " USR.USUA_CALLENUM, ";
                    strSQL += " USR.USUA_TELCASA, ";
                    strSQL += " USR.USUA_NUMCELPERSONAL, ";
                    strSQL += " USR.USUA_OBSE, ";
                    strSQL += " USR.USUA_ESTATUS, ";
                    strSQL += " USR.ID_COLONIA, ";
                    strSQL += " USR.USUA_CP, ";
                    strSQL += " USR.SEXO, ";
                    strSQL += " DISPO.ID_DISPOSITIVO, DISPO.DISPO_NUMTELEFONO, DISPO.DISPO_MODELO, DISPO.DISPO_MARCA, DISPO.DISPO_MEID, DISPO.DISPO_DESCRIPCION ";
                    strSQL += " FROM SEML_THE_USUARIO USR, ";
                    strSQL += " SEML_TDI_USUARIOCAT USRCAT, ";
                    strSQL += " SEML_TDI_OPCIONCAT OPTUSRCAT, ";
                    strSQL += " SEML_THE_CATALOGO CAT, ";
                    strSQL += " SEML_THE_DISPOSITIVO DISPO, ";
                    strSQL += " SEML_TDI_USUARIODISPOSITIVO USRDISPO ";
                    strSQL += " WHERE     USR.USUA_LLAV_PR = USRCAT.USUA_LLAV_PR ";
                    strSQL += " AND USRCAT.ID_OPCIONCAT = OPTUSRCAT.ID_OPCIONCAT ";
                    strSQL += " AND CAT.ID_CATALOGO = OPTUSRCAT.ID_CATALOGO ";
                    strSQL += " AND USRDISPO.ID_DISPOSITIVO=DISPO.ID_DISPOSITIVO ";
                    strSQL += " AND USR.USUA_LLAV_PR=USRDISPO.USUA_LLAV_PR ";
                    strSQL += " AND USR.USUA_ESTATUS = 'A' ";
                    strSQL += " AND USRCAT.USUACAT_STAT = 'A' ";
                    strSQL += " AND OPTUSRCAT.OPCIONCAT_STAT = 'A'  ";
                    strSQL += " AND CAT.CATALOGO_STAT = 'A'  ";
                    strSQL += " AND DISPO.DISPO_ESTATUS='A' ";
                    strSQL += " AND USRDISPO.USUADISPO_ESTATUS ='A'    ";
                    if (usuario.UsuarioSexo != 'T' && usuario.UsuarioSexo.ToString() != "" && usuario.UsuarioSexo != '\0' && usuario.UsuarioSexo != '0')
                        strSQL += " AND USR.sexo = '" + usuario.UsuarioSexo + "' ";
                    if (usuario.UsuarioNombre != "" && usuario.UsuarioNombre !=null)
                        strSQL += " AND UPPER(USR.usua_nombre) like UPPER('%" + usuario.UsuarioNombre + "%') ";
                    if (usuario.UsuarioApellPaterno != "" && usuario.UsuarioApellPaterno !=null)
                        strSQL += " AND UPPER(USR.usua_apellpaterno) like UPPER('%" + usuario.UsuarioApellPaterno + "%') ";
                    if (usuario.UsuarioApellMaterno != "" && usuario.UsuarioApellMaterno !=null)
                        strSQL += " AND UPPER(USR.usua_apellmaterno) like UPPER('%" + usuario.UsuarioApellMaterno + "%') ";
                    if (Catalogos != "")
                        strSQL += " AND  OPTUSRCAT.ID_OPCIONCAT IN (" + Catalogos + " ) ";

                    strSQL += " ORDER BY USR.USUA_LLAV_PR,OPTUSRCAT.ID_OPCIONCAT ";
 
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);
                consultaIQRY.AddScalar("DISPO_DESCRIPCION", NHibernateUtil.String);//0
                consultaIQRY.AddScalar("USUA_LLAV_PR", NHibernateUtil.Int32);//1
                consultaIQRY.AddScalar("OPCIONCAT_DESC", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("ID_OPCIONCAT", NHibernateUtil.Int32);//3
                consultaIQRY.AddScalar("USUA_NOMBRE", NHibernateUtil.String);//4
                consultaIQRY.AddScalar("USUA_APELLPATERNO", NHibernateUtil.String);//5
                consultaIQRY.AddScalar("USUA_APELLMATERNO", NHibernateUtil.String);//6
                consultaIQRY.AddScalar("USUA_FECHNACIMIENTO", NHibernateUtil.String);//7               
                consultaIQRY.AddScalar("USUA_FOTOURL", NHibernateUtil.String);//8               
                consultaIQRY.AddScalar("USUA_EMAIL", NHibernateUtil.String);//9               
                consultaIQRY.AddScalar("USUA_CALLENUM", NHibernateUtil.String);//10               
                consultaIQRY.AddScalar("USUA_TELCASA", NHibernateUtil.String);//11               
                consultaIQRY.AddScalar("USUA_NUMCELPERSONAL", NHibernateUtil.String);//12              
                consultaIQRY.AddScalar("USUA_OBSE", NHibernateUtil.String);//13     
                consultaIQRY.AddScalar("USUA_ESTATUS", NHibernateUtil.Character);//14               
                consultaIQRY.AddScalar("ID_COLONIA", NHibernateUtil.Int32);//15               
                consultaIQRY.AddScalar("USUA_CP", NHibernateUtil.String);//16               
                consultaIQRY.AddScalar("SEXO", NHibernateUtil.Character);//17  
                consultaIQRY.AddScalar("ID_DISPOSITIVO", NHibernateUtil.Character);//18 DISPO., DISPO., DISPO., DISPO.

                consultaIQRY.AddScalar("DISPO_NUMTELEFONO", NHibernateUtil.String);//19 
                consultaIQRY.AddScalar("DISPO_MODELO", NHibernateUtil.String);//20
                consultaIQRY.AddScalar("DISPO_MARCA", NHibernateUtil.String);//21 
                consultaIQRY.AddScalar("DISPO_MEID", NHibernateUtil.String);//22
                consultaIQRY.AddScalar("DISPO_DESCRIPCION", NHibernateUtil.String);//23

                IList lista = consultaIQRY.List();
              
                List<string> catalogo=new List<string>();
                string cadena = "";
                int idUsr = 0;
                foreach (Object[] obj in lista)
                {
                    if (System.Convert.ToInt32(obj[1]) != idUsr)
                    {
                        idUsr = System.Convert.ToInt32(obj[1]);
                        cadena += idUsr + ":" + System.Convert.ToString(obj[2]) + ";";
                    }
                    else
                    {
                        cadena = cadena.Substring(0, cadena.Length - 1);
                        cadena += "|" + System.Convert.ToString(obj[2]) + ";";
                    }
                    
                }

                cadena = cadena.Substring(0, cadena.Length - 1);

                string[] arreglo = cadena.Split(';');

                foreach (var datos in arreglo)
                {
                    int idUser = System.Convert.ToInt32(datos.Split(':')[0]);
                    string catalogUser = datos.Split(':')[1];
                    foreach (Object[] obj in lista)
                    {
                        if (System.Convert.ToInt32(obj[1]) == idUser) {

                            THE_Usuario oUsuario = new THE_Usuario();
                            oUsuario.DescDispositivo = System.Convert.ToString(obj[0]);
                            oUsuario.Catalogos = catalogUser;                            
                            oUsuario.UsuarioLlavePrimaria = idUser;
                            oUsuario.UsuarioNombre =System.Convert.ToString(obj[4]);
                            oUsuario.UsuarioApellPaterno =  System.Convert.ToString(obj[5]);
                            oUsuario.UsuarioApellMaterno = System.Convert.ToString(obj[6]);
                            oUsuario.UsuarioFechNacimiento = System.Convert.ToString(obj[7]);
                            oUsuario.UsuarioFoto =System.Convert.ToString(obj[8]);
                            oUsuario.UsuarioEmail =System.Convert.ToString(obj[9]);
                            oUsuario.UsuarioCalleNum = System.Convert.ToString(obj[10]);
                            oUsuario.UsuarioTelCasa = System.Convert.ToString(obj[11]);
                            oUsuario.UsuarioNumCelularPersonal = System.Convert.ToString(obj[12]);
                            oUsuario.UsuarioObse = System.Convert.ToString(obj[13]);
                            oUsuario.UsuarioEstatus = System.Convert.ToChar(obj[14]);
                            oUsuario.IdColonia = new TDI_Colonias { IdColonia = System.Convert.ToInt32(obj[15]) };
                            oUsuario.UsuarioCodigoPostal = System.Convert.ToString(obj[16]);
                            oUsuario.UsuarioSexo = System.Convert.ToChar(obj[17]);
                            oUsuario.IdDisposisito = System.Convert.ToInt32(obj[18]);

                            oUsuario.DispoTelefono = System.Convert.ToString(obj[19]);
                            oUsuario.DispoModelo = System.Convert.ToString(obj[20]);
                            oUsuario.DispoMarca = System.Convert.ToString(obj[21]);
                            oUsuario.DispoMeid = System.Convert.ToString(obj[22]);
                            oUsuario.DispoDesc = System.Convert.ToString(obj[23]);



                            listaUsuario.Add(oUsuario);
                            break;
                        }                    
                    }
                   
                }
              
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosUsuario");
                listaUsuario = null;
                return listaUsuario;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }

            return listaUsuario;
            #endregion     
        }


        public static Boolean ActualizaUsuario(THE_Usuario usua)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Usuario>(usua);
        }

        public static Boolean EliminaUsuario(THE_Usuario usua)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_Usuario>(usua);
        }

        public static IList<THE_Usuario> ObtieneUsuarioPorLlavPr(int LlavPr)
        {
            try
            {
                string strQuery = string.Empty;
                strQuery = "FROM THE_Usuario Usuario WHERE USUA_LLAV_PR = " + LlavPr + " " + " AND USUA_ESTATUS = 'A'";
                return NHibernateHelperORACLE.SingleSessionFind<THE_Usuario>(strQuery);
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosUsuario");
                return new List<THE_Usuario>();
            }
        }
    }
}
