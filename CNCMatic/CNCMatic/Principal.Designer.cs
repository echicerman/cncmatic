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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
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
            this.gbMovXY = new System.Windows.Forms.GroupBox();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dXFFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gCodeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comunicaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.acercaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.grpOperacion = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.prgBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMachName = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOsVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtLineaManual = new System.Windows.Forms.TextBox();
            this.grpPrev = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mG_CS_BasicViewer1 = new MacGen.MG_CS_BasicViewer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tscboMachines = new System.Windows.Forms.ToolStripComboBox();
            this.tsbDisplay = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuRapidLines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRapidPoints = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAxisLines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAxisindicator = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbToolsFilter = new System.Windows.Forms.ToolStripButton();
            this.tsbScreens = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuOneScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTwoScreens = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFourScreens = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbWebCheck = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPan = new System.Windows.Forms.ToolStripButton();
            this.tsbZoom = new System.Windows.Forms.ToolStripButton();
            this.tsbRotate = new System.Windows.Forms.ToolStripButton();
            this.tsbFence = new System.Windows.Forms.ToolStripButton();
            this.tsbFit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.grpPrevisualizacion = new System.Windows.Forms.GroupBox();
            this.tblScreens = new System.Windows.Forms.TableLayoutPanel();
            this.MG_Viewer1 = new MacGen.MG_CS_BasicViewer();
            this.tsbView = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuTop = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFront = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIsometric = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbSelect = new System.Windows.Forms.ToolStripButton();
            this.btnInicio = new System.Windows.Forms.Button();
            this.btnStop2 = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnMovXY_Der = new CNCMatic.Principal.RepeatButton();
            this.btnMovXY_Izq = new CNCMatic.Principal.RepeatButton();
            this.btnMovXY_Aba = new CNCMatic.Principal.RepeatButton();
            this.btnMovXY_Arr = new CNCMatic.Principal.RepeatButton();
            this.btnMovZ_Aba = new CNCMatic.Principal.RepeatButton();
            this.btnMovZ_Arr = new CNCMatic.Principal.RepeatButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnCirculo = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEsfera = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.dToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLinea = new System.Windows.Forms.ToolStripButton();
            this.btnArco = new System.Windows.Forms.ToolStripButton();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.gbPosicionActual.SuspendLayout();
            this.gbMovZ.SuspendLayout();
            this.gbMovXY.SuspendLayout();
            this.menu.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.grpOperacion.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpPrev.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
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
            this.gbPosicionActual.Location = new System.Drawing.Point(275, 103);
            this.gbPosicionActual.Name = "gbPosicionActual";
            this.gbPosicionActual.Size = new System.Drawing.Size(103, 128);
            this.gbPosicionActual.TabIndex = 3;
            this.gbPosicionActual.TabStop = false;
            this.gbPosicionActual.Text = "Posición Actual";
            // 
            // lblPosZ
            // 
            this.lblPosZ.AutoSize = true;
            this.lblPosZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosZ.Location = new System.Drawing.Point(6, 79);
            this.lblPosZ.Name = "lblPosZ";
            this.lblPosZ.Size = new System.Drawing.Size(15, 13);
            this.lblPosZ.TabIndex = 5;
            this.lblPosZ.Text = "Z";
            // 
            // txtPosZ
            // 
            this.txtPosZ.Location = new System.Drawing.Point(26, 76);
            this.txtPosZ.Name = "txtPosZ";
            this.txtPosZ.Size = new System.Drawing.Size(63, 20);
            this.txtPosZ.TabIndex = 4;
            this.txtPosZ.Text = "0";
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPosY.Location = new System.Drawing.Point(6, 53);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(15, 13);
            this.lblPosY.TabIndex = 3;
            this.lblPosY.Text = "Y";
            // 
            // txtPosY
            // 
            this.txtPosY.Location = new System.Drawing.Point(26, 50);
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
            this.gbMovZ.Location = new System.Drawing.Point(193, 102);
            this.gbMovZ.Name = "gbMovZ";
            this.gbMovZ.Size = new System.Drawing.Size(68, 129);
            this.gbMovZ.TabIndex = 1;
            this.gbMovZ.TabStop = false;
            this.gbMovZ.Text = "Mov-Z";
            // 
            // gbMovXY
            // 
            this.gbMovXY.Controls.Add(this.btnMovXY_Der);
            this.gbMovXY.Controls.Add(this.btnMovXY_Izq);
            this.gbMovXY.Controls.Add(this.btnMovXY_Aba);
            this.gbMovXY.Controls.Add(this.btnMovXY_Arr);
            this.gbMovXY.Location = new System.Drawing.Point(16, 102);
            this.gbMovXY.Name = "gbMovXY";
            this.gbMovXY.Size = new System.Drawing.Size(163, 128);
            this.gbMovXY.TabIndex = 0;
            this.gbMovXY.TabStop = false;
            this.gbMovXY.Text = "Mov-XY";
            this.gbMovXY.Enter += new System.EventHandler(this.gbMovXY_Enter);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.verToolStripMenuItem,
            this.comunicaciónToolStripMenuItem,
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
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(36, 20);
            this.verToolStripMenuItem.Text = "Ver";
            // 
            // comunicaciónToolStripMenuItem
            // 
            this.comunicaciónToolStripMenuItem.Name = "comunicaciónToolStripMenuItem";
            this.comunicaciónToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.comunicaciónToolStripMenuItem.Text = "Comunicación";
            this.comunicaciónToolStripMenuItem.Click += new System.EventHandler(this.comunicaciónToolStripMenuItem_Click);
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
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // acercaToolStripMenuItem
            // 
            this.acercaToolStripMenuItem.Name = "acercaToolStripMenuItem";
            this.acercaToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.acercaToolStripMenuItem.Text = "Acerca De";
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
            this.toolStrip1.Size = new System.Drawing.Size(78, 673);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(75, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(75, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(75, 6);
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
            this.grpOperacion.Location = new System.Drawing.Point(110, 428);
            this.grpOperacion.Name = "grpOperacion";
            this.grpOperacion.Size = new System.Drawing.Size(396, 237);
            this.grpOperacion.TabIndex = 26;
            this.grpOperacion.TabStop = false;
            this.grpOperacion.Text = "Operación Manual";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgBar,
            this.lblUserName,
            this.lblMachName,
            this.lblOsVersion});
            this.statusStrip1.Location = new System.Drawing.Point(78, 675);
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
            // txtLineaManual
            // 
            this.txtLineaManual.Location = new System.Drawing.Point(110, 385);
            this.txtLineaManual.Name = "txtLineaManual";
            this.txtLineaManual.Size = new System.Drawing.Size(328, 20);
            this.txtLineaManual.TabIndex = 29;
            this.txtLineaManual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLineaManual_KeyPress);
            // 
            // grpPrev
            // 
            this.grpPrev.Controls.Add(this.groupBox2);
            this.grpPrev.Controls.Add(this.toolStrip2);
            this.grpPrev.Location = new System.Drawing.Point(529, 28);
            this.grpPrev.Name = "grpPrev";
            this.grpPrev.Size = new System.Drawing.Size(618, 637);
            this.grpPrev.TabIndex = 31;
            this.grpPrev.TabStop = false;
            this.grpPrev.Text = "Previsualización";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(6, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(606, 587);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.mG_CS_BasicViewer1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 568);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // mG_CS_BasicViewer1
            // 
            this.mG_CS_BasicViewer1.AxisIndicatorScale = 0.75F;
            this.mG_CS_BasicViewer1.BackColor = System.Drawing.Color.Black;
            this.mG_CS_BasicViewer1.BreakPoint = -1;
            this.mG_CS_BasicViewer1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mG_CS_BasicViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mG_CS_BasicViewer1.DynamicViewManipulation = true;
            this.mG_CS_BasicViewer1.FourthAxis = 0F;
            this.mG_CS_BasicViewer1.Location = new System.Drawing.Point(1, 1);
            this.mG_CS_BasicViewer1.Margin = new System.Windows.Forms.Padding(0);
            this.mG_CS_BasicViewer1.Name = "mG_CS_BasicViewer1";
            this.mG_CS_BasicViewer1.Pitch = 0F;
            this.mG_CS_BasicViewer1.Roll = 0F;
            this.mG_CS_BasicViewer1.RotaryType = MacGen.RotaryMotionType.BMC;
            this.mG_CS_BasicViewer1.Size = new System.Drawing.Size(598, 566);
            this.mG_CS_BasicViewer1.TabIndex = 1;
            this.mG_CS_BasicViewer1.ViewManipMode = MacGen.MG_CS_BasicViewer.ManipMode.SELECTION;
            this.mG_CS_BasicViewer1.Yaw = 0F;
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbOpen,
            this.tscboMachines,
            this.tsbDisplay,
            this.tsbToolsFilter,
            this.tsbScreens,
            this.tsbWebCheck,
            this.toolStripSeparator6,
            this.tsbPan,
            this.tsbZoom,
            this.tsbRotate,
            this.tsbFence,
            this.tsbFit,
            this.toolStripSeparator7,
            this.tsbView,
            this.tsbSelect});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(612, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbOpen
            // 
            this.tsbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(23, 22);
            this.tsbOpen.Text = "Open";
            // 
            // tscboMachines
            // 
            this.tscboMachines.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscboMachines.Name = "tscboMachines";
            this.tscboMachines.Padding = new System.Windows.Forms.Padding(1);
            this.tscboMachines.Size = new System.Drawing.Size(75, 25);
            // 
            // tsbDisplay
            // 
            this.tsbDisplay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRapidLines,
            this.mnuRapidPoints,
            this.mnuAxisLines,
            this.mnuAxisindicator});
            this.tsbDisplay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDisplay.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbDisplay.Name = "tsbDisplay";
            this.tsbDisplay.Size = new System.Drawing.Size(13, 22);
            this.tsbDisplay.Text = "&Display";
            // 
            // mnuRapidLines
            // 
            this.mnuRapidLines.Checked = true;
            this.mnuRapidLines.CheckOnClick = true;
            this.mnuRapidLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuRapidLines.Name = "mnuRapidLines";
            this.mnuRapidLines.Size = new System.Drawing.Size(145, 22);
            this.mnuRapidLines.Text = "Rapid &Lines";
            // 
            // mnuRapidPoints
            // 
            this.mnuRapidPoints.Checked = true;
            this.mnuRapidPoints.CheckOnClick = true;
            this.mnuRapidPoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuRapidPoints.Name = "mnuRapidPoints";
            this.mnuRapidPoints.Size = new System.Drawing.Size(145, 22);
            this.mnuRapidPoints.Text = "Rapid &Points";
            // 
            // mnuAxisLines
            // 
            this.mnuAxisLines.Checked = true;
            this.mnuAxisLines.CheckOnClick = true;
            this.mnuAxisLines.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuAxisLines.Name = "mnuAxisLines";
            this.mnuAxisLines.Size = new System.Drawing.Size(145, 22);
            this.mnuAxisLines.Text = "&Axis Lines";
            // 
            // mnuAxisindicator
            // 
            this.mnuAxisindicator.Checked = true;
            this.mnuAxisindicator.CheckOnClick = true;
            this.mnuAxisindicator.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuAxisindicator.Name = "mnuAxisindicator";
            this.mnuAxisindicator.Size = new System.Drawing.Size(145, 22);
            this.mnuAxisindicator.Text = "Axis &Indicator";
            // 
            // tsbToolsFilter
            // 
            this.tsbToolsFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbToolsFilter.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbToolsFilter.Name = "tsbToolsFilter";
            this.tsbToolsFilter.Size = new System.Drawing.Size(23, 22);
            this.tsbToolsFilter.Text = "Tool Layers";
            // 
            // tsbScreens
            // 
            this.tsbScreens.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbScreens.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOneScreen,
            this.mnuTwoScreens,
            this.mnuFourScreens});
            this.tsbScreens.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbScreens.Name = "tsbScreens";
            this.tsbScreens.Size = new System.Drawing.Size(13, 22);
            this.tsbScreens.Text = "Screens";
            // 
            // mnuOneScreen
            // 
            this.mnuOneScreen.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.mnuOneScreen.Name = "mnuOneScreen";
            this.mnuOneScreen.Size = new System.Drawing.Size(107, 22);
            this.mnuOneScreen.Tag = "1";
            this.mnuOneScreen.Text = "&1 One";
            // 
            // mnuTwoScreens
            // 
            this.mnuTwoScreens.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.mnuTwoScreens.Name = "mnuTwoScreens";
            this.mnuTwoScreens.Size = new System.Drawing.Size(107, 22);
            this.mnuTwoScreens.Tag = "2";
            this.mnuTwoScreens.Text = "&2 Two";
            // 
            // mnuFourScreens
            // 
            this.mnuFourScreens.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.mnuFourScreens.Name = "mnuFourScreens";
            this.mnuFourScreens.Size = new System.Drawing.Size(107, 22);
            this.mnuFourScreens.Tag = "4";
            this.mnuFourScreens.Text = "&4 Four";
            // 
            // tsbWebCheck
            // 
            this.tsbWebCheck.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbWebCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbWebCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWebCheck.Name = "tsbWebCheck";
            this.tsbWebCheck.Size = new System.Drawing.Size(23, 22);
            this.tsbWebCheck.Text = "Phone Home";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPan
            // 
            this.tsbPan.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbPan.CheckOnClick = true;
            this.tsbPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPan.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbPan.Name = "tsbPan";
            this.tsbPan.Size = new System.Drawing.Size(23, 22);
            this.tsbPan.Tag = "Pan";
            this.tsbPan.Text = "Pan";
            // 
            // tsbZoom
            // 
            this.tsbZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbZoom.CheckOnClick = true;
            this.tsbZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbZoom.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbZoom.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbZoom.Name = "tsbZoom";
            this.tsbZoom.Size = new System.Drawing.Size(23, 22);
            this.tsbZoom.Tag = "Zoom";
            this.tsbZoom.Text = "Zoom";
            // 
            // tsbRotate
            // 
            this.tsbRotate.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbRotate.CheckOnClick = true;
            this.tsbRotate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRotate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRotate.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbRotate.Name = "tsbRotate";
            this.tsbRotate.Size = new System.Drawing.Size(23, 22);
            this.tsbRotate.Tag = "Rotate";
            this.tsbRotate.Text = "Rotate";
            // 
            // tsbFence
            // 
            this.tsbFence.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbFence.CheckOnClick = true;
            this.tsbFence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFence.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFence.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbFence.Name = "tsbFence";
            this.tsbFence.Size = new System.Drawing.Size(23, 22);
            this.tsbFence.Tag = "Fence";
            this.tsbFence.Text = "Fence";
            // 
            // tsbFit
            // 
            this.tsbFit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFit.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbFit.Name = "tsbFit";
            this.tsbFit.Size = new System.Drawing.Size(23, 22);
            this.tsbFit.Tag = "Fit";
            this.tsbFit.Text = "Fit";
            this.tsbFit.ToolTipText = "View Fit [Shift + Click All Views]";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
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
            this.MG_Viewer1.Size = new System.Drawing.Size(578, 566);
            this.MG_Viewer1.TabIndex = 1;
            this.MG_Viewer1.ViewManipMode = MacGen.MG_CS_BasicViewer.ManipMode.SELECTION;
            this.MG_Viewer1.Yaw = 0F;
            // 
            // tsbView
            // 
            this.tsbView.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
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
            this.mnuTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.mnuTop.Size = new System.Drawing.Size(160, 22);
            this.mnuTop.Tag = "Top";
            this.mnuTop.Text = "&Top";
            // 
            // mnuFront
            // 
            this.mnuFront.Image = ((System.Drawing.Image)(resources.GetObject("mnuFront.Image")));
            this.mnuFront.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuFront.Name = "mnuFront";
            this.mnuFront.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuFront.Size = new System.Drawing.Size(160, 22);
            this.mnuFront.Tag = "Front";
            this.mnuFront.Text = "&Front";
            // 
            // mnuRight
            // 
            this.mnuRight.Image = ((System.Drawing.Image)(resources.GetObject("mnuRight.Image")));
            this.mnuRight.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuRight.Name = "mnuRight";
            this.mnuRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuRight.Size = new System.Drawing.Size(160, 22);
            this.mnuRight.Tag = "Right";
            this.mnuRight.Text = "&Right";
            // 
            // mnuIsometric
            // 
            this.mnuIsometric.Image = ((System.Drawing.Image)(resources.GetObject("mnuIsometric.Image")));
            this.mnuIsometric.ImageTransparentColor = System.Drawing.Color.Fuchsia;
            this.mnuIsometric.Name = "mnuIsometric";
            this.mnuIsometric.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuIsometric.Size = new System.Drawing.Size(160, 22);
            this.mnuIsometric.Tag = "ISO";
            this.mnuIsometric.Text = "&Isometric";
            // 
            // tsbSelect
            // 
            this.tsbSelect.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbSelect.Checked = true;
            this.tsbSelect.CheckOnClick = true;
            this.tsbSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSelect.Image = ((System.Drawing.Image)(resources.GetObject("tsbSelect.Image")));
            this.tsbSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSelect.Name = "tsbSelect";
            this.tsbSelect.Size = new System.Drawing.Size(23, 22);
            this.tsbSelect.Tag = "Select";
            this.tsbSelect.Text = "Select";
            // 
            // btnInicio
            // 
            this.btnInicio.BackColor = System.Drawing.Color.White;
            this.btnInicio.Image = global::CNCMatic.Properties.Resources.HomeButton;
            this.btnInicio.Location = new System.Drawing.Point(10, 20);
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
            this.btnStop2.Location = new System.Drawing.Point(111, 20);
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
            this.btnPlay.Location = new System.Drawing.Point(315, 20);
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
            this.btnPause.Location = new System.Drawing.Point(214, 20);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(71, 76);
            this.btnPause.TabIndex = 24;
            this.btnPause.UseVisualStyleBackColor = false;
            // 
            // btnMovXY_Der
            // 
            this.btnMovXY_Der.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Der.Image = global::CNCMatic.Properties.Resources.flecha_DER;
            this.btnMovXY_Der.Location = new System.Drawing.Point(107, 43);
            this.btnMovXY_Der.Name = "btnMovXY_Der";
            this.btnMovXY_Der.Size = new System.Drawing.Size(46, 50);
            this.btnMovXY_Der.TabIndex = 10;
            this.btnMovXY_Der.UseVisualStyleBackColor = false;
            this.btnMovXY_Der.Click += new System.EventHandler(this.btnMovXY_Der_Click);
            this.btnMovXY_Der.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnMovXY_Der_MouseUp);
            // 
            // btnMovXY_Izq
            // 
            this.btnMovXY_Izq.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Izq.Image = global::CNCMatic.Properties.Resources.flecha_IZQ;
            this.btnMovXY_Izq.Location = new System.Drawing.Point(6, 43);
            this.btnMovXY_Izq.Name = "btnMovXY_Izq";
            this.btnMovXY_Izq.Size = new System.Drawing.Size(44, 50);
            this.btnMovXY_Izq.TabIndex = 9;
            this.btnMovXY_Izq.UseVisualStyleBackColor = false;
            this.btnMovXY_Izq.Click += new System.EventHandler(this.btnMovXY_Izq_Click);
            // 
            // btnMovXY_Aba
            // 
            this.btnMovXY_Aba.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovXY_Aba.Location = new System.Drawing.Point(56, 72);
            this.btnMovXY_Aba.Name = "btnMovXY_Aba";
            this.btnMovXY_Aba.Size = new System.Drawing.Size(44, 45);
            this.btnMovXY_Aba.TabIndex = 8;
            this.btnMovXY_Aba.UseVisualStyleBackColor = false;
            this.btnMovXY_Aba.Click += new System.EventHandler(this.btnMovXY_Aba_Click);
            // 
            // btnMovXY_Arr
            // 
            this.btnMovXY_Arr.BackColor = System.Drawing.Color.White;
            this.btnMovXY_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovXY_Arr.Location = new System.Drawing.Point(56, 15);
            this.btnMovXY_Arr.Name = "btnMovXY_Arr";
            this.btnMovXY_Arr.Size = new System.Drawing.Size(44, 46);
            this.btnMovXY_Arr.TabIndex = 7;
            this.btnMovXY_Arr.UseVisualStyleBackColor = false;
            this.btnMovXY_Arr.Click += new System.EventHandler(this.btnMovXY_Arr_Click);
            // 
            // btnMovZ_Aba
            // 
            this.btnMovZ_Aba.BackColor = System.Drawing.Color.White;
            this.btnMovZ_Aba.Image = global::CNCMatic.Properties.Resources.flecha_ABA;
            this.btnMovZ_Aba.Location = new System.Drawing.Point(13, 73);
            this.btnMovZ_Aba.Name = "btnMovZ_Aba";
            this.btnMovZ_Aba.Size = new System.Drawing.Size(49, 45);
            this.btnMovZ_Aba.TabIndex = 7;
            this.btnMovZ_Aba.UseVisualStyleBackColor = false;
            this.btnMovZ_Aba.Click += new System.EventHandler(this.btnMovZ_Aba_Click);
            // 
            // btnMovZ_Arr
            // 
            this.btnMovZ_Arr.BackColor = System.Drawing.Color.White;
            this.btnMovZ_Arr.Image = global::CNCMatic.Properties.Resources.flecha_ARR;
            this.btnMovZ_Arr.Location = new System.Drawing.Point(13, 19);
            this.btnMovZ_Arr.Name = "btnMovZ_Arr";
            this.btnMovZ_Arr.Size = new System.Drawing.Size(48, 43);
            this.btnMovZ_Arr.TabIndex = 6;
            this.btnMovZ_Arr.UseVisualStyleBackColor = false;
            this.btnMovZ_Arr.Click += new System.EventHandler(this.btnMovZ_Arr_Click);
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
            this.btnCirculo.Size = new System.Drawing.Size(137, 22);
            this.btnCirculo.Text = "2D - Circulo";
            this.btnCirculo.Click += new System.EventHandler(this.btnCirculo_Click);
            // 
            // btnEsfera
            // 
            this.btnEsfera.Name = "btnEsfera";
            this.btnEsfera.Size = new System.Drawing.Size(137, 22);
            this.btnEsfera.Text = "3D - Esfera";
            this.btnEsfera.Click += new System.EventHandler(this.btnEsfera_Click);
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
            this.toolStripDropDownButton2.ToolTipText = "Cuadrado/Cubo";
            // 
            // dToolStripMenuItem2
            // 
            this.dToolStripMenuItem2.Name = "dToolStripMenuItem2";
            this.dToolStripMenuItem2.Size = new System.Drawing.Size(151, 22);
            this.dToolStripMenuItem2.Text = "2D - Cuadrado";
            // 
            // dToolStripMenuItem3
            // 
            this.dToolStripMenuItem3.Name = "dToolStripMenuItem3";
            this.dToolStripMenuItem3.Size = new System.Drawing.Size(151, 22);
            this.dToolStripMenuItem3.Text = "3D - Cubo";
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
            // btnLimpiar
            // 
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.Image = global::CNCMatic.Properties.Resources.brush_icon__1_;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLimpiar.Location = new System.Drawing.Point(432, 375);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(79, 39);
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
            this.ClientSize = new System.Drawing.Size(1162, 697);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.grpPrev);
            this.Controls.Add(this.txtLineaManual);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpOperacion);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CNC Matic";
            this.Load += new System.EventHandler(this.Principal_Load);
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
            this.grpPrev.ResumeLayout(false);
            this.grpPrev.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem comunicaciónToolStripMenuItem;
        private System.Windows.Forms.GroupBox grpPrev;
        internal System.Windows.Forms.ToolStrip toolStrip2;
        internal System.Windows.Forms.ToolStripButton tsbOpen;
        internal System.Windows.Forms.ToolStripComboBox tscboMachines;
        internal System.Windows.Forms.ToolStripDropDownButton tsbDisplay;
        internal System.Windows.Forms.ToolStripMenuItem mnuRapidLines;
        internal System.Windows.Forms.ToolStripMenuItem mnuRapidPoints;
        internal System.Windows.Forms.ToolStripMenuItem mnuAxisLines;
        internal System.Windows.Forms.ToolStripMenuItem mnuAxisindicator;
        internal System.Windows.Forms.ToolStripButton tsbToolsFilter;
        internal System.Windows.Forms.ToolStripDropDownButton tsbScreens;
        internal System.Windows.Forms.ToolStripMenuItem mnuOneScreen;
        internal System.Windows.Forms.ToolStripMenuItem mnuTwoScreens;
        internal System.Windows.Forms.ToolStripMenuItem mnuFourScreens;
        internal System.Windows.Forms.ToolStripButton tsbWebCheck;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripButton tsbPan;
        internal System.Windows.Forms.ToolStripButton tsbZoom;
        internal System.Windows.Forms.ToolStripButton tsbRotate;
        internal System.Windows.Forms.ToolStripButton tsbFence;
        internal System.Windows.Forms.ToolStripButton tsbFit;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripDropDownButton tsbView;
        internal System.Windows.Forms.ToolStripMenuItem mnuTop;
        internal System.Windows.Forms.ToolStripMenuItem mnuFront;
        internal System.Windows.Forms.ToolStripMenuItem mnuRight;
        internal System.Windows.Forms.ToolStripMenuItem mnuIsometric;
        internal System.Windows.Forms.ToolStripButton tsbSelect;
        private System.Windows.Forms.GroupBox grpPrevisualizacion;
        private System.Windows.Forms.TableLayoutPanel tblScreens;
        internal MacGen.MG_CS_BasicViewer MG_Viewer1;
        private System.Windows.Forms.GroupBox groupBox2;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal MacGen.MG_CS_BasicViewer mG_CS_BasicViewer1;
    }
}

