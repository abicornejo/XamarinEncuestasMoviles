using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Entidades_EncuestasMoviles;
using BLL_EncuestasMoviles;
using System.Configuration;

namespace WS_EncuestaMovil
{
    /// <summary>
    /// Summary description for ServiceEncuestaMovil
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSEncuestaMovil : System.Web.Services.WebService
    {
        public AuthHeader Credentials;
         Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
                
        #region Dispositivos

        /// <summary>
        /// Obtiene los Datos del Dispositivo por Numero de Telefono
        /// </summary>
        /// <param name="NumeroTelefono">Obtener Datos del Dispositivo por Numero de Telefono</param>
        /// <returns>List con objetos de tipo THE_Dispositivo</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public List<THE_Dispositivo> ObtenerDispositivoNumero(double NumeroTelefono)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return (List<THE_Dispositivo>)MngNegocioDispositivo.ObtenerDispositivoNumero(NumeroTelefono);
        }

        
        #endregion

        #region Encuestas

        /// <summary>
        /// Obtiene la(s) Encuesta(s) Asignadas a un Dispositivo
        /// </summary>
        /// <param name="IdDispositivo">Obtener Datos de la Encuesta por clave Id del Dispositivo</param>
        /// <returns>List con objetos de tipo TDI_EncuestaDispositivo</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public List<TDI_EncuestaDispositivo> ObtieneEncuestaPorDispositivo(double NumeroTel)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return MngNegocioEncuestaDispositivo.ObtieneEncuestaPorDispositivo(NumeroTel);
        } 
        #endregion


        #region Métodos Comunicación Dispositivo

        /// <summary>
        /// Guarda las coordenadas geográficas del dispositivo.
        /// </summary>
        /// <returns>Bool: True-> si se guardo correctamente y falso si hubo un error.</returns>
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public bool GuardaCoordenadasDispositivo(double numeroTelDispositivo, string latitud, string longitud, string CercaDe)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return MngNegocioDispositivo.GuardaCoordenadasDispositivo(numeroTelDispositivo, latitud, longitud, CercaDe);
        }

        /// <summary>
        /// Método que guarda las respuestas de las encuestas.
        /// </summary>
        /// <returns> True: si se guardo conrrectamente, FALSE: ocurrio un error al guardar</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public bool GuardaEncuestaContestada2(int IdEncuesta, List<string> PreguntaRespuesta, List<string> LogRespuesta,double NumeroTel, int idEnvio)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return MngNegocioPreguntasRespuestas.GuardaEncuestaContestada(IdEncuesta, PreguntaRespuesta, LogRespuesta, NumeroTel, idEnvio);
        }


        /// <summary>
        /// Método que guarda las respuestas de las encuestas.
        /// </summary>
        /// <returns> True: si se guardo conrrectamente, FALSE: ocurrio un error al guardar</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public bool GuardaEncuestaContestada(int IdEncuesta, List<string> PreguntaRespuesta, List<string> LogRespuesta, double NumeroTel)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return MngNegocioPreguntasRespuestas.GuardaEncuestaContestada(IdEncuesta, PreguntaRespuesta, LogRespuesta, NumeroTel);
        }

        /// <summary>
        /// Método que guarda las respuestas de las encuestas.
        /// </summary>
        /// <returns> True: si se guardo conrrectamente, FALSE: ocurrio un error al guardar</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public bool GuardaLogRespuestaSeleccionadas(List<string> RespuestasSeleccionadas)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
               return MngNegocioLogRespSelected.GuardarLogRespuestaSeleccionadas(RespuestasSeleccionadas);
        }


        /// <summary>
        /// Método que guarda el porcentaje de bateria de los dispositivos que responden las encuestas.
        /// </summary>
        /// <returns> True: si se guardo conrrectamente, FALSE: ocurrio un error al guardar</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public bool GuardaEstadoBateria(double numeroTel, int porcentajeBateria, string fechaLog)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return MngNegocioPorcentajeBateria.GuardaPorcentajeBateria(numeroTel, porcentajeBateria, fechaLog);
        }


        /// <summary>
        /// Método que obtiene el tiempo en el cual el celular notificara si existen encuestas disponibles.
        /// </summary>
        /// <returns> True: si se guardo conrrectamente, FALSE: ocurrio un error al guardar</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public List<THE_PeriodoEncuesta> ObtienePeriodosPorEncuesta(int idEncuesta)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return MngNegocioPeriodoEncuesta.ObtienePeriodosPorEncuesta(idEncuesta);
        }


        /// <summary>
        /// Método que actualiza el token del dispositivo
        /// </summary>
        /// <returns> True: si se guardo conrrectamente, FALSE: ocurrio un error al guardar</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]
        public bool ActualizaTokenDispositivo(double numTelefonico, string tokenTelefono)
        {
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            else
                return MngNegocioDispositivo.ActualizaTokenDispositivo(numTelefonico, tokenTelefono);
        }


        /// <summary>
        /// Método que actualiza la version del dispositivo
        /// </summary>
        /// <returns> True: si se guardo conrrectamente, FALSE: ocurrio un error al guardar</returns>
        /// 
        [SoapHeader("Credentials", Required = true)]
        [WebMethod]

        public bool ActualizaVersionDispo(string NUM_TEL, int VERSION_CODE, string VER_NAME, string VER_DATE)
        {
            bool exito = false;
            string CredentialAccess = _ChyperRijndael.Transmute(ConfigurationManager.AppSettings["LlaveAccesoHTTPS"].ToString(), Azteca.Utility.Security.enmTransformType.intDecrypt);
            if (Credentials.UserName != CredentialAccess && Credentials.Password != CredentialAccess)
            {
                throw new SoapException("Unauthorized", SoapException.ClientFaultCode);
            }
            else
            {
                try
                {
                    List<TDI_DispoApVersion> ExisteDispo = MngNegocioDispositivo.VerificaDispoIntoVersion(NUM_TEL);

                    if (ExisteDispo.Count > 0)
                    {
                        ExisteDispo[0].NUM_TEL = NUM_TEL;
                        ExisteDispo[0].NUMBER = VERSION_CODE;
                        ExisteDispo[0].VER_NAME = VER_NAME;
                        ExisteDispo[0].VER_DATE = DateTime.Now;
                        exito = MngNegocioDispositivo.ActualizaVersionDispo(ExisteDispo[0]);
                    }
                    else
                    {
                        TDI_DispoApVersion ObjVersion = new TDI_DispoApVersion();
                        ObjVersion.NUM_TEL = NUM_TEL;
                        ObjVersion.NUMBER = VERSION_CODE;
                        ObjVersion.VER_NAME = VER_NAME;
                        ObjVersion.VER_DATE = DateTime.Now;
                        exito = MngNegocioDispositivo.GuardaVersionDispo(ObjVersion);

                    }
                }
                catch (Exception ms)
                {

                }
                return exito;
            }
        }



        #endregion
    }
    public class AuthHeader : SoapHeader
    {
        public string UserName;
        public string Password;
    }
}
