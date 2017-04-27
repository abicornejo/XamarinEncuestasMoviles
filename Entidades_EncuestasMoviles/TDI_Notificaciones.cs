using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Notificaciones
    {
        #region Constructor

        public TDI_Notificaciones() { }

        #endregion


        #region Atributos

        private int _id_notifica;

        private string  _token;

        private string _mensaje;

        private int _periodo;

        private int _id_enc;

        private int _status;

        private int _contador;

        private string _telefono;

        private int _idDispo;

        private int _id_envio;

        #endregion

        #region Propiedades

        public virtual int IdNotificacion
        {
            get { return _id_notifica; }
            set { _id_notifica = value; }
        }
        public virtual string TokenDispositivo
        {
            get { return _token; }
            set { _token = value; }
        }
        public virtual string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }
        public virtual int Periodo
        {
            get { return _periodo; }
            set { _periodo = value; }
        }
        public virtual int IdEncuesta
        {
            get { return _id_enc; }
            set { _id_enc= value; }
        }
        public virtual int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public virtual string Telefono { 
        
             get { return _telefono; }
             set { _telefono= value; }
        }

        public virtual int IdDispo
        {
            get { return _idDispo; }
            set { _idDispo = value; }
        }

        public virtual int IdEnvio
        {
            get { return _id_envio; }
            set { _id_envio = value; }
        }
        //public virtual int Contador
        //{
        //    get { return _contador; }
        //    set { _contador = value; }
        //}
      
        #endregion


    }
}
