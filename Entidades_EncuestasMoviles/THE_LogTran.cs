using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_LogTran
    {
        #region Constructor
        public THE_LogTran() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del LogTransacciones.
        /// </summary>
        private int _logtLlavPr;
        /// <summary>
        /// Usuario realiza la Transaccion.
        /// </summary>
        private string _logtUsua;
        /// <summary>
        /// Fecha de Creacion de la Transaccion.
        /// </summary>
        private DateTime _logtFech;
        /// <summary>
        /// Ip crea la Transaccion.
        /// </summary>
        private string _logtUsip;
        /// <summary>
        /// Tipo de Transaccion.
        /// </summary>
        private TDI_Transacc _tranLlavPr;
        /// <summary>
        /// Descripcion de la Transaccion.
        /// </summary>
        private string _logtDesc;
        /// <summary>
        /// Dominio crea Transaccion.
        /// </summary>
        private string _logtDomi;
        /// <summary>
        /// Maquina crea Transaccion.
        /// </summary>
        private string _logtMach;
        /// <summary>
        /// Id Empleado.
        /// </summary>
        private THE_Empleado _emplLlavPr;
        #endregion

        #region Propiedades
        public virtual int LogtLlavPr
        {
            get { return _logtLlavPr; }
            set { _logtLlavPr = value; }
        }
        public virtual string LogtUsua
        {
            get { return _logtUsua; }
            set { _logtUsua = value; }
        }
        public virtual DateTime LogtFech
        {
            get { return _logtFech; }
            set { _logtFech = value; }
        }
        public virtual string LogtUsIp
        {
            get { return _logtUsip; }
            set { _logtUsip = value; }
        }
        public virtual TDI_Transacc TranLlavPr
        {
            get { return _tranLlavPr; }
            set { _tranLlavPr = value; }
        }
        public virtual string LogtDesc
        {
            get { return _logtDesc; }
            set { _logtDesc = value; }
        }
        public virtual string LogtDomi
        {
            get { return _logtDomi; }
            set { _logtDomi = value; }
        }
        public virtual string LogtMach
        {
            get { return _logtMach; }
            set { _logtMach = value; }
        }
        public virtual THE_Empleado EmplLlavPr
        {
            get { return _emplLlavPr; }
            set { _emplLlavPr = value; }
        }
        #endregion
    }
}
