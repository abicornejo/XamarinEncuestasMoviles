using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_LogRespSelected
    {
        #region Constructor
        public THE_LogRespSelected() { }
        #endregion

        #region Atributos

        private int _no_resp;
        
        private int _id_Resp;

        private int _orden_Resp;

        private int _ref_Id_Encuesta;
        
        private string _desc_Resp;

        private string _evento_Resp;

        private DateTime _fecha;

        private double _numTel;

        #endregion

        #region Propiedades

        public virtual int NoResp
        {
            get { return _no_resp; }
            set { _no_resp = value; }
        }

        public virtual int IdRespSelected
        {
            get { return _id_Resp; }
            set { _id_Resp = value; }
        }

        public virtual int OrdenRespSelected
        {
            get { return _orden_Resp; }
            set { _orden_Resp = value; }
        }

        public virtual int IdEncuestaSelected
        {
            get { return _ref_Id_Encuesta; }
            set { _ref_Id_Encuesta = value; }
        }

        public virtual string DescRespuestaSelected
        {
            get { return _desc_Resp; }
            set { _desc_Resp = value; }
        }

        public virtual string Evento_Resp
        {
            get { return _evento_Resp; }
            set { _evento_Resp = value; }
        }

        public virtual DateTime Fecha_Evento
        {

            get { return _fecha; }
            set { _fecha = value; }

        }

        public virtual double NumTel
        {

            get { return _numTel; }
            set { _numTel = value; }

        }
        
        #endregion

    }



}
