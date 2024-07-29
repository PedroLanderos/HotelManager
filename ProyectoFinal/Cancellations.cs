using Middleware;
using System;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class Cancellations : Form
    {
        private readonly Communication middleware = new Communication();

        public Cancellations()
        {
            InitializeComponent();
        }

        private void confirmarButton_Click(object sender, EventArgs e)
        {
            try
            {
                Guid reservacionId = Guid.Parse(CodigoReservacionText.Text);

                // Primero, marcar las habitaciones como disponibles
                middleware.CancelarReservacion(reservacionId);

                // Luego, guardar la cancelación
                middleware.GuardarCancelacion(reservacionId);

                MessageBox.Show("La cancelación ha sido exitosa.");
                this.Close(); // Cerrar la ventana actual
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor, ingrese un código de reservación válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la cancelación: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
