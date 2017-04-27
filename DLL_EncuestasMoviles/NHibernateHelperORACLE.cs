using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using NHibernate;
using Azteca.Utility.Security;
using System.Configuration;
using Microsoft.Win32;
using System.IO;
using com.dsi.pgp;

namespace DLL_EncuestasMoviles
{
    public sealed class NHibernateHelperORACLE
    {
        #region Variables
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory _sessionFactory;
        private static ISession session;
        #endregion

        static NHibernateHelperORACLE()
        {   
            NHibernate.Cfg.Configuration cfg = new NHibernate.Cfg.Configuration();
            Rijndael _ChyperRijndael = new Rijndael();

            /* encriptado BD */         
            string nombreLlave = "app_encuestas_moviles";

            //Modificar para que apunte a la llave privada, ya sea en una ruta del servidor o en el registro de windows.
            string LlavePublica = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlavePublica"], enmTransformType.intDecrypt);
            string LlavePrivada = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlavePrivada"], enmTransformType.intDecrypt);
                        
            //Modificar para que apunte al passphrase almacenado en el resgistro de windows, no almacenar en el disco duro, sólo en el registro de windows.
            string passphrase = "";
            try
            {
                passphrase = (string)Registry.LocalMachine.OpenSubKey("SOFTWARE\\" + nombreLlave).GetValue("passphrase");
            }
            catch
            {
                passphrase = (string)Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\" + nombreLlave).GetValue("passphrase");                
            }

            string error = "";
            string cadena = "";
            try
            {
                Stream desencriptado = PGPUtil.DesencriptarTexto(File.OpenRead(LlavePublica), File.OpenRead(LlavePrivada), null, passphrase.ToCharArray()).Datos;
                using (StreamReader reader = new StreamReader(desencriptado))
                {
                    string contents = reader.ReadToEnd();
                    cadena = contents;                    
                }
                cadena = "Data Source=ENCUEST_TVAQA;User Id=encuestas_moviles;Password=F6_G66#NG6_GF573F";
            }
            catch(Exception ex)
            {
                cadena = "Data Source=" + _ChyperRijndael.Transmute(System.Configuration.ConfigurationManager.AppSettings["cnAztMusInstance"], enmTransformType.intDecrypt) +
                            ";User Id=" + _ChyperRijndael.Transmute(System.Configuration.ConfigurationManager.AppSettings["cnAztMusUser"], enmTransformType.intDecrypt) +
                            ";Password=" + _ChyperRijndael.Transmute(System.Configuration.ConfigurationManager.AppSettings["cnAztMusPwd"], enmTransformType.intDecrypt);

                //cadena = "Data Source=ENCUEST_TVAQA" +
                //            ";User Id=" + _ChyperRijndael.Transmute(System.Configuration.ConfigurationManager.AppSettings["cnAztMusUser"], enmTransformType.intDecrypt) +
                //            ";Password=" + _ChyperRijndael.Transmute(System.Configuration.ConfigurationManager.AppSettings["cnAztMusPwd"], enmTransformType.intDecrypt);

                cadena = "Data Source=ENCUEST_TVAQA;User Id=encuestas_moviles;Password=F6_G66#NG6_GF573F";
                error = ex.Message.ToString() + " -> " + ex.StackTrace.ToString();
            }
            
            try
            {
                //var con = new Oracle.DataAccess.Client.OracleConnection(cadena);//
                var con= new System.Data.OracleClient.OracleConnection(cadena);
                //OracleConnection con = new OracleConnection(cadena);
                con.Open();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            
            //cfg.Properties.Add(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.Oracle9iDialect");        // 
            cfg.Properties.Add(NHibernate.Cfg.Environment.Dialect, "NHibernate.Dialect.Oracle9iDialect");
            cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionDriver, "NHibernate.Driver.OracleClientDriver");                
            cfg.Properties.Add(NHibernate.Cfg.Environment.ProxyFactoryFactoryClass, "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");                
            cfg.Properties.Add(NHibernate.Cfg.Environment.ConnectionString, cadena);
            cfg.AddAssembly("DLL_EncuestasMoviles");
            cfg.Configure();                
            _sessionFactory = cfg.BuildSessionFactory();            
        }

