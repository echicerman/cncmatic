using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VirtualSerial;
using System.IO.Ports;

namespace CNCMatic
{
    public partial class FrmComunicacion : Form
    {
        public FrmComunicacion()
        {
          
            InitializeComponent();

            foreach (string s in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(s);
            }
        }

        private void FrmComunicacion_Load(object sender, EventArgs e)
        {

        }
    }
}
