using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_PrograDispositivo
    {

        #region Constructor
        public THE_PrograDispositivo() { }
        #endregion

        #region Atributos

           private THE_Programacion _ID_PROGRAMACION;
           private string _PROGRAMACION_NOMBRE;
           private int _ID_PROGXFECHASEMANA;
           private int _ID_PRO_DISPO;
           private string _ENCUESTA_NOMBRE;
           private THE_Encuesta _ID_ENCUESTA;
           private string _TIPOPROGRAMACION_DESC;
           private TDI_TipoProgramacion _ID_TIPOPROGRAMACION;
           private string _DISPO_DESCRIPCION;
           private THE_Dispositivo _ID_DISPOSITIVO;
           private char _ESTATUS;
           private int _ID_PROGRA, _ID_ENC, _ID_TIP_PROGRA, _ID_DISPO;
           private string _ColorEstatus;

        #endregion
        
        #region Propiedades

           public virtual THE_Programacion ID_PROGRAMACION
           {

               get { return _ID_PROGRAMACION; }
               set { _ID_PROGRAMACION = value; }
           }
           public virtual string PROGRAMACION_NOMBRE
           {

               get { return _PROGRAMACION_NOMBRE; }
               set { _PROGRAMACION_NOMBRE = value; }
           }
           public virtual int ID_PROGXFECHASEMANA
           {

               get { return _ID_PROGXFECHASEMANA; }
               set { _ID_PROGXFECHASEMANA = value; }
           }
           public virtual int ID_PRO_DISPO
           {

               get { return _ID_PRO_DISPO; }
               set { _ID_PRO_DISPO = value; }
           }
           public virtual string ENCUESTA_NOMBRE
           {

               get { return _ENCUESTA_NOMBRE; }
               set { _ENCUESTA_NOMBRE = value; }
           }
           public virtual THE_Encuesta ID_ENCUESTA
           {

               get { return _ID_ENCUESTA; }
               set { _ID_ENCUESTA = value; }
           }
           public virtual string TIPOPROGRAMACION_DESC
           {

               get { return _TIPOPROGRAMACION_DESC; }
               set { _TIPOPROGRAMACION_DESC = value; }
           }
           public virtual TDI_TipoProgramacion ID_TIPOPROGRAMACION
           {

               get { return _ID_TIPOPROGRAMACION; }
               set { _ID_TIPOPROGRAMACION = value; }
           }
           public virtual string DISPO_DESCRIPCION
           {

               get { return _DISPO_DESCRIPCION; }
               set { _DISPO_DESCRIPCION = value; }
           }
           public virtual THE_Dispositivo ID_DISPOSITIVO {

               get { return _ID_DISPOSITIVO; }
               set { _ID_DISPOSITIVO = value; }
           }
           public virtual char ESTATUS
           {
               get { return _ESTATUS; }
               set { _ESTATUS = value; }
           }
            public virtual int ID_PROGRA {
                get { return _ID_PROGRA; }
                set { _ID_PROGRA = value; }
            }
            public virtual int ID_ENC{
                get { return _ID_ENC; }
                set { _ID_ENC = value; }
            }
            public virtual int ID_TIP_PROGRA{
                get { return _ID_TIP_PROGRA; }
                set { _ID_TIP_PROGRA = value; }
            }
            public virtual int ID_DISPO
            {
                get { return _ID_DISPO; }
                set { _ID_DISPO = value; }
            }
            public virtual string ColorEstatus
            {

                get { return _ColorEstatus; }
                set { _ColorEstatus = value; }
            }
        #endregion

    }
}
