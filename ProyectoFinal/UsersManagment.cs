using System;
using System.Windows.Forms;
using Middleware;

namespace ProyectoFinal
{
    public partial class UsersManagment : Form
    {
        Middleware.Communication middle = new Middleware.Communication();

        public UsersManagment()
        {
            InitializeComponent();
        }

        private void LimpiarCampos()
        {
            emailText.Text = "";
            passwordText.Text = "";
            NombreText.Text = "";
            NominaText.Text = "";
            DomicilioText.Text = "";
            TelCasaText.Text = "";
            TelCelText.Text = "";
            checkBox1.Checked = false;
            dateTimePicker.Value = DateTime.Now;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                string formattedDate = DateTime.Now.ToString("yyyy-MM-dd"); // Formatear la fecha actual
                middle.Register(emailText.Text, passwordText.Text, NombreText.Text, NominaText.Text, dateTimePicker.Value.ToString("yyyy-MM-dd"),
                    DomicilioText.Text, TelCasaText.Text, TelCelText.Text, checkBox1.Checked, Convert.ToDateTime(formattedDate));

                MessageBox.Show("Usuario registrado");

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar usuario: " + ex.Message);
            }
        }
    }
}
