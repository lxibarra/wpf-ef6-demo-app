using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace WpfApplication2
{
    public static class Utilerias
    {
        public static bool esTexto(string captura)
        {
            return Regex.IsMatch(captura.Trim(), @"^[a-zA-Z\s]+$");
        }

        public static bool esNumero(string captura)
        {
            return Regex.IsMatch(captura.Trim(), @"^\d+$");
        }

        public static void LimpiarTextBoxes(params TextBox[] arr)
        {
            foreach (var t in arr)
            {
                t.Text = string.Empty;
            }
        }
    }
}
