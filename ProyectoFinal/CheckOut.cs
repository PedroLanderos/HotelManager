using Cassandra;
using Middleware;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class CheckOut : Form
    {
        private readonly Communication middleware = new Communication();
        public CheckOut()
        {
            InitializeComponent();
        }

        private void buscarButton_Click(object sender, EventArgs e)
        {
            try
            {
                RowSet rows = middleware.ObtenerReservacion(Guid.Parse(idReservacionText.Text));
                foreach (var row in rows)
                {
                    DatosCheckOut datos = new DatosCheckOut(
                        row.GetValue<Guid>("codigo_reservacion"),
                        row.GetValue<List<Guid>>("habitaciones"),
                        row.GetValue<LocalDate>("fecha_inicio"),
                        row.GetValue<LocalDate>("fecha_fin"),
                        row.GetValue<Guid>("id_cliente"),
                        row.GetValue<int>("cantidad_personas_hospedadas"),
                        row.GetValue<string>("anticipo"),
                        row.GetValue<string>("restante_pagar"),
                        row.GetValue<string>("medio_pago"),
                        row.GetValue<Guid>("id_usuario_registro"),
                        row.GetValue<LocalTime>("hora_registro"),
                        row.GetValue<LocalDate>("fecha_registro")
                    );

                    GuardarDatosEnArchivo(datos);

                    MessageBox.Show("Si desea volver a agendar acceda al menu principal!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar la reservación: " + ex.Message);
            }
        }

        private void GuardarDatosEnArchivo(DatosCheckOut datos)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Guardar datos de check-out";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                {
                    sw.WriteLine("Código de Reservación: " + datos.CodigoReservacion);
                    sw.WriteLine("Habitaciones: " + string.Join(", ", datos.Habitaciones));
                    sw.WriteLine("Fecha de Inicio: " + datos.FechaInicio);
                    sw.WriteLine("Fecha de Fin: " + datos.FechaFin);
                    sw.WriteLine("ID Cliente: " + datos.IdCliente);
                    sw.WriteLine("Cantidad de Personas Hospedadas: " + datos.CantidadPersonasHospedadas);
                    sw.WriteLine("Anticipo: " + datos.Anticipo);
                    sw.WriteLine("Restante a Pagar: " + datos.RestantePagar);
                    sw.WriteLine("Medio de Pago: " + datos.MedioPago);
                    sw.WriteLine("ID Usuario de Registro: " + datos.IdUsuarioRegistro);
                    sw.WriteLine("Hora de Registro: " + datos.HoraRegistro);
                    sw.WriteLine("Fecha de Registro: " + datos.FechaRegistro);
                }

                MessageBox.Show("Datos guardados correctamente en: " + saveFileDialog.FileName);
                this.Close(); // Cerrar la ventana actual
            }
        }

        public class DatosCheckOut
        {
            public Guid CodigoReservacion { get; set; }
            public List<Guid> Habitaciones { get; set; }
            public LocalDate FechaInicio { get; set; }
            public LocalDate FechaFin { get; set; }
            public Guid IdCliente { get; set; }
            public int CantidadPersonasHospedadas { get; set; }
            public string Anticipo { get; set; }
            public string RestantePagar { get; set; }
            public string MedioPago { get; set; }
            public Guid IdUsuarioRegistro { get; set; }
            public LocalTime HoraRegistro { get; set; }
            public LocalDate FechaRegistro { get; set; }

            public DatosCheckOut(Guid codigoReservacion, List<Guid> habitaciones, LocalDate fechaInicio, LocalDate fechaFin, Guid idCliente, int cantidadPersonasHospedadas,
                                 string anticipo, string restantePagar, string medioPago, Guid idUsuarioRegistro, LocalTime horaRegistro, LocalDate fechaRegistro)
            {
                CodigoReservacion = codigoReservacion;
                Habitaciones = habitaciones;
                FechaInicio = fechaInicio;
                FechaFin = fechaFin;
                IdCliente = idCliente;
                CantidadPersonasHospedadas = cantidadPersonasHospedadas;
                Anticipo = anticipo;
                RestantePagar = restantePagar;
                MedioPago = medioPago;
                IdUsuarioRegistro = idUsuarioRegistro;
                HoraRegistro = horaRegistro;
                FechaRegistro = fechaRegistro;
            }
        }
    }
}
