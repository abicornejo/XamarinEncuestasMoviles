using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_Menu
    {
        #region Constructor
        public TDI_Menu() { }
        #endregion

        #region Atributos
        private int _menullavpr;
        private int _menudepe;
        private string _menudesc;
        private string _menuurl;
        private char _menuactivo;
        #endregion

        #region Propiedades
        public virtual int MenuLlavPr
        {
            get { return _menullavpr; }
            set { _menullavpr = value; }
        }
        public virtual int MenuDepe
        {
            get { return _menudepe; }
            set { _menudepe = value; }
        }
        public virtual string MenuDesc
        {
            get { return _menudesc; }
            set { _menudesc = value; }
        }
        public virtual string MenuUrl
        {
            get { return _menuurl; }
            set { _menuurl = value; }
        }
        public virtual char MenuActivo
        {
            get { return _menuactivo; }
            set { _menuactivo = value; }
        }
        #endregion
    }
}
