namespace CNCMatic
{
    partial class FrmConfiguracion
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
            this.grpConfGral = new System.Windows.Forms.GroupBox();
            this.rbtAbsoluta = new System.Windows.Forms.RadioButton();
            this.rbtRelativa = new System.Windows.Forms.RadioButton();
            this.lblTipoProg = new System.Windows.Forms.Label();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.rbtPULG = new System.Windows.Forms.RadioButton();
            this.rbtMM = new System.Windows.Forms.RadioButton();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.grpConfigMaterial = new System.Windows.Forms.GroupBox();
            this.txtGrados = new System.Windows.Forms.MaskedTextBox();
            this.lblGrados = new System.Windows.Forms.Label();
            this.txtVueltas = new System.Windows.Forms.MaskedTextBox();
            this.cmbMotor = new System.Windows.Forms.ComboBox();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.lblVueltas = new System.Windows.Forms.Label();
            this.lblMotor = new System.Windows.Forms.Label();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.grpUnidadMedida = new System.Windows.Forms.GroupBox();
            this.grpTipoProgr = new System.Windows.Forms.GroupBox();
            this.lblConfig = new System.Windows.Forms.Label();
            this.cmbConfiguracion = new System.Windows.Forms.ComboBox();
            this.grpConfGral.SuspendLayout();
            this.grpConfigMaterial.SuspendLayout();
            this.grpUnidadMedida.SuspendLayout();
            this.grpTipoProgr.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConfGral
            // 
            this.grpConfGral.Controls.Add(this.grpTipoProgr);
            this.grpConfGral.Controls.Add(this.grpUnidadMedida);
            this.grpConfGral.Controls.Add(this.lblPuerto);
            this.grpConfGral.Controls.Add(this.portComboBox);
            this.grpConfGral.Location = new System.Drawing.Point(9, 50);
            this.grpConfGral.Name = "grpConfGral";
            this.grpConfGral.Size = new System.Drawing.Size(453, 206);
            this.grpConfGral.TabIndex = 0;
            this.grpConfGral.TabStop = false;
            this.grpConfGral.Text = "Configuración General";
            // 
            // rbtAbsoluta
            // 
            this.rbtAbsoluta.AutoSize = true;
            this.rbtAbsoluta.Location = new System.Drawing.Point(131, 8);
            this.rbtAbsoluta.Name = "rbtAbsoluta";
            this.rbtAbsoluta.Size = new System.Drawing.Size(66, 17);
            this.rbtAbsoluta.TabIndex = 20;
            this.rbtAbsoluta.TabStop = true;
            this.rbtAbsoluta.Text = "Absoluta";
            this.rbtAbsoluta.UseVisualStyleBackColor = true;
            // 
            // rbtRelativa
            // 
            this.rbtRelativa.AutoSize = true;
            this.rbtRelativa.Location = new System.Drawing.Point(130, 38);
            this.rbtRelativa.Name = "rbtRelativa";
            this.rbtRelativa.Size = new System.Drawing.Size(64, 17);
            this.rbtRelativa.TabIndex = 19;
            this.rbtRelativa.TabStop = true;
            this.rbtRelativa.Text = "Relativa";
            this.rbtRelativa.UseVisualStyleBackColor = true;
            // 
            // lblTipoProg
            // 
            this.lblTipoProg.AutoSize = true;
            this.lblTipoProg.Location = new System.Drawing.Point(7, 25);
            this.lblTipoProg.Name = "lblTipoProg";
            this.lblTipoProg.Size = new System.Drawing.Size(114, 13);
            this.lblTipoProg.TabIndex = 18;
            this.lblTipoProg.Text = "Tipo de Programación:";
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Location = new System.Drawing.Point(125, 179);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(68, 13);
            this.lblPuerto.TabIndex = 17;
            this.lblPuerto.Text = "Puerto COM:";
            // 
            // portComboBox
            // 
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(206, 176);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(121, 21);
            this.portComboBox.TabIndex = 16;
            // 
            // rbtPULG
            // 
            this.rbtPULG.AutoSize = true;
            this.rbtPULG.Location = new System.Drawing.Point(131, 46);
            this.rbtPULG.Name = "rbtPULG";
            this.rbtPULG.Size = new System.Drawing.Size(97, 17);
            this.rbtPULG.TabIndex = 3;
            this.rbtPULG.TabStop = true;
            this.rbtPULG.Text = "pulgadas (pulg)";
            this.rbtPULG.UseVisualStyleBackColor = true;
            // 
            // rbtMM
            // 
            this.rbtMM.AutoSize = true;
            this.rbtMM.Location = new System.Drawing.Point(131, 16);
            this.rbtMM.Name = "rbtMM";
            this.rbtMM.Size = new System.Drawing.Size(95, 17);
            this.rbtMM.TabIndex = 2;
            this.rbtMM.TabStop = true;
            this.rbtMM.Text = "milimetros (mm)";
            this.rbtMM.UseVisualStyleBackColor = true;
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Location = new System.Drawing.Point(10, 33);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(82, 13);
            this.lblUnidad.TabIndex = 0;
            this.lblUnidad.Text = "Unidad Medida:";
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(198, 411);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 1;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // grpConfigMaterial
            // 
            this.grpConfigMaterial.Controls.Add(this.txtGrados);
            this.grpConfigMaterial.Controls.Add(this.lblGrados);
            this.grpConfigMaterial.Controls.Add(this.txtVueltas);
            this.grpConfigMaterial.Controls.Add(this.cmbMotor);
            this.grpConfigMaterial.Controls.Add(this.cmbMaterial);
            this.grpConfigMaterial.Controls.Add(this.lblVueltas);
            this.grpConfigMaterial.Controls.Add(this.lblMotor);
            this.grpConfigMaterial.Controls.Add(this.lblMaterial);
            this.grpConfigMaterial.Location = new System.Drawing.Point(9, 262);
            this.grpConfigMaterial.Name = "grpConfigMaterial";
            this.grpConfigMaterial.Size = new System.Drawing.Size(453, 139);
            this.grpConfigMaterial.TabIndex = 2;
            this.grpConfigMaterial.TabStop = false;
            this.grpConfigMaterial.Text = "Configuración por Material";
            // 
            // txtGrados
            // 
            this.txtGrados.Location = new System.Drawing.Point(116, 101);
            this.txtGrados.Mask = "9999999";
            this.txtGrados.Name = "txtGrados";
            this.txtGrados.Size = new System.Drawing.Size(79, 20);
            this.txtGrados.TabIndex = 8;
            // 
            // lblGrados
            // 
            this.lblGrados.AutoSize = true;
            this.lblGrados.Location = new System.Drawing.Point(21, 104);
            this.lblGrados.Name = "lblGrados";
            this.lblGrados.Size = new System.Drawing.Size(88, 13);
            this.lblGrados.TabIndex = 7;
            this.lblGrados.Text = "Grados por paso:";
            // 
            // txtVueltas
            // 
            this.txtVueltas.Location = new System.Drawing.Point(116, 65);
            this.txtVueltas.Mask = "9999999";
            this.txtVueltas.Name = "txtVueltas";
            this.txtVueltas.Size = new System.Drawing.Size(79, 20);
            this.txtVueltas.TabIndex = 6;
            // 
            // cmbMotor
            // 
            this.cmbMotor.FormattingEnabled = true;
            this.cmbMotor.Location = new System.Drawing.Point(289, 27);
            this.cmbMotor.Name = "cmbMotor";
            this.cmbMotor.Size = new System.Drawing.Size(121, 21);
            this.cmbMotor.TabIndex = 4;
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Location = new System.Drawing.Point(74, 27);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(121, 21);
            this.cmbMaterial.TabIndex = 3;
            // 
            // lblVueltas
            // 
            this.lblVueltas.AutoSize = true;
            this.lblVueltas.Location = new System.Drawing.Point(21, 68);
            this.lblVueltas.Name = "lblVueltas";
            this.lblVueltas.Size = new System.Drawing.Size(89, 13);
            this.lblVueltas.TabIndex = 2;
            this.lblVueltas.Text = "Vueltas por paso:";
            // 
            // lblMotor
            // 
            this.lblMotor.AutoSize = true;
            this.lblMotor.Location = new System.Drawing.Point(236, 30);
            this.lblMotor.Name = "lblMotor";
            this.lblMotor.Size = new System.Drawing.Size(37, 13);
            this.lblMotor.TabIndex = 1;
            this.lblMotor.Text = "Motor:";
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Location = new System.Drawing.Point(21, 30);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(47, 13);
            this.lblMaterial.TabIndex = 0;
            this.lblMaterial.Text = "Material:";
            // 
            // grpUnidadMedida
            // 
            this.grpUnidadMedida.Controls.Add(this.rbtPULG);
            this.grpUnidadMedida.Controls.Add(this.lblUnidad);
            this.grpUnidadMedida.Controls.Add(this.rbtMM);
            this.grpUnidadMedida.Location = new System.Drawing.Point(100, 19);
            this.grpUnidadMedida.Name = "grpUnidadMedida";
            this.grpUnidadMedida.Size = new System.Drawing.Size(240, 70);
            this.grpUnidadMedida.TabIndex = 21;
            this.grpUnidadMedida.TabStop = false;
            // 
            // grpTipoProgr
            // 
            this.grpTipoProgr.Controls.Add(this.rbtRelativa);
            this.grpTipoProgr.Controls.Add(this.lblTipoProg);
            this.grpTipoProgr.Controls.Add(this.rbtAbsoluta);
            this.grpTipoProgr.Location = new System.Drawing.Point(100, 92);
            this.grpTipoProgr.Name = "grpTipoProgr";
            this.grpTipoProgr.Size = new System.Drawing.Size(240, 63);
            this.grpTipoProgr.TabIndex = 22;
            this.grpTipoProgr.TabStop = false;
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Location = new System.Drawing.Point(12, 16);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(75, 13);
            this.lblConfig.TabIndex = 3;
            this.lblConfig.Text = "Configuracion:";
            // 
            // cmbConfiguracion
            // 
            this.cmbConfiguracion.FormattingEnabled = true;
            this.cmbConfiguracion.Location = new System.Drawing.Point(109, 14);
            this.cmbConfiguracion.Name = "cmbConfiguracion";
            this.cmbConfiguracion.Size = new System.Drawing.Size(240, 21);
            this.cmbConfiguracion.TabIndex = 4;
            // 
            // FrmConfiguracion
            // 
            this.AcceptButton = this.btnGrabar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 449);
            this.Controls.Add(this.cmbConfiguracion);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.grpConfigMaterial);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.grpConfGral);
            this.HelpButton = true;
            this.Name = "FrmConfiguracion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion";
            this.Load += new System.EventHandler(this.FrmConfiguracion_Load);
            this.grpConfGral.ResumeLayout(false);
            this.grpConfGral.PerformLayout();
            this.grpConfigMaterial.ResumeLayout(false);
            this.grpConfigMaterial.PerformLayout();
            this.grpUnidadMedida.ResumeLayout(false);
            this.grpUnidadMedida.PerformLayout();
            this.grpTipoProgr.ResumeLayout(false);
            this.grpTipoProgr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConfGral;
        private System.Windows.Forms.Label lblUnidad;
        private System.Windows.Forms.RadioButton rbtPULG;
        private System.Windows.Forms.RadioButton rbtMM;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Label lblPuerto;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.RadioButton rbtAbsoluta;
        private System.Windows.Forms.RadioButton rbtRelativa;
        private System.Windows.Forms.Label lblTipoProg;
        private System.Windows.Forms.GroupBox grpConfigMaterial;
        private System.Windows.Forms.MaskedTextBox txtVueltas;
        private System.Windows.Forms.ComboBox cmbMotor;
        private System.Windows.Forms.ComboBox cmbMaterial;
        private System.Windows.Forms.Label lblVueltas;
        private System.Windows.Forms.Label lblMotor;
        private System.Windows.Forms.Label lblMaterial;
        private System.Windows.Forms.MaskedTextBox txtGrados;
        private System.Windows.Forms.Label lblGrados;
        private System.Windows.Forms.GroupBox grpTipoProgr;
        private System.Windows.Forms.GroupBox grpUnidadMedida;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.ComboBox cmbConfiguracion;
    }
}