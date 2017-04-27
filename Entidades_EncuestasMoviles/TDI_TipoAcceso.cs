using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_TipoAcceso
    {
        #region Constructor
        public TDI_TipoAcceso() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Tipo de Acceso.
        /// </summary>
        private int _idTipoAcceso;
        /// <summary>
        /// Descripcion del Tipo de Acceso.
        /// </summary>
        private string _tipoAccesoDesc;
        /// <summary>
        /// Estatus del Tipo de Acceso.
        /// </summary>
        private char _tipoAccesoEstatus;
        #endregion

        #region Propiedades
        public virtual int IdTipoAcceso
        {
            get { return _idTipoAcceso; }
            set { _idTipoAcceso = value; }
        }
        public virtual string TipoAccesoDesc
        {
            get { return _tipoAccesoDesc; }
            set { _tipoAccesoDesc = value; }
        }
        public virtual char TipoAccesoEstatus
        {
            get { return _tipoAccesoEstatus; }
            set { _tipoAccesoEstatus = value; }
        }
        #endregion
    }
}
