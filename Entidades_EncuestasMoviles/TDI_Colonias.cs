using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Colonias
    {
        #region Constructor
        public TDI_Colonias() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Colonia.
        /// </summary>
        private int _idColonia;
        /// <summary>
        /// Nombre de la Colonia.
        /// </summary>
        private string _coloniaNombre;
        /// <summary>
        /// Llave maestra del Municipio.
        /// </summary>
        private TDI_Municipios _idMunicipio;
        /// <summary>
        /// Llave maestra del Asentamiento.
        /// </summary>
        private THE_TipoAsenta _idAsentamiento;
        /// <summary>
        /// Llave maestra de la Zona.
        /// </summary>
        private THE_TipoZona _idZona;
        #endregion

        #region Propiedades
        public virtual int IdColonia
        {
            get { return _idColonia; }
            set { _idColonia = value; }
        }
        public virtual string ColoniaNombre
        {
            get { return _coloniaNombre; }
            set { _coloniaNombre = value; }
        }
        public virtual TDI_Municipios IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }
        public virtual THE_TipoAsenta IdAsentamiento
        {
            get { return _idAsentamiento; }
            set { _idAsentamiento = value; }
        }
        public virtual THE_TipoZona IdZona
        {
            get { return _idZona; }
            set { _idZona = value; }
        }
        #endregion
    }
}
