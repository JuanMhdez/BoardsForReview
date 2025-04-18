using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoardsForReview.Modelo;

namespace BoardsForReview.Controlador
{
    class Ctrl
    {

        ModeloCargarDatos modeloCargarDatos = new ModeloCargarDatos();
        
        public DataTable cargarDatos()
        {
            DataTable dt = new DataTable();

            modeloCargarDatos.leerConfig();
            dt = modeloCargarDatos.DatosXray();

            return dt;

        }

    }
}
