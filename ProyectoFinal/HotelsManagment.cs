using Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class HotelsManagment : Form
    {
        private readonly Communication middleware = new Communication();
        HashSet<string> servicios = new HashSet<string>();

        public HotelsManagment()
        {
            InitializeComponent();
        }

        private void registrarButton_Click(object sender, EventArgs e)
        {
            // Validar que el número de pisos es un entero válido
            if (!int.TryParse(nPisosText.Text, out int numeroPisos))
            {
                MessageBox.Show("Por favor, ingrese un número entero válido para el número de pisos.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            middleware.RegistrarHotel(hotelText.Text, ciudadText.Text, estadoText.Text, paisText.Text, domicilioText.Text,
                numeroPisos, 0, esTuristico.Checked, servicios.ToList(), caracText.Text, Convert.ToDateTime(dateTimePicker.Text));

            MessageBox.Show("Hotel registrado");

            // Limpiar los campos después de registrar el hotel
            hotelText.Text = string.Empty;
            ciudadText.Text = string.Empty;
            estadoText.Text = string.Empty;
            paisText.Text = string.Empty;
            domicilioText.Text = string.Empty;
            nPisosText.Text = string.Empty;
            esTuristico.Checked = false;
            serviciosText.Text = string.Empty;
            servicios.Clear();
            caracText.Text = string.Empty;
            dateTimePicker.Value = DateTime.Now;  // Restablecer DateTimePicker al valor actual
        }

        private void serviciosText_TextChanged(object sender, EventArgs e)
        {
            string texto = serviciosText.Text;
            string aux = "";
            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == ',')
                {
                    servicios.Add(aux);
                    aux = "";
                }
                else
                {
                    aux += texto[i];
                }
            }
        }

        private void nPisosText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos y teclas de control como la tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
