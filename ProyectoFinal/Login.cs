using System;
using System.Windows.Forms;
using Middleware;
using System.Collections.Generic; 
namespace ProyectoFinal
{
    public partial class Login : Form
    {
        private readonly Communication middle;
        int contador = 0;

        public Login()
        {
            InitializeComponent();
            middle = new Communication();

            passwordText.UseSystemPasswordChar = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int valor = middle.Login(userText.Text, passwordText.Text);

            if (valor == 1 || valor == 2)
            {
                MainScreen main = new MainScreen(valor, userText.Text);
                main.Show();
                this.Hide();
            }
            else if (valor == -1)
            {
                MessageBox.Show("El usuario está deshabilitado. Contacte al administrador.");
            }
            else
            {
                contador++;
                if (contador == 3)
                {
                    middle.DeshabilitarUsuario(userText.Text);
                    MessageBox.Show("El usuario ha sido deshabilitado.");
                }
                else
                {
                    MessageBox.Show("Fallo al inicio de sesión. Intento " + contador + " de 3.");
                }
            }
        }
    }
}
