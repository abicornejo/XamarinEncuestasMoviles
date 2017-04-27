using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_EstatusMensaje
    {
        #region Constructor
        public TDI_EstatusMensaje() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Estatus del Mensaje.
        /// </summary>
        private int _idEstatusMensaje;
        /// <summary>
        /// Descripcion del Estatus del Mensaje.
        /// </summary>
        private string _estatusMensajeDesc;
        #endregion

        #region Propiedades
        public virtual int IdEstatusMensaje
        {
            get { return _idEstatusMensaje; }
            set { _idEstatusMensaje = value; }
        }
        public virtual string EstatusMensajeDescripcion
        {
            get { return _estatusMensajeDesc; }
            set { _estatusMensajeDesc = value; }
        }
        #endregion
    }
}
