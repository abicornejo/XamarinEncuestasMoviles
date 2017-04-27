using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Entidades_EncuestasMoviles;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosPreguntasRespuestas
    {
        public static Boolean GuardaEncuestaContestada(List<TDI_PreguntasRespuestas> LstPreguntasRespuestas)
        {
            GuardaLogTransacc("Conexión de dispositivo Android con el Web Service - No. Tel: " + LstPreguntasRespuestas[0].IdDispositivo.NumerodelTelefono.ToString(), 26, Convert.ToDouble(LstPreguntasRespuestas[0].IdDispositivo.NumerodelTelefono.ToString()));
            try
            {
                foreach (TDI_PreguntasRespuestas PregResp in LstPreguntasRespuestas)
                {
                   bool r=NHibernateHelperORACLE.SingleSessionSave<TDI_PreguntasRespuestas>(PregResp);
                   if (!r) {
                       return false;
                   }
                }
                GuardaLogTransacc("Metodo consumido desde Android: GuardaEncuestaContestada - No. Tel: " + LstPreguntasRespuestas[0].IdDispositivo.NumerodelTelefono.ToString(), 30, Convert.ToDouble(LstPreguntasRespuestas[0].IdDispositivo.NumerodelTelefono.ToString()));
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
        public static void GuardaLogTransacc(string Desc, int IdTran, double NumTel)
        {
            THE_LogTran oLogTran = new THE_LogTran();
            oLogTran.LogtDesc = Desc;
            oLogTran.LogtDomi = "Android";
            oLogTran.LogtFech = DateTime.Now;
            oLogTran.LogtMach = "Android";
            oLogTran.LogtUsIp = NumTel.ToString();
            oLogTran.LogtUsua = "Android";
            oLogTran.TranLlavPr = new TDI_Transacc() { TranLlavPr = IdTran };
            MngDatosTransacciones.GuardaLogTransaccion(oLogTran);
        }

    }
}
