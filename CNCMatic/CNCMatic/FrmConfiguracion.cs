using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO.Ports;

namespace CNCMatic
{
    public partial class FrmConfiguracion : Form
    {
        public FrmConfiguracion()
        {
            InitializeComponent();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmConfiguracion_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(s);
            }
        }

        private void CargaMateriales()
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml("xmlDB.xml");

            
            
            //XmlNode nodo=doc.ReadNode();
            

        }
    }
}
