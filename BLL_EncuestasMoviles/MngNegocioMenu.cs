using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using DLL_EncuestasMoviles;

namespace BLL_EncuestasMoviles
{
    public class MngNegocioMenu
    {
        public static List<TDI_Menu> ObtieneMenuPuesto(string cvePuesto)
        {
            return (List<TDI_Menu>)MngDatosMenu.ObtieneMenuPuesto(cvePuesto);
        }
    }
}
