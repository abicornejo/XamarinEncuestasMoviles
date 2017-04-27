using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Estatus
    {
        #region Constructor
        public TDI_Estatus() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Estatus.
        /// </summary>
        private int _idEstatus;
        /// <summary>
        /// Descripcion del Estatus.
        /// </summary>
        private string _estatusDesc;
        /// <summary>
        /// Identificador si es Estatus Dispositivo.
        /// </summary>
        private char _estatusDispo;
        #endregion

        #region Propiedades
        public virtual int IdEstatus
        {
            get { return _idEstatus; }
            set { _idEstatus = value; }
        }
        public virtual string EstatusDescripcion
        {
            get { return _estatusDesc; }
            set { _estatusDesc = value; }
        }
        public virtual char EstatusDispositivo
        {
            get { return _estatusDispo; }
            set { _estatusDispo = value; }
        }
        #endregion
    }
}
