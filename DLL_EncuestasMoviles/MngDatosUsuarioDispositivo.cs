using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Entidades_EncuestasMoviles;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosUsuarioDispositivo
    {
        public static Boolean AsignaDispoUsuario(TDI_UsuarioDispositivo UsuaDispo)
        {
            return NHibernateHelperORACLE.SingleSessionSave<TDI_UsuarioDispositivo>(UsuaDispo);
        }

        public static IList<TDI_UsuarioDispositivo> ObtieneDispositivoPorUsuario(int IdUsuario)
        {
            List<TDI_UsuarioDispositivo> lstDispoDisponibles = new List<TDI_UsuarioDispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT dispo.ID_DISPOSITIVO disId, usua.USUA_LLAV_PR usuaLlavPr ";
            strSQL += " FROM seml_the_dispositivo dispo, ";
            strSQL += " seml_the_usuario usua, ";
            strSQL += " seml_tdi_usuariodispositivo usuadis ";
            strSQL += " WHERE usua.USUA_LLAV_PR = " + IdUsuario;
            strSQL += " AND dispo.id_dispositivo = usuadis.id_dispositivo ";
            strSQL += " AND usua.usua_llav_pr = usuadis.usua_llav_pr ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("disId", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("usuaLlavPr", NHibernateUtil.Int32);//1

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_UsuarioDispositivo oDispo = new TDI_UsuarioDispositivo();

                    oDispo.IdDispositivo = MngDatosDispositivo.ObtieneDispositivoPorID(System.Convert.ToInt32(obj[0]))[0];
                    oDispo.UsuarioLlavePrimaria = MngDatosUsuario.ObtieneUsuarioPorLlavPr(System.Convert.ToInt32(obj[1]))[0];
                    oDispo.UsuarioLlavePrimaria.EstadoInfo = MngDatosEstado.ObtieneEstadoPorCP(int.Parse(oDispo.UsuarioLlavePrimaria.UsuarioCodigoPostal))[0];

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

        public static IList<TDI_UsuarioDispositivo> ObtieneDispoUsuarioPorIdDispo(int IdDispo)
        {
            List<TDI_UsuarioDispositivo> lstDispoDisponibles = new List<TDI_UsuarioDispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT dispo.ID_DISPOSITIVO disId, usua.USUA_LLAV_PR usuaLlavPr ";
            strSQL += " FROM seml_the_dispositivo dispo, ";
            strSQL += " seml_the_usuario usua, ";
            strSQL += " seml_tdi_usuariodispositivo usuadis ";
            strSQL += " WHERE dispo.id_dispositivo = " + IdDispo;
            strSQL += " AND dispo.id_dispositivo = usuadis.id_dispositivo ";
            strSQL += " AND usua.usua_llav_pr = usuadis.usua_llav_pr ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("disId", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("usuaLlavPr", NHibernateUtil.Int32);//1

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_UsuarioDispositivo oDispo = new TDI_UsuarioDispositivo();
                    
                    oDispo.IdDispositivo = MngDatosDispositivo.ObtieneDispositivoPorID(System.Convert.ToInt32(obj[0]))[0];
                    oDispo.UsuarioLlavePrimaria = MngDatosUsuario.ObtieneUsuarioPorLlavPr(System.Convert.ToInt32(obj[1]))[0];
                    oDispo.UsuarioLlavePrimaria.EstadoInfo = MngDatosEstado.ObtieneEstadoPorCP(int.Parse(oDispo.UsuarioLlavePrimaria.UsuarioCodigoPostal))[0];

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
        public static IList<TDI_UsuarioDispositivo> ObtieneUsuariosConDispositivoAsignado()
        {
            List<TDI_UsuarioDispositivo> lstDispoDisponibles = new List<TDI_UsuarioDispositivo>();
            string strSQL = string.Empty;
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            ISession session = NHibernateHelperORACLE.GetSession();

            strSQL += " SELECT emp.usua_llav_pr usuaLlavPr, emp.usua_nombre nombre, emp.usua_apellpaterno apaterno,emp.usua_apellmaterno amaterno ";
            strSQL += " FROM seml_the_usuario emp, ";
            strSQL += " seml_the_dispositivo disp, ";
            strSQL += " seml_tdi_usuariodispositivo usuadisp ";
            strSQL += " WHERE emp.usua_llav_pr = usuadisp.usua_llav_pr ";
            strSQL += " AND disp.id_dispositivo = usuadisp.id_dispositivo ";
            strSQL += " AND emp.usua_estatus = 'A' ";
            strSQL += " AND disp.dispo_estatus = 'A' ";
            strSQL += " AND usuadisp.usuadispo_estatus = 'A' ";

            try
            {
                ISQLQuery consultaIQRY = session.CreateSQLQuery(strSQL);

                consultaIQRY.AddScalar("usuaLlavPr", NHibernateUtil.Int32);//0
                consultaIQRY.AddScalar("nombre", NHibernateUtil.String);//1
                consultaIQRY.AddScalar("apaterno", NHibernateUtil.String);//2
                consultaIQRY.AddScalar("amaterno", NHibernateUtil.String);//3

                IList lista = consultaIQRY.List();

                foreach (Object[] obj in lista)
                {
                    TDI_UsuarioDispositivo oDispo = new TDI_UsuarioDispositivo();
                    THE_Usuario user = new THE_Usuario();
                    user.UsuarioLlavePrimaria = System.Convert.ToInt32(obj[0]);
                    user.UsuarioNombre = System.Convert.ToString(obj[1]) + " " + System.Convert.ToString(obj[2]) + " " + System.Convert.ToString(obj[3]);
                    oDispo.UsuarioLlavePrimaria = user;                    
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
        public static Boolean EliminaDispoUsuario(TDI_UsuarioDispositivo UsuaDispo)
        {
            return NHibernateHelperORACLE.SingleSessionDelete<TDI_UsuarioDispositivo>(UsuaDispo);
        }
        public static Boolean EliminaUserDispo(TDI_UsuarioDispositivo UsuaDispo)
        {
            return NHibernateHelperORACLE.SingleSessionUpdate<TDI_UsuarioDispositivo>(UsuaDispo);
        }
    }
}
