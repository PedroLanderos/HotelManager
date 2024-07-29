using System;
using System.Windows.Forms;
using Middleware;

namespace ProyectoFinal
{
    public partial class HabilitarUsuario : Form
    {
        private readonly Communication middle;

        public HabilitarUsuario()
        {
            InitializeComponent();
            middle = new Communication();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string correo = textBox1.Text;
            if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor, ingrese un correo electrónico.");
                return;
            }

            try
            {
                middle.HabilitarUsuario(correo);
                MessageBox.Show("Usuario habilitado exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al habilitar usuario: " + ex.Message);
            }
        }
    }
}
