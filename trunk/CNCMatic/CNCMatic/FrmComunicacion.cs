using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Timers;
using CNC;

namespace CNCMatic
{
    public partial class FrmComunicacion : Form
    {
        public FrmComunicacion()
        {
            InitializeComponent();

            buscarPuertos();
        }

        //private string ultimaInstruccion;
        private System.Timers.Timer timer;
        private void FrmComunicacion_Load(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer(5000);
            timer.Enabled = true;
            //timer.Elapsed += new ElapsedEventHandler(proximaInstruccion);
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

        //private void DataReceivedCallback(string text)
        //{
        //    if (receivedTextBox.InvokeRequired)
        //    {
        //        Port.DataReceivedCallbackDelegate d = new Port.DataReceivedCallbackDelegate(DataReceivedCallback);
        //        Invoke(d, new object[] { text });
        //    }
        //    else
        //    {
        //        receivedTextBox.AppendText("\n" + text);
        //    }
        //}


        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Port.DataReceivedCallback = new Port.DataReceivedCallbackDelegate(DataReceivedCallback);
                //Port.Connect(portComboBox.Items[portComboBox.SelectedIndex].ToString());
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
                //Port.CloseConnection();
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

            //Port.Write(sendTextBox.Text);

        }

        public void IniciarTransmision()
        {

            //conectar("COM1");

            //enviarConfiguracion();

            //enviarInstrucciones();

            //desconectar();


        }

        //private void conectar(string puerto)
        //{
        //    Port.DataReceivedCallback = new Port.DataReceivedCallbackDelegate(DataReceivedCallback);
        //    Port.Connect(puerto);

        //}
        //private void enviarConfiguracion()
        //{

        //}
        //private void enviarInstrucciones()
        //{
        //    //armamos un Timer para el tiempo de espera entre cada instruccion enviada
        //    /*System.Timers.Timer t = new System.Timers.Timer(5000);
        //    t.Enabled = true;
        //    t.Elapsed += new ElapsedEventHandler(proximaInstruccion);
        //    */

        //    this.ultimaInstruccion = (this.Owner as Principal).proximaInstruccion();
        //    //Port.Write(ultimaInstruccion);
        //    timer.Start();

        //    //while (this.ultimaInstruccion != "")
        //    //{


        //    //    //ultimaInstruccion = (this.Owner as Principal).proximaInstruccion();

        //    //    //System.Threading.Thread.Sleep(1000);

        //    //    t.Start();
        //    //}
        //}
        //private void proximaInstruccion(object sender, ElapsedEventArgs e)
        //{

        //    this.ultimaInstruccion = (this.Owner as Principal).proximaInstruccion();

        //    if (ultimaInstruccion != "")
        //    {
        //        //Port.Write(ultimaInstruccion);

        //        ultimaInstruccion = (this.Owner as Principal).proximaInstruccion();

        //        timer.Start();
        //    }
        //    else
        //    {
        //        //desconectar();
        //    }
        //}
        //private void desconectar()
        //{
        //    Port.CloseConnection();
        //}
    }
}
