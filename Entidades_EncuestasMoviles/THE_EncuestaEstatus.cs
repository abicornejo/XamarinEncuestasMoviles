using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_EncuestaEstatus
    {
        #region Constructor
        public THE_EncuestaEstatus() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Id de la encuesta
        /// </summary>
        private int _idEstatus;
        
        /// <summary>
        /// Numero de Telefonos en ese estatus
        /// </summary>
        private int _numero;
        #endregion

        #region Propiedades
        public virtual int IdEstatus
        {
            get { return _idEstatus; }
            set { _idEstatus = value; }
        }

        public virtual int Numero
        {
            get { return _numero; }
            set { _numero = value; }
        }
        #endregion
    }
}
