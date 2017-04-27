using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Presentacion
    {
        #region Constructor
        public TDI_Presentacion() { }
        #endregion Constructor

        #region Atributos
        /// <summary>
        /// Id presentacion
        /// </summary>
        private int _idPresentacion;
        /// <summary>
        /// Descripción de la presentacion
        /// </summary>
        private string _PresentacionDescripcion;
        /// <summary>
        ///  Estatus de la presentacion
        /// </summary>
        private string _PresentacionEstatus;
        #endregion Atributos

        #region Propiedades
        public virtual int IdPresentacion
        {
            get { return _idPresentacion; }
            set { _idPresentacion = value; }
        }

        public virtual string PresentacionDescripcion
        {
            get { return _PresentacionDescripcion; }
            set { _PresentacionDescripcion = value; }
        }

        public virtual string PresentacionEstatus
        {
            get { return _PresentacionEstatus; }
            set { _PresentacionEstatus = value; }
        }
        #endregion Propiedades

    }
}
