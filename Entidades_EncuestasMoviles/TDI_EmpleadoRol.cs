using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_EmpleadoRol
    {
        #region Constructor
        public TDI_EmpleadoRol()
        {
            _empleadollaveprimaria = new THE_Empleado();
            _idRol = new TDI_Rol();
        }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Empleado.
        /// </summary>
        private THE_Empleado _empleadollaveprimaria;
        /// <summary>
        /// Llave maestra del Rol.
        /// </summary>
        private TDI_Rol _idRol;
        #endregion

        #region Propiedades
        public virtual THE_Empleado EmpleadoLlavePrimaria
        {
            get { return _empleadollaveprimaria; }
            set { _empleadollaveprimaria = value; }
        }
        public virtual TDI_Rol IdRol
        {
            get { return _idRol; }
            set { _idRol = value; }
        }
        #endregion

        #region Metodos
        public override bool Equals(object obj)
        {
            if (this == obj)
            { return true; }

            TDI_EmpleadoRol oEmplRol = obj as TDI_EmpleadoRol;
            if (oEmplRol == null)
            { return false; }

            if (this._empleadollaveprimaria.EmpleadoLlavePrimaria != oEmplRol._empleadollaveprimaria.EmpleadoLlavePrimaria && this._idRol.IdRol != oEmplRol._idRol.IdRol)
            { return false; }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this._empleadollaveprimaria.GetHashCode() + this._idRol.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
