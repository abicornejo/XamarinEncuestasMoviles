using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public  class TDI_TieneRespAleatorias
    {
        #region Constructor
        public TDI_TieneRespAleatorias() { }
        #endregion

        #region Atributos
        /// <summary>
        /// id del tipo de respuesta.
        /// </summary>
        private int _id_tiene_resp_alea;
        /// <summary>
        /// descripcion del tipo de respuesta.
        /// </summary>
        private string _desc_tiene_resp_alea;
        /// <summary>
        /// status del tipo de respuesta.
        /// </summary>
        private string _status_tiene_resp_alea;

        #endregion

        public virtual int IdPreAleatoria
        {
            get { return _id_tiene_resp_alea; }
            set { _id_tiene_resp_alea = value; }
        }
        public virtual string DescTieneRespAlea
        {
            get { return _desc_tiene_resp_alea; }
            set { _desc_tiene_resp_alea = value; }
        }
        public virtual string StatusTieneRespAlea
        {
            get { return _status_tiene_resp_alea; }
            set { _status_tiene_resp_alea = value; }
        }

        #region Propiedades
        #endregion
    }
}
