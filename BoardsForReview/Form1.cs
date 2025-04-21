using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardsForReview.Controlador;

namespace BoardsForReview
{
    public partial class Form1: Form
    {

        Timer checkMinimizedTimer;

        public Form1()
        {

            // timer para cargar las piezas por probar
            InitializeComponent();
            timer1 = new Timer();
           // timer1.Interval = 10000; //
            timer1.Interval = 5 * 60 * 1000; // 5 minutos
            timer1.Tick += timer1_Tick;
            timer1.Start();


            // quitar los iconos de close y maximizar
            this.ControlBox = true;
            this.MinimizeBox = true;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;


            // Abrir si esta minimizado cada 5 min


            if (cargarDatos() > 0) {

                checkMinimizedTimer = new Timer();
                checkMinimizedTimer.Interval = 5 * 60 * 1000; // 5 minutos en milisegundos
               // checkMinimizedTimer.Interval = 10000; // 10 segundos
                                                              // checkMinimizedTimer.Interval = 10000;
                checkMinimizedTimer.Tick += CheckIfMinimized;
                checkMinimizedTimer.Start();

            }



            // llevar siempre al frente
            this.TopMost = true;    // Poner al frente

            // oculta la ventana
            this.ShowInTaskbar = false;


        }

        // Funcion para abrir si esta minimizado
        private void CheckIfMinimized(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                // Restaurar la ventana
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;    // Poner al frente
                this.Activate(); // Lleva la ventana al frente
            }
        }


        // Quitar el boton de cerrar
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_NOCLOSE = 0x200;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }


        Ctrl ctrl = new Ctrl();

        private void Form1_Load(object sender, EventArgs e)
        {
            
            cargarDatos();


        }

        public int cargarDatos()
        {

            // Limpiar cualquier dato previo en el DataGridView
            // dataGridView1.Rows.Clear();

            DataTable dt = new DataTable();

            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dt;

            dt = ctrl.cargarDatos();

            // Quita los indices
            dataGridView1.RowHeadersVisible = false;

            // Elimina la ultima fila
            dataGridView1.AllowUserToAddRows = false;

            // Deshabilitar que se puede editar las celdas
            dataGridView1.ReadOnly = true;

            // Autoajustar el ancho de las coliumnas
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Fuente de los datos
            dataGridView1.DataSource = dt;


            // Contador de tarjetas
            int totalFilas = dataGridView1.Rows.Count;

            label2.Text = "Unidades por validar:   "  + totalFilas;

            // Hora de actualizacion
            label3.Text = "Última actualización: " + DateTime.Now.ToString("HH:mm:ss");

            // Tamño de la fuente
            // Cambiar fuente de celdas
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10); // Tamaño más pequeño

            // Cambiar el color de fondo del encabezado
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 134, 56);

            // Cambiar el color del texto del encabezado
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // Hacer que se apliquen los estilos personalizados
            dataGridView1.EnableHeadersVisualStyles = false;

            return totalFilas;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            cargarDatos();
        }
    }
}
