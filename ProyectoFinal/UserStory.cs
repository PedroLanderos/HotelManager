using Cassandra;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ProyectoFinal
{
    public partial class UserStory : Form
    {
        Middleware.Communication middle = new Middleware.Communication();

        public UserStory()
        {
            InitializeComponent();
            txtNombreCliente.TextChanged += new EventHandler(this.BuscarClientes);
            txtEmail.TextChanged += new EventHandler(this.BuscarClientes);
            txtRFC.TextChanged += new EventHandler(this.BuscarClientes);

            datagridClientes.CellDoubleClick += new DataGridViewCellEventHandler(this.datagridClientes_CellDoubleClick);
        }

        private void BuscarClientes(object sender, EventArgs e)
        {
            string nombre = txtNombreCliente.Text;
            string email = txtEmail.Text;
            string rfc = txtRFC.Text;

            if (!string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(rfc))
            {
                RowSet clientes = middle.BuscarClientes(nombre, email, rfc);
                ConvertInDataGrid(datagridClientes, clientes);
            }
            else
            {
                datagridClientes.DataSource = null; // Clear the grid if no search criteria
            }
        }

        private void ConvertInDataGrid(DataGridView datagrid, RowSet resultSet)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Nombre");
            dataTable.Columns.Add("Email");
            dataTable.Columns.Add("RFC");

            foreach (var row in resultSet)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = row.GetValue<Guid>("id_cliente").ToString();
                dataRow["Nombre"] = row.GetValue<string>("nombre");
                dataRow["Email"] = row.GetValue<string>("email");
                dataRow["RFC"] = row.GetValue<string>("rfc");
                dataTable.Rows.Add(dataRow);
            }

            datagrid.DataSource = dataTable;
        }

        private void datagridClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = datagridClientes.Rows[e.RowIndex];
                Guid idCliente = Guid.Parse(row.Cells["ID"].Value.ToString());
                MostrarReservaciones(idCliente);
            }
        }

        private void MostrarReservaciones(Guid idCliente)
        {
            RowSet reservaciones = middle.ObtenerReservaciones();
            ConvertInDataGridReservaciones(dataGridView2, reservaciones, idCliente);
        }

        private void ConvertInDataGridReservaciones(DataGridView datagrid, RowSet resultSet, Guid idCliente)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Código de Reservación");
            dataTable.Columns.Add("Habitaciones");
            dataTable.Columns.Add("Fecha de Inicio");
            dataTable.Columns.Add("Fecha de Fin");
            dataTable.Columns.Add("Cantidad de Personas");
            dataTable.Columns.Add("Anticipo");
            dataTable.Columns.Add("Restante a Pagar");
            dataTable.Columns.Add("Medio de Pago");
            dataTable.Columns.Add("Usuario de Registro");
            dataTable.Columns.Add("Fecha de Registro");
            dataTable.Columns.Add("Hora de Registro");

            foreach (var row in resultSet)
            {
                if (row.GetValue<Guid>("id_cliente") == idCliente)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Código de Reservación"] = row.GetValue<Guid>("codigo_reservacion").ToString();
                    dataRow["Habitaciones"] = string.Join(", ", row.GetValue<List<Guid>>("habitaciones"));
                    dataRow["Fecha de Inicio"] = row.GetValue<LocalDate>("fecha_inicio").ToString();
                    dataRow["Fecha de Fin"] = row.GetValue<LocalDate>("fecha_fin").ToString();
                    dataRow["Cantidad de Personas"] = row.GetValue<int>("cantidad_personas_hospedadas");
                    dataRow["Anticipo"] = row.GetValue<string>("anticipo");
                    dataRow["Restante a Pagar"] = row.GetValue<string>("restante_pagar");
                    dataRow["Medio de Pago"] = row.GetValue<string>("medio_pago");
                    dataRow["Usuario de Registro"] = row.GetValue<Guid>("id_usuario_registro").ToString();
                    dataRow["Fecha de Registro"] = row.GetValue<LocalDate>("fecha_registro").ToString();
                    dataRow["Hora de Registro"] = row.GetValue<LocalTime>("hora_registro").ToString();
                    dataTable.Rows.Add(dataRow);
                }
            }

            datagrid.DataSource = dataTable;
        }
    }
}
