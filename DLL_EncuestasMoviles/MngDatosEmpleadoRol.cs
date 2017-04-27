using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entidades_EncuestasMoviles;
using NHibernate;
using System.Collections;
using System.Xml;

namespace DLL_EncuestasMoviles
{
    public class MngDatosEmpleadoRol
    {
        public static XmlDocument GetUserDataByNumEmpleado(string numEmpl, string emplUsua)
        {
            ISession session = NHibernateHelperORACLE.GetSession();
            string QueryEmpl = string.Empty;
            string QueryFabrEmpr = string.Empty;

            QueryEmpl += " SELECT empl.empl_usua username, emplrol.id_rol puesto, ";
            QueryEmpl += " emplrol.empl_llav_pr empleado, (empl.empl_nombre || ' ' ||empl.EMPL_APELLPATERNO|| ' ' ||empl.EMPL_APELLMATERNO ) nombreempleado, ";
            QueryEmpl += " rol.rol_descripcion puestonombre, ";
            QueryEmpl += " empl.empl_mail mail ";
            QueryEmpl += " FROM seml_the_empleado empl, seml_tdi_empleadorol emplrol, seml_tdi_rol rol ";
            QueryEmpl += " WHERE empl.empl_llav_pr = emplrol.empl_llav_pr ";
            QueryEmpl += " AND emplrol.id_rol = rol.id_rol ";
            QueryEmpl += " AND rol.rol_estatus = 'A' ";

            if (numEmpl != string.Empty)
            {
                QueryEmpl += " AND EMPL.EMPL_NUM =  " + numEmpl;
            }

            if (emplUsua != string.Empty)
                QueryEmpl += " AND UPPER(EMPL.EMPL_USUA) = '" + emplUsua.ToUpper() + "'";

            QueryEmpl += " AND empl.empl_estatus = 'A' ";

            ISQLQuery query = session.CreateSQLQuery(QueryEmpl);

            query.AddScalar("username", NHibernateUtil.String);//0
            query.AddScalar("puesto", NHibernateUtil.String);//1
            query.AddScalar("empleado", NHibernateUtil.String);//2
            query.AddScalar("nombreempleado", NHibernateUtil.String);//3
            query.AddScalar("puestonombre", NHibernateUtil.String);//4   
            query.AddScalar("mail", NHibernateUtil.String);//5 

            IList listaEmpl = query.List();

            session.Close();
            session.Dispose();
            session = null;

            XmlDocument xml = new XmlDocument();
            XmlElement nRoot = xml.CreateElement("USER");

            if (listaEmpl.Count > 0)
            {

                XmlElement nUserName = xml.CreateElement("NUMUSUA");
                nUserName.AppendChild(xml.CreateTextNode(((object[])(listaEmpl[0]))[0].ToString()));
                nRoot.AppendChild(nUserName);

                XmlElement nPuesto = xml.CreateElement("PUESTO");
                nPuesto.AppendChild(xml.CreateTextNode(((object[])(listaEmpl[0]))[1].ToString()));
                nRoot.AppendChild(nPuesto);

                XmlElement nNumEmpl = xml.CreateElement("NUMEMPL");
                nNumEmpl.AppendChild(xml.CreateTextNode(((object[])(listaEmpl[0]))[2].ToString()));
                nRoot.AppendChild(nNumEmpl);

                XmlElement nNomEmpl = xml.CreateElement("NOMBEMPL");
                nNomEmpl.AppendChild(xml.CreateTextNode(((object[])(listaEmpl[0]))[3].ToString()));
                nRoot.AppendChild(nNomEmpl);

                XmlElement nPuestoNom = xml.CreateElement("PUESTONOMB");
                nPuestoNom.AppendChild(xml.CreateTextNode(((object[])(listaEmpl[0]))[4].ToString()));
                nRoot.AppendChild(nPuestoNom);

                XmlElement nMail = xml.CreateElement("MAIL");
                nPuestoNom.AppendChild(xml.CreateTextNode(((object[])(listaEmpl[0]))[5].ToString()));
                nRoot.AppendChild(nMail);
            }
            xml.AppendChild(nRoot);
            return xml;
        }

        public static IList<THE_Empleado> GetEmailEmpleados()
        {
            ISession session = NHibernateHelperORACLE.GetSession();
            string QueryEmpl = string.Empty;            

            List<THE_Empleado> lstMailEmpl = new List<THE_Empleado>();

            QueryEmpl += " SELECT empl.empl_mail mail, empl.empl_nombre nombre ";            
            QueryEmpl += " FROM seml_the_empleado empl, seml_tdi_empleadorol emplrol, seml_tdi_rol rol ";
            QueryEmpl += " WHERE empl.empl_llav_pr = emplrol.empl_llav_pr ";
            QueryEmpl += " AND emplrol.id_rol = rol.id_rol ";
            QueryEmpl += " AND rol.rol_estatus = 'A' ";
            QueryEmpl += " AND empl.empl_estatus = 'A' ";

            try
            {
                //return NHibernateHelperORACLE.SingleSessionFind<THE_Empleado>(QueryEmpl);
                ISQLQuery query = session.CreateSQLQuery(QueryEmpl);
                query.AddScalar("mail", NHibernateUtil.String);//0
                query.AddScalar("nombre", NHibernateUtil.String);//0

                IList listaEmpl = query.List();

                foreach (Object[] obj in listaEmpl)
                {
                    THE_Empleado oEmpl = new THE_Empleado();
                    oEmpl.EmpleadoMail =System.Convert.ToString(obj[0]);
                    oEmpl.EmpleadoNombre = System.Convert.ToString(obj[1]);
                    lstMailEmpl.Add(oEmpl);
                }
            }
            catch (Exception ex)
            {
                lstMailEmpl = null;
                return lstMailEmpl;
            }
            finally
            {
                session.Close();
                session.Dispose();
                session = null;
            }
            return lstMailEmpl;
            
        }
    }
}
