using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Estado
    {
        #region Constructor
        public TDI_Estado() { }
        #endregion 

        #region Atributos
        /// <summary>
        /// Llave maestra del Estado.
        /// </summary>
        private int _idEstado;
        /// <summary>
        /// Nombre del Estado.
        /// </summary>
        private string _estadoNombre;
        /// <summary>
        /// Estatus del Estado.
        /// </summary>
        private char _estadoEstatus;
        #endregion

        #region Propiedades
        public virtual int IdEstado
        {
            get { return _idEstado; }
            set { _idEstado = value; }
        }
        public virtual string EstadoNombre
        {
            get { return _estadoNombre; }
            set { _estadoNombre = value; }
        }
        public virtual char EstadoEstatus
        {
            get { return _estadoEstatus; }
            set { _estadoEstatus = value; }
        }
        #endregion
    }
}
