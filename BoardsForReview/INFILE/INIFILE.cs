using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoardsForReview.INFILE
{
    class INIFILE
    {

        // Declaración de las funciones de Windows para leer desde un archivo .ini
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetPrivateProfileString(string section, string key, string defaultValue,
            StringBuilder returnValue, int size, string filePath);



        // Método para leer un valor del archivo .ini
        public string GetIniValue(string section, string key, string filePath)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, sb.Capacity, filePath);
            return sb.ToString();
        }


    }
}
