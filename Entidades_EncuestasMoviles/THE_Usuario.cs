using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_Usuario
    {
        #region Constructor
        public THE_Usuario() { }
        #endregion

        #region Atributos
        /// <summary>
        /// Llave maestra del Usuario.
        /// </summary>
        private int _usuariollaveprimaria;
        /// <summary>
        /// Nombre del Usuario.
        /// </summary>
        private string _usuarioNombre;
        /// <summary>
        /// Apellido Paterno del Usuario.
        /// </summary>
        private string _usuarioApellPaterno;
        /// <summary>
        /// Apellido Materno del Usuario.
        /// </summary>      
        private string _usuarioApellMaterno;
        /// <summary>
        /// Fecha de Nacimiento del Usuario.
        /// </summary>
        private string _usuarioFechNacimiento;
        /// <summary>
        /// Foto del Usuario.
        /// </summary>
        private string _usuarioFoto;
        /// <summary>
        /// Email del Usuario.
        /// </summary>
        private string _usuarioEmail;
        /// <summary>
        /// Calle y Numero del Usuario.
        /// </summary>
        private string _usuarioCalleNum;
        /// <summary>
        /// Telefono de Casa del Usuario.
        /// </summary>
        private string _usuarioTelCasa;
        /// <summary>
        /// Numero Celular Personal del Usuario.
        /// </summary>
        private string _usuarioNumCelularPersonal;
        /// <summary>
        /// Estatus del Usuario.
        /// </summary>
        private char _usuarioEstatus;
        /// <summary>
        /// Observaciones del Usuario.
        /// </summary>
        private string _usuarioObse;
        /// <summary>
        /// Datos del Domicilio del Usuario.
        /// </summary>
        private TDI_Colonias _idColonia;
        /// <summary>
        /// Codigo Postal del Usuario.
        /// </summary>
        private string _usuarioCP;
        /// <summary>
        /// Sexo del Usuario.
        /// </summary>
        private char _usuarioSexo;
        /// <summary>
        /// Imagen del Dispositivo Asignado al Usuario.
        /// </summary>
        private string _imgDispoUsuario;
        /// <summary>
        /// Estado Vive Usuario.
        /// </summary>
        private TDI_Estado _estadoInfo;
         /// <summary>
        /// Estado Vive Usuario.
        /// </summary>
       private string _UsuGen;
        /// <summary>
        /// Estado Vive Usuario.
        /// </summary>
        private string _UsuGrEdad;
        /// <summary>
        /// Grupo Edad.
        /// </summary>
        private string _UsuNse;
        /// <summary>
        /// NSE.
        /// </summary>
        private string _UsuEnc;
        /// <summary>
        /// Fecha de envio.
        /// </summary>
        private string _UsuFeEnv;
        /// <summary>
        /// Fecha de respuesta
        /// </summary>
        private string _UsuFeResp;
        /// <summary>
        /// Fecha empieza a contestar encuesta
        /// </summary>
        private string _UsuFeEmp;  /// <summary>
        /// Fechatermina de contestar la encuesta
        /// </summary>
        private string _UsuFeTer;





        private string _resp;
        private string _preg;

        private string _Catalogos;

        private string _DescDispo;

        private int _idDispositivo;

        private string _dispo_numTel, _dispo_modelo, _dispo_marca, _dispo_desc, _dispo_meid;

        #endregion

        #region Propiedades
        public virtual int UsuarioLlavePrimaria
        {
            get { return _usuariollaveprimaria; }
            set { _usuariollaveprimaria = value; }
        }
        public virtual string UsuarioNombre
        {
            get { return _usuarioNombre; }
            set { _usuarioNombre = value; }
        }
        public virtual string UsuarioApellPaterno
        {
            get { return _usuarioApellPaterno; }
            set { _usuarioApellPaterno = value; }
        }
        public virtual string UsuarioApellMaterno
        {
            get { return _usuarioApellMaterno; }
            set { _usuarioApellMaterno = value; }
        }
        public virtual string UsuarioFechNacimiento
        {
            get { return _usuarioFechNacimiento; }
            set { _usuarioFechNacimiento = value; }
        }
        public virtual string UsuarioFoto
        {
            get { return _usuarioFoto; }
            set { _usuarioFoto = value; }
        }
        public virtual string UsuarioEmail
        {
            get { return _usuarioEmail; }
            set { _usuarioEmail = value; }
        }
        public virtual string UsuarioCalleNum
        {
            get { return _usuarioCalleNum; }
            set { _usuarioCalleNum = value; }
        }
        public virtual string UsuarioTelCasa
        {
            get { return _usuarioTelCasa; }
            set { _usuarioTelCasa = value; }
        }
        public virtual string UsuarioNumCelularPersonal
        {
            get { return _usuarioNumCelularPersonal; }
            set { _usuarioNumCelularPersonal = value; }
        }
        public virtual char UsuarioEstatus
        {
            get { return _usuarioEstatus; }
            set { _usuarioEstatus = value; }
        }
        public virtual string UsuarioObse
        {
            get { return _usuarioObse; }
            set { _usuarioObse = value; }
        }
        public virtual TDI_Colonias IdColonia
        {
            get { return _idColonia; }
            set { _idColonia = value; }
        }
        public virtual string UsuarioCodigoPostal
        {
            get { return _usuarioCP; }
            set { _usuarioCP = value; }
        }
        public virtual char UsuarioSexo
        {
            get { return _usuarioSexo; }
            set { _usuarioSexo = value; }
        }
        public virtual string ImagenDispoUsuario
        {
            get { return _imgDispoUsuario; }
            set { _imgDispoUsuario = value; }
        }
        public virtual TDI_Estado EstadoInfo
        {
            get { return _estadoInfo; }
            set { _estadoInfo = value; }
        }
        public virtual string UsuaNom {
            get { return _usuarioNombre; }
            set { _usuarioNombre = value; }
        
        }
        public virtual string UsuGen {
            get { return _UsuGen; }
            set { _UsuGen = value; }
        }
        public virtual string UsuGrEdad{
            get { return _UsuGrEdad; }
            set { _UsuGrEdad = value; }
        }
        public virtual string UsuNse{
            get { return _UsuNse; }
            set { _UsuNse = value; }
        }
        public virtual string UsuEnc{
            get { return _UsuEnc; }
            set { _UsuEnc = value; }
        }
        public virtual string UsuFeEnv{
            get { return _UsuFeEnv; }
            set { _UsuFeEnv = value; }
        }
        public virtual string UsuFeResp{
            get { return _UsuFeResp; }
            set { _UsuFeResp = value; }
        }

        public virtual string Pregunta
        {
            get { return _preg; }
            set { _preg = value; }
        }

        public virtual string Respuesta
        {
            get { return _resp; }
            set { _resp = value; }
        }

        public virtual string Catalogos
        {
            get { return _Catalogos; }
            set { _Catalogos = value; }
        }
        public virtual string DescDispositivo
        {
            get { return _DescDispo; }
            set { _DescDispo = value; }
        }

        public virtual int IdDisposisito {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }
        
        public virtual string DispoTelefono
        {
            get { return _dispo_numTel; }
            set { _dispo_numTel = value; }
        }


        public virtual string DispoMarca
        {
            get { return _dispo_marca; }
            set { _dispo_marca = value; }
        }

        public virtual string DispoModelo
        {
            get { return _dispo_modelo; }
            set { _dispo_modelo = value; }
        }

        public virtual string DispoDesc
        {
            get { return _dispo_desc; }
            set { _dispo_desc = value; }
        }

        public virtual string DispoMeid
        {
            get { return _dispo_meid; }
            set { _dispo_meid = value; }
        }

        public virtual string UsuFeEmp
        {
            get { return _UsuFeEmp; }
            set { _UsuFeEmp = value; }
        }

        public virtual string UsuFeTer
        {
            get { return _UsuFeTer; }
            set { _UsuFeTer = value; }
        }

        #endregion
    }
}
