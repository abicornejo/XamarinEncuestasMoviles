using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Encuesta
    {
        #region Constructor
        public THE_Encuesta() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Encuesta.
        /// </summary>
        private int _idEncuesta;
        /// <summary>
        /// Nombre de la Encuesta.
        /// </summary>
        private string _nombreEncuesta;
        /// <summary>
        /// Fecha de Creacion de la Encuesta.
        /// </summary>
        private DateTime _fechaCrea;
        /// <summary>
        /// Fecha limite de la Encuesta.
        /// </summary>
        private string _fechLimite;
        /// <summary>
        /// Llave primaria del empleado que crea la encuesta.
        /// </summary>
        private THE_Empleado _empleadollaveprimaria;
        /// <summary>
        /// Llave maestra del estatus de la Encuesta.
        /// </summary>
        private TDI_Estatus _idestatus;
        /// <summary>
        /// Almacena las Preguntas Asociadas a la Encuesta.
        /// </summary>
        private List<THE_Preguntas> _lstPreg;
        /// <summary>
        /// Define el Estatus Activo o Inactivo de la Encuesta.
        /// </summary>
        private char _encuestaStat;
        /// <summary>
        /// Descripcion del Estatus.
        /// </summary>
        private string _nombreEstatus;
        /// <summary>
        /// Puntos de la Encuesta.
        /// </summary>
        private int _puntosEncuesta;
        /// <summary>
        /// Puntos minimos requeridos.
        /// </summary>
        private int _minRquerido;
        /// <summary>
        /// Puntos maximos esperados.
        /// </summary>
        private int _maxEsperado;
        /// <summary>
        /// Hora límite.
        /// </summary>
        private string _horaLimite;
        /// <summary>       
        /// Llave maestra del tipo de Encuesta.
        /// </summary>
        private TDI_TipoEncuesta _idTipoEncuesta;
        /// <summary>
        /// Almacena el tiempo en que cada encuesta se notificara si existe alguna encuesta.
        /// </summary>
        //private List<THE_PeriodoEncuesta> _lstPeriodos;
        private int _id_envio;

        private string _desc_id_tipoEnc;

        private int _id_tipoEnc;

        #endregion

        #region Propiedades
        public virtual int IdEncuesta
        {
            get { return _idEncuesta; }
            set { _idEncuesta = value; }
        }
        public virtual string NombreEncuesta
        {
            get { return _nombreEncuesta; }
            set { _nombreEncuesta = value; }
        }
        public virtual DateTime FechaCreaEncuesta
        {
            get { return _fechaCrea; }
            set { _fechaCrea = value; }
        }
        public virtual string FechaLimiteEncuesta
        {
            get { return _fechLimite; }
            set { _fechLimite = value; }
        }
        public virtual THE_Empleado EmpleadoLlavePrimaria
        {
            get { return _empleadollaveprimaria; }
            set { _empleadollaveprimaria = value; }
        }
        public virtual TDI_Estatus IdEstatus
        {
            get { return _idestatus; }
            set { _idestatus = value; }
        }
        public virtual List<THE_Preguntas> LstPreg
        {
            get { return _lstPreg; }
            set { _lstPreg = value; }
        }
        public virtual char EncuestaStat
        {
            get { return _encuestaStat; }
            set { _encuestaStat = value; }
        }
        public virtual string NombreEstatus
        {
            get { return _nombreEstatus; }
            set { _nombreEstatus = value; }
        }

        public virtual int PuntosEncuesta
        {
            get { return _puntosEncuesta; }
            set { _puntosEncuesta = value; }
        }

        public virtual int MinimoRequerido
        {
            get { return _minRquerido; }
            set { _minRquerido = value; }
        }
        public virtual int MaximoEsperado
        {
            get { return _maxEsperado; }
            set { _maxEsperado = value; }
        }
        public virtual string HoraLimiteEncuesta
        {
            get { return _horaLimite; }
            set { _horaLimite = value; }
        }
        public virtual TDI_TipoEncuesta IdTipoEncuesta
        {
            get { return _idTipoEncuesta; }
            set { _idTipoEncuesta = value; }
        }
        public virtual int IdEnvio
        {
            get { return _id_envio; }
            set { _id_envio = value; }
        }

        public virtual int IdTipoEnc
        {
            get { return _id_tipoEnc; }
            set { _id_tipoEnc = value; }
        }

        public virtual string DescIdTipoEnc
        {
            get{return _desc_id_tipoEnc;}
            set { _desc_id_tipoEnc=value; }
        } 


        //public virtual List<THE_PeriodoEncuesta> LstPeriodos
        //{
        //    get { return _lstPeriodos; }
        //    set { _lstPeriodos = value; }
        //}

        #endregion 
    }
}
