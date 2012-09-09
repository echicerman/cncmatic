using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DXF;
using G.Traducciones;
using G.Servicios;
using G.Objetos;
using System.IO;
using MacGen;

namespace CNCMatic
{
    public partial class Principal : Form
    {
        Boolean flag = true;
        int i = 0;
        private string mCncFile;
        private clsProcessor mProcessor = clsProcessor.Instance();
        private clsSettings mSetup = clsSettings.Instance();
        private MG_CS_BasicViewer mViewer;
        public int proxLinea;

        public class RepeatButton : System.Windows.Forms.Button
        {
            protected override void OnMouseDown(MouseEventArgs e)
            {
                base.OnMouseDown(e);
                if (e.Button == MouseButtons.Left)
                {
                    OnClick(EventArgs.Empty);
                    RepeatButton.repeatButtonTimer.Tick += new EventHandler(repeatButtonTimer_Tick);
                    repeatButtonTimer.Start();
                }
            }
            protected override void OnMouseUp(MouseEventArgs e)
            {
                this.mousingUp = true;

                base.OnMouseUp(e);

                if (e.Button == MouseButtons.Left)
                {
                    repeatButtonTimer.Stop();
                    RepeatButton.repeatButtonTimer.Tick -= new EventHandler(repeatButtonTimer_Tick);
                }
                this.mousingUp = false;
            }
            protected override void OnClick(EventArgs e)
            {
                if (this.mousingUp) return;
                base.OnClick(e);
            }
            private void repeatButtonTimer_Tick(object sender, EventArgs e)
            {
                OnClick(EventArgs.Empty);
            }
            private bool mousingUp = false;
            static RepeatButton()
            {
                repeatButtonTimer.Interval = 50;
            }
            private static System.Windows.Forms.Timer repeatButtonTimer = new Timer();
        }
        public Principal()
        {
            InitializeComponent();

            //cargamos informacion en la barra de estado
            this.lblUserName.Text = "User: " + Environment.UserName;
            this.lblOsVersion.Text = "OS Version: " + Environment.OSVersion;
            this.lblMachName.Text = "PC: " + Environment.MachineName;

            mViewer = this.MG_Viewer1;
            mProcessor.OnAddBlock += new clsProcessor.OnAddBlockEventHandler(mProcessor_OnAddBlock);

            MG_CS_BasicViewer.OnSelection += new MG_CS_BasicViewer.OnSelectionEventHandler(mViewer_OnSelection);
            MG_CS_BasicViewer.MouseLocation += new MG_CS_BasicViewer.MouseLocationEventHandler(mViewer_MouseLocation);
            MG_CS_BasicViewer.OnStatus += new MG_CS_BasicViewer.OnStatusEventHandler(mViewer_OnStatus);

            mSetup.MachineActivated += new clsSettings.MachineActivatedEventHandler(mSetup_MachineActivated);

            mSetup.LoadAllMachines(System.IO.Directory.GetCurrentDirectory() + "\\Data");
            mProcessor.Init(mSetup.Machine);

            //proxima linea a transmitir
            proxLinea = 0;
        }



        private void gbMovXY_Enter(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.txtPosX.Text = "0";
            this.txtPosY.Text = "0";
            this.txtPosZ.Text = "0";

            AgregaTextoEditor(false, Metodos.IrA(0, 0, 0));
        }

        public void Mov_Menos(System.Windows.Forms.TextBox txt)
        {
            if (Convert.ToInt32(txt.Text) > 0)
            {
                txt.Text = (Convert.ToInt32(txt.Text) - 1).ToString();
            }
        }

        public void Mov_Mas(System.Windows.Forms.TextBox txt)
        {
            txt.Text = (Convert.ToInt32(txt.Text) + 1).ToString();
        }

        private void btnMovZ_Arr_Click(object sender, EventArgs e)
        {
            this.Mov_Mas(this.txtPosZ);
        }

        private void btnMovZ_Aba_Click(object sender, EventArgs e)
        {
            this.Mov_Menos(this.txtPosZ);
        }

        private void btnMovXY_Izq_Click(object sender, EventArgs e)
        {
            this.Mov_Menos(this.txtPosX);
        }

        private void btnMovXY_Der_Click(object sender, EventArgs e)
        {
            this.Mov_Mas(this.txtPosX);

            if (flag == true)
            {
                AgregaTextoEditor(false, Metodos.IrA(Configuracion.X_MAX, 0, 0));
                flag = false;
            }
        }

        private void btnMovXY_Arr_Click(object sender, EventArgs e)
        {
            this.Mov_Mas(this.txtPosY);
        }

        private void btnMovXY_Aba_Click(object sender, EventArgs e)
        {
            this.Mov_Menos(this.txtPosY);
        }

