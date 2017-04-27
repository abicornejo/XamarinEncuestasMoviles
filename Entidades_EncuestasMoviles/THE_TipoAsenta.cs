using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_TipoAsenta
    {
        #region Constructor
        public THE_TipoAsenta() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Asentamiento.
        /// </summary>
        private int _idAsentamiento;
        /// <summary>
        /// Nombre del Asentamiento.
        /// </summary>
        private string _asentamientoNombre;
        #endregion

        #region Propiedades
        public virtual int IdAsentamiento
        {
            get { return _idAsentamiento; }
            set { _idAsentamiento = value; }
        }
        public virtual string AsentamientoNombre
        {
            get { return _asentamientoNombre; }
            set { _asentamientoNombre = value; }
        }
        #endregion
    }
}
