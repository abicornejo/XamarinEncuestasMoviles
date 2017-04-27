using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_LogAcceso
    {
        #region Constructor
        public TDI_LogAcceso() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Log de Acceso.
        /// </summary>
        private int _idLogAcceso;
        /// <summary>
        /// Maquina del Log de Acceso.
        /// </summary>
        private string _logAccesoMaquina;
        /// <summary>
        /// Fecha del Log de Acceso.
        /// </summary>
        private DateTime _logAccesoFecha;
        /// <summary>
        /// Dominio del Log de Acceso.
        /// </summary>
        private string _logAccesoDominio;
        /// <summary>
        /// IP del Log de Acceso.
        /// </summary>
        private string _logAccesoIp;
        /// <summary>
        /// Llave maestra del Empleado.
        /// </summary>
        private string _empleadousua;
        /// <summary>
        /// Llave maestra del Tipo de Acceso.
        /// </summary>
        private TDI_TipoAcceso _idTipoAcceso;
        /// <summary>
        /// Llave maestra del Empleado.
        /// </summary>
        private THE_Empleado _emplLlavPr;
        #endregion

        #region Propiedades
        public virtual int IdLogAcceso
        {
            get { return _idLogAcceso; }
            set { _idLogAcceso = value; }
        }
        public virtual string LogAccesoMaquina
        {
            get { return _logAccesoMaquina; }
            set { _logAccesoMaquina = value; }
        }
        public virtual DateTime LogAccesoFecha
        {
            get { return _logAccesoFecha; }
            set { _logAccesoFecha = value; }
        }
        public virtual string LogAccesoDominio
        {
            get { return _logAccesoDominio; }
            set { _logAccesoDominio = value; }
        }
        public virtual string LogAccesoIP
        {
            get { return _logAccesoIp; }
            set { _logAccesoIp = value; }
        }
        public virtual string EmpleadoUsua
        {
            get { return _empleadousua; }
            set { _empleadousua = value; }
        }
        public virtual TDI_TipoAcceso IdTipoAcceso
        {
            get { return _idTipoAcceso; }
            set { _idTipoAcceso = value; }
        }
        public virtual THE_Empleado EmplLlavPr
        {
            get { return _emplLlavPr; }
            set { _emplLlavPr = value; }
        }
        #endregion
    }
}
