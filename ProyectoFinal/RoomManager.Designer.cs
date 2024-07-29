namespace ProyectoFinal
{
    partial class RoomManager
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.NcamasText = new System.Windows.Forms.TextBox();
            this.PrecioPersonaText = new System.Windows.Forms.TextBox();
            this.PersonasHabText = new System.Windows.Forms.TextBox();
            this.AmenidadesText = new System.Windows.Forms.TextBox();
            this.UbiHabText = new System.Windows.Forms.TextBox();
            this.NivHabText = new System.Windows.Forms.TextBox();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.idHotelText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.camasText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numeros de camas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Precio por noche por persona";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Personas por habitacion";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(434, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(157, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Amenidades disponibles";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(434, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(152, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Ubicacion de habitacion";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(434, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 16);
            this.label8.TabIndex = 4;
            this.label8.Text = "Nivel de habitacion";
            // 
            // NcamasText
            // 
            this.NcamasText.Location = new System.Drawing.Point(114, 89);
            this.NcamasText.Name = "NcamasText";
            this.NcamasText.Size = new System.Drawing.Size(100, 22);
            this.NcamasText.TabIndex = 7;
            this.NcamasText.TextChanged += new System.EventHandler(this.NcamasText_TextChanged);
            // 
            // PrecioPersonaText
            // 
            this.PrecioPersonaText.Location = new System.Drawing.Point(114, 214);
            this.PrecioPersonaText.Name = "PrecioPersonaText";
            this.PrecioPersonaText.Size = new System.Drawing.Size(100, 22);
            this.PrecioPersonaText.TabIndex = 9;
            // 
            // PersonasHabText
            // 
            this.PersonasHabText.Location = new System.Drawing.Point(114, 283);
            this.PersonasHabText.Name = "PersonasHabText";
            this.PersonasHabText.Size = new System.Drawing.Size(100, 22);
            this.PersonasHabText.TabIndex = 10;
            // 
            // AmenidadesText
            // 
            this.AmenidadesText.Location = new System.Drawing.Point(437, 214);
            this.AmenidadesText.Name = "AmenidadesText";
            this.AmenidadesText.Size = new System.Drawing.Size(100, 22);
            this.AmenidadesText.TabIndex = 13;
            // 
            // UbiHabText
            // 
            this.UbiHabText.Location = new System.Drawing.Point(437, 152);
            this.UbiHabText.Name = "UbiHabText";
            this.UbiHabText.Size = new System.Drawing.Size(100, 22);
            this.UbiHabText.TabIndex = 12;
            // 
            // NivHabText
            // 
            this.NivHabText.Location = new System.Drawing.Point(437, 89);
            this.NivHabText.Name = "NivHabText";
            this.NivHabText.Size = new System.Drawing.Size(100, 22);
            this.NivHabText.TabIndex = 11;
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(437, 363);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(106, 23);
            this.RegisterButton.TabIndex = 14;
            this.RegisterButton.Text = "Registrar habitacion";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // idHotelText
            // 
            this.idHotelText.Location = new System.Drawing.Point(434, 284);
            this.idHotelText.Name = "idHotelText";
            this.idHotelText.Size = new System.Drawing.Size(100, 22);
            this.idHotelText.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(431, 264);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(172, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "ID del hotel de la habitacion";
            // 
            // camasText
            // 
            this.camasText.Location = new System.Drawing.Point(114, 153);
            this.camasText.Name = "camasText";
            this.camasText.Size = new System.Drawing.Size(100, 22);
            this.camasText.TabIndex = 20;
            this.camasText.TextChanged += new System.EventHandler(this.camasText_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 19;
            this.label2.Text = "Camas";
            // 
            // RoomManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.camasText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.idHotelText);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.RegisterButton);
            this.Controls.Add(this.AmenidadesText);
            this.Controls.Add(this.UbiHabText);
            this.Controls.Add(this.NivHabText);
            this.Controls.Add(this.PersonasHabText);
            this.Controls.Add(this.PrecioPersonaText);
            this.Controls.Add(this.NcamasText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "RoomManager";
            this.Text = "RoomManager";
            //this.Load += new System.EventHandler(this.RoomManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox NcamasText;
        private System.Windows.Forms.TextBox PrecioPersonaText;
        private System.Windows.Forms.TextBox PersonasHabText;
        private System.Windows.Forms.TextBox AmenidadesText;
        private System.Windows.Forms.TextBox UbiHabText;
        private System.Windows.Forms.TextBox NivHabText;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.TextBox idHotelText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox camasText;
        private System.Windows.Forms.Label label2;
    }
}