using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Dispositivo
    {
        #region Constructor
        public THE_Dispositivo() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Dispositivo.
        /// </summary>
        private int _idDispositivo;
        /// <summary>
        /// Numero de Telefono del Dispositivo.
        /// </summary>
        private string _dispoNumTelefono;
        /// <summary>
        /// Modelo del Dispositivo.
        /// </summary>
        private string _dispoModelo;
        /// <summary>
        /// Marca del Dispositivo.
        /// </summary>
        private string _dispoMarca;
        /// <summary>
        /// Imagen del Dispositivo.
        /// </summary>
        private string _dispoImagen;
        /// <summary>
        /// Descripcion del Dispositivo.
        /// </summary>
        private string _dispoDesc;
        /// <summary>
        /// Meid del Dispositivo.
        /// </summary>
        private string _dispoMeid;
        /// <summary>
        /// Mdn del Dispositivo.
        /// </summary>
        private string _dispoMdn;
        /// <summary>
        /// Estatus del Dispositivo.
        /// </summary>
        private char _dispoEstatus;
        /// <summary>
        /// Identifica Encuesta Dispositivo.
        /// </summary>
        private string _colorEstatus;
        /// <summary>
        /// Estatus CheckBox.
        /// </summary>
        private string _estatusCheck;
        /// <summary>
        /// Checked CheckBox.
        /// </summary>
        private string _chkEnabled;
        /// <summary>
        /// Colo saber Estatus.
        /// </summary>
        private string _strColor;
        /// <summary>
        /// Token del dispositivo
        /// </summary>
        private string _tokenDispo;




        #endregion

        #region Propiedades
        public virtual int IdDispositivo
        {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }
        public virtual string NumerodelTelefono
        {
            get { return _dispoNumTelefono; }
            set { _dispoNumTelefono = value; }
        }
        public virtual string Modelo
        {
            get { return _dispoModelo; }
            set { _dispoModelo = value; }
        }
        public virtual string Marca
        {
            get { return _dispoMarca; }
            set { _dispoMarca = value; }
        }
        public virtual string ImagenTelefono
        {
            get { return _dispoImagen; }
            set { _dispoImagen = value; }
        }
        public virtual string DispositivoDesc
        {
            get { return _dispoDesc; }
            set { _dispoDesc = value; }
        }
        public virtual string DispositivoMeid
        {
            get { return _dispoMeid; }
            set { _dispoMeid = value; }
        }
        public virtual string DispositivoMdn
        {
            get { return _dispoMdn; }
            set { _dispoMdn = value; }
        }
        public virtual char DispositivoEstatus
        {
            get { return _dispoEstatus; }
            set { _dispoEstatus = value; }
        }
        public virtual string ColorEstatus
        {
            get { return _colorEstatus; }
            set { _colorEstatus = value; }
        }
        public virtual string EstatusCheck
        {
            get { return _estatusCheck; }
            set { _estatusCheck = value; }
        }
        public virtual string ChkEnabled
        {
            get { return _chkEnabled; }
            set { _chkEnabled = value; }
        }
        public virtual string StrColor
        {
            get { return _strColor; }
            set { _strColor = value; }
        }
        public virtual string TokenDispositivo
        {
            get { return _tokenDispo; }
            set { _tokenDispo = value; }
        }

        #endregion
    }
}
