using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;


namespace DLL_EncuestasMoviles
{
  public class MngDatosLogRespSelected
    {
      public static Boolean GuardarLogRespuestaSeleccionadas(THE_LogRespSelected respuestas)
      {
          return NHibernateHelperORACLE.SingleSessionSave<THE_LogRespSelected>(respuestas);
      }
    }
}
