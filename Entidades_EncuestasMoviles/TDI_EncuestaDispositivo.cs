using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_EncuestaDispositivo
    {
        #region Constructor
        public TDI_EncuestaDispositivo() 
        {
            _idDispositivo = new THE_Dispositivo();
            _idEncuesta = new THE_Encuesta();
           
        }
        #endregion

        #region Atributos
        private int _idEnvio;
        /// <summary>
        /// Llave maestra del Dispositivo.
        /// </summary>
        private THE_Dispositivo _idDispositivo;
        /// <summary>
        /// Llave maestra de la Encuesta.
        /// </summary>
        private THE_Encuesta _idEncuesta;
        private List<THE_Encuesta> _listaEncuesta;
        private List<THE_Preguntas> _listPreg;
        private List<THE_PeriodoEncuesta> _listPeriodo;
        private double _numTel;
        private string _descTel;
        private int _idDispo;
        /// <summary>
        /// Llave maestra del Estatus.
        /// </summary>
        private TDI_Estatus _idEstatus;
        private int _id_envio;
        private string _ColorEstatus;
        #endregion

        #region Propiedades
        public virtual THE_Dispositivo IdDispositivo
        {
            get { return _idDispositivo; }
            set { _idDispositivo = value; }
        }
        public virtual THE_Encuesta IdEncuesta
        {
            get { return _idEncuesta; }
            set { _idEncuesta = value; }
        }
        public virtual List<THE_Encuesta> ListaEncuesta
        {
            get { return _listaEncuesta; }
            set { _listaEncuesta = value; }
        }
        public virtual TDI_Estatus IdEstatus
        {
            get { return _idEstatus; }
            set { _idEstatus = value; }
        }
        public virtual List<THE_Preguntas> ListPreg
        {
            get { return _listPreg; }
            set { _listPreg = value; }
        }

        public virtual List<THE_PeriodoEncuesta> ListPeriodos
        {
            get { return _listPeriodo; }
            set { _listPeriodo = value; }
        }

        public virtual double NumTel {

            get { return _numTel; }
            set { _numTel = value; }
        }

        public virtual int IdDispo
        {

            get { return _idDispo; }
            set { _idDispo = value; }
        }

        public virtual string DescTel
        {
            get { return _descTel; }
            set { _descTel = value; }
        }

        public virtual int IdEnvio {
            get { return _idEnvio; }
            set { _idEnvio = value; }
        }
        public virtual string ColorEstatus
        {
            get { return _ColorEstatus; }
            set { _ColorEstatus = value; }
        }
        #endregion

        #region Metodos
        public override bool Equals(object obj)
        {
            if (this == obj)
            { return true; }

            TDI_EncuestaDispositivo oEncuestaDispo = obj as TDI_EncuestaDispositivo;
            if (oEncuestaDispo == null)
            { return false; }

            if (this._idDispositivo.IdDispositivo != oEncuestaDispo._idDispositivo.IdDispositivo && this._idEncuesta.IdEncuesta != oEncuestaDispo._idEncuesta.IdEncuesta)
            { return false; }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result;
                result = this._idDispositivo.GetHashCode() + this._idEncuesta.GetHashCode();
                return result;
            }
        }

        #endregion
    }
}
