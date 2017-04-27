using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_UsuarioCat
    {
        #region Constructor
        public TDI_UsuarioCat() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Usuario.
        /// </summary>
        private THE_Usuario _usuaLlavPr;
        /// <summary>
        /// Llave maestra del la Opcion del Catalogo.
        /// </summary>
        private TDI_OpcionCat _idOpcionCat;
        /// <summary>
        /// Estatus de la Opcion por Usuario.
        /// </summary>
        private char _usuacatStat;
        #endregion

        #region Propiedades
        public virtual THE_Usuario UsuaLlavPr
        {
            get { return _usuaLlavPr; }
            set { _usuaLlavPr = value; }
        }
        public virtual TDI_OpcionCat IdOpcionCat
        {
            get { return _idOpcionCat; }
            set { _idOpcionCat = value; }
        }
        public virtual char UsuaCatStat
        {
            get { return _usuacatStat; }
            set { _usuacatStat = value; }
        }
        #endregion

        #region Metodos
        public override bool Equals(object obj)
        {
            if (this == obj)
            { return true; }

            TDI_UsuarioCat oUsuaDispo = obj as TDI_UsuarioCat;
            if (oUsuaDispo == null)
            { return false; }

            if (this._usuaLlavPr.UsuarioLlavePrimaria != oUsuaDispo._usuaLlavPr.UsuarioLlavePrimaria && this._idOpcionCat.IdOpcionCat != oUsuaDispo._idOpcionCat.IdOpcionCat)
            { return false; }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this._usuaLlavPr.GetHashCode() + this._idOpcionCat.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
