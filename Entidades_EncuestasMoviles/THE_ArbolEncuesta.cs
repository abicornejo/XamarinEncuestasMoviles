using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_ArbolEncuesta
    {
        #region Constructor
        public THE_ArbolEncuesta() 
        {
        }
        #endregion

        #region Atributos
        private int _ID_Pregunta;
        private string _Pregunta_Desc;
        private int _ID_Respuesta;
        private string _Respuesta_Desc;
        private int _ID_PreguntaAnterior;
        #endregion

        #region Propiedades
        public virtual int ID_Pregunta
        {
            get {return _ID_Pregunta; }
            set { _ID_Pregunta = value; }
        }

        public virtual string Pregunta_Desc
        {
            get { return _Pregunta_Desc; }
            set { _Pregunta_Desc = value; }
        }

        public virtual int ID_Respuesta
        {
            get { return _ID_Respuesta; }
            set { _ID_Respuesta = value; }
        }

        public virtual string Respuesta_Desc
        {
            get { return _Respuesta_Desc; }
            set { _Respuesta_Desc = value; }
        }

        public virtual int ID_PreguntaAnterior
        {
            get { return _ID_PreguntaAnterior; }
            set { _ID_PreguntaAnterior = value; }
        }
        #endregion

    }
}
