using Cassandra;
using Middleware;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ProyectoFinal
{
    public partial class ReservationManager : Form
    {
        private readonly Communication middleware = new Communication();
        public List<Guid> habitaciones = new List<Guid>();
        int totalPersonas = 0;
        Guid IDCLIENTE;
        int precioTotal = 0;

        public ReservationManager()
        {
            InitializeComponent();
            ConvertInDataGrid(DataGridClientes, middleware.MostrarClientes("", "", ""));
            DataGridClientes.CellContentDoubleClick += DataGridClientes_CellContentDoubleClick;
            DataGridCiudades.CellContentDoubleClick += DataGridCiudades_CellContentDoubleClick;
            dataGridHoteles.CellContentDoubleClick += dataGridHoteles_CellContentDoubleClick;
            DataGridHabitaciones.CellContentDoubleClick += DataGridHabitaciones_CellContentDoubleClick;
        }

        public void ConvertInDataGrid(DataGridView datagrid, RowSet resulSet)
        {
            // Convertir el resultado en un DataTable
            DataTable dataTable = new DataTable();
            foreach (var column in resulSet.Columns)
            {
                dataTable.Columns.Add(column.Name);
            }

            foreach (var row in resulSet)
            {
                DataRow dataRow = dataTable.NewRow();
                foreach (var column in resulSet.Columns)
                {
                    dataRow[column.Name] = row[column.Name];
                }
                dataTable.Rows.Add(dataRow);
            }

            // Enlazar el DataTable al DataGridView
            datagrid.DataSource = dataTable;
        }

        private void apeBuscarText_TextChanged(object sender, EventArgs e)
        {
            RowSet resultSet = middleware.MostrarClientes(apeBuscarText.Text, "", "");
            ConvertInDataGrid(DataGridClientes, resultSet);
        }

        private void RFCbuscarText_TextChanged(object sender, EventArgs e)
        {
            RowSet resultSet = middleware.MostrarClientes("", RFCbuscarText.Text, "");
            ConvertInDataGrid(DataGridClientes, resultSet);
        }

        private void emailBuscarText_TextChanged(object sender, EventArgs e)
        {
            RowSet resultSet = middleware.MostrarClientes("", "", emailBuscarText.Text);
            ConvertInDataGrid(DataGridClientes, resultSet);
        }

        private void DataGridClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string idCliente = DataGridClientes.Rows[e.RowIndex].Cells["id_cliente"].Value.ToString();

                // Asignar el valor a IDCLIENTE
                IDCLIENTE = Guid.Parse(idCliente);
                MessageBox.Show(IDCLIENTE.ToString());
                // Obtener el valor de la celda seleccionada
                string valorSeleccionado = DataGridClientes.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // Determinar en qué columna se hizo doble clic
                string nombreColumna = DataGridClientes.Columns[e.ColumnIndex].Name;
                ConvertInDataGrid(DataGridCiudades, middleware.MostrarCiudades());

                //DataGridClientes.DataSource = null;
            }
        }

        private void DataGridCiudades_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string valorSeleccionado = DataGridCiudades.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            ConvertInDataGrid(dataGridHoteles, middleware.MostrarHotelEnCiudad(valorSeleccionado));
            //DataGridCiudades.DataSource = null;
        }

        private void dataGridHoteles_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string valorSeleccionado = dataGridHoteles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            MessageBox.Show(valorSeleccionado);
            ConvertInDataGrid(DataGridHabitaciones, middleware.MostrarHabitacionesEnHotel(Guid.Parse(valorSeleccionado)));
            //dataGridHoteles.DataSource = null;
        }

        private void DataGridHabitaciones_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string valorSeleccionado = DataGridHabitaciones.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if (!habitaciones.Contains(Guid.Parse(valorSeleccionado)))
            {
                IDhabText.Text = valorSeleccionado;
                string precioNocheStr = DataGridHabitaciones.Rows[e.RowIndex].Cells["precio_noche"].Value.ToString();
                int precioNoche = int.Parse(precioNocheStr);

                // Sumar el valor de precio_noche a precioTotal
                precioTotal += precioNoche;
                MessageBox.Show(precioTotal.ToString());
            }
            else
                MessageBox.Show("La habitación ya ha sido agregada");
        }

        private void AgregarHabButton_Click(object sender, EventArgs e)
        {
            int nPersonas = Convert.ToInt32(nPersonasText.Text);
            int nPersonasDisponibles = middleware.ObtenerPersonasPorHabitacion(Guid.Parse(IDhabText.Text));

            if (nPersonas <= nPersonasDisponibles)
            {
                //guardar la habitación elegida para la reservación
                totalPersonas += nPersonas;
                habitaciones.Add(Guid.Parse(IDhabText.Text));

                MessageBox.Show("Habitación agregada");
            }
        }

        private void ReservarButton_Click(object sender, EventArgs e)
        {
            if (finPicker.Value > inicioPicker.Value)
            {
                if (habitaciones.Count > 0)
                {
                    TimeSpan dif = finPicker.Value - inicioPicker.Value;
                    int noches = ((int)dif.TotalDays) + 1;

                    int deudaFinal = noches * precioTotal;
                    string deudaFinalString = deudaFinal.ToString();

                    // Validar que el anticipo es menor que la deuda final
                    if (int.TryParse(AnticipoDadoText.Text, out int anticipo))
                    {
                        if (anticipo < deudaFinal)
                        {
                            middleware.RegistrarReserva(habitaciones, totalPersonas, inicioPicker.Value, finPicker.Value,
                                IDCLIENTE, AnticipoDadoText.Text, MedioPagoText.Text, deudaFinalString);
                            MessageBox.Show("Deuda final: " + deudaFinalString);
                            MessageBox.Show("Reservación hecha");
                            ClearAllFields(); // Limpiar todos los campos después de realizar la reservación
                        }
                        else
                        {
                            MessageBox.Show("El anticipo debe ser menor que la deuda total", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un valor válido para el anticipo.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Elige al menos una habitación");
                }
            }
            else
            {
                MessageBox.Show("Elige fechas válidas");
            }
        }

        private void ClearAllFields()
        {
            // Limpiar DataGridViews
            DataGridClientes.DataSource = null;
            DataGridCiudades.DataSource = null;
            dataGridHoteles.DataSource = null;
            DataGridHabitaciones.DataSource = null;

            // Limpiar TextBoxes
            apeBuscarText.Clear();
            RFCbuscarText.Clear();
            emailBuscarText.Clear();
            IDhabText.Clear();
            nPersonasText.Clear();
            AnticipoDadoText.Clear();
            MedioPagoText.Clear();

            // Restablecer DateTimePickers
            inicioPicker.Value = DateTime.Now;
            finPicker.Value = DateTime.Now;

            // Limpiar otras variables y listas
            habitaciones.Clear();
            totalPersonas = 0;
            precioTotal = 0;
            IDCLIENTE = Guid.Empty;
        }
    }
}
