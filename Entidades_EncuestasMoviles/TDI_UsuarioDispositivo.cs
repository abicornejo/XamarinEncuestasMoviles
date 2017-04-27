using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_UsuarioDispositivo
    {
        #region Constructor
        public TDI_UsuarioDispositivo() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Usuario.
        /// </summary>
        private THE_Usuario _usuariollaveprimaria;
        /// <summary>
        /// Llave maestra del Dispositivo.
        /// </summary>
        private THE_Dispositivo _idDispositivo;
        /// <summary>
        /// Estatus del Dispositivo por Usuario.
        /// </summary>
        private char _usuaDispoEstatus;
        #endregion

        #region Propiedades
        public virtual THE_Usuario UsuarioLlavePrimaria
        {
            get { return _usuariollaveprimaria; }
            set { _usuariollaveprimaria = value; }
        }
        public virtual THE_Dispositivo IdDispositivo
        {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }
        public virtual char UsuaDispoEstatus
        {
            get { return _usuaDispoEstatus; }
            set { _usuaDispoEstatus = value; }
        }
        #endregion

        #region Metodos
        public override bool Equals(object obj)
        {
            if (this == obj)
            { return true; }

            TDI_UsuarioDispositivo oUsuaDispo = obj as TDI_UsuarioDispositivo;
            if (oUsuaDispo == null)
            { return false; }

            if (this._idDispositivo.IdDispositivo != oUsuaDispo._idDispositivo.IdDispositivo && this._usuariollaveprimaria.UsuarioLlavePrimaria != oUsuaDispo._usuariollaveprimaria.UsuarioLlavePrimaria)
            { return false; }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this._idDispositivo.IdDispositivo.GetHashCode() + this._usuariollaveprimaria.UsuarioLlavePrimaria.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
