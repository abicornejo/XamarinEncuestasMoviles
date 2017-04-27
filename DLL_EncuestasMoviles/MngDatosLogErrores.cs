using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;
using System.Configuration;

namespace DLL_EncuestasMoviles
{
    public class MngDatosLogErrores
    {
        public static Boolean GuardarLogErrores(THE_LogError LogErrores)
        {
            if (LogErrores.Error.Length > 1000)
                LogErrores.Error = LogErrores.Error.Substring(0, 999);

            return NHibernateHelperORACLE.SingleSessionSave<THE_LogError>(LogErrores);
        }

        public static void GuardaError(Exception Ex, string nombreClase)
        {
            Azteca.Utility.Security.Rijndael _ChyperRijndael = new Azteca.Utility.Security.Rijndael();
            THE_LogError saveError = new THE_LogError();

            try
            {
                saveError.EmplUsua = _ChyperRijndael.Transmute(ConfigurationSettings.AppSettings["numeroUsuario"], Azteca.Utility.Security.enmTransformType.intDecrypt);
                saveError.FechaCreacion = DateTime.Now;
                saveError.Pantalla = nombreClase;
                saveError.Error = Ex.Message + "\n" + Ex.StackTrace + "\n" + Ex.Data;
                GuardarLogErrores(saveError);
            }
            catch
            {

            }
            finally
            {
                saveError = null;
                _ChyperRijndael = null;
            }

        }
    }
}
