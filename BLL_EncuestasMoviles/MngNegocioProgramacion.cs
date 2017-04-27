using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;


namespace BLL_EncuestasMoviles
{
    public class MngNegocioProgramacion
    {
        public static Boolean GuardaProgramacionPorEncuesta(THE_Programacion programacion)
        {
            return MngDatosProgramacion.GuardaProgramacionPorEncuesta(programacion);
        }
                
        public static List<THE_Programacion> ObtieneProgramacionesPorEncuesta(int idEncuesta)
        {
            return (List<THE_Programacion>)MngDatosProgramacion.ObtieneProgramacionesPorEncuesta(idEncuesta);
        }

        public static List<THE_Programacion> ObtieneProgramaciones(string idEncuesta)
        {
            return (List<THE_Programacion>)MngDatosProgramacion.ObtieneProgramaciones(idEncuesta);
        }
       
        public static List<THE_Programacion> ObtieneProgramacionesbyEncuesta(string idEncuesta)
        {
            return (List<THE_Programacion>)MngDatosProgramacion.ObtieneProgramacionesbyEncuesta(idEncuesta);
        }
        public static List<THE_PrograDispositivo> ObtieneDispositivosProgramados()
        {
            return (List<THE_PrograDispositivo>)MngDatosProgramacion.ObtieneDispositivosProgramados();
        }

        public static List<THE_PrograDispositivo> ObtenDispoProgramadosByProgramacion(string idProgramacion)
        {
            return (List<THE_PrograDispositivo>)MngDatosProgramacion.ObtenDispoProgramadosByProgramacion(idProgramacion);
        }


        public static Boolean ActualizaProgramacion(THE_Programacion programacion)
        {
            return MngDatosProgramacion.ActualizaProgramacion(programacion);
        }

        public static Boolean AgregaDispositivoProgramados(THE_PrograDispositivo programacion)
        {
            return MngDatosProgramacion.AgregaDispositivoProgramados(programacion);
        }

        //public static Boolean existeDispoInProgramacion(string idProgramacion, string idDispositivo, string idEncuesta, string idTipoProgramacion)
            
        //    //return (MngDatosProgramacion.(idProgramacion, idDispositivo, idEncuesta, idTipoProgramacion));
        //}

        public static Boolean existeDispoInProgramacion(string idProgramacion, string idDispositivo, string idEncuesta, string idTipoProgramacion)
        {
            return MngDatosProgramacion.existeDispoInProgramacion(idProgramacion, idDispositivo, idEncuesta, idTipoProgramacion);
        }


        public static Boolean EliminaDispositivoProgramados(THE_PrograDispositivo programacion)
        {
            return MngDatosProgramacion.EliminaDispositivoProgramados(programacion);
        }


        public static THE_Programacion ObtieneProgramacionPorID(int idProgramacion)
        {
            return MngDatosProgramacion.ObtieneProgramacionPorID(idProgramacion);
        }
        
        public static Boolean EliminaProgramacion(THE_Programacion programacion)
        {
            return MngDatosProgramacion.EliminaProgramacion(programacion);
        }



    }
}
