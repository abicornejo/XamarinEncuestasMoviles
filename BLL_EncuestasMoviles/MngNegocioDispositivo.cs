using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioDispositivo
    {
        public static IList<THE_Dispositivo> ObtenerDispositivoNumero(double NumeroTelefono)
        {
            return MngDatosDispositivo.ObtenerDispositivoNumero(NumeroTelefono);
        }

        public static Boolean GuardaAltaDispositivo(THE_Dispositivo dispo)
        {
            return MngDatosDispositivo.GuardaAltaDispositivo(dispo);
        }

        public static Int32 GuardaAltaDispo(THE_Dispositivo dispo)
        {
            return MngDatosDispositivo.GuardaAltaDispo(dispo);
        }

        public static Boolean GuardaVersionDispo(TDI_DispoApVersion dispoVersion) {

            return MngDatosDispositivo.GuardaVersionDispo(dispoVersion);
        }
        public static Boolean ActualizaVersionDispo(TDI_DispoApVersion dispoVersion)
        {

            return MngDatosDispositivo.ActualizaVersionDispo(dispoVersion);
        }
        public static List<TDI_DispoApVersion> VerificaDispoIntoVersion(string numTelefono)
        {

            return MngDatosDispositivo.VerificaDispoIntoVersion(numTelefono);
        }

        public static List<THE_Dispositivo> ObtieneTodosDispositivos()
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.ObtieneTodosDispositivos();
        }
        public static List<THE_Dispositivo> GetLastDispositivo()
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.GetLastDispositivo();
        }
        public static Boolean EliminaDispositivo(THE_Dispositivo dispo)
        {
            return MngDatosDispositivo.EliminaDispositivo(dispo);
        }

        public static Boolean ActualizaDispositivo(THE_Dispositivo dispo)
        {
            return MngDatosDispositivo.ActualizaDispositivo(dispo);
        }
        public static List<TDI_UbicacionDispositivo> ObtieneCoordenadasDispositivo(int UsuarioId, string fechaInicial, string fechaFinal)
        {
            return (List<TDI_UbicacionDispositivo>)MngDatosDispositivo.ObtieneCoordenadasDispositivo(UsuarioId, fechaInicial, fechaFinal);
        }
        public static Boolean GuardaCoordenadasDispositivo(double numeroTelDispositivo, string latitud, string longitud, string CercaDe)
        {
            try
            {
                List<THE_Dispositivo> oDispo = (List<THE_Dispositivo>)MngDatosDispositivo.ObtenerDispositivoNumero(numeroTelDispositivo);

                TDI_UbicacionDispositivo LogPosicionDispositivo = new TDI_UbicacionDispositivo();
                LogPosicionDispositivo.IdDispositivo = oDispo[0];
                LogPosicionDispositivo.Latitud = latitud;
                LogPosicionDispositivo.Longitud = longitud;
                LogPosicionDispositivo.DispoUbicacionCercaDe = CercaDe;
                LogPosicionDispositivo.IdDispositivo.NumerodelTelefono = numeroTelDispositivo.ToString();
                return MngDatosDispositivo.GuardaCoordenadasDispositivo(LogPosicionDispositivo);

            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public static Boolean ActualizaTokenDispositivo(double numeroTelefonico, string tokenDispositivo) {

            List<THE_Dispositivo> oDispo = (List<THE_Dispositivo>)MngDatosDispositivo.ObtenerDispositivoNumero(numeroTelefonico);
            if (oDispo.Count > 0)
            {
                oDispo[0].TokenDispositivo = tokenDispositivo;
                THE_Dispositivo dispoToUpdate=oDispo[0];              
                return MngDatosDispositivo.ActualizaDispositivo(dispoToUpdate);     
            }
            else {
                return false;
            }
        }

        public static List<THE_Dispositivo> ObtieneDispositivosDisponibles()
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.ObtieneDispositivosDisponibles();
        }

        public static List<THE_Dispositivo> ObtieneDispositivoPorID(int IdDispo)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.ObtieneDispositivoPorID(IdDispo);
        }

        public static List<THE_Dispositivo> ObtieneDispositivosAsignadosUsuario()
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.ObtieneDispositivosAsignadosUsuario();
        }

        public static List<THE_Dispositivo> BuscaDispositivoFiltros(string NombUsuario, string NumeroTel, string NombEstado, string NombMuni, string[] Catalogos)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.BuscaDispositivoFiltros(NombUsuario, NumeroTel, NombEstado, NombMuni, Catalogos);
        }

        public static List<THE_Dispositivo> BusquedaDispositivoPorNumeroTel(string NumeroTelefono)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.BusquedaDispositivoPorNumeroTel(NumeroTelefono);
        }

        public static List<THE_Dispositivo> BusquedaDispositivoPorNumeroTel(string NumeroTelefono, int idDispositivo)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.BusquedaDispositivoPorNumeroTel(NumeroTelefono, idDispositivo);
        }

        public static List<THE_Dispositivo> BusquedaDispositivoPorMEID(string MEIDTelefono)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.BusquedaDispositivoPorMEID(MEIDTelefono);
        }

        public static List<THE_Dispositivo> BusquedaDispositivoPorMEID(string MEIDTelefono, int idDispositivo)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.BusquedaDispositivoPorMEID(MEIDTelefono, idDispositivo);
        }

        public static List<THE_Dispositivo> BusquedaDispositivoPorMDN(string MDNTelefono)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.BusquedaDispositivoPorMDN(MDNTelefono);
        }

        public static List<THE_Dispositivo> BusquedaDispositivoPorMDN(string MDNTelefono, int idDispositivo)
        {
            return (List<THE_Dispositivo>)MngDatosDispositivo.BusquedaDispositivoPorMDN(MDNTelefono, idDispositivo);
        }
    }
}
