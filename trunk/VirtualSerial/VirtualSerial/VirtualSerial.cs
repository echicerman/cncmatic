using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;
using System.Windows.Forms;
using System.Threading;

namespace VirtualSerial
{
    public static class Port
    {
        public delegate void DataReceivedCallbackDelegate(string text);

        #region Private Properties
        private static SerialPort virtualPort = null;
        private static bool connected = false;
        private const string handshake = "Hi CNCMatic";
        private static DataReceivedCallbackDelegate callback;
        #endregion

        #region Getters & Setters
        public static SerialPort VirtualPort
        {
            get
            {
                if (virtualPort != null && virtualPort.IsOpen)
                {
                    return virtualPort;
                }
                else
                {
                    throw new Exception("La máquina no está conectada.");
                }
            }
            set { virtualPort = value; }
        }
        public static bool Connected
        {
            get { return connected; }
            set { connected = value; }
        }
        public static SafeControls.SafeToolStripStatusLabel Label { get; set; }
        public static DataReceivedCallbackDelegate DataReceivedCallback
        {
            get { return callback; }
            set { callback = value; }
        }
        public static string Handshake
        {
            get { return handshake; }
        }
        #endregion

        /// <summary>
        /// Limpia los buffers de IN/OUT y Cierra la conexion
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                VirtualPort.DiscardInBuffer();
                VirtualPort.DiscardOutBuffer();
                VirtualPort.Close();
            }
            catch { }
            finally
            {
                Connected = false;
            }
        }

        static void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            ((System.Timers.Timer)sender).Stop();
            if (!Connected)
            {
                CloseConnection();
                DataReceivedCallback("No se Recibió Callback... closing");
            }
            else
            {
                DataReceivedCallback("Handshake response received OK!");
            }
        }

        /// <summary>
        /// Abre la conexion con el puerto
        /// </summary>
        /// <param name="portName"></param>
        public static void OpenConnection(string portName)
        {
            try
            {
                VirtualPort = new SerialPort(portName);
                virtualPort.Open(); // ojo que TIENE que usar la property directamente y no el metodo

                VirtualPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);

                connected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Conectado(string portName)
        {
            if (virtualPort != null)
            {
                return (virtualPort.PortName == portName && connected);
            }
            else
                return false;

        }

        public static void Connect(string portName)
        {
            try
            {
                VirtualPort = new SerialPort(portName);
                virtualPort.Open(); // ojo que TIENE que usar la property directamente y no el metodo

                VirtualPort.DataReceived += new SerialDataReceivedEventHandler(Port_DataReceived);
                VirtualPort.Write(Handshake);
                
                /*Timer t = new Timer(5000);
                t.Enabled = true;
                t.Start();
                t.Elapsed += new ElapsedEventHandler(t_Elapsed);*/
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }            
        }

        private static void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (Connected)
                {
                    DataReceivedCallback(VirtualPort.ReadExisting());
                }
                else
                {
                    string handshakeResponse = VirtualPort.ReadExisting();
                    Connected = Handshake.Length == int.Parse(handshakeResponse);
                }
            }
            catch(Exception ex)
            {
                CloseConnection();
                
                //Invoke(d, new object[] { text });
                
                Label.Text = "Error: Port." + ex.Message;
                //throw ex;
            }
        }

        /// <summary>
        /// Escribe en el puerto la cadena recibida, en caso de error cierra la conexion
        /// </summary>
        /// <param name="text"></param>
        public static void Write(string text)
        {
            try
            {
                if (!connected)
                    OpenConnection("COM5");
                VirtualPort.Write(text);
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw ex;
            }
        }
    }
}
