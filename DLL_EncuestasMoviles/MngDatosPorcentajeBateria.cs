using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosPorcentajeBateria
    {

        public static Boolean GuardaPorcentajeBateria(double numeroTel, int porcentajeBateria, string fechaLog)
        {
            THE_Porcentaje_Bateria bateria = new THE_Porcentaje_Bateria();


            bateria.Numero_Telefonico = numeroTel;
            bateria.Porcentaje_Bateria = porcentajeBateria;
            bateria.Fecha_Revision = Convert.ToDateTime(fechaLog);

            return NHibernateHelperORACLE.SingleSessionSave<THE_Porcentaje_Bateria>(bateria);            
        }

    }
}
