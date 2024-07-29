using Middleware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class ClientsManager : Form
    {
        private readonly Communication middleware = new Communication();
        public ClientsManager()
        {
            InitializeComponent();
        }

        private void RegistrarButton_Click(object sender, EventArgs e)
        {
            if (!IsValidEmail(EmailText.Text))
            {
                MessageBox.Show("Por favor, ingrese un correo electrónico válido.", "Correo inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int correoExiste = middleware.VerificarEmail(EmailText.Text);
            if (correoExiste == 1)
            {
                MessageBox.Show("El correo electrónico ya está registrado.", "Correo existente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            middleware.RegistrarCliente(NombreText.Text, DomicilioText.Text, RfcText.Text,
                EmailText.Text, NumeroCasaText.Text, NumeroCelularText.Text, ReferenciaHotelText.Text,
                Convert.ToDateTime(dateTimePicker.Text), EstadoCivilText.Text);

            MessageBox.Show("Cliente registrado");

            // Limpiar los campos después de registrar el cliente
            NombreText.Text = string.Empty;
            DomicilioText.Text = string.Empty;
            RfcText.Text = string.Empty;
            EmailText.Text = string.Empty;
            NumeroCasaText.Text = string.Empty;
            NumeroCelularText.Text = string.Empty;
            ReferenciaHotelText.Text = string.Empty;
            dateTimePicker.Value = DateTime.Now;  // Restablecer DateTimePicker al valor actual
            EstadoCivilText.Text = string.Empty;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
