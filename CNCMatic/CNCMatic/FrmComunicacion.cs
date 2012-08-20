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

            buscarPuertos();
        }

        private void FrmComunicacion_Load(object sender, EventArgs e)
        {

        }

        private void buscarPuertos()
        {
            portComboBox.Items.Clear();
            
            foreach (string s in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(s);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            buscarPuertos();
        }

        private void DataReceivedCallback(string text)
        {
            if (receivedTextBox.InvokeRequired)
            {
                Port.DataReceivedCallbackDelegate d = new Port.DataReceivedCallbackDelegate(DataReceivedCallback);
                Invoke(d, new object[] { text });
            }
            else
            {
                receivedTextBox.AppendText("\n" + text);
            }
        }


        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                Port.DataReceivedCallback = new Port.DataReceivedCallbackDelegate(DataReceivedCallback);
                Port.Connect(portComboBox.Items[portComboBox.SelectedIndex].ToString());
                connectButton.Enabled = false;
                disconnectButton.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                Port.CloseConnection();
                disconnectButton.Enabled = false;
                connectButton.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            //string[] comandos;

            //comandos = this.txtPreview.Lines;

            Port.Write(sendTextBox.Text);
            //Port.Write(comandos[i]);
            //i++;
        }
    }
}
