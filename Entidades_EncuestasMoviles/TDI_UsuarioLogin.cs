using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;

namespace EncuestasMoviles
{
    [Serializable]
    public class TDI_UsuarioLogin
    {
        #region Attributes
        private TDI_TipoAcceso _TipoAcceso;
        private string _Usuario;
        #endregion

        #region Properties
        public virtual TDI_TipoAcceso TipoAcceso { get { return _TipoAcceso; } set { _TipoAcceso = value; } }
        public virtual string Usuario { get { return _Usuario; } set { _Usuario = value; } }
        #endregion

        #region Constructor
        public TDI_UsuarioLogin()
        {
        }
        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            TDI_UsuarioLogin objUsuarioLogin = obj as TDI_UsuarioLogin;
            if (objUsuarioLogin == null)
                return false;

            if (this.TipoAcceso != objUsuarioLogin.TipoAcceso)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this.TipoAcceso.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
