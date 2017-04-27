using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Catalogo
    {
        #region Constructor
        public THE_Catalogo() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Catalogo.
        /// </summary>
        private int _idCatalogo;
        /// <summary>
        /// Descripcion del Catalogo.
        /// </summary>
        private string _catalogoDesc;
        /// <summary>
        /// Estatus del Catalogo.
        /// </summary>
        private char _catalogoStat;
        #endregion

        #region Propiedades
        public virtual int IdCatalogo
        {
            get { return _idCatalogo; }
            set { _idCatalogo = value; }
        }
        public virtual string CatalogoDesc
        {
            get { return _catalogoDesc; }
            set { _catalogoDesc = value; }
        }
        public virtual char CatalogoStat
        {
            get { return _catalogoStat; }
            set { _catalogoStat = value; }
        }
        #endregion
    }
}
