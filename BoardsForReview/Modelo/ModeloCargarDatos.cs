using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardsForReview.DB;
using BoardsForReview.INFILE;
using MySqlConnector;


namespace BoardsForReview.Modelo
{
    class ModeloCargarDatos
    {

        // Variables donde se almacenarán los valores leídos del archivo .ini
        string codigoOperacion;
        string maquina1;
        string maquina2;


        public void leerConfig()
        {

            INIFILE infile = new INIFILE();


            // Ruta destino: Documentos\Boards for Review\config.ini
            string carpetaDestino = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Boards for Review"
            );

            string archivoDestino = Path.Combine(carpetaDestino, "config.ini");

            // Ruta origen: desde donde se ejecuta el programa (config incluido en la publicación)
            string archivoOrigen = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");

            // Verificamos si ya existe en Documentos
            if (!File.Exists(archivoDestino))
            {
                Directory.CreateDirectory(carpetaDestino); // Asegurarse de que la carpeta existe
                File.Copy(archivoOrigen, archivoDestino);  // Copiar config.ini
                Console.WriteLine("Archivo config.ini copiado a Documentos.");
            }
            else
            {
                Console.WriteLine("El archivo config.ini ya existe en Documentos.");
            }


            // ruta config del proyecto
            string configFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Boards for Review",
                "config.ini"
            );


            // MessageBox.Show("Ruta del archivo config.ini: " + configFilePath);

            if (!System.IO.File.Exists(configFilePath))
            {
                MessageBox.Show($"El archivo de configuración no se encuentra en la ruta esperada: {configFilePath}");
                return;  // Salir si el archivo no existe
            }

            // Leer valores del archivo .ini y asignarlos a las variables
            codigoOperacion = infile.GetIniValue("Settings", "opcode", configFilePath);
            maquina1 = infile.GetIniValue("Settings", "machine1", configFilePath);
            maquina2 = infile.GetIniValue("Settings", "machine2", configFilePath);

            // nomaquinas = int.Parse(infile.GetIniValue("Settings", "FontSize", configFilePath));

           // MessageBox.Show($"UserName: {codigoOperacion}\nTheme: {maquina1}\nFont Size: {maquina2}");

            datosConfig datos = new datosConfig(codigoOperacion, maquina1, maquina2);



        }

        public DataTable DatosXray()
        {

            datosConfig data = new datosConfig(codigoOperacion,maquina1,maquina2);

            MySqlConnection conexion = Conexion.getConexion();

            try
            {

                conexion.Open();

                string qry = "SELECT \r\n  inventory_master.serial as Serial,\r\n    inventory_master.carrier_id as 'Carrier ID',\r\n wo_transaction.system_id as 'Máquina',\r\n    inventory_master.moddate as 'Hora de prueba'\r\nFROM\r\n    runcard.inventory_master\r\n  LEFT JOIN\r\n wo_defects ON inventory_master.serial = wo_defects.serial\r\n        AND inventory_master.moddate = wo_defects.moddate\r\n        LEFT JOIN\r\n    wo_transaction ON wo_defects.trans_num = wo_transaction.trans_num\r\nWHERE\r\n    inventory_master.opcode = '"+ data._opcode +"'\r\n        AND inventory_master.status = 'on hold'\r\n        AND wo_transaction.system_id IN ('"+ data._maquina1+"', '"+ data._maquina2 +"');";

                MySqlDataAdapter adapter = new MySqlDataAdapter(qry, conexion);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                conexion.Close();

                return dt;

            }

            catch(MySqlException ex)
            {

                MessageBox.Show("Error al conectar a la base de datos" + ex.Message);
                return null;

            }

            catch (Exception ex)
            {

                MessageBox.Show("Error al conectar a la base de datos" + ex.Message);
                return null;
            }
        }
    }
}
