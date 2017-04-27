using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Programacion
    {
        #region Constructor
        public THE_Programacion() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Programación.
        /// </summary>
        private int _idProgramacion;
        /// <summary>
        /// Nombre de la Programación.
        /// </summary>
        private string _programacionNombre;
        /// <summary>
        /// Llave maestra de la Encuesta a la que Pertenece.
        /// </summary>
        private THE_Encuesta _idEncuesta;
        /// <summary>
        /// Almacena las Programaciones Asociadas a la Encuesta.
        /// </summary>
        private List<THE_Programacion> _listaProgramaciones;
        /// <summary>
        /// id de el tipo de programación
        /// </summary>
        private TDI_TipoProgramacion _idTipoProgramacion;       
        /// <summary>
        /// Estatus de la Programación.
        /// </summary>
        private char _programacionEstatus;
        /// <summary>
        /// Id del dispositivo el cual se le asignara la programacion de la encuesta.
        /// </summary>
        //private THE_Dispositivo _idDispositivo;

        private string _ENCUESTANOMBRE,_DESCPROGRAMACION, _HORA, _FECHA;

        private int _IDENC, _IDTIPOPROGRA,_IDPROFECHASEMANA;
       
        #endregion

        #region Propiedades
        public virtual int IdProgramacion
        {
            get { return _idProgramacion; }
            set { _idProgramacion = value; }
        }
        public virtual string ProgramacionNombre
        {
            get { return _programacionNombre; }
            set { _programacionNombre = value; }
        }
        public virtual THE_Encuesta IdEncuesta
        {
            get { return _idEncuesta; }
            set { _idEncuesta = value; }
        }
        public virtual List<THE_Programacion> ListaProgramaciones
        {
            get { return _listaProgramaciones; }
            set { _listaProgramaciones = value; }
        } 
        public virtual char ProgramacionEstatus
        {
            get { return _programacionEstatus; }
            set { _programacionEstatus = value; }
        }
        public virtual TDI_TipoProgramacion IdTipoProgramacion
        {
            get { return _idTipoProgramacion; }
            set { _idTipoProgramacion = value; }
        }
        public virtual int IdTipoProg
        {
            get { return IdTipoProgramacion.IdTipoProgramacion; }
        }
        public virtual string DescTipoProg
        {
            get { return IdTipoProgramacion.TipoProgramacionDescripcion; }
        }

        //public virtual THE_Dispositivo ID_Dispositivo
        //{
        //    get { return _idDispositivo; }
        //    set { _idDispositivo = value; }
        //}

        public virtual string ENCUESTANOMBRE{
            get { return _ENCUESTANOMBRE; }
            set { _ENCUESTANOMBRE = value; }
        }

        public virtual string DESCTIPOPROGRAMACION
        {
            get { return _DESCPROGRAMACION; }
            set { _DESCPROGRAMACION = value; }
        }

        public virtual int IDENC
        {
            get { return _IDENC; }
            set { _IDENC = value; }
        }

        public virtual int IDTIPOPROGRA
        {
            get { return _IDTIPOPROGRA; }
            set { _IDTIPOPROGRA = value; }
        }
        public virtual int IDPROFECHASEMANA
        {
            get { return _IDPROFECHASEMANA; }
            set { _IDPROFECHASEMANA = value; }
        }

        public virtual string HORA
        {
            get { return _HORA; }
            set { _HORA = value; }
        } 
        public virtual string FECHA
        {
            get { return _FECHA; }
            set { _FECHA = value; }
        }
        #endregion
    }
}
