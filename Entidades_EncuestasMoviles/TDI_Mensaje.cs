using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Mensaje
    {
        #region Constructor
        public TDI_Mensaje() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Mensaje.
        /// </summary>
        private int _idMensaje;
        /// <summary>
        /// Dispositivo del Mensaje.
        /// </summary>
        private THE_Dispositivo _idDispositivo;
        /// <summary>
        /// Empleado crea el Mensaje.
        /// </summary>
        private THE_Empleado _empleadollaveprimaria;
        /// <summary>
        /// Texto del Mensaje.
        /// </summary>
        private string _mensajeDesc;
        /// <summary>
        /// Estatus del Mensaje.
        /// </summary>
        private TDI_EstatusMensaje _estatusMensaje;
        #endregion

        #region Propiedades
        public virtual int IdMensaje
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
        public virtual string MensajeDescripcion
        {
            get { return _mensajeDesc; }
            set { _mensajeDesc = value; }
        }
        public virtual TDI_EstatusMensaje EstatusMensaje
        {
            get { return _estatusMensaje; }
            set { _estatusMensaje = value; }
        }
        #endregion
    }
}
