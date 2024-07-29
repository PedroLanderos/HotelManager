namespace ProyectoFinal
{
    partial class CheckOut
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
            this.idReservacionText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buscarButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // idReservacionText
            // 
            this.idReservacionText.Location = new System.Drawing.Point(152, 62);
            this.idReservacionText.Name = "idReservacionText";
            this.idReservacionText.Size = new System.Drawing.Size(122, 22);
            this.idReservacionText.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(101, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(282, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "INGRESE EL CODIGO DE LA RESERVACION";
            // 
            // buscarButton
            // 
            this.buscarButton.Location = new System.Drawing.Point(177, 102);
            this.buscarButton.Name = "buscarButton";
            this.buscarButton.Size = new System.Drawing.Size(75, 23);
            this.buscarButton.TabIndex = 7;
            this.buscarButton.Text = "BUSCAR";
            this.buscarButton.UseVisualStyleBackColor = true;
            this.buscarButton.Click += new System.EventHandler(this.buscarButton_Click);
            // 
            // CheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 212);
            this.Controls.Add(this.buscarButton);
            this.Controls.Add(this.idReservacionText);
            this.Controls.Add(this.label3);
            this.Name = "CheckOut";
            this.Text = "CheckOut";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox idReservacionText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buscarButton;
    }
}