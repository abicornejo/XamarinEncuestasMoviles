using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_PeriodoEncuesta
    {

        #region constructor

        public THE_PeriodoEncuesta() { }

        #endregion termina constructor

        #region atributos

        private int _id_perido;
        private THE_Encuesta _id_encuesta;
        private int _perido;

        #endregion 

        #region propiedades

        public virtual int IdPeriodo {
            get { return _id_perido; }
            set { _id_perido = value; }        
        }

        public virtual THE_Encuesta IdEncuesta {
            get { return _id_encuesta; }
            set { _id_encuesta = value; }  
        }

        public virtual int Periodo {
            get { return _perido; }
            set { _perido = value; }  
        }
        
        #endregion 
    }
}
