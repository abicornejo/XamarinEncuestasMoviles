using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades_EncuestasMoviles
{
    [Serializable]
    public class THE_PUSTRAN
    {

        #region Contructor

        public THE_PUSTRAN() { 
        
        }

        #endregion

        #region Atributos

        private int _id_tran;
        private string _desc_tran;
        private DateTime fecha;

        #endregion


        #region Propiedades

        public virtual int IdTran
        {
            get { return _id_tran; }
            set { _id_tran = value; }
        }
        public virtual string DescTran
        {
            get { return _desc_tran; }
            set { _desc_tran = value; }
        }
        public virtual DateTime FechaCreaEncuesta
        {
            get { return fecha; }
            set { fecha = value; }
        }


        
        #endregion

       

    }
}
