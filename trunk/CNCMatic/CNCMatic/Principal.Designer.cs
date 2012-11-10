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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.importaDXF = new System.Windows.Forms.OpenFileDialog();
            this.importaG = new System.Windows.Forms.OpenFileDialog();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.gbMovZ = new System.Windows.Forms.GroupBox();
            this.lblZa = new System.Windows.Forms.Label();
            this.lblZr = new System.Windows.Forms.Label();
            this.btnMovZ_Aba = new System.Windows.Forms.Button();
            this.btnMovZ_Arr = new System.Windows.Forms.Button();
            this.gbMovXY = new System.Windows.Forms.GroupBox();
            this.lblYr = new System.Windows.Forms.Label();
            this.lblYa = new System.Windows.Forms.Label();
            this.lblXr = new System.Windows.Forms.Label();
            this.lblXa = new System.Windows.Forms.Label();
            this.btnMovXY_Der = new System.Windows.Forms.Button();
            this.btnMovXY_Izq = new System.Windows.Forms.Button();
            this.btnMovXY_Aba = new System.Windows.Forms.Button();
            this.btnMovXY_Arr = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dXFFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gCodeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnCirculo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.dToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCubo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnLinea = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnArco = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.grpOperacion = new System.Windows.Forms.GroupBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnStop2 = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMachName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOsVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.prgBar = new SafeControls.SafeToolStripProgressBar();
            this.lblPosicionActual = new SafeControls.SafeToolStripStatusLabel();
            this.lblEstado = new SafeControls.SafeToolStripStatusLabel();
            this.txtLineaManual = new System.Windows.Forms.TextBox();
            this.grpPrev = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.BreakPointSlider = new System.Windows.Forms.TrackBar();
            this.MG_Viewer1 = new MacGen.MG_CS_BasicViewer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbDisplay = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuRapidLines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRapidPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAxisLines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAxisindicator = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPan = new System.Windows.Forms.ToolStripButton();
            this.tsbZoom = new System.Windows.Forms.ToolStripButton();
            this.tsbRotate = new System.Windows.Forms.ToolStripButton();
            this.tsbFence = new System.Windows.Forms.ToolStripButton();
            this.tsbFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSelect = new System.Windows.Forms.ToolStripButton();
            this.tsbView = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuTop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFront = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIsometric = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.Coordinates = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.lblStatus = new System.Windows.Forms.ToolStripLabel();
            this.grpPrevisualizacion = new System.Windows.Forms.GroupBox();
            this.tblScreens = new System.Windows.Forms.TableLayoutPanel();
            this.CodeTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.gbMovZ.SuspendLayout();
            this.gbMovXY.SuspendLayout();
            this.menu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grpOperacion.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpPrev.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BreakPointSlider)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
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
            this.importaG.DefaultExt = "*.*";
            this.importaG.FileName = "*.*";
            this.importaG.Filter = "gcode(*.gcode)|*.gcode|cnc(*.cnc)|*.cnc|Archivos de texto(*.txt)|*.txt|Todos(*.*)" +
                "|*.*";
            // 
            // txtPreview
            // 
            this.txtPreview.Location = new System.Drawing.Point(110, 40);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPreview.Size = new System.Drawing.Size(401, 324);
            this.txtPreview.TabIndex = 1;
            this.txtPreview.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPreview_KeyPress);
            // 
            // gbMovZ
            // 
            this.gbMovZ.Controls.Add(this.lblZa);
            this.gbMovZ.Controls.Add(this.lblZr);
            this.gbMovZ.Controls.Add(this.btnMovZ_Aba);
            this.gbMovZ.Controls.Add(this.btnMovZ_Arr);
            this.gbMovZ.Enabled = false;
            this.gbMovZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMovZ.Location = new System.Drawing.Point(234, 102);
            this.gbMovZ.Name = "gbMovZ";
            this.gbMovZ.Size = new System.Drawing.Size(82, 129);
            this.gbMovZ.TabIndex = 5;
            this.gbMovZ.TabStop = false;
            this.gbMovZ.Text = "Mov-Z";
            // 
            // lblZa
            // 
            this.lblZa.AutoSize = true;
            this.lblZa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZa.ForeColor = System.Drawing.Color.Green;
            this.lblZa.Location = new System.Drawing.Point(27, 12);
            this.lblZa.Name = "lblZa";
            this.lblZa.Size = new System.Drawing.Size(22, 13);
            this.lblZa.TabIndex = 9;
            this.lblZa.Text = "+Z";
            // 
            // lblZr
            // 
            this.lblZr.AutoSize = true;
            this.lblZr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblZr.ForeColor = System.Drawing.Color.Red;
            this.lblZr.Location = new System.Drawing.Point(32, 112);
            this.lblZr.Name = "lblZr";
            this.lblZr.Size = new System.Drawing.Size(19, 13);
            this.lblZr.TabIndex = 8;
            this.lblZr.Text = "-Z";
            // 
            // btnMovZ_Aba
            // 
            this.btnMovZ_Aba.BackColor = System.Drawing.Color.White;
            this.btnMovZ_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovZ_Aba.Location = new System.Drawing.Point(17, 68);
            this.btnMovZ_Aba.Name = "btnMovZ_Aba";
            this.btnMovZ_Aba.Size = new System.Drawing.Size(49, 45);
            this.btnMovZ_Aba.TabIndex = 1;
            this.btnMovZ_Aba.UseVisualStyleBackColor = false;
            this.btnMovZ_Aba.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMovZ_Aba_MouseDown);
            this.btnMovZ_Aba.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovZ_Aba_MouseUp);
            // 
            // btnMovZ_Arr
            // 
            this.btnMovZ_Arr.BackColor = System.Drawing.Color.White;
            this.btnMovZ_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovZ_Arr.Location = new System.Drawing.Point(17, 25);
            this.btnMovZ_Arr.Name = "btnMovZ_Arr";
            this.btnMovZ_Arr.Size = new System.Drawing.Size(48, 43);
            this.btnMovZ_Arr.TabIndex = 0;
            this.btnMovZ_Arr.UseVisualStyleBackColor = false;
            this.btnMovZ_Arr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMovZ_Arr_MouseDown);
            this.btnMovZ_Arr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovZ_Arr_MouseUp);
            // 
            // gbMovXY
            // 
            this.gbMovXY.Controls.Add(this.lblYr);
            this.gbMovXY.Controls.Add(this.lblYa);
            this.gbMovXY.Controls.Add(this.lblXr);
            this.gbMovXY.Controls.Add(this.lblXa);
            this.gbMovXY.Controls.Add(this.btnMovXY_Der);
            this.gbMovXY.Controls.Add(this.btnMovXY_Izq);
            this.gbMovXY.Controls.Add(this.btnMovXY_Aba);
            this.gbMovXY.Controls.Add(this.btnMovXY_Arr);
            this.gbMovXY.Enabled = false;
            this.gbMovXY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbMovXY.Location = new System.Drawing.Point(26, 102);
            this.gbMovXY.Name = "gbMovXY";
            this.gbMovXY.Size = new System.Drawing.Size(202, 128);
            this.gbMovXY.TabIndex = 4;
            this.gbMovXY.TabStop = false;
            this.gbMovXY.Text = "Mov-XY";
            // 
            // lblYr
            // 
            this.lblYr.AutoSize = true;
            this.lblYr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYr.ForeColor = System.Drawing.Color.Red;
            this.lblYr.Location = new System.Drawing.Point(91, 112);
            this.lblYr.Name = "lblYr";
            this.lblYr.Size = new System.Drawing.Size(19, 13);
            this.lblYr.TabIndex = 7;
            this.lblYr.Text = "-Y";
            // 
            // lblYa
            // 
            this.lblYa.AutoSize = true;
            this.lblYa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYa.ForeColor = System.Drawing.Color.Green;
            this.lblYa.Location = new System.Drawing.Point(91, 10);
            this.lblYa.Name = "lblYa";
            this.lblYa.Size = new System.Drawing.Size(22, 13);
            this.lblYa.TabIndex = 6;
            this.lblYa.Text = "+Y";
            // 
            // lblXr
            // 
            this.lblXr.AutoSize = true;
            this.lblXr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXr.ForeColor = System.Drawing.Color.Red;
            this.lblXr.Location = new System.Drawing.Point(6, 59);
            this.lblXr.Name = "lblXr";
            this.lblXr.Size = new System.Drawing.Size(19, 13);
            this.lblXr.TabIndex = 5;
            this.lblXr.Text = "-X";
            // 
            // lblXa
            // 
            this.lblXa.AutoSize = true;
            this.lblXa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXa.ForeColor = System.Drawing.Color.Green;
            this.lblXa.Location = new System.Drawing.Point(176, 59);
            this.lblXa.Name = "lblXa";
            this.lblXa.Size = new System.Drawing.Size(22, 13);
            this.lblXa.TabIndex = 4;
            this.lblXa.Text = "+X";
            // 
            // btnMovXY_Der
            // 
            this.btnMovXY_Der.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Der.Image = global::CNCMatic.Properties.Resources.flecha_DER;
            this.btnMovXY_Der.Location = new System.Drawing.Point(130, 43);
            this.btnMovXY_Der.Name = "btnMovXY_Der";
            this.btnMovXY_Der.Size = new System.Drawing.Size(46, 50);
            this.btnMovXY_Der.TabIndex = 2;
            this.btnMovXY_Der.UseVisualStyleBackColor = false;
            this.btnMovXY_Der.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Der_MouseDown);
            this.btnMovXY_Der.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Der_MouseUp);
            // 
            // btnMovXY_Izq
            // 
            this.btnMovXY_Izq.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Izq.Image = global::CNCMatic.Properties.Resources.flecha_IZQ;
            this.btnMovXY_Izq.Location = new System.Drawing.Point(31, 43);
            this.btnMovXY_Izq.Name = "btnMovXY_Izq";
            this.btnMovXY_Izq.Size = new System.Drawing.Size(44, 50);
            this.btnMovXY_Izq.TabIndex = 0;
            this.btnMovXY_Izq.UseVisualStyleBackColor = false;
            this.btnMovXY_Izq.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Izq_MouseDown);
            this.btnMovXY_Izq.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Izq_MouseUp);
            // 
            // btnMovXY_Aba
            // 
            this.btnMovXY_Aba.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovXY_Aba.Location = new System.Drawing.Point(81, 67);
            this.btnMovXY_Aba.Name = "btnMovXY_Aba";
            this.btnMovXY_Aba.Size = new System.Drawing.Size(44, 45);
            this.btnMovXY_Aba.TabIndex = 3;
            this.btnMovXY_Aba.UseVisualStyleBackColor = false;
            this.btnMovXY_Aba.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Aba_MouseDown);
            this.btnMovXY_Aba.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Aba_MouseUp);
            // 
            // btnMovXY_Arr
            // 
            this.btnMovXY_Arr.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovXY_Arr.Location = new System.Drawing.Point(81, 22);
            this.btnMovXY_Arr.Name = "btnMovXY_Arr";
            this.btnMovXY_Arr.Size = new System.Drawing.Size(44, 46);
            this.btnMovXY_Arr.TabIndex = 1;
            this.btnMovXY_Arr.UseVisualStyleBackColor = false;
            this.btnMovXY_Arr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Arr_MouseDown);
            this.btnMovXY_Arr.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Arr_MouseUp);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.configuracionToolStripMenuItem,
            this.ayudaToolStripMenuItem,
            this.salirMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1149, 24);
            this.menu.TabIndex = 0;
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
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuracionToolStripMenuItem.Text = "Configuración";
            this.configuracionToolStripMenuItem.Click += new System.EventHandler(this.configuracionToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca De";
            this.acercaDeToolStripMenuItem.Click += new System.EventHandler(this.acercaDeToolStripMenuItem_Click);
            // 
            // salirMenuItem
            // 
            this.salirMenuItem.Name = "salirMenuItem";
            this.salirMenuItem.Size = new System.Drawing.Size(41, 20);
            this.salirMenuItem.Text = "Salir";
            this.salirMenuItem.Click += new System.EventHandler(this.salirMenuItem_Click);
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
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(78, 665);
            this.toolStrip1.TabIndex = 5;
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
            this.btnCirculo});
            this.toolStripDropDownButton1.Image = global::CNCMatic.Properties.Resources.circle_icon;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(75, 68);
            this.toolStripDropDownButton1.Text = "Circulo/Esfera";
            // 
            // btnCirculo
            // 
            this.btnCirculo.Name = "btnCirculo";
            this.btnCirculo.Size = new System.Drawing.Size(137, 22);
            this.btnCirculo.Text = "2D - Circulo";
            this.btnCirculo.Click += new System.EventHandler(this.btnCirculo_Click);
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
            this.menuItemCubo});
            this.toolStripDropDownButton2.Image = global::CNCMatic.Properties.Resources.square_icon;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(75, 68);
            this.toolStripDropDownButton2.Text = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.ToolTipText = "Cuadrado/Cubo";
            // 
            // dToolStripMenuItem2
            // 
            this.dToolStripMenuItem2.Name = "dToolStripMenuItem2";
            this.dToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
            this.dToolStripMenuItem2.Text = "2D - Cuadrado";
            this.dToolStripMenuItem2.Click += new System.EventHandler(this.dToolStripMenuItem2_Click);
            // 
            // menuItemCubo
            // 
            this.menuItemCubo.Name = "menuItemCubo";
            this.menuItemCubo.Size = new System.Drawing.Size(151, 22);
            this.menuItemCubo.Text = "3D - Cubo";
            this.menuItemCubo.Click += new System.EventHandler(this.menuItemCubo_Click);
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
            this.grpOperacion.Controls.Add(this.btnRestart);
            this.grpOperacion.Controls.Add(this.btnConnect);
            this.grpOperacion.Controls.Add(this.btnInicio);
            this.grpOperacion.Controls.Add(this.btnStop2);
            this.grpOperacion.Controls.Add(this.btnPlay);
            this.grpOperacion.Controls.Add(this.btnPause);
            this.grpOperacion.Controls.Add(this.gbMovXY);
            this.grpOperacion.Controls.Add(this.gbMovZ);
            this.grpOperacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOperacion.Location = new System.Drawing.Point(110, 428);
            this.grpOperacion.Name = "grpOperacion";
            this.grpOperacion.Size = new System.Drawing.Size(396, 237);
            this.grpOperacion.TabIndex = 4;
            this.grpOperacion.TabStop = false;
            this.grpOperacion.Text = "Operación Manual";
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.Color.White;
            this.btnRestart.Enabled = false;
            this.btnRestart.Image = global::CNCMatic.Properties.Resources.Reload_icon;
            this.btnRestart.Location = new System.Drawing.Point(331, 145);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(45, 44);
            this.btnRestart.TabIndex = 6;
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.White;
            this.btnConnect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnConnect.Image = global::CNCMatic.Properties.Resources.Captura21;
            this.btnConnect.Location = new System.Drawing.Point(315, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(71, 76);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.BackColor = System.Drawing.Color.White;
            this.btnInicio.Enabled = false;
            this.btnInicio.Image = global::CNCMatic.Properties.Resources.HomeButton;
            this.btnInicio.Location = new System.Drawing.Point(10, 20);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(71, 76);
            this.btnInicio.TabIndex = 0;
            this.btnInicio.UseVisualStyleBackColor = false;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // btnStop2
            // 
            this.btnStop2.BackColor = System.Drawing.Color.White;
            this.btnStop2.Enabled = false;
            this.btnStop2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnStop2.Image = global::CNCMatic.Properties.Resources.Stop_Normal_Red_icon;
            this.btnStop2.Location = new System.Drawing.Point(111, 20);
            this.btnStop2.Name = "btnStop2";
            this.btnStop2.Size = new System.Drawing.Size(71, 76);
            this.btnStop2.TabIndex = 1;
            this.btnStop2.UseVisualStyleBackColor = false;
            this.btnStop2.Click += new System.EventHandler(this.btnStop2_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.BackColor = System.Drawing.Color.White;
            this.btnPlay.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPlay.Image = global::CNCMatic.Properties.Resources.Play_1_Normal_icon__1_;
            this.btnPlay.Location = new System.Drawing.Point(315, 20);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(71, 76);
            this.btnPlay.TabIndex = 23;
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackColor = System.Drawing.Color.White;
            this.btnPause.Enabled = false;
            this.btnPause.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPause.Image = global::CNCMatic.Properties.Resources.Pause_Normal_Red_icon;
            this.btnPause.Location = new System.Drawing.Point(214, 20);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(71, 76);
            this.btnPause.TabIndex = 2;
            this.btnPause.UseVisualStyleBackColor = false;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUserName,
            this.lblMachName,
            this.lblOsVersion,
            this.prgBar,
            this.lblPosicionActual,
            this.lblEstado});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1149, 22);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblUserName
            // 
            this.lblUserName.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 17);
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
            this.lblOsVersion.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.lblOsVersion.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.lblOsVersion.Name = "lblOsVersion";
            this.lblOsVersion.Size = new System.Drawing.Size(4, 17);
            // 
            // prgBar
            // 
            this.prgBar.MarqueeAnimationSpeed = 90;
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(250, 16);
            this.prgBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lblPosicionActual
            // 
            this.lblPosicionActual.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblPosicionActual.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.lblPosicionActual.Name = "lblPosicionActual";
            this.lblPosicionActual.Size = new System.Drawing.Size(4, 17);
            // 
            // lblEstado
            // 
            this.lblEstado.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.lblEstado.BorderStyle = System.Windows.Forms.Border3DStyle.Bump;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(4, 17);
            this.lblEstado.TextChanged += new System.EventHandler(this.lblEstado_TextChanged);
            // 
            // txtLineaManual
            // 
            this.txtLineaManual.Location = new System.Drawing.Point(110, 385);
            this.txtLineaManual.Name = "txtLineaManual";
            this.txtLineaManual.Size = new System.Drawing.Size(316, 20);
            this.txtLineaManual.TabIndex = 2;
            this.txtLineaManual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineaManual_KeyPress);
            // 
            // grpPrev
            // 
            this.grpPrev.Controls.Add(this.groupBox2);
            this.grpPrev.Controls.Add(this.toolStrip2);
            this.grpPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPrev.Location = new System.Drawing.Point(529, 28);
            this.grpPrev.Name = "grpPrev";
            this.grpPrev.Size = new System.Drawing.Size(618, 637);
            this.grpPrev.TabIndex = 6;
            this.grpPrev.TabStop = false;
            this.grpPrev.Text = "Previsualización";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(0, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(612, 587);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.BreakPointSlider, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.MG_Viewer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(606, 568);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // BreakPointSlider
            // 
            this.BreakPointSlider.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BreakPointSlider.Location = new System.Drawing.Point(1, 547);
            this.BreakPointSlider.Margin = new System.Windows.Forms.Padding(0);
            this.BreakPointSlider.Name = "BreakPointSlider";
            this.BreakPointSlider.Size = new System.Drawing.Size(604, 20);
            this.BreakPointSlider.TabIndex = 0;
            this.BreakPointSlider.TickStyle = System.Windows.Forms.TickStyle.None;
            this.BreakPointSlider.ValueChanged += new System.EventHandler(this.BreakPointSlider_ValueChanged);
            // 
            // MG_Viewer1
            // 
            this.MG_Viewer1.AxisIndicatorScale = 0.75F;
            this.MG_Viewer1.BackColor = System.Drawing.Color.Black;
            this.MG_Viewer1.BreakPoint = -1;
            this.MG_Viewer1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MG_Viewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MG_Viewer1.DynamicViewManipulation = true;
            this.MG_Viewer1.FourthAxis = 0F;
            this.MG_Viewer1.Location = new System.Drawing.Point(1, 1);
            this.MG_Viewer1.Margin = new System.Windows.Forms.Padding(0);
            this.MG_Viewer1.Name = "MG_Viewer1";
            this.MG_Viewer1.Pitch = 0F;
            this.MG_Viewer1.Roll = 0F;
            this.MG_Viewer1.RotaryType = MacGen.RotaryMotionType.BMC;
            this.MG_Viewer1.Size = new System.Drawing.Size(604, 545);
            this.MG_Viewer1.TabIndex = 0;
            this.MG_Viewer1.ViewManipMode = MacGen.MG_CS_BasicViewer.ManipMode.SELECTION;
            this.MG_Viewer1.Yaw = 0F;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbDisplay,
            this.toolStripSeparator6,
            this.tsbPan,
            this.tsbZoom,
            this.tsbRotate,
            this.tsbFence,
            this.tsbFit,
            this.toolStripSeparator7,
            this.tsbSelect,
            this.tsbView,
            this.toolStripSeparator8,
            this.Coordinates,
            this.toolStripSeparator9,
            this.lblStatus});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(612, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbDisplay
            // 
            this.tsbDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRapidLines,
            this.mnuRapidPoints,
            this.mnuAxisLines,
            this.mnuAxisindicator});
            this.tsbDisplay.Image = global::CNCMatic.Properties.Resources.EditInformation;
            this.tsbDisplay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDisplay.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbDisplay.Name = "tsbDisplay";
            this.tsbDisplay.Size = new System.Drawing.Size(29, 22);
            this.tsbDisplay.Text = "Ver...";
            // 
            // mnuRapidLines
            // 
            this.mnuRapidLines.Checked = true;
            this.mnuRapidLines.CheckOnClick = true;
            this.mnuRapidLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuRapidLines.Name = "mnuRapidLines";
            this.mnuRapidLines.Size = new System.Drawing.Size(194, 22);
            this.mnuRapidLines.Text = "Ver Lineas Rapidas";
            this.mnuRapidLines.CheckedChanged += new System.EventHandler(this.DisplayCheckChanged);
            // 
            // mnuRapidPoints
            // 
            this.mnuRapidPoints.Checked = true;
            this.mnuRapidPoints.CheckOnClick = true;
            this.mnuRapidPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuRapidPoints.Name = "mnuRapidPoints";
            this.mnuRapidPoints.Size = new System.Drawing.Size(194, 22);
            this.mnuRapidPoints.Text = "Ver Puntos Rapidos";
            this.mnuRapidPoints.CheckedChanged += new System.EventHandler(this.DisplayCheckChanged);
            // 
            // mnuAxisLines
            // 
            this.mnuAxisLines.Checked = true;
            this.mnuAxisLines.CheckOnClick = true;
            this.mnuAxisLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuAxisLines.Name = "mnuAxisLines";
            this.mnuAxisLines.Size = new System.Drawing.Size(194, 22);
            this.mnuAxisLines.Text = "Ver Extensiones de Ejes";
            this.mnuAxisLines.CheckedChanged += new System.EventHandler(this.DisplayCheckChanged);
            // 
            // mnuAxisindicator
            // 
            this.mnuAxisindicator.Checked = true;
            this.mnuAxisindicator.CheckOnClick = true;
            this.mnuAxisindicator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuAxisindicator.Name = "mnuAxisindicator";
            this.mnuAxisindicator.Size = new System.Drawing.Size(194, 22);
            this.mnuAxisindicator.Text = "Ver Ejes";
            this.mnuAxisindicator.CheckedChanged += new System.EventHandler(this.DisplayCheckChanged);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPan
            // 
            this.tsbPan.CheckOnClick = true;
            this.tsbPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPan.Image = global::CNCMatic.Properties.Resources.viewpan;
            this.tsbPan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPan.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbPan.Name = "tsbPan";
            this.tsbPan.Size = new System.Drawing.Size(23, 22);
            this.tsbPan.Tag = "Pan";
            this.tsbPan.Text = "Mover";
            this.tsbPan.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbZoom
            // 
            this.tsbZoom.CheckOnClick = true;
            this.tsbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoom.Image = global::CNCMatic.Properties.Resources.viewzoom;
            this.tsbZoom.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbZoom.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbZoom.Name = "tsbZoom";
            this.tsbZoom.Size = new System.Drawing.Size(23, 22);
            this.tsbZoom.Tag = "Zoom";
            this.tsbZoom.Text = "Zoom";
            this.tsbZoom.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbRotate
            // 
            this.tsbRotate.CheckOnClick = true;
            this.tsbRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRotate.Image = global::CNCMatic.Properties.Resources.viewrotate;
            this.tsbRotate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRotate.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbRotate.Name = "tsbRotate";
            this.tsbRotate.Size = new System.Drawing.Size(23, 22);
            this.tsbRotate.Tag = "Rotate";
            this.tsbRotate.Text = "Rotar";
            this.tsbRotate.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbFence
            // 
            this.tsbFence.CheckOnClick = true;
            this.tsbFence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFence.Image = global::CNCMatic.Properties.Resources.ViewFence;
            this.tsbFence.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFence.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbFence.Name = "tsbFence";
            this.tsbFence.Size = new System.Drawing.Size(23, 22);
            this.tsbFence.Tag = "Fence";
            this.tsbFence.Text = "Enfocar";
            this.tsbFence.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbFit
            // 
            this.tsbFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFit.Image = global::CNCMatic.Properties.Resources.ViewFit;
            this.tsbFit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFit.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbFit.Name = "tsbFit";
            this.tsbFit.Size = new System.Drawing.Size(23, 22);
            this.tsbFit.Tag = "Fit";
            this.tsbFit.Text = "Ajustar";
            this.tsbFit.ToolTipText = "View Fit [Shift + Click All Views]";
            this.tsbFit.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbSelect
            // 
            this.tsbSelect.Checked = true;
            this.tsbSelect.CheckOnClick = true;
            this.tsbSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelect.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelect.Image")));
            this.tsbSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Size = new System.Drawing.Size(23, 22);
            this.tsbSelect.Tag = "Select";
            this.tsbSelect.Text = "Seleccionar";
            this.tsbSelect.Click += new System.EventHandler(this.ViewButtonClicked);
            // 
            // tsbView
            // 
            this.tsbView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTop,
            this.mnuFront,
            this.mnuRight,
            this.mnuIsometric});
            this.tsbView.Image = ((System.Drawing.Image)(resources.GetObject("tsbView.Image")));
            this.tsbView.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.tsbView.Name = "tsbView";
            this.tsbView.Size = new System.Drawing.Size(29, 22);
            this.tsbView.Text = "&View";
            // 
            // mnuTop
            // 
            this.mnuTop.Image = ((System.Drawing.Image)(resources.GetObject("mnuTop.Image")));
            this.mnuTop.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuTop.Name = "mnuTop";
            this.mnuTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuTop.Size = new System.Drawing.Size(166, 22);
            this.mnuTop.Tag = "Superior";
            this.mnuTop.Text = "&Superior";
            this.mnuTop.Click += new System.EventHandler(this.mnuViewOrient_Click);
            // 
            // mnuFront
            // 
            this.mnuFront.Image = ((System.Drawing.Image)(resources.GetObject("mnuFront.Image")));
            this.mnuFront.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuFront.Name = "mnuFront";
            this.mnuFront.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuFront.Size = new System.Drawing.Size(166, 22);
            this.mnuFront.Tag = "Frontal";
            this.mnuFront.Text = "&Frontal";
            this.mnuFront.Click += new System.EventHandler(this.mnuViewOrient_Click);
            // 
            // mnuRight
            // 
            this.mnuRight.Image = ((System.Drawing.Image)(resources.GetObject("mnuRight.Image")));
            this.mnuRight.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuRight.Name = "mnuRight";
            this.mnuRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.mnuRight.Size = new System.Drawing.Size(166, 22);
            this.mnuRight.Tag = "Lateral";
            this.mnuRight.Text = "&Lateral";
            this.mnuRight.Click += new System.EventHandler(this.mnuViewOrient_Click);
            // 
            // mnuIsometric
            // 
            this.mnuIsometric.Image = ((System.Drawing.Image)(resources.GetObject("mnuIsometric.Image")));
            this.mnuIsometric.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuIsometric.Name = "mnuIsometric";
            this.mnuIsometric.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuIsometric.Size = new System.Drawing.Size(166, 22);
            this.mnuIsometric.Tag = "Isometrica";
            this.mnuIsometric.Text = "&Isometrica";
            this.mnuIsometric.Click += new System.EventHandler(this.mnuViewOrient_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // Coordinates
            // 
            this.Coordinates.Name = "Coordinates";
            this.Coordinates.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 22);
            // 
            // grpPrevisualizacion
            // 
            this.grpPrevisualizacion.Location = new System.Drawing.Point(0, 0);
            this.grpPrevisualizacion.Name = "grpPrevisualizacion";
            this.grpPrevisualizacion.Size = new System.Drawing.Size(200, 100);
            this.grpPrevisualizacion.TabIndex = 0;
            this.grpPrevisualizacion.TabStop = false;
            // 
            // tblScreens
            // 
            this.tblScreens.BackColor = System.Drawing.Color.Black;
            this.tblScreens.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tblScreens.ColumnCount = 1;
            this.tblScreens.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblScreens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblScreens.Location = new System.Drawing.Point(0, 0);
            this.tblScreens.Margin = new System.Windows.Forms.Padding(0);
            this.tblScreens.Name = "tblScreens";
            this.tblScreens.RowCount = 1;
            this.tblScreens.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblScreens.Size = new System.Drawing.Size(200, 100);
            this.tblScreens.TabIndex = 0;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::CNCMatic.Properties.Resources.brush_icon__1_;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(432, 375);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(79, 39);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1149, 0);
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 666);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1149, 23);
            this.toolStripContainer1.TabIndex = 32;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1149, 689);
            this.Controls.Add(this.toolStripContainer1);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.grpPrev);
            this.Controls.Add(this.txtLineaManual);
            this.Controls.Add(this.grpOperacion);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1165, 727);
            this.MinimumSize = new System.Drawing.Size(1165, 726);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CNC Matic";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Principal_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Principal_ResizeEnd);
            this.gbMovZ.ResumeLayout(false);
            this.gbMovZ.PerformLayout();
            this.gbMovXY.ResumeLayout(false);
            this.gbMovXY.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grpOperacion.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpPrev.ResumeLayout(false);
            this.grpPrev.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BreakPointSlider)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog importaDXF;
        private System.Windows.Forms.OpenFileDialog importaG;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.GroupBox gbMovXY;
        private System.Windows.Forms.GroupBox gbMovZ;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Button btnMovZ_Arr;
        private System.Windows.Forms.Button btnMovZ_Aba;
        private System.Windows.Forms.Button btnMovXY_Der;
        private System.Windows.Forms.Button btnMovXY_Izq;
        private System.Windows.Forms.Button btnMovXY_Aba;
        private System.Windows.Forms.Button btnMovXY_Arr;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop2;
        private System.Windows.Forms.GroupBox grpOperacion;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem importaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dXFFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gCodeFileToolStripMenuItem;
        private System.Windows.Forms.TextBox txtLineaManual;
        private SafeControls.SafeToolStripProgressBar prgBar;
        private System.Windows.Forms.ToolStripStatusLabel lblUserName;
        private System.Windows.Forms.ToolStripStatusLabel lblMachName;
        private System.Windows.Forms.ToolStripStatusLabel lblOsVersion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem btnCirculo;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem dToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem menuItemCubo;
        private System.Windows.Forms.ToolStripButton btnLinea;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnArco;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpPrev;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripDropDownButton tsbDisplay;
        internal System.Windows.Forms.ToolStripMenuItem mnuRapidLines;
        internal System.Windows.Forms.ToolStripMenuItem mnuRapidPoints;
        internal System.Windows.Forms.ToolStripMenuItem mnuAxisLines;
        internal System.Windows.Forms.ToolStripMenuItem mnuAxisindicator;
        internal System.Windows.Forms.ToolStripButton tsbPan;
        internal System.Windows.Forms.ToolStripButton tsbZoom;
        internal System.Windows.Forms.ToolStripButton tsbRotate;
        internal System.Windows.Forms.ToolStripButton tsbFence;
        internal System.Windows.Forms.ToolStripButton tsbFit;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripButton tsbSelect;
        private System.Windows.Forms.GroupBox grpPrevisualizacion;
        private System.Windows.Forms.TableLayoutPanel tblScreens;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal MacGen.MG_CS_BasicViewer MG_Viewer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripLabel Coordinates;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripLabel lblStatus;
        private System.Windows.Forms.ToolTip CodeTip;
        internal System.Windows.Forms.TrackBar BreakPointSlider;
        internal System.Windows.Forms.ToolStripDropDownButton tsbView;
        internal System.Windows.Forms.ToolStripMenuItem mnuTop;
        internal System.Windows.Forms.ToolStripMenuItem mnuFront;
        internal System.Windows.Forms.ToolStripMenuItem mnuRight;
        internal System.Windows.Forms.ToolStripMenuItem mnuIsometric;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        private SafeControls.SafeToolStripStatusLabel lblEstado;
        private SafeControls.SafeToolStripStatusLabel lblPosicionActual;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.ToolStripMenuItem salirMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.Label lblYr;
        private System.Windows.Forms.Label lblYa;
        private System.Windows.Forms.Label lblXr;
        private System.Windows.Forms.Label lblXa;
        private System.Windows.Forms.Label lblZa;
        private System.Windows.Forms.Label lblZr;
    }
}

