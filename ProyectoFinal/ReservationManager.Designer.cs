namespace ProyectoFinal
{
    partial class ReservationManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataGridClientes = new System.Windows.Forms.DataGridView();
            this.DataGridCiudades = new System.Windows.Forms.DataGridView();
            this.dataGridHoteles = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.ReservarButton = new System.Windows.Forms.Button();
            this.nPersonasText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.apeBuscarText = new System.Windows.Forms.TextBox();
            this.CiudadElegidaText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.RFCbuscarText = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.emailBuscarText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AnticipoDadoText = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.DataGridHabitaciones = new System.Windows.Forms.DataGridView();
            this.IDhabText = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.AgregarHabButton = new System.Windows.Forms.Button();
            this.inicioPicker = new System.Windows.Forms.DateTimePicker();
            this.finPicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MedioPagoText = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridCiudades)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHoteles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridHabitaciones)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridClientes
            // 
            this.DataGridClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridClientes.Location = new System.Drawing.Point(38, 142);
            this.DataGridClientes.Name = "DataGridClientes";
            this.DataGridClientes.RowHeadersWidth = 51;
            this.DataGridClientes.RowTemplate.Height = 24;
            this.DataGridClientes.Size = new System.Drawing.Size(355, 280);
            this.DataGridClientes.TabIndex = 0;
            this.DataGridClientes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridClientes_CellContentDoubleClick);
            // 
            // DataGridCiudades
            // 
            this.DataGridCiudades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridCiudades.Location = new System.Drawing.Point(412, 142);
            this.DataGridCiudades.Name = "DataGridCiudades";
            this.DataGridCiudades.RowHeadersWidth = 51;
            this.DataGridCiudades.RowTemplate.Height = 24;
            this.DataGridCiudades.Size = new System.Drawing.Size(355, 280);
            this.DataGridCiudades.TabIndex = 1;
            this.DataGridCiudades.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridCiudades_CellContentDoubleClick);
            // 
            // dataGridHoteles
            // 
            this.dataGridHoteles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridHoteles.Location = new System.Drawing.Point(38, 504);
            this.dataGridHoteles.Name = "dataGridHoteles";
            this.dataGridHoteles.RowHeadersWidth = 51;
            this.dataGridHoteles.RowTemplate.Height = 24;
            this.dataGridHoteles.Size = new System.Drawing.Size(355, 280);
            this.dataGridHoteles.TabIndex = 2;
            this.dataGridHoteles.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHoteles_CellContentDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(847, 628);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Numero de personas por habitacion";
            // 
            // ReservarButton
            // 
            this.ReservarButton.Location = new System.Drawing.Point(1257, 653);
            this.ReservarButton.Name = "ReservarButton";
            this.ReservarButton.Size = new System.Drawing.Size(75, 23);
            this.ReservarButton.TabIndex = 7;
            this.ReservarButton.Text = "Reservar";
            this.ReservarButton.UseVisualStyleBackColor = true;
            this.ReservarButton.Click += new System.EventHandler(this.ReservarButton_Click);
            // 
            // nPersonasText
            // 
            this.nPersonasText.Location = new System.Drawing.Point(850, 647);
            this.nPersonasText.Name = "nPersonasText";
            this.nPersonasText.Size = new System.Drawing.Size(100, 22);
            this.nPersonasText.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Cliente a buscar";
            // 
            // apeBuscarText
            // 
            this.apeBuscarText.Location = new System.Drawing.Point(38, 96);
            this.apeBuscarText.Name = "apeBuscarText";
            this.apeBuscarText.Size = new System.Drawing.Size(100, 22);
            this.apeBuscarText.TabIndex = 13;
            this.apeBuscarText.TextChanged += new System.EventHandler(this.apeBuscarText_TextChanged);
            // 
            // CiudadElegidaText
            // 
            this.CiudadElegidaText.Location = new System.Drawing.Point(38, 476);
            this.CiudadElegidaText.Name = "CiudadElegidaText";
            this.CiudadElegidaText.Size = new System.Drawing.Size(100, 22);
            this.CiudadElegidaText.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Ciudad elegida";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "Apellidos";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(151, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 16);
            this.label9.TabIndex = 20;
            this.label9.Text = "RFC";
            // 
            // RFCbuscarText
            // 
            this.RFCbuscarText.Location = new System.Drawing.Point(154, 96);
            this.RFCbuscarText.Name = "RFCbuscarText";
            this.RFCbuscarText.Size = new System.Drawing.Size(100, 22);
            this.RFCbuscarText.TabIndex = 19;
            this.RFCbuscarText.TextChanged += new System.EventHandler(this.RFCbuscarText_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(271, 77);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 16);
            this.label10.TabIndex = 22;
            this.label10.Text = "Correo electronico";
            // 
            // emailBuscarText
            // 
            this.emailBuscarText.Location = new System.Drawing.Point(274, 96);
            this.emailBuscarText.Name = "emailBuscarText";
            this.emailBuscarText.Size = new System.Drawing.Size(100, 22);
            this.emailBuscarText.TabIndex = 21;
            this.emailBuscarText.TextChanged += new System.EventHandler(this.emailBuscarText_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(416, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 16);
            this.label6.TabIndex = 29;
            this.label6.Text = "Cliente elegido";
            // 
            // AnticipoDadoText
            // 
            this.AnticipoDadoText.Location = new System.Drawing.Point(1257, 610);
            this.AnticipoDadoText.Name = "AnticipoDadoText";
            this.AnticipoDadoText.Size = new System.Drawing.Size(100, 22);
            this.AnticipoDadoText.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1254, 591);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(151, 16);
            this.label14.TabIndex = 30;
            this.label14.Text = "Anticipo que se va a dar";
            // 
            // DataGridHabitaciones
            // 
            this.DataGridHabitaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridHabitaciones.Location = new System.Drawing.Point(419, 504);
            this.DataGridHabitaciones.Name = "DataGridHabitaciones";
            this.DataGridHabitaciones.RowHeadersWidth = 51;
            this.DataGridHabitaciones.RowTemplate.Height = 24;
            this.DataGridHabitaciones.Size = new System.Drawing.Size(355, 280);
            this.DataGridHabitaciones.TabIndex = 32;
            this.DataGridHabitaciones.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridHabitaciones_CellContentDoubleClick);
            // 
            // IDhabText
            // 
            this.IDhabText.Location = new System.Drawing.Point(850, 574);
            this.IDhabText.Name = "IDhabText";
            this.IDhabText.Size = new System.Drawing.Size(100, 22);
            this.IDhabText.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(847, 555);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(118, 16);
            this.label15.TabIndex = 33;
            this.label15.Text = "ID de la habitacion";
            // 
            // AgregarHabButton
            // 
            this.AgregarHabButton.Location = new System.Drawing.Point(850, 692);
            this.AgregarHabButton.Name = "AgregarHabButton";
            this.AgregarHabButton.Size = new System.Drawing.Size(75, 23);
            this.AgregarHabButton.TabIndex = 35;
            this.AgregarHabButton.Text = "Agregar";
            this.AgregarHabButton.UseVisualStyleBackColor = true;
            this.AgregarHabButton.Click += new System.EventHandler(this.AgregarHabButton_Click);
            // 
            // inicioPicker
            // 
            this.inicioPicker.Location = new System.Drawing.Point(1234, 415);
            this.inicioPicker.Name = "inicioPicker";
            this.inicioPicker.Size = new System.Drawing.Size(200, 22);
            this.inicioPicker.TabIndex = 36;
            // 
            // finPicker
            // 
            this.finPicker.Location = new System.Drawing.Point(1234, 476);
            this.finPicker.Name = "finPicker";
            this.finPicker.Size = new System.Drawing.Size(200, 22);
            this.finPicker.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1231, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 38;
            this.label1.Text = "Fecha inicio";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1231, 457);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 39;
            this.label2.Text = "Fecha fin";
            // 
            // MedioPagoText
            // 
            this.MedioPagoText.Location = new System.Drawing.Point(1257, 567);
            this.MedioPagoText.Name = "MedioPagoText";
            this.MedioPagoText.Size = new System.Drawing.Size(100, 22);
            this.MedioPagoText.TabIndex = 41;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1254, 548);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(99, 16);
            this.label16.TabIndex = 40;
            this.label16.Text = "Medio de pago";
            // 
            // ReservationManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1499, 922);
            this.Controls.Add(this.MedioPagoText);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.finPicker);
            this.Controls.Add(this.inicioPicker);
            this.Controls.Add(this.AgregarHabButton);
            this.Controls.Add(this.IDhabText);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.DataGridHabitaciones);
            this.Controls.Add(this.AnticipoDadoText);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.emailBuscarText);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.RFCbuscarText);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CiudadElegidaText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.apeBuscarText);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nPersonasText);
            this.Controls.Add(this.ReservarButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dataGridHoteles);
            this.Controls.Add(this.DataGridCiudades);
            this.Controls.Add(this.DataGridClientes);
            this.Name = "ReservationManager";
            this.Text = "ReservationManager";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridCiudades)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHoteles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridHabitaciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ReservarButton;
        private System.Windows.Forms.TextBox nPersonasText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox apeBuscarText;
        private System.Windows.Forms.TextBox CiudadElegidaText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox RFCbuscarText;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox emailBuscarText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox AnticipoDadoText;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.DataGridView DataGridClientes;
        public System.Windows.Forms.DataGridView DataGridCiudades;
        public System.Windows.Forms.DataGridView dataGridHoteles;
        public System.Windows.Forms.DataGridView DataGridHabitaciones;
        private System.Windows.Forms.TextBox IDhabText;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button AgregarHabButton;
        private System.Windows.Forms.DateTimePicker inicioPicker;
        private System.Windows.Forms.DateTimePicker finPicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MedioPagoText;
        private System.Windows.Forms.Label label16;
    }
}