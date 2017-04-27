using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioEncuestaDispositivo
    {
        public static List<TDI_EncuestaDispositivo> ObtieneEncuestaPorDispositivo(double NumeroTel)
        {
            return (List<TDI_EncuestaDispositivo>)MngDatosEncuestaDispositivo.ObtieneEncuestaPorDispositivo(NumeroTel);
        }

        public static List<TDI_EncuestaDispositivo> ObtieneDispositivosPorEncuesta(int idEncuesta)
        {
            return (List<TDI_EncuestaDispositivo>)MngDatosEncuestaDispositivo.ObtieneDispositivosPorEncuesta(idEncuesta);
        }

        public static List<TDI_EncuestaDispositivo> ObtieneDispositivosActivos(string idEncuesta)
        {
            return (List<TDI_EncuestaDispositivo>)MngDatosEncuestaDispositivo.ObtieneDispositivosActivos(idEncuesta);
        }

        //public static List<THE_PrograDispositivo> ObtieneDispositivosProgramados(string idProgramacion)
        //{
        //    return (List<THE_PrograDispositivo>)MngDatosEncuestaDispositivo.ObtieneDispositivosProgramados(idProgramacion);
        //}

        public static List<TDI_EncuestaDispositivo> ObtieneDispositivosPorIdEnvio(int idEnvio)
        {
            return (List<TDI_EncuestaDispositivo>)MngDatosEncuestaDispositivo.ObtieneDispositivosPorIdEnvio(idEnvio);
        }

        //public static List<TDI_EncuestaDispositivo> ObDispoByIdDispoNumTel(int idDispo, double numTelefono)
        //{
        //    return (List<TDI_EncuestaDispositivo>)MngDatosEncuestaDispositivo.ObtDispoByIdDispoNumTel(idDispo, numTelefono);
        //}


        public static Boolean AlmacenaDispoEncuesta(TDI_EncuestaDispositivo DispoEncu)
        {
            return MngDatosEncuestaDispositivo.AlmacenaDispoEncuesta(DispoEncu);
        }

        public static Boolean ActualizaEstatusDispoEncu(TDI_EncuestaDispositivo DispoEncu)
        {
            return MngDatosEncuestaDispositivo.ActualizaEstatusDispoEncu(DispoEncu);
        }

        public static Boolean InsertNewDispoEncuesta(TDI_EncuestaDispositivo DispoEncu)
        {
            return MngDatosEncuestaDispositivo.InsertNewDispoEncuesta(DispoEncu);
        }

        public static List<TDI_EncuestaDispositivo> ObtieneEstatusDispoEncu(int IdDispositivo, int IdEncuSel)
        {
            return (List<TDI_EncuestaDispositivo>)MngDatosEncuestaDispositivo.ObtieneEstatusDispoEncu(IdDispositivo, IdEncuSel);
        }

    }
}
