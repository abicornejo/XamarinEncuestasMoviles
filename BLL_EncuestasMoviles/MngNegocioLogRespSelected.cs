using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
   public class MngNegocioLogRespSelected
    {
       public static Boolean GuardarLogRespuestaSeleccionadas(List<string> respuestas)
       {
           Boolean resultado = true;//id_resp[0],orden[1],id_encuesta[2],desc_resp[3]
           ////idRespuesta[0], titulo_Respuesta[1], idPreguntaactual[2], idPreguntasiguiente[3], idEncuesta[4], evento[6], 7numTel[]
           int posicion = 1;
           foreach(string resp in respuestas){
               
               if (resp != null)
               {
                   string[] ids = resp.Split('|');
                   THE_LogRespSelected PregResp = new THE_LogRespSelected();
                   PregResp.IdRespSelected = System.Convert.ToInt32(ids[0]);
                   PregResp.OrdenRespSelected = posicion;
                   PregResp.IdEncuestaSelected = System.Convert.ToInt32(ids[4]);
                   PregResp.DescRespuestaSelected = ids[1].ToString();
                   PregResp.Evento_Resp = ids[5].ToString();
                   PregResp.Fecha_Evento = Convert.ToDateTime(ids[6]);
                   PregResp.NumTel = Convert.ToDouble(ids[7]);

                   if (!MngDatosLogRespSelected.GuardarLogRespuestaSeleccionadas(PregResp))
                   {
                       return false;
                   }
                   
               }
               else {
                   return false;
               }

              posicion++;
           }
           return resultado;          
       }
    }
}
