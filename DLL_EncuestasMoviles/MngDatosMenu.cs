using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;

namespace DLL_EncuestasMoviles
{
    public class MngDatosMenu
    {
        public static IList<TDI_Menu> ObtieneMenuPuesto(string cvePuesto)
        {
            ISession session = NHibernateHelperORACLE.GetSession();
            IList listaTodoMenu = null;
            string QueryMenu = string.Empty;

            QueryMenu += " SELECT DISTINCT menu.menu_llav_pr menullav, menu.menu_desc menudesc, ";
            QueryMenu += " menu.menu_url menuurl, menu.menu_depe menudepe, menu.menu_pos ";
            QueryMenu += " FROM seml_tdi_menu menu, ";
            QueryMenu += " seml_the_empleado empl, ";
            QueryMenu += " seml_tdi_empleadorol rol, ";
            QueryMenu += " seml_tdi_merol merol ";
            QueryMenu += " WHERE rol.empl_llav_pr = empl.empl_llav_pr ";
            QueryMenu += " AND rol.id_rol = " + cvePuesto;
            QueryMenu += " AND menu.menu_llav_pr = merol.menu_llav_pr ";
            QueryMenu += " AND rol.id_rol = merol.id_rol ";
            QueryMenu += " AND menu.menu_activo = 'A' ";
            QueryMenu += " ORDER BY menu.MENU_POS ";

            try
            {
                ISQLQuery query = session.CreateSQLQuery(QueryMenu);

                query.AddScalar("menullav", NHibernateUtil.Int32);//0
                query.AddScalar("menudesc", NHibernateUtil.String);//1
                query.AddScalar("menuurl", NHibernateUtil.String);//2
                query.AddScalar("menudepe", NHibernateUtil.Int32);//3

                listaTodoMenu = query.List();

            }
            catch (Exception ex)
            {
              
                return new List<TDI_Menu>();
            }

            IList<TDI_Menu> resultMenu = new List<TDI_Menu>();

            foreach (Object[] MenEmp in listaTodoMenu)
            {
                TDI_Menu menu = new TDI_Menu();

                menu.MenuLlavPr = (Int32)MenEmp[0];
                menu.MenuDesc = (string)MenEmp[1];
                menu.MenuUrl = (string)MenEmp[2];
                menu.MenuDepe = (Int32)MenEmp[3];

                resultMenu.Add(menu);
            }

            session.Close();
            session.Dispose();
            session = null;

            return resultMenu;
        }
    }
}
