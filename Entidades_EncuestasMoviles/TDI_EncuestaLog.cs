using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_EncuestaLog
    {
        #region Constructor
        public TDI_EncuestaLog() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Log de Encuesta.
        /// </summary>
        private int _idLog;
        /// <summary>
        /// Descripcion del Log de Encuesta.
        /// </summary>
        private string _logDesc;
        /// <summary>
        /// Fecha del Log de Encuesta.
        /// </summary>
        private DateTime _logFecha;
        /// <summary>
        /// Dispositivo del Log de Encuesta.
        /// </summary>
        private THE_Dispositivo _idDispositivo;
        /// <summary>
        /// Log de la Encuesta.
        /// </summary>
        private THE_Encuesta _idEncuesta;
        #endregion

        #region Propiedades
        public virtual int IdLog
        {
            get { return _idLog; }
            set { _idLog = value; }
        }
        public virtual string LogDescripcion
        {
            get { return _logDesc; }
            set { _logDesc = value; }
        }
        public virtual DateTime LogFecha
        {
            get { return _logFecha; }
            set { _logFecha = value; }
        }
        public virtual THE_Dispositivo IdDispositivo
        {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }
        public virtual THE_Encuesta IdEncuesta
        {
            get { return _idEncuesta; }
            set { _idEncuesta = value; }
        }
        #endregion
    }
}
