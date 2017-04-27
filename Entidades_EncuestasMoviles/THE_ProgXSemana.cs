using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_ProgXSemana
    {
        #region Constructor
        public THE_ProgXSemana() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de Programacion por semana
        /// </summary>
        private int _idProgXSemana;
        /// <sumary>
        /// Día de programación
        /// </summary>
        private string _dia;
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
        public virtual int IdProgXSemana
        {
            get { return _idProgXSemana; }
            set { _idProgXSemana = value; }
        }
        public virtual string Dia
        {
            get { return _dia; }
            set { _dia = value; }
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
