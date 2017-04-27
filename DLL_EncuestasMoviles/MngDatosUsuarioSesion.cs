using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosUsuarioSesion
    {
        public static Boolean GuardaSesionUsuario(THE_SesionUsuario usuario)
        {
            return NHibernateHelperORACLE.SingleSessionSave<THE_SesionUsuario>(usuario);
        }
        public static IList<THE_SesionUsuario> VerExisteSesionUsuario(int idusuario, string ipMaquina)
        {
            try
            {
                ISession session = NHibernateHelperORACLE.GetSession();
                List<THE_SesionUsuario> lstUsuarioSesion = new List<THE_SesionUsuario>();
              
                string strSQL = string.Empty;              
              
                strSQL += " SELECT DISTINCT usesion.empl_llav_pr idusuario, ";
                strSQL += " usesion.ip_usr ipuser, usesion.id_sesion idsesion";               
                strSQL += " FROM seml_the_sesion_usuario usesion ";
                if (idusuario.ToString() != "")
                {
                    strSQL += " WHERE usesion.empl_llav_pr=" + idusuario;
                }
                if (ipMaquina != "")
                {
                    strSQL += " AND usesion.ip_usr='" + ipMaquina + "' "; 
                } 

                try {

                    ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                    consultaIQRY.AddScalar("idusuario", NHibernateUtil.Int32);//0
                    consultaIQRY.AddScalar("ipuser", NHibernateUtil.String);//1   
                    consultaIQRY.AddScalar("idsesion", NHibernateUtil.Int32);

                    IList lista = consultaIQRY.List();

                    foreach (Object[] obj in lista)
                    {
                        THE_SesionUsuario oDispo = new THE_SesionUsuario();
                        oDispo.EmplLlavPr = new THE_Empleado { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                        oDispo.DirIP = System.Convert.ToString(obj[1]);
                        oDispo.IdSesion = System.Convert.ToInt32(obj[2]);
                        lstUsuarioSesion.Add(oDispo);
                    }
                }
                catch(Exception Ms){                
                    lstUsuarioSesion = null;
                    return lstUsuarioSesion;                
                }finally{               
                    session.Close();
                    session.Dispose();
                    session = null;                
                }
                return lstUsuarioSesion;

                
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosUsuarioSesion");
                return new List<THE_SesionUsuario>();
            }
        }

        public static IList<THE_SesionUsuario> ExisteSesionUsuario(int idusuario)
        {
            try
            {
                ISession session = NHibernateHelperORACLE.GetSession();
                List<THE_SesionUsuario> lstUsuarioSesion = new List<THE_SesionUsuario>();
              
                string strSQL = string.Empty;              
              
                strSQL += " SELECT DISTINCT usesion.empl_llav_pr idusuario, ";
                strSQL += " usesion.ip_usr ipuser, usesion.id_sesion idsesion";               
                strSQL += " FROM seml_the_sesion_usuario usesion ";
                if (idusuario.ToString() != "")
                {
                    strSQL += " WHERE usesion.empl_llav_pr=" + idusuario;
                }

                try {

                    ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                    consultaIQRY.AddScalar("idusuario", NHibernateUtil.Int32);//0
                    consultaIQRY.AddScalar("ipuser", NHibernateUtil.String);//1   
                    consultaIQRY.AddScalar("idsesion", NHibernateUtil.Int32);

                    IList lista = consultaIQRY.List();

                    foreach (Object[] obj in lista)
                    {
                        THE_SesionUsuario oDispo = new THE_SesionUsuario();
                        oDispo.EmplLlavPr = new THE_Empleado { EmpleadoLlavePrimaria = System.Convert.ToInt32(obj[0]) };
                        oDispo.DirIP = System.Convert.ToString(obj[1]);
                        oDispo.IdSesion = System.Convert.ToInt32(obj[2]);
                        lstUsuarioSesion.Add(oDispo);
                    }
                }
                catch(Exception Ms){                
                    lstUsuarioSesion = null;
                    return lstUsuarioSesion;                
                }finally{               
                    session.Close();
                    session.Dispose();
                    session = null;                
                }
                return lstUsuarioSesion;

                
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "MngDatosUsuarioSesion");
                return new List<THE_SesionUsuario>();
            }
        }

        public static Boolean ActualizaSesionUsuario(THE_SesionUsuario usuario)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<THE_SesionUsuario>(usuario);
        }

        public static Boolean EliminaSesionUsuario(THE_SesionUsuario usuario)
        {
            return NHibernateHelperORACLE.SingleSessionDelete<THE_SesionUsuario>(usuario);
        }
    }
}
