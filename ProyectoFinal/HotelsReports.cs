using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Cassandra;
using Middleware;

namespace ProyectoFinal
{
    public partial class HotelsReports : Form
    {
        Middleware.Communication middle = new Middleware.Communication();

        public HotelsReports()
        {
            InitializeComponent();
            txtpais.TextChanged += new EventHandler(this.BuscarHoteles);
            txtciudad.TextChanged += new EventHandler(this.BuscarHoteles);
            dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(this.DataGridView1_CellDoubleClick);
        }

        private void BuscarHoteles(object sender, EventArgs e)
        {
            string ciudad = txtciudad.Text;
            string pais = txtpais.Text;
            int anho = 0;

            if (!string.IsNullOrEmpty(ciudad) || !string.IsNullOrEmpty(pais) || anho > 0)
            {
                RowSet hoteles = middle.BuscarHoteles(ciudad, pais, anho);
                ConvertInDataGridHoteles(dataGridView1, hoteles);
            }
            else
            {
                dataGridView1.DataSource = null; // Clear the grid if no search criteria
            }
        }

        private void ConvertInDataGridHoteles(DataGridView datagrid, RowSet resultSet)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Pais");
            dataTable.Columns.Add("Ciudad");
            dataTable.Columns.Add("Anho de Inicio");

            foreach (var row in resultSet)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = row.GetValue<Guid>("id").ToString();
                dataRow["Pais"] = row.GetValue<string>("pais");
                dataRow["Ciudad"] = row.GetValue<string>("ciudad");
                dataRow["Anho de Inicio"] = row.GetValue<LocalDate>("fecha_inicio_operaciones").Year.ToString();
                dataTable.Rows.Add(dataRow);
            }

            datagrid.DataSource = dataTable;
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                Guid hotelId = Guid.Parse(row.Cells["ID"].Value.ToString());

                // Obtener habitaciones del hotel seleccionado
                RowSet habitaciones = middle.ObtenerHabitacionesPorHotel(hotelId);

                // Obtener reservaciones de las habitaciones obtenidas
                List<Guid> habitacionesIds = new List<Guid>();
                foreach (var habRow in habitaciones)
                {
                    habitacionesIds.Add(habRow.GetValue<Guid>("id_habitacion"));
                }
                RowSet reservaciones = middle.ObtenerReservacionesPorHabitaciones(habitacionesIds);

                // Calcular ingresos totales y llenar el segundo DataGridView
                LlenarDataGridView2(hotelId, reservaciones);
            }
        }

        private void LlenarDataGridView2(Guid hotelId, RowSet reservaciones)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Nombre del Hotel");
            dataTable.Columns.Add("Ciudad");
            dataTable.Columns.Add("Ingresos Totales");

            var hotelInfo = middle.ObtenerInfoHotel(hotelId).FirstOrDefault();
            if (hotelInfo != null)
            {
                string nombreHotel = hotelInfo.GetValue<string>("nombre_hotel");
                string ciudad = hotelInfo.GetValue<string>("ciudad");

                decimal ingresosTotales = 0;
                foreach (var row in reservaciones)
                {
                    decimal anticipo = decimal.Parse(row.GetValue<string>("anticipo"));
                    decimal restantePagar = decimal.Parse(row.GetValue<string>("restante_pagar"));
                    ingresosTotales += anticipo + restantePagar;
                }

                DataRow dataRow = dataTable.NewRow();
                dataRow["Nombre del Hotel"] = nombreHotel;
                dataRow["Ciudad"] = ciudad;
                dataRow["Ingresos Totales"] = ingresosTotales.ToString();
                dataTable.Rows.Add(dataRow);

                dataGridView2.DataSource = dataTable;
            }
        }
    }
}
