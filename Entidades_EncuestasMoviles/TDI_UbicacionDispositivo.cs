using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_UbicacionDispositivo
    {

        #region Constructor
        public TDI_UbicacionDispositivo() { }
        #endregion 

        
        #region Atributos
        /// <summary>
        /// Dispositivo al que pertenece el registro
        /// </summary>
        private THE_Dispositivo _idDispositivo;
        /// <summary>
        /// Dispositivo al que pertenece el registro
        /// </summary>
        private THE_Usuario _idUsuario;
        /// <summary>
        /// Longitud del dispositivo.
        /// </summary>
        private string _longitud;
        /// <summary>
        /// Latitud del dispositivo.
        /// </summary>
        private string _latitud;
        /// <summary>
        /// fecha que fue insertado el registro.
        /// </summary>
        private DateTime _fecha;
        /// <summary>
        /// Llave Primaria de DispoUbicacion.
        /// </summary>
        private int _idDispoUbicacion;
        /// <summary>
        /// Info Dispositivo Ubicacion Cerca De.
        /// </summary>
        private string _dispoUbicacionCercaDe;        
        #endregion

        #region Propiedades

        public virtual THE_Dispositivo IdDispositivo
        {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }
        public virtual THE_Usuario IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        public virtual string Longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }
        public virtual string Latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }
        public virtual DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public virtual int IdDispoUbicacion
        {
            get { return _idDispoUbicacion; }
            set { _idDispoUbicacion = value; }
        }
        public virtual string DispoUbicacionCercaDe
        {
            get { return _dispoUbicacionCercaDe; }
            set { _dispoUbicacionCercaDe = value; }
        }        
        #endregion

    }
}
