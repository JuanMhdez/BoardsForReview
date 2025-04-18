using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BoardsForReview.INFILE;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BoardsForReview.Modelo
{
    class datosConfig
    {

        public string _opcode { get; set; }
        public string _maquina1 { get; set; }
        public string _maquina2 { get; set; }


        public datosConfig(string opcode, string maquina1 , string maquina2)
        {
            this._opcode = opcode;
            this._maquina1 = maquina1;
            this._maquina2 = maquina2;

        }

    }
}
