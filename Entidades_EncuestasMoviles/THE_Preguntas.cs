using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Preguntas
    {
        #region Constructor
        public THE_Preguntas() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Pregunta.
        /// </summary>
        private int _idPregunta;
        /// <summary>
        /// Descripcion de la Pregunta.
        /// </summary>
        private string _preguntaDesc;
        /// <summary>
        /// Llave maestra de la Encuesta a la que Pertenece.
        /// </summary>
        private THE_Encuesta _idEncuesta;
        /// <summary>
        /// Almacena las Preguntas Asociadas a la Encuesta.
        /// </summary>
        private List<THE_Preguntas> _listaPreguntas;
        /// <summary>
        /// Almacena las Respuestas Asociadas a la Pregunta.
        /// </summary>
        private List<THE_Respuestas> _lstResp;
        /// <summary>
        /// Fecha de Creación de la Pregunta.
        /// </summary>
        private DateTime _fechCrea;
        /// <summary>
        /// Estatus de la Pregunta.
        /// </summary>
        private char _estatus;
        /// <summary>
        /// id de el tipo de respuesta
        /// </summary>
        private THE_Tipo_Respuestas _id_tipo_resp;
        /// <summary>
        /// id de la restriccion de las respuestas
        /// </summary>
        private TDI_TieneRespAleatorias _id_pre_aleatoria;




        #endregion

        #region Propiedades
        public virtual int IdPregunta
        {
            get { return _idPregunta; }
            set { _idPregunta = value; }
        }
        public virtual string PreguntaDesc
        {
            get { return _preguntaDesc; }
            set { _preguntaDesc = value; }
        }
        public virtual THE_Encuesta IdEncuesta
        {
            get { return _idEncuesta; }
            set { _idEncuesta = value; }
        }
        public virtual List<THE_Preguntas> ListaPreguntas
        {
            get { return _listaPreguntas; }
            set { _listaPreguntas = value; }
        }
        public virtual List<THE_Respuestas> ListaRespuestas
        {
            get { return _lstResp; }
            set { _lstResp = value; }
        }
        public virtual DateTime FechaCrea
        {
            get { return _fechCrea; }
            set { _fechCrea = value; }
        }
        public virtual char Estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
        }

        public virtual THE_Tipo_Respuestas IdTipoResp
        {
            get { return _id_tipo_resp; }
            set { _id_tipo_resp = value; }
        }

        public virtual TDI_TieneRespAleatorias IdPreAleatoria
        {
            get { return _id_pre_aleatoria; }
            set { _id_pre_aleatoria = value; }
        }

        public virtual int IdResp
        {
            get { return IdTipoResp.IdTipoResp; }
        }
        public virtual string DescResp
        {

            get { return IdTipoResp.DescTipoResp; }
        }


        public virtual int IdRespAleatorias
        {
            get { return IdPreAleatoria.IdPreAleatoria; }
        }
        public virtual string DescRespAleatorias
        {

            get { return IdPreAleatoria.DescTieneRespAlea; }
        }


        #endregion
    }
}
