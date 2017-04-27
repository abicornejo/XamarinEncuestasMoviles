using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioGraficas
    {
        public static List<THE_EncuestaEstatus> ConsultaEncuestasEstatus(int IdEncuesta, bool MostrarTodos)
        {
            return (List<THE_EncuestaEstatus>)MngDatosGraficas.ConsultaEncuestasEstatus(IdEncuesta, MostrarTodos);
        }

        public static List<TDI_GraficasEncuesta> GraficarEncuesta(int IdEncuesta, bool MostrarTodos, bool FueraHorario, string Catalogos)
        {
            return (List<TDI_GraficasEncuesta>)MngDatosGraficas.GraficarEncuesta(IdEncuesta, MostrarTodos, FueraHorario, Catalogos);
        }
        public static List<TDI_GraficasEncuesta> DibujaGrafica(int IdEncuesta,  bool DentroHorario, string Catalogos, string idPregunta)
        {
            return (List<TDI_GraficasEncuesta>)MngDatosGraficas.DibujaGrafica(IdEncuesta,  DentroHorario, Catalogos, idPregunta);
        }
        public static bool EnviaGraficaCorreo(string NomRemitente, string Destinatario, string NombDestinatario, string Remitente, int IdEncuesta)
        {
            return MngDatosGraficas.EnviaGraficaCorreo(NomRemitente, Destinatario, NombDestinatario, Remitente, IdEncuesta);
        }

        public static List<TDI_GraficasEncuesta> GraficaEncuestaExportaPDF(int IdEncuesta)
        {
            return (List<TDI_GraficasEncuesta>)MngDatosGraficas.GraficaEncuestaExportaPDF(IdEncuesta);
        }

        public static List<TDI_GraficasEncuesta> GeTopOfMind(string idPregunta, string idsDispos)
        {
            return (List<TDI_GraficasEncuesta>)MngDatosGraficas.GeTopOfMind(idPregunta, idsDispos);
        }

    }
}
