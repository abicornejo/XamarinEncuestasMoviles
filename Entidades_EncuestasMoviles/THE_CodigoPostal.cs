using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_CodigoPostal
    {
        #region Constructor
        public THE_CodigoPostal() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Codigo Postal.
        /// </summary>
        private int _idCodigoPostal;
        #endregion

        #region Propiedades
        public virtual int IdCodigoPostal
        {
            get { return _idCodigoPostal; }
            set { _idCodigoPostal = value; }
        }
        #endregion
    }
}
