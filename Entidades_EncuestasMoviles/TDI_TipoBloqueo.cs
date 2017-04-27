using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class TDI_TipoBloqueo
    {
        #region Atributos
        private int _CveTipoBloqueo;
        private string _DescTipoBloqueo;
        private string _StatusTipoBloqueo;
        #endregion

        #region Propiedades
        public virtual int CveTipoBloqueo { get { return _CveTipoBloqueo; } set { _CveTipoBloqueo = value; } }
        public virtual string DescTipoBloqueo { get { return _DescTipoBloqueo; } set { _DescTipoBloqueo = value; } }
        public virtual string StatusTipoBloqueo { get { return _StatusTipoBloqueo; } set { _StatusTipoBloqueo = value; } }
        #endregion

        #region Constructor
        public TDI_TipoBloqueo()
        {
        }
        #endregion
    }
}
