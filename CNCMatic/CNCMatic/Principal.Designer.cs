﻿namespace CNCMatic
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
            this.importaG = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
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
            this.txtGpreview = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbStop = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.gbInicio = new System.Windows.Forms.GroupBox();
            this.btnInicio = new System.Windows.Forms.Button();
            this.gbPosicionActual = new System.Windows.Forms.GroupBox();
            this.lblPosZ = new System.Windows.Forms.Label();
            this.txtPosZ = new System.Windows.Forms.TextBox();
            this.lblPosY = new System.Windows.Forms.Label();
            this.txtPosY = new System.Windows.Forms.TextBox();
            this.lblPosX = new System.Windows.Forms.Label();
            this.txtPosX = new System.Windows.Forms.TextBox();
            this.gbMovZ = new System.Windows.Forms.GroupBox();
            this.btnMovZ_Aba = new CNCMatic.Principal.RepeatButton();
            this.btnMovZ_Arr = new CNCMatic.Principal.RepeatButton();
            this.gbMovXY = new System.Windows.Forms.GroupBox();
            this.btnMovXY_Der = new CNCMatic.Principal.RepeatButton();
            this.btnMovXY_Izq = new CNCMatic.Principal.RepeatButton();
            this.btnMovXY_Aba = new CNCMatic.Principal.RepeatButton();
            this.btnMovXY_Arr = new CNCMatic.Principal.RepeatButton();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.grpImportacion.SuspendLayout();
            this.pnlImportG.SuspendLayout();
            this.pnlImportDXF.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbStop.SuspendLayout();
            this.gbInicio.SuspendLayout();
            this.gbPosicionActual.SuspendLayout();
            this.gbMovZ.SuspendLayout();
            this.gbMovXY.SuspendLayout();
            this.SuspendLayout();
            // 
            // importaDXF
            // 
            this.importaDXF.DefaultExt = "*.dxf";
            this.importaDXF.FileName = "*.dxf";
            this.importaDXF.Filter = "Archivos DXF (*.dxf)|*.dxf";
            // 
            // importaG
            // 
            this.importaG.DefaultExt = "*.gcode";
            this.importaG.FileName = "*.gcode";
            this.importaG.Filter = "Codigo G(*.gcode)|*.gcode";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(730, 396);
            this.tabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grpImportacion);
            this.tabPage1.Controls.Add(this.txtGpreview);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(722, 370);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grpImportacion
            // 
            this.grpImportacion.BackColor = System.Drawing.Color.Transparent;
            this.grpImportacion.Controls.Add(this.rdbG);
            this.grpImportacion.Controls.Add(this.rdbDXF);
            this.grpImportacion.Controls.Add(this.pnlImportG);
            this.grpImportacion.Controls.Add(this.pnlImportDXF);
            this.grpImportacion.Location = new System.Drawing.Point(28, 76);
            this.grpImportacion.Name = "grpImportacion";
            this.grpImportacion.Size = new System.Drawing.Size(348, 218);
            this.grpImportacion.TabIndex = 5;
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
            // 
            // btnBuscarDXF
            // 
            this.btnBuscarDXF.Location = new System.Drawing.Point(230, -1);
            this.btnBuscarDXF.Name = "btnBuscarDXF";
            this.btnBuscarDXF.Size = new System.Drawing.Size(29, 23);
            this.btnBuscarDXF.TabIndex = 7;
            this.btnBuscarDXF.Text = "...";
            this.btnBuscarDXF.UseVisualStyleBackColor = true;
            // 
            // txtFilePathD
            // 
            this.txtFilePathD.Enabled = false;
            this.txtFilePathD.Location = new System.Drawing.Point(3, 3);
            this.txtFilePathD.Name = "txtFilePathD";
            this.txtFilePathD.Size = new System.Drawing.Size(220, 20);
            this.txtFilePathD.TabIndex = 6;
            // 
            // txtGpreview
            // 
            this.txtGpreview.Location = new System.Drawing.Point(484, 88);
            this.txtGpreview.Multiline = true;
            this.txtGpreview.Name = "txtGpreview";
            this.txtGpreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtGpreview.Size = new System.Drawing.Size(210, 199);
            this.txtGpreview.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbStop);
            this.tabPage2.Controls.Add(this.gbInicio);
            this.tabPage2.Controls.Add(this.gbPosicionActual);
            this.tabPage2.Controls.Add(this.gbMovZ);
            this.tabPage2.Controls.Add(this.gbMovXY);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(722, 370);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbStop
            // 
            this.gbStop.Controls.Add(this.btnStop);
            this.gbStop.Location = new System.Drawing.Point(241, 7);
            this.gbStop.Name = "gbStop";
            this.gbStop.Size = new System.Drawing.Size(68, 86);
            this.gbStop.TabIndex = 5;
            this.gbStop.TabStop = false;
            this.gbStop.Text = "Stop";
            // 
            // btnStop
            // 
            this.btnStop.Image = global::CNCMatic.Properties.Resources.stop1;
            this.btnStop.Location = new System.Drawing.Point(6, 18);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(54, 55);
            this.btnStop.TabIndex = 2;
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // gbInicio
            // 
            this.gbInicio.Controls.Add(this.btnInicio);
            this.gbInicio.Location = new System.Drawing.Point(167, 7);
            this.gbInicio.Name = "gbInicio";
            this.gbInicio.Size = new System.Drawing.Size(68, 86);
            this.gbInicio.TabIndex = 4;
            this.gbInicio.TabStop = false;
            this.gbInicio.Text = "Inicio";
            // 
            // btnInicio
            // 
            this.btnInicio.Image = global::CNCMatic.Properties.Resources.HomeButton;
            this.btnInicio.Location = new System.Drawing.Point(6, 18);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(54, 55);
            this.btnInicio.TabIndex = 2;
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // gbPosicionActual
            // 
            this.gbPosicionActual.Controls.Add(this.lblPosZ);
            this.gbPosicionActual.Controls.Add(this.txtPosZ);
            this.gbPosicionActual.Controls.Add(this.lblPosY);
            this.gbPosicionActual.Controls.Add(this.txtPosY);
            this.gbPosicionActual.Controls.Add(this.lblPosX);
            this.gbPosicionActual.Controls.Add(this.txtPosX);
            this.gbPosicionActual.Location = new System.Drawing.Point(18, 6);
            this.gbPosicionActual.Name = "gbPosicionActual";
            this.gbPosicionActual.Size = new System.Drawing.Size(125, 109);
            this.gbPosicionActual.TabIndex = 3;
            this.gbPosicionActual.TabStop = false;
            this.gbPosicionActual.Text = "Posición Actual";
            // 
            // lblPosZ
            // 
            this.lblPosZ.AutoSize = true;
            this.lblPosZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosZ.Location = new System.Drawing.Point(6, 74);
            this.lblPosZ.Name = "lblPosZ";
            this.lblPosZ.Size = new System.Drawing.Size(15, 13);
            this.lblPosZ.TabIndex = 5;
            this.lblPosZ.Text = "Z";
            // 
            // txtPosZ
            // 
            this.txtPosZ.Location = new System.Drawing.Point(26, 71);
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(78, 20);
            this.txtPosZ.TabIndex = 4;
            this.txtPosZ.Text = "0";
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosY.Location = new System.Drawing.Point(6, 48);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(15, 13);
            this.lblPosY.TabIndex = 3;
            this.lblPosY.Text = "Y";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(26, 45);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(78, 20);
            this.txtPosY.TabIndex = 2;
            this.txtPosY.Text = "0";
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosX.Location = new System.Drawing.Point(6, 22);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(15, 13);
            this.lblPosX.TabIndex = 1;
            this.lblPosX.Text = "X";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(26, 19);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(78, 20);
            this.txtPosX.TabIndex = 0;
            this.txtPosX.Text = "0";
            // 
            // gbMovZ
            // 
            this.gbMovZ.Controls.Add(this.btnMovZ_Aba);
            this.gbMovZ.Controls.Add(this.btnMovZ_Arr);
            this.gbMovZ.Location = new System.Drawing.Point(18, 134);
            this.gbMovZ.Name = "gbMovZ";
            this.gbMovZ.Size = new System.Drawing.Size(68, 162);
            this.gbMovZ.TabIndex = 1;
            this.gbMovZ.TabStop = false;
            this.gbMovZ.Text = "Mov-Z";
            // 
            // btnMovZ_Aba
            // 
            this.btnMovZ_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovZ_Aba.Location = new System.Drawing.Point(5, 95);
            this.btnMovZ_Aba.Name = "btnMovZ_Aba";
            this.btnMovZ_Aba.Size = new System.Drawing.Size(57, 61);
            this.btnMovZ_Aba.TabIndex = 7;
            this.btnMovZ_Aba.UseVisualStyleBackColor = true;
            this.btnMovZ_Aba.Click += new System.EventHandler(this.btnMovZ_Aba_Click);
            // 
            // btnMovZ_Arr
            // 
            this.btnMovZ_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovZ_Arr.Location = new System.Drawing.Point(5, 28);
            this.btnMovZ_Arr.Name = "btnMovZ_Arr";
            this.btnMovZ_Arr.Size = new System.Drawing.Size(57, 61);
            this.btnMovZ_Arr.TabIndex = 6;
            this.btnMovZ_Arr.UseVisualStyleBackColor = true;
            this.btnMovZ_Arr.Click += new System.EventHandler(this.btnMovZ_Arr_Click);
            // 
            // gbMovXY
            // 
            this.gbMovXY.Controls.Add(this.btnMovXY_Der);
            this.gbMovXY.Controls.Add(this.btnMovXY_Izq);
            this.gbMovXY.Controls.Add(this.btnMovXY_Aba);
            this.gbMovXY.Controls.Add(this.btnMovXY_Arr);
            this.gbMovXY.Location = new System.Drawing.Point(101, 134);
            this.gbMovXY.Name = "gbMovXY";
            this.gbMovXY.Size = new System.Drawing.Size(200, 162);
            this.gbMovXY.TabIndex = 0;
            this.gbMovXY.TabStop = false;
            this.gbMovXY.Text = "Mov-XY";
            this.gbMovXY.Enter += new System.EventHandler(this.gbMovXY_Enter);
            // 
            // btnMovXY_Der
            // 
            this.btnMovXY_Der.Image = global::CNCMatic.Properties.Resources.flecha_DER;
            this.btnMovXY_Der.Location = new System.Drawing.Point(132, 86);
            this.btnMovXY_Der.Name = "btnMovXY_Der";
            this.btnMovXY_Der.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Der.TabIndex = 10;
            this.btnMovXY_Der.UseVisualStyleBackColor = true;
            this.btnMovXY_Der.Click += new System.EventHandler(this.btnMovXY_Der_Click);
            // 
            // btnMovXY_Izq
            // 
            this.btnMovXY_Izq.Image = global::CNCMatic.Properties.Resources.flecha_IZQ;
            this.btnMovXY_Izq.Location = new System.Drawing.Point(6, 86);
            this.btnMovXY_Izq.Name = "btnMovXY_Izq";
            this.btnMovXY_Izq.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Izq.TabIndex = 9;
            this.btnMovXY_Izq.UseVisualStyleBackColor = true;
            this.btnMovXY_Izq.Click += new System.EventHandler(this.btnMovXY_Izq_Click);
            // 
            // btnMovXY_Aba
            // 
            this.btnMovXY_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovXY_Aba.Location = new System.Drawing.Point(69, 95);
            this.btnMovXY_Aba.Name = "btnMovXY_Aba";
            this.btnMovXY_Aba.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Aba.TabIndex = 8;
            this.btnMovXY_Aba.UseVisualStyleBackColor = true;
            this.btnMovXY_Aba.Click += new System.EventHandler(this.btnMovXY_Aba_Click);
            // 
            // btnMovXY_Arr
            // 
            this.btnMovXY_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovXY_Arr.Location = new System.Drawing.Point(69, 28);
            this.btnMovXY_Arr.Name = "btnMovXY_Arr";
            this.btnMovXY_Arr.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Arr.TabIndex = 7;
            this.btnMovXY_Arr.UseVisualStyleBackColor = true;
            this.btnMovXY_Arr.Click += new System.EventHandler(this.btnMovXY_Arr_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(754, 420);
            this.Controls.Add(this.tabControl);
            this.Name = "Principal";
            this.Text = "CNC Matic";
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.grpImportacion.ResumeLayout(false);
            this.grpImportacion.PerformLayout();
            this.pnlImportG.ResumeLayout(false);
            this.pnlImportG.PerformLayout();
            this.pnlImportDXF.ResumeLayout(false);
            this.pnlImportDXF.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gbStop.ResumeLayout(false);
            this.gbInicio.ResumeLayout(false);
            this.gbPosicionActual.ResumeLayout(false);
            this.gbPosicionActual.PerformLayout();
            this.gbMovZ.ResumeLayout(false);
            this.gbMovXY.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog importaDXF;
        private System.Windows.Forms.OpenFileDialog importaG;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox grpImportacion;
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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gbMovXY;
        private System.Windows.Forms.GroupBox gbMovZ;
        private System.Windows.Forms.GroupBox gbPosicionActual;
        private System.Windows.Forms.Label lblPosZ;
        private System.Windows.Forms.TextBox txtPosZ;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.GroupBox gbInicio;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.GroupBox gbStop;
        private System.Windows.Forms.Button btnStop;
        private RepeatButton btnMovZ_Arr;
        private Principal.RepeatButton btnMovZ_Aba;
        private Principal.RepeatButton btnMovXY_Der;
        private Principal.RepeatButton btnMovXY_Izq;
        private Principal.RepeatButton btnMovXY_Aba;
        private Principal.RepeatButton btnMovXY_Arr;
    }
}
