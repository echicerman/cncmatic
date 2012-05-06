namespace CNCMatic
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.importaDXF = new System.Windows.Forms.OpenFileDialog();
            this.grpImportacion = new System.Windows.Forms.GroupBox();
            this.rdbG = new System.Windows.Forms.RadioButton();
            this.rdbDXF = new System.Windows.Forms.RadioButton();
            this.pnlImportG = new System.Windows.Forms.Panel();
            this.btnImportarG = new System.Windows.Forms.Button();
            this.btnBuscarG = new System.Windows.Forms.Button();
            this.txtFilePathG = new System.Windows.Forms.TextBox();
            this.pnlImportDXF = new System.Windows.Forms.Panel();
            this.btnImportarDXF = new System.Windows.Forms.Button();
            this.btnBuscarDXF = new System.Windows.Forms.Button();
            this.txtFilePathD = new System.Windows.Forms.TextBox();
            this.importaG = new System.Windows.Forms.OpenFileDialog();
            this.txtGpreview = new System.Windows.Forms.TextBox();
            this.grpImportacion.SuspendLayout();
            this.pnlImportG.SuspendLayout();
            this.pnlImportDXF.SuspendLayout();
            this.SuspendLayout();
            // 
            // importaDXF
            // 
            this.importaDXF.DefaultExt = "*.dxf";
            this.importaDXF.FileName = "*.dxf";
            this.importaDXF.Filter = "Archivos DXF (*.dxf)|*.dxf";
            // 
            // grpImportacion
            // 
            this.grpImportacion.BackColor = System.Drawing.Color.Transparent;
            this.grpImportacion.Controls.Add(this.rdbG);
            this.grpImportacion.Controls.Add(this.rdbDXF);
            this.grpImportacion.Controls.Add(this.pnlImportG);
            this.grpImportacion.Controls.Add(this.pnlImportDXF);
            this.grpImportacion.Location = new System.Drawing.Point(12, 26);
            this.grpImportacion.Name = "grpImportacion";
            this.grpImportacion.Size = new System.Drawing.Size(348, 218);
            this.grpImportacion.TabIndex = 3;
            this.grpImportacion.TabStop = false;
            this.grpImportacion.Text = "Importación Archivos";
            // 
            // rdbG
            // 
            this.rdbG.AutoSize = true;
            this.rdbG.Location = new System.Drawing.Point(25, 118);
            this.rdbG.Name = "rdbG";
            this.rdbG.Size = new System.Drawing.Size(33, 17);
            this.rdbG.TabIndex = 9;
            this.rdbG.TabStop = true;
            this.rdbG.Text = "G";
            this.rdbG.UseVisualStyleBackColor = true;
            this.rdbG.CheckedChanged += new System.EventHandler(this.rdbG_CheckedChanged);
            // 
            // rdbDXF
            // 
            this.rdbDXF.AutoSize = true;
            this.rdbDXF.Location = new System.Drawing.Point(25, 19);
            this.rdbDXF.Name = "rdbDXF";
            this.rdbDXF.Size = new System.Drawing.Size(46, 17);
            this.rdbDXF.TabIndex = 8;
            this.rdbDXF.TabStop = true;
            this.rdbDXF.Text = "DXF";
            this.rdbDXF.UseVisualStyleBackColor = true;
            this.rdbDXF.CheckedChanged += new System.EventHandler(this.rdbDXF_CheckedChanged);
            // 
            // pnlImportG
            // 
            this.pnlImportG.Controls.Add(this.btnImportarG);
            this.pnlImportG.Controls.Add(this.btnBuscarG);
            this.pnlImportG.Controls.Add(this.txtFilePathG);
            this.pnlImportG.Enabled = false;
            this.pnlImportG.Location = new System.Drawing.Point(25, 141);
            this.pnlImportG.Name = "pnlImportG";
            this.pnlImportG.Size = new System.Drawing.Size(268, 59);
            this.pnlImportG.TabIndex = 7;
            // 
            // btnImportarG
            // 
            this.btnImportarG.Location = new System.Drawing.Point(54, 29);
            this.btnImportarG.Name = "btnImportarG";
            this.btnImportarG.Size = new System.Drawing.Size(98, 23);
            this.btnImportarG.TabIndex = 8;
            this.btnImportarG.Text = "Importar G";
            this.btnImportarG.UseVisualStyleBackColor = true;
            // 
            // btnBuscarG
            // 
            this.btnBuscarG.Location = new System.Drawing.Point(230, -1);
            this.btnBuscarG.Name = "btnBuscarG";
            this.btnBuscarG.Size = new System.Drawing.Size(29, 23);
            this.btnBuscarG.TabIndex = 7;
            this.btnBuscarG.Text = "...";
            this.btnBuscarG.UseVisualStyleBackColor = true;
            this.btnBuscarG.Click += new System.EventHandler(this.btnBuscarG_Click);
            // 
            // txtFilePathG
            // 
            this.txtFilePathG.Enabled = false;
            this.txtFilePathG.Location = new System.Drawing.Point(3, 3);
            this.txtFilePathG.Name = "txtFilePathG";
            this.txtFilePathG.Size = new System.Drawing.Size(220, 20);
            this.txtFilePathG.TabIndex = 6;
            // 
            // pnlImportDXF
            // 
            this.pnlImportDXF.Controls.Add(this.btnImportarDXF);
            this.pnlImportDXF.Controls.Add(this.btnBuscarDXF);
            this.pnlImportDXF.Controls.Add(this.txtFilePathD);
            this.pnlImportDXF.Enabled = false;
            this.pnlImportDXF.Location = new System.Drawing.Point(25, 42);
            this.pnlImportDXF.Name = "pnlImportDXF";
            this.pnlImportDXF.Size = new System.Drawing.Size(268, 59);
            this.pnlImportDXF.TabIndex = 6;
            // 
            // btnImportarDXF
            // 
            this.btnImportarDXF.Location = new System.Drawing.Point(54, 29);
            this.btnImportarDXF.Name = "btnImportarDXF";
            this.btnImportarDXF.Size = new System.Drawing.Size(98, 23);
            this.btnImportarDXF.TabIndex = 8;
            this.btnImportarDXF.Text = "Importar DXF";
            this.btnImportarDXF.UseVisualStyleBackColor = true;
            this.btnImportarDXF.Click += new System.EventHandler(this.btnImportarDXF_Click);
            // 
            // btnBuscarDXF
            // 
            this.btnBuscarDXF.Location = new System.Drawing.Point(230, -1);
            this.btnBuscarDXF.Name = "btnBuscarDXF";
            this.btnBuscarDXF.Size = new System.Drawing.Size(29, 23);
            this.btnBuscarDXF.TabIndex = 7;
            this.btnBuscarDXF.Text = "...";
            this.btnBuscarDXF.UseVisualStyleBackColor = true;
            this.btnBuscarDXF.Click += new System.EventHandler(this.btnBuscarDXF_Click);
            // 
            // txtFilePathD
            // 
            this.txtFilePathD.Enabled = false;
            this.txtFilePathD.Location = new System.Drawing.Point(3, 3);
            this.txtFilePathD.Name = "txtFilePathD";
            this.txtFilePathD.Size = new System.Drawing.Size(220, 20);
            this.txtFilePathD.TabIndex = 6;
            // 
            // importaG
            // 
            this.importaG.DefaultExt = "*.gcode";
            this.importaG.FileName = "*.gcode";
            this.importaG.Filter = "Codigo G(*.gcode)|*.gcode";
            // 
            // txtGpreview
            // 
            this.txtGpreview.Location = new System.Drawing.Point(468, 38);
            this.txtGpreview.Multiline = true;
            this.txtGpreview.Name = "txtGpreview";
            this.txtGpreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGpreview.Size = new System.Drawing.Size(210, 199);
            this.txtGpreview.TabIndex = 4;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CNCMatic.Properties.Resources.logo_teCNoC;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(704, 256);
            this.Controls.Add(this.grpImportacion);
            this.Controls.Add(this.txtGpreview);
            this.Name = "Principal";
            this.Text = "CNC Matic";
            this.grpImportacion.ResumeLayout(false);
            this.grpImportacion.PerformLayout();
            this.pnlImportG.ResumeLayout(false);
            this.pnlImportG.PerformLayout();
            this.pnlImportDXF.ResumeLayout(false);
            this.pnlImportDXF.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog importaDXF;
        private System.Windows.Forms.GroupBox grpImportacion;
        private System.Windows.Forms.OpenFileDialog importaG;
        private System.Windows.Forms.RadioButton rdbG;
        private System.Windows.Forms.RadioButton rdbDXF;
        private System.Windows.Forms.Panel pnlImportG;
        private System.Windows.Forms.Button btnImportarG;
        private System.Windows.Forms.Button btnBuscarG;
        private System.Windows.Forms.TextBox txtFilePathG;
        private System.Windows.Forms.Panel pnlImportDXF;
        private System.Windows.Forms.Button btnImportarDXF;
        private System.Windows.Forms.Button btnBuscarDXF;
        private System.Windows.Forms.TextBox txtFilePathD;
        private System.Windows.Forms.TextBox txtGpreview;
    }
}

