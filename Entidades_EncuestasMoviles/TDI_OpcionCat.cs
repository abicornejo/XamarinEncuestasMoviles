using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_OpcionCat
    {
        #region Constructor
        public TDI_OpcionCat() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del la Opcion del Catalogo.
        /// </summary>
        private int _idOpcionCat;
        /// <summary>
        /// Descripcion de la Opcion del Catalogo.
        /// </summary>
        private string _opcionCatDesc;
        /// <summary>
        /// Llave maestra del Catalogo.
        /// </summary>
        private THE_Catalogo _idCatalogo;
        /// <summary>
        /// Estatus de la  Opcion del Catalogo.
        /// </summary>
        private char _opcionCatStat;
        #endregion

        #region Propiedades
        public virtual int IdOpcionCat
        {
            get { return _idOpcionCat; }
            set { _idOpcionCat = value; }
        }
        public virtual string OpcionCatDesc
        {
            get { return _opcionCatDesc; }
            set { _opcionCatDesc = value; }
        }
        public virtual THE_Catalogo IdCatalogo
        {
            get { return _idCatalogo; }
            set { _idCatalogo = value; }
        }
        public virtual char OpcionCatStat
        {
            get { return _opcionCatStat; }
            set { _opcionCatStat = value; }
        }
        #endregion
    }
}
