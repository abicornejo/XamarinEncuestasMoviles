using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_DispoApVersion
    {

        #region Constructor
        public TDI_DispoApVersion() { }
        #endregion

        #region Atributos

       private int _ID_DISAPVER;				
       private string _NUM_TEL;
       private int _NUMBER;				
       private string  _VER_NAME;
       private DateTime _VER_DATE;				

        #endregion

        #region Propiedades

       public virtual int ID_DISAPVER
       {
           get { return _ID_DISAPVER; }
           set { _ID_DISAPVER = value; }
       }
       public virtual string NUM_TEL
       {
           get { return _NUM_TEL; }
           set { _NUM_TEL = value; }
       }
       public virtual int NUMBER
       {
           get { return _NUMBER; }
           set { _NUMBER = value; }
       }
       public virtual string VER_NAME
       {
           get { return _VER_NAME; }
           set { _VER_NAME = value; }
       }
       public virtual DateTime VER_DATE
       {
           get { return _VER_DATE; }
           set { _VER_DATE = value; }
       }

        #endregion

    }
}
