using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_LogError
    {
        #region Constructor
        public THE_LogError() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Error.
        /// </summary>
        private int _cveLogErrores;
        /// <summary>
        /// Llave maestra del Empleado.
        /// </summary>
        private string _emplUsua;
        /// <summary>
        /// Fecha del Error.
        /// </summary>
        private DateTime _fechaCreacion;
        /// <summary>
        /// Descripcion del Error.
        /// </summary>
        private string _error;
        /// <summary>
        /// Pantalla del Error.
        /// </summary>
        private string _pantalla;
        /// <summary>
        /// Ip del Error.
        /// </summary>
        private string _dirIP;
        /// <summary>
        /// Nombre de la Maquina del Error.
        /// </summary>
        private string _machineName;
        /// <summary>
        /// Dominio.
        /// </summary>
        private string _dominio;
        /// <summary>
        /// Id del Empleado.
        /// </summary>
        private THE_Empleado _emplLlavPr;
        #endregion

        #region Propiedades
        public virtual int CveLogErrores
        {
            get { return _cveLogErrores; }
            set { _cveLogErrores = value; }
        }
        public virtual string EmplUsua
        {
            get { return _emplUsua; }
            set { _emplUsua = value; }
        }
        public virtual DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }
        public virtual string Error
        {
            get { return _error; }
            set { _error = value; }
        }
        public virtual string Pantalla
        {
            get { return _pantalla; }
            set { _pantalla = value; }
        }
        public virtual string DirIP
        {
            get { return _dirIP; }
            set { _dirIP = value; }
        }
        public virtual string MachineName
        {
            get { return _machineName; }
            set { _machineName = value; }
        }
        public virtual string Dominio   
        {
            get { return _dominio; }
            set { _dominio = value; }
        }
        public virtual THE_Empleado EmplLlavPr
        {
            get { return _emplLlavPr; }
            set { _emplLlavPr = value; }
        }
        #endregion
    }
}