        private void btnMovXY_Der_MouseUp(object sender, MouseEventArgs e)
        {
            AgregaTextoEditor(false, Metodos.Stop());
            flag = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            AgregaTextoEditor(true, "");
            this.LimpiarPrevisualizador();
        }

        private void LimpiarPrevisualizador()
        {
            OpenFile(System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Limpiar.cnc");
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            AgregaTextoEditor(false, Metodos.Stop());
        }

        /// <summary>
        /// Funcion que agrega una nueva linea de texto al editor visual
        /// </summary>
        /// <param name="limpia">Establece si se debe vaciar el editor previamente</param>
        /// <param name="text">Texto a agregar en el editor</param>
        private void AgregaTextoEditor(bool limpia, string text)
        {
            //si no se se limpia sumamos el texto
            if (!limpia)
                this.txtPreview.Text += text;
            else
                this.txtPreview.Text = text;

            //si el texto no es blanco, sumamos una nueva linea
            if (text != "")
                this.txtPreview.Text += Environment.NewLine;
        }

        private void dXFFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //realizamos la busqueda del archivo
            if (importaDXF.ShowDialog() == DialogResult.OK)
            {
                //realizamos la importacion del DXF
                DxfDoc doc = new DxfDoc();
                doc.Cargar(importaDXF.FileName);

                List<string> sl = Traduce.Lineas(doc.Lineas);
                List<string> sa = Traduce.Arcos(doc.Arcos);
                List<string> sc = Traduce.Circulos(doc.Circulos);
                List<string> se = Traduce.Elipses(doc.Elipses);
                List<string> sp = Traduce.Puntos(doc.Puntos);
                List<string> ss = Traduce.Polilineas(doc.Polilineas);

                //Creo un archivo temporal para previsualizar
                string curTempFileName = System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Temp" ;

                using (StreamWriter sw = File.CreateText(curTempFileName))
                {
                    foreach (string s in sl)
                    {
                        AgregaTextoEditor(false, s);
                        sw.WriteLine(s);
                    }
                   foreach (string s in sa)
                    {
                        AgregaTextoEditor(false, s);
                        sw.WriteLine(s);
                    }
                   foreach (string s in sc)
                   {
                       AgregaTextoEditor(false, s);
                       sw.WriteLine(s);
                   }
                   foreach (string s in se)
                   {
                       AgregaTextoEditor(false, s);
                       sw.WriteLine(s);
                   }
                   foreach (string s in sp)
                   {
                       AgregaTextoEditor(false, s);
                       sw.WriteLine(s);
                   }
                   foreach (string s in ss)
                   {
                       AgregaTextoEditor(false, s);
                       sw.WriteLine(s);
                   }
                   sw.Close();
                   OpenFile(curTempFileName);
                }

                
            }


        }

        //***************************************

        static void crearArchivoTemp()
        {

            string curTempFileName = "";

            curTempFileName = Path.GetTempFileName();

            //Ahora creamos fisicamente el archivo

            using (StreamWriter sw = File.CreateText(curTempFileName))
            {

                sw.WriteLine("Primera linea del archivo");

                sw.Close();

            }

            Console.WriteLine("Se ha creado el archivo temporal satisfactoriamente!!");

            Console.ReadLine();

        }

        //*******************************************

        private void gCodeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //realizamos la busqueda del archivo
            if (importaG.ShowDialog() == DialogResult.OK)
            {
                //importacion de codigo G
                Importacion imp = new Importacion();
                List<string> lineas = imp.leeGfile(importaG.FileName);

                foreach (string s in lineas)
                    this.txtPreview.Text += (s + Environment.NewLine);

                //Muestra codigo en el previsualizador
                OpenFile(importaG.FileName);

                //string mensaje = "¿Desea que el sistema intente optimizar el código G importado? Atención: esta operación podría variar el orden de fresado preestablecido.";

                //DialogResult r = MessageBox.Show(mensaje, "Optimización de código G", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                //if (r == DialogResult.Yes)
                //{
                //    //lanzamos optimizacion de código G
                //}

            }




        }

        private void txtLineaManual_KeyPress(object sender, KeyPressEventArgs e)
        {
            //si la tecla presionada es Enter, agregamos la linea al editor
            if (e.KeyChar == (char)Keys.Enter)
            {
                AgregaTextoEditor(false, this.txtLineaManual.Text.Trim());
                this.txtLineaManual.Text = "";
            }
        }

