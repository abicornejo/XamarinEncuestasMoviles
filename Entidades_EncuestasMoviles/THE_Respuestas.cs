using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Respuestas
    {
        #region Constructor
        public THE_Respuestas() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Respuesta.
        /// </summary>
        private int _idRespuesta;
        /// <summary>
        /// Llave maestra de la Pregunta.
        /// </summary>
        private THE_Preguntas _idPregunta;
        /// <summary>
        /// Descripcion de la Respuesta.
        /// </summary>
        private string _respuestaDesc;
        /// <summary>
        /// Llave maestra de la Pregunta Siguiente.
        /// </summary>
        private int _idSiguientePregunta;
        /// <summary>
        /// Estatus de la Respuesta.
        /// </summary>
        private char _respuEstatus;
        /// <summary>
        /// Descripcion de la Pregunta Siguiente.
        /// </summary>
        private string _descSigPreg;
        #endregion

        #region Propiedades
        public virtual int IdRespuesta
        {
            get { return _idRespuesta; }
            set { _idRespuesta = value; }
        }
        public virtual THE_Preguntas IdPregunta
        {
            get { return _idPregunta; }
            set { _idPregunta = value; }
        }
        public virtual string RespuestaDescripcion
        {
            get { return _respuestaDesc; }
            set { _respuestaDesc = value; }
        }
        public virtual int IdSiguientePregunta
        {
            get { return _idSiguientePregunta; }
            set { _idSiguientePregunta = value; }
        }
        public virtual char RespuestaEstatus
        {
            get { return _respuEstatus; }
            set { _respuEstatus = value; }
        }
        public virtual string DescSigPreg
        {
            get { return _descSigPreg; }
            set { _descSigPreg = value; }
        }
        #endregion
    }
}
