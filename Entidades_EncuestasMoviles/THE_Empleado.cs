using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Empleado
    {
        #region Constructor
        public THE_Empleado() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del empleado.
        /// </summary>
        private int _empleadollaveprimaria;
        /// <summary>
        /// Nombre del empleado.
        /// </summary>
        private string _empleadoNombre;
        /// <summary>
        /// Apellido Paterno del empleado.
        /// </summary>
        private string _empleadoApellPaterno;
        /// <summary>
        /// Apellido Materno del empleado.
        /// </summary>
        private string _empleadoApellMaterno;
        /// <summary>
        /// Usuario de Red del empleado.
        /// </summary>
        private string _empleadoUsua;
        /// <summary>
        /// Estatus del empleado.
        /// </summary>
        private char _empleadoEstatus;
        /// <summary>
        /// email del empleado.
        /// </summary>
        private string _empleadoMail;


        #endregion

        #region Propiedades
        public virtual int EmpleadoLlavePrimaria
        {
            get { return _empleadollaveprimaria; }
            set { _empleadollaveprimaria = value; }
        }
        public virtual string EmpleadoNombre
        {
            get { return _empleadoNombre; }
            set { _empleadoNombre = value; }
        }
        public virtual string EmpleadoApellPaterno
        {
            get { return _empleadoApellPaterno; }
            set { _empleadoApellPaterno = value; }
        }
        public virtual string EmpleadoApellMaterno
        {
            get { return _empleadoApellMaterno; }
            set { _empleadoApellMaterno = value; }
        }
        public virtual string EmpleadoUsua
        {
            get { return _empleadoUsua; }
            set { _empleadoUsua = value; }
        }
        public virtual char EmpleadoEstatus
        {
            get { return _empleadoEstatus; }
            set { _empleadoEstatus = value; }
        }
        public virtual string EmpleadoMail
        {
            get { return _empleadoMail; }
            set { _empleadoMail = value; }
        }
        #endregion
    }
}
