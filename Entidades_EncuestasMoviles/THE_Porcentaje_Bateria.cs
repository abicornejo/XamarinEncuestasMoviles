using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Porcentaje_Bateria
    {
        #region Contructor
        public THE_Porcentaje_Bateria() { 
        }
        #endregion

        #region Atributos

        private int _id_rev;
        private double _number_Telephone;
        private int _porcentaje_Bateria;
        private DateTime _fecha;

        #endregion

        #region Propiedades

        public virtual int IdRevision{

            get { return _id_rev; }
            set { _id_rev = value; }
        }
        public virtual double Numero_Telefonico
        {
            get { return _number_Telephone; }
            set { _number_Telephone = value; }
        }

        public virtual int Porcentaje_Bateria
        {
            get { return _porcentaje_Bateria; }
            set { _porcentaje_Bateria = value; }
        }

        public virtual DateTime Fecha_Revision {
            get { return _fecha; }
            set { _fecha = value; }
        }

        #endregion
    }
}
