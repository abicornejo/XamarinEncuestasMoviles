using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_EncEncrypt
    {
        #region Contructor

        public TDI_EncEncrypt() { }

        #endregion

        #region Atributos

        /// <summary>
        /// id autoincrementable de la tabla 
        /// </summary>
        private int _idEncEncrypt;
        /// <summary>
        /// id de la encuesta encriptada
        /// </summary>
        private string _idEncrypt;
        /// <summary>
        /// llave foranea de la encuesta
        /// </summary>
        private THE_Encuesta _idEncuesta;


        #endregion

        #region Propiedades

        public virtual int IdEncEncrypt
        {
            get { return _idEncEncrypt; }
            set { _idEncEncrypt = value; }
        }
        public virtual string IdEncrypt
        {
            get { return _idEncrypt; }
            set { _idEncrypt = value; }
        }
        public virtual THE_Encuesta IdEncuesta
        {
            get { return _idEncuesta; }
            set { _idEncuesta = value; }
        }


        #endregion
    }
}
