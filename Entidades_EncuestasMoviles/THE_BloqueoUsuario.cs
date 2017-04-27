using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_BloqueoUsuario
    {
        #region Atributos
        private TDI_TipoBloqueo _TipoBloqueo;
        private string _Usuario;
        private THE_Empleado _emplLlavPr;
        #endregion

        #region Propiedades
        public virtual TDI_TipoBloqueo TipoBloqueo { get { return _TipoBloqueo; } set { _TipoBloqueo = value; } }
        public virtual string Usuario { get { return _Usuario; } set { _Usuario = value; } }
        public virtual THE_Empleado EmplLlavPr
        {
            get { return _emplLlavPr; }
            set { _emplLlavPr = value; }
        }
        #endregion

        #region Constructor
        public THE_BloqueoUsuario()
        {
        }
        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            THE_BloqueoUsuario objBloqueUsuario = obj as THE_BloqueoUsuario;
            if (objBloqueUsuario == null)
                return false;

            if (this.TipoBloqueo.CveTipoBloqueo != objBloqueUsuario.TipoBloqueo.CveTipoBloqueo)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this.TipoBloqueo.CveTipoBloqueo.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
