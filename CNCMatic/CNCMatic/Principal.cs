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

namespace CNCMatic
{
    public partial class Principal : Form
    {
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

        
    }
}
