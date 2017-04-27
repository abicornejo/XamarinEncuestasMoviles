using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Rol
    {
        #region Constructor
        public TDI_Rol() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Rol.
        /// </summary>
        private int _idRol;
        /// <summary>
        /// Descripcion del Rol.
        /// </summary>
        private string _rolDesc;
        /// <summary>
        /// Estatus del Rol.
        /// </summary>
        private char _rolEstatus;
        #endregion

        #region Propiedades
        public virtual int IdRol
        {
            get { return _idRol; }
            set { _idRol = value; }
        }
        public virtual string RolDescripcion
        {
            get { return _rolDesc; }
            set { _rolDesc = value; }
        }
        public virtual char RolEstatus
        {
            get { return _rolEstatus; }
            set { _rolEstatus = value; }
        }
        #endregion
    }
}
