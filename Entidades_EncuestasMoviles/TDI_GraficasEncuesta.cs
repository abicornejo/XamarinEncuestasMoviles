using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_GraficasEncuesta
    {
        #region Constructor
        public TDI_GraficasEncuesta() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Id de la pregunta
        /// </summary>
        private int _idPregunta;
        /// <summary>
        /// Descripción de la pregunta
        /// </summary>
        private string _PreguntaDescripcion;
        /// <summary>
        /// Id de la respuesta
        /// </summary>
        private int _idRespuesta;
        /// <summary>
        ///  Descripción de la respuesta
        /// </summary>
        private string _RespuestaDescripcion;
        /// <summary>
        /// Contador de la respuesta
        /// </summary>
        private int _contador;
        /// <summary>
        /// Id de la siguiente pregunta
        /// </summary>
        private int _idSiguientePregunta;
        /// <summary>
        /// Total Respondieron Encuesta
        /// </summary>
        private int _total;
        /// <summary>
        /// Dispositivos contestaron la encuesta
        /// </summary>
        private string _dispositivos;
        /// <summary>
        /// Id del tipo de respuesta multiple seleccion o solo unica seleccion
        /// </summary>
        private int _IdTipoResp;
        /// <summary>
        /// Id del tipo de respuesta multiple seleccion o solo unica seleccion
        /// </summary>
        private string _numTelefonicos;
        /// <summary>
        /// Id Dispositivo
        /// </summary>
        private int _id_dispo;
        /// <summary>
        /// Id opcion del catalogo
        /// </summary>
        private int id_opcion_cata;
        /// <summary>
        /// Descripcion del catalogo
        /// </summary>
        private string _desc_catalogo;
        #endregion

        #region Propiedades
        public virtual int IdPregunta
        {
            get { return _idPregunta; }
            set { _idPregunta = value; }
        }

        public virtual string PreguntaDescripcion
        {
            get { return _PreguntaDescripcion; }
            set { _PreguntaDescripcion = value; }
        }

        public virtual int IdRespuesta
        {
            get { return _idRespuesta; }
            set { _idRespuesta = value; }
        }

        public virtual string RespuestaDescripcion
        {
            get { return _RespuestaDescripcion; }
            set { _RespuestaDescripcion = value; }
        }

        public virtual int Contador
        {
            get { return _contador; }
            set { _contador = value; }
        }

        public virtual int IdSiguientePregunta
        {
            get { return _idSiguientePregunta; }
            set { _idSiguientePregunta = value; }
        }

        public virtual int Total
        {
            get { return _total; }
            set { _total = value; }
        }

        public virtual string Dispositivos
        {
            get { return _dispositivos; }
            set { _dispositivos = value; }
        }

        public virtual int IDTipoResp
        {
            get { return _IdTipoResp; }
            set { _IdTipoResp = value; }
        }

        public virtual string Num_Telefonicos
        {
            get { return _numTelefonicos; }
            set { _numTelefonicos = value; }
        }

        public virtual int ID_Dispo
        {
            get { return _id_dispo; }
            set { _id_dispo = value; }
        }

        public virtual int ID_Opcion_Catalogo
        {
            get { return id_opcion_cata; }
            set { id_opcion_cata = value; }
        }

        public virtual string Desc_Catalogo
        {
            get { return _desc_catalogo; }
            set { _desc_catalogo = value; }
        }

        #endregion
    }
}
