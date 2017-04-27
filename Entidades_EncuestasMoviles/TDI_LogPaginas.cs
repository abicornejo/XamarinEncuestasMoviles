using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_LogPaginas
    {
        #region Constructor
        public TDI_LogPaginas() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Log de Acceso.
        /// </summary>
        private int _idLogPagina;
        /// <summary>
        /// Maquina del Log de Acceso.
        /// </summary>
        private DateTime _logFecha;
        /// <summary>
        /// Fecha del Log de Acceso.
        /// </summary>
        private string _logIp;
        /// <summary>
        /// Dominio del Log de Acceso.
        /// </summary>
        private string _logUrlPagina;
        /// <summary>
        /// Llave maestra del Empleado.
        /// </summary>
        private THE_Empleado _empleadollaveprimaria;       
        #endregion

        #region Propiedades
        public virtual int IdLogPagina
        {
            get { return _idLogPagina; }
            set { _idLogPagina = value; }
        }
        public virtual DateTime LogFecha
        {
            get { return _logFecha; }
            set { _logFecha = value; }
        }
        public virtual string LogIp
        {
            get { return _logIp; }
            set { _logIp = value; }
        }
        public virtual string LogUrlPagina
        {
            get { return _logUrlPagina; }
            set { _logUrlPagina = value; }
        }
        
        public virtual THE_Empleado EmpleadoLlavePrimaria
        {
            get { return _empleadollaveprimaria; }
            set { _empleadollaveprimaria = value; }
        }        
        #endregion
    }
}
