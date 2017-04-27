using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Mono.Data.Sqlite;

namespace ENCUESTA_MOVIL.Servicio
{
    public class Comun
    {
        public Comun()
        {

        }

        public static SqliteConnection AbreBD()
        {
            bool exists;
            SqliteConnection connection;

            //Buscamos el directorio personal en el dispositivo
            var personalFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //Buscamos el directorio database en la carpeta personal
            var databaseFolderPath = string.Format(@"{0}/database", personalFolderPath.Substring(0, personalFolderPath.LastIndexOf('/')));

            if (!Directory.Exists(databaseFolderPath))
            {
                //Si no existe la creamos
                Directory.CreateDirectory(databaseFolderPath); 
            }
            
            if (!Directory.Exists(databaseFolderPath))
            {
                //Si no existe quiere decir que en el paso anterior no se creo correctamente el dir
                throw new Exception(string.Format("{0} no existe!", databaseFolderPath));
            }

            string dbPath = Path.Combine(databaseFolderPath, "Encuestadb.db3");
            //Verifica si existe el archivo mencionado en la parte de arriba

            exists = System.IO.File.Exists(dbPath);

            if (!exists)
            {
                SqliteConnection.CreateFile(dbPath);//Si no existe crea la base de datos
            }
            
            connection = new SqliteConnection("Data Source=" + dbPath);

            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();

            connection.Open();
            
            return connection;
        }
    }
}