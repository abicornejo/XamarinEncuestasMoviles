using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Ciudad
    {
        #region Constructor
        public TDI_Ciudad() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Ciudad.
        /// </summary>
        private int _idCiudad;
        /// <summary>
        /// Nombre de la Ciudad.
        /// </summary>
        private string _ciudadNombre;
        /// <summary>
        /// Estatus de la Ciudad.
        /// </summary>
        private char _ciudadEstatus;
        /// <summary>
        /// Estado al que pertenece la Ciudad.
        /// </summary>
        private TDI_Estado _idEstado;
        #endregion

        #region Propiedades
        public virtual int IdCiudad
        {
            get { return _idCiudad; }
            set { _idCiudad = value; }
        }
        public virtual string CiudadNombre
        {
            get { return _ciudadNombre; }
            set { _ciudadNombre = value; }
        }
        public virtual char CiudadEstatus
        {
            get { return _ciudadEstatus; }
            set { _ciudadEstatus = value; }
        }
        public virtual TDI_Estado IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }
        #endregion
    }
}
