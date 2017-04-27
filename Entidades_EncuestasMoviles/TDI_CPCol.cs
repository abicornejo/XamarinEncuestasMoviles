using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_CPCol
    {
        #region Constructor
        public TDI_CPCol() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Codigo Postal.
        /// </summary>
        private THE_CodigoPostal _idCodigoPostal;
        /// <summary>
        /// Llave maestra de la Colonia.
        /// </summary>
        private TDI_Colonias _idColonia;
        #endregion

        #region Propiedades
        public virtual THE_CodigoPostal IdCodigoPostal
        {
            get { return _idCodigoPostal; }
            set { _idCodigoPostal = value; }
        }
        public virtual TDI_Colonias IdColonia
        {
            get { return _idColonia; }
            set { _idColonia = value; }
        }
        #endregion

        #region Metodos
        public override bool Equals(object obj)
        {
            if (this == obj)
            { return true; }

            TDI_CPCol oCPCol = obj as TDI_CPCol;
            if (oCPCol == null)
            { return false; }

            if (this._idCodigoPostal.IdCodigoPostal != oCPCol._idCodigoPostal.IdCodigoPostal && this._idColonia.IdColonia != oCPCol._idColonia.IdColonia)
            { return false; }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this._idCodigoPostal.GetHashCode() + this._idColonia.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
