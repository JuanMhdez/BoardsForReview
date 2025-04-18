using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Configuration;


namespace BoardsForReview.DB
{
    public class Conexion
    {

        public static MySqlConnection getConexion()
        {

            string Cservidor = ConfigurationManager.AppSettings["SERVER"];
            string Cpuerto = ConfigurationManager.AppSettings["PUERTO"];
            string Cusuario = ConfigurationManager.AppSettings["USUARIO"];
            string Cpassword = ConfigurationManager.AppSettings["PASSWORD"];
            string Cdb = ConfigurationManager.AppSettings["DB"];

            string servidor = Cservidor;
            string puerto = Cpuerto;
            string usuario = Cusuario;
            string password = Cpassword;
            string db = Cdb;

            string cadenaConexion = "server=" + servidor + "; port=" + puerto + "; user id=" + usuario + "; password=" + password + "; database=" + db;
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);
            return conexion;


        }

    }
}