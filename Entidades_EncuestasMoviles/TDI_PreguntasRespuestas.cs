using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_PreguntasRespuestas
    {

        #region Constructor
        public TDI_PreguntasRespuestas() { }
        #endregion 

        
        #region Atributos
        /// <summary>
        /// Llave Primaria de Secuencia Pregunta Respuesta
        /// </summary>
        private int _idPreguRespu;

        /// <summary>
        /// Dispositivo al que pertenece el registro
        /// </summary>
        private THE_Dispositivo _idDispositivo;

        /// <summary>
        /// Encuesta a la que pertenece el registro.
        /// </summary>
        private THE_Encuesta _idEncuesta;

        /// <summary>
        /// Pregunta  a la que pertenece el registro.
        /// </summary>
        private THE_Preguntas _idPregunta;

        /// <summary>
        /// Respuesta  a la que pertenece el registro.
        /// </summary>
        private THE_Respuestas _idRespuesta;
        /// <summary>
        /// Fecha en el que se registran las respuestas
        /// </summary>
        private TDI_EncuestaDispositivo _id_Envio;
        /// <summary>
        /// _IdEnvio aa respoder
        /// </summary>
        
        
        #endregion

        #region Propiedades

        public virtual int IdPreguntasRespuestas
        {
            get { return _idPreguRespu; }
            set { _idPreguRespu = value; }
        }

        public virtual THE_Dispositivo IdDispositivo
        {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }

        public virtual THE_Encuesta IdEncuesta
        {
            get { return _idEncuesta; }
            set { _idEncuesta = value; }
        }
        public virtual THE_Preguntas IdPregunta
        {
            get { return _idPregunta; }
            set { _idPregunta = value; }
        }

        public virtual THE_Respuestas IdRespuesta
        {
            get { return _idRespuesta; }
            set { _idRespuesta = value; }
        }

        public virtual TDI_EncuestaDispositivo IdEnvio {
            get { return _id_Envio; }
            set { _id_Envio = value; }
        }
        //public virtual DateTime FechaInsercion {

        //    get { return _fecha_insercion; }
        //    set { _fecha_insercion = value; }
        //}
        
        #endregion

    }
}
