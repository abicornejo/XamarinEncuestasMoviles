using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_SesionUsuario
    {
        #region Constructor

        public THE_SesionUsuario() { }

        #endregion

        #region Atributos     
        /// <summary>
        /// Ip del Error.
        /// </summary>
        private string _dirIP;

        /// <summary>
        /// Id del Empleado.
        /// </summary>
        private THE_Empleado _emplLlavPr;

        /// <summary>
        /// Id del Empleado.
        /// </summary>
        private int _idSesion;

        /// <summary>
        /// Fecha del Error.
        /// </summary>
        private DateTime _fechaCreacion;

        #endregion

        #region Propiedades

        public virtual string DirIP
        {
            get { return _dirIP; }
            set { _dirIP = value; }
        }

        public virtual THE_Empleado EmplLlavPr
        {
            get { return _emplLlavPr; }
            set { _emplLlavPr = value; }
        }

        public virtual DateTime FechaCreacion
        {
            get { return _fechaCreacion; }
            set { _fechaCreacion = value; }
        }

        public virtual int IdSesion 
        {
            get { return _idSesion; }
            set { _idSesion = value; }        
        }

        #endregion
    }
}
