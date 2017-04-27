using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Municipios
    {
        #region Constructor
        public TDI_Municipios() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Municipio.
        /// </summary>
        private int _idMunicipio;
        /// <summary>
        /// Nombre del Municipio.
        /// </summary>
        private string _municipioNombre;
        /// <summary>
        /// Ciudad a la que pertenece el Municipio.
        /// </summary>
        private TDI_Estado _municipioEstado;
        /// <summary>
        /// Indica el Estatus del Municipio.
        /// </summary>
        private char _municipioStat;
        #endregion

        #region Propiedades
        public virtual int IdMunicipio
        {
            get { return _idMunicipio; }
            set { _idMunicipio = value; }
        }
        public virtual string MunicipioNombre
        {
            get { return _municipioNombre; }
            set { _municipioNombre = value; }
        }
        public virtual TDI_Estado MunicipioEstado
        {
            get { return _municipioEstado; }
            set { _municipioEstado = value; }
        }
        public virtual char MunicipioStat
        {
            get { return _municipioStat; }
            set { _municipioStat = value; }
        }
        #endregion
    }
}