        private void acercaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Acerca a = new Acerca();
            a.ShowDialog();
        }

        private void btnLinea_Click(object sender, EventArgs e)
        {
            G01_Lineal g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            AgregaTextoEditor(false, g.ToString());

            //Muestra figura en el previsualizador
            PrevisualizarFigurasManual();
        }

        private void btnArco_Click(object sender, EventArgs e)
        {
            G02_ArcoH g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            AgregaTextoEditor(false, g.ToString());

            //Muestra figura en el previsualizador
            PrevisualizarFigurasManual();

        }

        private void PrevisualizarFigurasManual()
        {
            string curTempFileName = System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Temp";

            using (StreamWriter sw = File.CreateText(curTempFileName))
            {
                sw.WriteLine(txtPreview.Text);
                sw.Close();
                OpenFile(curTempFileName);
            }
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            G02_CirculoH g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            AgregaTextoEditor(false, g.ToString());

            //string curTempFileName = System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Temp";

            //Muestra figura en el previsualizador
            PrevisualizarFigurasManual();
        }

        private void btnEsfera_Click(object sender, EventArgs e)
        {

        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmConfiguracion()).ShowDialog();

        }

        private void Principal_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Virgin == true)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                this.Location = Properties.Settings.Default.ViewFormLocation;
                this.Size = Properties.Settings.Default.ViewFormSize;
            }

            SetDefaultViews();
            Properties.Settings.Default.Virgin = false;
        }

        private void comunicaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmComunicacion()).ShowDialog();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPreview.Text.Trim() == "")
                {
                    MessageBox.Show("No hay información para guardar", "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Codigo G(*.gcode)|*.gcode";
                save.DefaultExt = "*.gcode";
                save.FileName = "*.gcode";
                save.ShowDialog();

                string filename = save.FileName;

                if (filename == "*.gcode" || filename == "")
                {
                    MessageBox.Show("Error: no se ha especificado un nombre de archivo valido", "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //guardamos el contenido del txtPreview en un archivo .gcode
                if (guardaGfile(filename))
                    MessageBox.Show("Se ha guardado el archivo correctamente", "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message, "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IniciarFresado(object sender, EventArgs e)
        {
            this.Refresh();
            if (OpenFileDialog1.FileName.Length > 0)
            {
                OpenFile(OpenFileDialog1.FileName);
            }
        }

        private bool guardaGfile(string path)
        {
            try
            {

                StreamWriter sr = new StreamWriter(path);

                foreach (string linea in txtPreview.Lines)
                {
                    if (linea.Trim() != "")
                    {
                        sr.WriteLine(linea);
                    }
                }

                sr.Close();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }


        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.ViewFormLocation = this.Location;
                    Properties.Settings.Default.ViewFormSize = this.Size;
                }
            }
            catch
            {
            }
        }

        private void mProcessor_OnAddBlock(int idx, int ct)
        {
            try
            {
                this.prgBar.Maximum = ct;
                this.prgBar.Value = idx;
                if (ct > 10000)
                {
                    //Refresh every 1000 blocks 
                    if (1000 % idx == 0)
                    {
                        mViewer.FindExtents();
                        mViewer.Redraw(true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("mProcessor_OnAddBlock: " + ex.Message, ex);
            }
        }

        private void mViewer_MouseLocation(float x, float y)
        {
            Coordinates.Text = "X=" + x.ToString("0.000") + " Y=" + y.ToString("0.000");
        }

        private void ViewportActivated(object sender, System.EventArgs e)
        {

        }

        private void SetDefaultViews()
        {
            //Case "Top" 
            MG_Viewer1.Pitch = 0f;
            MG_Viewer1.Roll = 0f;
            MG_Viewer1.Yaw = 0f;
            MG_Viewer1.FindExtents();
            mViewer.Redraw(true);
        }

        private void mViewer_OnSelection(System.Collections.Generic.List<clsMotionRecord> hits)
        {
            lblStatus.Text = hits[0].Codestring;
            string[] tipString = new string[hits.Count];
            for (int r = 0; r <= hits.Count - 1; r++)
            {
                tipString[r] = hits[r].Codestring;
            }
            this.CodeTip.SetToolTip(mViewer, string.Join(Environment.NewLine, tipString));
        }

        private void mViewer_OnStatus(string msg, int index, int max)
        {
            lblStatus.Text = msg;
            prgBar.Maximum = max;
            prgBar.Value = index;
            toolStrip2.Refresh();
        }

        private void mSetup_MachineActivated(clsMachine m)
        {
            {
                MG_Viewer1.RotaryDirection = (RotaryDirection)m.RotaryDir;
                MG_Viewer1.RotaryPlane = (Axis)m.RotaryAxis;
                MG_Viewer1.RotaryType = (RotaryMotionType)m.RotaryType;
                MG_Viewer1.ViewManipMode = MG_CS_BasicViewer.ManipMode.SELECTION;

                MG_Viewer1.FindExtents();
                mViewer.Redraw(true);

            }
        }

        private void ViewButtonClicked(object sender, EventArgs e)
        {
            string tag = sender.GetType().GetProperty("Tag").GetValue(sender, null).ToString();
            switch (tag)
            {
                case "Fit":
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        MG_Viewer1.FindExtents();
                    }
                    else
                    {
                        mViewer.FindExtents();
                    }

                    break;
                case "Pan":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.PAN;
                    tsbPan.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Fence":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.FENCE;
                    tsbFence.Checked = true;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Zoom":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.ZOOM;
                    tsbZoom.Checked = true;
                    tsbRotate.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Rotate":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.ROTATE;
                    tsbRotate.Checked = true;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = false;
                    break;
                case "Select":
                    mViewer.ViewManipMode = MG_CS_BasicViewer.ManipMode.SELECTION;
                    tsbRotate.Checked = false;
                    tsbZoom.Checked = false;
                    tsbFence.Checked = false;
                    tsbPan.Checked = false;
                    tsbSelect.Checked = true;
                    break;
            }

        }
        private void mnuViewOrient_Click(object sender, System.EventArgs e)
        {
            switch (((System.Windows.Forms.ToolStripMenuItem)sender).Tag.ToString())
            {
                case "Superior":
                    mViewer.Pitch = 0;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 0;
                    break;
                case "Frontal":
                    mViewer.Pitch = 270;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 360;
                    break;
                case "Lateral":
                    mViewer.Pitch = 270;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 270;
                    break;
                case "Isometrica":
                    mViewer.Pitch = 315;
                    mViewer.Roll = 0;
                    mViewer.Yaw = 315;
                    break;
            }
            mViewer.FindExtents();
            mViewer.Redraw(true);
        }
        private void Principal_ResizeEnd(object sender, EventArgs e)
        {
            MG_Viewer1.FindExtents();
            mViewer.Redraw(true);
        }

        private void DisplayCheckChanged(object sender, EventArgs e)
        {
            if (mViewer == null) return;

            mViewer.DrawRapidLines = mnuRapidLines.Checked;
            mViewer.DrawRapidPoints = mnuRapidPoints.Checked;
            mViewer.DrawAxisLines = mnuAxisLines.Checked;
            mViewer.DrawAxisIndicator = mnuAxisindicator.Checked;
            mViewer.Redraw(true);
        }

        private void ProcessFile(string fileName)
        {
            if (fileName == null)
            {
                return;
            }
            if (!System.IO.File.Exists(fileName))
            {
                lblStatus.Text = "File does not exist!";
                return;
            }
            lblStatus.Text = "Processing...";
            MG_CS_BasicViewer.MotionBlocks.Clear();
            mProcessor.Init(mSetup.Machine);
            mProcessor.ProcessFile(fileName, MG_CS_BasicViewer.MotionBlocks);

            BreakPointSlider.Maximum = MG_CS_BasicViewer.MotionBlocks.Count - 1;
            if (mViewer.BreakPoint > MG_CS_BasicViewer.MotionBlocks.Count - 1)
            {
                mViewer.BreakPoint = MG_CS_BasicViewer.MotionBlocks.Count - 1;
            }
            mViewer.GatherTools();
            lblStatus.Text = "Done";
            prgBar.Value = 0;

        }

        private void BreakPointSlider_ValueChanged(object sender, EventArgs e)
        {
            mViewer.BreakPoint = BreakPointSlider.Value;
            mViewer.Redraw(true);
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog1.ShowDialog();
            this.Refresh();
            if (OpenFileDialog1.FileName.Length > 0)
            {
                OpenFile(OpenFileDialog1.FileName);
            }
        }

        private void OpenFile(string fileName)
        {
            long[] ticks = new long[2];
            mCncFile = fileName;
            mSetup.MatchMachineToFile(mCncFile);

            ProcessFile(mCncFile);
            mViewer.BreakPoint = MG_CS_BasicViewer.MotionBlocks.Count - 1;

            mViewer.Pitch = mSetup.Machine.ViewAngles[0];
            mViewer.Roll = mSetup.Machine.ViewAngles[1];
            mViewer.Yaw = mSetup.Machine.ViewAngles[2];
            mViewer.Init();

            ticks[0] = DateTime.Now.Ticks;
            MG_Viewer1.FindExtents();
            ticks[1] = DateTime.Now.Ticks;
            MG_Viewer1.DynamicViewManipulation = (ticks[1] - ticks[0]) < 2000000;
            mViewer.Redraw(true);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {

            this.LimpiarPrevisualizador();
            PrevisualizarFigurasManual();
        }

        public string proximaInstruccion()
        {
            return txtPreview.Lines[this.proxLinea++];
        }

        private void dToolStripMenuItem2_Click(object sender, EventArgs e)
        {

            G01_Cuadrado g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            AgregaTextoEditor(false, g.ToString());

            //string curTempFileName = System.IO.Directory.GetCurrentDirectory() + "\\Samples\\Temp";

            //Muestra figura en el previsualizador
            PrevisualizarFigurasManual();
        }
    }

}
