using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioPreguntasRespuestas
    {
        public static Boolean GuardaEncuestaContestada(int idEncuesta , List<string> PreguntaRespuesta,List<string> Logrespuestas, double NumeroTel, int idEnvio)
        {
            int idDispositivo = 0;
            List<TDI_PreguntasRespuestas> TodasLasPreguntasYRespuestas = new List<TDI_PreguntasRespuestas>();
            try
            {
                List<THE_Dispositivo> objDispositivo = (List<THE_Dispositivo>)MngNegocioDispositivo.ObtenerDispositivoNumero(NumeroTel);
                foreach (THE_Dispositivo disp in objDispositivo)
                {
                    idDispositivo = disp.IdDispositivo;
                }

                if (idDispositivo != 0)
                {
                    foreach (string pregconresp in PreguntaRespuesta)
                    {
                        if (pregconresp != null)
                        {
                            string[] ids = pregconresp.Split('|');
                            TDI_PreguntasRespuestas PregResp = new TDI_PreguntasRespuestas();

                            PregResp.IdDispositivo = new THE_Dispositivo { IdDispositivo = idDispositivo };
                            PregResp.IdEncuesta = new THE_Encuesta { IdEncuesta = idEncuesta };
                            PregResp.IdPregunta = new THE_Preguntas { IdPregunta = int.Parse(ids[0].ToString()) };
                            PregResp.IdRespuesta = new THE_Respuestas { IdRespuesta = int.Parse(ids[1].ToString()) };
                            PregResp.IdEnvio = new TDI_EncuestaDispositivo { IdEnvio = idEnvio };
                            TodasLasPreguntasYRespuestas.Add(PregResp);
                            TodasLasPreguntasYRespuestas[0].IdDispositivo.NumerodelTelefono = NumeroTel.ToString();
                        }
                    }


                    Boolean resultado = true;
                    int posicion = 1;
                    foreach (string resp in Logrespuestas)
                    {

                        if (resp != null)
                        {
                            string[] ids = resp.Split('|');
                            THE_LogRespSelected PregResp = new THE_LogRespSelected();
                            PregResp.IdRespSelected = System.Convert.ToInt32(ids[0]);
                            PregResp.OrdenRespSelected = posicion;
                            PregResp.IdEncuestaSelected = System.Convert.ToInt32(ids[4]);
                            PregResp.DescRespuestaSelected = ids[1].ToString();
                            PregResp.Evento_Resp = ids[5].ToString();
                            PregResp.Fecha_Evento = Convert.ToDateTime(ids[6].ToString());
                            PregResp.NumTel =Convert.ToDouble(ids[7].ToString());

                            if (!MngDatosLogRespSelected.GuardarLogRespuestaSeleccionadas(PregResp))
                            {
                                resultado=false;
                                break;
                            }
                        }
                        else
                        {
                            resultado=false;
                            break;
                        }

                        posicion++;
                    }


                    if (MngDatosPreguntasRespuestas.GuardaEncuestaContestada(TodasLasPreguntasYRespuestas) && resultado)
                    {
                        TDI_EncuestaDispositivo encuDis = new TDI_EncuestaDispositivo();
                        encuDis.IdEnvio = idEnvio;
                        encuDis.IdDispositivo = TodasLasPreguntasYRespuestas[0].IdDispositivo;
                        encuDis.IdEncuesta = TodasLasPreguntasYRespuestas[0].IdEncuesta;
                        encuDis.IdEstatus = new TDI_Estatus() { IdEstatus = 4 };
                        if (MngDatosEncuestaDispositivo.ActualizaEstatusDispoEncu(encuDis)) {
                            Console.WriteLine("actualizo el estatus de la encuesta a dos del dispositivo" + TodasLasPreguntasYRespuestas[0].IdDispositivo);
                            return true;
                        }
                        return false;

                    }
                    else {
                        Console.WriteLine("No gurado encuesta");
                        return false;
                    }                    
                }
                return false;
            }
            catch
            {

                return false;
            }
        }

        public static Boolean GuardaEncuestaContestada(int idEncuesta, List<string> PreguntaRespuesta, List<string> Logrespuestas, double NumeroTel)
        {
            int idDispositivo = 0;
            int idEnvio = 0;
            List<TDI_PreguntasRespuestas> TodasLasPreguntasYRespuestas = new List<TDI_PreguntasRespuestas>();
            try
            {
                
                List<THE_Dispositivo> objDispositivo = (List<THE_Dispositivo>)MngNegocioDispositivo.ObtenerDispositivoNumero(NumeroTel);
                foreach (THE_Dispositivo disp in objDispositivo)
                {
                    idDispositivo = disp.IdDispositivo;
                }
               
               
                if (idDispositivo != 0)
                {

                    //-------------------------------
                    IList<TDI_EncuestaDispositivo> DispoEncuesta2 = MngDatosEncuestaDispositivo.ObtieneDispoByIdEncNumTel(idEncuesta.ToString(), NumeroTel.ToString());
                    if (DispoEncuesta2.Count > 0)
                    {

                        idEnvio = DispoEncuesta2[0].IdEnvio;

                    }
                    //-------------------------------

                    
                    foreach (string pregconresp in PreguntaRespuesta)
                    {
                        if (pregconresp != null)
                        {
                            string[] ids = pregconresp.Split('|');
                            TDI_PreguntasRespuestas PregResp = new TDI_PreguntasRespuestas();

                            PregResp.IdDispositivo = new THE_Dispositivo { IdDispositivo = idDispositivo };
                            PregResp.IdEncuesta = new THE_Encuesta { IdEncuesta = idEncuesta };
                            PregResp.IdPregunta = new THE_Preguntas { IdPregunta = int.Parse(ids[0].ToString()) };
                            PregResp.IdRespuesta = new THE_Respuestas { IdRespuesta = int.Parse(ids[1].ToString()) };
                            //PregResp.FechaInsercion = new TDI_PreguntasRespuestas { FechaInsercion = DateTime.Parse(ids[0]) }; 
                            PregResp.IdEnvio = new TDI_EncuestaDispositivo { IdEnvio = idEnvio };//new TDI_EncuestaDispositivo { IdEnvio = idEnvio };
                            TodasLasPreguntasYRespuestas.Add(PregResp);
                            TodasLasPreguntasYRespuestas[0].IdDispositivo.NumerodelTelefono = NumeroTel.ToString();
                        }
                    }


                    Boolean resultado = true;
                    int posicion = 1;
                    foreach (string resp in Logrespuestas)
                    {

                        if (resp != null)
                        {
                            string[] ids = resp.Split('|');
                            THE_LogRespSelected PregResp = new THE_LogRespSelected();
                            PregResp.IdRespSelected = System.Convert.ToInt32(ids[0]);
                            PregResp.OrdenRespSelected = posicion;
                            PregResp.IdEncuestaSelected = System.Convert.ToInt32(ids[4]);
                            PregResp.DescRespuestaSelected = ids[1].ToString();
                            PregResp.Evento_Resp = ids[5].ToString();
                            PregResp.Fecha_Evento = Convert.ToDateTime(ids[6].ToString());
                            PregResp.NumTel = Convert.ToDouble(ids[7].ToString());

                            if (!MngDatosLogRespSelected.GuardarLogRespuestaSeleccionadas(PregResp))
                            {
                                resultado = false;
                                break;
                            }
                        }
                        else
                        {
                            resultado = false;
                            break;
                        }

                        posicion++;
                    }


                    if (MngDatosPreguntasRespuestas.GuardaEncuestaContestada(TodasLasPreguntasYRespuestas) && resultado)
                    {
                        IList<TDI_EncuestaDispositivo> DispoEncuesta = MngDatosEncuestaDispositivo.ObtieneDispoByIdEncNumTel(idEncuesta.ToString(), NumeroTel.ToString());
                        if (DispoEncuesta.Count > 0)
                        {

                            TDI_EncuestaDispositivo encuDis = new TDI_EncuestaDispositivo();
                            encuDis.IdEnvio = DispoEncuesta[0].IdEnvio;
                            encuDis.IdDispositivo = TodasLasPreguntasYRespuestas[0].IdDispositivo;
                            encuDis.IdEncuesta = TodasLasPreguntasYRespuestas[0].IdEncuesta;
                            encuDis.IdEstatus = new TDI_Estatus() { IdEstatus = 4 };
                            if (MngDatosEncuestaDispositivo.ActualizaEstatusDispoEncu(encuDis))
                            {
                                Console.WriteLine("actualizo el estatus de la encuesta a dos del dispositivo" + TodasLasPreguntasYRespuestas[0].IdDispositivo);
                                return true;
                            }





                        }
                        return false;

                    }
                    else
                    {
                        Console.WriteLine("No gurado encuesta");
                        return false;
                    }
                }
                return false;
            }
            catch
            {

                return false;
            }
        }

    }
}
