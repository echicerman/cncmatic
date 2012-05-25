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

namespace CNCMatic
{
    public partial class Principal : Form
    {
        Boolean flag=true;
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
        }

        


        private void btnBuscarDXF_Click(object sender, EventArgs e)
        {
            if (importaDXF.ShowDialog() == DialogResult.OK)
            {
                txtFilePathD.Text = importaDXF.FileName;
                
            }
        }

        private void btnImportarDXF_Click(object sender, EventArgs e)
        {
            DxfDoc doc = new DxfDoc();
            doc.Cargar(this.txtFilePathD .Text);

            List<string> sl=Traduce.Lineas(doc.Lineas);
            List<string> sa = Traduce.Arcos(doc.Arcos);

            foreach (string s in sl)
                this.txtGpreview.Text += (s+Environment.NewLine);
            foreach (string s in sa)
                this.txtGpreview.Text += (s + Environment.NewLine);
        }

        private void rdbDXF_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlImportDXF.Enabled = this.rdbDXF.Checked;
            
        }

        private void btnBuscarG_Click(object sender, EventArgs e)
        {
            if (importaG.ShowDialog() == DialogResult.OK)
            {
                txtFilePathG.Text = importaG.FileName;

            }
        }

        private void rdbG_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlImportG.Enabled = this.rdbG.Checked;
        }

        private void btnImportarG_Click(object sender, EventArgs e)
        {
            Importacion imp = new Importacion();
            List<string> lineas = imp.leeGfile(this.txtFilePathG.Text);

            foreach (string s in lineas)
                this.txtGpreview.Text += (s + Environment.NewLine);
        }

        private void gbMovXY_Enter(object sender, EventArgs e)
        {

        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            this.txtPosX.Text = "0"; 
            this.txtPosY.Text = "0";
            this.txtPosZ.Text = "0";

            this.txtPreviewManual.Text += Metodos.IrA(0, 0, 0) + Environment.NewLine; 
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
                this.txtPreviewManual.Text += Metodos.IrA(Configuracion.X_MAX, 0, 0) + Environment.NewLine;
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

        private void btnStop_Click(object sender, EventArgs e)
        {
           this.txtPreviewManual.Text += Metodos.Stop() + Environment.NewLine ; 
        }

        private void btnMovXY_Der_MouseUp(object sender, MouseEventArgs e)
        {
            this.txtPreviewManual.Text += Metodos.Stop() + Environment.NewLine;
            flag = true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.txtPreviewManual.Text = "";
        }
                
    }
}
