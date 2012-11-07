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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfiguracion));
            this.grpConfGral = new System.Windows.Forms.GroupBox();
            this.txtAltura = new System.Windows.Forms.TextBox();
            this.lblAltura = new System.Windows.Forms.Label();
            this.txtVelocMov = new System.Windows.Forms.TextBox();
            this.txtLargoSeccion = new System.Windows.Forms.TextBox();
            this.txtMaxZ = new System.Windows.Forms.NumericUpDown();
            this.txtMaxY = new System.Windows.Forms.NumericUpDown();
            this.txtMaxX = new System.Windows.Forms.NumericUpDown();
            this.lblLargoSeccion = new System.Windows.Forms.Label();
            this.grpMotores = new System.Windows.Forms.GroupBox();
            this.lblMotorZ = new System.Windows.Forms.Label();
            this.txtGradosZ = new System.Windows.Forms.TextBox();
            this.txtVueltasZ = new System.Windows.Forms.TextBox();
            this.lblMotorY = new System.Windows.Forms.Label();
            this.txtGradosY = new System.Windows.Forms.TextBox();
            this.txtVueltasY = new System.Windows.Forms.TextBox();
            this.lblMotorX = new System.Windows.Forms.Label();
            this.txtGradosX = new System.Windows.Forms.TextBox();
            this.txtVueltasX = new System.Windows.Forms.TextBox();
            this.lblGrados = new System.Windows.Forms.Label();
            this.lblVueltas = new System.Windows.Forms.Label();
            this.lblVelocMov = new System.Windows.Forms.Label();
            this.lblMaxZ = new System.Windows.Forms.Label();
            this.lblMaxY = new System.Windows.Forms.Label();
            this.lblMaxX = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpTipoProgr = new System.Windows.Forms.GroupBox();
            this.rbtRelativa = new System.Windows.Forms.RadioButton();
            this.lblTipoProg = new System.Windows.Forms.Label();
            this.rbtAbsoluta = new System.Windows.Forms.RadioButton();
            this.grpUnidadMedida = new System.Windows.Forms.GroupBox();
            this.rbtPULG = new System.Windows.Forms.RadioButton();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.rbtMM = new System.Windows.Forms.RadioButton();
            this.lblPuerto = new System.Windows.Forms.Label();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.lblConfig = new System.Windows.Forms.Label();
            this.cmbConfiguracion = new System.Windows.Forms.ComboBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombrePerfil = new System.Windows.Forms.TextBox();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.grpConfGral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxX)).BeginInit();
            this.grpMotores.SuspendLayout();
            this.grpTipoProgr.SuspendLayout();
            this.grpUnidadMedida.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConfGral
            // 
            this.grpConfGral.Controls.Add(this.txtAltura);
            this.grpConfGral.Controls.Add(this.lblAltura);
            this.grpConfGral.Controls.Add(this.txtVelocMov);
            this.grpConfGral.Controls.Add(this.txtLargoSeccion);
            this.grpConfGral.Controls.Add(this.txtMaxZ);
            this.grpConfGral.Controls.Add(this.txtMaxY);
            this.grpConfGral.Controls.Add(this.txtMaxX);
            this.grpConfGral.Controls.Add(this.lblLargoSeccion);
            this.grpConfGral.Controls.Add(this.grpMotores);
            this.grpConfGral.Controls.Add(this.lblVelocMov);
            this.grpConfGral.Controls.Add(this.lblMaxZ);
            this.grpConfGral.Controls.Add(this.lblMaxY);
            this.grpConfGral.Controls.Add(this.lblMaxX);
            this.grpConfGral.Controls.Add(this.btnRefresh);
            this.grpConfGral.Controls.Add(this.grpTipoProgr);
            this.grpConfGral.Controls.Add(this.grpUnidadMedida);
            this.grpConfGral.Controls.Add(this.lblPuerto);
            this.grpConfGral.Controls.Add(this.portComboBox);
            this.grpConfGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpConfGral.Location = new System.Drawing.Point(9, 47);
            this.grpConfGral.Name = "grpConfGral";
            this.grpConfGral.Size = new System.Drawing.Size(508, 284);
            this.grpConfGral.TabIndex = 1;
            this.grpConfGral.TabStop = false;
            this.grpConfGral.Text = "Configuración General";
            // 
            // txtAltura
            // 
            this.txtAltura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAltura.Location = new System.Drawing.Point(367, 133);
            this.txtAltura.Name = "txtAltura";
            this.txtAltura.Size = new System.Drawing.Size(71, 20);
            this.txtAltura.TabIndex = 9;
            this.txtAltura.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAltura_KeyPress);
            // 
            // lblAltura
            // 
            this.lblAltura.AutoSize = true;
            this.lblAltura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAltura.Location = new System.Drawing.Point(233, 139);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(78, 13);
            this.lblAltura.TabIndex = 43;
            this.lblAltura.Text = "Nivel de altura:";
            // 
            // txtVelocMov
            // 
            this.txtVelocMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVelocMov.Location = new System.Drawing.Point(366, 82);
            this.txtVelocMov.Name = "txtVelocMov";
            this.txtVelocMov.Size = new System.Drawing.Size(72, 20);
            this.txtVelocMov.TabIndex = 7;
            this.txtVelocMov.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVelocMov_KeyPress);
            // 
            // txtLargoSeccion
            // 
            this.txtLargoSeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLargoSeccion.Location = new System.Drawing.Point(366, 107);
            this.txtLargoSeccion.Name = "txtLargoSeccion";
            this.txtLargoSeccion.Size = new System.Drawing.Size(72, 20);
            this.txtLargoSeccion.TabIndex = 8;
            this.txtLargoSeccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLargoSeccion_KeyPress);
            // 
            // txtMaxZ
            // 
            this.txtMaxZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxZ.Location = new System.Drawing.Point(449, 23);
            this.txtMaxZ.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.txtMaxZ.Name = "txtMaxZ";
            this.txtMaxZ.Size = new System.Drawing.Size(48, 20);
            this.txtMaxZ.TabIndex = 4;
            // 
            // txtMaxY
            // 
            this.txtMaxY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxY.Location = new System.Drawing.Point(362, 23);
            this.txtMaxY.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(48, 20);
            this.txtMaxY.TabIndex = 3;
            // 
            // txtMaxX
            // 
            this.txtMaxX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxX.Location = new System.Drawing.Point(270, 23);
            this.txtMaxX.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(48, 20);
            this.txtMaxX.TabIndex = 2;
            // 
            // lblLargoSeccion
            // 
            this.lblLargoSeccion.AutoSize = true;
            this.lblLargoSeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLargoSeccion.Location = new System.Drawing.Point(232, 113);
            this.lblLargoSeccion.Name = "lblLargoSeccion";
            this.lblLargoSeccion.Size = new System.Drawing.Size(133, 13);
            this.lblLargoSeccion.TabIndex = 32;
            this.lblLargoSeccion.Text = "Largo de sección (curvas):";
            // 
            // grpMotores
            // 
            this.grpMotores.Controls.Add(this.lblMotorZ);
            this.grpMotores.Controls.Add(this.txtGradosZ);
            this.grpMotores.Controls.Add(this.txtVueltasZ);
            this.grpMotores.Controls.Add(this.lblMotorY);
            this.grpMotores.Controls.Add(this.txtGradosY);
            this.grpMotores.Controls.Add(this.txtVueltasY);
            this.grpMotores.Controls.Add(this.lblMotorX);
            this.grpMotores.Controls.Add(this.txtGradosX);
            this.grpMotores.Controls.Add(this.txtVueltasX);
            this.grpMotores.Controls.Add(this.lblGrados);
            this.grpMotores.Controls.Add(this.lblVueltas);
            this.grpMotores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMotores.Location = new System.Drawing.Point(3, 155);
            this.grpMotores.Name = "grpMotores";
            this.grpMotores.Size = new System.Drawing.Size(499, 120);
            this.grpMotores.TabIndex = 10;
            this.grpMotores.TabStop = false;
            this.grpMotores.Text = "Configuración por Motor";
            // 
            // lblMotorZ
            // 
            this.lblMotorZ.AutoSize = true;
            this.lblMotorZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotorZ.Location = new System.Drawing.Point(26, 94);
            this.lblMotorZ.Name = "lblMotorZ";
            this.lblMotorZ.Size = new System.Drawing.Size(65, 13);
            this.lblMotorZ.TabIndex = 19;
            this.lblMotorZ.Text = "Motor Eje Z:";
            // 
            // txtGradosZ
            // 
            this.txtGradosZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGradosZ.Location = new System.Drawing.Point(323, 91);
            this.txtGradosZ.Name = "txtGradosZ";
            this.txtGradosZ.Size = new System.Drawing.Size(79, 20);
            this.txtGradosZ.TabIndex = 5;
            this.txtGradosZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGradosZ_KeyPress);
            // 
            // txtVueltasZ
            // 
            this.txtVueltasZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVueltasZ.Location = new System.Drawing.Point(162, 91);
            this.txtVueltasZ.Name = "txtVueltasZ";
            this.txtVueltasZ.Size = new System.Drawing.Size(98, 20);
            this.txtVueltasZ.TabIndex = 4;
            this.txtVueltasZ.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVueltasZ_KeyPress);
            // 
            // lblMotorY
            // 
            this.lblMotorY.AutoSize = true;
            this.lblMotorY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotorY.Location = new System.Drawing.Point(26, 70);
            this.lblMotorY.Name = "lblMotorY";
            this.lblMotorY.Size = new System.Drawing.Size(65, 13);
            this.lblMotorY.TabIndex = 16;
            this.lblMotorY.Text = "Motor Eje Y:";
            // 
            // txtGradosY
            // 
            this.txtGradosY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGradosY.Location = new System.Drawing.Point(323, 67);
            this.txtGradosY.Name = "txtGradosY";
            this.txtGradosY.Size = new System.Drawing.Size(79, 20);
            this.txtGradosY.TabIndex = 3;
            this.txtGradosY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGradosY_KeyPress);
            // 
            // txtVueltasY
            // 
            this.txtVueltasY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVueltasY.Location = new System.Drawing.Point(162, 67);
            this.txtVueltasY.Name = "txtVueltasY";
            this.txtVueltasY.Size = new System.Drawing.Size(98, 20);
            this.txtVueltasY.TabIndex = 2;
            this.txtVueltasY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVueltasY_KeyPress);
            // 
            // lblMotorX
            // 
            this.lblMotorX.AutoSize = true;
            this.lblMotorX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotorX.Location = new System.Drawing.Point(26, 45);
            this.lblMotorX.Name = "lblMotorX";
            this.lblMotorX.Size = new System.Drawing.Size(65, 13);
            this.lblMotorX.TabIndex = 13;
            this.lblMotorX.Text = "Motor Eje X:";
            // 
            // txtGradosX
            // 
            this.txtGradosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGradosX.Location = new System.Drawing.Point(323, 42);
            this.txtGradosX.Name = "txtGradosX";
            this.txtGradosX.Size = new System.Drawing.Size(79, 20);
            this.txtGradosX.TabIndex = 1;
            this.txtGradosX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGradosX_KeyPress);
            // 
            // txtVueltasX
            // 
            this.txtVueltasX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVueltasX.Location = new System.Drawing.Point(162, 42);
            this.txtVueltasX.Name = "txtVueltasX";
            this.txtVueltasX.Size = new System.Drawing.Size(98, 20);
            this.txtVueltasX.TabIndex = 0;
            this.txtVueltasX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVueltasX_KeyPress);
            // 
            // lblGrados
            // 
            this.lblGrados.AutoSize = true;
            this.lblGrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrados.Location = new System.Drawing.Point(321, 23);
            this.lblGrados.Name = "lblGrados";
            this.lblGrados.Size = new System.Drawing.Size(85, 13);
            this.lblGrados.TabIndex = 7;
            this.lblGrados.Text = "Grados por paso";
            // 
            // lblVueltas
            // 
            this.lblVueltas.AutoSize = true;
            this.lblVueltas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVueltas.Location = new System.Drawing.Point(161, 23);
            this.lblVueltas.Name = "lblVueltas";
            this.lblVueltas.Size = new System.Drawing.Size(101, 13);
            this.lblVueltas.TabIndex = 2;
            this.lblVueltas.Text = "Distancia por vuelta";
            // 
            // lblVelocMov
            // 
            this.lblVelocMov.AutoSize = true;
            this.lblVelocMov.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVelocMov.Location = new System.Drawing.Point(232, 85);
            this.lblVelocMov.Name = "lblVelocMov";
            this.lblVelocMov.Size = new System.Drawing.Size(128, 13);
            this.lblVelocMov.TabIndex = 30;
            this.lblVelocMov.Text = "Velocidad de movimiento:";
            // 
            // lblMaxZ
            // 
            this.lblMaxZ.AutoSize = true;
            this.lblMaxZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxZ.Location = new System.Drawing.Point(411, 26);
            this.lblMaxZ.Name = "lblMaxZ";
            this.lblMaxZ.Size = new System.Drawing.Size(37, 13);
            this.lblMaxZ.TabIndex = 28;
            this.lblMaxZ.Text = "MaxZ:";
            // 
            // lblMaxY
            // 
            this.lblMaxY.AutoSize = true;
            this.lblMaxY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxY.Location = new System.Drawing.Point(323, 26);
            this.lblMaxY.Name = "lblMaxY";
            this.lblMaxY.Size = new System.Drawing.Size(37, 13);
            this.lblMaxY.TabIndex = 26;
            this.lblMaxY.Text = "MaxY:";
            // 
            // lblMaxX
            // 
            this.lblMaxX.AutoSize = true;
            this.lblMaxX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxX.Location = new System.Drawing.Point(231, 26);
            this.lblMaxX.Name = "lblMaxX";
            this.lblMaxX.Size = new System.Drawing.Size(37, 13);
            this.lblMaxX.TabIndex = 24;
            this.lblMaxX.Text = "MaxX:";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::CNCMatic.Properties.Resources.Refresh_icon;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(459, 51);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(31, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // grpTipoProgr
            // 
            this.grpTipoProgr.Controls.Add(this.rbtRelativa);
            this.grpTipoProgr.Controls.Add(this.lblTipoProg);
            this.grpTipoProgr.Controls.Add(this.rbtAbsoluta);
            this.grpTipoProgr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTipoProgr.Location = new System.Drawing.Point(3, 85);
            this.grpTipoProgr.Name = "grpTipoProgr";
            this.grpTipoProgr.Size = new System.Drawing.Size(226, 70);
            this.grpTipoProgr.TabIndex = 1;
            this.grpTipoProgr.TabStop = false;
            // 
            // rbtRelativa
            // 
            this.rbtRelativa.AutoSize = true;
            this.rbtRelativa.Location = new System.Drawing.Point(122, 46);
            this.rbtRelativa.Name = "rbtRelativa";
            this.rbtRelativa.Size = new System.Drawing.Size(64, 17);
            this.rbtRelativa.TabIndex = 1;
            this.rbtRelativa.TabStop = true;
            this.rbtRelativa.Text = "Relativa";
            this.rbtRelativa.UseVisualStyleBackColor = true;
            // 
            // lblTipoProg
            // 
            this.lblTipoProg.AutoSize = true;
            this.lblTipoProg.Location = new System.Drawing.Point(6, 31);
            this.lblTipoProg.Name = "lblTipoProg";
            this.lblTipoProg.Size = new System.Drawing.Size(114, 13);
            this.lblTipoProg.TabIndex = 18;
            this.lblTipoProg.Text = "Tipo de Programación:";
            // 
            // rbtAbsoluta
            // 
            this.rbtAbsoluta.AutoSize = true;
            this.rbtAbsoluta.Location = new System.Drawing.Point(122, 16);
            this.rbtAbsoluta.Name = "rbtAbsoluta";
            this.rbtAbsoluta.Size = new System.Drawing.Size(66, 17);
            this.rbtAbsoluta.TabIndex = 0;
            this.rbtAbsoluta.TabStop = true;
            this.rbtAbsoluta.Text = "Absoluta";
            this.rbtAbsoluta.UseVisualStyleBackColor = true;
            // 
            // grpUnidadMedida
            // 
            this.grpUnidadMedida.Controls.Add(this.rbtPULG);
            this.grpUnidadMedida.Controls.Add(this.lblUnidad);
            this.grpUnidadMedida.Controls.Add(this.rbtMM);
            this.grpUnidadMedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpUnidadMedida.Location = new System.Drawing.Point(4, 13);
            this.grpUnidadMedida.Name = "grpUnidadMedida";
            this.grpUnidadMedida.Size = new System.Drawing.Size(225, 70);
            this.grpUnidadMedida.TabIndex = 0;
            this.grpUnidadMedida.TabStop = false;
            // 
            // rbtPULG
            // 
            this.rbtPULG.AutoSize = true;
            this.rbtPULG.Location = new System.Drawing.Point(120, 46);
            this.rbtPULG.Name = "rbtPULG";
            this.rbtPULG.Size = new System.Drawing.Size(97, 17);
            this.rbtPULG.TabIndex = 1;
            this.rbtPULG.TabStop = true;
            this.rbtPULG.Text = "pulgadas (pulg)";
            this.rbtPULG.UseVisualStyleBackColor = true;
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Location = new System.Drawing.Point(8, 31);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(82, 13);
            this.lblUnidad.TabIndex = 0;
            this.lblUnidad.Text = "Unidad Medida:";
            // 
            // rbtMM
            // 
            this.rbtMM.AutoSize = true;
            this.rbtMM.Location = new System.Drawing.Point(120, 16);
            this.rbtMM.Name = "rbtMM";
            this.rbtMM.Size = new System.Drawing.Size(95, 17);
            this.rbtMM.TabIndex = 0;
            this.rbtMM.TabStop = true;
            this.rbtMM.Text = "milimetros (mm)";
            this.rbtMM.UseVisualStyleBackColor = true;
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuerto.Location = new System.Drawing.Point(233, 56);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(68, 13);
            this.lblPuerto.TabIndex = 17;
            this.lblPuerto.Text = "Puerto COM:";
            // 
            // portComboBox
            // 
            this.portComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(307, 51);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(155, 21);
            this.portComboBox.TabIndex = 5;
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfig.Location = new System.Drawing.Point(12, 16);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(140, 13);
            this.lblConfig.TabIndex = 3;
            this.lblConfig.Text = "Perfil de Configuración:";
            // 
            // cmbConfiguracion
            // 
            this.cmbConfiguracion.FormattingEnabled = true;
            this.cmbConfiguracion.Location = new System.Drawing.Point(154, 13);
            this.cmbConfiguracion.Name = "cmbConfiguracion";
            this.cmbConfiguracion.Size = new System.Drawing.Size(271, 21);
            this.cmbConfiguracion.TabIndex = 4;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(14, 16);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(122, 13);
            this.lblNombre.TabIndex = 6;
            this.lblNombre.Text = "Nombre del nuevo perfil:";
            this.lblNombre.Visible = false;
            // 
            // txtNombrePerfil
            // 
            this.txtNombrePerfil.Location = new System.Drawing.Point(154, 13);
            this.txtNombrePerfil.Name = "txtNombrePerfil";
            this.txtNombrePerfil.Size = new System.Drawing.Size(271, 20);
            this.txtNombrePerfil.TabIndex = 0;
            this.txtNombrePerfil.Visible = false;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrabar.Image = global::CNCMatic.Properties.Resources.save1;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(199, 346);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(138, 31);
            this.btnGrabar.TabIndex = 2;
            this.btnGrabar.Text = "Grabar y Cerrar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::CNCMatic.Properties.Resources.Cancel_icon;
            this.btnCancelar.Location = new System.Drawing.Point(471, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(40, 40);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::CNCMatic.Properties.Resources.New_icon;
            this.btnNuevo.Location = new System.Drawing.Point(431, 2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(40, 40);
            this.btnNuevo.TabIndex = 3;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // FrmConfiguracion
            // 
            this.AcceptButton = this.btnGrabar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 386);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtNombrePerfil);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.cmbConfiguracion);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.grpConfGral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmConfiguracion";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuración";
            this.Load += new System.EventHandler(this.FrmConfiguracion_Load);
            this.grpConfGral.ResumeLayout(false);
            this.grpConfGral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxX)).EndInit();
            this.grpMotores.ResumeLayout(false);
            this.grpMotores.PerformLayout();
            this.grpTipoProgr.ResumeLayout(false);
            this.grpTipoProgr.PerformLayout();
            this.grpUnidadMedida.ResumeLayout(false);
            this.grpUnidadMedida.PerformLayout();
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
        private System.Windows.Forms.GroupBox grpTipoProgr;
        private System.Windows.Forms.GroupBox grpUnidadMedida;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.ComboBox cmbConfiguracion;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombrePerfil;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblMaxZ;
        private System.Windows.Forms.Label lblMaxY;
        private System.Windows.Forms.Label lblMaxX;
        private System.Windows.Forms.Label lblLargoSeccion;
        private System.Windows.Forms.Label lblVelocMov;
        private System.Windows.Forms.NumericUpDown txtMaxX;
        private System.Windows.Forms.NumericUpDown txtMaxZ;
        private System.Windows.Forms.NumericUpDown txtMaxY;
        private System.Windows.Forms.TextBox txtLargoSeccion;
        private System.Windows.Forms.TextBox txtVelocMov;
        private System.Windows.Forms.TextBox txtAltura;
        private System.Windows.Forms.Label lblAltura;
        private System.Windows.Forms.GroupBox grpMotores;
        private System.Windows.Forms.TextBox txtGradosX;
        private System.Windows.Forms.TextBox txtVueltasX;
        private System.Windows.Forms.Label lblGrados;
        private System.Windows.Forms.Label lblVueltas;
        private System.Windows.Forms.Label lblMotorZ;
        private System.Windows.Forms.TextBox txtGradosZ;
        private System.Windows.Forms.TextBox txtVueltasZ;
        private System.Windows.Forms.Label lblMotorY;
        private System.Windows.Forms.TextBox txtGradosY;
        private System.Windows.Forms.TextBox txtVueltasY;
        private System.Windows.Forms.Label lblMotorX;
    }
}