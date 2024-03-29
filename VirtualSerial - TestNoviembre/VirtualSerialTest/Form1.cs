﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace VirtualSerialTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            foreach(string s in SerialPort.GetPortNames())
            {
                portComboBox.Items.Add(s);
            }
        }

        private void DataReceivedCallback(string text)
        {
            if (receivedTextBox.InvokeRequired)
            {
                VirtualSerial.Port.DataReceivedCallbackDelegate d = new VirtualSerial.Port.DataReceivedCallbackDelegate(DataReceivedCallback);
                Invoke(d, new object[] { text });
            }
            else
            {
                receivedTextBox.AppendText(text);
                receivedTextBox.AppendText(Environment.NewLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                VirtualSerial.Port.DataReceivedCallback = new VirtualSerial.Port.DataReceivedCallbackDelegate(DataReceivedCallback);
                VirtualSerial.Port.Connect(portComboBox.Items[portComboBox.SelectedIndex].ToString());
                connectButton.Enabled = false;
                disconnectButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al querer conectar.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                VirtualSerial.Port.CloseConnection();
                disconnectButton.Enabled = false;
                connectButton.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Erro al cerrar la conexión.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VirtualSerial.Port.Write(sendTextBox.Text);
        }

        void sendTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.sendButton.PerformClick();
            }
        }
    }
}
