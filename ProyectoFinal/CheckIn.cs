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
    public partial class CheckIn : Form
    {
        private readonly Communication middle = new Communication();
        public CheckIn()
        {
            InitializeComponent();
        }

        private void confirmarButton_Click(object sender, EventArgs e)
        {
            middle.Checin(Guid.Parse(CodigoReservacionText.Text));
            MessageBox.Show("El check in ha sido exitoso y se han liberado las HABITACIONES!");
        }
    }
}
