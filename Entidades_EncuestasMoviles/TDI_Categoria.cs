using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Categoria
    {
        #region Constructor
        public TDI_Categoria() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra de la Categoria.
        /// </summary>
        private int _idCategoria;
        /// <summary>
        /// Descripcion de la Categoria.
        /// </summary>
        private string _categoriaDesc;
        /// <summary>
        /// Fecha de la Categoria.
        /// </summary>
        private DateTime _categoriaFecha;
        /// <summary>
        /// Categoria de la que Depende.
        /// </summary>
        private int _categoriaDepe;
        /// <summary>
        /// Empleado que crea la Categoria.
        /// </summary>
        private THE_Empleado _empleadollaveprimaria;
        /// <summary>
        /// Usuario al que se le asigna la Categoria.
        /// </summary>
        private THE_Usuario _usuariollaveprimaria;
        #endregion

        #region Propiedades
        public virtual int IdCategoria
        {
            get { return _idCategoria; }
            set { _idCategoria = value; }
        }
        public virtual string CategoriaDescripcion
        {
            get { return _categoriaDesc; }
            set { _categoriaDesc = value; }
        }
        public virtual DateTime CategoriaFecha
        {
            get { return _categoriaFecha; }
            set { _categoriaFecha = value; }
        }
        public virtual int CategoriaDepe
        {
            get { return _categoriaDepe; }
            set { _categoriaDepe = value; }
        }
        public virtual THE_Empleado EmpleadoLlavePrimaria
        {
            get { return _empleadollaveprimaria; }
            set { _empleadollaveprimaria = value; }
        }
        public virtual THE_Usuario UsuarioLlavePrimaria
        {
            get { return _usuariollaveprimaria; }
            set { _usuariollaveprimaria = value; }
        }
        #endregion
    }
}
