using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_TipoEncuesta
    {
        #region Constructor
        public TDI_TipoEncuesta() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Tipo Encuesta.
        /// </summary>
        private int _idTipoEncuesta;
        /// <summary>
        /// Descripcion del Tipo.
        /// </summary>
        private string _tipoEncuestaDesc;
        /// <summary>
        /// Estatus tipo encuesta.
        /// </summary>
        private char _tipoEncuestaEstatus;
        #endregion

        #region Propiedades
        public virtual int IdTipoEncuesta
        {
            get { return _idTipoEncuesta; }
            set { _idTipoEncuesta = value; }
        }
        public virtual string TipoEncuestaDescripcion
        {
            get { return _tipoEncuestaDesc; }
            set { _tipoEncuestaDesc = value; }
        }
        public virtual char TipoEncuestaEstatus
        {
            get { return _tipoEncuestaEstatus; }
            set { _tipoEncuestaEstatus = value; }
        }
        #endregion
    }
}
