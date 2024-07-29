using Middleware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class MainScreen : Form
    {
        private readonly Communication middle = new Communication();
        public MainScreen(int userRole, string email)
        {
            InitializeComponent();
            if (userRole == 1) //el usuario no es adminstrador 
            {
                registrarUsuarioButton.Visible = false; 
                cancelarRButton.Visible = false;
                rVentasButton.Visible = false;
                RHotelesButton.Visible = false;
            }
        }
        private void registrarUsuarioButton_Click(object sender, EventArgs e)
        {
            UsersManagment u = new UsersManagment();
            u.Show();
        }
        private void cancelarRButton_Click(object sender, EventArgs e)
        {
            Cancellations c = new Cancellations();
            c.Show();
        }
        private void RHotelesButton_Click(object sender, EventArgs e)
        {
            HotelsReports h = new HotelsReports();
            h.Show();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            RoomManager r = new RoomManager();
            r.Show();   
        }

        private void button11_Click(object sender, EventArgs e)
        {
            HotelsManagment h = new HotelsManagment();
            h.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ClientsManager c = new ClientsManager();
            c.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReservationManager r = new ReservationManager();
            r.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Cancellations c = new Cancellations();
            c.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CheckIn c = new CheckIn();
            c.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckOut c = new CheckOut();
            c.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            HabilitarUsuario h = new HabilitarUsuario();
            h.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            UserStory c = new UserStory();
            c.Show();
        }
    }
}
