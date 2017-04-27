using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Transacc
    {
        #region Constructor
        public TDI_Transacc() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Transaccion.
        /// </summary>
        private int _tranLlavPr;
        /// <summary>
        /// Descripcion de la Transaccion.
        /// </summary>
        private string _tranDesc;
        /// <summary>
        /// Estatus de la Transaccion.
        /// </summary>
        private char _tranStat;
        #endregion

        #region Propiedades
        public virtual int TranLlavPr
        {
            get { return _tranLlavPr; }
            set { _tranLlavPr = value; }
        }
        public virtual string TranDesc
        {
            get { return _tranDesc; }
            set { _tranDesc = value; }
        }
        public virtual char TranStat
        {
            get { return _tranStat; }
            set { _tranStat = value; }
        }
        #endregion
    }
}
