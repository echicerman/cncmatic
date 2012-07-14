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
            this.importaG = new System.Windows.Forms.OpenFileDialog();
            this.txtPreview = new System.Windows.Forms.TextBox();
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
            this.disconnectButton = new System.Windows.Forms.Button();
            this.sendButton = new System.Windows.Forms.Button();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.receivedTextBox = new System.Windows.Forms.TextBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dXFFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gCodeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnCirculo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEsfera = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.dToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLinea = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnArco = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.grpOperacion = new System.Windows.Forms.GroupBox();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnStop2 = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.prgBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMachName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOsVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlPrevisualizacion = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLineaManual = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.gbPosicionActual.SuspendLayout();
            this.gbMovZ.SuspendLayout();
            this.gbMovXY.SuspendLayout();
            this.menu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grpOperacion.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlPrevisualizacion.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            // txtPreview
            // 
            this.txtPreview.Location = new System.Drawing.Point(110, 40);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPreview.Size = new System.Drawing.Size(401, 324);
            this.txtPreview.TabIndex = 6;
            // 
            // gbPosicionActual
            // 
            this.gbPosicionActual.Controls.Add(this.lblPosZ);
            this.gbPosicionActual.Controls.Add(this.txtPosZ);
            this.gbPosicionActual.Controls.Add(this.lblPosY);
            this.gbPosicionActual.Controls.Add(this.txtPosY);
            this.gbPosicionActual.Controls.Add(this.lblPosX);
            this.gbPosicionActual.Controls.Add(this.txtPosX);
            this.gbPosicionActual.Location = new System.Drawing.Point(348, 128);
            this.gbPosicionActual.Name = "gbPosicionActual";
            this.gbPosicionActual.Size = new System.Drawing.Size(103, 161);
            this.gbPosicionActual.TabIndex = 3;
            this.gbPosicionActual.TabStop = false;
            this.gbPosicionActual.Text = "Posición Actual";
            // 
            // lblPosZ
            // 
            this.lblPosZ.AutoSize = true;
            this.lblPosZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosZ.Location = new System.Drawing.Point(6, 126);
            this.lblPosZ.Name = "lblPosZ";
            this.lblPosZ.Size = new System.Drawing.Size(15, 13);
            this.lblPosZ.TabIndex = 5;
            this.lblPosZ.Text = "Z";
            // 
            // txtPosZ
            // 
            this.txtPosZ.Location = new System.Drawing.Point(26, 123);
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(63, 20);
            this.txtPosZ.TabIndex = 4;
            this.txtPosZ.Text = "0";
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosY.Location = new System.Drawing.Point(6, 75);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(15, 13);
            this.lblPosY.TabIndex = 3;
            this.lblPosY.Text = "Y";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(26, 72);
            this.txtPosY.Name = "txtPosY";
            this.txtPosY.Size = new System.Drawing.Size(63, 20);
            this.txtPosY.TabIndex = 2;
            this.txtPosY.Text = "0";
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosX.Location = new System.Drawing.Point(6, 27);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(15, 13);
            this.lblPosX.TabIndex = 1;
            this.lblPosX.Text = "X";
            // 
            // txtPosX
            // 
            this.txtPosX.Location = new System.Drawing.Point(26, 24);
            this.txtPosX.Name = "txtPosX";
            this.txtPosX.Size = new System.Drawing.Size(63, 20);
            this.txtPosX.TabIndex = 0;
            this.txtPosX.Text = "0";
            // 
            // gbMovZ
            // 
            this.gbMovZ.Controls.Add(this.btnMovZ_Aba);
            this.gbMovZ.Controls.Add(this.btnMovZ_Arr);
            this.gbMovZ.Location = new System.Drawing.Point(274, 127);
            this.gbMovZ.Name = "gbMovZ";
            this.gbMovZ.Size = new System.Drawing.Size(68, 162);
            this.gbMovZ.TabIndex = 1;
            this.gbMovZ.TabStop = false;
            this.gbMovZ.Text = "Mov-Z";
            // 
            // btnMovZ_Aba
            // 
            this.btnMovZ_Aba.BackColor = System.Drawing.Color.White;
            this.btnMovZ_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovZ_Aba.Location = new System.Drawing.Point(5, 95);
            this.btnMovZ_Aba.Name = "btnMovZ_Aba";
            this.btnMovZ_Aba.Size = new System.Drawing.Size(57, 61);
            this.btnMovZ_Aba.TabIndex = 7;
            this.btnMovZ_Aba.UseVisualStyleBackColor = false;
            this.btnMovZ_Aba.Click += new System.EventHandler(this.btnMovZ_Aba_Click);
            // 
            // btnMovZ_Arr
            // 
            this.btnMovZ_Arr.BackColor = System.Drawing.Color.White;
            this.btnMovZ_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovZ_Arr.Location = new System.Drawing.Point(6, 19);
            this.btnMovZ_Arr.Name = "btnMovZ_Arr";
            this.btnMovZ_Arr.Size = new System.Drawing.Size(57, 61);
            this.btnMovZ_Arr.TabIndex = 6;
            this.btnMovZ_Arr.UseVisualStyleBackColor = false;
            this.btnMovZ_Arr.Click += new System.EventHandler(this.btnMovZ_Arr_Click);
            // 
            // gbMovXY
            // 
            this.gbMovXY.Controls.Add(this.btnMovXY_Der);
            this.gbMovXY.Controls.Add(this.btnMovXY_Izq);
            this.gbMovXY.Controls.Add(this.btnMovXY_Aba);
            this.gbMovXY.Controls.Add(this.btnMovXY_Arr);
            this.gbMovXY.Location = new System.Drawing.Point(68, 127);
            this.gbMovXY.Name = "gbMovXY";
            this.gbMovXY.Size = new System.Drawing.Size(200, 162);
            this.gbMovXY.TabIndex = 0;
            this.gbMovXY.TabStop = false;
            this.gbMovXY.Text = "Mov-XY";
            this.gbMovXY.Enter += new System.EventHandler(this.gbMovXY_Enter);
            // 
            // btnMovXY_Der
            // 
            this.btnMovXY_Der.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Der.Image = global::CNCMatic.Properties.Resources.flecha_DER;
            this.btnMovXY_Der.Location = new System.Drawing.Point(137, 56);
            this.btnMovXY_Der.Name = "btnMovXY_Der";
            this.btnMovXY_Der.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Der.TabIndex = 10;
            this.btnMovXY_Der.UseVisualStyleBackColor = false;
            this.btnMovXY_Der.Click += new System.EventHandler(this.btnMovXY_Der_Click);
            this.btnMovXY_Der.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Der_MouseUp);
            // 
            // btnMovXY_Izq
            // 
            this.btnMovXY_Izq.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Izq.Image = global::CNCMatic.Properties.Resources.flecha_IZQ;
            this.btnMovXY_Izq.Location = new System.Drawing.Point(6, 57);
            this.btnMovXY_Izq.Name = "btnMovXY_Izq";
            this.btnMovXY_Izq.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Izq.TabIndex = 9;
            this.btnMovXY_Izq.UseVisualStyleBackColor = false;
            this.btnMovXY_Izq.Click += new System.EventHandler(this.btnMovXY_Izq_Click);
            // 
            // btnMovXY_Aba
            // 
            this.btnMovXY_Aba.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovXY_Aba.Location = new System.Drawing.Point(72, 95);
            this.btnMovXY_Aba.Name = "btnMovXY_Aba";
            this.btnMovXY_Aba.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Aba.TabIndex = 8;
            this.btnMovXY_Aba.UseVisualStyleBackColor = false;
            this.btnMovXY_Aba.Click += new System.EventHandler(this.btnMovXY_Aba_Click);
            // 
            // btnMovXY_Arr
            // 
            this.btnMovXY_Arr.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovXY_Arr.Location = new System.Drawing.Point(72, 19);
            this.btnMovXY_Arr.Name = "btnMovXY_Arr";
            this.btnMovXY_Arr.Size = new System.Drawing.Size(57, 61);
            this.btnMovXY_Arr.TabIndex = 7;
            this.btnMovXY_Arr.UseVisualStyleBackColor = false;
            this.btnMovXY_Arr.Click += new System.EventHandler(this.btnMovXY_Arr_Click);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(277, 20);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(88, 23);
            this.disconnectButton.TabIndex = 20;
            this.disconnectButton.Text = "Desconectar";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(291, 49);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 19;
            this.sendButton.Text = "Enviar";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(9, 48);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(276, 20);
            this.sendTextBox.TabIndex = 18;
            // 
            // receivedTextBox
            // 
            this.receivedTextBox.Location = new System.Drawing.Point(9, 78);
            this.receivedTextBox.Multiline = true;
            this.receivedTextBox.Name = "receivedTextBox";
            this.receivedTextBox.Size = new System.Drawing.Size(357, 138);
            this.receivedTextBox.TabIndex = 17;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(196, 19);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 16;
            this.connectButton.Text = "Conectar";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Puerto";
            // 
            // portComboBox
            // 
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(50, 19);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(121, 21);
            this.portComboBox.TabIndex = 14;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.verToolStripMenuItem,
            this.configuracionToolStripMenuItem,
            this.ayudaToolStripMenuItem,
            this.acercaToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1162, 24);
            this.menu.TabIndex = 21;
            this.menu.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importaciónToolStripMenuItem,
            this.guardarComoToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 20);
            this.toolStripMenuItem1.Text = "Archivo";
            // 
            // importaciónToolStripMenuItem
            // 
            this.importaciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dXFFileToolStripMenuItem,
            this.gCodeFileToolStripMenuItem});
            this.importaciónToolStripMenuItem.Name = "importaciónToolStripMenuItem";
            this.importaciónToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.importaciónToolStripMenuItem.Text = "Importar...";
            // 
            // dXFFileToolStripMenuItem
            // 
            this.dXFFileToolStripMenuItem.Name = "dXFFileToolStripMenuItem";
            this.dXFFileToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.dXFFileToolStripMenuItem.Text = "DXF File";
            this.dXFFileToolStripMenuItem.Click += new System.EventHandler(this.dXFFileToolStripMenuItem_Click);
            // 
            // gCodeFileToolStripMenuItem
            // 
            this.gCodeFileToolStripMenuItem.Name = "gCodeFileToolStripMenuItem";
            this.gCodeFileToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.gCodeFileToolStripMenuItem.Text = "G Code File";
            this.gCodeFileToolStripMenuItem.Click += new System.EventHandler(this.gCodeFileToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.guardarComoToolStripMenuItem.Text = "Guardar como...";
            // 
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.verToolStripMenuItem.Text = "Ver";
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuracionToolStripMenuItem.Text = "Configuración";
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaToolStripMenuItem
            // 
            this.acercaToolStripMenuItem.Name = "acercaToolStripMenuItem";
            this.acercaToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.acercaToolStripMenuItem.Text = "Acerca";
            this.acercaToolStripMenuItem.Click += new System.EventHandler(this.acercaToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(64, 64);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripDropDownButton2,
            this.toolStripSeparator3,
            this.btnLinea,
            this.toolStripSeparator4,
            this.btnArco,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(78, 660);
            this.toolStrip1.TabIndex = 22;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(75, 15);
            this.toolStripLabel1.Text = "Dibujo";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(75, 6);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCirculo,
            this.btnEsfera});
            this.toolStripDropDownButton1.Image = global::CNCMatic.Properties.Resources.circle_icon;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(75, 68);
            this.toolStripDropDownButton1.Text = "Circulo/Esfera";
            // 
            // btnCirculo
            // 
            this.btnCirculo.Name = "btnCirculo";
            this.btnCirculo.Size = new System.Drawing.Size(152, 22);
            this.btnCirculo.Text = "2D-Circulo";
            this.btnCirculo.Click += new System.EventHandler(this.btnCirculo_Click);
            // 
            // btnEsfera
            // 
            this.btnEsfera.Name = "btnEsfera";
            this.btnEsfera.Size = new System.Drawing.Size(152, 22);
            this.btnEsfera.Text = "3D-Esfera";
            this.btnEsfera.Click += new System.EventHandler(this.btnEsfera_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(75, 6);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dToolStripMenuItem2,
            this.dToolStripMenuItem3});
            this.toolStripDropDownButton2.Image = global::CNCMatic.Properties.Resources.square_icon;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(75, 68);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            // 
            // dToolStripMenuItem2
            // 
            this.dToolStripMenuItem2.Name = "dToolStripMenuItem2";
            this.dToolStripMenuItem2.Size = new System.Drawing.Size(88, 22);
            this.dToolStripMenuItem2.Text = "2D";
            // 
            // dToolStripMenuItem3
            // 
            this.dToolStripMenuItem3.Name = "dToolStripMenuItem3";
            this.dToolStripMenuItem3.Size = new System.Drawing.Size(88, 22);
            this.dToolStripMenuItem3.Text = "3D";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(75, 6);
            // 
            // btnLinea
            // 
            this.btnLinea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLinea.Image = global::CNCMatic.Properties.Resources.draw_line_icon;
            this.btnLinea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLinea.Name = "btnLinea";
            this.btnLinea.Size = new System.Drawing.Size(75, 68);
            this.btnLinea.Text = "Linea";
            this.btnLinea.Click += new System.EventHandler(this.btnLinea_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(75, 6);
            // 
            // btnArco
            // 
            this.btnArco.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnArco.Image = global::CNCMatic.Properties.Resources.Actions_layer_visible_on_icon2;
            this.btnArco.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnArco.Name = "btnArco";
            this.btnArco.Size = new System.Drawing.Size(75, 68);
            this.btnArco.Text = "Arco";
            this.btnArco.Click += new System.EventHandler(this.btnArco_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(75, 6);
            // 
            // grpOperacion
            // 
            this.grpOperacion.Controls.Add(this.btnInicio);
            this.grpOperacion.Controls.Add(this.btnStop2);
            this.grpOperacion.Controls.Add(this.btnPlay);
            this.grpOperacion.Controls.Add(this.btnPause);
            this.grpOperacion.Controls.Add(this.gbMovXY);
            this.grpOperacion.Controls.Add(this.gbMovZ);
            this.grpOperacion.Controls.Add(this.gbPosicionActual);
            this.grpOperacion.Location = new System.Drawing.Point(628, 346);
            this.grpOperacion.Name = "grpOperacion";
            this.grpOperacion.Size = new System.Drawing.Size(519, 303);
            this.grpOperacion.TabIndex = 26;
            this.grpOperacion.TabStop = false;
            this.grpOperacion.Text = "Operación Manual";
            // 
            // btnInicio
            // 
            this.btnInicio.BackColor = System.Drawing.Color.White;
            this.btnInicio.Image = global::CNCMatic.Properties.Resources.HomeButton;
            this.btnInicio.Location = new System.Drawing.Point(103, 29);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(71, 76);
            this.btnInicio.TabIndex = 2;
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnStop2
            // 
            this.btnStop2.BackColor = System.Drawing.Color.White;
            this.btnStop2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStop2.Image = global::CNCMatic.Properties.Resources.Stop_Normal_Red_icon;
            this.btnStop2.Location = new System.Drawing.Point(180, 29);
            this.btnStop2.Name = "btnStop2";
            this.btnStop2.Size = new System.Drawing.Size(71, 76);
            this.btnStop2.TabIndex = 25;
            this.btnStop2.UseVisualStyleBackColor = false;
            this.btnStop2.Click += new System.EventHandler(this.btnStop2_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.White;
            this.btnPlay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlay.Image = global::CNCMatic.Properties.Resources.Play_1_Normal_icon__1_;
            this.btnPlay.Location = new System.Drawing.Point(332, 29);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(71, 76);
            this.btnPlay.TabIndex = 23;
            this.btnPlay.UseVisualStyleBackColor = false;
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.White;
            this.btnPause.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPause.Image = global::CNCMatic.Properties.Resources.Pause_Normal_Red_icon;
            this.btnPause.Location = new System.Drawing.Point(257, 29);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(71, 76);
            this.btnPause.TabIndex = 24;
            this.btnPause.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgBar,
            this.lblUserName,
            this.lblMachName,
            this.lblOsVersion});
            this.statusStrip1.Location = new System.Drawing.Point(78, 662);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1084, 22);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // prgBar
            // 
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(250, 16);
            // 
            // lblUserName
            // 
            this.lblUserName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblUserName.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(4, 17);
            // 
            // lblMachName
            // 
            this.lblMachName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblMachName.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.lblMachName.Name = "lblMachName";
            this.lblMachName.Size = new System.Drawing.Size(4, 17);
            // 
            // lblOsVersion
            // 
            this.lblOsVersion.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblOsVersion.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.lblOsVersion.Name = "lblOsVersion";
            this.lblOsVersion.Size = new System.Drawing.Size(4, 17);
            // 
            // pnlPrevisualizacion
            // 
            this.pnlPrevisualizacion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPrevisualizacion.Controls.Add(this.label2);
            this.pnlPrevisualizacion.Location = new System.Drawing.Point(628, 40);
            this.pnlPrevisualizacion.Name = "pnlPrevisualizacion";
            this.pnlPrevisualizacion.Size = new System.Drawing.Size(519, 300);
            this.pnlPrevisualizacion.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "<<previsualizador>>";
            // 
            // txtLineaManual
            // 
            this.txtLineaManual.Location = new System.Drawing.Point(110, 385);
            this.txtLineaManual.Name = "txtLineaManual";
            this.txtLineaManual.Size = new System.Drawing.Size(295, 20);
            this.txtLineaManual.TabIndex = 29;
            this.txtLineaManual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineaManual_KeyPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.portComboBox);
            this.groupBox1.Controls.Add(this.connectButton);
            this.groupBox1.Controls.Add(this.sendTextBox);
            this.groupBox1.Controls.Add(this.disconnectButton);
            this.groupBox1.Controls.Add(this.sendButton);
            this.groupBox1.Controls.Add(this.receivedTextBox);
            this.groupBox1.Location = new System.Drawing.Point(145, 428);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 221);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comunicacion";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::CNCMatic.Properties.Resources.brush_icon;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(411, 366);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(100, 56);
            this.btnLimpiar.TabIndex = 7;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1162, 684);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLineaManual);
            this.Controls.Add(this.pnlPrevisualizacion);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpOperacion);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.menu);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CNC Matic";
            this.gbPosicionActual.ResumeLayout(false);
            this.gbPosicionActual.PerformLayout();
            this.gbMovZ.ResumeLayout(false);
            this.gbMovXY.ResumeLayout(false);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpOperacion.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlPrevisualizacion.ResumeLayout(false);
            this.pnlPrevisualizacion.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog importaDXF;
        private System.Windows.Forms.OpenFileDialog importaG;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.GroupBox gbMovXY;
        private System.Windows.Forms.GroupBox gbMovZ;
        private System.Windows.Forms.GroupBox gbPosicionActual;
        private System.Windows.Forms.Label lblPosZ;
        private System.Windows.Forms.TextBox txtPosZ;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.TextBox txtPosY;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.TextBox txtPosX;
        private System.Windows.Forms.Button btnInicio;
        private RepeatButton btnMovZ_Arr;
        private RepeatButton btnMovZ_Aba;
        private RepeatButton btnMovXY_Der;
        private RepeatButton btnMovXY_Izq;
        private RepeatButton btnMovXY_Aba;
        private RepeatButton btnMovXY_Arr;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.TextBox receivedTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem verToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem acercaToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop2;
        private System.Windows.Forms.GroupBox grpOperacion;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem importaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dXFFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gCodeFileToolStripMenuItem;
        private System.Windows.Forms.Panel pnlPrevisualizacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLineaManual;
        private System.Windows.Forms.ToolStripProgressBar prgBar;
        private System.Windows.Forms.ToolStripStatusLabel lblUserName;
        private System.Windows.Forms.ToolStripStatusLabel lblMachName;
        private System.Windows.Forms.ToolStripStatusLabel lblOsVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem btnCirculo;
        private System.Windows.Forms.ToolStripMenuItem btnEsfera;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem3;
        private System.Windows.Forms.ToolStripButton btnLinea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnArco;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

