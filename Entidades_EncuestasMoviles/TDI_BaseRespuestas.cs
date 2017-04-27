using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_BaseRespuestas
    {
        #region Constructor
        public TDI_BaseRespuestas() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Id de la respueLlave maestra del Estatus.
        /// </summary>
        private int _idRespuesta;
        /// <summary>
        /// Descripcion del Estatus.
        /// </summary>
        private string _respuestasDesc;

        /// <summary>
        /// Estatus de la respuesta
        /// </summary>
        private string _estatus;
        
        #endregion

        #region Propiedades
        public virtual int IdRespuesta
        {
            get { return _idRespuesta; }
            set { _idRespuesta = value; }
        }

        public virtual string RespuestasDesc
        {
            get { return _respuestasDesc; }
            set { _respuestasDesc = value; }
        }

        public virtual string Estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
        }

        
        #endregion


    }
}
