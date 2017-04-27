using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_TipoProgramacion
    {
        #region Constructor
        public TDI_TipoProgramacion() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Tipo Programacion.
        /// </summary>
        private int _idTipoProgramacion;
        /// <summary>
        /// Descripcion del Tipo.
        /// </summary>
        private string _tipoProgramacionDesc;
        /// <summary>
        /// Estatus tipo programacion.
        /// </summary>
        private char _tipoProgramacionEstatus;
        #endregion

        #region Propiedades
        public virtual int IdTipoProgramacion
        {
            get { return _idTipoProgramacion; }
            set { _idTipoProgramacion = value; }
        }
        public virtual string TipoProgramacionDescripcion
        {
            get { return _tipoProgramacionDesc; }
            set { _tipoProgramacionDesc = value; }
        }
        public virtual char TipoProgramacionEstatus
        {
            get { return _tipoProgramacionEstatus; }
            set { _tipoProgramacionEstatus = value; }
        }
        #endregion
    }
}
