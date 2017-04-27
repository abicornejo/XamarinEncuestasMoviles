using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_TipoZona
    {
        #region Constructor
        public THE_TipoZona() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Zona.
        /// </summary>
        private int _idZona;
        /// <summary>
        /// Nombre de la Zona.
        /// </summary>
        private string _zonaNombre;
        #endregion

        #region Propiedades
        public virtual int IdZona
        {
            get { return _idZona; }
            set { _idZona = value; }
        }
        public virtual string ZonaNombre
        {
            get { return _zonaNombre; }
            set { _zonaNombre = value; }
        }
        #endregion
    }
}
