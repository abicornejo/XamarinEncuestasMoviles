using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class IntentosUsuario
    {
        private int _TipoIntento;
        private int _NumIntento;

        public int TipoIntento { get { return _TipoIntento; } set { _TipoIntento = value; } }
        public int NumIntento { get { return _NumIntento; } set { _NumIntento = value; } }


        public IntentosUsuario()
        {

        }
    }

    [Serializable]
    public class IntentosUserXIP
    {
        private int _TipoIntento;
        private string _NoIP;
        private int _NumIntento;

        public int TipoIntento { get { return _TipoIntento; } set { _TipoIntento = value; } }
        public string NoIP { get { return _NoIP; } set { _NoIP = value; } }
        public int NumIntento { get { return _NumIntento; } set { _NumIntento = value; } }

        public IntentosUserXIP()
        {

        }
    }
}
