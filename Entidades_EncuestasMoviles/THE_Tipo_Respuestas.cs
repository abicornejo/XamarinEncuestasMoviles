using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Tipo_Respuestas
    {
        #region Constructor
        public THE_Tipo_Respuestas() { }
        #endregion

        #region Atributos

        /// <summary>
        /// id del tipo de respuesta.
        /// </summary>
        private int _id_tipo_resp;
        /// <summary>
        /// descripcion del tipo de respuesta.
        /// </summary>
        private string _desc_tipo_resp;
        /// <summary>
        /// status del tipo de respuesta.
        /// </summary>
        private string _status_tipo_resp;


        #endregion


        #region Propiedades

        public virtual int IdTipoResp
        {
            get { return _id_tipo_resp; }
            set { _id_tipo_resp = value; }
        }
        public virtual string DescTipoResp
        {
            get { return _desc_tipo_resp; }
            set { _desc_tipo_resp = value; }
        }
        public virtual string StatusTipoResp
        {
            get { return _status_tipo_resp; }
            set { _status_tipo_resp = value; }
        }

        #endregion

    }
}
