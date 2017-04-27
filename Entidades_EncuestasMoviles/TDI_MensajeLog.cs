using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_MensajeLog
    {
        #region Constructor
        public TDI_MensajeLog() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Mensaje Log.
        /// </summary>
        private int _idMensajeLog;
        /// <summary>
        /// Descripcion del Mensaje Log.
        /// </summary>
        private string _mensajeLogDesc;
        /// <summary>
        /// Fecha del Mensaje Log.
        /// </summary>
        private DateTime _mensajeLogFecha;
        /// <summary>
        /// Llave maestra del Mensaje.
        /// </summary>
        private TDI_Mensaje _idMensaje;
        /// <summary>
        /// Llave maestra del Dispositivo.
        /// </summary>
        private THE_Dispositivo _idDispositivo;
        /// <summary>
        /// Llave maestra del Empleado.
        /// </summary>
        private THE_Empleado _empleadollaveprimaria;
        #endregion

        #region Propiedades
        public virtual int IdMensajeLog
        {
            get { return _idMensajeLog; }
            set { _idMensajeLog = value; }
        }
        public virtual string MensajeLogDescripcion
        {
            get { return _mensajeLogDesc; }
            set { _mensajeLogDesc = value; }
        }
        public virtual DateTime MensajeLogFecha
        {
            get { return _mensajeLogFecha; }
            set { _mensajeLogFecha = value; }
        }
        public virtual TDI_Mensaje IdMensaje
        {
            get { return _idMensaje; }
            set { _idMensaje = value; }
        }
        public virtual THE_Dispositivo IdDispositivo
        {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }
        public virtual THE_Empleado EmpleadoLlavePrimaria
        {
            get { return _empleadollaveprimaria; }
            set { _empleadollaveprimaria = value; }
        }
        #endregion
    }
}
