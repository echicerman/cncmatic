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
            this.grpConfigMaterial = new System.Windows.Forms.GroupBox();
            this.txtGrados = new System.Windows.Forms.MaskedTextBox();
            this.lblGrados = new System.Windows.Forms.Label();
            this.txtVueltas = new System.Windows.Forms.MaskedTextBox();
            this.cmbMotor = new System.Windows.Forms.ComboBox();
            this.cmbMaterial = new System.Windows.Forms.ComboBox();
            this.lblVueltas = new System.Windows.Forms.Label();
            this.lblMotor = new System.Windows.Forms.Label();
            this.lblMaterial = new System.Windows.Forms.Label();
            this.lblConfig = new System.Windows.Forms.Label();
            this.cmbConfiguracion = new System.Windows.Forms.ComboBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombrePerfil = new System.Windows.Forms.TextBox();
            this.txtMaxX = new System.Windows.Forms.MaskedTextBox();
            this.lblMaxX = new System.Windows.Forms.Label();
            this.txtMaxY = new System.Windows.Forms.MaskedTextBox();
            this.lblMaxY = new System.Windows.Forms.Label();
            this.txtMaxZ = new System.Windows.Forms.MaskedTextBox();
            this.lblMaxZ = new System.Windows.Forms.Label();
            this.txtMaterialNombre = new System.Windows.Forms.TextBox();
            this.lblMaterialNombre = new System.Windows.Forms.Label();
            this.txtMotorNombre = new System.Windows.Forms.TextBox();
            this.lblMotorNombre = new System.Windows.Forms.Label();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnGrabaMotor = new System.Windows.Forms.Button();
            this.btnGrabaMaterial = new System.Windows.Forms.Button();
            this.btnAltaMotor = new System.Windows.Forms.Button();
            this.btnAltaMaterial = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpConfGral.SuspendLayout();
            this.grpTipoProgr.SuspendLayout();
            this.grpUnidadMedida.SuspendLayout();
            this.grpConfigMaterial.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConfGral
            // 
            this.grpConfGral.Controls.Add(this.txtMaxZ);
            this.grpConfGral.Controls.Add(this.lblMaxZ);
            this.grpConfGral.Controls.Add(this.txtMaxY);
            this.grpConfGral.Controls.Add(this.lblMaxY);
            this.grpConfGral.Controls.Add(this.txtMaxX);
            this.grpConfGral.Controls.Add(this.lblMaxX);
            this.grpConfGral.Controls.Add(this.btnRefresh);
            this.grpConfGral.Controls.Add(this.grpTipoProgr);
            this.grpConfGral.Controls.Add(this.grpUnidadMedida);
            this.grpConfGral.Controls.Add(this.lblPuerto);
            this.grpConfGral.Controls.Add(this.portComboBox);
            this.grpConfGral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpConfGral.Location = new System.Drawing.Point(9, 47);
            this.grpConfGral.Name = "grpConfGral";
            this.grpConfGral.Size = new System.Drawing.Size(453, 164);
            this.grpConfGral.TabIndex = 0;
            this.grpConfGral.TabStop = false;
            this.grpConfGral.Text = "Configuración General";
            // 
            // grpTipoProgr
            // 
            this.grpTipoProgr.Controls.Add(this.rbtRelativa);
            this.grpTipoProgr.Controls.Add(this.lblTipoProg);
            this.grpTipoProgr.Controls.Add(this.rbtAbsoluta);
            this.grpTipoProgr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTipoProgr.Location = new System.Drawing.Point(224, 19);
            this.grpTipoProgr.Name = "grpTipoProgr";
            this.grpTipoProgr.Size = new System.Drawing.Size(223, 70);
            this.grpTipoProgr.TabIndex = 22;
            this.grpTipoProgr.TabStop = false;
            // 
            // rbtRelativa
            // 
            this.rbtRelativa.AutoSize = true;
            this.rbtRelativa.Location = new System.Drawing.Point(131, 46);
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
            this.lblTipoProg.Location = new System.Drawing.Point(7, 31);
            this.lblTipoProg.Name = "lblTipoProg";
            this.lblTipoProg.Size = new System.Drawing.Size(114, 13);
            this.lblTipoProg.TabIndex = 18;
            this.lblTipoProg.Text = "Tipo de Programación:";
            // 
            // rbtAbsoluta
            // 
            this.rbtAbsoluta.AutoSize = true;
            this.rbtAbsoluta.Location = new System.Drawing.Point(131, 16);
            this.rbtAbsoluta.Name = "rbtAbsoluta";
            this.rbtAbsoluta.Size = new System.Drawing.Size(66, 17);
            this.rbtAbsoluta.TabIndex = 20;
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
            this.grpUnidadMedida.Location = new System.Drawing.Point(6, 19);
            this.grpUnidadMedida.Name = "grpUnidadMedida";
            this.grpUnidadMedida.Size = new System.Drawing.Size(212, 70);
            this.grpUnidadMedida.TabIndex = 21;
            this.grpUnidadMedida.TabStop = false;
            // 
            // rbtPULG
            // 
            this.rbtPULG.AutoSize = true;
            this.rbtPULG.Location = new System.Drawing.Point(103, 46);
            this.rbtPULG.Name = "rbtPULG";
            this.rbtPULG.Size = new System.Drawing.Size(97, 17);
            this.rbtPULG.TabIndex = 3;
            this.rbtPULG.TabStop = true;
            this.rbtPULG.Text = "pulgadas (pulg)";
            this.rbtPULG.UseVisualStyleBackColor = true;
            // 
            // lblUnidad
            // 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Location = new System.Drawing.Point(12, 31);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(82, 13);
            this.lblUnidad.TabIndex = 0;
            this.lblUnidad.Text = "Unidad Medida:";
            // 
            // rbtMM
            // 
            this.rbtMM.AutoSize = true;
            this.rbtMM.Location = new System.Drawing.Point(103, 16);
            this.rbtMM.Name = "rbtMM";
            this.rbtMM.Size = new System.Drawing.Size(95, 17);
            this.rbtMM.TabIndex = 2;
            this.rbtMM.TabStop = true;
            this.rbtMM.Text = "milimetros (mm)";
            this.rbtMM.UseVisualStyleBackColor = true;
            // 
            // lblPuerto
            // 
            this.lblPuerto.AutoSize = true;
            this.lblPuerto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPuerto.Location = new System.Drawing.Point(71, 137);
            this.lblPuerto.Name = "lblPuerto";
            this.lblPuerto.Size = new System.Drawing.Size(68, 13);
            this.lblPuerto.TabIndex = 17;
            this.lblPuerto.Text = "Puerto COM:";
            // 
            // portComboBox
            // 
            this.portComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(145, 134);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(154, 21);
            this.portComboBox.TabIndex = 16;
            // 
            // grpConfigMaterial
            // 
            this.grpConfigMaterial.Controls.Add(this.btnGrabaMotor);
            this.grpConfigMaterial.Controls.Add(this.btnGrabaMaterial);
            this.grpConfigMaterial.Controls.Add(this.txtMotorNombre);
            this.grpConfigMaterial.Controls.Add(this.lblMotorNombre);
            this.grpConfigMaterial.Controls.Add(this.txtMaterialNombre);
            this.grpConfigMaterial.Controls.Add(this.lblMaterialNombre);
            this.grpConfigMaterial.Controls.Add(this.btnAltaMotor);
            this.grpConfigMaterial.Controls.Add(this.btnAltaMaterial);
            this.grpConfigMaterial.Controls.Add(this.txtGrados);
            this.grpConfigMaterial.Controls.Add(this.lblGrados);
            this.grpConfigMaterial.Controls.Add(this.txtVueltas);
            this.grpConfigMaterial.Controls.Add(this.cmbMotor);
            this.grpConfigMaterial.Controls.Add(this.cmbMaterial);
            this.grpConfigMaterial.Controls.Add(this.lblVueltas);
            this.grpConfigMaterial.Controls.Add(this.lblMotor);
            this.grpConfigMaterial.Controls.Add(this.lblMaterial);
            this.grpConfigMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpConfigMaterial.Location = new System.Drawing.Point(9, 215);
            this.grpConfigMaterial.Name = "grpConfigMaterial";
            this.grpConfigMaterial.Size = new System.Drawing.Size(453, 101);
            this.grpConfigMaterial.TabIndex = 2;
            this.grpConfigMaterial.TabStop = false;
            this.grpConfigMaterial.Text = "Configuración por Material";
            // 
            // txtGrados
            // 
            this.txtGrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrados.Location = new System.Drawing.Point(344, 65);
            this.txtGrados.Mask = "9999999";
            this.txtGrados.Name = "txtGrados";
            this.txtGrados.Size = new System.Drawing.Size(79, 20);
            this.txtGrados.TabIndex = 8;
            // 
            // lblGrados
            // 
            this.lblGrados.AutoSize = true;
            this.lblGrados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrados.Location = new System.Drawing.Point(249, 68);
            this.lblGrados.Name = "lblGrados";
            this.lblGrados.Size = new System.Drawing.Size(88, 13);
            this.lblGrados.TabIndex = 7;
            this.lblGrados.Text = "Grados por paso:";
            // 
            // txtVueltas
            // 
            this.txtVueltas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVueltas.Location = new System.Drawing.Point(116, 62);
            this.txtVueltas.Mask = "9999999";
            this.txtVueltas.Name = "txtVueltas";
            this.txtVueltas.Size = new System.Drawing.Size(79, 20);
            this.txtVueltas.TabIndex = 6;
            // 
            // cmbMotor
            // 
            this.cmbMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMotor.FormattingEnabled = true;
            this.cmbMotor.Location = new System.Drawing.Point(278, 26);
            this.cmbMotor.Name = "cmbMotor";
            this.cmbMotor.Size = new System.Drawing.Size(121, 21);
            this.cmbMotor.TabIndex = 4;
            // 
            // cmbMaterial
            // 
            this.cmbMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMaterial.FormattingEnabled = true;
            this.cmbMaterial.Location = new System.Drawing.Point(65, 27);
            this.cmbMaterial.Name = "cmbMaterial";
            this.cmbMaterial.Size = new System.Drawing.Size(121, 21);
            this.cmbMaterial.TabIndex = 3;
            // 
            // lblVueltas
            // 
            this.lblVueltas.AutoSize = true;
            this.lblVueltas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVueltas.Location = new System.Drawing.Point(21, 65);
            this.lblVueltas.Name = "lblVueltas";
            this.lblVueltas.Size = new System.Drawing.Size(89, 13);
            this.lblVueltas.TabIndex = 2;
            this.lblVueltas.Text = "Vueltas por paso:";
            // 
            // lblMotor
            // 
            this.lblMotor.AutoSize = true;
            this.lblMotor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotor.Location = new System.Drawing.Point(241, 30);
            this.lblMotor.Name = "lblMotor";
            this.lblMotor.Size = new System.Drawing.Size(37, 13);
            this.lblMotor.TabIndex = 1;
            this.lblMotor.Text = "Motor:";
            // 
            // lblMaterial
            // 
            this.lblMaterial.AutoSize = true;
            this.lblMaterial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterial.Location = new System.Drawing.Point(12, 30);
            this.lblMaterial.Name = "lblMaterial";
            this.lblMaterial.Size = new System.Drawing.Size(47, 13);
            this.lblMaterial.TabIndex = 0;
            this.lblMaterial.Text = "Material:";
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
            this.cmbConfiguracion.Size = new System.Drawing.Size(227, 21);
            this.cmbConfiguracion.TabIndex = 4;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(42, 54);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(89, 13);
            this.lblNombre.TabIndex = 6;
            this.lblNombre.Text = "Nombre del perfil:";
            this.lblNombre.Visible = false;
            // 
            // txtNombrePerfil
            // 
            this.txtNombrePerfil.Location = new System.Drawing.Point(154, 51);
            this.txtNombrePerfil.Name = "txtNombrePerfil";
            this.txtNombrePerfil.Size = new System.Drawing.Size(227, 20);
            this.txtNombrePerfil.TabIndex = 7;
            this.txtNombrePerfil.Visible = false;
            // 
            // txtMaxX
            // 
            this.txtMaxX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxX.Location = new System.Drawing.Point(61, 103);
            this.txtMaxX.Mask = "9999999";
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(48, 20);
            this.txtMaxX.TabIndex = 25;
            // 
            // lblMaxX
            // 
            this.lblMaxX.AutoSize = true;
            this.lblMaxX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxX.Location = new System.Drawing.Point(21, 106);
            this.lblMaxX.Name = "lblMaxX";
            this.lblMaxX.Size = new System.Drawing.Size(37, 13);
            this.lblMaxX.TabIndex = 24;
            this.lblMaxX.Text = "MaxX:";
            // 
            // txtMaxY
            // 
            this.txtMaxY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxY.Location = new System.Drawing.Point(222, 103);
            this.txtMaxY.Mask = "9999999";
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(48, 20);
            this.txtMaxY.TabIndex = 27;
            // 
            // lblMaxY
            // 
            this.lblMaxY.AutoSize = true;
            this.lblMaxY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxY.Location = new System.Drawing.Point(182, 106);
            this.lblMaxY.Name = "lblMaxY";
            this.lblMaxY.Size = new System.Drawing.Size(37, 13);
            this.lblMaxY.TabIndex = 26;
            this.lblMaxY.Text = "MaxY:";
            // 
            // txtMaxZ
            // 
            this.txtMaxZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxZ.Location = new System.Drawing.Point(375, 103);
            this.txtMaxZ.Mask = "9999999";
            this.txtMaxZ.Name = "txtMaxZ";
            this.txtMaxZ.Size = new System.Drawing.Size(48, 20);
            this.txtMaxZ.TabIndex = 29;
            // 
            // lblMaxZ
            // 
            this.lblMaxZ.AutoSize = true;
            this.lblMaxZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaxZ.Location = new System.Drawing.Point(335, 106);
            this.lblMaxZ.Name = "lblMaxZ";
            this.lblMaxZ.Size = new System.Drawing.Size(37, 13);
            this.lblMaxZ.TabIndex = 28;
            this.lblMaxZ.Text = "MaxZ:";
            // 
            // txtMaterialNombre
            // 
            this.txtMaterialNombre.Location = new System.Drawing.Point(118, 52);
            this.txtMaterialNombre.Name = "txtMaterialNombre";
            this.txtMaterialNombre.Size = new System.Drawing.Size(97, 20);
            this.txtMaterialNombre.TabIndex = 12;
            this.txtMaterialNombre.Visible = false;
            // 
            // lblMaterialNombre
            // 
            this.lblMaterialNombre.AutoSize = true;
            this.lblMaterialNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaterialNombre.Location = new System.Drawing.Point(18, 55);
            this.lblMaterialNombre.Name = "lblMaterialNombre";
            this.lblMaterialNombre.Size = new System.Drawing.Size(103, 13);
            this.lblMaterialNombre.TabIndex = 11;
            this.lblMaterialNombre.Text = "Nombre del material:";
            this.lblMaterialNombre.Visible = false;
            // 
            // txtMotorNombre
            // 
            this.txtMotorNombre.Location = new System.Drawing.Point(335, 54);
            this.txtMotorNombre.Name = "txtMotorNombre";
            this.txtMotorNombre.Size = new System.Drawing.Size(97, 20);
            this.txtMotorNombre.TabIndex = 14;
            this.txtMotorNombre.Visible = false;
            // 
            // lblMotorNombre
            // 
            this.lblMotorNombre.AutoSize = true;
            this.lblMotorNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotorNombre.Location = new System.Drawing.Point(235, 57);
            this.lblMotorNombre.Name = "lblMotorNombre";
            this.lblMotorNombre.Size = new System.Drawing.Size(93, 13);
            this.lblMotorNombre.TabIndex = 13;
            this.lblMotorNombre.Text = "Nombre del motor:";
            this.lblMotorNombre.Visible = false;
            // 
            // btnGrabar
            // 
            this.btnGrabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGrabar.Image = global::CNCMatic.Properties.Resources.save1;
            this.btnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGrabar.Location = new System.Drawing.Point(190, 322);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(88, 40);
            this.btnGrabar.TabIndex = 1;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::CNCMatic.Properties.Resources.Cancel_icon;
            this.btnCancelar.Location = new System.Drawing.Point(422, 3);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(40, 40);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = global::CNCMatic.Properties.Resources.New_icon;
            this.btnNuevo.Location = new System.Drawing.Point(382, 3);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(40, 40);
            this.btnNuevo.TabIndex = 5;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGrabaMotor
            // 
            this.btnGrabaMotor.Enabled = false;
            this.btnGrabaMotor.Image = global::CNCMatic.Properties.Resources.Ok_icon2;
            this.btnGrabaMotor.Location = new System.Drawing.Point(422, 24);
            this.btnGrabaMotor.Name = "btnGrabaMotor";
            this.btnGrabaMotor.Size = new System.Drawing.Size(26, 26);
            this.btnGrabaMotor.TabIndex = 16;
            this.btnGrabaMotor.UseVisualStyleBackColor = true;
            this.btnGrabaMotor.Click += new System.EventHandler(this.btnGrabaMotor_Click);
            // 
            // btnGrabaMaterial
            // 
            this.btnGrabaMaterial.Enabled = false;
            this.btnGrabaMaterial.Image = global::CNCMatic.Properties.Resources.Ok_icon2;
            this.btnGrabaMaterial.Location = new System.Drawing.Point(209, 23);
            this.btnGrabaMaterial.Name = "btnGrabaMaterial";
            this.btnGrabaMaterial.Size = new System.Drawing.Size(26, 26);
            this.btnGrabaMaterial.TabIndex = 15;
            this.btnGrabaMaterial.UseVisualStyleBackColor = true;
            this.btnGrabaMaterial.Click += new System.EventHandler(this.btnGrabaMaterial_Click);
            // 
            // btnAltaMotor
            // 
            this.btnAltaMotor.Image = global::CNCMatic.Properties.Resources.New_icon2;
            this.btnAltaMotor.Location = new System.Drawing.Point(398, 24);
            this.btnAltaMotor.Name = "btnAltaMotor";
            this.btnAltaMotor.Size = new System.Drawing.Size(26, 26);
            this.btnAltaMotor.TabIndex = 10;
            this.btnAltaMotor.UseVisualStyleBackColor = true;
            this.btnAltaMotor.Click += new System.EventHandler(this.btnAltaMotor_Click);
            // 
            // btnAltaMaterial
            // 
            this.btnAltaMaterial.Image = global::CNCMatic.Properties.Resources.New_icon2;
            this.btnAltaMaterial.Location = new System.Drawing.Point(185, 23);
            this.btnAltaMaterial.Name = "btnAltaMaterial";
            this.btnAltaMaterial.Size = new System.Drawing.Size(26, 26);
            this.btnAltaMaterial.TabIndex = 9;
            this.btnAltaMaterial.UseVisualStyleBackColor = true;
            this.btnAltaMaterial.Click += new System.EventHandler(this.btnAltaMaterial_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::CNCMatic.Properties.Resources.Refresh_icon;
            this.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRefresh.Location = new System.Drawing.Point(297, 133);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(31, 23);
            this.btnRefresh.TabIndex = 23;
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FrmConfiguracion
            // 
            this.AcceptButton = this.btnGrabar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 377);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtNombrePerfil);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.cmbConfiguracion);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.grpConfigMaterial);
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
            this.grpTipoProgr.ResumeLayout(false);
            this.grpTipoProgr.PerformLayout();
            this.grpUnidadMedida.ResumeLayout(false);
            this.grpUnidadMedida.PerformLayout();
            this.grpConfigMaterial.ResumeLayout(false);
            this.grpConfigMaterial.PerformLayout();
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
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombrePerfil;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.MaskedTextBox txtMaxZ;
        private System.Windows.Forms.Label lblMaxZ;
        private System.Windows.Forms.MaskedTextBox txtMaxY;
        private System.Windows.Forms.Label lblMaxY;
        private System.Windows.Forms.MaskedTextBox txtMaxX;
        private System.Windows.Forms.Label lblMaxX;
        private System.Windows.Forms.Button btnAltaMaterial;
        private System.Windows.Forms.Button btnAltaMotor;
        private System.Windows.Forms.TextBox txtMaterialNombre;
        private System.Windows.Forms.Label lblMaterialNombre;
        private System.Windows.Forms.TextBox txtMotorNombre;
        private System.Windows.Forms.Label lblMotorNombre;
        private System.Windows.Forms.Button btnGrabaMotor;
        private System.Windows.Forms.Button btnGrabaMaterial;
    }
}