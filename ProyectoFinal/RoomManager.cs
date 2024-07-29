using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Middleware;
using System.Net.NetworkInformation;

namespace ProyectoFinal
{
    public partial class RoomManager : Form
    {
        private readonly Communication middleware = new Communication();
        Dictionary<int, string> camas = new Dictionary<int, string>();
        int numeroCamas;

        public RoomManager()
        {
            InitializeComponent();
            camasText.Enabled = false;

            // Validar que solo se puedan ingresar números en los campos que lo requieran
            NcamasText.KeyPress += new KeyPressEventHandler(NumericTextBox_KeyPress);
            PrecioPersonaText.KeyPress += new KeyPressEventHandler(NumericTextBox_KeyPress);
            PersonasHabText.KeyPress += new KeyPressEventHandler(NumericTextBox_KeyPress);
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            int hotelExiste = middleware.HotelExiste(idHotelText.Text);
            if (hotelExiste == 0)
            {
                MessageBox.Show("El ID del hotel no existe.", "Hotel no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            middleware.RegistrarHabitacion(Convert.ToInt32(NcamasText.Text), camas, PrecioPersonaText.Text, Convert.ToInt32(PersonasHabText.Text),
                NivHabText.Text, UbiHabText.Text, AmenidadesText.Text, idHotelText.Text);

            MessageBox.Show("Habitación registrada");

            // Limpiar los campos después de registrar la habitación
            NcamasText.Text = string.Empty;
            camasText.Text = string.Empty;
            camasText.Enabled = false;
            PrecioPersonaText.Text = string.Empty;
            PersonasHabText.Text = string.Empty;
            NivHabText.Text = string.Empty;
            UbiHabText.Text = string.Empty;
            AmenidadesText.Text = string.Empty;
            idHotelText.Text = string.Empty;
            camas.Clear();
            numeroCamas = 0;
        }

        private void camasText_TextChanged(object sender, EventArgs e)
        {
            string texto = camasText.Text;
            int cantidadCamas = 0;
            string aux = "";
            string[] palabras = new string[numeroCamas];
            int contador = 0;

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i] == ',')
                {
                    palabras[contador] = aux;
                    cantidadCamas++;
                    aux = "";
                    contador++;
                }
                else
                {
                    aux += texto[i];
                }
            }

            if (cantidadCamas == numeroCamas)
            {
                camasText.Enabled = false;
                for (int i = 0; i < palabras.Length; i++)
                {
                    camas.Add(i, palabras[i]);
                }
            }
        }

        private void NcamasText_TextChanged(object sender, EventArgs e)
        {
            // Habilitar el cuadro de texto de camas
            camasText.Enabled = true;

            // Intentar convertir el texto a un número entero
            try
            {
                numeroCamas = string.IsNullOrEmpty(NcamasText.Text) ? 0 : Convert.ToInt32(NcamasText.Text);
            }
            catch (FormatException)
            {
                // En caso de error al convertir, asignar 0
                numeroCamas = 0;
            }
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (como backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
