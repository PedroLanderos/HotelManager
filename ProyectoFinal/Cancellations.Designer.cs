namespace ProyectoFinal
{
    partial class Cancellations
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
            this.confirmarButton = new System.Windows.Forms.Button();
            this.CodigoReservacionText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // confirmarButton
            // 
            this.confirmarButton.Location = new System.Drawing.Point(140, 158);
            this.confirmarButton.Name = "confirmarButton";
            this.confirmarButton.Size = new System.Drawing.Size(75, 23);
            this.confirmarButton.TabIndex = 5;
            this.confirmarButton.Text = "Confirmar check in";
            this.confirmarButton.UseVisualStyleBackColor = true;
            this.confirmarButton.Click += new System.EventHandler(this.confirmarButton_Click);
            // 
            // CodigoReservacionText
            // 
            this.CodigoReservacionText.Location = new System.Drawing.Point(61, 106);
            this.CodigoReservacionText.Name = "CodigoReservacionText";
            this.CodigoReservacionText.Size = new System.Drawing.Size(239, 22);
            this.CodigoReservacionText.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ingrese su codigo de reservacion unico";
            // 
            // Cancellations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 319);
            this.Controls.Add(this.confirmarButton);
            this.Controls.Add(this.CodigoReservacionText);
            this.Controls.Add(this.label1);
            this.Name = "Cancellations";
            this.Text = "Cancellations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmarButton;
        private System.Windows.Forms.TextBox CodigoReservacionText;
        private System.Windows.Forms.Label label1;
    }
}