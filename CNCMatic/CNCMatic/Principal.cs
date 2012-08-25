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

namespace CNCMatic
{
    public partial class Principal : Form
    {
        Boolean flag = true;
        int i = 0;
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


                foreach (string s in sl)
                    AgregaTextoEditor(false, s);
                foreach (string s in sa)
                    AgregaTextoEditor(false, s);
            }


        }

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

                string mensaje = "¿Desea que el sistema intente optimizar el código G importado? Atención: esta operación podría variar el orden de fresado preestablecido.";

                DialogResult r = MessageBox.Show(mensaje, "Optimización de código G", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (r == DialogResult.Yes)
                {
                    //lanzamos optimizacion de código G
                }

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

        }

        private void btnArco_Click(object sender, EventArgs e)
        {
            G02_CirculoH g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            AgregaTextoEditor(false, g.ToString());
        }

        private void btnCirculo_Click(object sender, EventArgs e)
        {
            G02_CirculoH g;

            FrmDibujoParams dibujoParams = new FrmDibujoParams(out g);
            dibujoParams.ShowDialog();

            AgregaTextoEditor(false, g.ToString());
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

        }

        private void comunicaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new FrmComunicacion()).ShowDialog();
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPreview.Text.Trim() == "") {
                    MessageBox.Show("No hay información para guardar","Guardar como...",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                if(guardaGfile(filename))
                    MessageBox.Show("Se ha guardado el archivo correctamente", "Guardar como...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error: " + ex.Message,"Guardar como...",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

    }
}
