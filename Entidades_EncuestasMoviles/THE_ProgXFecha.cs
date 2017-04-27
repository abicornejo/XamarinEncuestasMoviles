using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_ProgXFecha
    {
        #region Constructor
        public THE_ProgXFecha() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de Programacion por fecha
        /// </summary>
        private int _idProgXFecha;
        /// <sumary>
        /// Fecha de programación
        /// </summary>
        private DateTime _fecha;
        /// <summary>
        /// Hora de programación
        /// </summary>
        private string _hora;
        /// <summary>
        /// Id de Programación
        /// </summary>
        private THE_Programacion _idProgramacion;
        /// <summary>
        /// Estatus
        /// </summary>
        private char _estatus;
        #endregion

        #region Propiedades
        public virtual int IdProgXFecha
        {
            get { return _idProgXFecha; }
            set { _idProgXFecha = value; }
        }
        public virtual DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        public virtual string Hora
        {
            get { return _hora;}
            set { _hora = value; }
        }
        public virtual THE_Programacion IdProgramacion
        {
            get { return _idProgramacion; }
            set { _idProgramacion = value; }
        }
        public virtual int IdProg
        {
            get { return IdProgramacion.IdProgramacion; }
        }
        public virtual string DescProg
        {
            get { return IdProgramacion.ProgramacionNombre; }
        }        
        public virtual char Estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
        }
        #endregion
    }
}