        public static ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        public static Boolean SingleSessionSave<T>(T entity)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Save(entity);
                transaction.Commit();
                session.Close();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                transaction.Rollback();
                session.Close();
                return false;
            }
        }

        public static T SingleSessionSave<T>(T entity, string WhatEver)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Save(entity);
                transaction.Commit();
                session.Close();
                return entity;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                session.Close();
                return entity;
            }
        }

        public static T SingleSessionSave<T>(T entity, string WhatEver, ISession session)
        {

            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Save(entity);
                transaction.Commit();
                return entity;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                return entity;
            }
        }

        public static bool SingleSessionSave<T>(T entity, ISession session)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Save(entity);
                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                session.Close();
                return false;
            }
        }

        public static T SingleSessionSaveT<T>(T entity, ISession session)
        {
            session.Save(entity);
            return entity;
        }

        public static T SingleSessionSaveT<T>(T entity, ISession session, ITransaction trans)
        {
            try
            {
                session.Save(entity);
                trans.Commit();
                return entity;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                return entity;
            }
        }

        public static Boolean SingleSessionUpdate<T>(T entity)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Update(entity);
                transaction.Commit();
                session.Close();
                return true;
            }
            catch
            {
                transaction.Rollback();
                session.Close();
                return false;
            }
        }

        public static Boolean SingleSessionUpdate<T>(T entity, ISession session)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Update(entity);
                transaction.Commit();
                session.Close();
                return true;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                session.Close();
                return false;
            }
        }

        public static Boolean SingleSessionDelete<T>(T entity)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Delete(entity);
                transaction.Commit();
                session.Close();
                return true;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                session.Close();
                return false;
            }
        }

        public static void SingleSessionDelete<T>(T entity, ISession session)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.Delete(entity);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
            }
        }

        public static Boolean SingleSessionSaveOrUpdate<T>(T entity)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.SaveOrUpdate(entity);
                transaction.Commit();
                session.Close();
                return true;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                session.Close();
                return false;
            }
        }

        public static T SingleSessionSaveOrUpdate<T>(T entity, string Whetever)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.SaveOrUpdate(entity);
                transaction.Commit();
                session.Close();
                return entity;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                session.Close();
                return entity;
            }

        }

        public static T SingleSessionSaveOrUpdate<T>(T entity, ISession session)
        {
            ITransaction transaction = session.BeginTransaction();
            try
            {
                session.SaveOrUpdate(entity);
                transaction.Commit();
                return entity;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                return entity;
            }

        }

        public static bool SingleSessionSaveOrUpdateBool<T>(T entity, ISession session)
        {
            ITransaction transaction = session.BeginTransaction();

            try
            {
                session.SaveOrUpdate(entity);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                transaction.Rollback();
                return false;
            }

        }

        public static T SingleSessionLoad<T, K>(K key)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            T entity = session.Load<T>(key);
            transaction.Commit();
            session.Close();

            return entity;
        }

        public static T SingleSessionGet<T, K>(K key)
        {
            ISession session = GetSession();
            ITransaction transaction = session.BeginTransaction();
            T entity = session.Get<T>(key);
            transaction.Commit();
            session.Close();

            return entity;
        }

        public static IList<T> SingleSessionFind<T>(string queryString)
        {
            IList<T> list = new List<T>();
            try
            {
                ISession session = GetSession();
                IQuery query = session.CreateQuery(queryString);
                list = query.List<T>();
                session.Close();
                return list;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return list;

            }
            finally
            {
                if (session != null)
                    session.Close();
            }
        }

        public static IList<T> SingleSessionFind<T>(string queryString, ISession session)
        {
            IList<T> list = new List<T>();
            try
            {
                IQuery query = session.CreateQuery(queryString);
                list = query.List<T>();
                return list;
            }
            catch (Exception ex)
            {
                MngDatosLogErrores.GuardaError(ex, "NHibernateHelperORACLE");
                return list;
            }
        }

        public static IList<T> SingleSessionFind<T>(string queryString, DateTime[] arregloFechas)
        {
            IList<T> list = new List<T>();


            try
            {
                ISession session = GetSession();
                IQuery query = session.CreateQuery(queryString);
                int i = 0;
                foreach (DateTime item in arregloFechas)
                {
                    query.SetDateTime("fecha_" + i.ToString(), item);
                    i++;
                }

                list = query.List<T>();
                session.Close();
                return list;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (session != null)
                    session.Close();
            }
        }

        public static void CloseSessionFactory()
        {
            if (_sessionFactory != null)
            {
                _sessionFactory.Close();
            }
        }

        #region Conexion a Oracle sin Nhibernate

        #region Decodificacion
        /// <summary>
        /// Metodo para extraer el password del archivo de configuracion
        /// y desencriptarlo
        /// </summary>
        /// <returns>Password desencriptado</returns>
        public static string GetDBPass()
        {
            Rijndael _ChyperRijndael = new Rijndael();
            AppSettingsReader appRdr = new AppSettingsReader();
            return _ChyperRijndael.Transmute(appRdr.GetValue("cnAztMusPwd", typeof(string)).ToString(), enmTransformType.intDecrypt);
        }

        /// <summary>
        /// Metodo para extraer el usuario del archivo de configuracion
        /// y desencriptarlo
        /// </summary>
        /// <returns>Usuario desencriptado</returns>
        public static string GetDBUser()
        {
            Rijndael _ChyperRijndael = new Rijndael();
            AppSettingsReader appRdr = new AppSettingsReader();
            return _ChyperRijndael.Transmute(appRdr.GetValue("cnAztMusUser", typeof(string)).ToString(), enmTransformType.intDecrypt);
        }

        /// <summary>
        /// Metodo para extraer el nombre del server del archivo de configuracion
        /// y desencriptarlo
        /// </summary>
        /// <returns>Nombre del Server desencriptado</returns>
        public static string GetDBInstance()
        {
            Rijndael _ChyperRijndael = new Rijndael();
            AppSettingsReader appRdr = new AppSettingsReader();
            return _ChyperRijndael.Transmute(appRdr.GetValue("cnAztMusInstance", typeof(string)).ToString(), enmTransformType.intDecrypt);
        }
        #endregion

        /// <summary>
        /// Inicializa la variable de conexion
        /// </summary>
        private static string SetConSrt()
        {
            return "Data Source = " + GetDBInstance() + "; " +
                       "User Id = " + GetDBUser() + "; Password = " + GetDBPass() + "; ";
        }

        /// <summary>
        /// Atributo que inicializa la conexion
        /// </summary>
        public static OracleConnection Conexion()
        {
            try
            {
                OracleConnection conn = new OracleConnection(SetConSrt());
                return conn;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo para Actualizar la BD, mediante un SP y sus parametros
        /// </summary>
        /// <param name="cmd">Stored Procedure</param>
        /// <param name="param">Parametros</param>
        public static int ExecuteNonQuery(string cmd, OracleParameter[] param)
        {
            OracleConnection conn = Conexion();
            try
            {
                ExecuteNonQuery(cmd, param, conn);
                return 1;
            }
            catch
            {
                return 0;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }

        /// <summary>
        /// Metodo para Actualizar la BD, mediante un SP y sus parametros
        /// </summary>
        /// <param name="cmd">Stored Procedure</param>
        /// <param name="param">Parametros</param>
        /// 
        public static void ExecuteNonQuery(string cmd, OracleParameter[] param, OracleConnection Connection)
        {
            OracleCommand command = new OracleCommand(cmd, Connection);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                for (int j = 0; j < param.Length; j++)
                    command.Parameters.Add(param[j]);

                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            finally
            {
                command.Connection.Close();
                command.Dispose();
                command = null;
            }
        }

        /// <summary>
        /// Metodo para Actualizar la BD, mediante un SP y sus parametros
        /// </summary>
        /// <param name="cmd">Stored Procedure</param>
        /// <param name="param">Parametros</param>
        /// 
        public static int ExecuteNonQuery(string cmd, OracleParameter[] param, int salida)
        {
            OracleConnection conn = Conexion();
            OracleCommand command = new OracleCommand(cmd, conn);
            try
            {
                command.CommandType = CommandType.StoredProcedure;
                for (int j = 0; j < param.Length; j++)
                    command.Parameters.Add(param[j]);

                command.Connection.Open();
                command.ExecuteNonQuery();
                return int.Parse(param[salida].Value.ToString());
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }
            finally
            {
                conn.Dispose();
                conn = null;
                command.Connection.Close();
                command.Dispose();
                command = null;
            }

        }

        #endregion
    }
}
